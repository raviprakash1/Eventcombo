using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCombo.Models;
using System.Collections;
using System.Data;
using System.Text;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace EventCombo.Controllers
{
    public class EventController : Controller
    {
        // GET: Event
        [HttpPost]
        public ActionResult EventCreation(EventCreation model)
        {
           
            return View();
        }

        public ActionResult EventCreation()
        {
            if ((Session["AppId"] != null))
            {
                Session["Fromname"] = "events";
                Session["ReturnUrl"] = null;
                //List<string> ListItems = new List<string>();
                //ListItems.Add("Select");
                //ListItems.Add("India");
                //ListItems.Add("Australia");
                //ListItems.Add("America");
                //ListItems.Add("South Africa");
                //SelectList Countries = new SelectList(ListItems);
                //ViewData["Country"] = Countries;
                //ViewBag.Countries = new SelectList(ListItems);
                string defaultCountry = "";
                using (EventComboEntities db = new EventComboEntities())
                {
                    var countryQuery = (from c in db.Countries
                                        orderby c.Country1 ascending
                                        select c).Distinct();
                    List<SelectListItem> countryList = new List<SelectListItem>();
                    defaultCountry = "United States";

                    foreach (var item in countryQuery)
                    {
                        countryList.Add(new SelectListItem()
                        {
                            Text = item.Country1,
                            Value = item.CountryID.ToString(),
                            Selected = (item.CountryID.ToString().Trim() == defaultCountry.Trim() ? true : false)
                        });
                    }

                    var rows = (from myRow in db.EventTypes
                                select myRow).ToList();
                    List<SelectListItem> EventType = new List<SelectListItem>();
                    EventType.Add(new SelectListItem()
                    {
                        Text = "Select",
                        Value = "0",
                        Selected = true
                    });
                    foreach (var item in rows)
                    {
                        EventType.Add(new SelectListItem()
                        {
                            Text = item.EventType1,
                            Value = item.EventTypeID.ToString(),
                        });
                    }


                    var EventCat = (from myRow in db.EventCategories
                                    select myRow).ToList();
                    List<SelectListItem> EventCategory = new List<SelectListItem>();
                    EventCategory.Add(new SelectListItem()
                    {
                        Text = "Select",
                        Value = "0",
                        Selected = true
                    });
                    foreach (var item in EventCat)
                    {
                        EventCategory.Add(new SelectListItem()
                        {
                            Text = item.EventCategory1,
                            Value = item.EventCategoryID.ToString(),
                        });
                    }








                    ViewBag.CountryID = countryList;
                    ViewBag.EventType = EventType;
                    ViewBag.ddlEventCategory = EventCategory;

                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }
        }

        public long SaveEvent(EventCreation model)
        {
            long lEventId = 0;
            try
            {
                string strUserId = (Session["AppId"] != null ? Session["AppId"].ToString() : "");
                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    Event ObjEC = new Event();

                    ObjEC.EventTypeID = model.EventTypeID;
                    ObjEC.EventCategoryID = model.EventCategoryID;
                    ObjEC.EventSubCategoryID = model.EventSubCategoryID;
                    ObjEC.UserID = strUserId;
                    ObjEC.EventTitle = model.EventTitle;
                    ObjEC.DisplayStartTime = model.DisplayStartTime;
                    ObjEC.DisplayEndTime = model.DisplayEndTime;
                    ObjEC.DisplayTimeZone = model.DisplayTimeZone;
                    ObjEC.EventDescription = model.EventDescription;
                    ObjEC.EventPrivacy = model.EventPrivacy;
                    ObjEC.Private_ShareOnFB = model.Private_ShareOnFB;
                    ObjEC.Private_GuestOnly = model.Private_GuestOnly;
                    ObjEC.Private_Password = model.Private_Password;
                    ObjEC.EventUrl = model.EventUrl;
                    ObjEC.PublishOnFB = model.PublishOnFB;
                    ObjEC.EventStatus = model.EventStatus;
                    ObjEC.IsMultipleEvent = model.IsMultipleEvent;
                    ObjEC.TimeZone = model.TimeZone;
                    ObjEC.DisplayStartTime = model.DisplayStartTime;
                    ObjEC.DisplayEndTime = model.DisplayEndTime;
                    ObjEC.DisplayTimeZone = model.DisplayTimeZone;
                    ObjEC.FBUrl = model.FBUrl;
                    ObjEC.TwitterUrl = model.TwitterUrl;
                    ObjEC.LastLocationAddress = model.LastLocationAddress;
                    ObjEC.AddressStatus = model.AddressStatus;
                    ObjEC.EnableFBDiscussion = model.EnableFBDiscussion;
                    objEnt.Events.Add(ObjEC);
                    // Address info
                    if (model.AddressDetail != null)
                    {
                        Address ObjAdd = new Models.Address();
                        foreach (Address objA in model.AddressDetail)
                        {
                            ObjAdd.EventId = ObjEC.EventID;
                            ObjAdd.Address1 = objA.Address1;
                            ObjAdd.Address2 = objA.Address2;
                            ObjAdd.City = objA.City;
                            ObjAdd.CountryID = objA.CountryID;
                            ObjAdd.State = objA.State;
                            ObjAdd.UserId = strUserId;
                            ObjAdd.VenueName = objA.VenueName;
                            ObjAdd.Zip = objA.Zip;

                            ObjAdd.Name = "";
                            objEnt.Addresses.Add(ObjAdd);

                        }
                    }
                    // Event on Single Timing 
                    if (model.EventVenue != null)
                    {
                        EventVenue objEVenue = new EventVenue();
                        foreach (EventVenue objEv in model.EventVenue)
                        {
                            objEVenue.EventID = ObjEC.EventID;
                            objEVenue.EventStartDate = objEv.EventStartDate;
                            objEVenue.EventEndDate = objEv.EventEndDate;
                            objEVenue.EventStartTime = objEv.EventStartTime;
                            objEVenue.EventEndTime = objEv.EventEndTime;
                            objEnt.EventVenues.Add(objEVenue);
                        }
                    }
                    // Event on Multiple timing 
                    if (model.MultipleEvents != null)
                    {
                        MultipleEvent objMEvents = new MultipleEvent();
                        foreach (MultipleEvent objME in model.MultipleEvents)
                        {
                            objMEvents.EventID = ObjEC.EventID;
                            objMEvents.Frequency = objME.Frequency;
                            objMEvents.WeeklyDay = objME.WeeklyDay;
                            objMEvents.MonthlyDay = objME.MonthlyDay;
                            objMEvents.MonthlyWeek = objME.MonthlyWeek;
                            objMEvents.MonthlyWeekDays = objME.MonthlyWeekDays;
                            objMEvents.StartingFrom = objME.StartingFrom;
                            objMEvents.StartingTo = objME.StartingTo;
                            objMEvents.StartTime = objME.StartTime;
                            objMEvents.EndTime = objME.EndTime;
                            objEnt.MultipleEvents.Add(objMEvents);
                        }
                    }
                    // Orgnizer
                    if (model.Orgnizer != null)
                    {
                        Event_Orgnizer_Detail objEOrg = new Event_Orgnizer_Detail();
                        foreach (Event_Orgnizer_Detail objOr in model.Orgnizer)
                        {
                            objEOrg.Orgnizer_Event_Id = ObjEC.EventID;
                            objEOrg.Orgnizer_Name = objOr.Orgnizer_Name;
                            objEOrg.Orgnizer_Desc = objOr.Orgnizer_Desc;
                            objEnt.Event_Orgnizer_Detail.Add(objEOrg);
                        }
                    }
                    objEnt.SaveChanges();
                    lEventId = ObjEC.EventID;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return lEventId;
        }

        public ActionResult returnfees()
        {
            List<Fees> eList = new List<Fees>();
            EventComboEntities objEnt = new EventComboEntities();


            var rows = (from myRow in objEnt.Fee_Structure
                        select myRow).ToList();

            return Json(rows.ToArray(), JsonRequestBehavior.AllowGet);

        }




        //public string Dictionary<string,string> Test()
        //{


        //}

        public JsonResult GetEventType()
        {
            JsonResult objJs = new JsonResult();
            try
            {
                List<EventType> eList = new List<EventType>();
                EventComboEntities objEnt = new EventComboEntities();
                var rows = (from myRow in objEnt.EventTypes
                            select myRow).ToList();
                return Json(rows, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return objJs;

            }
        }
        public string GetSubCat(long lECatId)
        {

            StringBuilder strHtml = new StringBuilder();
            try
            {
                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    var EventCat = (from myRow in objEnt.EventSubCategories
                                    where myRow.EventCategoryID == lECatId
                                    select myRow).ToList();

                    //strHtml.Append("< option value =0 selected=true>Select</ option > ");
                    foreach (var item in EventCat)
                        strHtml.Append("<option value=" + item.EventSubCategoryID.ToString() + ">" + item.EventSubCategory1 + "</option>");

                    return strHtml.ToString();
                }
            }
            catch (Exception ex)
            {
                return strHtml.ToString();

            }


        }

        public string CheckEventUrl(string strUserUrl)
        {

            try
            {
                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    var Eventurl = (from myRow in objEnt.Events
                                    where myRow.EventUrl == strUserUrl
                                    select myRow).SingleOrDefault();

                    if (Eventurl == null) return "N";
                    if (Eventurl.ToString().Equals(string.Empty))
                        return "N"; // Not Exists
                    else
                        return "Y"; // Exists
                }
            }
            catch (Exception ex)
            {
                return "Y";

            }


        }

        public string GetPreviousAddress()
        {
            string strUsers = (Session["AppId"] != null ? Session["AppId"].ToString() : "");

            StringBuilder strHtml = new StringBuilder();
            try
            {
                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    var PrevAdd = (from myRow in objEnt.Addresses
                                    where myRow.UserId == strUsers
                                    select myRow).ToList();

                    //strHtml.Append("< option value =0 selected=true>Select</ option > ");
                    foreach (var item in PrevAdd)
                        strHtml.Append("<option value=" + item.AddressID.ToString() + ">" + item.VenueName + "," + item.Address1 + "," + item.Address2 + "," + item.City + "," + item.Zip + "</option>");

                    return strHtml.ToString();
                }
            }
            catch (Exception ex)
            {
                return strHtml.ToString();

            }


        }




        //public JsonResult GetEventType()
        //{
        //    List<EventType> objETL = new List<EventType>();
        //    //List<KeyValuePair<string, string>> objList = new List<KeyValuePair<string, string>>();
        //    try
        //    {


        //    using (EventComboEntities objEntity = new EventComboEntities())
        //    {
        //            //IEnumerable<SelectListItem> EventTypeList = (from ET in objEntity.EventTypes select ET).AsEnumerable().Select(ET => new SelectListItem() { Text = ET.EventType1, Value = ET.EventTypeID.ToString() });

        //            //SelectList objSL = new SelectList(EventTypeList, "Value", "Text");
        //            var EventList = objEntity.EventTypes.SqlQuery("Select EventType as 'EventType1',EventTypeID from EventType").ToList();
        //            //EventCategories.SqlQuery("Select * from EventCategory").ToList<Event>




        //            //var EventList = (from Perm in objEntity.EventTypes
        //            //                 select new EventType
        //            //                 {
        //            //                     EventTypeID = Perm.EventTypeID,
        //            //                     EventType1 = Perm.EventType1
        //            //                 }
        //            //            );

        //            //JsonResult j = Json(modelPerm, JsonRequestBehavior.AllowGet);
        //            //objETL = modelPerm.ToList();
        //            return  Json(EventList.ToArray(),JsonRequestBehavior.AllowGet);

        //        //return modelPerm.ToList();

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        return Json("Result", JsonRequestBehavior.AllowGet); ;
        //    }
        //}

       

        protected ImageFormat getImageFormat(String path)
        {
            switch (Path.GetExtension(path))
            {
                case ".bmp": return ImageFormat.Bmp;
                case ".gif": return ImageFormat.Gif;
                case ".jpg": return ImageFormat.Jpeg;
                case ".png": return ImageFormat.Png;
                default: break;
            }
            return ImageFormat.Jpeg;
        }
        private void HandleImageUpload(HttpPostedFileBase file, string path1)
        {//ProfileID_SequentialImage#
            Image img = RezizeImage(Image.FromStream(file.InputStream), 900, 600);
            string path = file.FileName;
            img.Save(path1, getImageFormat(path));
        }
        private Image RezizeImage(Image img, int maxWidth, int maxHeight)
        {
            if (img.Height < maxHeight && img.Width < maxWidth) return img;
            using (img)
            {
                Double xRatio = (double)img.Width / maxWidth;
                Double yRatio = (double)img.Height / maxHeight;
                Double ratio = Math.Max(xRatio, yRatio);
                int nnx = (int)Math.Floor(img.Width / ratio);
                int nny = (int)Math.Floor(img.Height / ratio);
                Bitmap cpy = new Bitmap(nnx, nny, PixelFormat.Format32bppArgb);
                cpy.SetResolution(72, 72);
                using (Graphics gr = Graphics.FromImage(cpy))
                {
                    gr.Clear(Color.Transparent);

                    // This is said to give best quality when resizing images
                    gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    gr.DrawImage(img,
                        new Rectangle(0, 0, nnx, nny),
                        new Rectangle(0, 0, img.Width, img.Height),
                        GraphicsUnit.Pixel);
                }
                return cpy;
            }

        }


        public class Fees
        {
            public string Feetype { get; set; }
            public string FeeAmount { get; set; }
            public string TotalAmount { get; set; }
        }
        public JsonResult SaveUploadedFile(string Uniqueid)
        {
            if (Session["AppId"] != null)
            {
                Random rnd = new Random();
                var rndnumber = rnd.Next(1, 7);
                //string Name = Request.Form[1];
                //if (Request.Files.Count > 0)
                //{
                //    HttpPostedFileBase file = Request.Files[0];
                //}
                string Userid = Session["AppId"].ToString();
                bool isSavedSuccessfully = true;
                string fName = "";
                string content_type = "";
                var pathnew = "";
                try
                {
                    foreach (string fileName in Request.Files)
                    {
                        HttpPostedFileBase file = Request.Files[fileName];
                        //Save file content goes here
                        fName = file.FileName;
                        if (file != null && file.ContentLength > 0)
                        {
                            fName = file.FileName;
                            content_type = file.ContentType;
                            var originalDirectory = new DirectoryInfo(string.Format("{0}\\Images\\events\\event_flyers", Server.MapPath(@"\")));

                            string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "imagepath");

                            var fileName1 = Path.GetFileName(file.FileName);

                            //var format=    getImageFormat(fileName1);

                            bool isExists = System.IO.Directory.Exists(pathString);

                            if (!isExists)
                                System.IO.Directory.CreateDirectory(pathString);

                            var path = string.Format("{0}\\{1}", pathString, file.FileName);
                            var imageformat = getImageFormat(path);
                            var NFilename = file.FileName;
                            pathnew = string.Format("{0}\\{1}", pathString, NFilename);
                            //using (EventComboEntities objEntity = new EventComboEntities())
                            //{
                            //    EventTempImage Imageold = objEntity.EventTempImages.FirstOrDefault(i => i.EvenUniqueid == Uniqueid);



                            //    //profile.UserProfileImage = fName;
                            //    //Image.ContentType = content_type;

                            //        EventTempImage Image = new EventTempImage();
                            //        Image.EventImageUrl = NFilename;
                            //        Image.EvenUniqueid = Uniqueid;
                            //        Image.ImageType = content_type;
                            //        Image.UserId = Userid;
                            //        objEntity.EventTempImages.Add(Image);
                            //        objEntity.SaveChanges();


                            //}
                            // file.SaveAs(path);
                            HandleImageUpload(file, pathnew);
                        }

                    }

                }
                catch (Exception ex)
                {
                    isSavedSuccessfully = false;
                }


                if (isSavedSuccessfully)
                {
                    return Json(new { image_name = fName, image_type = content_type, image_path = pathnew });
                }
                else
                {
                    return Json(new { Message = "Error in saving file" });
                }
            }
            else
            {
                return Json(new { Message = "Error in saving file" });

            }
        }
        //public void SaveTable(EventCreation model)
        //{


        //}

    }

}