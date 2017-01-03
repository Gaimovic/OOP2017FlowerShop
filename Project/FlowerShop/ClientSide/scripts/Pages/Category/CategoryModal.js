//$(document).ready(function () {
//    CategoryModal.onReady();
//});


//let CategoryModal = (function (main) {

//    function initAction() {

//        $(document).on("click", ".btn-edit", function (event) {

//            event.preventDefault();

//            // $(".field-category-list").find(".close-field").fadeIn(3000);

//            event.stopPropagation();

//            let $this = $(this);

//            $.ajax({
//                url: $this.attr("href"),
//                method: "GET",
//                success: function (response) {
//                    $(document).find(".modal-body").html(response);
//                }
//            });


//        });
//    }

//    var me = {};

//    me.onReady = function () {

//        initAction();
//    };

//    return me;

//})(main);
