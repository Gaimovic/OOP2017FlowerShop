using FlowerShopLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShopLibrary.Services
{
    public class OrderService : IOrderService
    {
        private readonly IDataContext _idatacontext;

        public OrderService(IDataContext idatacontext)
        {
            _idatacontext = idatacontext;
        }

        public int Create(Order order)
        {
            _idatacontext.Orders.Add(order);

            _idatacontext.SaveChanges();

            return order.OrderId;
        }

    }

    public interface IOrderService
    {
        int Create(Order order);
    }
}
