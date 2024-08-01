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
    public class UserController : Controller
    {
        private SMAXV2Entities db = new SMAXV2Entities();
        // GET: User
        // GET: /User/

        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.UserGroup);


            return View(users.ToList());
        }

        // GET: /User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            Util.Crypto cry = new Util.Crypto();
            user.US_Password = cry.Encrypt(user.US_Password, "Intelli");


            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: /User/Create
        public ActionResult Create()
        {
            //if (db.Users.Where(x => x.US_Login == Session[]).ToList().Count > 0)            
            ViewBag.FK_UG_GroupId = new SelectList(db.UserGroups, "PK_UG_GroupId", "UG_GroupName");
            return View();
        }

        // POST: /User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "US_Id,US_User,US_Login,US_Password,FK_UG_GroupId,US_Created,US_Modified")] User user)
        {
            //Util.Crypto();     

            if (ModelState.IsValid)
            {
                //if (db.Smx_CardHolder.Where(x => x.Ch_EmpId == user.US_Login).ToList().Count > 0)
                //{       
                Util.Crypto cry = new Util.Crypto();
                //  user.US_Password = cry.Encrypt(user.US_Password, "Intelli");
                db.Users.Add(user);

                //if (db.Database.ExecuteSqlCommand("select count([PH_US_Password])  from Passwordhistory where [PH_US_Password] in (select top 3 [PH_US_Password]  from Passwordhistory where [FK_US_ID]=" + user.US_Id + " order by  PH_US_Created desc) and [PH_US_Password]='" + cry.Encrypt(user.US_Password, "Intelli") + "'") == 0)
                //{
                Util.CommonClass CC = new Util.CommonClass();
                if (CC.GetDataTable("select [US_Login]  from Users where [US_Login] ='" + user.US_Login + "'").Rows.Count == 0)
                {

                    db.SaveChanges();
                    db.Database.ExecuteSqlCommand("update users set us_password='" + cry.Encrypt(user.US_Password, "Intelli") + "' where US_Login='" + user.US_Login + "'");
                    //   db.Database.ExecuteSqlCommand("insert Passwordhistory values ('" + user.US_Login + "','" + cry.Encrypt(user.US_Password, "Intelli") + "','" + user.US_Id + "','" + DateTime.Now + "','" + DateTime.Now + "'");
                    db.Database.ExecuteSqlCommand("insert into Passwordhistory values ('" + cry.Encrypt(user.US_Password, "Intelli") + "'," + user.US_Id + ",'" + DateTime.Now.ToString() + "','" + DateTime.Now.ToString() + "')");

                    //}
                    return RedirectToAction("Index");
                }
                else
                    ViewBag.ErrorMsg = "Login name already exists!";
                //}
                //else
                //{
                //    ViewBag.ErrorMsg = "Login Id is not a valid User id. Please enter a valid User id.";
                //    ViewBag.FK_UG_GroupId = new SelectList(db.UserGroups, "PK_UG_GroupId", "UG_GroupName", user.FK_UG_GroupId);
                //    return View(user);
                //}
            }

            ViewBag.FK_UG_GroupId = new SelectList(db.UserGroups, "PK_UG_GroupId", "UG_GroupName", user.FK_UG_GroupId);
            return View(user);
        }

        //public ActionResult CreatePassword([Bind(Include = "PH_Us_Id,PH_US_Login,PH_US_Password,PH_US_Created,PH_US_Modified")] Passwordhistory Passwordhistory)
        //{
        //    Util.Crypto cry = new Util.Crypto();
        //    Passwordhistory.PH_US_Password = cry.Encrypt(Passwordhistory.PH_US_Password, "Intelli");
        //    if (ModelState.IsValid)
        //    {
        //        //if (db.Smx_CardHolder.Where(x => x.Ch_EmpId == user.US_Login).ToList().Count > 0)
        //        //{                   
        //            db.Passwordhistories.Add(Passwordhistory);
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //}

        // GET: /User/Edit/5

        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_UG_GroupId = new SelectList(db.UserGroups, "PK_UG_GroupId", "UG_GroupName", user.FK_UG_GroupId);
            return View(user);
        }

        // POST: /User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "US_Id,US_User,US_Login,US_Password,FK_UG_GroupId,US_Created,US_Modified")] User user)
        {
            if (ModelState.IsValid)
            {
                Util.Crypto cry = new Util.Crypto();
                //  user.US_Password = cry.Encrypt(user.US_Password, "Intelli");

                //if (db.Smx_CardHolder.Where(x => x.Ch_EmpId == user.US_Login).ToList().Count > 0)
                //{
                db.Entry(user).State = EntityState.Modified;
                Util.CommonClass CC = new Util.CommonClass();



                if (CC.GetDataTable("select [PH_US_Password]  from Passwordhistory where [PH_US_Password] in (select top 3 [PH_US_Password]  from Passwordhistory where [FK_US_ID]=" + user.US_Id + " order by  PH_US_Created desc) and [PH_US_Password]='" + cry.Encrypt(user.US_Password, "Intelli") + "' and [FK_US_ID] = " + user.US_Id).Rows.Count == 0)
                {
                    db.SaveChanges();
                    db.Database.ExecuteSqlCommand("update users set us_password='" + cry.Encrypt(user.US_Password, "Intelli") + "' where Us_id=" + user.US_Id);
                    db.Database.ExecuteSqlCommand("insert into Passwordhistory values ('" + cry.Encrypt(user.US_Password, "Intelli") + "'," + user.US_Id + ",'" + DateTime.Now.ToString() + "','" + DateTime.Now.ToString() + "')");

                    return RedirectToAction("Index");
                }

                else
                    ViewBag.ErrorMsg = "Password should not be last 3 password history";
                //}
                //else
                //{
                //    ViewBag.FK_UG_GroupId = new SelectList(db.UserGroups, "PK_UG_GroupId", "UG_GroupName", user.FK_UG_GroupId);
                //    return View(user);
                //}

            }
            ViewBag.FK_UG_GroupId = new SelectList(db.UserGroups, "PK_UG_GroupId", "UG_GroupName", user.FK_UG_GroupId);
            return View(user);
        }

        // GET: /User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            Util.CommonClass CC = new Util.CommonClass();
            CC.ExecuteSQL("delete from Passwordhistory where FK_US_ID =" + id);
            db.Users.Remove(user);
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