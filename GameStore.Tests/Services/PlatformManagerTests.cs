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
    public class PlatformManagerTests
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
            _unitOfWorkMock.Setup(x => x.Platforms.Create(It.IsAny<Platform>()));

            var testEntity = GetTestEntity();
            var result = testEntity.CreateNewPlatform(new Platform());

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [Test]
        public void Edit()
        {
            _unitOfWorkMock.Setup(x => x.Platforms.Update(It.IsAny<Platform>()));

            var testEntity = GetTestEntity();
            var result = testEntity.EditPlatform(1, new Platform());

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [Test]
        public void Delete()
        {
            _unitOfWorkMock.Setup(x => x.Platforms.Delete(It.IsAny<Platform>()));

            var testEntity = GetTestEntity();
            var result = testEntity.DeletePlatform(new Platform());

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [Test]
        public void DeleteById()
        {
            _unitOfWorkMock.Setup(x => x.Platforms.DeleteById(It.IsAny<int>()));

            var testEntity = GetTestEntity();
            var result = testEntity.DeletePlatformById(1);

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [Test]
        public void GetAll()
        {
            _unitOfWorkMock.Setup(x => x.Platforms.GetAll()).Returns(new List<Platform>());

            var testEntity = GetTestEntity();
            var result = testEntity.GetAllPlatforms();

            Assert.IsInstanceOf(typeof(IEnumerable<Platform>), result);
        }

        [Test]
        public void Find()
        {
            _unitOfWorkMock.Setup(x => x.Platforms.Find(It.IsAny<Expression<Func<Platform, bool>>>())).Returns(new List<Platform>());

            var testEntity = GetTestEntity();
            var result = testEntity.Find(x => x.IsDeleted == true);

            Assert.IsInstanceOf(typeof(IEnumerable<Platform>), result);
        }

        private IPlatformManager GetTestEntity()
        {
            return new PlatformManager(_unitOfWorkMock.Object);
        }
    }
}
