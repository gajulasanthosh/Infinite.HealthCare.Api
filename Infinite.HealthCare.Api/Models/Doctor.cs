using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infinite.HealthCare.Api.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string DoctorName { get; set; }
        public string EmailId { get; set; }
        
        public string Qualification { get; set; }
        public string Specilization { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PhoneNo { get; set; }
        public Users users { get; set; }
        public int UserId { get; set; }
    }
}
