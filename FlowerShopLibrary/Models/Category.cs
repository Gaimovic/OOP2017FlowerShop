using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShopLibrary.Models
{
    public class Category
    {
        public Category()
        {
            Products = new List<Product>();
            Images = new List<CategoryImages>();
        }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public bool CategoryIsDeleted { get; set; }

        public ICollection<CategoryImages> Images { get; set; }

        public DateTime CategoryCreated { get; set; }
        public int CreatedById { get; set; }
        public User CreatedBy { get; set; }

        public DateTime? CategoryModified{ get; set; }
        public int? ModifiedById { get; set; }
        public User ModifiedBy { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
