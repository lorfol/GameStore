using System.Web.Mvc;
using GameStore.Domain.Core.DomainModels;

namespace GameStore.Web.PaymentStrategy.Interfaces
{
    public interface IPaymentContext
    {
        ActionResult CreatePayment(PaymentMethodsEnum methods);
    }
}
