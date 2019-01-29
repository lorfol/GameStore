using System;
using System.Collections.Generic;
using System.Data.Entity;
using GameStore.Domain.Core.DomainModels;
using GameStore.Infrastructure.Data;
using GameStore.Infrastructure.Data.Repositories;
using Moq;
using NUnit.Framework;

namespace GameStore.Tests
{
    [TestFixture]
    public class GenericRepositoryTests
    {
        private Mock<GameStoreDbContext> contextMock;
        private Mock<DbSet<Game>> entitiesMock;

        [Test]
        public void CreateGame_CallsAddMethod()
        {
            Mock<GameStoreDbContext> contextMock = new Mock<GameStoreDbContext>();
            Mock<DbSet<Game>> entitiesMock = new Mock<DbSet<Game>>();

            contextMock.Setup(x => x.Set<Game>()).Returns(entitiesMock.Object);
            entitiesMock.Setup(x => x.Add(It.IsAny<Game>()));

            GenericRepository<Game> genericRepository = new GenericRepository<Game>(contextMock.Object);

            var game = new Game
            {
                Name = "",
                Key = "",
                Genres = new List<Genre>(),
                Publishers = new List<Publisher>(),
                Platforms = new List<Platform>(),
                IsDeleted = false,
                Comments = new List<Comment>(),
                UnitsInStock = 12,
                Id = 1,
                Description = "",
                Price = 123,
                Discontinued = true
            };

            genericRepository.Create(game);

            entitiesMock.Verify(x => x.Add(game), Times.Once);
        }

        [Test]
        public void CreatePublisher_CallsAddMethod()
        {

            Mock<GameStoreDbContext> contextMock = new Mock<GameStoreDbContext>();
            Mock<DbSet<Publisher>> entitiesMock = new Mock<DbSet<Publisher>>();

            contextMock.Setup(x => x.Set<Publisher>()).Returns(entitiesMock.Object);
            entitiesMock.Setup(x => x.Add(It.IsAny<Publisher>()));

            GenericRepository<Publisher> genericRepository = new GenericRepository<Publisher>(contextMock.Object);

            var item = new Publisher
            {
                CompanyName = "qwer",
                Description = "asdf",
                Id = 1,
                IsDeleted = false,
                Games = new List<Game>(),
                HomePage = "zxcv"
            };

            genericRepository.Create(item);

            entitiesMock.Verify(x => x.Add(item), Times.Once);
        }

        [Test]
        public void CreateOrder_CallsAddMethod()
        {

            Mock<GameStoreDbContext> contextMock = new Mock<GameStoreDbContext>();
            Mock<DbSet<Order>> entitiesMock = new Mock<DbSet<Order>>();

            contextMock.Setup(x => x.Set<Order>()).Returns(entitiesMock.Object);
            entitiesMock.Setup(x => x.Add(It.IsAny<Order>()));

            GenericRepository<Order> genericRepository = new GenericRepository<Order>(contextMock.Object);

            var item = new Order
            {
                Id = 1,
                IsDeleted = true,
                Positions = new List<OrderDetails>(),
                CustomerId = 1,
                OrderDate = DateTime.UtcNow
            };

            genericRepository.Create(item);

            entitiesMock.Verify(x => x.Add(item), Times.Once);
        }

        [Test]
        public void CreateOrderDetails_CallsAddMethod()
        {

            Mock<GameStoreDbContext> contextMock = new Mock<GameStoreDbContext>();
            Mock<DbSet<OrderDetails>> entitiesMock = new Mock<DbSet<OrderDetails>>();

            contextMock.Setup(x => x.Set<OrderDetails>()).Returns(entitiesMock.Object);
            entitiesMock.Setup(x => x.Add(It.IsAny<OrderDetails>()));

            GenericRepository<OrderDetails> genericRepository = new GenericRepository<OrderDetails>(contextMock.Object);

            var item = new OrderDetails
            {
                Game = new Game(),
                Order = new Order(),
                Id = 1,
                IsDeleted = true,
                Price = 0.01m,
                Discount = 0.01f,
                GameId = 1,
                OrderId = 1,
                Quantity = 10
            };

            genericRepository.Create(item);

            entitiesMock.Verify(x => x.Add(item), Times.Once);
        }

        [Test]
        public void GetById_CallsFindAndRemoveMethods()
        {
            contextMock = new Mock<GameStoreDbContext>();
            entitiesMock = new Mock<DbSet<Game>>();
            contextMock.Setup(x => x.Set<Game>()).Returns(entitiesMock.Object);

            entitiesMock.Setup(x => x.Find(It.IsAny<object>())).Returns(new Game());

            GenericRepository<Game> genericRepository = new GenericRepository<Game>(contextMock.Object);

            genericRepository.GetById(1);

            entitiesMock.Verify(x => x.Find(1), Times.Once);
        }

        [Test]
        public void AddGenre()
        {
            contextMock = new Mock<GameStoreDbContext>();
            Mock<DbSet<Genre>> entitiesMock;
            entitiesMock = new Mock<DbSet<Genre>>();
            contextMock.Setup(x => x.Set<Genre>()).Returns(entitiesMock.Object);

            entitiesMock.Setup(x => x.Find(It.IsAny<object>())).Returns(new Genre
            {
                Id = 1,
                Games = new List<Game>(),
                Name = "54574"
            });

            GenericRepository<Genre> genericRepository = new GenericRepository<Genre>(contextMock.Object);

            var genre = new Genre
            {
                Id = 1,
                Games = new List<Game>(),
                Name = "54574"
            };

            genericRepository.Create(genre);

            entitiesMock.Verify(x => x.Add(genre), Times.Once);
        }

        [Test]
        public void AddPlatform()
        {
            contextMock = new Mock<GameStoreDbContext>();
            Mock<DbSet<Platform>> entitiesMock;
            entitiesMock = new Mock<DbSet<Platform>>();
            contextMock.Setup(x => x.Set<Platform>()).Returns(entitiesMock.Object);

            entitiesMock.Setup(x => x.Find(It.IsAny<object>())).Returns(new Platform
            {
                Id = 1,
                Games = new List<Game>(),
                Type = "type"
            });

            GenericRepository<Platform> genericRepository = new GenericRepository<Platform>(contextMock.Object);

            var platform = new Platform()
            {
                Id = 1,
                Games = new List<Game>(),
                Type = "type"
            };

            genericRepository.Create(platform);

            entitiesMock.Verify(x => x.Add(platform), Times.Once);
        }
    }
}
