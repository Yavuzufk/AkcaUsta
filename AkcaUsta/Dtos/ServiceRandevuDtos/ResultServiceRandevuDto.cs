using System.ComponentModel.DataAnnotations;

namespace AkcaUsta.Dtos.ServiceRandevuDtos
{
    public class ResultServiceRandevuDto
    {
        public int ServiceRandevuID { get; set; }

        public string Name { get; set; } = null!;

        [EmailAddress]
        public string Mail { get; set; } = null!;

        public string Service { get; set; } = null!;

        public DateTime Date { get; set; }

        public string? SpecialRequest { get; set; }
        public bool? RandevuStatus { get; set; }
    }
}
