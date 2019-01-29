using System;
using System.Collections.Generic;
using GameStore.Domain.Core.Enums;

namespace GameStore.Domain.Core.DomainModels
{
    public class Order : BaseEntity
    {
        public Order()
        {
            Positions = new List<OrderDetails>();
            Status = OrderStatusEnum.New;
        }

        public int? CustomerId { get; set; } // TODO : temporary nullable coz we don't have customer yet

        public DateTime OrderDate { get; set; }

        public OrderStatusEnum Status { get; set; }

        public virtual List<OrderDetails> Positions { get; set; }
    }
}
