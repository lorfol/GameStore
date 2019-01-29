using GameStore.Domain.Core.DomainModels;
using System.Data.Entity;

namespace GameStore.Infrastructure.Data
{
    public class GameStoreDbContext : DbContext
    {
        public GameStoreDbContext() : base("DefaultConnection")
        {
        }

        public virtual IDbSet<Game> Games { get; set; }

        public virtual IDbSet<Comment> Comments { get; set; }

        public virtual IDbSet<Genre> Genres { get; set; }

        public virtual IDbSet<Platform> Platforms { get; set; }

        public virtual IDbSet<Publisher> Publishers { get; set; }

        public virtual IDbSet<Order> Orders { get; set; }

        public virtual IDbSet<OrderDetails> OrderDetails { get; set; }
    }
}
