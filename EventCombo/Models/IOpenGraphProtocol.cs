using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCombo.Models
{
  public interface IOpenGraphProtocol
  {
    string OGPTitle { get; set; }
    string OGPType { get; set; }
    string OGPUrl { get; set; }
    string OGPImage { get; set; }
    string OGPDescription { get; set; }
  }
}
