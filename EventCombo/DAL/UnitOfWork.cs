using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventCombo.Models;
using System.Data.Entity;
using System.Data;

namespace EventCombo.DAL
{

  public interface IUnitOfWork : IDisposable
  {
    void Commit();
    void Rollback();
    DbContext Context { get; }
  }

  public class EntityFrameworkUnitOfWork : IUnitOfWork
  {
    private bool disposed = false;
    private DbContext _context;
    private DbContextTransaction _transaction;

    public EntityFrameworkUnitOfWork(IDbContextFactory factory)
    {
      if (factory == null)
        throw new ArgumentNullException("factory");

      _context = factory.GetContext();
      _transaction = _context.Database.BeginTransaction();
    }

    public DbContext Context 
    { 
      get { return _context; }
    }

    public void Commit()
    {
      _transaction.Commit();
    }

    public void Rollback()
    {
      _transaction.Rollback();
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!this.disposed)
      {
        if (disposing)
        {
          _transaction.Dispose();
        }
      }
      this.disposed = true;
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }
  }
}