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
    public class TimeZoneController : Controller
    {
        private SMAXV2Entities db = new SMAXV2Entities();

        // GET: /TimeZone/
        public ActionResult Index()
        {
            return View(db.Smx_TimeZone.ToList());
        }

        // GET: /TimeZone/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_TimeZone smx_timezone = db.Smx_TimeZone.Find(id);
            if (smx_timezone == null)
            {
                return HttpNotFound();
            }
            return View(smx_timezone);
        }

        // GET: /TimeZone/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /TimeZone/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="TZ_ID,TZ_NAME,TZ_UPDATE_STATUS,TZ_CREATED,TZ_MODIFIED,TZ_MODIFIEDBY")] Smx_TimeZone smx_timezone)
        {
            if (ModelState.IsValid)
            {
                List<Smx_TimeZone> TimeZone;
                TimeZone = db.Smx_TimeZone.ToList();
                if (TimeZone.Count != 40)
                {
                    TimeZone = db.Smx_TimeZone.Where(x => x.TZ_NAME == smx_timezone.TZ_NAME).ToList();

                    if (TimeZone.Count != 0)
                    {
                        ViewBag.ErrorMsg = "TimeZone Name already Exists!";
                        return View();
                    }
                    db.Smx_TimeZone.Add(smx_timezone);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(smx_timezone);
        }

        [HttpPost]
        public JsonResult AddTZ(Smx_TimeZone smx_timezone)
        {

            if (smx_timezone.TZ_NAME != null && smx_timezone.TZ_NAME.Trim()!="")
            {
                List<Smx_TimeZone> TimeZone = db.Smx_TimeZone.Where(x => x.TZ_NAME == smx_timezone.TZ_NAME).ToList();

                if (TimeZone.Count != 0)
                {
                    ViewBag.ErrorMsg = "TimeZone Name already Exists!";
                    return Json(new { Err = "1" }, JsonRequestBehavior.AllowGet);
                }
                smx_timezone.TZ_UPDATE_STATUS = false;
                smx_timezone.TZ_CREATED = DateTime.Now;
                smx_timezone.TZ_MODIFIED = DateTime.Now;
                smx_timezone.TZ_MODIFIEDBY = "Admin";
                db.Smx_TimeZone.Add(smx_timezone);
                db.SaveChanges();
                return Json(new { Err = "0" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ViewBag.ErrorMsg = "TimeZone Name Should Not be Empty!";
                return Json(new { Err = "2" }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: /TimeZone/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_TimeZone smx_timezone = db.Smx_TimeZone.Find(id);
            if (smx_timezone == null)
            {
                return HttpNotFound();
            }
            return View(smx_timezone);
        }

        // POST: /TimeZone/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="TZ_ID,TZ_NAME,TZ_UPDATE_STATUS,TZ_CREATED,TZ_MODIFIED,TZ_MODIFIEDBY")] Smx_TimeZone smx_timezone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(smx_timezone).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(smx_timezone);
        }

        // GET: /TimeZone/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_TimeZone smx_timezone = db.Smx_TimeZone.Find(id);
            if (smx_timezone == null)
            {
                return HttpNotFound();
            }
            return View(smx_timezone);
        }

        // POST: /TimeZone/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Smx_TimeZone smx_timezone = db.Smx_TimeZone.Find(id);
            db.Smx_TimeZone.Remove(smx_timezone);
            db.SaveChanges();
            return RedirectToAction("Index");
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
