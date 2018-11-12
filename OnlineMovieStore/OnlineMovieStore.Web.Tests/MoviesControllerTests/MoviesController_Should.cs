using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineMovieStore.Models.Models;
using OnlineMovieStore.Services.Contracts;
using OnlineMovieStore.Web.Controllers;
using OnlineMovieStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace OnlineMovieStore.Web.Tests.MoviesControllerTests
{
    [TestClass]
    public class MoviesController_Should
    {
        [TestMethod]
        public void IndexAction_ReturnsViewResult()
        {
            var moviesService = this.SetupMockMoviesService();
            var usersService = this.SetupMockUsersService();
            var mockUserManager = this.SetupMockUserManager();
            var viewModelMock = new Mock<MovieSearchViewModel>();
            var controller = new MoviesController(moviesService.Object, mockUserManager.Object, usersService.Object);
            
            var result = controller.Index(viewModelMock.Object);

            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }

        [TestMethod]
        public void IndexAction_ReturnsCorrectViewModel()
        {
            var moviesService = this.SetupMockMoviesService();
            var usersService = this.SetupMockUsersService();
            var mockUserManager = this.SetupMockUserManager();
            var viewModelMock = new Mock<MovieSearchViewModel>();
            var controller = new MoviesController(moviesService.Object, mockUserManager.Object, usersService.Object);

            var result = controller.Index(viewModelMock.Object) as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(MovieSearchViewModel));

        }

        [TestMethod]
        public void IndexAction_CallCorrectServiceMethod()
        {
            var moviesService = this.SetupMockMoviesService();
            var usersService = this.SetupMockUsersService();
            var mockUserManager = this.SetupMockUserManager();
            var viewModelMock = new Mock<MovieSearchViewModel>();
            var controller = new MoviesController(moviesService.Object, mockUserManager.Object, usersService.Object);

            var result = controller.Index(viewModelMock.Object) as ViewResult;

            moviesService.Verify(m => m.ListByContainingText(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()),Times.Once);
            moviesService.Verify(m => m.TotalContainingText(It.IsAny<string>()), Times.Once);

        }

        [TestMethod]
        public void DetailsAction_ReturnsRedirectResult()
        {
            var controller = this.SetupController();
            var viewModel = new MoviesViewModel() { Title = "Title", ActorName = "A.FirstName A.LastName",
                Year = 2018, Price = 20, Description="Description", Genre = new List<GenreMovie>(), IsOwned=true,
                Image = "ImageURL", TrailerUrl = "TrailerURL", AddedOn= DateTime.Now};

            var result = controller.Details(viewModel);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectResult = (RedirectToActionResult)result;
            Assert.AreEqual("Details", redirectResult.ActionName);
            Assert.AreEqual("Movies", redirectResult.ControllerName);
            var redResult = redirectResult.RouteValues.Values.ToList();
            Assert.AreEqual("Title", redResult[0]);
        }

        [TestMethod]
        public void DetailsAction_InvalidModelState_RedisplaysView()
        {
            var controller = this.SetupController();
            controller.ModelState.AddModelError("error", "error");
            var viewModel = new MoviesViewModel();

            var result = controller.Details(viewModel);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.Model, typeof(MoviesViewModel));
            Assert.IsNull(viewResult.ViewName); 
        }


        private Mock<IUsersService> SetupMockUsersService()
        {
            var usersServiceMock = new Mock<IUsersService>();


            return usersServiceMock;
        }

        private Mock<IMoviesService> SetupMockMoviesService()
        {
            var moviesServiceMock = new Mock<IMoviesService>();
            moviesServiceMock
                .Setup(ms => ms.ListAllMovies(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<Movie>());

            moviesServiceMock
                .Setup(ms => ms.ListByContainingText(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<Movie>());

            moviesServiceMock
                .Setup(ms => ms.Total())
                .Returns(It.IsAny<int>());

            moviesServiceMock
                .Setup(ms => ms.Total())
                .Returns(It.IsAny<int>());

            moviesServiceMock
                .Setup(ms => ms.TotalContainingText(It.IsAny<string>()))
                .Returns(It.IsAny<int>());

            return moviesServiceMock;
        }

        private Mock<UserManager<ApplicationUser>> SetupMockUserManager()
        {
           return new Mock<UserManager<ApplicationUser>>(
                    new Mock<IUserStore<ApplicationUser>>().Object,
                    new Mock<IOptions<IdentityOptions>>().Object,
                    new Mock<IPasswordHasher<ApplicationUser>>().Object,
                    new IUserValidator<ApplicationUser>[0],
                    new IPasswordValidator<ApplicationUser>[0],
                    new Mock<ILookupNormalizer>().Object,
                    new Mock<IdentityErrorDescriber>().Object,
                    new Mock<IServiceProvider>().Object,
                    new Mock<ILogger<UserManager<ApplicationUser>>>().Object);
        }

        private MoviesController SetupController()
        {
            var moviesService = new Mock<IMoviesService>();
            var usersService = new Mock<IUsersService>();
            var user = SetupMockUserManager();

            var controller = new MoviesController(moviesService.Object, user.Object, usersService.Object)
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

    }
}
