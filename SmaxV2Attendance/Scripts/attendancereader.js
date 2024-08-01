$(document).ready(function () {

    

    $('#radio1').click(function () {
        if ($('#radio1').is(':checked')) {
            $('#singlereaders').show();
            $('#inreader').hide();
            $('#outreader').hide();
            $('#inoutreaders').hide();
            $("#SuccessMsgdiv1").hide();
            $('#SuccessMsgdiv2').hide();
        }

    });

    $('#radio2').click(function () {
        if ($('#radio2').is(':checked')) {
            $('#singlereaders').hide();
            $('#inreader').show();
            $('#outreader').show();
            $('#inoutreaders').hide();
            $("#SuccessMsgdiv1").hide();
            $('#SuccessMsgdiv2').hide();
        }
    });
    $('#radio3').click(function () {
        if ($('#radio3').is(':checked')) {
            $('#inoutreaders').show();
            $('#singlereaders').hide();
            $('#inreader').hide();
            $('#outreader').hide();
            $("#SuccessMsgdiv1").hide();
            $('#SuccessMsgdiv2').hide();
        }

    });

    // None Readers Single Select Right To Left And Multi Select Right To Left

    $('#noneright').click(function (e) {
        var listOfObjects = [];
        var opts;
        var selectedOpts = $('#ddl_nonereaders option:selected');
        if (selectedOpts.length == 0) {
          //  alert("Nothing to move.");
            e.preventDefault();
        }

        $('#ddl_arnonereaders').append($(selectedOpts).clone());


        $(selectedOpts).remove();
        if (selectedOpts.length > 0) {
            for (var i = 0; i < selectedOpts.length; i++) {
                opts = selectedOpts[i].value;
                listOfObjects.push(opts);
            }
        }
        SubmitAssignedReaders(listOfObjects);
        e.preventDefault();
        
    });

    $('#nonerightall').click(function (e) {
        var listOfObjects = [];
        var opts;
        var selectedOpts = $('#ddl_nonereaders option');
        if (selectedOpts.length == 0) {
           // alert("Nothing to move.");
            e.preventDefault();
        }

        $('#ddl_arnonereaders').append($(selectedOpts).clone());
        $(selectedOpts).remove();
        if (selectedOpts.length > 0) {
            for (var i = 0; i < selectedOpts.length; i++) {
                opts = selectedOpts[i].value;
                listOfObjects.push(opts);
            }
        }
        SubmitAssignedReaders(listOfObjects);
        e.preventDefault();
     
    });

    // Single Select Left To Right And Multi Select Left To Right

    $('#noneleft').click(function (e) {
        var listOfObjects = [];
        var opts;

        var selectedOpts = $('#ddl_arnonereaders option:selected');
        if (selectedOpts.length == 0) {
          //  alert("Nothing to move.");
            e.preventDefault();
        }

        $('#ddl_nonereaders').append($(selectedOpts).clone());
       

        $(selectedOpts).remove();
        if (selectedOpts.length > 0) {
            for (var i = 0; i < selectedOpts.length; i++) {
                opts = selectedOpts[i].value;
                listOfObjects.push(opts);
            }
        }
        RemoveAssignedReaders(listOfObjects)

        e.preventDefault();
    });

    $('#noneleftall').click(function (e) {
        var listOfObjects = [];
        var opts;

        var selectedOpts = $('#ddl_arnonereaders option');
        if (selectedOpts.length == 0) {
           // alert("Nothing to move.");
            e.preventDefault();
        }

        $('#ddl_nonereaders').append($(selectedOpts).clone());
        $(selectedOpts).remove();
        if (selectedOpts.length > 0) {
            for (var i = 0; i < selectedOpts.length; i++) {
                opts = selectedOpts[i].value;
                listOfObjects.push(opts);
            }
        }
        RemoveAssignedReaders(listOfObjects)
        e.preventDefault();
    });


    // In Readers Single Select Right To Left And Multi Select Right To Left

    $('#inright').click(function (e) {
        var listOfObjects = [];
        var opts;
        var selectedOpts = $('#ddl_inreaders option:selected');
        if (selectedOpts.length == 0) {
           // alert("Nothing to move.");
            e.preventDefault();
        }

        $('#ddl_arinreaders').append($(selectedOpts).clone());
       

        $(selectedOpts).remove();
        if (selectedOpts.length > 0) {
            for (var i = 0; i < selectedOpts.length; i++) {
                opts = selectedOpts[i].value;
                listOfObjects.push(opts);
            }
        }
        SubmitAssignedReaders(listOfObjects);
        e.preventDefault();
      
    });

    $('#inrightall').click(function (e) {
        var listOfObjects = [];
        var opts;
        var selectedOpts = $('#ddl_inreaders option');
        if (selectedOpts.length == 0) {
          //  alert("Nothing to move.");
            e.preventDefault();
        }

        $('#ddl_arinreaders').append($(selectedOpts).clone());
        $(selectedOpts).remove();
        if (selectedOpts.length > 0) {
            for (var i = 0; i < selectedOpts.length; i++) {
                opts = selectedOpts[i].value;
                listOfObjects.push(opts);
            }
        }
        SubmitAssignedReaders(listOfObjects);
        e.preventDefault();
     
    });

   // Single Select Left To Right And Multi Select Left To Right

    $('#inleft').click(function (e) {
        var listOfObjects = [];      
        var opts;

        var selectedOpts = $('#ddl_arinreaders option:selected');
        if (selectedOpts.length == 0) {
           // alert("Nothing to move.");
            e.preventDefault();
        }

        $('#ddl_inreaders').append($(selectedOpts).clone());   
        
        $(selectedOpts).remove();
        if (selectedOpts.length > 0) {
            for (var i = 0; i < selectedOpts.length; i++) {
                opts = selectedOpts[i].value;
                listOfObjects.push(opts);
            }
        }
        RemoveAssignedReaders(listOfObjects)


        e.preventDefault();
    });

    $('#inleftall').click(function (e) {
        var listOfObjects = [];
        var opts;

        var selectedOpts = $('#ddl_arinreaders option');        

        if (selectedOpts.length == 0) {
          //  alert("Nothing to move.");
            e.preventDefault();
        }

        $('#ddl_inreaders').append($(selectedOpts).clone());
        
        $(selectedOpts).remove();
        if (selectedOpts.length > 0) {
            for (var i = 0; i < selectedOpts.length; i++) {
                opts = selectedOpts[i].value;
                listOfObjects.push(opts);
            }
        }
        RemoveAssignedReaders(listOfObjects)
        e.preventDefault();
    });

    // Out Readers Single Select Right To Left And Multi Select Right To Left

    $('#outright').click(function (e) {
        var listOfObjects = [];
        var opts;
        var selectedOpts = $('#ddl_outreaders option:selected');
        if (selectedOpts.length == 0) {
           // alert("Nothing to move.");
            e.preventDefault();
        }

        $('#ddl_aroutreaders').append($(selectedOpts).clone());
        $(selectedOpts).remove();
        if (selectedOpts.length > 0) {
            for (var i = 0; i < selectedOpts.length; i++) {
                opts = selectedOpts[i].value;
                listOfObjects.push(opts);
            }
        }
        SubmitAssignedReaders(listOfObjects);
        e.preventDefault();
        
    });

    $('#outrightall').click(function (e) {
        var listOfObjects = [];
        var opts;
        var selectedOpts = $('#ddl_outreaders option');
        if (selectedOpts.length == 0) {
           // alert("Nothing to move.");
            e.preventDefault();
        }

        $('#ddl_aroutreaders').append($(selectedOpts).clone());
        $(selectedOpts).remove();
        if (selectedOpts.length > 0) {
            for (var i = 0; i < selectedOpts.length; i++) {
                opts = selectedOpts[i].value;
                listOfObjects.push(opts);
            }
        }
        SubmitAssignedReaders(listOfObjects);
        e.preventDefault();
       
    });

    // Single Select Left To Right And Multi Select Left To Right

    $('#outleft').click(function (e) {
        var listOfObjects = [];
        var opts;
        var selectedOpts = $('#ddl_aroutreaders option:selected');
        if (selectedOpts.length == 0) {
           // alert("Nothing to move.");
            e.preventDefault();
        }

        $('#ddl_outreaders').append($(selectedOpts).clone());
        $(selectedOpts).remove();
        if (selectedOpts.length > 0) {
            for (var i = 0; i < selectedOpts.length; i++) {
                opts = selectedOpts[i].value;
                listOfObjects.push(opts);
            }
        }
        RemoveAssignedReaders(listOfObjects)
        e.preventDefault();
    });

    $('#outleftall').click(function (e) {
        var listOfObjects = [];
        var opts;
        var selectedOpts = $('#ddl_aroutreaders option');
        if (selectedOpts.length == 0) {
           // alert("Nothing to move.");
            e.preventDefault();
        }

        $('#ddl_outreaders').append($(selectedOpts).clone());
        $(selectedOpts).remove();
        if (selectedOpts.length > 0) {
            for (var i = 0; i < selectedOpts.length; i++) {
                opts = selectedOpts[i].value;
                listOfObjects.push(opts);
            }
        }
        RemoveAssignedReaders(listOfObjects)
        e.preventDefault();
    });


    $('#noneioright').click(function (e) {
        var listOfObjects = [];
        var opts;
        var selectedOpts = $('#ddl_iononereaders option:selected');
        if (selectedOpts.length == 0) {
           // alert("Nothing to move.");
            e.preventDefault();
        }

        $('#ddl_ioarnonereaders').append($(selectedOpts).clone());


        $(selectedOpts).remove();
        if (selectedOpts.length > 0) {
            for (var i = 0; i < selectedOpts.length; i++) {
                opts = selectedOpts[i].value;
                listOfObjects.push(opts);
            }
        }
        SubmitAssignedReaders(listOfObjects);
        e.preventDefault();

    });

    $('#noneiorightall').click(function (e) {
        var listOfObjects = [];
        var opts;
        var selectedOpts = $('#ddl_iononereaders option');
        if (selectedOpts.length == 0) {
            //alert("Nothing to move.");
            e.preventDefault();
        }

        $('#ddl_ioarnonereaders').append($(selectedOpts).clone());
        $(selectedOpts).remove();
        if (selectedOpts.length > 0) {
            for (var i = 0; i < selectedOpts.length; i++) {
                opts = selectedOpts[i].value;
                listOfObjects.push(opts);
            }
        }
        SubmitAssignedReaders(listOfObjects);
        e.preventDefault();

    });

    // Single Select Left To Right And Multi Select Left To Right

    $('#noneioleft').click(function (e) {
        var listOfObjects = [];
        var opts;

        var selectedOpts = $('#ddl_ioarnonereaders option:selected');
        if (selectedOpts.length == 0) {
           // alert("Nothing to move.");
            e.preventDefault();
        }

        $('#ddl_iononereaders').append($(selectedOpts).clone());


        $(selectedOpts).remove();
        if (selectedOpts.length > 0) {
            for (var i = 0; i < selectedOpts.length; i++) {
                opts = selectedOpts[i].value;
                listOfObjects.push(opts);
            }
        }
        RemoveAssignedReaders(listOfObjects)

        e.preventDefault();
    });

    $('#noneioleftall').click(function (e) {
        var listOfObjects = [];
        var opts;

        var selectedOpts = $('#ddl_ioarnonereaders option');
        if (selectedOpts.length == 0) {
           // alert("Nothing to move.");
            e.preventDefault();
        }

        $('#ddl_iononereaders').append($(selectedOpts).clone());
        $(selectedOpts).remove();
        if (selectedOpts.length > 0) {
            for (var i = 0; i < selectedOpts.length; i++) {
                opts = selectedOpts[i].value;
                listOfObjects.push(opts);
            }
        }
        RemoveAssignedReaders(listOfObjects)
        e.preventDefault();
    });


    $("#btn_submit").click(function () {

        var listOfObjects = [];
        var inOpts = [];
        var outOpts = [];
        var noneOpts = [];
        var noneioOpts = [];
        var opts;

        inOpts = $('#ddl_arinreaders option');     
        var outOpts = $('#ddl_aroutreaders option');  
        var noneOpts = $('#ddl_arnonereaders option');  
        var noneioOpts = $('#ddl_ioarnonereaders option');  

  
        if (inOpts.length > 0) {
            for (var i = 0; i < inOpts.length; i++) {
                opts = inOpts[i].value;            
                    listOfObjects.push(opts); 
                }                               
            }

        if (outOpts.length > 0) {
            for (var i = 0; i < outOpts.length; i++) {
                opts = outOpts[i].value;
                listOfObjects.push(opts); 
            }
        }
         //var url = fullhost + "/AttendanceReader/SaveAssignedReader?";
        //alert(listOfObjects);
        if (noneOpts.length > 0) {
            for (var i = 0; i < noneOpts.length; i++) {
                opts = '"'+noneOpts[i].value+'"';      
                listOfObjects.push(opts); 

            }
        }   
        if (noneioOpts.length > 0) {
            for (var i = 0; i < noneioOpts.length; i++) {
                opts = '"' + noneioOpts[i].value + '"';
                listOfObjects.push(opts);

            }
        }   

        //var readersid =""
       // readersid = listOfObjects.map(d => `"${d}"`).join(',');
        alert("Selected Readers Are Assigned" + listOfObjects)               
        confirm("Are you want to assigned the selected Readers!" + listOfObjects);

      
        var url = fullhost + "/AttendanceReader/SaveAssignedReader?";
        url += "readersid=" + listOfObjects;
        //alert(url);
        //json = PostURLasJSON(url, loadjsondata);

        $.ajax({
            type: 'POST',
            url: url,           
            success: function (data) {
                

                if (data == "OK") {
                    loadjsondata("OK")
                }
            }
        });

    });

    

    

});
function SubmitAssignedReaders(listOfObjects) {

    $('#SuccessMsgdiv2').hide();
    $("#SuccessMsgdiv1").hide();    
   
    if (listOfObjects != "") {

        
        confirm("Are you want to remove the assigned Readers!" + listOfObjects);
      
        //confirm("Are you want to assigned the selected Readers!" + listOfObjects);

        var url = fullhost + "/AttendanceReader/SaveAssignedReader?";
        url += "readersid=" + listOfObjects;
        //alert(url);
        //json = PostURLasJSON(url, loadjsondata);

        $.ajax({
            type: 'POST',
            url: url,
            success: function (data) {
                
                if (data == "OK") {
                    $("#SuccessMsgdiv1").show();

                    $("#SuccessMsgTxt1").text("Readers Moved Successfully!");
                } else {
                    $('#SuccessMsgdiv2').show();
                    $("#SuccessMsgTxt2").html("Readers Moved NOT updated..!");
                }
            }
        });
    } else {
        //alert("Select Reader From the List");
         $('#SuccessMsgdiv2').show();
        $("#SuccessMsgTxt2").html("Select Reader From the List");
      
    }
}

