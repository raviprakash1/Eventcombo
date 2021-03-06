﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.Models;
using System.Data;
using System.Text;
using EventCombo;
using System.Configuration;
using PagedList;
using System.Data.SqlClient;

namespace CMS.Controllers
{
    [CustomAuthorization("10")]
    public class UsersController : Controller
    {
        // GET: Users
        EmsEntities db = new EmsEntities();
        public ActionResult Deleteuser(string userid)
        {
            try
            {
                db.Database.ExecuteSqlCommand("Delete from User_Permission_Detail where UP_User_Id='" + userid + "'");
                Profile prof = db.Profiles.Where(i => i.UserID == userid).FirstOrDefault();
                db.Profiles.Remove(prof);
                db.SaveChanges();

                db.Database.ExecuteSqlCommand("Delete from AspNetUserRoles where UserId='" + userid + "'");


                AspNetUser user = db.AspNetUsers.Find(userid);
                db.AspNetUsers.Remove(user);
                db.SaveChanges();


                return Content("Deleted");

            }
            catch (Exception ex)
            {
                return Content(ex.Message);

            }

        }

        public ActionResult Users(string SearchStringFirstName, string SearchStringLastName, string SearchStringEmail,string sortorder,int PageF=0)
        {
          
            
            if ((Session["UserID"] != null))
            {int page_live = 1;
               
                int pageSize = 50;
                PageF = PageF > 0 ? PageF : 1;
             
                pageSize = pageSize > 0 ? pageSize : 50;
                ViewData["EventComboClientDomain"] = ConfigurationManager.AppSettings["EventComboClientDomain"];

                List<V_Users> objuser = GetAllUsers(SearchStringFirstName, SearchStringLastName, SearchStringEmail);
                switch (sortorder)
                {
                    case "sno":
                        objuser = objuser.OrderBy(s => s.Sno).ToList();
                        break;
                    case "snodesc":
                        objuser = objuser.OrderByDescending(s => s.Sno).ToList();
                        break;
                    case "name":
                        objuser = objuser.OrderBy(s => s.FirstName).ToList();
                        break;
                    case "namedesc":
                        objuser = objuser.OrderByDescending(s => s.FirstName).ToList();
                        break;
                    case "role":
                        objuser = objuser.OrderBy(s => s.RoleType).ToList();
                        break;
                    case "roledesc":
                        objuser = objuser.OrderByDescending(s => s.RoleType).ToList();
                        break;
                    case "email":
                        objuser = objuser.OrderBy(s => s.EMail).ToList();
                        break;
                    case "emaildesc":
                        objuser = objuser.OrderByDescending(s => s.EMail).ToList();
                        break;
                    case "event":
                        objuser = objuser.OrderBy(s => s.EventCount).ToList();
                        break;
                    case "eventdesc":
                        objuser = objuser.OrderByDescending(s => s.EventCount).ToList();
                        break;
                    case "deals":
                        objuser = objuser.OrderBy(s => s.deals).ToList();
                        break;
                    case "dealsdesc":
                        objuser = objuser.OrderByDescending(s => s.deals).ToList();
                        break;
                    case "location_desc":
                        objuser = objuser.OrderByDescending(s => s.Ipcountry).ToList();
                        break;
                    case "location":
                        objuser = objuser.OrderBy(s => s.Ipcountry).ToList();
                        break;
                    case "ticketpurchased_desc":
                        objuser = objuser.OrderByDescending(s => s.TicketPurchased).ToList();
                        break;
                    case "ticketpurchased":
                        objuser = objuser.OrderBy(s => s.TicketPurchased).ToList();
                        break;
                    case "dealpurchased_desc":
                        objuser = objuser.OrderByDescending(s => s.dealpurchased).ToList();
                        break;
                    case "dealpurchased":
                        objuser = objuser.OrderBy(s => s.dealpurchased).ToList();
                        break;
                    case "Organiser":
                        objuser = objuser.OrderBy(s => s.OrganiserId).ToList();
                        break;
                    case "Orgaizerdesc":
                        objuser = objuser.OrderByDescending(s => s.OrganiserId).ToList();
                        break;
                    case "Merchant":
                        objuser = objuser.OrderBy(s => s.MerchantId).ToList();
                        break;
                    case "Merchantdesc":
                        objuser = objuser.OrderByDescending(s => s.MerchantId).ToList();
                       break;
                    case "status":
                        objuser = objuser.OrderBy(s => s.UserStatusId).ToList();
                        break;
                    case "statusdesc":
                        objuser = objuser.OrderByDescending(s => s.UserStatusId).ToList();
                        break;
                    case "Online":
                        objuser = objuser.OrderBy(s => s.Online).ToList();
                        break;
                    case "Onlinedesc":
                        objuser = objuser.OrderByDescending(s => s.Online).ToList();
                        break;
                    default:
                        objuser = objuser.OrderBy(s => s.Sno).ToList();
                        break;
                }


                var users= objuser.ToPagedList(PageF, pageSize);
              
                ViewData["Userscount"] = objuser.Count();
                if (objuser.Count == 0)
                    ViewData["SearchedUser"] = 0;
            //foreach (var item in objuser)
            //{
                //var ans = db.Database.SqlQuery<string>("select RoleId from AspNetUserRoles where Userid=@p0", item.Id).FirstOrDefault();
                //    ans = (ans != null ? ans : "0");
                //    if (int.Parse(ans) == 1)
                //    {
                //        item.RoleType = "Super Admin";
                //    }
                //    if (int.Parse(ans) == 2)
                //    {
                //        item.RoleType = "Admin";
                //    }
                //    if (int.Parse(ans) == 3)
                //    {
                //        var countperrm = db.Permission_Detail.Where(x => x.Permission_Category == "APP").Count();
                //        var countuserperm = db.User_Permission_Detail.Where(x => x.UP_User_Id == item.Id).Count();

                //        if (countuserperm < countperrm)
                //        {
                //            item.RoleType = "Member-Limited";

                //            ans = "4";
                //        }
                //        else
                //        {
                //            item.RoleType = "Member";
                //        }
                //    }
                //    item.Role = ans;
                //var evtcount = db.Database.SqlQuery<Int32>("select count(*) from Event  where isnull(Parent_EventID,0)=0 and Userid=@p0", item.Id).FirstOrDefault();
                //item.EventCount = evtcount;
                //var ticketpurchased= db.Database.SqlQuery<Int32>("select isnull(sum(TPD_Purchased_Qty),0) from Ticket_Purchased_Detail  where TPD_User_Id=@p0", item.Id).FirstOrDefault();
                //item.TicketPurchased = ticketpurchased;
           // }
            //int iCount = (PageF == 1 ? 0:PageF-1);
            List<SelectListItem> PageFilter = new List<SelectListItem>();

                //PageFilter.Add(new SelectListItem()
                //{
                //    Text = "Select",
                //    Value = "0",
                //    Selected = (iCount == 0 ? true : false)
                //});
                //List<UsersTemplate> objuser1 = GetAllUsers(SearchStringFirstName, SearchStringLastName, SearchStringEmail);
                //foreach (var item in objuser1)
                //{
                //    var ans = db.Database.SqlQuery<Int32>("select count(*) from Event  where Userid=@p0", item.Id).FirstOrDefault();
                //    item.EventCount = ans;


                //}
                int iCount = 0;
                int i = 0; int z = 0; double iUcount = objuser.Count; int iGapValue = 50;
                string strText = "";
                if (iUcount > iGapValue)
                {
                    double dRowCnt = iUcount / iGapValue;

                    if ((dRowCnt - (int)dRowCnt) != 0) dRowCnt = (int)dRowCnt + 1;

                    for (i = 0; i < dRowCnt; i++)
                    {
                        strText = (z + 1).ToString() + " - " + (z + iGapValue).ToString();
                        PageFilter.Add(new SelectListItem()
                        {
                            Text = strText,
                            Value = (i).ToString(),
                            Selected = (PageF == i ? true : false)
                        }); 
                        z = z + iGapValue;
                        iUcount = iUcount - iGapValue;
                        //if (iUcount < iGapValue)
                        //{
                        //    strText = (z + 1).ToString() + " - " + (z + iGapValue).ToString();
                        //    PageFilter.Add(new SelectListItem()
                        //    {
                        //        Text = strText,
                        //        Value = (i + 1).ToString(),
                        //        Selected = (iCount == z ? true : false)
                        //    });
                        //}
                        //for (i = 0; i < iUcount; i++)
                        //{
                        //    if ((z + iGapValue) == 106000)
                        //    {
                        //        string str = "";
                        //    }
                        //    strText = (z + 1).ToString() + " - " + (z + iGapValue).ToString();
                        //    PageFilter.Add(new SelectListItem()
                        //    {
                        //        Text = strText,
                        //        Value = (i).ToString(),
                        //        Selected = (iCount == z ? true : false)
                        //    });
                        //    z = z + iGapValue;
                        //    iUcount = iUcount - iGapValue;
                        //    if (iUcount < iGapValue)
                        //    {
                        //        strText = (z + 1).ToString() + " - " + (z + iGapValue).ToString();
                        //        PageFilter.Add(new SelectListItem()
                        //        {
                        //            Text = strText,
                        //            Value = (i + 1).ToString(),
                        //            Selected = (iCount == z ? true : false)
                        //        });
                        //    }
                    }

                    if (iUcount > 0)
                    {
                        if (iUcount < iGapValue)
                        {
                            strText = (z + 1).ToString() + " - " + (z + iGapValue).ToString();
                            PageFilter.Add(new SelectListItem()
                            {
                                Text = strText,
                                Value = (i).ToString(),
                                Selected = (iCount == z ? true : false)
                            });
                        }
                    }
                    if (iCount > 0)
                    {
                        if (iCount < objuser.Count)
                            objuser = objuser.GetRange(iCount - iGapValue, iGapValue);
                        else
                        {
                            //objuser = objuser.GetRange(iCount - iGapValue, ((iCount - (objuser.Count + 1))));
                            int iGap = (iCount - iGapValue);
                            objuser = objuser.GetRange(iGap, (objuser.Count - iGap));
                            //objlst = objlst.GetRange(iGap, (objlst.Count - iGap));
                        }
                    }
                }
                else
                {
                    PageFilter.Add(new SelectListItem()
                    {
                        Text = "1 - 50",
                        Value = "0",
                        Selected = (iCount == 50 ? true : false)
                    });



                }
                PageFilter.Insert(0,new SelectListItem()
                {
                    Text = "Select",
                    Value = "S",
                    //Selected = (iCount == 50 ? true : false)
                });


                TempData["SearchStringFirstName"] = SearchStringFirstName;
                TempData["SearchStringLastName"] = SearchStringLastName;
                TempData["SearchStringEmail"] = SearchStringEmail;

                TempData["sortorder"] = sortorder;
                //PageFilter.Add(new SelectListItem()
                //{
                //    Text = "1 - 5",
                //    Value = "5",
                //    Selected = (iCount == 5 ? true : false)
                //});
                //PageFilter.Add(new SelectListItem()
                //{
                //    Text = "5 - 10",
                //    Value = "10",
                //    Selected = (iCount == 10 ? true : false)
                //});

                ViewBag.ddlPageF = PageFilter;
                var userid = Session["UserID"].ToString();
                TempData["Pagesize"] = pageSize;
                TempData["PageNo"] = PageF;


            // List<Permissions> objPerm = GetPermission("APP");
            // UsersTemplate objU = new UsersTemplate();
            //  objU.objPermissions = GetPermission("APP");
            // objuser.Add(objU);
                return View(users);
            }
            else
            {
                return RedirectToAction("Login", "Home");

            }
        }
        public List<V_Users> GetAllUsers(string SearchStringFirstName, string SearchStringLastName, string SearchStringEmail)
        {
            string user = (Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);

            if (SearchStringFirstName == null) SearchStringFirstName = "";
            if (SearchStringLastName == null) SearchStringLastName = "";
            if (SearchStringEmail == null) SearchStringEmail = "";
            List<V_Users> objEv = new List<V_Users>();
            using (EmsEntities objEntity = new EmsEntities())
            {

                string strsql = "";
              
                if (!string.IsNullOrWhiteSpace(SearchStringFirstName))
                {
                    var str = "%" + SearchStringFirstName + "%";
                    strsql += " And (rtrim(ltrim(Us.FirstName)) like rtrim(ltrim('" + str + "')) OR rtrim(ltrim(Us.Id)) like rtrim(ltrim('" + str + "')))";

                }
             
               
                if (!string.IsNullOrWhiteSpace(SearchStringLastName))
                {
                    var str = "%" + SearchStringLastName + "%";
                    strsql += " And rtrim(ltrim(Us.LastName)) like rtrim(ltrim('" + str + "'))";

                }
                if (!string.IsNullOrWhiteSpace(SearchStringEmail))
                {
                    var str = "%" + SearchStringEmail + "%";
                    strsql += " And (rtrim(ltrim(Us.EMail)) like rtrim(ltrim('" + str + "')) OR rtrim(ltrim(Us.Id)) like rtrim(ltrim('" + str + "')))";

                }
              
                List<Object> sqlParamsList = new List<object>();
                var eventID = new SqlParameter("@EventID", strsql);
                string sql = "";

                if (!string.IsNullOrEmpty(strsql))
                {
                    objEv = db.V_Users.SqlQuery("Select * from V_Users Us where 1=1 " + strsql + " ").ToList<V_Users>();
                }
                else
                {
                    objEv = db.V_Users.SqlQuery("Select * from V_Users").ToList<V_Users>();
                }
                //List<V_Users> modelUserTemp = (from x in objEntity.V_Users select x).OrderBy(x=>x.FirstName).ToList();
              

                ////return modelUserTemp.ToList();
                //if (!SearchStringFirstName.Equals(string.Empty))
                //{
                //    modelUserTemp = modelUserTemp.Where(us => us.FirstName.Contains(SearchStringFirstName) ).ToList();
                  
                //}
                //if (!SearchStringLastName.Equals(string.Empty))
                //{
                //    modelUserTemp = modelUserTemp.Where(us => us.LastName.Contains(SearchStringLastName) ).ToList();

                //}
                //if (!SearchStringEmail.Equals(string.Empty))
                //{
                //    modelUserTemp = modelUserTemp.Where(us => us.EMail.Contains(SearchStringEmail) ).ToList();

                //}

                //if (!SearchStringFirstName.Equals(string.Empty) || !SearchStringLastName.Equals(string.Empty) || !SearchStringEmail.Equals(string.Empty))
                //    return modelUserTemp.Where(us => us.FirstName.ToLower().Contains(SearchStringFirstName.ToLower()) || us.LastName.ToLower().Contains(SearchStringLastName.ToLower())
                //    || us.EMail.ToLower().Contains(SearchStringEmail.ToLower()) || us.Id.Contains(SearchStringFirstName)|| us.Id.Contains(SearchStringLastName) || us.Id.Contains(SearchStringEmail)).ToList();
                //else
                    return objEv.ToList();

                

            }
        }




