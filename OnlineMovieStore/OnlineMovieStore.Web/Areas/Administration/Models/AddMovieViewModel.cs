using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineMovieStore.Models.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OnlineMovieStore.Web.Areas.Administration.Models
{
    public class AddMovieViewModel
    {
        public IEnumerable<string> GenreId { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        public string ActorId { get; set; }

        [Required]
        [Range(0, 99.99)]
        public double Price { get; set; }

        [Required]
        [Range(1900,2018)]
        public short Year { get; set; }

        [Required]
        public string ImageURL { get; set; }

        [Required]
        public string TrailerURL { get; set; }

        [Required]
        [StringLength(350)]
        public string Description { get; set; }

        public GenreCheckBox[] Genres { get; set; }
        public IEnumerable<SelectListItem> Actors { get; set; }

        public AddMovieViewModel()
        {

        }

        public AddMovieViewModel(List<Actor> actors, List<Genre> genres)
        {
            this.Genres = genres.Select(g => new GenreCheckBox { Id = g.Id, Name = g.Name }).ToArray();
            this.Actors = actors.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = $"{a.FirstName} {a.LastName}" }).ToList();
        }
    }
}
