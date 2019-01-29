using GameStore.Domain.Interfaces.DomainRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.Core.DomainModels;

namespace GameStore.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Game> Games { get; }

        IGenericRepository<Comment> Comments { get; }

        IGenericRepository<Genre> Genres { get; }

        IGenericRepository<Platform> Platforms { get; }

        IGenericRepository<Publisher> Publishers { get; }

        IGenericRepository<Order> Orders { get; }

        IGenericRepository<OrderDetails> OrderDetails { get; }

        void Save();
    }
}
