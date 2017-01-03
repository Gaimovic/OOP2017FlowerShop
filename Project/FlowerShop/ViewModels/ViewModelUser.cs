using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace FlowerShop.ViewModels
{
    public class ViewModelUser
    {
        public int Id { get; set; }

        [DisplayName("User Name")]
        public string UserName { get; set; }

        [DisplayName("User Surname")]
        public string UserLastname { get; set; }

        [DisplayName("Registrated Date")]
        public string UserCreated { get; set; }
    }
}