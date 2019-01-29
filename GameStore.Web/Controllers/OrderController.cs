using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using GameStore.Domain.Core.DomainModels;
using GameStore.Services.Interfaces;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderManager _orderManager;
        private readonly IOrderDetailsManager _orderDetailsManager;
        private readonly IGameManager _gameManager;

        private const string basketSession = "Basket";
        private const string countSession = "Count";

        public OrderController(IOrderManager orderManager, IOrderDetailsManager orderDetailsManager, IGameManager gameManager)
        {
            _orderManager = orderManager;
            _orderDetailsManager = orderDetailsManager;
            _gameManager = gameManager;
        }

        [HttpPost]
        public ActionResult ConfirmOrder(IList<BasketItemViewModel> items)
        {
            var orderDetails =
                Mapper.Map<IEnumerable<BasketItemViewModel>, IEnumerable<OrderDetails>>(items).ToList();

            foreach (var item in orderDetails)
            {
                _orderDetailsManager.CreateNewOrderDetails(item);
            }

            if (orderDetails.Count == 0)
            {
                ViewBag.ErrorMessage = "Order was empty";

                return View("Error");
            }

            var order = new Order
            {
                OrderDate = DateTime.UtcNow,
                Positions = orderDetails
            };

            _orderManager.CreateNewOrder(order);


            var itemsInBasket = ((List<string>)Session[basketSession]).Distinct().ToList();

            foreach (var item in items)
            {
                itemsInBasket.Remove(item.Key);
            }

            Session[basketSession] = itemsInBasket;
            Session[countSession] = itemsInBasket.Count;

            return RedirectToAction("OrderDetails", order);
        }

        public ActionResult OrderDetails(Order order)
        {
            return View("OrderDetails", order);
        }
    }
}