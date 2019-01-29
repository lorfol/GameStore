using GameStore.Domain.Core.DomainModels;
using GameStore.Domain.Interfaces;
using GameStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Infrastructure.Business
{
    public class GenreManager : IGenreManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenreManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public HttpStatusCode CreateNewGenre(Genre genre)
        {
            _unitOfWork.Genres.Create(genre);
            _unitOfWork.Save();

            return HttpStatusCode.OK;
        }

        public HttpStatusCode EditGenre(int genreId, Genre genre)
        {
            _unitOfWork.Genres.Update(genre);
            _unitOfWork.Save();

            return HttpStatusCode.OK;
        }

        public HttpStatusCode DeleteGenre(Genre genre)
        {
            _unitOfWork.Genres.Delete(genre);
            _unitOfWork.Save();

            return HttpStatusCode.OK;
        }

        public HttpStatusCode DeleteGenreById(int genreId)
        {
            _unitOfWork.Genres.DeleteById(genreId);
            _unitOfWork.Save();

            return HttpStatusCode.OK;
        }

        public IEnumerable<Genre> GetAllGenres()
        {
            var genresFromDb = _unitOfWork.Genres.GetAll();

            return genresFromDb;
        }

        public IEnumerable<Genre> Find(Expression<Func<Genre, bool>> predicate)
        {
            return _unitOfWork.Genres.Find(predicate);
        }
    }
}
