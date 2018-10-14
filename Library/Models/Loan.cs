using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class Loan
    {
        [Key, Column(Order = 0)]
        public int BookId { get; set; }
        [Key, Column(Order = 1)]
        public int CustomerId { get; set; }
        [Required]
        [Display(Name = "Loan Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LoanDate { get; set; }
        [Required]
        [Display(Name = "Return Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReturnDate { get; set; }
        [ForeignKey("BookId")]
        [Display(Name = "Book Name")]
        public virtual Book Book { get; set; }

        [Display(Name = "Customer Id")]
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
    }
}