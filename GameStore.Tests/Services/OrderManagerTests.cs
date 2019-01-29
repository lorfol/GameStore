using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using GameStore.Domain.Core.DomainModels;
using GameStore.Domain.Interfaces;
using GameStore.Infrastructure.Business;
using GameStore.Services.Interfaces;
using Moq;
using NUnit.Framework;

namespace GameStore.Tests.Services
{
    [TestFixture()]
    public class OrderManagerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;

        [SetUp]
        public void TestSetup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Test]
        public void CreateNew()
        {
            _unitOfWorkMock.Setup(x => x.Orders.Create(It.IsAny<Order>()));

            var testEntity = GetTestEntity();
            var result = testEntity.CreateNewOrder(new Order());

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [Test]
        public void Edit()
        {
            _unitOfWorkMock.Setup(x => x.Orders.Update(It.IsAny<Order>()));

            var testEntity = GetTestEntity();
            var result = testEntity.EditOrder(new Order());

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [Test]
        public void Delete()
        {
            _unitOfWorkMock.Setup(x => x.Orders.Delete(It.IsAny<Order>()));

            var testEntity = GetTestEntity();
            var result = testEntity.DeleteOrder(new Order());

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [Test]
        public void DeleteById()
        {
            _unitOfWorkMock.Setup(x => x.Orders.DeleteById(It.IsAny<int>()));

            var testEntity = GetTestEntity();
            var result = testEntity.DeleteOrderById("aaa");

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [Test]
        public void GetAllOrdersByCustomerId()
        {
            _unitOfWorkMock.Setup(x => x.Orders.Find(It.IsAny<Expression<Func<Order, bool>>>())).Returns(new List<Order>());

            var testEntity = GetTestEntity();
            var result = testEntity.GetAllOrdersByCustomerId(1);

            Assert.IsInstanceOf(typeof(ICollection<Order>), result);
        }

        [Test]
        public void GetAll()
        {
            _unitOfWorkMock.Setup(x => x.Orders.GetAll()).Returns(new List<Order>());

            var testEntity = GetTestEntity();
            var result = testEntity.GetAllOrders();

            Assert.IsInstanceOf(typeof(IEnumerable<Order>), result);
        }

        [Test]
        public void Find()
        {
            _unitOfWorkMock.Setup(x => x.Orders.Find(It.IsAny<Expression<Func<Order, bool>>>())).Returns(new List<Order>());

            var testEntity = GetTestEntity();
            var result = testEntity.GetAllOrdersByCustomerId(1);

            Assert.IsInstanceOf(typeof(ICollection<Order>), result);
        }

        private IOrderManager GetTestEntity()
        {
            return new OrderManager(_unitOfWorkMock.Object);
        }
    }
}
