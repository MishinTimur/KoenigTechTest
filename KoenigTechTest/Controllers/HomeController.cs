using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Services;

namespace KoenigTechTest.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> PaymentSystems()
        {
            using (var service = new ExchangersService())
            {
                var res = await service.GetPaymentSystemsAsync();
                return new JsonResult()
                {
                    Data = res,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        public async Task<JsonResult> Exchangers(Guid give, Guid get, double amount = 1, bool? calculateGive = null)
        {
            using (var service = new ExchangersService())
            {
                var res = await service.GetExchangersByPaymentsAsync(give, get, amount, calculateGive);
                return new JsonResult()
                {
                    Data = res,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
    }
}