using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineMovieStore.Services.Services.Contracts;
using OnlineMovieStore.Web.Areas.Administration.Models;
using OnlineMovieStore.Web.Data;
using System;

namespace OnlineMovieStore.Web.Areas.Admin.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private int pageSize = 10;
        private IOrdersService ordersService;

        public AdminController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index(AdminIndexViewModel model)
        {
            if (model.SearchText == null)
            {
                model.TotalPages = (int)Math.Ceiling(this.ordersService.TotalOrders() / (double)pageSize);
                model.Orders = this.ordersService.ListByPage(model.Page, pageSize);
            }
            else
            {
                model.TotalPages = (int)Math.Ceiling(this.ordersService.TotalContainingText(model.SearchText) / (double)pageSize);
                model.Orders = this.ordersService.ListOrdersContainingText(model.SearchText, model.Page, pageSize);
            }

            return View(model);
        }
    }
}