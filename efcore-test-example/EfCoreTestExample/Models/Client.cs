using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrzykladoweKolowkium2.Models
{
    [Table("Client")]
    public class Client
    {
        [Key]
        public int ID { get; set; }
        [Required, MaxLength(100)]
        public string FirstName { get; set; } = null!;
        [Required, MaxLength(120)]
        public string LastName { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; } = null!;
    }
}
