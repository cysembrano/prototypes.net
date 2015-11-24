$(function () {
    $("#savePwdForm").validate({
        rules: {
            "OldPwd": {
                required: true
            },
            "NewPwd": {
                required: true
            }
        },
        messages: {
            "OldPwd": {
                required: "&nbsp;&nbsp;This field is required."
            },
            "NewPwd": {
                required: "&nbsp;&nbsp;This field is required."
            }
        },
        submitHandler: function (form) {
            form.submit();
        },
        errorLabelContainer: "#savePwdFormErrContainer",
        highlight: function (label) {
            $(label).closest(".form-group").addClass('has-error');
            $(label).addClass("text-danger");
        },
        success: function (error, element) {
            $(element).closest(".form-group").removeClass('has-error');
        }

    });

    //SETUP: ddProfileChangeTimezone change
    $("#ddProfileChangeTimezone").change(function () {
        $("#hdnSelectedTimeZone").val($(this).val());
    });
});