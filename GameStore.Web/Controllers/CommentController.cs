using System;
using System.Collections.Generic;
using GameStore.Domain.Core.DomainModels;
using GameStore.Services.Interfaces;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using GameStore.Web.ViewModels;
using WebGrease.Css.Extensions;

namespace GameStore.Web.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentManager _commentManager;
        private readonly IGameManager _gameManager;

        private const string duration = "duration";
        private const string messageForDeletedQuote = "Comment was deleted...";

        public CommentController(ICommentManager commentManager, IGameManager gameManager)
        {
            this._commentManager = commentManager;
            _gameManager = gameManager;
        }

        [HttpPost]
        public ActionResult LeaveCommentToGame(Comment comment, string gameKey)
        {
            _commentManager.AddCommentToGame(comment, gameKey);

            return RedirectToAction("GetAllCommentsByGameKey", "Comment", gameKey);
        }

        [HttpPost]
        public ActionResult ReplyToComment(int parentCommentId, Comment newComment, string gameKey, string quote)
        {
            if (quote != null && quote != String.Empty)
            {
                newComment.IsQuote = true;
            }

            _commentManager.AddReplyToComment(newComment, parentCommentId, gameKey);

            return RedirectToAction("GetAllCommentsByGameKey", "Comment", new { gameKey });
        }

        [HttpPost]
        public ActionResult DeleteComment(int commentId, string gameKey)
        {
            var comment = _commentManager.GetById(commentId);
            comment.IsDeleted = true;
            _commentManager.EditComment(comment);

            var childComments = _commentManager.FindByParentId(comment.Id).Where(c => c.IsQuote);
            childComments.ForEach(c => c.Quote = messageForDeletedQuote);
            childComments.ForEach(c=>_commentManager.EditComment(c));

            return RedirectToAction("GetAllCommentsByGameKey", "Comment", new { gameKey });
        }

        [HttpPost]
        public ActionResult Ban(object user, string gameKey, FormCollection form)
        {
            string banDuration = Request.Form[duration].ToString();

            return RedirectToAction("GetAllCommentsByGameKey", "Comment", new { gameKey });
        }

        [HttpGet]
        public ViewResult GetAllCommentsByGameKey(string gameKey)
        {
            ViewBag.GameKey = gameKey;

            var response = this._commentManager.GetAllCommentsByGameKey(gameKey).Where(x => x.CommentId == null);

            var result = Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentViewModel>>(response).ToList();

            return View("CommentsForGame", result);
        }
    }
}