using Microsoft.EntityFrameworkCore;
using OnlineMovieStore.Models.Models;
using OnlineMovieStore.Services.Contracts;
using OnlineMovieStore.Web.Data;
using System.Collections.Generic;
using System.Linq;

namespace OnlineMovieStore.Services
{
    public class UserService : IUsersService
    {
        private readonly ApplicationDbContext context;

        public UserService(ApplicationDbContext context)
        {
            this.context = context;
        }

        //TODO: GET ORDERS OF THE CURRENT USER
        public IEnumerable<Movie> Orders(string id)
        {
            return this.context.Orders
               .Where(u => u.UserId == id)
               .Select(m => m.Movie)
               .Include(m => m.Genres)
                .ThenInclude(mg => mg.Genre)
               .Include(m => m.Actor)
               .ToList();
        }
    }
}