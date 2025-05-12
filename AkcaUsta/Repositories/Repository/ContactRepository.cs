using AkcaUsta.Context;
using AkcaUsta.Entity;
using AkcaUsta.Repositories.IRepository;

namespace AkcaUsta.Repositories.Repository
{
    public class ContactRepository : GenericRepository<Contact>, IContactDal
    {
        public ContactRepository(AppDbContext context) : base(context)
        {
        }
    }
}
