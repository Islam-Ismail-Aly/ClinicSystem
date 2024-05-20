using ClinicSystem.Core.Entities;

namespace ClinicSystem.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<Secretary> GetSecretaryByIdAsync(string id);
    }
}
