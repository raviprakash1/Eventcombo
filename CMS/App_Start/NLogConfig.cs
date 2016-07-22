using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLog;
using System.Configuration;

namespace CMS
{
  public static class NLogConfig
  {
    public static void Configure()
    {
      var mailconfigs = LogManager.Configuration.AllTargets
        .Where(t => ((t is NLog.Targets.Wrappers.AsyncTargetWrapper) && ((t as NLog.Targets.Wrappers.AsyncTargetWrapper).WrappedTarget is NLog.Targets.MailTarget)))
        .Select(t => (NLog.Targets.MailTarget)(t as NLog.Targets.Wrappers.AsyncTargetWrapper).WrappedTarget);
      foreach(var mc in mailconfigs)
      {
        mc.SmtpServer = ConfigurationManager.AppSettings["Host"];
        mc.SmtpPort = int.Parse(ConfigurationManager.AppSettings["Port"]);
        mc.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
        mc.SmtpUserName = ConfigurationManager.AppSettings["UserName"];
        mc.SmtpPassword = ConfigurationManager.AppSettings["Password"];
        mc.SmtpAuthentication = NLog.Targets.SmtpAuthenticationMode.Basic;
        mc.From = ConfigurationManager.AppSettings["DefaultEmail"];
      }
      LogManager.ReconfigExistingLoggers();
    }
  }
}