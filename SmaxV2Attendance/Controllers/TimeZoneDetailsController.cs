using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmaxV2Attendance.Models;

namespace SmaxV2Attendance.Controllers
{
    [CustAuthFilter]
    public class TimeZoneDetailsController : Controller
    {
        private SMAXV2Entities db = new SMAXV2Entities();

        // GET: /TimeZoneDetails/
        public ActionResult Index()
        {
            var smx_timezonedetails = db.Smx_TimeZoneDetails.Include(s => s.Smx_TimeZone);
            return View(smx_timezonedetails.ToList());
        }

        // GET: /TimeZoneDetails/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_TimeZoneDetails smx_timezonedetails = db.Smx_TimeZoneDetails.Find(id);
            if (smx_timezonedetails == null)
            {
                return HttpNotFound();
            }
            return View(smx_timezonedetails);
        }

        // GET: /TimeZoneDetails/Create
        public ActionResult Create()
        {
            ViewBag.TZD_TZ_ID = new SelectList(db.Smx_TimeZone, "TZ_ID", "TZ_NAME");
            //--- newly added custom code ----//
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Every Day", Value = "10", Selected = true });
            items.Add(new SelectListItem { Text = "Week Days", Value = "11" });
            items.Add(new SelectListItem { Text = "Week Ends", Value = "12" });
            items.Add(new SelectListItem { Text = "Monday", Value = "0" });
            items.Add(new SelectListItem { Text = "Tudesday", Value = "1" });
            items.Add(new SelectListItem { Text = "Wednessday", Value = "2" });
            items.Add(new SelectListItem { Text = "Thursday", Value = "3" });
            items.Add(new SelectListItem { Text = "Friday", Value = "4" });
            items.Add(new SelectListItem { Text = "Saturday", Value = "5" });
            items.Add(new SelectListItem { Text = "Sunday", Value = "6" });
            items.Add(new SelectListItem { Text = "Holiday", Value = "7" });
            ViewBag.TZD_DAYS = items;
            //--- end of custom code -----//
            return View();
        }

        // POST: /TimeZoneDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public JsonResult Save([Bind(Include = "TZD_ID,TZD_TZ_ID,TZD_DAYS,TZD_START_TIME,TZD_END_TIME,TZD_SPECIFIC_DATE")] Smx_TimeZoneDetails[] smx_timezonedetails)
        {
            if (ModelState.IsValid)
            {
                db.Database.ExecuteSqlCommand("Delete from Smx_TimeZoneDetails where TZD_TZ_ID='" + smx_timezonedetails[0].TZD_TZ_ID + "'");

                //------- Save the new records for the Timezone id TZD_TZ_ID  -----------------------------
                foreach (Smx_TimeZoneDetails x in smx_timezonedetails)
                {
                    db.Smx_TimeZoneDetails.Add(x);
                    db.SaveChanges();
                }
                return Json(new { updated = "true" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { updated = "false" }, JsonRequestBehavior.AllowGet);
        }

        // GET: /TimeZoneDetails/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_TimeZoneDetails smx_timezonedetails = db.Smx_TimeZoneDetails.Find(id);
            if (smx_timezonedetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.TZD_TZ_ID = new SelectList(db.Smx_TimeZone, "TZ_ID", "TZ_NAME", smx_timezonedetails.TZD_TZ_ID);
            return View(smx_timezonedetails);
        }

        // POST: /TimeZoneDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "TZD_ID,TZD_TZ_ID,TZD_DAYS,TZD_START_TIME,TZD_END_TIME,TZD_SPECIFIC_DATE,TZD_CREATED,TZD_MODIFIED,TZD_MODIFIEDBY")] Smx_TimeZoneDetails smx_timezonedetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(smx_timezonedetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TZD_TZ_ID = new SelectList(db.Smx_TimeZone, "TZ_ID", "TZ_NAME", smx_timezonedetails.TZD_TZ_ID);
            return View(smx_timezonedetails);
        }

        // GET: /TimeZoneDetails/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_TimeZoneDetails smx_timezonedetails = db.Smx_TimeZoneDetails.Find(id);
            if (smx_timezonedetails == null)
            {
                return HttpNotFound();
            }
            return View(smx_timezonedetails);
        }

        // POST: /TimeZoneDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        public JsonResult DeleteConfirmed(Smx_TimeZoneDetails[] smx_timezonedetails)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    decimal id = smx_timezonedetails[0].TZD_TZ_ID;
                    List<Smx_AccessLevelDetails> smx_accesslvldetails = db.Smx_AccessLevelDetails.Where(x => x.ALD_TZ_ID == id).ToList();
                    if (smx_accesslvldetails.Count == 0 )
                    {
                        db.Database.ExecuteSqlCommand("Delete from Smx_TimeZoneDetails where TZD_TZ_ID='" + smx_timezonedetails[0].TZD_TZ_ID + "'");
                        db.Database.ExecuteSqlCommand("Delete from Smx_TimeZone where TZ_ID='" + smx_timezonedetails[0].TZD_TZ_ID + "'");
                    }
                    else
                    {
                        return Json(new { status = "This timezone has been used in AccessLevel details. \n Delete operation failed." }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch(Exception ex)
                {
                    if(ex.Message.Contains("REFERENCE"))
                    {
                        return Json(new { status = "This timezone has been used in AccessLevel details. \n Delete operation failed." }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { status = "Error Occured while deleting timezone..." }, JsonRequestBehavior.AllowGet);
                    }
                    
                }

            }
            return Json(new { status = "true" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTimeZoneDetails(decimal TZD_TZ_ID)
        {
            Array smx_timezonedetails = db.Smx_TimeZoneDetails.Select(e => new
            {
                e.TZD_TZ_ID,
                e.TZD_DAYS,
                e.TZD_START_TIME,
                e.TZD_END_TIME
            }).Where(x => x.TZD_TZ_ID == TZD_TZ_ID).ToArray();
            return Json(smx_timezonedetails, JsonRequestBehavior.AllowGet);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
