using eTickets.IdentityEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        [Required]
        public Guid UserId { get; set; }

        //Navigation properties
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public ICollection<CartMovie> CartMovies { get; set; }

    }
}
