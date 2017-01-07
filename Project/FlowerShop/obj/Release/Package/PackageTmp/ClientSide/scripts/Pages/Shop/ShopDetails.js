$(document).ready(function () {
    ShopDetails.onReady();
});


let ShopDetails = (function (main) {


    function initAction() {

        $(".images").on("click", "img", function () {

            let $this = $(this);
            let content = $(".modal-content");

            let image = $this.attr("src");

            content.html("<img/>");
            content.find("img").css({"height": "500px", "width":"600px"})
            content.find("img").attr("src", image);

            $("#myModal").modal("show");
        });

        $(".modal-content").on("click", "img", function () {

            $("#myModal").modal("hide");
        });
    }


    var me = {};

    me.onReady = function () {

        initAction();

    };

    return me;

})(main);
