using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmaxV2Attendance.Models;
using System.Data;

namespace SmaxV2Attendance.Controllers
{
    [CustAuthFilter]
    public class EventTaskController : Controller
    {
        private SMAXV2Entities db = new SMAXV2Entities();
        //
        // GET: /EventTask/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Smx_EventTask smx_eventtask)
        {
            if (ModelState.IsValid)
            {
                List<Smx_EventTask> smx_eventtask_lst = db.Smx_EventTask.Where(x => x.ET_Device_IPAddress == smx_eventtask.ET_Device_IPAddress).ToList();

                if (smx_eventtask_lst.Count != 0)
                {
                    Util.CommonClass CC = new Util.CommonClass();
                    CC.ExecuteSQL("Delete from Smx_EventTask where ET_Device_IPAddress = '" + smx_eventtask.ET_Device_IPAddress.ToString() + "'");
                }
                db.Smx_EventTask.Add(smx_eventtask);
                db.SaveChanges();
                ViewBag.Info = "Events Configured Succesfully for device IP : " + smx_eventtask.ET_Device_IPAddress.ToString();
                return RedirectToAction("Index");
            }
            ViewBag.Info = "Error occured in configuring the events for device IP : ";
            return View();
        }

	}
}