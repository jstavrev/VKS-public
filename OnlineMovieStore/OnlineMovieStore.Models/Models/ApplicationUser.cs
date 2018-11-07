using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace OnlineMovieStore.Models.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Order> Orders { get; set; }

        public ICollection<WatchedMovies> WatchedMovies { get; set; }
    }
}