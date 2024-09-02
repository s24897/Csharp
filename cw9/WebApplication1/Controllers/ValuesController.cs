using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CW8.Models;
using CW8.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace CW8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ValuesController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetDoctors()
        {
            return Ok(await _context.Doctors.Select(e=>new
            {
                IdDoctor = e.IdDoctor,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email
            }).ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddDoctor(DoctorAdd doctorAdd)
        {
            
           _context.Doctors.Add(new Doctor
            {
                FirstName = doctorAdd.FirstName,
                LastName = doctorAdd.LastName,
                Email = doctorAdd.Email
            });
            await _context.SaveChangesAsync();
            
            return Created("",doctorAdd);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateDoctor(DoctorUpdate doctorUpdate)
        {
            
            var doctor = await _context.Doctors.FirstOrDefaultAsync(e => e.IdDoctor == doctorUpdate.IdDoctor);
            if (doctor == null)
            {
                return NotFound();
            }

            doctor.FirstName = doctorUpdate.FirstName;
            doctor.LastName = doctorUpdate.LastName;
            doctor.Email = doctorUpdate.Email;
            
            await _context.SaveChangesAsync();

            return Ok(await _context.Doctors.FirstOrDefaultAsync(e => e.IdDoctor == doctorUpdate.IdDoctor));

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDoctor(int index)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(e => e.IdDoctor == index);
            if (doctor == null)
            {
                return NotFound();
            }

            _context.Doctors.Remove(doctor);
           await _context.SaveChangesAsync();
           
           return Ok();
        }
    }
}
