using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Exam.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Exam.Controllers
{
    public class LoginController : Controller
    {
        private ExamContext _Context;
        public LoginController(ExamContext Context)
        {
            _Context = Context;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("register")]
        public IActionResult Register(User userReg)
        {
            if (ModelState.IsValid){
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                userReg.Password = Hasher.HashPassword(userReg, userReg.Password);
                _Context.Add(userReg);
                _Context.SaveChanges();
                HttpContext.Session.SetInt32("UserId", userReg.UserId);
                return RedirectToAction("Index", "Home");
            }
            else {
                return View("Index", userReg);
            }
            
        }
        [HttpPost("login")]
        public IActionResult Login(User userLog)
        {
            var user = _Context.Users.SingleOrDefault(u => u.Email == userLog.Email);
            if(user != null && userLog.Password != null)
            {
                var Hasher = new PasswordHasher<User>();
                if(0 != Hasher.VerifyHashedPassword(user, user.Password, userLog.Password))
                {
                    HttpContext.Session.SetInt32("UserId", user.UserId);
                        return RedirectToAction("Index", "Home");
                }
            }
            return View("Login", userLog);
        }
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
