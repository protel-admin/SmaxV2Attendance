var status = true;
$(document).ready(function () {

    $("#btnadd").click(function () {
        //alert("hi");
        
        $('#tblreaderdetails > tr').each(function () {

            if ($(this).find('input').is(':checked')) {
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
                
            }
        });
    });

    $("#AreaName").change(function () {

        var ar_name = $("#AreaName").val();
        var jsonstr = "";
        $("#AR_NAME").val(ar_name);

        jsonstr = $.ajax({
            type: "GET",
            url: fullhost + "/Areas/GetAreaDetails?areaname=" + ar_name,
            cache: false,
            async: false,
            dataType: "json"
        }).responseText;


        var json = JSON.parse(jsonstr);
        ////Clear the access level details table row...
        //$("#tblaldetails").html("");
        ////Uncheck all the Device details table....

        $('#tbl_in > tr').each(function () {
            $(this).find('input[type="checkbox"]').removeAttr('checked');
        });

        $('#tbl_out > tr').each(function () {
            $(this).find('input[type="checkbox"]').removeAttr('checked');
        });

        for(i=0;i<=json.length-1;i++)
        {
            var id = "#Chk_" + json[i].AR_IPADDRESS.replace(".", "_").replace(".", "_").replace(".", "_");
            $(id).prop('checked', true);
        }

        if(json[0].AR_APB == 1)
        {
            $("#Chk_APB").prop('checked', true);
        }
        else
        {
            $("#Chk_APB").prop('checked', false);
        }
    });

    $("#btnsave").click(function () {

        //pagefadeout();
        


        if (validate())
        {
            var jsondata = GetURLasJSON(fullhost + "/Areas/GetLastAPBNo")
            var apb_in = parseInt(jsondata.apbno) + 1;
            var apb_out = apb_in + 1;
            var isapb = 0;
            if ($('#Chk_APB').is(':checked'))
            {
                isapb = 1;
            }
            else
            {
                isapb = 0;
            }
            jsonObj = [];
            $('#tbl_in > tr').each(function () {
                var AreaName = $("#AR_NAME").val();
                if ($(this).find('input').is(':checked')) {
                    var ipaddress = $(this).find("td").eq(2).html();
                    var location = $(this).find("td").eq(4).html();
                    jsonObj.push({
                        AR_NAME: AreaName,
                        AR_NODEID: '1',
                        AR_TYPE: '1',
                        AR_APB: isapb,
                        AR_LN_ID: location,
                        AR_IPADDRESS: ipaddress,
                        AR_APBNUMBER: apb_in,
                        AR_STATUS: 'N',
                        AR_DELETED: false
                    });

                }
            });

            $('#tbl_out > tr').each(function () {
                var AreaName = $("#AR_NAME").val();
                if ($(this).find('input').is(':checked')) {
                    var ipaddress = $(this).find("td").eq(2).html();
                    var location = $(this).find("td").eq(4).html();
                    jsonObj.push({
                        AR_NAME: AreaName,
                        AR_NODEID: '1',
                        AR_TYPE: '2',
                        AR_APB: isapb,
                        AR_LN_ID: location,
                        AR_IPADDRESS: ipaddress,
                        AR_APBNUMBER: apb_out,
                        AR_STATUS: 'N',
                        AR_DELETED:false
                    });

                }
            });
            var status = "";
            //alert(JSON.stringify(jsonObj));
            $.ajax({
                type: "POST",
                url: fullhost + "/Areas/Save",
                data: { smx_areas: jsonObj },
                async: false,
                dataType: "json",
                success: function (data) {
                    status = "true";
                }
            });

            if (status == "true") {
                alert("Area Name Details Saved Successfully!");
            }
            return true;
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
        var ar_name = $("#AreaName").val();
       
        var status = "";
        //alert(JSON.stringify(jsonObj));
        $.ajax({
            type: "GET",
            url: fullhost + "/Areas/DeleteArea?areaname=" + ar_name,
            async: false,
            dataType: "json",
            success: function (data) {
                status = data;
            }
        });

        if (status.status == "true")
        {
            alert("Area Deleted Successfully!");
            window.location = fullhost + "/Areas/"
        }
        else
        {
            alert("Area not Deleted!");
        }
    });

});

function formclear() {
    $("#AreaName").prop('selectedIndex', -1);
    $("#AR_NAME").prop('value', "");

    $('#tbl_in > tr').each(function () {
        $(this).find('input[type="checkbox"]').removeAttr('checked');
    });

    $('#tbl_out > tr').each(function () {
        $(this).find('input[type="checkbox"]').removeAttr('checked');
    });

   
    //window.location = "Create";
}

function validate() {
    //--------- General Validation Check ----------------------
    status = true;

    if ($("#AR_NAME").val() == null) {
        alert("Please select Area Name");
        $("#AR_NAME").focus();
        return false;
    }
    if ($('#tbl_in tr').length > 0) {
        var in_selected = false;
        $('#tbl_in tr').each(function () {
            
            if ($(this).find('input').is(':checked')) {
                in_selected = true;
            }
        });
    }
    
    if ($('#tbl_out tr').length > 0) {
        var out_selected = false;
        $('#tbl_out tr').each(function () {

            if ($(this).find('input').is(':checked')) {
                out_selected = true;
            }
        });
    }

    if (in_selected == true && out_selected == true)
    {
        status = true;
    }
    else
    {
        alert("Please check you have selected at least one IN and one OUT reader.");
        status = false;
    }
    return status;
}


function deleterow(obj) {
    $(obj).parent().parent().remove()
    //alert($("#tbltimedetails tr").length);
}

