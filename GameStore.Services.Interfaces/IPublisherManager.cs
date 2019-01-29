using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using GameStore.Domain.Core.DomainModels;

namespace GameStore.Services.Interfaces
{
    public interface IPublisherManager
    {
        HttpStatusCode CreateNewPublisher(Publisher publisher);

        HttpStatusCode EditPublisher(int publisherId, Publisher publisher);

        HttpStatusCode DeletePublisher(Publisher publisher);

        HttpStatusCode DeletePublisherById(int id);

        IEnumerable<Publisher> GetAllPublishers();

        IEnumerable<Publisher> Find(Expression<Func<Publisher, bool>> predicate);
    }
}
