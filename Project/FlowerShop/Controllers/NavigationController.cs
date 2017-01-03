using FlowerShop.ViewModels.ViewModelNavigation;
using FlowerShopLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlowerShop.Controllers
{
    public class NavigationController : Controller
    {
        private readonly INavigationService _inavigationservice;

        public NavigationController(INavigationService inavigationservice)
        {
            _inavigationservice = inavigationservice;
        }
        // GET: Navigation

        public PartialViewResult Menu()
        {
            var model = _inavigationservice.GetListNav();

            return PartialView("_CategoryMenu", model);
        }


        public PartialViewResult LastProductContent()
        {
            var model = new List<ViewModelNavigation>();
            var list = _inavigationservice.LastProduct(3).ToList();

            foreach(var value in list)
            {
                model.Add(new ViewModelNavigation()
                {
                    Id = value.Id,
                    LastProductName = value.FlowerName
                });
            }

            return PartialView("_LastProductContent" , model);

        }
    }
}