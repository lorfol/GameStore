using System.Collections.Generic;
using System.Net;
using GameStore.Domain.Core.DomainModels;

namespace GameStore.Services.Interfaces
{
    public interface IOrderManager
    {
        HttpStatusCode CreateNewOrder(Order order);

        HttpStatusCode EditOrder(Order order);

        HttpStatusCode DeleteOrder(Order order);

        HttpStatusCode DeleteOrderById(string orderId);

        IEnumerable<Order> GetAllOrdersByCustomerId(int customerId);

        IEnumerable<Order> GetAllOrders();
    }
}
