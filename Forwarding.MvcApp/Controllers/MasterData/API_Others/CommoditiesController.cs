﻿using Forwarding.MvcApp.Models.MasterData.Others.Generated;
using System;
using System.Web.Http;
using System.Web.Script.Serialization;
using WebMatrix.WebData;

namespace Forwarding.MvcApp.Controllers.MasterData.API_Others
{
    public class CommoditiesController : ApiController
    {

        //[Route("/api/Commodities/LoadAll")]
        [HttpGet, HttpPost]
        //sherif: to be used in select boxes
        public Object[] LoadAll(string pOrderBy)
        {
            CCommodities objCCommodities = new CCommodities();
            objCCommodities.GetList(" order by " + pOrderBy);
            return new Object[] { new JavaScriptSerializer().Serialize(objCCommodities.lstCVarCommodities) };
        }

        // [Route("/api/Commodities/LoadWithPaging/{pPageNumber}/{pPageSize}")]
        [HttpGet, HttpPost]
        public Object[] LoadWithPaging(Int32 pPageNumber, Int32 pPageSize, string pSearchKey)
        {
            CCommodities objCCommodities = new CCommodities();
            //objCCommodities.GetList(string.Empty); //GetList() fn loads without paging

            pSearchKey = (pSearchKey == null ? "" : pSearchKey.Trim().ToUpper());
            Int32 _RowCount = objCCommodities.lstCVarCommodities.Count;
            string whereClause = " Where Code LIKE N'%" + pSearchKey + "%' "
                + " OR Name LIKE N'%" + pSearchKey + "%' "
                + " OR LocalName LIKE N'%" + pSearchKey + "%' ";
            objCCommodities.GetListPaging(pPageSize, pPageNumber, whereClause, "Code", out _RowCount);
            return new Object[] { new JavaScriptSerializer().Serialize(objCCommodities.lstCVarCommodities), _RowCount };
        }

        // [Route("/api/Commodities/Insert/{pCode}/{pName}/{pLocalName}}")]
        [HttpGet, HttpPost]
        public bool Insert(string pCode, string pName, string pLocalName, string pNotes, decimal pIMOClass, Int32 pUNNumber
            , bool pIsInactive, bool pIsIMO , string pCommercialName, string pLoadingTemperature, string pUnloadingTemperature,string pDensity)
        //public bool Insert(String pCode, String pName)
        {
            bool _result = false;

            CVarCommodities objCVarCommodities = new CVarCommodities();

            objCVarCommodities.Code = (pCode == null ? "0" : pCode.Trim().ToUpper());
            objCVarCommodities.Name = pName.ToUpper();
            objCVarCommodities.LocalName = (pLocalName == null ? "0" : pLocalName.ToUpper());
            objCVarCommodities.Notes = (pNotes == null ? "0" : pNotes);
            objCVarCommodities.IMOClass = pIMOClass;
            objCVarCommodities.UNNumber = pUNNumber;
            objCVarCommodities.IsInactive = pIsInactive;
            objCVarCommodities.IsIMO = pIsIMO;
            objCVarCommodities.CommercialName = (pCommercialName == null ? "0": pCommercialName);
            objCVarCommodities.LoadingTemperature = (pLoadingTemperature == null ? "0" : pLoadingTemperature);
            objCVarCommodities.UnloadingTemperature = (pUnloadingTemperature == null ? "0" : pUnloadingTemperature);
            objCVarCommodities.Density = (pDensity == null ? "0" : pDensity);

            objCVarCommodities.TimeLocked = DateTime.Parse("01-01-1900");
            objCVarCommodities.LockingUserID = 0;

            objCVarCommodities.CreatorUserID = objCVarCommodities.ModificatorUserID = WebSecurity.CurrentUserId;
            objCVarCommodities.CreationDate = objCVarCommodities.ModificationDate = DateTime.Now;

            CCommodities objCCommodities = new CCommodities();
            objCCommodities.lstCVarCommodities.Add(objCVarCommodities);
            Exception checkException = objCCommodities.SaveMethod(objCCommodities.lstCVarCommodities);
            if (checkException != null) // an exception is caught in the model
            {
                if (checkException.Message.Contains("UNIQUE"))
                    _result = false;
            }
            else //not unique
                _result = true;
            return _result;
        }

