using OnlineMovieStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMovieStore.Web.Models
{
    public class AllMoviesViewModel
    {
        public AllMoviesViewModel()
        {

        }
        public AllMoviesViewModel(IEnumerable<Movie> movies)
        {
            this.Movies = movies.Select(m => new MoviesViewModel(m));
        }

        public IEnumerable<MoviesViewModel> Movies { get; set; }
    }
}
