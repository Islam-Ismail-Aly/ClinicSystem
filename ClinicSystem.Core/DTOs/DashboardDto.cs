using ClinicSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicSystem.Core.DTOs
{
    public class DashboardDto
    {
        public int DoctorsCount { get; set; }
        public int PatientsCount { get; set; }
        public int UsersCount { get; set; }
        public int SecretariesCount { get; set; }
        public List<Appointment> LatestAppointments { get; set; }
    }
}
