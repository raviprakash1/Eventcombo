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
            styles.LoadStyle("evnt-tkt-container", "width", "800px");
            string myString = "";       
            myString = readFile;        
            //myString = myString.Replace("¶¶Email¶¶", model.Email);
            //myString = myString.Replace("¶¶Website¶¶", url1);
            System.Text.StringBuilder str = new System.Text.StringBuilder();

            //string mystring = "<div><table border='5'><tr><th rowspan = '4';></th><th>" + strDateTime + "Narrow down the road at Luxx </th><th rowspan = '2' ></th ></tr><tr><td>sda</td></tr><tr><td> Order #462744270. Ordered by bubs jones on October 10, 2015 9:17 PM</td><td>QR Code</td></tr><tr><td>This is a free ticket registration</td><td>QR Code</td></tr></table></ div >";
            str.Append(myString);
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