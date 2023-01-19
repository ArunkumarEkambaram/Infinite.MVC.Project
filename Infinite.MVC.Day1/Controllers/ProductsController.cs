using Infinite.MVC.Day1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Infinite.MVC.Day1.Controllers
{
    //[Authorize(Roles = "Admin, Employee")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context = null;

        public ProductsController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            //var products = new Product().GetProducts();
            var products = _context.Products.ToList();
            return View(products);
        }

        [Authorize(Roles = "Customer")]        
        public ActionResult Details(int id)
        {
            var product = _context.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                return View(product);
            }
            return HttpNotFound();
        }

        [Authorize(Roles = "Employee, Admin")]
        public ActionResult Create()
        {
            var categories = _context.Categories.ToList();
            ViewBag.Categories = categories;
            return View();
        }


        [Authorize(Roles = "Employee, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            var categories = _context.Categories.ToList();
            ViewBag.Categories = categories;
            return View();
        }


        [Authorize(Roles = "Employee, Admin")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                var categories = _context.Categories.ToList();
                ViewBag.Categories = categories;
                return View(product);
            }
            return HttpNotFound("Product Id doesn't exists");
        }


        [Authorize(Roles = "Employee, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (product != null)
            {
                var productInDb = _context.Products.Find(product.Id);
                if (productInDb != null)
                {
                    productInDb.Price = product.Price;
                    productInDb.Quantity = product.Quantity;
                    //_context.Products.AddOrUpdate(productInDb);
                    _context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            var categories = _context.Categories.ToList();
            ViewBag.Categories = categories;
            return View(product);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var productInDb = _context.Products.FirstOrDefault(p => p.Id == id);
            if (productInDb != null)
            {
                _context.Products.Remove(productInDb);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}