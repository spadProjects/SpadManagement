
(function () {
    var pluginName = 'autosave';

    var timeOutId = 0,
        delay = 0.4, // in seconds || CKEDITOR.config.autosave_delay
        ajaxActive = false;

    var startTimer = function (event) {
        if (timeOutId) {
            clearTimeout(timeOutId);
        }
        timeOutId = setTimeout(onTimer, delay * 1000, event);
    }

    var onTimer = function (event) {
        if (ajaxActive) {
            startTimer(event);
            ajaxActive = false;
        }
        else {
            //ajaxActive = true;
            //updateRequest();
            //console.log("auto save working...");
        }
    }

    CKEDITOR.plugins.add(pluginName, {
        init: function (editor) {
            //editor.on('key', startTimer);
        }
    });
})();