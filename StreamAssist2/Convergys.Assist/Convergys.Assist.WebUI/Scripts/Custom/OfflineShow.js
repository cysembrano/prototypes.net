$(function () {
    //CLEAR
    $("a.clear").click(function () {
        if (confirm('Are you sure you want to clear?')) {
            $(":text").val('');
        }
    });

    //SETUP: Validations
    $("#offlineForm").validate({
        rules: {
            "Offline.CaseIdentity": {
                required: true
            },
            "selContactType": {
                required: true,
                min: 0
            },
            "selActivityType": {
                required: true,
                min: 0
            }
        },
        messages: {
            "Offline.CaseIdentity": {
                required: "Case Identity: This field is required."
            },
            "selContactType": {
                required: "Contact Type: This field is required.",
                min: "Contact Type: This field is required."
            },
            "selActivityType": {
                required: "Activity Type: This field is required.",
                min: "Activity Type: This field is required."
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


    //SETUP: Timer
    $('#TimerStop').click(function () {
        $("#TimerPanel").attr('class', 'panel panel-success');
        var seconds = $('#Timer').data('seconds');
        var empid = $("#hdnEmpId").val();
        var logid = $("#Offline_OfflineLogId").val();
        if (seconds != null) {
            var duration = $('#Timer').data('duration');

            var endTime = new Date();

            var comments = $('#OfflineEvent_Comments').val();

            //Set status 0 = default, 1 = success, 2=warning, 3=danger
            var status;
            if (seconds >= duration * 2) { status = "danger"; }
            else if (seconds < duration) { status = "success" }
            else { status = "warning" };

            var ofEvent = {
                endTime: endTime,
                comments: comments,
                elapsedTime: GetTimeElapsed(seconds),
                status: status,
                logid: logid
            };

            $('#Timer').timer('remove');
            $('#OfflineEvent_Comments').val('');
            SaveOfflineEvent(ofEvent);
        }
    });
    $('#TimerPlay').click(function () {
        if ($('#Timer').data('seconds') == null || $('#Timer').data('seconds') == 0) {
            $('title').html('Offline Start')
            $('#Timer').timer({
                format: '%H:%M:%S',
                duration: '30m',
                callback: function () {
                    var duration = $('#Timer').data('duration');
                    var seconds = $('#Timer').data('seconds');
                    if (duration == seconds) {
                        $("#TimerPanel").attr('class', 'panel panel-warning');
                        $("title").html('Warn: ' + seconds + 's')
                    }
                    else {
                        $("#TimerPanel").attr('class', 'panel panel-danger');
                        $("title").html('Danger: ' + seconds + 's')
                    }
                },
                repeat: true,
            });
        }
        else {
            $('#Timer').timer('resume');
        }

    });
    $('#TimerPause').click(function () {
        var sec = $('#Timer').data('seconds');
        if (typeof sec !== "undefined") {
            $('#Timer').timer('pause');
        }

    });

    //Get Time Elapsed expressed int HH:MM:SS
    function GetTimeElapsed(time) {
        // Hours, minutes and seconds
        var hrs = ~~(time / 3600);
        var mins = ~~((time % 3600) / 60);
        var secs = time % 60;

        // Output like "1:01" or "4:03:59" or "123:03:59"
        ret = (hrs < 10 && hrs != 0 ? "0" : "");
        ret += (hrs == 0 ? "00" : hrs) + ":" + (mins < 10 && mins != 0 ? "0" : "");
        ret += (mins == 0 ? "00" : mins) + ":" + (secs < 10 && secs != 0 ? "0" : "");
        ret += (secs == 0 ? "00" : secs);
        return ret;
    }

    function SaveOfflineEvent(ofEvent) {
        var vPath = $("#hdnVPath").val() == "/" ? "" : $("#hdnVPath").val();
        var ajaxReasonList = {
            url: vPath + "/Offline/SaveEvent/",
            type: "post",
            data: JSON.stringify(ofEvent),
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                var eventJSONString = $('#JSONOfflineEvents').val();
                var ofEvents;
                if (eventJSONString == "") { ofEvents = new Array(); }
                else { ofEvents = JSON.parse(eventJSONString); }
                ofEvents.push(result);
                $('#JSONOfflineEvents').val(JSON.stringify(ofEvents));
                RefreshOfflineEventTable();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $("#errorContainer").html(jqXHR.responseText);
            }
        }

        $.ajax($.extend(true, ajaxReasonList, null));
    }

    //Function: AJAX Refresh DropDown list
    function refreshDropDownList(ddHdnId, ddId, ActionName) {
        var teamId = $("#hdnTeamId").val();
        var vPath = $("#hdnVPath").val() == "/" ? "" : $("#hdnVPath").val();
        var selectedId = $("#" + ddHdnId).val() == "" ? "-1" : $("#" + ddHdnId).val();
        if (teamId != null) {
            var ajaxReasonList = {
                url: vPath + "/Offline/" + ActionName + "/" + teamId + "?selected=" + selectedId,
                type: "get",
                success: function (result) {
                    refreshDropDownListSuccess(ddId, result);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $("#errorContainer").html(jqXHR.responseText);
                }
            }

            $.ajax($.extend(true, ajaxReasonList, null));
        }
    }

    //Function:  On successful ajax call for reasons
    function refreshDropDownListSuccess(ddId, result) {
        $("#" + ddId).html(result);
    }

    //CALL: On Document Ready Functions
    refreshDropDownList('hdnContactTypeId', 'selContactType', 'GetContactTypesByTeam');
    refreshDropDownList('hdnActivityTypeId', 'selActivityType', 'GetActivityTypesByTeam');
    RefreshOfflineEventTable();
});

//Function:  When select for Contact or Activity Type list changes.
function TypeDropDownChange(ddHdnId, ddHdnText, ddId) {
    $("#" + ddHdnId).val($("#" + ddId).val());
    $("#" + ddHdnText).val($("#" + ddId + " option:selected").text());
}

//Function: Delete offline event
function deleteOfflineEvent(offLogId, offEventId) {
    if (confirm('Are you sure you want to delete this offline event?')) {

        var vPath = $("#hdnVPath").val() == "/" ? "" : $("#hdnVPath").val();
        var jsonParameter = { "logId": offLogId, "eventid": offEventId };
        var ajaxReasonList = {
            url: vPath + "/Offline/DeleteEvent/",
            type: "post",
            data: JSON.stringify(jsonParameter),
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                $('#JSONOfflineEvents').val(JSON.stringify(result));
                RefreshOfflineEventTable();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $("#errorContainer").html(jqXHR.responseText);
            }
        }

        $.ajax($.extend(true, ajaxReasonList, null));
    }
}


//Update Events Table
function RefreshOfflineEventTable() {
    var tbody = $('#EventsTable tbody');
    tbody.html('');
    var eventJSONString = $('#JSONOfflineEvents').val();
    var ofEvents;
    if (eventJSONString == "") { ofEvents = new Array(); }
    else { ofEvents = JSON.parse(eventJSONString); }

    var actionType = $('#ActionType').val();

    $.each(ofEvents, function (i, ofEvent) {
        var row = '<tr class="' + ofEvent["status"] + '"><td>';
        row += ofEvent["comments"] + '</td><td>';
        row += ofEvent["elapsedTime"] + '</td><td>';
        if (actionType == 'View') {
            '</td></tr>';
        } else {
            row += '<a href="#" onclick="deleteOfflineEvent(' + ofEvent["logid"] + ',' + ofEvent["eventid"] + ')">'
            row += '<i class="fa fa-times"></i></a></td></tr>';
        }
        tbody.append(row);
    });
}