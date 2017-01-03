using FlowerShop.ViewModels.ViewModelCart;
using FlowerShopLibrary.Models;
using FlowerShopLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlowerShop.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _iproductservice;
        private readonly IOrderService _iorderservice;
        private readonly IOrderDetailsService _iorderdetailservice;

        public CartController(IProductService iproductservice,
                              IOrderService iorderservice,
                              IOrderDetailsService iorderdetailservice)
        {
            _iproductservice = iproductservice;
            _iorderservice = iorderservice;
            _iorderdetailservice = iorderdetailservice;       
        }

        private int IsInCart(int id)
        {
           var cart = (List<ViewModelItem>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Products.Id == id)
                    return i;
            return -1;
        }

        public ActionResult Index()
        {

            return View();
        }
        // GET: Cart
        public ActionResult Buy(int id)
        {
            var getProductById = _iproductservice.GetProductById(id);
            if (Session["cart"] == null)
            {
               var cart = new List<ViewModelItem>();
                cart.Add(new ViewModelItem()
                {
                    Products = getProductById,
                    DiscountedPrice = getProductById.FlowerDiscount != null ? (getProductById.FlowerPrice - (getProductById.FlowerPrice / 100 * (decimal)getProductById.FlowerDiscount)) : 0,
                    Quantity = 1
                });
                Session["cart"] = cart;
            }
            else
            {
                var cart = (List<ViewModelItem>)Session["cart"];
               // var getProductById = _iproductservice.GetProductById(id);
                int index = IsInCart(id);
                if (index == -1)
                {
                    cart.Add(new ViewModelItem()
                    {
                        Products = getProductById,
                        DiscountedPrice = getProductById.FlowerDiscount != null ? (getProductById.FlowerPrice - (getProductById.FlowerPrice / 100 * (decimal)getProductById.FlowerDiscount)) : 0,
                        Quantity = 1
                    });
                }
                else
                {
                    cart[index].Quantity = cart[index].Quantity + 1;
                }
                Session["cart"] = cart;
            }

            return View("Index");
        }

        public ActionResult Delete(int id)
        {
            var cart = (List<ViewModelItem>)Session["cart"];
            cart.RemoveAt(id);
            Session["cart"] = cart;
            return View("Index");
        }

        public ActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Checkout(ViewModelOrder model)
        {
            var cart = (List<ViewModelItem>)Session["cart"];
            decimal total = 0;
            
            foreach(var value in cart)
            {
                total = total + (value.Quantity * (value.Products.FlowerDiscount != null ? (value.Products.FlowerPrice - (value.Products.FlowerPrice / 100 * (decimal)value.Products.FlowerDiscount)) : value.Products.FlowerPrice));
            }
            //Save Order
            Order order = new Order()
            {
                FirstName = model.ViewOrderFirstName,
                LastName = model.ViewOrderLastName,
                OrderAdress = model.ViewOrderAdress,
                OrderCity = model.ViewOrderCity,
                OrderPostalCode = model.ViewOrderPostalCode,
                OrderCountry = model.ViewOrderCountry,
                OrderPhone = model.ViewOrderPhone,
                OrderEmail = model.ViewOrderEmail,
                OrderDate = DateTime.UtcNow,
                OrderTotal = total   
            };
            int orderId = _iorderservice.Create(order);


            //Save Order Details
            foreach(var value in cart)
            {
                OrderDetails orderdetails = new OrderDetails()
                {
                    OrderId = orderId,
                    ProductId = value.Products.Id,
                    //UnitPrice = value.Products.FlowerPrice,
                    UnitPrice = value.Products.FlowerDiscount != null ? (value.Products.FlowerPrice - (value.Products.FlowerPrice / 100 * (decimal)value.Products.FlowerDiscount)) : value.Products.FlowerPrice,
                    //value.FlowerDiscount != null ? (value.FlowerPrice - (value.FlowerPrice / 100 * (decimal)value.FlowerDiscount)) : 0
                    Quantaty = value.Quantity
                };
                _iorderdetailservice.Create(orderdetails);
            }

            // remove cart
            Session.Remove("cart");

            return View("Thanks");
        }
    }
}
