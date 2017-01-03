using FlowerShopLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShopLibrary.Services
{
    public class OrderDetailsService : IOrderDetailsService
    {
        private readonly IDataContext _idatacontext;

        public OrderDetailsService(IDataContext idatacontext)
        {
            _idatacontext = idatacontext;
        }

        public void Create(OrderDetails orderdetails)
        {
            _idatacontext.OrderDetails.Add(orderdetails);

            _idatacontext.SaveChanges();

        }
    }

    public interface IOrderDetailsService
    {
        void Create(OrderDetails orderdetails);
    }
}
