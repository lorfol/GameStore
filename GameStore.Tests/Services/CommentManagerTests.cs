using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using GameStore.Domain.Core.DomainModels;
using GameStore.Domain.Interfaces;
using GameStore.Infrastructure.Business;
using GameStore.Services.Interfaces;
using Moq;
using NUnit.Framework;
using WebGrease.Css.Extensions;

namespace GameStore.Tests.Services
{
    [TestFixture]
    public class CommentManagerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;

        [SetUp]
        public void TestSetup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Test]
        public void CommentManager_AddCommentToGame_CallsCreateAndGetByIdActionFromUnitOfWork()
        {
            _unitOfWorkMock.Setup(x => x.Comments.Create(It.IsAny<Comment>()));
            _unitOfWorkMock.Setup(x => x.Games.GetById(It.IsAny<int>())).Returns(new Game());

            var testEntity = GetTestEntity();
            var comment = GetDummyComment();
            testEntity.AddCommentToGame(comment, "key");

            _unitOfWorkMock.Verify(x => x.Comments.Create(comment), Times.Once);
        }

        [Test]
        public void AddCommentToGame_ThrowValidationExceptionWhenCommentIsNull()
        {
            var testedService = GetTestEntity();

            Assert.Throws<ValidationException>(() => testedService.AddCommentToGame(null, null));
        }

        [Test]
        public void AddCommentToGame_ThrowValidationExceptionWhenIdIsLessThenZeroOrEqual()
        {
            var testedService = GetTestEntity();

            Assert.Throws<ValidationException>(() => testedService.AddCommentToGame(new Comment(), ""));
        }

        [Test]
        public void CommentManager_AddReplyToComment_CallsCreateAndGetByIdActionFromUnitOfWork()
        {
            _unitOfWorkMock.Setup(x => x.Comments.Create(It.IsAny<Comment>()));
            _unitOfWorkMock.Setup(x => x.Comments.GetById(It.IsAny<int>())).Returns(new Comment());

            var testEntity = GetTestEntity();
            var comment = GetDummyComment();
            testEntity.AddReplyToComment(comment, 1, "key");

            _unitOfWorkMock.Verify(x => x.Comments.Create(comment), Times.Once);

        }

        [Test]
        public void AddReplyToComment_ThrowValidationExceptionWhenCommentIsNull()
        {
            var testedService = GetTestEntity();

            Assert.Throws<ValidationException>(() => testedService.AddReplyToComment(null, 1, "key"));
        }

        [Test]
        public void AddReplyToComment_ThrowValidationException()
        {
            var testedService = GetTestEntity();

            Assert.Throws<ValidationException>(() => testedService.AddReplyToComment(new Comment(), -1, "key"));
        }

        [Test]
        public void CommentManager_GetAllCommentsByGameKey_ShouldReturnIEnumerableOfComments()
        {
            const string gameKey = "Key";

            _unitOfWorkMock.Setup(x => x.Games.Find(It.IsAny<Expression<Func<Game, bool>>>()))
                .Returns(
                    new List<Game>
                    {
                        new Game { Key = gameKey, Comments = new List<Comment>() }
                    });

            var testEntity = GetTestEntity();
            var result = testEntity.GetAllCommentsByGameKey(gameKey);
            
            Assert.AreEqual(true, result.All(x=>x.Game.Key == gameKey));
        }

        [Test]
        public void GetAllCommentsByGameKey_ThrowValidationException()
        {
            var testedService = GetTestEntity();

            Assert.Throws<ValidationException>(() => testedService.GetAllCommentsByGameKey(null));
        }

        [Test]
        public void FindByParentId()
        {
            _unitOfWorkMock.Setup(x => x.Comments.Find(It.IsAny<Expression<Func<Comment, bool>>>()))
                .Returns(
                    new List<Comment>
                    {
                        new Comment { CommentId = 1 }
                    });

            var testEntity = GetTestEntity();
            var result = testEntity.FindByParentId(1);

            Assert.AreEqual(true, result.All(x=>x.CommentId == 1));

        }

        [Test]
        public void EditComment()
        {
            _unitOfWorkMock.Setup(x => x.Comments.Update(It.IsAny<Comment>()));

            var testEntity = GetTestEntity();
            var result = testEntity.EditComment(new Comment());
            
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [Test]
        public void DeleteComment()
        {
            _unitOfWorkMock.Setup(x => x.Comments.Delete(It.IsAny<Comment>()));

            var testEntity = GetTestEntity();
            var result = testEntity.DeleteComment(new Comment());

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [Test]
        public void DeleteCommentById()
        {
            _unitOfWorkMock.Setup(x => x.Comments.DeleteById(It.IsAny<int>()));

            var testEntity = GetTestEntity();
            var result = testEntity.DeleteCommentById(1);

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        private ICommentManager GetTestEntity()
        {
            return new CommentManager(_unitOfWorkMock.Object);
        }

        private Comment GetDummyComment()
        {
            return new Comment()
            {
                Id = 1,
                CommentId = 1,
                ParentComment = new Comment(),
                Game = new Game(),
                Name = "Name",
                Body = "commentary",
                GameId = 1
            };
        }
    }
}
