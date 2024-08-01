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
    public class AreasController : Controller
    {
        private SMAXV2Entities db = new SMAXV2Entities();
        //
        // GET: /Areas/
        public ActionResult Index()
        {
            ViewBag.AR_NAME = new SelectList(db.vw_AreaName, "AR_NAME", "AR_NAME");
            ViewBag.IN_Reader = Util.Utility.GetInReaders();
            ViewBag.OUT_Reader = Util.Utility.GetOutReaders();
            return View();
        }

        [HttpPost]
        public JsonResult Save([Bind(Include = "AR_ID,AR_NAME,AR_NODEID,AR_TYPE,AR_APB,AR_LN_ID,AR_IPADDRESS,AR_APBNUMBER,AR_STATUS,AR_DELETED")]Smx_Areas[] smx_areas)
        {
            if (ModelState.IsValid)
            {
                string areaname = smx_areas[0].AR_NAME;
                List<Smx_Areas> ar_lst = db.Smx_Areas.Where(e => e.AR_NAME == areaname).ToList();
                if (ar_lst.Count() == 0)
                {
                    //------- Save the new records for the Timezone id TZD_TZ_ID  -----------------------------
                    foreach (Smx_Areas x in smx_areas)
                    {
                        //x.ALD_CREATED = DateTime.Now;
                        //x.ALD_MODIFIED = DateTime.Now;
                        //x.ALD_MODIFIEDBY = "Admin";
                        db.Smx_Areas.Add(x);
                        db.SaveChanges();
                    }
                    return Json(new { updated = "true" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //------- First delete the existind recirds and Save the new records for the Timezone id TZD_TZ_ID  ----------------------
                    db.Database.ExecuteSqlCommand("Delete from Smx_Areas where AR_NAME='" + smx_areas[0].AR_NAME + "'");
                    foreach (Smx_Areas x in smx_areas)
                    {
                        //x.ALD_CREATED = DateTime.Now;
                        //x.ALD_MODIFIED = DateTime.Now;
                        //x.ALD_MODIFIEDBY = "Admin";
                        db.Smx_Areas.Add(x);
                        db.SaveChanges();
                    }
                    return Json(new { updated = "true" }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { updated = "false" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteArea(string areaname)
        {
            Util.CommonClass CC = new Util.CommonClass();
            int cnt = CC.ExecuteSQL("update Smx_Areas set AR_DELETED = '1' where AR_NAME = '" + areaname + "'");
            return Json(new { status = "true" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLastAPBNo()
        {
            int APBNo = 1;
            Util.CommonClass CC = new Util.CommonClass();
            DataTable dt = CC.GetDataTable("  Select Case when (Max([AR_APBNUMBER]) is NULL ) then 0 else Max([AR_APBNUMBER]) end  APBNo from Smx_Areas");
            if(dt.Rows.Count > 0)
            {
                APBNo = Convert.ToInt16(dt.Rows[0]["APBNo"].ToString());
            }
            return Json(new { apbno = APBNo.ToString() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAreaDetails(string areaname)
        {
            Array smx_areas;
            using (db)
            {
                smx_areas = db.Smx_Areas.Select(e => new
                {
                    e.AR_APB,
                    e.AR_APBNUMBER,
                    e.AR_IPADDRESS,
                    e.AR_LN_ID,
                    e.AR_NAME,
                    e.AR_NODEID,
                    e.AR_STATUS,
                    e.AR_TYPE
                    
                }).Where(x=>x.AR_NAME == areaname).ToArray();
            }
            return Json(smx_areas, JsonRequestBehavior.AllowGet);
        }

	}
}