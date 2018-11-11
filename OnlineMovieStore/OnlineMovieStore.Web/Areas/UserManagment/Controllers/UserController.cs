using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineMovieStore.Models.Models;
using OnlineMovieStore.Services.Contracts;
using OnlineMovieStore.Web.Areas.UserManagment.Models;

namespace OnlineMovieStore.Web.Areas.UserManagment.Controllers
{
    [Area("UserManagment")]
    [Route("User/[controller]")]
    public class UserController : Controller
    {
        private readonly IUsersService userService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public UserController(IUsersService userService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.userService = userService ?? throw new System.ArgumentNullException(nameof(userService));
            this.userManager = userManager ?? throw new System.ArgumentNullException(nameof(userManager));
            this.signInManager = signInManager ?? throw new System.ArgumentNullException(nameof(signInManager));
        }

        [Route("[action]")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("[action]/{id}")]
        public IActionResult MyMovies(string id)
        {
            var content = this.userService.Orders(id);
            if (content == null)
            {
                return NotFound($"Unable to load user with this id.");
            }

            var viewModel = new UserMoviesViewModel(content);

            return this.View(viewModel);
        }

        [Route("[action]")]
        public IActionResult OrdersDetails()
        {
            var userOrders = this.userService.OrdersDetails(this.userManager.GetUserId(User));
            if (userOrders == null)
            {
                return NotFound($"Unable to load user order details.");
            }

            var viewModel = new UserOrdersViewModel(userOrders);

            return View(viewModel);
        }

        //[Authorize]
        // [ValidateAntiForgeryToken]
        // [HttpPost]
        [Route("[action]/{id}")]
        [HttpGet]
        public IActionResult Deposit(string id)
        {
            var user = this.userService.GetUser(id);
            if (user == null)
            {
                return NotFound($"Unable to load user with this id.");
            }

            var userModel = new UserDepositViewModel(user);

            return View(userModel);
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult Deposit(UserDepositViewModel model)
        {
            if(!ModelState.IsValid)
            {
                string id = userManager.GetUserId(User);
                var u = this.userService.GetUser(id);
                model.Balance = u.Balance;

                return View(model);
            }

            var user = this.userService.AddToVallet(model.DepositSum, this.userManager.GetUserId(User));
            if (user == null)
            {
                return NotFound($"Unable to load user details.");
            }
            var userModel = new UserDepositViewModel(user);

            return View(userModel);
        }

        [Route("[action]")]

        [HttpGet]
        public IActionResult AccountSettings()
        {
            var user = this.userService.GetUser(this.userManager.GetUserId(User));
            if (user == null)
            {
                return NotFound($"Unable to load user with this id.");
            }

            var userModel = new UserViewModel(user);

            return View(userModel);
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult AccountSettings(UserViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                //Update user settings
                var user = this.userService.UpdateAccountDetails(model.Username, model.Email, model.PhoneNumber, this.userManager.GetUserId(User));
                if (user == null)
                {
                    return NotFound($"Unable to load user account settings.");
                }

                //Pass model
                var userModel = new UserViewModel(user);

                this.signInManager.SignOutAsync();

                return RedirectToAction("Logout", "Account", new { area = "Identity" });
            }

            return this.View(model);
        }

        // [Authorize]

        [Route("[action]")]
        public IActionResult RequestMovie()
        {
            return View();
        }

        // [Authorize]

        [Route("[action]")]
        public IActionResult Help()
        {
            return View();
        }
    }
}