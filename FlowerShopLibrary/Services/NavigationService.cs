using FlowerShopLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShopLibrary.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IDataContext _idatacontext;

        public NavigationService(IDataContext idatacontext)
        {
            _idatacontext = idatacontext;
        }

        public IQueryable<string> GetListNav(){

            return _idatacontext.Category
                .Where(c =>c.CategoryIsDeleted != true)
                .Select(c => c.CategoryName)        
                .Distinct()
                .OrderBy(c => c);
        }

        public IQueryable<Product> LastProduct(int n)
        {
            return _idatacontext.Products.OrderByDescending(p => p.Id).Take(n);
        }
    }

    public interface INavigationService
    {
        IQueryable<string> GetListNav();
        IQueryable<Product> LastProduct(int n);
    }   
}
