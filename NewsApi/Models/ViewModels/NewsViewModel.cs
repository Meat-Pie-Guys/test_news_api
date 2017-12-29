using System;
using System.ComponentModel.DataAnnotations;

namespace NewsApi.Models.ViewModels
{
    public class NewsViewModel
    {
        [Required]
        [MaxLength(20)]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
    }
}