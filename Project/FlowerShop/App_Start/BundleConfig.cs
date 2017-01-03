using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace FlowerShop.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
                bundles.Add(new ScriptBundle("~/bundles/main-scripts").Include(
                    "~/ClientSide/frameworks/jquery/scripts/jquery-{version}.js",
                    "~/ClientSide/frameworks/bootstrap/scripts/bootstrap.js",
                     "~/ClientSide/scripts/Pages/preloader.js",
                    "~/ClientSide/scripts/main.js",
                    "~/ClientSide/scripts/search.js",
                    "~/ClientSide/scripts/Pages/register_login.js"
                ));
         
            //main
                bundles.Add(new StyleBundle("~/bundles/main-styles")
                    .Include("~/ClientSide/frameworks/bootstrap/css/bootstrap.css", new CssRewriteUrlTransform())
                    .Include("~/ClientSide/frameworks/bootstrap/css/bootstrap-theme.css", new CssRewriteUrlTransform())
                    .Include("~/ClientSide/style/main.css", new CssRewriteUrlTransform())
                    .Include("~/ClientSide/style/table.css", new CssRewriteUrlTransform())

                );
            // Home 

                bundles.Add(new ScriptBundle("~/bundles/skrollr-js").Include(
                    "~/ClientSide/frameworks/javascript/skrollr.js",
                      "~/ClientSide/scripts/Pages/Home/homeMain.js"
                ));

                bundles.Add(new StyleBundle("~/bundles/home-main.css")
                      .Include("~/ClientSide/frameworks/bootstrap/css/bootstrap.css", new CssRewriteUrlTransform())
                      .Include("~/ClientSide/frameworks/bootstrap/css/bootstrap-theme.css", new CssRewriteUrlTransform())
                      .Include("~/ClientSide/style/Pages/Home/HomeMain.css", new CssRewriteUrlTransform())
                  );

            // Shop 
                bundles.Add(new ScriptBundle("~/bundles/shop-index-js").Include(
                      "~/ClientSide/scripts/Pages/Shop/ShopIndex.js",
                      "~/ClientSide/frameworks/jquery/scripts/jquery.cicle.js"

                ));

                bundles.Add(new ScriptBundle("~/bundles/shop-category-js").Include(
                    "~/ClientSide/scripts/Pages/Shop/ShopCategory.js"
              ));

                bundles.Add(new ScriptBundle("~/bundles/jquery-cicle-js").Include(      
                     "~/ClientSide/frameworks/jquery/scripts/jquery.cicle.js"
               ));

                bundles.Add(new StyleBundle("~/bundles/shop-main.css")
                     .Include("~/ClientSide/style/Pages/Shop/ShopMain.css", new CssRewriteUrlTransform())
                 );

                bundles.Add(new StyleBundle("~/bundles/shop-index.css")
                    .Include("~/ClientSide/style/Pages/Shop/ShopIndex.css", new CssRewriteUrlTransform())       
                );

            //Shop - Details

                bundles.Add(new ScriptBundle("~/bundles/shop-details-js").Include(
                    "~/ClientSide/scripts/Pages/Shop/ShopDetails.js"
                     )
                    );

                bundles.Add(
                    new StyleBundle("~/bundles/shop-details.css")
                    .Include("~/ClientSide/style/Pages/Shop/ShopDetails.css", new CssRewriteUrlTransform())
                    );

            //Cart
                bundles.Add(
                   new StyleBundle("~/bundles/cart-main.css")
                   .Include("~/ClientSide/style/Pages/Cart/CartMain.css", new CssRewriteUrlTransform())
                   );
            //Category

                bundles.Add(new StyleBundle("~/bundles/category-styles")
                        .Include("~/ClientSide/style/Pages/Category/CategoryList.css", new CssRewriteUrlTransform())
                    );
                
                    bundles.Add(new StyleBundle("~/bundles/category-index-styles")
                        .Include("~/ClientSide/style/Pages/Category/CategoryIndex.css", new CssRewriteUrlTransform())
                    );
          
                bundles.Add(new ScriptBundle("~/bundles/category-index-js").Include(
                      "~/ClientSide/scripts/Pages/Category/CategoryIndex.js"
                  ));
                bundles.Add(new ScriptBundle("~/bundles/category-js").Include(
                        "~/ClientSide/scripts/Pages/Category/CategoryList.js"
                    ));
                
            //dropzone
                bundles.Add(new ScriptBundle("~/bundles/dropzone-js").Include(
                   "~/ClientSide/frameworks/dropzone/dropzone-scripts/dropzone.js"
          

               ));
                bundles.Add(new StyleBundle("~/bundles/dropzone-css").Include(
                  "~/ClientSide/frameworks/dropzone/dropzone-style/dropzone.css",
                  "~/ClientSide/frameworks/dropzone/dropzone-style/basic.css"
              ));
       

            //Product
                bundles.Add(new ScriptBundle("~/bundles/product-js").Include(
                    "~/ClientSide/scripts/Pages/Product/List.js"
                ));

                bundles.Add(new ScriptBundle("~/bundles/product-category-js").Include(
                    "~/ClientSide/scripts/Pages/Product/ProductCategoryList.js"
                ));

                bundles.Add(new StyleBundle("~/product-style")
                   .Include("~/ClientSide/style/Pages/Product/ProductList.css"
                   ));


            //News

                bundles.Add(new ScriptBundle("~/bundles/news-js").Include(
                    "~/ClientSide/scripts/Pages/News/NewsList.js"
                ));

                bundles.Add(new StyleBundle("~/news-list-css")
                   .Include("~/ClientSide/style/Pages/News/NewsList.css"
                   ));
        }
    }
}