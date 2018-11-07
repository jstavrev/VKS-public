using System.ComponentModel.DataAnnotations;

namespace OnlineMovieStore.Models.Models
{
    public class GenreMovie
    {
        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        public int GenreId { get; set; }

        public Genre Genre { get; set; }
    }
}
