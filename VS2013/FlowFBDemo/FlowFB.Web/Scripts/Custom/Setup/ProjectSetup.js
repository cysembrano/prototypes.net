$(function () {
    $('#tblSetup').editableTableWidget({
        cloneProperties: ['class']
    });

    $('#tblSetup .kvval').on('change', function(evt, newValue) {
        if (isNaN(newValue)) {
            return false; // mark cell as invalid 
        }
        else
        {
            var serviceURL = $("#thisURL").val();

            $.ajax({
                type: "GET",
                url: serviceURL,
                contentType: "application/json; charset=utf-8",
                data: {
                    AP: $('#tblSetup #APIInvoice').html(),
                    GL: $('#tblSetup #CostCenter').html(),
                    Tax: $('#tblSetup #GLCodes').html(),
                    Cost: $('#tblSetup #TaxCodes').html()
                },
                dataType: "json",
                success: function (data, status) { },
                error: function (error) { alert('Error saving project IDs' + error.toString); }
            });

        }
    });

    //Prevent changes from Key
    $('#tblSetup .kvkey').on('validate', function (evt, newValue) {
        if (true) {
            return false; // mark cell as invalid 
        }
    });

});