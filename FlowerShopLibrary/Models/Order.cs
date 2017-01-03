using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShopLibrary.Models
{
    public class Order
    {
        public Order()
        {
            OrderDetails = new List<OrderDetails>();
        }

        public int OrderId { get; set; }

        //public int UserOrderId { get; set; }
        //public User UserOrder { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string OrderAdress { get; set; }

        public string OrderCity { get; set; }

        public string OrderPostalCode { get; set; }

        public string OrderCountry { get; set; }

        public string OrderPhone { get; set; }

        public string OrderEmail { get; set; }

        public decimal OrderTotal { get; set; }

        public DateTime OrderDate { get; set; }

        public ICollection<OrderDetails> OrderDetails { get; set; }

    }
}
