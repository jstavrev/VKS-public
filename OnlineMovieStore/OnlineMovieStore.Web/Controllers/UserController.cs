using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineMovieStore.Models.Models;
using OnlineMovieStore.Services.Contracts;
using OnlineMovieStore.Web.Models;

namespace OnlineMovieStore.Web.Controllers
{
    public class UserController : Controller
    {
        private IUsersService userService;
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(IUsersService userService, UserManager<ApplicationUser> userManager)
        {
            this.userService = userService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult MyMovies(string id)
        {
            var content = this.userService.Orders(id);
            var viewModel = new UserMoviesViewModel(content);

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult RequestMovie()
        {
            return View();
        }

        [Authorize]
        public IActionResult Help()
        {
            return View();
        }
    }
}