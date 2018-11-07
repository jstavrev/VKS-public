using OnlineMovieStore.Models.Models;
using System.Collections.Generic;

namespace OnlineMovieStore.Services.Contracts
{
    public interface IUsersService
    {
        IEnumerable<Movie> Orders(string id);
    }
}