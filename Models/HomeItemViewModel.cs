using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAppliance.Models
{
    public class HomeItemViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }

        public double Price => OrderItems.Sum(p=>p.Price);
    }
}
