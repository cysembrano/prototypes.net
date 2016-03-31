$(function () {
    //Event: Team List drop down Change
    $("#ddTeamList").change(function () {
        var selectedTeamId = $(this).val();
        $("#hdnTeamId").val(selectedTeamId);

        resetElements('Contact');
        refreshDropDownList('Contact');

        resetElements('Activity');
        refreshDropDownList('Activity');
    });

    //Function: AJAX Refresh reason list
    function refreshDropDownList(ddHdnId, ddId, ActionName) {
        var teamId = $("#hdnTeamId").val();
        var vPath = $("#hdnVPath").val() == "/" ? "" : $("#hdnVPath").val();
        var selectedId = $("#" + ddHdnId).val() == "" ? "-1" : $("#" + ddHdnId).val();
        if (teamId != null) {
            var ajaxReasonList = {
                url: vPath + "/Offline/" + ActionName + "/" + teamId + "?selected=" + selectedId + "&addOne=false",
                type: "get",
                success: function (result) {
                    refreshDropDownListSuccess(ddId, ActionName, result);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $("#errorContainer").html(jqXHR.responseText);
                }
            }

            $.ajax($.extend(true, ajaxReasonList, null));
        }
    }

    //Function:  On successful ajax call for reasons
    function refreshDropDownListSuccess(ddId, ActionName, result) {
        $("#" + ddId).html(result);
        if (ActionName == "GetContactTypesByTeam") {
            setControlButtons('Contact');
            setControlButtons('Activity');
        }
    }

    //Function: Reset elements
    function resetElements(dataType) {
        $("#txtAdd" + dataType + "Type").val(null);
        $("#smlSelected" + dataType + "TypeText").val(null);
        $("#hdn" + dataType + "TypeId").html(null);
    }

    function AddType(dataType) {
        var teamId = $("#hdnTeamId").val();
        var txtAddVal = $("#txtAdd" + dataType + "Type").val();
        var href = $("#btn" + dataType + "TypeAdd").attr("href");
        var route = href + teamId + "?" + dataType + "Type=" + txtAddVal;
        $("#btn" + dataType + "TypeAdd").attr("href", route);
    }

    function SaveType(dataType) {
        var teamId = $("#hdnTeamId").val();
        var selectedId = $("#hdn" + dataType + "TypeId").val();
        var txtAddVal = $("#txtAdd" + dataType + "Type").val();
        var href = $("#btn" + dataType + "TypeSave").attr("href");
        var route = href + teamId + "?" + dataType + "Type=" + txtAddVal + "&" + dataType + "TypeId=" + selectedId;
        $("#btn" + dataType + "TypeSave").attr("href", route);
    }

    function DeleteType(dataType) {
        var teamId = $("#hdnTeamId").val();
        var selectedId = $("#hdn" + dataType + "TypeId").val();
        var href = $("#btn" + dataType + "TypeDelete").attr("href");
        var route = href + teamId + "?" + dataType + "TypeId=" + selectedId;
        $("#btn" + dataType + "TypeDelete").attr("href", route);
    }

    //Event:  On contact add click, change href
    $("#btnContactTypeAdd").click(function () {
        AddType('Contact');
    });

    //Event:  On activity add click, change href
    $("#btnActivityTypeAdd").click(function () {
        AddType('Activity');
    });

    //Event:  On contact save click, change href
    $("#btnContactTypeSave").click(function () {
        SaveType('Contact');
    });

    //Event:  On activity save click, change href
    $("#btnActivityTypeSave").click(function () {
        SaveType('Activity');
    });

    //Event:  On contact save click, change href
    $("#btnContactTypeDelete").click(function () {
        DeleteType('Contact');
    });

    //Event:  On activity save click, change href
    $("#btnActivityTypeDelete").click(function () {
        DeleteType('Activity');
    });

    function setTypeDisabled(dataType) {
        setElementDisabled($.trim($("#txtAdd" + dataType + "Type").val()) == "", "#btn" + dataType + "TypeAdd");
        setElementDisabled($("#hdn" + dataType + "TypeId").val().length < 1 || $.trim($("#txtAdd" + dataType + "Type").val()) == "", "#btn" + dataType + "TypeSave");
        setElementDisabled($("#hdn" + dataType + "TypeId").val().length < 1, "#btn" + dataType + "TypeDelete");
    }

    ////Event:  On reason textbox being typed on
    $("#txtAddContactType").keyup(function () {
        setTypeDisabled('Contact');
    });

    ////Event:  On reason textbox being typed on
    $("#txtAddActivityType").keyup(function () {
        setTypeDisabled('Activity');
    });


    refreshDropDownList('hdnContactTypeId', 'selContactType', 'GetContactTypesByTeam');
    resetElements('Contact');
    setControlButtons('Contact');

    refreshDropDownList('hdnActivityTypeId', 'selActivityType', 'GetActivityTypesByTeam');
    resetElements('Activity');
    setControlButtons('Activity');

});


//Function:  When select for reason list changes.
function typeListChange(dataType) {
    var Id = $("#sel" + dataType + "Type option:selected").val();
    if (typeof Id !== 'undefined') {
        var Text = $("#sel" + dataType + "Type option:selected").text();
        $("#txtAdd" + dataType + "Type").val(Text);
        $("#smlSelected" + dataType + "TypeText").html("Selected: " + Text);
        $("#hdn" + dataType + "TypeId").val(Id);
        setControlButtons(dataType);
    }
}

//Function:  Sets disabled properties of reason control buttons
function setControlButtons(dataType) {
    //Setup:  Reason Save button disabled if no reason is selected
    setElementDisabled($("#hdn" + dataType + "TypeId").val().length < 1, "#btn" + dataType + "TypeSave");

    //Setup: set Delete button disabled if no select is available
    setElementDisabled($("#hdn" + dataType + "TypeId").val().length < 1, "#btn" + dataType + "TypeDelete");

    //Setup: set Add button disabled if textbox is empty
    setElementDisabled($.trim($("#txtAdd" + dataType + "Type").val()) == "", "#btn" + dataType + "TypeAdd");
}

//Function: setElement enabled or disabled with given condition
function setElementDisabled(condition, elementId) {
    if (condition)
        $(elementId).attr("disabled", "disabled");
    else
        $(elementId).removeAttr("disabled");

}