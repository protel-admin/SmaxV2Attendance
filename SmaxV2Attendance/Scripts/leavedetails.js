$(document).ready(function () {

    var radioval = "0.5";
    $("#srcdate").val(getcurrentdate());
    $("#trgdate").val(getcurrentdate());

    $("#div_collapse").on("hide.bs.collapse", function () {
        $("#head_collapse").html('<span class="glyphicon glyphicon-collapse-down"></span> Open');
    });
    $("#div_collapse").on("show.bs.collapse", function () {
        $("#head_collapse").html('<span class="glyphicon glyphicon-collapse-up"></span> Close');
    });

    $("#ddl_DepartmentId").change(function () {
        $("#lstemp").disabled;
        loademployee("#lstemp");
        $("#jqxWidget").show();
        $("#lstemp").enabled;
    });

    $("#radio1").click(function () {
        radioval = "0.5";
    });

    $("#radio2").click(function () {
        radioval = "1.0";
    });

    $("#btn_View").click(function () {
        if ($("#jqxWidget").is(':visible') == true && typeof checkedItems != "undefined" && checkedItems != "")
        {
            var url = fullhost + "/LeaveDetails/ViewLeaveData?";
            url += "srcdate=" + $("#srcdate").val();
            url += "&trgdate=" + $("#trgdate").val();
            url += "&employeeid=" + checkedItems;
            url += "&keyword=" + $("#Txt_Keyword").val();
            data = GetURLasString(url);
            if(data != "" )
            {
                $("#datagrid1").html(data);
                $("#datagrid1").dataTable();
            }

        }
        else
        {
            alert("Please type some Emp Id or Emp Name to search.")
        }

    });

    $("#btn_submit").click(function () {
        if ($("#jqxWidget").is(':visible') == true && typeof checkedItems != "undefined" && checkedItems != "" && radioval != "" && $("#ddl_Leave").val() != "") {

            var diff = datediff('d', $("#srcdate").val(), $("#trgdate").val())
            if (diff < 365)
            {
                var url = fullhost + "/LeaveDetails/SaveLeaveDetails?";
                url += "srcdate=" + $("#srcdate").val();
                url += "&trgdate=" + $("#trgdate").val();
                url += "&employeeid=" + checkedItems;
                url += "&leavetype=" + $("#ddl_Leave").val();
                url += "&duration=" + radioval;
                url += "&dept=" + $("#ddl_DepartmentId").val();
                url += "&operation=insert";
                json = GetURLasJSONStr_callback(url, loadjsondata);
            }
            else
            {
                alert("Selected date range is greater than 365 days!")
            }

        }
        else {
            alert("Employee selection or Leave Details selection is missing...");
        }
    });

    $("#btn_update").click(function () {
        if ($("#jqxWidget").is(':visible') == true && typeof checkedItems != "undefined" && checkedItems != "" && radioval != "" && $("#ddl_Leave").val() != "") {

            var diff = datediff('d', $("#srcdate").val(), $("#trgdate").val())
            if (diff < 365) {
                var url = fullhost + "/LeaveDetails/SaveLeaveDetails?";
                url += "srcdate=" + $("#srcdate").val();
                url += "&trgdate=" + $("#trgdate").val();
                url += "&employeeid=" + checkedItems;
                url += "&leavetype=" + $("#ddl_Leave").val();
                url += "&duration=" + radioval;
                url += "&dept=" + $("#ddl_DepartmentId").val();
                url += "&operation=update";
                json = GetURLasJSONStr_callback(url, loadjsondata);
            }
            else {
                alert("Selected date range is greater than 365 days!")
            }

        }
        else {
            alert("Employee selection or Leave Details selection is missing...");
        }
    });

    $("#btn_clear").click(function () {
        if ($("#jqxWidget").is(':visible') == true && typeof checkedItems != "undefined" && radioval != "" && checkedItems != "" && $("#ddl_Leave").val() != "") {

            var diff = datediff('d', $("#srcdate").val(), $("#trgdate").val())
            if (diff < 365) {
                var url = fullhost + "/LeaveDetails/SaveLeaveDetails?";
                url += "srcdate=" + $("#srcdate").val();
                url += "&trgdate=" + $("#trgdate").val();
                url += "&employeeid=" + checkedItems;
                url += "&leavetype=" + $("#ddl_Leave").val();
                url += "&duration=" + radioval;
                url += "&dept=" + $("#ddl_DepartmentId").val();
                url += "&operation=delete";
                json = GetURLasJSONStr_callback(url, loadjsondata);
            }
            else {
                alert("Selected date range is greater than 365 days!")
            }

        }
        else {
            alert("Employee selection or Leave Details selection is missing...");
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

   
    function loadjsondata(jsonstr) {
        if (jsonstr == "OK") {
            alert("Record Updated Successfully!");
        }
        else {
            alert("Record NOT updated Successfully!");
        }

    }
});