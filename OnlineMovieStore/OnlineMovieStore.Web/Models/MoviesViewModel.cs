using OnlineMovieStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMovieStore.Web.Models
{
    public class MoviesViewModel
    {
        public MoviesViewModel()
        {

        }
        public MoviesViewModel(Movie movie)
        {
            this.Title = movie.Title;
            this.Year = movie.Year;
            this.Genre = new List<GenreMovie>(movie.Genres);
            this.ActorName = movie.Actor.FirstName + " " + movie.Actor.LastName;
            this.Price = movie.Price;
            this.Image = movie.Image;
            this.AddedOn = movie.CreatedOn;
            this.Description = movie.Description;
            this.TrailerUrl = movie.TrailerURL;
            this.IsDeleted = movie.IsDeleted;
        }

        public string Title { get; set; }
        public short Year { get; set; }
        public IEnumerable<GenreMovie> Genre { get; set; }
        public string ActorName { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public DateTime? AddedOn { get;set; }
        public string Description { get; set; }
        public string TrailerUrl { get; set; }
        public bool IsOwned { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
    }
}
