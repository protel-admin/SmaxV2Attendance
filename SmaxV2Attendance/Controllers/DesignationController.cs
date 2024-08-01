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
    public class DesignationController : Controller
    {
        private SMAXV2Entities db = new SMAXV2Entities();

        public DesignationController()
        {
            ViewBag.Info = (new Util.Information()).Designation_Info;
        }

        // GET: /Designation/
        public ActionResult Index()
        {
            return View(db.Smx_Designation.ToList());
        }

        // GET: /Designation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_Designation smx_designation = db.Smx_Designation.Find(id);
            if (smx_designation == null)
            {
                return HttpNotFound();
            }
            return View(smx_designation);
        }

        // GET: /Designation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Designation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="DN_ID,DN_NAME,DN_SHORTNAME,DN_CREATED,DN_MODIFIED,DN_MODIFIEDBY")] Smx_Designation smx_designation)
        {
            if (ModelState.IsValid)
            {
                db.Smx_Designation.Add(smx_designation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(smx_designation);
        }

        // GET: /Designation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_Designation smx_designation = db.Smx_Designation.Find(id);
            if (smx_designation == null)
            {
                return HttpNotFound();
            }
            return View(smx_designation);
        }

        // POST: /Designation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="DN_ID,DN_NAME,DN_SHORTNAME,DN_CREATED,DN_MODIFIED,DN_MODIFIEDBY")] Smx_Designation smx_designation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(smx_designation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(smx_designation);
        }

        // GET: /Designation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_Designation smx_designation = db.Smx_Designation.Find(id);
            if (smx_designation == null)
            {
                return HttpNotFound();
            }
            return View(smx_designation);
        }

        // POST: /Designation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Smx_Designation smx_designation = db.Smx_Designation.Find(id);
            db.Smx_Designation.Remove(smx_designation);
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
