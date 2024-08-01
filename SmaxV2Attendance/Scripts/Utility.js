

function GetURLasJSON(urlstring)
{
    jsonstr = $.ajax({
        type: "GET",
        url: urlstring,
        cache: false,
        async: false,
        dataType: "json"
    }).responseText;

    var json = JSON.parse(jsonstr);
    return json;
}

function GetURLasString(urlstring) {
    jsonstr = $.ajax({
        type: "GET",
        url: urlstring,
        cache: false,
        async: false,
        dataType: "json"
    }).responseText;

    return jsonstr;
}

function GetURLasString_callback(urlstring,callbackfunc) {
    jsonstr = $.ajax({
        type: "GET",
        url: urlstring,
        cache: false,
        async: true,
        success: function (data) {
            callbackfunc(data);
        }
    });
}

function GetURLasJSON_callback(urlstring,callbackfunc) {
    $.ajax({
        url: urlstring,
        dataType: 'json',
        success: function (data) {
            callbackfunc(data);
        }
    });

    
}

function GetURLasJSONStr_callback(urlstring, callbackfunc) {
    $.ajax({
        url: urlstring,
        success: function (data) {
            callbackfunc(data);
        }
    });
}

function PostURLasJSON(urlstring,datatopost)
{
    var returndata;
    var status;
    jsonstr = $.ajax({
        type: "POST",
        url: urlstring,
        data: datatopost,
        async: true,
        dataType: "json",
        success: function (data) {
            status = data;
            returndata = data;
        }
    }).responseText;
    var json = JSON.parse(jsonstr);
    return json;
}

function pagefadeout() {
   //$("body").append("<div id='overlay' style='background-color:grey;position:absolute;top:0;left:0;height:100%;width:100%;z-index:999'></div>");
    //$('form :input').attr('disabled', 'disabled');
    $.blockUI({ message: '<h1><img src="' + fullhost + '/Images/loader.gif" /></h1>' });
}

function pagefadein() {
    //$('form :input').removeAttr('disabled');
    //$("#overlay").remove();
    $.unblockUI();
}

function printpreview(rpthtml)
{
    var str = "";
    str += "<html>";
    str += "<head>";
    str += "<link href='/Content/bootstrap.css' rel='stylesheet'/>"
    str += "</head>";
    str += "<body>";
    str += "<table class='table table-striped table-bordered'>";
    str += rpthtml;
    str += "</table>";
    str += "</Body>";
    str += "</html>";
    var win = window.open();
    win.document.write(str);
}

function getcurrentdate()
{
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear();

    if (dd < 10) {
        dd = '0' + dd
    }

    if (mm < 10) {
        mm = '0' + mm
    }

    today = mm + '/' + dd + '/' + yyyy;
    return today;
}

function datediff(datepart, fromdatestr, todatestr) {
    // datepart: 'y', 'm', 'w', 'd', 'h', 'n', 's'
    datepart = datepart.toLowerCase();
    fromdate = new Date(fromdatestr);
    todate = new Date(todatestr);
    var diff = todate - fromdate;	
    var divideBy = { w:604800000, 
        d:86400000, 
        h:3600000, 
        n:60000, 
        s:1000 };	
  
    return Math.floor( diff/divideBy[datepart]);
}