using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Webgentle.BookStore.Enums;
using Webgentle.BookStore.Helpers;

namespace Webgentle.BookStore.Models
{
    public class BookModel
    {
        public int id { get; set; }
        [StringLength(100, MinimumLength = 5)]
        [Required(ErrorMessage = "Please enter book title")]
        //[MyCustomValidationAttribute("good")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter Author Name")]
        public string Author { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public string Category { get; set; }
        [Required(ErrorMessage = "Please enter book Language")]
       
        public int LanguageId { get; set; }
             public string Language { get; set; }
        [Required(ErrorMessage = "Please enter book TotalPage")]
        [Display(Name ="Total pages of book")]
        public int? ToTalPages { get; set; }
        [Display(Name ="Choose Book Cover")]
        [Required]
        public IFormFile coverPhoto { get; set; }
        public string CoverImageUrl { get; set; }
        [Display(Name = "Choose Book gallery Images")]
        [Required]
        public IFormFileCollection galleryFiles { get; set; }
        public List<GalleryModel> Gallery { get; set; }
    }
}
