using Microsoft.AspNetCore.Mvc;
using OnlineMovieStore.Web.Data;

namespace OnlineMovieStore.Web.Areas.Admin.Controllers
{
    [Area("Administration")]
    public class AdminController : Controller
    {
        private ApplicationDbContext context;

        public AdminController(ApplicationDbContext ctxt)
        {
            this.context = ctxt;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}