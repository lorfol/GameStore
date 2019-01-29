using System;
using AutoMapper;
using GameStore.Domain.Core.DomainModels;
using GameStore.Services.Interfaces;
using GameStore.Web.ViewModels;
using PagedList;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Reflection;
using Org.BouncyCastle.Math;

namespace GameStore.Web.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameManager _gameManager;
        private readonly IGenreManager _genreManager;
        private readonly IPlatformManager _platformManager;
        private readonly IPublisherManager _publisherManager;

        public GameController(IGameManager gameManager, IGenreManager genreManager, IPlatformManager platformTypeManager, IPublisherManager publisherManager)
        {
            _gameManager = gameManager;
            _genreManager = genreManager;
            _platformManager = platformTypeManager;
            _publisherManager = publisherManager;
        }

        [HttpGet]
        public ActionResult GetAllGames(FilterOutputModel filterOutput, int? page)
        {
            if (filterOutput == null)
            {
                filterOutput = Session["FilterState"] as FilterOutputModel;
            }

            if (filterOutput.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)
                .Any(property => property.CanRead && property.GetValue(filterOutput, null) != null))
            {
                var result = _gameManager.GetFilteredResult(Mapper.Map<FilterOutputModel, IFilterModel>(filterOutput));

                int perPage = result.Count();

                if (Int32.TryParse(filterOutput.ItemsPerPage, out int number))
                {
                    if (number != 0) perPage = number;
                }


                var mappingResult = Mapper.Map<IEnumerable<Game>, IEnumerable<GameViewModel>>(result).ToPagedList(page ?? 1, perPage);
                var model = new GamesWithFilterComplexModel() { GameList = mappingResult, Filter = GetFilterModel() };

                Session["FilterState"] = filterOutput;

                return View("AllGames", model);
            }
            else
            {
                Session["FilterState"] = null;

                var gamesFromDb = _gameManager.GetAllGames().ToList();

                var mappingResult = Mapper.Map<IEnumerable<Game>, IEnumerable<GameViewModel>>(gamesFromDb).ToPagedList(page ?? 1, 2);
                var model = new GamesWithFilterComplexModel() { GameList = mappingResult, Filter = GetFilterModel() };

                return View("AllGames", model);
            }
        }

        [HttpGet]
        public ActionResult CreateGame()
        {
            var gameViewModel = new CreateGameViewModel();

            var genresFromDb = _genreManager.GetAllGenres().ToList();
            var platformsFromDb = _platformManager.GetAllPlatforms().ToList();
            var publishersFromDb = _publisherManager.GetAllPublishers().ToList();

            gameViewModel.AllGenres = new MultiSelectList(genresFromDb.OrderBy(i => i.Category), "Id", "Name", "Category");
            gameViewModel.AllPlatforms = new MultiSelectList(platformsFromDb.OrderBy(i => i.Type), "Id", "Type");
            gameViewModel.AllPublishers = new MultiSelectList(publishersFromDb.OrderBy(i => i.CompanyName), "Id", "CompanyName");

            return View("CreateGame", gameViewModel);
        }

        [HttpPost]
        public ActionResult CreateGame(CreateGameViewModel model)
        {
            var game = Mapper.Map<CreateGameViewModel, Game>(model);

            if (model.SelectedPlatforms.Any())
            {
                game.Platforms = _platformManager.Find(x => model.SelectedPlatforms.Any(f => f == x.Id)).ToList();
            }

            if (model.SelectedGenres.Any())
            {
                game.Genres = _genreManager.Find(x => model.SelectedGenres.Any(f => f == x.Id)).ToList();
            }

            if (model.SelectedPublishers.Any())
            {
                game.Publishers = _publisherManager.Find(x => model.SelectedPublishers.Any(f => f == x.Id)).ToList();
            }

            game.AddingDate = DateTime.UtcNow;
            var request = _gameManager.CreateNewGame(game);

            if (request != HttpStatusCode.Created)
            {
                throw new ValidationException("Invalid form data.");
            }

            return RedirectToAction("GetAllGames");

        }

        [HttpGet]
        public ActionResult EditGame(string gameKey)
        {
            var game = _gameManager.GetGameByKey(gameKey);

            var viewModel = Mapper.Map<Game, CreateGameViewModel>(game);

            var genres = _genreManager.GetAllGenres().OrderBy(a => a.Name).ToList();
            var publishers = _publisherManager.GetAllPublishers().OrderBy(p => p.CompanyName).ToList();
            var platforms = _platformManager.GetAllPlatforms().OrderBy(p => p.Type).ToList();

            viewModel.SelectedPlatforms = game.Platforms?
                .Where(g => platforms.Any(n => n.Id == g.Id))
                .Select(f => f.Id).ToList();

            viewModel.SelectedGenres = game.Genres?
                .Where(g => genres.Any(n => n.Id == g.Id))
                .Select(f => f.Id).ToList();

            viewModel.SelectedPublishers = game.Publishers?
                .Where(g => publishers.Any(n => n.Id == g.Id))
                .Select(f => f.Id).ToList();

            viewModel.AllPublishers = new MultiSelectList(publishers, "Id", "CompanyName", viewModel.SelectedPublishers);
            viewModel.AllPlatforms = new MultiSelectList(platforms, "Id", "Type", viewModel.SelectedPlatforms);
            viewModel.AllGenres = new MultiSelectList(genres, "Id", "Name", viewModel.SelectedGenres);

            ViewBag.GameKey = gameKey;

            return View("EditGame", viewModel);
        }

        [HttpPost]
        public ActionResult EditGame(CreateGameViewModel gameViewModel) // TODO : game editing
        {
            var gameFromDb = _gameManager.GetGameByKey(gameViewModel.Key);

            Mapper.Map(gameViewModel, gameFromDb);

            gameFromDb.Genres.RemoveAll(x => gameViewModel.SelectedGenres.All(f => f != x.Id));
            gameFromDb.Publishers.RemoveAll(x => gameViewModel.SelectedPublishers.All(f => f != x.Id));
            gameFromDb.Platforms.RemoveAll(x => gameViewModel.SelectedPlatforms.All(f => f != x.Id));

            if (gameViewModel.SelectedGenres != null)
            {
                var newGenresIds = gameViewModel.SelectedGenres.Except(gameFromDb.Genres.Select(g => g.Id));
                var newGenres = _genreManager.Find(f => newGenresIds.Any(g => g == f.Id));
                gameFromDb.Genres.AddRange(newGenres);
            }

            if (gameViewModel.SelectedPlatforms != null)
            {
                var newPlatformsIds = gameViewModel.SelectedPlatforms.Except(gameFromDb.Platforms.Select(g => g.Id));
                var newPlatforms = _platformManager.Find(f => newPlatformsIds.Any(g => g == f.Id));
                gameFromDb.Platforms.AddRange(newPlatforms);
            }

            if (gameViewModel.SelectedPublishers != null)
            {
                var newPublishersIds = gameViewModel.SelectedPublishers.Except(gameFromDb.Publishers.Select(g => g.Id));
                var newPublishers = _publisherManager.Find(f => newPublishersIds.Any(g => g == f.Id));
                gameFromDb.Publishers.AddRange(newPublishers);
            }

            _gameManager.EditGame(gameFromDb);

            return RedirectToAction("GetAllGames");
        }

        public ActionResult DeleteGame(int gameId)
        {

            var result = _gameManager.DeleteGameById(gameId);

            if (result != HttpStatusCode.OK)
            {
                throw new ValidationException("Invalid data.");
            }

            return RedirectToAction("GetAllGames");
        }

        public ViewResult GetGameDetails(string key)
        {
            var response = _gameManager.GetGameByKey(key);
            response.CountOfViews++;
            _gameManager.EditGame(response);

            var mapResult = Mapper.Map<Game, GameViewModel>(response);

            return View("GameDetails", mapResult);
        }

        [HttpGet]
        public FileResult DownloadGame(string gameKey)
        {
            string path = Server.MapPath("~/App_Data/KindaGame.txt");

            FileStream fs = new FileStream(path, FileMode.Open);

            string fileType = "application/txt";
            string fileName = "KindaGame.txt";

            return File(fs, fileType, fileName);
        }

        [OutputCache(Duration = 60)]
        public int GetCountOfGames()
        {
            return _gameManager.GetAllGames().Count();
        }

        private FilterViewModel GetFilterModel()
        {
            var filter = (FilterViewModel)Activator.CreateInstance(typeof(FilterViewModel), _genreManager, _platformManager, _publisherManager);

            return filter;
        }
    }
}