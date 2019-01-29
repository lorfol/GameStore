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
    public interface IGenreManager
    {
        HttpStatusCode CreateNewGenre(Genre genre);

        HttpStatusCode EditGenre(int genreId, Genre genre);

        HttpStatusCode DeleteGenre(Genre genre);

        HttpStatusCode DeleteGenreById(int genreId);

        IEnumerable<Genre> GetAllGenres();

        IEnumerable<Genre> Find(Expression<Func<Genre, bool>> predicate);
    }
}
