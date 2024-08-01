using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmaxV2.Models;

namespace SmaxV2.Controllers
{
    [CustAuthFilter]
    public class CardHolderController : Controller
    {
        private SMAXV2Entities db = new SMAXV2Entities();

        // GET: /CardHolder/
        public ActionResult Index(string searchString)
        {
            //var smx_cardholder = db.Smx_CardHolder.Include(s => s.Smx_Branch).Include(s => s.Smx_CardStatus).Include(s => s.Smx_Category).Include(s => s.Smx_Company).Include(s => s.Smx_Department).Include(s => s.Smx_Designation).Include(s => s.Smx_EmployeeStatus).Include(s => s.Smx_Location).Include(s => s.Smx_Message).Include(s => s.Smx_Unit);
            //var smx_cardholder = db.Smx_CardHolder.Include(s => s.Smx_CardStatus).Include(s => s.Smx_EmployeeStatus).Include(s => s.Smx_Location);
            //var smx_cardholder_list = smx_cardholder.Select(e => new CardHolderDisplay()
            //{
            //    Ch_CardNo = e.Ch_CardNo,
            //    Ch_EmpId = e.Ch_EmpId,
            //    Ch_FName = e.Ch_FName,
            //    LN_Name = e.Smx_Location.LN_NAME,
            //    CS_Name = e.Smx_CardStatus.CS_Name,
            //    ES_Name = e.Smx_EmployeeStatus.ES_NAME,
            //    Ch_CardIssued = e.Ch_CardIssued
            //}).ToList();
            ////var smx_cardholder_list = from x in smx_cardholder select x.Ch_CardNo
            //return View(smx_cardholder_list);

            List<CardHolderDisplay> smx_cardholder = new List<CardHolderDisplay>();
            if(searchString != null)
            {
                smx_cardholder = db.Smx_CardHolder.Where(s => s.Ch_CardNo.ToString().Contains(searchString)
                                       || s.Ch_FName.ToUpper().Contains(searchString.ToUpper())
                                       || s.Ch_LName.ToUpper().Contains(searchString.ToUpper())
                                       || s.Ch_EmpId.ToUpper().Contains(searchString.ToUpper())
                                       ).Select(e => new CardHolderDisplay()
                                        {
                                            Ch_CardNo = e.Ch_CardNo,
                                            Ch_EmpId = e.Ch_EmpId,
                                            Ch_FName = e.Ch_FName,
                                            LN_Name = e.Smx_Location.LN_NAME,
                                            CS_Name = e.Smx_CardStatus.CS_Name,
                                            ES_Name = e.Smx_EmployeeStatus.ES_NAME,
                                            Ch_CardIssued = e.Ch_CardIssued
                                        }
                                       ).ToList();

            }
            return View(smx_cardholder);
        }

        // GET: /CardHolder/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_CardHolder smx_cardholder = db.Smx_CardHolder.Find(id);
            if (smx_cardholder == null)
            {
                return HttpNotFound();
            }
            return View(smx_cardholder);
        }

        // GET: /CardHolder/Create
        public ActionResult Create()
        {
            ViewBag.Ch_Br_Id = new SelectList(db.Smx_Branch, "BR_ID", "BR_Name");
            ViewBag.Ch_CS_Id = new SelectList(db.Smx_CardStatus, "CS_Id", "CS_Name");
            ViewBag.Ch_Ct_Id = new SelectList(db.Smx_Category, "CT_ID", "CT_NAME");
            ViewBag.Ch_Cg_Id = new SelectList(db.Smx_Company, "CG_ID", "CG_NAME");
            ViewBag.Ch_Dp_Id = new SelectList(db.Smx_Department, "DP_ID", "DP_NAME");
            ViewBag.Ch_Dn_Id = new SelectList(db.Smx_Designation, "DN_ID", "DN_NAME");
            ViewBag.Ch_Es_ID = new SelectList(db.Smx_EmployeeStatus, "ES_ID", "ES_NAME");
            ViewBag.Ch_Ln_Id = new SelectList(db.Smx_Location, "LN_ID", "LN_NAME");
            ViewBag.Ch_MS_Id = new SelectList(db.Smx_Message, "MS_ID", "MS_NAME");
            ViewBag.Ch_Ut_Id = new SelectList(db.Smx_Unit, "UT_ID", "UT_NAME");
            return View();
        }