function RemoveAssignedReaders(listOfObjects) {

    $('#SuccessMsgdiv2').hide();
    $("#SuccessMsgdiv1").hide();

    //alert(listOfObjects);
    if (listOfObjects != "") {
       
        confirm("Are you want to remove the assigned Readers!" + listOfObjects);

        var url = fullhost + "/AttendanceReader/RemoveAssignedReader?";
        url += "readersid=" + listOfObjects;
        //alert(url);
        //json = PostURLasJSON(url, loadjsondata);

        $.ajax({
            type: 'POST',
            url: url,
            success: function (data) {
         
                if (data == "OK") {
                    $("#SuccessMsgdiv1").show();

                    $("#SuccessMsgTxt1").text("Readers Moved Successfully!");
                } else {
                    $('#SuccessMsgdiv2').show();
        $("#SuccessMsgTxt2").html("Readers Moved NOT updated..!");
                }
            }
        });
    } else {
       
        //alert("Select Reader From the List");
        $('#SuccessMsgdiv2').show();
        $("#SuccessMsgTxt2").html("Select Reader From the List");
      
    }
}

function loadjsondata(jsonstr) {
    if (jsonstr == "OK") {
        alert("Readers Moved Successfully!");
       
        //$("#SuccessMsgdiv1").show();

        //$("#SuccessMsgTxt1").text("Readers Moved Successfully!");

    }
    else {
        
         alert("Readers NOT moved !");
        //$('#SuccessMsgdiv2').show();
        //$("#SuccessMsgTxt2").html("Readers Moved NOT updated..!");
    }

}

