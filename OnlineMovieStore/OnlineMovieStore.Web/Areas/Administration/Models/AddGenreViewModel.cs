using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMovieStore.Web.Areas.Administration.Models
{
    public class AddGenreViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
