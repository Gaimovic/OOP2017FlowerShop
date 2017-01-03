using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShopLibrary.Models
{
    public class ProductImages
    {
        public int Id { get; set; }

        public byte[] ProductImage { get; set; }

        public int? ProductId { get; set; }
        public Product Product { get; set; }

        public bool IsDeleted { get; set; }
    }
}
