namespace AkcaUsta.Entity
{
    public class Service
    {
        public int ServiceID { get; set; }
        public string Icon { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public ICollection<ServiceFeature> Features { get; set; } = new List<ServiceFeature>();
    }
}
