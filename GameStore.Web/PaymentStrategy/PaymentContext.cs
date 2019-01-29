using System.Collections.Generic;
using System.Web.Mvc;
using GameStore.Domain.Core.DomainModels;
using GameStore.Web.PaymentStrategy.Interfaces;
using GameStore.Web.PaymentStrategy.PaymentStrategies;

namespace GameStore.Web.PaymentStrategy
{
    public class PaymentContext : IPaymentContext
    {
        private readonly Dictionary<PaymentMethodsEnum, IPaymentStrategy> _strategies;

        public PaymentContext()
        {
            _strategies = new Dictionary<PaymentMethodsEnum, IPaymentStrategy>
            {
                { PaymentMethodsEnum.Bank, new BankPaymentStrategy() },
                { PaymentMethodsEnum.IBox, new TerminalPaymentStrategy() },
                { PaymentMethodsEnum.Visa, new VisaPaymentStrategy() }
            };
        }

        public ActionResult CreatePayment(PaymentMethodsEnum method)
        {
            return _strategies[method].Pay();
        }
    }
}