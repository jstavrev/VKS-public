using Microsoft.AspNetCore.Mvc;
using OnlineMovieStore.Models.Models;
using OnlineMovieStore.Services.Contracts;
using OnlineMovieStore.Web.Areas.Administration.Models;
using OnlineMovieStore.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineMovieStore.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class ManageMoviesController : Controller
    {
        private const int pageSize = 20;
        private ApplicationDbContext context;
        private IMoviesService movieService;

        public ManageMoviesController(ApplicationDbContext ctxt, IMoviesService movie)
        {
            this.context = ctxt;
            this.movieService = movie;
        }

        public IActionResult Movies(MoviesIndexViewModel model)
        {
            if (model.SearchText == null)
            {
                model.TotalPages = (int)Math.Ceiling(this.movieService.Total() / (double)pageSize);
                model.Movies = this.movieService.ListAllMovies(model.Page, pageSize);
            }
            else
            {
                model.TotalPages = (int)Math.Ceiling(this.movieService.TotalContainingText(model.SearchText) / (double)pageSize);
                model.Movies = this.movieService.ListByContainingText(model.SearchText, model.Page, pageSize);
            }

            return View(model);
        }

        public IActionResult AddMovie()
        {
            List<Actor> actors = this.context.Actors.ToList();
            List<Genre> genres = this.context.Genres.ToList();

            AddMovieViewModel vM = new AddMovieViewModel(actors, genres);

            return View(vM);
        }

        [HttpPost]
        public IActionResult AddMovie(AddMovieViewModel vm)
        {
            if (!this.ModelState.IsValid)
            {
                List<Actor> actors = this.context.Actors.ToList();
                List<Genre> genre = this.context.Genres.ToList();

                AddMovieViewModel model = new AddMovieViewModel(actors, genre);

                return View(model);
            }

            List<Genre> genres = new List<Genre>();

            foreach (var g in vm.Genres)
            {
                if (g.IsChecked == true)
                {
                    genres.Add(new Genre { Id = g.Id, Name = g.Name });
                }
            }

            this.movieService.AddMovie(vm.ImageURL, vm.Title, vm.Year, genres, int.Parse(vm.ActorId), vm.Price, vm.Description, vm.TrailerURL);

            return RedirectToAction("Movies", "ManageMovies");
        }
    }
}