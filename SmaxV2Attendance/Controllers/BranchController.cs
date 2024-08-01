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
    public class BranchController : Controller
    {
        private SMAXV2Entities db = new SMAXV2Entities();

        public BranchController()
        {
            ViewBag.Info = (new Util.Information()).Branch_Info;
        }

        // GET: /Branch/
        public ActionResult Index()
        {
            return View(db.Smx_Branch.ToList());
        }

        // GET: /Branch/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_Branch smx_branch = db.Smx_Branch.Find(id);
            if (smx_branch == null)
            {
                return HttpNotFound();
            }
            return View(smx_branch);
        }

        // GET: /Branch/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Branch/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="BR_ID,BR_Name,BR_CREATED,BR_MODIFIED,BR_MODIFIEDBY")] Smx_Branch smx_branch)
        {
            if (ModelState.IsValid)
            {
                List<Smx_Branch> Branch = db.Smx_Branch.Where(x => x.BR_Name == smx_branch.BR_Name).ToList();

                if (Branch.Count != 0)
                {
                    ViewBag.ErrorMsg = (new Util.Information()).Branch_Dup_Msg;
                    return View();
                }
                db.Smx_Branch.Add(smx_branch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(smx_branch);
        }

        // GET: /Branch/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_Branch smx_branch = db.Smx_Branch.Find(id);
            if (smx_branch == null)
            {
                return HttpNotFound();
            }
            return View(smx_branch);
        }

        // POST: /Branch/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="BR_ID,BR_Name,BR_CREATED,BR_MODIFIED,BR_MODIFIEDBY")] Smx_Branch smx_branch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(smx_branch).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(smx_branch);
        }

        // GET: /Branch/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_Branch smx_branch = db.Smx_Branch.Find(id);
            if (smx_branch == null)
            {
                return HttpNotFound();
            }
            return View(smx_branch);
        }

        // POST: /Branch/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Smx_Branch smx_branch = db.Smx_Branch.Find(id);
            db.Smx_Branch.Remove(smx_branch);
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
