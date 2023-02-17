using Infinite.HealthCare.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infinite.HealthCare.Api.Repositories
{
    public class PatientRepository : IRepository<Patient>, IGetRepository<Patient>
    {
        private readonly ApplicationDbContext _DbContext;

        public PatientRepository(ApplicationDbContext Dbcontext)
        {
            _DbContext = Dbcontext;
        }
        public async Task Create(Patient obj)
        {
            _DbContext.Patients.Add(obj);
            await _DbContext.SaveChangesAsync();
        }

        public async Task<Patient> Delete(int id)
        {
            var patientDb = await _DbContext.Patients.FindAsync(id);
            if (patientDb != null)
            {
                _DbContext.Patients.Remove(patientDb);
                await _DbContext.SaveChangesAsync();
                return patientDb;
            }
            return null;
        }

        public IEnumerable<Patient> GetAll()
        {
            return _DbContext.Patients.ToList();
        }

        public async Task<Patient> GetById(int id)
        {
            var patient = await _DbContext.Patients.FindAsync(id);
            if (patient != null)
            {
                return patient;
            }
            return null;
        }

        public async Task<Patient> Update(int id, Patient obj)
        {
            var patientDb = await _DbContext.Patients.FindAsync(id);
            if (patientDb != null)
            {
                //patientDb.FullName = obj.FullName;
                patientDb.Age = obj.Age;
                patientDb.City = obj.City;
                patientDb.MobileNo = obj.MobileNo;
                patientDb.State = obj.State;
                patientDb.PinCode = obj.PinCode;
                patientDb.Address = obj.Address;
                _DbContext.Patients.Update(patientDb);
                await _DbContext.SaveChangesAsync();
                return patientDb;
            }
            return null;
        }
    }
}
