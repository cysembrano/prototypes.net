$(function () {
    //Setup:  On Document ready, call these functions.
    refreshReasonList();
    setReasonControlButtons();
    resetElements();

    //Event: Team List drop down Change
    $("#ddTeamList").change(function () {
        var selectedTeamId = $(this).val();
        $("#hdnTeamIdSelected").val(selectedTeamId);
        resetElements();
        refreshReasonList();
    });

    //Function: AJAX Refresh reason list
    function refreshReasonList() {
        var teamId = $("#hdnTeamIdSelected").val();
        var vPath = $("#hdnVPath").val() == "/" ? "" : $("#hdnVPath").val();
        if (teamId != null) {
            var ajaxReasonList = {
                url: vPath + "/Callback/GetReasonListByTeamSettings/" + teamId,
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

    //Function: Reset elements
    function resetElements()
    {
        $("#txtAddReason").val(null);
        $("#hdnReasonSelectedId").val(null);
        $("#smlSelectedReasonText").html(null);
    }

    //Function:  On successful ajax call for reasons
    function refreshReasonListSuccess(result) {
        $("#selReasonList").html(result);
        setReasonControlButtons();
    }

    //Event:  On reason add click, change href
    $("#btnReasonAdd").click(function () {
        var teamId = $("#hdnTeamIdSelected").val();
        var txtAddReasonVal = $("#txtAddReason").val();
        var href = $(this).attr("href");
        var route = href + teamId + "?reason=" + txtAddReasonVal;
        $(this).attr("href", route);
    });

    //Event:  On reason delete click, change href
    $("#btnReasonDelete").click(function () {
        var teamId = $("#hdnTeamIdSelected").val();
        var selectedReasonId = $("#hdnReasonSelectedId").val();
        var href = $(this).attr("href");
        var route = href + teamId + "?reasonId=" + selectedReasonId;
        $(this).attr("href", route);
    });

    //Event:  On reason save click, change href
    $("#btnReasonSave").click(function () {
        var teamId = $("#hdnTeamIdSelected").val();
        var selectedReasonId = $("#hdnReasonSelectedId").val();
        var txtAddReasonVal = $("#txtAddReason").val();
        var href = $(this).attr("href");
        var route = href + teamId + "?reasonId=" + selectedReasonId + "&reason=" + txtAddReasonVal;
        $(this).attr("href", route);
    });

    //Event:  On reason textbox being typed on
    $("#txtAddReason").keyup(function () {
        setElementDisabled($.trim($("#txtAddReason").val()) == "", "#btnReasonAdd");
        setElementDisabled($("#hdnReasonSelectedId").val().length < 1 || $.trim($("#txtAddReason").val()) == "", "#btnReasonSave");
        setElementDisabled($("#hdnReasonSelectedId").val().length < 1, "#btnReasonDelete");
    });

});

//Function:  When select for reason list changes.
function reasonListChange() {
    var reasonId = $("#selReasonList option:selected").val();
    if (typeof reasonId !== 'undefined') {
        var reasonText = $("#selReasonList option:selected").text();
        $("#txtAddReason").val(reasonText);
        $("#smlSelectedReasonText").html("Selected: " + reasonText);
        $("#hdnReasonSelectedId").val(reasonId);
        setReasonControlButtons();
    }
}

//Function:  Sets disabled properties of reason control buttons
function setReasonControlButtons() {
    //Setup:  Reason Save button disabled if no reason is selected
    setElementDisabled($("#hdnReasonSelectedId").val().length < 1, "#btnReasonSave");

    //Setup: set Delete button disabled if no select is available
    setElementDisabled($("#hdnReasonSelectedId").val().length < 1, "#btnReasonDelete");

    //Setup: set Add button disabled if textbox is empty
    setElementDisabled($.trim($("#txtAddReason").val()) == "", "#btnReasonAdd");
}

//Function: setElement enabled or disabled with given condition
function setElementDisabled(condition, elementId) {
    if (condition)
        $(elementId).attr("disabled", "disabled");
    else
        $(elementId).removeAttr("disabled");

}