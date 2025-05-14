using System.ComponentModel.DataAnnotations;

namespace AkcaUsta.Dtos.AppUserDtos
{
    public class UserLoginDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }  // Beni Hatırla alanı

    }
}
