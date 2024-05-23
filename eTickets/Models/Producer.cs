using eTickets.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Producer:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Profile Picture")]
        [Required(ErrorMessage ="Profile Picture is Required.")]
        public string? ProfilePictureURL { get; set; }
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full name is Required.")]
        [StringLength(50, ErrorMessage = "Full name must be between 3 and 50 characters.")]
        public string? FullName { get; set; }
        [Display(Name = "Biography")]
        [Required(ErrorMessage = "Bio is Required.")]
        public string? Bio { get; set; }

        //Relationships
        public List<Movie>? Movies { get; set; }

    }
}
