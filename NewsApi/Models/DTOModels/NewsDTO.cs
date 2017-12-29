using System;

namespace NewsApi.Models.DTOModels
{
    public class NewsDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Content { get; set; }
    }
}