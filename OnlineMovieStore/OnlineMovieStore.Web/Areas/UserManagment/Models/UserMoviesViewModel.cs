using OnlineMovieStore.Models.Models;
using OnlineMovieStore.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineMovieStore.Web.Areas.UserManagment.Models
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