using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forwarding.MvcApp.Controllers.Accounting
{
    public class CourierAndLastMileController : BaseController
    {
        #region MasterData

        [HttpGet]
        public PartialViewResult ViewOrders(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/CourierAndLastMile/Orders/_Orders.cshtml");
        }
        
        [HttpGet]
        public PartialViewResult ViewDomestic_AWB(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/CourierAndLastMile/Orders/_Domestic_AWB.cshtml");
        }

        [HttpGet]
        public PartialViewResult ViewDispatcher(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/CourierAndLastMile/Orders/_Dispatcher.cshtml");
        }
        [HttpGet]
        public PartialViewResult ViewRunnerTransactions(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/CourierAndLastMile/Orders/_RunnerTransactions.cshtml");
        }
        
        [HttpGet]
        public PartialViewResult ViewTransactions(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/CourierAndLastMile/Orders/_Transactions.cshtml");
        }
        [HttpGet]
        public PartialViewResult ViewShipmentDelivery(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/CourierAndLastMile/Operations/_ShipmentDelivery.cshtml");
        }

        #endregion MasterData

        #region Reports

        #endregion Reports
    }
}
