using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using System;
using System.Collections.Generic;
using System.Text;
using OnlineMovieStore.Web.Areas.Admin.Controllers;
using OnlineMovieStore.Web.Areas.Administration.Models;
using Microsoft.AspNetCore.Mvc;
using OnlineMovieStore.Services.Services.Contracts;
using OnlineMovieStore.Models.Models;

namespace OnlineMovieStore.Web.Tests.AdminControllerTests
{
    [TestClass]
    public class AdminController
    {

        [TestMethod]
        public void IndexAction_ReturnsViewResult()
        {
            var ordersService = this.SetupMockOrdersService();

            var viewModelMock = new Mock<AdminIndexViewModel>();
            var controller = new Areas.Admin.Controllers.AdminController(ordersService.Object);

            var result = controller.Index(viewModelMock.Object);

            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }

        [TestMethod]
        public void IndexAction_ReturnsCorrectViewModel()
        {
            var ordersService = this.SetupMockOrdersService();
            var viewModelMock = new Mock<AdminIndexViewModel>();
            var controller = new Areas.Admin.Controllers.AdminController(ordersService.Object);

            var result = controller.Index(viewModelMock.Object) as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(AdminIndexViewModel));

        }

        [TestMethod]
        public void IndexAction_CallCorrectServiceMethod()
        {
            var ordersService = this.SetupMockOrdersService();
            var viewModelMock = new Mock<AdminIndexViewModel>();
            var controller = new Areas.Admin.Controllers.AdminController(ordersService.Object);
            
            var result = controller.Index(viewModelMock.Object) as ViewResult;

            ordersService.Verify(o => o.TotalContainingText(It.IsAny<string>()), Times.Once);
            ordersService.Verify(o => o.ListOrdersContainingText(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }
                  
        private Mock<IOrdersService> SetupMockOrdersService()
        {
            var ordersServiceMock = new Mock<IOrdersService>();
            ordersServiceMock
                .Setup(o => o.ListAllOrders())
                .Returns(new List<Order>());

            ordersServiceMock
                .Setup(o => o.ListOrdersContainingText(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<Order>());

            ordersServiceMock
                .Setup(o => o.ListByPage(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<Order>());

            ordersServiceMock
                .Setup(o => o.TotalOrders())
                .Returns(It.IsAny<int>());

            ordersServiceMock
                .Setup(o => o.TotalContainingText(It.IsAny<string>()))
                .Returns(It.IsAny<int>());

            return ordersServiceMock;
        }
    }
}