        // POST: /CardHolder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "Ch_Photo")] Smx_CardHolder smx_cardholder, HttpPostedFileBase Ch_Photo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if ((smx_cardholder.Ch_ISPin == true && smx_cardholder.Ch_PinNo.ToString().Length == 4 && smx_cardholder.Ch_PinNo > 0 && smx_cardholder.Ch_PinNo < 10000) || smx_cardholder.Ch_ISPin == false)
                    {
                        byte[] imagedata;
                        if (Ch_Photo != null)
                        {
                            imagedata = new byte[Ch_Photo.ContentLength];
                            Ch_Photo.InputStream.Read(imagedata, 0, imagedata.Length);
                            smx_cardholder.Ch_Photo = imagedata;
                        }
                        //byte[] imagedata = new byte[Ch_Photo.ContentLength];
                        //Ch_Photo.InputStream.Read(imagedata, 0, imagedata.Length);
                        //smx_cardholder.Ch_Photo = imagedata;  
                        db.Smx_CardHolder.Add(smx_cardholder);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.ErrorMsg = "Please check for a valid PIN number.";
                    }
                }
            }
            catch(Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    ViewBag.ErrorMsg = "Error adding card holder. Check for a duplicate entries (Emp No).";
                }
                else
                {
                    ViewBag.ErrorMsg = "Error adding card holder.";
                }
                
            }           

            ViewBag.Ch_Br_Id = new SelectList(db.Smx_Branch, "BR_ID", "BR_Name", smx_cardholder.Ch_Br_Id);
            ViewBag.Ch_CS_Id = new SelectList(db.Smx_CardStatus, "CS_Id", "CS_Name", smx_cardholder.Ch_CS_Id);
            ViewBag.Ch_Ct_Id = new SelectList(db.Smx_Category, "CT_ID", "CT_NAME", smx_cardholder.Ch_Ct_Id);
            ViewBag.Ch_Cg_Id = new SelectList(db.Smx_Company, "CG_ID", "CG_NAME", smx_cardholder.Ch_Cg_Id);
            ViewBag.Ch_Dp_Id = new SelectList(db.Smx_Department, "DP_ID", "DP_NAME", smx_cardholder.Ch_Dp_Id);
            ViewBag.Ch_Dn_Id = new SelectList(db.Smx_Designation, "DN_ID", "DN_NAME", smx_cardholder.Ch_Dn_Id);
            ViewBag.Ch_Es_ID = new SelectList(db.Smx_EmployeeStatus, "ES_ID", "ES_NAME", smx_cardholder.Ch_Es_ID);
            ViewBag.Ch_Ln_Id = new SelectList(db.Smx_Location, "LN_ID", "LN_NAME", smx_cardholder.Ch_Ln_Id);
            ViewBag.Ch_MS_Id = new SelectList(db.Smx_Message, "MS_ID", "MS_NAME", smx_cardholder.Ch_MS_Id);
            ViewBag.Ch_Ut_Id = new SelectList(db.Smx_Unit, "UT_ID", "UT_NAME", smx_cardholder.Ch_Ut_Id);
            return View(smx_cardholder);
        }

        // GET: /CardHolder/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_CardHolder smx_cardholder = db.Smx_CardHolder.Find(id);
            if (smx_cardholder == null)
            {
                return HttpNotFound();
            }
            ViewBag.Ch_Br_Id = new SelectList(db.Smx_Branch, "BR_ID", "BR_Name", smx_cardholder.Ch_Br_Id);
            ViewBag.Ch_CS_Id = new SelectList(db.Smx_CardStatus, "CS_Id", "CS_Name", smx_cardholder.Ch_CS_Id);
            ViewBag.Ch_Ct_Id = new SelectList(db.Smx_Category, "CT_ID", "CT_NAME", smx_cardholder.Ch_Ct_Id);
            ViewBag.Ch_Cg_Id = new SelectList(db.Smx_Company, "CG_ID", "CG_NAME", smx_cardholder.Ch_Cg_Id);
            ViewBag.Ch_Dp_Id = new SelectList(db.Smx_Department, "DP_ID", "DP_NAME", smx_cardholder.Ch_Dp_Id);
            ViewBag.Ch_Dn_Id = new SelectList(db.Smx_Designation, "DN_ID", "DN_NAME", smx_cardholder.Ch_Dn_Id);
            ViewBag.Ch_Es_ID = new SelectList(db.Smx_EmployeeStatus, "ES_ID", "ES_NAME", smx_cardholder.Ch_Es_ID);
            ViewBag.Ch_Ln_Id = new SelectList(db.Smx_Location, "LN_ID", "LN_NAME", smx_cardholder.Ch_Ln_Id);
            ViewBag.Ch_MS_Id = new SelectList(db.Smx_Message, "MS_ID", "MS_NAME", smx_cardholder.Ch_MS_Id);
            ViewBag.Ch_Ut_Id = new SelectList(db.Smx_Unit, "UT_ID", "UT_NAME", smx_cardholder.Ch_Ut_Id);
            ViewData["Ch_Dn_Id_VAL"] = smx_cardholder.Ch_Dn_Id.ToString();
            ViewData["Ch_Ct_Id_VAL"] = smx_cardholder.Ch_Ct_Id.ToString();
            ViewData["Ch_Cg_Id_VAL"] = smx_cardholder.Ch_Cg_Id.ToString();
            ViewData["Ch_Ut_Id_VAL"] = smx_cardholder.Ch_Ut_Id.ToString();
            ViewData["Ch_Br_Id_VAL"] = smx_cardholder.Ch_Br_Id.ToString();
            ViewData["Ch_Ln_Id_VAL"] = smx_cardholder.Ch_Ln_Id.ToString();
            ViewData["Ch_Dp_Id_VAL"] = smx_cardholder.Ch_Dp_Id.ToString();
            ViewData["Ch_CS_Id_VAL"] = smx_cardholder.Ch_CS_Id.ToString();
            ViewData["Ch_MS_Id_VAL"] = smx_cardholder.Ch_MS_Id.ToString();
            ViewData["Ch_Es_ID_VAL"] = smx_cardholder.Ch_Es_ID.ToString();
            return View(smx_cardholder);
        }

        // POST: /CardHolder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude = "Ch_Photo")] Smx_CardHolder smx_cardholder, HttpPostedFileBase Ch_Photo)
        {
            if (ModelState.IsValid)
            {
                if ((smx_cardholder.Ch_ISPin == true))
                {
                    if(smx_cardholder.Ch_PinNo.ToString().Length < 4 || smx_cardholder.Ch_PinNo == 0 || smx_cardholder.Ch_PinNo >= 10000)
                    {
                        ViewBag.Ch_Br_Id = new SelectList(db.Smx_Branch, "BR_ID", "BR_Name", smx_cardholder.Ch_Br_Id);
                        ViewBag.Ch_CS_Id = new SelectList(db.Smx_CardStatus, "CS_Id", "CS_Name", smx_cardholder.Ch_CS_Id);
                        ViewBag.Ch_Ct_Id = new SelectList(db.Smx_Category, "CT_ID", "CT_NAME", smx_cardholder.Ch_Ct_Id);
                        ViewBag.Ch_Cg_Id = new SelectList(db.Smx_Company, "CG_ID", "CG_NAME", smx_cardholder.Ch_Cg_Id);
                        ViewBag.Ch_Dp_Id = new SelectList(db.Smx_Department, "DP_ID", "DP_NAME", smx_cardholder.Ch_Dp_Id);
                        ViewBag.Ch_Dn_Id = new SelectList(db.Smx_Designation, "DN_ID", "DN_NAME", smx_cardholder.Ch_Dn_Id);
                        ViewBag.Ch_Es_ID = new SelectList(db.Smx_EmployeeStatus, "ES_ID", "ES_NAME", smx_cardholder.Ch_Es_ID);
                        ViewBag.Ch_Ln_Id = new SelectList(db.Smx_Location, "LN_ID", "LN_NAME", smx_cardholder.Ch_Ln_Id);
                        ViewBag.Ch_MS_Id = new SelectList(db.Smx_Message, "MS_ID", "MS_NAME", smx_cardholder.Ch_MS_Id);
                        ViewBag.Ch_Ut_Id = new SelectList(db.Smx_Unit, "UT_ID", "UT_NAME", smx_cardholder.Ch_Ut_Id);
                        ViewData["Ch_Dn_Id_VAL"] = smx_cardholder.Ch_Dn_Id.ToString();
                        ViewData["Ch_Ct_Id_VAL"] = smx_cardholder.Ch_Ct_Id.ToString();
                        ViewData["Ch_Cg_Id_VAL"] = smx_cardholder.Ch_Cg_Id.ToString();
                        ViewData["Ch_Ut_Id_VAL"] = smx_cardholder.Ch_Ut_Id.ToString();
                        ViewData["Ch_Br_Id_VAL"] = smx_cardholder.Ch_Br_Id.ToString();
                        ViewData["Ch_Ln_Id_VAL"] = smx_cardholder.Ch_Ln_Id.ToString();
                        ViewData["Ch_Dp_Id_VAL"] = smx_cardholder.Ch_Dp_Id.ToString();
                        ViewData["Ch_CS_Id_VAL"] = smx_cardholder.Ch_CS_Id.ToString();
                        ViewData["Ch_MS_Id_VAL"] = smx_cardholder.Ch_MS_Id.ToString();
                        ViewData["Ch_Es_ID_VAL"] = smx_cardholder.Ch_Es_ID.ToString();
                        ViewBag.ErrorMsg = "Please enter a valid PIN number";
                        return View(smx_cardholder);
                    }
                }

                CheckHotlist(smx_cardholder);
                byte[] imagedata;
                if (Ch_Photo != null)
                {
                    imagedata = new byte[Ch_Photo.ContentLength];
                    Ch_Photo.InputStream.Read(imagedata, 0, imagedata.Length);
                    smx_cardholder.Ch_Photo = imagedata;
                }
                else
                {
                    imagedata = GetImageFromDataBase(smx_cardholder.Ch_EmpId);
                    smx_cardholder.Ch_Photo = imagedata;
                    //var entry = db.Entry(smx_cardholder);
                    //entry.State = EntityState.Modified;
                    //var excluded = new[] { "Ch_Photo" };
                    //foreach (var name in excluded)
                    //{
                    //    entry.Property(name).IsModified = false;
                    //}
                    //entry.Property("Ch_Photo").IsModified = false;
                }
                byte[] Finger1 = new byte[256];
                byte[] Finger2 = new byte[256];
                GetFingerFromDB(smx_cardholder.Ch_EmpId, ref Finger1, ref Finger2);
                smx_cardholder.Ch_Finger1 = Finger1;
                smx_cardholder.Ch_Finger2 = Finger2;
                db.Entry(smx_cardholder).State = EntityState.Modified;
                int modified = db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Ch_Br_Id = new SelectList(db.Smx_Branch, "BR_ID", "BR_Name", smx_cardholder.Ch_Br_Id);
            ViewBag.Ch_CS_Id = new SelectList(db.Smx_CardStatus, "CS_Id", "CS_Name", smx_cardholder.Ch_CS_Id);
            ViewBag.Ch_Ct_Id = new SelectList(db.Smx_Category, "CT_ID", "CT_NAME", smx_cardholder.Ch_Ct_Id);
            ViewBag.Ch_Cg_Id = new SelectList(db.Smx_Company, "CG_ID", "CG_NAME", smx_cardholder.Ch_Cg_Id);
            ViewBag.Ch_Dp_Id = new SelectList(db.Smx_Department, "DP_ID", "DP_NAME", smx_cardholder.Ch_Dp_Id);
            ViewBag.Ch_Dn_Id = new SelectList(db.Smx_Designation, "DN_ID", "DN_NAME", smx_cardholder.Ch_Dn_Id);
            ViewBag.Ch_Es_ID = new SelectList(db.Smx_EmployeeStatus, "ES_ID", "ES_NAME", smx_cardholder.Ch_Es_ID);
            ViewBag.Ch_Ln_Id = new SelectList(db.Smx_Location, "LN_ID", "LN_NAME", smx_cardholder.Ch_Ln_Id);
            ViewBag.Ch_MS_Id = new SelectList(db.Smx_Message, "MS_ID", "MS_NAME", smx_cardholder.Ch_MS_Id);
            ViewBag.Ch_Ut_Id = new SelectList(db.Smx_Unit, "UT_ID", "UT_NAME", smx_cardholder.Ch_Ut_Id);
            ViewData["Ch_Dn_Id_VAL"] = smx_cardholder.Ch_Dn_Id.ToString();
            ViewData["Ch_Ct_Id_VAL"] = smx_cardholder.Ch_Ct_Id.ToString();
            ViewData["Ch_Cg_Id_VAL"] = smx_cardholder.Ch_Cg_Id.ToString();
            ViewData["Ch_Ut_Id_VAL"] = smx_cardholder.Ch_Ut_Id.ToString();
            ViewData["Ch_Br_Id_VAL"] = smx_cardholder.Ch_Br_Id.ToString();
            ViewData["Ch_Ln_Id_VAL"] = smx_cardholder.Ch_Ln_Id.ToString();
            ViewData["Ch_Dp_Id_VAL"] = smx_cardholder.Ch_Dp_Id.ToString();
            ViewData["Ch_CS_Id_VAL"] = smx_cardholder.Ch_CS_Id.ToString();
            ViewData["Ch_MS_Id_VAL"] = smx_cardholder.Ch_MS_Id.ToString();
            ViewData["Ch_Es_ID_VAL"] = smx_cardholder.Ch_Es_ID.ToString();
            return View(smx_cardholder);
        }

        // GET: /CardHolder/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_CardHolder smx_cardholder = db.Smx_CardHolder.Find(id);
            if (smx_cardholder == null)
            {
                return HttpNotFound();
            }
            return View(smx_cardholder);
        }

        // POST: /CardHolder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Smx_CardHolder smx_cardholder = db.Smx_CardHolder.Find(id);
            db.Smx_CardHolder.Remove(smx_cardholder);
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

        public ActionResult RetrieveImage(string id)
        {
            byte[] cover = GetImageFromDataBase(id);
            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }

        public byte[] GetImageFromDataBase(string Id)
        {
            var q = from temp in db.Smx_CardHolder where temp.Ch_EmpId == Id select temp.Ch_Photo;
            byte[] cover = q.First();
            return cover;
        }

        public void GetFingerFromDB(string Id, ref byte[] Finger1, ref byte[] Finger2)
        {
            var f1 = from temp in db.Smx_CardHolder  where temp.Ch_EmpId == Id select temp.Ch_Finger1 ;
            var f2 = from temp in db.Smx_CardHolder where temp.Ch_EmpId == Id select temp.Ch_Finger2;
            //byte[] cover = q.First();
            Finger1 = f1.First();
            Finger2 = f2.First();

        }

        public void CheckHotlist(Smx_CardHolder obj)
        {
            //Util.CommonClass CC = new Util.CommonClass();
            //DataTable dt = CC.GetDataTable("Select * from Smx_CardHolder where [Ch_EmpId] = '" + obj.Ch_EmpId + "'");
            //if(dt.Rows.Count > 0)
            //{
            //    if(dt.Rows[0]["CH_CS_ID"].ToString() != "6" && obj.Ch_CS_Id == 6)
            //    {
            //        CC.ExecuteSQL("EXEC sp_InsertHotlist '" + obj.Ch_EmpId + "'");
            //    }
            //}
        }
    }
}
