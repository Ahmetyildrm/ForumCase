using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Admin : User
    {
        [Required(ErrorMessage = "Admin Username is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Username is 60 characters.")]
        public string Username { get; set; }

    }
}
