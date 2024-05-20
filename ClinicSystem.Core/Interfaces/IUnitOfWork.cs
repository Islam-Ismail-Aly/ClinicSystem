namespace ClinicSystem.Core.Interfaces
{
    public interface IUnitOfWork<T> where T : class
    {
        IGenericRepository<T> Entity { get; }
        IUserRepository UserRepository { get; }
        Task SaveAsync();
    }
}
