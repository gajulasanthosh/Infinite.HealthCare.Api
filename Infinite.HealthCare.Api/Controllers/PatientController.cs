using Infinite.HealthCare.Api.Models;
using Infinite.HealthCare.Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Infinite.HealthCare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IRepository<Patient> _repository;
        private readonly IGetRepository<Patient> _getRepository;
        private readonly ApplicationDbContext _DbContext;

        public PatientController(IRepository<Patient> repository, IGetRepository<Patient> getRepository, ApplicationDbContext dbContext)
        {
            _repository = repository;
            _getRepository = getRepository;
            _DbContext = dbContext;
            
        }

        //[Authorize(Roles ="Admin")]
        [HttpGet("GetAllPatients")]
        public IEnumerable<Patient> GetAllPatients()
        {
            return _getRepository.GetAll();
        }

        [Authorize]
        [HttpGet("GetPatientById/{id}", Name = "GetPatientById")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            var patient = await _getRepository.GetById(id);
            if (patient != null)
            {
                return Ok(patient);
            }
            return NotFound("Patient not found");
        }

        [Authorize]
        [HttpPost("CreatePatient")]
        public async Task<IActionResult> Createpatient([FromBody] Patient patient)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest();
            }
            await _repository.Create(patient);
            
            
            return CreatedAtRoute("GetPatientById", new { id = patient.Id }, patient);

        }

        [Authorize]
        [HttpPut("UpdatePatient/{id}")]
        public async Task<IActionResult> UpdatePatient(int id, Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _repository.Update(id, patient);
            if (result != null)
            {
                return NoContent();
            }
            return NotFound("Patient not found");
        }

        [Authorize(Roles ="Admin")]
        [HttpDelete("DeletePatient/{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var result = await _repository.Delete(id);
            if (result != null)
            {
                return Ok();
            }
            return NotFound("Patient not found");
        }


        
    }
}
