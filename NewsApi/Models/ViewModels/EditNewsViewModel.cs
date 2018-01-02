using System;
using System.ComponentModel.DataAnnotations;

namespace NewsApi.Models.ViewModels
{
    /// <summary>
    /// The expected attributes when editing a news article.
    /// </summary>
    public class EditNewsViewModel
    {
        /// <summary>
        /// The title of the news article.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.TITLE_REQUIRED)]
        [MaxLength(50, ErrorMessage = ErrorMessages.TITLE_TOO_LONG)]
        public string Title { get; set; }

        /// <summary>
        /// The news content of the news article.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.CONTENT_REQUIRED)]
        public string Content { get; set; }
    }
}