$(document).ready(function () {
    RegLog.onReady();
});


let RegLog = (function (main) {

    function initAction() {

        $(document).on("click", "#loginLink", function (event) {

            event.preventDefault();

            let $this = $(this);

            $.ajax({
                url: $this.attr("href"),
                method: "GET",
                success: function (response) {
                    $(document).find(".modal-content").html(response);
                }
            });
        });

        $(document).on("click", "#registerLink", function (event) {

            event.preventDefault();

            let $this = $(this);

            console.log($this.attr("href"));

            $.ajax({
                url: $this.attr("href"),
                method: "GET",
                success: function (response) {
                    $(document).find(".modal-content").html(response);
                }
            });
        });


        $(document).on("click", ".close-modal", function () {
            $("#myModal").modal("hide");
        });
    }


    var me = {};

    me.onReady = function () {

        initAction();

    };

    return me;

})(main);
