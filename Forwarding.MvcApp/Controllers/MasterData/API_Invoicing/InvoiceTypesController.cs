using Forwarding.MvcApp.Models.MasterData.Invoicing.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using WebMatrix.WebData;

namespace Forwarding.MvcApp.Controllers.MasterData.API_Invoicing
{
    public class InvoiceTypesController : ApiController
    {
        //[Route("/api/InvoiceTypes/LoadAll")]
        [HttpGet, HttpPost]
        //sherif: to be used in select boxes
        public Object[] LoadAll(string pWhereClause)
        {
            CInvoiceTypes objCInvoiceTypes = new CInvoiceTypes();
            objCInvoiceTypes.GetList(pWhereClause);
            return new Object[] { new JavaScriptSerializer().Serialize(objCInvoiceTypes.lstCVarInvoiceTypes) };
        }

        // [Route("/api/InvoiceTypes/LoadWithPaging/{pPageNumber}/{pPageSize}")]
        [HttpGet, HttpPost]
        public Object[] LoadWithPaging(Int32 pPageNumber, Int32 pPageSize, string pSearchKey)
        {
            CInvoiceTypes objCInvoiceTypes = new CInvoiceTypes();

            pSearchKey = (pSearchKey == null ? "" : pSearchKey.Trim().ToUpper());
            Int32 _RowCount = objCInvoiceTypes.lstCVarInvoiceTypes.Count;
            string whereClause = " Where (Code LIKE '%" + pSearchKey + "%' "
                + " OR Name LIKE '%" + pSearchKey + "%' "
                + " OR LocalName LIKE '%" + pSearchKey + "%') "
                + " AND IsInactive=0 ";
            objCInvoiceTypes.GetListPaging(pPageSize, pPageNumber, whereClause, "Name", out _RowCount);
            return new Object[] { new JavaScriptSerializer().Serialize(objCInvoiceTypes.lstCVarInvoiceTypes), _RowCount };
        }

        // [Route("/api/InvoiceTypes/Insert/{pCode}/{pName}/{pLocalName}}")]
        [HttpGet, HttpPost]
        public bool Insert(String pCode, String pName, String pLocalName, string pNotes/*, bool pIsAddedManually*/, bool pIsInactive, bool pIsWarehouseType
            , bool pIsSendToETA)
        {
            bool _result = false;
            CVarInvoiceTypes objCVarInvoiceTypes = new CVarInvoiceTypes();

            objCVarInvoiceTypes.Code = (pCode == null ? "" : pCode.ToUpper());
            objCVarInvoiceTypes.Name = pName.ToUpper();
            objCVarInvoiceTypes.LocalName = (pLocalName == null ? "" : pLocalName.ToUpper());
            objCVarInvoiceTypes.Notes = (pNotes == null ? "" : pNotes.ToUpper());
            
            objCVarInvoiceTypes.IsInactive = pIsInactive;
            objCVarInvoiceTypes.TimeLocked = DateTime.Parse("01-01-1900");
            objCVarInvoiceTypes.LockingUserID = 0;

            objCVarInvoiceTypes.CreatorUserID = objCVarInvoiceTypes.ModificatorUserID = WebSecurity.CurrentUserId;
            objCVarInvoiceTypes.CreationDate = objCVarInvoiceTypes.ModificationDate = DateTime.Now;

            objCVarInvoiceTypes.IsWarehouseType = pIsWarehouseType;
            objCVarInvoiceTypes.IsSendToETA = pIsSendToETA;

            CInvoiceTypes objCInvoiceTypes = new CInvoiceTypes();
            objCInvoiceTypes.lstCVarInvoiceTypes.Add(objCVarInvoiceTypes);
            Exception checkException = objCInvoiceTypes.SaveMethod(objCInvoiceTypes.lstCVarInvoiceTypes);
            if (checkException != null) // an exception is caught in the model
            {
                if (checkException.Message.Contains("UNIQUE"))
                    _result = false;
            }
            else //not unique
                _result = true;
            return _result;
        }

        // [Route("/api/InvoiceTypes/Update/{pCode}/{pName}/{pLocalName}}")]
        [HttpGet, HttpPost]
        public bool Update(Int32 pID, String pCode, String pName, String pLocalName, string pNotes/*, bool pIsAddedManually*/, bool pIsInactive, bool pIsWarehouseType
            , bool pIsSendToETA)
        {
            bool _result = false;

            string updateClause = "";
            updateClause += " Code = " + (pCode == null ? " NULL " : ("N'" + pCode + "'"));
            updateClause += " , Name = N'" + pName + "'";
            updateClause += " , LocalName = " + (pLocalName == null ? " NULL " : ("N'" + pLocalName + "'"));
            updateClause += " , Notes = " + (pNotes == null ? " NULL " : ("N'" + pNotes + "'"));
            updateClause += " , ModificatorUserID = " + WebSecurity.CurrentUserId.ToString();
            updateClause += " , ModificationDate = GETDATE() ";
            updateClause += " , IsSendToETA = " + (pIsSendToETA == false ? " 0 " : " 1 ");
            updateClause += " , IsWarehouseType = " + (pIsWarehouseType == false ? " 0 " : " 1 ");

            updateClause += " WHERE ID = " + pID.ToString();
            
            CInvoiceTypes objCInvoiceTypes = new CInvoiceTypes();
            Exception checkException = objCInvoiceTypes.UpdateList(updateClause);
            if (checkException != null) // an exception is caught in the model
            {
                if (checkException.Message.Contains("UNIQUE"))
                    _result = false;
            }
            else //not unique
                _result = true;
            
            
            return _result;
        }

        // [Route("/api/InvoiceTypes/Delete/{pInvoiceTypesIDs}")]
        [HttpGet, HttpPost]
        public bool Delete(String pInvoiceTypesIDs)
        {
            bool _result = false;
            CInvoiceTypes objCInvoiceTypes = new CInvoiceTypes();
            foreach (var currentID in pInvoiceTypesIDs.Split(','))
            {
                objCInvoiceTypes.lstDeletedCPKInvoiceTypes.Add(new CPKInvoiceTypes() { ID = Int32.Parse(currentID.Trim()) });
            }

            Exception checkException = objCInvoiceTypes.DeleteItem(objCInvoiceTypes.lstDeletedCPKInvoiceTypes);
            if (checkException != null) // an exception is caught in the model
            {
                if (checkException.Message.Contains("DELETE")) // some or all of the rows were not deleted due to dependencies
                    _result = false;
            }
            else //deleted successfully
                _result = true;
            return _result;
        }

        //If pInvoiceTypeID is 0 then this is unbind, else bind
        [HttpGet, HttpPost]
        public bool BindOrUnbindList(int pInvoiceTypeID, String pSelectedChargeTypesIDs)
        {
            bool _result = false;
            CChargeTypes objCChargeTypes = new CChargeTypes();
            string pUpdateClause = "";
            string pWhereClause = "";
            pUpdateClause = " InvoiceTypeID = " + (pInvoiceTypeID == 0 ? " NULL " : pInvoiceTypeID.ToString());
            //building the where clause
            foreach (var currentID in pSelectedChargeTypesIDs.Split(','))
            {
                //i am sure i ve at least 1 selectedID isa
                pWhereClause += (pWhereClause == "" ? " WHERE ID = " + currentID.ToString()
                    : " OR ID = " + currentID.ToString());
            }
            pUpdateClause += pWhereClause;
            Exception checkException = objCChargeTypes.UpdateList(pUpdateClause);
            if (checkException != null) // an exception is caught in the model
            {
            }
            else //set successfully
                _result = true;
            return _result;
        }
        
    }
}
