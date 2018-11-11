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
    public class ManageActorsController : Controller
    {
        private const int pageSize = 10;
        private IActorsService actorsService;

        public ManageActorsController(IActorsService actors)
        {
            this.actorsService = actors;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Actors(ActorsViewModel model)
        {
            if (model.SearchText == null)
            {
                model.TotalPages = (int)Math.Ceiling(this.actorsService.Total() / (double)pageSize);
                model.Actors = this.actorsService.GetActorsPerPage(model.Page, pageSize);
            }
            else
            {
                model.TotalPages = (int)Math.Ceiling(this.actorsService.TotalContainingText(model.SearchText) / (double)pageSize);
                model.Actors = this.actorsService.ListByContainingText(model.SearchText, model.Page, pageSize);
            }

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddActor()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddActor(AddActorViewModel vM)
        {
            if (!this.ModelState.IsValid)
            {
                return View();
            }

                var actors = this.actorsService.GetAll().ToList();

            foreach (var actor in actors)
            {
                if (actor.FirstName == vM.FirstName && actor.LastName == vM.LastName)
                {
                    ViewData["ActorExists"] = "An actor with this name already exists!";
                    return View(vM);
                }
            }

            this.actorsService.AddActor(vM.FirstName, vM.LastName, vM.Age);

            return RedirectToAction("Actors", "ManageActors");
        }
    }
}