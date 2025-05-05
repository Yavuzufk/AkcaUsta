using AkcaUsta.Context;
using AkcaUsta.Entity;
using AkcaUsta.Repository.IRepository;

namespace AkcaUsta.Repositories.Repository
{
    public class BuisnessDataRepository : GenericRepository<BuisnessData>, IBuisnessDataDAl
    {
        public BuisnessDataRepository(AppDbContext context) : base(context)
        {
        }
    }
}
