using FlowerShopLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShopLibrary.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IDataContext _idatacontext;

        public CategoryService(IDataContext idatacontext)
        {
            _idatacontext = idatacontext;
        }

        public Category GetById(int id)
        {
            return _idatacontext.Category
                .Include(p => p.Products)
                .Include(c => c.Images)
                .FirstOrDefault(c => c.CategoryId == id);
        }

        public void AddCategory(Category category)
        {
            _idatacontext.Category.Add(category);

            _idatacontext.SaveChanges();
        }

        public void Update(Category category)
        {
            _idatacontext.Entry(category).State = EntityState.Modified;

            _idatacontext.SaveChanges();
        }

        public bool MarkAsDeleted(int id)
        {
            var category = _idatacontext.Category
                .Include(i => i.Images)
                .FirstOrDefault(c => c.CategoryId == id);

            if (category != null)
            {
                category.CategoryIsDeleted = true;
                category.Images.All( i => i.IsDeleted = true);

                Update(category);

                return true;
            }

            return false;
        }

        public IQueryable<Category> GetGategories()
        {
            return _idatacontext.Category
                .Include(i => i.Images)
                .Include(p => p.Products)
                .Where(c => c.CategoryIsDeleted != true);
              
        }


    }

    public interface ICategoryService
    {
        
        Category GetById(int id);
        void AddCategory(Category category);
        void Update(Category category);
        bool MarkAsDeleted(int id);
        IQueryable<Category> GetGategories();


    }
}
