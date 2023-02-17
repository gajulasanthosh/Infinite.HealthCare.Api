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
    public class AppointmentController : ControllerBase
    {
        private readonly IRepository<Appointment> _repository;
        private readonly IGetRepository<Appointment> _getRepository;

        public AppointmentController(IRepository<Appointment> repository, IGetRepository<Appointment> getRepository)
        {
            _repository = repository;
            _getRepository = getRepository;
        }

        [HttpGet("GetAllAppointments")]
        public IEnumerable<Appointment> GetAllAppointments()
        {
            return _getRepository.GetAll();
        }

        [HttpGet("GetAppointmentById/{id}", Name = "GetAppointmentById")]
        public async Task<IActionResult> GetAppointmentById(int id)
        {
            var appointment = await _getRepository.GetById(id);
            if (appointment != null)
            {
                return Ok(appointment);
            }
            return NotFound("Appointment not found");
        }

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
    }
}
