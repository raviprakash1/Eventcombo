using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventCombo.Models
{
  public class ECImageViewModel
  {
    public long ECImageId { get; set; }
    public string Filename { get; set; }
    public string ImagePath { get; set; }
    [JsonIgnore]
    public string FilePath { get; set; }
    public byte ECImageTypeId { get; set; }
    public string TypeName { get; set; }
    [JsonIgnore]
    public string UserId { get; set; }
  }
}