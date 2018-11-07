using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineMovieStore.Services.Services.Contracts;
using OnlineMovieStore.Web.Areas.Administration.Models;

namespace OnlineMovieStore.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class ManageGenresController : Controller
    {
        private IGenresService genreService;

        public ManageGenresController(IGenresService genre)
        {
            this.genreService = genre;
        }

        public IActionResult Genres()
        {
            return View();
        }

        public IActionResult AddGenre()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddGenre(AddGenreViewModel vM)
        {
            this.genreService.AddGenre(vM.Name);

            return RedirectToAction("Genres", "ManageGenres");
        }
    }
}