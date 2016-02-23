using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.Models;

namespace CMS.Controllers
{
    public class FeeSettingController : Controller
    {
        // GET: FeeSetting
        EmsEntities db = new EmsEntities();
        public ActionResult Index()
        {
            var feestructure = (from c in db.Fee_Structure
                                select c).FirstOrDefault();

            FeeSetting fee = new FeeSetting();
            if (feestructure != null)
            {
                if (feestructure.FS_Amount != null)
                {
                    fee.value = feestructure.FS_Amount.ToString();

                }
                else
                {
                    fee.value = "0.99";
                }
                if (feestructure.FS_Percentage != null)
                {
                    string feeP = feestructure.FS_Percentage.ToString();
                    if (feeP.Contains("."))
                    {
                        var split = feeP.Split('.');
                        var array1 = split[0];
                        var array2 = split[1];
                        if (long.Parse(array2) > 0)
                        {
                            fee.percentage = feestructure.FS_Percentage.ToString();
                        }
                        else
                        {
                            fee.percentage = array1;
                        }
                    }
                    else
                    {
                        fee.percentage = feestructure.FS_Percentage.ToString();
                    }


                }
                else
                {
                    fee.percentage = "5";
                }
            }else
            {
                fee.value = "0.99";
                fee.percentage = "5";
            }
            return View(fee);
        }

public string savefee(FeeSetting model)
        {
            var msg = "";
            try
            {
                var feestructure = (from c in db.Fee_Structure
                                    select c).FirstOrDefault(); var ObjEC = db.Fee_Structure.FirstOrDefault();
                feestructure.FS_Amount = decimal.Parse(model.value);
                feestructure.FS_Percentage = decimal.Parse(model.percentage);
                feestructure.FS_Apply = model.Apply;
                db.SaveChanges();

                msg= "S";
            }
            catch (Exception ex)
            {
                msg = "N";
            }

            return msg;
        }
    
    }
}