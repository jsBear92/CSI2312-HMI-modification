using Microsoft.AspNetCore.Mvc;
using HMI.Data;
using HMI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMI.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly HMIContext _context;

        public RegistrationController (HMIContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Account account)
        {
            _context.Add(account);
            _context.SaveChanges();
            ViewBag.message = "The user " + account.Username + " is saved successfully";
            return View();
        }
    }
}
