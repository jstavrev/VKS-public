using OnlineMovieStore.Models.Models;
using System.Collections.Generic;

namespace OnlineMovieStore.Services.Contracts
{
    public interface IUsersService
    {
        IEnumerable<Movie> Orders(string id);

        ApplicationUser GetUser(string id);

        IEnumerable<Movie> OrdersDetails(string id);

        ApplicationUser AddToVallet(double amount, string userId);

        double GetBalance(string id);
    }
}