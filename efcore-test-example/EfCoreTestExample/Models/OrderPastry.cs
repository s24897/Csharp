using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrzykladoweKolowkium2.Models
{
    [Table("Order_Pastry"), PrimaryKey(nameof(OrderID), nameof(PastryID))]
    public class OrderPastry
    {
        public int OrderID { get; set; }
        public int PastryID { get; set; }
        [Required]
        public int Amount { get; set; }
        [MaxLength(300)]
        public string? Comme { get; set; }
        [ForeignKey(nameof(PastryID))]
        public virtual Pastry Pastry { get; set; } = null!;
        [ForeignKey(nameof(OrderID))]
        public virtual Order Order { get; set; } = null!;
    }
}
