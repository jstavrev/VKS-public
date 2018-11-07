using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineMovieStore.Web.Data;

namespace OnlineMovieStore.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class ManageUsersController : Controller
    {
        private ApplicationDbContext context;

        public ManageUsersController(ApplicationDbContext ctxt)
        {
            this.context = ctxt;
        }

        public IActionResult Users()
        {
            return View();
        }
    }
}