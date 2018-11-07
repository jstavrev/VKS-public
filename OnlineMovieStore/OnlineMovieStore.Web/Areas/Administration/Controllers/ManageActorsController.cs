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
    public class ManageActorsController : Controller
    {
        private IActorsService actorsService;

        public ManageActorsController(IActorsService actors)
        {
            this.actorsService = actors;
        }

        public IActionResult Actors()
        {
            return View();
        }

        public IActionResult AddActor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddActor(AddActorViewModel vM)
        {
            this.actorsService.AddActor(vM.FirstName, vM.LastName, vM.Age);

            return RedirectToAction("Actors", "ManageActors");
        }
    }
}