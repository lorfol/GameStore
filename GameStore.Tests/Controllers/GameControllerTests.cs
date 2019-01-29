using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Web.Mvc;
using AutoMapper;
using GameStore.Domain.Core.DomainModels;
using GameStore.Services.Interfaces;
using GameStore.Web.Controllers;
using GameStore.Web.Mapping;
using GameStore.Web.ViewModels;
using Moq;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;

namespace GameStore.Tests.Controllers
{
    [TestFixture]
    public class GameControllerTests
    {
        private Mock<IGameManager> _gameManagerMock;
        private Mock<IPlatformManager> _platformManagerMock;
        private Mock<IGenreManager> _genreManagerMock;
        private Mock<IPublisherManager> _publisherManagerMock;

        [SetUp]
        public void SetupContext()
        {
            _gameManagerMock = new Mock<IGameManager>();
            _genreManagerMock = new Mock<IGenreManager>();
            _platformManagerMock = new Mock<IPlatformManager>();
            _publisherManagerMock = new Mock<IPublisherManager>();
        }

        //[Test]
        //public void CreateGame_ShouldReturnErrorViewWhenKeyIsNull()
        //{
        //    var testedService = GetTestEntity();
        //    var result = testedService.CreateGame(new CreateGameViewModel { Key = null }) as ViewResult;

        //    Assert.AreEqual("Error", result.ViewName);
        //}

        //[Test]
        //public void CreateGame_ShouldReturnErrorViewWhenKeyIsEmpty()
        //{
        //    var testedService = GetTestEntity();
        //    var result = testedService.CreateGame(new CreateGameViewModel {Key = String.Empty}) as ViewResult;

        //    Assert.AreEqual("Error", result.ViewName);
        //}

        [Test]
        public void GameController_DeleteGame_ReturnCorrectView()
        {
            var testedService = GetTestEntity();
            var dummyGame = GetDummyGame();

            _gameManagerMock.Setup(x => x.DeleteGameById(It.IsAny<int>())).Returns(HttpStatusCode.OK);

            var result = testedService.DeleteGame(dummyGame.Id) as RedirectToRouteResult;

            Assert.AreEqual(true, result.RouteValues.ContainsValue("GetAllGames"));
        }

        [Test]
        public void GameController_GetAllGames_ReturnCorrectView()
        {
            var testedService = GetTestEntity();
            _gameManagerMock.Setup(x => x.GetAllGames()).Returns(new List<Game>());

            var result = testedService.GetAllGames(null,null) as ViewResult;

            Assert.AreEqual("AllGames", result.ViewName);
        }

        [Test]
        public void GameController_GetGameDetails_ReturnCorrectView()
        {
            var testedService = GetTestEntity();
            const string dummyKey = "Key";

            var res = testedService.GetGameDetails(dummyKey) as ViewResult;

            _gameManagerMock.Verify(x => x.GetGameByKey(dummyKey), Times.Once);
            Assert.AreEqual("GameDetails", res.ViewName);
        }

        [Test]
        public void GameController_GetGameDetails_ShouldErrorViewWhenKeyIsNull()
        {
            var testedService = GetTestEntity();
            var result = testedService.GetGameDetails(null) as ViewResult;

            Assert.AreEqual("GameDetails", result.ViewName);
        }

        [Test]
        public void GameController_GetGameDetails_ShouldReturnErrorViewWhenKeyIsEmptyString()
        {
            var testedService = GetTestEntity();

            var result = testedService.GetGameDetails(string.Empty) as ViewResult;

            Assert.AreEqual("GameDetails", result.ViewName);
        }

        [Test]
        public void CreateGet_ReturnCorrectView()
        {
            var testedInstance = GetTestEntity();

            _genreManagerMock.Setup(x => x.GetAllGenres()).Returns(new List<Genre>());
            _platformManagerMock.Setup(x => x.GetAllPlatforms()).Returns(new List<Platform>());
            _publisherManagerMock.Setup(x => x.GetAllPublishers()).Returns(new List<Publisher>());

            var res = testedInstance.CreateGame() as ViewResult;

            Assert.AreEqual("CreateGame", res.ViewName);
        }

        //[Test]
        //public void EditGame_Get()
        //{
        //    const string key = "key";

        //    _gameManagerMock.Setup(x => x.GetGameByKey(It.IsAny<string>()))
        //        .Returns(GetDummyGame());
        //    _publisherManagerMock.Setup(x => x.GetAllPublishers()).Returns(new List<Publisher>()
        //        {new Publisher() {Id = 1}, new Publisher() {Id = 2}});
        //    _platformManagerMock.Setup(x => x.GetAllPlatforms()).Returns(new List<Platform>()
        //        {new Platform() {Id = 1}, new Platform() {Id = 2}});
        //    _genreManagerMock.Setup(x => x.GetAllGenres())
        //        .Returns(new List<Genre>() { new Genre() { Id = 1 }, new Genre() { Id = 2 } });

