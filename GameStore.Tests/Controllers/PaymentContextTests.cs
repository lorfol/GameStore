using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using GameStore.Services.Interfaces;
using GameStore.Web.Controllers;
using GameStore.Web.PaymentStrategy;
using GameStore.Web.PaymentStrategy.Interfaces;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Rotativa;

namespace GameStore.Tests.Controllers
{
    [TestFixture]
    public class PaymentContextTests
    {
        [Test]
        public void CreatePayment()
        {
            PaymentContext pc = new PaymentContext();

            var bank = pc.CreatePayment(PaymentMethodsEnum.Bank);
            var visa = pc.CreatePayment(PaymentMethodsEnum.Visa) as ViewResult;
            var terminal = pc.CreatePayment(PaymentMethodsEnum.IBox) as ViewResult;

            Assert.IsInstanceOf<ActionAsPdf>(bank);
            Assert.AreEqual("VisaPayForm", visa.ViewName);
            Assert.AreEqual("IBoxTerminalPayForm", terminal.ViewName);
        }
    }
}
