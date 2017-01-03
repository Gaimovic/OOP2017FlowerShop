using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShopLibrary.Models
{
    public class Product
    {

        public Product()
        {
            OrderDetails = new List<OrderDetails>();
            FlowerImages = new List<ProductImages>();
        }

        public int Id { get; set; }

        [Display(Name = "Product")]
        public string FlowerName { get; set; }

        public string FlowerDescription { get; set; }

        [Display(Name = "Price")]
        public decimal FlowerPrice { get; set; }

        public decimal? FlowerDiscount { get; set; }

        public ICollection<ProductImages> FlowerImages { get; set; }

        public bool IsDeleted { get; set; }

        public bool Special { get; set; }

        public int? CategoryId { get; set; }
        public Category Category { get; set; }


        public DateTime FlowerAdded { get; set; }
        public int CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }


        public DateTime? ModifiedDateTime { get; set; }
        public int? ModifiedById { get; set; }
        public virtual User ModifiedBy { get; set; }

        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
