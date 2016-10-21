using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventCombo.DAL;
using EventCombo.Models;
using AutoMapper;
using NLog;
using System.Data.SqlClient;
using System.Data;
using Hangfire;

namespace EventCombo.Service
{
  public class PurchasingService : IPurchasingService
  {
    private IUnitOfWorkFactory _factory;
    private IMapper _mapper;
    private ILogger _logger;
    public static readonly int MaxLockCount = 1;

    public PurchasingService(): this (new EntityFrameworkUnitOfWorkFactory(new EventComboContextFactory(new EventComboEntities())), AutomapperConfig.Config.CreateMapper()) {}

    public PurchasingService(IUnitOfWorkFactory factory, IMapper mapper)
    {
      if (factory == null)
        throw new ArgumentNullException("factory");
      if (mapper == null)
        throw new ArgumentNullException("mapper");

      _factory = factory;
      _mapper = mapper;
      _logger = LogManager.GetCurrentClassLogger();
    }


    public TicketLockResult TryLockTickets(TicketLockRequest req)
    {
      TicketLockResult res = new TicketLockResult();
      using (var uow = _factory.GetUnitOfWork())
        try
        {
          res = CanLock(req);
          if (res.TicketsAvailable)
          {
            if (res.LockCount > 0)
            {
              DeleteOldestLock(req, uow);
              res.LockCount = GetLockedCount(req.EventId, req.IP);
            }
            if (res.LockCount == 0)
            {
              res.LockId = LockTickets(req, uow).ToString();
              uow.Context.SaveChanges();
              uow.Commit();
            }
            else
              uow.Rollback();
          }
          else
            uow.Rollback();
        }
        catch (Exception ex)
        {
          _logger.Error(ex, "Error during TryLockTickets.");
          uow.Rollback();
        }

      return res;
    }

    private Guid LockTickets(TicketLockRequest req, IUnitOfWork uow)
    {
      IRepository<LockOrder> loRepo = new GenericRepository<LockOrder>(_factory.ContextFactory);
      IRepository<Ticket_Quantity_Detail> tqdRepo = new GenericRepository<Ticket_Quantity_Detail>(_factory.ContextFactory);
      LockOrder lOrder = new LockOrder()
      {
        LockOrderId = Guid.NewGuid(),
        EventId = req.EventId,
        IP = req.IP,
        Locktime = DateTime.UtcNow,
        UserId = req.UserId
      };
      foreach (var ticket in req.Tickets)
      {
        var tDB = tqdRepo.GetByID(ticket.TicketQuantityDetailId);
        if (tDB != null)
        {
          LockTicket lTicket = _mapper.Map<LockTicket>(ticket);
          int feeType;
          if (Int32.TryParse(tDB.Ticket.Fees_Type, out feeType))
            lTicket.ECFeeType = feeType;
          lTicket.LockOrderId = lOrder.LockOrderId;
          lTicket.Amount = tDB.Ticket.Price ?? 0;
          lTicket.CustomerFee = tDB.Ticket.Customer_Fee ?? 0;
          lTicket.ECFeeAmount = tDB.Ticket.T_EcAmount ?? 0;
          lTicket.ECFeePercent = (tDB.Ticket.Price ?? 0) * (tDB.Ticket.T_Ecpercent ?? 0) / 100;
          if ((tDB.Ticket.TicketTypeID ?? 0) == 3)
            lTicket.Quantity = 1;
          lOrder.LockTickets.Add(lTicket);
        }
      }
      loRepo.Insert(lOrder);
      uow.Context.SaveChanges();
      BackgroundJob.Schedule<PurchasingService>(ps => ps.DeleteTicketLock(lOrder.LockOrderId.ToString(), req.IP), TimeSpan.FromSeconds(60 * 1 + 20));
      return lOrder.LockOrderId;
    }

