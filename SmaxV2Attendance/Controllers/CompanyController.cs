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
    public class CompanyController : Controller
    {
        private SMAXV2Entities db = new SMAXV2Entities();

        public CompanyController()
        {
            ViewBag.Info = (new Util.Information()).Company_Info;
        }

        // GET: /Company/
        public ActionResult Index()
        {
            return View(db.Smx_Company.ToList());
        }

        // GET: /Company/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_Company smx_company = db.Smx_Company.Find(id);
            if (smx_company == null)
            {
                return HttpNotFound();
            }
            return View(smx_company);
        }

        // GET: /Company/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Company/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Smx_Company smx_company)
        {
            if (ModelState.IsValid)
            {
                List<Smx_Company> Company = db.Smx_Company.Where(x => x.CG_NAME == smx_company.CG_NAME).ToList();

                if (Company.Count != 0)
                {
                    ViewBag.ErrorMsg = (new Util.Information()).Company_Dup_Msg;
                    return View();
                }

                db.Smx_Company.Add(smx_company);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(smx_company);
        }

        // GET: /Company/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_Company smx_company = db.Smx_Company.Find(id);
            if (smx_company == null)
            {
                return HttpNotFound();
            }
            return View(smx_company);
        }

        // POST: /Company/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Smx_Company smx_company)
        {
            if (ModelState.IsValid)
            {
                db.Entry(smx_company).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(smx_company);
        }

        // GET: /Company/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_Company smx_company = db.Smx_Company.Find(id);
            if (smx_company == null)
            {
                return HttpNotFound();
            }
            return View(smx_company);
        }

        // POST: /Company/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Smx_Company smx_company = db.Smx_Company.Find(id);
            db.Smx_Company.Remove(smx_company);
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
