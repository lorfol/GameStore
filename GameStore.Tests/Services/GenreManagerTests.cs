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
    public class GenreManagerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;

        [SetUp]
        public void TestSetup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Test]
        public void CreateNewGenre()
        {
            _unitOfWorkMock.Setup(x => x.Genres.Create(It.IsAny<Genre>()));

            var testEntity = GetTestEntity();
            var result = testEntity.CreateNewGenre(new Genre());

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [Test]
        public void EditGenre()
        {
            _unitOfWorkMock.Setup(x => x.Genres.Update(It.IsAny<Genre>()));

            var testEntity = GetTestEntity();
            var result = testEntity.EditGenre(1, new Genre());

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [Test]
        public void DeleteGenre()
        {
            _unitOfWorkMock.Setup(x => x.Genres.Update(It.IsAny<Genre>()));

            var testEntity = GetTestEntity();
            var result = testEntity.DeleteGenre(new Genre());

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [Test]
        public void DeleteGenreById()
        {
            _unitOfWorkMock.Setup(x => x.Genres.DeleteById(It.IsAny<int>()));

            var testEntity = GetTestEntity();
            var result = testEntity.DeleteGenreById(1);

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [Test]
        public void GetAllGenres()
        {
            _unitOfWorkMock.Setup(x => x.Genres.GetAll()).Returns(new List<Genre>());

            var testEntity = GetTestEntity();
            var result = testEntity.GetAllGenres();

            Assert.IsInstanceOf(typeof(IEnumerable<Genre>), result);
        }

        [Test]
        public void Find()
        {
            _unitOfWorkMock.Setup(x => x.Genres.Find(It.IsAny<Expression<Func<Genre,bool>>>())).Returns(new List<Genre>());

            var testEntity = GetTestEntity();
            var result = testEntity.Find(x=>x.IsDeleted == true);

            Assert.IsInstanceOf(typeof(IEnumerable<Genre>), result);
        }

        private IGenreManager GetTestEntity()
        {
            return new GenreManager(_unitOfWorkMock.Object);
        }
    }
}
