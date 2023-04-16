﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Forwarding.MvcApp.Controllers.MasterData
{
    //[Authorize]
    public class WebSiteController : BaseController
    {
  
        #region BasicData
        [HttpGet]
        //public ActionResult ViewCountries()
        public PartialViewResult ViewWebSite_YourOperations(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/WebSite/_WebSite_YourOperations.cshtml");
        }

        [HttpGet]
        //public ActionResult ViewCountries()
        public PartialViewResult ViewWebSite_YourInvoices(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/WebSite/_WebSite_YourInvoices.cshtml");
        }
        [HttpGet]
        //public ActionResult ViewCountries()
        public PartialViewResult ViewWebSite_Form13States(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/WebSite/_WebSite_Form13States.cshtml");
        }
        [HttpGet]
        //public ActionResult ViewCountries()
        public PartialViewResult ViewWebSite_YourSubAccount(string pCutlureID)
        {
            SetLanguage(pCutlureID);
            return PartialView("~/Views/WebSite/_WebSite_YourSubAccount.cshtml");
        }

        #endregion BasicData


    }
}
