using SmaxV2Attendance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmaxV2Attendance.Controllers
{
    [CustAuthFilter]
    public class LiveMonitorController : Controller
    {
        //
        // GET: /LiveMonitor/
        private SMAXV2Entities db = new SMAXV2Entities();
        public ActionResult Index()
        {           
            return View();
        }

        public JsonResult GetLiveTransaction(int ttype,int ln_id)
        {
            List<vw_Smx_Transaction> smx_livtransaction = Util.Utility.GetLiveTransactionData(ttype,ln_id);

            return Json(smx_livtransaction, JsonRequestBehavior.AllowGet);
        }

	}
}