using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace Library.Models
{
    public class Book : IValidatableObject
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Book Name")]
        [Required]
        public string Name { get; set; }
        [Required]
        public string Author { get; set; }
        [Display(Name = "Release Date")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }
        [Required]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }
        [ForeignKey("GenreId")]
        public Genre Genre { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Regex.Matches(Author, @"[a-zA-Z]").Count != Author.Length)
            {
                yield return
                  new ValidationResult(errorMessage: "Author contains only letters",
                                       memberNames: new[] { "Author" });
            }
        }
    }
}