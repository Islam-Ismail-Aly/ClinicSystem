using ClinicSystem.Web.ViewModels.DoctorManagement;

namespace ClinicSystem.Web.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly IUnitOfWork<Appointment> _appointmentUnitOfWork;
        private readonly IUnitOfWork<Doctor> _doctorUnitOfWork;
        private readonly IUserRepository _userRepository;

        public DoctorsController(IUnitOfWork<Appointment> appointmentUnitOfWork, IUnitOfWork<Doctor> doctorUnitOfWork
            , IUserRepository userRepository)
        {
            _appointmentUnitOfWork = appointmentUnitOfWork;
            _doctorUnitOfWork = doctorUnitOfWork;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Appointments(int doctorId, DateTime? startDate, DateTime? endDate)
        {
            var today = DateTime.Today;
            var appointments = _appointmentUnitOfWork.Entity
                                    .GetAllIncluding(a => a.Doctor, a => a.Patient);

            // Apply additional filtering based on doctorId, startDate, endDate if needed
            appointments = appointments.Where(a => a.DoctorId == doctorId &&
                                                   (startDate == null || a.AppointmentDate >= startDate.Value) &&
                                                   (endDate == null || a.AppointmentDate <= endDate.Value));

            if (startDate == null && endDate == null)
            {
                appointments = appointments.Where(a => a.AppointmentDate.Date == today);
            }

            var doctor = await _doctorUnitOfWork.Entity.GetByIdAsync(doctorId);
            var schedule = await _userRepository.GetScheduleByDoctorIdAsync(doctorId);

            if (doctor == null || schedule == null)
            {
                return NotFound();
            }

            var customText = $"Doctor {doctor.FirstName} {doctor.LastName} works from {schedule.StartTime.Hours} o'clock to {schedule.EndTime.Hours} o'clock every day in the week except {schedule.DayOff}.";

            var viewModel = new DoctorAppointmentsViewModel
            {
                Appointments = appointments,
                Doctor = new DoctorViewModel
                {
                    Id = doctor.Id,
                    FirstName = doctor.FirstName,
                    LastName = doctor.LastName,
                    StartTime = schedule.StartTime,
                    EndTime = schedule.EndTime,
                    DayOff = schedule.DayOff
                },
                CustomText = customText
            };

            return View("Index", viewModel);
        }

    }
}
