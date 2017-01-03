$(document).ready(function () {
    ProductCategoryList.onReady();
});


let ProductCategoryList = (function (main) {

    
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

        //Ajax Edit

        $(document).on("click", ".btn-edit", function (event) {

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

        $(document).on("click","#btnUpload", function (event) {
            
            event.preventDefault();

            console.log("test");
        });

        $(document).on("click", ".btn-add", function (event) {

            event.preventDefault();
            

            let $this = $(this);


            event.stopPropagation();

            $.ajax({
                url: $this.attr("href"),
                method: "GET",
                success: function (response) {

                    $(".modal-content").html(response);

                    // console.log($("#MyId").data("url"));

                    $("div#MyId").dropzone({
                        //url: "/Product/Add",
                        url: $("#MyId").data("url"),
                        paramName: "file",
                        autoProcessQueue: false,
                        uploadMultiple: true,
                        maxFiles: 3,
                        parallelUploads: 20,
                        init: function () {
                            let dropzone = this;
                         
                            let submitButton = document.querySelector("#btnUpload")

                            $("#btnUpload").on("click", function (e) {
                                dropzone.processQueue();
                            });

                            this.on('sending', function (data, xhr, formData) {
                                formData.append("ViewFlowerName", $("#ViewFlowerName").val());
                                formData.append("ViewFlowerDescription", $("#ViewFlowerDescription").val());
                                formData.append("ViewFlowerPrice", $("#ViewFlowerPrice").val());
                                formData.append("ViewCategoryId", $("#ViewCategoryId").val());
                                formData.append("ViewFlowerDiscount", $("#ViewFlowerDiscount").val());
                                formData.append("Special", $("#Special").val());
                            });
                        }

                    });
                    // DROPZONE Initialization
                }
            });

        });
        //Close Modal
        $(document).on("click", ".close-modal", function () {
            //     alert("Test");
            $("#myModal").modal("hide");
        });

        $(document).on('click', ".btn-category-link", function (event) {

            event.preventDefault();
            $this = $(this);
            let searchCategory = $this.data("value");
            $(".btn-category-link").removeClass("sort-category");
            $this.addClass("sort-category");
       
            console.log(searchCategory);
            $.ajax({
                url: $this.data("url"),
                data: { searchCategory: searchCategory },
                method: "POST",
                success: function (result) {
                    $(".tfoot-remove").remove();
                    $(document).find("tbody").replaceWith(result);
                }
            });
        });

    }

    function TableSorting() {
        let $search = $(".btn-search");
        let $table = $(".table.table-with-sorting");
        let searchString = $("input#searchbox").val().toString();

        console.log(searchString);

        let sortDirections = {
            Ascending: 0,
            Discending: 1
        }

        //Sorting

        $table.on("click", "th.sortable", function (event) {
            let searchCategory = $(document).find(".btn-category-link.sort-category").data("value");
            let $this = $(this);
            console.log(searchCategory);
            let page = $(".paging-numper").html();
            // console.log(page);
            let SortDirection = $this.data("sort-direction");
            let $columns = $table.find("th.sortable");

            let fields = [];

            $columns.removeClass("is-sortable");
            $this.addClass("is-sortable");

            if (SortDirection === undefined || SortDirection.lenght === 0)
                SortDirection = sortDirections.Ascending;
            else if (SortDirection == sortDirections.Ascending)
                SortDirection = sortDirections.Discending;
            else
                SortDirection = sortDirections.Ascending;

            $this.data("sort-direction", SortDirection);

            $columns.each(function (i) {
                //  alert("text");
                let $column = $($columns[i]);
                let field = {
                    Name: $column.data("field-name"),
                    Sort: $column.hasClass("is-sortable"),
                    SortDirection: SortDirection
                };
                // alert(text(field.Name));


                fields.push(field);

            });
            console.log(fields[0]);

            $.ajax({
                url: $table.data("url"),
                data: { fields: fields, searchCategory: searchCategory, searchString: searchString, page: page },
                method: "POST",
                success: function (result) {
                    $(".tfoot-remove").remove();
                    $table.find("tbody").replaceWith(result);
                }
            });
        });

        //Search
        $("input#searchbox").keyup(function (event) {
            let searchCategory = $(document).find(".btn-category-link.sort-category").data("value");
            let page = $(".paging-numper").html();
            let searchString = $(this).val().toString();

         //   console.log(searchString);
            $.ajax({
                url: $search.data("url"),
                method: "POST",
                data: { searchString: searchString, searchCategory: searchCategory, page: page },
                success: function (result) {
                    $(".tfoot-remove").remove();
                    $table.find("tbody").replaceWith(result);
                }
            });
        });

       // pageNumeration
        $(document).on('click', ".paging-numper", function (event) {

            event.preventDefault();
            $this = $(this);
            let searchCategory = $(document).find(".btn-category-link.sort-category").data("value");
           // console.log(searchCategory);
            let page = $this.data("page");
            //console.log(page);
            //console.log(searchCategory);
            $.ajax({
                url: $this.data("url"),
                data: { searchCategory: searchCategory, page: page },
                method: "POST",
                success: function (result) {
                    $(".tfoot-remove").remove();
                    $(document).find("tbody").replaceWith(result);
                }
            });
        });
    }

    var me = {};

    me.onReady = function () {

        initAction();
        TableSorting();
    };

    return me;

})(main);
