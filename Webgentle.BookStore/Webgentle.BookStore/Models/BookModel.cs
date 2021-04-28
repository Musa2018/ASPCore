﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Webgentle.BookStore.Models
{
    public class BookModel
    {
        public int id { get; set; }
        [StringLength(100,MinimumLength =5)]
        [Required(ErrorMessage ="Please enter book title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter Author Name")]
        public string Author { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public string Category { get; set; }
        public string Language { get; set; }
        [Required(ErrorMessage = "Please enter book TotalPage")]
        [Display(Name ="Total pages of book")]
        public int? ToTalPages { get; set; }
    }
}
