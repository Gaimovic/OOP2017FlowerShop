$(function () {

    main.onReady();
});

var main = (function (me) {

    function initAjax() {
        $.ajaxSetup({
            error: function (jqXHR, exception) {
                if (jqXHR.status === 0) {
                    showError('This resource is not available. Please verify your connection.');
                } else if (jqXHR.status === 404) {
                    showError('Requested page is not found.');
                } else if (exception === 'timeout') {
                    showError('Request timed out.');
                } else if (exception === 'abort') {
                    showError('Request aborted.');
                } else {
                    showError('Server error has occured.');
                    showError(jqXHR);
                    showError(exception);
                }
            }
        });

        $(document).ajaxSend(function (event, request, settings) {
            //if (settings.showLoader !== false)
            //    loader.show();
        });

        $(document).ajaxComplete(function (event, request, settings) {
            //if (settings.showLoader !== false)
            //    loader.hide();
        });
        
    }

    function showError(text) {

        console.log(text);
    }

    me.showError = showError;

    me.onReady = function () {
        initAjax();
    };

    return me;

})(main || {});