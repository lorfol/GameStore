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
    [TestFixture]
    public class PublisherManagerTests
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
            _unitOfWorkMock.Setup(x => x.Publishers.Create(It.IsAny<Publisher>()));

            var testEntity = GetTestEntity();
            var result = testEntity.CreateNewPublisher(new Publisher());

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [Test]
        public void Edit()
        {
            _unitOfWorkMock.Setup(x => x.Publishers.Update(It.IsAny<Publisher>()));

            var testEntity = GetTestEntity();
            var result = testEntity.EditPublisher(1, new Publisher());

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [Test]
        public void Delete()
        {
            _unitOfWorkMock.Setup(x => x.Publishers.Delete(It.IsAny<Publisher>()));

            var testEntity = GetTestEntity();
            var result = testEntity.DeletePublisher(new Publisher());

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [Test]
        public void DeleteById()
        {
            _unitOfWorkMock.Setup(x => x.Publishers.DeleteById(It.IsAny<int>()));

            var testEntity = GetTestEntity();
            var result = testEntity.DeletePublisherById(1);

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [Test]
        public void GetAll()
        {
            _unitOfWorkMock.Setup(x => x.Publishers.GetAll()).Returns(new List<Publisher>());

            var testEntity = GetTestEntity();
            var result = testEntity.GetAllPublishers();

            Assert.IsInstanceOf(typeof(IEnumerable<Publisher>), result);
        }

        [Test]
        public void Find()
        {
            _unitOfWorkMock.Setup(x => x.Publishers.Find(It.IsAny<Expression<Func<Publisher, bool>>>())).Returns(new List<Publisher>());

            var testEntity = GetTestEntity();
            var result = testEntity.Find(x => x.IsDeleted == true);

            Assert.IsInstanceOf(typeof(IEnumerable<Publisher>), result);
        }

        private IPublisherManager GetTestEntity()
        {
            return new PublisherManager(_unitOfWorkMock.Object);
        }
    }
}
