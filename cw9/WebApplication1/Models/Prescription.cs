namespace WebApplication1.Models
{
    public class Prescription
    {
        public int IdPerscription { get; set; }
        public DateTime Date { get;set; }
        public DateTime DueDate { get; set; }
        public int IdDoctor { get; set; }
        public int IdPatient { get; set; }
        public virtual Patient  Patients { get; set; } = null!;
        public virtual Doctor Doctors { get; set; } = null!;

        public virtual ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = null!;
    }
}
