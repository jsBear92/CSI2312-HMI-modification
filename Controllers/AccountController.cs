using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using HMI.Data;
using HMI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HMI.Controllers
{
    public class AccountController : Controller
    {
        private readonly HMIContext _context;

        public AccountController(HMIContext context)
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
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            bool userExists = _context.Accounts.FirstOrDefault(x => x.Username == username) != null;
            bool passwordExists = _context.Accounts.FirstOrDefault(x => x.UserPassword == password) != null;
            int userType = _context.Accounts.Where(x => x.Username == username).Select(x => x.UserType).SingleOrDefault();

            if (!string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password))
            {
                return RedirectToAction("Login");
            }

            ClaimsIdentity identity = null;
            bool loginAuth = false;

            if (userExists && passwordExists)
            {
                if (userType == 0)
                {
                    identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "Administrator")
                    }, CookieAuthenticationDefaults.AuthenticationScheme);
                }
                else
                {
                    identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "Operator")
                    }, CookieAuthenticationDefaults.AuthenticationScheme);
                }

                loginAuth = true;
            }

            if (loginAuth)
            {
                var principal = new ClaimsPrincipal(identity);

                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
