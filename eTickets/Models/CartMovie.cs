using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class CartMovie
    {
        [Key]
        public int CartMovieId { get; set; }
        [Required]
        public int MovieId { get; set; }
        [Required]
        public int CartId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Subtotal { get; set; }

        //Navigation Properties
        [ForeignKey("MovieId")]
        public Movie Movie { get; set; }

        [ForeignKey("CartId")]
        public Cart Cart { get; set; }
    }
}
