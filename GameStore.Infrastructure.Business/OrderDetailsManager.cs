using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using GameStore.Domain.Core.DomainModels;
using GameStore.Domain.Interfaces;
using GameStore.Services.Interfaces;

namespace GameStore.Infrastructure.Business
{
    public class OrderDetailsManager : IOrderDetailsManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderDetailsManager(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public HttpStatusCode CreateNewOrderDetails(OrderDetails orderDetails)
        {
            _unitOfWork.OrderDetails.Create(orderDetails);
            _unitOfWork.Save();

            return HttpStatusCode.OK;
        }

        public HttpStatusCode DeleteOrderDetailsById(int orderDetailsId)
        {
            _unitOfWork.OrderDetails.DeleteById(orderDetailsId);
            _unitOfWork.Save();

            return HttpStatusCode.OK;
        }

        public IEnumerable<OrderDetails> GetAllOrderDetailsByOrderId(int id)
        {
            return _unitOfWork.OrderDetails.Find(o => o.OrderId == id);
        }

        public HttpStatusCode DeleteOrderDetails(OrderDetails orderDetails)
        {
            _unitOfWork.OrderDetails.Delete(orderDetails);
            _unitOfWork.Save();

            return HttpStatusCode.OK;
        }

        public HttpStatusCode EditOrderDetails(OrderDetails orderDetails)
        {
            _unitOfWork.OrderDetails.Update(orderDetails);
            _unitOfWork.Save();

            return HttpStatusCode.OK;
        }
    }
}
