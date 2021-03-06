﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Linq.Expressions;
using EventCombo.Models;
using System.Data.Entity.Infrastructure;

namespace EventCombo.DAL
{
  interface IRepository<TEntity> where TEntity : class
  {
    IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
    TEntity GetByID(object id);
    void Insert(TEntity entity);
    void Update(TEntity entityToUpdate);
    void Delete(object id);
    void Delete(TEntity entityToDelete);
    void Reload(TEntity entityToReload);
    IEnumerable<TEntity> SQLQuery(string sql, params object[] parameters);
  }

  public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
  {
    private DbContext _context;
    private DbSet<TEntity> _dbSet;

    public GenericRepository(IDbContextFactory factory)
    {
      if (factory == null)
        throw new ArgumentNullException("factory");
      this._context = factory.GetContext();
      this._dbSet = _context.Set<TEntity>();
    }

    public GenericRepository(DbContext context)
    {
      if (context == null)
        throw new ArgumentNullException("context");
      this._context = context;
      this._dbSet = _context.Set<TEntity>();
    }

    public virtual IEnumerable<TEntity> Get(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "")
    {
      IQueryable<TEntity> query = _dbSet;

      if (filter != null)
      {
        query = query.Where(filter);
      }

      foreach (var includeProperty in includeProperties.Split
          (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
      {
        query = query.Include(includeProperty);
      }

      if (orderBy != null)
      {
        return orderBy(query).ToList();
      }
      else
      {
        return query.ToList();
      }
    }

    public virtual TEntity GetByID(object id)
    {
      if (id == null)
        throw new ArgumentNullException("id");

      return _dbSet.Find(id);
    }

    public virtual void Insert(TEntity entity)
    {
      if (entity == null)
        throw new ArgumentNullException("entity");

      _dbSet.Add(entity);
    }

    public virtual void Delete(object id)
    {
      if (id == null)
        throw new ArgumentNullException("id");

      TEntity entityToDelete = _dbSet.Find(id);
      Delete(entityToDelete);
    }

    public virtual void Delete(TEntity entityToDelete)
    {
      if (entityToDelete == null)
        throw new ArgumentNullException("entityToDelete");

      if (_context.Entry(entityToDelete).State == EntityState.Detached)
      {
        _dbSet.Attach(entityToDelete);
      }
      _dbSet.Remove(entityToDelete);
    }

    public virtual void Update(TEntity entityToUpdate)
    {
      if (entityToUpdate == null)
        throw new ArgumentNullException("entityToUpdate");

      _dbSet.Attach(entityToUpdate);
      _context.Entry(entityToUpdate).State = EntityState.Modified;
    }
 
    public virtual  void Reload(TEntity entityToReload)
    {
      _context.Entry(entityToReload).Reload();
    }

    public IEnumerable<TEntity> SQLQuery(string sql, params object[] parameters)
    {
      return _context.Database.SqlQuery<TEntity>(sql, parameters);
    }

  }

}