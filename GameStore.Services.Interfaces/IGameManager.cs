using GameStore.Domain.Core.DomainModels;
using System.Collections.Generic;
using System.Net;

namespace GameStore.Services.Interfaces
{
    public interface IGameManager
    {
        HttpStatusCode CreateNewGame(Game game);

        HttpStatusCode EditGame(Game game);

        HttpStatusCode DeleteGame(Game game);

        HttpStatusCode DeleteGameById(int id);

        Game GetGameByKey(string gameKey);

        IEnumerable<Game> GetAllGames();

        IEnumerable<Game> GetGamesByGenre(string genre);

        IEnumerable<Game> GetGamesByPlatformType(string platform);

        IEnumerable<Game> GetFilteredResult(IFilterModel filter);
    }
}
