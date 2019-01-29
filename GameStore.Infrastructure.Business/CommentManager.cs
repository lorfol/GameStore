using GameStore.Domain.Core.DomainModels;
using GameStore.Domain.Interfaces;
using GameStore.Services.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace GameStore.Infrastructure.Business
{
    public class CommentManager : ICommentManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentManager(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public HttpStatusCode AddCommentToGame(Comment comment, string gameKey)
        {
            if (comment == null)
            {
                throw new ValidationException("Comment entity was null");
            }

            if (string.IsNullOrEmpty(gameKey))
            {
                throw new ValidationException("Game Key cannot be null/empty");
            }

            this._unitOfWork.Comments.Create(comment);
            this._unitOfWork.Games.Find(x => x.Key == gameKey).FirstOrDefault()?.Comments.Add(comment);
            this._unitOfWork.Save();

            return HttpStatusCode.Created;
        }

        public HttpStatusCode AddReplyToComment(Comment comment, int parentCommentId, string gameKey)
        {
            if (comment == null)
            {
                throw new ValidationException("Comment entity was null");
            }

            if (parentCommentId <= 0)
            {
                throw new ValidationException("Parent comment Id cannot be less than 1");
            }

            comment.ParentComment = _unitOfWork.Comments.GetById(parentCommentId);
            comment.Game = _unitOfWork.Games.Find(g=>g.Key==gameKey).SingleOrDefault();
            comment.GameId = comment.Game.Id;

            this._unitOfWork.Comments.Create(comment);

            _unitOfWork.Save();

            return HttpStatusCode.OK;
        }

        public HttpStatusCode EditComment(Comment comment)
        {
            _unitOfWork.Comments.Update(comment);

            _unitOfWork.Save();

            return HttpStatusCode.OK;
        }

        public HttpStatusCode DeleteComment(Comment comment)
        {
            _unitOfWork.Comments.Delete(comment);

            _unitOfWork.Save();

            return HttpStatusCode.OK;
        }

        public HttpStatusCode DeleteCommentById(int commentId)
        {
            _unitOfWork.Comments.DeleteById(commentId);

            _unitOfWork.Save();

            return HttpStatusCode.OK;
        }


        public IEnumerable<Comment> GetAllCommentsByGameKey(string gameKey)
        {
            if (string.IsNullOrEmpty(gameKey))
            {
                throw new ValidationException("Game Key cannot be null/empty");
            }
            
            return this._unitOfWork.Games.Find(g => g.Key == gameKey).FirstOrDefault()?.Comments;
        }

        public IEnumerable<Comment> FindByParentId(int id)
        {
            return this._unitOfWork.Comments.Find(g => g.CommentId == id).ToList();
        }

        public Comment GetById(int id)
        {
            return _unitOfWork.Comments.GetById(id);
        }
    }
}
