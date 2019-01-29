using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.Core.DomainModels;
using GameStore.Domain.Interfaces;
using GameStore.Services.Interfaces;

namespace GameStore.Infrastructure.Business
{
    public class PublisherManager : IPublisherManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public PublisherManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public HttpStatusCode CreateNewPublisher(Publisher publisher)
        {
            _unitOfWork.Publishers.Create(publisher);
            _unitOfWork.Save();

            return HttpStatusCode.OK;
        }

        public HttpStatusCode EditPublisher(int publisherId, Publisher publisher)
        {
            _unitOfWork.Publishers.Create(publisher);
            _unitOfWork.Save();

            return HttpStatusCode.OK;
        }

        public HttpStatusCode DeletePublisher(Publisher publisher)
        {
            _unitOfWork.Publishers.Delete(publisher);
            _unitOfWork.Save();

            return HttpStatusCode.OK;
        }

        public HttpStatusCode DeletePublisherById(int id)
        {
            _unitOfWork.Publishers.DeleteById(id);
            _unitOfWork.Save();

            return HttpStatusCode.OK;
        }

        public IEnumerable<Publisher> GetAllPublishers()
        {
            return _unitOfWork.Publishers.GetAll();
        }

        public IEnumerable<Publisher> Find(Expression<Func<Publisher, bool>> predicate)
        {
            return _unitOfWork.Publishers.Find(predicate);
        }
    }
}
