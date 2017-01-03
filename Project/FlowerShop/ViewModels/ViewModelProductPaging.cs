using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlowerShop.ViewModels
{
    public class ViewModelProductPaging<T> where T : class
    {
            public List<T> Data { get; set; }
            public int NumberOfPages { get; set; }
            public int CurrentPage { get; set; }
    }
}