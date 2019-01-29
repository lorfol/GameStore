using System.Collections.Generic;
using System.Linq;
using System.Net;
using GameStore.Domain.Core.DomainModels;
using GameStore.Domain.Interfaces;
using GameStore.Services.Interfaces;

namespace GameStore.Infrastructure.Business
{
    public class OrderManager : IOrderManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderManager(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public HttpStatusCode CreateNewOrder(Order order)
        {
            _unitOfWork.Orders.Create(order);
            _unitOfWork.Save();

            return HttpStatusCode.OK;
        }

        public HttpStatusCode EditOrder(Order order)
        {
            _unitOfWork.Orders.Update(order);
            _unitOfWork.Save();

            return HttpStatusCode.OK;
        }

        public HttpStatusCode DeleteOrder(Order order)
        {
            _unitOfWork.Orders.Delete(order);
            _unitOfWork.Save();

            return HttpStatusCode.OK;
        }

        public HttpStatusCode DeleteOrderById(string orderId)
        {
            _unitOfWork.Orders.DeleteById(orderId);
            _unitOfWork.Save();

            return HttpStatusCode.OK;
        }

        public IEnumerable<Order> GetAllOrdersByCustomerId(int customerId)
        {
            return _unitOfWork.Orders.Find(x => x.CustomerId == customerId).ToList();
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _unitOfWork.Orders.GetAll().ToList();
        }
    }
}
