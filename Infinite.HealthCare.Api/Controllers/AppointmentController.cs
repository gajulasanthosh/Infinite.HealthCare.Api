using Infinite.HealthCare.Api.Models;
using Infinite.HealthCare.Api.Repositories;
using Microsoft.AspNetCore.Authorization;
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
    public class AppointmentController : ControllerBase
    {
        private readonly IRepository<Appointment> _repository;
        private readonly ISpecRepository _specRepository;
        private readonly IGetRepository<AppointmentDto> _appointmentDtoReposiory; 
        private readonly IAppRepository<Appointment> _appRepository; 

        public AppointmentController(IRepository<Appointment> repository, IGetRepository<AppointmentDto> getDtoRepository, ISpecRepository specRepository,IAppRepository<Appointment> appRepository)
        {
            _repository = repository;
            _specRepository = specRepository;
            _appointmentDtoReposiory = getDtoRepository;
            _appRepository = appRepository;
        }

        [Authorize(Roles ="Admin,Doctor,Patient")]
        [HttpGet("GetAllAppointments")]
        public IEnumerable<AppointmentDto> GetAllAppointments()
        {
            return _appointmentDtoReposiory.GetAll();
        }

        [Authorize]
        [HttpGet("GetAppointmentById/{id}", Name = "GetAppointmentById")]
        public async Task<IActionResult> GetAppointmentById(int id)
        {
            var appointment = await _appointmentDtoReposiory.GetById(id);
            if (appointment != null)
            {
                return Ok(appointment);
            }
            return NotFound("Appointment not found");
        }

        [Authorize(Roles ="Patient")]
        [HttpPost("CreateAppointment")]
        public async Task<IActionResult> CreateAppointment([FromBody] Appointment appointment)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest();
            }
            await _repository.Create(appointment);
            return CreatedAtRoute("GetAppointmentById", new { id = appointment.Id }, appointment);

        }

       
        [Authorize(Roles ="Patient")]
        [HttpDelete("DeleteAppointment/{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var result = await _repository.Delete(id);
            if (result != null)
            {
                return Ok();
            }
            return NotFound("Appointment not found");
        }

        [HttpGet("GetSpecializations")]
        //Get Specializations
        public async Task<IActionResult> GetSpecializations()
        {
            var specs = await _specRepository.GetSpecializations();
            return Ok(specs);
        }

        [HttpGet("GetAppByPatId/{id}")]
        public async Task<IActionResult> GetAppByPatId(int id)
        {
            var appointents = await _appRepository.GetAllAppByPatId(id);
            if (appointents != null)
            {
                return Ok(appointents);
            }
            return NotFound("No Appointments in this city");
        }
    }
}
