using System;
using System.ComponentModel.DataAnnotations;

namespace SalonEasy.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Position { get; set; }  // Stylist, Barber, Receptionist, etc.

        [Phone]
        public string Contact { get; set; }

        [EmailAddress]
        public string Email { get; set; }     // Optional email field

        public DateTime HireDate { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true; // Active/Inactive status

        public string ProfilePictureUrl { get; set; }  // Optional avatar

        [Range(1, 5)]
        public double Rating { get; set; } = 4.5;  // Customer rating (1–5)

        public string Bio { get; set; } // Short description or skills
    }
}
