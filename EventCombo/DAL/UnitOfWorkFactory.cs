using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventCombo.DAL
{
  public interface IUnitOfWorkFactory
  {
    IUnitOfWork GetUnitOfWork();
    IDbContextFactory ContextFactory { get; }
  }

  public class EntityFrameworkUnitOfWorkFactory: IUnitOfWorkFactory
  {
    private IDbContextFactory _factory;

    public EntityFrameworkUnitOfWorkFactory(IDbContextFactory factory)
    {
      if (factory == null)
        throw new ArgumentNullException("factory");

      _factory = factory;
    }

    public IUnitOfWork GetUnitOfWork()
    {
      return new EntityFrameworkUnitOfWork(_factory);
    }

    public IDbContextFactory ContextFactory
    {
      get { return _factory; }
    }
  }

}