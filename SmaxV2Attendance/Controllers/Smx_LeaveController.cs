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
    public class Smx_LeaveController : Controller
    {
        private SMAXV2Entities db = new SMAXV2Entities();

        // GET: /Smx_Leave/
        public ActionResult Index()
        {
            return View(db.Smx_Leave.ToList());
        }

        // GET: /Smx_Leave/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_Leave smx_leave = db.Smx_Leave.Find(id);
            if (smx_leave == null)
            {
                return HttpNotFound();
            }
            return View(smx_leave);
        }

        // GET: /Smx_Leave/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Smx_Leave/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Lv_Id,Lv_ShortDesc,Lv_Description,Lv_MaxDays,Lv_MaxAllowed,Lv_Created,LV_Modified,Lv_Modifiedby")] Smx_Leave smx_leave)
        {
            if (ModelState.IsValid)
            {
                db.Smx_Leave.Add(smx_leave);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(smx_leave);
        }

        // GET: /Smx_Leave/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_Leave smx_leave = db.Smx_Leave.Find(id);
            if (smx_leave == null)
            {
                return HttpNotFound();
            }
            return View(smx_leave);
        }

        // POST: /Smx_Leave/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Lv_Id,Lv_ShortDesc,Lv_Description,Lv_MaxDays,Lv_MaxAllowed,Lv_Created,LV_Modified,Lv_Modifiedby")] Smx_Leave smx_leave)
        {
            if (ModelState.IsValid)
            {
                db.Entry(smx_leave).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(smx_leave);
        }

        // GET: /Smx_Leave/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_Leave smx_leave = db.Smx_Leave.Find(id);
            if (smx_leave == null)
            {
                return HttpNotFound();
            }
            return View(smx_leave);
        }

        // POST: /Smx_Leave/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Smx_Leave smx_leave = db.Smx_Leave.Find(id);
            db.Smx_Leave.Remove(smx_leave);
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
