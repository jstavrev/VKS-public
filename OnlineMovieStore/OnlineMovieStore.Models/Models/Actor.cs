using OnlineMovieStore.Models.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace OnlineMovieStore.Models.Models
{
    public class Actor : Entity
    {

        [Required]
        [MaxLength(25)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(25)]
        public string LastName { get; set; }

        [Required]
        [Range(6, 150)]
        public int Age { get; set; }

        public ICollection<Movie> Movie { get; set; }
    }
}
