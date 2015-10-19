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
    public class CreateEventController : Controller
    {
        // GET: Event
        [HttpPost]
        public ActionResult CreateEvent(EventCreation Model)
        {
            Session["Fromname"] = "events";
            return View();
        }

        public ActionResult CreateEvent()
        {
            if ((Session["AppId"] != null))
            {
                Session["Fromname"] = "events";
                Session["ReturnUrl"] = null;
          
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

    }
}