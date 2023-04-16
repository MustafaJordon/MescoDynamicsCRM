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
    public class CustomsClearanceReportController : ApiController
    {
        [HttpGet, HttpPost]
        public object[] LoadData(string pWhereClause)
        {
            string pMessageReturned = "";
            Exception checkException = null;
            int _RowCount = 0;
            CvwCCRoutings objCvwCCRoutings = new CvwCCRoutings();
            checkException = objCvwCCRoutings.GetListPaging(999999, 1, pWhereClause, "ID", out _RowCount);
            if (checkException != null)
                pMessageReturned = checkException.Message;
            //var pReportList = objCvwCCRoutings.lstCVarvwCCRoutings
            //    .GroupBy(g => new { g.ClientName })
            //    .Select(s => new
            //    {
            //        ClientName = s.First().ClientName
            //        ,
            //        OperationsCount = s.Count()
            //        ,
            //        PayablesWithoutVAT = s.Sum(i => i.PayablesWithoutVAT)
            //        ,
            //        ReceivablesWithoutVAT = s.Sum(i => i.ReceivablesWithoutVAT)
            //        ,
            //        Profit = s.Sum(i => i.ReceivablesWithoutVAT) - s.Sum(i => i.PayablesWithoutVAT)
            //    })
            //    .OrderByDescending(o => o.Profit)
            //    .ToList()
            //    .Take(pRange);
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[]
            {
                pMessageReturned
                , serializer.Serialize(objCvwCCRoutings.lstCVarvwCCRoutings) //pData[1]
            };
        }
    }
}