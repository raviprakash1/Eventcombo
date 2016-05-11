using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace EventCombo.Service
{
  public static class HtmlProcessing
  {
    static Regex _htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

    public static string StripTagsRegex(string source)
    {
      return _htmlRegex.Replace(source, string.Empty);
    }

    public static string GetShortString(string source, int minLength, int maxLength, string delimiter)
    {
      string res = source.Substring(0, maxLength);
      string append = "";
      int pos = res.LastIndexOf(delimiter);
      if (pos < minLength)
      {
        pos = res.LastIndexOf(' ');
        if (pos < minLength)
          pos = maxLength - 4;
        else
          pos = pos - 1;
        append = "...";
      }
      return res.Substring(0, pos + 1) + append;
    }
  }
}