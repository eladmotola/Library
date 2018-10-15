using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class User
    {
        [Key]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        // RoleId: 1-manager, 2-Librarian
        [Required]
        public int RoleId { get; set; }
    }
}