// Models/Appointment.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace SalonEasy.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required, StringLength(100)]
        public string ClientName { get; set; } = string.Empty;

        [Required, DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [RegularExpression(@"^(0[1-9]|1[0-2]):[0-5][0-9]\s?(?i)(AM|PM)$", ErrorMessage = "Time must be in hh:mm AM/PM format.")]
        public string Time { get; set; } = string.Empty;

        [StringLength(100)]
        public string ServiceType { get; set; } = string.Empty;

        [StringLength(250)]
        public string Notes { get; set; } = string.Empty;

        public bool IsConfirmed { get; set; }

        // Use UTC to avoid server-local variations
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
