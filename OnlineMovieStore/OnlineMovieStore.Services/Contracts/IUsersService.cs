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

        ApplicationUser UpdateAccountDetails(string UserName, string Email, string PhoneNumber, string userId);

        IEnumerable<ApplicationUser> GetAllUsers(int page = 1, int pageSize = 10);

        int Total();

        int TotalContainingText(string searchText);

        IEnumerable<ApplicationUser> UsersContainingText(string searchText, int page = 1, int pageSize = 10);
    }
}