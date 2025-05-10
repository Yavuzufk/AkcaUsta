using AkcaUsta.Context;
using AkcaUsta.Entity;
using AkcaUsta.Repositories.IRepository;
using AkcaUsta.Repository.IRepository;

namespace AkcaUsta.Repositories.Repository
{
    public class TechnicianRepository : GenericRepository<Technician>, ITechnicianDal
    {
        public TechnicianRepository(AppDbContext context) : base(context)
        {
        }
    }
}
