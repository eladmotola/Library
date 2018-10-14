using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
            if (!General.Validator.IsOnlyLetters(Author))
            {
                yield return
                  new ValidationResult(errorMessage: "Author contains only letters",
                                       memberNames: new[] { "Author" });
            }
        }
        
    }
}