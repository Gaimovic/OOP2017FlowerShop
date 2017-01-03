using FlowerShopLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlowerShop.ViewModels
{
    public class ViewModelProduct
    {
        [Display(Name = "Id")]
        public int ViewId { get; set; }

        [Display(Name = "Name")]
        public string ViewFlowerName { get; set; }

        [Display(Name = "Description")]
        public string ViewFlowerDescription { get; set; }

        [Display(Name = "Price")]
        public decimal ViewFlowerPrice { get; set; }

        [Display(Name = "Discounted Price")]
        public string ViewFlowerPriceWithDiscount { get; set; }

        [Display(Name ="Discount")]
        public int ViewFlowerDiscount { get; set; }

        [Display(Name = "IMG")]
       // public byte[] ViewImage { get; set; }
        public List<ProductImages> ViewImages { get; set; }

        [Display(Name = "CategoryId")]
        public int ViewCategoryId { get; set; }

        [Display(Name = "Category")]
        public string ViewCategory { get; set; }

        public IQueryable<SelectListItem> ViewCategories {get;set;}

        [Display(Name = "Date Created")]
        public string ViewFlowerAddedDate { get; set; }

        public bool ViewIsDeleted { get; set; }

        public bool Special { get; set; }
    }
}