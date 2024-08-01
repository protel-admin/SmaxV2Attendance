var status = true;
$(document).ready(function () {

    $("#btnadd").click(function () {
        //alert("hi");
        $("#tblaldetails").html("");
        $('#tblreaderdetails > tr').each(function () {

            if($(this).find('input').is(':checked'))
            {
                //alert($(this).find('td:eq(1)').text());
                var str = "<tr>"
                str += "<td>" + $(this).find('td:eq(1)').text() + "</td>";
                str += "<td>" + $("#TimeZone option:selected").val() + "</td>"
                str += "<td>" + $("#TimeZone option:selected").text() + "</td>";
                str += "<td>" + $(this).find('td:eq(2)').text() + "</td>";
                str += "<td>" + $(this).find('td:eq(3)').text() + "</td>";
                str += "<td>" + $(this).find('td:eq(4)').text() + "</td>";
                str += "<td><button onclick='deleterow(this)'>delete</button></td>"
                $("#tblaldetails").append(str);
                //if (checktablerow($(this).find('td:eq(2)').text())) {
                        
                //}
            }
        });
    });

    $("#AccessLevel").change(function () {
            
        var al_id = $("#AccessLevel").val();
        var jsonstr = "";
        $("#ALD_AL_NAME").val($("#AccessLevel :selected").text());
            
        jsonstr = $.ajax({
            type: "GET",
            url: fullhost + "/AccessLevelDetails/GetAccessLevelDetails?ALD_AL_ID=" + al_id,
            cache: false,
            async: false,
            dataType: "json"
        }).responseText;

            
        var json = JSON.parse(jsonstr);
        //Clear the access level details table row...
        $("#tblaldetails").html("");
        //Uncheck all the Device details table....

        $('#tblreaderdetails > tr').each(function () {
            $(this).find('input[type="checkbox"]').removeAttr('checked');
        });

        for (i = 0; i <= json.length - 1; i++) {
            var str = "<tr>"
            str += "<td>" + json[i].DE_NAME + "</td>";
            str += "<td>" + json[i].ALD_TZ_ID + "</td>";
            str += "<td>" + json[i].TZ_NAME + "</td>";
            str += "<td>" + json[i].ALD_READER_IPADDRESS + "</td>";
            str += "<td>" + json[i].LN_NAME + "</td>";
            str += "<td>" + json[i].ALD_LN_ID + "</td>";
            str += "<td><button onclick='deleterow(this)'>delete</button></td>"
            str += "</tr>"
            $("#tblaldetails").append(str);
            var id = "#Chk_" + json[i].ALD_READER_IPADDRESS.replace(".", "_").replace(".", "_").replace(".", "_");
            $(id).prop('checked', true);
            $("#TimeZone").val(json[i].ALD_TZ_ID);
        }

        //$('#tblreaderdetails tr').each(function () {
        //    $(this).find('input[type="checkbox"]').attr('checked', 'checked');
        //    //ttt($(this).find("td").eq(0).children('input'));
        //});
        //$("#div_aldetails").enabled;
    });
   
    $("#btnsave").click(function () {

        //pagefadeout();
        if (validate())
        {
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
        else
        {
            return false;
        }
            
            

        //pagefadein();

    });

    $("#btnreset").click(function () {
        formclear();
        return false;
    });

    $("#btndelete").click(function () {
        jsonObj = [];
        jsonObj.push({
            ALD_AL_ID:  $("#AccessLevel").val()
        });
        var status = "";
        //alert(JSON.stringify(jsonObj));
        $.ajax({
            type: "POST",
            url: fullhost + "/AccessLevelDetails/Delete",
            data: { smx_accessleveldetails: jsonObj },
            async: false,
            dataType: "json",
            success: function (data) {
                status = data;
            }
        });

        if (status.status == "true") {
            alert("Access Level Deleted Successfully!");
            window.location = "Create";
        }
        else {
            alert(status.status);
        }
    });

    $("#btnaddal").click(function () {


        var status = "";

        //alert(JSON.stringify(jsonObj));
        $.ajax({
            type: "POST",
            url: fullhost + "/AccessLevel/AddAL",
            data: { AL_Name: $("#ALD_AL_NAME").val() },
            async: false,
            dataType: "json",
            success: function (data) {
                if (data.Err == '0') {
                    status = "true";
                    alert("Access level created successfully!");
                    window.location = "Create";
                }
                else if (data.Err == '2')
                {
                    status = "false";
                    alert("Access level name should not be empty!");
                }
                else
                {
                    status = "false";
                    alert("Access level already exist!");
                }                               
            }
        });

    });

});
    
function formclear() {
    $("#TimeZone").prop('selectedIndex', -1);
    $("#AccessLevel").prop('selectedIndex', -1);
    $("#ALD_AL_NAME").prop('value', "");
    $("#tblaldetails").html("");
    $('#tblreaderdetails > tr').each(function () {
        $(this).find('input[type="checkbox"]').removeAttr('checked');
    });
    //window.location = "Create";
}

function validate() {
    //--------- General Validation Check ----------------------
    status = true;
        
    if ($("#AccessLevel").val() == null)
    {
        alert("Please select AccessLevel");
        $("#AccessLevel").focus();
        return false;
    }
    if ($('#tblaldetails tr').length > 0)
    {
        $('#tblaldetails tr').each(function () {

            if ($(this).find("td").eq(2).html().trim() == "")
            {
                alert("TimeZone Missing.");
                $(this).find("td").css("background", "#FC9EA6");
                status =  false;
            }

            if ($(this).find("td").eq(3).html().trim() == "") {
                alert("Reader Missing.");
                $(this).find("td").css("background", "#FC9EA6");
                status = false;
            }
                
        });
    }
    else
    {
        status = false;
    }
    return status;
}

function checktablerow(val) {
    //-------------- Check for any duplication data added to the table ---------------
    var status = true;
    $('#tblaldetails tr').each(function () {
        var dayval = $(this).find("td").eq(2).text();
        if (dayval == val) {
            //alert(getday(dayval) + " already exist to the list. \n Please delete the item and then try again.");
            status = false;
        }
    });
    return status;
}

function deleterow(obj) {
    $(obj).parent().parent().remove()
    //alert($("#tbltimedetails tr").length);
}

