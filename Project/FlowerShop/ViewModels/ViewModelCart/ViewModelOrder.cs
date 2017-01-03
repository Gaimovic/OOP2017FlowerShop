using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FlowerShop.ViewModels.ViewModelCart
{
    public class ViewModelOrder
    {
        [Display(Name = "Name")]
        public string ViewOrderFirstName { get; set; }

        [Display(Name = "Surname")]
        public string ViewOrderLastName { get; set; }

        [Display(Name = "Adress")]
        public string ViewOrderAdress { get; set; }

        [Display(Name = "City")]
        public string ViewOrderCity { get; set; }

        [Display(Name = "Postal Code")]
        public string ViewOrderPostalCode { get; set; }

        [Display(Name = "Country")]
        public string ViewOrderCountry { get; set; }

        [Display(Name = "Phone")]
        //[DataType(DataType.PhoneNumber)]
        public string ViewOrderPhone { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string ViewOrderEmail { get; set; }

        public decimal ViewOrderTotal { get; set; }

        //public DateTime OrderDate { get; set; }
    }
}