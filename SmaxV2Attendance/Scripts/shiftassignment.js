$(document).ready(function () {

    $("#div_daterange").show();
    //$("#radio2").selected;
    $("#srcdate").val(getcurrentdate());
    $("#trgdate").val(getcurrentdate());

    $("#ddl_ShiftId").change(function () {
        var shift_id = $("#ddl_ShiftId").val();
        json = GetURLasJSON(fullhost + "/AssignShiftDetails/GetShiftTime?shiftid=" + shift_id)
        $("#StartTime").val(json.Sftd_StartTime);
        $("#EndTime").val(json.Sftd_EndTime);
    });

    $("#ddl_DepartmentId").change(function () {
        $("#lstemp").disabled;
        loademployee("#lstemp");
        $("#jqxWidget").show();
        $("#lstemp").enabled;
    });

    $("#radio1").click(function () {
        $("#div_monthyear").show();
        $("#div_daterange").hide();
    });

    $("#radio2").click(function () {
        $("#div_monthyear").hide();
        $("#div_daterange").show();
    });

    $("#btn_submit").click(function () {
        if ($("#jqxWidget").is(':visible') == true && typeof checkedItems != "undefined" && checkedItems != "" && $("#ddl_ShiftId").val() != "")
        {
            var url = fullhost + "/AssignShiftDetails/SaveShiftAssignment?";
            url += "srcdate=" + $("#srcdate").val();
            url += "&trgdate=" + $("#trgdate").val();
            url += "&shiftid=" + $("#ddl_ShiftId").val();
            url += "&employeeid=" + checkedItems;
            //url += "&fromtime=" + $("#StartTime").val();
            //url += "&totime=" + $("#EndTime").val();
            json = GetURLasJSONStr_callback(url, loadjsondata);

        }
        else
        {
            alert("Employee selection or Shift selection is missing...");
        }
    });

    
    //------ jqx list box events starts ---------------------------//
    function loademployee(objlstbox)
    {
        var dept_id = $("#ddl_DepartmentId").val();
        if (dept_id != "")
        {
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

    function load_shifttimings(json) {
        $("#StartTime").val(json.Sftd_StartTime);
        $("#EndTime").val(json.Sftd_EndTime);


    }

    function loadjsondata(jsonstr)
    {
        if (jsonstr == "OK")
        {
            alert("Record Updated Successfully!");
        }
        else
        {
            alert("Record NOT updated Successfully!");
        }
        
    }
});