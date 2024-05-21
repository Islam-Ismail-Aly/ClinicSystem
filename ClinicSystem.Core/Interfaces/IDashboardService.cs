using ClinicSystem.Core.DTOs;

namespace ClinicSystem.Core.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardDto> GetDashboardDataAsync(bool orderAppointmentsAscending);
    }
}
