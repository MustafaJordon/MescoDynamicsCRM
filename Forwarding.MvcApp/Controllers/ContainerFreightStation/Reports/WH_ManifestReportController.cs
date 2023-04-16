using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using Forwarding.MvcApp.Models.Operations.Operations.Generated;
using Forwarding.MvcApp.Entities.Operations;

namespace Forwarding.MvcApp.Controllers.ContainerFreightStation.Reports
{
    public class WH_ManifestReportController : ApiController
    {
        [HttpGet, HttpPost]
        public object[] LoadData()
        {
            Exception checkException = null;
            Int32 _RowCount = 0;

            CvwOperationsWithMinimalColumns objCvwOperations = new CvwOperationsWithMinimalColumns();

            // nour 09052022
            //objCvwOperations.GetListPaging(99999, 1, "WHERE DirectionType<>1 AND TransportType<>2 AND ShipmentType<>2 and BLType<>2", "ID DESC", out _RowCount);
            objCvwOperations.GetListPaging(99999, 1, "WHERE DirectionType = 1 AND TransportType<>2 AND ShipmentType<>2 and BLType<>2", "ID DESC", out _RowCount);

            var pOperationList = objCvwOperations.lstCVarvwOperationsWithMinimalColumns
                   //.GroupBy(g => g.ReceiveID)
                   .Select(s => new
                   {
                       ID = s.ID
                       ,
                       Code = s.Code
                   }).ToList();

            var serializer = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue };
            return new object[] {
                _RowCount
                , serializer.Serialize(pOperationList) //data[1]
            };
        }


        [HttpGet, HttpPost]
        public object[] SaveRoadNumber(string pRoadNumber, string pWhereClause)
        {
            Exception checkException = null;
            bool _result = false;

            CRoutings ObjCRoutings = new CRoutings();

            checkException = ObjCRoutings.UpdateList(" RoadNumber ='" + pRoadNumber + "' " + pWhereClause);
            if (checkException == null)
            {
                _result = true;

            }

            return new object[] {
                _result
            };
        }

        [HttpGet, HttpPost]
        public object[] GetOperationRoadNumber(string pWhereClause)
        {
            Exception checkException = null;
            Int32 _RowCount = 0;

            CRoutings ObjCRoutings = new CRoutings();

            checkException = ObjCRoutings.GetListPaging(9999,1,pWhereClause, "RoadNumber", out _RowCount);
           // _RowCount = ObjCRoutings.lstCVarRoutings.Count;

            return new object[] {
                _RowCount
                , _RowCount > 0 ? (ObjCRoutings.lstCVarRoutings[0].RoadNumber != "0"? ObjCRoutings.lstCVarRoutings[0].RoadNumber :""  ) : "" //data[1]
            };
        }

        [HttpGet, HttpPost]
        public object[] PrintManifest(string pOperationID)
        {
            Exception checkException = null;
            bool RecordsExist = true;

            int _RowCount = 0;

            CvwOperations objCvwOperations = new CvwOperations();
            objCvwOperations.GetListPaging(999999, 1, " WHERE ID = " + pOperationID.ToString(), "ID", out _RowCount);

            CvwRoutings objCvwRoutings = new CvwRoutings();
            objCvwRoutings.GetListPaging(9999, 1, " WHERE OperationID = " + pOperationID.ToString() + " AND RoutingTypeID = 30", "ID", out _RowCount);

            CvwOperations objCvwHouses = new CvwOperations();
            objCvwHouses.GetListPaging(10000, 1, " WHERE MasterOperationID = " + pOperationID.ToString(), "ID", out _RowCount);

            CvwOperationContainersAndPackages objCvwOperationContainersAndPackages = new CvwOperationContainersAndPackages();
            objCvwOperationContainersAndPackages.GetListPaging(99999, 1, " WHERE OperationID = " + pOperationID.ToString(), "ID", out _RowCount);


            return new object[] { RecordsExist
                    , new JavaScriptSerializer().Serialize(objCvwRoutings.lstCVarvwRoutings)
                    , new JavaScriptSerializer().Serialize(objCvwOperations.lstCVarvwOperations[0])
                    , new JavaScriptSerializer().Serialize(objCvwHouses.lstCVarvwOperations)
                    , new JavaScriptSerializer().Serialize(objCvwOperationContainersAndPackages.lstCVarvwOperationContainersAndPackages)

                };
        }
    }
}
