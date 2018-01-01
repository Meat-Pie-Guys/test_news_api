using System;
using NewsApi.Models.ViewModels;

namespace NewsApi.Models.EntityModels
{
    /// <summary>
    /// News article as they are stored in the database.
    /// </summary>
    public class News
    {
        /// <summary>
        /// No argument constructor
        /// </summary>
        public News() { }

        /// <summary>
        /// Converts a view model for creating news to its corresponding entity.
        /// </summary>
        /// <param name="model">A request model for creating news</param>
        public News(AddNewsViewModel model)
        {
            Title = model.Title;
            Content = model.Content;
            ReleaseDate = DateTime.Now;
        }

        /// <summary>
        /// Updates an entity from a view model.
        /// </summary>
        /// <param name="model">A request model for editing news</param>
        public void Edit(EditNewsViewModel model)
        {
            Title = model.Title;
            Content = model.Content;
        }
        
        /// <summary>
        /// A unique identifier for the news article.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// The title of the news article.
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// The date and time when the news article was created.
        /// </summary>
        public DateTime ReleaseDate { get; set; }
        
        /// <summary>
        /// The news content of the news article.
        /// </summary>
        public string Content { get; set; }
    }
}