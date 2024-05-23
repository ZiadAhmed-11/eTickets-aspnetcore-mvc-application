using eTickets.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Actor:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Profile picture URL is required.")]
        public string? ProfilePictureURL { get; set; }
        [Required(ErrorMessage = "Full name is required.    ")]
        [StringLength(50,ErrorMessage ="Full name must be between 3 and 50 characters.")]
        public string? FullName { get; set; }
        [Required(ErrorMessage = "Biography is required.")]
        public string? Bio { get; set; }

        //relationships
        public List<Actor_Movie>? Actors_Movies { get; set; }
    }
}
