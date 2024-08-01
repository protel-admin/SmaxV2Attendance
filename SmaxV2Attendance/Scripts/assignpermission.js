$(document).ready(function () {

    
    $("#ddl_DepartmentId").change(function () {
        $("#lstemp").disabled;
        loademployee("#lstemp");
        $("#jqxWidget").show();
        $("#lstemp").enabled;
    });

    $("#trgdate").change(function () {
        if (typeof checkedItems != 'undefined')
        {
            var date = $("#trgdate").val();
            var empid = "" + checkedItems;
            json = GetURLasJSON(fullhost + "/AssignPermission/GetShiftTime?date=" + date + "&empid=" + checkedItems)
            if (json.Err == 0) {
                $("#ShiftName").val(json.ShiftName);
                $("#StartTime").val(json.StartTime);
                $("#EndTime").val(json.EndTime);
            }
            else {
                alert("Shift not available for this date.")
                $("#ShiftName").val("");
                $("#StartTime").val("");
                $("#EndTime").val("");
            }
        }
        else
        {
            alert("please select employee.")
            $("#trgdate").val("");
        }
        
        

    });
    
    $("#btn_submit").click(function () {
        if ($("#jqxWidget").is(':visible') == true && typeof checkedItems != "undefined" && $("#trgdate").val() != "" && ($('#radio1').is(':checked') || $('#radio2').is(':checked'))) {
            var url = fullhost + "/AssignPermission/SavePermission?";
            url += "trgdate=" + $("#trgdate").val();
            url += "&dept_id=" + $("#ddl_DepartmentId").val();
            url += "&starttime=" + $("#StartTime").val();
            url += "&endtime=" + $("#EndTime").val();
            var permission = "";
            if ($('#radio1').is(':checked'))
            {
                permission = "1sthalf"
            }
            else
            {
                permission = "2ndhalf"
            }
            url += "&permission=" + permission;
            url += "&desc=" + $("#Description").val();
            url += "&employeeid=" + checkedItems;
            json = GetURLasJSONStr_callback(url, loadjsondata);

        }
        else {
            alert("Employee selection or Shift selection is missing...");
        }
    });


    //------ jqx list box events starts ---------------------------//
    function loademployee(objlstbox) {
        var dept_id = $("#ddl_DepartmentId").val();
        if (dept_id != "") {
            json = GetURLasJSON(fullhost + "/AssignShiftDetails/GetEmp?deptid=" + dept_id)
            var source = [];
            for (i = 0; i <= json.length - 1; i++) {
                var fname = json[i].Ch_FName + '';
                if (fname == 'null')
                    fname = '';
                source[i] = json[i].Ch_EmpId + "|" + json[i].Ch_FName;
            }

            $(objlstbox).jqxListBox(
            {
                filterable: true,
                searchMode: 'containsignorecase',
                filterPlaceHolder: "Type Emp ID or Emp Name",
                width: 250,
                source: source,
                displayMember: "Ch_FName",
                valueMember: "Ch_EmpId",
                checkboxes: true,
                height: 250
            });

            $('#btnclear').on('click', function () {
                $("#lstemp").jqxListBox('uncheckAll');
            });

            $(objlstbox).on('checkChange', function (event) {
                var args = event.args;
                var items = $(objlstbox).jqxListBox('getCheckedItems');
                checkedItems = "";
                var value = ""
                $.each(items, function (index) {
                    value = this.label;
                    value = "''" + value.split('|')[0] + "''";
                    if (index < items.length - 1) {
                        checkedItems += value + ", ";
                    }
                    else checkedItems += value;
                });
                //$("#selectedemp").text(checkedItems);
            });
        }

    }

    //------ jqx list box events ends ---------------------------//

    //function load_shifttimings(json) {
    //    $("#StartTime").val(json.Sftd_StartTime);
    //    $("#EndTime").val(json.Sftd_EndTime);


    //}

    function loadjsondata(jsonstr) {
        if (jsonstr == "OK") {
            alert("Record Updated Successfully!");
        }
        else {
            alert("Record NOT updated Successfully!");
        }

    }
});