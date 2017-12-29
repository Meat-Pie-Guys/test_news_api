using System;

namespace NewsApi.Models.EntityModels
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Content { get; set; }
    }
}