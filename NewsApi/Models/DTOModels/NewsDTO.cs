using System;
using NewsApi.Models.EntityModels;

namespace NewsApi.Models.DTOModels
{
    /// <summary>
    /// The data transfer object model for news, as we want our clients to see them.
    /// </summary>
    public class NewsDTO
    {
        /// <summary>
        /// No argument constructor
        /// </summary>
        public NewsDTO() {}

        /// <summary>
        /// Converts a entity model of News to it corresponding data transfer object.
        /// </summary>
        /// <param name="news">A entity model for news</param>
        public NewsDTO(News news)
        {
            Id = news.Id;
            Title = news.Title;
            ReleaseDate = news.ReleaseDate;
            Content = news.Content;
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