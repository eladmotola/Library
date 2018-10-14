using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Customer : IValidatableObject
    {
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Customer Personal Id")]
        public string PersonalID { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Family Name")]
        public string FamilyName { get; set; }
        [Required]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        [Required]
        [Display(Name = "Birthday")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!General.Validator.IsOnlyLetters(FirstName))
            {
                yield return
                  new ValidationResult(errorMessage: "First Name contains only letters",
                                       memberNames: new[] { "FirstName" });
            }

            if (!General.Validator.IsOnlyLetters(FamilyName))
            {
                yield return
                  new ValidationResult(errorMessage: "Family Name contains only letters",
                                       memberNames: new[] { "FamilyName" });
            }

            if (!General.Validator.IsOnlyNumbers(PersonalID))
            {
                yield return
                  new ValidationResult(errorMessage: "Personal ID contains only numbers",
                                       memberNames: new[] { "PersonalID" });
            }

            if (!General.Validator.IsOnlyNumbers(PhoneNumber))
            {
                yield return
                  new ValidationResult(errorMessage: "Phone Number contains only numbers",
                                       memberNames: new[] { "PhoneNumber" });
            }

            if (!General.Validator.IsValidGender(Gender))
            {
                yield return
                  new ValidationResult(errorMessage: "Gender value can be M or F",
                                       memberNames: new[] { "Gender" });
            }

        }
    }
}