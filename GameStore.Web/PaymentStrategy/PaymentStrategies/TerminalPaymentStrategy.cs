using System.Web.Mvc;
using GameStore.Domain.Core.DomainModels;
using GameStore.Web.PaymentStrategy.Interfaces;

namespace GameStore.Web.PaymentStrategy.PaymentStrategies
{
    public class TerminalPaymentStrategy : IPaymentStrategy
    {
        public ActionResult Pay()
        {
            var vr = new ViewResult
            {
                ViewName = "IBoxTerminalPayForm",
                MasterName = "_Layout"
            };

            return vr;
        }
    }
}
