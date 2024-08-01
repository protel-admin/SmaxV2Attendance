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
    //[CustAuthFilter]
    public class AccessLevelController : Controller
    {
        private SMAXV2Entities db = new SMAXV2Entities();

        // GET: /AccessLevel/
        public ActionResult Index()
        {
            return View(db.Smx_AccessLevel.ToList());
        }

        // GET: /AccessLevel/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_AccessLevel smx_accesslevel = db.Smx_AccessLevel.Find(id);
            if (smx_accesslevel == null)
            {
                return HttpNotFound();
            }
            return View(smx_accesslevel);
        }

        // GET: /AccessLevel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /AccessLevel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="AL_ID,AL_NAME,AL_CREATED,AL_MODIFIED,AL_MODIFIEDBY")] Smx_AccessLevel smx_accesslevel)
        {
            if (ModelState.IsValid)
            {
                db.Smx_AccessLevel.Add(smx_accesslevel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(smx_accesslevel);
        }

        [HttpPost]
        public JsonResult AddAL(Smx_AccessLevel smx_accesslevel)
        {
            if (smx_accesslevel.AL_NAME != null && smx_accesslevel.AL_NAME.Trim() != "")            
            {
                List<Smx_AccessLevel> AccessLevel = db.Smx_AccessLevel.Where(x => x.AL_NAME == smx_accesslevel.AL_NAME).ToList();

                if (AccessLevel.Count() != 0)
                {
                    ViewBag.ErrorMsg = "Access Level Name already Exists!";
                    return Json(new { Err = "1" }, JsonRequestBehavior.AllowGet);
                }
                smx_accesslevel.AL_CREATED = DateTime.Now;
                smx_accesslevel.AL_MODIFIED = DateTime.Now;
                smx_accesslevel.AL_MODIFIEDBY = "Admin";
                db.Smx_AccessLevel.Add(smx_accesslevel);
                db.SaveChanges();
                return Json(new { Err = "0" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ViewBag.ErrorMsg = "Access Level Name Should Not be Empty!";
                return Json(new { Err = "2" }, JsonRequestBehavior.AllowGet);
            }

        }

        // GET: /AccessLevel/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_AccessLevel smx_accesslevel = db.Smx_AccessLevel.Find(id);
            if (smx_accesslevel == null)
            {
                return HttpNotFound();
            }
            return View(smx_accesslevel);
        }

        // POST: /AccessLevel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="AL_ID,AL_NAME,AL_CREATED,AL_MODIFIED,AL_MODIFIEDBY")] Smx_AccessLevel smx_accesslevel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(smx_accesslevel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(smx_accesslevel);
        }

        // GET: /AccessLevel/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_AccessLevel smx_accesslevel = db.Smx_AccessLevel.Find(id);
            if (smx_accesslevel == null)
            {
                return HttpNotFound();
            }
            return View(smx_accesslevel);
        }

        // POST: /AccessLevel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Smx_AccessLevel smx_accesslevel = db.Smx_AccessLevel.Find(id);
            db.Smx_AccessLevel.Remove(smx_accesslevel);
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
