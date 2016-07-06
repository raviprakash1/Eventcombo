using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventCombo.Models
{
  public class ECImageViewModel
  {
    private const string TempPath = "/Images/Temp/";
    private const string PermanentPath = "/Images/ECImages/";
    public long ECImageId { get; set; }
    public string Filename { get; set; }
    public string ImagePath
    {
      get { return String.IsNullOrEmpty(Filename) ? "" : ECImageId == 0 ? TempPath + Filename : PermanentPath + Filename; }
    }
    public string FilePath
    {
      get { return String.IsNullOrEmpty(Filename) ? "" : ECImageId == 0 ? TempPath + Filename : PermanentPath + Filename; }
    }
    public byte ECImageTypeId { get; set; }
    public string TypeName { get; set; }
    public string UserId { get; set; }
  }
}