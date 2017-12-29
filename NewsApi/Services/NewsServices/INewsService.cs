using System;
using System.Collections.Generic;
using NewsApi.Models.DTOModels;

namespace NewsApi.Services.NewsServices
{
    public interface INewsService
    {
        IEnumerable<NewsDTO> getNews();
    }
}