using System.Web.Mvc;
using GameStore.Domain.Core.DomainModels;
using GameStore.Web.PaymentStrategy.Interfaces;

namespace GameStore.Web.PaymentStrategy.PaymentStrategies
{
    public class VisaPaymentStrategy : IPaymentStrategy
    {
        public ActionResult Pay()
        {
            var vr = new ViewResult
            {
                ViewName = "VisaPayForm",
                MasterName = "_Layout",
                ViewData = new ViewDataDictionary(new Order())
            };

            return vr;
        }
    }
}
