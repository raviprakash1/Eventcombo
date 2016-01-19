using EventCombo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventCombo.Controllers
{
    public class ManageEventController : Controller
    {
        // GET: ManageEvent
        public ActionResult Index(long Eventid)
        {

            Session["Fromname"] = "events";
            return View();
        }

        public ActionResult CopyEvent(long Eventid)
        {
            TempData["EventId"] = Eventid.ToString();

            return View();
        }

        public long SaveEvent(long Eventid,string strEventTitle)
        {
            try
            {
                //PayPalRedirect redirect = PayPal.ExpressCheckout(new PayPalOrder { Amount = 50 });

                //Session["token"] = redirect.Token;
                //Response.Redirect(redirect.Url);

                //RedirectResult(redirect.Url);

                string strUserId = (Session["AppId"] != null ? Session["AppId"].ToString() : "");
                EventComboEntities objEntDup = new EventComboEntities();

                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    var vEvent = (from myEnt in objEnt.Events where myEnt.EventID == Eventid select myEnt).FirstOrDefault();
                    //vEvent.EventTitle = strEventTitle;
                    //vEvent.EventStatus = "Save";
                    //vEvent.EventID = 0;
                    //objEnt.Events.Add(vEvent);
                    if (strEventTitle.Trim().Equals("")) strEventTitle = vEvent.EventTitle;
                    Event ObjEC = new Event();
                    ObjEC.EventTypeID = vEvent.EventTypeID;
                    ObjEC.EventCategoryID = vEvent.EventCategoryID;
                    ObjEC.EventSubCategoryID = vEvent.EventSubCategoryID;
                    ObjEC.UserID = strUserId;
                    ObjEC.EventTitle  = strEventTitle;
                    ObjEC.DisplayStartTime = vEvent.DisplayStartTime;
                    ObjEC.DisplayEndTime = vEvent.DisplayEndTime;
                    ObjEC.DisplayTimeZone = vEvent.DisplayTimeZone;
                    ObjEC.EventDescription = vEvent.EventDescription;
                    ObjEC.EventPrivacy = vEvent.EventPrivacy;
                    ObjEC.Private_ShareOnFB = vEvent.Private_ShareOnFB;
                    ObjEC.Private_GuestOnly = vEvent.Private_GuestOnly;
                    ObjEC.Private_Password = vEvent.Private_Password;
                    ObjEC.EventUrl = vEvent.EventUrl;
                    ObjEC.PublishOnFB = vEvent.PublishOnFB;
                    ObjEC.IsMultipleEvent = vEvent.IsMultipleEvent;
                    ObjEC.TimeZone = vEvent.TimeZone;
                    ObjEC.DisplayStartTime = vEvent.DisplayStartTime;
                    ObjEC.DisplayEndTime = vEvent.DisplayEndTime;
                    ObjEC.DisplayTimeZone = vEvent.DisplayTimeZone;
                    ObjEC.FBUrl = vEvent.FBUrl;
                    ObjEC.TwitterUrl = vEvent.TwitterUrl;
                    ObjEC.LastLocationAddress = vEvent.LastLocationAddress;
                    ObjEC.AddressStatus = vEvent.AddressStatus;
                    ObjEC.EnableFBDiscussion = vEvent.EnableFBDiscussion;
                    ObjEC.Ticket_DAdress = vEvent.Ticket_DAdress;
                    ObjEC.Ticket_showremain = vEvent.Ticket_showremain;
                    ObjEC.Ticket_showvariable = vEvent.Ticket_showvariable;
                    ObjEC.Ticket_variabledesc = vEvent.Ticket_variabledesc;
                    ObjEC.Ticket_variabletype = vEvent.Ticket_variabletype;
                    ObjEC.ShowMap = vEvent.ShowMap;
                    ObjEC.Parent_EventID = 0;
                    ObjEC.CreateDate = DateTime.Now;
                    ObjEC.EventStatus = "Save";
                    objEnt.Events.Add(ObjEC);

                    var vAddress = (from myEnt in objEnt.Addresses where myEnt.EventId == Eventid select myEnt).ToList();
                    if (vAddress != null)
                    {
                        Models.Address ObjAdd = new Models.Address();
                        foreach (Address objA in vAddress)
                        {
                            ObjAdd = new Models.Address();
                            ObjAdd.EventId = ObjEC.EventID;
                            ObjAdd.Address1 = objA.Address1 == null ? "" : objA.Address1;
                            ObjAdd.Address2 = objA.Address2 == null ? "" : objA.Address2;
                            ObjAdd.City = objA.City == null ? "" : objA.City;
                            ObjAdd.CountryID = objA.CountryID;
                            ObjAdd.State = objA.State == null ? "" : objA.State;
                            ObjAdd.UserId = strUserId;
                            ObjAdd.VenueName = objA.VenueName;
                            ObjAdd.Zip = objA.Zip == null ? "" : objA.Zip;
                            ObjAdd.ConsolidateAddress = objA.ConsolidateAddress;
                            ObjAdd.Name = "";
                            objEnt.Addresses.Add(ObjAdd);
                        }
                        //foreach (Address objAdd in vAddress)
                        //{
                        //    objAdd.EventId = ObjEC.EventID;
                        //    objEnt.Addresses.Add(objAdd);
                        //}
                     }
                    var vEventVenue = (from myEnt in objEnt.EventVenues where myEnt.EventID == Eventid select myEnt).ToList();
                    if (vEventVenue != null)
                    {
                        EventVenue objEVenue = new EventVenue();
                        foreach (EventVenue objEv in vEventVenue)
                        {
                            objEVenue = new EventVenue();
                            objEVenue.EventID = ObjEC.EventID;
                            objEVenue.EventStartDate = objEv.EventStartDate;
                            objEVenue.EventEndDate = objEv.EventEndDate;
                            objEVenue.EventStartTime = objEv.EventStartTime;
                            objEVenue.EventEndTime = objEv.EventEndTime;
                            objEnt.EventVenues.Add(objEVenue);
                        }
                    }

                    var vEventAddress = (from myEnt in objEnt.MultipleEvents where myEnt.EventID == Eventid select myEnt).ToList();
                    if (vEventAddress != null)
                    {
                        MultipleEvent objMEvents = new MultipleEvent();
                        foreach (MultipleEvent objME in vEventAddress)
                        {
                            objMEvents = new MultipleEvent();
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


                    var vOrgnizer = (from myEnt in objEnt.Event_Orgnizer_Detail where myEnt.Orgnizer_Event_Id == Eventid select myEnt).ToList();
                    if (vOrgnizer != null)
                    {
                        Event_Orgnizer_Detail objEOrg = new Event_Orgnizer_Detail();
                        foreach (Event_Orgnizer_Detail objOr in vOrgnizer)
                        {
                            objEOrg = new Event_Orgnizer_Detail();
                            objEOrg.Orgnizer_Event_Id = ObjEC.EventID;
                            objEOrg.Orgnizer_Name = objOr.Orgnizer_Name;
                            objEOrg.Orgnizer_Desc = objOr.Orgnizer_Desc;
                            objEOrg.FBLink = objOr.FBLink;
                            objEOrg.Twitter = objOr.Twitter;
                            objEOrg.DefaultOrg = objOr.DefaultOrg;
                            objEOrg.Linkedin = objOr.Linkedin;
                            objEOrg.UserId = strUserId;
                            objEnt.Event_Orgnizer_Detail.Add(objEOrg);
                        }
                    }

                    var vTicket = (from myEnt in objEnt.Tickets where myEnt.E_Id == Eventid select myEnt).ToList();
                    if (vTicket != null)
                    {
                        Ticket ticket = new Ticket();
                        foreach (Ticket tick in vTicket)
                        {
                            ticket = new Ticket();
                            ticket.E_Id = ObjEC.EventID;
                            ticket.TicketTypeID = tick.TicketTypeID;
                            ticket.T_name = tick.T_name;
                            ticket.Qty_Available = tick.Qty_Available;
                            ticket.Price = tick.Price;
                            ticket.T_Desc = tick.T_Desc;
                            ticket.TicketTypeID = tick.TicketTypeID;
                            ticket.T_order = tick.T_order;
                            ticket.Show_T_Desc = tick.Show_T_Desc;
                            ticket.Fees_Type = tick.Fees_Type;
                            ticket.Sale_Start_Date = tick.Sale_Start_Date;
                            ticket.Sale_Start_Time = tick.Sale_Start_Time;
                            ticket.Sale_End_Date = tick.Sale_End_Date;
                            ticket.Sale_End_Time = tick.Sale_End_Time;
                            ticket.Hide_Ticket = tick.Hide_Ticket;
                            ticket.Auto_Hide_Sche = tick.Auto_Hide_Sche;
                            ticket.T_AutoSechduleType = tick.T_AutoSechduleType;
                            ticket.Hide_Untill_Date = tick.Hide_Untill_Date;
                            ticket.Hide_Untill_Time = tick.Hide_Untill_Time;
                            ticket.Hide_After_Date = tick.Hide_After_Date;
                            ticket.Hide_After_Time = tick.Hide_After_Time;
                            ticket.Min_T_Qty = tick.Min_T_Qty;
                            ticket.Max_T_Qty = tick.Max_T_Qty;
                            ticket.T_Disable = tick.T_Disable;
                            ticket.T_Mark_SoldOut = tick.T_Mark_SoldOut;
                            ticket.EC_Fee = tick.EC_Fee;
                            ticket.Customer_Fee = tick.Customer_Fee;
                            ticket.TotalPrice = tick.TotalPrice;
                            ticket.T_Discount = tick.T_Discount;
                            objEnt.Tickets.Add(ticket);
                        }
                    }



                    var vImage = (from myEnt in objEnt.EventImages where myEnt.EventID == Eventid select myEnt).ToList();
                    if (vImage != null)
                    {
                        EventImage Image = new EventImage();
                        foreach (EventImage img in vImage)
                        {
                            Image = new EventImage();
                            Image.EventID = ObjEC.EventID;
                            Image.EventImageUrl = img.EventImageUrl;
                            Image.ImageType = img.ImageType;
                            objEnt.EventImages.Add(Image);
                        }
                    }


                    var vVariable = (from myEnt in objEnt.Event_VariableDesc where myEnt.Event_Id == Eventid select myEnt).ToList();
                    if (vVariable != null)
                    {
                        Event_VariableDesc var = new Event_VariableDesc();
                        foreach (Event_VariableDesc variable in vVariable)
                        {
                            var = new Event_VariableDesc();
                            var.Event_Id = ObjEC.EventID;
                            var.VariableDesc = variable.VariableDesc;
                            var.Price = variable.Price;
                            objEnt.Event_VariableDesc.Add(var);
                        }
                    }
                    objEnt.SaveChanges();
                    Eventid = (from myEvt in objEnt.Events select myEvt.EventID).Max();
                    CreateEventController objCE = new CreateEventController();
                    objCE.ControllerContext = new ControllerContext(this.Request.RequestContext,objCE);
                    objCE.PublishEvent(Eventid);
                }
            }
            catch (Exception ex)
            {
                return Eventid;
            }
            return Eventid;
        }

    }
}