using System;
using System.ComponentModel.DataAnnotations;

namespace NewsApi.Models.ViewModels
{
    public class EditNewsViewModel
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
    }
}