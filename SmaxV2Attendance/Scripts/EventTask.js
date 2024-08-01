$(document).ready(function () {
    $('input[name="my-checkbox"]').bootstrapSwitch();
    $('input[name="my-checkbox"]').bootstrapSwitch('size', 'mini')
    $('input[name="my-checkbox"]').bootstrapSwitch('onColor', 'success')

    load_location(-1);

    $("#ddl_location").change(function () {
        var ln_id = $("#ddl_location").val();
        GetURLasJSON_callback(fullhost + "/NetworkMonitor/GetDevices?ln_id=" + ln_id, load_readerlist)
    });

    $("#btn-submit").click(function () {

        alert($("#chk_relay3")[0].checked);

        alert($("#chk_message")[0].checked);

        if (validate()) {
            jsonObj = [];
            var al_id = $("#AccessLevel").val();
            $('#tblaldetails tr').each(function () {

                var ipaddress = $(this).find("td").eq(3).html();
                var timezone = $(this).find("td").eq(1).html();
                var location = $(this).find("td").eq(5).html();
                jsonObj.push({
                    ALD_AL_ID: al_id,
                    ALD_READER_IPADDRESS: ipaddress,
                    ALD_TZ_ID: timezone,
                    ALD_LN_ID: location,
                    ALD_NODEID: '1'
                });
            });

            var status = "";
            //alert(JSON.stringify(jsonObj));
            $.ajax({
                type: "POST",
                url: fullhost + "/AccessLevelDetails/Save",
                data: { smx_accessleveldetails: jsonObj },
                async: false,
                dataType: "json",
                success: function (data) {
                    status = "true";
                }
            });

            if (status == "true") {
                alert("Access Level Details Saved Successfully!");
            }
            return false;
        }
        else {
            return false;
        }
        return false;
    });

    function load_location(ln_id) {
        var json = GetURLasJSON(fullhost + "/NetworkMonitor/Getlocation?ln_id=" + ln_id)

        $("#ddl_location").html("");
        $("#ddl_location").append("<option value='-1'>Select Location</option>")
        for (i = 0; i <= json.length - 1; i++) {
            var str = "<option value='" + json[i].LN_ID + "'>" + json[i].LN_NAME + "</option>"
            $("#ddl_location").append(str);
        }

    }

    function load_readerlist(json) {
        $("#ddl_device").html("");
        for (i = 0; i <= json.length - 1; i++) {
            var str = "<option value='" + json[i].DE_IPADDRESS + "'>" + json[i].DE_NAME + " </option>";
            $("#ddl_device").append(str);
        }
    }

    
});