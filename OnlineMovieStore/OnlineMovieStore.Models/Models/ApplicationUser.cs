using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineMovieStore.Models.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Order> Orders { get; set; }

        public ICollection<WatchedMovies> WatchedMovies { get; set; }

        [Range(0, 100000000)]
        [Required]
        public double Balance { get; set; }
    }
}