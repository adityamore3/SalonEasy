using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SalonEasy.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SalonEasy.Controllers
{
    [Route("[controller]")]
    public class AppointmentController : Controller
    {
        // In‐memory store with 5 dummy appointments
        private static readonly List<Appointment> _appointments = new()
        {
            new Appointment
            {
                Id = 1,
                EmployeeId = 1,
                ClientName = "Priya Sharma",
                Date = DateTime.Today.AddDays(1),
                Time = "10:00 AM",
                ServiceType = "Haircut",
                Notes = "Layered cut with wash",
                IsConfirmed = true,
                CreatedAt = DateTime.UtcNow.AddDays(-2)
            },
            new Appointment
            {
                Id = 2,
                EmployeeId = 2,
                ClientName = "Amit Verma",
                Date = DateTime.Today.AddDays(2),
                Time = "02:30 PM",
                ServiceType = "Beard Trim",
                Notes = "Classic beard shaping",
                IsConfirmed = false,
                CreatedAt = DateTime.UtcNow.AddDays(-1)
            },
            new Appointment
            {
                Id = 3,
                EmployeeId = 3,
                ClientName = "Meera Nair",
                Date = DateTime.Today.AddDays(3),
                Time = "11:00 AM",
                ServiceType = "Bridal Makeup",
                Notes = "Trial session",
                IsConfirmed = true,
                CreatedAt = DateTime.UtcNow.AddDays(-3)
            },
            new Appointment
            {
                Id = 4,
                EmployeeId = 4,
                ClientName = "Sandeep Singh",
                Date = DateTime.Today.AddDays(4),
                Time = "01:00 PM",
                ServiceType = "Hair Color",
                Notes = "Full hair coloring - brown",
                IsConfirmed = true,
                CreatedAt = DateTime.UtcNow.AddDays(-4)
            },
            new Appointment
            {
                Id = 5,
                EmployeeId = 5,
                ClientName = "Ritika Jain",
                Date = DateTime.Today.AddDays(5),
                Time = "03:00 PM",
                ServiceType = "Facial",
                Notes = "Gold facial",
                IsConfirmed = false,
                CreatedAt = DateTime.UtcNow.AddDays(-5)
            }
        };

        // 🔒 Enforce admin‐only access
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (HttpContext.Session.GetString("Admin") == null)
            {
                context.Result = RedirectToAction("Login", "Admin");
                return;
            }
            base.OnActionExecuting(context);
        }

        // GET: /Appointment
        [HttpGet("")]
        public IActionResult Index()
        {
            var sorted = _appointments
                .OrderBy(a => CombineDateTime(a.Date, a.Time))
                .ToList();

            return View(sorted);
        }

        // GET: /Appointment/Create
        [HttpGet("Create")]
        public IActionResult Create() => View();

        // POST: /Appointment/Create
        [HttpPost("Create"), ValidateAntiForgeryToken]
        public IActionResult Create(Appointment appointment)
        {
            if (!ModelState.IsValid)
                return View(appointment);

            appointment.Id = _appointments.Any()
                ? _appointments.Max(a => a.Id) + 1
                : 1;
            appointment.CreatedAt = DateTime.UtcNow;
            _appointments.Add(appointment);

            return RedirectToAction(nameof(Index));
        }

        // GET: /Appointment/Edit?id=5
        [HttpGet("Edit")]
        public IActionResult Edit(int id)
        {
            var appt = _appointments.FirstOrDefault(a => a.Id == id);
            if (appt == null) return NotFound();
            return View(appt);
        }

        // POST: /Appointment/Edit
        [HttpPost("Edit"), ValidateAntiForgeryToken]
        public IActionResult Edit(Appointment updated)
        {
            if (!ModelState.IsValid)
                return View(updated);

            var existing = _appointments.FirstOrDefault(a => a.Id == updated.Id);
            if (existing == null) return NotFound();

            existing.EmployeeId = updated.EmployeeId;
            existing.ClientName = updated.ClientName;
            existing.Date = updated.Date;
            existing.Time = updated.Time;
            existing.ServiceType = updated.ServiceType;
            existing.Notes = updated.Notes;
            existing.IsConfirmed = updated.IsConfirmed;

            return RedirectToAction(nameof(Index));
        }

        // GET: /Appointment/Delete?id=5
        [HttpGet("Delete")]
        public IActionResult Delete(int id)
        {
            var appt = _appointments.FirstOrDefault(a => a.Id == id);
            if (appt == null) return NotFound();
            return View(appt);
        }

        // POST: /Appointment/DeleteConfirmed
        [HttpPost("DeleteConfirmed"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var appt = _appointments.FirstOrDefault(a => a.Id == id);
            if (appt != null)
                _appointments.Remove(appt);

            return RedirectToAction(nameof(Index));
        }

        // Helper: parse and combine Date + Time
        private static DateTime CombineDateTime(DateTime date, string timeStr)
        {
            if (DateTime.TryParseExact(
                    timeStr.Trim().ToUpperInvariant(),
                    "hh:mm tt",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var t))
            {
                return date.Date.Add(t.TimeOfDay);
            }
            return date.Date;
        }
    }
}
