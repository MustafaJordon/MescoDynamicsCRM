using Forwarding.MvcApp.Models.Administration.Security.Generated;
using Forwarding.MvcApp.Models.Administration.Settings.Generated;
using Forwarding.MvcApp.Models.MasterData.Invoicing.Generated;
using Forwarding.MvcApp.Models.MasterData.Partners.Generated;
using Forwarding.MvcApp.Models.NoAccess.Generated;
using Forwarding.MvcApp.Models.Quotations.Quotations.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using WebMatrix.WebData;

namespace Forwarding.MvcApp.Controllers.Reports.API_Statistics
{
    public class QuotationsStatisticsController : ApiController
    {
        [HttpGet, HttpPost]//pID : is the InvoiceID
        public object[] GetStatisticsFilter()
        {
            CvwUsers objCvwUsers = new CvwUsers();
            objCvwUsers.GetList(" WHERE IsNull(CustomerID , 0) = 0 AND 1=1 ORDER BY Name ");

            CvwBranches objCvwBranches = new CvwBranches();
            objCvwBranches.GetList(" WHERE 1=1 ORDER BY Name ");

            CCustomers objCCustomers = new CCustomers();
            //objCCustomers.GetList(" WHERE 1=1 ORDER BY Name ");

            CAgents objCAgents = new CAgents();
            objCAgents.GetList(" WHERE 1=1 ORDER BY Name ");

            CvwCurrencies objCvwCurrencies = new CvwCurrencies();
            objCvwCurrencies.GetList(" WHERE 1=1 ORDER BY Code ");

            CNoAccessQuoteAndOperStages objCNoAccessQuoteAndOperStages = new CNoAccessQuoteAndOperStages();
            objCNoAccessQuoteAndOperStages.GetList(" WHERE IsQuotationStage = 1 AND IsInActive = 0 ORDER BY ViewOrder ");

            var pUsersList = objCvwUsers.lstCVarvwUsers.Select(s => new {ID = s.ID, IsSalesman = s.IsSalesman, Name = s.Name }).ToList();
            var pBranchesList = objCvwBranches.lstCVarvwBranches.Select(s => new {ID = s.ID, Code = s.Code, Name = s.Name }).ToList();
            var pCustomersList = objCCustomers.lstCVarCustomers.Select(s => new {ID = s.ID, Code = s.Code, Name = s.Name }).ToList();
            var pCurrenciesList = objCvwCurrencies.lstCVarvwCurrencies.Select(s => new {ID = s.ID, Code = s.Code, Name = s.Name }).ToList();
            var pNoAccessQuoteAndOperStagesList = objCNoAccessQuoteAndOperStages.lstCVarNoAccessQuoteAndOperStages.Select(s => new {ID = s.ID, Code = s.Code, Name = s.Name }).ToList();
            var pAgentsList = objCAgents.lstCVarAgents.Select(s => new {ID = s.ID, Code = s.Code, Name = s.Name }).ToList();

            return new object[] { 
                new JavaScriptSerializer().Serialize(pUsersList)//data[0]
                , new JavaScriptSerializer().Serialize(pBranchesList)//data[1]
                , new JavaScriptSerializer().Serialize(pCustomersList)//data[2]
                , new JavaScriptSerializer().Serialize(pCurrenciesList)//data[3]
                , new JavaScriptSerializer().Serialize(pNoAccessQuoteAndOperStagesList)//data[4]
                , new JavaScriptSerializer().Serialize(pAgentsList)//data[5]
            };
        }

        [HttpGet, HttpPost]
        public object[] LoadData(string pWhereClause)
        {
            bool pRecordsExist = false;
            Exception checkException = null;
            int _RowCount = 0;
            //var constOperationsFormID = 29;//OperationsManagement
            var constQuotationsFormID = 28; //QuotationsManagement

            CvwUserForms objCvwUserForms = new CvwUserForms();
            objCvwUserForms.GetList("WHERE UserID=" + WebSecurity.CurrentUserId.ToString() + " AND FormID=" + constQuotationsFormID.ToString());

            bool _IsHideOthersRecords = objCvwUserForms.lstCVarvwUserForms[0].HideOthersRecords;
            if (_IsHideOthersRecords)
                //pWhereClause += " AND (CreatorUserID=" + WebSecurity.CurrentUserId + " OR SalesmanID=" + WebSecurity.CurrentUserId + ")";
                pWhereClause += " AND (CreatorUserID=" + WebSecurity.CurrentUserId + " OR SalesmanID=" + WebSecurity.CurrentUserId + ")";

            CvwQuotationRoute objCvwQuotationRoute = new CvwQuotationRoute();
            checkException = objCvwQuotationRoute.GetListPaging(5000 ,1, pWhereClause, "CodeSerial", out _RowCount);

            if (objCvwQuotationRoute.lstCVarvwQuotationRoute.Count > 0 && checkException == null)
                pRecordsExist = true;

            int pCreatedCount = objCvwQuotationRoute.lstCVarvwQuotationRoute.Where(w => w.QuotationStageID == 1).Count();
            int pAcceptedCount = objCvwQuotationRoute.lstCVarvwQuotationRoute.Where(w => w.QuotationStageID == 4).Count();
            int pDeclinedCount = objCvwQuotationRoute.lstCVarvwQuotationRoute.Where(w => w.QuotationStageID == 5).Count();

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] 
            {
                pRecordsExist //data[0]
                , serializer.Serialize(objCvwQuotationRoute.lstCVarvwQuotationRoute) //data[1]
                , pCreatedCount //data[2]
                , pAcceptedCount //data[3]
                , pDeclinedCount //data[4]
            };
        }
    }
}
