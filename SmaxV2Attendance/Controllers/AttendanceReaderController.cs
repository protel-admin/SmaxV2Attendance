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
    public class AttendanceReaderController : Controller
    {
        //
        // GET: /AttendanceReader/        
        private SMAXV2Entities db = new SMAXV2Entities();

        public ActionResult Index()
        {

            try
            {

                var nonedevices = from dv in db.Smx_Devices
                                where !(from ar in db.Smx_AttendanceReaders
                                        select ar.AR_IPAddress).Contains(dv.DE_IPADDRESS)
                                where dv.DE_READERTYPE.ToString() == "3"
                                select new
                                {
                                    NONE_ID = dv.DE_ID,
                                    NONE_NAME = dv.DE_NAME
                                };

                ViewBag.NONE_DE_ID = new SelectList(nonedevices, "NONE_ID", "NONE_NAME");
                

                var indevices = from dv in db.Smx_Devices
                                where !(from ar in db.Smx_AttendanceReaders
                                        select ar.AR_IPAddress).Contains(dv.DE_IPADDRESS)
                                where dv.DE_READERTYPE.ToString() == "1"
                                select new
                                {
                                    IN_ID = dv.DE_ID,
                                    IN_NAME = dv.DE_NAME
                                };



                ViewBag.IN_DE_ID = new SelectList(indevices, "IN_ID", "IN_NAME");

                var outdevices = from dv in db.Smx_Devices
                                where !(from ar in db.Smx_AttendanceReaders
                                        select ar.AR_IPAddress).Contains(dv.DE_IPADDRESS)
                                where dv.DE_READERTYPE.ToString() == "2"
                                select new
                                {
                                    OUT_ID = dv.DE_ID,
                                    OUT_NAME = dv.DE_NAME
                                };


                ViewBag.OUT_DE_ID = new SelectList(outdevices, "OUT_ID", "OUT_NAME");

                var iononedevices = from dv in db.Smx_Devices
                                  where !(from ar in db.Smx_AttendanceReaders
                                          select ar.AR_IPAddress).Contains(dv.DE_IPADDRESS)
                                  where dv.DE_READERTYPE.ToString() == "4"
                                  select new
                                  {
                                      IO_NONE_ID = dv.DE_ID,
                                      IO_NONE_NAME = dv.DE_NAME
                                  };

                ViewBag.IO_NONE_DE_ID = new SelectList(iononedevices, "IO_NONE_ID", "IO_NONE_NAME");


                var arnonereaders = from c in db.Smx_AttendanceReaders
                                    join d in db.Smx_Devices on c.AR_IPAddress equals d.DE_IPADDRESS
                                    where c.AR_ReaderType.ToString() == "3"
                                    select new
                                    {

                                        AR_NONE_ID = d.DE_ID,
                                        AR_NONE_NAME = d.DE_NAME
                                    };

                ViewBag.NONE_AR_ID = new SelectList(arnonereaders, "AR_NONE_ID", "AR_NONE_NAME");

                var arinreaders = from c in db.Smx_AttendanceReaders
                                  join d in db.Smx_Devices on c.AR_IPAddress equals d.DE_IPADDRESS
                                  where c.AR_ReaderType.ToString() == "1"
                                  select new
                                  {

                                      AR_IN_ID = d.DE_ID,
                                      AR_IN_NAME = d.DE_NAME
                                  };

                ViewBag.IN_AR_ID = new SelectList(arinreaders, "AR_IN_ID", "AR_IN_NAME");
                                           

              

                var aroutreaders = from c in db.Smx_AttendanceReaders
                                   join d in db.Smx_Devices on c.AR_IPAddress equals d.DE_IPADDRESS
                                   where c.AR_ReaderType.ToString() == "2"
                                   select new
                                   {

                                       AR_OUT_ID = d.DE_ID,
                                       AR_OUT_NAME = d.DE_NAME
                                   };

                ViewBag.OUT_AR_ID = new SelectList(aroutreaders, "AR_OUT_ID", "AR_OUT_NAME");

                var ioarnonereaders = from c in db.Smx_AttendanceReaders
                                    join d in db.Smx_Devices on c.AR_IPAddress equals d.DE_IPADDRESS
                                    where c.AR_ReaderType.ToString() == "4"
                                    select new
                                    {

                                        IO_AR_NONE_ID = d.DE_ID,
                                        IO_AR_NONE_NAME = d.DE_NAME
                                    };

                ViewBag.IO_NONE_AR_ID = new SelectList(ioarnonereaders, "IO_AR_NONE_ID", "IO_AR_NONE_NAME");

            }
            catch(Exception e)
            {

                throw e;

            }
            return View();
        }

        public ActionResult AttendanceReaders()
        {

            try
            {

                var nonedevices = from dv in db.Smx_Devices
                                  where !(from ar in db.Smx_AttendanceReaders
                                          select ar.AR_IPAddress).Contains(dv.DE_IPADDRESS)
                                  where dv.DE_READERTYPE.ToString() == "3"
                                  select new
                                  {
                                      NONE_ID = dv.DE_ID,
                                      NONE_NAME = dv.DE_NAME
                                  };

                ViewBag.NONE_DE_ID = new SelectList(nonedevices, "NONE_ID", "NONE_NAME");


                var indevices = from dv in db.Smx_Devices
                                where !(from ar in db.Smx_AttendanceReaders
                                        select ar.AR_IPAddress).Contains(dv.DE_IPADDRESS)
                                where dv.DE_READERTYPE.ToString() == "1"
                                select new
                                {
                                    IN_ID = dv.DE_ID,
                                    IN_NAME = dv.DE_NAME
                                };



                ViewBag.IN_DE_ID = new SelectList(indevices, "IN_ID", "IN_NAME");

                var outdevices = from dv in db.Smx_Devices
                                 where !(from ar in db.Smx_AttendanceReaders
                                         select ar.AR_IPAddress).Contains(dv.DE_IPADDRESS)
                                 where dv.DE_READERTYPE.ToString() == "2"
                                 select new
                                 {
                                     OUT_ID = dv.DE_ID,
                                     OUT_NAME = dv.DE_NAME
                                 };


                ViewBag.OUT_DE_ID = new SelectList(outdevices, "OUT_ID", "OUT_NAME");

                var iononedevices = from dv in db.Smx_Devices
                                    where !(from ar in db.Smx_AttendanceReaders
                                            select ar.AR_IPAddress).Contains(dv.DE_IPADDRESS)
                                    where dv.DE_READERTYPE.ToString() == "4"
                                    select new
                                    {
                                        IO_NONE_ID = dv.DE_ID,
                                        IO_NONE_NAME = dv.DE_NAME
                                    };

                ViewBag.IO_NONE_DE_ID = new SelectList(iononedevices, "IO_NONE_ID", "IO_NONE_NAME");

                var arnonereaders = from c in db.Smx_AttendanceReaders
                                    join d in db.Smx_Devices on c.AR_IPAddress equals d.DE_IPADDRESS
                                    where c.AR_ReaderType.ToString() == "3"
                                    select new
                                    {

                                        AR_NONE_ID = d.DE_ID,
                                        AR_NONE_NAME = d.DE_NAME
                                    };

                ViewBag.NONE_AR_ID = new SelectList(arnonereaders, "AR_NONE_ID", "AR_NONE_NAME");

                var arinreaders = from c in db.Smx_AttendanceReaders
                                  join d in db.Smx_Devices on c.AR_IPAddress equals d.DE_IPADDRESS
                                  where c.AR_ReaderType.ToString() == "1"
                                  select new
                                  {

                                      AR_IN_ID = d.DE_ID,
                                      AR_IN_NAME = d.DE_NAME
                                  };

                ViewBag.IN_AR_ID = new SelectList(arinreaders, "AR_IN_ID", "AR_IN_NAME");




                var aroutreaders = from c in db.Smx_AttendanceReaders
                                   join d in db.Smx_Devices on c.AR_IPAddress equals d.DE_IPADDRESS
                                   where c.AR_ReaderType.ToString() == "2"
                                   select new
                                   {

                                       AR_OUT_ID = d.DE_ID,
                                       AR_OUT_NAME = d.DE_NAME
                                   };

                ViewBag.OUT_AR_ID = new SelectList(aroutreaders, "AR_OUT_ID", "AR_OUT_NAME");

                var ioarnonereaders = from c in db.Smx_AttendanceReaders
                                      join d in db.Smx_Devices on c.AR_IPAddress equals d.DE_IPADDRESS
                                      where c.AR_ReaderType.ToString() == "4"
                                      select new
                                      {

                                          IO_AR_NONE_ID = d.DE_ID,
                                          IO_AR_NONE_NAME = d.DE_NAME
                                      };

                ViewBag.IO_NONE_AR_ID = new SelectList(ioarnonereaders, "IO_AR_NONE_ID", "IO_AR_NONE_NAME");

            }
            catch (Exception e)
            {

                throw e;

            }
            return View();
        }

        public string SaveAssignedReader(string readersid)
        {

            Util.CommonClass CC = new Util.CommonClass();
            string status = "";
            object jsonstr = "";
            string IpAddr = "";
            string readerType = "";

            try
            {
                string[] readerarr = readersid.Split(',');
                
                for (int i = 0; i <= readerarr.Length - 1; i++)
                {
                    string sql = "select DE_IPADDRESS,DE_READERTYPE from Smx_Devices where DE_ID = '" + readerarr[i].Replace("''", "") + "'";
                    DataTable dt = CC.GetDataTable(sql);
                    if (dt.Rows.Count > 0)
                    {
                
                        IpAddr = dt.Rows[0]["DE_IPADDRESS"].ToString();
                        readerType = dt.Rows[0]["DE_READERTYPE"].ToString();

                    }
                    else
                    {
                        jsonstr = new { IPAddress = "", ReaderType = "" };
                    }

                    string myObjectString = jsonstr.ToString();

                    dt = CC.GetDataTable("select * from Smx_AttendanceReaders where AR_IPAddress = '" + IpAddr + "' and AR_ReaderType = '" + readerType + "'");
                    if (dt.Rows.Count == 0)
                    {
                        sql = "Insert into [Smx_AttendanceReaders] ( ";
                        sql += "[AR_IPAddress],";
                        sql += "[AR_ReaderType],";
                        sql += "[AR_Createdby],";
                        sql += "[AR_Modifiedby]";
                       

                        sql += ") values ( ";
                        
                        sql += "'" + IpAddr + "', ";                       
                        sql += "'" + readerType + "', ";               
                        sql += "'" + "Admin" + "', ";
                        sql += "'" + "Admin" + "'";
                    
                        sql += ")";
                        int cnt = CC.ExecuteSQL(sql);
                        status = "OK";
                        string Al_desc = "Create ReaderAssigned";
                        string Al_Remarks = "Created";

                        status = UpdateActionLogHistory(status, Al_desc, Al_Remarks);

                    }
                    else
                    {
                        sql = "Update [Smx_AttendanceReaders]  ";
                        sql += "set [AR_IPAddress] = '" + IpAddr + "', ";
                        sql += "[AR_ReaderType] = '" + readerType + "', ";
                        sql += "[AR_Modifiedby] = '" + "Admin" + "'";                        
                        sql += "where AR_IPAddress = '" + IpAddr + "' and AR_ReaderType = '" + readerType + "'";
                        int cnt = CC.ExecuteSQL(sql);
                        status = "OK";
                        string Al_desc = "Update Assigned Readers";
                        string Al_Remarks = "Updated";
                        status = UpdateActionLogHistory(status, Al_desc, Al_Remarks);
                    }
                }
            }
            catch (Exception ex)
            {
                status = "ERR";
            }


            return status;
        }

        public string RemoveAssignedReader(string readersid)
        {

            Util.CommonClass CC = new Util.CommonClass();
            string status = "";
            object jsonstr = "";
            string IpAddr = "";
            string readerType = "";

            try
            {
                string[] readerarr = readersid.Split(',');

                for (int i = 0; i <= readerarr.Length - 1; i++)
                {
                    string sql = "select DE_IPADDRESS,DE_READERTYPE from Smx_Devices where DE_ID = '" + readerarr[i].Replace("''", "") + "'";
                    DataTable dt = CC.GetDataTable(sql);
                    if (dt.Rows.Count > 0)
                    {

                        IpAddr = dt.Rows[0]["DE_IPADDRESS"].ToString();
                        readerType = dt.Rows[0]["DE_READERTYPE"].ToString();

                    }
                    else
                    {
                        jsonstr = new { IPAddress = "", ReaderType = "" };
                    }

                    string myObjectString = jsonstr.ToString();

                    dt = CC.GetDataTable("select * from Smx_AttendanceReaders where AR_IPAddress = '" + IpAddr + "' and AR_ReaderType = '" + readerType + "'");
                    if (dt.Rows.Count == 0)
                    {
                        //sql = "Insert into [Smx_AttendanceReaders] ( ";
                        //sql += "[AR_IPAddress],";
                        //sql += "[AR_ReaderType],";
                        //sql += "[AR_Createdby],";
                        //sql += "[AR_Modifiedby]";


                        //sql += ") values ( ";

                        //sql += "'" + IpAddr + "', ";
                        //sql += "'" + readerType + "', ";
                        //sql += "'" + "Admin" + "', ";
                        //sql += "'" + "Admin" + "'";

                        //sql += ")";
                        //int cnt = CC.ExecuteSQL(sql);
                        //status = "OK";
                        //string Al_desc = "Create ReaderAssigned";
                        //string Al_Remarks = "Created";

                        //status = UpdateActionLogHistory(status, Al_desc, Al_Remarks);

                    }
                    else
                    {
                        sql = "Delete From [Smx_AttendanceReaders]  ";                        
                        sql += "where AR_IPAddress = '" + IpAddr + "' and AR_ReaderType = '" + readerType + "'";
                        int cnt = CC.ExecuteSQL(sql);
                        status = "OK";
                        string Al_desc = "Remove Assigned Readers";
                        string Al_Remarks = "Removed";
                        status = UpdateActionLogHistory(status, Al_desc, Al_Remarks);
                    }
                }
            }
            catch (Exception ex)
            {
                status = "ERR";
            }


            return status;
        }


        public string UpdateActionLogHistory(string status, string Al_desc, string Al_Remarks)
        {
            Util.CommonClass CC = new Util.CommonClass();


            if (status == "OK")
            {
                var sql = "Insert into [Smx_ActionLogHistory] ( ";
                sql += "[ALH_Description],";
                sql += "[ALH_Remarks],";
                sql += "[ALH_Createdby]";
                sql += ") values ( ";
                sql += "'" + Al_desc + "', ";
                sql += "'" + Al_Remarks + "',";
                sql += "' Admin '";
                sql += ")";
                int cnt = CC.ExecuteSQL(sql);
                status = "OK";

            }
            return status;
        }
    }
}