using SmaxV2Attendance.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmaxV2Attendance.Controllers
{
    [CustAuthFilter]
    public class AssignPermissionController : Controller
    {
        //
        // GET: /AssignPermission/
        private SMAXV2Entities db = new SMAXV2Entities();

        public ActionResult Index()
        {
            ViewBag.DP_ID = new SelectList(db.Smx_Department, "DP_ID", "DP_NAME");
            return View();
        }

        public JsonResult GetShiftTime(string date, string empid)
        {
            Util.CommonClass CC = new Util.CommonClass();
            object jsonstr = "";
            DateTime src = Convert.ToDateTime(date);
            string dd = "Sft_D" + Convert.ToInt16(date.Split('/')[1]).ToString();
            string sql = "Select " + dd + " from Smx_ShiftAssignment where Sft_ChId = '" + empid.Replace("''","") + "' and Sft_MonthYear = '" + src.Month.ToString() + "_" + src.Year.ToString() + "' and " + dd + " is not null";
            DataTable dt = CC.GetDataTable(sql);
            DataTable dt1 = new DataTable();
            if(dt.Rows.Count > 0)
            {
                dt1 = CC.GetDataTable("select * from [Smx_ShiftDetails] where [Sftd_Id] = '" + dt.Rows[0][dd] + "'");
                if(dt1.Rows.Count > 0)
                {
                    jsonstr = new { Err = 0,ShiftName = dt1.Rows[0]["Sftd_Name"], StartTime = dt1.Rows[0]["Sftd_StartTime"], EndTime = dt1.Rows[0]["Sftd_EndTime"] };
                }
                else
                {
                    jsonstr = new {Err = 1, ShiftName = "", StartTime = "", EndTime = ""};
                }
            }
            else
            {
                jsonstr = new {Err = 1, ShiftName = "", StartTime = "", EndTime = ""};
            }

            return Json(jsonstr, JsonRequestBehavior.AllowGet);
        }

        public partial class Shift
        {
            public decimal Sftd_Id { get; set; }
            public string Sftd_Name { get; set; }
            public string Sftd_StartTime { get; set; }
            public string Sftd_EndTime { get; set; }
        }

        public string SavePermission(int dept_id, string trgdate, string starttime, string endtime, string permission, string desc, string employeeid)
        {
            Util.CommonClass CC = new Util.CommonClass();
            string status = "";
            try
            {
                string[] employeearr = employeeid.Split(',');
                for (int i = 0; i <= employeearr.Length - 1; i++)
                {
                    //string sql = "Insert into [Smx_ShiftAssignmentDetails] (FK_Sft_Id,FK_Emp_Id,Sftd_DateTime) values ('" + shiftid.ToString() + "','" + employeearr[i].Replace("''","") + "','" + srcdate + "') ";
                    
                    DateTime trg = Convert.ToDateTime(trgdate);
                    DataTable dt = CC.GetDataTable("select * from Smx_Permission where PR_ChId = '" + employeearr[i].Replace("''", "") + "' and PR_Date = '" + trgdate + "' and PR_permission = '" + permission + "'");
                    if(dt.Rows.Count == 0)
                    {
                        var sql = "Insert into [Smx_Permission] ( ";
                        sql += "[PR_ChId],";
                        sql += "[PR_Date],";
                        sql += "[PR_StartTime],";
                        sql += "[PR_EndTime],";
                        sql += "[FK_DP_ID],";
                        sql += "[PR_Permission],";
                        sql += "[PR_Description],";
                        sql += "[PR_UpdatedBy]";

                        sql += ") values ( ";
                        sql += "'" + employeearr[i].Replace("''", "") + "', ";
                        sql += "'" + trgdate + "', ";
                        sql += "'" + trgdate + " " + starttime + "', ";
                        sql += "'" + trgdate + " " + endtime + "', ";
                        sql += "'" + dept_id + "', ";
                        sql += "'" + permission + "', ";
                        sql += "'" + desc + "', ";
                        sql += "'Admin' ";
                        sql += ")";
                        int cnt = CC.ExecuteSQL(sql);
                        status = "OK";
                    }
                    else
                    {
                        var sql = "Update [Smx_Permission]  ";
                        sql += "set [PR_ChId] = '" + employeearr[i].Replace("''", "") + "', ";
                        sql += "[PR_Date] = '" + trgdate + "', ";
                        sql += "[PR_StartTime] = '" + starttime + "', ";
                        sql += "[PR_EndTime] = '" +  endtime + "', ";
                        sql += "[FK_DP_ID] = '" + dept_id + "', ";
                        sql += "[PR_Permission] = '" + permission + "', ";
                        sql += "[PR_Description] = '" + desc + "', ";
                        sql += "[PR_UpdatedBy] = 'Admin', ";
                        sql += "[PR_ModifiedDate] = '" + DateTime.Now.ToShortDateString() + "' ";
                        sql += "where PR_ChId = '" + employeearr[i].Replace("''", "") + "' and PR_Date = '" + trgdate + "' and PR_permission = '" + permission + "'";
                        int cnt = CC.ExecuteSQL(sql);
                        status = "OK";

                    }
                    
                }
            }
            catch (Exception ex)
            {
                status = "ERR";
            }

            return status;

        }
	}
}