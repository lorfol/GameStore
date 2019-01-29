using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GameStore.Domain.Core.DomainModels;
using GameStore.Domain.Interfaces;
using GameStore.Infrastructure.Business;
using GameStore.Services.Interfaces;
using Moq;
using NUnit.Framework;

namespace GameStore.Tests.Services
{
    [TestFixture]
    public class GameManagerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;

        [SetUp]
        public void TestSetup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Test]
        public void GameManager_CreateNewGame_CallsCreateActionFromUnitOfWork()
        {
            _unitOfWorkMock.Setup(h => h.Games.Create(It.IsAny<Game>()));

            var dummyGame = GetGame();

            var testEntity = GetTestEntity();
            testEntity.CreateNewGame(dummyGame);

            _unitOfWorkMock.Verify(x => x.Games.Create(dummyGame), Times.Once);
        }

        [Test]
        public void GameManager_DeleteGame_CallsDeleteActionFromUnitOfWork()
        {
            _unitOfWorkMock.Setup(x => x.Games.Delete(It.IsAny<Game>()));

            var dummyGame = GetGame();

            var testEntity = GetTestEntity();
            testEntity.DeleteGame(dummyGame);

            _unitOfWorkMock.Verify(x => x.Games.Delete(dummyGame), Times.Once);
        }

        [Test]
        public void GameManager_DeleteGameById_CallsDeleteActionFromUnitOfWork()
        {
            _unitOfWorkMock.Setup(x => x.Games.DeleteById(It.IsAny<int>()));

            var dummyGame = GetGame();

            var testEntity = GetTestEntity();
            testEntity.DeleteGameById(dummyGame.Id);

            _unitOfWorkMock.Verify(x => x.Games.DeleteById(dummyGame.Id), Times.Once);
        }

        [Test]
        public void GameManager_EditGame_CallsEditActionFromUnitOfWork()
        {
            _unitOfWorkMock.Setup(x => x.Games.Update(It.IsAny<Game>()));

            var dummyGame = GetGame();

            var testEntity = GetTestEntity();
            testEntity.EditGame(dummyGame);

            _unitOfWorkMock.Verify(x => x.Games.Update(dummyGame), Times.Once);
        }

        [Test]
        public void GameManager_GetAllGames_CallsGetAllActionFromUnitOfWork()
        {
            _unitOfWorkMock.Setup(x => x.Games.GetAll()).Returns(new List<Game>());

            var testEntity = GetTestEntity();
            testEntity.GetAllGames();

            _unitOfWorkMock.Verify(x => x.Games.GetAll(), Times.Once);
        }

        [Test]
        public void GameManager_GetGameByKey_CallsFindActionFromUnitOfWork()
        {
            _unitOfWorkMock.Setup(x => x.Games.Find(It.IsAny<Expression<Func<Game, bool>>>())).Returns(new List<Game>());

            var testEntity = GetTestEntity();
            testEntity.GetGameByKey("Key");

            _unitOfWorkMock.Verify(x => x.Games.Find(It.IsAny<Expression<Func<Game, bool>>>()), Times.Once);
        }

        [Test]
        public void GameManager_GetGamesByGenre_CallsFindActionFromUnitOfWork()
        {
            _unitOfWorkMock.Setup(x => x.Games.Find(It.IsAny<Expression<Func<Game, bool>>>())).Returns(new List<Game>());

            var testEntity = GetTestEntity();
            testEntity.GetGamesByGenre("GenreName");

            _unitOfWorkMock.Verify(x => x.Games.Find(It.IsAny<Expression<Func<Game, bool>>>()), Times.Once);
        }

        [Test]
        public void GameManager_GetGamesByPlatformType_CallsFindActionFromUnitOfWork()
        {
            _unitOfWorkMock.Setup(x => x.Games.Find(It.IsAny<Expression<Func<Game, bool>>>())).Returns(new List<Game>());

            var testEntity = GetTestEntity();
            testEntity.GetGamesByPlatformType("PlatformName");

            _unitOfWorkMock.Verify(x => x.Games.Find(It.IsAny<Expression<Func<Game, bool>>>()), Times.Once);
        }

        private IGameManager GetTestEntity()
        {
            return new GameManager(_unitOfWorkMock.Object);
        }

        private Game GetGame()
        {
            return new Game
            {
                Id = 1,
                Comments = new List<Comment>(),
                Name = "Name", Key = "Key",
                Description = "string",
                Genres = new List<Genre>
                {
                    new Genre
                    {
                        Id = 1,
                        Games = new List<Game>(),
                        Name = "564654"
                    }
                },
                Platforms = new List<Platform>()
            };
        }
    }
}
