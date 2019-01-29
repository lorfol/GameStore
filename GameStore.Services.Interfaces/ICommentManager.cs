using GameStore.Domain.Core.DomainModels;
using System.Collections.Generic;
using System.Net;

namespace GameStore.Services.Interfaces
{
    public interface ICommentManager
    {
        HttpStatusCode AddCommentToGame(Comment comment, string gameKey);

        HttpStatusCode AddReplyToComment(Comment comment, int commentId, string gameKey);

        HttpStatusCode EditComment(Comment comment);

        HttpStatusCode DeleteComment(Comment comment);

        HttpStatusCode DeleteCommentById(int commentId);

        IEnumerable<Comment> GetAllCommentsByGameKey(string gameKey);

        IEnumerable<Comment> FindByParentId(int id);

        Comment GetById(int id);
    }
}