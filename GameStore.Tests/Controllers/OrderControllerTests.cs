using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using GameStore.Domain.Core.DomainModels;
using GameStore.Services.Interfaces;
using GameStore.Web.Controllers;
using GameStore.Web.ViewModels;
using Moq;
using MvcContrib.TestHelper;
using NUnit.Framework;

namespace GameStore.Tests.Controllers
{
    [TestFixture]
    public class OrderControllerTests
    {
        private Mock<IOrderManager> _orderMock;
        private Mock<IOrderDetailsManager> _orderdetailsMock;
        private Mock<IGameManager> _gameMock;

        [SetUp]
        public void Setup()
        {
            _orderMock = new Mock<IOrderManager>();
            _orderdetailsMock = new Mock<IOrderDetailsManager>();
            _gameMock = new Mock<IGameManager>();
        }

        private OrderController GetTestedEntity()
        {
            return new OrderController(_orderMock.Object, _orderdetailsMock.Object, _gameMock.Object);
        }

        [Test]
        public void ConfirmOrder_ReturnActionResult()
        {
            var controller = GetTestedEntity();

            TestControllerBuilder builder = new TestControllerBuilder();

            builder.InitializeController(controller);
            builder.Session["Basket"] = new List<string>() { "asd", "asd", "key" };
            var inCart = (List<string>)builder.Session["Basket"];
            builder.Session["Count"] = inCart.Count;

            var res = controller.ConfirmOrder(
                new List<BasketItemViewModel>()
                {
                    new BasketItemViewModel() {Key = "asd"},
                    new BasketItemViewModel() {Key = "asd"},
                    new BasketItemViewModel() {Key = "key"}
                }) 
                as RedirectToRouteResult;

            inCart = (List<string>)builder.Session["Basket"];

            Assert.AreEqual(true, res.RouteValues.ContainsValue("OrderDetails"));
            Assert.AreEqual(0, inCart.Count);
        }

        [Test]
        public void OrderDetails_ReturnRedirectToActionResult()
        {
            var controller = GetTestedEntity();



            var res = controller.OrderDetails(new Order()) as ViewResult;

            Assert.AreEqual("OrderDetails", res.ViewName);
        }
    }
}