    private void DeleteOldestLock(TicketLockRequest req, IUnitOfWork uow)
    {
      IRepository<LockOrder> loRepo = new GenericRepository<LockOrder>(_factory.ContextFactory);
      IRepository<LockTicket> ltRepo = new GenericRepository<LockTicket>(_factory.ContextFactory);
      var lorder = loRepo.Get(filter: (o => (o.EventId == req.EventId) && (o.IP == req.IP)),
                              orderBy: (query => query.OrderBy(o => o.Locktime))).FirstOrDefault();
      if (lorder != null)
      {
        foreach (var ticket in lorder.LockTickets.ToList())
          ltRepo.Delete(ticket);
        loRepo.Delete(lorder);
      }
      uow.Context.SaveChanges();
    }

    public bool DeleteTicketLock(string lockId, string ip)
    {
      bool result = false;
      using (var uow = _factory.GetUnitOfWork())
        try
        {
          result = DeleteTicketLock(lockId, ip, uow);
          if (result)
            uow.Commit();
          else
            uow.Rollback();
        }
        catch (Exception ex)
        {
          _logger.Error(ex, String.Format("Exception during DeleteTicketLock fo lockId = {0}", lockId));
          uow.Rollback();
        }
      return result;
    }

    public bool DeleteTicketLock(string lockId, string ip, IUnitOfWork uow)
    {
      IRepository<LockOrder> loRepo = new GenericRepository<LockOrder>(_factory.ContextFactory);
      IRepository<LockTicket> ltRepo = new GenericRepository<LockTicket>(_factory.ContextFactory);
      var lorder = loRepo.GetByID(lockId);
      if ((lorder == null) || (lorder.IP != ip))
        return false;

      foreach (var ticket in lorder.LockTickets)
        ltRepo.Delete(ticket);
      loRepo.Delete(lorder);
      uow.Context.SaveChanges();
      return true;
    }

    private int GetLockedCount(long eventId, string ip)
    {
      IRepository<LockOrder> loRepo = new GenericRepository<LockOrder>(_factory.ContextFactory);
      return loRepo.Get(s => (s.EventId == eventId) && (s.IP == ip)).Count();
    }

    public TicketLockResult CanLock(TicketLockRequest req)
    {
      TicketLockResult res = new TicketLockResult()
      {
        LockCount = 0,
        LockId = "",
        TicketsAvailable = true,
        Message = ""
      };
      if (GetLockedCount(req.EventId, req.IP) >= MaxLockCount)
      {
        res.LockCount = MaxLockCount;
        res.Message = String.Format("There is {0} tries from your IP. Do you like to clear data about your started orders?", MaxLockCount);
      }
      else
      {
        IRepository<AvailableTicket_View> atRepo = new GenericRepository<AvailableTicket_View>(_factory.ContextFactory);
        IRepository<Event> eRepo = new GenericRepository<Event>(_factory.ContextFactory);
        var e = eRepo.GetByID(req.EventId);
        res.TicketsAvailable = (e != null) && !String.IsNullOrEmpty(e.EventStatus) && (e.EventStatus.ToUpper() == "LIVE");
        if (res.TicketsAvailable)
          foreach (var ticket in req.Tickets)
          {
            res.TicketsAvailable = res.TicketsAvailable && atRepo.Get(t => (t.TQD_Id == ticket.TicketQuantityDetailId) && ((t.RemainingQuantity ?? 0) >= ticket.Quantity)).Any();
          }
      }
      return res;
    }

    public void DeleteExpiredLocks(int minutes)
    {
      _factory.ContextFactory.GetContext().Database.ExecuteSqlCommand("EXEC DeleteExpiredLocks @minutes", new SqlParameter("minutes", SqlDbType.Int) { Value = minutes } );
    }

    public EventPurchaseInfoViewModel GetEventPurchaseInfo(string lockId, string ip)
    {
      EventPurchaseInfoViewModel res = new EventPurchaseInfoViewModel();

      return res;
    }
  }
}