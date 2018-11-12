using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineMovieStore.Models.Models;
using OnlineMovieStore.Services.Services.Contracts;
using OnlineMovieStore.Web.Areas.Administration.Controllers;
using OnlineMovieStore.Web.Areas.Administration.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace OnlineMovieStore.Web.Tests.ManageGenreControllerTests
{
    [TestClass]
    public class ManageGenreController_Should
    {
        [TestMethod]
        public void IndexAction_ReturnsViewResult()
        {
            var genresService = this.SetupMockGenresService();

            var viewModelMock = new Mock<GenresViewModel>();
            var controller = new ManageGenresController(genresService.Object);

            var result = controller.Genres(viewModelMock.Object);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void IndexAction_ReturnsCorrectViewModel()
        {
            var genresService = this.SetupMockGenresService();
            var viewModelMock = new Mock<GenresViewModel>();
            var controller = new ManageGenresController(genresService.Object);

            var result = controller.Genres(viewModelMock.Object) as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(GenresViewModel));
        }

        [TestMethod]
        public void IndexAction_CallCorrectServiceMethod()
        {
            var genresService = this.SetupMockGenresService();
            var viewModelMock = new Mock<GenresViewModel>();
            var controller = new ManageGenresController(genresService.Object);

            var result = controller.Genres(viewModelMock.Object) as ViewResult;

            genresService.Verify(g => g.TotalContainingText(It.IsAny<string>()), Times.Once);
            genresService.Verify(g => g.ListByContainingText(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void AddGenreAction_ReturnsRedirectResult()
        {
            var controller = this.SetupController();
            var viewModel = new AddGenreViewModel()
            {
                Name = "GenreName"
            };

            var result = controller.AddGenre(viewModel);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectResult = (RedirectToActionResult)result;
            Assert.AreEqual("Genres", redirectResult.ActionName);
            Assert.AreEqual("ManageGenres", redirectResult.ControllerName);

            Assert.IsNull(redirectResult.RouteValues);
        }

        [TestMethod]
        public void AddGenreAction_InvalidModelState_RedisplaysView()
        {
            var controller = this.SetupController();
            controller.ModelState.AddModelError("error", "error");
            var viewModel = new AddGenreViewModel();

            var result = controller.AddGenre(viewModel);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.Model, typeof(AddGenreViewModel));
            Assert.IsNull(viewResult.ViewName);
        }

        private ManageGenresController SetupController()
        {
            var genresService = new Mock<IGenresService>();

            var controller = new ManageGenresController(genresService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = new ClaimsPrincipal()
                    }
                },
                TempData = new Mock<ITempDataDictionary>().Object
            };

            return controller;
        }

        private Mock<IGenresService> SetupMockGenresService()
        {
            var genresServiceMock = new Mock<IGenresService>();

            genresServiceMock
                .Setup(o => o.ListByContainingText(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<Genre>());

            genresServiceMock
                .Setup(o => o.GetGenresPerPage(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<Genre>());

            genresServiceMock
                .Setup(g => g.Total())
                .Returns(It.IsAny<int>());

            genresServiceMock
                .Setup(g => g.TotalContainingText(It.IsAny<string>()))
                .Returns(It.IsAny<int>());

            return genresServiceMock;
        }
    }
}
