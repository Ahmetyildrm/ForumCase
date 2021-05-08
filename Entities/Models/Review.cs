using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Review
    {
        [Column("ReviewId")]
        public Guid Id { get; set; }


        [Required(ErrorMessage = "Review Content is a required field.")]
        [MaxLength(500, ErrorMessage = "Maximum length for the Content is 500 characters.")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Review Title is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Title is 60 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Review Star is a required field.")]
        [MaxLength(1, ErrorMessage = "Maximum length for the Star is 1 characters.")]
        public int Star { get; set; }

        [Required(ErrorMessage = "Review Status is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Status is 30 characters.")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Review OparatedBy is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the OparatedBy is 30 characters.")]
        public string OparatedBy { get; set; }



        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User User{ get; set; }
        
    }
}
