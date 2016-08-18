using EventCombo.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventCombo
{
  public class HangfirePreload : System.Web.Hosting.IProcessHostPreloadClient
  {
    public void Preload(string[] parameters)
    {
      HangfireBootstrapper.Instance.Start();
    }
  }
}