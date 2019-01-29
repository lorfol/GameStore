using GameStore.Domain.Interfaces;
using GameStore.Infrastructure.Business;
using GameStore.Infrastructure.Data;
using GameStore.Services.Interfaces;
using GameStore.Web.PaymentStrategy;
using GameStore.Web.PaymentStrategy.Interfaces;
using Ninject.Modules;
using Ninject.Web.Common;

namespace GameStore.Web.IoC
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<ICommentManager>().To<CommentManager>();
            Bind<IGameManager>().To<GameManager>();
            Bind<IGenreManager>().To<GenreManager>();
            Bind<IPlatformManager>().To<PlatformManager>();
            Bind<IPublisherManager>().To<PublisherManager>();
            Bind<IOrderManager>().To<OrderManager>();
            Bind<IOrderDetailsManager>().To<OrderDetailsManager>();
            Bind<IPaymentContext>().To<PaymentContext>();

            Bind<GameStoreDbContext>().ToSelf().InRequestScope();
        }
    }
}