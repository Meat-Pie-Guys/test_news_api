using System;
using System.Linq;
using System.Collections.Generic;
using NewsApi.Models.DTOModels;
using NewsApi.Models.ViewModels;
using NewsApi.Models.EntityModels;

namespace NewsApi.Repositories.NewsRepositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly AppDataContext _db;

        public NewsRepository(AppDataContext db)
        {
            _db = db;
        }
        public IEnumerable<NewsDTO> getNews()
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