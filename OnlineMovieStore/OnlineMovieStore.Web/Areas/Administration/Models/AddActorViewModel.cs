using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMovieStore.Web.Areas.Administration.Models
{
    public class AddActorViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

    }
}
