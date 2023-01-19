using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Infinite.MVC.Day1.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string EmailId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required, StringLength(50)]
        public string Password { get; set; }

        //Navigation Property
        public ICollection<UserRolesMapping> UserRolesMappings { get; set; }
    }

    public class Role
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }
    }

    public class RegisterViewModel
    {
        [Required, StringLength(50)]
        [RegularExpression("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$", ErrorMessage = "Invalid Email Id")]
        public string EmailId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required, StringLength(50)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password and Confirm Password doesn't match")]
        public string ConfirmPassword { get; set; }

       // [Required]
        [Display(Name = "Roles")]
        public int RoleId { get; set; }

        public List<Role> Roles { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required, StringLength(50)]
        public string Password { get; set; }
    }

    public class UserRolesMapping
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }

        public Role Role { get; set; }
        public int RoleId { get; set; }
    }
}