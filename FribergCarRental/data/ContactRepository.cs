using FribergCarRental.Models.Entities;

namespace FribergCarRental.data
{
    public class ContactRepository : GenericRepository<Contact, ApplicationDbContext>, IContactRepository
    {
        public ContactRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
