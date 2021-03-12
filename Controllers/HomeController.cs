using HomeAppliance.Data;
using HomeAppliance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAppliance.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var products = _dbContext.Set<Product>().ToList();
            var orders = _dbContext.Set<OrderItem>().ToList();
            var homeItem = new HomeItemViewModel { Products = products,OrderItems = orders };
            return View(homeItem);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToBusket(int Id) {
            var item = _dbContext.Set<Product>().FirstOrDefault(p => p.Id == Id);
            var order = new OrderItem { Name=item.Name,Price = item.Price };

            _dbContext.Set<OrderItem>().Add(order);
            _dbContext.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult ConfirmOrder()
        {
            _dbContext.Set<OrderItem>().RemoveRange(_dbContext.OrderItems);
            _dbContext.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult ClearOrder()
        {
            _dbContext.Set<OrderItem>().RemoveRange(_dbContext.OrderItems);
            _dbContext.SaveChanges();
            return RedirectToAction("index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
