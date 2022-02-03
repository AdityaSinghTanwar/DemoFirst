using demoTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace demoTest.Controllers
{
    public class HomeController : Controller
    {
        WebTestEntities webTestEntities = new WebTestEntities();

        [HttpGet]
        public ActionResult Index()
        {
            //var user = webTestEntities.Users.ToList();
            var userResult = webTestEntities.GetUserNew(); // GetUserNew_Result
            var user = userResult.Select(s => new User
            {
                Email = s.Email,
                Password = s.Password,
                Id = s.Id
            }).ToList();
            return View(user); //User
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                webTestEntities.Users.Add(new User
                {
                    Email = user.Email,
                    Password = user.Password
                });
                webTestEntities.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var usr = webTestEntities.Users.FirstOrDefault(w => w.Id == id);
            if (usr != null)
            {
                webTestEntities.Users.Remove(usr);
                webTestEntities.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}