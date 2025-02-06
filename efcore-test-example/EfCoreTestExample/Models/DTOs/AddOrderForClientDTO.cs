using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PrzykladoweKolowkium2.Models.DTOs
{
    public class AddOrderForClientDTO 
    {
        [Required]
        public int EmployeeID { get; set; }
        [Required]
        public DateTime AcceptedAt { get; set; }
        [MaxLength(300)]
        public string? Comments { get; set; }
        [Required]
        public ICollection<AddOrderForClientPastryDTO> Pastries { get; set; } = null!;
    }

    public class AddOrderForClientPastryDTO
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required, Range(1, int.MaxValue), DefaultValue(1)]
        public int Amount { get; set; }
        [MaxLength(300)]
        public string? Comments { get; set; }
    }
}
