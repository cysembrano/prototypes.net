$(function () {

    function stringfyTable() {
        var myRows = { myRows: [] };
        var $th = $('table th');
        $('table tbody tr').each(function (i, tr) {
            var obj = {}, $tds = $(tr).find('td');
            $th.each(function (index, th) {
                obj[$(th).text()] = $tds.find('input').eq(index).val();
            });
            myRows.myRows.push(obj);
        });
        return JSON.stringify(myRows);
    };

    function saveTableCodes(JSONvalues) {
        var serviceURL = $("#SaveCodesUrl").val();

        $.ajax({
            type: "POST",  
            url: serviceURL,
            contentType: 'application/json; charset=UTF-8',
            data: JSONvalues
        });
    }

    $(document).ajaxStop(function () {
        setTimeout(function () { window.location.reload()}, 100);
    });

   $(document).on('click', '.btn-add', function (e) {
        e.preventDefault();

        var controlForm = $(this).closest('table'),
            currentEntry = $(this).parents('tr:first'),
            newEntry = $(currentEntry.clone()).appendTo(controlForm);

        newEntry.find('input').val('');
        controlForm.find('tr:not(:last) .btn-add')
            .removeClass('btn-add').addClass('btn-remove')
            .removeClass('btn-success').addClass('btn-danger')
            .html('<span class="icon-minus-sign"></span>');        

    }).on('click', '.btn-remove', function (e) {
        $(this).parents('tr:first').remove();
        e.preventDefault();
        return false;
    });

   $('#btnSaveCodes').click(function () {
       saveTableCodes(stringfyTable());
   });

});


