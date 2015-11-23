using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.Models;
using System.Data;
using System.Text;

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
            List<UsersTemplate> objuser = GetAllUsers(SearchStringFirstName, SearchStringLastName, SearchStringEmail);
            int iCount = (PageF != null ? Convert.ToInt32(PageF) : 0);
            List<SelectListItem> PageFilter = new List<SelectListItem>();
             
            PageFilter.Add(new SelectListItem()
            {
                Text = "Select",
                Value = "0",
                Selected = (iCount == 0 ? true : false)
            });

            int i = 0; int z = 0; int iUcount = objuser.Count;int iGapValue = 50;
            string strText = "";
            if (iUcount > iGapValue)
            {
                for (i = 0; i < iUcount; i++)
                {
                    strText = z.ToString() + " - " + (z + iGapValue).ToString();
                    PageFilter.Add(new SelectListItem()
                    {
                        Text = strText,
                        Value = (z+ iGapValue).ToString(),
                        Selected = (iCount == z ? true : false)
                    });
                    z = z + iGapValue;
                    iUcount = iUcount - iGapValue;
                    if (iUcount < iGapValue)
                    {
                        strText = z.ToString() + " - " + (z + iGapValue).ToString();
                        PageFilter.Add(new SelectListItem()
                        {
                            Text = strText,
                            Value = (z + iGapValue).ToString(),
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
                    Text = "0 - 50",
                    Value = "50",
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



           
            // List<Permissions> objPerm = GetPermission("APP");
            // UsersTemplate objU = new UsersTemplate();
            //  objU.objPermissions = GetPermission("APP");
            // objuser.Add(objU);
            return View(objuser);
        }
        public List<UsersTemplate> GetAllUsers(string SearchStringFirstName, string SearchStringLastName, string SearchStringEmail)
        {
            string user = (Session["AppId"] != null ? Session["AppId"].ToString() : string.Empty);

            if (SearchStringFirstName == null) SearchStringFirstName = "";
            if (SearchStringLastName == null) SearchStringLastName = "";
            if (SearchStringEmail == null) SearchStringEmail = "";

            using (EmsEntities objEntity = new EmsEntities())
            {
                var modelUserTemp = (from UserTemp in objEntity.AspNetUsers
                                     join Pr in objEntity.Profiles on UserTemp.Id equals Pr.UserID
                                     where UserTemp.Id != user
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
                                         Online= UserTemp.LoginStatus
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
                        strHtml.Append(" . </td>");
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

    }
}