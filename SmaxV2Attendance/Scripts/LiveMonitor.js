$(document).ready(function () {    
    load_location(-1);
    var autorefresh = 1;
    $("#btnreport").click(function () {
        GetURLasJSON_callback(fullhost + "/LiveMonitor/GetLiveTransaction?ttype=" + $("#ddl_ttype").val() + "&ln_id=" + $("#ddl_location").val(), load_transaction)
    });

    $("#chkautorefresh").click(function () {
        if ($("#chkautorefresh")[0].checked == true) {
            autorefresh = 1;
            setTimeout(load_autolivetrans, 3000)
        }
        else {
            autorefresh = 0;
        }
    });

    //setTimeout(load_autolivetrans, 3000)
    load_autolivetrans()

    function load_transaction(json)
    {   
        $("#livetransactions").html("");
       
        if (json.length > 0)
        {
            for (i = 0; i <= json.length - 1; i++)
            {
                var css = "";                
                switch (json[i].Tr_TType) {
                    case 1:
                        css = 'success';
                        break;
                    case 2:
                        css = 'danger';
                        break;
                    case 3:
                        css = 'danger';
                        break;
                    case 4:
                        css = 'success';
                        break;
                    case 5:
                        css = 'danger';
                        break;
                    case 11:
                        css = 'info';
                        break;
                    case 17:
                        css = 'success';
                        break;
                    case 18:
                        css = 'danger';
                        break;
                    case 20:
                        css = 'success';
                        break;
                    case 26:
                        css = 'danger';
                        break;
                    case 33:
                        css = 'success';
                        break;
                    case 34:
                        css = 'danger';
                        break;
                    case 36:
                        css = 'success';
                        break;
                    case 42:
                        css = 'danger';
                        break;
                    case 58:
                        css = 'danger';
                        break;
                    case 113:
                        css = 'danger';
                        break;
                    case 114:
                        css = 'danger';
                        break;
                    case 170:
                        css = 'danger';
                        break;
                    case 186:
                        css = 'danger';
                        break;
                    case 188:
                        css = 'danger';
                        break;
                    case 189:
                        css = 'danger';
                        break;
                    case 202:
                        css = 'danger';
                        break;
                    case 204:
                        css = 'danger';
                        break;
                    case 218:
                        css = 'danger';
                        break;                                        
                    case 220:
                        css = 'danger';
                        break;                    
                    case 252:
                        css = 'danger';
                        break;
                    case 253:
                        css = 'danger';
                        break;
                    default:
                        css = '';
                        break;
                }

                var str = "<tr class='" + css + "'>";
                str += "<td>" + json[i].Date + "</td>";
                str += "<td>" + json[i].Time + "</td>";
                str += "<td>" + json[i].Device_Name + "</td>";
                str += "<td>" + json[i].Emp_Id + "</td>";
                str += "<td>" + json[i].Name + "</td>";
                str += "<td>" + json[i].Message + "</td>";
                str += "</tr>";
                $("#livetransactions").append(str);
            }

            //$("#datagrid1").dataTable();
            $("#datagrid1").show();
            if (autorefresh == 1) {
                setTimeout(load_autolivetrans, 3000)
            }
        }
    }

    function load_location(ln_id) {        
        var json = GetURLasJSON(fullhost + "/NetworkMonitor/Getlocation?ln_id=" + ln_id)
        
        $("#ddl_location").html("");
        $("#ddl_location").append("<option value='-1'>Select Location</option>")
        for (i = 0; i <= json.length - 1; i++) {
            var str = "<option value='" + json[i].LN_ID + "'>" + json[i].LN_NAME + "</option>"
            $("#ddl_location").append(str);
        }
          
    }

    function load_autolivetrans()
    {
        //GetURLasJSON_callback("./LiveMonitor/GetLiveTransaction?ttype=0", load_transaction)
        GetURLasJSON_callback(fullhost + "/LiveMonitor/GetLiveTransaction?ttype=" + $("#ddl_ttype").val() + "&ln_id=" + $("#ddl_location").val(), load_transaction)
    }
});