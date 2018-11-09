using OnlineMovieStore.Models.Models;
using System.Collections.Generic;

namespace OnlineMovieStore.Web.Areas.UserManagment.Models
{
    public class UserViewModel
    {
        public UserViewModel(ApplicationUser user)
        {
            this.Username = user.UserName;
            this.Balance = user.Balance;
            this.Orders = user.Orders;
            this.Email = user.Email;
            this.IsEmailConfirmed = user.EmailConfirmed;
            this.PhoneNumber = user.PhoneNumber;
        }

        public string Username { get; set; }

        public double Balance { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public IEnumerable<Order> Orders { get; set; }
    }
}