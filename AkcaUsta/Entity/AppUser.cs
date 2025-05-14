using Microsoft.AspNetCore.Identity;

namespace AkcaUsta.Entity
{
    public class AppUser:IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string ShortDescription { get; set; } = null!;
    }
}
