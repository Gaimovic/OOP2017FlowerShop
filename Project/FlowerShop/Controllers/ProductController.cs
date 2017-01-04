using FlowerShop.Core;
using FlowerShop.Util;
using FlowerShop.ViewModels;
using FlowerShopLibrary.Models;
using FlowerShopLibrary.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlowerShop.Controllers
{
    public class ProductController : Controller
    {
        private const int pageSize = 14;

        private readonly IProductService _iproductservice;

        public ProductController(IProductService iproductservice)
        {
            _iproductservice = iproductservice;
        }

        private IQueryable<SelectListItem> GetCategoryList()
        {
            var categorries = _iproductservice.GetCategory();

            return from category in categorries
                   select new SelectListItem { Text = category.CategoryName, Value = category.CategoryId.ToString() };
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
                        case "Id":
                            if (field.SortDirection == SortDirection.Descending)
                                list = list.OrderBy(c => c.Id);
                            else
                                list = list.OrderByDescending(c => c.Id);
                            break;
                        case "Price":
                            if (field.SortDirection == SortDirection.Descending)
                                list = list.OrderBy(c => c.FlowerPrice);
                            else
                                list = list.OrderByDescending(c => c.FlowerPrice);
                            break;
                    }
                }
            }
            return list;
        }
        // GET: Product
        public ActionResult Index(string categoryName)
        {
            IEnumerable<Product> list;
            if (categoryName != null)
            {
                list = _iproductservice.GetProductCategories(categoryName);
            }
            else
            {
                list = _iproductservice.GetProductsList();
            }

            if (list != null)
            {
                list.ToList();

                var model = new List<ViewModelProduct>();

                foreach (var value in list)
                {
                    model.Add(new ViewModelProduct()
                    {
                        ViewId = value.Id,
                        ViewFlowerName = value.FlowerName,
                        ViewFlowerDescription = value.FlowerDescription,
                        ViewFlowerPrice = value.FlowerPrice,
                        ViewFlowerAddedDate = value.FlowerAdded.ToString(),
                        ViewImages = value.FlowerImages.ToList(),
                        ViewCategoryId = (int)value.CategoryId,
                        ViewCategory = value.Category.CategoryName
                    });
                }
                return View(model);
            }
            return View();
        }

        public ActionResult Add()
        {
            var model = new ViewModelProduct();

            model.ViewCategories = GetCategoryList();

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Add", model);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(ViewModelProduct model, IEnumerable<HttpPostedFileBase> file)
        {
            //var test = Request.Form["ViewFlowerName"].ToString();

            var ProductImages = new List<ProductImages>();


            foreach (var uploadedImage in file)
            {
                //HttpPostedFileBase file = Request.Files[fileName];

                using (var binaryReader = new BinaryReader(uploadedImage.InputStream))
                {
                    ProductImages.Add(new ProductImages()
                    {
                        ProductImage = binaryReader.ReadBytes(uploadedImage.ContentLength)
                    });
                };
            }

            var product = new Product()
            {
                FlowerName = model.ViewFlowerName,
                FlowerDescription = model.ViewFlowerDescription,
                FlowerPrice = model.ViewFlowerPrice,
                FlowerDiscount = model.ViewFlowerDiscount,
                Special = model.Special,
                FlowerImages = ProductImages,
                CategoryId = model.ViewCategoryId,
                CreatedById = 1,

                FlowerAdded = DateTime.UtcNow
            };

            model.ViewCategories = GetCategoryList();

            _iproductservice.Add(product);

            return RedirectToAction("List", "Product");
        }

        public ActionResult Edit(int id)
        {
            var product = _iproductservice.GetProductById(id);

            var model = new ViewModelProduct()
            {
                ViewId = product.Id,
                ViewFlowerName = product.FlowerName,
                ViewFlowerDescription = product.FlowerDescription,
                ViewFlowerPrice = product.FlowerPrice,
                ViewIsDeleted = product.IsDeleted,
                Special = product.Special
                //ViewCategoryId = (int)product.CategoryId,  
            };
            model.ViewCategories = GetCategoryList();

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Edit", model);
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(ViewModelProduct model, IEnumerable<HttpPostedFileBase> file)
        {
            if (ModelState.IsValid)
            {
                var ProductImages = new List<ProductImages>();
                //  byte[] Image = null;

                foreach (var uploadedImage in file)
                {
                    using (var binaryReader = new BinaryReader(uploadedImage.InputStream))
                    {
                        ProductImages.Add(new ProductImages()
                        {
                            ProductImage = binaryReader.ReadBytes(uploadedImage.ContentLength)
                        });

                    };
                }
                var product = _iproductservice.GetProductById(model.ViewId);

                product.FlowerName = model.ViewFlowerName;
                product.FlowerDescription = model.ViewFlowerDescription;
                product.FlowerPrice = model.ViewFlowerPrice;
                product.ModifiedById = 1;
                product.ModifiedDateTime = DateTime.UtcNow;
                product.FlowerImages = ProductImages;

                product.IsDeleted = model.ViewIsDeleted;
                product.Special = model.Special;
                product.CategoryId = model.ViewCategoryId;
                model.ViewCategories = GetCategoryList();

                _iproductservice.Update(product);
            }
            return RedirectToAction("List", "Product");
        }

        public ActionResult DeleteProduct(int id)
        {
            var success = _iproductservice.MarkAsDeleted(id);

            return Json(new { success = success });
        }

        [ClaimsAuthorize(MyRole = "Admin")]

        public ActionResult List(string searchString)
        {

            var list = _iproductservice.GetProductsList();

            list.ToList();

            var model = new List<ViewModelProduct>();

            var modelproductPaging = new ViewModelProductPaging<ViewModelProduct>();

            foreach (var value in list)
            {
                //int price;
                //if (value.FlowerDiscount != null)
                //{
                //    price = Decimal.ToInt32(value.FlowerPrice);
                //}
                model.Add(new ViewModelProduct()
                {
                    ViewId = value.Id,
                    ViewFlowerName = value.FlowerName,
                    ViewFlowerDescription = value.FlowerDescription,
                    ViewFlowerPrice = value.FlowerPrice,
                    ViewFlowerDiscount = Decimal.ToInt32(value.FlowerDiscount != null ? (decimal)value.FlowerDiscount : 0),
                    //  ViewFlowerPriceWithDiscount = (value.FlowerPrice - (value.FlowerPrice/100 * (decimal)value.FlowerDiscount)).ToString(),
                    ViewCategory = value.Category.CategoryName,
                    ViewFlowerAddedDate = value.FlowerAdded.ToString(),
                    Special = value.Special,
                    ViewImages = value.FlowerImages.ToList()
                });

            }

            modelproductPaging.Data = model.Take(pageSize).ToList();
            modelproductPaging.NumberOfPages = Convert.ToInt32(Math.Ceiling((decimal)model.Count() / pageSize));
            modelproductPaging.CurrentPage = 1;


            return View(modelproductPaging);
        }

        [ClaimsAuthorize(MyRole = "Admin")]
        public ActionResult ProductAjaxList(TableParam tableParam, string searchCategory, string searchString, string page)
        {
            var list = _iproductservice.GetProductsList();

            if (searchCategory != null)
            {
                list = _iproductservice.GetProductCategories(searchCategory);
            }

            if (searchString != null)
            {
                list = _iproductservice.SearchList(list, searchString);
            }

            var modelproductPaging = new ViewModelProductPaging<ViewModelProduct>();

            list = SortProductList(tableParam.Fields, list);

            list.ToList();

            var model = new List<ViewModelProduct>();

            foreach (var value in list)
            {
                model.Add(new ViewModelProduct()
                {
                    ViewId = value.Id,
                    ViewFlowerName = value.FlowerName,
                    ViewFlowerDescription = value.FlowerDescription,
                    ViewFlowerPrice = value.FlowerPrice,
                    ViewFlowerDiscount = Decimal.ToInt32(value.FlowerDiscount != null ? (decimal)value.FlowerDiscount : 0),
                    //ViewFlowerPriceWithDiscount = value.FlowerPrice - (value.FlowerPrice / 100 * (decimal)value.FlowerDiscount),
                    ViewCategory = value.Category.CategoryName,
                    ViewFlowerAddedDate = value.FlowerAdded.ToString(),
                    ViewImages = value.FlowerImages.ToList()
                });
            }

            modelproductPaging.Data = model.Take(pageSize).ToList();
            modelproductPaging.NumberOfPages = Convert.ToInt32(Math.Ceiling((decimal)model.Count() / pageSize));
            modelproductPaging.CurrentPage = 1;

            if (page != null)
            {
                var pageNum = Int32.Parse(page);
                modelproductPaging.Data = model.Skip(pageSize * (pageNum - 1)).Take(pageSize).ToList();
                modelproductPaging.CurrentPage = pageNum;
            }


            if (Request.IsAjaxRequest())
            {
                return PartialView("_ProductCategoryList", modelproductPaging);
            }
            return View("List", modelproductPaging);
        }
    }
}