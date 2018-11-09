using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineMovieStore.Models.Models;
using OnlineMovieStore.Services.Contracts;
using OnlineMovieStore.Web.Models;

namespace OnlineMovieStore.Web.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService moviesService;

        public MoviesController(IMoviesService moviesService)
        {
            this.moviesService = moviesService ?? throw new ArgumentNullException(nameof(moviesService));
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

        public IActionResult Details(string title)
        {
           // ModelState.Clear();

            var movie = this.moviesService.ListMoviesByTitle(title);

            return View(new MoviesViewModel(movie));
        }
    }
}