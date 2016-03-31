$(function () {
    //Setup: Schedule Date Picker
    $("#dtpFrom").datetimepicker();
    $("#dtpTo").datetimepicker({
        useCurrent: false
    });

    $("#dtpFrom").on("dp.change", function (e) {
        $('#dtpTo').data("DateTimePicker").minDate(e.date);
    });

    $("#dtpTo").on("dp.change", function (e) {
        $('#dtpFrom').data("DateTimePicker").maxDate(e.date);
    });

  
    //Function: setEmployeeListDropDown
    function setEmployeeListDropDown() {
        var teamId = $("#hdnTeamIdSelected").val();
        var empId = $("#hdnEmpIdSelected").val();
        var vPath = $("#hdnVPath").val() == "/" ? "" : $("#hdnVPath").val();
        $.ajaxSetup(
            {
                url: teamId != null ? vPath + "/Offline/GetEmployeeTeams/" + teamId + "?EmpId=" + empId : vPath + "/Offline/GetEmployeeTeams/",
                type: "get",
                success: function (result) {
                    $("#ddEmpList").html(result);
                    $("#hdnEmpIdSelected").val($("#ddEmpList").val());
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $("#ddEmpList").html(jqXHR.responseText);
                }
            }
        );
        $.ajax();

    };

    //Setup: ddEmpList 
    setEmployeeListDropDown();

    //Event:  Team dropdown change
    $("#ddTeamList").change(function () {
        var selectedId = $(this).val();
        $("#hdnTeamIdSelected").val(selectedId);

        setEmployeeListDropDown();
    });

    //Event:  EmpList dropdown change
    $("#ddEmpList").change(function () {
        var selectedId = $(this).val();
            $("#hdnEmpIdSelected").val(selectedId);
    });

    //Function: Reset Search Filter
    function resetSearchFilter() {
        $("#txtFilterKeywords").val('');
        $("#ddFilterStatus").val('1'); //Open
        $("#dtpFilterScheduledFrom").val('');
        $('#dtpFilterScheduledTo').val('');
        $('#ddTeamList').val('');
        $('#ddEmpList').val(-1);
        $('#hdnTeamIdSelected').val('');
        $('#hdnEmpIdSelected').val(-1);
    }

    //Event: btnClear
    $("#btnClear").click(function () {
        resetSearchFilter();
    });

    //Event: When enter key is pressed.
    $("input").keypress(function (event) {
        if (event.which == 13) {
            event.preventDefault();
            $("form").submit();
        }
    });

});




