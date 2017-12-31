using System;
using System.Collections.Generic;
using NewsApi.Models.DTOModels;
using NewsApi.Models.ViewModels;

namespace NewsApi.Services.NewsServices
{
    public interface INewsService
    {
        IEnumerable<NewsDTO> GetNews();
        void AddNews(NewsViewModel newNews);
    }
}