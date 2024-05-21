using ClinicSystem.Core.DTOs;
using ClinicSystem.Core.Entities;
using ClinicSystem.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicSystem.Infrastructure.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ICommonRepository<Doctor> _doctorRepository;
        private readonly ICommonRepository<Patient> _patientRepository;
        private readonly ICommonRepository<ApplicationUser> _userRepository;
        private readonly ICommonRepository<Secretary> _secretaryRepository;
        private readonly ICommonRepository<Appointment> _appointmentRepository;

        public DashboardService(
            ICommonRepository<Doctor> doctorRepository,
            ICommonRepository<Patient> patientRepository,
            ICommonRepository<ApplicationUser> userRepository,
            ICommonRepository<Secretary> secretaryRepository,
            ICommonRepository<Appointment> appointmentRepository)
        {
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _userRepository = userRepository;
            _secretaryRepository = secretaryRepository;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<DashboardDto> GetDashboardDataAsync(bool orderAppointmentsAscending)
        {
            var latestAppointments = await _appointmentRepository.GetOrderedByIncludingAsync(
                a => a.AppointmentDate,
                orderAppointmentsAscending,
                a => a.Doctor,
                a => a.Patient);

            var dashboardData = new DashboardDto
            {
                DoctorsCount = await _doctorRepository.CountAsync(),
                PatientsCount = await _patientRepository.CountAsync(),
                UsersCount = await _userRepository.CountAsync(),
                SecretariesCount = await _secretaryRepository.CountAsync(),
                LatestAppointments = latestAppointments.Take(5).ToList()
            };

            return dashboardData;
        }
    }
}
