$(document).ready(function () {
    CategoryList.onReady();
});


let CategoryList = (function (main) {

    function initAction() {
         
        $(".box-circle").mouseover(function () {
            $this = $(this);

            $this.find("img").addClass("imageAction");
        });

        $(".box-circle").mouseleave(function () {
            $this = $(this);

            $this.find("img").removeClass("imageAction");
        });

    }

    var me = {};

    me.onReady = function () {

        initAction();
    };

    return me;

})(main);
