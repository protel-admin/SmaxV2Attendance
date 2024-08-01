using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmaxV2Attendance.Models;
using SmaxV2API;

namespace SmaxV2Attendance.Controllers
{
    [CustAuthFilter]
    public class DeviceController : Controller
    {
        private SMAXV2Entities db = new SMAXV2Entities();

        // GET: /Device/
        public ActionResult Index()
        {
            SMAXV2Entities db = new SMAXV2Entities();
            var smx_devices = db.Smx_Devices.Include(s => s.Smx_Location);
            return View(smx_devices.ToList());
        }

        // GET: /Device/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_Devices smx_devices = db.Smx_Devices.Find(id);
            if (smx_devices == null)
            {
                return HttpNotFound();
            }
            return View(smx_devices);
        }

        // GET: /Device/Create
        public ActionResult Create()
        {
            //----------- Generating  Dropdownlist -----------------
            ViewData["DE_READERTYPE"] = Util.Utility.GetReaderTypeDDL();
            ViewData["DE_IP1_NONC"] = Util.Utility.GetInputTypeNONCDDL();
            ViewData["DE_IP2_NONC"] = Util.Utility.GetInputTypeNONCDDL();
            ViewData["DE_MEMORY"] = Util.Utility.GetMemoryTypeDDL();
            ViewData["DE_DOTZ"] = Util.Utility.GetTimeZoneList();
            //ViewData["DE_READERMODE"] = Util.Utility.GetReaderModeDDL();
            //-----------------------------------------------------------------

            ViewBag.DE_LN_ID = new SelectList(db.Smx_Location, "LN_ID", "LN_NAME");
            return View();
        }

        // POST: /Device/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DE_ID,DE_IPADDRESS,DE_NODEID,DE_NAME,DE_LN_ID,DE_MESSAGE,DE_READERTYPE,DE_RELAYTIME,DE_DOTL,DE_DOTZ,DE_IP1_NAME,DE_IP2_NAME,DE_IP1_NONC,DE_IP2_NONC,DE_MEMORY,DE_OPERATIONAL,DE_MODEL,DE_FIRMWARE,DE_FIREALARM,DE_CREATED,DE_MODIFIED,DE_MODIFIEDBY")] Smx_Devices smx_devices)
        {
            if (ModelState.IsValid)
            {
                db.Smx_Devices.Add(smx_devices);
                db.SaveChanges();
                UpdateDevice(smx_devices);
                return RedirectToAction("Index");
            }

            //----------- Generating Dropdownlist -----------------
            ViewData["DE_READERTYPE"] = Util.Utility.GetReaderTypeDDL();
            ViewData["DE_IP1_NONC"] = Util.Utility.GetInputTypeNONCDDL();
            ViewData["DE_IP2_NONC"] = Util.Utility.GetInputTypeNONCDDL();
            ViewData["DE_MEMORY"] = Util.Utility.GetMemoryTypeDDL();
            ViewData["DE_DOTZ"] = Util.Utility.GetTimeZoneList();
            //ViewData["DE_READERMODE"] = Util.Utility.GetReaderModeDDL();
            //-----------------------------------------------------------------
            ViewBag.DE_LN_ID = new SelectList(db.Smx_Location, "LN_ID", "LN_NAME", smx_devices.DE_LN_ID);
            return View(smx_devices);
        }

        // GET: /Device/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_Devices smx_devices = db.Smx_Devices.Find(id);
            if (smx_devices == null)
            {
                return HttpNotFound();
            }

            //--------------Generating Dropdown list -------------------------------------------
            ViewData["DE_READERTYPE"] = Util.Utility.GetReaderTypeDDL(smx_devices.DE_READERTYPE);
            ViewData["DE_IP1_NONC"] = Util.Utility.GetInputTypeNONCDDL(smx_devices.DE_IP1_NONC);
            ViewData["DE_IP2_NONC"] = Util.Utility.GetInputTypeNONCDDL(smx_devices.DE_IP2_NONC);
            ViewData["DE_MEMORY"] = Util.Utility.GetMemoryTypeDDL(smx_devices.DE_MEMORY);
            ViewData["DE_DOTZ"] = Util.Utility.GetTimeZoneList();
            ViewData["DE_DOTZ_VAL"] = smx_devices.DE_DOTZ.ToString();
            //ViewData["DE_READERMODE"] = Util.Utility.GetReaderModeDDL();
            //----------------------------------------------------------------------------------

