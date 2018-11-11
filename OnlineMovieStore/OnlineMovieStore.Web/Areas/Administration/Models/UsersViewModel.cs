using OnlineMovieStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMovieStore.Web.Areas.Administration.Models
{
    public class UsersViewModel
    {
        public UsersViewModel()
        {

        }

        public IEnumerable<ApplicationUser> Users { get; set; }

        public int TotalPages { get; set; }

        public int Page { get; set; } = 1;

        public int PreviousPage => this.Page ==
            1 ? 1 : this.Page - 1;

        public int NextPage => this.Page ==
            this.TotalPages ? this.TotalPages : this.Page + 1;

        public string SearchText { get; set; } = string.Empty;
    }
}
