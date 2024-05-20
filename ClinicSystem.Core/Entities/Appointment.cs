using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicSystem.Core.Entities
{
    public class Appointment : BaseEntity
    {
        public DateTime AppointmentDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public int Duration { get; set; } = 30;
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int SecretaryId { get; set; }
        public Secretary Secretary { get; set; }
    }
}
