using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMovieStore.Models.Models
{
    public class WatchedMovies
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int MovieId { get; set; }

        public Movie Movie { get; set; }
    }
}
