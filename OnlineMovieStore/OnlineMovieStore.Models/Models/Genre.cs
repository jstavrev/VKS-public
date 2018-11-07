using OnlineMovieStore.Models.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineMovieStore.Models.Models
{
    public class Genre : Entity
    {

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public ICollection<GenreMovie> Genres { get; set; }
    }
}