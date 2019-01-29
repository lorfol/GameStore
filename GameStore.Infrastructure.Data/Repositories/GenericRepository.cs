using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using GameStore.Domain.Core;
using GameStore.Domain.Interfaces.DomainRepositories;
using LinqKit;

namespace GameStore.Infrastructure.Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly GameStoreDbContext _context;
        private readonly IDbSet<TEntity> _entities;

        public GenericRepository(GameStoreDbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public void Create(TEntity item)
        {
            _entities.Add(item);
        }

        public void Update(TEntity item)
        {
            var entity = _entities.Find(item.Id);

            if (entity == null)
            {
                return;
            }

            _context.Entry(entity).CurrentValues.SetValues(item);
        }

        public void Delete(TEntity item)
        {
            var entity = _entities.Find(item);

            entity.IsDeleted = true;

            Update(entity);
        }

        public void DeleteById(object id)
        {
            var entity = _entities.Find(id);

            entity.IsDeleted = true;

            Update(entity);
        }

        public TEntity GetById(object id)
        {
            return _entities.Find(id);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return GetExistingEntities().AsExpandable().Where(predicate);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return GetExistingEntities().ToList();
        }

        private IQueryable<TEntity> GetExistingEntities()
        {
            return _entities.Where(x => !x.IsDeleted);
        }
    }
}
