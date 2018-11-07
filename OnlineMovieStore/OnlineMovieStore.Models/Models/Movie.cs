using OnlineMovieStore.Models.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineMovieStore.Models.Models
{
    public class Movie : Entity
    {

        [Required]
        public string Image { get; set; }

        [Required]
        [MaxLength(40)]
        public string Title { get; set; }

        [Required]
        [Range(1800,9999)]
        public short Year { get; set; }

        [Required]
        [Range(1, 200)]
        public double Price { get; set; }

        public int ActorId { get; set; }

        public Actor Actor { get; set; }

        public ICollection<Order> Orders { get; set; }

        public ICollection<WatchedMovies> Movies { get; set; }

        public ICollection<GenreMovie> Genres { get; set; }
    }
}
