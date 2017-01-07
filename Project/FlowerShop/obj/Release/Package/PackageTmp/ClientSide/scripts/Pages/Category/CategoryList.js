$(document).ready(function () {
    ProductList.onReady();
});


let ProductList = (function (main) {

    // DELETE , EDIT, ADD(Dropzone on picture upload)
    function initAction() {

        //Ajax DELETE
        
        $(document).on("click", ".btn-delete", function (event) {

            event.preventDefault();

            let $this = $(this);


            $.ajax({
                url: $this.attr("href"),
                method: "POST",
                success: function (result) {
                    if (result.success) {
                        $this.closest("tr").remove();
                    } else {
                        alert("Mistake could not Delete!");
                    }
                }

            });
        });

        ////Ajax Edit
        $(document).on("click", ".btn-edit", function (event) {

            alart("test");
            event.preventDefault();

            event.stopPropagation();

            let $this = $(this);

            $.ajax({
                url: $this.attr("href"),
                method: "GET",
                success: function (response) {
                    $(document).find(".modal-content").html(response);
                }
            });
        });
        //Ajax Add

        $(document).on("click", ".btn-add", function (event) {
         
            event.preventDefault();

            let $this = $(this);

            event.stopPropagation();

            $.ajax({
                url: $this.attr("href"),
                method: "GET",
                success: function (response) {

                    $(document).find(".modal-content").html(response);

                }
            });

        });

        //Close Modal
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
