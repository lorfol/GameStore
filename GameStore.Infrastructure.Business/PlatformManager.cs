using GameStore.Domain.Core.DomainModels;
using GameStore.Domain.Interfaces;
using GameStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;

namespace GameStore.Infrastructure.Business
{
    public class PlatformManager : IPlatformManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlatformManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public HttpStatusCode CreateNewPlatform(Platform platform)
        {
            _unitOfWork.Platforms.Create(platform);
            _unitOfWork.Save();

            return HttpStatusCode.OK;
        }

        public HttpStatusCode EditPlatform(int platformId, Platform platform)
        {
            _unitOfWork.Platforms.Update(platform);
            _unitOfWork.Save();

            return HttpStatusCode.OK;
        }

        public HttpStatusCode DeletePlatform(Platform platform)
        {
            _unitOfWork.Platforms.Delete(platform);
            _unitOfWork.Save();

            return HttpStatusCode.OK;
        }

        public HttpStatusCode DeletePlatformById(int platformId)
        {
            _unitOfWork.Platforms.DeleteById(platformId);
            _unitOfWork.Save();

            return HttpStatusCode.OK;
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            var platformsFromDb = _unitOfWork.Platforms.GetAll();

            return platformsFromDb;
        }

        public IEnumerable<Platform> Find(Expression<Func<Platform, bool>> predicate)
        {
            return _unitOfWork.Platforms.Find(predicate);
        }
    }
}
