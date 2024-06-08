using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public string CustomerName { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
