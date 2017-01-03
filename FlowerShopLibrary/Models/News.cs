using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShopLibrary.Models
{
    public class News
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime NewsAdded { get; set; }
        public int CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }

        public DateTime? ModifiedDateTime { get; set; }
        public int? ModifiedById { get; set; }
        public virtual User ModifiedBy { get; set; }
    }
}
