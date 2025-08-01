using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SalonEasy.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SalonEasy.Controllers
{
    public class AdminController : Controller
    {
        private static readonly List<Admin> admins = new()
        {
            new Admin { Id = 1, Username = "admin", Password = "admin123" }
        };

        private static readonly List<LeaveRequest> _leaves = new()
        {
            new LeaveRequest { Id = 1, Employee = "Neha Verma", From = DateTime.Parse("2025-08-01"), To = DateTime.Parse("2025-08-02"), Reason = "Medical Leave", Status = "Approved" },
            new LeaveRequest { Id = 2, Employee = "Amit Singh", From = DateTime.Parse("2025-07-25"), To = DateTime.Parse("2025-07-27"), Reason = "Personal", Status = "Approved" },
            new LeaveRequest { Id = 3, Employee = "Parth", From = DateTime.Parse("2025-08-15"), To = DateTime.Parse("2025-08-20"), Reason = "Family event", Status = "Pending" },
            new LeaveRequest { Id = 4, Employee = "Ritika Shah", From = DateTime.Parse("2025-09-05"), To = DateTime.Parse("2025-09-07"), Reason = "Marriage", Status = "Pending" },
            new LeaveRequest { Id = 5, Employee = "Kunal Mehra", From = DateTime.Parse("2025-10-10"), To = DateTime.Parse("2025-10-11"), Reason = "Vacation", Status = "Rejected" }
        };

        private static readonly List<ProductUsed> _used = new()
        {
            new ProductUsed { Id = 1, AppointmentId = "APT-1023", ProductName = "Loreal Shampoo", QuantityUsed = 1 },
            new ProductUsed { Id = 2, AppointmentId = "APT-1024", ProductName = "Nivea Cream", QuantityUsed = 2 },
            new ProductUsed { Id = 3, AppointmentId = "APT-1025", ProductName = "Matrix Conditioner", QuantityUsed = 1 },
            new ProductUsed { Id = 4, AppointmentId = "APT-1026", ProductName = "Lakme Serum", QuantityUsed = 1 },
            new ProductUsed { Id = 5, AppointmentId = "APT-1027", ProductName = "Dove Hair Oil", QuantityUsed = 2 }
        };

        private static readonly List<ProductStock> _stock = new()
        {
            new ProductStock { Id = 1, ProductName = "Loreal Shampoo", QuantityAvailable = 15 },
            new ProductStock { Id = 2, ProductName = "Nivea Cream", QuantityAvailable = 3 },
            new ProductStock { Id = 3, ProductName = "Matrix Conditioner", QuantityAvailable = 8 },
            new ProductStock { Id = 4, ProductName = "Lakme Serum", QuantityAvailable = 12 },
            new ProductStock { Id = 5, ProductName = "Dove Hair Oil", QuantityAvailable = 20 },
            new ProductStock { Id = 6, ProductName = "Himalaya Face Wash", QuantityAvailable = 18 },
            new ProductStock { Id = 7, ProductName = "Biotique Toner", QuantityAvailable = 7 },
            new ProductStock { Id = 8, ProductName = "Pantene Conditioner", QuantityAvailable = 10 },
            new ProductStock { Id = 9, ProductName = "Tresemme Shampoo", QuantityAvailable = 5 },
            new ProductStock { Id = 10, ProductName = "Mamaearth Face Cream", QuantityAvailable = 9 }
        };

        private static int _leaveId = 6, _usedId = 6, _stockId = 11;

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Admin") != null)
                return RedirectToAction("Dashboard");

            return View();
        }

        [HttpPost]
        public IActionResult Login(Admin model)
        {
            var admin = admins.FirstOrDefault(a => a.Username == model.Username && a.Password == model.Password);

            if (admin != null)
            {
                HttpContext.Session.SetString("Admin", admin.Username);
                return RedirectToAction("Dashboard");
            }

            ViewBag.Error = "Invalid username or password";
            return View();
        }

        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("Admin") == null)
                return RedirectToAction("Login");

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Admin");
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult LeaveRequest()
        {
            if (HttpContext.Session.GetString("Admin") == null)
                return RedirectToAction("Login");
            return View(_leaves);
        }

        [HttpPost]
        public IActionResult AddLeave(LeaveRequest l)
        {
            l.Id = _leaveId++;
            _leaves.Add(l);
            return RedirectToAction("LeaveRequest");
        }

        [HttpPost]
        public IActionResult UpdateLeave(LeaveRequest l)
        {
            var x = _leaves.FirstOrDefault(i => i.Id == l.Id);
            if (x != null)
            {
                x.Employee = l.Employee;
                x.From = l.From;
                x.To = l.To;
                x.Reason = l.Reason;
                x.Status = l.Status;
            }
            return RedirectToAction("LeaveRequest");
        }

        [HttpPost]
        public IActionResult DeleteLeave(int id)
        {
            var x = _leaves.FirstOrDefault(i => i.Id == id);
            if (x != null) _leaves.Remove(x);
            return RedirectToAction("LeaveRequest");
        }

        [HttpGet]
        public IActionResult ProductUsed()
        {
            if (HttpContext.Session.GetString("Admin") == null)
                return RedirectToAction("Login");
            return View(_used);
        }

        [HttpPost]
        public IActionResult AddProductUsed(ProductUsed p)
        {
            p.Id = _usedId++;
            _used.Add(p);
            return RedirectToAction("ProductUsed");
        }

        [HttpPost]
        public IActionResult UpdateProductUsed(ProductUsed p)
        {
            var x = _used.FirstOrDefault(i => i.Id == p.Id);
            if (x != null)
            {
                x.AppointmentId = p.AppointmentId;
                x.ProductName = p.ProductName;
                x.QuantityUsed = p.QuantityUsed;
            }
            return RedirectToAction("ProductUsed");
        }

        [HttpPost]
        public IActionResult DeleteProductUsed(int id)
        {
            var x = _used.FirstOrDefault(i => i.Id == id);
            if (x != null) _used.Remove(x);
            return RedirectToAction("ProductUsed");
        }

        [HttpGet]
        public IActionResult ProductStock()
        {
            if (HttpContext.Session.GetString("Admin") == null)
                return RedirectToAction("Login");
            return View(_stock);
        }

        [HttpPost]
        public IActionResult AddProductStock(ProductStock p)
        {
            p.Id = _stockId++;
            _stock.Add(p);
            return RedirectToAction("ProductStock");
        }

        [HttpPost]
        public IActionResult UpdateProductStock(ProductStock p)
        {
            var x = _stock.FirstOrDefault(i => i.Id == p.Id);
            if (x != null)
            {
                x.ProductName = p.ProductName;
                x.QuantityAvailable = p.QuantityAvailable;
            }
            return RedirectToAction("ProductStock");
        }

        [HttpPost]
        public IActionResult DeleteProductStock(int id)
        {
            var x = _stock.FirstOrDefault(i => i.Id == id);
            if (x != null) _stock.Remove(x);
            return RedirectToAction("ProductStock");
        }
    }
}
