$(document).ready(function () {

    var l = $('.ladda-button').ladda();

    l.click(function () {
        // Start loading
        l.ladda('start');
        $("#inputForm").submit();
        // Timeout example
        // Do something in backend and then stop ladda
        setTimeout(function() {
                l.ladda('stop');
            },
            500);


    });
});