            ViewBag.DE_LN_ID = new SelectList(db.Smx_Location, "LN_ID", "LN_NAME", smx_devices.DE_LN_ID);
            return View(smx_devices);
        }

        // POST: /Device/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DE_ID,DE_IPADDRESS,DE_NODEID,DE_NAME,DE_LN_ID,DE_MESSAGE,DE_READERTYPE,DE_RELAYTIME,DE_DOTL,DE_DOTZ,DE_IP1_NAME,DE_IP2_NAME,DE_IP1_NONC,DE_IP2_NONC,DE_MEMORY,DE_OPERATIONAL,DE_MODEL,DE_FIRMWARE,DE_FIREALARM,DE_CREATED,DE_MODIFIED,DE_MODIFIEDBY")] Smx_Devices smx_devices)
        {
            if (ModelState.IsValid)
            {
                db.Entry(smx_devices).State = EntityState.Modified;
                db.SaveChanges();
                UpdateDevice(smx_devices);
                return RedirectToAction("Index");
            }

            //--------------Generating Dropdown list -------------------------------------------
            ViewData["DE_READERTYPE"] = Util.Utility.GetReaderTypeDDL(smx_devices.DE_READERTYPE);
            ViewData["DE_IP1_NONC"] = Util.Utility.GetInputTypeNONCDDL(smx_devices.DE_IP1_NONC);
            ViewData["DE_IP2_NONC"] = Util.Utility.GetInputTypeNONCDDL(smx_devices.DE_IP2_NONC);
            ViewData["DE_MEMORY"] = Util.Utility.GetMemoryTypeDDL(smx_devices.DE_MEMORY);
            ViewData["DE_DOTZ"] = Util.Utility.GetTimeZoneList();
            //ViewData["DE_READERMODE"] = Util.Utility.GetReaderModeDDL();
            //----------------------------------------------------------------------------------                       

            ViewBag.DE_LN_ID = new SelectList(db.Smx_Location, "LN_ID", "LN_NAME", smx_devices.DE_LN_ID);
            return View(smx_devices);
        }

        // GET: /Device/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smx_Devices smx_devices = db.Smx_Devices.Find(id);
            if (smx_devices == null)
            {
                return HttpNotFound();
            }
            return View(smx_devices);
        }

        // POST: /Device/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Util.CommonClass CC = new Util.CommonClass();
            DataTable dt_device = CC.GetDataTable("Select DE_IPADDRESS from Smx_Devices where DE_ID = '" + id.ToString() + "'");
            string ipadress = "";
            if(dt_device.Rows.Count > 0 )
            {
                ipadress = dt_device.Rows[0]["DE_IPADDRESS"].ToString();
            }
            DataTable dt_accessleveldetails = CC.GetDataTable("select * from Smx_AccessLevelDetails where ALD_READER_IPADDRESS = '" + ipadress + "'");
            Smx_Devices smx_devices = new Smx_Devices();
            smx_devices = db.Smx_Devices.Find(id);
            if(dt_accessleveldetails.Rows.Count == 0)
            {
                db.Smx_Devices.Remove(smx_devices);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Info = "This device has a conflit with access level details. Please remove this device from access level details before delete.";
            return View(smx_devices);
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        bool UpdateSystemTime(EI843Bio obj)
        {
            bool status = false;
            //EI843Bio obj = new EI843Bio(reader, 1001);
            //obj.OpenClient();
            DateTime now = new DateTime();
            now = DateTime.Now;
            status = obj.SetDateTime(now.Day, now.Month, now.Year, now.Hour, now.Minute, now.Second);
            //obj.CloseClient();
            return status;
        }

        bool UpdateDOTL_Relay(EI843Bio obj, int relaytime, int dotl )
        {
            bool status = false;
            status = obj.SetRelayTime(relaytime, dotl);
            return status;
        }

        bool UpdateInputSensor(EI843Bio obj, int input1, int input2,int input3,int input4)
        {
            // FrmMain.DAXT1.SetInputSensor(CmbIpaddress.Text, PortNo, TxtNodeId.Text, &H80 + RsChk.Fields("INPUT1NONC").Value, &H80 + RsChk.Fields("INPUT2NONC").Value, &H80 + RsChk.Fields("INPUT3NONC").Value, &H80 + RsChk.Fields("INPUT4NONC").Value) = True Then
            bool status = false;
            status = obj.SetInputSensor(input1,input2,input3,input4);
            return status;
        }

        bool UpdateMessage(EI843Bio obj, string message)
        {
            bool status = false;
            status = obj.SetMessage(EI843Bio.MessageType.Idle,message);
            return status;
        }

        bool UpdateMemFullStatus(EI843Bio obj, EI843Bio.FIFOMode mode)
        {
            bool status = false;
            status = obj.SetTransactionFIFO(mode);
            return status;
        }

        bool UpdateFireAlarm(EI843Bio obj, int flag)
        {
            bool status = false;
            status = obj.SetFireAlarmStatus(Convert.ToByte(flag));
            return status;
        }

        void UpdateDevice(Smx_Devices smx_devices)
        {
            //---------- Update Device Commands -----------------------------
            ViewBag.ErrorMsg = "";
            bool status = false;
            EI843Bio obj = new EI843Bio(smx_devices.DE_IPADDRESS, 1001);
            obj.OpenClient();
            //------ Set SystemTime
            status = UpdateSystemTime(obj);
            if (!status)
            { ViewBag.ErrorMsg += "SystemTime set failed!\n"; }
            else
            { ViewBag.ErrorMsg += "SystemTime set sucessfull!\n"; }

            //------- Set Memeory Full -----------------

            EI843Bio.FIFOMode fifo;
            if (smx_devices.DE_MEMORY == "1")
            {
                //----------- Memory Full ---------------
                fifo = EI843Bio.FIFOMode.FIFO_Disabled;
            }
            else
            {
                //----------- First In First Out --------------
                fifo = EI843Bio.FIFOMode.FIFO_Enabled;
            }
            status = UpdateMemFullStatus(obj, fifo);
            if (!status)
            { ViewBag.ErrorMsg += "MemeoryFullMode set failed!\n"; }
            else
            { ViewBag.ErrorMsg += "MemeoryFullMode set sucessfull!\n"; }

            //--------- Set Message --------------------------------------
            status = UpdateMessage(obj, smx_devices.DE_MESSAGE);
            if (!status)
            { ViewBag.ErrorMsg += "Message set failed!\n"; }
            else
            { ViewBag.ErrorMsg += "Message set sucessfull!\n"; }

            //---------- Set Relay Time -----------------------------------
            status = UpdateDOTL_Relay(obj, Convert.ToInt16(smx_devices.DE_RELAYTIME), Convert.ToInt16(smx_devices.DE_DOTL));
            if (!status)
            { ViewBag.ErrorMsg += "RelayTime set failed!\n"; }
            else
            { ViewBag.ErrorMsg += "RelayTime set sucessfull!\n"; }

            //---------- Set Fire Alarm -------------------------------------//
            int flag;
            if (smx_devices.DE_FIREALARM == 1)
            { flag = 1; }
            else
            { flag = 0; }
            status = UpdateFireAlarm(obj, flag);
            if (!status)
            {
                if (flag == 1)
                { ViewBag.ErrorMsg += "FireAlarm Enable failed!\n"; }
                else
                { ViewBag.ErrorMsg += "FireAlarm Disable failed!\n"; }
            }
            else
            {
                if (flag == 1)
                { ViewBag.ErrorMsg += "FireAlarm Enable sucessfull!\n"; }
                else
                { ViewBag.ErrorMsg += "FireAlarm Disable sucessfull!\n"; }
            }

            //---------- Set Input values -----------------------------------
            status = UpdateInputSensor(obj, Convert.ToInt16(smx_devices.DE_IP1_NONC), Convert.ToInt16(smx_devices.DE_IP2_NONC),0,0);
            if (!status)
            { ViewBag.ErrorMsg += "Input sensor mode set failed!\n"; }
            else
            { ViewBag.ErrorMsg += "Input sensor mode set sucessfull!\n"; }

            //---------- Set In/Out Mode -----------------------------------

            //-----------------------------------------------------------------
            obj.CloseClient();
        }
    }
}
