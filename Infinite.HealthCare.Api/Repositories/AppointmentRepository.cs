using Infinite.HealthCare.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infinite.HealthCare.Api.Repositories
{
    public class AppointmentRepository : IRepository<Appointment>, IGetRepository<Appointment>
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

        public IEnumerable<Appointment> GetAll()
        {
            return _DbContext.Appointmnets.ToList();
        }

        public async Task<Appointment> GetById(int id)
        {
            var appointment = await _DbContext.Appointmnets.FindAsync(id);
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
    }
}
