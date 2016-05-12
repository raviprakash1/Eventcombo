﻿using EventCombo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EventCombo.DAL
{
  public interface IDbContextFactory
  {
    DbContext GetContext();
  }

  public class EventComboContextFactory : IDbContextFactory
  {
    private readonly DbContext _context;

    public EventComboContextFactory(DbContext context = null)
    {
      if (context == null)
        _context = new EventComboEntities();
      else
        _context = context;
    }

    public DbContext GetContext()
    {
      return _context;
    }
  }
}