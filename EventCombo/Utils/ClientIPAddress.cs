using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventCombo.Utils
{
    public static class ClientIPAddress
    {
        public static String GetLanIPAddress(HttpRequestBase httpRequest)
        {
            string strIpAddress;
            strIpAddress = httpRequest.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (strIpAddress == null)
            {
                strIpAddress = httpRequest.ServerVariables["REMOTE_ADDR"];
            }
            return strIpAddress;
        }
    }
}