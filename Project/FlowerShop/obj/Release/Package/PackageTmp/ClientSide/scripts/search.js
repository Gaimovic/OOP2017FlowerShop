$(function () {

    search.onReady();
});

var search = (function (me) {

    function initAjax() {
       
        $(document).on("click", ".search-button", function () {

            let $this = $(this);
            console.log($this.data("url"));
            let search = $("input.form-control").val().toString();

            console.log(search);
            //$.ajax({
            //    url: $this.data("url"),
            //    data: { search: search},
            //    method: "POST",
            //    success: function (result) {
                  
            //        window.location.href = $this.data("url");
            //    }
            //});
        });

    }


    me.onReady = function () {
        initAjax();
    };

    return me;

})(search || {});