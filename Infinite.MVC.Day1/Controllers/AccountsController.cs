using Infinite.MVC.Day1.Models;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace Infinite.MVC.Day1.Controllers
{
    public class AccountsController : Controller
    {
        private readonly ApplicationDbContext _dbContext = null;

        public AccountsController()
        {
            _dbContext = new ApplicationDbContext();
        }

        [HttpGet]
        public ActionResult Register()
        {
           return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User user = new User
                    {
                        EmailId = registerViewModel.EmailId,
                        Username = registerViewModel.Username,
                        Password = registerViewModel.Password
                    };
                    _dbContext.Users.Add(user);
                    _dbContext.SaveChanges();
                }
            }
            catch (DbUpdateException ex) 
            {                
                ModelState.AddModelError("", ex.InnerException.InnerException.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
    }
}