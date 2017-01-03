using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShopLibrary.Models
{
    public class CategoryImages
    {
        public int Id { get; set; }

        public byte[] CategoryImage { get; set; }

        public bool IsDeleted { get; set; }
    }
}
