$(document).ready(function () {
    ShopIndex.onReady();
});


let ShopIndex = (function (main) {

    function NewsCarousele() {

        $("#play").click(function () {
            $(".slider").cycle("resume");
            return false;
        });

        $("#pause").click(function () {
            $(".slider").cycle("pause");
            return false;
        });


        $(".slider").cycle({
            fx: 'scrollHorz',
            next: '#next',
            prev: '#prev',
            pager: '#pager'
            //timeout: 3000
            // pause: 1 speed: 900
        });
    }

    function initAction() {

        let myIndex = 0;
        carousel();

        function carousel() {
            let i;
            let x = document.getElementsByClassName("MySlides");

            for (i = 0; i < x.length; i++) {
                x[i].style.display = "none";
            }

            myIndex++;
            if (myIndex > x.length) { myIndex = 1 }
            x[myIndex - 1].style.display = "block";
            setTimeout(carousel, 9000);
        }

        let $shopCategory = $(".product-category-box ul").find("a");

        let $productSort = $(".shop-index-sorting");
        let sortDirections = {
            Ascending: 0,
            Discending: 1
        }

        //Carusel


        //Category Sorting

        $(document).on("click", ".category-sorting", function (event) {
            event.preventDefault();
            // alert("buttonWork!");
            let page = $(".active").find("a").html();


            $this = $(this);

            let searchCategory = $this.html();
            console.log(searchCategory);

            $shopCategory.removeClass("selected-category");
            $this.addClass("selected-category");

            $.ajax({
                url: $this.attr("href"),
                data: { searchCategory: searchCategory, page: page },
                method: "POST",
                success: function (result) {
                    $(".shop-index-box").replaceWith(result);
                }
            });
        });

        // Order By
        $(document).on("click", "a.sortable", function (event) {

            event.preventDefault();

            let page = $(".active").find("a").html();
            console.log(page);
            // $shopCategory.closest("ul").find("selected-category").text();
            let searchCategory = $shopCategory.closest("ul").find(".selected-category").html();
            console.log(searchCategory);

            let $this = $(this);

            
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
                data: { fields: fields, searchCategory: searchCategory, page : page },
                method: "POST",
                success: function (result) {
                    $(".shop-index-box").replaceWith(result);
                }
            });
        });
    }
    
    function SideMenu() {
        $(document).on("click", ".button-side-menu", function () {

            let $this = $(this);

            $(".small-side-menu").css({"width":"200px"});
        });

        $(document).on("mouseleave", ".small-side-menu", function () {

            let $this = $(this);

            $(".small-side-menu").css({ "width": "0px" });
        });

        $(document).on("click", ".small-side-menu", function () {

            let $this = $(this);

            $(".small-side-menu").css({ "width": "0px" });
        });

    }

    let me = {};

    me.onReady = function () {

        NewsCarousele();
        initAction();
        SideMenu();
        
    };

    return me;

})(main);
