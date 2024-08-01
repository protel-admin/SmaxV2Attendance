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
    public class HolidayController : Controller
    {
        private SMAXV2Entities db = new SMAXV2Entities();

        public HolidayController()
        {
            ViewBag.Info = (new Util.Information()).Holiday_Info;
        }

        // GET: /Holiday/
        public ActionResult Index()
        {
            return View(db.Smx_Holiday.ToList());
        }

        // GET: /Holiday/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_Holiday smx_holiday = db.Smx_Holiday.Find(id);
            if (smx_holiday == null)
            {
                return HttpNotFound();
            }
            return View(smx_holiday);
        }

        // GET: /Holiday/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Holiday/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="HD_ID,HD_DATE,HD_DESC,HD_ISREADERDOWNLOAD,HD_UPDATE_STATUS,HD_CREATED,HD_MODIFIED,HD_MODIFIEDBY")] Smx_Holiday smx_holiday)
        {
            if (ModelState.IsValid)
            {
                List<Smx_Holiday> HolidayList = db.Smx_Holiday.Where(x => x.HD_DATE == smx_holiday.HD_DATE).ToList();

                if (HolidayList.Count != 0)
                {
                    ViewBag.ErrorMsg = (new Util.Information()).Holiday_Dup_Msg;
                    return View();
                }
                db.Smx_Holiday.Add(smx_holiday);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(smx_holiday);
        }

        // GET: /Holiday/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_Holiday smx_holiday = db.Smx_Holiday.Find(id);
            if (smx_holiday == null)
            {
                return HttpNotFound();
            }
            return View(smx_holiday);
        }

        // POST: /Holiday/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="HD_ID,HD_DATE,HD_DESC,HD_ISREADERDOWNLOAD,HD_UPDATE_STATUS,HD_CREATED,HD_MODIFIED,HD_MODIFIEDBY")] Smx_Holiday smx_holiday)
        {
            if (ModelState.IsValid)
            {
                db.Entry(smx_holiday).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(smx_holiday);
        }

        // GET: /Holiday/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_Holiday smx_holiday = db.Smx_Holiday.Find(id);
            if (smx_holiday == null)
            {
                return HttpNotFound();
            }
            return View(smx_holiday);
        }

        // POST: /Holiday/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Smx_Holiday smx_holiday = db.Smx_Holiday.Find(id);
            db.Smx_Holiday.Remove(smx_holiday);
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
