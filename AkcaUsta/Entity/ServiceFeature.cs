namespace AkcaUsta.Entity
{
    public class ServiceFeature
    {
        public int ServiceFeatureID { get; set; }

        public int ServiceId { get; set; }
        public Service Service { get; set; } = null!;

        public string Feature { get; set; } = null!;
    }
}
