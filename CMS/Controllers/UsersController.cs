using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.Models;
using System.Data;
using System.Text;
using EventCombo;
using System.Configuration;

namespace CMS.Controllers
{
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

        public ActionResult Users(string SearchStringFirstName, string SearchStringLastName, string SearchStringEmail,string PageF)
        {
            EventListController objEl = new EventListController();
            
            if ((Session["UserID"] != null))
            {

                ViewData["EventComboClientDomain"] = ConfigurationManager.AppSettings["EventComboClientDomain"];

                List<UsersTemplate> objuser = GetAllUsers(SearchStringFirstName, SearchStringLastName, SearchStringEmail);
                ViewData["Userscount"] = objuser.Count();
                if (objuser.Count == 0)
                    ViewData["SearchedUser"] = 0;
            foreach (var item in objuser)
            {
                var ans = db.Database.SqlQuery<string>("select RoleId from AspNetUserRoles where Userid=@p0", item.Id).FirstOrDefault();
              
                if(int.Parse(ans)==1)
                {
                    item.RoleType = "Super Admin";
                }
                if (int.Parse(ans) == 2)
                {
                    item.RoleType = "Admin";
                }
                if (int.Parse(ans) == 3)
                {
                    var countperrm = db.Permission_Detail.Where(x => x.Permission_Category == "APP").Count();
                    var countuserperm = db.User_Permission_Detail.Where(x => x.UP_User_Id == item.Id).Count();

                    if (countuserperm < countperrm)
                    {
                        item.RoleType = "Member-Limited";
                      
                        ans = "4";
                    }
                    else
                    {
                        item.RoleType = "Member";
                    }
                }
                item.Role = ans;
                var evtcount = db.Database.SqlQuery<Int32>("select count(*) from Event  where isnull(Parent_EventID,0)=0 and Userid=@p0", item.Id).FirstOrDefault();
                item.EventCount = evtcount;
                var ticketpurchased= db.Database.SqlQuery<Int64>("select isnull(sum(TPD_Purchased_Qty),0) from Ticket_Purchased_Detail  where TPD_User_Id=@p0", item.Id).FirstOrDefault();
                item.TicketPurchased = ticketpurchased;
            }
            int iCount = (PageF != null ? Convert.ToInt32(PageF) : 0);
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
           int i = 0; int z = 0; int iUcount = objuser.Count;int iGapValue = 50;
            string strText = "";
            if (iUcount > iGapValue)
            {
                for (i = 0; i < iUcount; i++)
                {
                    strText = (z+1).ToString() + " - " + (z + iGapValue).ToString();
                    PageFilter.Add(new SelectListItem()
                    {
                        Text = strText,
                        Value = (i).ToString(),
                        Selected = (iCount == z ? true : false)
                    });
                    z = z + iGapValue;
                    iUcount = iUcount - iGapValue;
                    if (iUcount < iGapValue)
                    {
                        strText = (z+1).ToString() + " - " + (z + iGapValue).ToString();
                        PageFilter.Add(new SelectListItem()
                        {
                            Text = strText,
                            Value = (i+1).ToString(),
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
                        objuser = objuser.GetRange(iGap, (objuser.Count-iGap));
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

            ViewBag.PageF = PageFilter;
                var userid = Session["UserID"].ToString();
           


            // List<Permissions> objPerm = GetPermission("APP");
            // UsersTemplate objU = new UsersTemplate();
            //  objU.objPermissions = GetPermission("APP");
            // objuser.Add(objU);
            return View(objuser);
            }
            else
            {
                return RedirectToAction("Login", "Home");

            }
        }
        public List<UsersTemplate> GetAllUsers(string SearchStringFirstName, string SearchStringLastName, string SearchStringEmail)
        {
            string user = (Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);

            if (SearchStringFirstName == null) SearchStringFirstName = "";
            if (SearchStringLastName == null) SearchStringLastName = "";
            if (SearchStringEmail == null) SearchStringEmail = "";

            using (EmsEntities objEntity = new EmsEntities())
            {
                var modelUserTemp = (from UserTemp in objEntity.AspNetUsers
                                     join Pr in objEntity.Profiles on UserTemp.Id equals Pr.UserID
                                     orderby Pr.FirstName ascending
                                       select new UsersTemplate
                                     {
                                         EMail = UserTemp.Email,
                                         UserName = UserTemp.UserName,
                                         Id = UserTemp.Id,
                                         Organiser = Pr.Organiser.Trim(),
                                         Merchant = Pr.Merchant.Trim(),
                                         UserStatus = Pr.UserStatus.Trim(),
                                         FirstName = Pr.FirstName,
                                         LastName = Pr.LastName,
                                         Online= UserTemp.LoginStatus,
                                         Role="",
                                         State=!string.IsNullOrEmpty(Pr.State)? Pr.State:"",
                                         OrganiserId= Pr.Organiser.Trim()=="Y"?"Yes":"No",
                                         MerchantId= Pr.Merchant.Trim()=="Y"?"Yes":"No",
                                         UserStatusId= Pr.UserStatus.Trim()=="Y"?"Enable":"Disable",
                                         Ipcountry=Pr.Ipcountry
                                        
                                     }
                                    );

                
                //return modelUserTemp.ToList();
                if (!SearchStringFirstName.Equals(string.Empty) || !SearchStringLastName.Equals(string.Empty) || !SearchStringEmail.Equals(string.Empty))
                    return modelUserTemp.Where(us => us.FirstName.ToLower().Contains(SearchStringFirstName.ToLower()) || us.LastName.ToLower().Contains(SearchStringLastName.ToLower())
                    || us.EMail.ToLower().Contains(SearchStringEmail.ToLower())).ToList();
                else
                    return modelUserTemp.ToList();

                

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