using Forwarding.MvcApp.Models.Operations.Operations.Customized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Forwarding.MvcApp.Controllers.Reports.API_Statistics
{
    public class CustomersReportController : ApiController
    {
        [HttpGet, HttpPost]
        public object[] LoadData(string pWhereClause, Int32 pRange)
        {
            string pMessageReturned = "";
            Exception checkException = null;
            int _RowCount = 0;
            CvwOperationsStatistics objCvwOperationsStatistics = new CvwOperationsStatistics();
            checkException = objCvwOperationsStatistics.GetListPaging(99999, 1, pWhereClause, "ID", out _RowCount);
            if (checkException != null)
                pMessageReturned = checkException.Message;
            var pReportList = objCvwOperationsStatistics.lstCVarvwOperationsStatistics
                .GroupBy(g => new { g.ClientName })
                .Select(s => new
                {
                    ClientName = s.First().ClientName
                    ,
                    OperationsCount = s.Count()
                    ,
                    PayablesWithoutVAT = s.Sum(i => i.PayablesWithoutVAT)
                    ,
                    ReceivablesWithoutVAT = s.Sum(i => i.ReceivablesWithoutVAT)
                    ,
                    Profit = s.Sum(i => i.ReceivablesWithoutVAT) - s.Sum(i => i.PayablesWithoutVAT)
                })
                .OrderByDescending(o => o.Profit)
                .ToList()
                .Take(pRange);
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[]
            {
                pMessageReturned
                , serializer.Serialize(pReportList.Take(pRange)) //pData[1]
            };
        }
    }
}
