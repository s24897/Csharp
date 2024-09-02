using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class MyDbContext : DbContext
    {

        public MyDbContext(DbContextOptions options) : base(options) 
        { 
        
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescriptionMedicament> prescriptionMedicaments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Prescription>(e =>
            {
                e.HasKey(e => e.IdPerscription);
                e.Property(e => e.Date).IsRequired();
                e.Property(e => e.DueDate).IsRequired();

                e.HasOne(e => e.Doctors)
                .WithMany(e => e.Prescriptions)
                .HasForeignKey(e => e.IdDoctor)
                .OnDelete(DeleteBehavior.ClientCascade);

                e.HasOne(e => e.Patients)
                .WithMany(e => e.Prescriptions)
                .HasForeignKey(e => e.IdPatient)
                .OnDelete(DeleteBehavior.ClientCascade);

                e.ToTable("Prescription");


                e.HasData(new List<Prescription> 
                {
                    new Prescription
                    { 
                        IdPerscription = 1,
                        Date = DateTime.Today,
                        DueDate = DateTime.Now,
                        IdDoctor = 1,
                        IdPatient = 1
                        
                    },

                    new Prescription
                    { 
                        IdPerscription = 2,
                        Date = DateTime.Today,
                        DueDate = DateTime.Now,
                        IdDoctor = 2,
                        IdPatient = 2
                    }
                    
                });

            });

            modelBuilder.Entity<Patient>(e =>
            {
                e.HasKey(e => e.IdPatient);
                e.Property(e => e.FirstName).HasMaxLength(120).IsRequired();
                e.Property(e => e.LastName).HasMaxLength(120).IsRequired();
                e.Property(e => e.BirthDate).IsRequired();


                e.HasData(new List<Patient>
                {
                    new Patient 
                    { 
                        IdPatient = 1,
                        FirstName= "Ada",
                        LastName = "Pado",
                        BirthDate = DateTime.Now
                    },

                    new Patient 
                    {
                        IdPatient = 2,
                        FirstName = "Maciek",
                        LastName = "Krakowiak",
                        BirthDate = DateTime.Today
                    
                    }
                });

            });

            modelBuilder.Entity<Doctor>(e =>
            {
                e.HasKey(e => e.IdDoctor);
                e.Property(e => e.FirstName).HasMaxLength(120).IsRequired();
                e.Property(e => e.LastName).HasMaxLength(120).IsRequired();
                e.Property(e => e.Email).HasMaxLength(120).IsRequired();

                e.HasData(new List<Doctor>
                {
                    new Doctor
                    {
                        IdDoctor = 1,
                        FirstName = "Adam",
                        LastName = "Gil",
                        Email = "s25555@pjwstk.edu.pl"
                    },

                    new Doctor
                    {

                        IdDoctor = 2,
                        FirstName = "Bartosz",
                        LastName = "Iwańczyk",
                        Email = "iw@gmail.com"
                    }


                }); 

            });

            modelBuilder.Entity<Medicament>(e =>
            {
                e.HasKey(e => e.IdMedicament);
                e.Property(e => e.Name).HasMaxLength(120).IsRequired();
                e.Property(e => e.Description).HasMaxLength(120).IsRequired();  
                e.Property(e => e.Type).HasMaxLength(120).IsRequired();

                e.HasData(new List<Medicament>
                {
                    new Medicament
                    {
                        IdMedicament = 1,
                        Name = "Gripex",
                        Description = "Lek",
                        Type = "tabletki"
                    },

                    new Medicament 
                    {
                        IdMedicament = 2,
                        Name = "rutinoskorbin",
                        Description = "Lek na odporność",
                        Type = "tabletki"
                    }

                });  
                    

            });

            modelBuilder.Entity<PrescriptionMedicament>(e => 
            {
                e.HasKey(e => new { e.IdMedicament, e.IdPerscription });
                e.Property(e => e.Dose).IsRequired();
                e.Property(e => e.Details).HasMaxLength(120).IsRequired();

                e.HasOne(e => e.Medicaments)
                .WithMany(e => e.PerscriptionMedicaments)
                .HasForeignKey(e => e.IdMedicament)
                .OnDelete(DeleteBehavior.ClientCascade);

                e.HasOne(e => e.Prescriptions)
                .WithMany(e => e.PrescriptionMedicaments)
                .HasForeignKey(e => e.IdPerscription)
                .OnDelete(DeleteBehavior.ClientCascade);

                e.ToTable("Prescription_Medicament");


                e.HasData(new List<PrescriptionMedicament>
                {
                    new PrescriptionMedicament
                    {
                        IdMedicament = 1,
                        IdPerscription = 1,
                        Dose = 10,
                        Details = "6x dziennie "
                    },

                    new PrescriptionMedicament
                    { 
                        IdMedicament = 2,
                        IdPerscription =2,
                        Dose = 12,
                        Details = "5x dziennie"
                    }

                    

                });
            });
        }
    }
}
