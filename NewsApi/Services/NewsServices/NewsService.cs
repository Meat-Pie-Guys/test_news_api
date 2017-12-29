using System;
using System.Collections.Generic;
using NewsApi.Models.DTOModels;
using NewsApi.Models.ViewModels;
using NewsApi.Repositories.NewsRepositories;

namespace NewsApi.Services.NewsServices
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _repo;

        public NewsService(INewsRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<NewsDTO> getNews()
        {
            return _repo.getNews();
        }

        public void AddNews(NewsViewModel newNews)
        {
            _repo.AddNews(newNews);
        }
    }
}