using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Net;
using System.IO;
using System.Globalization;
using System.Text;

namespace EventCombo.Models
{
    public class PayPal
    {
        public static PayPalRedirect ExpressCheckout(PayPalOrder order)
        {
            NameValueCollection values = new NameValueCollection();
            //   string strGUID = (Session["TicketLockedId"] != null ? Session["TicketLockedId"].ToString() : "");
            //values["CANCELURL"] = PayPalSettings.CancelUrl;

            values["METHOD"] = "SetExpressCheckout";
            values["RETURNURL"] = PayPalSettings.ReturnUrl;
            values["CANCELURL"] = order.CancelUrl;
            values["PAYMENTACTION"] = "SALE";
            values["CURRENCYCODE"] = "USD";
            values["BUTTONSOURCE"] = "PP-ECWizard";
            values["USER"] = PayPalSettings.Username;
            values["PWD"] = PayPalSettings.Password;
            values["SIGNATURE"] = PayPalSettings.Signature;
            values["SUBJECT"] = "";
            values["VERSION"] = "93";
            values["BRANDNAME"] = "Eventcombo.com";
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
              .Select(key => String.Format("{0}={1}", key, values[key])));
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("https://{0}/nvp", PayPalSettings.ApiDomain));
            request.Method = "POST";
            request.KeepAlive = false;
            byte[] byteArray = Encoding.UTF8.GetBytes(data);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            using (StreamReader reader = new StreamReader(request.GetResponse().GetResponseStream()))
            {
                return HttpUtility.ParseQueryString(reader.ReadToEnd());
            }

        }
        public static bool GetCheckoutDetails(string token, ref string PayerID, ref string retMsg)
        {
            NameValueCollection values = new NameValueCollection();

            values["METHOD"] = "GetExpressCheckoutDetails";
            values["TOKEN"] = token;
            values["USER"] = PayPalSettings.Username;
            values["PWD"] = PayPalSettings.Password;
            values["SIGNATURE"] = PayPalSettings.Signature;
            values["SUBJECT"] = "";
            values["VERSION"] = "93";
            values = Submit(values);


            string ack = values["ACK"].ToLower();

            if (ack == "success" || ack == "successwithwarning")
            {

                PayerID = values["PayerID"];
                return true;
            }
            else
            {
                retMsg = "ErrorCode=" + values["L_ERRORCODE0"] + "&" +
                    "Desc=" + values["L_SHORTMESSAGE0"] + "&" +
                    "Desc2=" + values["L_LONGMESSAGE0"];
                return false;
            }

        }

        public static bool DoCheckoutPayment(string finalPaymentAmount, string token, string PayerID, ref string retMsg)
        {
            NameValueCollection values = new NameValueCollection();

            values["METHOD"] = "DoExpressCheckoutPayment";
            values["TOKEN"] = token;
            values["PAYERID"] = PayerID;
            values["PAYMENTREQUEST_0_AMT"] = finalPaymentAmount;
            values["PAYMENTREQUEST_0_CURRENCYCODE"] = "USD";
            values["PAYMENTREQUEST_0_PAYMENTACTION"] = "Sale";
            values["USER"] = PayPalSettings.Username;
            values["PWD"] = PayPalSettings.Password;
            values["SIGNATURE"] = PayPalSettings.Signature;
            values["SUBJECT"] = "";
            values["VERSION"] = "93";
            values = Submit(values);


            string ack = values["ACK"].ToLower();

            if (ack == "success" || ack == "successwithwarning")
            {
                retMsg = values["PAYMENTINFO_0_TRANSACTIONID"].ToString(); ;
                return true;
            }
            else
            {
                retMsg = "ErrorCode=" + values["L_ERRORCODE0"] + "&" +
                    "Desc=" + values["L_SHORTMESSAGE0"] + "&" +
                    "Desc2=" + values["L_LONGMESSAGE0"];
                return false;
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