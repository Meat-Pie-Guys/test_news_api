using System;
using System.Collections.Generic;
using NewsApi.Models.DTOModels;

namespace NewsApi.Repositories.NewsRepositories
{
    public class NewsRepository : INewsRepository
    {
        public IEnumerable<NewsDTO> getNews()
        {
            return new List<NewsDTO>
            {
                new NewsDTO {Id = 1, Title = "XX", ReleaseDate = DateTime.Now, Content = "XXXX"},
                new NewsDTO {Id = 2, Title = "YY", ReleaseDate = DateTime.Now, Content = "YYYY"},
            };
        }
    }
}