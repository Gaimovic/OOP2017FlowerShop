using FlowerShop.ViewModels;
using FlowerShopLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlowerShop.Controllers
{
    public class HomeController : Controller
    {
        //private readonly IUserService _iuserservice;

        //public HomeController(IUserService iuserservice)
        //{
        //    _iuserservice = iuserservice;
        //}
        // GET: Home
        public ActionResult Index()
        {

            return View();
        }
    }
}