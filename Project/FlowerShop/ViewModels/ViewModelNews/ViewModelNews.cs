using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FlowerShop.ViewModels.ViewModelNews
{
    public class ViewModelNews
    {
        [Display(Name = "Id")]
        public int ViewId { get; set; }

        [Display(Name = "Title")]
        public string ViewTitle { get; set; }

        [Display(Name = "Desc")]
        public string ViewDescription { get; set; }

        [Display(Name = "Image")]
        public byte[] ViewImage { get; set; }

        [Display(Name = "Created By")]
        public int CreatedById { get; set; }

        [Display(Name = "Date Added")]
        public string CreatedDateTime { get; set; }

        public int ModefiedById { get; set; }

        public string ModifiedDateTime { get; set; }
    }
}