﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineMovieStore.Models.Models;
using OnlineMovieStore.Services.Contracts;
using OnlineMovieStore.Services.Services.Contracts;
using OnlineMovieStore.Web.Areas.Administration.Models;
using OnlineMovieStore.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineMovieStore.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Admin")]
    public class ManageMoviesController : Controller
    {
        private const int pageSize = 10;
        private IActorsService actorsService;
        private IGenresService genreService;
        private IMoviesService movieService;

        public ManageMoviesController(IActorsService actorsService, IMoviesService movie, IGenresService genreService)
        {
            this.actorsService = actorsService;
            this.genreService = genreService;
            this.movieService = movie;
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        public IActionResult AddMovie()
        {
            List<Actor> actors = this.actorsService.GetAll().ToList();
            List<Genre> genres = this.genreService.GetAll().ToList();

            AddMovieViewModel vM = new AddMovieViewModel(actors, genres);

            return View(vM);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddMovie(AddMovieViewModel vm)
        {
            if (!this.ModelState.IsValid)
            {
                List<Actor> actors = this.actorsService.GetAll().ToList();
                List<Genre> genre = this.genreService.GetAll().ToList();

                AddMovieViewModel model = new AddMovieViewModel(actors, genre);

                return View(model);
            }

            var movies = this.movieService.GetAll();

            foreach (var movie in movies)
            {
                if (movie.Title.Equals(vm.Title, StringComparison.InvariantCultureIgnoreCase))
                {
                    ViewData["MovieExists"] = "A movie with this title already exists!";
                    return View(vm);
                }
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

        public IActionResult Delete(int id)
        {

            this.movieService.DeleteMovie(id);
            return RedirectToAction("Movies", "ManageMovies");
        }
    }
}