        public ActionResult Permission()
        {
            return View(GetPermission("APP"));
        }

        public List<Permissions> GetPermission(string strCategory)
        {

            using (EmsEntities objEntity = new EmsEntities())
            {
                var modelPerm = (from Perm in objEntity.Permission_Detail
                                 where Perm.Permission_Category.Equals(strCategory)
                                 select new Permissions
                                 {
                                     Permission_Id = Perm.Permission_Id,
                                     Permission_Desc = Perm.Permission_Desc,
                                     Permission_Category = Perm.Permission_Category
                                 }
                                    );


                return modelPerm.ToList();

            }

        }

        public string SavePermisions(string strUserId, string strPermission, string strRole)
        {
            string strResult = "";
            try
            {


                string[] strAry = strPermission.Split('#');
                EmsEntities objEnt = new EmsEntities();
                User_Permission_Detail upd = new User_Permission_Detail();
                foreach (string str in strAry)
                {
                    if (!str.Trim().Equals(string.Empty))
                    {
                        upd = new User_Permission_Detail();
                        upd.UP_Permission_Id = Convert.ToInt16(str);
                        upd.UP_User_Id = strUserId;
                        objEnt.User_Permission_Detail.Add(upd);
                    }
                }
                string strRoleId = objEnt.GetSetUserRole(strUserId, "SET", strRole).Single();
                objEnt.User_Permission_Detail.RemoveRange(objEnt.User_Permission_Detail.Where(x => x.UP_User_Id == strUserId));
                objEnt.SaveChanges();


                //var vRoleId =objEnt.Database.SqlQuery<int>("Select RoleId from AspNetUserRoles where UserId='" + strUserId + "'").Single();
                //var vRoleId = objEnt.Database.SqlQuery("GetSetUserRole",new { "@user_Id: " });



                strResult = "Y";
            }
            catch (Exception ex)
            {
                strResult = "N";
            }
            return strResult;
        }


