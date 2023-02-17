using Infinite.HealthCare.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infinite.HealthCare.Api.Repositories
{
    public class DoctorRepository : IRepository<Doctor>, IGetRepository<Doctor>
    {
        private readonly ApplicationDbContext _DbContext;

        public DoctorRepository(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task Create(Doctor obj)
        {
            _DbContext.Doctors.Add(obj);
            await _DbContext.SaveChangesAsync();
        }

        public async Task<Doctor> Delete(int id)
        {
            var doctorDb = await _DbContext.Doctors.FindAsync(id);
            if (doctorDb != null)
            {
                _DbContext.Doctors.Remove(doctorDb);
                await _DbContext.SaveChangesAsync();
                return doctorDb;
            }
            return null;
        }

        public IEnumerable<Doctor> GetAll()
        {
            return _DbContext.Doctors.ToList();
        }

        public async Task<Doctor> GetById(int id)
        {
            var doctor = await _DbContext.Doctors.FindAsync(id);
            if (doctor != null)
            {
                return doctor;
            }
            return null;
        }

        public async Task<Doctor> Update(int id, Doctor obj)
        {
            var doctorDb = await _DbContext.Doctors.FindAsync(id);
            if(doctorDb != null)
            {
                doctorDb.Address = obj.Address;
                doctorDb.City = obj.City;
                doctorDb.PhoneNo = obj.PhoneNo;
                _DbContext.Doctors.Update(doctorDb);
                await _DbContext.SaveChangesAsync();
                return doctorDb;
            }
            return null;
        }
    }
}
