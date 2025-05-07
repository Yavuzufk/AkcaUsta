using AkcaUsta.Entity;

namespace AkcaUsta.Repository.IRepository
{
    public interface IServiceFeatureDal: IGenericDal<ServiceFeature>
    {
       Task<List<ServiceFeature>> GetFeaturesByServiceID(int ServiceID);
    }
}
