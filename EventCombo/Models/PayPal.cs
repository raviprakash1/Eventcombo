﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Net;
using System.IO;
using System.Globalization;
namespace EventCombo.Models
{
    public class PayPal
    {
        public static PayPalRedirect ExpressCheckout(PayPalOrder order)
        {
            NameValueCollection values = new NameValueCollection();

            values["METHOD"] = "SetExpressCheckout";
            values["RETURNURL"] = order.ReturnUrl;
            values["CANCELURL"] = order.CancelUrl;
            values["PAYMENTACTION"] = "SALE";
            values["CURRENCYCODE"] = "USD";
            values["BUTTONSOURCE"] = "PP-ECWizard";
            values["USER"] = PayPalSettings.Username;
            values["PWD"] = PayPalSettings.Password;
            values["SIGNATURE"] = PayPalSettings.Signature;
            values["SUBJECT"] = "";
            values["VERSION"] = "98";
            values["AMT"] = order.Amount.ToString();

            values = Submit(values);

            string ack = values["ACK"].ToLower();

            if (ack == "success" || ack == "successwithwarning")
            {
                return new PayPalRedirect
                {
                    Token = values["TOKEN"],
                    Url = String.Format("https://{0}/cgi-bin/webscr?cmd=_express-checkout&token={1}",
                      PayPalSettings.CgiDomain, values["TOKEN"])
                };
            }
            else
            {
                throw new Exception(values["L_LONGMESSAGE0"]);
            }
        }

        private static NameValueCollection Submit(NameValueCollection values)
        {
            string data = String.Join("&", values.Cast<string>()
              .Select(key => String.Format("{0}={1}", key, HttpUtility.UrlEncode(values[key]))));

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
               String.Format("https://{0}/nvp", PayPalSettings.ApiDomain));

            request.Method = "POST";
            request.ContentLength = data.Length;

            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(data);
            }

            using (StreamReader reader = new StreamReader(request.GetResponse().GetResponseStream()))
            {
                return HttpUtility.ParseQueryString(reader.ReadToEnd());
            }
        }
    }
    public class PayPalOrder
    {
        public decimal? Amount { get; set; }
        public string OrderId { get; set; }
        public string ReturnUrl { get; set; }
        public string CancelUrl { get; set; }
    }

    public class PayPalRedirect
    {
        public string Url { get; set; }
        public string Token { get; set; }
    }
}