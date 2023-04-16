using Forwarding.MvcApp.Models.OperAcc.Generated;
using Forwarding.MvcApp.Models.Administration.Security.Generated;
using Forwarding.MvcApp.Models.Administration.Settings.Generated;
using Forwarding.MvcApp.Models.LocalEmails.Generated;
using Forwarding.MvcApp.Models.MasterData.Invoicing.Generated;
using System;
using System.Web.Http;
using System.Web.Script.Serialization;
using WebMatrix.WebData;
using System.Net.Mail;
using Forwarding.MvcApp.Models.MasterData.Locations.Generated;
using Forwarding.MvcApp.Models.CRM.CRM_Clients.Generated;
using Forwarding.MvcApp.Models.CRM.CRM_FollowUp.Generated;
using Forwarding.MvcApp.Models.MasterData.Others.Generated;
using System.Web;
//using IronPdf;
using Forwarding.BLL.Utilities;
using System.Text.RegularExpressions;
using System.IO;
using Forwarding.MvcApp.Models.MasterData.Partners.Generated;
using Forwarding.MvcApp.Models.NoAccess.Generated;
using Forwarding.MvcApp.Models.CRM.Generated;
using OpenHtmlToPdf;
using Forwarding.MvcApp.Models.Accounting.Transactions.Generated;
using MoreLinq;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Forwarding.MvcApp.Models.Operations.Operations.Generated;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace Forwarding.MvcApp.Controllers.Administration.API_Settings
{
    public class DefaultsController : ApiController
    {

        #region delete

        //[HttpGet, HttpPost]
        //public async Task<object[]> eFBL_Generate(Int64 pID_eFBL)
        //{

        //    #region Getting Data And Validating
        //    string pReturnedMessage = "";
        //    string json = "";
        //    int _RowCount = 0;
        //    string pFileName = "";
        //    byte[] pdfInBytes = null;

        //    int constCustomerPartnerTypeID = 1;
        //    int constAgentPartnerTypeID = 2;
        //    //int constShippingAgentPartnerTypeID = 3;
        //    //int constCustomsClearanceAgentPartnerTypeID = 4;
        //    int constShippingLinePartnerTypeID = 5;
        //    //int constAirlinePartnerTypeID = 6;
        //    //int constTruckerPartnerTypeID = 7;
        //    //int constSupplierPartnerTypeID = 8;
        //    //int constCustodyPartnerTypeID = 20;
        //    Exception checkException = null;

        //    #region Get MapPath

        //    //I put MapPath at the top because after await it becomes null
        //    bool exists = Directory.Exists(HttpContext.Current.Server.MapPath(pID_eFBL.ToString()));
        //    if (!exists)
        //        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(pID_eFBL.ToString()));
        //    string mapPath = HttpContext.Current.Server.MapPath(pID_eFBL.ToString());
        //    #endregion Get MapPath
        //    CvwOperations objCvwOperations = new CvwOperations();
        //    CvwOperationContainersAndPackages objCvwOperationContainersAndPackages = new CvwOperationContainersAndPackages();

        //    CContacts objCShipperContacts = new CContacts();
        //    CvwAddresses objCShipperAddresses = new CvwAddresses();

        //    CContacts objCConsigneeContacts = new CContacts();
        //    CvwAddresses objCConsigneeAddresses = new CvwAddresses();

        //    CContacts objCAgentContacts = new CContacts();
        //    CvwAddresses objCAgentAddresses = new CvwAddresses();

        //    CContacts objCShippingLineContacts = new CContacts();
        //    CvwAddresses objCShippingLineAddresses = new CvwAddresses();

        //    #region Get Data
        //    checkException = objCvwOperations.GetListPaging(1, 1, "WHERE ID=" + pID_eFBL, "ID", out _RowCount);
        //    checkException = objCvwOperationContainersAndPackages.GetListPaging(999, 1, "WHERE OperationID=" + pID_eFBL, "ID", out _RowCount);

        //    checkException = objCShipperAddresses.GetList("WHERE PartnerID=" + objCvwOperations.lstCVarvwOperations[0].ShipperID + " AND PartnerTypeID=" + constCustomerPartnerTypeID);
        //    checkException = objCShipperContacts.GetList("WHERE PartnerID=" + objCvwOperations.lstCVarvwOperations[0].ShipperID + " AND PartnerTypeID=" + constCustomerPartnerTypeID);

        //    checkException = objCConsigneeAddresses.GetList("WHERE PartnerID=" + objCvwOperations.lstCVarvwOperations[0].ConsigneeID + " AND PartnerTypeID=" + constCustomerPartnerTypeID);
        //    checkException = objCConsigneeContacts.GetList("WHERE PartnerID=" + objCvwOperations.lstCVarvwOperations[0].ConsigneeID + " AND PartnerTypeID=" + constCustomerPartnerTypeID);

        //    checkException = objCAgentAddresses.GetList("WHERE PartnerID=" + objCvwOperations.lstCVarvwOperations[0].AgentID + " AND PartnerTypeID=" + constAgentPartnerTypeID);
        //    checkException = objCAgentContacts.GetList("WHERE PartnerID=" + objCvwOperations.lstCVarvwOperations[0].AgentID + " AND PartnerTypeID=" + constAgentPartnerTypeID);

        //    checkException = objCShippingLineAddresses.GetList("WHERE PartnerID=" + objCvwOperations.lstCVarvwOperations[0].ShippingLineID + " AND PartnerTypeID=" + constShippingLinePartnerTypeID);
        //    checkException = objCShippingLineContacts.GetList("WHERE PartnerID=" + objCvwOperations.lstCVarvwOperations[0].ShippingLineID + " AND PartnerTypeID=" + constShippingLinePartnerTypeID);
        //    #endregion Get Data

        //    #region Validate
        //    if (objCShipperContacts.lstCVarContacts.Count == 0 || objCShipperAddresses.lstCVarvwAddresses.Count == 0)
        //    {
        //        pReturnedMessage = "Please, check you shipper's address and contact person";
        //    }
        //    else if (objCShipperContacts.lstCVarContacts[0].Phone1 == "0")
        //        pReturnedMessage = "Please, enter shipper contact's phone";
        //    else if (objCShipperContacts.lstCVarContacts[0].Email == "0")
        //        pReturnedMessage = "Please, enter shipper contact's email";
        //    else if (objCShipperAddresses.lstCVarvwAddresses[0].ZipCode == "0")
        //        pReturnedMessage = "Please, enter shipper post code";
        //    else if (objCShipperAddresses.lstCVarvwAddresses[0].StreetLine1 == "0")
        //        pReturnedMessage = "Please, enter shipper street";
        //    else if (objCShipperAddresses.lstCVarvwAddresses[0].CityID == 0)
        //        pReturnedMessage = "Please, enter shipper city";
        //    else if (objCShipperAddresses.lstCVarvwAddresses[0].CountryID == 0)
        //        pReturnedMessage = "Please, enter shipper country";

        //    else if (objCConsigneeContacts.lstCVarContacts.Count == 0)
        //    {
        //        pReturnedMessage = "Please, check you Consignee's contact person";
        //    }

        //    else if (objCShippingLineContacts.lstCVarContacts.Count == 0 || objCShippingLineAddresses.lstCVarvwAddresses.Count == 0)
        //    {
        //        pReturnedMessage = "Please, check the carrier's contact person and address";
        //    }
        //    #endregion Validate

        //    if (pReturnedMessage == "") //Mandatory Data is complete
        //    {
        //        #region exchanged_document
        //        json += " { \n";
        //        json += "   \"exchanged_document\": { \n";
        //        json += "     \"documentStatus\": { \n";
        //        json += "       \"value\": \"" + (objCvwOperations.lstCVarvwOperations[0].eFBLID == "0" ? "TBD" : objCvwOperations.lstCVarvwOperations[0].eFBLID) + "\" \n"; //????????????
        //        json += "     }, \n";

        //        json += "     \"issueDateTime\": { \n";
        //        json += "       \"value\": \"" + DateTime.Now.Year + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString().PadLeft(2, '0') + "T" + DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0') + ":00.000" + "\", \n"; //2021-03-31T12:34:56.123;
        //        json += "       \"format\": \"YYYY-MM-DDThh:mm:ss.sss\" \n";
        //        json += "     }, \n";

        //        json += "     \"firstSignatoryAuthentication\": { \n";
        //        json += "       \"actualDateTime\": { \n";
        //        json += "         \"value\": \"" + DateTime.Now.Year + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString().PadLeft(2, '0') + "T" + DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0') + ":00.000" + "\", \n"; //2021-03-31T12:34:56.012
        //        json += "         \"format\": \"YYYY-MM-DDThh:mm:ss.sss\" \n";
        //        json += "       }, \n";
        //        json += "       \"id\": \"12345\", \n"; //????????????
        //        json += "       \"statement\": { \n"; //????????????
        //        json += "         \"value\": \"\" \n";
        //        json += "       } \n";
        //        json += "     }, \n";

        //        json += "     \"issueLocation\": { \n";
        //        json += "       \"id\": \"" + objCvwOperations.lstCVarvwOperations[0].POLCode + "\", \n"; //????????????
        //        json += "       \"countryCode\": \"" + objCvwOperations.lstCVarvwOperations[0].POLCountryCode + "\", \n";  //????????????
        //        json += "       \"name\": { \n";
        //        json += "         \"value\": \"" + objCvwOperations.lstCVarvwOperations[0].POLName + "\" \n";
        //        json += "       } \n";
        //        json += "     }, \n";
        //        json += "     \"originalIssuedQuantity\": \"" + (objCvwOperations.lstCVarvwOperations[0].NumberOfOriginalBills == 0 ? 1 : objCvwOperations.lstCVarvwOperations[0].NumberOfOriginalBills) + "\" \n";
        //        json += "   }, \n";
        //        #endregion exchanged_document

        //        #region supply_chain_consignment
        //        json += "   \"supply_chain_consignment\": { \n";
        //        json += "     \"cargoInsuranceNotProvidedByCTO\": true, \n"; //CTO:Combined Transport Operator [Carrier/Line]

        //        #region Consignor
        //        {
        //            json += "     \"consignor\": { \n";
        //            json += "       \"name\": { \n";
        //            json += "         \"value\": \"" + objCvwOperations.lstCVarvwOperations[0].ShipperName + "\" \n";
        //            json += "       }, \n";
        //            json += "       \"definedContactDetails\": { \n";
        //            json += "         \"personName\": { \n";
        //            json += "           \"value\": \"" + objCShipperContacts.lstCVarContacts[0].Name + "\" \n";
        //            json += "         }, \n";
        //            json += "         \"telephone\": { \n";
        //            json += "           \"value\": \"" + objCShipperContacts.lstCVarContacts[0].Phone1 + "\" \n";
        //            json += "         }, \n";
        //            json += "         \"mobileTelephone\":{ \n";
        //            json += "           \"value\": \"" + (objCShipperContacts.lstCVarContacts[0].Mobile1 == "0" ? "" : objCShipperContacts.lstCVarContacts[0].Mobile1) + "\" \n";
        //            json += "         }, \n";
        //            json += "         \"emailAddress\":{ \n";
        //            json += "           \"value\": \"" + (objCShipperContacts.lstCVarContacts[0].Email == "0" ? "" : objCShipperContacts.lstCVarContacts[0].Email) + "\" \n";
        //            json += "         } \n";
        //            json += "       }, \n";
        //            json += "       \"postalAddress\": { \n";
        //            json += "         \"postcode\": \"" + objCShipperAddresses.lstCVarvwAddresses[0].ZipCode + "\", \n";
        //            json += "         \"streetName\": \"" + objCShipperAddresses.lstCVarvwAddresses[0].StreetLine1 + "\", \n";
        //            json += "         \"cityName\": \"" + objCShipperAddresses.lstCVarvwAddresses[0].CityName + "\", \n";
        //            json += "         \"countryCode\": \"" + objCShipperAddresses.lstCVarvwAddresses[0].CountryCode + "\", \n";
        //            json += "         \"countryName\": \"" + objCShipperAddresses.lstCVarvwAddresses[0].CountryName + "\" \n";
        //            json += "       } \n";
        //            json += "     }, \n";
        //        }
        //        #endregion Consignor

        //        #region Consignee
        //        {
        //            json += "     \"consignee\": { \n";
        //            json += "       \"name\": { \n";
        //            json += "         \"value\": \"" + objCvwOperations.lstCVarvwOperations[0].ConsigneeName + "\" \n";
        //            json += "       } \n";

        //            json += "       ,\"definedContactDetails\": { \n";
        //            json += "         \"personName\": { \n";
        //            json += "           \"value\": \"" + objCConsigneeContacts.lstCVarContacts[0].Name + "\" \n";
        //            json += "         }, \n";
        //            json += "         \"telephone\": { \n";
        //            json += "           \"value\": \"" + objCConsigneeContacts.lstCVarContacts[0].Phone1 + "\" \n";
        //            json += "         }, \n";
        //            json += "         \"mobileTelephone\":{ \n";
        //            json += "           \"value\": \"" + (objCConsigneeContacts.lstCVarContacts[0].Mobile1 == "0" ? "" : objCConsigneeContacts.lstCVarContacts[0].Mobile1) + "\" \n";
        //            json += "         }, \n";
        //            json += "         \"emailAddress\":{ \n";
        //            json += "           \"value\": \"" + (objCConsigneeContacts.lstCVarContacts[0].Email == "0" ? "" : objCConsigneeContacts.lstCVarContacts[0].Email) + "\" \n";
        //            json += "         } \n";
        //            json += "       } \n";
        //            if (objCConsigneeAddresses.lstCVarvwAddresses.Count > 0)
        //            {
        //                json += "       ,\"postalAddress\": { \n";
        //                json += "         \"postcode\": \"" + objCConsigneeAddresses.lstCVarvwAddresses[0].ZipCode + "\", \n";
        //                json += "         \"streetName\": \"" + objCConsigneeAddresses.lstCVarvwAddresses[0].StreetLine1 + "\", \n";
        //                json += "         \"cityName\": \"" + objCConsigneeAddresses.lstCVarvwAddresses[0].CityName + "\", \n";
        //                json += "         \"countryCode\": \"" + objCConsigneeAddresses.lstCVarvwAddresses[0].CountryCode + "\", \n";
        //                json += "         \"countryName\": \"" + objCConsigneeAddresses.lstCVarvwAddresses[0].CountryName + "\" \n";
        //                json += "       } \n";
        //            }
        //            json += "     }, \n";
        //        }
        //        #endregion Consignee

        //        #region ctoAgent
        //        if (objCvwOperations.lstCVarvwOperations[0].AgentID > 0)
        //        {
        //            json += "     \"ctoAgent\": { \n";
        //            json += "       \"name\": { \n";
        //            json += "         \"value\": \"" + objCvwOperations.lstCVarvwOperations[0].AgentName + "\" \n";
        //            json += "       } \n";
        //            if (objCAgentContacts.lstCVarContacts.Count > 0)
        //            {
        //                json += "       ,\"definedContactDetails\": { \n";
        //                json += "         \"personName\": { \n";
        //                json += "           \"value\": \"" + objCAgentContacts.lstCVarContacts[0].Name + "\" \n";
        //                json += "         }, \n";
        //                json += "         \"telephone\": { \n";
        //                json += "           \"value\": \"" + (objCAgentContacts.lstCVarContacts[0].Phone1 == "0" ? "" : objCAgentContacts.lstCVarContacts[0].Phone1) + "\" \n";
        //                json += "         }, \n";
        //                json += "         \"mobileTelephone\":{ \n";
        //                json += "           \"value\": \"" + (objCAgentContacts.lstCVarContacts[0].Mobile1 == "0" ? "" : objCAgentContacts.lstCVarContacts[0].Mobile1) + "\" \n";
        //                json += "         }, \n";
        //                json += "         \"emailAddress\":{ \n";
        //                json += "           \"value\": \"" + (objCAgentContacts.lstCVarContacts[0].Email == "0" ? "" : objCAgentContacts.lstCVarContacts[0].Email) + "\" \n";
        //                json += "         } \n";
        //                json += "       } \n";
        //            }
        //            if (objCAgentAddresses.lstCVarvwAddresses.Count > 0)
        //            {
        //                json += "       ,\"postalAddress\": { \n";
        //                json += "         \"postcode\": \"" + objCAgentAddresses.lstCVarvwAddresses[0].ZipCode + "\", \n";
        //                json += "         \"streetName\": \"" + objCAgentAddresses.lstCVarvwAddresses[0].StreetLine1 + "\", \n";
        //                json += "         \"cityName\": \"" + objCAgentAddresses.lstCVarvwAddresses[0].CityName + "\", \n";
        //                json += "         \"countryCode\": \"" + objCAgentAddresses.lstCVarvwAddresses[0].CountryCode + "\", \n";
        //                json += "         \"countryName\": \"" + objCAgentAddresses.lstCVarvwAddresses[0].CountryName + "\" \n";
        //                json += "       } \n";
        //            }
        //            json += "     }, \n";
        //        }
        //        #endregion ctoAgent

        //        #region CTO //CTO:Combined Transport Operator [Carrier/Line]
        //        if (objCvwOperations.lstCVarvwOperations[0].ShippingLineName != "0")
        //        {
        //            json += "     \"cto\": { \n";
        //            json += "       \"id\": [{ \n";
        //            json += "         \"value\": \"13a8cc04-8aae-417f-9519-1c79e1fd8dac\", \n";
        //            json += "         \"identificationSchemeAgency\": \"FIATA\" \n"; //????????
        //            json += "       } \n";
        //            json += "       ], \n";
        //            json += "       \"name\": { \n";
        //            json += "         \"value\": \"" + objCvwOperations.lstCVarvwOperations[0].ShippingLineName + "\" \n";
        //            json += "       } \n";
        //            if (objCShippingLineContacts.lstCVarContacts.Count > 0)
        //            {
        //                json += "       ,\"definedContactDetails\": { \n";
        //                json += "         \"personName\": { \n";
        //                json += "           \"value\": \"" + objCShippingLineContacts.lstCVarContacts[0].Name + "\" \n";
        //                json += "         }, \n";
        //                json += "         \"telephone\": { \n";
        //                json += "           \"value\": \"" + (objCShippingLineContacts.lstCVarContacts[0].Phone1 == "0" ? "" : objCShippingLineContacts.lstCVarContacts[0].Phone1) + "\" \n";
        //                json += "         }, \n";
        //                json += "         \"mobileTelephone\":{ \n";
        //                json += "           \"value\": \"" + (objCShippingLineContacts.lstCVarContacts[0].Mobile1 == "0" ? "" : objCShippingLineContacts.lstCVarContacts[0].Mobile1) + "\" \n";
        //                json += "         }, \n";
        //                json += "         \"emailAddress\":{ \n";
        //                json += "           \"value\": \"" + (objCShippingLineContacts.lstCVarContacts[0].Email == "0" ? "" : objCShippingLineContacts.lstCVarContacts[0].Email) + "\" \n";
        //                json += "         } \n";
        //                json += "       } \n";
        //            }
        //            if (objCShippingLineAddresses.lstCVarvwAddresses.Count > 0)
        //            {
        //                json += "       ,\"postalAddress\": { \n";
        //                json += "         \"postcode\": \"" + objCShippingLineAddresses.lstCVarvwAddresses[0].ZipCode + "\", \n";
        //                json += "         \"streetName\": \"" + objCShippingLineAddresses.lstCVarvwAddresses[0].StreetLine1 + "\", \n";
        //                json += "         \"cityName\": \"" + objCShippingLineAddresses.lstCVarvwAddresses[0].CityName + "\", \n";
        //                json += "         \"countryCode\": \"" + objCShippingLineAddresses.lstCVarvwAddresses[0].CountryCode + "\", \n";
        //                json += "         \"countryName\": \"" + objCShippingLineAddresses.lstCVarvwAddresses[0].CountryName + "\" \n";
        //                json += "       } \n";
        //            }
        //            json += "     }, \n";
        //        }
        //        #endregion CTO

        //        #region Notify
        //        {
        //            json += "     \"notifiedParty\": [ \n";
        //            json += "       { \n";
        //            json += "         \"id\": [{ \n";
        //            json += "           \"value\": \"13a8cc04-8aae-417f-9519-1c79e1fd8dac\", \n"; //??????????
        //            json += "           \"identificationSchemeAgency\": \"SPEDLOGSWISS\" \n"; //??????????
        //            json += "         } \n";
        //            json += "         ], \n";
        //            json += "         \"name\": { \n";
        //            json += "           \"value\": \"" + (objCvwOperations.lstCVarvwOperations[0].Notify1Name == "0" ? "" : objCvwOperations.lstCVarvwOperations[0].Notify1Name) + "\" \n";
        //            json += "         }, \n";
        //            json += "         \"definedContactDetails\": { \n";
        //            json += "           \"personName\": { \n";
        //            json += "             \"value\": \"" + "" + "\" \n";
        //            json += "           }, \n";
        //            json += "           \"telephone\":{\"value\": \"" + "" + "\"}, \n";
        //            json += "           \"mobileTelephone\":{\"value\": \"" + "" + "\"}, \n";
        //            json += "           \"emailAddress\":{\"value\": \"" + "" + "\"} \n";
        //            json += "         } \n";
        //            //json += "         ,\"postalAddress\": { \n";
        //            //json += "           \"postcode\": \"" + "" + "\", \n";
        //            //json += "           \"streetName\": \"" + (objCvwOperations.lstCVarvwOperations[0].Notify1Address == "0" ? "" : objCvwOperations.lstCVarvwOperations[0].Notify1Address) + "\", \n";
        //            //json += "           \"cityName\": \"MENINGIE WEST\", \n";
        //            //json += "           \"countryCode\": \"AU\", \n";
        //            //json += "           \"countryName\": \"Australia\" \n";
        //            //json += "         } \n";
        //            json += "       } \n";
        //            json += "     ], \n";
        //        }
        //        #endregion Notify

        //        #region carrierAcceptanceLocation
        //        json += "     \"carrierAcceptanceLocation\": { \n";
        //        json += "       \"id\": \"" + objCvwOperations.lstCVarvwOperations[0].POLCountryCode + "\", \n"; //???????????????
        //        json += "       \"name\": { \n";
        //        json += "         \"value\": \"" + objCvwOperations.lstCVarvwOperations[0].POLName + "\" \n";
        //        json += "       } \n";
        //        json += "     }, \n";
        //        #endregion carrierAcceptanceLocation

        //        #region consigneeReceiptLocation
        //        json += "     \"consigneeReceiptLocation\": { \n";
        //        json += "       \"id\": \"" + objCvwOperations.lstCVarvwOperations[0].PODCode + "\", \n"; //???????????????
        //        json += "       \"name\": { \n";
        //        json += "         \"value\": \"" + objCvwOperations.lstCVarvwOperations[0].PODName + "\" \n";
        //        json += "       } \n";
        //        json += "     }, \n";
        //        #endregion consigneeReceiptLocation


        //        json += "     \"numberOfPackages\": " + objCvwOperations.lstCVarvwOperations[0].NumberOfPackages + " \n";

        //        #region add this in loop for Marks and numbers line by line 
        //        if (objCvwOperationContainersAndPackages.lstCVarvwOperationContainersAndPackages.Count > 0)
        //        {
        //            json += "     ,\"includedConsignmentItem\": [";
        //            for (int i = 0; i < objCvwOperationContainersAndPackages.lstCVarvwOperationContainersAndPackages.Count; i++)
        //            {
        //                json += (i == 0 ? "" : ",");
        //                json += "               { \n";
        //                json += "                   \"goodsTypeCode\": { \n";
        //                json += "                     \"value\": \"" + objCvwOperationContainersAndPackages.lstCVarvwOperationContainersAndPackages[i].DescriptionOfGoods.Replace("\n", ", ") + "\" \n";
        //                json += "                   }, \n";
        //                json += "                   \"grossWeight\": { \n";
        //                json += "                     \"value\": " + objCvwOperationContainersAndPackages.lstCVarvwOperationContainersAndPackages[i].GrossWeight + ", \n";
        //                json += "                     \"unit\": \"kg\" \n";
        //                json += "                   }, \n";
        //                json += "                   \"grossVolume\": { \n";
        //                json += "                     \"value\": " + objCvwOperationContainersAndPackages.lstCVarvwOperationContainersAndPackages[i].Volume + ", \n";
        //                json += "                     \"unit\": \"cbm\" \n";
        //                json += "                   } \n";
        //                json += "                   ,\"cargoNatureIdentification\": { \n";
        //                json += "                     \"identificationText\": [ \n";
        //                json += "                       { \n";
        //                json += "                         \"value\": \"" + objCvwOperationContainersAndPackages.lstCVarvwOperationContainersAndPackages[i].PackageTypeNameOnContainer + "\" \n";
        //                json += "                       } \n";
        //                json += "                     ] \n";
        //                json += "                   }, \n";
        //                json += "                   \"transportPackage\": [{ \n";
        //                json += "                     \"itemQuantity\": " + objCvwOperationContainersAndPackages.lstCVarvwOperationContainersAndPackages[i].NumberOfPackagesOnContainer + ", \n";
        //                json += "                     \"shippingMarks\": [\"" + objCvwOperationContainersAndPackages.lstCVarvwOperationContainersAndPackages[i].MarksAndNumbers.Replace("\n", "\",\"") + "\"], \n";
        //                json += "                     \"typeText\": \"" + "" + "\" \n";
        //                json += "                   } \n";
        //                json += "                 ] \n";
        //                json += "               }";
        //            }
        //            json += "    ] \n";
        //        }
        //        #endregion add this in loop for Marks and numbers line by line









        //        //json += "     ,\"applicableServiceCharge\": [{ \n";
        //        //json += "       \"paymentArrangementCode\": { \n";
        //        //json += "         \"value\": \"A\", \n";
        //        //json += "         \"agency\": \"UN/EDIFACT\" \n";
        //        //json += "       }, \n";
        //        //json += "       \"paymentPlace\": { \n";
        //        //json += "         \"name\": { \n";
        //        //json += "           \"value\": \"Kastanienallee 12, 26579 Hinte, Niedersachsen, Germany\" \n";
        //        //json += "         }, \n";
        //        //json += "         \"id\": \"9501101020023\" \n";
        //        //json += "       } \n";
        //        //json += "     }] \n";
        //        json += "     ,\"loadingBaseportLocation\": { \n";
        //        json += "       \"countryCode\":\"" + objCvwOperations.lstCVarvwOperations[0].POLCountryCode + "\", \n";
        //        json += "       \"id\": \"" + objCvwOperations.lstCVarvwOperations[0].POLCode + "\", \n"; //??????????????
        //        json += "       \"name\": { \n";
        //        json += "         \"value\": \"" + objCvwOperations.lstCVarvwOperations[0].POLName + "\" \n";
        //        json += "       } \n";
        //        json += "     } \n";
        //        json += "     ,\"unloadingBaseportLocation\": { \n";
        //        json += "       \"countryCode\": \"" + objCvwOperations.lstCVarvwOperations[0].PODCountryCode + "\", \n";
        //        json += "       \"id\": \"" + objCvwOperations.lstCVarvwOperations[0].PODCode + "\", \n"; //??????????????
        //        json += "       \"name\": { \n";
        //        json += "         \"value\": \"" + objCvwOperations.lstCVarvwOperations[0].PODName + "\" \n";
        //        json += "       } \n";
        //        json += "     } \n";
        //        //json += "     ,\"mainCarriageTransportMovement\": [{ \n";
        //        //json += "       \"typeCode\": { \n";
        //        //json += "         \"value\": \"503\" \n";
        //        //json += "       } \n";
        //        //json += "       ,\"typeText\": { \n";
        //        //json += "         \"value\": \"Wood chips vessel\" \n";
        //        //json += "       } \n";
        //        //json += "     }] \n";
        //        //json += "     ,\"declaredValueForCarriageAmount\": { \n";
        //        //json += "       \"value\": 10000000, \n";
        //        //json += "       \"currency\": \"EUR\" \n";
        //        //json += "     } \n";
        //        //json += "     ,\"totalChargeAmount\": { \n";
        //        //json += "       \"value\": 15000000, \n";
        //        //json += "       \"currency\": \"EUR\" \n";
        //        //json += "     } \n";
        //        json += "   } \n";
        //        json += " } \n";
        //        #endregion supply_chain_consignment

        //        //File.WriteAllText(@"D:\eFBL.json", json);
        //    }

        //    #endregion Getting Data And Validting

        //    #region Call WebAPI
        //    if (pReturnedMessage == "") //Mandatory Data is complete
        //    {
        //        var Global_access_token = "";
        //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //        ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

        //        string tokenUrl = $"https://keycloak.kapsule-eu-uat.komgo.io/auth/realms/fiata/protocol/openid-connect/token";
        //        var req = new HttpRequestMessage(HttpMethod.Post, tokenUrl);

        //        req.Content = new FormUrlEncodedContent(new Dictionary<string, string>
        //        {
        //            ["username"] = "istnetworks-staging@efbl.fiata.org",
        //            ["password"] = "Luminance-Playgroup-Donor8-Flagman-Exchange",
        //            ["client_id"] = "fiata",
        //            ["grant_type"] = "password"
        //        });

        //        using (var client = new HttpClient())
        //        {
        //            var res = await client.SendAsync(req);

        //            string returnedJSON = await res.Content.ReadAsStringAsync();
        //            var jsonAccess = JsonConvert.DeserializeObject<MyDetail>(returnedJSON);

        //            var access_token = jsonAccess.access_token;
        //            var expires_in = jsonAccess.expires_in;
        //            var scope = jsonAccess.scope;
        //            var token_type = jsonAccess.token_type;

        //            Global_access_token = access_token;
        //        }
        //        ////////////////Send details////////////////////
        //        string integrationsFiataUrl = $"https://api.kapsule-eu-uat.komgo.io/api/trakk/v0/integrations/fiata/fbl-json?softwareProviderId=e651b830-a153-4bb4-ac13-2583720bff32";

        //        string jsonData = json;  //" { \r\n   \"exchanged_document\": { \r\n     \"documentStatus\": { \r\n       \"value\": \"TBD\" \r\n     }, \r\n     \"issueDateTime\": { \r\n       \"value\": \"2022-04-27T05:50:00.000\", \r\n       \"format\": \"YYYY-MM-DDThh:mm:ss.sss\" \r\n     }, \r\n     \"firstSignatoryAuthentication\": { \r\n       \"actualDateTime\": { \r\n         \"value\": \"2022-04-27T05:50:00.000\", \r\n         \"format\": \"YYYY-MM-DDThh:mm:ss.sss\" \r\n       }, \r\n       \"id\": \"12345\", \r\n       \"statement\": { \r\n         \"value\": \"\" \r\n       } \r\n     }, \r\n     \"issueLocation\": { \r\n       \"id\": \"AFQLT\", \r\n       \"countryCode\": \"AF\", \r\n       \"name\": { \r\n         \"value\": \"QALAT\" \r\n       } \r\n     }, \r\n     \"originalIssuedQuantity\": \"1\" \r\n   }, \r\n   \"supply_chain_consignment\": { \r\n     \"cargoInsuranceNotProvidedByCTO\": true, \r\n     \"consignor\": { \r\n       \"name\": { \r\n         \"value\": \"AVL GROUP\" \r\n       }, \r\n       \"definedContactDetails\": { \r\n         \"personName\": { \r\n           \"value\": \"M. ABDELAZIZ\" \r\n         }, \r\n         \"telephone\": { \r\n           \"value\": \"038372727\" \r\n         }, \r\n         \"mobileTelephone\":{ \r\n           \"value\": \"03767373\" \r\n         }, \r\n         \"emailAddress\":{ \r\n           \"value\": \"AVL@AVLGROUP.COM\" \r\n         } \r\n       }, \r\n       \"postalAddress\": { \r\n         \"postcode\": \"21321\", \r\n         \"streetName\": \"STREET 1\", \r\n         \"cityName\": \"BARBUDA\", \r\n         \"countryCode\": \"AG\", \r\n         \"countryName\": \"ANTIGUA AND BARBUDA\" \r\n       } \r\n     }, \r\n     \"consignee\": { \r\n       \"name\": { \r\n         \"value\": \"ALFA M.GSS\" \r\n       } \r\n       ,\"definedContactDetails\": { \r\n         \"personName\": { \r\n           \"value\": \"CONTACT NAME\" \r\n         }, \r\n         \"telephone\": { \r\n           \"value\": \"\" \r\n         }, \r\n         \"mobileTelephone\":{ \r\n           \"value\": \"\" \r\n         }, \r\n         \"emailAddress\":{ \r\n           \"value\": \"\" \r\n         } \r\n       } \r\n     }, \r\n     \"ctoAgent\": { \r\n       \"name\": { \r\n         \"value\": \"ABC AGENCY\" \r\n       } \r\n       ,\"definedContactDetails\": { \r\n         \"personName\": { \r\n           \"value\": \"ABC\" \r\n         }, \r\n         \"telephone\": { \r\n           \"value\": \"89867787\" \r\n         }, \r\n         \"mobileTelephone\":{ \r\n           \"value\": \"86765757\" \r\n         }, \r\n         \"emailAddress\":{ \r\n           \"value\": \"ABC@AGENCY.COM\" \r\n         } \r\n       } \r\n       ,\"postalAddress\": { \r\n         \"postcode\": \"\", \r\n         \"streetName\": \"232 DFDFDF\", \r\n         \"cityName\": \"BENISAF\", \r\n         \"countryCode\": \"DZ\", \r\n         \"countryName\": \"ALGERIA\" \r\n       } \r\n     }, \r\n     \"cto\": { \r\n       \"id\": [{ \r\n         \"value\": \"13a8cc04-8aae-417f-9519-1c79e1fd8dac\", \r\n         \"identificationSchemeAgency\": \"FIATA\" \r\n       } \r\n       ], \r\n       \"name\": { \r\n         \"value\": \"ABC CONTAINERLINE N.V.\" \r\n       } \r\n       ,\"definedContactDetails\": { \r\n         \"personName\": { \r\n           \"value\": \"ABC SHIPPING LNE CONTACT\" \r\n         }, \r\n         \"telephone\": { \r\n           \"value\": \"\" \r\n         }, \r\n         \"mobileTelephone\":{ \r\n           \"value\": \"\" \r\n         }, \r\n         \"emailAddress\":{ \r\n           \"value\": \"\" \r\n         } \r\n       } \r\n       ,\"postalAddress\": { \r\n         \"postcode\": \"\", \r\n         \"streetName\": \"\", \r\n         \"cityName\": \"QALAT\", \r\n         \"countryCode\": \"AF\", \r\n         \"countryName\": \"AFGHANISTAN\" \r\n       } \r\n     }, \r\n     \"notifiedParty\": [ \r\n       { \r\n         \"id\": [{ \r\n           \"value\": \"13a8cc04-8aae-417f-9519-1c79e1fd8dac\", \r\n           \"identificationSchemeAgency\": \"SPEDLOGSWISS\" \r\n         } \r\n         ], \r\n         \"name\": { \r\n           \"value\": \"\" \r\n         }, \r\n         \"definedContactDetails\": { \r\n           \"personName\": { \r\n             \"value\": \"\" \r\n           }, \r\n           \"telephone\":{\"value\": \"\"}, \r\n           \"mobileTelephone\":{\"value\": \"\"}, \r\n           \"emailAddress\":{\"value\": \"\"} \r\n         } \r\n       } \r\n     ], \r\n     \"carrierAcceptanceLocation\": { \r\n       \"id\": \"AF\", \r\n       \"name\": { \r\n         \"value\": \"QALAT\" \r\n       } \r\n     }, \r\n     \"consigneeReceiptLocation\": { \r\n       \"id\": \"DZALG\", \r\n       \"name\": { \r\n         \"value\": \"ALGER (ALGIERS)\" \r\n       } \r\n     }, \r\n     \"numberOfPackages\": 0, \r\n     \"includedConsignmentItem\": [{ \r\n       \"goodsTypeCode\": { \r\n         \"value\": \"0\" \r\n       }, \r\n       \"grossWeight\": { \r\n         \"value\": 0.00000, \r\n         \"unit\": \"kg\" \r\n       }, \r\n       \"grossVolume\": { \r\n         \"value\": 0.00000, \r\n         \"unit\": \"cbm\" \r\n       } \r\n       ,\"cargoNatureIdentification\": { \r\n         \"identificationText\": [ \r\n           { \r\n             \"value\": \"0\" \r\n           } \r\n         ] \r\n       }, \r\n       \"transportPackage\": [{ \r\n         \"itemQuantity\": 0, \r\n         \"shippingMarks\": [\"\"], \r\n         \"typeText\": \"\" \r\n       } \r\n       ] \r\n     }] \r\n     ,\"loadingBaseportLocation\": { \r\n       \"countryCode\":\"AF\", \r\n       \"id\": \"AFQLT\", \r\n       \"name\": { \r\n         \"value\": \"QALAT\" \r\n       } \r\n     } \r\n     ,\"unloadingBaseportLocation\": { \r\n       \"countryCode\": \"DZ\", \r\n       \"id\": \"DZALG\", \r\n       \"name\": { \r\n         \"value\": \"ALGER (ALGIERS)\" \r\n       } \r\n     } \r\n   } \r\n } \r\n";

        //        var httpWebRequest = (HttpWebRequest)WebRequest.Create(integrationsFiataUrl);
        //        httpWebRequest.ContentType = "application/json";
        //        httpWebRequest.Method = "POST";
        //        httpWebRequest.Headers.Add("Authorization", ("Bearer " + Global_access_token));
        //        httpWebRequest.Headers.Add("X-Alias-ID", "fiata");

        //        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        //        {
        //            streamWriter.Write(jsonData);
        //        }

        //        try
        //        {
        //            WebResponse myResp = httpWebRequest.GetResponse();
        //            pFileName = myResp.Headers.Get("Content-Disposition").Split('=')[1];

        //            using (Stream stream = myResp.GetResponseStream())
        //            using (MemoryStream ms = new MemoryStream())
        //            {
        //                int count = 0;
        //                do
        //                {
        //                    byte[] buf = new byte[1024];
        //                    count = stream.Read(buf, 0, 1024);
        //                    ms.Write(buf, 0, count);
        //                }
        //                while (stream.CanRead && count > 0);
        //                pdfInBytes = ms.ToArray();
        //            }


        //            #region Save File
        //            COperations objCOperations = new COperations();
        //            objCOperations.UpdateList("eFBLStatus=10, eFBLID=N'" + pFileName.Split('.')[0] + "' WHERE ID=" + objCvwOperations.lstCVarvwOperations[0].ID);

        //            var filename = pFileName;
        //            string pathString = Path.Combine(mapPath, filename);
        //            File.WriteAllBytes(pathString, pdfInBytes);
        //            #endregion Save File

        //        }
        //        catch (Exception ex)
        //        {
        //            pReturnedMessage = ex.Message;
        //        }
        //    }
        //    #endregion Call WebAPI

        //    return new object[] {
        //        pReturnedMessage
        //        , pdfInBytes
        //        , pFileName
        //    };
        //}

        //public class MyDetail
        //{
        //    public string access_token { get; set; }
        //    public string expires_in { get; set; }
        //    public string token_type { get; set; }
        //    public string scope { get; set; }
        //}

        #endregion delete

        [HttpGet, HttpPost]
        public Object[] LoadAll(string pWhereClause)
        {
            int _RowCount = 0;
            int constPaymentTypeCheque = 20;
            //sendEmailViaWebApi();
            Exception checkException = new Exception();
            //PageDirectly objPageDirectly = new PageDirectly();
            //string _PageDirectly = objPageDirectly._PageDirectly;

            CUsers objCUsers = new CUsers();
            objCUsers.UpdateList("HeartBeatDate=GETDATE() WHERE ID=" + WebSecurity.CurrentUserId);

            CvwDefaults objCvwDefaults = new CvwDefaults();
            objCvwDefaults.GetListPaging(1, 1, pWhereClause, "ID", out _RowCount);

            CvwUsers objCvwUsers = new CvwUsers();
            objCvwUsers.GetList(" WHERE ID = " + WebSecurity.CurrentUserId); //i am sure i ve 1 row isa
            if (objCvwUsers.lstCVarvwUsers.Count > 0)
            {
                objCvwDefaults.lstCVarvwDefaults[0].UserBranchID = objCvwUsers.lstCVarvwUsers[0].BranchID;
            }
            CvwBranches objCvwBranches = new CvwBranches();
            //objCvwBranches.GetList("WHERE 1=1 ORDER BY Name");
            objCvwBranches.GetList("WHERE isnull(isDepartement, 0)=0 ORDER BY Name");


            

            CvwCurrencies objCvwCurrencies = new CvwCurrencies();
            objCvwCurrencies.GetList("WHERE 1=1 ORDER BY Code");

            #region CRM Action Alarm
            //CvwCRM_FollowUps objCvwCRM_FollowUps = new CvwCRM_FollowUps();
            //objCvwCRM_FollowUps.GetList("WHERE IsAlarmSent=0 AND DATEDIFF(DAY, GETDATE(), NextStepDate)<=AlarmDays" + " \n"
            //    + " AND DATEDIFF(DAY, GETDATE(), NextStepDate)>=0 AND SalesRep=" + WebSecurity.CurrentUserId);
            //{
            //    for (int i = 0; i < objCvwCRM_FollowUps.lstCVarvwCRM_FollowUps.Count; i++)
            //    {
            //        CVarEmail objCVarEmail = new CVarEmail();
            //        objCVarEmail.Subject = "Action For " + objCvwCRM_FollowUps.lstCVarvwCRM_FollowUps[i].Name;
            //        objCVarEmail.Body = "Action: '" + objCvwCRM_FollowUps.lstCVarvwCRM_FollowUps[i].Action + " \n"
            //            + "Sales Lead: " + objCvwCRM_FollowUps.lstCVarvwCRM_FollowUps[i].Name + " \n"
            //            + "Action Date: " + objCvwCRM_FollowUps.lstCVarvwCRM_FollowUps[i].ActionDate.ToShortDateString() + " \n";
            //        objCVarEmail.QuotationRouteID = 0;
            //        objCVarEmail.SenderUserID = objCvwCRM_FollowUps.lstCVarvwCRM_FollowUps[i].ModifatorUserID;
            //        objCVarEmail.SendingDate = DateTime.Now;
            //        objCEmail.lstCVarEmail.Add(objCVarEmail);
            //        Exception checkException_temp = objCEmail.SaveMethod(objCEmail.lstCVarEmail);
            //        if (checkException_temp == null) //add to EmailReceivers
            //        {
            //            CVarEmailReceiver objCVarEmailReceiver = new CVarEmailReceiver();
            //            objCVarEmailReceiver.EmailID = objCVarEmail.ID;
            //            objCVarEmailReceiver.ReceiverUserID = objCvwCRM_FollowUps.lstCVarvwCRM_FollowUps[i].SalesRep;
            //            objCVarEmailReceiver.ReceivingDate = DateTime.Parse("01-01-1900");
            //            objCVarEmailReceiver.IsAlarm = true;

            //            objCEmailReceiver.lstCVarEmailReceiver.Add(objCVarEmailReceiver);
            //            checkException_temp = objCEmailReceiver.SaveMethod(objCEmailReceiver.lstCVarEmailReceiver);
            //            if (checkException_temp == null)
            //            {
            //                CCRM_FollowUp objCCRM_FollowUp = new CCRM_FollowUp();
            //                objCCRM_FollowUp.UpdateList("IsAlarmSent=1 WHERE ID=" + objCvwCRM_FollowUps.lstCVarvwCRM_FollowUps[i].ID);
            //            }

            //        }
            //    }
            //}
            #endregion CRM Action Alarm

          
            #region Send Department Notifications
            //CvwServiceDepartment objCvwServiceDepartment = new CvwServiceDepartment();
            ////ReThink: hint Select On by Dates from vwOperations --> select from Service Department that dates-->send to users belonging to that department
            //checkException = objCvwServiceDepartment.GetListPaging(999999, 1, "WHERE DepartmentID=" + objCvwUsers.lstCVarvwUsers[0].DepartmentID, "NotificationDateOption,ViewOrder", out _RowCount);
            //for (int i = 0; i < objCvwServiceDepartment.lstCVarvwServiceDepartment.Count; i++)
            //{
            //    //TODO: Send Email to all next role deparment users
            //}
            #endregion Send Department Notifications

            CvwEmailReceiver objCvwEmailReceiver = new CvwEmailReceiver();
            objCvwEmailReceiver.GetList("WHERE IsAlarm=1 AND ReceiverUserID=" + WebSecurity.CurrentUserId.ToString() + " ORDER BY ID DESC");

            CvwAccPayment objCvwUnderCollectCheques = new CvwAccPayment();
            objCvwUnderCollectCheques.GetListPaging(1000, 1, "WHERE IsDeleted=0 AND PaymentTypeID=" + constPaymentTypeCheque.ToString() + " AND IsApproved=0 AND IsRefused=0 AND CAST(DueDate AS date) <= GETDATE()", "DueDate", out _RowCount);

            CPorts objCAirPorts = new CPorts();
            objCAirPorts.GetList("WHEHRE CountryID=" + objCvwDefaults.lstCVarvwDefaults[0].DefaultCountryID + " AND IsAir=1 ORDER By Name");

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new Object[] {
                new JavaScriptSerializer().Serialize(objCvwDefaults.lstCVarvwDefaults[0]) //data[0]
                , new JavaScriptSerializer().Serialize(objCvwBranches.lstCVarvwBranches) //data[1]
                , new JavaScriptSerializer().Serialize(objCvwCurrencies.lstCVarvwCurrencies) //data[2]
                , WebSecurity.CurrentUserId.ToString() //data[3]
                , objCvwUsers.lstCVarvwUsers.Count > 0 ? objCvwUsers.lstCVarvwUsers[0].Name : "" //data[4]
                , serializer.Serialize(objCvwEmailReceiver.lstCVarvwEmailReceiver) //data[5]
                , serializer.Serialize(objCvwUnderCollectCheques.lstCVarvwAccPayment) //data[6]
                , serializer.Serialize(objCAirPorts.lstCVarPorts) //data[7]
                , serializer.Serialize(objCvwUsers.lstCVarvwUsers[0]) //data[8]
            };
        }
        
        [HttpPost]
        public Object[] Update([FromBody] UpdateDefaultsData DefaultObject)
        {
            bool _result = false;
            string pUpdateClause = "";

            int _RowCount = 0;
            CDefaults objCDefaults_temp = new CDefaults();
            objCDefaults_temp.GetListPaging(999999, 1, "WHERE 1=1", "ID", out _RowCount);
            var _DefaultEmailPassword = objCDefaults_temp.lstCVarDefaults[0].Email_Password;



            pUpdateClause = " CompanyName = N'" + DefaultObject.pCompanyName + "'";
            pUpdateClause += " , CompanyLocalName = N'" + DefaultObject.pCompanyLocalName.ToString() + "' ";
            pUpdateClause += " , BranchID = " + DefaultObject.pBranchID.ToString();
            pUpdateClause += " , CurrencyID = " + DefaultObject.pCurrencyID.ToString();
            pUpdateClause += " , ForeignCurrencyID = " + DefaultObject.pForeignCurrencyID.ToString();

            pUpdateClause += " , AddressLine1 = " + (DefaultObject.pAddressLine1 == "" ? " NULL " : "N'" + DefaultObject.pAddressLine1 + "'");
            pUpdateClause += " , AddressLine2 = " + (DefaultObject.pAddressLine2 == "" ? " NULL " : "N'" + DefaultObject.pAddressLine2 + "'");
            pUpdateClause += " , AddressLine3 = " + (DefaultObject.pAddressLine3 == "" ? " NULL " : "N'" + DefaultObject.pAddressLine3 + "'");
            pUpdateClause += " , AddressLine4 = " + (DefaultObject.pAddressLine4 == "" ? " NULL " : "N'" + DefaultObject.pAddressLine4 + "'");
            pUpdateClause += " , AddressLine5 = " + (DefaultObject.pAddressLine5 == "" ? " NULL " : "N'" + DefaultObject.pAddressLine5 + "'");
            pUpdateClause += " , Email = " + (DefaultObject.pEmail == "" ? " NULL " : "N'" + DefaultObject.pEmail + "'");
            pUpdateClause += " , Website = " + (DefaultObject.pWebsite == "" ? " NULL " : "N'" + DefaultObject.pWebsite + "'");
            pUpdateClause += " , Phones = " + (DefaultObject.pPhones == "" ? " NULL " : "N'" + DefaultObject.pPhones + "'");
            pUpdateClause += " , Faxes = " + (DefaultObject.pFaxes == "" ? " NULL " : "N'" + DefaultObject.pFaxes + "'");

            pUpdateClause += " , BankName = " + (DefaultObject.pBankName == "" ? " NULL " : "N'" + DefaultObject.pBankName + "'");
            pUpdateClause += " , AccountName = " + (DefaultObject.pAccountName == "" ? " NULL " : "N'" + DefaultObject.pAccountName + "'");
            pUpdateClause += " , BankAddress = " + (DefaultObject.pBankAddress == "" ? " NULL " : "N'" + DefaultObject.pBankAddress + "'");
            pUpdateClause += " , SwiftCode = " + (DefaultObject.pSwiftCode == "" ? " NULL " : "N'" + DefaultObject.pSwiftCode + "'");
            pUpdateClause += " , AccountNumber = " + (DefaultObject.pAccountNumber == "" ? " NULL " : "N'" + DefaultObject.pAccountNumber + "'");
            pUpdateClause += " , TaxNumber = " + (DefaultObject.pTaxNumber == "" ? " NULL " : "N'" + DefaultObject.pTaxNumber + "'");
            pUpdateClause += " , CommericalRegNo = " + (DefaultObject.pCommericalRegNo == "" ? " NULL " : "N'" + DefaultObject.pCommericalRegNo + "'");
            pUpdateClause += " , VatIDNo = " + (DefaultObject.pVatIDNo == "" ? " NULL " : "N'" + DefaultObject.pVatIDNo + "'");

            pUpdateClause += " , InvoiceLeftPosition = " + (DefaultObject.pInvoiceLeftPosition == "0" ? " NULL " : "N'" + DefaultObject.pInvoiceLeftPosition + "'");
            pUpdateClause += " , InvoiceLeftSignature = " + (DefaultObject.pInvoiceLeftSignature == "0" ? " NULL " : "N'" + DefaultObject.pInvoiceLeftSignature + "'");
            pUpdateClause += " , InvoiceMiddlePosition = " + (DefaultObject.pInvoiceMiddlePosition == "0" ? " N'' " : "N'" + DefaultObject.pInvoiceMiddlePosition + "'");
            pUpdateClause += " , InvoiceMiddleSignature = " + (DefaultObject.pInvoiceMiddleSignature == "0" ? " N'' " : "N'" + DefaultObject.pInvoiceMiddleSignature + "'");
            pUpdateClause += " , InvoiceRightPosition = " + (DefaultObject.pInvoiceRightPosition == "0" ? " N'' " : "N'" + DefaultObject.pInvoiceRightPosition + "'");
            pUpdateClause += " , InvoiceRightSignature = " + (DefaultObject.pInvoiceRightSignature == "0" ? " N'' " : "N'" + DefaultObject.pInvoiceRightSignature + "'");

            pUpdateClause += " , ImportOceanDays = " + DefaultObject.pImportOceanDays.ToString();
            pUpdateClause += " , ImportAirDays   = " + DefaultObject.pImportAirDays.ToString();
            pUpdateClause += " , ImportInlandDays   = " + DefaultObject.pImportInlandDays.ToString();
            pUpdateClause += " , ExportOceanDays = " + DefaultObject.pExportOceanDays.ToString();
            pUpdateClause += " , ExportAirDays   = " + DefaultObject.pExportAirDays.ToString();
            pUpdateClause += " , ExportInlandDays   = " + DefaultObject.pExportInlandDays.ToString();
            pUpdateClause += " , DomesticOceanDays = " + DefaultObject.pDomesticOceanDays.ToString();
            pUpdateClause += " , DomesticAirDays   = " + DefaultObject.pDomesticAirDays.ToString();
            pUpdateClause += " , DomesticInlandDays   = " + DefaultObject.pDomesticInlandDays.ToString();

            pUpdateClause += " , SmallBusinessBelow   = " + DefaultObject.pSmallBusinessBelow.ToString();
            pUpdateClause += " , MediumBusinessBelow   = " + DefaultObject.pMediumBusinessBelow.ToString();
            //pUpdateClause += " , IsDepartmentOption = " + objCDefaults_temp.lstCVarDefaults[0].UnEditableCompanyName;

            if (DefaultObject.pEmail_Password == "0" || DefaultObject.pEmail_Password == "" || DefaultObject.pEmail_Password == null)
            {
                pUpdateClause += " , Email_Password = " + ((_DefaultEmailPassword == "0" || _DefaultEmailPassword == "" || _DefaultEmailPassword == null) ? " N'' " : "N'" + _DefaultEmailPassword + "'");
            }
            else
            {
                pUpdateClause += " , Email_Password = " + ((DefaultObject.pEmail_Password == "0" || DefaultObject.pEmail_Password == "" || DefaultObject.pEmail_Password == null) ? " N'' " : "N'" + CEncryptDecrypt.Encrypt(DefaultObject.pEmail_Password, true) + "'");
            }






            pUpdateClause += " , Email_DisplayName = " + (DefaultObject.pEmail_DisplayName == "0" ? " N'' " : "N'" + DefaultObject.pEmail_DisplayName + "'");
            pUpdateClause += " , Email_Host = " + (DefaultObject.pEmail_Host == "0" ? " N'' " : "N'" + DefaultObject.pEmail_Host + "'");
            pUpdateClause += " , Email_Port = " + (DefaultObject.pEmail_Port == "0" ? " 0 " : "" + DefaultObject.pEmail_Port + "");
            pUpdateClause += " , Email_IsSSL = " + (DefaultObject.pEmail_IsSSL == "0" ? " 0 " : " " + DefaultObject.pEmail_IsSSL + " ");
            pUpdateClause += " , ShowUserSalesmen = " + (DefaultObject.pShowUserSalesmen == "0" ? " 0 " : " " + DefaultObject.pShowUserSalesmen + " ");
            pUpdateClause += " , IsAddChargeAuto = " + (DefaultObject.pIsAddChargeAuto ? "1" : "0");

            pUpdateClause += " , Email_Header = " + (DefaultObject.pEmail_Header == "0" ? " N'' " : "N'" + DefaultObject.pEmail_Header + "'");
            pUpdateClause += " , Email_Footer = " + (DefaultObject.pEmail_Footer == "0" ? " N'' " : "N'" + DefaultObject.pEmail_Footer + "'");
            pUpdateClause += " , ModificatorUserID = " + WebSecurity.CurrentUserId.ToString();
            pUpdateClause += " , ModificationDate = GETDATE() ";

            pUpdateClause += " WHERE ID = " + DefaultObject.pID.ToString();

            CDefaults objCDefaults = new CDefaults();
            Exception checkException = objCDefaults.UpdateList(pUpdateClause);

            if (checkException != null) // an exception is caught in the model
            {
                if (checkException.Message.Contains("UNIQUE"))
                    _result = false;
            }
            else //not unique
            {
                //CCurrencies objCCurrencies = new CCurrencies();
                //pUpdateClause = "";
                //pUpdateClause += " CurrentExchangeRate = 1 ";
                //pUpdateClause += " , ModificatorUserID = " + WebSecurity.CurrentUserId.ToString();
                //pUpdateClause += " , ModificationDate = GETDATE() ";
                //pUpdateClause += " WHERE ID = " + pCurrencyID;
                //objCCurrencies.UpdateList(pUpdateClause);
                CCurrencyDetails objCCurrencyDetails = new CCurrencyDetails();
                pUpdateClause = "";
                pUpdateClause += " ExchangeRate = 1 ";
                pUpdateClause += " , FromDate='19800101' ";
                pUpdateClause += " , ToDate='2099-12-31 23:59:59.000' ";
                pUpdateClause += " WHERE Currency_ID = " + DefaultObject.pCurrencyID;
                objCCurrencyDetails.UpdateList(pUpdateClause);
                _result = true;
            }
            return new Object[] {
                _result //data[0]

            };
        }

        [HttpPost, HttpGet]
        public object[] AutomaticEmail(int pDummyForAutomaticEmail)
        {
            var constCustomerPartnerTypeID = 1;
            var constAgentPartnerTypeID = 2;
            
            Exception checkException = null;
            int _RowCount = 0;
            int constAutoEmailHalfMonthly = 10;
            int constAutoEmailMonthly = 20;
            CvwDefaults objCvwDefaults = new CvwDefaults();
            checkException = objCvwDefaults.GetListPaging(999999, 1, "WHERE 1=1", "ID", out _RowCount);

            if (objCvwDefaults.lstCVarvwDefaults[0].IsDepartmentOption && objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName != "GBL")
            {
                #region Customers Aging/InvoiceReport
                CCustomers objCCustomers = new CCustomers();
               // objCCustomers.lstCVarCustomers[0].
                CAutoEmailSentCustomer objCAutoEmailSentCustomer = new CAutoEmailSentCustomer();
                checkException = objCCustomers.GetListPaging(999999, 1, "WHERE Email<>'' AND Email IS NOT NULL " + " \n"
                    + "AND (" + " \n"
                    + "     EmailOptionAging IN (" + constAutoEmailHalfMonthly + "," + constAutoEmailMonthly + ")" + " \n"
                    + "     OR EmailOptionInvoicesReport IN (" + constAutoEmailHalfMonthly + "," + constAutoEmailMonthly + ")" + " \n"
                    + "     OR EmailOptionPartnerStatement IN (" + constAutoEmailHalfMonthly + "," + constAutoEmailMonthly + ")" + " \n"
                    + "     )"
                    , "ID", out _RowCount);
                for (int i = 0; i < objCCustomers.lstCVarCustomers.Count; i++)
                {
                    DateTime _CurrentDateTime = DateTime.Now;
                    int _CurrentYear = _CurrentDateTime.Year;
                    int _CurrentMonth = _CurrentDateTime.Month;
                    int _CurrentDay = _CurrentDateTime.Day;
                    int _YearClause = 0;
                    int _MonthClause = 0;
                    int _DayFromClause = 1;
                    int _DayToClause = 15;
                    string _DateFrom = "";
                    string _DateTo = "";
                    #region Monthly Getting Year & Month for WhereClause
                    if (objCCustomers.lstCVarCustomers[i].EmailOptionAging == constAutoEmailMonthly
                        || objCCustomers.lstCVarCustomers[i].EmailOptionInvoicesReport == constAutoEmailMonthly || objCCustomers.lstCVarCustomers[i].EmailOptionPartnerStatement == constAutoEmailMonthly)
                    {
                        if (_CurrentMonth == 1) //First month in the year
                        {
                            _YearClause = _CurrentYear - 1;
                            _MonthClause = 12;
                        }
                        else //Current Month is not January
                        {
                            _YearClause = _CurrentYear;
                            _MonthClause = _CurrentMonth - 1;
                        }
                        _DayFromClause = 1;
                        _DayToClause = DateTime.DaysInMonth(_YearClause, _MonthClause);
                    }
                    #endregion Monthly Getting Year & Month for WhereClause
                    #region Half-Monthly Getting Year & Month for WhereClause
                    else if (objCCustomers.lstCVarCustomers[i].EmailOptionAging == constAutoEmailHalfMonthly
                        || objCCustomers.lstCVarCustomers[i].EmailOptionInvoicesReport == constAutoEmailHalfMonthly
                        || objCCustomers.lstCVarCustomers[i].EmailOptionPartnerStatement == constAutoEmailHalfMonthly)
                    {
                        if (_CurrentMonth == 1 && _CurrentDay <= 15)
                        {
                            _YearClause = _CurrentYear - 1;
                            _MonthClause = 12;
                            _DayFromClause = 16;
                            _DayToClause = DateTime.DaysInMonth(_YearClause, _MonthClause);
                        }
                        else //Not January
                        {
                            _YearClause = _CurrentYear;
                            if (_CurrentDay <= 15)
                            {
                                _MonthClause = _CurrentMonth - 1;
                                _DayFromClause = 16;
                                _DayToClause = DateTime.DaysInMonth(_YearClause, _MonthClause);
                            }
                            else if (_CurrentDay > 15)
                            {
                                _MonthClause = _CurrentMonth;
                                _DayFromClause = 1;
                                _DayToClause = 15;
                            }
                        }
                    } //HalfMonthly
                    #endregion Half-Monthly Getting Year & Month for WhereClause

                    _DateFrom = _DayFromClause.ToString().PadLeft(2, '0') + "/" + _MonthClause.ToString().PadLeft(2, '0') + "/" + _YearClause;
                    _DateTo = _DayToClause.ToString().PadLeft(2, '0') + "/" + _MonthClause.ToString().PadLeft(2, '0') + "/" + _YearClause;


                    var _DateFromMMDDYYYY = _MonthClause.ToString().PadLeft(2, '0')  + "/" +  _DayFromClause.ToString().PadLeft(2, '0') + "/" + _YearClause;
                    var _DateToMMDDYYYY = _MonthClause.ToString().PadLeft(2, '0') + "/" + _DayToClause.ToString().PadLeft(2, '0') + "/" + _YearClause;

                    #region Check and Send for Aging
                    {
                        #region Building the _WhereClause to check if already sent
                        string _WhereClause = "WHERE CustomerID=" + objCCustomers.lstCVarCustomers[i].ID + " \n";
                        _WhereClause += "AND EmailType=N'Aging'" + " \n";
                        _WhereClause += "AND SentYear=" + _YearClause + " \n";
                        _WhereClause += "AND SentMonth=" + _MonthClause + " \n";
                        if (objCCustomers.lstCVarCustomers[i].EmailOptionAging == constAutoEmailHalfMonthly && _CurrentDay <= 15)
                            _WhereClause += "AND IsSecondHalf=1" + " \n"; //if _CurrentDay <= 15 then check the second half of the previous month
                        else if (objCCustomers.lstCVarCustomers[i].EmailOptionAging == constAutoEmailHalfMonthly && _CurrentDay > 15)
                            _WhereClause += "AND IsFirstHalf=1" + " \n";
                        #endregion Building the _WhereClause to check if already sent
                        #region Send Email if needed
                        checkException = objCAutoEmailSentCustomer.GetListPaging(999999, 1, _WhereClause, "ID", out _RowCount);
                        if (objCAutoEmailSentCustomer.lstCVarAutoEmailSentCustomer.Count == 0
                            && objCvwDefaults.lstCVarvwDefaults[0].Email != "0" && objCvwDefaults.lstCVarvwDefaults[0].Email != ""
                            && objCvwDefaults.lstCVarvwDefaults[0].Email_Password != "0" && objCvwDefaults.lstCVarvwDefaults[0].Email_Password != ""
                            && objCvwDefaults.lstCVarvwDefaults[0].Email_DisplayName != "0" && objCvwDefaults.lstCVarvwDefaults[0].Email_DisplayName != ""
                            && objCvwDefaults.lstCVarvwDefaults[0].Email_Host != "0" && objCvwDefaults.lstCVarvwDefaults[0].Email_Host != ""
                            && objCvwDefaults.lstCVarvwDefaults[0].Email_Port != 0)
                        {
                            #region Preparing the URL
                            string arr_Values =
                                _DateFrom
                                + "," + _DateTo
                                + ",01/01/2000" //txtFromDueDate
                                + ",01/01/2100" //txtToDueDate
                                + "," + constCustomerPartnerTypeID //slPartnerType
                                + "," + objCCustomers.lstCVarCustomers[i].ID //slPartner
                                + ",true" //cbAging
                            ;
                            var _hyperlink = "<a href='" + HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/GlobalReports/ViewInvoiceReport?&pTitle=Aging Report&arr_Values=" + arr_Values + "&pReportName=Aging Report'>" + "Aging Report [ "+ objCCustomers.lstCVarCustomers[i].Name+ "] From " + _DateFrom + " To " + _DateTo + "</a>";

                            #endregion Preparing the URL
                            #region Send the email
                            string _MailMessageReturned = "";
                            _MailMessageReturned = Static_SendAutoEmailWithHyperink(objCvwDefaults.lstCVarvwDefaults[0].Email, objCCustomers.lstCVarCustomers[i].Email
                                , objCvwDefaults.lstCVarvwDefaults[0].Email_Host, objCvwDefaults.lstCVarvwDefaults[0].Email_DisplayName
                                , "Periodical Aging Report", _hyperlink, objCvwDefaults.lstCVarvwDefaults[0].Email_Password
                                , objCvwDefaults.lstCVarvwDefaults[0].Email_Port, objCvwDefaults.lstCVarvwDefaults[0].Email_IsSSL , objCvwDefaults.lstCVarvwDefaults[0].Email_Footer);
                            #endregion Send the email
                            #region Insert into AutoEmailSentCustomer
                            if (_MailMessageReturned == "")
                            {
                                CVarAutoEmailSentCustomer objCVarAutoEmailSentCustomer = new CVarAutoEmailSentCustomer();
                                objCVarAutoEmailSentCustomer.CustomerID = objCCustomers.lstCVarCustomers[i].ID;
                                objCVarAutoEmailSentCustomer.EmailType = "Aging";
                                objCVarAutoEmailSentCustomer.SentYear = _YearClause;
                                objCVarAutoEmailSentCustomer.SentMonth = _MonthClause;
                                objCVarAutoEmailSentCustomer.IsFirstHalf = objCCustomers.lstCVarCustomers[i].EmailOptionAging == constAutoEmailHalfMonthly && _CurrentDay > 15 ? true : false;
                                objCVarAutoEmailSentCustomer.IsSecondHalf = objCCustomers.lstCVarCustomers[i].EmailOptionAging == constAutoEmailHalfMonthly && _CurrentDay <= 15 ? true : false;
                                objCVarAutoEmailSentCustomer.CreationDate = DateTime.Now;
                                objCAutoEmailSentCustomer.lstCVarAutoEmailSentCustomer.Add(objCVarAutoEmailSentCustomer);
                                checkException = objCAutoEmailSentCustomer.SaveMethod(objCAutoEmailSentCustomer.lstCVarAutoEmailSentCustomer);
                            }
                            #endregion Insert into AutoEmailSentCustomer
                        }
                        #endregion Send Email if needed
                    }
                    #endregion Check and Send for Aging

                    #region Check and Send for InvoiceReport
                    {
                        #region Building the _WhereClause to check if already sent
                        string _WhereClause = "WHERE CustomerID=" + objCCustomers.lstCVarCustomers[i].ID + " \n";
                        _WhereClause += "AND EmailType=N'InvoiceReport'" + " \n";
                        _WhereClause += "AND SentYear=" + _YearClause + " \n";
                        _WhereClause += "AND SentMonth=" + _MonthClause + " \n";
                        if (objCCustomers.lstCVarCustomers[i].EmailOptionAging == constAutoEmailHalfMonthly && _CurrentDay <= 15)
                            _WhereClause += "AND IsSecondHalf=1" + " \n"; //if _CurrentDay <= 15 then check the second half of the previous month
                        else if (objCCustomers.lstCVarCustomers[i].EmailOptionAging == constAutoEmailHalfMonthly && _CurrentDay > 15)
                            _WhereClause += "AND IsFirstHalf=1" + " \n";
                        #endregion Building the _WhereClause to check if already sent
                        #region Send Email if needed
                        checkException = objCAutoEmailSentCustomer.GetListPaging(999999, 1, _WhereClause, "ID", out _RowCount);
                        if (objCAutoEmailSentCustomer.lstCVarAutoEmailSentCustomer.Count == 0
                            && objCvwDefaults.lstCVarvwDefaults[0].Email != "0" && objCvwDefaults.lstCVarvwDefaults[0].Email != ""
                            && objCvwDefaults.lstCVarvwDefaults[0].Email_Password != "0" && objCvwDefaults.lstCVarvwDefaults[0].Email_Password != ""
                            && objCvwDefaults.lstCVarvwDefaults[0].Email_DisplayName != "0" && objCvwDefaults.lstCVarvwDefaults[0].Email_DisplayName != ""
                            && objCvwDefaults.lstCVarvwDefaults[0].Email_Host != "0" && objCvwDefaults.lstCVarvwDefaults[0].Email_Host != ""
                            && objCvwDefaults.lstCVarvwDefaults[0].Email_Port != 0)
                        {
                            #region Preparing the URL
                            string arr_Values =
                                _DateFrom
                                + "," + _DateTo
                                + ",01/01/2000" //txtFromDueDate
                                + ",01/01/2100" //txtToDueDate
                                + "," + constCustomerPartnerTypeID //slPartnerType
                                + "," + objCCustomers.lstCVarCustomers[i].ID //slPartner
                                + ",false" //cbAging
                            ;
                            var _hyperlink = "<a href='" + HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/GlobalReports/ViewInvoiceReport?&pTitle=Invoices Report&arr_Values=" + arr_Values + "&pReportName=Aging Report'>" + "Invoices Report ["+ objCCustomers.lstCVarCustomers[i].Name + "] From " + _DateFrom + " To " + _DateTo + "</a>";
                            #endregion Preparing the URL
                            #region Send the email
                            string _MailMessageReturned = "";
                            _MailMessageReturned = Static_SendAutoEmailWithHyperink(objCvwDefaults.lstCVarvwDefaults[0].Email, objCCustomers.lstCVarCustomers[i].Email
                                , objCvwDefaults.lstCVarvwDefaults[0].Email_Host, objCvwDefaults.lstCVarvwDefaults[0].Email_DisplayName
                                , "Periodical Invoices Report", _hyperlink, objCvwDefaults.lstCVarvwDefaults[0].Email_Password
                                , objCvwDefaults.lstCVarvwDefaults[0].Email_Port, objCvwDefaults.lstCVarvwDefaults[0].Email_IsSSL , objCvwDefaults.lstCVarvwDefaults[0].Email_Footer);
                            #endregion Send the email
                            #region Insert into AutoEmailSentCustomer
                            if (_MailMessageReturned == "")
                            {
                                CVarAutoEmailSentCustomer objCVarAutoEmailSentCustomer = new CVarAutoEmailSentCustomer();
                                objCVarAutoEmailSentCustomer.CustomerID = objCCustomers.lstCVarCustomers[i].ID;
                                objCVarAutoEmailSentCustomer.EmailType = "InvoiceReport";
                                objCVarAutoEmailSentCustomer.SentYear = _YearClause;
                                objCVarAutoEmailSentCustomer.SentMonth = _MonthClause;
                                objCVarAutoEmailSentCustomer.IsFirstHalf = objCCustomers.lstCVarCustomers[i].EmailOptionAging == constAutoEmailHalfMonthly && _CurrentDay > 15 ? true : false;
                                objCVarAutoEmailSentCustomer.IsSecondHalf = objCCustomers.lstCVarCustomers[i].EmailOptionAging == constAutoEmailHalfMonthly && _CurrentDay <= 15 ? true : false;
                                objCVarAutoEmailSentCustomer.CreationDate = DateTime.Now;
                                objCAutoEmailSentCustomer.lstCVarAutoEmailSentCustomer.Add(objCVarAutoEmailSentCustomer);
                                checkException = objCAutoEmailSentCustomer.SaveMethod(objCAutoEmailSentCustomer.lstCVarAutoEmailSentCustomer);
                            }
                            #endregion Insert into AutoEmailSentCustomer
                        }
                        #endregion Send Email if needed
                    }
                    #endregion Check and Send for InvoiceReport

                    #region Check and Send for CustomerStatement
                    {
                        #region Building the _WhereClause to check if already sent
                        string _WhereClause = "WHERE CustomerID=" + objCCustomers.lstCVarCustomers[i].ID + " \n";
                        _WhereClause += "AND EmailType=N'CustomerStatementEG'" + " \n";
                        _WhereClause += "AND SentYear=" + _YearClause + " \n";
                        _WhereClause += "AND SentMonth=" + _MonthClause + " \n";
                        if (objCCustomers.lstCVarCustomers[i].EmailOptionPartnerStatement == constAutoEmailHalfMonthly && _CurrentDay <= 15)
                            _WhereClause += "AND IsSecondHalf=1" + " \n"; //if _CurrentDay <= 15 then check the second half of the previous month
                        else if (objCCustomers.lstCVarCustomers[i].EmailOptionPartnerStatement == constAutoEmailHalfMonthly && _CurrentDay > 15)
                            _WhereClause += "AND IsFirstHalf=1" + " \n";
                        #endregion Building the _WhereClause to check if already sent
                        #region Send Email if needed
                        checkException = objCAutoEmailSentCustomer.GetListPaging(999999, 1, _WhereClause, "ID", out _RowCount);
                        if (objCAutoEmailSentCustomer.lstCVarAutoEmailSentCustomer.Count == 0
                            && objCvwDefaults.lstCVarvwDefaults[0].Email != "0" && objCvwDefaults.lstCVarvwDefaults[0].Email != ""
                            && objCvwDefaults.lstCVarvwDefaults[0].Email_Password != "0" && objCvwDefaults.lstCVarvwDefaults[0].Email_Password != ""
                            && objCvwDefaults.lstCVarvwDefaults[0].Email_DisplayName != "0" && objCvwDefaults.lstCVarvwDefaults[0].Email_DisplayName != ""
                            && objCvwDefaults.lstCVarvwDefaults[0].Email_Host != "0" && objCvwDefaults.lstCVarvwDefaults[0].Email_Host != ""
                            && objCvwDefaults.lstCVarvwDefaults[0].Email_Port != 0)
                        {
                            #region Preparing the URL

                            // Get Avaliable Currencies
                            CvwA_JVDetails cvwA_JVDetails = new CvwA_JVDetails();
                            cvwA_JVDetails.GetList("where isnull( SubAccount_ID , 0 )= " + objCCustomers.lstCVarCustomers[i].SubAccountID + " AND ( SELECT TOP(1) aj.ID FROM A_JV AS aj WHERE JV_ID = aj.ID AND aj.JVDate BETWEEN \'" + _DateFromMMDDYYYY + "\' and \'" + _DateToMMDDYYYY + "\') IS NOT null");
                                if(cvwA_JVDetails.lstCVarvwA_JVDetails != null && cvwA_JVDetails.lstCVarvwA_JVDetails.Count > 0)
                                {


                                var DistinctDetails = cvwA_JVDetails.lstCVarvwA_JVDetails.DistinctBy(x => x.CurrencyCode).ToList();

                                var arr_ValuesGroup = new List<string>();
                                string _hyperlinks = "";
                                for (int j = 0; j < DistinctDetails.Count; j++)
                                {
                             string arr_Values =
                                    _DateFrom
                            + "," + _DateTo
                            + "," + "\"IST\""
                            + "," + DistinctDetails[j].Currency_ID
                            + "," + DistinctDetails[j].CurrencyCode
                            + "," + "4"
                            + "," + "\"CLIENTS\""
                            + "," + objCCustomers.lstCVarCustomers[i].SubAccountID
                            + ",\"" + objCCustomers.lstCVarCustomers[i].Name + "\""
                            + "," + "true"
                            + "," + "false"
                            + "," + "false"
                            + "," + "true"
                            + "," + "false";
                            _hyperlinks += " <div><a href='" + HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/GlobalReports/ViewOperAccountStatement?&pTitle=Invoices Report&arr_Values=" + arr_Values + "&pReportName=SubAccount Ledger Report'>" + "Account Statement " + DistinctDetails[j].CurrencyCode + "-" + objCCustomers.lstCVarCustomers[i].Name + " From " + _DateFrom + " To " + _DateTo + "</a></div></br> ";
                                }
                                
                            #endregion Preparing the URL
                            #region Send the email
                            string _MailMessageReturned = "";
                            _MailMessageReturned = Static_SendAutoEmailWithHyperink(objCvwDefaults.lstCVarvwDefaults[0].Email, objCCustomers.lstCVarCustomers[i].Email
                                , objCvwDefaults.lstCVarvwDefaults[0].Email_Host, objCvwDefaults.lstCVarvwDefaults[0].Email_DisplayName
                                , "Account Statement Report", _hyperlinks, objCvwDefaults.lstCVarvwDefaults[0].Email_Password
                                , objCvwDefaults.lstCVarvwDefaults[0].Email_Port, objCvwDefaults.lstCVarvwDefaults[0].Email_IsSSL , objCvwDefaults.lstCVarvwDefaults[0].Email_Footer);
                            #endregion Send the email
                            #region Insert into AutoEmailSentCustomer
                            if (_MailMessageReturned == "")
                            {
                                CVarAutoEmailSentCustomer objCVarAutoEmailSentCustomer = new CVarAutoEmailSentCustomer();
                                objCVarAutoEmailSentCustomer.CustomerID = objCCustomers.lstCVarCustomers[i].ID;
                                objCVarAutoEmailSentCustomer.EmailType = "CustomerStatementEG";
                                objCVarAutoEmailSentCustomer.SentYear = _YearClause;
                                objCVarAutoEmailSentCustomer.SentMonth = _MonthClause;
                                objCVarAutoEmailSentCustomer.IsFirstHalf = objCCustomers.lstCVarCustomers[i].EmailOptionPartnerStatement == constAutoEmailHalfMonthly && _CurrentDay > 15 ? true : false;
                                objCVarAutoEmailSentCustomer.IsSecondHalf = objCCustomers.lstCVarCustomers[i].EmailOptionPartnerStatement == constAutoEmailHalfMonthly && _CurrentDay <= 15 ? true : false;
                                objCVarAutoEmailSentCustomer.CreationDate = DateTime.Now;
                                objCAutoEmailSentCustomer.lstCVarAutoEmailSentCustomer.Add(objCVarAutoEmailSentCustomer);
                                checkException = objCAutoEmailSentCustomer.SaveMethod(objCAutoEmailSentCustomer.lstCVarAutoEmailSentCustomer);
                            }
                            #endregion Insert into AutoEmailSentCustomer
                                }


                        }
                        #endregion Send Email if needed
                    }
                    #endregion Check and Send for CustomerStatement





                }
                #endregion Customers Aging/InvoiceReport
            } //if (objCDefaults.lstCVarDefaults[0].IsDepartmentOption)


            #region CRM Alarm User And Manager
            CCRM_privilege objCCRM_privilege = new CCRM_privilege();
            objCCRM_privilege.GetList(" Where ID = 40");
            //CEmail objCEmail = new CEmail();
            //CEmailReceiver objCEmailReceiver = new CEmailReceiver();
            CvwCRMAlartFollowUp objCvwCRMAlartFollowUp = new CvwCRMAlartFollowUp();
            //objCvwCRMAlartFollowUp.GetList(" Where 1=1 AND (ManagerAlarmed <> 1 AND UserAlarmed <> 1)");
            objCvwCRMAlartFollowUp.GetList(" Where 1=1");
            for (int j = 0; j < objCvwCRMAlartFollowUp.lstCVarvwCRMAlartFollowUp.Count; j++)
            {
                string AlarmName = objCvwCRMAlartFollowUp.lstCVarvwCRMAlartFollowUp[j].Alarm;
                //if (AlarmName == "alartUser" && objCvwCRMAlartFollowUp.lstCVarvwCRMAlartFollowUp[j].UserAlarmed == 0)
                if (objCvwCRMAlartFollowUp.lstCVarvwCRMAlartFollowUp[j].SalesRep == WebSecurity.CurrentUserId && objCvwCRMAlartFollowUp.lstCVarvwCRMAlartFollowUp[j].DifferenceDays <= objCvwCRMAlartFollowUp.lstCVarvwCRMAlartFollowUp[j].AlarmDays && objCvwCRMAlartFollowUp.lstCVarvwCRMAlartFollowUp[j].UserAlarmed == 0)
                {
                    CVarEmail objCVarEmail = new CVarEmail();
                    CEmail objCEmail = new CEmail();
                    CEmailReceiver objCEmailReceiver = new CEmailReceiver();
                    objCVarEmail.Subject = " CRM Follow Up";
                    objCVarEmail.Body = "Action: '" + objCvwCRMAlartFollowUp.lstCVarvwCRMAlartFollowUp[j].ActionName + " \n"
                        + "Sales Lead: " + objCvwCRMAlartFollowUp.lstCVarvwCRMAlartFollowUp[j].CRM_ClientName + " \n"
                        + "Action Date: " + objCvwCRMAlartFollowUp.lstCVarvwCRMAlartFollowUp[j].ActionDate.ToShortDateString() + " \n";
                    objCVarEmail.QuotationRouteID = 0;
                    objCVarEmail.SenderUserID = objCvwCRMAlartFollowUp.lstCVarvwCRMAlartFollowUp[j].SalesRep;
                    objCVarEmail.SendingDate = DateTime.Now;
                    objCEmail.lstCVarEmail.Add(objCVarEmail);
                    Exception checkException_temp = objCEmail.SaveMethod(objCEmail.lstCVarEmail);
                    if (checkException_temp == null) //add to EmailReceivers
                    {
                        CVarEmailReceiver objCVarEmailReceiver = new CVarEmailReceiver();
                        objCVarEmailReceiver.EmailID = objCVarEmail.ID;
                        objCVarEmailReceiver.ReceiverUserID = objCvwCRMAlartFollowUp.lstCVarvwCRMAlartFollowUp[j].SalesRep;
                        objCVarEmailReceiver.ReceivingDate = DateTime.Parse("01-01-1900");
                        objCVarEmailReceiver.IsAlarm = true;

                        objCEmailReceiver.lstCVarEmailReceiver.Add(objCVarEmailReceiver);
                        checkException_temp = objCEmailReceiver.SaveMethod(objCEmailReceiver.lstCVarEmailReceiver);
                        if (checkException_temp == null)
                        {
                            CCRM_FollowUp objCCRM_FollowUp = new CCRM_FollowUp();
                            objCCRM_FollowUp.UpdateList("UserAlarmed=1 WHERE ID=" + objCvwCRMAlartFollowUp.lstCVarvwCRMAlartFollowUp[j].ID);
                        }
                    }

                }
                else if (objCvwCRMAlartFollowUp.lstCVarvwCRMAlartFollowUp[j].DifferenceDays <= 0 && objCvwCRMAlartFollowUp.lstCVarvwCRMAlartFollowUp[j].ManagerAlarmed == 0 && objCvwCRMAlartFollowUp.lstCVarvwCRMAlartFollowUp[j].UserAlarmed == 1)
                {
                    var Managers = objCCRM_privilege.lstCVarCRM_privilege[0].UsersIDs.Split(',');
                    {
                        CVarEmail objCVarEmail = new CVarEmail();
                        CEmail objCEmail = new CEmail();
                        CEmailReceiver objCEmailReceiver = new CEmailReceiver();
                        objCVarEmail.Subject = " CRM Follow Up Alarm With Actions";
                        objCVarEmail.Body = "Action: '" + objCvwCRMAlartFollowUp.lstCVarvwCRMAlartFollowUp[j].ActionName + " Has Statue :("+ objCvwCRMAlartFollowUp.lstCVarvwCRMAlartFollowUp[j].NextActionStatueName+ ") \n"
                            + "Sales Lead: " + objCvwCRMAlartFollowUp.lstCVarvwCRMAlartFollowUp[j].CRM_ClientName + " \n"
                            + "Action Date: " + objCvwCRMAlartFollowUp.lstCVarvwCRMAlartFollowUp[j].ActionDate.ToShortDateString() + " \n";
                        objCVarEmail.QuotationRouteID = 0;
                        objCVarEmail.SenderUserID = objCvwCRMAlartFollowUp.lstCVarvwCRMAlartFollowUp[j].SalesRep;
                        objCVarEmail.SendingDate = DateTime.Now;
                        objCEmail.lstCVarEmail.Add(objCVarEmail);
                        Exception checkException_temp = objCEmail.SaveMethod(objCEmail.lstCVarEmail);
                        if (checkException_temp == null) //add to EmailReceivers
                        {
                            for (int cnt = 0; cnt < Managers.Length; cnt++)
                            {
                                CVarEmailReceiver objCVarEmailReceiver = new CVarEmailReceiver();
                                objCVarEmailReceiver.EmailID = objCVarEmail.ID;
                                objCVarEmailReceiver.ReceiverUserID = Convert.ToInt32(Managers[cnt]);//objCvwCRMAlartFollowUp.lstCVarvwCRMAlartFollowUp[j].SalesRep;
                                objCVarEmailReceiver.ReceivingDate = DateTime.Parse("01-01-1900");
                                objCVarEmailReceiver.IsAlarm = true;

                                objCEmailReceiver.lstCVarEmailReceiver.Add(objCVarEmailReceiver);
                                checkException_temp = objCEmailReceiver.SaveMethod(objCEmailReceiver.lstCVarEmailReceiver);
                                if (checkException_temp == null)
                                {
                                    CCRM_FollowUp objCCRM_FollowUp = new CCRM_FollowUp();
                                    objCCRM_FollowUp.UpdateList("ManagerAlarmed=1 WHERE ID=" + objCvwCRMAlartFollowUp.lstCVarvwCRMAlartFollowUp[j].ID);
                                }
                            }
                        }

                    }
                }
            }
            #endregion

            #region CRM Setub inactive saleslead
            int pNoDays = 0;
            CCRM_SetupInvalidSalesLeadMonths objCRM_SetupInvalidSalesLeadMonths = new CCRM_SetupInvalidSalesLeadMonths();
            objCRM_SetupInvalidSalesLeadMonths.GetList(" Where ID = (select MAX(ID) from CRM_SetupInvalidSalesLeadMonths )");
            if (objCRM_SetupInvalidSalesLeadMonths.lstCVarCRM_SetupInvalidSalesLeadMonths.Count > 0)
                pNoDays = objCRM_SetupInvalidSalesLeadMonths.lstCVarCRM_SetupInvalidSalesLeadMonths[0].NumOfMonths * 30;
            CvwSetubSalesLead objCvwSetubSalesLead = new CvwSetubSalesLead();
            objCvwSetubSalesLead.GetList(" Where 1=1");

            for (int i = 0; i < objCvwSetubSalesLead.lstCVarvwSetubSalesLead.Count; i++)
            {
                
                if (objCvwSetubSalesLead.lstCVarvwSetubSalesLead[i].DaysToLastAction > pNoDays)
                {
                    objCvwSetubSalesLead.lstCVarvwSetubSalesLead[i].Valid = 0;
                    objCvwSetubSalesLead.lstCVarvwSetubSalesLead[i].DaysDiff = objCvwSetubSalesLead.lstCVarvwSetubSalesLead[i].DaysToLastAction - pNoDays;
                }
            }

            CCRM_SetubSalesLead objCCRM_SetubSalesLead = new CCRM_SetubSalesLead();
            #region Insert CRM_SetubSalesLead
            //if (pID == 0)
            {
                for (int i = 0; i < objCvwSetubSalesLead.lstCVarvwSetubSalesLead.Count; i++)
                {
                    CVarCRM_SetubSalesLead objCVarCRM_SetubSalesLead = new CVarCRM_SetubSalesLead();
                    if(objCvwSetubSalesLead.lstCVarvwSetubSalesLead[i].Valid == 0)
                    {
                        objCVarCRM_SetubSalesLead.ID = 0;
                        objCVarCRM_SetubSalesLead.ClientID = objCvwSetubSalesLead.lstCVarvwSetubSalesLead[i].ClientID;
                        objCVarCRM_SetubSalesLead.ClientName = objCvwSetubSalesLead.lstCVarvwSetubSalesLead[i].ClientName;
                        objCVarCRM_SetubSalesLead.Code = objCvwSetubSalesLead.lstCVarvwSetubSalesLead[i].Code.ToString();
                        objCVarCRM_SetubSalesLead.LastActionDate = objCvwSetubSalesLead.lstCVarvwSetubSalesLead[i].LastActionDate;
                        objCVarCRM_SetubSalesLead.ACTION = objCvwSetubSalesLead.lstCVarvwSetubSalesLead[i].ACTION;
                        objCVarCRM_SetubSalesLead.DaysToLastAction = decimal.Parse(objCvwSetubSalesLead.lstCVarvwSetubSalesLead[i].DaysToLastAction.ToString());
                        objCVarCRM_SetubSalesLead.DaysDiff = decimal.Parse(objCvwSetubSalesLead.lstCVarvwSetubSalesLead[i].DaysDiff.ToString());
                        objCVarCRM_SetubSalesLead.InValid = ((objCvwSetubSalesLead.lstCVarvwSetubSalesLead[i].Valid) == 0 ? true : false);
                        objCVarCRM_SetubSalesLead.NoDays = pNoDays;// int.Parse(objCvwSetubSalesLead.lstCVarvwSetubSalesLead[i].NoDays);
                        objCVarCRM_SetubSalesLead.CreatorUserID = objCVarCRM_SetubSalesLead.ModificationUserID = WebSecurity.CurrentUserId;
                        objCVarCRM_SetubSalesLead.CreationDate = objCVarCRM_SetubSalesLead.ModificationDate = DateTime.Now;
                        objCCRM_SetubSalesLead.lstCVarCRM_SetubSalesLead.Add(objCVarCRM_SetubSalesLead);
                        if (objCVarCRM_SetubSalesLead.InValid == true)
                        {
                            CCRM_Clients CCRM_ClientsSetub = new CCRM_Clients();

                            string _UpdateClauseSetub = " ClientStatus=30 WHERE ID=" + objCvwSetubSalesLead.lstCVarvwSetubSalesLead[i].ClientID + "";
                            checkException = CCRM_ClientsSetub.UpdateList(_UpdateClauseSetub);
                        }
                       
                    }
                    else
                    {
                        CCRM_Clients CCRM_ClientsSetub = new CCRM_Clients();

                        string _UpdateClauseSetub = " ClientStatus=20 WHERE ID=" + objCvwSetubSalesLead.lstCVarvwSetubSalesLead[i].ClientID + "";
                        checkException = CCRM_ClientsSetub.UpdateList(_UpdateClauseSetub);
                    }

                }

                checkException = objCCRM_SetubSalesLead.SaveMethod(objCCRM_SetubSalesLead.lstCVarCRM_SetubSalesLead);
            }
            #endregion Insert CRM_SetubSalesLead
            #endregion

            return new object[] { };
        }

        //private void sendEmailViaWebApi()
        //{
        //    string subject = "Email Subject";
        //    string body = "Email body";
        //    string FromMail = "sherif@istegy.com";
        //    string emailTo = "sherif@istegy.com";
        //    MailMessage mail = new MailMessage();
        //    SmtpClient SmtpServer = new SmtpClient("mail.reckonbits.com.pk");
        //    mail.From = new MailAddress(FromMail);
        //    mail.To.Add(emailTo);
        //    mail.Subject = subject;
        //    mail.Body = body;
        //    SmtpServer.Port = 25;
        //    SmtpServer.Credentials = new System.Net.NetworkCredential("sherif@istegy.com", "123456");
        //    SmtpServer.EnableSsl = false;
        //    SmtpServer.Send(mail);
        //}
        //private void sendEmailViaWebApi()
        //{
        //    string subject = "Email Subject";
        //    string body = "Email body";
        //    string FromMail = "sherif@istegy.com";
        //    string emailTo = "sherif@istegy.com";
        //    MailMessage mail = new MailMessage();

        //    SmtpClient SmtpServer = new SmtpClient();
        //    SmtpServer.UseDefaultCredentials = true;

        //    mail.From = new MailAddress(FromMail);
        //    mail.To.Add(emailTo);
        //    mail.Subject = subject;
        //    mail.Body = body;
        //    SmtpServer.Port = 25;
        //    //SmtpServer.Port = Convert.ToInt32(ConfigurationSettings.AppSettings["PORT"].ToString());
        //    //SmtpServer.Port = Convert.ToInt32(Configuration!System.Configuration.ConfigurationManager.AppSettings["PORT"].ToString());
        //    SmtpServer.Credentials = new System.Net.NetworkCredential("sherif@istegy.com", "123456");
        //    SmtpServer.EnableSsl = false;
        //    SmtpServer.Send(mail);
        //}

        //[HttpGet, HttpPost]
        //public object[] sendEmailViaWebApi()
        //{
        //    string subject = "Email Subject";
        //    string body = "Dear Sir, your cargo is at \n https://www.google.com/maps/?q=28.766621599999997,29.232078399999995" + " Latitude: 28.766621599999997 °, Longitude: 29.232078399999995 °</a>";
        //    string FromMail = "noreply-Rename@istegy.com";
        //    string emailTo = "sherifanwar@yahoo.com";
        //    string CC = "sherifanwar80@gmail.com";
        //    MailMessage mail = new MailMessage();

        //    SmtpClient SmtpServer = new SmtpClient();
        //    SmtpServer.UseDefaultCredentials = true;
        //    SmtpClient SmtpServer = new SmtpClient("smtpout.secureserver.net", SmtpServer.Port);

        //    mail.From = new MailAddress(FromMail);
        //    mail.To.Add(emailTo);
        //    mail.CC.Add(CC);
        //    mail.Subject = subject;
        //    mail.Body = body;
        //    mail.Attachments.Add(new Attachment("C:\\Users\\Sherif\\Desktop\\CompanyLogo.jpg"));
        //    SmtpServer.Port = 25;
        //    SmtpServer.Port = Convert.ToInt32(ConfigurationSettings.AppSettings["PORT"].ToString());
        //    SmtpServer.Port = Convert.ToInt32(Configuration!System.Configuration.ConfigurationManager.AppSettings["PORT"].ToString());
        //    SmtpServer.Host = "smtpout.secureserver.net";
        //    SmtpServer.Credentials = new System.Net.NetworkCredential(FromMail, "123456");
        //    SmtpServer.EnableSsl = true;//false
        //    SmtpServer.Send(mail);

        //    return new object[] { };
        //}

        //[HttpGet, HttpPost]
        //public object[] sendEmailViaWebApi()
        //{
        //    string subject = "Email Subject";
        //    string body = "Dear Sir, your cargo is at \n https://www.google.com/maps/?q=28.766621599999997,29.232078399999995" + " Latitude: 28.766621599999997 °, Longitude: 29.232078399999995 °</a>";
        //    string FromMail = "noreply-Rename@istegy.com";
        //    string emailTo = "sherifanwar@yahoo.com";
        //    string CC = "sherifanwar80@gmail.com";
        //    MailMessage mail = new MailMessage();

        //    SmtpClient SmtpServer = new SmtpClient();
        //    SmtpServer.UseDefaultCredentials = true;
        //    //SmtpClient SmtpServer = new SmtpClient("smtpout.secureserver.net", SmtpServer.Port);

        //    mail.From = new MailAddress(FromMail);
        //    mail.To.Add(emailTo);
        //    mail.CC.Add(CC);
        //    mail.Subject = subject;
        //    mail.Body = body;
        //    mail.Attachments.Add(new Attachment("C:\\Users\\Sherif\\Desktop\\CompanyLogo.jpg"));
        //    //SmtpServer.Port = 25;
        //    //SmtpServer.Port = Convert.ToInt32(ConfigurationSettings.AppSettings["PORT"].ToString());
        //    //SmtpServer.Port = Convert.ToInt32(Configuration!System.Configuration.ConfigurationManager.AppSettings["PORT"].ToString());
        //    SmtpServer.Host = "smtpout.secureserver.net";
        //    SmtpServer.Credentials = new System.Net.NetworkCredential(FromMail, "N@123456");
        //    SmtpServer.EnableSsl = true;//false
        //    SmtpServer.Send(mail);

        //    return new object[] { };
        //}

        [HttpGet]
        public static string Static_SendAutoEmailWithHyperink(string pMailFrom, string pMailTo, string pEmail_Host, string pDisplayName, string pSubject
            , string pHyperlink, string pEmail_Password, Int32 pPort_Out, bool pMailTo_IsSSL , string pEmailFooter)
        {
            string _MailMessageReturned = "";
            bool _boolEmailFound = false;
            var mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(pEmail_Host, pPort_Out);
            SmtpServer.UseDefaultCredentials = true;
            SmtpServer.Credentials = new System.Net.NetworkCredential(pMailFrom, CEncryptDecrypt.Decrypt(pEmail_Password, true));

            mail.From = new MailAddress(pMailFrom, pDisplayName);
            _boolEmailFound = true;
            mail.To.Add(pMailTo);
            //mail.CC.Add(CC);
            mail.Subject = pSubject;
            mail.Body = "Dear Sir/Ma'am, <br/><br/> &emsp; Good day,<br /><br />";
            mail.Body += " &emsp; Please, follow the link " + pHyperlink;
            mail.Body += "<br/><br/>Thanks and best regards ......... <br/> ";
            mail.Body += pEmailFooter;
            //"<a href='" + pUrl + "'>" + pEmail_Subject + "</a>"

            mail.IsBodyHtml = true;
            //mail.Attachments.Add(new Attachment(pathString));
            //mail.Attachments.Add(new Attachment("C:\\Users\\Sherif\\Desktop\\CompanyLogo.jpg"));
            //SmtpServer.Port = 25;
            //SmtpServer.Port = Convert.ToInt32(ConfigurationSettings.AppSettings["PORT"].ToString());
            //SmtpServer.Port = Convert.ToInt32(Configuration!System.Configuration.ConfigurationManager.AppSettings["PORT"].ToString());
            SmtpServer.EnableSsl = pMailTo_IsSSL;
            if (_boolEmailFound)
                try
                {
                    SmtpServer.Send(mail);
                }
                catch (Exception ex)
                {
                    _MailMessageReturned = ex.Message;
                }
            return _MailMessageReturned;
        }

        [HttpPost]
        public Object[] SendTestEmail([FromBody] TestEmailData Email)
        {
            //JavaScriptSerializer jsonSerialiser = new JavaScriptSerializer();
            string _MessageReturned = "";
            try
            {
                var mail = new System.Net.Mail.MailMessage();
                SmtpClient Client = new SmtpClient(Email.pEmail_Host, int.Parse(Email.pEmail_Port));
                Client.UseDefaultCredentials = true;
                Client.Credentials = new System.Net.NetworkCredential((Email.pEmail).ToLower(), Email.pEmail_Password);
                mail.From = new MailAddress((Email.pEmail).ToLower(), Email.pEmail_DisplayName);
                mail.To.Add(new MailAddress(Email.pEmail_To));// put to address here
                mail.Subject = Email.pEmail_Subject;        // put subject here  
                mail.Body = Email.pEmail_Body;
                mail.Body += (" <br> " + Email.pEmail_Footer);
                mail.IsBodyHtml = true;
                Client.EnableSsl = Email.pEmail_IsSSL; //config[0].Email_IsSSL == null || config[0].Email_IsSSL == false ? false : true;
                Client.Send(mail);
            }
            catch (Exception ex)
            {
                _MessageReturned = ex.Message;
            }
            return new Object[] {
                _MessageReturned //data[0]
            };
        }

        [HttpPost]
        public Object[] SendPDFEmail([FromBody] PDFEmailData Email)
        {
            JavaScriptSerializer jsonSerialiser = new JavaScriptSerializer();
            string _MessageReturned = "";
            try
            {
                // 1 - pdf file @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

                bool exists = Directory.Exists(HttpContext.Current.Server.MapPath("SysEmails"));
                if (!exists)
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("SysEmails"));

                var filename = Email.pEmail_ReportName + DateTime.Now.Ticks + ".pdf";
                string pathString = Path.Combine(HttpContext.Current.Server.MapPath("SysEmails"), filename);

                //-------------------------------------------------------------------------------------------------- Install-Package OpenHtmlToPdf -Version 1.12.0
                string htmlCode = Email.pHTML_ForPDF;
                htmlCode = htmlCode.Replace("\"/", "\"" + HttpContext.Current.Server.MapPath("~/"));
                string baseUrl = null;

                htmlCode = htmlCode.Replace("<head>", "<heade><meta http-equiv=\"Content-Type\" content=\"text/html;charset=UTF-8\">");
                htmlCode = htmlCode.Replace("<head>", "<heade><style> table { page-break-inside:auto } tr { page-break-inside:avoid; page-break-after:auto }</style>");

                byte[] content =
                Pdf.From(htmlCode)
               .OfSize(PaperSize.A4)
               .WithTitle(Email.pEmail_Subject)
               .WithoutOutline()
               .WithMargins(1.25.Centimeters())
               .Landscape()
               .Comressed()
               .Content();

                File.WriteAllBytes(pathString, content);

                var mail = new System.Net.Mail.MailMessage();

                mail.Attachments.Add(new Attachment(pathString));
                //SmtpClient Client = new SmtpClient("mail.sharksbay.com", 587);
                SmtpClient Client = new SmtpClient(Email.pEmail_Host, int.Parse(Email.pEmail_Port));
                Client.UseDefaultCredentials = true;
                Client.Credentials = new System.Net.NetworkCredential(Email.pEmail, Email.pEmail_Password);
                mail.From = new MailAddress(Email.pEmail, Email.pEmail_DisplayName);
                mail.To.Add(new MailAddress(Email.pEmail_To));// put to address here
                mail.Subject = Email.pEmail_Subject;  // put subject here  
                mail.Body = Email.pEmail_Body + " <br> " + Email.pEmail_Footer;
                mail.IsBodyHtml = true;
                Client.EnableSsl = Email.pEmail_IsSSL;
                Client.Send(mail);

                //if (File.Exists(pathString))
                //    File.Delete(pathString);
            }
            catch (Exception ex)
            {
                _MessageReturned = ex.Message;
            }
            return new Object[] {
                _MessageReturned
            };
        }


        [HttpPost]
        public Object[] SendEmailWithAttachment([FromBody] SendEmailWithAttachment pEmailData)
        {
            Exception checkException = null;
            string _MessageReturned = "";
            int _RowCount = 0;
            string pathString = "";
            var _ArrReceiverEmails = pEmailData.pEmail_To.Split(',');
            CvwUsers objCvwUsers = new CvwUsers();
            checkException = objCvwUsers.GetListPaging(999999, 1, "WHERE ID=" + WebSecurity.CurrentUserId, "ID", out _RowCount);
            CDefaults objCDefaults = new CDefaults();
            checkException = objCDefaults.GetListPaging(999999, 1, "WHERE 1=1", "ID", out _RowCount);
            //CDefaults objCDefaults = new CDefaults();
            //checkException = objCDefaults.GetListPaging(999999, 1, "WHERE 1=1", "ID", out _RowCount);

            if (objCDefaults.lstCVarDefaults[0].Email == "0"
                || objCDefaults.lstCVarDefaults[0].Email_DisplayName == "0" || objCDefaults.lstCVarDefaults[0].Email_DisplayName == ""
                || objCDefaults.lstCVarDefaults[0].Email_Host == "0" || objCDefaults.lstCVarDefaults[0].Email_Host == ""
                || objCDefaults.lstCVarDefaults[0].Email_Port == 0)
                //if (objCvwUsers.lstCVarvwUsers[0].Email == "0" || objCvwUsers.lstCVarvwUsers[0].Email == ""
                //|| objCvwUsers.lstCVarvwUsers[0].Email_Password == "0" || objCvwUsers.lstCVarvwUsers[0].Email_Password == ""
                //|| objCvwUsers.lstCVarvwUsers[0].Email_DisplayName == "0" || objCvwUsers.lstCVarvwUsers[0].Email_DisplayName == ""
                //|| objCvwUsers.lstCVarvwUsers[0].Email_Host == "0" || objCvwUsers.lstCVarvwUsers[0].Email_Host == ""
                //|| objCvwUsers.lstCVarvwUsers[0].Email_Port == 0)
                _MessageReturned = "Please, review your company default email settings.";
            else
            {
                string pattern = "[\\~#%&*{}/:<>?|\"-]";
                string replacement = " ";
                Regex regEx = new Regex(pattern);
                pEmailData.pFileName = Regex.Replace(regEx.Replace(pEmailData.pFileName, replacement), @"\s+", " ");
                try
                {
                    pathString = HttpContext.Current.Server.MapPath("").Replace("Defaults", pEmailData.pMapPath);
                    pathString = System.IO.Path.Combine(pathString, pEmailData.pFileName);

                    var mail = new System.Net.Mail.MailMessage();
                    mail.Attachments.Add(new Attachment(pathString));
                    //SmtpClient Client = new SmtpClient("mail.sharksbay.com", 587);
                    SmtpClient Client = new SmtpClient(objCDefaults.lstCVarDefaults[0].Email_Host, objCDefaults.lstCVarDefaults[0].Email_Port);
                    Client.UseDefaultCredentials = true;
                    Client.Credentials = new System.Net.NetworkCredential(objCDefaults.lstCVarDefaults[0].Email, CEncryptDecrypt.Decrypt(objCDefaults.lstCVarDefaults[0].Email_Password, true));

                    mail.From = new MailAddress(objCDefaults.lstCVarDefaults[0].Email, objCDefaults.lstCVarDefaults[0].Email_DisplayName);
                    for (int i = 0; i < _ArrReceiverEmails.Length; i++)
                        mail.To.Add(new MailAddress(_ArrReceiverEmails[i]));// put to address here
                    mail.Subject = pEmailData.pEmail_Subject;  // put subject here  
                    mail.Body = "<b>Sender : " + objCvwUsers.lstCVarvwUsers[0].Email_DisplayName + "</b><br>" + pEmailData.pEmail_Body + " <br> " + objCDefaults.lstCVarDefaults[0].Email_Footer;
                    mail.IsBodyHtml = true;

                    Client.EnableSsl = objCDefaults.lstCVarDefaults[0].Email_IsSSL;
                    Client.Send(mail);
                }
                catch (Exception ex)
                {
                    _MessageReturned = ex.Message;
                }
                //if (File.Exists(pathString))
                //    File.Delete(pathString);
            }
            return new Object[] {
                _MessageReturned //data[0]
            };
        }

        //  SendPDFEmailWithBodyTemplate Is User
        [HttpPost]
        public Object[] SendPDFEmailWithBodyTemplate([FromBody] SendPDFEmail_GeneralData Email)
        {
            Exception checkException = null;
            int _RowCount = 0;
            JavaScriptSerializer jsonSerialiser = new JavaScriptSerializer();
            string _MessageReturned = "";
            string pathString = "";
            CUsers objCUsers = new CUsers();
            checkException = objCUsers.GetListPaging(999999, 1, "Where ID=" + WebSecurity.CurrentUserId, "ID", out _RowCount);
            CDefaults objCDefaults = new CDefaults();
            checkException = objCDefaults.GetListPaging(999999, 1, "Where 1=1", "ID", out _RowCount);

            var CurrentUser = objCUsers.lstCVarUsers[0];

            //if (CurrentUser.Email == "0" || CurrentUser.Email == ""
            //    || CurrentUser.Email_Password == "0" || CurrentUser.Email_Password == ""
            //    || CurrentUser.Email_DisplayName == "0" || CurrentUser.Email_DisplayName == ""
            //    || CurrentUser.Email_Host == "0" || CurrentUser.Email_Host == ""
            //    || CurrentUser.Email_Port == 0)
            if (objCDefaults.lstCVarDefaults[0].Email == "0" || objCDefaults.lstCVarDefaults[0].Email == "" 
                || objCDefaults.lstCVarDefaults[0].Email_Password == "0" || objCDefaults.lstCVarDefaults[0].Email_Password == ""
                || objCDefaults.lstCVarDefaults[0].Email_DisplayName == "0" || objCDefaults.lstCVarDefaults[0].Email_DisplayName == ""
                || objCDefaults.lstCVarDefaults[0].Email_Host == "0" || objCDefaults.lstCVarDefaults[0].Email_Host == ""
                || objCDefaults.lstCVarDefaults[0].Email_Port == 0)
            {
                _MessageReturned = "Please, review your company default email settings.";
            }
            else
            {
                try
                {
                    string pattern = "[\\~#%&*{}/:<>?|\"-]";
                    string replacement = " ";
                    Regex regEx = new Regex(pattern);
                    Email.pEmail_ReportName = Regex.Replace(regEx.Replace(Email.pEmail_ReportName, replacement), @"\s+", " ");
                }
                catch
                {
                }
                // 1 - pdf file @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                try
                {
                    bool exists = Directory.Exists(HttpContext.Current.Server.MapPath("SysEmails"));
                    if (!exists)
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("SysEmails"));

                    var filename = Email.pEmail_ReportName + DateTime.Now.Ticks + ".pdf";
                    pathString = Path.Combine(HttpContext.Current.Server.MapPath("SysEmails"), filename);
                    //-------------------------------------------------------------------------------------------------- Install-Package OpenHtmlToPdf -Version 1.12.0
                    string htmlCode = Email.pHTML_ForPDF;
                    htmlCode = htmlCode.Replace("\"/", "\"" + HttpContext.Current.Server.MapPath("~/"));
                    string baseUrl = null;

                    htmlCode = htmlCode.Replace("<head>", "<heade><meta http-equiv=\"Content-Type\" content=\"text/html;charset=UTF-8\">");
                    htmlCode = htmlCode.Replace("<head>", "<heade><style> table { page-break-inside:auto } tr { page-break-inside:avoid; page-break-after:auto }</style>");

                    byte[] content =
                    Pdf.From(htmlCode)
                   .OfSize(PaperSize.A4)
                   .WithTitle(Email.pEmail_Subject)
                   .WithoutOutline()
                   .WithMargins(1.25.Centimeters())
                   .Landscape()
                   .Comressed()
                   .Content();

                    File.WriteAllBytes(pathString, content);

                    var mail = new System.Net.Mail.MailMessage();
                    mail.Attachments.Add(new Attachment(pathString));
                    //SmtpClient Client = new SmtpClient("mail.sharksbay.com", 587);
                    SmtpClient Client = new SmtpClient(objCDefaults.lstCVarDefaults[0].Email_Host, objCDefaults.lstCVarDefaults[0].Email_Port);
                    Client.UseDefaultCredentials = true;
                    Client.Credentials = new System.Net.NetworkCredential(objCDefaults.lstCVarDefaults[0].Email, CEncryptDecrypt.Decrypt(objCDefaults.lstCVarDefaults[0].Email_Password , true ));

                    mail.From = new MailAddress(objCDefaults.lstCVarDefaults[0].Email, objCDefaults.lstCVarDefaults[0].Email_DisplayName);
                    mail.To.Add(new MailAddress(Email.pEmail_To));// put to address here
                    mail.Subject = Email.pEmail_Subject;  // put subject here  
                    mail.Body = "<b>Sender : " + CurrentUser.Email_DisplayName + "</b><br>" + "Hello " + Email.pEmail_To + " !! <br/>";
                    mail.Body = mail.Body + "Please find attached file to this email of " + Email.pEmail_ReportName + " .... <br>";
                    mail.Body = mail.Body + "Best regards. <br> " + objCDefaults.lstCVarDefaults[0].Email_Footer;
                    mail.IsBodyHtml = true;

                    Client.EnableSsl = objCDefaults.lstCVarDefaults[0].Email_IsSSL;
                    Client.Send(mail);


                }
                catch (Exception ex)
                {
                    _MessageReturned = ex.Message;
                }
            }
    //if (File.Exists(pathString))
    //    File.Delete(pathString);

            return new Object[] {
                _MessageReturned //data[0]
            };
        }

        [HttpPost]
        public Object[] SendPDFEmail_General([FromBody] SendPDFEmail_GeneralData pEmailData)
        {
            Exception checkException = null;
            string _MessageReturned = "";
            int _RowCount = 0;
            string pathString = "";
            var _ArrReceiverEmails = pEmailData.pEmail_To.Split(',');
            CvwUsers objCvwUsers = new CvwUsers();
            checkException = objCvwUsers.GetListPaging(999999, 1, "WHERE ID=" + WebSecurity.CurrentUserId, "ID", out _RowCount);
            CDefaults objCDefaults = new CDefaults();
            checkException = objCDefaults.GetListPaging(999999, 1, "WHERE 1=1", "ID", out _RowCount);
            //CDefaults objCDefaults = new CDefaults();
            //checkException = objCDefaults.GetListPaging(999999, 1, "WHERE 1=1", "ID", out _RowCount);

            if (objCDefaults.lstCVarDefaults[0].Email == "0"
                || objCDefaults.lstCVarDefaults[0].Email_DisplayName == "0" || objCDefaults.lstCVarDefaults[0].Email_DisplayName == ""
                || objCDefaults.lstCVarDefaults[0].Email_Host == "0" || objCDefaults.lstCVarDefaults[0].Email_Host == ""
                || objCDefaults.lstCVarDefaults[0].Email_Port == 0)
            //if (objCvwUsers.lstCVarvwUsers[0].Email == "0" || objCvwUsers.lstCVarvwUsers[0].Email == ""
                //|| objCvwUsers.lstCVarvwUsers[0].Email_Password == "0" || objCvwUsers.lstCVarvwUsers[0].Email_Password == ""
                //|| objCvwUsers.lstCVarvwUsers[0].Email_DisplayName == "0" || objCvwUsers.lstCVarvwUsers[0].Email_DisplayName == ""
                //|| objCvwUsers.lstCVarvwUsers[0].Email_Host == "0" || objCvwUsers.lstCVarvwUsers[0].Email_Host == ""
                //|| objCvwUsers.lstCVarvwUsers[0].Email_Port == 0)
                _MessageReturned = "Please, review your company default email settings.";
            else
            {
                string pattern = "[\\~#%&*{}/:<>?|\"-]";
                string replacement = " ";
                Regex regEx = new Regex(pattern);
                pEmailData.pEmail_ReportName = Regex.Replace(regEx.Replace(pEmailData.pEmail_ReportName, replacement), @"\s+", " ");
                try
                {
                    // 1 - pdf file @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

                    bool exists = Directory.Exists(HttpContext.Current.Server.MapPath("SysEmails"));
                    if (!exists)
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("SysEmails"));

                    var filename = pEmailData.pEmail_ReportName + DateTime.Now.Ticks + ".pdf";
                    pathString = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("SysEmails"), filename);

                    //-------------------------------------------------------------------------------------------------- Install-Package OpenHtmlToPdf -Version 1.12.0
                    string htmlCode = pEmailData.pHTML_ForPDF;
                    htmlCode = htmlCode.Replace("\"/", "\"" + HttpContext.Current.Server.MapPath("~/"));
                    string baseUrl = null;

                    htmlCode = htmlCode.Replace("<head>", "<heade><meta http-equiv=\"Content-Type\" content=\"text/html;charset=UTF-8\">");
                    htmlCode = htmlCode.Replace("<head>", "<heade><style> table { page-break-inside:auto } tr { page-break-inside:avoid; page-break-after:auto }</style>");

                    byte[] content =
                    Pdf.From(htmlCode)
                   .OfSize(PaperSize.A4)
                   .WithTitle(pEmailData.pEmail_Subject)
                   .WithoutOutline()
                   .WithMargins(1.25.Centimeters())
                   .Portrait()
                   .Comressed()
                   .Content();

                    File.WriteAllBytes(pathString, content);

                    var mail = new System.Net.Mail.MailMessage();
                    mail.Attachments.Add(new Attachment(pathString));
                    //SmtpClient Client = new SmtpClient("mail.sharksbay.com", 587);
                    SmtpClient Client = new SmtpClient(objCDefaults.lstCVarDefaults[0].Email_Host, objCDefaults.lstCVarDefaults[0].Email_Port);
                    Client.UseDefaultCredentials = true;
                    Client.Credentials = new System.Net.NetworkCredential(objCDefaults.lstCVarDefaults[0].Email, CEncryptDecrypt.Decrypt(objCDefaults.lstCVarDefaults[0].Email_Password , true));

                    mail.From = new MailAddress(objCDefaults.lstCVarDefaults[0].Email, objCDefaults.lstCVarDefaults[0].Email_DisplayName);
                    for (int i = 0; i < _ArrReceiverEmails.Length; i++)
                        mail.To.Add(new MailAddress(_ArrReceiverEmails[i]));// put to address here
                    if (objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "DYN")
                        mail.CC.Add(objCDefaults.lstCVarDefaults[0].Email);
                    mail.Subject = pEmailData.pEmail_Subject;  // put subject here  
                    mail.Body = "<b>Sender : " + objCvwUsers.lstCVarvwUsers[0].Email_DisplayName + "</b><br>" + pEmailData.pEmail_Body + " <br> "  + objCDefaults.lstCVarDefaults[0].Email_Footer;
                    mail.IsBodyHtml = true;

                    Client.EnableSsl = objCDefaults.lstCVarDefaults[0].Email_IsSSL;
                    Client.Send(mail);
                }
                catch (Exception ex)
                {
                    _MessageReturned = ex.Message;
                }
                //if (File.Exists(pathString))
                //    File.Delete(pathString);
            }
            return new Object[] {
                _MessageReturned //data[0]
            };
        }
        [HttpPost]
        public Object[] SendUrlEmail_General([FromBody] SendUrlEmail_GeneralData pEmailData)
        {
            Exception checkException = null;
            string _MessageReturned = "";
            int _RowCount = 0;
            string pathString = "";
            //CvwUsers objCvwUsers = new CvwUsers();
            CDefaults objCDefaults = new CDefaults();
            //checkException = objCvwUsers.GetListPaging(999999, 1, "WHERE ID=" + WebSecurity.CurrentUserId, "ID", out _RowCount);
            checkException = objCDefaults.GetListPaging(999999, 1, "WHERE 1=1", "ID", out _RowCount);

            if (objCDefaults.lstCVarDefaults[0].Email == "0"
                || objCDefaults.lstCVarDefaults[0].Email_DisplayName == "0" || objCDefaults.lstCVarDefaults[0].Email_DisplayName == ""
                || objCDefaults.lstCVarDefaults[0].Email_Host == "0" || objCDefaults.lstCVarDefaults[0].Email_Host == ""
                || objCDefaults.lstCVarDefaults[0].Email_Port == 0)
                _MessageReturned = "Please, review your company default email settings.";
            else
            {
                string pattern = "[\\~#%&*{}/:<>?|\"-]";
                string replacement = " ";
                Regex regEx = new Regex(pattern);
                pEmailData.pEmail_ReportName = Regex.Replace(regEx.Replace(pEmailData.pEmail_ReportName, replacement), @"\s+", " ");
                try
                {
                    var mail = new System.Net.Mail.MailMessage();
                    SmtpClient Client = new SmtpClient(objCDefaults.lstCVarDefaults[0].Email_Host, objCDefaults.lstCVarDefaults[0].Email_Port);
                    Client.UseDefaultCredentials = true;
                    Client.Credentials = new System.Net.NetworkCredential(objCDefaults.lstCVarDefaults[0].Email, CEncryptDecrypt.Decrypt(objCDefaults.lstCVarDefaults[0].Email_Password, true));

                    mail.From = new MailAddress(objCDefaults.lstCVarDefaults[0].Email, objCDefaults.lstCVarDefaults[0].Email_DisplayName);
                    mail.To.Add(new MailAddress(pEmailData.pEmail_To));// put to address here
                    mail.Subject = pEmailData.pEmail_Subject;  // put subject here  


                    if (objCDefaults.lstCVarDefaults[0].Email_Footer != null && objCDefaults.lstCVarDefaults[0].Email_Footer != "")
                    {
                        mail.Body = pEmailData.pEmail_Body + " <br><br> " + objCDefaults.lstCVarDefaults[0].Email_Footer;
                    }
                    else
                    {
                        mail.Body = pEmailData.pEmail_Body + "<br><br>" + "Thanks & best regards.<br> ";
                    }


                    mail.IsBodyHtml = true;
                    Client.EnableSsl = objCDefaults.lstCVarDefaults[0].Email_IsSSL;
                    Client.Send(mail);
                }
                catch (Exception ex)
                {
                    _MessageReturned = ex.Message;
                }
                //if (File.Exists(pathString))
                //    File.Delete(pathString);
            }
            return new Object[] {
                _MessageReturned //data[0]
            };
        }
    } //of class

    public class UpdateDefaultsData
    {
        public Int32 pID { get; set; }
        public String pCompanyName { get; set; }
        public String pCompanyLocalName { get; set; }
        public Int32 pBranchID { get; set; }
        public String pAddressLine1 { get; set; }
        public String pAddressLine2 { get; set; }
        public String pAddressLine3 { get; set; }
        public String pAddressLine4 { get; set; }
        public String pAddressLine5 { get; set; }
        public String pPhones { get; set; }
        public String pFaxes { get; set; }
        public String pEmail { get; set; }
        public String pWebsite { get; set; }
        public String pBankName { get; set; }
        public String pAccountName { get; set; }
        public String pBankAddress { get; set; }
        public String pSwiftCode { get; set; }
        public String pAccountNumber { get; set; }
        public string pTaxNumber { get; set; }
        public int pImportOceanDays { get; set; }
        public int pImportAirDays { get; set; }
        public int pImportInlandDays { get; set; }
        public int pExportOceanDays { get; set; }
        public int pExportAirDays { get; set; }
        public int pExportInlandDays { get; set; }
        public int pDomesticOceanDays { get; set; }
        public int pDomesticAirDays { get; set; }
        public int pDomesticInlandDays { get; set; }
        public int pSmallBusinessBelow { get; set; }
        public int pMediumBusinessBelow { get; set; }
        public bool pIsDepartmentOption { get; set; }
        public int pCurrencyID { get; set; }
        public int pForeignCurrencyID { get; set; }
        public string pCommericalRegNo { get; set; }
        public string pVatIDNo { get; set; }
        public string pInvoiceLeftPosition { get; set; }
        public string pInvoiceLeftSignature { get; set; }
        public string pInvoiceMiddlePosition { get; set; }
        public string pInvoiceMiddleSignature { get; set; }
        public string pInvoiceRightPosition { get; set; }
        public string pInvoiceRightSignature { get; set; }
        public string pEmail_Password { get; set; }
        public string pEmail_DisplayName { get; set; }
        public string pEmail_Host { get; set; }
        public string pEmail_Port { get; set; }
        public string pEmail_IsSSL { get; set; }
        public string pEmail_Header { get; set; }
        public string pEmail_Footer { get; set; }
        public string pShowUserSalesmen { get; set; }
        public bool pIsAddChargeAuto { get; set; }
    }
    
    public class SendEmailWithAttachment
    {
        public string pFileName { get; set; }
        public string pMapPath { get; set; }
        public string pEmail_Subject { get; set; }
        public string pEmail_To { get; set; }
        public string pEmail_Body { get; set; }
    }

    public class TestEmailData
    {
        public String pEmail { get; set; }
        public string pEmail_Password { get; set; }
        public string pEmail_DisplayName { get; set; }
        public string pEmail_Host { get; set; }
        public string pEmail_Port { get; set; }
        public bool pEmail_IsSSL { get; set; }
        public string pEmail_Header { get; set; }
        public string pEmail_Footer { get; set; }
        public string pEmail_Subject { get; set; }
        public string pEmail_To { get; set; }
        public string pEmail_Body { get; set; }
    }


    public class PDFEmailData
    {
        public String pEmail { get; set; }
        public string pEmail_Password { get; set; }
        public string pEmail_DisplayName { get; set; }
        public string pEmail_Host { get; set; }
        public string pEmail_Port { get; set; }
        public bool pEmail_IsSSL { get; set; }
        public string pEmail_Header { get; set; }
        public string pEmail_Footer { get; set; }
        public string pEmail_Subject { get; set; }
        public string pEmail_To { get; set; }
        public string pEmail_Body { get; set; }
        public string pHTML_ForPDF { get; set; }
        public string pHTML_ForExcel { get; set; }
        public string pEmail_ReportName { get; set; }
    }

    public class SendPDFEmail_GeneralData
    {


        public string pEmail_Subject { get; set; }
        public string pEmail_To { get; set; }
        public string pEmail_Body { get; set; }
        public string pHTML_ForPDF { get; set; }
        public string pHTML_ForExcel { get; set; }
        public string pEmail_ReportName { get; set; }
    }
    public class SendUrlEmail_GeneralData
    {
        public string pEmail_Subject { get; set; }
        public string pEmail_To { get; set; }
        public string pEmail_Body { get; set; }
        public string pHTML_ForPDF { get; set; }
        public string pHTML_ForExcel { get; set; }
        public string pEmail_ReportName { get; set; }
    }
} //of controller
