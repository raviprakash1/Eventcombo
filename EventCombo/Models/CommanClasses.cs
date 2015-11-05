using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventCombo.Models
{
    public class CommanClasses
    {
        public string GetMessage(string strFormName,string strFormTag)
        {
            string result = "";
            try
            {
                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    result = (from myRow in objEnt.Messages
                              where myRow.Form_Name == strFormName && myRow.Form_Tag == strFormTag
                              select myRow.Message1).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                result = "There some error comes.Please contact system administrator";
            }
            return result;
        }
    }


    public partial class Ticket_Locked_Detail
    {
        public Ticket_Locked_Detail[] TLD_List { get; set; }
    }

    //public partial class Ticket_Pub
    //{
    //    public Ticket_Locked_Detail[] TLD_List { get; set; }
    //}

}