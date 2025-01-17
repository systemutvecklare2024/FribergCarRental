using FribergCarRental.Models;

namespace FribergCarRental.data
{
    public class ContactRepository : GenericRepository<Contact, ApplicationDbContext>, IContactRepository
    {
        public ContactRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
