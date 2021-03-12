using HomeAppliance.Data;
using HomeAppliance.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAppliance.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _dbContext;


        public ProductController(ApplicationDbContext context)
        {
            _dbContext = context;    
        }

        public IActionResult Index()
        {
            var products = _dbContext.Set<Product>().OrderBy(p=>p.Name).ToList();
            return View(products);
        }

        public IActionResult Edit(int Id) {
            var product = _dbContext.Set<Product>().FirstOrDefault(p => p.Id == Id);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Product product) {
            _dbContext.Set<Product>().Update(product);
            _dbContext.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Delete(int id) {
            var item = _dbContext.Set<Product>().FirstOrDefault(p=>p.Id==id);
            _dbContext.Set<Product>().Remove(item);
            _dbContext.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            _dbContext.Set<Product>().Add(product);
            _dbContext.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
