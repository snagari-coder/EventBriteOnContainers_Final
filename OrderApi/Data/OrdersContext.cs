using Microsoft.EntityFrameworkCore;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Data
{
    public class OrdersContext: DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public OrdersContext(DbContextOptions options) : base(options)
        {

        }
    }
}
