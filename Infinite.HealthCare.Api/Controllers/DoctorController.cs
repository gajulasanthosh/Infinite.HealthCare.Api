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
    public class DoctorController : ControllerBase
    {
        private readonly IRepository<Doctor> _repository;
        private readonly IGetRepository<Doctor> _getRepository;
        private readonly ApplicationDbContext _dbContext;

        public DoctorController(IRepository<Doctor> repository, IGetRepository<Doctor> getRepository,ApplicationDbContext dbContext)
        {
            _repository = repository;
            _getRepository = getRepository;
            _dbContext = dbContext;
        }

        [HttpGet("GetAllDoctors")]
        public IEnumerable<Doctor> GetAllDoctors()
        {
            return _getRepository.GetAll();
        }

        //[Authorize(Roles ="Admin,Doctor")]
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

        [Authorize(Roles ="Admin,Doctor")]
        [HttpPost("CreateDoctor")]
        public async Task<IActionResult> CreateDoctor([FromBody] Doctor doctor)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest();
            }
            //int id = doctor.UserId;
            //var loginId = User.FindFirstValue(ClaimTypes.Name);

            //var userinDb = _dbContext.Users.FirstOrDefault(x => x.UserName == loginId);
            //userinDb.Id = id;
            //doctor.UserId = id;
            await _repository.Create(doctor);
            
            //_dbContext.Users.Update(doctor);
            //_dbContext.SaveChanges();
            return CreatedAtRoute("GetDoctorById", new { id = doctor.Id }, doctor);

        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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
