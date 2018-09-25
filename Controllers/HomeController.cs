using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Exam.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Exam.Controllers
{
    public class HomeController : Controller
    {
        private ExamContext _Context;
        public HomeController(ExamContext Context)
        {
            _Context = Context;
        }
        [HttpGet("bright_ideas")]
        public IActionResult Index()
        {
            Console.WriteLine("==============================="+HttpContext.Session.GetInt32("UserId"));
            if (HttpContext.Session.GetInt32("UserId") == null ){
                return RedirectToAction("Index", "Login");
            }
            User user = _Context.Users.SingleOrDefault(u => u.UserId == (int)HttpContext.Session.GetInt32("UserId"));
            List<Idea> allIdeas= _Context.Ideas.Include(u => u.Owner).Include(l => l.LikedBy).OrderByDescending(l => l.LikedBy.Count).ToList();
            ViewBag.AllIdeas = allIdeas;
            ViewBag.name = (string)user.Alias;
            ViewBag.CurrentUser = user;
            return View();
            
        }
        [HttpPost("newidea")]
        public IActionResult PostNewIdea(string UserIdea)
        {
            if (HttpContext.Session.GetInt32("UserId") == null ){
                return RedirectToAction("Index", "Login");
            }
            if (UserIdea.Length > 0)
            {
                Idea newIdea = new Idea()
                {
                    UserId = (int)HttpContext.Session.GetInt32("UserId"),
                    UserIdea = UserIdea
                };
                _Context.Ideas.Add(newIdea);
                _Context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpGet("like/{ideaId}")]
        public IActionResult PostLike(int ideaId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null ){
                return RedirectToAction("Index", "Login");
            }
            Like newLike = new Like()
            {
                IdeasId = ideaId,
                UserId = (int)HttpContext.Session.GetInt32("UserId")
            };
            _Context.Likes.Add(newLike);
            _Context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet("users/{userId}")]
        public IActionResult Users(int userId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null ){
                return RedirectToAction("Index", "Login");
            } 
            ViewBag.SelectedUser = _Context.Users.Where(u => u.UserId == userId)
                                                 .Include(i => i.UsersIdeas)
                                                 .Include(l => l.UserLikes).Single(); 
            return View();
        }
        [HttpGet("bright_ideas/{ideaId}")]
        public IActionResult Idea(int ideaId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null ){
                return RedirectToAction("Index", "Login");
            } 
            ViewBag.SelectedIdea = _Context.Ideas.Where(i => i.IdeasId == ideaId)
                                                 .Include(u => u.Owner)
                                                 .Include(l => l.LikedBy)
                                                 .ThenInclude(u => u.User).Single(); 
            ViewBag.LikedBy = _Context.Likes.Where(i => i.IdeasId == ideaId).ToList();
            return View();
        }
        [HttpGet("deleteidea/{ideaId}")]
        public IActionResult DeleteIdea(int ideaId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null ){
                return RedirectToAction("Index", "Login");
            } 
            var ToDelete = new Idea { IdeasId = ideaId };
            _Context.Ideas.Attach(ToDelete);
            _Context.Ideas.Remove(ToDelete);
            _Context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
