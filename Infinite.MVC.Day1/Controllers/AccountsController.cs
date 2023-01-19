using Infinite.MVC.Day1.Models;
using System;
using System.Data.Entity;
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

                    ////Get the UserId
                    //var userId = user.Id;
                    //var roleId = registerViewModel.RoleId;
                    //UserRolesMapping userRoles = new UserRolesMapping
                    //{
                    //    UserId = userId,
                    //    RoleId = roleId
                    //};

                    //_dbContext.UserRolesMappings.Add(userRoles);
                    //_dbContext.SaveChanges();

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

        //Apply Role for User
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult AddRole()
        {
            UserRoleViewModel userRole = new UserRoleViewModel
            {
                Users = _dbContext.Users.ToList(),
                Roles = _dbContext.Roles.ToList()
            };
            return View(userRole);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddRole(UserRoleViewModel userRoleVM)
        {
            if (ModelState.IsValid)
            {
                UserRolesMapping userRolesMapping = new UserRolesMapping
                {
                    UserId = userRoleVM.UserId,
                    RoleId = userRoleVM.RoleId
                };
                _dbContext.UserRolesMappings.Add(userRolesMapping);
                _dbContext.SaveChanges();
            }

            UserRoleViewModel userRole = new UserRoleViewModel
            {
                Users = _dbContext.Users.ToList(),
                Roles = _dbContext.Roles.ToList()
            };
            return View(userRole);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult RolesAndUsers()
        {
            var userAndRoles = (from user in _dbContext.Users
                                join userRole in _dbContext.UserRolesMappings on user.Id equals userRole.UserId
                                join role in _dbContext.Roles on userRole.RoleId equals role.Id
                                orderby role.RoleName ascending
                                select new UsersAndRoles
                                {
                                    Username = user.Username,
                                    RoleName = role.RoleName
                                }).ToList();
            return View("RolesAndUsers", userAndRoles);
        }
    }
}