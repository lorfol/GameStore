using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using GameStore.Domain.Core.DomainModels;
using GameStore.Infrastructure.Business;
using GameStore.Services.Interfaces;
using GameStore.Web.Controllers;
using Moq;
using MvcContrib.TestHelper;
using MvcContrib.TestHelper.Fakes;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace GameStore.Tests.Controllers
{
    [TestFixture]
    public class BasketControllerTests
    {
        private Mock<IGameManager> _gameManagerMock;
        private Mock<ControllerContext> _contextMock;

        [SetUp]
        public void Setup()
        {
            _gameManagerMock = new Mock<IGameManager>();
        }

        [Test]
        [TestCase(10)]
        public void GetBasket_ShouldReturnBasketView(short value)
        {
            _gameManagerMock.Setup(x => x.GetGameByKey(It.IsAny<string>())).Returns(new Game() { Key = "asd", UnitsInStock = value });

            var bc = new BasketController(_gameManagerMock.Object);

            TestControllerBuilder builder = new TestControllerBuilder();

            builder.InitializeController(bc);
            builder.Session["Basket"] = new List<string>() { "asd" };

            var result = bc.GetBasket() as ViewResult;

            Assert.AreEqual("Basket", result.ViewName);
        }

        [Test]
        [TestCase(10)]
        public void AddToBasket_ShouldReturnOk(short value)
        {
            _gameManagerMock.Setup(x => x.GetGameByKey(It.IsAny<string>())).Returns(new Game() { Key = "asd", UnitsInStock = value });
            
            var bc = new BasketController(_gameManagerMock.Object);

            TestControllerBuilder builder = new TestControllerBuilder();

            builder.InitializeController(bc);
            builder.Session["Basket"] = new List<string>() { "asd", "str", "key" };

            var result = bc.AddToBasket("asd") as HttpStatusCodeResult;

            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        [TestCase(0)]
        public void AddToBasket_ShouldReturnNotFound(short value)
        {
            _gameManagerMock.Setup(x => x.GetGameByKey(It.IsAny<string>())).Returns(new Game() { Key = "asd", UnitsInStock = value });

            var bc = new BasketController(_gameManagerMock.Object);

            TestControllerBuilder builder = new TestControllerBuilder();

            builder.InitializeController(bc);
            builder.Session["Basket"] = new List<string>() { "asd", "str", "key" };

            var result = bc.AddToBasket("asd") as HttpStatusCodeResult;

            Assert.AreEqual(404, result.StatusCode);
        }

        [Test]
        public void RemoveFromBasket_ShouldRedirectToGetBasketAction()
        {
            var bc = new BasketController(_gameManagerMock.Object);

            TestControllerBuilder builder = new TestControllerBuilder();

            builder.InitializeController(bc);
            builder.Session["Basket"] = new List<string>() { "asd", "str", "key" };
            var inCart = (List<string>)builder.Session["Basket"];
            builder.Session["Count"] = inCart.Count;

            var result = bc.RemoveFromBasket("asd") as RedirectToRouteResult;

            var basket = (List<string>)builder.Session["Basket"];

            Assert.AreEqual(true, result.RouteValues.ContainsValue("GetBasket"));
            Assert.AreEqual(false, basket.Contains("asd"));
        }

        private BasketController GetTestEntity()
        {
            return new BasketController(_gameManagerMock.Object);
        }
    }
}
