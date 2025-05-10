using AkcaUsta.Entity;

namespace AkcaUsta.Repository.IRepository
{
    public interface IServiceDal: IGenericDal<Service>
    {
        Task<List<Service>> GetAllServicesWithFeaturesAsync();

    }
}
