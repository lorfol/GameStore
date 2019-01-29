using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using GameStore.Domain.Core.DomainModels;
using GameStore.Services.Interfaces;
using GameStore.Web.ViewModels;
using Microsoft.Ajax.Utilities;

namespace GameStore.Web.Controllers
{
    public class BasketController : Controller
    {
        private readonly IGameManager _gameManager;

        private const string basketSession = "Basket";
        private const string countSession = "Count";


        public BasketController(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public ActionResult GetBasket()
        {
            var gamesInCart = (List<string>)Session[basketSession];
            var basketViewModels = new List<BasketItemViewModel>();

            if (gamesInCart != null && gamesInCart.Count != 0)
            {
                KeysToViewModelsRebuilder(ref basketViewModels, gamesInCart);

                ViewBag.IsEmpty = false; // todo : try to check collection count on view
            }
            else
            {
                ViewBag.IsEmpty = true;
            }

            return View("Basket", basketViewModels);
        }

        public ActionResult AddToBasket(string gameKey)
        {
            if (Session[basketSession] == null)
            {
                Session[basketSession] = new List<string>();
            }

            var itemsInCart = (List<string>)Session[basketSession];

            var a = itemsInCart.Count(x => x == gameKey);
            var gCount = _gameManager.GetGameByKey(gameKey).UnitsInStock;

            if (a < gCount)
            {
                itemsInCart.Add(gameKey);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            Session[basketSession] = itemsInCart;
            Session[countSession] = itemsInCart.Count; // todo: try to avoid use this

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult RemoveFromBasket(string gameKey)
        {
            if (Session[basketSession] != null && (int)Session[countSession] >= 0)
            {
                var itemsInCart = (List<string>)Session[basketSession];

                itemsInCart.Remove(gameKey);

                Session[basketSession] = itemsInCart;
                Session[countSession] = itemsInCart.Count;

                if ((int)Session[countSession] == 0)
                {
                    Session[basketSession] = null;
                    ViewBag.IsEmpty = true;
                }
            }

            return RedirectToAction("GetBasket", "Basket");
        }

        private void KeysToViewModelsRebuilder(ref List<BasketItemViewModel> listToRebuild, IEnumerable<string> gameKeys) // todo: rename
        {
            var games = gameKeys.Select(x => _gameManager.GetGameByKey(x)).ToList();
            var tempGamesToView = Mapper.Map<IEnumerable<Game>, IEnumerable<BasketItemViewModel>>(games).ToList();

            listToRebuild = tempGamesToView.DistinctBy(g => g.Key).ToList();

            foreach (var item in listToRebuild)
            {
                item.UnitsInStock = Convert.ToInt16(tempGamesToView.Count(x => x.Key == item.Key));
                item.Price *= item.UnitsInStock;
            }
        }
    }
}