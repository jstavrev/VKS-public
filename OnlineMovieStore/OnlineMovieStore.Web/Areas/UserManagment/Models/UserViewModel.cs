using OnlineMovieStore.Models.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineMovieStore.Web.Areas.UserManagment.Models
{
    public class UserViewModel
    {
        public UserViewModel()
        {
        }

        public UserViewModel(ApplicationUser user)
        {
            this.Username = user.UserName;
            this.Balance = user.Balance;
            this.Orders = user.Orders;
            this.Email = user.Email;
            this.IsEmailConfirmed = user.EmailConfirmed;
            this.PhoneNumber = user.PhoneNumber;
        }

        [Required]
        [StringLength(20)]
        public string Username { get; set; }

        public double Balance { get; set; }

        [Required]
        [StringLength(20)]
        public string Email { get; set; }

        [StringLength(10)]
        public string PhoneNumber { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public IEnumerable<Order> Orders { get; set; }
    }
}