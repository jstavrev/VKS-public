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
    }
}