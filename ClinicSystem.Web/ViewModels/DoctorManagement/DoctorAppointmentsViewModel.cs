namespace ClinicSystem.Web.ViewModels.DoctorManagement
{
    public class DoctorAppointmentsViewModel
    {
        public IEnumerable<Appointment>? Appointments { get; set; }
        public DoctorViewModel? Doctor { get; set; }
        public string? CustomText { get; set; }
    }


    public class DoctorViewModel
    {
        public int? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DayOfWeek DayOff { get; set; }
    }

}
