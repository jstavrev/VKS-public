using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMovieStore.Web.Areas.Administration.Models
{
    public class GenreCheckBox
    {
        public int Id { get; set; }

        public bool IsChecked { get; set; }

        public string Name { get; set; }
    }
}
