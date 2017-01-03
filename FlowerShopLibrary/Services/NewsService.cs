using FlowerShopLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShopLibrary.Services
{
    public class NewsService : INewsService
    {
        private readonly IDataContext _idatacontext;

        public NewsService(IDataContext idatacontext)
        {
            _idatacontext = idatacontext;
        }

        public News GetNewsById(int id)
        {
            return _idatacontext.News
                                .Where(n => n.IsDeleted != true)
                                .FirstOrDefault(n => n.Id == id);
        }

        public IQueryable<News> GetNewsList()
        {
            return _idatacontext.News
                                .Where(n => n.IsDeleted != true);
        }

        public void Add(News news)
        {
            _idatacontext.News.Add(news);

            _idatacontext.SaveChanges();
        }

        public void Update(News news)
        {
            _idatacontext.Entry(news).State = EntityState.Modified;

            _idatacontext.SaveChanges();
        }

        public bool MarkAsDeleted(int id)
        {
            var news = GetNewsById(id);

            if(news != null)
            {
                news.IsDeleted = true;

                Update(news);

                return true;
            }

            return false;

        }
    }

    public interface INewsService
    {
        News GetNewsById(int id);
        IQueryable<News> GetNewsList();
        void Add(News news);
        void Update(News news);
        bool MarkAsDeleted(int id);
    }
}