        public string SaveOtherInfo(string strField, string strvalue, string strUserId)
        {
            string strResult = "";
            try
            {
                EmsEntities objEnt = new EmsEntities();
                objEnt.Database.ExecuteSqlCommand("Update Profile set " + strField + " = '" + strvalue + "' Where UserId = '" + strUserId + "'");
                strResult = "Y";
            }
            catch (Exception ex)
            {
                strResult = "N";
            }
            return strResult;
        }



      
        public string GetUserPermission(string strUserId)
        {
            StringBuilder strResult = new StringBuilder();
            try
            {
                EmsEntities objEms = new EmsEntities();
                List<User_Permission_Detail> objUpList = objEms.User_Permission_Detail.Where(x => x.UP_User_Id == strUserId).ToList();
                foreach (User_Permission_Detail upd in objUpList)
                    strResult.Append(upd.UP_Permission_Id.ToString() + "^");

                string strRoleId = objEms.GetSetUserRole(strUserId, "GET", "").Single();

                strResult.Append(strRoleId);
            }
            catch (Exception ex)
            {
                strResult.Clear();
                strResult.Append("Error");
            }
            return strResult.ToString();
        }

        public string GetUserEvents(string strUserId)
        {
            StringBuilder strHtml = new StringBuilder();
            using (EmsEntities objEntity = new EmsEntities())
            {
                var modelPerm = (from uEvt in objEntity.Events
                                 where uEvt.UserID.Equals(strUserId)
                                 select uEvt).ToList();
                int i = 0;
                if (modelPerm.Count > 0)
                {
                    foreach (Event obj in modelPerm)
                    {
                        i = i + 1;
                        strHtml.Append("<tr>");
                        strHtml.Append("<td align='left'>");
                        strHtml.Append(i.ToString());
                        strHtml.Append(". </td>");
                        strHtml.Append("<td align='left'>");
                        strHtml.Append(obj.EventTitle);
                        strHtml.Append("</td>");
                        strHtml.Append("</tr>");
                    }
                }
                else
                {
                    strHtml.Append("<tr>");
                    strHtml.Append("<td align='left'>");
                    strHtml.Append("No event history for this user.");
                    strHtml.Append("</td>");
                    strHtml.Append("</tr>");

                }
                return strHtml.ToString();
            }

        }
        public string GetUserEventsCounter(string strUserId)
        {
            StringBuilder strHtml = new StringBuilder();
            using (EmsEntities objEntity = new EmsEntities())
            {
                var modelPerm = (from uEvt in objEntity.Events
                                 where uEvt.UserID.Equals(strUserId)
                                 select uEvt).ToList();                                
                return modelPerm.Count.ToString();
            }

        }

    }
}