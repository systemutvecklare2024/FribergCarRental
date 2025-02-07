using FribergCarRental.Models.Entities;

namespace FribergCarRental.data
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User?>> AllWithBookingsAsync();
        Task<User?> FindByUsername(string username);
        Task<User?> GetWithBookingsAsync(int id);
        Task<User?> GetAccount(string username);
    }
}
