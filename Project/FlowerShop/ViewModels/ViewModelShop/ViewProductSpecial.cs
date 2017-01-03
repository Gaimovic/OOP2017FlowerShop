using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlowerShop.ViewModels.ViewModelShop
{
    public class ViewProductSpecial
    {
        public List<ViewModelShop> SimpleProduct { get; set; }

        public List<ViewModelShopSpecial> SpecialProduct { get; set; }
    }
}