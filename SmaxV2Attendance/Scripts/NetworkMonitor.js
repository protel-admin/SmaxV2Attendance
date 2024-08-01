    $(document).ready(function(){
        load_location(-1);
        //GetURLasJSON_callback("/NetworkMonitor/Getlocation?ln_id=-1", load_location)

        $("#ddl_location").change(function () {
            var ln_id = $("#ddl_location").val();
            GetURLasJSON_callback( fullhost + "/NetworkMonitor/GetDevices?ln_id=" + ln_id, load_readerlist)            
        });

        $("#ddl_command").change(function () {
            $("#ddl_config").prop('selectedIndex', 0);
        });

        $("#ddl_config").change(function () {
            $("#ddl_command").prop('selectedIndex', 0);
        });

        $("#btnsend").click(function () {
            var cmd_id =$("#ddl_command").val();
            var config_id =$("#ddl_config").val();
            var ln_id = $("#ddl_location").val();
            if (ln_id > 0)
            {
                if (cmd_id > 0 || config_id > 0)
                {
                    pagefadeout();
                    var cmd = GetCommand($("#ddl_command").val().trim());
                    var config = GetCommand($("#ddl_config").val().trim());
                    $("#downloadstatus").html("");
                    $("#downloadstatus").html("<tr><td colspan=3>Updating Configurations to reader... <br> This might take some time. Please Wait...</td></tr>");
                    $("#btnsend").attr("disabled", "true");
                    if (config != "") {
                        cmd = config;
                    }
                }
                else
                {
                    alert("Select atleast one Command/Configuration");
                }
                
            }
            else
            {
                alert("Select atleast One Location");
            }
            var reader = GetReader();
            GetURLasJSON_callback(fullhost + "/NetworkMonitor/" + cmd + "?Reader=" + reader, load_cmdexec_status)
            
            $("#btnsend").removeAttr("disabled");
            //if (config != "UpdateEverything")
            //{
            //    $("#downloadstatus").html("");
            //    if (config != "") {
            //        cmd = config;
            //    }
            //    var reader = GetReader();
            //    GetURLasJSON_callback("/NetworkMonitor/" + cmd + "?Reader=" + reader, load_cmdexec_status)
               
                
                
            //}
            //else
            //{
            //    $("#downloadstatus").html("");

            //    //------------------ All Reader updates for each command ----------
            //    //for (cmdid = 9; cmdid <= 13; cmdid++)
            //    //{
            //    //    if (cmdid != 10)
            //    //    {
            //    //        cmd = GetCommand(cmdid);
            //    //        var reader = GetReader();
            //    //        var json = GetURLasJSON("/NetworkMonitor/" + cmd + "?Reader=" + reader)                       
            //    //        for (i = 0; i <= json.length - 1; i++) {
            //    //            var color = ""
            //    //            if (!json[i].Status) {
            //    //                color = "#FF4A4A"
            //    //            }
            //    //            else {
            //    //                color = "#008000"
            //    //            }
            //    //            var str = "<tr style='color:" + color + ";'>";
            //    //            str += "<td>" + json[i].Reader + "</td>";
            //    //            str += "<td>" + json[i].Command + "</td>";
            //    //            str += "<td>" + json[i].Message + "</td>";
            //    //            str += "</tr>";
            //    //            $("#downloadstatus").append(str);
            //    //        }
            //    //    }
            //    //}

            //    //------------------ All Command updates for each reader ----------
            //    var reader = GetReader().split('|');
            //    for (readercnt = 0; readercnt <= reader.length-1; readercnt++)
            //    {
            //        if (reader[readercnt].toString().length > 0)
            //        {
            //            for (cmdid = 9; cmdid <= 13; cmdid++) {
            //                if (cmdid != 10) {
            //                    cmd = GetCommand(cmdid);
            //                    GetURLasJSON_callback("/NetworkMonitor/" + cmd + "?Reader=" + reader[readercnt],load_cmdexec_status)
                                
            //                }
            //            }
            //        }
                    
            //    }

            //}
            
           
        });

    });

function GetCommand(cmdval)
    {
        switch(parseInt(cmdval))
    {
            case 0:
                return "";
                break;
            case 1:
                return "Beep";
                break;
            case 2:
                return "GetReaderStatus";
                break;
            case 3:
                return "GetAccessCodeTransCount";
                break;
            case 4:
                return "PulseRelay";
                break;
            case 5:
                return "DesecureRelay";
                break;
            case 6:
                return "SecureRelay";
                break;
            case 7:
                return "ColdBoot";
                break;
            case 8:
                return "UpdateEverything";
                break;
            case 9:
                return "SystemTime";
                break;
            case 10:
                return "Cardholders";
                break;
            case 11:
                return "TimeZone";
                break;
            case 12:
                return "Holiday";                            
                break;
            case 13:
                return "Message";
                break;
            default:
                return "";
        }
}

function GetReader()
{
    var readerstr = "";
    $('#readerdetails > tr').each(function () {

        if ($(this).find('input').is(':checked')) {

            readerstr += $(this).find('td:eq(3)').text() + "|";            
        }
    });
    return readerstr;
}

function load_location(ln_id)
{
    var json = GetURLasJSON(fullhost + "/NetworkMonitor/Getlocation?ln_id=" + ln_id)

    $("#ddl_location").html("");
    $("#ddl_location").append("<option value='-1'>Select Location</option>")
    for (i = 0; i <= json.length - 1; i++) {
        var str = "<option value='" + json[i].LN_ID + "'>" + json[i].LN_NAME + "</option>"
        $("#ddl_location").append(str);
    }
   
}

function load_readerlist(json)
{
    $("#readerdetails").html("");
    for (i = 0; i <= json.length - 1; i++) {
        var str = "<tr>";
        str += "<td><input type='checkbox' id='Chk_'" + json[i].DE_ID + "/></td>";
        str += "<td>" + json[i].LN_NAME + "</td>";
        str += "<td>" + json[i].DE_NAME + "</td>";
        str += "<td>" + json[i].DE_IPADDRESS + "</td>";
        str += "<td>" + json[i].DE_OPERATIONAL + "</td>";
        str += "</tr>";
        $("#readerdetails").append(str);
    }

}

function load_cmdexec_status(json)
{
    $("#downloadstatus").html("");
    for (i = 0; i <= json.length - 1; i++) {
        var color = ""
        if (!json[i].Status) {
            color = "#FF4A4A"
        }
        else {
            color = "#008000"
        }
        var str = "<tr style='color:" + color + ";'>";
        str += "<td>" + json[i].Reader + "</td>";
        str += "<td>" + json[i].Command + "</td>";
        str += "<td>" + json[i].Message + "</td>";
        str += "</tr>";
        $("#downloadstatus").append(str);
    }

    pagefadein();
}

function callback_load_location(json)
{
    //var json = JSON.parse(jsonstr);
    
}

