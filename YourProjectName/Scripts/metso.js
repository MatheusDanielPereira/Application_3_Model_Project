function getDateFormat() {
    var nameEQ = "_datepickerformat=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

function getThousandSeparator() {
    var nameEQ = "_thousandseparator=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

function showLoading(msg) {
    if ($('#loadMe').length == 0) {
        $('html').append("<div class='modal fade' id='loadMe' tabindex='-1' role='dialog' aria-labelledby='loadMeLabel'> <div class='modal-dialog modal-sm' role='document'> <div class='modal-content'> <div class='modal-body text-center'> <img alt='' src='Content/images/loading2.gif' /> <p style='font-weight:bold;margin-top:10px;color:#3d3737;font-size:18px;'>" + msg + "</p></div></div></div></div>");
        $("#loadMe").modal({
            backdrop: "static",
            keyboard: false,
            show: true
        });

        $('#loadMe').on('hidden.bs.modal', function (e) {
            $("#loadMe").remove();
        })
    }
}

function hideLoading() {
    $("#loadMe").modal('hide');
}
