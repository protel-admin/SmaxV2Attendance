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
    public class AssignWeeklyOffController : Controller
    {
        //
        // GET: /AssignWeeklyOff/
        private SMAXV2Entities db = new SMAXV2Entities();
        public ActionResult Index()
        {
            
            ViewBag.DP_ID = new SelectList(db.Smx_Department, "DP_ID", "DP_NAME");
            return View();
        }


        public string SaveWeeklyOff(string srcdate, string trgdate, string employeeid, string days)
        {
            Util.CommonClass CC = new Util.CommonClass();
            string status = "";
            try
            {
                string[] employeearr = employeeid.Split(',');
                for (int i = 0; i <= employeearr.Length - 1; i++)
                {
                    //string sql = "Insert into [Smx_WeeklyoffDetails] (FK_WK_Id,FK_Emp_Id,Sftd_DateTime) values ('" + shiftid.ToString() + "','" + employeearr[i].Replace("''","") + "','" + srcdate + "') ";

                    DateTime src = Convert.ToDateTime(srcdate);
                    DateTime trg = Convert.ToDateTime(trgdate);
                    string[] daysarr = days.Split(',');
                    if (src.Month == trg.Month)
                    {
                        DataTable dt = CC.GetDataTable(" select * from [Smx_Weeklyoff] Where [WK_Chid] ='" + employeearr[i].Replace("''", "") + "' and [WK_Monthyear]='" + src.Month.ToString() + "_" + src.Year.ToString() + "'");
                        if (dt.Rows.Count == 0)
                        {
                            string sql = "Insert into [Smx_Weeklyoff] (WK_ChId,WK_MonthYear,";
                            int totaldays = trg.Subtract(src).Days;
                            DateTime tmpdate = src;
                            for (int j = 0; j <= totaldays; j++)
                            {
                                sql += "WK_D" + tmpdate.Day.ToString() + ",";
                                tmpdate = tmpdate.AddDays(1);
                            }
                            sql += "WK_Modifiedby,WK_Created,WK_Modified";
                            sql += ") values ( '" + employeearr[i].Replace("''", "") + "','" + src.Month.ToString() + "_" + src.Year.ToString() + "',";
                            tmpdate = src;
                            for (int j = 0; j <= totaldays; j++)
                            {
                                int day = Convert.ToInt16(tmpdate.DayOfWeek);
                                if(Array.Exists(daysarr,x=>x== day.ToString()))
                                {
                                    sql += "'1',";
                                }
                                else
                                {
                                    sql += "'0',";
                                }
                                tmpdate = tmpdate.AddDays(1);
                            }
                            sql += "'Admin','" + DateTime.Now.ToShortDateString() + "','" + DateTime.Now.ToShortDateString() + "'";
                            sql += ")";
                            CC.ExecuteSQL(sql);
                            status = "OK";
                        }
                        else
                        {
                            string sql = "Update Smx_Weeklyoff Set ";
                            int totaldays = trg.Subtract(src).Days;
                            DateTime tmpdate = src;
                            for (int j = 0; j <= totaldays; j++)
                            {
                                int day = Convert.ToInt16(tmpdate.DayOfWeek);
                                if (Array.Exists(daysarr, x => x == day.ToString()))
                                {
                                    sql += "WK_D" + tmpdate.Day.ToString() + "='1',";
                                }
                                else
                                {
                                    sql += "WK_D" + tmpdate.Day.ToString() + "='0',";
                                }
                                tmpdate = tmpdate.AddDays(1);
                            }
                            sql += "WK_Modified='" + DateTime.Now.ToShortDateString() + "'";
                            sql += " Where WK_Chid='" + employeearr[i].Replace("''", "") + "' and [WK_Monthyear]= '" + src.Month.ToString() + "_" + src.Year.ToString() + "'";
                            CC.ExecuteSQL(sql);
                            status = "OK";
                        }
                    }
                    else
                    {
                        //int month1days = src.Subtract(Convert.ToDateTime(trg.Month.ToString() + "/" + "01" + "/" + trg.Year.ToString())).Days;                                                    
                        int month1days = (Convert.ToDateTime(trg.Month.ToString() + "/" + "01" + "/" + trg.Year.ToString())).Subtract(src).Days;
                        DataTable dt = CC.GetDataTable(" select * from Smx_Weeklyoff Where WK_Chid='" + employeearr[i].Replace("''", "") + "' and WK_Monthyear='" + src.Month.ToString() + "_" + src.Year.ToString() + "'");
                        if (dt.Rows.Count == 0)
                        {
                            string sql = "Insert into [Smx_Weeklyoff] (WK_ChId,WK_MonthYear,";
                            DateTime tmpdate = src;
                            for (int j = 1; j <= month1days; j++)
                            {
                                sql += "WK_D" + tmpdate.Day.ToString() + ",";
                                tmpdate = tmpdate.AddDays(1);
                            }
                            sql += "WK_Modifiedby,WK_Created,WK_Modified";
                            sql += ") values ( '" + employeearr[i].Replace("''", "") + "','" + src.Month.ToString() + "_" + src.Year.ToString() + "',";
                            tmpdate = src;
                            for (int j = 1; j <= month1days; j++)
                            {
                                int day = Convert.ToInt16(tmpdate.DayOfWeek);
                                if (Array.Exists(daysarr, x => x == day.ToString()))
                                {
                                    sql += "'1',";
                                }
                                else
                                {
                                    sql += "'0',";
                                }
                                tmpdate = tmpdate.AddDays(1);
                            }
                            sql += "'Admin','" + DateTime.Now.ToShortDateString() + "','" + DateTime.Now.ToShortDateString() + "'";
                            sql += ")";
                            CC.ExecuteSQL(sql);
                            status = "OK";
                        }
                        else
                        {
                            string sql = "Update Smx_Weeklyoff Set ";
                            DateTime tmpdate = src;
                            for (int j = 1; j <= month1days; j++)
                            {
                                int day = Convert.ToInt16(tmpdate.DayOfWeek);
                                if (Array.Exists(daysarr, x => x == day.ToString()))
                                {
                                    sql += "WK_D" + tmpdate.Day.ToString() + " = '1',";
                                }
                                else
                                {
                                    sql += "WK_D" + tmpdate.Day.ToString() + " = '0',";
                                }
                                
                                tmpdate = tmpdate.AddDays(1);
                            }
                            sql += "WK_Modified='" + DateTime.Now.ToShortDateString() + "'";
                            sql += " Where WK_ChId='" + employeearr[i].Replace("''", "") + "' and WK_MonthYear= '" + src.Month.ToString() + "_" + src.Year.ToString() + "'";
                            CC.ExecuteSQL(sql);
                            status = "OK";
                        }

                        int month2days = trg.Subtract(Convert.ToDateTime(trg.Month.ToString() + "/" + "01" + "/" + trg.Year.ToString())).Days;
                        dt = CC.GetDataTable(" select * from Smx_Weeklyoff Where WK_ChId='" + employeearr[i].Replace("''", "") + "' and WK_MonthYear='" + trg.Month.ToString() + "_" + trg.Year.ToString() + "'");
                        if (dt.Rows.Count == 0)
                        {
                            string sql = "";
                            sql = "Insert into [Smx_Weeklyoff] (WK_ChId,WK_MonthYear,";
                            DateTime tmpdate = Convert.ToDateTime(trg.Month.ToString() + "/" + "01" + "/" + trg.Year.ToString());
                            for (int j = 0; j <= month2days; j++)
                            {
                                sql += "WK_D" + tmpdate.Day.ToString() + ",";
                                tmpdate = tmpdate.AddDays(1);
                            }
                            sql += "WK_Modifiedby,WK_Created,WK_Modified";
                            sql += ") values ( '" + employeearr[i].Replace("''", "") + "','" + trg.Month.ToString() + "_" + trg.Year.ToString() + "',";
                            tmpdate = Convert.ToDateTime(trg.Month.ToString() + "/" + "01" + "/" + trg.Year.ToString());
                            for (int j = 0; j <= month2days; j++)
                            {
                                int day = Convert.ToInt16(tmpdate.DayOfWeek);
                                if (Array.Exists(daysarr, x => x == day.ToString()))
                                {
                                    sql += "'1',";
                                }
                                else
                                {
                                    sql += "'0',";
                                }
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
                            sql = "Update Smx_Weeklyoff Set ";
                            DateTime tmpdate = Convert.ToDateTime(trg.Month.ToString() + "/" + "01" + "/" + trg.Year.ToString());
                            for (int j = 0; j <= month2days; j++)
                            {
                                int day = Convert.ToInt16(tmpdate.DayOfWeek);
                                if (Array.Exists(daysarr, x => x == day.ToString()))
                                {
                                    sql += "WK_D" + tmpdate.Day.ToString() + "='1',";
                                }
                                else
                                {
                                    sql += "WK_D" + tmpdate.Day.ToString() + "='0',";
                                }
                                tmpdate = tmpdate.AddDays(1);
                            }
                            sql += "WK_Modified='" + DateTime.Now.ToShortDateString() + "'";
                            sql += " Where WK_ChId='" + employeearr[i].Replace("''", "") + "' and WK_MonthYear= '" + trg.Month.ToString() + "_" + trg.Year.ToString() + "'";
                            CC.ExecuteSQL(sql);
                            status = "OK";
                        }
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