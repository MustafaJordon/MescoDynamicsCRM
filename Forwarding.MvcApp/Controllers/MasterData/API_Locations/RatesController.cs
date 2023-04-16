using Forwarding.MvcApp.Models.MasterData.Locations.Generated;
using Forwarding.MvcApp.Models.MasterData.Others.Generated;
using Forwarding.MvcApp.Models.MasterData.Partners.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using WebMatrix.WebData;

namespace Forwarding.MvcApp.Controllers.MasterData.API_Locations
{
    public class RatesController : ApiController
    {
        //[Route("/api/Cities/LoadAll")]
        [HttpGet, HttpPost]
        //sherif: to be used in select boxes
        public Object[] LoadAll(string pWhereClause,string Tables)
        {
            CPorts Ports = new CPorts();
            if (Tables == "City-Region-PackageTypes")
            {
                CCities objCCities = new CCities();
                objCCities.GetList(pWhereClause);

                CPackageTypes objCPackageTypes = new CPackageTypes();
                objCPackageTypes.GetList(" Where 1=1");

                Ports.GetList(" Where 1=1 and IsFactories=1");
                return new Object[] { new JavaScriptSerializer().Serialize(objCCities.lstCVarCities),
                                  new JavaScriptSerializer().Serialize(Ports.lstCVarPorts),
                                  new JavaScriptSerializer().Serialize(objCPackageTypes.lstCVarPackageTypes)
                };
            }
            else if (Tables == "Region")
            {
                Ports.GetList(pWhereClause + " AND IsFactories=1");
                return new Object[] { 
                                  new JavaScriptSerializer().Serialize(Ports.lstCVarPorts)
                };
            }
            else
                return new Object[] {new JavaScriptSerializer().Serialize(Ports.lstCVarPorts)};
        }

        [HttpGet, HttpPost]
        public Object[] LoadWithPaging(Int32 pPageNumber, Int32 pPageSize, string pSearchKey)
        {
            CLM_Rates objCLM_Rates = new CLM_Rates();
            Int32 _RowCount = 0;
            
            pSearchKey = (pSearchKey == null ? "" : pSearchKey.Trim().ToUpper());
            string whereClause = " Where Code LIKE '%" + pSearchKey + "%' "
                + " OR Name LIKE '%" + pSearchKey + "%' ";

            objCLM_Rates.GetListPaging(pPageSize, pPageNumber, whereClause, " Name ", out _RowCount);
                return new Object[] { new JavaScriptSerializer().Serialize(objCLM_Rates.lstCVarLM_Rates), _RowCount };
        }
        
        [HttpGet, HttpPost]
        public Object[] RateRegions_LoadingWithPaging(Int32 pPageNumber, Int32 pPageSize, string pSearchKey)
        {
            CvwLM_RateRegions objCvwLM_RateRegions = new CvwLM_RateRegions();
            Int32 _RowCount = 0;

            pSearchKey = (pSearchKey == null ? "" : pSearchKey.Trim().ToUpper());
            string whereClause = " Where CityNameFrom LIKE '%" + pSearchKey + "%' "
                + " OR RegionNameFrom LIKE '%" + pSearchKey + "%' "
                + " OR CityNameTo LIKE '%" + pSearchKey + "%' "
                + " OR RegionNameTo LIKE '%" + pSearchKey + "%' ";

            objCvwLM_RateRegions.GetListPaging(pPageSize, pPageNumber, whereClause, " CityIDFrom ", out _RowCount);
            return new Object[] { new JavaScriptSerializer().Serialize(objCvwLM_RateRegions.lstCVarvwLM_RateRegions), _RowCount };
        }
        
        [HttpGet, HttpPost]
        public Object[] RateRegions_LoadingWithPagingByRateID(Int32 pPageNumber, Int32 pPageSize, string pWhereClause,string pOrderBy)
        {
            CvwLM_RateRegions objCvwLM_RateRegions = new CvwLM_RateRegions();
            Int32 _RowCount = 0;

            objCvwLM_RateRegions.GetListPaging(pPageSize, pPageNumber, pWhereClause, pOrderBy, out _RowCount);
            return new Object[] { new JavaScriptSerializer().Serialize(objCvwLM_RateRegions.lstCVarvwLM_RateRegions), _RowCount };
        }

