using SmaxV2Attendance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmaxV2Attendance.Controllers
{
    public class CustAuthFilter : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //filterContext.Controller.ViewBag.AutherizationMessage = "";
            if (System.Web.HttpContext.Current.Session["User"] != "" && System.Web.HttpContext.Current.Session["User"] != null)
            {
                var rd = System.Web.HttpContext.Current.Request.RequestContext.RouteData;
                string currentAction = rd.GetRequiredString("action");
                string currentController = rd.GetRequiredString("controller");
                SMAXV2Entities db = new SMAXV2Entities();
                int usergroup = Convert.ToInt16(System.Web.HttpContext.Current.Session["UserGroup"]);
                string controller = currentController;
                List<Entitlement> ent = db.Entitlements.Where(x => x.FK_UG_GroupId == usergroup && x.FK_SC_ScreenName == controller).ToList();
                if (ent.Count == 0)
                {
                    //System.Web.HttpContext.Current.Session["User"] = null;
                    //System.Web.HttpContext.Current.Session["UserGroup"] = null;
                    System.Web.HttpContext.Current.Session["ErrMsg"] = "You are not an authorized User!\n Please contact admin for access!";
                    //System.Web.HttpContext.Current.Response.Redirect("/Home/Index");
                    System.Web.HttpContext.Current.Response.Redirect(System.Web.HttpContext.Current.Request.UrlReferrer.ToString());
                    //ViewData["ErrMsg"] = "You are not an authorized User!\n Please contact admin for access!";
                    //status = false;
                }
            }
            else
            {
                System.Web.HttpContext.Current.Session["ErrMsg"] = "You are not an Logged In (or) Session Expired!";
                System.Web.HttpContext.Current.Response.Redirect("/Login/Index?MsgId=1");
            }
            
        }
    }
}
