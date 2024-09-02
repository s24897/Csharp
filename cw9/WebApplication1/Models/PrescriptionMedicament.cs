namespace WebApplication1.Models
{
    public class PrescriptionMedicament
    {
        public int IdMedicament { get; set; }
        public int IdPerscription { get; set; } 
        public int? Dose { get; set; }
        public string Details { get; set; } = null!;

        public virtual Prescription Prescriptions { get; set; } = null!;
        public virtual Medicament Medicaments { get; set; } = null!;
    }
}
