using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using OnlineMovieStore.Models.Models;
using OnlineMovieStore.Services.Contracts;
using OnlineMovieStore.Web.Models;

namespace OnlineMovieStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMoviesService moviesService;
        private readonly IMemoryCache cache;

        public HomeController(IMoviesService moviesService, IMemoryCache cache)
        {
            this.moviesService = moviesService ?? throw new ArgumentNullException(nameof(moviesService));
            this.cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public IActionResult Index()
        {
            var movies = this.GetMoviesCashed();

            AllMoviesViewModel model = new AllMoviesViewModel(movies);

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private IEnumerable<Movie> GetMoviesCashed()
        {
            return this.cache.GetOrCreate("HigestPriceMovies", entry =>
            {
                entry.AbsoluteExpiration = DateTime.UtcNow.AddHours(3);
                return this.moviesService.ListMoviesByHigherPrice(9);
            });
        }
    }
}
