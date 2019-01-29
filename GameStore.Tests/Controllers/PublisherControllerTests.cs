using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using GameStore.Domain.Core.DomainModels;
using GameStore.Services.Interfaces;
using GameStore.Web.Controllers;
using GameStore.Web.ViewModels;
using Moq;
using NUnit.Framework;

namespace GameStore.Tests.Controllers
{
    [TestFixture]
    public class PublisherControllerTests
    {
        private Mock<IPublisherManager> _managerMock;

        [SetUp]
        public void Setup()
        {
            _managerMock = new Mock<IPublisherManager>();
        }

        [Test]
        public void ShouldReturnCorrectView()
        {
            var service = new PublisherController(_managerMock.Object);

            _managerMock.Setup(x => x.Find(It.IsAny<Expression<Func<Publisher, bool>>>()))
                .Returns(new List<Publisher>{new Publisher()});

            var result = service.PublisherDetails("name") as ViewResult;

            Assert.AreEqual("PublisherDetails", result.ViewName);
        }

        [Test]
        public void CreateMethodShouldReturnActionResult()
        {
            var service = new PublisherController(_managerMock.Object);

            var res = service.CreatePublisher() as ViewResult;
            
            Assert.AreEqual("CreatePublisherForm", res.ViewName);
        }

        [Test]
        public void CreatePostMethodShouldReturnActionResult()
        {
            var service = new PublisherController(_managerMock.Object);

            var res = service.CreatePublisher(new CreatePublisherViewModel { CompanyName = "ssss", Description = "ssssss", HomePage = "www.asd.com" }) as RedirectToRouteResult;
            
            Assert.AreEqual(true, res.RouteValues.ContainsValue("GetAllGames"));
        }
    }
}