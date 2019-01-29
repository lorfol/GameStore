using GameStore.Domain.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Services.Interfaces
{
    public interface IPlatformManager
    {
        HttpStatusCode CreateNewPlatform(Platform platform);

        HttpStatusCode EditPlatform(int platformId, Platform platform);

        HttpStatusCode DeletePlatform(Platform platform);

        HttpStatusCode DeletePlatformById(int platformId);

        IEnumerable<Platform> GetAllPlatforms();

        IEnumerable<Platform> Find(Expression<Func<Platform, bool>> predicate);
    }
}
