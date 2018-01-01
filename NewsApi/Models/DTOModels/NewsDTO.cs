using System;
using NewsApi.Models.EntityModels;

namespace NewsApi.Models.DTOModels
{
    public class NewsDTO
    {

        public NewsDTO() {}
        public NewsDTO(News news)
        {
            Id = news.Id;
            Title = news.Title;
            ReleaseDate = news.ReleaseDate;
            Content = news.Content;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Content { get; set; }
    }
}