        // [Route("/api/Commodities/Update/{pCode}/{pName}/{pLocalName}}")]
        [HttpGet, HttpPost]
        public bool Update(Int32 pID, string pCode, string pName, string pLocalName, string pNotes, decimal pIMOClass, Int32 pUNNumber
            , bool pIsInactive, bool pIsIMO, string pCommercialName, string pLoadingTemperature, string pUnloadingTemperature, string pDensity)
        {
            bool _result = false;

            CVarCommodities objCVarCommodities = new CVarCommodities();

            //the next 4 lines to get the CreatorUserID and CreationDate and set them as they were entered before
            CCommodities objCGetCreationInformation = new CCommodities();
            objCGetCreationInformation.GetItem(pID);
            objCVarCommodities.CreatorUserID = objCGetCreationInformation.lstCVarCommodities[0].CreatorUserID;
            objCVarCommodities.CreationDate = objCGetCreationInformation.lstCVarCommodities[0].CreationDate;

            objCVarCommodities.ID = pID;
            objCVarCommodities.Code = (pCode == null ? "0" : pCode.Trim().ToUpper());
            objCVarCommodities.Name = pName.ToUpper();
            objCVarCommodities.LocalName = (pLocalName == null ? "0" : pLocalName.ToUpper());
            objCVarCommodities.Notes = (pNotes == null ? "0" : pNotes);
            objCVarCommodities.IMOClass = pIMOClass;
            objCVarCommodities.UNNumber = pUNNumber;
            objCVarCommodities.IsInactive = pIsInactive;
            objCVarCommodities.IsIMO = pIsIMO;
            

            objCVarCommodities.CommercialName = (pCommercialName == null ? "0" : pCommercialName);
            objCVarCommodities.LoadingTemperature = (pLoadingTemperature == null ? "0" : pLoadingTemperature);
            objCVarCommodities.UnloadingTemperature = (pUnloadingTemperature == null ? "0" : pUnloadingTemperature);
            objCVarCommodities.Density = (pDensity == null ? "0" : pDensity);

            objCVarCommodities.TimeLocked = DateTime.Parse("01-01-1900");
            objCVarCommodities.LockingUserID = 0;

            objCVarCommodities.ModificatorUserID = WebSecurity.CurrentUserId;
            objCVarCommodities.ModificationDate = DateTime.Now;

            CCommodities objCCommodities = new CCommodities();
            objCCommodities.lstCVarCommodities.Add(objCVarCommodities);
            Exception checkException = objCCommodities.SaveMethod(objCCommodities.lstCVarCommodities);
            if (checkException != null) // an exception is caught in the model
            {
                if (checkException.Message.Contains("UNIQUE"))
                    _result = false;
            }
            else //not unique
                _result = true;
            return _result;
        }

        //// [Route("/api/Commodities/DeleteByID/{pID}")]
        //[HttpGet, HttpPost]
        //public void DeleteByID(Int32 pID)
        //{
        //    CCommodities objCCommodities = new CCommodities();
        //    objCCommodities.lstDeletedCPKCommodities.Add(new CPKCommodities() { ID = pID });
        //    objCCommodities.DeleteItem(objCCommodities.lstDeletedCPKCommodities);
        //}

        // [Route("/api/Commodities/Delete/{pCommoditiesIDs}")]
        [HttpGet, HttpPost]
        public bool Delete(String pCommoditiesIDs)
        {
            bool _result = false;
            CCommodities objCCommodities = new CCommodities();
            foreach (var currentID in pCommoditiesIDs.Split(','))
            {
                objCCommodities.lstDeletedCPKCommodities.Add(new CPKCommodities() { ID = Int32.Parse(currentID.Trim()) });
            }

            Exception checkException = objCCommodities.DeleteItem(objCCommodities.lstDeletedCPKCommodities);
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
        public object[] InsertFromOperations(string pCodeFromOperations, string pNameFromOperations, string pLocalNameFromOperations)
        {
            string _MessageReturned = "";

            CVarCommodities objCVarCommodities = new CVarCommodities();
            CCommodities objCCommodities = new CCommodities();
            objCVarCommodities.Code = pCodeFromOperations.ToUpper();
            objCVarCommodities.Name = pNameFromOperations.ToUpper();
            objCVarCommodities.LocalName = (pLocalNameFromOperations == null ? "" : pLocalNameFromOperations.ToUpper());
            objCVarCommodities.Notes = "";
            objCVarCommodities.IMOClass = 0;
            objCVarCommodities.UNNumber = 0;
            objCVarCommodities.IsInactive = false;
            objCVarCommodities.IsIMO = false;

            objCVarCommodities.CommercialName = "0";
            objCVarCommodities.LoadingTemperature = "0";
            objCVarCommodities.UnloadingTemperature = "0";
            objCVarCommodities.Density = "0";

            objCVarCommodities.TimeLocked = DateTime.Parse("01-01-1900");
            objCVarCommodities.LockingUserID = 0;

            objCVarCommodities.CreatorUserID = objCVarCommodities.ModificatorUserID = WebSecurity.CurrentUserId;
            objCVarCommodities.CreationDate = objCVarCommodities.ModificationDate = DateTime.Now;

            objCCommodities.lstCVarCommodities.Add(objCVarCommodities);
            Exception checkException = objCCommodities.SaveMethod(objCCommodities.lstCVarCommodities);
            if (checkException == null) //get returned data
            {
                objCCommodities.GetList("WHERE IsInactive=0 ORDER BY Name");
            }
            else
                _MessageReturned = checkException.Message;
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] {
                _MessageReturned
                , _MessageReturned == "" ? objCVarCommodities.ID : 0 //pInsertedID = pData[1]
                , _MessageReturned == "" ? serializer.Serialize(objCCommodities.lstCVarCommodities) : null //pCommodities = pData[2]
            };
        }

    }
}
