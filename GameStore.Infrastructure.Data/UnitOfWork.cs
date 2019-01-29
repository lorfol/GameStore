using GameStore.Domain.Interfaces;
using GameStore.Domain.Interfaces.DomainRepositories;
using GameStore.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.Core.DomainModels;

namespace GameStore.Infrastructure.Data
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly GameStoreDbContext _dbContext;

        private IGenericRepository<Game> _gameRepository;
        private IGenericRepository<Comment> _commentRepository;
        private IGenericRepository<Genre> _genreRepository;
        private IGenericRepository<Platform> _platformRepository;
        private IGenericRepository<Publisher> _publisherRepository;
        private IGenericRepository<Order> _orderRepository;
        private IGenericRepository<OrderDetails> _orderDetailsRepository;


        public UnitOfWork(GameStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IGenericRepository<Game> Games => _gameRepository ?? (_gameRepository = new GenericRepository<Game>(_dbContext));

        public IGenericRepository<Comment> Comments => _commentRepository ?? (_commentRepository = new GenericRepository<Comment>(_dbContext));

        public IGenericRepository<Genre> Genres => _genreRepository ?? (_genreRepository = new GenericRepository<Genre>(_dbContext));

        public IGenericRepository<Platform> Platforms => _platformRepository ?? (_platformRepository = new GenericRepository<Platform>(_dbContext));

        public IGenericRepository<Publisher> Publishers => _publisherRepository ?? (_publisherRepository = new GenericRepository<Publisher>(_dbContext));

        public IGenericRepository<Order> Orders => _orderRepository ?? (_orderRepository = new GenericRepository<Order>(_dbContext));

        public IGenericRepository<OrderDetails> OrderDetails => _orderDetailsRepository ?? (_orderDetailsRepository = new GenericRepository<OrderDetails>(_dbContext));

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
