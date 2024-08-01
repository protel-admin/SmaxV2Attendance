using SmaxV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmaxV2.Controllers
{
    public class LoginController : Controller
    {
        SMAXV2Entities db = new SMAXV2Entities();
        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Index(string user, string pwd)
        {
            Session["ErrMsg"] = "";            
            List<User> usr = db.Users.Where(x => x.US_Login == user && x.US_Password == pwd).ToList();
            if (usr.Count > 0)
            {                
                Session["User"] = user;
                Session["UserGroup"] = usr[0].FK_UG_GroupId;
                return RedirectToAction("Index", "Home");
            }
            ViewData["ErrMsg"] = "UserId or Password  Incorrect";
            return RedirectToAction("Index","Login");
        }
    }
}