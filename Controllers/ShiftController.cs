using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SalonEasy.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalonEasy.Controllers
{
    public class ShiftController : Controller
    {
        private static List<Shift> shifts = new List<Shift>
        {
            new Shift { Id = 1, EmployeeId = 1, StartTime = DateTime.Today.AddHours(9), EndTime = DateTime.Today.AddHours(17) },
            new Shift { Id = 2, EmployeeId = 2, StartTime = DateTime.Today.AddDays(1).AddHours(10), EndTime = DateTime.Today.AddDays(1).AddHours(18) },
            new Shift { Id = 3, EmployeeId = 3, StartTime = DateTime.Today.AddDays(2).AddHours(8), EndTime = DateTime.Today.AddDays(2).AddHours(16) },
            new Shift { Id = 4, EmployeeId = 4, StartTime = DateTime.Today.AddDays(3).AddHours(11), EndTime = DateTime.Today.AddDays(3).AddHours(19) },
            new Shift { Id = 5, EmployeeId = 5, StartTime = DateTime.Today.AddDays(4).AddHours(12), EndTime = DateTime.Today.AddDays(4).AddHours(20) }
        };

        // 🔒 Redirect to admin login if no session key
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (HttpContext.Session.GetString("Admin") == null)
            {
                context.Result = RedirectToAction("Login", "Admin");
                return;
            }
            base.OnActionExecuting(context);
        }

        // GET: /Shift/
        public IActionResult Index() => View(shifts);

        // GET: /Shift/Assign
        public IActionResult Assign() => View();

        // POST: /Shift/Assign
        [HttpPost]
        public IActionResult Assign(Shift shift)
        {
            shift.Id = shifts.Any() ? shifts.Max(s => s.Id) + 1 : 1;
            shifts.Add(shift);
            return RedirectToAction("Index");
        }

        // GET: /Shift/Edit/5
        public IActionResult Edit(int id)
        {
            var shift = shifts.FirstOrDefault(s => s.Id == id);
            if (shift == null) return NotFound();
            return View(shift);
        }

        // POST: /Shift/Edit
        [HttpPost]
        public IActionResult Edit(Shift updated)
        {
            var shift = shifts.FirstOrDefault(s => s.Id == updated.Id);
            if (shift == null) return NotFound();

            shift.EmployeeId = updated.EmployeeId;
            shift.StartTime = updated.StartTime;
            shift.EndTime = updated.EndTime;

            return RedirectToAction("Index");
        }

        // GET: /Shift/Delete/5
        public IActionResult Delete(int id)
        {
            var shift = shifts.FirstOrDefault(s => s.Id == id);
            if (shift == null) return NotFound();
            return View(shift);
        }

        // POST: /Shift/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var shift = shifts.FirstOrDefault(s => s.Id == id);
            if (shift != null) shifts.Remove(shift);
            return RedirectToAction("Index");
        }
    }
}
