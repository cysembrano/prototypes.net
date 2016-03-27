$(function () {
    $("a.clear").click(function () {
        if (confirm('Are you sure you want to clear?')) {
            $(":text").val('');
        }
    });

    //SETUP: Validations
    $("#callbackForm").validate({
        rules: {
            "Callback.CustomerName": {
                required: true
            },
            "Callback.Contact1Phone": {
                required: true
            },
            "Callback.CustomerCallbackTimeStart": {
                required: true
            },
            "Callback.AgentCallbackTimeStart": {
                required: true
            },
            "selReasonList": {
                required: true,
                min: 0
            }

        },
        messages: {
            "Callback.CustomerName": {
                required: "Customer Name: This field is required."
            },
            "Callback.Contact1Phone": {
                required: "Contact 1: This field is required."
            },
            "Callback.CustomerCallbackTimeStart": {
                required: "Customer Start Time: This field is required."
            },
            "Callback.AgentCallbackTimeStart": {
                required: "Agent Start Time: This field is required."
            },
            "selReasonList": {
                required: "Callback Reason: This field is required.",
                min: "Callback Reason: This field is required."
            }
        },
        submitHandler: function (form) {
            form.submit();
        },
        errorLabelContainer: "#errorContainer",
        highlight: function (label) {
            $(label).closest(".form-group").addClass('has-error');
            $(label).addClass("text-danger");
        },
        success: function (error, element) {
            $(element).closest(".form-group").removeClass('has-error');
        }

    });

    //SETUP: DateTimePickers
    $('#dtpCustSched').datetimepicker({
        stepping: 15
    });
    $('#dtpAgentSched').datetimepicker({
        stepping: 15
    });

    $("#dtpCustSched").on("dp.change", function (e) {
        syncTimezone(e.date, "CUST");
    });

    $("#dtpAgentSched").on("dp.change", function (e) {
        syncTimezone(e.date, "AGENT");
    });

    //CALL: On Document Ready Functions
    refreshReasonList();
    setEmployeeListDropDown();

    //FUNCTION: Syncs timezones between Agent and Cust.
    function syncTimezone(currentValue, actor) {
        var tzDiff, toConvert;
        var tzAgent = parseFloat($("#ddAgentTimeZone").val());
        var tzCust = parseFloat($("#ddCustTimeZone").val());
        tzDiff = Math.abs(tzAgent - tzCust);

        if (actor == "AGENT") {
            toConvert = "#dtpInputCustSched";
            if (tzAgent > tzCust)
                $(toConvert).val(currentValue.subtract(tzDiff, 'hours').format("MM/DD/YYYY h:mm A"));
            else
                $(toConvert).val(currentValue.add(tzDiff, 'hours').format("MM/DD/YYYY h:mm A"));
        }
        else if (actor == "CUST") {
            toConvert = "#dtpInputAgentSched";
            if (tzCust > tzAgent)
                $(toConvert).val(currentValue.subtract(tzDiff, 'hours').format("MM/DD/YYYY h:mm A"));
            else
                $(toConvert).val(currentValue.add(tzDiff, 'hours').format("MM/DD/YYYY h:mm A"));
        }
    }

    //EVENT: AgentTimeZone DropDown
    $("#ddAgentTimeZone").change(function () {
        var selfInput = $("#dtpInputAgentSched").val();
        if (selfInput != "")
            syncTimezone(moment(selfInput, "MM/DD/YYYY h:mm A"), "AGENT");
    });
    //EVENT: CustTimeZone DropDown
    $("#ddCustTimeZone").change(function () {
        var selfInput = $("#dtpInputCustSched").val();
        if (selfInput != "")
            syncTimezone(moment(selfInput, "MM/DD/YYYY h:mm A"), "CUST");
    });

    //Function: setEmployeeListDropDown
    function setEmployeeListDropDown() {
        var teamId = $("#hdnTeamIdSelected").val();
        var empId = $("#hdnEmpIdSelected").val();
        var vPath = $("#hdnVPath").val() == "/" ? "" : $("#hdnVPath").val();
        $.ajaxSetup(
            {
                url: teamId != null ? vPath + "/Callback/GetEmployeeTeams/" + teamId + "?EmpId=" + empId + "&anyOption=false"
                    : vPath + "/Callback/GetEmployeeTeams/?anyOption=false",
                type: "get",
                success: function (result) {
                    $("#ddCallbackShowAgent").html(result);
                    $("#hdnEmpIdSelected").val($("#ddCallbackShowAgent").val());
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $("#ddCallbackShowAgent").html(jqXHR.responseText);
                }
            }
        );
        $.ajax();

    };

    //Event:  Team dropdown change
    $("#ddTeamList").change(function () {
        $("#hdnTeamIdSelected").val($(this).val());
        $("#hdnTeamIdSelected").val($("#ddTeamList option:selected").text());
        refreshReasonList();
        setEmployeeListDropDown();
    });

    //Event:  Employee dropdown change
    $("#ddCallbackShowAgent").change(function () {
        $("#hdnEmpIdSelected").val($(this).val());
        $("#hdnEmpNameSelected").val($("#ddCallbackShowAgent option:selected").text());
    });


    //Event:  EmpList dropdown change
    $("#ddCallbackShowAgent").change(function () {
        var selectedId = $(this).val();
        $("#hdnEmpIdSelected").val(selectedId);
    });

    //Function: AJAX Refresh reason list
    function refreshReasonList() {
        var teamId = $("#hdnTeamIdSelected").val();
        var vPath = $("#hdnVPath").val() == "/" ? "" : $("#hdnVPath").val();
        var selectedId = $("#hdnReasonSelectedId").val() == "" ? "-1" : $("#hdnReasonSelectedId").val();
        if (teamId != null) {
            var ajaxReasonList = {
                url: vPath + "/Callback/GetReasonListByTeamShow/" + teamId + "?selected=" + selectedId,
                type: "get",
                success: function (result) {
                    refreshReasonListSuccess(result);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $("#errorContainer").html(jqXHR.responseText);
                }
            }

            $.ajax($.extend(true, ajaxReasonList, null));
        }
    }

    //Function:  On successful ajax call for reasons
    function refreshReasonListSuccess(result) {
        $("#selReasonList").html(result);
    }
});

//Function:  When select for reason list changes.
function reasonListChange() {
    $("#hdnReasonSelectedId").val($("#selReasonList").val());
    $("#hdnReasonSelectedText").val($("#selReasonList option:selected").text());
}
