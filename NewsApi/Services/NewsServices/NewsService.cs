using System;
using System.Linq;
using System.Collections.Generic;
using NewsApi.Models.DTOModels;
using NewsApi.Models.EntityModels;
using NewsApi.Models.ViewModels;
using NewsApi.Repositories;

namespace NewsApi.Services.NewsServices
{
    public class NewsService : INewsService
    {
        private readonly AppDataContext _db;

        public NewsService(AppDataContext db)
        {
            _db = db;
        }

        public IEnumerable<NewsDTO> GetNews()
        {
            return 
            (
                from n in _db.News
                select new NewsDTO
                {
                    Id = n.Id,
                    Title = n.Title,
                    ReleaseDate = n.ReleaseDate,
                    Content = n.Content
                }
            ).ToList();
        }

        public void AddNews(NewsViewModel newNews)
        {
            _db.Add(new News { Title = newNews.Title, ReleaseDate = DateTime.Now, Content = newNews.Content });
            _db.SaveChanges();
        }
    }
}