        //    var controller = GetTestEntity();

        //    var result = controller.EditGame(key) as ViewResult;
        //    var game = result.ViewData.Model as CreateGameViewModel;
        //    var expected = GetViewModelGame();

        //    _gameManagerMock.Verify(x => x.GetGameByKey(key), Times.Once);
        //    _genreManagerMock.Verify(x => x.GetAllGenres(), Times.Once);
        //    _publisherManagerMock.Verify(x => x.GetAllPublishers(), Times.Once);
        //    _platformManagerMock.Verify(x => x.GetAllPlatforms(), Times.Once);
        //    Assert.AreEqual(expected.Key, game.Key);
        //    Assert.AreEqual(expected.SelectedGenres, game.SelectedGenres);
        //    Assert.AreEqual(expected.SelectedPlatforms, game.SelectedPlatforms);
        //    Assert.AreEqual(expected.SelectedPublishers, game.SelectedPublishers);
        //    Assert.AreEqual("EditGame", result.ViewName);
        //}

        [Test]
        public void EditGame_ShouldReturnCorrectView()
        {
            const string key = "key";

            _gameManagerMock.Setup(x => x.GetGameByKey(It.IsAny<string>()))
                .Returns(GetDummyGame());

            var controller = GetTestEntity();

            var result = controller.EditGame(key) as ViewResult;

            Assert.AreEqual("EditGame", result.ViewName);
        }

        [Test]
        public void EditGame_SelectedOnlyPublishers()
        {
            const string key = "key";

            _gameManagerMock.Setup(x => x.GetGameByKey(It.IsAny<string>()))
                .Returns(GetDummyGame());
            _publisherManagerMock.Setup(x => x.GetAllPublishers()).Returns(new List<Publisher>()
                {new Publisher() {Id = 1}, new Publisher() {Id = 2}});

            var controller = GetTestEntity();

            var result = controller.EditGame(key) as ViewResult;
            var game = result.ViewData.Model as CreateGameViewModel;
            var expected = GetViewModelGame();

            _gameManagerMock.Verify(x => x.GetGameByKey(key), Times.Once);
            Assert.AreEqual(expected.SelectedPublishers, game.SelectedPublishers);
            Assert.AreEqual(0, game.SelectedGenres.Count);
            Assert.AreEqual(0, game.SelectedPlatforms.Count);
        }

        [Test]
        public void EditGame_SelectedOnlyGenres()
        {
            const string key = "key";

            _gameManagerMock.Setup(x => x.GetGameByKey(It.IsAny<string>()))
                .Returns(GetDummyGame());
            _genreManagerMock.Setup(x => x.GetAllGenres())
                .Returns(new List<Genre>() { new Genre() { Id = 1 }, new Genre() { Id = 2 } });

            var controller = GetTestEntity();

            var result = controller.EditGame(key) as ViewResult;
            var game = result.ViewData.Model as CreateGameViewModel;
            var expected = GetViewModelGame();

            _gameManagerMock.Verify(x => x.GetGameByKey(key), Times.Once);
            Assert.AreEqual(expected.SelectedGenres, game.SelectedGenres);
            Assert.AreEqual(0, game.SelectedPublishers.Count);
            Assert.AreEqual(0, game.SelectedPlatforms.Count);
        }

        [Test]
        public void EditGame_SelectedOnlyPlatforms()
        {
            const string key = "key";

            _gameManagerMock.Setup(x => x.GetGameByKey(It.IsAny<string>()))
                .Returns(GetDummyGame());
            _platformManagerMock.Setup(x => x.GetAllPlatforms())
                .Returns(new List<Platform>() { new Platform() { Id = 1 }, new Platform() { Id = 2 } });

            var controller = GetTestEntity();

            var result = controller.EditGame(key) as ViewResult;
            var game = result.ViewData.Model as CreateGameViewModel;
            var expected = GetViewModelGame();

            _gameManagerMock.Verify(x => x.GetGameByKey(key), Times.Once);
            Assert.AreEqual(expected.SelectedPlatforms, game.SelectedPlatforms);
            Assert.AreEqual(0, game.SelectedGenres.Count);
            Assert.AreEqual(0, game.SelectedPublishers.Count);
        }

