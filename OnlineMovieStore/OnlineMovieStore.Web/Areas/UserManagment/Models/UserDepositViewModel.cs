using OnlineMovieStore.Models.Models;
using System.ComponentModel.DataAnnotations;

namespace OnlineMovieStore.Web.Areas.UserManagment.Models
{
    public class UserDepositViewModel
    {
        public UserDepositViewModel()
        {

        }

        public UserDepositViewModel(ApplicationUser user)
        {
            this.Balance = user.Balance;
            this.UserName = user.UserName;
        }

        public double Balance { get; set; }
        public string UserName { get; set; }

        public string SuccesfullDeposit { get; set; }

        [Required(ErrorMessage = "A deposit sum is required.")]
        [Range(1, 100000, ErrorMessage = "Enter a sum between 1 and 100000")]
        public double DepositSum { get; set; }
    }
}