using FlowerShopLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FlowerShop.ViewModels
{
    public class ViewModelCategory
    {
        [Display(Name = "Id")]
        public int CategoryId { get; set;}

        [Display(Name = "Name")]
        public string CategoryName { get; set; }

        [Display(Name = "Uploaded images")]
        public List<CategoryImages> Images { get; set; }

        public byte[] ViewImage { get; set; }

        public bool IsDeleted { get; set; }

        public int CreatedById { get; set; }

        public User CreatedBy { get; set; }

        public int ModifiedById { get; set; }

        public int Quantity { get; set; }

    }
}