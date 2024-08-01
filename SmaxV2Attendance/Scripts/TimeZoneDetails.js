var status = true;
$(document).ready(function () {

    formclear();

    $("#btnadd").click(function () {
        if (validate()) {
            var day = $("#TZD_DAYS option:selected").text();
            var dayval = $("#TZD_DAYS").val();
            if (dayval == 10) {
                for (var i = 0; i <= 6; i++) {
                    day = getday(i);
                    var stime = $("#TZD_START_TIME").val();
                    var etime = $("#TZD_END_TIME").val();
                    var str = "<tr>"
                    str += "<td>" + day + "</td>";
                    str += "<td class='hide'>" + i + "</td>";
                    str += "<td>" + stime + "</td>";
                    str += "<td>" + etime + "</td>";
                    str += "<td><button onclick='deleterow(this)'>delete</button></td>"
                    if (checktablerow(i)) {
                        $("#tbltimedetails").append(str);
                    }
                }
            }
            if (dayval == 11) {
                for (var i = 0; i <= 4; i++) {
                    day = getday(i);
                    var stime = $("#TZD_START_TIME").val();
                    var etime = $("#TZD_END_TIME").val();
                    var str = "<tr>"
                    str += "<td>" + day + "</td>";
                    str += "<td class='hide'>" + i + "</td>";
                    str += "<td>" + stime + "</td>";
                    str += "<td>" + etime + "</td>";
                    str += "<td><button onclick='deleterow(this)'>delete</button></td>"
                    if (checktablerow(i)) {
                        $("#tbltimedetails").append(str);
                    }
                }
            }
            if (dayval == 12) {
                for (var i = 5; i <= 6; i++) {
                    day = getday(i);
                    var stime = $("#TZD_START_TIME").val();
                    var etime = $("#TZD_END_TIME").val();
                    var str = "<tr>"
                    str += "<td>" + day + "</td>";
                    str += "<td class='hide'>" + i + "</td>";
                    str += "<td>" + stime + "</td>";
                    str += "<td>" + etime + "</td>";
                    str += "<td><button onclick='deleterow(this)'>delete</button></td>"
                    if (checktablerow(i)) {
                        $("#tbltimedetails").append(str);
                    }
                }
            }
            if (dayval >= 0 && dayval <= 6 || dayval == 7) {
                var stime = $("#TZD_START_TIME").val();
                var etime = $("#TZD_END_TIME").val();
                var str = "<tr>"
                str += "<td>" + day + "</td>";
                str += "<td class='hide'>" + dayval + "</td>";
                str += "<td>" + stime + "</td>";
                str += "<td>" + etime + "</td>";
                str += "<td><button onclick='deleterow(this)'>delete</button></td>"
                if (checktablerow(dayval)) {
                    $("#tbltimedetails").append(str);
                }
            }
        }
    });

    $("#btnsave").click(function () {

        pagefadeout();
        jsonObj = [];
        //alert("validation :" + chkemptyrow());
        if (chkemptyrow()) {
            //alert("Adding..");
            $('#tbltimedetails tr').each(function () {
                var tz_id = $("#TimeZone").val();
                var dayval = $(this).find("td").eq(1).html();
                var stime = $(this).find("td").eq(2).html();
                var etime = $(this).find("td").eq(3).html();

                jsonObj.push({
                    TZD_TZ_ID: tz_id,
                    TZD_DAYS: dayval,
                    TZD_START_TIME: stime,
                    TZD_END_TIME: etime,
                    //TZD_SPECIFIC_DATE: "1/1/2014"
                });
            });

            var status = "";
            //alert(JSON.stringify(jsonObj));
            $.ajax({
                type: "POST",
                url: fullhost + "/TimeZoneDetails/Save",
                data: { smx_timezonedetails: jsonObj },
                async: false,
                dataType: "json",
                success: function (data) {
                    status = "true";
                }
            });


            if (status == "true") {
                //window.location = "http://localhost:1591/TimeZoneDetails/Index";
                alert("Timezone Saved Successfully!");
                return false;
            }
        }

        return false;
        //pagefadein();

    });

    $("#TimeZone").change(function () {
        var tz_id = $("#TimeZone").val();
        var jsonstr = "";
        $("#TZD_TZ_NAME").val($("#TimeZone :selected").text());
        jsonstr = $.ajax({
            type: "GET",
            url: fullhost + "/TimeZoneDetails/GetTimeZoneDetails?TZD_TZ_ID=" + tz_id,
            cache: false,
            async: false,
            dataType: "json"
        }).responseText;

        var json = JSON.parse(jsonstr);
        $("#tbltimedetails").html("");
        for (i = 0; i <= json.length - 1; i++) {
            var str = "<tr>"
            str += "<td>" + getday(json[i].TZD_DAYS) + "</td>";
            str += "<td  class='hide'>" + json[i].TZD_DAYS + "</td>";
            str += "<td>" + json[i].TZD_START_TIME + "</td>";
            str += "<td>" + json[i].TZD_END_TIME + "</td>";
            str += "<td><button onclick='deleterow(this)'>delete</button></td>"
            str += "</tr>"
            $("#tbltimedetails").append(str);
        }



    });

    $("#btnreset").click(function () {
        formclear();
    });

    $("#btndelete").click(function () {
        jsonObj = [];
        jsonObj.push({
            TZD_TZ_ID: $("#TimeZone").val()
        });
        var status = "";
        //alert(JSON.stringify(jsonObj));
        $.ajax({
            type: "POST",
            url: fullhost + "/TimeZoneDetails/Delete",
            data: { smx_timezonedetails: jsonObj },
            async: false,
            success: function (data) {
                //alert(JSON.parse(data));
                status = data;
            }
        });

        if (status.status == "true") {
            alert("Timezone Deleted Successfully!");
            window.location = "Create";
        }
        else {
            alert(status.status)
        }

    });

    $("#btnaddtz").click(function () {


        var status = "";

        //alert(JSON.stringify(jsonObj));
        $.ajax({
            type: "POST",
            url: fullhost + "/TimeZone/AddTZ",
            data: { TZ_Name: $("#TZD_TZ_NAME").val() },
            async: false,
            dataType: "json",
            success: function (data) {
                if (data.Err == '0') {
                    status = "true";
                    alert("Timezone Created Successfully!");
                    window.location = "Create";
                }
                else if (data.Err == '2') {
                    status = "false";
                    alert("Timezone name should not be empty!");
                }
                else {
                    status = "false";
                    alert("Timezone Already Exist!");
                }                
            }
        });

    });
});

