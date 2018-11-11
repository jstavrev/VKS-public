using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineMovieStore.Services.Contracts;
using OnlineMovieStore.Web.Areas.Administration.Models;
using OnlineMovieStore.Web.Data;

namespace OnlineMovieStore.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Admin")]
    public class ManageUsersController : Controller
    {
        private const int pageSize = 10;
        private IUsersService usersService;

        public ManageUsersController(IUsersService users)
        {
            this.usersService = users;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Users(UsersViewModel model)
        {
            if (model.SearchText == null)
            {
                model.TotalPages = (int)Math.Ceiling(this.usersService.Total() / (double)pageSize);
                model.Users = this.usersService.GetAllUsers(model.Page, pageSize);
            }
            else
            {
                model.TotalPages = (int)Math.Ceiling(this.usersService.TotalContainingText(model.SearchText) / (double)pageSize);
                model.Users = this.usersService.UsersContainingText(model.SearchText, model.Page, pageSize);
            }

            return View(model);
        }

    }
}