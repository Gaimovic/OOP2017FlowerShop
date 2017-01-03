using FlowerShopLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlowerShop.ViewModels.ViewModelShop
{
    public class ViewModelShopSpecial
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public decimal ProductPrice { get; set; }

        // public byte[] ProductImage { get; set; }

        public List<ProductImages> ProductImages { get; set; }
    }
}