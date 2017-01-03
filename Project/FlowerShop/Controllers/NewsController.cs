using FlowerShop.ViewModels.ViewModelNews;
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
    public class NewsController : Controller
    {
        private readonly INewsService _inewsservice;

        public NewsController(INewsService inewsservice)
        {
            _inewsservice = inewsservice;
        }
      
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            var model = new ViewModelNews();

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Add", model);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(ViewModelNews uploadNews, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                byte[] Image = null;

                using (var binaryReader = new BinaryReader(file.InputStream))
                {
                    Image = binaryReader.ReadBytes(file.ContentLength);
                };

                var news = new News()
                {
                    Title = uploadNews.ViewTitle,
                    Description = uploadNews.ViewDescription,
                    Image = Image,
                    CreatedById = 1,
                    NewsAdded = DateTime.UtcNow

                };

                _inewsservice.Add(news);
            }
            return RedirectToAction("List", "News");
        }


        public ActionResult Edit(int id)
        {
            var news = _inewsservice.GetNewsById(id);

            
            var model = new ViewModelNews() {
                ViewId = news.Id,
                ViewTitle = news.Title,
                ViewDescription = news.Description,
                ViewImage = news.Image

            };
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Edit", model);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ViewModelNews uploadedNews, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                byte[] Image = null;

                using (var binaryReader = new BinaryReader(file.InputStream))
                {
                    Image = binaryReader.ReadBytes(file.ContentLength);
                };

                var news = _inewsservice.GetNewsById(uploadedNews.ViewId);

                news.Id = uploadedNews.ViewId;
                news.Title = uploadedNews.ViewTitle;
                news.Description = uploadedNews.ViewDescription;
                news.Image = Image;
                news.ModifiedById = 1;
                news.ModifiedDateTime = DateTime.UtcNow;

                _inewsservice.Update(news);
            }
            return RedirectToAction("List", "News");
        }

        public ActionResult DeleteNews(int id)
        {

            var success = _inewsservice.MarkAsDeleted(id);

            return Json(new { success = success });
        }

        public ActionResult List()
        {

            var list = _inewsservice.GetNewsList();

            list.ToList();

            var model = new List<ViewModelNews>();

            foreach(var value in list) {
                model.Add(new ViewModelNews()
                {
                    ViewId = value.Id,
                    ViewTitle = value.Title,
                    ViewDescription = value.Description,
                    ViewImage = value.Image,
                    CreatedById = value.CreatedById,
                    CreatedDateTime =value.NewsAdded.ToString(),
                    //ModefiedById = (int)value.ModifiedById,
                    //ModifiedDateTime = value.ModifiedDateTime.ToString()                
                }
             );
            }

            return View(model);
        }

    }
}