        [HttpGet, HttpPost]
        public bool Insert_Update(Rate pRate)
        {
            bool _result = false;
            Exception checkException = null;
           CVarLM_Rates objCVarLM_Rates = new CVarLM_Rates();

            objCVarLM_Rates.ID = pRate.ID;
            objCVarLM_Rates.Code = pRate.Code;
            objCVarLM_Rates.Name = pRate.Name;
            objCVarLM_Rates.RateFromDate = (pRate.RateFromDate == null ? DateTime.Parse("01/01/1900") : pRate.RateFromDate);
            objCVarLM_Rates.RateToDate = (pRate.RateToDate == null ? DateTime.Parse("01/01/1900") : pRate.RateToDate);
            objCVarLM_Rates.Remarks = pRate.Remarks;
            objCVarLM_Rates.CreatorUserID = WebSecurity.CurrentUserId;
            objCVarLM_Rates.creationDate = DateTime.Now;

            CLM_Rates objCLM_Rates = new CLM_Rates();
            objCLM_Rates.lstCVarLM_Rates.Add(objCVarLM_Rates);
            checkException = objCLM_Rates.SaveMethod(objCLM_Rates.lstCVarLM_Rates);
            if (checkException != null) // an exception is caught in the model
            {
                if (checkException.Message.Contains("UNIQUE"))
                    _result = false;
            }
            else //not unique
                _result = true;
            return _result;
        }
        
        [HttpGet, HttpPost]
        public bool Insert_Update_CustomerRate(int CustomerRateID, int pCustomerID ,int pRateID ,bool pRateIsInActive)
        {
            bool _result = false;

            CVarLM_Customer_Rates objCVarLM_Customer_Rates = new CVarLM_Customer_Rates();

            objCVarLM_Customer_Rates.ID = CustomerRateID;
            objCVarLM_Customer_Rates.CustomerID = pCustomerID;
            objCVarLM_Customer_Rates.RateID = pRateID;
            objCVarLM_Customer_Rates.FromDate = (DateTime.Parse("01/01/1900"));
            objCVarLM_Customer_Rates.ToDate = (DateTime.Parse("01/01/1900"));
            objCVarLM_Customer_Rates.IsInActive = pRateIsInActive;
            objCVarLM_Customer_Rates.CreatorUserID = WebSecurity.CurrentUserId;
            objCVarLM_Customer_Rates.creationDate = DateTime.Now;

            CLM_Customer_Rates objCLM_Customer_Rates = new CLM_Customer_Rates();
            objCLM_Customer_Rates.lstCVarLM_Customer_Rates.Add(objCVarLM_Customer_Rates);
            Exception checkException = objCLM_Customer_Rates.SaveMethod(objCLM_Customer_Rates.lstCVarLM_Customer_Rates);
            if (checkException != null) // an exception is caught in the model
            {
                if (checkException.Message.Contains("UNIQUE"))
                    _result = false;
            }
            else //not unique
                _result = true;
            return _result;
        }

        [HttpGet, HttpPost]
        public Object[] LoadCustomerRates(int pCustomerID)
        {
            CvwLM_Customer_Rates objCvwLM_Customer_Rates = new CvwLM_Customer_Rates();
            objCvwLM_Customer_Rates.GetList(" Where CustomerID = "+ pCustomerID);
            return new Object[] { new JavaScriptSerializer().Serialize(objCvwLM_Customer_Rates.lstCVarvwLM_Customer_Rates) };
        }
        
        [HttpGet, HttpPost]
        public Object[] LoadRatesToCustomer(string pWhereClause)
        {
            CvwLM_Customer_Rates objCvwLM_Customer_Rates = new CvwLM_Customer_Rates();
            objCvwLM_Customer_Rates.GetList(pWhereClause);
            if(objCvwLM_Customer_Rates.lstCVarvwLM_Customer_Rates.Count == 0)
                objCvwLM_Customer_Rates.GetList(" Where 1=1");
            return new Object[] { new JavaScriptSerializer().Serialize(objCvwLM_Customer_Rates.lstCVarvwLM_Customer_Rates) };
        }


