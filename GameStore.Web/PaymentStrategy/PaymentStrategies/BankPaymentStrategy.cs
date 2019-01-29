using System.Web.Mvc;
using GameStore.Web.PaymentStrategy.Interfaces;
using Rotativa;

namespace GameStore.Web.PaymentStrategy.PaymentStrategies
{
    public class BankPaymentStrategy : IPaymentStrategy
    {
        public ActionResult Pay()
        {
            var report = new ActionAsPdf("GenerateInvoiceFile");

            return report;
        }
    }
} 
