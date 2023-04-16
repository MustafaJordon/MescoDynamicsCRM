
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using Forwarding.MvcApp.Models.ContainerFreightStation.Transactions;
using Forwarding.MvcApp.Models.Warehousing.MasterData.Generated;
using Forwarding.MvcApp.Models.MasterData.Others.Generated;
using WebMatrix.WebData;
using Forwarding.MvcApp.Models.Operations.Operations.Generated;

namespace Forwarding.MvcApp.Controllers.ContainerFreightStation.Transactions
{
    public class WH_CFS_GateInController : ApiController
    {
        [HttpGet, HttpPost]
        public object[] WH_CFS_GateIn_LoadItem(string pOperationID, string pContainerID, string pHouseBillID)
        {
            bool _result = false;
            Exception checkException = null;
            Int32 _RowCount = 0;

            CvwWH_CFS_GateIn objCvwWH_CFS_GateIn = new CvwWH_CFS_GateIn();

            checkException = objCvwWH_CFS_GateIn.GetList("WHERE OperationID = " + pOperationID + " and ContainerID = " + pContainerID + " and HouseBillID = " + pHouseBillID);

            if (checkException == null)
            {
                _result = true;
                _RowCount = objCvwWH_CFS_GateIn.lstCVarvwWH_CFS_GateIn.Count;

            }

            CvwWH_HouseBillPackagesAllocation objCvwWH_HouseBillPackagesAllocation = new CvwWH_HouseBillPackagesAllocation();
            objCvwWH_HouseBillPackagesAllocation.GetList("WHERE HouseBillID = " + pHouseBillID);


            CWH_Warehouse objCWH_Warehouse = new CWH_Warehouse();
            CWH_Area objCWH_Area = new CWH_Area();
            CWH_Row objCWH_Row = new CWH_Row();
            CWH_RowLocation objCWH_RowLocation = new CWH_RowLocation();
            CWH_Notes objCWH_Notes = new CWH_Notes();
            CContainerTypes objCContainerTypes = new CContainerTypes();

            CvwWH_AvailableEmptyContainers objCvwWH_AvailableEmptyContainers = new CvwWH_AvailableEmptyContainers();

            // Getting Warehouses list                
            objCWH_Warehouse.GetList(" where 1=1 ");
            var pWarehousesList = objCWH_Warehouse.lstCVarWH_Warehouse
                .Select(s => new
                {
                    ID = s.ID
                    ,
                    Name = s.Name
                })
                .Distinct().OrderBy(o => o.Name).ToList();

            // Getting Areas list
            objCWH_Area.GetList(" where 1=2 ");
            var pAreasList = objCWH_Area.lstCVarWH_Area
                .Select(s => new
                {
                    ID = s.ID
                    ,
                    Name = s.Name
                })
                .Distinct().OrderBy(o => o.Name).ToList();

            // Getting Rows list
            objCWH_Row.GetList(" where 1=2 ");
            var pRowsList = objCWH_Row.lstCVarWH_Row
                .Select(s => new
                {
                    ID = s.ID
                    ,
                    Name = s.Name
                })
                .Distinct().OrderBy(o => o.Name).ToList();

            // Getting RowLocation list
            objCWH_RowLocation.GetList(" where 1=2 ");
            var pRowLocationsList = objCWH_RowLocation.lstCVarWH_RowLocation
                .Select(s => new
                {
                    ID = s.ID
                    ,
                    Code = s.Code
                })
                .Distinct().OrderBy(o => o.Code).ToList();

            // Getting Warehouse Notes list
            objCWH_Notes.GetList(" where IsForStoring = 1 ");
            var pWarehouseNotesList = objCWH_Notes.lstCVarWH_Notes
                .Select(s => new
                {
                    ID = s.ID
                    ,
                    Name = s.Name
                })
                .Distinct().OrderBy(o => o.Name).ToList();

            // Getting Container Types list
            objCContainerTypes.GetList(" where 1=1 ");

            var pContainerTypesList = objCContainerTypes.lstCVarContainerTypes
                .Select(ss => new
                {
                    ID = ss.ID,
                    Name = ss.Name
                })
                .Distinct().OrderBy(o => o.Name).ToList();

            // Getting Available Empty Containers list
            objCvwWH_AvailableEmptyContainers.GetList(" where 1=1 ");

            var serializer = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue };
            return new object[] {
                _result
                ,serializer.Serialize(_RowCount == 0 ? null :objCvwWH_CFS_GateIn.lstCVarvwWH_CFS_GateIn[0]) //data[1]
                ,serializer.Serialize(pWarehousesList) //2
                ,serializer.Serialize(pAreasList) //3
                ,serializer.Serialize(pRowsList) //4
                ,serializer.Serialize(pRowLocationsList) //5
                ,serializer.Serialize(pWarehouseNotesList) //6
                ,serializer.Serialize(pContainerTypesList) //7
                ,serializer.Serialize(objCvwWH_AvailableEmptyContainers.lstCVarvwWH_AvailableEmptyContainers) //8
                ,serializer.Serialize(objCvwWH_HouseBillPackagesAllocation.lstCVarvwWH_HouseBillPackagesAllocation)//9
            };
        }

