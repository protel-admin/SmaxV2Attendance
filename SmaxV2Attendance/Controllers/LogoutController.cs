using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmaxV2Attendance.Controllers
{
    
    public class LogoutController : Controller
    {
        //
        // GET: /Logout/
        public ActionResult Index()
        {
            Session["User"] = "";
            Session["UserGroup"] = "";
            Session["ErrMsg"] = "";
            return RedirectToAction("Index", "Login");
        }
	}
}