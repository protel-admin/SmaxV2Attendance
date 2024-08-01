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
    public class DepartmentController : Controller
    {
        private SMAXV2Entities db = new SMAXV2Entities();

        public DepartmentController()
        {
            ViewBag.Info = (new Util.Information()).Department_Info;
        }

        // GET: /Department/
        public ActionResult Index()
        {
            return View(db.Smx_Department.ToList());
        }

        // GET: /Department/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_Department smx_department = db.Smx_Department.Find(id);
            if (smx_department == null)
            {
                return HttpNotFound();
            }
            return View(smx_department);
        }

        // GET: /Department/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Department/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="DP_ID,DP_NAME,DP_SHORTNAME,DP_CREATED,DP_MODIFIED,DP_MODIFIEDBY")] Smx_Department smx_department)
        {
            if (ModelState.IsValid)
            {
                List<Smx_Department> Department = db.Smx_Department.Where(x => x.DP_NAME == smx_department.DP_NAME).ToList();

                if (Department.Count != 0)
                {
                    ViewBag.ErrorMsg = (new Util.Information()).Department_Dup_Msg;
                    return View();
                }
                db.Smx_Department.Add(smx_department);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(smx_department);
        }

        // GET: /Department/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_Department smx_department = db.Smx_Department.Find(id);
            if (smx_department == null)
            {
                return HttpNotFound();
            }
            return View(smx_department);
        }

        // POST: /Department/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="DP_ID,DP_NAME,DP_SHORTNAME,DP_CREATED,DP_MODIFIED,DP_MODIFIEDBY")] Smx_Department smx_department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(smx_department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(smx_department);
        }

        // GET: /Department/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_Department smx_department = db.Smx_Department.Find(id);
            if (smx_department == null)
            {
                return HttpNotFound();
            }
            return View(smx_department);
        }

        // POST: /Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Smx_Department smx_department = db.Smx_Department.Find(id);
            db.Smx_Department.Remove(smx_department);
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
