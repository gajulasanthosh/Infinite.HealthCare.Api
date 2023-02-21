using Infinite.HealthCare.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infinite.HealthCare.Api.Repositories
{
    public class AppointmentRepository : IRepository<Appointment>, IGetRepository<AppointmentDto>, ISpecRepository, IAppRepository<Appointment>
    {
        private readonly ApplicationDbContext _DbContext;

        public AppointmentRepository(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
        }
        public async Task Create(Appointment obj)
        {
            _DbContext.Appointmnets.Add(obj);
            await _DbContext.SaveChangesAsync();
        }

        public async Task<Appointment> Delete(int id)
        {
            var appointmentDb = await _DbContext.Appointmnets.FindAsync(id);
            if (appointmentDb != null)
            {
                _DbContext.Appointmnets.Remove(appointmentDb);
                await _DbContext.SaveChangesAsync();
                return appointmentDb;
            }
            return null;
        }

        public IEnumerable<AppointmentDto> GetAll()
        {
            //return _DbContext.Appointmnets.ToList();
            var appointments = _DbContext.Appointmnets.Include(x => x.Doctor).Include(y=> y.Patient).Select(x => new AppointmentDto
            {
                
                PatientId = x.PatientId,
                DoctorId = x.DoctorId,
                AppointmentDate = x.AppointmentDate,
                AppointmentTime=x.AppointmentTime,
                Problem = x.Problem,
                PatientName = x.Patient.FullName,
                DoctorName= x.Doctor.DoctorName
            }).ToList();


            return appointments;
        }

        public async Task<AppointmentDto> GetById(int id)
        {
            var appointments =await _DbContext.Appointmnets.Include(x => x.Doctor).Include(y => y.Patient).Select(x => new AppointmentDto
            {

                PatientId = x.PatientId,
                DoctorId = x.DoctorId,
                AppointmentDate = x.AppointmentDate,
                AppointmentTime = x.AppointmentTime,
                Problem = x.Problem,
                PatientName = x.Patient.FullName,
                DoctorName = x.Doctor.DoctorName
            }).ToListAsync();
            var appointment = appointments.FirstOrDefault(x => x.Id == id);
            //var appointment = await _DbContext.Appointmnets.FindAsync(id);
            if (appointment != null)
            {
                return appointment;
            }
            return null;
        }

        public Task<Appointment> Update(int id, Appointment obj)
        {
            throw new NotImplementedException();
        }

        //Get Genres
        public async Task<IEnumerable<Specialization>> GetSpecializations()
        {
            var specs = await _DbContext.Specializations.ToListAsync();
            return specs;
        }

        public async Task<IEnumerable<Appointment>> GetAllAppByPatId(int id)
        {
            var appointments = await _DbContext.Appointmnets.Where(h => h.PatientId == id).ToListAsync();
            if (appointments.Count > 0)
                return appointments;
            return null;
        }
    }
}
