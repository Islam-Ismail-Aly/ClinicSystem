namespace ClinicSystem.Web.ViewModels.AppointmentManagement
{
    public class UpdateAppointmentViewModel
    {
        public DateTime AppointmentDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public int Duration { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
    }
}
