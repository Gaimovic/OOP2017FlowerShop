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
                        
                        $("#MyId").dropzone({
                            url: "/Product/Add",
                            //  url: $("#MyId").data("url"),
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



    }
    // Sort Table , Search
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
            let $this = $(this);
            let page = parseInt($(".paging-numper").html());
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
                data: { fields: fields, page: page, searchString: searchString },
                method: "POST",
                success: function (result) {
                    $(".tfoot-remove").remove();
                    $table.find("tbody").replaceWith(result);
                }
            });
        });

        //Search
        $("input#searchbox").keyup(function (event) {
           
            let page = $(".paging-numper").html().toString();
            let searchString = $(this).val().toString();
            
            console.log(searchString);
            $.ajax({
                url: $search.data("url"),
                method: "POST",
                data: { searchString: searchString, page: page },
                success: function (result) {
                    $(".tfoot-remove").remove();
                    $table.find("tbody").replaceWith(result);
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
