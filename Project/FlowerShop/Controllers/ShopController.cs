using FlowerShop.Core;
using FlowerShop.Util;
using FlowerShop.ViewModels.ViewModelNews;
using FlowerShop.ViewModels.ViewModelShop;
using FlowerShopLibrary.Models;
using FlowerShopLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;


namespace FlowerShop.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _iproductservice;
        private readonly INewsService _inewsservice;
        private int PageSize = 4;

        public ShopController(IProductService iproductservice, INewsService inewsservice)
        {
            _iproductservice = iproductservice;
            _inewsservice = inewsservice;
        }

        public IQueryable<Product> SortProductList(IEnumerable<Field> sortOrder, IQueryable<Product> list)
        {
            if (sortOrder != null)
            {
                foreach (var field in sortOrder)
                {

                    if (!field.Sort)
                        continue;

                    switch (field.Name)
                    {
                        case "FlowerPrice":
                            if (field.SortDirection == SortDirection.Ascending)
                            {
                                list = list.OrderBy(c => c.FlowerPrice);
                            }
                            else
                                list = list.OrderByDescending(c => c.FlowerPrice);
                            break;
                    }
                }
            }
            return list;
        }
        // GET: Shop
        // [ClaimsAuthorize(MyRole = "User")]
        public ActionResult Index(int page = 1)
        {
            var model = new List<ViewModelShop>();

            var list = _iproductservice.GetProductsList().ToList();
            ViewBag.Categories = _iproductservice.GetCategory().ToList();

            foreach (var value in list)
            {
                model.Add(new ViewModelShop()
                {
                    Id = value.Id,
                    ProductName = value.FlowerName,
                    ProductDescription = value.FlowerDescription,
                    ProductImages = value.FlowerImages.ToList(),
                    ProductPrice = value.FlowerPrice,
                    ProductDiscount = value.FlowerDiscount.ToString(),
                    ProductPriceWithDiscount = Decimal.ToInt32(value.FlowerDiscount != null ? (value.FlowerPrice - (value.FlowerPrice / 100 * (decimal)value.FlowerDiscount)) : 0),
                    Special = value.Special
                });
            }

            PagedList<ViewModelShop> pmodel = new PagedList<ViewModelShop>(model, page, PageSize);

            return View(pmodel);
        }

        public ActionResult Browse()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var product = _iproductservice.GetProductById(id);

            var model = new ViewModelShopDetails()
            {
                DetailId = product.Id,
                DetailsName = product.FlowerName,
                DetailsDescription = product.FlowerDescription,
                DetailsImages = product.FlowerImages.ToList(),
                DetailsCategory = product.Category.ToString(),
                DetailsPrice = product.FlowerDiscount != null ? (product.FlowerPrice - (product.FlowerPrice / 100 * (decimal)product.FlowerDiscount)) : product.FlowerPrice
                //value.FlowerDiscount != null ? (value.FlowerPrice - (value.FlowerPrice / 100 * (decimal)value.FlowerDiscount)) : 0
            };

            return View(model);
        }

        public ActionResult AxajIndex(string searchCategory, TableParam tableParam, int page)
        {
            var list = _iproductservice.GetProductsList();
            var newList = new List<Product>();
            //ViewBag.Categories = _iproductservice.GetCategory().ToList();

            if (searchCategory != null)
            {
                list = _iproductservice.GetProductCategories(searchCategory);
            }

            foreach (var value in list)
            {
                newList.Add(new Product()
                {
                    Id = value.Id,
                    FlowerName = value.FlowerName,
                    FlowerDescription = value.FlowerDescription,
                    FlowerImages = value.FlowerImages,
                    FlowerDiscount = value.FlowerDiscount,
                    FlowerPrice = value.FlowerDiscount != null ? (value.FlowerPrice - (value.FlowerPrice / 100 * (decimal)value.FlowerDiscount)) : value.FlowerPrice,
                    Special = value.Special
                }
              );
            }

            IQueryable<Product> iquerableList = newList.AsQueryable();

            list = SortProductList(tableParam.Fields, iquerableList);

            var model = new List<ViewModelShop>();


            foreach (var value in list)
            {
                decimal flowerPrice = value.FlowerPrice;
                if (value.FlowerDiscount != null)
                {
                    flowerPrice = (value.FlowerPrice / (100 - (decimal)value.FlowerDiscount) * 100);
                }

                model.Add(new ViewModelShop()
                {
                    Id = value.Id,
                    ProductName = value.FlowerName,
                    ProductDescription = value.FlowerDescription,
                    ProductImages = value.FlowerImages.ToList(),
                    ProductDiscount = value.FlowerDiscount.ToString(),
                    ProductPrice = value.FlowerDiscount != null ? flowerPrice : value.FlowerPrice,
                    ProductPriceWithDiscount = Decimal.ToInt32(value.FlowerDiscount != null ? (flowerPrice - (flowerPrice / 100 * (decimal)value.FlowerDiscount)) : 0),
                    Special = value.Special
                });
            }

            //if (model.Count != 0)
            //{
                PagedList<ViewModelShop> pmodel = new PagedList<ViewModelShop>(model, page, PageSize);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Index", pmodel);
                }

                return View("Index", pmodel);
            //}
            //if (Request.IsAjaxRequest())
            //{
            //    return PartialView("_Index");
            //}
            //return View("Index");
        }

        public PartialViewResult SideSlideShow()
        {
            var list = _iproductservice.GetProductsList();
            var model = new List<ViewModelShop>();

            foreach (var value in list)
            {
                model.Add(new ViewModelShop()
                {
                    Id = value.Id,
                    ProductName = value.FlowerName,
                    ProductPrice = value.FlowerPrice,
                    ProductImages = value.FlowerImages.ToList()
                });
            }

            return PartialView("_SideSlideShow", model);
        }

        public ActionResult ProductSearch(string search)
        {
            var list = _iproductservice.GetProductsList();
            var model = new List<ViewModelShop>();

            if (search != null)
            {
                list = _iproductservice.SearchList(list, search);
            }

            foreach (var value in list)
            {
                decimal flowerPrice = value.FlowerPrice;
                if (value.FlowerDiscount != null)
                {
                    flowerPrice = (value.FlowerPrice / (100 - (decimal)value.FlowerDiscount) * 100);
                }

                model.Add(new ViewModelShop()
                {
                    Id = value.Id,
                    ProductName = value.FlowerName,
                    ProductDescription = value.FlowerDescription,
                    ProductImages = value.FlowerImages.ToList(),
                    ProductDiscount = value.FlowerDiscount.ToString(),
                    ProductPrice = value.FlowerPrice,
                    ProductPriceWithDiscount = Decimal.ToInt32(value.FlowerDiscount != null ? (flowerPrice - (flowerPrice / 100 * (decimal)value.FlowerDiscount)) : 0),
                    Special = value.Special
                }
              );
            }

            return View("Search", model);

        }

        //News Carousele

        public PartialViewResult NewsCarousel()
        {
            var list = _inewsservice.GetNewsList().ToList();
            var model = new List<ViewModelNews>();

            foreach (var value in list)
            {
                model.Add(new ViewModelNews()
                {
                    ViewId = value.Id,
                    ViewTitle = value.Title,
                    ViewDescription = value.Description,
                    ViewImage = value.Image
                });
            }

            return PartialView("_NewsCarousel", model);
        }

        //Category
        public ActionResult Category(string categoryName, TableParam tableParam, int page = 1)
        {
            var model = new List<ViewModelShop>();

            var list = _iproductservice.GetProductCategories(categoryName);
            //ViewBag.Categories = _iproductservice.GetCategory().ToList();


            list = SortProductList(tableParam.Fields, list);

            foreach (var value in list)
            {
                model.Add(new ViewModelShop()
                {
                    Id = value.Id,
                    ProductName = value.FlowerName,
                    ProductDescription = value.FlowerDescription,
                    ProductImages = value.FlowerImages.ToList(),
                    ProductPrice = value.FlowerPrice,
                    ProductDiscount = value.FlowerDiscount.ToString(),
                    ProductCategory = value.Category.CategoryName,
                    ProductPriceWithDiscount = Decimal.ToInt32(value.FlowerDiscount != null ? (value.FlowerPrice - (value.FlowerPrice / 100 * (decimal)value.FlowerDiscount)) : 0),
                    Special = value.Special
                });
            }
            
            if(model.Count != 0)
            {
                PagedList<ViewModelShop> pmodel = new PagedList<ViewModelShop>(model, page, PageSize);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Category", pmodel);
                }

                return View(pmodel);
            }
            return View();
        }
    }
}