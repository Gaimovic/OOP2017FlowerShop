$(document).ready(function () {
    ShopCategory.onReady();
});


let ShopCategory = (function (main) {


    function initAction() {

        let sortDirections = {
            Ascending: 0,
            Discending: 1
        }

        let $shopCategory = $(".product-category-box ul").find("a");

        $(document).on("click", "a.sortable", function (event) {

            event.preventDefault();

            let $this = $(this);
            let categoryName = $this.data("category").toString();
            let $productSort = $(".shop-index-sorting");
            let page = $(".active").find("a").html();
            console.log(page);

            console.log(categoryName);

            let SortDirection = $this.data("sort-direction");
            let $sortLinks = $productSort.find("a.sortable");

            let fields = [];

            $sortLinks.removeClass("is-sortable");
            $this.addClass("is-sortable");

            if (SortDirection === undefined || SortDirection.lenght === 0)
                SortDirection = sortDirections.Ascending;
            else if (SortDirection == sortDirections.Ascending)
                SortDirection = sortDirections.Discending;
            else
                SortDirection = sortDirections.Ascending;
            console.log(sortDirections);
            $this.data("sort-direction", SortDirection);

            $sortLinks.each(function (i) {
                let $link = $($sortLinks[i]);
                console.log($link);
                let field = {
                    Name: $link.data("field-name"),
                    Sort: $link.hasClass("is-sortable"),
                    SortDirection: SortDirection
                };
                fields.push(field);
            });
            console.log(fields[0]);

            $.ajax({
                url: $this.attr("href"),
                data: { fields: fields, categoryName: categoryName, page: page },
                method: "POST",
                success: function (result) {
                    $(".shop-index-box").replaceWith(result);
                }
            });
        });
    }


    var me = {};

    me.onReady = function () {

        initAction();

    };

    return me;

})(main);
