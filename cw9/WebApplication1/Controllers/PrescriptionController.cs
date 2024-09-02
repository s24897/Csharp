using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CW8.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace CW8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly MyDbContext _context;

        public PrescriptionController(MyDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetDoctors(int PrescriptionId)
        {
            var prescription = await _context.Prescriptions.FirstOrDefaultAsync(e => e.IdPerscription == PrescriptionId);
            if (prescription != null)
            {
                var doctor = await _context.Doctors.FirstOrDefaultAsync(e => e.IdDoctor == prescription.IdDoctor);
                var patient = await _context.Patients.FirstOrDefaultAsync(e => e.IdPatient == prescription.IdPatient);
                var PresciptionMedi = await _context.prescriptionMedicaments.FirstOrDefaultAsync(e => e.IdPerscription == prescription.IdPerscription);
                List<Medicament> listTempMed = await _context.Medicaments.Where(e=>e.IdMedicament == PresciptionMedi.IdMedicament).Select(e => new Medicament
                {
                    IdMedicament = e.IdMedicament,
                    Name = e.Name,
                    Description = e.Description,
                    Type = e.Type
                }).ToListAsync();

                var prescriptionGet = new PrescriptionGet
                {
                    Dose = (int)PresciptionMedi.Dose,
                    Details = PresciptionMedi.Details,
                    IdDoctor = doctor.IdDoctor,
                    FirstNameDoctor = doctor.FirstName,
                    LastNameDoctor = doctor.LastName,
                    EmailDoctor = doctor.Email,
                    IdPatient = patient.IdPatient,
                    FirstNamePatient = patient.FirstName,
                    LastNamePatient = patient.LastName,
                    BirthdatePatient = patient.BirthDate,
                    listMed = listTempMed,
                };

                return Ok(prescriptionGet);
            }

            return NotFound();
        }
    }
}
