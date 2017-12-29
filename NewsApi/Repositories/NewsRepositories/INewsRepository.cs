using System;
using System.Collections.Generic;
using NewsApi.Models.DTOModels;

namespace NewsApi.Repositories.NewsRepositories
{
    public interface INewsRepository
    {
        IEnumerable<NewsDTO> getNews();
    }
}