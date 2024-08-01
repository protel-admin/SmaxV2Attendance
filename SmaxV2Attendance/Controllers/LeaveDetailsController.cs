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
    public class LeaveDetailsController : Controller
    {
        private SMAXV2Entities db = new SMAXV2Entities();
        //
        // GET: /LeaveDetails/
        public ActionResult Index()
        {
            ViewBag.DP_ID = new SelectList(db.Smx_Department, "DP_ID", "DP_NAME");
            ViewBag.LeaveType = new SelectList(db.Smx_Leave, "Lv_Id", "Lv_Description");
            return View();
        }

        public string SaveLeaveDetails(string srcdate, string trgdate, string employeeid, string leavetype, string duration, string dept, string operation)
        {
            Util.CommonClass CC = new Util.CommonClass();
            string status = "";
            try
            {
                string[] employeearr = employeeid.Split(',');
                for (int i = 0; i <= employeearr.Length - 1; i++)
                {
                    //string sql = "Insert into [Smx_ShiftAssignmentDetails] (FK_Sft_Id,FK_Emp_Id,Sftd_DateTime) values ('" + shiftid.ToString() + "','" + employeearr[i].Replace("''","") + "','" + srcdate + "') ";

                    DateTime src = Convert.ToDateTime(srcdate);
                    DateTime trg = Convert.ToDateTime(trgdate);
                    int totaldays = trg.Subtract(src).Days;
                    DateTime tmpdate = src;
                    string sql = "";

                    if (operation.ToUpper() == "INSERT" || operation.ToUpper() == "UPDATE")
                    {
                        for (int j = 0; j <= totaldays; j++)
                        {
                            DataTable dt = CC.GetDataTable(" select * from Smx_LeaveDetails Where LTrim(RTrim(LD_ChId)) ='" + employeearr[i].Replace("''", "").Trim() + "' and LD_DateLDetails='" + tmpdate.ToShortDateString() + "'");
                            if (dt.Rows.Count == 0)
                            {
                                sql = "Insert into [Smx_LeaveDetails] ( ";
                                sql += "FK_Lv_Id, ";
                                sql += "LD_DateLDetails, "; 
                                sql += "LD_Duration, ";
                                sql += "LD_ChId, ";
                                sql += "LD_Unit, ";
                                sql += "LD_Created, ";
                                sql += "LD_Modified, ";
                                sql += "LD_Modifiedby ";
                                sql += " ) values ( ";
                                sql += "'" + leavetype + "', ";
                                sql += "'" + tmpdate.ToShortDateString() + "', ";
                                sql += "'" + duration + "', ";
                                sql += "'" + employeearr[i].Replace("''", "").Trim() + "', ";
                                sql += "'" + dept + "', ";
                                sql += "'" + DateTime.Now.ToShortDateString() + "', ";
                                sql += "'" + DateTime.Now.ToShortDateString() + "', ";
                                sql += "'Admin' ";
                                sql += " )";
                            }
                            else
                            {
                                sql = "Update Smx_LeaveDetails set ";
                                sql += "FK_Lv_Id = '" + leavetype + "', ";
                                sql += "LD_DateLDetails = '" + tmpdate.ToShortDateString() + "', ";
                                sql += "LD_Duration = '" + duration + "', ";
                                sql += "LD_ChId = '" + employeearr[i].Replace("''", "").Trim() + "', ";
                                sql += "LD_Unit = '" + dept + "', ";
                                sql += "LD_Modified  ='" + DateTime.Now.ToShortDateString() + "', ";
                                sql += "LD_Modifiedby = 'Admin' Where LTrim(RTrim(LD_ChId)) ='" + employeearr[i].Replace("''", "").Trim() + "' and LD_DateLDetails='" + tmpdate.ToShortDateString() + "'";
                            }
                            CC.ExecuteSQL(sql);
                            status = "OK";
                            tmpdate = tmpdate.AddDays(1);
                        }
                    }
                    else
                    {
                        sql = "Delete from Smx_LeaveDetails Where LTrim(RTrim(LD_ChId)) ='" + employeearr[i].Replace("''", "").Trim() + "' and LD_DateLDetails >='" + src.ToShortDateString() + "' and LD_DateLDetails <= '" + trg.ToShortDateString() +"'";
                        CC.ExecuteSQL(sql);
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
        
        public ActionResult ViewLeave()
        {
            ViewBag.DP_ID = new SelectList(db.Smx_Department, "DP_ID", "DP_NAME");
            ViewBag.LeaveType = new SelectList(db.Smx_Leave, "Lv_Id", "Lv_Description");
            return View();
        }

        public string ViewLeaveData(string srcdate, string trgdate,string employeeid)
        {
            Util.CommonClass CC = new Util.CommonClass();
            DataTable dt = CC.GetDataTable("select * from vw_LeaveDetails where EmpId in  (" +  employeeid.Replace("''","'") + ") and Leave_Date>= '" + srcdate + "' and Leave_Date <= '" + trgdate + "'");
            string str = "";
            string val = "";
            str += "<table class='tbl'>";
            str += "<thead>";
            str += "<tr >";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                str += "<th >" + dt.Columns[i].ColumnName + "</th>";
            }
            str += "</tr>";
            str += "</thead>"; 
            str += "<tbody>";
            decimal TotalHrs = 0;
            int hrs = 0;
            decimal mins = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TotalHrs = 0;
                str += "<tr>";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                   
                    str += "<td >" + dt.Rows[i][j].ToString() + "</td>";

                }
                str += "</tr>";
            }
            str += "</tbody>";
            str += "</table>";
            return str;
        }
        
	}
}