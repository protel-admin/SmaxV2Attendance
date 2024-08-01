$(document).ready(function () {

        pagefadein();
        $("#btnsave").hide();
        var reader = "";
        $("#btnstarttalk").click(function () {
            if (validation() == false)
                return false;
            pagefadeout();
            reader = $("#DE_IPADDRESS").val();
            url = fullhost + "/NetworkMonitor/GetReaderStatus?Reader=" + reader;
            
            //var json = GetURLasJSON_callback(fullhost + "/NetworkMonitor/GetReaderStatus?Reader=" + reader, checkreader)
            var json = GetURLasJSON_callback(url, checkreader)
           
        });

        function validation()
        {
            var ipaddress = $("#DE_IPADDRESS").val();
            if(ipaddress.trim() == "")
            {
                alert("Please enter a reader ip address!");
                return false;
            }
            
            var arr = ipaddress.split('.');
            if (arr.length == 4)
            {
                if (!$.isNumeric(arr[0])) {
                    return false;
                }
                if (!$.isNumeric(arr[1])) {
                    return false;
                }
                if (!$.isNumeric(arr[2])) {
                    return false;
                }
                if (!$.isNumeric(arr[3])) {
                    return false;
                }
                var ipaddress = parseInt(arr[0]).toString() + "." + parseInt(arr[1]).toString() + "." + parseInt(arr[2]).toString() + "." + parseInt(arr[3]).toString()
                $("#DE_IPADDRESS").val(ipaddress)
            }
            //alert($("#DE_IPADDRESS").val());
            return true;
        }

        function checkreader(json)
        {
            if (json.length > 0) {
                if (json[0].Status)
                {
                    url = fullhost + "/NetworkMonitor/GetReaderVersion?Reader=" + reader;
                    //var json = GetURLasJSON_callback(fullhost + "/NetworkMonitor/GetReaderVersion?Reader=" + reader, checkversion)
                    var json = GetURLasJSON_callback(url, checkversion)
                }
                else
                {
                    alert("Unable to Communicate with the reader!");
                    pagefadein();
                }
            }
            
        }

        function checkversion(json)
        {
            if (json.length > 0) {
                if (json[0].Status) {
                    $("#DE_MODEL").val(json[0].Message.toString().substring(0,10));
                    $("#DE_FIRMWARE").val(json[0].Message.toString().substring(10, json[0].Message.toString().length));
                    $("#DE_OPERATIONAL").prop("checked", true);
                    alert("IP Address Available.")
                    $("#btnsave").show();
                    //pagefadein();
                }
                else
                {
                    alert(json[0].Message);
                }
            }

            pagefadein();
        }

    });
