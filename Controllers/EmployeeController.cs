using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SalonEasy.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalonEasy.Controllers
{
    public class EmployeeController : Controller
    {
        // In-memory list of employees
        private static readonly List<Employee> employees = new()
        {
            new Employee { Id = 1, Name = "Anjali", Position = "Stylist", Contact = "999-123-4567", Email = "anjali@saloneasy.com", HireDate = new DateTime(2022,5,10), IsActive = true,  Rating = 4.8, Bio = "Specialist in bridal makeup and hair styling.", ProfilePictureUrl = "https://via.placeholder.com/100" },
            new Employee { Id = 2, Name = "Rahul",  Position = "Barber",  Contact = "888-456-7890", Email = "rahul@saloneasy.com", HireDate = new DateTime(2021,11,3), IsActive = true,  Rating = 4.5, Bio = "Master of fades, beards, and modern trims.", ProfilePictureUrl = "https://via.placeholder.com/100" },
            new Employee { Id = 3, Name = "Pooja",  Position = "Makeup Artist", Contact = "777-987-6543", Email = "pooja@saloneasy.com", HireDate = new DateTime(2023,2,20), IsActive = true,  Rating = 4.9, Bio = "Certified makeup artist with 5 years of experience.", ProfilePictureUrl = "https://via.placeholder.com/100" },
            new Employee { Id = 4, Name = "Amit",   Position = "Hairdresser", Contact = "666-654-3210", Email = "amit@saloneasy.com", HireDate = new DateTime(2020,8,15), IsActive = true,  Rating = 4.4, Bio = "Expert in hair coloring and styling for all ages.", ProfilePictureUrl = "https://via.placeholder.com/100" },
            new Employee { Id = 5, Name = "Neha",   Position = "Spa Therapist", Contact = "555-321-7890", Email = "neha@saloneasy.com", HireDate = new DateTime(2022,12,5),IsActive = true, Rating = 4.7, Bio = "Relaxation expert in massage, facials, and body treatments.", ProfilePictureUrl = "https://via.placeholder.com/100" }
        };

        // 🔒 Redirect to login if no "Admin" in session
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (HttpContext.Session.GetString("Admin") == null)
            {
                context.Result = RedirectToAction("Login", "Admin");
                return;
            }
            base.OnActionExecuting(context);
        }

        public IActionResult Index() => View(employees);

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Employee emp)
        {
            emp.Id = employees.Max(e => e.Id) + 1;
            emp.HireDate = DateTime.Now;
            emp.IsActive = true;
            employees.Add(emp);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var emp = employees.FirstOrDefault(e => e.Id == id);
            if (emp == null) return NotFound();
            return View(emp);
        }

        [HttpPost]
        public IActionResult Edit(Employee emp)
        {
            var existing = employees.FirstOrDefault(e => e.Id == emp.Id);
            if (existing != null)
            {
                existing.Name = emp.Name;
                existing.Position = emp.Position;
                existing.Contact = emp.Contact;
                existing.Email = emp.Email;
                existing.ProfilePictureUrl = emp.ProfilePictureUrl;
                existing.Rating = emp.Rating;
                existing.Bio = emp.Bio;
                existing.IsActive = emp.IsActive;
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var emp = employees.FirstOrDefault(e => e.Id == id);
            if (emp == null) return NotFound();
            return View(emp);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var emp = employees.FirstOrDefault(e => e.Id == id);
            if (emp != null) employees.Remove(emp);
            return RedirectToAction("Index");
        }
    }
}
