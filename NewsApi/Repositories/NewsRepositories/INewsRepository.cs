using System;
using System.Collections.Generic;
using NewsApi.Models.DTOModels;
using NewsApi.Models.ViewModels;

namespace NewsApi.Repositories.NewsRepositories
{
    public interface INewsRepository
    {
        IEnumerable<NewsDTO> getNews();
        void AddNews(NewsViewModel newNews);
    }
}