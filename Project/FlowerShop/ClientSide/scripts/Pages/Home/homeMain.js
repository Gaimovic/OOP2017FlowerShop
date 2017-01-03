$(document).ready(function () {
    HomePage.onReady();
});


let HomePage = (function (main) {

    function initAction() {

        // Init Skrollr
        let s = skrollr.init({
            render: function (data) {
                $("#info").text(data.curTop);
            }
        });

        $(".slide").mouseenter(function () {
            $this = $(this);

        //    console.log("test");
            $this.addClass("picture-animate");
        });

        $("#slide-1").mouseenter(function () {
            //console.log("test");
            $(".slide-1-hover").fadeIn(5000);  
        });

        $("#slide-2").mouseenter(function () {
            $(".slide-2-hover").fadeIn(5000);
        });
        $("#slide-3").mouseenter(function () {
            $(".slide-3-hover").fadeIn(5000);
        });
        $("#slide-4").mouseenter(function () {
            $(".slide-4-hover").fadeIn(5000);

        });
    }

    var me = {};

    me.onReady = function () {

        initAction();
    };

    return me;

})(main);
