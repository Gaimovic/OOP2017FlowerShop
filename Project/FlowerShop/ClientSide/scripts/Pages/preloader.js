$(document).ready(function () {

    if (window.addEventListener) {

        window.addEventListener('load', function () {

            let overlay = $(".overlay");

            overlay.fadeOut();

        });

    } else if (window.attachEvent) {

        window.attachEvent('onload', function () {

            let overlay = $(".overlay");

            overlay.remove();

        });
    }
    

});

