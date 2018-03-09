using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class Movie
    {
        public int ID { get; set; }

        [StringLength(60, MinimumLength = 3), Required]
        public string Title { get; set; }
        
        
        [Display(Name = "Release Date"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}", ApplyFormatInEditMode =false)]
        public DateTime ReleaseDate { get; set; }

        
        [StringLength(30), RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage ="This field only accepts alphabetical characters"), Required]
        public string Genre { get; set; }

        [Range(1, 100)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required, DataType("string")]
        public string Rating { get; set; }


        public List<Reviews> Review;
    }
}
