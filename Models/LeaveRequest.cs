namespace SalonEasy.Models
{
    public class LeaveRequest
    {
        public int Id { get; set; }
        public string Employee { get; set; } = string.Empty;
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
    }
}