function validate() {
    //--------- General Validation Check ----------------------

    //var validator = $("#frm_timezone").validate({
    //    rules: {
    //        TZD_START_TIME: {
    //            required: true
    //            //number: true
    //        },
    //        TZD_END_TIME: {
    //            required: true
    //            //number: true
    //        },
    //        TZD_DAYS: {
    //            required: true
    //        }
    //    }
    //});

    //if (!validator.element("#TZD_START_TIME"))
    //{ return false; }
    //if (!validator.element("#TZD_END_TIME"))
    //{ return false; }
    //if (!validator.element("#TZD_DAYS"))
    //{ return false; }

    //return true;

    if($("#TZD_START_TIME").val()== "")
    {
        alert("Please enter a valid start time.");
        $("#TZD_START_TIME").focus();
        return false;
    }

    if ($("#TZD_END_TIME").val() == "") {
        alert("Please enter a valid end time.");
        $("#TZD_END_TIME").focus();
        return false;
    }

    if ($("select[name='TZD_DAYS'] option:selected").index() == -1) {
        alert("Please select a day.");
        $("#TZD_DAYS").focus();
        return false;
    }

    return true;
}

function chkemptyrow() {
    if ($("#TimeZone").val() == null) {
        alert("Please select TimeZone");
        $("#TimeZone").focus();
        return false;
    }

    if ($("#tbltimedetails tr").length == 0) {
        alert("Please add some TimeZone Details");
        $("#btnadd").focus();
        return false;
    }
    return true;

}

function checktablerow(val) {
    //-------------- Check for any duplication data added to the table ---------------
    var status = true;
    $('#tbltimedetails tr').each(function () {
        var dayval = $(this).find("td").eq(1).html();
        if (dayval == val) {
            alert(getday(dayval) + " already exist to the list. \n Please delete the item and then try again.");
            status = false;
        }
    });
    return status;
}

function deleterow(obj) {
    $(obj).parent().parent().remove()
    //alert($("#tbltimedetails tr").length);
}

function getday(dayval) {
    var day = "";
    switch (parseInt(dayval)) {
        case 0:
            day = "Monday";
            break;
        case 1:
            day = "Tuesday";
            break;
        case 2:
            day = "Wednesday";
            break;
        case 3:
            day = "Thursday";
            break;
        case 4:
            day = "Friday";
            break;
        case 5:
            day = "Saturday";
            break;
        case 6:
            day = "Sunday";
            break;
        case 7:
            day = "Holiday";
            break;
    }
    return day;
}

function formclear() {
    $("#TimeZone").prop('selectedIndex', -1);
    $("#TZD_TZ_ID").prop('selectedIndex', -1);
    $("#TZD_DAYS").prop('selectedIndex', -1);
    $("#TZD_START_TIME").prop('value', "");
    $("#TZD_END_TIME").prop('value', "");
    $("#tbltimedetails").html("");
}

function pagefadeout() {
    //$("body").append("<div id='overlay' style='background-color:grey;position:absolute;top:0;left:0;height:100%;width:100%;z-index:999'></div>");
    //$('form :input').attr('disabled', 'disabled');
    //$.blockUI({ message: '<h1><img src="Images/loader.gif" /></h1>' });
}

function pagefadein() {
    //$('form :input').removeAttr('disabled');
    //$("#overlay").remove();
}