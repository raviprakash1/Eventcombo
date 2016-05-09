using EventCombo.DAL;
using EventCombo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace EventCombo.Service
{
  public class ManageEventService: IManageEventService
  {
    private IUnitOfWorkFactory _factory;

    public ManageEventService(IUnitOfWorkFactory factory)
    {
      if (factory == null)
        throw new ArgumentNullException("factory");
      _factory = factory;
    }

    public int GetPromoCodeCount(long eventId)
    {
      IRepository<Promo_Code> rep = new GenericRepository<Promo_Code>(_factory.ContextFactory);
      return rep.Get(filter: (pc => pc.PC_Eventid == eventId)).Count();
    }
  }
}