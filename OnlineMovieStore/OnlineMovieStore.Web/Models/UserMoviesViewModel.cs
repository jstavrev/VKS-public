using OnlineMovieStore.Models.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineMovieStore.Web.Models
{
    public class UserMoviesViewModel
    {
        public IEnumerable<MoviesViewModel> content;

        public UserMoviesViewModel(IEnumerable<Movie> content)
        {
            this.content = content.Select(m => new MoviesViewModel(m));
        }
    }
}