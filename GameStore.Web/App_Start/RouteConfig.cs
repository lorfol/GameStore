using System.Web.Mvc;
using System.Web.Routing;
using Ninject;

namespace GameStore.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes) // todo : route optimization
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            


            routes.MapRoute(
               name: "Create",
               url: "games/new",
               defaults: new { controller = "Game", action = "CreateGame" }
            );

            routes.MapRoute(
                name: "Update",
                url: "games/update/{gameKey}",
                defaults: new { controller = "Game", action = "EditGame" }
            );

            routes.MapRoute(
                name: "GameDetails",
                url: "game/{key}",
                defaults: new { controller = "Game", action = "GetGameDetails" }
            );

            routes.MapRoute(
                name: "Remove",
                url: "games/remove/{gameId}",
                defaults: new { controller = "Game", action = "DeleteGame" }
            );

            routes.MapRoute(
                name: "NewCommentToGame",
                url: "game/{gameKey}/newComment",
                defaults: new { controller = "Comment", action = "LeaveCommentToGame" }
            );

            routes.MapRoute(
                name: "NewReplyToComment",
                url: "game/{parentCommentId}/newReply",
                defaults: new { controller = "Comment", action = "ReplyToComment" }
            );

            routes.MapRoute(
                name: "GetAllComments",
                url: "game/{gameKey}/comments",
                defaults: new { controller = "Comment", action = "GetAllCommentsByGameKey" }
            );

            routes.MapRoute(
                name: "DeleteComment",
                url: "comment/{commentId}/delete",
                defaults: new { controller = "Comment", action = "DeleteComment" }
            );

            routes.MapRoute(
                name: "Ban",
                url: "comment/{commentId}/ban",
                defaults: new { controller = "Comment", action = "Ban" }
            );

            routes.MapRoute(
                name: "Download",
                url: "game/{gameKey}/download",
                defaults: new { controller = "Game", action = "DownloadGame" }

            );

            routes.MapRoute(
                name: "PublisherCreate",
                url: "publisher/new",
                defaults: new { controller = "Publisher", action = "CreatePublisher" }
            );

            routes.MapRoute(
                name: "PublisherDetails",
                url: "publisher/{CompanyName}",
                defaults: new { controller = "Publisher", action = "PublisherDetails" }
            );

            routes.MapRoute(
                name: "PublisherDelete",
                url: "publisher/{publisherId}/remove",
                defaults: new { controller = "Publisher", action = "DeletePublisher" }
            );

            routes.MapRoute(
                "Basket",
                "basket",
                new { controller = "Basket", action = "GetBasket" }
            );

            routes.MapRoute(
                "AddToBasket",
                "game/{gameKey}/buy",
                new { controller = "Basket", action = "AddToBasket" }
            );

            routes.MapRoute(
                "RemoveFromBasket",
                "basket/{gameKey}/remove",
                new { controller = "Basket", action = "RemoveFromBasket" }
            );

            routes.MapRoute(
                "GetCountOfGames",
                "games/count",
                new { controller = "Game", action = "GetCountOfGames" }
            );

            routes.MapRoute(
                "ConfirmOrder",
                "order/confirm",
                new { controller = "Order", action = "ConfirmOrder" }
            );

            routes.MapRoute(
                "OrderDetails",
                "order/details",
                new { controller = "Order", action = "OrderDetails" }
            );

            routes.MapRoute(
                "Pay",
                "payment/new",
                new { controller = "Payment", action = "CreatePayment" }
            );

            routes.MapRoute(
                "DummyPaymentResponse",
                "payment/pay",
                new { controller = "Payment", action = "DummyPaymentResponse" }
            );

            routes.MapRoute(
                name: "GetPdf",
                url: "payment/generatepdf",
                defaults: new { controller = "Payment", action = "GenerateInvoiceFile" }
            );

            routes.MapRoute(
                name: "Games",
                url: "games",
                defaults: new { controller = "Game", action = "GetAllGames" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Game", action = "GetAllGames", id = UrlParameter.Optional }
            );

        }
    }
}