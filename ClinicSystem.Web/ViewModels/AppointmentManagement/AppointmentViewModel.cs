using ClinicSystem.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicSystem.Web.ViewModels.AppointmentManagement
{
    public class AppointmentViewModel
    {
        public DateTime AppointmentDate { get; set; } = DateTime.Now;
        public TimeSpan StartTime { get; set; }
        public int Duration { get; set; } = 30;
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public List<SelectListItem>? Doctors { get; set; }
        public List<SelectListItem>? Patients { get; set; }
        public IEnumerable<Appointment>? Appointments { get; set; }
    }

    public class CreateAppointmentViewModel
    {
        public DateTime AppointmentDate { get; set; } = DateTime.Now;
        public TimeSpan StartTime { get; set; }
        public int Duration { get; set; } = 30;
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
    }
}
