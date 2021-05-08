using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class User
    {
        [Column("UserId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "User firstName is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the firstName is 60 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "User lastName is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the lastName is 60 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "User email is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the email is 60 characters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "User password is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the password is 60 characters.")]
        [MinLength(8, ErrorMessage = "Minimum length for the password is 8 characters.")]
        public string Password { get; set; }


        public ICollection<Review> Rewievs { get; set; }
    }
}
