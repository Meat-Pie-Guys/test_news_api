using System;
using NewsApi.Models.ViewModels;

namespace NewsApi.Models.EntityModels
{
    public class News
    {
        public News() { }
        public News(AddNewsViewModel model)
        {
            Title = model.Title;
            Content = model.Content;
            ReleaseDate = DateTime.Now;
        }

        public void Edit(EditNewsViewModel model)
        {
            Title = model.Title;
            Content = model.Content;
        }
        
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Content { get; set; }
    }
}