        [Test]
        public void EditGame_Post_WhenSelectedPropertiesCountBiggerThan0()
        {
            var viewModel = GetViewModelGame();
            var gameFromDb = GetDummyGame();
            gameFromDb.Genres.Add(new Genre { Id = 3 });
            gameFromDb.Platforms.Add(new Platform { Id = 3 });
            gameFromDb.Publishers.Add(new Publisher { Id = 3 });

            _gameManagerMock.Setup(x => x.GetGameByKey(It.IsAny<string>()))
                .Returns(gameFromDb);

            _platformManagerMock.Setup(x => x.Find(It.IsAny<Expression<Func<Platform, bool>>>()))
                .Returns(new List<Platform>());

            _genreManagerMock.Setup(x => x.Find(It.IsAny<Expression<Func<Genre, bool>>>()))
                .Returns(new List<Genre>());

            _publisherManagerMock.Setup(x => x.Find(It.IsAny<Expression<Func<Publisher, bool>>>()))
                .Returns(new List<Publisher>());

            var controller = GetTestEntity();
            var result = controller.EditGame(viewModel) as RedirectToRouteResult;

            _gameManagerMock.Verify(x => x.GetGameByKey(viewModel.Key), Times.Once);
            _gameManagerMock.Verify(x => x.EditGame(gameFromDb), Times.Once);
            Assert.AreEqual(true, result.RouteValues.ContainsValue("GetAllGames"));
            Assert.AreEqual(2, gameFromDb.Genres.Count);
            Assert.AreEqual(2, gameFromDb.Publishers.Count);
            Assert.AreEqual(2, gameFromDb.Platforms.Count);

        }

        [Test]
        public void Edit_Post_WhenSelectedPropertiesCountIs0()
        {
            var viewModel = GetViewModelGame();
            var gameFromDb = GetDummyGame();

            viewModel.SelectedGenres = new List<int>();
            viewModel.SelectedPlatforms = new List<int>();
            viewModel.SelectedPublishers = new List<int>();

            _gameManagerMock.Setup(x => x.GetGameByKey(It.IsAny<string>()))
                .Returns(gameFromDb);
            _platformManagerMock.Setup(x => x.Find(It.IsAny<Expression<Func<Platform, bool>>>()))
                .Returns(new List<Platform>());
            _genreManagerMock.Setup(x => x.Find(It.IsAny<Expression<Func<Genre, bool>>>()))
                .Returns(new List<Genre>());
            _publisherManagerMock.Setup(x => x.Find(It.IsAny<Expression<Func<Publisher, bool>>>()))
                .Returns(new List<Publisher>());

            var controller = GetTestEntity();
            var result = controller.EditGame(viewModel) as RedirectToRouteResult;

            _gameManagerMock.Verify(x => x.EditGame(gameFromDb), Times.Once);
            Assert.AreEqual(true, result.RouteValues.ContainsValue("GetAllGames"));
            Assert.AreEqual(0, gameFromDb.Genres.Count);
            Assert.AreEqual(0, gameFromDb.Publishers.Count);
            Assert.AreEqual(0, gameFromDb.Platforms.Count);
        }

        private GameController GetTestEntity()
        {
            return new GameController(_gameManagerMock.Object, _genreManagerMock.Object, _platformManagerMock.Object, _publisherManagerMock.Object);
        }

        private Game GetDummyGame()
        {
            return new Game()
            {
                Key = "key",
                Platforms = new List<Platform>()
                    {new Platform {Id = 1}, new Platform() {Id = 2}},
                Genres = new List<Genre> { new Genre() { Id = 1 }, new Genre() { Id = 2 } },
                Publishers = new List<Publisher>()
                    {new Publisher {Id = 1}, new Publisher() {Id = 2}}
            };
        }

        private CreateGameViewModel GetViewModelGame()
        {
            return new CreateGameViewModel()
            {
                Key = "key",
                SelectedPlatforms = new List<int> { 1, 2 },
                SelectedGenres = new List<int> { 1, 2 },
                SelectedPublishers = new List<int> { 1, 2 },
                AllPlatforms = new MultiSelectList(new List<Platform> { new Platform { Id = 1 }, new Platform { Id = 2 } }, "Id", "Type", new List<int> { 1, 2 }),
                AllGenres = new MultiSelectList(new List<Genre> { new Genre { Id = 1 }, new Genre { Id = 2 } }, "Id", "Name", new List<int> { 1, 2 }),
                AllPublishers = new MultiSelectList(new List<Publisher> { new Publisher { Id = 1 }, new Publisher { Id = 2 } }, "Id", "CompanyName", new List<int> { 1, 2 }),
            };
        }
    }
}