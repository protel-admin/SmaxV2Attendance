using SmaxV2Attendance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmaxV2Attendance.Controllers
{
    //[CustAuthFilter]
    public class LoginController : Controller
    {
        SMAXV2Entities db = new SMAXV2Entities();
        //
        // GET: /Login/
        public ActionResult Index()
        {
            //Util.Crypto cry = new Util.Crypto();
            //string encry = cry.Encrypt("electron", "EL");
            //string decry = cry.Decrypt(encry, "EL");
            return View();
        }

        [HttpPost]

        public ActionResult Index(string user, string pwd)
        {
            SMAXV2Entities db = new SMAXV2Entities();
            Util.Crypto cry = new Util.Crypto();
            pwd = cry.Encrypt(pwd, pwd);

            Session["ErrMsg"] = "";
            List<User> usr = db.Users.Where(x => x.US_Login == user && x.US_Password == pwd).ToList();
            if (usr.Count > 0)
            {

              //  0+vrro29Ngq9/ij+9h4UQA==Intelli@73
                Session["User"] = user;
                Session["UserGroup"] = usr[0].FK_UG_GroupId;
                return RedirectToAction("Index", "Home");
            }
            ViewData["ErrMsg"] = "UserId or Password  Incorrect";
            return RedirectToAction("Index","Login");
        }
    }
}