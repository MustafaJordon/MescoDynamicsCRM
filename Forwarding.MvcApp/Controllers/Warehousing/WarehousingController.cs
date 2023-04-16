using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forwarding.MvcApp.Controllers.Warehousing
{
    public class WarehousingController : BaseController
    {
        #region MasterData
        [HttpGet]
        public PartialViewResult ViewWarehouse(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/Warehousing/MasterData/_Warehouse.cshtml");
        }
        [HttpGet]
        public PartialViewResult ViewArea(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/Warehousing/MasterData/_Area.cshtml");
        }
        [HttpGet]
        public PartialViewResult ViewRow(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/Warehousing/MasterData/_Row.cshtml");
        }
        [HttpGet]
        public PartialViewResult ViewMainWarehouse(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/Warehousing/MasterData/_MainWarehouse.cshtml");
        }

        [HttpGet]
        public PartialViewResult ViewWarehouseNotes(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/Warehousing/MasterData/_WarehouseNotes.cshtml");
        }
        [HttpGet]
        public PartialViewResult ViewWarehousingChargeTypes(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/Warehousing/MasterData/_WarehousingChargeTypes.cshtml");
        }
        #endregion MasterData

        #region Transactions
        [HttpGet]
        public PartialViewResult ViewContract(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/Warehousing/Transactions/_Contract.cshtml");
        }
        [HttpGet]
        public PartialViewResult ViewReceive(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/Warehousing/Transactions/_Receive.cshtml");
        }
        [HttpGet]
        public PartialViewResult ViewTransferProducts(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/Warehousing/Transactions/_TransferProducts.cshtml");
        }
        [HttpGet]
        public PartialViewResult ViewPDI(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/Warehousing/Transactions/_PDI.cshtml");
        }
        [HttpGet]
        public PartialViewResult ViewAgingAdjustment(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/Warehousing/Transactions/_AgingAdjustment.cshtml");
        }
        [HttpGet]
        public PartialViewResult ViewPickup(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/Warehousing/Transactions/_Pickup.cshtml");
        }
        [HttpGet]
        public PartialViewResult ViewVehicleService(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/Warehousing/Transactions/_VehicleService.cshtml");
        }

        public PartialViewResult ViewWHInvoice(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/Warehousing/Transactions/_WHInvoice.cshtml");
        }
        #endregion Transactions

        #region Reports
        [HttpGet]
        public PartialViewResult ViewInventory(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/Warehousing/Reports/_Inventory.cshtml");
        }
        [HttpGet]
        public PartialViewResult ViewProductLog(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/Warehousing/Reports/_ProductLog.cshtml");
        }
        [HttpGet]
        public PartialViewResult ViewStockLedger(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/Warehousing/Reports/_StockLedger.cshtml");
        }
        [HttpGet]
        public PartialViewResult ViewVehicleReport(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/Warehousing/Reports/_VehicleReport.cshtml");
        }
        [HttpGet]
        public PartialViewResult ViewAreaLocationsChart(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/Warehousing/Reports/_AreaLocationsChart.cshtml");
        }
        
        #endregion Reports

    }
}