        [HttpGet, HttpPost]
        public Object[] WH_CFS_GateIn_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned(Int32 pPageNumber, Int32 pPageSize, string pWhereClause, string pOrderBy)
        {
            bool _result = false;
            Exception checkException = null;
            int _RowCount = 0;

            CvwWH_CFS_GateIn objCvwWH_CFS_GateIn = new CvwWH_CFS_GateIn();

            pWhereClause = (pWhereClause == null ? "" : pWhereClause.Trim().ToUpper());

            checkException = objCvwWH_CFS_GateIn.GetListPaging(pPageSize, pPageNumber, pWhereClause, pOrderBy, out _RowCount);

            if (checkException == null)
            {
                _result = true;
            }
            return new Object[] {
                _result
                , _RowCount
                ,new JavaScriptSerializer().Serialize(objCvwWH_CFS_GateIn.lstCVarvwWH_CFS_GateIn) //0
                };
        }

        [HttpGet, HttpPost]
        public object[] WH_CFS_GateIn_Insert([FromBody] InsertGateIn InsertGateIn)
        {
            bool _result = false;
            int _RowCount = 0;
            CVarWH_Inventory objCVarWH_Inventory = new CVarWH_Inventory();

            objCVarWH_Inventory.EntryDate = InsertGateIn.pEntryDate.Year == 1 ? DateTime.Parse("1-1-1900") : InsertGateIn.pEntryDate;
            objCVarWH_Inventory.OperationID = InsertGateIn.pOperationID == "" ? 0 : long.Parse(InsertGateIn.pOperationID);
            objCVarWH_Inventory.ContainerID = InsertGateIn.pContainerID == "" ? 0 : long.Parse(InsertGateIn.pContainerID);
            objCVarWH_Inventory.HouseBillID = InsertGateIn.pHouseBillID == "" ? 0 : long.Parse(InsertGateIn.pHouseBillID);
            objCVarWH_Inventory.BookingPartyID = InsertGateIn.pBookingPartyID == "" ? 0 : int.Parse(InsertGateIn.pBookingPartyID);
            objCVarWH_Inventory.WarehouseID = InsertGateIn.pWarehouseID == "" ? 0 : int.Parse(InsertGateIn.pWarehouseID);
            objCVarWH_Inventory.AreaID = InsertGateIn.pAreaID == "" ? 0 : int.Parse(InsertGateIn.pAreaID);
            objCVarWH_Inventory.RowID = InsertGateIn.pRowID == "" ? 0 : int.Parse(InsertGateIn.pRowID);
            objCVarWH_Inventory.RowLocationID = InsertGateIn.pRowLocationID == "" ? 0 : int.Parse(InsertGateIn.pRowLocationID);
            objCVarWH_Inventory.WarehouseNoteID = InsertGateIn.pWarehouseNoteID == "" ? 0 : int.Parse(InsertGateIn.pWarehouseNoteID);
            objCVarWH_Inventory.EmptyContainerID = InsertGateIn.pEmptyContainerID == "" ? 0 : long.Parse(InsertGateIn.pEmptyContainerID);
            objCVarWH_Inventory.OtherRemarks = InsertGateIn.pOtherRemarks == null ? "0" : InsertGateIn.pOtherRemarks.Trim().ToString();
            objCVarWH_Inventory.StorageEndDate = DateTime.Parse("1-1-1900");
            objCVarWH_Inventory.KalmarOnCount = 0;
            objCVarWH_Inventory.KalmarOffCount = 0;

            // nour 09052022
            objCVarWH_Inventory.DriverNameIn = "";
            objCVarWH_Inventory.DriverNameOut = "";
            objCVarWH_Inventory.TruckNoIn = "";
            objCVarWH_Inventory.TruckNoOut = "";

            objCVarWH_Inventory.HasDamage = InsertGateIn.pHasDamage;
            objCVarWH_Inventory.DamageDescription = InsertGateIn.pDamageDescription == null ? "0" : InsertGateIn.pDamageDescription.Trim().ToString();

            objCVarWH_Inventory.CustomsSealNumber = "0";
            objCVarWH_Inventory.CustomsCertificateNumber = "0";
            objCVarWH_Inventory.CustomsFeesAmount = 0;
            objCVarWH_Inventory.CustomsFeesVAT = 0;
            objCVarWH_Inventory.CustomsFeesTotal = 0;

