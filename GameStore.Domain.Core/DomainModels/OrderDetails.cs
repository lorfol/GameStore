using System;

namespace GameStore.Domain.Core.DomainModels
{
    public class OrderDetails : BaseEntity
    {
        public decimal Price { get; set; }

        public short Quantity { get; set; }

        public float Discount { get; set; }

        public int GameId { get; set; }

        public Game Game { get; set; }

        public int? OrderId { get; set; }

        public Order Order { get; set; }
    }
}
