using SmaxV2Attendance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using ClosedXML.Excel;
using System.Text;

namespace SmaxV2Attendance.Controllers
{
    [CustAuthFilter]
    public class ReportsController : Controller
    {
        private SMAXV2Entities db = new SMAXV2Entities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TransactionReport()
        {
            return View();
        }

        public ActionResult AttendenceForMonthStatus(string month, string year)
        {

            return View();
        }        
        
        public ActionResult MonthlyAttendanceStatusExport(string month, string year)
        {
            string str = "";
            string val = "";

            Util.CommonClass CC = new Util.CommonClass();
            DataTable dt = new DataTable();
            dt = CC.GetDataTable("exec sp_GetAttendenceForMonthStatus '" + month + "', '" + year + "'");

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    str += dt.Columns[i].ColumnName + ",";
                }

                str += "\r\n";
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        val = dt.Rows[i][j].ToString();
                        if (val.Trim() != "")
                        {
                            //val = val.Replace(" ", "&nbsp;");
                            val = val.Replace("<br/>", " ");
                            str += val + ",";
                        }
                        else
                        {
                            str += "NR,";
                        }

                    }
                    str += "\r\n";
                }

            }


            //string str = GetMonthlyAttendanceDetail();
            ViewData["CsvContent"] = str;
            return View();
        }


        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public  ActionResult GetEmployeeFilter(string SearchText="") {
            List<EmployeeListItemDto> list = new List<EmployeeListItemDto>();     
            try
            {

                string Qry = $@"select Ch_EmpId EmployeeId,Ch_FName EmployeeName
                ,Ch_Csnnumber CSN,isnull(DP_NAME,'') as Department
                ,isnull(DN_NAME,'') as Designation
                ,isnull(UT_NAME,'') as Unit
                from vw_Smx_CardHolderDetails
where ch_empId like '%{SearchText}%' or Ch_Fname like '%{SearchText}%'
                order by Ch_EmpId

                ";


                list = db.Database.SqlQuery<EmployeeListItemDto>(Qry).ToList();


            }

            catch (Exception e) {
            }
            return View(list);

        }


        public JsonResult GetTransaction()
        {
            DateTime dt = new DateTime(2022,5,5);
            List<vw_Smx_Transaction> vw_smx_transaction = db.vw_Smx_Transaction.AsNoTracking().Where(x=>x.Date_Time >= dt).ToList();
            return Json(vw_smx_transaction, JsonRequestBehavior.AllowGet);
        }

        public string GetMonthlyAttendanceStatus(string month, string year)
        {
            Util.CommonClass CC = new Util.CommonClass();
            DataTable dt = new DataTable();
            dt = CC.GetDataTable("exec sp_GetAttendenceForMonthStatus '" + month + "', '" + year + "'");
            string str = "";
            string val = "";
            str += "<table>";
            str += "<thead>";
            str += "<tr bgcolor='#CFCFCF'>";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                str += "<td style='width:100px'><b>&nbsp;&nbsp;" + dt.Columns[i].ColumnName + "&nbsp;&nbsp;</b></td>";
            }
            str += "<td style='width:100px'><b>Total Present</b></td>";
            str += "<td style='width:100px'><b>Total Absent</b></td>";
            str += "</tr>";
            str += "</thead>";
            str += "<tbody>";
            int pr_cnt = 0;
            int ab_cnt = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                pr_cnt = 0;
                ab_cnt = 0;
                str += "<tr>";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    val = dt.Rows[i][j].ToString();
                    
                    if (val.Trim() != "")
                    {
                        if (val == "PR")
                        {
                            str += "<td style='background-color:#56f15e'>" + val + "</td>";
                            pr_cnt = pr_cnt + 1;
                        }
                        else if(val == "AB")
                        {
                            str += "<td style='background-color:#f1565e'>" + val + "</td>";
                            ab_cnt = ab_cnt + 1;
                        }
                        else
                        {
                            str += "<td >" + val + "</td>";                           
                        }
                        //if (val == "AB")
                        //{
                        //    str += "<td style='background-color:#ff0000'>" + val + "</td>";
                        //}
                        
                    }
                    else
                    {
                        str += "<td style='background-color:#f1565e'>AB</td>";
                        ab_cnt = ab_cnt + 1;
                    }

                }
                str += "<td>" + pr_cnt + "</td>";
                str += "<td>" + ab_cnt + "</td>";
                str += "</tr>";
            }
            str += "</tbody>";
            str += "</table>";

            return str;
        }

        public ActionResult GetMonthlyAttendanceStatusExport(string month, string year, string reportname, string reporttype)
        {
            Util.CommonClass CC = new Util.CommonClass();
            DataTable dt = new DataTable();
            dt = CC.GetDataTable("exec sp_GetAttendenceForMonthStatus '" + month + "', '" + year + "'");
            string str = "";
            string val = "";
            str += "<table>";
            str += "<thead>";
            str += "<tr>";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                str += "<th>" + dt.Columns[i].ColumnName + "</th>";
            }
            str += "<th >Total Present</th>";
            str += "<th >Total Absent</th>";
            str += "</tr>";
            str += "</thead>";
            str += "<tbody>";
            int pr_cnt = 0;
            int ab_cnt = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                pr_cnt = 0;
                ab_cnt = 0;
                str += "<tr>";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    val = dt.Rows[i][j].ToString();

                    if (val.Trim() != "")
                    {
                        if (val == "PR")
                        {
                            str += "<td style='background-color:#56f15e'>" + val + "</td>";
                            pr_cnt = pr_cnt + 1;
                        }
                        else if (val == "AB")
                        {
                            str += "<td style='background-color:#f1565e'>" + val + "</td>";
                            ab_cnt = ab_cnt + 1;
                        }
                        else
                        {
                            str += "<td >" + val + "</td>";
                        }
                        //if (val == "AB")
                        //{
                        //    str += "<td style='background-color:#ff0000'>" + val + "</td>";
                        //}

                    }
                    else
                    {
                        str += "<td style='background-color:#f1565e'>AB</td>";
                        ab_cnt = ab_cnt + 1;
                    }

                }
                str += "<td>" + pr_cnt + "</td>";
                str += "<td>" + ab_cnt + "</td>";
                str += "</tr>";
            }
            str += "</tbody>";
            str += "</table>";

            DataTable dtnew = Util.Utility.ConvertHTMLTablesToDataSet(str);
            MemoryStream fs = new MemoryStream();
            if (reporttype.ToUpper() == "EXCEL")
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add("Report");
                    ws.Cell(1, 1).Value = reportname;
                    ws.Cell(1, 1).Style.Font.FontSize = 20;
                    ws.Cell(1, 1).Style.Font.Bold = true;
                    ws.Range(1, 1, 1, 10).Merge().AddToNamed("Titles");


                    ws.Cell(2, 1).Value = "Total Number of Records : " + dtnew.Rows.Count.ToString();
                    ws.Cell(2, 1).Style.Font.FontSize = 16;
                    ws.Cell(2, 1).Style.Font.Bold = true;
                    ws.Range(2, 1, 2, 10).Merge().AddToNamed("NoOfRecords");

                    for (int i = 1; i <= dtnew.Columns.Count; i++)
                    {
                        ws.Cell(4, i).Value = dtnew.Columns[i - 1].Caption;
                        ws.Cell(4, i).Style.Font.Bold = true;
                        ws.Cell(4, i).Style.Fill.SetBackgroundColor(XLColor.Yellow);
                    }
                    var rangeWithData = ws.Cell(5, 1).InsertData(dtnew.AsEnumerable());
                    wb.SaveAs(fs);
                }
            }

            if (reporttype.ToUpper() == "PDF")
            {
                fs = Util.Utility.ExportToPdf(dtnew, reportname + " \r\n " + "Total Number of Records : " + dtnew.Rows.Count.ToString() + "\r\n");
            }

            ViewData["FileName"] = reportname;
            ViewData["FileType"] = reporttype;
            ViewData["FileContent"] = fs;

            return View();
        }

        //---------------------------------------------------------------------------
        //---------------- All new reports starts here ------------------------------
        //---------------------------------------------------------------------------

        //------------ Employee Details Report -----------------------
        public ActionResult GetEmployeedetailsReport()
        {
            return View();
        }

        public string GetEmployeedetailsReportData(string devices, string accesslevels, string EmployeedIds = "")
        {
            Util.CommonClass CC = new Util.CommonClass();
            DataTable dt = new DataTable();
            string Qry = string.Empty;
            Qry = $@"select Ch_Csnnumber Cardno,Ch_EmpId EmployeeId,Ch_FName EmployeeName,isnull(DP_NAME,'') as Department,isnull(DN_NAME,'') as Designation,isnull(UT_NAME,'') as Unit,isnull(CS_NAME,'') as CardStatus from  [vw_Smx_CardHolderDetails]
            where 1=1";

            if (Convert.ToInt32(devices) > 0)
            {
                Qry += $" and CS_Id='{devices }'";
            }

            //if (Convert.ToInt32(accesslevels) > 0)
            //{
            //    Qry += $" and CAL_AL_ID='{accesslevels}'";
            //}


            if (string.IsNullOrEmpty(EmployeedIds) == false)
            {

                Qry += $" and Ch_EmpId in (select colA  from dbo.split('{EmployeedIds}',',')) ";
            }
            Qry = Qry + "  order by Ch_EmpId asc ";

            dt = CC.GetDataTable(Qry);



            //dt = CC.GetDataTable("select * from  [vw_Smx_CardAccesslevel_Employee]");
            string str = "";
            string val = "";
            str += "<table>";
            str += "<thead>";
            str += "<tr bgcolor='#CFCFCF'>";
            //for (int i = 0; i < dt.Columns.Count; i++)
            //{
            //    str += "<td style='width:100px'><b>&nbsp;&nbsp;" + dt.Columns[i].ColumnName + "&nbsp;&nbsp;</b></td>";
            //}
            str += "<td style='width:100px'><b>&nbsp;&nbsp;Card Number&nbsp;&nbsp;</b></td>";
            str += "<td style='width:100px'><b>&nbsp;&nbsp;Employee Id&nbsp;&nbsp;</b></td>";
            str += "<td style='width:100px'><b>&nbsp;&nbsp;Name&nbsp;&nbsp;</b></td>";
            str += "<td style='width:100px'><b>&nbsp;&nbsp;Department&nbsp;&nbsp;</b></td>";
            str += "<td style='width:100px'><b>&nbsp;&nbsp;Designation &nbsp;&nbsp;</b></td>";
            str += "<td style='width:100px'><b>&nbsp;&nbsp;Unit &nbsp;&nbsp;</b></td>";
            str += "<td style='width:100px'><b>&nbsp;&nbsp;CardStatus &nbsp;&nbsp;</b></td>";
            str += "</tr>";
            str += "</thead>";
            str += "<tbody>";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                str += "<tr>";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    val = dt.Rows[i][j].ToString();
                    str += "<td>" + val + "</td>";
                }
                str += "</tr>";
            }
            str += "</tbody>";
            str += "</table>";

            return str;
        }

        public ActionResult GetEmployeedetailsReportDataExport(string reportname, string reporttype, string devices, string accesslevels, string EmployeedIds = "")
        {
            Util.CommonClass CC = new Util.CommonClass();
            DataTable dt = new DataTable();
            //dt = CC.GetDataTable("select Ch_CardNo as [Card No],	Ch_EmpId as [Emp ID],	Ch_FName as [Name],	DE_NAME as [Device],	AL_NAME as [Access Level],	LN_NAME as [Location] from  [vw_Smx_CardAccesslevel_Employee]");
            string Qry = string.Empty;
            Qry = $@"select Ch_Csnnumber Cardno,Ch_EmpId EmployeeId,Ch_FName EmployeeName,isnull(DP_NAME,'') as Department,isnull(DN_NAME,'') as Designation,isnull(UT_NAME,'') as Unit,isnull(CS_NAME,'') as CardStatus from  [vw_Smx_CardHolderDetails]
            where 1=1";

            if (Convert.ToInt32(devices) > 0)
            {
                Qry += $" and Cs_ID='{devices }'";
            }

            //if (Convert.ToInt32(accesslevels) > 0)
            //{
            //    Qry += $" and CAL_AL_ID='{accesslevels}'";
            //}


            if (string.IsNullOrEmpty(EmployeedIds) == false)
            {

                Qry += $" and Ch_EmpId in (select colA  from dbo.split('{EmployeedIds}',',')) ";
            }
            Qry = Qry + "  order by Ch_EmpId asc ";

            dt = CC.GetDataTable(Qry);




            MemoryStream fs = new MemoryStream();
            if (reporttype.ToUpper() == "EXCEL")
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add("Report");
                    ws.Cell(1, 1).Value = reportname;
                    ws.Cell(1, 1).Style.Font.FontSize = 20;
                    ws.Cell(1, 1).Style.Font.Bold = true;
                    ws.Range(1, 1, 1, 6).Merge().AddToNamed("Titles");


                    ws.Cell(2, 1).Value = "Total Number of Records : " + dt.Rows.Count.ToString();
                    ws.Cell(2, 1).Style.Font.FontSize = 16;
                    ws.Cell(2, 1).Style.Font.Bold = true;
                    ws.Range(2, 1, 2, 6).Merge().AddToNamed("NoOfRecords");


                    ws.Cell(3, 1).Value = "Generated Time : " + DateTime.Now;
                    ws.Cell(3, 1).Style.Font.FontSize = 10;
                    ws.Cell(3, 1).Style.Font.Bold = true;
                    ws.Range(3, 1, 3, 6).Merge().AddToNamed("NoOfRecords");

                    ws.Cell(4, 1).Value = "Generated By : " + Session["user"];
                    ws.Cell(4, 1).Style.Font.FontSize = 10;
                    ws.Cell(4, 1).Style.Font.Bold = true;
                    ws.Range(4, 1, 4, 6).Merge().AddToNamed("NoOfRecords");

                    for (int i = 1; i <= dt.Columns.Count; i++)
                    {
                        ws.Cell(6, i).Value = dt.Columns[i - 1].Caption;
                        ws.Cell(6, i).Style.Font.Bold = true;
                        ws.Cell(6, i).Style.Fill.SetBackgroundColor(XLColor.Yellow);
                    }
                    var rangeWithData = ws.Cell(7, 1).InsertData(dt.AsEnumerable());
                    wb.SaveAs(fs);
                }
            }

            if (reporttype.ToUpper() == "PDF")
            {
                fs = Util.Utility.ExportToPdf(dt, reportname + " \r\n " + "Total Number of Records : " + dt.Rows.Count.ToString() + "\r\n");
            }

            ViewData["FileName"] = reportname;
            ViewData["FileType"] = reporttype;
            ViewData["FileContent"] = fs;

            return View();
        }

        //-------------No Flash Employees Report ---------------------

        public ActionResult GetNoFlashEmployees()
        {
            return View();
        }

        public string GetNoFlashEmployeesData(string devices, string accesslevels, string EmployeedIds = "")
        {
            Util.CommonClass CC = new Util.CommonClass();
            DataTable dt = new DataTable();
            string Qry = string.Empty;
            Qry = $@"Select Ch_Csnnumber Cardno,Ch_EmpId EmployeeId,Ch_FName EmployeeName,isnull(DP_NAME,'') as Department,isnull(DN_NAME,'') as Designation,isnull(UT_NAME,'') as Unit,isnull(CS_NAME,'') as CardStatus from  [vw_Smx_CardHolderDetails]
                Where Ch_Empid not in (select Tr_EmpId from Smx_Transaction AS A INNER JOIN dbo.Smx_AttendanceReaders AS B ON A.Tr_IpAddress=B.AR_IpAddress  where Tr_Date>=(SELECT DATEADD(day, -Dayscount, Cast(GETDATE() as date)) from Smx_LastFlashdayscount)) ";
            //Where Ch_Empid not in (select Tr_EmpId from Smx_Transaction  where Tr_Date>=(SELECT DATEADD(day, -Dayscount, Cast(GETDATE() as date)) from Smx_LastFlashdayscount)) ";


            if (string.IsNullOrEmpty(EmployeedIds) == false)
            {

                Qry += $" and Ch_EmpId in (select colA  from dbo.split('{EmployeedIds}',',')) ";
            }
            Qry = Qry + "  Order by Ch_EmpId ";

            dt = CC.GetDataTable(Qry);



            //dt = CC.GetDataTable("select * from  [vw_Smx_CardAccesslevel_Employee]");
            string str = "";
            string val = "";
            str += "<table>";
            str += "<thead>";
            str += "<tr bgcolor='#CFCFCF'>";
            //for (int i = 0; i < dt.Columns.Count; i++)
            //{
            //    str += "<td style='width:100px'><b>&nbsp;&nbsp;" + dt.Columns[i].ColumnName + "&nbsp;&nbsp;</b></td>";
            //}
            str += "<td style='width:100px'><b>&nbsp;&nbsp;Card Number&nbsp;&nbsp;</b></td>";
            str += "<td style='width:100px'><b>&nbsp;&nbsp;Employee Id&nbsp;&nbsp;</b></td>";
            str += "<td style='width:100px'><b>&nbsp;&nbsp;Name&nbsp;&nbsp;</b></td>";
            str += "<td style='width:100px'><b>&nbsp;&nbsp;Department&nbsp;&nbsp;</b></td>";
            str += "<td style='width:100px'><b>&nbsp;&nbsp;Designation &nbsp;&nbsp;</b></td>";
            str += "<td style='width:100px'><b>&nbsp;&nbsp;Unit &nbsp;&nbsp;</b></td>";
            str += "<td style='width:100px'><b>&nbsp;&nbsp;CardStatus &nbsp;&nbsp;</b></td>";
            str += "</tr>";
            str += "</thead>";
            str += "<tbody>";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                str += "<tr>";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    val = dt.Rows[i][j].ToString();
                    str += "<td>" + val + "</td>";
                }
                str += "</tr>";
            }
            str += "</tbody>";
            str += "</table>";

            return str;
        }

        public ActionResult GetNoFlashEmployeesDataExport(string reportname, string reporttype, string devices, string accesslevels, string EmployeedIds = "")
        {
            Util.CommonClass CC = new Util.CommonClass();
            DataTable dt = new DataTable();
            //dt = CC.GetDataTable("select Ch_CardNo as [Card No],	Ch_EmpId as [Emp ID],	Ch_FName as [Name],	DE_NAME as [Device],	AL_NAME as [Access Level],	LN_NAME as [Location] from  [vw_Smx_CardAccesslevel_Employee]");
            string Qry = string.Empty;
            Qry = $@"Select Ch_Csnnumber Cardno,Ch_EmpId EmployeeId,Ch_FName EmployeeName,isnull(DP_NAME,'') as Department,isnull(DN_NAME,'') as Designation,isnull(UT_NAME,'') as Unit,isnull(CS_NAME,'') as CardStatus from  [vw_Smx_CardHolderDetails]
                    Where Ch_Empid not in (select Tr_EmpId from Smx_Transaction AS A INNER JOIN dbo.Smx_AttendanceReaders AS B ON A.Tr_IpAddress=B.AR_IpAddress where Tr_Date>=(SELECT DATEADD(day, -Dayscount, Cast(GETDATE() as date)) from Smx_LastFlashdayscount)) ";
            //Where Ch_Empid not in (select Tr_EmpId from Smx_Transaction where Tr_Date >= (SELECT DATEADD(day, -Dayscount, Cast(GETDATE() as date)) from Smx_LastFlashdayscount)) ";



            if (string.IsNullOrEmpty(EmployeedIds) == false)
            {

                Qry += $" and Ch_EmpId in (select colA  from dbo.split('{EmployeedIds}',',')) ";
            }
            Qry = Qry + "  order by Ch_EmpId asc ";

            dt = CC.GetDataTable(Qry);



            MemoryStream fs = new MemoryStream();
            if (reporttype.ToUpper() == "EXCEL")
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add("Report");
                    ws.Cell(1, 1).Value = reportname;
                    ws.Cell(1, 1).Style.Font.FontSize = 20;
                    ws.Cell(1, 1).Style.Font.Bold = true;
                    ws.Range(1, 1, 1, 6).Merge().AddToNamed("Titles");


                    ws.Cell(2, 1).Value = "Total Number of Records : " + dt.Rows.Count.ToString();
                    ws.Cell(2, 1).Style.Font.FontSize = 16;
                    ws.Cell(2, 1).Style.Font.Bold = true;
                    ws.Range(2, 1, 2, 6).Merge().AddToNamed("NoOfRecords");


                    ws.Cell(3, 1).Value = "Generated Time : " + DateTime.Now;
                    ws.Cell(3, 1).Style.Font.FontSize = 10;
                    ws.Cell(3, 1).Style.Font.Bold = true;
                    ws.Range(3, 1, 3, 6).Merge().AddToNamed("NoOfRecords");

                    ws.Cell(4, 1).Value = "Generated By : " + Session["user"];
                    ws.Cell(4, 1).Style.Font.FontSize = 10;
                    ws.Cell(4, 1).Style.Font.Bold = true;
                    ws.Range(4, 1, 4, 6).Merge().AddToNamed("NoOfRecords");

                    for (int i = 1; i <= dt.Columns.Count; i++)
                    {
                        ws.Cell(6, i).Value = dt.Columns[i - 1].Caption;
                        ws.Cell(6, i).Style.Font.Bold = true;
                        ws.Cell(6, i).Style.Fill.SetBackgroundColor(XLColor.Yellow);
                    }
                    var rangeWithData = ws.Cell(7, 1).InsertData(dt.AsEnumerable());
                    wb.SaveAs(fs);
                }
            }

            if (reporttype.ToUpper() == "PDF")
            {
                fs = Util.Utility.ExportToPdf(dt, reportname + " \r\n " + "Total Number of Records : " + dt.Rows.Count.ToString() + "\r\n");
            }

            ViewData["FileName"] = reportname;
            ViewData["FileType"] = reporttype;
            ViewData["FileContent"] = fs;

            return View();
        }

        //------------ Access Level Report --------------

        public ActionResult GetAccessLevelReport()
        {

            return View();
        }

        public string GetAccessLevelReportData(string devices, string accesslevels, string EmployeedIds = "")
        {
            Util.CommonClass CC = new Util.CommonClass();
            DataTable dt = new DataTable();
            string Qry = string.Empty;
            Qry = $@"select Ch_CardNo,Ch_EmpId,Ch_FName,DE_NAME,AL_NAME,LN_NAME from  [vw_Smx_CardAccesslevel_Employee_Report]
            where 1=1";

            if (Convert.ToInt32(devices) > 0)
            {
                Qry += $" and DE_ID='{devices }'";
            }

            if (Convert.ToInt32(accesslevels) > 0)
            {
                Qry += $" and CAL_AL_ID='{accesslevels}'";
            }


            if (string.IsNullOrEmpty(EmployeedIds) == false)
            {

                Qry += $" and Ch_EmpId in (select colA  from dbo.split('{EmployeedIds}',',')) ";
            }
            Qry = Qry + "  order by Ch_empid,AL_NAME,DE_NAME asc ";

            dt = CC.GetDataTable(Qry);



            //dt = CC.GetDataTable("select * from  [vw_Smx_CardAccesslevel_Employee]");
            string str = "";
            string val = "";
            str += "<table>";
            str += "<thead>";
            str += "<tr bgcolor='#CFCFCF'>";
            //for (int i = 0; i < dt.Columns.Count; i++)
            //{
            //    str += "<td style='width:100px'><b>&nbsp;&nbsp;" + dt.Columns[i].ColumnName + "&nbsp;&nbsp;</b></td>";
            //}
            str += "<td style='width:100px'><b>&nbsp;&nbsp;Card Number&nbsp;&nbsp;</b></td>";
            str += "<td style='width:100px'><b>&nbsp;&nbsp;Employee Id&nbsp;&nbsp;</b></td>";
            str += "<td style='width:100px'><b>&nbsp;&nbsp;Name&nbsp;&nbsp;</b></td>";
            str += "<td style='width:100px'><b>&nbsp;&nbsp;Device&nbsp;&nbsp;</b></td>";
            str += "<td style='width:100px'><b>&nbsp;&nbsp;AccessLevel &nbsp;&nbsp;</b></td>";
            str += "<td style='width:100px'><b>&nbsp;&nbsp;Location &nbsp;&nbsp;</b></td>";
            str += "</tr>";
            str += "</thead>";
            str += "<tbody>";
            for ( int i = 0; i < dt.Rows.Count; i++)
            {
                str += "<tr>";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    val = dt.Rows[i][j].ToString();
                    str += "<td>" + val + "</td>";
                }
                str += "</tr>";
            }
            str += "</tbody>";
            str += "</table>";

            return str;
        }

        public ActionResult GetAccessLevelReportDataExport(string reportname, string reporttype,string devices, string accesslevels, string EmployeedIds = "")
        {
            Util.CommonClass CC = new Util.CommonClass();
            DataTable dt = new DataTable();
            //dt = CC.GetDataTable("select Ch_CardNo as [Card No],	Ch_EmpId as [Emp ID],	Ch_FName as [Name],	DE_NAME as [Device],	AL_NAME as [Access Level],	LN_NAME as [Location] from  [vw_Smx_CardAccesslevel_Employee]");
            string Qry = string.Empty;
            Qry = $@"select Ch_CardNo as [Card No],	Ch_EmpId as [Emp ID],	Ch_FName as [Name],	DE_NAME as [Device],	AL_NAME as [Access Level],	LN_NAME as [Location] from  [vw_Smx_CardAccesslevel_Employee_Report]
            where 1=1";

            if (Convert.ToInt32(devices) > 0)
            {
                Qry += $" and DE_ID='{devices }'";
            }

            if (Convert.ToInt32(accesslevels) > 0)
            {
                Qry += $" and CAL_AL_ID='{accesslevels}'";
            }


            if (string.IsNullOrEmpty(EmployeedIds) == false)
            {

                Qry += $" and Ch_EmpId in (select colA  from dbo.split('{EmployeedIds}',',')) ";
            }
            Qry = Qry + "  order by Ch_empid,AL_NAME,DE_NAME asc ";

            dt = CC.GetDataTable(Qry);


            
            MemoryStream fs = new MemoryStream();
            if (reporttype.ToUpper() == "EXCEL")
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add("Report");
                    ws.Cell(1, 1).Value = reportname;
                    ws.Cell(1, 1).Style.Font.FontSize = 20;
                    ws.Cell(1, 1).Style.Font.Bold = true;
                    ws.Range(1, 1, 1, 6).Merge().AddToNamed("Titles");


                    ws.Cell(2, 1).Value = "Total Number of Records : " + dt.Rows.Count.ToString();
                    ws.Cell(2, 1).Style.Font.FontSize = 16;
                    ws.Cell(2, 1).Style.Font.Bold = true;
                    ws.Range(2, 1, 2, 6).Merge().AddToNamed("NoOfRecords");


                    ws.Cell(3, 1).Value = "Generated Time : " + DateTime.Now;
                    ws.Cell(3, 1).Style.Font.FontSize = 10;
                    ws.Cell(3, 1).Style.Font.Bold = true;
                    ws.Range(3, 1, 3, 6).Merge().AddToNamed("NoOfRecords");

                    ws.Cell(4, 1).Value = "Generated By : " + Session["user"];
                    ws.Cell(4, 1).Style.Font.FontSize = 10;
                    ws.Cell(4, 1).Style.Font.Bold = true;
                    ws.Range(4, 1, 4, 6).Merge().AddToNamed("NoOfRecords");

                    for (int i = 1; i <= dt.Columns.Count; i++)
                    {
                        ws.Cell(6, i).Value = dt.Columns[i - 1].Caption;
                        ws.Cell(6, i).Style.Font.Bold = true;
                        ws.Cell(6, i).Style.Fill.SetBackgroundColor(XLColor.Yellow);
                    }
                    var rangeWithData = ws.Cell(7, 1).InsertData(dt.AsEnumerable());
                    wb.SaveAs(fs);
                }
            }

            if (reporttype.ToUpper() == "PDF")
            {
                fs = Util.Utility.ExportToPdf(dt, reportname + " \r\n " + "Total Number of Records : " + dt.Rows.Count.ToString() + "\r\n");
            }

            ViewData["FileName"] = reportname;
            ViewData["FileType"] = reporttype;
            ViewData["FileContent"] = fs;

            return View();
        }

        //----------- Transaction Report  -------------

        public ActionResult GetTransactionReport()
        {
            return View();
        }

        public string GetTransactionReportData(string srcdate, string trgdate, string devices, string ttypes,string EmployeedIds="")
        {
            Util.CommonClass CC = new Util.CommonClass();
            DataTable dt = new DataTable();
            StringBuilder strB = new StringBuilder();
            //dt = CC.GetDataTable("select Emp_Id,	Name as [FirstName],	Ch_LName as [LastName],	Card_Number as [BarCode Number],	[Date],	[Time],	LN_NAME as [Location], Message, Device_Name from  [vw_Smx_Transaction] where Convert(date,Date_Time) = '" + srcdate + "' order by Date_Time desc");

            string Qry = string.Empty;
            Qry = $@"Select Emp_Id,	Name as [FirstName],
            Ch_LName as [LastName],	Card_Number as [Card No],
            [Date],	[Time],	LN_NAME as [Location], Message, Device_Name 
            from  [vw_Smx_Transaction_Report] 
            where Convert(date,Date_Time) between '{Convert.ToDateTime(srcdate).ToString("MM/dd/yyyy")}' and '{ Convert.ToDateTime(trgdate).ToString("MM/dd/yyyy") }'";

            if (Convert.ToInt32(devices) > 0)
            {
                Qry += $" and De_Id='{devices }'";
            }
            
            if (Convert.ToInt32(ttypes) > 0)
            {
                 Qry+=  $" and Tr_TType='{ttypes}'";
            }


            if (string.IsNullOrEmpty(EmployeedIds) == false)
            {

                Qry += $" and Emp_Id in (select colA  from dbo.split('{EmployeedIds}',',')) ";
            }
            Qry = Qry + "  order by Date,Time asc ";

            dt = CC.GetDataTable(Qry);
            

            
            string str = "";
            string val = "";
            if (dt.Rows.Count > 1000)
            {
                str += "<table>";
                str += "<thead>";
                str += "<tr bgcolor='#CFCFCF'>";
                str += "<td>Exception Message</td>";
                str += "</tr>";
                str += "</thead>";
                str += "<tbody>";
                str += "<tr><td>Total number of records exceeds 1000 rows. Please click export button to get the report.</td></tr>";
                str += "</tbody>";
                str += "</table>";

            }
            else
            {
                str += "<table>";
                str += "<thead>";
                str += "<tr bgcolor='#CFCFCF'>";
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    str += "<td style='width:100px'><b>&nbsp;&nbsp;" + dt.Columns[i].ColumnName + "&nbsp;&nbsp;</b></td>";
                }
                //str += "<td style='width:100px'><b>&nbsp;&nbsp;Card Number&nbsp;&nbsp;</b></td>";
                //str += "<td style='width:100px'><b>&nbsp;&nbsp;Employee Id&nbsp;&nbsp;</b></td>";
                //str += "<td style='width:100px'><b>&nbsp;&nbsp;Name&nbsp;&nbsp;</b></td>";
                //str += "<td style='width:100px'><b>&nbsp;&nbsp;Device&nbsp;&nbsp;</b></td>";
                //str += "<td style='width:100px'><b>&nbsp;&nbsp;Access Level Name Number&nbsp;&nbsp;</b></td>";
                //str += "<td style='width:100px'><b>&nbsp;&nbsp;Location Name&nbsp;&nbsp;</b></td>";
                str += "</tr>";
                str += "</thead>";
                str += "<tbody>";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str += "<tr>";
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        val = dt.Rows[i][j].ToString();
                        str += "<td>" + val + "</td>";
                    }
                    str += "</tr>";
                }
                str += "</tbody>";
                str += "</table>";
            }
            

            return str;
        }

        public JsonResult GetTransactiontypes()
        {
            Array ttypes;
            ttypes = Util.Utility.GetTransactiontypes();

            return Json(ttypes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDevices()
        {
            Array devices;
            devices = Util.Utility.GetDevice();

            return Json(devices, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNoflashCount()
        {
            Array devices;
            devices = Util.Utility.GetNoflashCount();

            return Json(devices, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAccesslvels()
        {
            Array accesslevels;
            accesslevels = Util.Utility.GetAccesslevel();

            return Json(accesslevels, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCardStatus()
        {
            Array cardstatus;
            cardstatus = Util.Utility.GetCardStatus();
            return Json(cardstatus, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTransactionReportDataExport(string srcdate, string trgdate, string devices, string ttypes, string reportname, string reporttype, string EmployeedIds = "")
        {
            Util.CommonClass CC = new Util.CommonClass();
            DataTable dt = new DataTable();
            //dt = CC.GetDataTable("select Emp_Id,	Name as [FirstName],	Ch_LName as [LastName],	Card_Number as [BarCode Number],	[Date],	[Time],	LN_NAME as [Location], Message, Device_Name from  [vw_Smx_Transaction] where Convert(date,Date_Time) = '" + srcdate + "'");
            //if (Convert.ToInt32(devices) > 0)
            //{
            //    if (Convert.ToInt32(ttypes) > 0)
            //    {
            //        dt = CC.GetDataTable("select Emp_Id,	Name as [FirstName],	Ch_LName as [LastName],	Card_Number as [BarCode Number],	[Date],	[Time],	LN_NAME as [Location], Message, Device_Name from  [vw_Smx_Transaction] where Convert(date,Date_Time) between '" + srcdate + "' and '" + trgdate + "' and De_Id='" + devices + "' and Tr_TType='" + ttypes + "' order by Date,Time asc");
            //    }
            //    else
            //    {
            //        dt = CC.GetDataTable("select Emp_Id,	Name as [FirstName],	Ch_LName as [LastName],	Card_Number as [BarCode Number],	[Date],	[Time],	LN_NAME as [Location], Message, Device_Name from  [vw_Smx_Transaction] where Convert(date,Date_Time) between '" + srcdate + "' and '" + trgdate + "' and De_Id='" + devices + "' order by Date,Time asc");
            //    }
            //}
            //else
            //{
            //    if (Convert.ToInt32(ttypes) > 0)
            //    {
            //        dt = CC.GetDataTable("select Emp_Id,	Name as [FirstName],	Ch_LName as [LastName],	Card_Number as [BarCode Number],	[Date],	[Time],	LN_NAME as [Location], Message, Device_Name from  [vw_Smx_Transaction] where Convert(date,Date_Time) between '" + srcdate + "' and '" + trgdate + "' and Tr_TType='" + ttypes + "' order by Date,Time asc");
            //    }
            //    else
            //    {
            //        dt = CC.GetDataTable("select Emp_Id,	Name as [FirstName],	Ch_LName as [LastName],	Card_Number as [BarCode Number],	[Date],	[Time],	LN_NAME as [Location], Message, Device_Name from  [vw_Smx_Transaction] where Convert(date,Date_Time) between '" + srcdate + "' and '" + trgdate + "' order by Date,Time asc");
            //    }
            //} 
            string Qry = string.Empty;
            Qry = $@"Select Emp_Id,	Name as [FirstName],
            Ch_LName as [LastName],	Card_Number as [Card No],
            [Date],	[Time],	LN_NAME as [Location], Message, Device_Name 
            from  [vw_Smx_Transaction_Report]             
            where Convert(date, Date_Time) between '{Convert.ToDateTime(srcdate).ToString("MM/dd/yyyy")}' and '{ Convert.ToDateTime(trgdate).ToString("MM/dd/yyyy") }'";

            if (Convert.ToInt32(devices) > 0)
            {
                Qry += $" and De_Id='{devices }'";
            }

            if (Convert.ToInt32(ttypes) > 0)
            {
                Qry += $" and Tr_TType='{ttypes}'";
            }


            if (string.IsNullOrEmpty(EmployeedIds) == false)
            {

                Qry += $" and Emp_Id in (select colA  from dbo.split('{EmployeedIds}',',')) ";
            }
            Qry = Qry + "  order by Date,Time asc ";

            dt = CC.GetDataTable(Qry);



            MemoryStream fs = new MemoryStream();
            if (reporttype.ToUpper() == "EXCEL")
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add("Report");
                    ws.Cell(1, 1).Value = reportname;
                    ws.Cell(1, 1).Style.Font.FontSize = 20;
                    ws.Cell(1, 1).Style.Font.Bold = true;
                    ws.Range(1, 1, 1, 10).Merge().AddToNamed("Titles");


                    ws.Cell(2, 1).Value = "Total Number of Records : " + dt.Rows.Count.ToString();
                    ws.Cell(2, 1).Style.Font.FontSize = 16;
                    ws.Cell(2, 1).Style.Font.Bold = true;
                    ws.Range(2, 1, 2, 10).Merge().AddToNamed("NoOfRecords");


                    ws.Cell(3, 1).Value = "Generated Time : " + DateTime.Now;
                    ws.Cell(3, 1).Style.Font.FontSize = 10;
                    ws.Cell(3, 1).Style.Font.Bold = true;
                    ws.Range(3, 1, 3, 6).Merge().AddToNamed("NoOfRecords");

                    ws.Cell(4, 1).Value = "Generated By : " + Session["user"];
                    ws.Cell(4, 1).Style.Font.FontSize = 10;
                    ws.Cell(4, 1).Style.Font.Bold = true;
                    ws.Range(4, 1, 4, 6).Merge().AddToNamed("NoOfRecords");

                    for (int i = 1; i <= dt.Columns.Count; i++)
                    {
                        ws.Cell(6, i).Value = dt.Columns[i - 1].Caption;
                        ws.Cell(6, i).Style.Font.Bold = true;
                        ws.Cell(6, i).Style.Fill.SetBackgroundColor(XLColor.Yellow);
                    }
                    var rangeWithData = ws.Cell(7, 1).InsertData(dt.AsEnumerable());
                    wb.SaveAs(fs);
                }
            }

            if (reporttype.ToUpper() == "PDF")
            {
                fs = Util.Utility.ExportToPdf(dt, reportname + " \r\n " + "Total Number of Records : " + dt.Rows.Count.ToString() + "\r\n");
            }

            ViewData["FileName"] = reportname;
            ViewData["FileType"] = reporttype;
            ViewData["FileContent"] = fs;

            return View();
        }

        //------------ Attendance Single Day report --------------

        public ActionResult GetAttendanceSingleDay()
        {
            return View();
        }

        public string GetAttendanceSingleDayData(string srcdate, string trgdate, string devices, string ttypes, string EmployeedIds = "")
        {
            Util.CommonClass CC = new Util.CommonClass();
            DataTable dt = new DataTable();
            dt = CC.GetDataTable("exec sp_Firstin_Lastout_employeewise '" + Convert.ToDateTime(srcdate).ToString("MM/dd/yyyy") + "', '" + Convert.ToDateTime(trgdate).ToString("MM/dd/yyyy") + "','"+ EmployeedIds + "'");
            string str = "";
            string val = "";
            str += "<table>";
            str += "<thead>";
            str += "<tr bgcolor='#CFCFCF'>";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                str += "<td style='width:100px'><b>&nbsp;&nbsp;" + dt.Columns[i].ColumnName + "&nbsp;&nbsp;</b></td>";
            }
            //str += "<td style='width:100px'><b>Total Hours</b></td>";
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
                    val = dt.Rows[i][j].ToString();
                    if (val.Trim() != "")
                    {
                        string[] seperator = { "<br/>" };
                        var valsubset = val.Split(seperator, StringSplitOptions.None);
                        if (valsubset.Length == 4)
                        {
                            TotalHrs = TotalHrs + Convert.ToDecimal(valsubset[2].Split(':')[1]);
                            TotalHrs = TotalHrs + +Convert.ToDecimal("0." + valsubset[2].Split(':')[2]) * Convert.ToDecimal("1.66667");
                        }
                        str += "<td >" + dt.Rows[i][j].ToString().Replace(" ", "&nbsp;") + "</td>";
                    }
                    else
                    {
                        str += "<td>NR</td>";
                    }

                }
                //hrs = Convert.ToInt16(TotalHrs.ToString().Split('.')[0]);
                //mins = (TotalHrs - hrs);
                //mins = mins * Convert.ToDecimal("0.6");
                //mins = Math.Round(mins, 2);
                //TotalHrs = Convert.ToDecimal(hrs + mins);
                //str += "<td>" + TotalHrs + "</td>";
                str += "</tr>";
            }
            str += "</tbody>";
            str += "</table>";

            return str;
        }

        public ActionResult GetAttendanceSingleDayDataExport(string srcdate, string trgdate, string reportname, string reporttype, string devices, string ttypes, string EmployeedIds = "")
        {
            Util.CommonClass CC = new Util.CommonClass();
            DataTable dt = new DataTable();            
            dt = CC.GetDataTable("exec sp_Firstin_Lastout_employeewise '" + Convert.ToDateTime(srcdate).ToString("MM/dd/yyyy") + "', '" + Convert.ToDateTime(trgdate).ToString("MM/dd/yyyy") + "','" + EmployeedIds + "'");

            string str = "";
            string val = "";
            str += "<table>";
            str += "<thead>";
            str += "<tr>";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                str += "<th>";
                str += "" + dt.Columns[i].ColumnName + "";
                str += "</th>";
            }
            //str += "<td style='width:100px'><b>Total Hours</b></td>";
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
                    val = dt.Rows[i][j].ToString();
                    if (val.Trim() != "")
                    {
                        string[] seperator = { "<br/>" };
                        var valsubset = val.Split(seperator, StringSplitOptions.None);
                        if (valsubset.Length == 4)
                        {
                            TotalHrs = TotalHrs + Convert.ToDecimal(valsubset[2].Split(':')[1]);
                            TotalHrs = TotalHrs + +Convert.ToDecimal("0." + valsubset[2].Split(':')[2]) * Convert.ToDecimal("1.66667");
                        }
                        str += "<td >" + dt.Rows[i][j].ToString().Replace("<br/>", "\r\n") + "</td>";
                    }
                    else
                    {
                        str += "<td>NR</td>";
                    }

                }
                //hrs = Convert.ToInt16(TotalHrs.ToString().Split('.')[0]);
                //mins = (TotalHrs - hrs);
                //mins = mins * Convert.ToDecimal("0.6");
                //mins = Math.Round(mins, 2);
                //TotalHrs = Convert.ToDecimal(hrs + mins);
                //str += "<td>" + TotalHrs + "</td>";
                str += "</tr>";
            }
            str += "</tbody>";
            str += "</table>";

            DataTable dtnew = Util.Utility.ConvertHTMLTablesToDataSet(str);
            MemoryStream fs = new MemoryStream();
            if (reporttype.ToUpper() == "EXCEL")
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add("Report");
                    ws.Cell(1, 1).Value = reportname;
                    ws.Cell(1, 1).Style.Font.FontSize = 20;
                    ws.Cell(1, 1).Style.Font.Bold = true;
                    ws.Range(1, 1, 1, 10).Merge().AddToNamed("Titles");


                    ws.Cell(2, 1).Value = "Total Number of Records : " + dtnew.Rows.Count.ToString();
                    ws.Cell(2, 1).Style.Font.FontSize = 16;
                    ws.Cell(2, 1).Style.Font.Bold = true;
                    ws.Range(2, 1, 2, 10).Merge().AddToNamed("NoOfRecords");

                    ws.Cell(3, 1).Value = "Generated Time : " + DateTime.Now;
                    ws.Cell(3, 1).Style.Font.FontSize = 10;
                    ws.Cell(3, 1).Style.Font.Bold = true;
                    ws.Range(3, 1, 3, 6).Merge().AddToNamed("NoOfRecords");

                    ws.Cell(4, 1).Value = "Generated By : " + Session["user"];
                    ws.Cell(4, 1).Style.Font.FontSize = 10;
                    ws.Cell(4, 1).Style.Font.Bold = true;
                    ws.Range(4, 1, 4, 6).Merge().AddToNamed("NoOfRecords");

                    for (int i = 1; i <= dtnew.Columns.Count; i++)
                    {
                        ws.Cell(6, i).Value = dtnew.Columns[i - 1].Caption;
                        ws.Cell(6, i).Style.Font.Bold = true;
                        ws.Cell(6, i).Style.Fill.SetBackgroundColor(XLColor.Yellow);
                    }
                    var rangeWithData = ws.Cell(7, 1).InsertData(dtnew.AsEnumerable());
                    wb.SaveAs(fs);
                }
            }

            if (reporttype.ToUpper() == "PDF")
            {
                fs = Util.Utility.ExportToPdf(dtnew, reportname + " \r\n " + "Total Number of Records : " + dtnew.Rows.Count.ToString() + "\r\n");
            }

            ViewData["FileName"] = reportname;
            ViewData["FileType"] = reporttype;
            ViewData["FileContent"] = fs;

            return View();
        }

        //------------ Attendance Monthly Detail Report ---------------

        public ActionResult GetMonthlyAttendanceDetail()
        {
            return View();
        }

        public string GetMonthlyAttendanceDetailData(string month, string year)
        {
            Util.CommonClass CC = new Util.CommonClass();
            DataTable dt = new DataTable();
            dt = CC.GetDataTable("exec sp_GetAttendenceForMonth '" + month + "', '" + year + "'");
            string str = "";
            string val = "";
            str += "<table>";
            str += "<thead>";
            str += "<tr bgcolor='#CFCFCF'>";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                str += "<td style='width:100px'><b>&nbsp;&nbsp;" + dt.Columns[i].ColumnName + "&nbsp;&nbsp;</b></td>";
            }
            str += "<td style='width:100px'><b>Total Hours</b></td>";
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
                    val = dt.Rows[i][j].ToString();
                    if (val.Trim() != "")
                    {
                        string[] seperator = { "<br/>" };
                        var valsubset = val.Split(seperator, StringSplitOptions.None);
                        if (valsubset.Length == 4)
                        {
                            TotalHrs = TotalHrs + Convert.ToDecimal(valsubset[2].Split(':')[1]);
                            TotalHrs = TotalHrs + +Convert.ToDecimal("0." + valsubset[2].Split(':')[2]) * Convert.ToDecimal("1.66667");
                        }
                        str += "<td >" + dt.Rows[i][j].ToString().Replace(" ", "&nbsp;") + "</td>";
                    }
                    else
                    {
                        str += "<td>NR</td>";
                    }

                }
                hrs = Convert.ToInt16(TotalHrs.ToString().Split('.')[0]);
                mins = (TotalHrs - hrs);
                mins = mins * Convert.ToDecimal("0.6");
                mins = Math.Round(mins, 2);
                TotalHrs = Convert.ToDecimal(hrs + mins);
                str += "<td>" + TotalHrs + "</td>";
                str += "</tr>";
            }
            str += "</tbody>";
            str += "</table>";

            return str;
        }

        public ActionResult GetMonthlyAttendanceDetailDataExport(string month, string year, string reportname, string reporttype)
        {
            Util.CommonClass CC = new Util.CommonClass();
            DataTable dt = new DataTable();
            dt = CC.GetDataTable("exec sp_GetAttendenceForMonth '" + month + "', '" + year + "'");
            string str = "";
            string val = "";
            str += "<table>";
            str += "<thead>";
            str += "<tr >";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                str += "<th >" + dt.Columns[i].ColumnName + "</th>";
            }
            str += "<th>Total Hours</th>";
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
                    val = dt.Rows[i][j].ToString();
                    if (val.Trim() != "")
                    {
                        string[] seperator = { "<br/>" };
                        var valsubset = val.Split(seperator, StringSplitOptions.None);
                        if (valsubset.Length == 4)
                        {
                            TotalHrs = TotalHrs + Convert.ToDecimal(valsubset[2].Split(':')[1]);
                            TotalHrs = TotalHrs + +Convert.ToDecimal("0." + valsubset[2].Split(':')[2]) * Convert.ToDecimal("1.66667");
                        }
                        str += "<td >" + dt.Rows[i][j].ToString().Replace("<br/>", "\r\n;") + "</td>";
                    }
                    else
                    {
                        str += "<td>NR</td>";
                    }

                }
                hrs = Convert.ToInt16(TotalHrs.ToString().Split('.')[0]);
                mins = (TotalHrs - hrs);
                mins = mins * Convert.ToDecimal("0.6");
                mins = Math.Round(mins, 2);
                TotalHrs = Convert.ToDecimal(hrs + mins);
                str += "<td>" + TotalHrs + "</td>";
                str += "</tr>";
            }
            str += "</tbody>";
            str += "</table>";


            DataTable dtnew = Util.Utility.ConvertHTMLTablesToDataSet(str);
            MemoryStream fs = new MemoryStream();
            if (reporttype.ToUpper() == "EXCEL")
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add("Report");
                    ws.Cell(1, 1).Value = reportname;
                    ws.Cell(1, 1).Style.Font.FontSize = 20;
                    ws.Cell(1, 1).Style.Font.Bold = true;
                    ws.Range(1, 1, 1, 10).Merge().AddToNamed("Titles");


                    ws.Cell(2, 1).Value = "Total Number of Records : " + dtnew.Rows.Count.ToString();
                    ws.Cell(2, 1).Style.Font.FontSize = 16;
                    ws.Cell(2, 1).Style.Font.Bold = true;
                    ws.Range(2, 1, 2, 10).Merge().AddToNamed("NoOfRecords");

                    for (int i = 1; i <= dtnew.Columns.Count; i++)
                    {
                        ws.Cell(4, i).Value = dtnew.Columns[i - 1].Caption;
                        ws.Cell(4, i).Style.Font.Bold = true;
                        ws.Cell(4, i).Style.Fill.SetBackgroundColor(XLColor.Yellow);
                    }
                    var rangeWithData = ws.Cell(5, 1).InsertData(dtnew.AsEnumerable());
                    wb.SaveAs(fs);
                }
            }

            if (reporttype.ToUpper() == "PDF")
            {
                fs = Util.Utility.ExportToPdf(dtnew, reportname + " \r\n " + "Total Number of Records : " + dtnew.Rows.Count.ToString() + "\r\n");
            }

            ViewData["FileName"] = reportname;
            ViewData["FileType"] = reporttype;
            ViewData["FileContent"] = fs;

            return View();
        }

        //-------------- Leave Report Sumary --------------------------------
        
        public ActionResult LeaveSummaryReport()
        {
            return View();
        }

        public string LeaveSummaryReportData()
        {
            Util.CommonClass CC = new Util.CommonClass();
            DataTable dt = new DataTable();
            DataTable dtemp = new DataTable();
            DataTable dtleave = new DataTable();
            dt = CC.GetDataTable("select * from  vw_LeaveSummary");
            dtemp = CC.GetDataTable("Select distinct Ch_EmpId,Ch_FName from Smx_CardHolder");
            dtleave = CC.GetDataTable("Select [Lv_ShortDesc],Lv_Description,[Lv_MaxDays] from Smx_Leave");
            string str = "";
            string val = "";
            str += "<table>";
            str += "<thead>";
            str += "<tr bgcolor='#CFCFCF'>";
            //for (int i = 0; i < dt.Columns.Count; i++)
            //{
            //    str += "<td style='width:100px'><b>&nbsp;&nbsp;" + dt.Columns[i].ColumnName + "&nbsp;&nbsp;</b></td>";
            //}
            str += "<td style='width:100px'><b>&nbsp;&nbsp;S.No&nbsp;&nbsp;</b></td>";
            str += "<td style='width:100px'><b>&nbsp;&nbsp;Employee Id&nbsp;&nbsp;</b></td>";
            str += "<td style='width:100px'><b>&nbsp;&nbsp;Name&nbsp;&nbsp;</b></td>";
            for (int cnt = 0; cnt <= dtleave.Rows.Count - 1; cnt++)
            {
                str += "<td style='width:100px'><b>" + dtleave.Rows[cnt]["Lv_Description"].ToString() + "</b></td>";
                //str += "<td style='width:100px'><b>CL</b></td>";
                //str += "<td style='width:100px'><b>SL</b></td>";
            }
            
            
            str += "</tr>";
            str += "</thead>";
            str += "<tbody>";
            for (int empcount = 0; empcount <= dtemp.Rows.Count - 1; empcount++ )
            {
              //  DataTable tblFiltered = dt.AsEnumerable()
              //.Where(row => row.Field<String>("Ch_EmpId") == dtemp.Rows[empcount]["Ch_EmpId"])
              //.OrderByDescending(row => row.Field<String>("Lv_Description"))
              //.CopyToDataTable();
                str += "<tr>";
                str += "<td>" + (empcount+1).ToString() + "</td>";
                str += "<td>" + dtemp.Rows[empcount]["Ch_EmpId"] + "</td>";
                str += "<td>" + dtemp.Rows[empcount]["Ch_FName"] + "</td>";
                for(int leavecnt = 0; leavecnt <= dtleave.Rows.Count -1 ; leavecnt++)
                {
                    DataRow[] tblFiltered = dt.Select("Ch_EmpId = '" + dtemp.Rows[empcount]["Ch_EmpId"] + "' and Lv_ShortDesc = '" + dtleave.Rows[leavecnt]["Lv_ShortDesc"] + "'");

                    if (tblFiltered.Length  > 0)
                    {
                        string openbal = tblFiltered[0]["Lv_ShortDesc"].ToString() + " Opening Balance : " + tblFiltered[0]["MaxDays"].ToString();
                        
                        string leavetaken = tblFiltered[0]["Lv_ShortDesc"].ToString() + " Leave Taken : " + tblFiltered[0]["LeaveTaken"].ToString();
                        string availbal = tblFiltered[0]["Lv_ShortDesc"].ToString() + " Balance : " + (Convert.ToDecimal(tblFiltered[0]["MaxDays"]) - Convert.ToDecimal(tblFiltered[0]["LeaveTaken"])).ToString();
                        str += "<td>" + openbal + "<br> " + leavetaken + "<br />" + availbal + "</td>";

                        //if(tblFiltered.Length > 0)
                        //{
                        //    string openbal = tblFiltered[i]["Lv_ShortDesc"].ToString() + " : " + tblFiltered[i]["MaxDays"].ToString();
                        //    str += "<td>" + openbal + "</td>";
                        //    string leavetaken  = tblFiltered[i]["Lv_ShortDesc"].ToString() + " : " + tblFiltered[i]["LeaveTaken"].ToString();
                        //    str += "<td>" + leavetaken + "</td>";
                        //    string availbal = "Balance : " + (Convert.ToDecimal(tblFiltered[i]["MaxDays"]) - Convert.ToDecimal(tblFiltered[i]["LeaveTaken"])).ToString();
                        //    str += "<td>" + availbal + "</td>";
                        //}
                        //else
                        //{
                        //    str += "<td>0</td>";
                        //    str += "<td>0</td>";
                        //    str += "<td>0</td>";
                        //}

                    }
                    else
                    {
                        str += "<td> Opening Balance : " + dtleave.Rows[leavecnt]["Lv_MaxDays"] + "<br/> Leave Availed : 0<br /> Balance Leave : " + dtleave.Rows[leavecnt]["Lv_MaxDays"] + " </td>";
                    }
                    
                }
                
               
                str += "</tr>";
            }
                
            str += "</tbody>";
            str += "</table>";

            return str;
        }

        public ActionResult LeaveSummaryReportDataExport(string reportname, string reporttype)
        {
            Util.CommonClass CC = new Util.CommonClass();
            DataTable dt = new DataTable();
            DataTable dtemp = new DataTable();
            DataTable dtleave = new DataTable();
            dt = CC.GetDataTable("select * from  vw_LeaveSummary");
            dtemp = CC.GetDataTable("Select distinct Ch_EmpId,Ch_FName from Smx_CardHolder");
            dtleave = CC.GetDataTable("Select [Lv_ShortDesc],Lv_Description,[Lv_MaxDays] from Smx_Leave");
            string str = "";
            string val = "";
            str += "<table>";
            str += "<thead>";
            str += "<tr>";
            //for (int i = 0; i < dt.Columns.Count; i++)
            //{
            //    str += "<td style='width:100px'><b>&nbsp;&nbsp;" + dt.Columns[i].ColumnName + "&nbsp;&nbsp;</b></td>";
            //}
            str += "<th>S.No</th>";
            str += "<th>Employee Id</th>";
            str += "<th>Name</th>";
            for (int cnt = 0; cnt <= dtleave.Rows.Count - 1; cnt++)
            {
                str += "<th>" + dtleave.Rows[cnt]["Lv_Description"].ToString() + "</th>";
                //str += "<td style='width:100px'><b>CL</b></td>";
                //str += "<td style='width:100px'><b>SL</b></td>";
            }


            str += "</tr>";
            str += "</thead>";
            str += "<tbody>";
            for (int empcount = 0; empcount <= dtemp.Rows.Count - 1; empcount++)
            {
                //  DataTable tblFiltered = dt.AsEnumerable()
                //.Where(row => row.Field<String>("Ch_EmpId") == dtemp.Rows[empcount]["Ch_EmpId"])
                //.OrderByDescending(row => row.Field<String>("Lv_Description"))
                //.CopyToDataTable();
                str += "<tr>";
                str += "<td>" + (empcount + 1).ToString() + "</td>";
                str += "<td>" + dtemp.Rows[empcount]["Ch_EmpId"] + "</td>";
                str += "<td>" + dtemp.Rows[empcount]["Ch_FName"] + "</td>";
                for (int leavecnt = 0; leavecnt <= dtleave.Rows.Count - 1; leavecnt++)
                {
                    DataRow[] tblFiltered = dt.Select("Ch_EmpId = '" + dtemp.Rows[empcount]["Ch_EmpId"] + "' and Lv_ShortDesc = '" + dtleave.Rows[leavecnt]["Lv_ShortDesc"] + "'");

                    if (tblFiltered.Length > 0)
                    {
                        string openbal = tblFiltered[0]["Lv_ShortDesc"].ToString() + " Opening Balance : " + tblFiltered[0]["MaxDays"].ToString();

                        string leavetaken = tblFiltered[0]["Lv_ShortDesc"].ToString() + " Leave Taken : " + tblFiltered[0]["LeaveTaken"].ToString();
                        string availbal = tblFiltered[0]["Lv_ShortDesc"].ToString() + " Balance : " + (Convert.ToDecimal(tblFiltered[0]["MaxDays"]) - Convert.ToDecimal(tblFiltered[0]["LeaveTaken"])).ToString();
                        str += "<td>" + openbal + "\r\n" + leavetaken + "\r\n" + availbal + "</td>";
                    }
                    else
                    {
                        str += "<td> Opening Balance : " + dtleave.Rows[leavecnt]["Lv_MaxDays"] + "\r\n Leave Availed : 0 \r\n Balance Leave : " + dtleave.Rows[leavecnt]["Lv_MaxDays"] + " </td>";
                    }

                }
                str += "</tr>";
            }

            str += "</tbody>";
            str += "</table>";

            DataTable dtnew = Util.Utility.ConvertHTMLTablesToDataSet(str);
            MemoryStream fs = new MemoryStream();
            if (reporttype.ToUpper() == "EXCEL")
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add("Report");
                    ws.Cell(1, 1).Value = reportname;
                    ws.Cell(1, 1).Style.Font.FontSize = 20;
                    ws.Cell(1, 1).Style.Font.Bold = true;
                    ws.Range(1, 1, 1, 10).Merge().AddToNamed("Titles");


                    ws.Cell(2, 1).Value = "Total Number of Records : " + dtnew.Rows.Count.ToString();
                    ws.Cell(2, 1).Style.Font.FontSize = 16;
                    ws.Cell(2, 1).Style.Font.Bold = true;
                    ws.Range(2, 1, 2, 10).Merge().AddToNamed("NoOfRecords");

                    for (int i = 1; i <= dtnew.Columns.Count; i++)
                    {
                        ws.Cell(4, i).Value = dtnew.Columns[i - 1].Caption;
                        ws.Cell(4, i).Style.Font.Bold = true;
                        ws.Cell(4, i).Style.Fill.SetBackgroundColor(XLColor.Yellow);
                    }
                    var rangeWithData = ws.Cell(5, 1).InsertData(dtnew.AsEnumerable());
                    wb.SaveAs(fs);
                }
            }

            if (reporttype.ToUpper() == "PDF")
            {
                fs = Util.Utility.ExportToPdf(dtnew, reportname + " \r\n " + "Total Number of Records : " + dtnew.Rows.Count.ToString() + "\r\n");
            }

            ViewData["FileName"] = reportname;
            ViewData["FileType"] = reporttype;
            ViewData["FileContent"] = fs;

            return View();
        }

        //-------------- Late comers Report -----------------------------------

        public ActionResult LatecomersReport()
        {
            return View();
        }

        public string GetLatecomersReportData(string date, int gracetime)
        {
            Util.CommonClass CC = new Util.CommonClass();
            DataTable dt = new DataTable();
            dt = CC.GetDataTable("exec sp_GetLatecomers '" + date + "', '" + gracetime + "'");
            string str = "";
            string val = "";
            str += "<table>";
            str += "<thead>";
            str += "<tr bgcolor='#CFCFCF'>";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                str += "<td style='width:100px'><b>&nbsp;&nbsp;" + dt.Columns[i].ColumnName + "&nbsp;&nbsp;</b></td>";
            }
            //str += "<td style='width:100px'><b>Total Hours</b></td>";
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
                    val = dt.Rows[i][j].ToString();
                    if (val.Trim() != "")
                    {
                        string[] seperator = { "<br/>" };
                        var valsubset = val.Split(seperator, StringSplitOptions.None);
                        if (valsubset.Length == 4)
                        {
                            TotalHrs = TotalHrs + Convert.ToDecimal(valsubset[2].Split(':')[1]);
                            TotalHrs = TotalHrs + +Convert.ToDecimal("0." + valsubset[2].Split(':')[2]) * Convert.ToDecimal("1.66667");
                        }
                        str += "<td >" + dt.Rows[i][j].ToString().Replace(" ", "&nbsp;") + "</td>";
                    }
                    else
                    {
                        str += "<td>NR</td>";
                    }

                }
                //hrs = Convert.ToInt16(TotalHrs.ToString().Split('.')[0]);
                //mins = (TotalHrs - hrs);
                //mins = mins * Convert.ToDecimal("0.6");
                //mins = Math.Round(mins, 2);
                //TotalHrs = Convert.ToDecimal(hrs + mins);
                //str += "<td>" + TotalHrs + "</td>";
                str += "</tr>";
            }
            str += "</tbody>";
            str += "</table>";

            return str;
        }

        public ActionResult GetLatecomersReportDataExport(string date, string gracetime, string reportname, string reporttype)
        {
            Util.CommonClass CC = new Util.CommonClass();
            DataTable dt = new DataTable();
            dt = CC.GetDataTable("exec sp_GetLatecomers '" + date + "', '" + gracetime + "'");
            string str = "";
            string val = "";
            str += "<table>";
            str += "<thead>";
            str += "<tr >";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                str += "<th >" + dt.Columns[i].ColumnName + "</th>";
            }
            str += "<th>Total Hours</th>";
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
                    val = dt.Rows[i][j].ToString();
                    if (val.Trim() != "")
                    {
                        string[] seperator = { "<br/>" };
                        var valsubset = val.Split(seperator, StringSplitOptions.None);
                        if (valsubset.Length == 4)
                        {
                            TotalHrs = TotalHrs + Convert.ToDecimal(valsubset[2].Split(':')[1]);
                            TotalHrs = TotalHrs + +Convert.ToDecimal("0." + valsubset[2].Split(':')[2]) * Convert.ToDecimal("1.66667");
                        }
                        str += "<td >" + dt.Rows[i][j].ToString().Replace("<br/>", "\r\n;") + "</td>";
                    }
                    else
                    {
                        str += "<td>NR</td>";
                    }

                }
                hrs = Convert.ToInt16(TotalHrs.ToString().Split('.')[0]);
                mins = (TotalHrs - hrs);
                mins = mins * Convert.ToDecimal("0.6");
                mins = Math.Round(mins, 2);
                TotalHrs = Convert.ToDecimal(hrs + mins);
                str += "<td>" + TotalHrs + "</td>";
                str += "</tr>";
            }
            str += "</tbody>";
            str += "</table>";


            DataTable dtnew = Util.Utility.ConvertHTMLTablesToDataSet(str);
            MemoryStream fs = new MemoryStream();
            if (reporttype.ToUpper() == "EXCEL")
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add("Report");
                    ws.Cell(1, 1).Value = reportname;
                    ws.Cell(1, 1).Style.Font.FontSize = 20;
                    ws.Cell(1, 1).Style.Font.Bold = true;
                    ws.Range(1, 1, 1, 10).Merge().AddToNamed("Titles");


                    ws.Cell(2, 1).Value = "Total Number of Records : " + dtnew.Rows.Count.ToString();
                    ws.Cell(2, 1).Style.Font.FontSize = 16;
                    ws.Cell(2, 1).Style.Font.Bold = true;
                    ws.Range(2, 1, 2, 10).Merge().AddToNamed("NoOfRecords");

                    for (int i = 1; i <= dtnew.Columns.Count; i++)
                    {
                        ws.Cell(4, i).Value = dtnew.Columns[i - 1].Caption;
                        ws.Cell(4, i).Style.Font.Bold = true;
                        ws.Cell(4, i).Style.Fill.SetBackgroundColor(XLColor.Yellow);
                    }
                    var rangeWithData = ws.Cell(5, 1).InsertData(dtnew.AsEnumerable());
                    wb.SaveAs(fs);
                }
            }

            if (reporttype.ToUpper() == "PDF")
            {
                fs = Util.Utility.ExportToPdf(dtnew, reportname + " \r\n " + "Total Number of Records : " + dtnew.Rows.Count.ToString() + "\r\n");
            }

            ViewData["FileName"] = reportname;
            ViewData["FileType"] = reporttype;
            ViewData["FileContent"] = fs;

            return View();
        }
              
        
        //-------------- Early comers Report ---------------------------------

        //-------------- Late going Report -----------------------------------

        //-------------- Early going Report ----------------------------------

        //-------------- Employee Card Issued Report -------------------------

          public ActionResult GetEmployeecardIssuedReport()
        {
            return View();         
        }

          public string GetEmployeecardIssuedReportData()
          {
              Util.CommonClass CC = new Util.CommonClass();
              DataTable dt = new DataTable();
              dt = CC.GetDataTable("select * from  [vw_smx_CardIssuedReport]");
              string str = "";
              string val = "";
              str += "<table>";
              str += "<thead>";
              str += "<tr bgcolor='#CFCFCF'>";
              //for (int i = 0; i < dt.Columns.Count; i++)
              //{
              //    str += "<td style='width:100px'><b>&nbsp;&nbsp;" + dt.Columns[i].ColumnName + "&nbsp;&nbsp;</b></td>";
              //}              
              str += "<td style='width:100px'><b>&nbsp;&nbsp;Employee Id&nbsp;&nbsp;</b></td>";
              str += "<td style='width:100px'><b>&nbsp;&nbsp;Name&nbsp;&nbsp;</b></td>";
              str += "<td style='width:100px'><b>&nbsp;&nbsp;Designation&nbsp;&nbsp;</b></td>";
              str += "<td style='width:100px'><b>&nbsp;&nbsp;Department&nbsp;&nbsp;</b></td>";
              str += "<td style='width:100px'><b>&nbsp;&nbsp;Location&nbsp;&nbsp;</b></td>";              
              str += "</tr>";
              str += "</thead>";
              str += "<tbody>";
              for (int i = 0; i < dt.Rows.Count; i++)
              {
                  str += "<tr>";
                  for (int j = 0; j < dt.Columns.Count; j++)
                  {
                      val = dt.Rows[i][j].ToString();
                      str += "<td>" + val + "</td>";
                  }
                  str += "</tr>";
              }
              str += "</tbody>";
              str += "</table>";

              return str;
          }

          public ActionResult GetEmployeecardIssuedReportDataExport(string reportname, string reporttype)
          {
              Util.CommonClass CC = new Util.CommonClass();
              DataTable dt = new DataTable();
              dt = CC.GetDataTable("select * from vw_smx_CardIssuedReport");
              MemoryStream fs = new MemoryStream();
              if (reporttype.ToUpper() == "EXCEL")
              {
                  using (XLWorkbook wb = new XLWorkbook())
                  {
                      var ws = wb.Worksheets.Add("Report");
                      ws.Cell(1, 1).Value = reportname;
                      ws.Cell(1, 1).Style.Font.FontSize = 20;
                      ws.Cell(1, 1).Style.Font.Bold = true;
                      ws.Range(1, 1, 1, 10).Merge().AddToNamed("Titles");


                      ws.Cell(2, 1).Value = "Total Number of Records : " + dt.Rows.Count.ToString();
                      ws.Cell(2, 1).Style.Font.FontSize = 16;
                      ws.Cell(2, 1).Style.Font.Bold = true;
                      ws.Range(2, 1, 2, 10).Merge().AddToNamed("NoOfRecords");

                      for (int i = 1; i <= dt.Columns.Count; i++)
                      {
                          ws.Cell(4, i).Value = dt.Columns[i - 1].Caption;
                          ws.Cell(4, i).Style.Font.Bold = true;
                          ws.Cell(4, i).Style.Fill.SetBackgroundColor(XLColor.Yellow);
                      }
                      var rangeWithData = ws.Cell(5, 1).InsertData(dt.AsEnumerable());
                      wb.SaveAs(fs);
                  }
              }

              if (reporttype.ToUpper() == "PDF")
              {
                  fs = Util.Utility.ExportToPdf(dt, reportname + " \r\n " + "Total Number of Records : " + dt.Rows.Count.ToString() + "\r\n");
              }

              ViewData["FileName"] = reportname;
              ViewData["FileType"] = reporttype;
              ViewData["FileContent"] = fs;

              return View();
          }
        
        //-------------- Employee Card Not Issued Report ---------------------

          public ActionResult GetEmployeecardNotIssuedReport()
          {
              return View();
          }

          public string GetEmployeecardNotIssuedReportData()
          {
              Util.CommonClass CC = new Util.CommonClass();
              DataTable dt = new DataTable();
              dt = CC.GetDataTable("select * from  [vw_smx_CardNotIssuedReport]");
              string str = "";
              string val = "";
              str += "<table>";
              str += "<thead>";
              str += "<tr bgcolor='#CFCFCF'>";
              //for (int i = 0; i < dt.Columns.Count; i++)
              //{
              //    str += "<td style='width:100px'><b>&nbsp;&nbsp;" + dt.Columns[i].ColumnName + "&nbsp;&nbsp;</b></td>";
              //}
              //str += "<td style='width:100px'><b>&nbsp;&nbsp;Card Number&nbsp;&nbsp;</b></td>";
              str += "<td style='width:100px'><b>&nbsp;&nbsp;Employee Id&nbsp;&nbsp;</b></td>";
              str += "<td style='width:100px'><b>&nbsp;&nbsp;Name&nbsp;&nbsp;</b></td>";
              str += "<td style='width:100px'><b>&nbsp;&nbsp;Designation&nbsp;&nbsp;</b></td>";
              str += "<td style='width:100px'><b>&nbsp;&nbsp;Department&nbsp;&nbsp;</b></td>";
              str += "<td style='width:100px'><b>&nbsp;&nbsp;Location&nbsp;&nbsp;</b></td>";              
              str += "</tr>";
              str += "</thead>";
              str += "<tbody>";
              for (int i = 0; i < dt.Rows.Count; i++)
              {
                  str += "<tr>";
                  for (int j = 0; j < dt.Columns.Count; j++)
                  {
                      val = dt.Rows[i][j].ToString();
                      str += "<td>" + val + "</td>";
                  }
                  str += "</tr>";
              }
              str += "</tbody>";
              str += "</table>";

              return str;
          }

          public ActionResult GetEmployeecardNotIssuedReportDataExport(string reportname, string reporttype)
          {
              Util.CommonClass CC = new Util.CommonClass();
              DataTable dt = new DataTable();
              dt = CC.GetDataTable("select * from vw_smx_CardnotIssuedReport");
              MemoryStream fs = new MemoryStream();
              if (reporttype.ToUpper() == "EXCEL")
              {
                  using (XLWorkbook wb = new XLWorkbook())
                  {
                      var ws = wb.Worksheets.Add("Report");
                      ws.Cell(1, 1).Value = reportname;
                      ws.Cell(1, 1).Style.Font.FontSize = 20;
                      ws.Cell(1, 1).Style.Font.Bold = true;
                      ws.Range(1, 1, 1, 10).Merge().AddToNamed("Titles");


                      ws.Cell(2, 1).Value = "Total Number of Records : " + dt.Rows.Count.ToString();
                      ws.Cell(2, 1).Style.Font.FontSize = 16;
                      ws.Cell(2, 1).Style.Font.Bold = true;
                      ws.Range(2, 1, 2, 10).Merge().AddToNamed("NoOfRecords");

                      for (int i = 1; i <= dt.Columns.Count; i++)
                      {
                          ws.Cell(4, i).Value = dt.Columns[i - 1].Caption;
                          ws.Cell(4, i).Style.Font.Bold = true;
                          ws.Cell(4, i).Style.Fill.SetBackgroundColor(XLColor.Yellow);
                      }
                      var rangeWithData = ws.Cell(5, 1).InsertData(dt.AsEnumerable());
                      wb.SaveAs(fs);
                  }
              }

              if (reporttype.ToUpper() == "PDF")
              {
                  fs = Util.Utility.ExportToPdf(dt, reportname + " \r\n " + "Total Number of Records : " + dt.Rows.Count.ToString() + "\r\n");
              }

              ViewData["FileName"] = reportname;
              ViewData["FileType"] = reporttype;
              ViewData["FileContent"] = fs;

              return View();
          }

        //-----------------------------------------------------------------------------
        //----------------- All new reports end here ----------------------------------
        //-----------------------------------------------------------------------------

	}
}