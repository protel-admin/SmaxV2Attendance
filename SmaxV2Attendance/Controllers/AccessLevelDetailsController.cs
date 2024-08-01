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
    public class AccessLevelDetailsController : Controller
    {
        private SMAXV2Entities db = new SMAXV2Entities();

        // GET: /AccessLevelDetails/
        public ActionResult Index()
        {
            var smx_accessleveldetails = db.Smx_AccessLevelDetails.Include(s => s.Smx_AccessLevel).Include(s => s.Smx_TimeZone);
            return View(smx_accessleveldetails.ToList());
        }

        // GET: /AccessLevelDetails/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_AccessLevelDetails smx_accessleveldetails = db.Smx_AccessLevelDetails.Find(id);
            if (smx_accessleveldetails == null)
            {
                return HttpNotFound();
            }
            return View(smx_accessleveldetails);
        }

        // GET: /AccessLevelDetails/Create
        public ActionResult Create()
        {
            ViewBag.ALD_AL_ID = new SelectList(db.Smx_AccessLevel, "AL_ID", "AL_NAME");
            ViewBag.ALD_TZ_ID = new SelectList(db.Smx_TimeZone, "TZ_ID", "TZ_NAME");
            ViewBag.ALD_DE_IPADDRESS = Util.Utility.GetDeviceList();
            return View();
        }

        // POST: /AccessLevelDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ALD_ID,ALD_AL_ID,ALD_NODEID,ALD_TZ_ID,ALD_READER_IPADDRESS,ALD_LN_ID,ALD_CREATED,ALD_MODIFIED,ALD_MODIFIEDBY")] Smx_AccessLevelDetails smx_accessleveldetails)
        {
            if (ModelState.IsValid)
            {
                db.Smx_AccessLevelDetails.Add(smx_accessleveldetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ALD_AL_ID = new SelectList(db.Smx_AccessLevel, "AL_ID", "AL_NAME", smx_accessleveldetails.ALD_AL_ID);
            ViewBag.ALD_TZ_ID = new SelectList(db.Smx_TimeZone, "TZ_ID", "TZ_NAME", smx_accessleveldetails.ALD_TZ_ID);
            return View(smx_accessleveldetails);
        }

        [HttpPost]
        public JsonResult Save([Bind(Include = "ALD_ID,ALD_AL_ID,ALD_NODEID,ALD_TZ_ID,ALD_READER_IPADDRESS,ALD_LN_ID")] Smx_AccessLevelDetails[] smx_accessleveldetails)
        {
            if (ModelState.IsValid)
            {
                decimal alid = smx_accessleveldetails[0].ALD_AL_ID;
                List<Smx_AccessLevelDetails> al_lst = db.Smx_AccessLevelDetails.Where(e => e.ALD_AL_ID.Equals(alid)).ToList();
                if (al_lst.Count() == 0)
                {
                    //------- Save the new records for the Timezone id TZD_TZ_ID  -----------------------------
                    foreach (Smx_AccessLevelDetails x in smx_accessleveldetails)
                    {
                        x.ALD_CREATED = DateTime.Now;
                        x.ALD_MODIFIED = DateTime.Now;
                        x.ALD_MODIFIEDBY = "Admin";
                        db.Smx_AccessLevelDetails.Add(x);
                        db.SaveChanges();
                    }
                    return Json(new { updated = "true" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //------- First delete the existind recirds and Save the new records for the Timezone id TZD_TZ_ID  ----------------------
                    DateTime created = Convert.ToDateTime(al_lst[0].ALD_CREATED);
                    db.Database.ExecuteSqlCommand("Delete from Smx_AccessLevelDetails where ALD_AL_ID='" + smx_accessleveldetails[0].ALD_AL_ID + "'");
                    foreach (Smx_AccessLevelDetails x in smx_accessleveldetails)
                    {
                        x.ALD_CREATED = created;
                        x.ALD_MODIFIED = DateTime.Now;
                        x.ALD_MODIFIEDBY = "Admin";
                        db.Smx_AccessLevelDetails.Add(x);
                        db.SaveChanges();
                    }
                    return Json(new { updated = "true" }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { updated = "false" }, JsonRequestBehavior.AllowGet);
        }
        // GET: /AccessLevelDetails/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_AccessLevelDetails smx_accessleveldetails = db.Smx_AccessLevelDetails.Find(id);
            if (smx_accessleveldetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.ALD_AL_ID = new SelectList(db.Smx_AccessLevel, "AL_ID", "AL_NAME", smx_accessleveldetails.ALD_AL_ID);
            ViewBag.ALD_TZ_ID = new SelectList(db.Smx_TimeZone, "TZ_ID", "TZ_NAME", smx_accessleveldetails.ALD_TZ_ID);
            return View(smx_accessleveldetails);
        }

        // POST: /AccessLevelDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ALD_ID,ALD_AL_ID,ALD_NODEID,ALD_TZ_ID,ALD_READER_IPADDRESS,ALD_CREATED,ALD_MODIFIED,ALD_MODIFIEDBY")] Smx_AccessLevelDetails smx_accessleveldetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(smx_accessleveldetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ALD_AL_ID = new SelectList(db.Smx_AccessLevel, "AL_ID", "AL_NAME", smx_accessleveldetails.ALD_AL_ID);
            ViewBag.ALD_TZ_ID = new SelectList(db.Smx_TimeZone, "TZ_ID", "TZ_NAME", smx_accessleveldetails.ALD_TZ_ID);
            return View(smx_accessleveldetails);
        }

        // GET: /AccessLevelDetails/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_AccessLevelDetails smx_accessleveldetails = db.Smx_AccessLevelDetails.Find(id);
            if (smx_accessleveldetails == null)
            {
                return HttpNotFound();
            }
            return View(smx_accessleveldetails);
        }

        // POST: /AccessLevelDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        public JsonResult DeleteConfirmed(Smx_AccessLevelDetails[] smx_accessleveldetails)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    decimal id = smx_accessleveldetails[0].ALD_AL_ID;
                    List<Smx_CardholderAccessLevel> smx_cardholderaccesslvl = db.Smx_CardholderAccessLevel.Where(x => x.CAL_AL_ID == id).ToList();
                    if (smx_cardholderaccesslvl.Count == 0)
                    {
                        db.Database.ExecuteSqlCommand("Delete from Smx_AccessLevelDetails where ALD_AL_ID='" + smx_accessleveldetails[0].ALD_AL_ID + "'");
                        db.Database.ExecuteSqlCommand("Delete from Smx_AccessLevel where AL_ID='" + smx_accessleveldetails[0].ALD_AL_ID + "'");
                    }
                    else
                    {
                        return Json(new { status = "This Access Level has been linked to one or more card holder(s). \n Delete operation failed." }, JsonRequestBehavior.AllowGet);
                    }
                    
                }
                catch(Exception ex)
                {
                    if (ex.Message.Contains("REFERENCE"))
                    {
                        return Json(new { status = "This Access Level has been used in other places(Forein Key Vialation). \n Delete operation failed." }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { status = "Error Occured while deleting accesslevel details..." }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(new { status = "true" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAccessLevelDetails(decimal ALD_AL_ID)
        {
            Array smx_accessleveldetails = Util.Utility.GetAccessLevelDetailsList(Convert.ToDecimal(ALD_AL_ID));
            return Json(smx_accessleveldetails, JsonRequestBehavior.AllowGet);
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
