using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmaxV2API;
using SmaxV2Attendance.Models;
using Newtonsoft.Json;

namespace SmaxV2Attendance.Controllers
{
    
    public class NetworkMonitorController : Controller
    {
        int udpport = 1560;
        //
        // GET: /NetworkMonitor/        
        public ActionResult Index()
        {
            return View();
        }
        
        public JsonResult Getlocation(int ln_id)
        {
            Array location;
            if(ln_id == -1)
            {
                location = Util.Utility.GetLocation();
            }
            else
            {
                location = Util.Utility.GetLocation(ln_id);
            }
            return Json(location, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDevices(int ln_id)
        {
            Array devices;
            if (ln_id == 0)
            {
                devices = Util.Utility.GetDevice();
            }
            else
            {
                devices = Util.Utility.GetDevice(ln_id);
            }
            return Json(devices, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Device Commands
        /// </summary>
        /// <param name="Reader"></param>
        /// <returns></returns>

        public JsonResult Beep(string Reader)
        {
            if (Reader.Length == 0)
            {
                ViewBag.ErrorMsg = "Select atleast one reader!";
                return Json(new { Err = "1" }, JsonRequestBehavior.AllowGet);
                //Select atleast One Location / Ip_Address
                //return View();                
            }
            else if (Reader.Substring(Reader.Length - 1, 1) == "|")
            {
                Reader = Reader.Substring(0, Reader.Length - 1);
            }
            
            string[] ReaderArr = Reader.Split('|');
            StatusMsg[] statusmsg = new StatusMsg[ReaderArr.Length];
            //StatusMsg msg = new StatusMsg();
            for (int i = 0; i <= ReaderArr.Length - 1;i++ )
            {
                string reader = ReaderArr[i].Trim();
                if (reader != "")
                {
                    EI843Bio obj = new EI843Bio(reader, 1001);
                    //obj.UDPStop(reader, udpport);
                    try
                    {
                        statusmsg[i] = new StatusMsg();
                        bool status = false;
                        bool ReaderStatus = false;
                        status = obj.StartTalk();
                        if (status)
                        { ReaderStatus = true; }
                        else
                        {
                            status = obj.StartTalk();
                            if (status)
                            { ReaderStatus = true; }
                            else
                            {
                                status = obj.StartTalk();
                                if (status) { ReaderStatus = true; }
                                else { ReaderStatus = false; }
                            }
                        }
                        if (ReaderStatus)
                        {
                            
                            //EI843Bio obj = new EI843Bio(reader, 1001);
                            status = obj.BeepReader();
                            if (status)
                            {
                                statusmsg[i].Reader = reader;
                                statusmsg[i].Command = "Beep";
                                statusmsg[i].Status = true;
                                statusmsg[i].Message = "Beep Success";
                            }
                            else
                            {
                                statusmsg[i].Reader = reader;
                                statusmsg[i].Command = "Beep";
                                statusmsg[i].Status = false;
                                statusmsg[i].Message = "Beep Failed";
                            }
                        }
                        else
                        {
                            statusmsg[i].Reader = reader;
                            statusmsg[i].Command = "GetAccessCodeTransCount";
                            statusmsg[i].Status = false;
                            statusmsg[i].Message = reader + "Reader is In Active";
                        }
                    }
                    catch (Exception ex)
                    {
                        statusmsg[i].Reader = reader;
                        statusmsg[i].Command = "Beep";
                        statusmsg[i].Status = false;
                        statusmsg[i].Message = ex.Message;
                    }
                    //obj.UDPStart(reader, udpport);
                }
            }
            //return Json(new { status = "true" }, JsonRequestBehavior.AllowGet);
            return Json(statusmsg, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetReaderStatus(string Reader)
         {

            if (Reader.Substring(Reader.Length - 1, 1) == "|")
            {
                Reader = Reader.Substring(0, Reader.Length - 1);
            }
            string[] ReaderArr = Reader.Split('|');
            StatusMsg[] statusmsg = new StatusMsg[ReaderArr.Length];
            //StatusMsg msg = new StatusMsg();
            for (int i = 0; i <= ReaderArr.Length - 1; i++)
            {
                string reader = ReaderArr[i].Trim();
                if (reader != "")
                {
                    EI843Bio obj = new EI843Bio(reader, 1001);                    
                    //obj.UDPStop(reader,udpport);
                    try
                    {                        
                        statusmsg[i] = new StatusMsg();
                        //EI843Bio obj = new EI843Bio(reader, 1001);
                        bool status = false;
                        bool ReaderStatus = false;
                        status=obj.OpenClient();
                        if (status==false)
                        {
                            status=obj.OpenClient();
                            if (status == false)
                            {
                                status = obj.OpenClient();
                            }
                        }
                        if (status)
                        {
                            status = obj.StartTalk();
                            if (status==false)
                            {
                                status = obj.StartTalk();
                                if (status == false)
                                {
                                    status = obj.StartTalk();
                                }
                            }
                        }                        
                        obj.CloseClient();
                        if (status)
                        { ReaderStatus = true; }
                        else
                        { ReaderStatus = false; }
                        
                        if (ReaderStatus)
                        {
                            statusmsg[i].Reader = reader;
                            statusmsg[i].Command = "Start Talk";
                            statusmsg[i].Status = true;
                            statusmsg[i].Message = "Active";
                        }                            
                        else
                        {                        
                            statusmsg[i].Reader = reader;
                            statusmsg[i].Command = "Start Talk";
                            statusmsg[i].Status = false;
                            statusmsg[i].Message = "In Active";
                       }
                    }                    
                    catch (Exception ex)
                    {
                        statusmsg[i].Reader = reader;
                        statusmsg[i].Command = "Start Talk";
                        statusmsg[i].Status = false;
                        statusmsg[i].Message = ex.Message;
                    }
                    //obj.UDPStart(reader, udpport);
                }
            }
            //return Json(new { status = "true" }, JsonRequestBehavior.AllowGet);
            return Json(statusmsg, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetReaderVersion(string Reader)
        {

            if (Reader.Substring(Reader.Length - 1, 1) == "|")
            {
                Reader = Reader.Substring(0, Reader.Length - 1);
            }
            string[] ReaderArr = Reader.Split('|');
            StatusMsg[] statusmsg = new StatusMsg[ReaderArr.Length];
            //StatusMsg msg = new StatusMsg();
            for (int i = 0; i <= ReaderArr.Length - 1; i++)
            {
                string reader = ReaderArr[i].Trim();
                if (reader != "")
                {
                    try
                    {
                        string version = "";
                        bool status = false;
                        statusmsg[i] = new StatusMsg();
                        EI843Bio obj = new EI843Bio(reader, 1001);
                        status=obj.OpenClient();
                        if (status==false)
                        {
                            status=obj.OpenClient();
                            if (status == false)
                            {
                                status = obj.OpenClient();
                            }
                        }
                        if (status)
                        {
                            version = obj.GetReaderVersion();
                        }                        
                        obj.CloseClient();
                        if (version != "")
                        {
                            statusmsg[i].Reader = reader;
                            statusmsg[i].Command = "Get Version";
                            statusmsg[i].Status = true;
                            statusmsg[i].Message = version;
                        }
                        else
                        {
                            statusmsg[i].Reader = reader;
                            statusmsg[i].Command = "Get Version";
                            statusmsg[i].Status = false;
                            statusmsg[i].Message = "Command Failed";
                        }
                    }
                    catch (Exception ex)
                    {
                        statusmsg[i].Reader = reader;
                        statusmsg[i].Command = "Get Version";
                        statusmsg[i].Status = false;
                        statusmsg[i].Message = ex.Message;
                    }

                }
            }
            //return Json(new { status = "true" }, JsonRequestBehavior.AllowGet);
            return Json(statusmsg, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAccessCodeTransCount(string Reader)
        {

            if (Reader.Substring(Reader.Length - 1, 1) == "|")
            {
                Reader = Reader.Substring(0, Reader.Length - 1);
            }
            string[] ReaderArr = Reader.Split('|');
            StatusMsg[] statusmsg = new StatusMsg[ReaderArr.Length];
            //StatusMsg msg = new StatusMsg();
            for (int i = 0; i <= ReaderArr.Length - 1; i++)
            {
                string reader = ReaderArr[i].Trim();
                if (reader != "")
                {
                    try
                    {
                        statusmsg[i] = new StatusMsg();                            
                        EI843Bio obj = new EI843Bio(reader, 1001);
                        bool status = false;
                        bool ReaderStatus = false;
                        status = obj.StartTalk();
                        if (status)
                        { ReaderStatus = true; }
                        else
                        {
                            status = obj.StartTalk();
                            if (status)
                            { ReaderStatus = true; }
                            else
                            {
                                status = obj.StartTalk();
                                if (status) { ReaderStatus = true; }
                                else { ReaderStatus = false; }
                            }
                        }
                        if (ReaderStatus)
                        {
                            
                            EI843Bio.ReaderMemoryStatus result = obj.GetAccessCodeTransCount();
                            string str = "";
                            str += "Transaction Limit : " + result.Transaction_Limits + "\n";
                            str += "Transaction Used: " + result.Transaction_Used + "\n";
                            str += "CardHolders Limit : " + result.CardHolder_Limits + "\n";
                            str += "CardHolders Used: " + result.CardHolder_Used + "\n";
                            statusmsg[i].Reader = reader;
                            statusmsg[i].Command = "GetAccessCodeTransCount";
                            statusmsg[i].Status = true;
                            statusmsg[i].Message = str;
                        }
                        else
                        {
                            statusmsg[i].Reader = reader;
                            statusmsg[i].Command = "GetAccessCodeTransCount";
                            statusmsg[i].Status = false;
                            statusmsg[i].Message = reader + "Reader is In Active";
                        }
                        //bool status = false;                        
                    }
                    catch (Exception ex)
                    {
                        statusmsg[i].Reader = reader;
                        statusmsg[i].Command = "GetAccessCodeTransCount";
                        statusmsg[i].Status = false;
                        statusmsg[i].Message = ex.Message;
                    }

                }
            }
            //return Json(new { status = "true" }, JsonRequestBehavior.AllowGet);
            return Json(statusmsg, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PulseRelay(string Reader)
        {

            if (Reader.Substring(Reader.Length - 1, 1) == "|")
            {
                Reader = Reader.Substring(0, Reader.Length - 1);
            }
            string[] ReaderArr = Reader.Split('|');
            StatusMsg[] statusmsg = new StatusMsg[ReaderArr.Length];
            //StatusMsg msg = new StatusMsg();
            for (int i = 0; i <= ReaderArr.Length - 1; i++)
            {
                string reader = ReaderArr[i].Trim();
                if (reader != "")
                {
                    try
                    {
                        bool status=false;                        
                        statusmsg[i] = new StatusMsg();
                        EI843Bio obj = new EI843Bio(reader, 1001);
                        status = obj.OpenRelay();

                        if (status)
                        {
                            statusmsg[i].Reader = reader;
                            statusmsg[i].Command = "Pulse Relay";
                            statusmsg[i].Status = true;
                            statusmsg[i].Message = "Set Successfully";
                        }
                        else
                        {
                            statusmsg[i].Reader = reader;
                            statusmsg[i].Command = "Pulse Relay";
                            statusmsg[i].Status = false;
                            statusmsg[i].Message = "Not Set";
                        }
                    }
                    catch (Exception ex)
                    {
                        statusmsg[i].Reader = reader;
                        statusmsg[i].Command = "Pulse Relay";
                        statusmsg[i].Status = false;
                        statusmsg[i].Message = ex.Message;
                    }

                }
            }
            //return Json(new { status = "true" }, JsonRequestBehavior.AllowGet);
            return Json(statusmsg, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DesecureRelay(string Reader)
        {

            if (Reader.Substring(Reader.Length - 1, 1) == "|")
            {
                Reader = Reader.Substring(0, Reader.Length - 1);
            }
            string[] ReaderArr = Reader.Split('|');
            StatusMsg[] statusmsg = new StatusMsg[ReaderArr.Length];
            //StatusMsg msg = new StatusMsg();
            for (int i = 0; i <= ReaderArr.Length - 1; i++)
            {
                string reader = ReaderArr[i].Trim();
                if (reader != "")
                {
                    try
                    {
                        bool status = false;
                        statusmsg[i] = new StatusMsg();
                        EI843Bio obj = new EI843Bio(reader, 1001);
                        status = obj.DesecureRelay();

                        if (status)
                        {
                            statusmsg[i].Reader = reader;
                            statusmsg[i].Command = "Desecure Relay";
                            statusmsg[i].Status = true;
                            statusmsg[i].Message = "Set Successfully";
                        }
                        else
                        {
                            statusmsg[i].Reader = reader;
                            statusmsg[i].Command = "Desecure Relay";
                            statusmsg[i].Status = false;
                            statusmsg[i].Message = "Not Set";
                        }
                    }
                    catch (Exception ex)
                    {
                        statusmsg[i].Reader = reader;
                        statusmsg[i].Command = "Desecure Relay";
                        statusmsg[i].Status = false;
                        statusmsg[i].Message = ex.Message;
                    }

                }
            }
            //return Json(new { status = "true" }, JsonRequestBehavior.AllowGet);
            return Json(statusmsg, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SecureRelay(string Reader)
        {

            if (Reader.Substring(Reader.Length - 1, 1) == "|")
            {
                Reader = Reader.Substring(0, Reader.Length - 1);
            }
            string[] ReaderArr = Reader.Split('|');
            StatusMsg[] statusmsg = new StatusMsg[ReaderArr.Length];
            //StatusMsg msg = new StatusMsg();
            for (int i = 0; i <= ReaderArr.Length - 1; i++)
            {
                statusmsg[i] = new StatusMsg();
                string reader = ReaderArr[i].Trim();
                if (reader != "")
                {
                    try
                    {
                        bool status = false;                        
                        EI843Bio obj = new EI843Bio(reader, 1001);
                        status = obj.CloseRelay();

                        if (status)
                        {
                            statusmsg[i].Reader = reader;
                            statusmsg[i].Command = "Secure Relay";
                            statusmsg[i].Status = true;
                            statusmsg[i].Message = "Set Successfully";
                        }
                        else
                        {
                            statusmsg[i].Reader = reader;
                            statusmsg[i].Command = "Secure Relay";
                            statusmsg[i].Status = false;
                            statusmsg[i].Message = "Not Set";
                        }
                    }
                    catch (Exception ex)
                    {
                        statusmsg[i].Reader = reader;
                        statusmsg[i].Command = "Secure Relay";
                        statusmsg[i].Status = false;
                        statusmsg[i].Message = ex.Message;
                    }

                }
            }
            //return Json(new { status = "true" }, JsonRequestBehavior.AllowGet);
            return Json(statusmsg, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ColdBoot(string Reader)
        {
            if (Reader.Substring(Reader.Length - 1, 1) == "|")
            {
                Reader = Reader.Substring(0, Reader.Length - 1);
            }
            string[] ReaderArr = Reader.Split('|');
            StatusMsg[] statusmsg = new StatusMsg[ReaderArr.Length];
            //StatusMsg msg = new StatusMsg();
            for (int i = 0; i <= ReaderArr.Length - 1; i++)
            {
                string reader = ReaderArr[i].Trim();
                if (reader != "")
                {
                    try
                    {
                        statusmsg[i] = new StatusMsg();                            
                        EI843Bio obj = new EI843Bio(reader, 1001);
                        bool status = false;
                        bool ReaderStatus = false;
                        status = obj.StartTalk();
                        if (status)
                        { ReaderStatus = true; }
                        else
                        {
                            status = obj.StartTalk();
                            if (status)
                            { ReaderStatus = true; }
                            else
                            {
                                status = obj.StartTalk();
                                if (status) { ReaderStatus = true; }
                                else { ReaderStatus = false; }
                            }
                        }
                        if (ReaderStatus)
                        {                            
                            status = obj.ColdBoot();
                            if (status)
                            {
                                statusmsg[i].Reader = reader;
                                statusmsg[i].Command = "Factory Reset";
                                statusmsg[i].Status = true;
                                statusmsg[i].Message = "Successfully Factory Reset";
                            }
                            else
                            {
                                statusmsg[i].Reader = reader;
                                statusmsg[i].Command = "Factory Reset";
                                statusmsg[i].Status = false;
                                statusmsg[i].Message = "Factory Reset Failed";
                            }
                        }
                        else
                        {
                            statusmsg[i].Reader = reader;
                            statusmsg[i].Command = "Factory Reset";
                            statusmsg[i].Status = false;
                            statusmsg[i].Message = reader + "Reader is In Active";
                        }
                    }
                    catch (Exception ex)
                    {
                        statusmsg[i].Reader = reader;
                        statusmsg[i].Command = "Factory Reset";
                        statusmsg[i].Status = false;
                        statusmsg[i].Message = ex.Message;
                    }

                }
            }
            //return Json(new { status = "true" }, JsonRequestBehavior.AllowGet);
            return Json(statusmsg, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Holiday(string Reader)
        {

            if (Reader.Substring(Reader.Length - 1, 1) == "|")
            {
                Reader = Reader.Substring(0, Reader.Length - 1);
            }
            string[] ReaderArr = Reader.Split('|');
            bool ReaderStatus = false;
            StatusMsg[] statusmsg = new StatusMsg[ReaderArr.Length];
            //StatusMsg msg = new StatusMsg();
            for (int i = 0; i <= ReaderArr.Length - 1; i++)
            {
                string reader = ReaderArr[i].Trim();
                if (reader != "")
                {
                    EI843Bio obj = new EI843Bio(reader, 1001);
                    //obj.UDPStop(reader, udpport);
                    try
                    {
                        statusmsg[i] = new StatusMsg();
                        bool status = false;
                        status = obj.StartTalk();
                        if (status)
                        { ReaderStatus = true; }
                        else
                        {
                            status = obj.StartTalk();
                            if (status)
                            { ReaderStatus = true; }
                            else
                            {
                                status = obj.StartTalk();
                                if (status) { ReaderStatus = true; }
                                else { ReaderStatus = false; }
                            }
                        }
                        if (ReaderStatus)
                        {                               
                            //EI843Bio obj = new EI843Bio(reader, 1001);                        
                            DateTime[] holiday = Util.Utility.GetHoliday();
                            status = obj.SetHoliday(holiday);
                            string MessageString = "";
                            foreach (var s in holiday)
                            {
                                MessageString += s.ToShortDateString() + ",";
                            }
                            if (status)
                            {
                                statusmsg[i].Reader = reader;
                                statusmsg[i].Command = "SetHoliday";
                                statusmsg[i].Status = true;
                                statusmsg[i].Message = MessageString + "  -   Set Successfully";
                            }
                            else
                            {
                                statusmsg[i].Reader = reader;
                                statusmsg[i].Command = "SetHoliday";
                                statusmsg[i].Status = false;
                                statusmsg[i].Message = MessageString + "  -   Set Failed";
                            }                        
                        }
                        else                      
                        {
                            statusmsg[i].Reader = reader;
                            statusmsg[i].Command = "SetHoliday";
                            statusmsg[i].Status = false;
                            statusmsg[i].Message = reader + "Reader is In Active";
                        }                        
                    }
                    catch (Exception ex)
                    {
                        statusmsg[i].Reader = reader;
                        statusmsg[i].Command = "SetHoliday";
                        statusmsg[i].Status = false;
                        statusmsg[i].Message = ex.Message;
                    }
                    //obj.UDPStart(reader, udpport);
                }
            }
            //return Json(new { status = "true" }, JsonRequestBehavior.AllowGet);
            return Json(statusmsg, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TimeZone(string Reader)
        {

            if (Reader.Substring(Reader.Length - 1, 1) == "|")
            {
                Reader = Reader.Substring(0, Reader.Length - 1);
            }
            string[] ReaderArr = Reader.Split('|');
            StatusMsg[] statusmsg = new StatusMsg[ReaderArr.Length];
            //StatusMsg msg = new StatusMsg();
            for (int i = 0; i <= ReaderArr.Length - 1; i++)
            {
                string reader = ReaderArr[i].Trim();
                bool ReaderStatus =false;
                if (reader != "")
                {
                    EI843Bio obj = new EI843Bio(reader, 1001);
                    bool status = false;
                    //obj.UDPStop(reader, udpport);                                        
                    try
                    {
                        statusmsg[i] = new StatusMsg();
                        status = obj.StartTalk();
                        if (status)
                        { ReaderStatus = true; }
                        else
                        {
                            status = obj.StartTalk();
                            if (status)
                            { ReaderStatus = true; }
                            else
                            {
                                status = obj.StartTalk();
                                if (status) { ReaderStatus = true; }
                                else { ReaderStatus = false; }
                            }
                        }
                        if (ReaderStatus)
                        {                            
                            //EI843Bio obj = new EI843Bio(reader, 1001);
                            SMAXV2Entities db = new SMAXV2Entities();
                            List<Smx_TimeZone> timezone = db.Smx_TimeZone.ToList();
                            foreach (Smx_TimeZone item in timezone)
                            {
                                int[] day = new int[0];
                                string[] starttime = new string[0];
                                string[] endtime = new string[0];
                                Util.Utility.GetTimeZoneDetails(Convert.ToInt16(item.TZ_ID), ref day, ref starttime, ref endtime);
                                if (day.Length > 0)
                                {
                                    status = obj.SetTimeZone(Convert.ToInt16(item.TZ_ID), day, starttime, endtime);
                                    if (status)
                                    {
                                        statusmsg[i].Reader = reader;
                                        statusmsg[i].Command = "SetTimeZone";
                                        statusmsg[i].Status = true;
                                        statusmsg[i].Message = statusmsg[i].Message + "TimeZone Name - '" + item.TZ_NAME + "' Set Successfully\n";
                                    }
                                    else
                                    {
                                        statusmsg[i].Reader = reader;
                                        statusmsg[i].Command = "SetTimeZone";
                                        statusmsg[i].Status = false;
                                        statusmsg[i].Message = statusmsg[i].Message + "TimeZone Nme '" + item.TZ_NAME + "' Set Failed\n";
                                    }
                                }
                            }
                        }
                        else
                        {
                            statusmsg[i].Reader = reader;
                            statusmsg[i].Command = "SetTimeZone";
                            statusmsg[i].Status = false;
                            statusmsg[i].Message = reader + "Reader is In Active";
                        }
                    }
                    catch (Exception ex)
                    {
                        statusmsg[i].Reader = reader;
                        statusmsg[i].Command = "SetTimeZone";
                        statusmsg[i].Status = false;
                        statusmsg[i].Message = ex.Message;
                    }                
                    //obj.UDPStart(reader, udpport);
                }
            }
            //return Json(new { status = "true" }, JsonRequestBehavior.AllowGet);
            return Json(statusmsg, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Cardholders(string Reader)
        {

            if (Reader.Substring(Reader.Length - 1, 1) == "|")
            {
                Reader = Reader.Substring(0, Reader.Length - 1);
            }
            string[] ReaderArr = Reader.Split('|');
            bool ReaderStatus = false;
            StatusMsg[] statusmsg = new StatusMsg[ReaderArr.Length];
            //StatusMsg msg = new StatusMsg();
            for (int i = 0; i <= ReaderArr.Length - 1; i++)
            {
                string reader = ReaderArr[i].Trim();
                bool status = false;
                if (reader != "")
                {
                    EI843Bio obj = new EI843Bio(reader, 1001);
                    //obj.UDPStop(reader, udpport);                                        
                    try
                    {
                        statusmsg[i] = new StatusMsg();
                        status = obj.StartTalk();
                        if (status)
                        { ReaderStatus = true; }
                        else
                        {
                            status = obj.StartTalk();
                            if (status)
                            { ReaderStatus = true; }
                            else
                            {
                                status = obj.StartTalk();
                                if (status) { ReaderStatus = true; }
                                else { ReaderStatus = false; }
                            }
                        }
                        if (ReaderStatus)
                        {                            
                            //EI843Bio obj = new EI843Bio(reader, 1001);
                            SMAXV2Entities db = new SMAXV2Entities();
                            status = obj.ClearAccessCode();
                            if (status)
                            {
                                List<vw_Smx_CardAccesslevel_Download> CardAccesslevel = db.vw_Smx_CardAccesslevel_Download.Where(e => e.ALD_READER_IPADDRESS == reader).ToList();
                                foreach (vw_Smx_CardAccesslevel_Download item in CardAccesslevel)
                                {
                                    string AccessCode = item.Ch_CardNo.ToString();
                                    DateTime ValidTo;
                                    int MsgId = 0;

                                    if (item.Ch_ValidTo == null)
                                    {
                                        ValidTo = new DateTime(2020, 12, 31);
                                    }
                                    else
                                    {
                                        ValidTo = Convert.ToDateTime(item.Ch_ValidTo);
                                    }

                                    //if (item.Ch_MS_Id != null)
                                    //{
                                    //    MsgId = Convert.ToInt32(item.Ch_MS_Id);
                                    //}
                                    status = obj.SetDatabase(item.Ch_CardNo.ToString(), ValidTo, Convert.ToInt32(item.ALD_TZ_ID), MsgId, Convert.ToByte(item.Ch_ISPin), Convert.ToByte(item.Ch_ISBio), Convert.ToByte(item.Ch_AntiPassBack),1);
                                    if (status)
                                    {
                                        statusmsg[i].Reader = reader;
                                        statusmsg[i].Command = "SetCardholder";
                                        statusmsg[i].Status = true;
                                        //statusmsg[i].Message = statusmsg[i].Message + "Emp Id - '" + item.Ch_EmpId + "' Set Successfully\n";
                                        statusmsg[i].Message = statusmsg[i].Message + "<font color='#008000'>" + item.Ch_EmpId + " - Sucess</fonr></br>";
                                    }
                                    else
                                    {
                                        statusmsg[i].Reader = reader;
                                        statusmsg[i].Command = "SetCardholder";
                                        statusmsg[i].Status = false;
                                        //statusmsg[i].Message = statusmsg[i].Message + "Emp Id - '" + item.Ch_EmpId + "' Set Failed\n";
                                        statusmsg[i].Message = statusmsg[i].Message + "<font color='#800000'>" + item.Ch_EmpId + " - Failed</fonr></br>";
                                    }
                                }
                            }
                        }
                        else
                        {
                            statusmsg[i].Reader = reader;
                            statusmsg[i].Command = "SetCardholder";
                            statusmsg[i].Status = false;
                            statusmsg[i].Message = reader + "Reader is In Active";
                        }                        
                    }
                    catch (Exception ex)
                    {
                        statusmsg[i].Reader = reader;
                        statusmsg[i].Command = "SetCardholder";
                        statusmsg[i].Status = false;
                        statusmsg[i].Message = ex.Message;
                    }                
                    //obj.UDPStart(reader, udpport);
                }
            }
            //return Json(new { status = "true" }, JsonRequestBehavior.AllowGet);
            return Json(statusmsg, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Message(string Reader)
        {

            if (Reader.Substring(Reader.Length - 1, 1) == "|")
            {
                Reader = Reader.Substring(0, Reader.Length - 1);
            }
            string[] ReaderArr = Reader.Split('|');
            bool ReaderStatus = false;
            bool status = false;
            StatusMsg[] statusmsg = new StatusMsg[ReaderArr.Length];
            //StatusMsg msg = new StatusMsg();
            for (int i = 0; i <= ReaderArr.Length - 1; i++)
            {
                string reader = ReaderArr[i].Trim();
                if (reader != "")
                {
                    EI843Bio obj = new EI843Bio(reader, 1001);
                    //obj.UDPStop(reader, udpport);                                        
                    try
                    {
                        statusmsg[i] = new StatusMsg();
                        status = obj.StartTalk();
                        if (status)
                        { ReaderStatus = true; }
                        else
                        {
                            status = obj.StartTalk();
                            if (status)
                            { ReaderStatus = true; }
                            else
                            {
                                status = obj.StartTalk();
                                if (status) { ReaderStatus = true; }
                                else { ReaderStatus = false; }
                            }
                        }
                        if (ReaderStatus)
                        {                            
                            //EI843Bio obj = new EI843Bio(reader, 1001);

                            List<Smx_Message> message = Util.Utility.GetMessage();
                            foreach (var msg in message)
                            {
                                status = obj.SetMessage((EI843Bio.MessageType)Convert.ToInt16(msg.MS_NAME), msg.MS_LINE1);
                                if (status)
                                {
                                    statusmsg[i].Reader = reader;
                                    statusmsg[i].Command = "SetMessage";
                                    statusmsg[i].Status = true;
                                    statusmsg[i].Message += "Message ID '" + msg.MS_NAME + "' Set Successfully <br>";
                                }
                                else
                                {
                                    statusmsg[i].Reader = reader;
                                    statusmsg[i].Command = "SetMessage";
                                    statusmsg[i].Status = false;
                                    statusmsg[i].Message += "Message ID '" + msg.MS_NAME + "' Set Failed <br>";
                                }
                            }
                        }
                        else
                        {
                            statusmsg[i].Reader = reader;
                            statusmsg[i].Command = "SetMessage";
                            statusmsg[i].Status = false;
                            statusmsg[i].Message = reader + "Reader is In Active";
                        }                            
                    }
                    catch (Exception ex)
                    {
                        statusmsg[i].Reader = reader;
                        statusmsg[i].Command = "SetMessage";
                        statusmsg[i].Status = false;
                        statusmsg[i].Message = ex.Message;
                    }                    
                    //obj.UDPStart(reader, udpport);
                }
            }
            //return Json(new { status = "true" }, JsonRequestBehavior.AllowGet);
            return Json(statusmsg, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SystemTime(string Reader)
        {

            if (Reader.Substring(Reader.Length - 1, 1) == "|")
            {
                Reader = Reader.Substring(0, Reader.Length - 1);
            }
            string[] ReaderArr = Reader.Split('|');
            bool ReaderStatus = false;
            bool status = false;
            StatusMsg[] statusmsg = new StatusMsg[ReaderArr.Length];
            //StatusMsg msg = new StatusMsg();
            for (int i = 0; i <= ReaderArr.Length - 1; i++)
            {
                string reader = ReaderArr[i].Trim();
                if (reader != "")
                {
                    EI843Bio obj = new EI843Bio(reader, 1001);
                    //obj.UDPStop(reader, udpport);
                    try
                    {
                        statusmsg[i] = new StatusMsg();
                        status = obj.StartTalk();
                        if (status)
                        { ReaderStatus = true; }
                        else
                        {
                            status = obj.StartTalk();
                            if (status)
                            { ReaderStatus = true; }
                            else
                            {
                                status = obj.StartTalk();
                                if (status) { ReaderStatus = true; }
                                else { ReaderStatus = false; }
                            }
                        }
                        if (ReaderStatus)
                        {                            
                            DateTime now = new DateTime();
                            now = DateTime.Now;
                            //EI843Bio obj = new EI843Bio(reader, 1001);
                            status = obj.SetDateTime(now.Day, now.Month, now.Year, now.Hour, now.Minute, now.Second);

                            if (status)
                            {
                                statusmsg[i].Reader = reader;
                                statusmsg[i].Command = "System Time";
                                statusmsg[i].Status = true;
                                statusmsg[i].Message = "System time update successfully";
                            }
                            else
                            {
                                statusmsg[i].Reader = reader;
                                statusmsg[i].Command = "System Time";
                                statusmsg[i].Status = false;
                                statusmsg[i].Message = "System Time Not Set";
                            }
                        }
                        else
                        {
                            statusmsg[i].Reader = reader;
                            statusmsg[i].Command = "System Time";
                            statusmsg[i].Status = false;
                            statusmsg[i].Message = reader + "Reader is In Active";
                        }
                    }
                    catch (Exception ex)
                    {
                        statusmsg[i].Reader = reader;
                        statusmsg[i].Command = "System Time ";
                        statusmsg[i].Status = false;
                        statusmsg[i].Message = ex.Message;
                    }                   
                   //obj.UDPStart(reader, udpport);
                }
            }
            //return Json(new { status = "true" }, JsonRequestBehavior.AllowGet);
            return Json(statusmsg, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateEverything(string Reader)
        {
            if (Reader.Substring(Reader.Length - 1, 1) == "|")
            {
                Reader = Reader.Substring(0, Reader.Length - 1);
            }
            string[] ReaderArr = Reader.Split('|');
            //StatusMsg[] statusmsg = new StatusMsg[ReaderArr.Length];
            //StatusMsg msg = new StatusMsg();
            List<StatusMsg> ResultMessagetoUI = new List<StatusMsg>();
            ResultMessagetoUI.Capacity = ReaderArr.Length * 4;
            for (int i = 0; i <= ReaderArr.Length - 1; i++)
            {
                string reader = ReaderArr[i].Trim();
                if (reader != "")
                {
                    var JsonObj = new JsonResult();
                    JsonObj = SystemTime(reader);
                    string jsonstr = JsonConvert.SerializeObject(JsonObj.Data);
                    var systimelst = JsonConvert.DeserializeObject<List<StatusMsg>>(jsonstr);
                    for (int cnt = 0; cnt <= systimelst.Count - 1; cnt++)
                    {
                        ResultMessagetoUI.Add(systimelst[cnt]);
                    }

                    JsonObj = TimeZone(reader);
                    jsonstr = JsonConvert.SerializeObject(JsonObj.Data);
                    var timezonelst = JsonConvert.DeserializeObject<List<StatusMsg>>(jsonstr);
                    for (int cnt = 0; cnt <= timezonelst.Count - 1; cnt++)
                    {
                        ResultMessagetoUI.Add(timezonelst[cnt]);
                    }

                    JsonObj = Holiday(reader);
                    jsonstr = JsonConvert.SerializeObject(JsonObj.Data);
                    var holidaylst = JsonConvert.DeserializeObject<List<StatusMsg>>(jsonstr);
                    for (int cnt = 0; cnt <= holidaylst.Count - 1; cnt++)
                    {
                        ResultMessagetoUI.Add(holidaylst[cnt]);
                    }

                    JsonObj = Message(reader);
                    jsonstr = JsonConvert.SerializeObject(JsonObj.Data);
                    var messagelst = JsonConvert.DeserializeObject<List<StatusMsg>>(jsonstr);
                    for (int cnt = 0; cnt <= messagelst.Count - 1; cnt++)
                    {
                        ResultMessagetoUI.Add(messagelst[cnt]);
                    }

                    JsonObj = Cardholders(reader);
                    jsonstr = JsonConvert.SerializeObject(JsonObj.Data);
                    var Cardholderlst = JsonConvert.DeserializeObject<List<StatusMsg>>(jsonstr);
                    for (int cnt = 0; cnt <= Cardholderlst.Count - 1; cnt++)
                    {
                        ResultMessagetoUI.Add(Cardholderlst[cnt]);
                    }

                }
            }
            //return Json(new { status = "true" }, JsonRequestBehavior.AllowGet);
            return Json(ResultMessagetoUI, JsonRequestBehavior.AllowGet);
        }

        public partial class ReaderArray
        {
            public string data { get; set; }
        }

        public class StatusMsg
        {
            public string Reader {get;set;}
            public string Command { get; set; }
            public bool Status { get; set; }
            public string Message { get; set; }
        }
	}
}