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
    public class CategoryController : Controller
    {
        private SMAXV2Entities db = new SMAXV2Entities();

        public CategoryController()
        {
            ViewBag.Info = (new Util.Information()).Category_Info;
        }
        
        // GET: /Category/
        public ActionResult Index()
        {
            return View(db.Smx_Category.ToList());
        }

        // GET: /Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_Category smx_category = db.Smx_Category.Find(id);
            if (smx_category == null)
            {
                return HttpNotFound();
            }
            return View(smx_category);
        }

        // GET: /Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="CT_ID,CT_NAME,CT_SHORTNAME,CT_CREATED,CT_MODIFIED,CT_MODIFIEDBY")] Smx_Category smx_category)
        {
            if (ModelState.IsValid)
            {
                List<Smx_Category> Category = db.Smx_Category.Where(x => x.CT_NAME == smx_category.CT_NAME).ToList();

                if (Category.Count != 0)
                {
                    ViewBag.ErrorMsg = (new Util.Information()).Category_Dup_Msg;
                    return View();
                }
                db.Smx_Category.Add(smx_category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(smx_category);
        }

        // GET: /Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_Category smx_category = db.Smx_Category.Find(id);
            if (smx_category == null)
            {
                return HttpNotFound();
            }
            return View(smx_category);
        }

        // POST: /Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="CT_ID,CT_NAME,CT_SHORTNAME,CT_CREATED,CT_MODIFIED,CT_MODIFIEDBY")] Smx_Category smx_category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(smx_category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(smx_category);
        }

        // GET: /Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_Category smx_category = db.Smx_Category.Find(id);
            if (smx_category == null)
            {
                return HttpNotFound();
            }
            return View(smx_category);
        }

        // POST: /Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Smx_Category smx_category = db.Smx_Category.Find(id);
            db.Smx_Category.Remove(smx_category);
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