            objCVarWH_Inventory.AddedBy = InsertGateIn.pAddedBy == null ? WebSecurity.CurrentUserId : WebSecurity.CurrentUserId;
            objCVarWH_Inventory.AddedAt = DateTime.Now;
            objCVarWH_Inventory.UpdatedBy = InsertGateIn.pUpdatedBy == null ? WebSecurity.CurrentUserId : WebSecurity.CurrentUserId;
            objCVarWH_Inventory.UpdatedAt = DateTime.Now;


            CWH_Inventory objCWH_Inventory = new CWH_Inventory();
            objCWH_Inventory.lstCVarWH_Inventory.Add(objCVarWH_Inventory);

            Exception checkException = objCWH_Inventory.SaveMethod(objCWH_Inventory.lstCVarWH_Inventory);

            CvwWH_CFS_GateIn objCvwWH_CFS_GateIn = new CvwWH_CFS_GateIn();
            if (checkException == null)
            {
                _result = true;
                objCvwWH_CFS_GateIn.GetListPaging(InsertGateIn.pPageSize, InsertGateIn.pPageNumber, InsertGateIn.pWhereClause, InsertGateIn.pOrderBy, out _RowCount);


                // Insert House Bill Packages details
                CVarWH_HouseBillPackagesAllocation objCVarWH_HouseBillPackagesAllocation = new CVarWH_HouseBillPackagesAllocation();
                CWH_HouseBillPackagesAllocation objCWH_HouseBillPackagesAllocation = new CWH_HouseBillPackagesAllocation();

                JavaScriptSerializer jsonSerialiser = new JavaScriptSerializer();
                List<CVarWH_HouseBillPackagesAllocation> listofCVarWH_HouseBillPackagesAllocation = jsonSerialiser.Deserialize<List<CVarWH_HouseBillPackagesAllocation>>(InsertGateIn.pHouseBillPackages);
                objCWH_HouseBillPackagesAllocation.lstCVarWH_HouseBillPackagesAllocation = listofCVarWH_HouseBillPackagesAllocation;
                int Count = listofCVarWH_HouseBillPackagesAllocation.Count();

                for (int i = 0; i < Count; i++)
                {
                    objCWH_HouseBillPackagesAllocation.lstCVarWH_HouseBillPackagesAllocation[i].ID = 0;
                    objCWH_HouseBillPackagesAllocation.lstCVarWH_HouseBillPackagesAllocation[i].HouseBillID = Int64.Parse(InsertGateIn.pHouseBillID);
                    objCWH_HouseBillPackagesAllocation.lstCVarWH_HouseBillPackagesAllocation[i].WarehouseID = Int32.Parse(InsertGateIn.pWarehouseID);
                    objCWH_HouseBillPackagesAllocation.lstCVarWH_HouseBillPackagesAllocation[i].AreaID = Int32.Parse(InsertGateIn.pAreaID);
                    objCWH_HouseBillPackagesAllocation.lstCVarWH_HouseBillPackagesAllocation[i].RowID = Int32.Parse(InsertGateIn.pRowID);

                    objCWH_HouseBillPackagesAllocation.lstCVarWH_HouseBillPackagesAllocation[i].OperationContainersAndPackageID = listofCVarWH_HouseBillPackagesAllocation[i].OperationContainersAndPackageID;
                    objCWH_HouseBillPackagesAllocation.lstCVarWH_HouseBillPackagesAllocation[i].RowLocationID = listofCVarWH_HouseBillPackagesAllocation[i].RowLocationID;
                    objCWH_HouseBillPackagesAllocation.lstCVarWH_HouseBillPackagesAllocation[i].HasDamage = listofCVarWH_HouseBillPackagesAllocation[i].HasDamage;
                    objCWH_HouseBillPackagesAllocation.lstCVarWH_HouseBillPackagesAllocation[i].DamageDescription = listofCVarWH_HouseBillPackagesAllocation[i].DamageDescription;
                    objCWH_HouseBillPackagesAllocation.lstCVarWH_HouseBillPackagesAllocation[i].Remarks = listofCVarWH_HouseBillPackagesAllocation[i].Remarks;
                    objCWH_HouseBillPackagesAllocation.lstCVarWH_HouseBillPackagesAllocation[i].AddedBy = WebSecurity.CurrentUserId;
                    objCWH_HouseBillPackagesAllocation.lstCVarWH_HouseBillPackagesAllocation[i].AddedAt = DateTime.Now;
                    objCWH_HouseBillPackagesAllocation.lstCVarWH_HouseBillPackagesAllocation[i].UpdatedBy = WebSecurity.CurrentUserId;
                    objCWH_HouseBillPackagesAllocation.lstCVarWH_HouseBillPackagesAllocation[i].UpdatedAt = DateTime.Now;

                }
                checkException = objCWH_HouseBillPackagesAllocation.SaveMethod(objCWH_HouseBillPackagesAllocation.lstCVarWH_HouseBillPackagesAllocation);
            }

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] {
                _result //pData[0]
                , _result ? objCVarWH_Inventory.ID : 0 //pData[1]
                , _result ? serializer.Serialize(objCvwWH_CFS_GateIn.lstCVarvwWH_CFS_GateIn) : null //pData[2]
                , _RowCount //pData[3]
            };
        }


        [HttpGet, HttpPost]
        public Object[] WH_CFS_GateIn_FillOperationsList(string pWhereClause, string pOrderBy)
        {
            Exception checkException = null;
            Int32 _RowCount = 0;
            bool _result = false;

            CvwOperationsWithMinimalColumns objCvwOperations = new CvwOperationsWithMinimalColumns();

            checkException = objCvwOperations.GetListPaging(99999, 1, pWhereClause, pOrderBy, out _RowCount);

            if (checkException == null)
            {
                _result = true;
            }

            var pOperationList = objCvwOperations.lstCVarvwOperationsWithMinimalColumns
                    //.GroupBy(g => g.ReceiveID)
                    .Select(s => new
                    {
                        ID = s.ID
                        ,
                        Code = s.Code
                    }).ToList();


            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new Object[] {
                 _result
                , serializer.Serialize(pOperationList) //data[1]
               
            };
        }


        public class InsertGateIn
        {
            public String pOperationID { get; set; }

            public String pContainerID { get; set; }

            public String pHouseBillID { get; set; }

            public String pBookingPartyID { get; set; }

            public String pWarehouseID { get; set; }

            public String pAreaID { get; set; }

            public String pRowID { get; set; }

            public String pRowLocationID { get; set; }

            public String pWarehouseNoteID { get; set; }

            public String pEmptyContainerID { get; set; }

            public DateTime pEntryDate { get; set; }

            public String pOtherRemarks { get; set; }
            public Boolean pHasDamage { get; set; }
            public String pDamageDescription { get; set; }
            public String pHouseBillPackages { get; set; }

            public String pAddedBy { get; set; }

            public DateTime pAddedAt { get; set; }

            public String pUpdatedBy { get; set; }

            public DateTime pUpdatedAt { get; set; }


            /*****************************/
            public string pWhereClause { get; set; }
            public Int32 pPageSize { get; set; }
            public Int32 pPageNumber { get; set; }
            public string pOrderBy { get; set; }
        }

        #region O T H E R  F U N C T I O N S
        [HttpGet, HttpPost]
        public Object[] GetWarehouseAreas(Int32 pWarehouseID)
        {
            bool _result = false;
            Exception checkException = null;

            CWH_Area objCWH_Area = new CWH_Area();

            // Getting Areas list
            checkException = objCWH_Area.GetList(" where WarehouseID = " + pWarehouseID.ToString());

            if (checkException == null)
            {
                _result = true;
            }
            var pAreasList = objCWH_Area.lstCVarWH_Area
                .Select(s => new
                {
                    ID = s.ID
                    ,
                    Name = s.Name
                })
                .Distinct().OrderBy(o => o.Name).ToList();


            return new Object[]
            {
                _result // 0
                ,new JavaScriptSerializer().Serialize(pAreasList) // 1
            };
        }

        [HttpGet, HttpPost]
        public Object[] GetAreaRows(Int32 pAreaID)
        {
            bool _result = false;
            Exception checkException = null;

            CWH_Row objCWH_Row = new CWH_Row();

            // Getting Rows list
            checkException = objCWH_Row.GetList(" where AreaID = " + pAreaID.ToString());

            if (checkException == null)
            {
                _result = true;
            }
            var pRowsList = objCWH_Row.lstCVarWH_Row
                .Select(s => new
                {
                    ID = s.ID
                    ,
                    Name = s.Name
                })
                .Distinct().OrderBy(o => o.Name).ToList();


            return new Object[]
            {
                _result // 0
                ,new JavaScriptSerializer().Serialize(pRowsList) // 1
            };
        }

        [HttpGet, HttpPost]
        public Object[] GetRowLocations(Int32 pRowID)
        {
            bool _result = false;
            Exception checkException = null;

            CWH_RowLocation objCWH_RowLocation = new CWH_RowLocation();

            // Getting Locations list
            checkException = objCWH_RowLocation.GetList(" where RowID = " + pRowID.ToString());

            if (checkException == null)
            {
                _result = true;
            }
            var pRowLocation = objCWH_RowLocation.lstCVarWH_RowLocation
                .Select(s => new
                {
                    ID = s.ID
                    ,
                    Code = s.Code
                })
                .Distinct().OrderBy(o => o.Code).ToList();

            return new Object[]
            {
                _result // 0
                ,new JavaScriptSerializer().Serialize(pRowLocation) // 1
            };
        }

        #endregion
    }
}
