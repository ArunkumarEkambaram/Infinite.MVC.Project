using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Infinite.MVC.Day1.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("MyCon")
        {

        }

        //Table Mapping
        public DbSet<Product> Products { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Address> Addresses { get; set; }

        //For Authentication and Authorization
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRolesMapping> UserRolesMappings { get; set; }
       
    }
}