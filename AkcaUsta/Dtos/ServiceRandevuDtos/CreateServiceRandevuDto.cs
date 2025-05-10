using System.ComponentModel.DataAnnotations;

namespace AkcaUsta.Dtos.ServiceRandevuDtos
{
    public class CreateServiceRandevuDto
    {
        [Required(ErrorMessage = "Ad alanı boş bırakılamaz.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Mail adresi gereklidir.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta giriniz.")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Hizmet seçilmelidir.")]
        public string Service { get; set; }

        [Required(ErrorMessage = "Tarih seçilmelidir.")]
        public DateTime Date { get; set; }

        public string? SpecialRequest { get; set; }
    }
}
