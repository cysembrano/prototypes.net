$(function () {
    //SETUP CALL: Initialize Employee List
    getEmployeeList();
    setRoleList();

    //EVENT: On team list change refresh Employee list.
    $("#ddTeamList").change(function () {
        $("#hdnSelectedTeamId").val($(this).val());
        getEmployeeList();
    });

    $("#ddRoleChangeEmployee").change(function () {
        $("#hdnRoleSelectedId").val($(this).val());
        setRoleList();
    });

    $("#ddRoleList").change(function () {
        $("#hdnRoleSelectedRole").val($(this).val());
    });

    //FUNCTION: Ajax Employee list
    function getEmployeeList(){
        var teamId = $("#hdnSelectedTeamId").val();
        var vPath = $("#hdnVPath").val() == "/" ? "" : $("#hdnVPath").val();
        if (teamId != null) {
            var ajaxReasonList = {
                url: vPath + "/Setup/GetEmployeeListByTeam/" + teamId,
                type: "get",
                success: function (result) {
                    refreshRoleEmployeeList(result);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $("#errorContainer").html(jqXHR.responseText);
                }
            }
            $.ajax($.extend(true, ajaxReasonList, null));
        }
    }

    //FUNCTION:  On successful ajax update employee select
    function refreshRoleEmployeeList(result) {
        $("#ddRoleChangeEmployee").html(result);
    }

    function setRoleList() {
        $("#ddRoleList option").filter(function () {
            var id = $("#hdnRoleSelectedId").val().split("|");
            if (id[1])
                return $(this).val() == id[1];
            else
                return $(this).val() == 0;
        }).attr('selected', true);
    }
    
});