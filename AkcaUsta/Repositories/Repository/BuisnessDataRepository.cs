using AkcaUsta.Context;
using AkcaUsta.Dtos.BuisnessDataDtos;
using AkcaUsta.Entity;
using AkcaUsta.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace AkcaUsta.Repositories.Repository
{
    public class BuisnessDataRepository : GenericRepository<BuisnessData>, IBuisnessDataDAl
    {
        public BuisnessDataRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddToExistingDataAsync(ResultBuisnessDataDto resultBuisnessDataDto)
        {
            using var _context = new AppDbContext();
            // Veritabanından mevcut veriyi alıyoruz (örnek olarak ilk veriyi alıyoruz)
            var existingData = await _context.BuisnessDatas.FirstOrDefaultAsync();

            if (existingData == null)
            {
                // Eğer veri bulunamazsa bir hata fırlatabilirsiniz veya yeni bir veri oluşturabilirsiniz
                throw new Exception("Veri bulunamadı");
            }

            // Yeni gelen verilerle mevcut veriyi güncelliyoruz
            existingData.BuisnessDataID = resultBuisnessDataDto.BuisnessDataID;
            existingData.ExpertTechnicians += resultBuisnessDataDto.ExpertTechnicians;
            existingData.SatisfiedClients += resultBuisnessDataDto.SatisfiedClients;
            existingData.CompleateProjects += resultBuisnessDataDto.CompleateProjects;

            // Veritabanındaki mevcut veriyi güncelliyoruz
            _context.BuisnessDatas.Update(existingData); // Entity Framework ile mevcut veriyi güncelliyoruz

            // Değişiklikleri kaydediyoruz, tek bir SaveChangesAsync yeterli
            await _context.SaveChangesAsync();

        }
    }
}
