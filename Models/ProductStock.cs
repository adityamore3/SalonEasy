namespace SalonEasy.Models
{
    public class ProductStock
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int QuantityAvailable { get; set; }
    }
}
