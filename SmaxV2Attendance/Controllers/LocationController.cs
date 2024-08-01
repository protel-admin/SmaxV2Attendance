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
    public class LocationController : Controller
    {
        private SMAXV2Entities db = new SMAXV2Entities();

        public LocationController()
        {
            ViewBag.Info = (new Util.Information()).Location_Info;
        }

        // GET: /Location/
        public ActionResult Index()
        {
            return View(db.Smx_Location.ToList());
        }

        // GET: /Location/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_Location smx_location = db.Smx_Location.Find(id);
            if (smx_location == null)
            {
                return HttpNotFound();
            }
            return View(smx_location);
        }

        // GET: /Location/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Location/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="LN_ID,LN_NAME,LN_ADDRESS,LN_SHORTNAME,LN_CREATED,LN_MODIFIED,LN_MODIFIEDBY")] Smx_Location smx_location)
        {
            if (ModelState.IsValid)
            {
                List<Smx_Location> Location = db.Smx_Location.Where(x => x.LN_NAME == smx_location.LN_NAME).ToList();

                if (Location.Count != 0)
                {
                    List<Smx_Location> LocationList = db.Smx_Location.Where(x => x.LN_NAME == smx_location.LN_NAME).ToList();

                    if (LocationList.Count != 0)
                    {
                        ViewBag.ErrorMsg = (new Util.Information()).Branch_Dup_Msg;
                        return View();
                    }
                    ViewBag.ErrorMsg = (new Util.Information()).Location_Dup_Msg;
                    return View();
                }

                db.Smx_Location.Add(smx_location);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(smx_location);
        }

        // GET: /Location/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_Location smx_location = db.Smx_Location.Find(id);
            if (smx_location == null)
            {
                return HttpNotFound();
            }
            return View(smx_location);
        }

        // POST: /Location/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="LN_ID,LN_NAME,LN_ADDRESS,LN_SHORTNAME,LN_CREATED,LN_MODIFIED,LN_MODIFIEDBY")] Smx_Location smx_location)
        {
            if (ModelState.IsValid)
            {
                db.Entry(smx_location).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(smx_location);
        }

        // GET: /Location/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_Location smx_location = db.Smx_Location.Find(id);
            if (smx_location == null)
            {
                return HttpNotFound();
            }
            return View(smx_location);
        }

        // POST: /Location/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Smx_Location smx_location = db.Smx_Location.Find(id);
            db.Smx_Location.Remove(smx_location);
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
