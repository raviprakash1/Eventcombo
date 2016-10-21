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
      if (String.IsNullOrEmpty(source))
        return source;
      return _htmlRegex.Replace(source, string.Empty);
    }

    public static string PrepareString(string source)
    {
      if (String.IsNullOrEmpty(source))
        return source;
      return HttpUtility.JavaScriptStringEncode(
        StripTagsRegex(
          HttpUtility.HtmlDecode(
            source.Replace("\n", String.Empty)
            .Replace("\r", String.Empty)
            .Replace("\t", String.Empty))));
    }

    public static string GetShortString(string source, int minLength, int maxLength, string delimiter)
    {
      if (String.IsNullOrEmpty(source))
        return source;
      delimiter = String.IsNullOrEmpty(delimiter) ? "" : delimiter;
      if (minLength > maxLength)
        throw new ArgumentException("Wrong minimum and maximum lengths.");
      if (source.Length <= maxLength)
        return source;
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

    public static string ResolveServerUrl(string serverUrl, bool forceHttps)
    {
      if (serverUrl.IndexOf("://") > -1)
        return serverUrl;

      string newUrl = serverUrl;
      Uri originalUri = System.Web.HttpContext.Current.Request.Url;
      newUrl = (forceHttps ? "https" : originalUri.Scheme) +
          "://" + originalUri.Authority + newUrl;
      return newUrl;
    }

    public static string PrepareForUrl(string str, int maxLength)
    {
      if (String.IsNullOrEmpty(str))
        return str;
      string res = System.Text.RegularExpressions.Regex.Replace(str.Trim().ToLower().Replace(" ", "-"), "[^a-zA-Z0-9_-]+", "");
      res = res.Substring(0, Math.Min(res.Length, maxLength));
      return res;
    }

  }
}
