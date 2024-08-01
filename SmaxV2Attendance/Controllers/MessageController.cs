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
    public class MessageController : Controller
    {
        private SMAXV2Entities db = new SMAXV2Entities();

        // GET: /Message/
        public ActionResult Index()
        {
            return View(db.Smx_Message.ToList());
        }

        // GET: /Message/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_Message smx_message = db.Smx_Message.Find(id);
            if (smx_message == null)
            {
                return HttpNotFound();
            }
            return View(smx_message);
        }

        // GET: /Message/Create
        public ActionResult Create()
        {
            ViewData["MS_NAME"] = Util.Utility.GetMessageDDL();
            return View();
        }

        // POST: /Message/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MS_ID,MS_NAME,MS_LINE1,MS_LINE2,MS_CREATED,MS_MODIFIED,MS_MODIFIEDBY")] Smx_Message smx_message)
        {
            if (ModelState.IsValid)
            {
                db.Smx_Message.Add(smx_message);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["MS_NAME"] = Util.Utility.GetMessageDDL();
            return View(smx_message);
        }

        // GET: /Message/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_Message smx_message = db.Smx_Message.Find(id);
            if (smx_message == null)
            {
                return HttpNotFound();
            }
            ViewData["MS_NAME"] = Util.Utility.GetMessageDDL();
            return View(smx_message);
        }

        // POST: /Message/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MS_ID,MS_NAME,MS_LINE1,MS_LINE2,MS_CREATED,MS_MODIFIED,MS_MODIFIEDBY")] Smx_Message smx_message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(smx_message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["MS_NAME"] = Util.Utility.GetMessageDDL();
            return View(smx_message);
        }

        // GET: /Message/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_Message smx_message = db.Smx_Message.Find(id);
            if (smx_message == null)
            {
                return HttpNotFound();
            }
            return View(smx_message);
        }

        // POST: /Message/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Smx_Message smx_message = db.Smx_Message.Find(id);
            db.Smx_Message.Remove(smx_message);
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
