using System.Net;
using System.Web.Mvc;
using GameStore.Domain.Core.DomainModels;
using GameStore.Domain.Core.Enums;
using GameStore.Services.Interfaces;
using GameStore.Web.PaymentStrategy;
using GameStore.Web.PaymentStrategy.Interfaces;
using Rotativa;

namespace GameStore.Web.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentContext _paymentContext;
        private readonly IOrderManager _orderManager;

        public PaymentController(IPaymentContext paymentManager, IOrderManager orderManager)
        {
            _paymentContext = paymentManager;
            _orderManager = orderManager;
        }

        public ActionResult CreatePayment(PaymentMethodsEnum method, Order order)
        {
            order.Status = OrderStatusEnum.Paid;
            _orderManager.EditOrder(order);

            return _paymentContext.CreatePayment(method);
        }

        [HttpPost]
        public HttpStatusCode DummyPaymentResponse()
        {
            return HttpStatusCode.OK;
        }

        public ActionResult GenerateInvoiceFile()
        {
            return View("PdfInvoice");
        }
    }
}