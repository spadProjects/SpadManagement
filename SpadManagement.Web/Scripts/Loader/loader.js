function startLoader() {
    setTimeout(function () {
        $('body').removeClass('loaded');
        //document.getElementById("entry-header").style.display = '';
        //document.getElementById("titleReceive").style.display = 'none';
        //document.getElementById("titleSend").style.display = '';
    }, 0);
}

function endLoader() {
    setTimeout(function () {
        $('body').addClass('loaded');
        //document.getElementById("entry-header").style.display = 'none';
    }, 0);
}
