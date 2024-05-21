namespace ClinicSystem.Web.ViewModels.Dashboard
{
    public class DashboardViewModel
    {
        public int DoctorsCount { get; set; }
        public int PatientsCount { get; set; }
        public int UsersCount { get; set; }
        public int SecretariesCount { get; set; }
        public List<Appointment> LatestAppointments { get; set; }
    }
}
