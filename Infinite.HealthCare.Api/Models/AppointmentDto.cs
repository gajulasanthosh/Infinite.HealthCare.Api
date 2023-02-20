using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infinite.HealthCare.Api.Models
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        
        public int PatientId { get; set; }
        public string PatientName { get; set; }

        public int DoctorId { get; set; }
        public string DoctorName { get; set; }

        public DateTime AppointmentDate { get; set; }
        public DateTime AppointmentTime { get; set; }
        public string Problem { get; set; }
    }
}
