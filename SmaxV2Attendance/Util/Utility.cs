using iTextSharp.text;
using iTextSharp.text.pdf;
using SmaxV2Attendance.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using System.Collections;

namespace SmaxV2Attendance.Util
{
    public class Utility
    {
        public static byte[] HexStringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        public static string ByteArrayToHexString(byte[] ByteArray)
        {
            return BitConverter.ToString(ByteArray);
        }

        public static bool _TryPing(string strIpAddress, int intPort, int nTimeoutMsec)
        {
            Socket socket = null;
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.DontLinger, false);


                IAsyncResult result = socket.BeginConnect(strIpAddress, intPort, null, null);
                bool success = result.AsyncWaitHandle.WaitOne(nTimeoutMsec, true);

                return socket.Connected;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (null != socket)
                    socket.Close();
            }
        }

        public static bool TestConnection(string ipAddress, int Port, TimeSpan waitTimeSpan)
        {
            using (TcpClient tcpClient = new TcpClient())
            {
                IAsyncResult result = tcpClient.BeginConnect(ipAddress, Port, null, null);
                WaitHandle timeoutHandler = result.AsyncWaitHandle;
                try
                {
                    if (!result.AsyncWaitHandle.WaitOne(waitTimeSpan, false))
                    {
                        tcpClient.Close();
                        return false;
                    }

                    tcpClient.EndConnect(result);
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    timeoutHandler.Close();
                }
                return true;
            }
        }

        public static SelectList GetReaderTypeDDL()
        {
            var list = new SelectList(new[] 
            {
                new { ID = "", Name = "Select Reader Type" },
                new { ID = "1", Name = "IN" },
                new { ID = "2", Name = "OUT" },
                new { ID = "3", Name = "None" },
            },
            "ID", "Name", "0");

            return list;
        }


        public static SelectList GetReaderModeDDL()
        {
            var list = new SelectList(new[] 
            {
                new { ID = "", Name = "Select Reader Mode" },
                new { ID = "1", Name = "Attendance" },
                new { ID = "2", Name = "Access" },                
            },
            "ID", "Name", "0");

            return list;
        }

        public static SelectList GetReaderTypeDDL(string SelectedValue)
        {
            var list = new SelectList(new[] 
            {
                new { ID = "", Name = "Select Reader Type" },
                new { ID = "1", Name = "IN" },
                new { ID = "2", Name = "OUT" },
                new { ID = "3", Name = "None" },
            },
            "ID", "Name", SelectedValue);

            return list;
        }

        public static SelectList GetReaderModeDDL(string SelectedValue)
        {
            var list = new SelectList(new[] 
            {
                new { ID = "", Name = "Select Reader Mode" },
                new { ID = "1", Name = "Attendance" },
                new { ID = "2", Name = "Access" },                
            },
            "ID", "Name", SelectedValue);

            return list;
        }

        public static SelectList GetInputTypeNONCDDL()
        {
            var list = new SelectList(new[] 
            {
                new { ID = "0", Name = "Normally Open" },
                new { ID = "1", Name = "Normally Close" },
            },
            "ID", "Name", "0");

            return list;
        }

        public static SelectList GetInputTypeNONCDDL(string SelectedValue)
        {
            var list = new SelectList(new[] 
            {
                new { ID = "0", Name = "Normally Open" },
                new { ID = "1", Name = "Normally Close" },
            },
            "ID", "Name", SelectedValue);

            return list;
        }


        public static SelectList GetMemoryTypeDDL()
        {
            var list = new SelectList(new[] 
            {
                new { ID = "-1", Name = "Select Memory Type" },
                new { ID = "0", Name = "Full" },
                new { ID = "1", Name = "Overwrite" },
            },
            "ID", "Name", "-1");

            return list;
        }

        public static SelectList GetMemoryTypeDDL(string SelectedValue)
        {
            var list = new SelectList(new[] 
            {
                new { ID = "0", Name = "Full" },
                new { ID = "1", Name = "Overwrite" },
            },
            "ID", "Name", SelectedValue);

            return list;
        }

        public static SelectList GetMessageDDL()
        {
            var list = new SelectList(new[] 
            {
                new { ID = "0", Name = "Select a Message" },
                new { ID = "1", Name = "CardAccepted" },
                new { ID = "2", Name = "CardDisallowed" },
                new { ID = "4", Name = "InvalidShift" },
                new { ID = "64", Name = "CustomMessage" }
            },
            "ID", "Name", "0");

            return list;
        }

        public static SelectList GetTimeZoneList()
        {
            SMAXV2Entities db = new SMAXV2Entities();
            return new SelectList(db.Smx_TimeZone, "TZ_ID", "TZ_NAME");
        }

        public static SelectList GetTimeZoneListwithDefault()
        {
            SMAXV2Entities db = new SMAXV2Entities();

            List<Smx_TimeZone> smx_timezone = db.Smx_TimeZone.ToList();
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "Select >>", Value = "0" });
            foreach (Smx_TimeZone item in smx_timezone)
            {
                list.Add(new SelectListItem { Text = item.TZ_NAME, Value = item.TZ_ID.ToString() });

            }
            SelectList lst = new SelectList(list);

            return new SelectList(lst);
        }

        public static SelectList GetDepartment()
        {
            SMAXV2Entities db = new SMAXV2Entities();
            return new SelectList(db.Smx_Department, "DP_ID", "DP_NAME");
        }

        public static SelectList GetDesignation()
        {
            SMAXV2Entities db = new SMAXV2Entities();
            return new SelectList(db.Smx_Designation, "DN_ID", "DN_NAME");
        }

        public static SelectList GetAccessLevelList()
        {
            SMAXV2Entities db = new SMAXV2Entities();
            return new SelectList(db.Smx_AccessLevel, "AL_ID", "AL_NAME");
        }

        public static SelectList GetDeviceList()
        {
            SMAXV2Entities db = new SMAXV2Entities();
            SelectList smx_devices;
            using(db)
            {
                 smx_devices = new SelectList( db.Smx_Devices.Select(e => new
                {
                    e.DE_NAME,
                    e.DE_LN_ID,
                    e.Smx_Location.LN_NAME,
                    e.DE_IPADDRESS,
                    Reader = e.DE_IPADDRESS + "|" + e.DE_NAME + "|" + e.Smx_Location.LN_NAME + "|" + e.DE_LN_ID
                }).ToList(), "DE_IPADDRESS", "Reader");
            }
            return smx_devices;
        }

        public static SelectList GetInReaders()
        {
            SMAXV2Entities db = new SMAXV2Entities();
            SelectList smx_devices;
            using (db)
            {
                smx_devices = new SelectList(db.Smx_Devices.Where(x=>x.DE_READERTYPE == "1").Select(e => new
                {
                    e.DE_NAME,
                    e.DE_LN_ID,
                    e.Smx_Location.LN_NAME,
                    e.DE_IPADDRESS,
                    Reader = e.DE_IPADDRESS + "|" + e.DE_NAME + "|" + e.Smx_Location.LN_NAME + "|" + e.DE_LN_ID
                }).ToList(), "DE_IPADDRESS", "Reader");
            }
            return smx_devices;
        }

        public static SelectList GetOutReaders()
        {
            SMAXV2Entities db = new SMAXV2Entities();
            SelectList smx_devices;
            using (db)
            {
                smx_devices = new SelectList(db.Smx_Devices.Where(x => x.DE_READERTYPE == "2").Select(e => new
                {
                    e.DE_NAME,
                    e.DE_LN_ID,
                    e.Smx_Location.LN_NAME,
                    e.DE_IPADDRESS,
                    Reader = e.DE_IPADDRESS + "|" + e.DE_NAME + "|" + e.Smx_Location.LN_NAME + "|" + e.DE_LN_ID
                }).ToList(), "DE_IPADDRESS", "Reader");
            }
            return smx_devices;
        }


        public static SelectList GetAccessLevelDetailsList()
        {
            SMAXV2Entities db = new SMAXV2Entities();
            SelectList smx_AccessLevelDetails;
            using (db)
            {
                smx_AccessLevelDetails = new SelectList(db.Smx_AccessLevelDetails.Select(e => new
                {
                    e.ALD_READER_IPADDRESS,
                    e.ALD_TZ_ID,
                    e.Smx_TimeZone.TZ_NAME,
                    e.ALD_LN_ID,
                    e.Smx_Location.LN_NAME,
                }).ToList());
            }
            return smx_AccessLevelDetails;
        }

        public static Array GetAccessLevelDetailsList(decimal AL_ID)
        {
            SMAXV2Entities db = new SMAXV2Entities();
            Array smx_AccessLevelDetails;
            using (db)
            {
                smx_AccessLevelDetails = db.Smx_AccessLevelDetails
                                        .Join(db.Smx_Devices,
                                        c => c.ALD_READER_IPADDRESS,
                                        o => o.DE_IPADDRESS,
                                        (c, o) => new
                                            {
                                                ALD_AL_ID = c.ALD_AL_ID,
                                                ALD_READER_IPADDRESS = c.ALD_READER_IPADDRESS,
                                                ALD_TZ_ID = c.ALD_TZ_ID,
                                                TZ_NAME = c.Smx_TimeZone.TZ_NAME,
                                                ALD_LN_ID = c.ALD_LN_ID,
                                                LN_NAME = c.Smx_Location.LN_NAME,
                                                DE_NAME = o.DE_NAME
                                            }
                                        ).Where(x => x.ALD_AL_ID.Equals(AL_ID)).ToArray();
                    
            }
            return smx_AccessLevelDetails;
        }

        public static Array GetLocation()
        {
            SMAXV2Entities db = new SMAXV2Entities();
            Array smx_location;
            using (db)
            {
                smx_location = db.Smx_Location.Select(e => new
                {
                  e.LN_ID,
                  e.LN_NAME,
                }).ToArray();
            }
            return smx_location;
        }

        public static Array GetLocation(int ln_id)
        {
            SMAXV2Entities db = new SMAXV2Entities();
            Array smx_location;
            using (db)
            {
                smx_location = db.Smx_Location.Select(e => new
                {
                    e.LN_ID,
                    e.LN_NAME
                }).Where(e => e.LN_ID == ln_id).ToArray();
            }
            return smx_location;
        }

        public static Array GetTransactiontypes()
        {
            SMAXV2Entities db = new SMAXV2Entities();
            Array smx_transactiontype;
            using (db)
            {
                smx_transactiontype = db.Smx_TransactionType.Select(e => new
                {
                    e.TT_CODE,
                    e.TT_DESCRIPTION
                }).OrderBy(e => e.TT_DESCRIPTION).ToArray();
            }
            return smx_transactiontype;
        }

        public static Array GetAccesslevel()
        {
            SMAXV2Entities db = new SMAXV2Entities();
            Array smx_accesslevels;
            using (db)
            {
                smx_accesslevels = db.Smx_AccessLevel.Select(e => new
                {
                    e.AL_ID,
                    e.AL_NAME
                }).OrderBy(e=>e.AL_NAME).ToArray();
            }
            return smx_accesslevels;
        }

        public static Array GetCardStatus()
        {
            SMAXV2Entities db = new SMAXV2Entities();
            Array smx_cardstatus;
            using (db)
            {
                smx_cardstatus = db.Smx_CardStatus.Select(e => new
                {
                    e.CS_Id,
                    e.CS_Name
                }).OrderBy(e => e.CS_Name).ToArray();
            }
            return smx_cardstatus;
        }

        public static Array GetDevice()
        {
            SMAXV2Entities db = new SMAXV2Entities();
            Array smx_devices;
            
            using (db)
            {
                smx_devices = db.Smx_Devices.Join(db.Smx_AttendanceReaders, e => e.DE_IPADDRESS, f => f.AR_IPAddress,
                    (e, f) => new { e, f }).Where(m => m.e.DE_OPERATIONAL == "true").
                    Select(m => new
                    {
                        m.e.DE_ID,
                        m.e.DE_NAME,
                        m.e.DE_LN_ID,
                        m.e.Smx_Location.LN_NAME,
                        m.e.DE_IPADDRESS,
                        m.e.DE_OPERATIONAL
                    }).ToArray();
            }
            return smx_devices;
        }

        public static Array GetNoflashCount()
        {
            SMAXV2Entities db = new SMAXV2Entities();
            Array smx_lastflashcount;
            using (db)
            {
                smx_lastflashcount = db.Smx_LastFlashdayscount.Select(e => new
                {
                    e.ID,
                    e.Dayscount
                }).ToArray();
            }
            return smx_lastflashcount;
        }

        public static Array GetDevice(int ln_id)
        {
            SMAXV2Entities db = new SMAXV2Entities();
            Array smx_devices;
            using (db)
            {
                smx_devices = db.Smx_Devices.Select(e => new
                {
                    e.DE_ID,
                    e.DE_NAME,
                    e.DE_LN_ID,
                    e.Smx_Location.LN_NAME,
                    e.DE_IPADDRESS,
                    e.DE_OPERATIONAL
                }).Where(e => e.DE_LN_ID == ln_id && e.DE_OPERATIONAL == "true").ToArray();
            }
            return smx_devices;
        }

        public static Array GetUnit()
        {
            SMAXV2Entities db = new SMAXV2Entities();
            Array smx_unit;
            using (db)
            {
                smx_unit = db.Smx_Unit.Select(e => new
                {
                    e.UT_ID,
                    e.UT_NAME
                }).ToArray();
            }
            return smx_unit;
        }

        public static List<Smx_Message> GetMessage()
        {
            SMAXV2Entities db = new SMAXV2Entities();
            List<Smx_Message> smx_message;
            using (db)
            {
                smx_message = db.Smx_Message.ToList();
            }
            return smx_message;
        }

        public static DateTime[] GetHoliday()
        {
            SMAXV2Entities db = new SMAXV2Entities();
            using(db)
            {
                List<Smx_Holiday> smx_holiday = db.Smx_Holiday.ToList();
                DateTime[] dt = new DateTime[smx_holiday.Count];
                int i = 0;
                foreach (Smx_Holiday holiday in smx_holiday)
                {
                    dt[i] = holiday.HD_DATE;
                    i++;
                }
                return dt;
            }
            
        }

        public static void GetTimeZoneDetails(int tz_id, ref int[] day, ref string[] starttime, ref string[] endtime)
        {
            SMAXV2Entities db = new SMAXV2Entities();
            using(db)
            {
                List<Smx_TimeZoneDetails> timezonedetails = db.Smx_TimeZoneDetails.Where(e=>e.TZD_TZ_ID==tz_id).ToList();
                int i = 0;
                Array.Resize(ref  day, day.Length + timezonedetails.Count);
                Array.Resize(ref  starttime, starttime.Length + timezonedetails.Count);
                Array.Resize(ref  endtime, endtime.Length + timezonedetails.Count);
                foreach(var item in timezonedetails)
                {
                    day[i] = Convert.ToInt16(item.TZD_DAYS);
                    starttime[i] = item.TZD_START_TIME;
                    endtime[i] = item.TZD_END_TIME;
                    i += 1;
                }
                
            }
        }


        public static List<vw_Smx_Transaction> GetLiveTransactionData(int ttype,int ln_id)
        {
            SMAXV2Entities db = new SMAXV2Entities();
            db.Database.CommandTimeout = 0;
            List<vw_Smx_Transaction> smx_livetransaction = new List<vw_Smx_Transaction>();
            using (db)
            {
                DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                if(ttype == 0 && ln_id <= 0 )
                {
                    
                    //smx_livetransaction = db.vw_Smx_Transaction.AsNoTracking().OrderByDescending(x => x.Time).ToList();
                    smx_livetransaction = db.vw_Smx_Transaction.AsNoTracking().Where(x=>x.Date_Time >= dt).OrderByDescending(x=>x.Date_Time).Take(500).ToList();
                }
                else if (ttype != 0 && ln_id <= 0)
                {
                    //smx_livetransaction = db.vw_Smx_Transaction.AsNoTracking().Where(x => x.Tr_TType == ttype).OrderByDescending(x => x.Time).ToList();
                    smx_livetransaction = db.vw_Smx_Transaction.AsNoTracking().Where(x => x.Tr_TType == ttype && x.Date_Time >= dt).OrderByDescending(x => x.Date_Time).Take(500).ToList();
                }
                else if (ttype != 0 && ln_id > 0)
                {
                    //smx_livetransaction = db.vw_Smx_Transaction.AsNoTracking().Where(x => x.Tr_TType == ttype && x.DE_LN_ID == ln_id).OrderByDescending(x => x.Time).ToList();
                    smx_livetransaction = db.vw_Smx_Transaction.AsNoTracking().Where(x => x.Tr_TType == ttype && x.DE_LN_ID == ln_id && x.Date_Time >= dt).OrderByDescending(x => x.Date_Time).Take(500).ToList();
                }
                else if (ttype == 0 && ln_id > 0)
                {
                    //smx_livetransaction = db.vw_Smx_Transaction.AsNoTracking().Where(x => x.DE_LN_ID == ln_id).OrderByDescending(x => x.Time).ToList();
                    smx_livetransaction = db.vw_Smx_Transaction.AsNoTracking().Where(x => x.DE_LN_ID == ln_id && x.Date_Time >= dt).OrderByDescending(x => x.Date_Time).Take(500).ToList();
                }
            }
            return smx_livetransaction;
        }

        public static void ExportExcel(DataTable DtToExcel, string ReportName)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(DtToExcel, "Report");
                string path = System.Web.HttpContext.Current.Server.MapPath(".") + "\\Export\\" + ReportName + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + ".xlsx";
                MemoryStream fs = new MemoryStream();
                wb.SaveAs(fs);
                //System.Web.HttpContext.Current.Response.Redirect(".//GetReports//Export//" + ReportName + "_"  + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + ".xlsx");
                //System.Web.HttpContext.Current.Response.Clear();
                //System.Web.HttpContext.Current.Response.ContentType = "MS-Excel/xls";
                //System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(path));
                //System.Web.HttpContext.Current.Response.TransmitFile(path);
                //System.Web.HttpContext.Current.Response.WriteFile(path);
                //System.IO.File.Delete(path);

                //string myName = System.Web.HttpContext.Current.Server.UrlEncode(ReportName + "_" + DateTime.Now.ToShortDateString() + ".xlsx");
                //MemoryStream stream = GetStream(ExcelWorkbook);

                System.Web.HttpContext.Current.Response.Clear();
                byte[] fileData = System.Text.ASCIIEncoding.ASCII.GetBytes("<Table><tr><td>ID</td><td>Emp</td></tr></Table>");
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=MonthlyAttendanceReport.csv");
                HttpContext.Current.Response.ContentType = "application/CSV";
                HttpContext.Current.Response.Write("Hello");
                HttpContext.Current.Response.End();
                //System.Web.HttpContext.Current.Response.Buffer = true;
                //System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + ReportName + ".xlsx");
                //System.Web.HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                //System.Web.HttpContext.Current.Response.Write(fs.GetBuffer());
                //System.Web.HttpContext.Current.Response.Flush();
                //System.Web.HttpContext.Current.Response.OutputStream.Close();
                //HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                //System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=MonthlyAttendanceReport.xlsx");

                //HttpContext.Current.Response.BinaryWrite(fs.GetBuffer());
                //HttpContext.Current.Response.End();

                wb.Dispose();
                GC.Collect();
                GC.SuppressFinalize(wb);
                //HttpContext.Current.ApplicationInstance.CompleteRequest();

                //System.Web.HttpContext.Current.Response.End();
            }
        }

        public Stream GetStream(XLWorkbook excelWorkbook)
        {
            Stream fs = new MemoryStream();
            excelWorkbook.SaveAs(fs);
            fs.Position = 0;
            return fs;
        }

        public static MemoryStream ExportToPdf(DataTable dt)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                pdfDoc.Open();
                iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 5);

                PdfPTable table = new PdfPTable(dt.Columns.Count);
                PdfPRow row = null;
                table.WidthPercentage = 100;
                int iCol = 0;
                string colname = "";
                PdfPCell cell = new PdfPCell(new Phrase("Report"));

                cell.Colspan = dt.Columns.Count;

                foreach (DataColumn c in dt.Columns)
                {

                    table.AddCell(new Phrase(c.ColumnName, font5));
                }

                foreach (DataRow r in dt.Rows)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dt.Columns.Count - 1; i++)
                        {
                            table.AddCell(new Phrase(r[i].ToString(), font5));
                        }
                    }
                }
                pdfDoc.Add(table);
                pdfDoc.Close();
                return memoryStream;
            }

        }

        public static MemoryStream ExportToPdf(DataTable dt, string pageheading)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document pdfDoc = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                writer.PageEvent = new Util.PdfEvents.ITextEvents();
                pdfDoc.Open();
                iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 5);

                //PdfPTable table1 = new PdfPTable(1);
                //PdfPRow row1 = null;
                //table1.WidthPercentage = 100;
                //table1.AddCell("This is a Report");
                Phrase str = new Phrase("\r\n " + pageheading + " \r\n");
                pdfDoc.Add(str);


                PdfPTable table = new PdfPTable(dt.Columns.Count);
                PdfPRow row = null;
                table.WidthPercentage = 100;
                int iCol = 0;
                string colname = "";
                PdfPCell cell = new PdfPCell(new Phrase("Report"));

                cell.Colspan = dt.Columns.Count;

                foreach (DataColumn c in dt.Columns)
                {

                    table.AddCell(new Phrase(c.ColumnName, font5));
                }

                foreach (DataRow r in dt.Rows)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dt.Columns.Count - 1; i++)
                        {
                            table.AddCell(new Phrase(r[i].ToString(), font5));
                        }
                    }
                }
                pdfDoc.Add(table);
                pdfDoc.Close();
                return memoryStream;
            }

        }

        public static DataTable ConvertHTMLTablesToDataSet(string HTML)
        {
            // Declarations    
            DataSet ds = new DataSet();
            DataTable dt = null;
            DataRow dr = null;
            string TableExpression = "<table[^>]*>(.*?)</table>";
            string HeaderExpression = "<th[^>]*>(.*?)</th>";
            string RowExpression = "<tr[^>]*>(.*?)</tr>";
            string ColumnExpression = "<td[^>]*>(.*?)</td>";
            bool HeadersExist = false;
            int iCurrentColumn = 0;
            int iCurrentRow = 0;

            // Get a match for all the tables in the HTML    
            MatchCollection Tables = Regex.Matches(HTML, TableExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);

            // Loop through each table element    
            foreach (Match Table in Tables)
            {

                // Reset the current row counter and the header flag    
                iCurrentRow = 0;
                HeadersExist = false;

                // Add a new table to the DataSet    
                dt = new DataTable();

                // Create the relevant amount of columns for this table (use the headers if they exist, otherwise use default names)    
                if (Table.Value.Contains("<th"))
                {
                    // Set the HeadersExist flag    
                    HeadersExist = true;

                    // Get a match for all the rows in the table    
                    MatchCollection Headers = Regex.Matches(Table.Value, HeaderExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);

                    // Loop through each header element    
                    foreach (Match Header in Headers)
                    {
                        if (!dt.Columns.Contains(Header.Groups[1].ToString()))
                            dt.Columns.Add(Header.Groups[1].ToString().Replace("&nbsp;", "").Replace("<tr><th>",""));
                    }
                }
                else
                {
                    for (int iColumns = 1; iColumns <= Regex.Matches(Regex.Matches(Regex.Matches(Table.Value, TableExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase).ToString(), RowExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase).ToString(), ColumnExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase).Count; iColumns++)
                    {
                        dt.Columns.Add("Column " + iColumns);
                    }
                }

                // Get a match for all the rows in the table    
                MatchCollection Rows = Regex.Matches(Table.Value, RowExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);

                // Loop through each row element    
                foreach (Match Row in Rows)
                {

                    // Only loop through the row if it isn't a header row    
                    if (!(iCurrentRow == 0 & HeadersExist == true))
                    {

                        // Create a new row and reset the current column counter    
                        dr = dt.NewRow();
                        iCurrentColumn = 0;

                        // Get a match for all the columns in the row    
                        MatchCollection Columns = Regex.Matches(Row.Value, ColumnExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);

                        // Loop through each column element    
                        foreach (Match Column in Columns)
                        {
                            if (Columns.Count != iCurrentColumn)
                                // Add the value to the DataRow    
                                dr[iCurrentColumn] = Convert.ToString(Column.Groups[1]).Replace("&nbsp;", "");

                            // Increase the current column     
                            iCurrentColumn += 1;
                        }

                        // Add the DataRow to the DataTable    
                        dt.Rows.Add(dr);

                    }

                    // Increase the current row counter    
                    iCurrentRow += 1;
                }

                // Add the DataTable to the DataSet    
                ds.Tables.Add(dt);

            }

            return ds.Tables[0];

        }

        private static void WriteLog(string str)
        {
            try
            {
                string Folder = System.Web.HttpContext.Current.Server.MapPath(".") + "\\Log\\";
                string FileName = Folder + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + ".txt";
                if (!File.Exists(FileName))
                {
                    FileStream fs = File.Create(FileName);
                    fs.Close();
                    fs = null;
                }
                StreamWriter sw = new StreamWriter(FileName, true);
                sw.WriteLine(DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString() + " ==> " + str);
                sw.Close();
                sw = null;
            }
            catch (Exception ex)
            {

            }
        }
    }
}