using System.Net;
using GameStore.Domain.Core.DomainModels;
using GameStore.Domain.Interfaces;
using GameStore.Infrastructure.Business;
using GameStore.Services.Interfaces;
using Moq;
using NUnit.Framework;

namespace GameStore.Tests.Services
{
    [TestFixture]
    public class OrderDetailsManagerTests
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
            _unitOfWorkMock.Setup(x => x.OrderDetails.Create(It.IsAny<OrderDetails>()));

            var testEntity = GetTestEntity();
            var result = testEntity.CreateNewOrderDetails(new OrderDetails());

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [Test]
        public void Edit()
        {
            _unitOfWorkMock.Setup(x => x.OrderDetails.Update(It.IsAny<OrderDetails>()));

            var testEntity = GetTestEntity();
            var result = testEntity.EditOrderDetails(new OrderDetails());

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [Test]
        public void Delete()
        {
            _unitOfWorkMock.Setup(x => x.OrderDetails.Delete(It.IsAny<OrderDetails>()));

            var testEntity = GetTestEntity();
            var result = testEntity.DeleteOrderDetails(new OrderDetails());

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [Test]
        public void DeleteById()
        {
            _unitOfWorkMock.Setup(x => x.OrderDetails.DeleteById(It.IsAny<int>()));

            var testEntity = GetTestEntity();
            var result = testEntity.DeleteOrderDetailsById(1);

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        private IOrderDetailsManager GetTestEntity()
        {
            return new OrderDetailsManager(_unitOfWorkMock.Object);
        }
    }
}