using System.Web;
using System.Web.Optimization;

namespace SmaxV2Attendance
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js", "~/Scripts/jquery.unobtrusive-ajax.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/timepicker").Include(
                      "~/Scripts/jquery.timepicker.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/datetimepicker").Include(
                      "~/Scripts/moment.js",
                      "~/Scripts/bootstrap-datetimepicker.js"));


            bundles.Add(new StyleBundle("~/Content/timepicker").Include(
                      "~/Content/jquery.timepicker.css"));

            bundles.Add(new StyleBundle("~/Content/datetimepicker").Include(
                      "~/Content/bootstrap-datetimepicker.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/validator").Include(
                      "~/Scripts/jquery.validate.min.js",
                      "~/Scripts/additional-methods.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/utility").Include(
                      "~/Scripts/Utility.js"));

            bundles.Add(new ScriptBundle("~/bundles/livemonitor").Include(
                      "~/Scripts/LiveMonitor.js"));

            

            bundles.Add(new ScriptBundle("~/bundles/blockui").Include(
                      "~/Scripts/jquery.blockUI.js"));

            bundles.Add(new ScriptBundle("~/bundles/timezonedetails").Include(
                      "~/Scripts/TimeZoneDetails.js"));

            bundles.Add(new ScriptBundle("~/bundles/accessleveldetails").Include(
                      "~/Scripts/AccessLevelDetails.js"));

            bundles.Add(new ScriptBundle("~/bundles/networkmonitor").Include(
                      "~/Scripts/NetworkMonitor.js"));

            bundles.Add(new ScriptBundle("~/bundles/device").Include(
                      "~/Scripts/Device.js"));

            bundles.Add(new ScriptBundle("~/bundles/datagrid").Include(
                      "~/Scripts/jquery.dataTables.js",
                      "~/Scripts/dataTables.bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/bulkassignment").Include(
                      "~/Scripts/BulkAssignment.js"));

            bundles.Add(new ScriptBundle("~/bundles/shiftassignment").Include(
                      "~/Scripts/shiftassignment.js"));

            bundles.Add(new ScriptBundle("~/bundles/weeklyoffassignment").Include(
                      "~/Scripts/weeklyoffassignment.js"));

            bundles.Add(new ScriptBundle("~/bundles/assignpermission").Include(
                      "~/Scripts/assignpermission.js"));

            bundles.Add(new ScriptBundle("~/bundles/leavedetails").Include(
                      "~/Scripts/leavedetails.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapswitch").Include(
                      "~/Scripts/bootstrap-switch.min.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrapswitch").Include(
                      "~/Content/bootstrap-switch.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/chkboxlist").Include(
                      "~/Scripts/demos.js",
                      "~/Scripts/jqxcore.js",
                      "~/Scripts/jqxdata.js",
                      "~/Scripts/jqxbuttons.js",
                      "~/Scripts/jqxscrollbar.js",
                      "~/Scripts/jqxlistbox.js",
                      "~/Scripts/jqxcheckbox.js"));

            bundles.Add(new StyleBundle("~/Content/chkboxliststyle").Include(
                      "~/Content/jqx.base.css"));

            bundles.Add(new ScriptBundle("~/bundles/areadetails").Include(
                      "~/Scripts/Areadetails.js"));

            bundles.Add(new ScriptBundle("~/bundles/eventtask").Include(
                     "~/Scripts/EventTask.js"));


      
        }
    }
}
