using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventCombo.Models
{
  public interface IBaseViewModel
  {
    string BaseTitle { get; set; }
    string BaseUserId { get; set; }
    string BaseUserName { get; set; }
    string BaseUserEmail { get; set; }
  }

  public class BaseViewModel: IBaseViewModel
  {
    public string BaseTitle { get; set; }
    public string BaseUserId { get; set; }
    public string BaseUserName { get; set; }
    public string BaseUserEmail { get; set; }
  }
}