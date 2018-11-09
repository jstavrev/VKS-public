using OnlineMovieStore.Models.Models;

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

        public double DepositSum { get; set; }
    }
}