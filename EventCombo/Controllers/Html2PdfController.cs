using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace EventCombo.Controllers
{
    public class Html2PdfController : Controller
    {
        // GET: Html2Pdf
        public ActionResult Index()

        {
            GenerateHTML2PDF("", "", "", "", "", "");
            return View();
        }


        public void GenerateHTML2PDF(string strEvent, string strDateTime, string strOrderInfo, string strType, string strLocation, string strPaymentStatus)
        {

            string readFile = "";
            using (StreamReader reader = new StreamReader(Server.MapPath("~/ticketpdf.html")))
            {
                readFile = reader.ReadToEnd();
            }
            iTextSharp.text.html.simpleparser.StyleSheet styles = new iTextSharp.text.html.simpleparser.StyleSheet();


            string myString = "";
            myString = readFile;
            //myString = myString.Replace("¶¶Email¶¶", model.Email);
            //myString = myString.Replace("¶¶Website¶¶", url1);
            System.Text.StringBuilder str = new System.Text.StringBuilder();

            string mystring = "<div style='width: 800px; float: left; background:#d9d9d9; font-family: arial;'>" +
            " <div style='width:80px;float:left;background:#fff;margin:10px;'>" +
            " </div>" +
               "<div style='width:480px;float:left;margin:10px 10px 0 10px;>" +
               "<div style='width:460px;float:left; background:#fff;padding:10px;margin-bottom:10px;'>" +
                "<div style='width:100%;float:left;font-size:18px;color: #555;'>Event</div>" +
                "<h1 style='margin:5px 0;font-size:30px;font-weight:normal;'> Narrow down the road at Luxx down the road at Luxx</h1></div>" +
                "<div style='width:480px;float:left;margin-bottom:10px;'>" +
                 "<div style='width:215px;float:left;background:#fff;padding:10px;margin-right:10px;'>" +
                 "<div style='width: 100%;float:left;font-size:18px;color:#555;'>Date + Time</div>" +
                  "<div style='width: 100%;float:left;font-size:20px;color:#000;margin-top:5px;'> " +
                  "Tuesday, November 17, 2015 from 7:00 PM to 10:00 PM(CST) </div> </div> " +
                  " <div style='width:215px;float:left;background:#fff;padding:10px;'>" +
                  "<div style='width:100%;float:left;font-size:18px;color:#555;'>Location</div> " +
                  "<div style='width:100%;float:left;font-size:20px;color:#000;margin-top:5px;'>Luxy Lashes & Skin 5885 San Felipe Street #Ste 475 Hoston, TX 77057</div> "+
                   "</div></div><div style='width: 460px; float: left; background:#fff; padding:10px; margin-bottom:10px;'>"+
                   "<div style='width: 100%;float: left;font-size:18px;color:#555;'>Event Order</div>"+
                   "<div style='width: 100%;float:left;font-size:20px;color:#000;margin-top: 5px;'>Order # 463464. Ordered by jones on October 10, 2015 9:17PM</div>"+
               " </div ><div style= 'width: 460px; float: left; background:#fff; padding:10px; margin-bottom:10px;' >"+
               " < div style = 'width: 100%; float: left; font-size: 18px; color: #555;' > Type </ div >"+
                "< div  style = 'width: 100%; float: left; font-size: 20px; color: #000; margin-top: 5px;text-align: right;' > This is Free Ticket registration</ div >"+
                 " </ div ></ div >< div style = 'width: 170px; float: left; margin: 10px 0 0 0;' > < div style = 'width: 100%; float: left;  background:#fff; padding: 10px;'>"+
                 ""+
                  "  <div style = 'width: 100%; float: left; font-size: 18px; color: #555; min-height: 210px; position: relative; ' >"+
                   "     < div style='position: absolute; bottom: 0;left: 0;'>Payment Status"+
                  "<div  style= 'width: 100%; float: left; font-size: 20px; color: #000; margin-top: 5px;text-align: center;'> Free Order</div>"+
                   "</div> </div></div> <div style = 'width: 100%; float: left; background:#fff; text-align: center; padding: 10px;  margin-top: 10px;' >"+
                    ""+
                " </ div ></ div ></ div > ";
            str.Append(mystring);
        Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter(str);
        //HtmlTextWriter hw = new HtmlTextWriter(sw);
        //pnlPerson.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();

        }
}
}