using System;
using System.ComponentModel.DataAnnotations;

namespace NewsApi.Models.ViewModels
{
    /// <summary>
    /// The expected attributes when adding a news article.
    /// </summary>
    public class AddNewsViewModel
    {
        /// <summary>
        /// The title of the news article.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// The news content of the news article.
        /// </summary>
        [Required]
        public string Content { get; set; }
    }
}