
using Microsoft.EntityFrameworkCore.Storage;

namespace FribergCarRental.data.UnitOfWork
{
    public class UserContactUnitOfWork : IUserContactUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction _transaction;
        public IUserRepository UserRepository { get; }

        public IContactRepository ContactRepository { get; }

        public UserContactUnitOfWork(ApplicationDbContext context, IUserRepository userRepository, IContactRepository contactRepository)
        {
            _context = context;
            UserRepository = userRepository;
            ContactRepository = contactRepository;
        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction?.Rollback();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context?.Dispose();
        }
    }
}
