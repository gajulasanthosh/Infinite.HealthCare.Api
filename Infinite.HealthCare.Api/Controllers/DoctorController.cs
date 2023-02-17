using Infinite.HealthCare.Api.Models;
using Infinite.HealthCare.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infinite.HealthCare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IRepository<Doctor> _repository;
        private readonly IGetRepository<Doctor> _getRepository;

        public DoctorController(IRepository<Doctor> repository, IGetRepository<Doctor> getRepository)
        {
            _repository = repository;
            _getRepository = getRepository;
        }

        [HttpGet("GetAllDoctors")]
        public IEnumerable<Doctor> GetAllDoctors()
        {
            return _getRepository.GetAll();
        }

        [HttpGet("GetDoctorById/{id}", Name = "GetDoctorById")]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            var doctor = await _getRepository.GetById(id);
            if (doctor != null)
            {
                return Ok(doctor);
            }
            return NotFound("Doctor not found");
        }

        [HttpPost("CreateDoctor")]
        public async Task<IActionResult> CreateDoctor([FromBody] Doctor doctor)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest();
            }
            await _repository.Create(doctor);
            //int id = patient.CustomerId;
            //var loginId = User.FindFirstValue(ClaimTypes.Name);

            //var userinDb = _dbContext.Users.FirstOrDefault(x => x.LoginID == loginId);
            //userinDb.CustomerID = id;
            //_dbContext.Users.Update(userinDb);
            //_dbContext.SaveChanges();
            return CreatedAtRoute("GetDoctorById", new { id = doctor.Id }, doctor);

        }

        [HttpPut("UpdateDoctor/{id}")]
        public async Task<IActionResult> UpdateDoctor(int id, Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _repository.Update(id, doctor);
            if (result != null)
            {
                return NoContent();
            }
            return NotFound("Doctor not found");
        }

        [HttpDelete("DeleteDoctor/{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var result = await _repository.Delete(id);
            if (result != null)
            {
                return Ok();
            }
            return NotFound("Doctor not found");
        }
    }
}
