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
    public class UnitController : Controller
    {
        private SMAXV2Entities db = new SMAXV2Entities();

        public UnitController()
        {
            ViewBag.Info = (new Util.Information()).Unit_Info;
        }

        // GET: /Unit/
        public ActionResult Index()
        {
            return View(db.Smx_Unit.ToList());
        }

        // GET: /Unit/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_Unit smx_unit = db.Smx_Unit.Find(id);
            if (smx_unit == null)
            {
                return HttpNotFound();
            }
            return View(smx_unit);
        }
        // GET: /Unit/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: /Unit/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="UT_ID,UT_NAME,UT_DESCRIPTION,UT_CREATED,UT_MODIFIED,UT_MODIFIEDBY")] Smx_Unit smx_unit)
        {
            if (ModelState.IsValid)
            {

                List<Smx_Unit> Unit = db.Smx_Unit.Where(x => x.UT_NAME == smx_unit.UT_NAME).ToList();

                if (Unit.Count != 0)
                {
                    ViewBag.ErrorMsg = (new Util.Information()).Unit_Dup_Msg;
                    return View();
                }
                db.Smx_Unit.Add(smx_unit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(smx_unit);
        }

        // GET: /Unit/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_Unit smx_unit = db.Smx_Unit.Find(id);
            if (smx_unit == null)
            {
                return HttpNotFound();
            }
            return View(smx_unit);
        }

        // POST: /Unit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="UT_ID,UT_NAME,UT_DESCRIPTION,UT_CREATED,UT_MODIFIED,UT_MODIFIEDBY")] Smx_Unit smx_unit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(smx_unit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(smx_unit);
        }

        // GET: /Unit/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_Unit smx_unit = db.Smx_Unit.Find(id);
            if (smx_unit == null)
            {
                return HttpNotFound();
            }
            return View(smx_unit);
        }

        // POST: /Unit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Smx_Unit smx_unit = db.Smx_Unit.Find(id);
            db.Smx_Unit.Remove(smx_unit);
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
