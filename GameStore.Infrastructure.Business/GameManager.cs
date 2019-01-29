using GameStore.Domain.Core.DomainModels;
using GameStore.Domain.Interfaces;
using GameStore.Infrastructure.Business.Filtering;
using GameStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using GameStore.Infrastructure.Business.Filtering.PipeLine;

namespace GameStore.Infrastructure.Business
{
    public class GameManager : IGameManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public GameManager(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public HttpStatusCode CreateNewGame(Game game)
        {
            this._unitOfWork.Games.Create(game);
            this._unitOfWork.Save();
            return HttpStatusCode.Created;
        }

        public HttpStatusCode DeleteGame(Game game)
        {
            this._unitOfWork.Games.Delete(game);
            this._unitOfWork.Save();
            return HttpStatusCode.OK;
        }

        public HttpStatusCode DeleteGameById(int id)
        {
            this._unitOfWork.Games.DeleteById(id);
            this._unitOfWork.Save();
            return HttpStatusCode.OK;
        }

        public HttpStatusCode EditGame(Game game)
        {
            this._unitOfWork.Games.Update(game);
            this._unitOfWork.Save();
            return HttpStatusCode.OK;
        }

        public IEnumerable<Game> GetAllGames()
        {
            return this._unitOfWork.Games.GetAll(); // todo : remove try 
        }

        public Game GetGameByKey(string key)
        {
            return this._unitOfWork.Games.Find(g => g.Key == key).SingleOrDefault();
        }

        public IEnumerable<Game> GetGamesByGenre(string genre)
        {
            return this._unitOfWork.Games.Find(g => g.Genres.Select(f => f.Name).Contains(genre));
        }

        public IEnumerable<Game> GetGamesByPlatformType(string platform)
        {
            return this._unitOfWork.Games.Find(g => g.Platforms.Select(f => f.Type).Contains(platform));
        }

        public IEnumerable<Game> GetFilteredResult(IFilterModel filter)
        {
            //pipeline call
            PipeLineStarter pipeLine = new PipeLineStarter();

            var expression = pipeLine.Start(filter);

            var filtered = _unitOfWork.Games.Find(expression).ToList();

            Sorter sorter = new Sorter();
            var result = sorter.OrderByOptions(filter, filtered);

            return result;
        }

        //private Dictionary<string, object> PropertiesFromType(object atype)
        //{
        //    if (atype == null) return null;

        //    Type t = atype.GetType();
        //    PropertyInfo[] props = t.GetProperties();
        //    Dictionary<string, object> dict = new Dictionary<string, object>();
        //    foreach (PropertyInfo prp in props)
        //    {
        //        object value = prp.GetValue(atype, new object[] { });
        //        dict.Add(prp.Name, value);
        //    }

        //    return dict;
        //}
    }
}