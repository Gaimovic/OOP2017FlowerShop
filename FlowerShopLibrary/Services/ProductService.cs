using FlowerShopLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShopLibrary.Services
{
    public class ProductService : IProductService
    {
        private readonly IDataContext _idatacontext;

        public ProductService(IDataContext idatacontext)
        {
            _idatacontext = idatacontext;
        }

        public Product GetProductById(int id)
        {
            return _idatacontext.Products
                .Where(p => p.IsDeleted != true)
                .Include(c => c.FlowerImages)
                .Include(c => c.Category)
                .FirstOrDefault(p => p.Id == id);
        }


        public IQueryable<Product> GetProductsList()
        {
            return _idatacontext.Products
                .Include(c => c.Category)
                .Include(i=>i.FlowerImages)
                .Where(c => !c.IsDeleted);
        }

        public IQueryable<Product> GetProductCategories(string categoryName)
        {
            return _idatacontext.Products
                .Include(c => c.Category)
                .Include( i => i.FlowerImages)
                .Where(c => !c.IsDeleted && c.Category.CategoryName == categoryName);
        }

        public void Add(Product product)
        {
            _idatacontext.Products.Add(product);

            _idatacontext.SaveChanges();
        }


        public void Update(Product product)
        {
            _idatacontext.Entry(product).State = EntityState.Modified;

            _idatacontext.SaveChanges();
        }

        public bool MarkAsDeleted(int id)
        {
            var product = _idatacontext.Products
                .Include(i => i.FlowerImages)
                .FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                product.IsDeleted = true;
                product.FlowerImages.All(i => i.IsDeleted = true);

                Update(product);

                return true;
            }

            return false;
        }

        public IQueryable<Category> GetCategory()
        {
            return _idatacontext.Category
                .Where(c => c.CategoryIsDeleted != true);
        }

        public IQueryable<Product> SearchList(IQueryable<Product> list, string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                list = list.Where(s => s.FlowerName.Contains(searchString)
                                       || s.FlowerPrice.ToString().Contains(searchString)
                                       || s.Category.CategoryName.Contains(searchString));
            }
            return list;
        }
    }

    public interface IProductService
    {
        Product GetProductById(int id);
        IQueryable<Product> GetProductsList();
        IQueryable<Product> GetProductCategories(string categoryName);
        void Add(Product product);
        void Update(Product product);
        bool MarkAsDeleted(int id);
        IQueryable<Category> GetCategory();
        IQueryable<Product> SearchList(IQueryable<Product> list, string searchString);
    }
}
