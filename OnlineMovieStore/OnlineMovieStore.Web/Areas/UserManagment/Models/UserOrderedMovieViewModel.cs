using OnlineMovieStore.Models.Models;

namespace OnlineMovieStore.Web.Areas.UserManagment.Models
{
    public class UserOrderedMovieViewModel
    {
        public UserOrderedMovieViewModel(Movie movie)
        {
            this.Name = movie.Title;
            this.Price = movie.Price;
        }

        public string Name { get; set; }

        public double Price { get; set; }
    }
}