
function disablePage() {
    document.getElementById('bodyFields').disabled = true;
    document.getElementById('submitBtn').disabled = true;
    
    //document.getElementById('btnReloadPage').style.display = '';
}

function showErrorPopUp(message) {
    var messages = message.split(",");
    var errorTitle = '';

    for (var i = 0; i < messages.length; i++) {
        errorTitle += '<li>' + messages[i] + '</li>';
    }

    document.getElementById('exceptionBox').style.display = '';
    document.getElementById('exceptionMessage').innerHTML = '<ul>' + errorTitle + '</ul>';

    
}

function hideErrorPopUp() {
    document.getElementById('exceptionBox').style.display = 'none';

}

function goToControllerAction(controllerName, ActionName, parameter) {
    debugger;
    var url = '/' + controllerName + '/' + ActionName;

    if (parameter != undefined) {
        url += '/' + parameter
    }

    window.location.href = url;
}

function deleteRow(ControllerName, returnActionName, parameter,title) {
    debugger;
    var isConfirmed = confirm("آیا برای حذف  " + title+ ' اطمینان دارید؟')

    if (isConfirmed) {
        var url = '/' + ControllerName + '/delete/' + parameter;
        $.post(url, function (data) {
            debugger;
            search(1);
            //goToControllerAction(ControllerName, returnActionName);
        });
    }
}

function ExportToExcel(controllerName) {
    debugger;
    var grIds = $('#gridIds').val();

    goToControllerAction(controllerName, 'ExportToExcel', grIds)
}
