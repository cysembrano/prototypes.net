$(function () {
    $("#loginForm").validate({
        rules: {
            EmailAddress: {
                required: true,
                email: true
            }
        },
        messages: {
            EmailAddress: {
                required: "Email: This field is required.",
                email: "Email:  Incorrect format."
            }
        },
        submitHandler: function (form) {
            form.submit();
        },
        errorLabelContainer: "#errorContainer",
        highlight: function (label) {
            $(label).closest(".input-group").addClass('has-error');
            $(label).addClass("text-danger");
        },
        success: function (error, element) {
            $(element).closest(".input-group").removeClass('has-error');
        }
 
    });
});

$("#btnLogin").click(function () {
    $("#errorContainer").empty();
});

$("#btnSendPassword").click(function () {
    $("#errorContainer").empty();
});





