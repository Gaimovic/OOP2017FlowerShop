using FlowerShopLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlowerShop.ViewModels.ViewModelCart
{
    public class ViewModelItem
    {
        public Product Products { get; set; }

        public decimal DiscountedPrice { get; set; }

        public int Quantity { get; set; }
    }
}