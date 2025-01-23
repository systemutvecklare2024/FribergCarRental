using FribergCarRental.Models.Entities;

namespace FribergCarRental.data
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> FindByUsername(string username);
    }
}
