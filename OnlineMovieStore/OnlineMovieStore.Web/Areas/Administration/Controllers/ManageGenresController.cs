using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineMovieStore.Services.Services.Contracts;
using OnlineMovieStore.Web.Areas.Administration.Models;

namespace OnlineMovieStore.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Admin")]
    public class ManageGenresController : Controller
    {
        private const int pageSize = 10;
        private IGenresService genreService;

        public ManageGenresController(IGenresService genre)
        {
            this.genreService = genre;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Genres(GenresViewModel model)
        {
            if (model.SearchText == null)
            {
                model.TotalPages = (int)Math.Ceiling(this.genreService.Total() / (double)pageSize);
                model.Genres = this.genreService.GetGenresPerPage(model.Page, pageSize);
            }
            else
            {
                model.TotalPages = (int)Math.Ceiling(this.genreService.TotalContainingText(model.SearchText) / (double)pageSize);
                model.Genres = this.genreService.ListByContainingText(model.SearchText, model.Page, pageSize);
            }

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddGenre()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddGenre(AddGenreViewModel vM)
        {
            var genres = this.genreService.GetAll();

            foreach (var g in genres)
            {
                if (g.Name.Equals(vM.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    ViewData["GenreExists"] = "A genre with this name already exists!";
                    return View(vM);
                }
            }

            if (this.ModelState.IsValid)
            {
                this.genreService.AddGenre(vM.Name);

                return RedirectToAction("Genres", "ManageGenres");

            }

            return View(vM);
        }
    }
}