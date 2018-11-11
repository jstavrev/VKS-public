using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineMovieStore.Models.Models;
using OnlineMovieStore.Services.Contracts;
using OnlineMovieStore.Web.Models;

namespace OnlineMovieStore.Web.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService moviesService;

        private readonly UserManager<ApplicationUser> userManager;

        private readonly IUsersService usersService;

        public MoviesController(IMoviesService moviesService, UserManager<ApplicationUser> userManager, IUsersService usersService)
        {
            this.moviesService = moviesService ?? throw new ArgumentNullException(nameof(moviesService));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
        }

        public IActionResult Index(MovieSearchViewModel model)
        {

            if (model.SearchText == null)
            {
                model.Movies = this.moviesService.ListAllMovies(model.Page, 10);
              //  model.Movies = this.moviesService.ListMovies(model.Page - 1, null);
                model.TotalPages = (int)Math.Ceiling(this.moviesService.Total() / (double)10);
            }
            else
            {
                model.Movies = this.moviesService.ListByContainingText(model.SearchText,model.Page, 10);
                // model.Movies = this.moviesService.ListMovies(model.Page - 1, model.SearchText);
                model.TotalPages = (int)Math.Ceiling(this.moviesService.TotalContainingText(model.SearchText) / (double)10);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Details(string title)
        {
            string userId = this.userManager.GetUserId(User);
            var userOrderedMovies = this.usersService.Orders(userId);

            var movie = this.moviesService.ListMoviesByTitle(title);

            var model = new MoviesViewModel(movie);

            foreach (var m in userOrderedMovies)
            {
                if(m.Title == title)
                {
                    model.IsOwned = true;
                }
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Details(MoviesViewModel model)
        {
            string userId = this.userManager.GetUserId(User);
            this.moviesService.BuyMovie(model.Title, userId);

            return RedirectToAction("Details", "Movies", new { title = $"{model.Title}" });
        }
    }
}