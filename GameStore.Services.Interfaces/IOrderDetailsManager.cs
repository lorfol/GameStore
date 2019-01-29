using System.Collections.Generic;
using System.Net;
using GameStore.Domain.Core.DomainModels;

namespace GameStore.Services.Interfaces
{
    public interface IOrderDetailsManager
    {
        HttpStatusCode CreateNewOrderDetails(OrderDetails orderDetails);

        HttpStatusCode EditOrderDetails(OrderDetails orderDetails);

        HttpStatusCode DeleteOrderDetails(OrderDetails orderDetails);

        HttpStatusCode DeleteOrderDetailsById(int orderDetailsId);

        IEnumerable<OrderDetails> GetAllOrderDetailsByOrderId(int id);
    }
}
