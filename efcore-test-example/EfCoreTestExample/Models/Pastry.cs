using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrzykladoweKolowkium2.Models
{
    [Table("Pastry")]
    public class Pastry
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(150), Required]
        public string Name { get; set; } = null!;
        [Precision(10, 2), Required]
        public decimal Price { get; set; }
        [MaxLength(40), Required]
        public string Type { get; set; } = null!;
        public virtual ICollection<OrderPastry> OrderPastries { get; set; } = null!;
    }
}
