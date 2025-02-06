using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrzykladoweKolowkium2.Models
{
    [Table("Order")]
    public class Order
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public DateTime AcceptedAt { get; set; }
        public DateTime? FulfilledAt { get; set; }
        [MaxLength(300)]
        public string? Comments { get; set; }
        public int ClientID { get; set; }
        public int EmployeeID { get; set; }
        [ForeignKey(nameof(ClientID))]
        public virtual Client Client { get; set; } = null!;
        [ForeignKey(nameof(EmployeeID))]
        public virtual Employee Employee { get; set; } = null!;
        public virtual ICollection<OrderPastry> OrderPastries { get; set; } = null!;
    }
}