        [HttpGet, HttpPost]
        public bool RateRegionsInsert_Update(RateRegions pRateRegions)
        {
            bool _result = false;

            CVarLM_RateRegions objCVarLM_RateRegions = new CVarLM_RateRegions();

            objCVarLM_RateRegions.ID = pRateRegions.ID;
            objCVarLM_RateRegions.RateID = pRateRegions.RateID;
            objCVarLM_RateRegions.CityIDFrom = pRateRegions.CityIDFrom;
            objCVarLM_RateRegions.CityIDTo = pRateRegions.CityIDTo;
            objCVarLM_RateRegions.RegionIDFrom = pRateRegions.RegionIDFrom;
            objCVarLM_RateRegions.RegionIDTo = pRateRegions.RegionIDTo;
            objCVarLM_RateRegions.Cost = pRateRegions.Cost;
            objCVarLM_RateRegions.Selling = pRateRegions.Selling;
            objCVarLM_RateRegions.Remarks = pRateRegions.Remarks;

            objCVarLM_RateRegions.PackageTypeID = pRateRegions.PackageTypeID;
            objCVarLM_RateRegions.Quantity = pRateRegions.Quantity;

            objCVarLM_RateRegions.CreatorUserID = WebSecurity.CurrentUserId;
            objCVarLM_RateRegions.creationDate = DateTime.Now;

            CLM_RateRegions objCLM_RateRegions = new CLM_RateRegions();
            objCLM_RateRegions.lstCVarLM_RateRegions.Add(objCVarLM_RateRegions);
            Exception checkException = objCLM_RateRegions.SaveMethod(objCLM_RateRegions.lstCVarLM_RateRegions);
            if (checkException != null) // an exception is caught in the model
            {
                if (checkException.Message.Contains("UNIQUE"))
                    _result = false;
            }
            else //not unique
                _result = true;
            return _result;
        }
        
        
        [HttpGet, HttpPost]
        public bool Delete(String pRatesIDs)
        {
            bool _result = false;
            CLM_Rates objCLM_Rates = new CLM_Rates();
            foreach (var currentID in pRatesIDs.Split(','))
            {
                objCLM_Rates.lstDeletedCPKLM_Rates.Add(new CPKLM_Rates() { ID = Int32.Parse(currentID.Trim()) });
            }

            Exception checkException = objCLM_Rates.DeleteItem(objCLM_Rates.lstDeletedCPKLM_Rates);
            if (checkException != null) // an exception is caught in the model
            {
                if (checkException.Message.Contains("DELETE")) // some or all of the rows were not deleted due to dependencies
                    _result = false;
            }
            else //deleted successfully
                _result = true;
            return _result;
        }

        
        [HttpGet, HttpPost]
        public bool DeleteLM_Customer_Rates(String pCustomer_Rate_ID)
        {
            bool _result = false;
            CLM_Customer_Rates objCLM_Customer_Rates = new CLM_Customer_Rates();
            foreach (var currentID in pCustomer_Rate_ID.Split(','))
            {
                objCLM_Customer_Rates.lstDeletedCPKLM_Customer_Rates.Add(new CPKLM_Customer_Rates() { ID = Int32.Parse(currentID.Trim()) });
            }

            Exception checkException = objCLM_Customer_Rates.DeleteItem(objCLM_Customer_Rates.lstDeletedCPKLM_Customer_Rates);
            if (checkException != null) // an exception is caught in the model
            {
                if (checkException.Message.Contains("DELETE")) // some or all of the rows were not deleted due to dependencies
                    _result = false;
            }
            else //deleted successfully
                _result = true;
            return _result;
        }


        [HttpGet, HttpPost]
        public bool DeleteRateRegions(String pRateRegionIDs)
        {
            bool _result = false;
            CLM_RateRegions objCLM_RateRegions = new CLM_RateRegions();
            foreach (var currentID in pRateRegionIDs.Split(','))
            {
                objCLM_RateRegions.lstDeletedCPKLM_RateRegions.Add(new CPKLM_RateRegions() { ID = Int32.Parse(currentID.Trim()) });
            }

            Exception checkException = objCLM_RateRegions.DeleteItem(objCLM_RateRegions.lstDeletedCPKLM_RateRegions);
            if (checkException != null) // an exception is caught in the model
            {
                if (checkException.Message.Contains("DELETE")) // some or all of the rows were not deleted due to dependencies
                    _result = false;
            }
            else //deleted successfully
                _result = true;
            return _result;
        }

    }

    public class Rate
    {
        public int ID { get; set; }
        public String Code{get; set;}
        public String Name { get; set; }
        public DateTime RateFromDate { get; set; }
        public DateTime RateToDate { get; set; }
        public String Remarks { get; set; }
    }

    public class RateRegions
    {
        public Int32 ID { get; set; }
        public Int32 RateID { get; set; }
        
        public Int32 CityIDFrom { get; set; }
        public Int32 RegionIDFrom { get; set; }
        public Int32 CityIDTo { get; set; }
        public Int32 RegionIDTo { get; set; }
        public Decimal Cost { get; set; }
        public Decimal Selling { get; set; }
        public String Remarks { get; set; }
        public int PackageTypeID { get; set; }
        public decimal Quantity { get; set; }
    }
}
