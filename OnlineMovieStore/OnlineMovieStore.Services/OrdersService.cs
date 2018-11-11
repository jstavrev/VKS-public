using Microsoft.EntityFrameworkCore;
using OnlineMovieStore.Models.Models;
using OnlineMovieStore.Services.Services.Contracts;
using OnlineMovieStore.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineMovieStore.Services.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly ApplicationDbContext context;

        public OrdersService(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<Order> ListAllOrders()
        {
            return this.context.Orders
                .Include(m => m.Movie)
                .Include(u => u.User).ToList();
        }

        public List<Order> ListByPage(int page = 1, int pageSize = 10)
        {
            return this.context.Orders.Include(u => u.User).Include(m => m.Movie).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<Order> ListOrdersContainingText(string searchText, int page = 1, int pageSize = 10)
        {
            return this.context.Orders.Include(u => u.User).Include(m => m.Movie).Where(o => o.User.UserName.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public int TotalContainingText(string searchText)
        {
            return this.context.Orders.Include(u => u.User).Where(o => o.User.UserName.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)).Count();
        }

        public int TotalOrders()
        {
            return this.context.Orders.Count();
        }
    }
}