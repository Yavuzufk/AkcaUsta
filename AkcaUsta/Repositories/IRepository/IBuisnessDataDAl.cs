using AkcaUsta.Dtos.BuisnessDataDtos;
using AkcaUsta.Entity;

namespace AkcaUsta.Repository.IRepository
{
    public interface IBuisnessDataDAl: IGenericDal<BuisnessData>
    {
        Task AddToExistingDataAsync(ResultBuisnessDataDto resultBuisnessDataDto);
    }
}
