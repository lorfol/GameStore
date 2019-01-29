using System.Web.Mvc;
using GameStore.Domain.Core.DomainModels;
using GameStore.Services.Interfaces;
using GameStore.Web.Controllers;
using Moq;
using NUnit.Framework;

namespace GameStore.Tests.Controllers
{
    [TestFixture]
    public class CommentControllerTest
    {
        private Mock<ICommentManager> _commentManagerMock;
        private Mock<IGameManager> _gameManagerMock;

        [SetUp]
        public void SetupContext()
        {
            _commentManagerMock = new Mock<ICommentManager>();
            _gameManagerMock = new Mock<IGameManager>();
        }

        [Test]
        public void LeaveCommentToGame_ShouldReturnRedirectResult()
        {
            var dummyComment = GetDummyComment();
            var gameKey = "key";
            var testedService = GetTestEntity();

            var res = testedService.LeaveCommentToGame(dummyComment, gameKey) as RedirectToRouteResult;
            
            Assert.AreEqual(true, res.RouteValues.ContainsValue("GetAllCommentsByGameKey"));
        }

        [Test]
        public void CommentController_LeaveCommentToGame_CallsAddActionFromManager()
        {
            var dummyComment = GetDummyComment();
            var gameKey = "key";

            var testedService = GetTestEntity();
            var res = testedService.LeaveCommentToGame(dummyComment, gameKey) as RedirectToRouteResult;

            Assert.AreEqual(true, res.RouteValues.ContainsValue("GetAllCommentsByGameKey"));
        }

        [Test]
        public void CommentController_GetAllCommentsByGameKey_ShouldReturnCorrectViewResult()
        {
            var testedService = GetTestEntity();
            var gameKey = "SAMPLE";
            var res = testedService.GetAllCommentsByGameKey(gameKey) as ViewResult;

            Assert.AreEqual("CommentsForGame", res.ViewName);
        }

        private CommentController GetTestEntity()
        {
            return new CommentController(_commentManagerMock.Object, _gameManagerMock.Object);
        }

        private Comment GetDummyComment()
        {
            return new Comment()
            {
                Id = 1,
                CommentId = 1,
                Game = new Game(),
                Name = "Name",
                Body = "commentary",
                GameId = 1
            };
        }
    }
}
