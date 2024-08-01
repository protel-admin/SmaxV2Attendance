using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmaxV2Attendance.Models;

namespace SmaxV2Attendance.Controllers
{
    [CustAuthFilter]
    public class BulkAssignmentController : Controller
    {
        private SMAXV2Entities db = new SMAXV2Entities();
        //
        // GET: /BulkAssignment/
        public ActionResult Index()
        {
            ViewData["Department"] = Util.Utility.GetDepartment();
            ViewData["Designation"] = Util.Utility.GetDesignation();
            ViewData["ALD_DE_IPADDRESS"] = Util.Utility.GetAccessLevelList();
            return View();
        }

        public JsonResult GetEmployee(int DP_ID, int DN_ID)
        {
            SMAXV2Entities db = new SMAXV2Entities();
            Array smx_cardholder = null;
            if(DP_ID > 0 && DN_ID > 0)
            {
                smx_cardholder = db.Smx_CardHolder.Select(e => new
                {
                    e.Ch_EmpId,
                    e.Ch_FName,
                    e.Ch_LName,
                    e.Ch_Dp_Id,
                    e.Ch_Dn_Id,
                    e.Smx_Department.DP_NAME,
                    e.Smx_Designation.DN_NAME 
                }).Where(x => x.Ch_Dp_Id  == DP_ID && x.Ch_Dn_Id == DN_ID).ToArray();

            }
            return Json(smx_cardholder, JsonRequestBehavior.AllowGet);

        }
	}
}