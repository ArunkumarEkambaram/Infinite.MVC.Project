using Infinite.MVC.Day1.Models;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

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
            // var roles = _dbContext.Roles.ToList();
            RegisterViewModel viewModel = new RegisterViewModel
            {
                Roles = _dbContext.Roles.ToList()
            };
            return View(viewModel);
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

                    //Get the UserId
                    var userId = user.Id;
                    var roleId = registerViewModel.RoleId;
                    UserRolesMapping userRoles = new UserRolesMapping
                    {
                        UserId = userId,
                        RoleId = roleId
                    };

                    _dbContext.UserRolesMappings.Add(userRoles);
                    _dbContext.SaveChanges();

                    return RedirectToAction("Login");
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

            //Populate the Dropdownlist (Roles)
            RegisterViewModel viewModel = new RegisterViewModel
            {
                Roles = _dbContext.Roles.ToList()
            };
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            var isValid = _dbContext.Users.Any(u => u.Username == loginViewModel.Username && u.Password == loginViewModel.Password);
            if (isValid)
            {
                FormsAuthentication.SetAuthCookie(loginViewModel.Username, true);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Username or Password deosn't match.");
            return View();
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
        }
    }
}