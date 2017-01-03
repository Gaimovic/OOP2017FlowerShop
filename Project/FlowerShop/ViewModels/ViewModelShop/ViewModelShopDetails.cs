using FlowerShopLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlowerShop.ViewModels.ViewModelShop
{
    public class ViewModelShopDetails
    {
        public int DetailId { get; set; }

        public string DetailsName { get; set; }

        public string DetailsDescription { get; set; }

        public decimal DetailsPrice { get; set; }

        public string DetailsCategory { get; set; }

        public List<ProductImages> DetailsImages { get; set; }


    }
}