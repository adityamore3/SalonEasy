namespace SalonEasy.Models
{
    public class ProductUsed
    {
        public int Id { get; set; }
        public string AppointmentId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public int QuantityUsed { get; set; }
    }
}
