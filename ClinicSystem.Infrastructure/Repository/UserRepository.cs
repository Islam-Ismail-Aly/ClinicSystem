using ClinicSystem.Core.Entities;
using ClinicSystem.Core.Interfaces;
using ClinicSystem.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Infrastructure.Repository
{
    public class UserRepository : IDisposable, IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public UserRepository(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<Secretary> GetSecretaryByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var result = await _context.Secretaries.FirstOrDefaultAsync(s => s.ApplicationUserId == user.Id);

            return result;
        }

        public async Task<Schedule> GetScheduleByDoctorIdAsync(int id)
        {
            var schedule = await _context.Schedules.FirstOrDefaultAsync(s => s.DoctorId == id);

            return schedule;
        }

        public void Dispose()
        {
            _userManager.Dispose();
        }


    }
}
