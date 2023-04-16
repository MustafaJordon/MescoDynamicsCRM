using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Forwarding.MvcApp.Controllers.MasterData
{
    //[Authorize]
    public class FAController : BaseController
    {
  
        #region BasicData
        [HttpGet]
        //public ActionResult ViewCountries()
        public PartialViewResult ViewFA_AssetsGroups(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/FA/MasterData/_FA_AssetsGroups.cshtml");
        }
        [HttpGet]
        public PartialViewResult ViewFA_Assets(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/FA/MasterData/_FA_Assets.cshtml");
        }
        [HttpGet]
        public PartialViewResult ViewFA_DestructionsStopsPeriod(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/FA/MasterData/_FA_DestructionsStopsPeriod.cshtml");
        }

        [HttpGet]
        public PartialViewResult ViewFA_AssetsReports(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/FA/FA_Reports/_FA_AssetsReports.cshtml");
        }
        [HttpGet]
        public PartialViewResult ViewFA_TransactionsReports(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/FA/FA_Reports/_FA_TransactionsReports.cshtml");
        }


        [HttpGet]
        public PartialViewResult ViewFA_Addition(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/FA/FA_Transactions/_FA_Addition.cshtml");
        }

        [HttpGet]
        public PartialViewResult ViewFA_Exclusion(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/FA/FA_Transactions/_FA_Exclusion.cshtml");
        }

        [HttpGet]
        public PartialViewResult ViewFA_Depreciation(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/FA/FA_Transactions/_FA_Depreciations.cshtml");
        }
        [HttpGet]
        public PartialViewResult ViewFA_DepreciationsByAssets(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/FA/FA_Transactions/_FA_DepreciationsByAssets.cshtml");
        }
        [HttpGet]
        public PartialViewResult ViewFA_StopDepreciations(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/FA/FA_Transactions/_FA_StopDepreciations.cshtml");
        }
        [HttpGet]
        public PartialViewResult ViewFA_AssetsInventory(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/FA/FA_Transactions/_FA_AssetsInventory.cshtml");
        }
        
        [HttpGet]
        public PartialViewResult ViewFA_AssetsApproving(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/FA/FA_Approving/_FA_AssetsApproving.cshtml");
        }
        [HttpGet]
        public PartialViewResult ViewFA_AssetsUnApproving(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/FA/FA_Approving/_FA_AssetsUnApproving.cshtml");
        }

       

        #endregion BasicData


    }
}
