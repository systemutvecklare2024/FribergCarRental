using FribergCarRental.Models.Entities;

namespace FribergCarRental.data
{
    public class CarRepository : GenericRepository<Car, ApplicationDbContext>, ICarRepository
    {
        public CarRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
