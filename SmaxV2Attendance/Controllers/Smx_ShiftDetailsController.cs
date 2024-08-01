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
    public class Smx_ShiftDetailsController : Controller
    {
        private SMAXV2Entities db = new SMAXV2Entities();

        // GET: Smx_ShiftDetails
        public ActionResult Index()
        {
            return View(db.Smx_ShiftDetails.ToList());
        }

        // GET: Smx_ShiftDetails/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_ShiftDetails smx_ShiftDetails = db.Smx_ShiftDetails.Find(id);
            if (smx_ShiftDetails == null)
            {
                return HttpNotFound();
            }
            return View(smx_ShiftDetails);
        }

        // GET: Smx_ShiftDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Smx_ShiftDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sftd_Id,Sftd_Name,Sftd_StartTime,Sftd_EndTime,Sftd_Hours_Id,Sftd_Created,Sftd_Modified,Sftd_Modifiedby")] Smx_ShiftDetails smx_ShiftDetails)
        {
            if (ModelState.IsValid)
            {
                db.Smx_ShiftDetails.Add(smx_ShiftDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(smx_ShiftDetails);
        }

        // GET: Smx_ShiftDetails/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_ShiftDetails smx_ShiftDetails = db.Smx_ShiftDetails.Find(id);
            if (smx_ShiftDetails == null)
            {
                return HttpNotFound();
            }
            return View(smx_ShiftDetails);
        }

        // POST: Smx_ShiftDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sftd_Id,Sftd_Name,Sftd_StartTime,Sftd_EndTime,Sftd_Hours_Id,Sftd_Created,Sftd_Modified,Sftd_Modifiedby")] Smx_ShiftDetails smx_ShiftDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(smx_ShiftDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(smx_ShiftDetails);
        }

        // GET: Smx_ShiftDetails/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_ShiftDetails smx_ShiftDetails = db.Smx_ShiftDetails.Find(id);
            if (smx_ShiftDetails == null)
            {
                return HttpNotFound();
            }
            return View(smx_ShiftDetails);
        }

        // POST: Smx_ShiftDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Smx_ShiftDetails smx_ShiftDetails = db.Smx_ShiftDetails.Find(id);
            db.Smx_ShiftDetails.Remove(smx_ShiftDetails);
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
