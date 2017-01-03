using FlowerShopLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlowerShop.ViewModels.ViewModelShop
{
    public class ViewModelShop
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public decimal ProductPrice { get; set; }

        public decimal ProductPriceWithDiscount { get; set; }

        public string ProductDiscount { get; set; }

        public string ProductCategory { get; set; }
    
        public bool Special { get; set; }

        public List<Category> Categories { get; set; }

        public List<ProductImages> ProductImages { get; set; }
    }
}