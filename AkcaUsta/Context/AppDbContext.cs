using AkcaUsta.Entity;
using Microsoft.EntityFrameworkCore;

namespace AkcaUsta.Context
{
    public class AppDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-A82GBS0\\SQLEXPRESS; Database=AkcaUstaDB;Integrated Security=True;TrustServerCertificate=True;");
        }

        public DbSet<About> Abouts { get; set; }
        public DbSet<BuisnessData> BuisnessDatas { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceFeature> ServiceFeatures { get; set; }
        public DbSet<ServiceRandevu> ServiceRandevus { get; set; }
    }
}
