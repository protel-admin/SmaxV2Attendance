$(document).ready(function () {

    $("#btngetemp").click(function () {

        var DP_ID = $("#DDL_DEPT").val();
        var DN_ID = $("#DDL_DESIG").val();
        if (DP_ID == "")
        {
            DP_ID = "0";
        }
        if (DN_ID == "")
        {
            DN_ID = 0;
        }
        url = fullhost + "/BulkAssignment/GetEmployee?DP_ID=" + DP_ID + "&DN_ID=" + DN_ID;

        //var json = GetURLasJSON_callback(fullhost + "/NetworkMonitor/GetReaderStatus?Reader=" + reader, checkreader)
        GetURLasJSON_callback(url, getemployees)

    });

    function getemployees(json) {
        $("#tblempdetails").html("");
        if (json.length > 0)
        {
            
            for(i=0;i<=json.length-1;i++)
            {
                var str = "<tr>";
                str += "<td><input type='checkbox' id='Chk_" + json[i].Ch_EmpId + "' /></td>";
                str += "<td>" + json[i].Ch_EmpId + "</td>";
                str += "<td>" + json[i].Ch_FName + " " + json[i].Ch_LName + "</td>";
                str += "<td>" + json[i].DP_NAME + "</td>";
                str += "<td>" + json[i].DN_NAME + "</td>";
                str += "</tr>";
                $("#tblempdetails").append(str);
            }
            
        }
    }
});