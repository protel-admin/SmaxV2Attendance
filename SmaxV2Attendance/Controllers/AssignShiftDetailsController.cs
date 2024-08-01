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
    public class AssignShiftDetailsController : Controller
    {
        private SMAXV2Entities db = new SMAXV2Entities();
        //
        // GET: /AssignShiftDetails/
        public ActionResult Index()
        {
            ViewBag.DP_ID = new SelectList(db.Smx_Department, "DP_ID", "DP_NAME");
            ViewBag.Sftd_Name = new SelectList(db.Smx_ShiftDetails, "Sftd_Id", "Sftd_Name");
            return View();
        }

        public JsonResult GetShiftTime(decimal shiftid)
        {
            Shift shift = db.Smx_ShiftDetails.Where(x => x.Sftd_Id == shiftid).Select(e => new Shift()
            {
                Sftd_Id = e.Sftd_Id,
                Sftd_Name = e.Sftd_Name,
                Sftd_StartTime = e.Sftd_StartTime,
                Sftd_EndTime = e.Sftd_EndTime 
            }).First();
            return Json(shift, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEmp(int deptid)
        {
            List<Employee> emp = db.Smx_CardHolder.Where(s => s.Ch_Dp_Id == deptid).Select(e => new Employee()
                                       {
                                           Ch_EmpId = e.Ch_EmpId,
                                           Ch_FName = e.Ch_FName
                                       }).ToList();
            return Json(emp, JsonRequestBehavior.AllowGet);
        }

        public partial class Shift
        {
            public decimal Sftd_Id { get; set; }
            public string Sftd_Name { get; set; }
            public string Sftd_StartTime { get; set; }
            public string Sftd_EndTime { get; set; }
        }
        public partial class Employee
        {
            public string Ch_EmpId { get; set; }
            public int Ch_Dp_Id { get; set; }
            public string Ch_FName { get; set; }
        }

        public string SaveShiftAssignment(string srcdate, string trgdate,string employeeid,int shiftid)
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
                    
                    if (src.Month == trg.Month)
                    {
                        DataTable dt = CC.GetDataTable(" select * from Smx_ShiftAssignment Where Sft_ChId='" + employeearr[i].Replace("''", "") + "' and Sft_MonthYear='" + src.Month.ToString() + "_" + src.Year.ToString() + "'");
                        if (dt.Rows.Count == 0)
                        {
                            string sql = "Insert into [Smx_ShiftAssignment] (Sft_ChId,Sft_MonthYear,";
                            int totaldays = trg.Subtract(src).Days;
                            DateTime tmpdate = src;
                            for (int j = 1; j <= totaldays; j++)
                            {
                                sql += "Sft_D" + tmpdate.Day.ToString() + ",";
                                tmpdate = tmpdate.AddDays(1);
                            }
                            sql += "Sft_Modifiedby,Sft_Created,Sft_Modified";
                            sql += ") values ( '" + employeearr[i].Replace("''", "") + "','" + src.Month.ToString() + "_" + src.Year.ToString() + "',";
                            for (int j = 1; j <= totaldays; j++)
                            {
                                sql += "'" + shiftid.ToString() + "',";
                                tmpdate = tmpdate.AddDays(1);
                            }
                            sql += "'Admin','" + DateTime.Now.ToShortDateString() + "','" + DateTime.Now.ToShortDateString() + "'";
                            sql += ")";
                            CC.ExecuteSQL(sql);
                            status = "OK";
                        }
                        else
                        {
                            string sql = "Update Smx_ShiftAssignment Set ";
                            int totaldays = trg.Subtract(src).Days + 1;
                            DateTime tmpdate = src;
                            for (int j = 1; j <= totaldays; j++)
                            {
                                sql += "Sft_D" + tmpdate.Day.ToString() + "='" + shiftid.ToString() + "',";                                
                                tmpdate = tmpdate.AddDays(1);
                            }
                            sql += "Sft_Modified='" + DateTime.Now.ToShortDateString() + "'";
                            sql += " Where Sft_ChId='" + employeearr[i].Replace("''", "") + "' and Sft_MonthYear= '" + src.Month.ToString() + "_" + src.Year.ToString() + "'";
                            CC.ExecuteSQL(sql);
                            status = "OK";
                        }                        
                    }
                    else
                    {
                        //int month1days = src.Subtract(Convert.ToDateTime(trg.Month.ToString() + "/" + "01" + "/" + trg.Year.ToString())).Days;                                                    
                        int month1days = (Convert.ToDateTime(trg.Month.ToString() + "/" + "01" + "/" + trg.Year.ToString())).Subtract(src).Days;
                        DataTable dt = CC.GetDataTable(" select * from Smx_ShiftAssignment Where Sft_ChId='" + employeearr[i].Replace("''", "") + "' and Sft_MonthYear='" + src.Month.ToString() + "_" + src.Year.ToString() + "'");
                        if (dt.Rows.Count == 0)
                        {                            
                            string sql = "Insert into [Smx_ShiftAssignment] (Sft_ChId,Sft_MonthYear,";
                            DateTime tmpdate = src;
                            for (int j = 1; j <= month1days; j++)
                            {
                                sql += "Sft_D" + tmpdate.Day.ToString() + ",";
                                tmpdate = tmpdate.AddDays(1);
                            }
                            sql += "Sft_Modifiedby,Sft_Created,Sft_Modified";
                            sql += ") values ( '" + employeearr[i].Replace("''", "") + "','" + src.Month.ToString() + "_" + src.Year.ToString() + "',";
                            for (int j = 1; j <= month1days; j++)
                            {
                                sql += "'" + shiftid.ToString() + "',";
                                tmpdate = tmpdate.AddDays(1);
                            }
                            sql += "'Admin','" + DateTime.Now.ToShortDateString() + "','" + DateTime.Now.ToShortDateString() + "'";
                            sql += ")";
                            CC.ExecuteSQL(sql);
                        }
                        else
                        {                           
                            string sql = "Update Smx_ShiftAssignment Set ";
                            DateTime tmpdate = src;
                            for (int j = 1; j <= month1days; j++)
                            {
                                sql += "Sft_D" + tmpdate.Day.ToString() + "='" + shiftid.ToString() + "',";                                
                                tmpdate = tmpdate.AddDays(1);
                            }
                            sql += "Sft_Modified='" + DateTime.Now.ToShortDateString() + "'";
                            sql += " Where Sft_ChId='" + employeearr[i].Replace("''", "") + "' and Sft_MonthYear= '" + src.Month.ToString() + "_" + src.Year.ToString() + "'";
                            CC.ExecuteSQL(sql);
                        }
                        
                        int month2days = trg.Subtract(Convert.ToDateTime(trg.Month.ToString() + "/" + "01" + "/" + trg.Year.ToString())).Days;
                        dt = CC.GetDataTable(" select * from Smx_ShiftAssignment Where Sft_ChId='" + employeearr[i].Replace("''", "") + "' and Sft_MonthYear='" + trg.Month.ToString() + "_" + trg.Year.ToString() + "'");
                        if (dt.Rows.Count == 0)
                        {
                            string sql = "";
                            sql = "Insert into [Smx_ShiftAssignment] (Sft_ChId,Sft_MonthYear,";
                            DateTime tmpdate = Convert.ToDateTime(trg.Month.ToString() + "/" + "01" + "/" + trg.Year.ToString());
                            for (int j = 0; j <= month2days; j++)
                            {
                                sql += "Sft_D" + tmpdate.Day.ToString() + ",";
                                tmpdate = tmpdate.AddDays(1);
                            }
                            sql += "Sft_Modifiedby,Sft_Created,Sft_Modified";
                            sql += ") values ( '" + employeearr[i].Replace("''", "") + "','" + trg.Month.ToString() + "_" + trg.Year.ToString() + "',";
                            for (int j = 1; j <= month2days; j++)
                            {
                                sql += "'" + shiftid.ToString() + "',";
                                tmpdate = tmpdate.AddDays(1);
                            }
                            sql += "'Admin','" + DateTime.Now.ToShortDateString() + "','" + DateTime.Now.ToShortDateString() + "'";
                            sql += ")";
                            CC.ExecuteSQL(sql);
                            status = "OK";
                        }
                        else
                        {                            
                            string sql = "";
                            sql = "Update Smx_ShiftAssignment Set ";
                            DateTime tmpdate = Convert.ToDateTime(trg.Month.ToString() + "/" + "01" + "/" + trg.Year.ToString());
                            for (int j = 0; j <= month2days; j++)
                            {
                                sql += "Sft_D" + tmpdate.Day.ToString() + "='" + shiftid.ToString() + "',";                                
                                tmpdate = tmpdate.AddDays(1);
                            }
                            sql += "Sft_Modified='" + DateTime.Now.ToShortDateString() + "'";
                            sql += " Where Sft_ChId='" + employeearr[i].Replace("''", "") + "' and Sft_MonthYear= '" + trg.Month.ToString() + "_" + trg.Year.ToString() + "'";
                            CC.ExecuteSQL(sql);
                            status = "OK";
                        }
                    }                    
                }
            }
            catch(Exception ex)
            {
                status = "ERR";
            }
            
                return status;

        }
	}
}