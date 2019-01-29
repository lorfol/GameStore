using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GameStore.Domain.Core;

namespace GameStore.Domain.Interfaces.DomainRepositories
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        void Create(TEntity item);

        void Update(TEntity item);

        void Delete(TEntity item);

        void DeleteById(object id);

        TEntity GetById(object id);

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> GetAll();
    }
}
