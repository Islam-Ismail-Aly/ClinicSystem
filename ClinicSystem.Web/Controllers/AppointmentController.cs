using ClinicSystem.Core.Entities;
using ClinicSystem.Core.Interfaces;
using ClinicSystem.Web.ViewModels.AppointmentManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

public class AppointmentController : Controller
{
    private readonly IUnitOfWork<Appointment> _appointmentUnitOfWork;
    private readonly IUnitOfWork<Doctor> _doctorUnitOfWork;
    private readonly IUnitOfWork<Patient> _patientUnitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly UserManager<ApplicationUser> _userManager;

    public AppointmentController(IUnitOfWork<Appointment> appointmentUnitOfWork,
        IUnitOfWork<Doctor> doctorUnitOfWork, IUnitOfWork<Patient> patientUnitOfWork, 
        UserManager<ApplicationUser> userManager, IUserRepository userRepository)
    {
        _appointmentUnitOfWork = appointmentUnitOfWork;
        _doctorUnitOfWork = doctorUnitOfWork;
        _patientUnitOfWork = patientUnitOfWork;
        _userManager = userManager;
        _userRepository = userRepository;
    }

    public async Task<IActionResult> Index()
    {
        var doctors = await _doctorUnitOfWork.Entity.GetAllAsync();
        var patients = await _patientUnitOfWork.Entity.GetAllAsync();

        var viewModel = new AppointmentViewModel
        {
            Doctors = doctors.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = string.Concat(d.FirstName, " ", d.LastName),
            }).ToList(),

            Patients = patients.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name
            }).ToList(),
            Appointments = _appointmentUnitOfWork.Entity.GetAllIncluding(d=>d.Doctor, p=>p.Patient).ToList()
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveAppointment(AppointmentViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return Json(new { success = false, message = "Validation errors occurred.", errors });
        }

        try
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var secretary = await _userRepository.GetSecretaryByIdAsync(currentUser.Id);

            if (secretary == null)
            {
                return Json(new { success = false, message = "Secretary not found." });
            }

            var appointment = new Appointment
            {
                AppointmentDate = model.AppointmentDate,
                StartTime = model.StartTime,
                Duration = model.Duration,
                DoctorId = model.DoctorId,
                PatientId = model.PatientId,
                SecretaryId = secretary.Id
            };

            await _appointmentUnitOfWork.Entity.InsertAsync(appointment);
            await _appointmentUnitOfWork.SaveAsync();

            return Json(new { success = true, message = "Appointment saved successfully." });
        }
        catch (Exception ex)
        {
            // Log the exception (ex)
            return Json(new { success = false, message = "An error occurred while saving the appointment." });
        }
    }


    [HttpPost]
    public async Task<IActionResult> UpdateAppointment(int id, AppointmentViewModel model)
    {
        if (ModelState.IsValid)
        {
            var appointment = await _appointmentUnitOfWork.Entity.GetByIdAsync(id);
            if (appointment == null)
            {
                return Json(new { success = false, message = "Appointment not found." });
            }

            appointment.AppointmentDate = model.AppointmentDate;
            appointment.StartTime = model.StartTime;
            appointment.Duration = model.Duration;
            appointment.DoctorId = model.DoctorId;
            appointment.PatientId = model.PatientId;

            await _appointmentUnitOfWork.Entity.UpdateAsync(appointment);
            await _appointmentUnitOfWork.SaveAsync();

            return Json(new { success = true, message = "Appointment updated successfully." });
        }

        var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
        return Json(new { success = false, message = "Error updating appointment.", errors });
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAppointment(int id)
    {
        var appointment = await _appointmentUnitOfWork.Entity.GetByIdAsync(id);
        if (appointment == null)
        {
            return Json(new { success = false, message = "Appointment not found." });
        }

        await _appointmentUnitOfWork.Entity.DeleteAsync(appointment);
        await _appointmentUnitOfWork.SaveAsync();

        return Json(new { success = true, message = "Appointment deleted successfully." });
    }

    public async Task<IActionResult> GetAppointment(int id)
    {
        var appointment = await _appointmentUnitOfWork.Entity.GetByIdAsync(id);
        if (appointment == null)
        {
            return NotFound();
        }

        var model = new AppointmentViewModel
        {
            AppointmentDate = appointment.AppointmentDate,
            StartTime = appointment.StartTime,
            Duration = appointment.Duration,
            DoctorId = appointment.DoctorId,
            PatientId = appointment.PatientId
        };

        return Json(model);
    }
}
