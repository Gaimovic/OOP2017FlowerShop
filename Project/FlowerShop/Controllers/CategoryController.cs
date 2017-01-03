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
    public class CategoryController : Controller
    {
        private readonly ICategoryService _icategoryservice;

        public CategoryController(ICategoryService icategoryservice)
        {
            _icategoryservice = icategoryservice;
        }
        // GET: Category
        public ActionResult Index()
        {
            var list = _icategoryservice.GetGategories().ToList();


            var model = new List<ViewModelCategory>();
          
            foreach (var value in list)
            {
               // if (value.CategoryIsDeleted != true)
                {
                    model.Add(new ViewModelCategory()
                    {
                        CategoryId = value.CategoryId,
                        CategoryName = value.CategoryName,
                        CreatedById = value.CreatedById,        
                        Images = value.Images.ToList(),
                        Quantity = value.Products.Where(p => p.CategoryId == value.CategoryId && !p.IsDeleted).Count()
                       
                    });
               }
            }

            return View(model);
        }

        public ActionResult Add()
        {
            var model = new ViewModelCategory() { };
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Add", model);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(ViewModelCategory model, IEnumerable<HttpPostedFileBase> file)
        {
            
            byte[] Image = null;
            //var list = file.ToList();
            //List<byte[]> images = new List<byte[]>();
            var categoryImages = new List<CategoryImages>();

            foreach(var uploadImage in file)
            {
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    categoryImages.Add(new CategoryImages()
                    {
                        CategoryImage = binaryReader.ReadBytes(uploadImage.ContentLength)
                     });
                    //Image = binaryReader.ReadBytes(uploadImage.ContentLength);
                };
            }


            if (ModelState.IsValid)
            {
                var category = new Category()
                {
                    CategoryName = model.CategoryName,
                    CreatedById = 1,
                    //Picture1 = images.BinarySearch,
                    Images = categoryImages,
                    CategoryCreated = DateTime.UtcNow
                };

                _icategoryservice.AddCategory(category);

                return RedirectToAction("List", "Category");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            var category = _icategoryservice.GetById(id);
            var model = new ViewModelCategory()
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                IsDeleted = category.CategoryIsDeleted
            };
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Edit", model);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ViewModelCategory model, IEnumerable<HttpPostedFileBase> file)
        {
            var categoryImages = new List<CategoryImages>();
            foreach (var uploadImage in file)
            {
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    categoryImages.Add(new CategoryImages()
                    {
                        CategoryImage = binaryReader.ReadBytes(uploadImage.ContentLength)
                    });
                    //Image = binaryReader.ReadBytes(uploadImage.ContentLength);
                };
            }

                var category = _icategoryservice.GetById(model.CategoryId);

                category.CategoryName = model.CategoryName;
                category.ModifiedById = 1;
                category.CategoryModified = DateTime.UtcNow;
                category.Images = categoryImages;

                _icategoryservice.Update(category);
            
            return RedirectToAction("List", "Category");
        }

        public ActionResult Delete(int id)
        {
            var success = _icategoryservice.MarkAsDeleted(id);

            return Json(new { success = success });
        }


        public ActionResult List()
        {
            var list = _icategoryservice.GetGategories().Where(c => c.CategoryIsDeleted != true).ToList();
            var model = new List<ViewModelCategory>();

            foreach (var value in list)
            {
                if (value.CategoryIsDeleted != true)
                {
                    model.Add(new ViewModelCategory()
                    {
                        CategoryId = value.CategoryId,
                        CategoryName = value.CategoryName,
                        CreatedById = value.CreatedById,
                        Images = value.Images.ToList()
                    });

                }
            }
            return View(model);
        }

    }
}