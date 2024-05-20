using Clinic.Infrastructure.Repository;
using ClinicSystem.Core.Interfaces;
using ClinicSystem.Infrastructure.Data;

namespace Clinic.Infrastructure.UnitOfWork
{
    public class UnitOfWork<T> : IDisposable, IUnitOfWork<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private IGenericRepository<T> _entity;
        private readonly IUserRepository _userRepository;

        public UnitOfWork(ApplicationDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public IGenericRepository<T> Entity
        {
            get
            {
                return _entity ?? (_entity = new GenericRepository<T>(_context));
            }
        }

        public IUserRepository UserRepository => _userRepository;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
