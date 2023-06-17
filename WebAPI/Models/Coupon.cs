namespace WebAPI.Models
{
    public class Coupon
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Percent { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
