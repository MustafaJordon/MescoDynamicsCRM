using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Net;
using RestSharp;
using Newtonsoft.Json;
using RestSharp.Authenticators;
using Newtonsoft.Json.Linq;
using Forwarding.MvcApp.Models.Customized;
using System.Linq;
using System.Reflection;
using Forwarding.MvcApp.Controllers.MasterData.API_Invoicing;
using System.Windows.Ink;
using Forwarding.MvcApp.Models.DynamicsCRM;
using Forwarding.MvcApp.Models.PS.PS_Transactions.Generated;
using Forwarding.MvcApp.Controllers.LocalEmails.LocalEmails;
using Forwarding.MvcApp.Controllers.Administration.API_Security;

namespace Forwarding.MvcApp.Controllers.DynamicsCRM
{
    public class DynamicsCRMController : ApiController
    {
        private string tenantID = "18628f3c-d584-4e0f-91cd-5a2f04a0d8cb";
        private string clientID = "e2efe480-d3ff-4cb5-88d8-b3d4c3960eb1";
        private string clientSecret = "CNY8Q~HP-ViMEspfOgiklPp~itYy8qu_OJ2wUacX";
        private string url = "https://mgcrm.api.crm4.dynamics.com";
        [System.Web.Http.HttpGet]
        public string Auth()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var client = new RestClient("https://login.windows.net");
            var request = new RestRequest($"/{tenantID}/oauth2/token", Method.POST);
            request.AddHeader("Accept", "application/json");
            var body = new
            {
                grant_type = "client_credentials",
                client_id = clientID,
                client_secret = clientSecret,
                resource = url
            };
            request.AddObject(body);
            try
            {
                var response = client.Execute(request);
                var AuthResponse = (JObject)JsonConvert.DeserializeObject(response.Content);
                string access_token = AuthResponse["access_token"].Value<string>();
                return access_token;
            }
            catch (Exception error)
            {
                return error.Message;
            }

        }

        [System.Web.Http.HttpGet]
        public object GetData(string tableName) //Query data from any dynamics crm table
        {
            string access_token = Auth();

            var client = new RestClient($"{url}");
            var request = new RestRequest($"/api/data/v9.2.23022.00178/{tableName}?$top=2", Method.GET);
            request.AddHeader("Accept", "application/json");
            client.Authenticator = new JwtAuthenticator(access_token);
            try
            {
                var response = client.Execute(request);
                var Data = JsonConvert.DeserializeObject(response.Content);
                return Data;
            }
            catch (Exception error)
            {
                return error.Message;
            }
        }   
        //[System.Web.Http.HttpGet] //Get all master data from dynamics crm .. I have commented these as we only need to get the master data which are linked to a Quotation 
        //public object GetCustomers()
        //{
        //    string access_token = Auth();
        //    var client = new RestClient($"{url}");
        //    var request = new RestRequest($"/api/data/v9.2.23022.00178/accounts?$select=name,mesco_arabicname,xollsp_vatnumber", Method.GET);
        //    string id;  //To store ForwardingID
        //    request.AddHeader("Accept", "application/json");
        //    client.Authenticator = new JwtAuthenticator(access_token);
        //    try
        //    {
        //        var response = client.Execute(request);
        //        var Data = (JObject)JsonConvert.DeserializeObject(response.Content);
        //        var results = Data.SelectToken("value");    //array of objects
        //        List<string> ExistingRecords = new List<string>();
        //        CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
        //        ExistingRecords = objCCustomizedDBCall.ExecuteQuery_Array($"select crmID from vwcrmCustomers");   //check if customer already existed
        //        foreach (var customer in results)
        //        {
        //            if (!ExistingRecords.Contains(customer["accountid"].ToString()))
        //            {
        //                string NumberOfA_JVDetails = objCCustomizedDBCall.CallStringFunction($"insert into customers(name,LocalName,VATNumber,notes,IsInternalCustomer,ManagerRoleID,AdministratorRoleID) values(N'{(string)customer["name"]}', N'{(string)customer["mesco_arabicname"]}','{(string)customer["xollsp_vatnumber"]}','{(string)customer["accountid"]}', 1, 5, 1)");     //insert customer from CRM to customers table
        //                //Getting the ID of the last inserted record
        //                id = objCCustomizedDBCall.ExecuteQuery_Array($"select MAX(ID)ID from Customers")[0];
        //                NumberOfA_JVDetails = objCCustomizedDBCall.CallStringFunction($"insert into crmIDs(crmTableName,ForwardingTableName,crmID,ForwardingID) values('accounts','Customers','{customer["accountid"]}',{id})");
        //            }
        //        }
        //        return Data;
        //    }
        //    catch (Exception error)
        //    {
        //        return error.Message;
        //    }
        //}

        //[System.Web.Http.HttpGet]
        //public object GetBranches() //Getting branches from CRM business units
        //{
        //    string access_token = Auth();
        //    var client = new RestClient($"{url}");
        //    var request = new RestRequest($"/api/data/v9.2.23022.00178/businessunits?$select=name", Method.GET);
        //    string id;  //To store ForwardingID
        //    request.AddHeader("Accept", "application/json");
        //    client.Authenticator = new JwtAuthenticator(access_token);
        //    try
        //    {
        //        var response = client.Execute(request);
        //        var Data = (JObject)JsonConvert.DeserializeObject(response.Content);
        //        var results = Data.SelectToken("value");    //array of objects
        //        List<string> ExistingRecords = new List<string>();
        //        CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
        //        ExistingRecords = objCCustomizedDBCall.ExecuteQuery_Array($"select crmID from vwcrmBranches");   //check if Branch already existed
        //        foreach (var Branch in results)
        //        {
        //            if (!ExistingRecords.Contains(Branch["businessunitid"].ToString()))
        //            {
        //                string NumberOfA_JVDetails = objCCustomizedDBCall.CallStringFunction($"insert into Branches(name,LocalName,Code) values('{Branch["name"]}','{Branch["name"]}','{Branch["name"].ToString().Substring(0,3)}')");
        //                //Getting the ID of the last inserted record
        //                id = objCCustomizedDBCall.ExecuteQuery_Array($"select MAX(ID)ID from Branches")[0];
        //                NumberOfA_JVDetails = objCCustomizedDBCall.CallStringFunction($"insert into crmIDs(crmTableName,ForwardingTableName,crmID,ForwardingID) values('businessunits','Branches','{Branch["businessunitid"]}',{id})");
        //            }
        //        }
        //        return Data;
        //    }
        //    catch (Exception error)
        //    {
        //        return error.Message;
        //    }
        //}

        //[System.Web.Http.HttpGet]
        //public object GetUsers() //Getting users from CRM systemusers table
        //{
        //    string access_token = Auth();
        //    var client = new RestClient($"{url}");
        //    var request = new RestRequest($"/api/data/v9.2.23022.00178/systemusers?$select=fullname", Method.GET);
        //    string id;  //To store ForwardingID
        //    request.AddHeader("Accept", "application/json");
        //    client.Authenticator = new JwtAuthenticator(access_token);
        //    try
        //    {
        //        var response = client.Execute(request);
        //        var Data = (JObject)JsonConvert.DeserializeObject(response.Content);
        //        var results = Data.SelectToken("value");    //array of objects
        //        List<string> ExistingRecords = new List<string>();
        //        CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
        //        ExistingRecords = objCCustomizedDBCall.ExecuteQuery_Array($"select crmID from vwcrmUsers");   //check if user already existed
        //        foreach (var User in results)
        //        {
        //            if (!ExistingRecords.Contains(User["systemuserid"].ToString()))
        //            {
        //                string NumberOfA_JVDetails = objCCustomizedDBCall.CallStringFunction($"insert into users(Username,Name,LocalName) values('{User["fullname"]}','{User["fullname"]}','{User["fullname"]}')");
        //                //Getting the ID of the last inserted record
        //                id = objCCustomizedDBCall.ExecuteQuery_Array($"select MAX(ID)ID from users")[0];
        //                NumberOfA_JVDetails = objCCustomizedDBCall.CallStringFunction($"insert into crmIDs(crmTableName,ForwardingTableName,crmID,ForwardingID) values('systemusers','Users','{User["systemuserid"]}',{id})");
        //            }
        //        }
        //        return Data;
        //    }
        //    catch (Exception error)
        //    {
        //        return error.Message;
        //    }
        //}

        //[System.Web.Http.HttpGet]
        //public object GetCommodityGroups() //Getting commodities from CRM CommodityGroups table
        //{
        //    string access_token = Auth();
        //    var client = new RestClient($"{url}");
        //    var request = new RestRequest($"/api/data/v9.2.23022.00178/xollsp_commoditygroups?$select=xollsp_name", Method.GET);
        //    request.AddHeader("Accept", "application/json");
        //    client.Authenticator = new JwtAuthenticator(access_token);
        //    try
        //    {
        //        var response = client.Execute(request);
        //        var Data = (JObject)JsonConvert.DeserializeObject(response.Content);
        //        var results = Data.SelectToken("value");    //array of objects
        //        string id;  //To store ForwardingID
        //        List<string> ExistingRecords = new List<string>();
        //        CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
        //        ExistingRecords = objCCustomizedDBCall.ExecuteQuery_Array($"select crmID from vwcrmCommodities");   //check if commodity already existed
        //        foreach (var Commodity in results)
        //        {
        //            if (!ExistingRecords.Contains(Commodity["xollsp_commoditygroupid"].ToString()))
        //            {
        //                string NumberOfA_JVDetails = objCCustomizedDBCall.CallStringFunction($"insert into Commodities(Name,LocalName) values('{Commodity["xollsp_name"]}','{Commodity["xollsp_name"]}')");
        //                //Getting the ID of the last inserted record
        //                id = objCCustomizedDBCall.ExecuteQuery_Array($"select MAX(ID)ID from Commodities")[0];
        //                NumberOfA_JVDetails = objCCustomizedDBCall.CallStringFunction($"insert into crmIDs(crmTableName,ForwardingTableName,crmID,ForwardingID) values('xollsp_commoditygroups','Commodities','{Commodity["xollsp_commoditygroupid"]}',{id})");
        //            }
        //        }
        //        return Data;
        //    }
        //    catch (Exception error)
        //    {
        //        return error.Message;
        //    }
        //}

        //[System.Web.Http.HttpGet]
        //public object GetLogisticServices() //Getting MoveTypes(service scope) from CRM LogisticServices table
        //{
        //    string access_token = Auth();
        //    var client = new RestClient($"{url}");
        //    var request = new RestRequest($"/api/data/v9.2.23022.00178/xollsp_servicedefinitions?$select=xollsp_name", Method.GET);
        //    request.AddHeader("Accept", "application/json");
        //    client.Authenticator = new JwtAuthenticator(access_token);
        //    try
        //    {
        //        var response = client.Execute(request);
        //        var Data = (JObject)JsonConvert.DeserializeObject(response.Content);
        //        var results = Data.SelectToken("value");    //array of objects
        //        string id;  //To store ForwardingID
        //        List<string> ExistingRecords = new List<string>();
        //        CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
        //        ExistingRecords = objCCustomizedDBCall.ExecuteQuery_Array($"select crmID from vwcrmMoveTypes");   //check if movetype already existed
        //        foreach (var MoveType in results)
        //        {
        //            if (!ExistingRecords.Contains(MoveType["xollsp_servicedefinitionid"].ToString()))
        //            {
        //                string NumberOfA_JVDetails = objCCustomizedDBCall.CallStringFunction($"insert into MoveTypes(Name,LocalName,IsWarehousing,IsOcean,IsAir,IsInland,IsCustomsClearance) values('{MoveType["xollsp_name"]}','{MoveType["xollsp_name"].ToString()}',0,1,1,1,0)");
        //                //Getting the ID of the last inserted record
        //                id = objCCustomizedDBCall.ExecuteQuery_Array($"select MAX(ID)ID from MoveTypes")[0];
        //                NumberOfA_JVDetails = objCCustomizedDBCall.CallStringFunction($"insert into crmIDs(crmTableName,ForwardingTableName,crmID,ForwardingID) values('xollsp_servicedefinitions','MoveTypes','{MoveType["xollsp_servicedefinitionid"]}',{id})");
        //            }
        //        }
        //        return Data;
        //    }
        //    catch (Exception error)
        //    {
        //        return error.Message;
        //    }
        //}

        //[System.Web.Http.HttpGet]
        //public object GetCountries() //Getting Countries from CRM
        //{
        //    string access_token = Auth();
        //    var client = new RestClient($"{url}");
        //    var request = new RestRequest($"/api/data/v9.2.23022.00178/xollsp_countries?$select=xollsp_name", Method.GET);
        //    request.AddHeader("Accept", "application/json");
        //    client.Authenticator = new JwtAuthenticator(access_token);
        //    try
        //    {
        //        var response = client.Execute(request);
        //        var Data = (JObject)JsonConvert.DeserializeObject(response.Content);
        //        var results = Data.SelectToken("value");    //array of objects
        //        string id;  //To store ForwardingID
        //        int counter = 0;    //To keep the country code constraint
        //        List<string> ExistingRecords = new List<string>();
        //        CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
        //        ExistingRecords = objCCustomizedDBCall.ExecuteQuery_Array($"select name from Countries");   //check if Country already exists
        //        foreach (var Country in results)
        //        {
        //            if (!ExistingRecords.Contains(Country["xollsp_name"].ToString().ToUpper()))
        //            {
        //                string NumberOfA_JVDetails = objCCustomizedDBCall.CallStringFunction($"insert into countries(name,RegionID,Code) values('{Country["xollsp_name"].ToString().ToUpper()}',1,'{counter++}')");
        //                id = objCCustomizedDBCall.ExecuteQuery_Array($"select MAX(ID)ID from Countries")[0];
        //                objCCustomizedDBCall.CallStringFunction($"insert into crmIDs(crmTableName,ForwardingTableName,crmID,ForwardingID) values('xollsp_countries','Countries','{Country["xollsp_countryid"]}',{id})");
        //            }
        //            else
        //            {
        //                var crmID = objCCustomizedDBCall.ExecuteQuery_Array($"select crmID from vwcrmCountries where name = '{Country["xollsp_name"].ToString().ToUpper()}'");
        //                if(crmID.Count == 0)    //Existed country with no crmID
        //                {
        //                    id = objCCustomizedDBCall.ExecuteQuery_Array($"select ID from Countries where name = '{Country["xollsp_name"].ToString().ToUpper()}'")[0];
        //                    objCCustomizedDBCall.CallStringFunction($"insert into crmIDs(crmTableName,ForwardingTableName,crmID,ForwardingID) values('xollsp_countries','Countries','{Country["xollsp_countryid"]}',{id})");
        //                }
        //            }
        //        }
        //        return Data;
        //    }
        //    catch (Exception error)
        //    {
        //        return error.Message;
        //    }
        //}

        //[System.Web.Http.HttpGet]
        //public object GetPorts() //Getting Ports from CRM Addresses table
        //{
        //    string access_token = Auth();
        //    var client = new RestClient($"{url}");
        //    var request = new RestRequest($"/api/data/v9.2.23022.00178/xollsp_addresses?$select=xollsp_name,_xollsp_countryid_value", Method.GET);
        //    request.AddHeader("Accept", "application/json");
        //    client.Authenticator = new JwtAuthenticator(access_token);
        //    try
        //    {
        //        var response = client.Execute(request);
        //        var Data = (JObject)JsonConvert.DeserializeObject(response.Content);
        //        var results = Data["value"].ToList();    //array of objects
        //        //maximum returned records for a single request is 5000 record, the next page of data will be in @odata.nextLink
        //        while (Data.ContainsKey("@odata.nextLink"))
        //        {
        //            client = new RestClient($"{Data["@odata.nextLink"]}");
        //            request = new RestRequest(Method.GET);
        //            request.AddHeader("Accept", "application/json");
        //            client.Authenticator = new JwtAuthenticator(access_token);
        //            try
        //            {
        //                response = client.Execute(request);
        //                Data = (JObject)JsonConvert.DeserializeObject(response.Content);
        //                var nextLinkData = Data["value"].ToList();
        //                results = results.Concat(nextLinkData).ToList();
        //            }
        //            catch (Exception error)
        //            {
        //                return error.Message;
        //            }
        //        }
        //        string id;  //To store ForwardingID
        //        int counter = 0;
        //        List<string> ExistingRecords = new List<string>();
        //        CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
        //        ExistingRecords = objCCustomizedDBCall.ExecuteQuery_Array($"select crmID from vwcrmPorts");   //check if movetype already existed
        //        foreach (var Port in results)
        //        {
        //            if (!ExistingRecords.Contains(Port["xollsp_addressid"].ToString()))
        //            {
        //                if(Port["_xollsp_countryid_value"].ToString() != "")
        //                {
        //                    var CountryID = objCCustomizedDBCall.ExecuteQuery_Array($"select ID from vwcrmCountries where crmID = '{Port["_xollsp_countryid_value"]}'")[0];
        //                    objCCustomizedDBCall.CallStringFunction($"insert into ports(name,code,IsPort,CountryID) values('{Port["xollsp_name"].ToString().ToUpper()}','{counter++}crm',1,{CountryID})");
        //                }
        //                else
        //                {
        //                    objCCustomizedDBCall.CallStringFunction($"insert into ports(name,code,IsPort) values('{Port["xollsp_name"].ToString().ToUpper()}','{counter++}crm',1)");
        //                }
        //                //Getting the ID of the last inserted record
        //                id = objCCustomizedDBCall.ExecuteQuery_Array($"select MAX(ID)ID from Ports")[0];
        //                objCCustomizedDBCall.CallStringFunction($"insert into crmIDs(crmTableName,ForwardingTableName,crmID,ForwardingID) values('xollsp_addresses','Ports','{Port["xollsp_addressid"]}',{id})");
        //            }
        //        }
        //        return Data;
        //    }
        //    catch (Exception error)
        //    {
        //        return error.Message;
        //    }
        //}

        //[System.Web.Http.HttpGet]
        //public object GetIncoterms() //Getting Incoterms from CRM
        //{
        //    string access_token = Auth();
        //    var client = new RestClient($"{url}");
        //    var request = new RestRequest($"/api/data/v9.2.23022.00178/xollsp_incoterms?$select=xollsp_name,xollsp_description", Method.GET);
        //    string id;  //To store ForwardingID
        //    request.AddHeader("Accept", "application/json");
        //    client.Authenticator = new JwtAuthenticator(access_token);
        //    try
        //    {
        //        var response = client.Execute(request);
        //        var Data = (JObject)JsonConvert.DeserializeObject(response.Content);
        //        var results = Data.SelectToken("value");    //array of objects
        //        List<string> ExistingRecords = new List<string>();
        //        CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
        //        ExistingRecords = objCCustomizedDBCall.ExecuteQuery_Array($"select crmID from vwcrmIncoterms");   //check if movetype already existed
        //        foreach (var Incoterm in results)
        //        {
        //            if (!ExistingRecords.Contains(Incoterm["xollsp_incotermid"].ToString()) /*NoofExistedRecords == "0"*/ )
        //            {
        //                string NumberOfA_JVDetails = objCCustomizedDBCall.CallStringFunction($"insert into Incoterms(code,name,LocalName) values('{Incoterm["xollsp_name"]}','{Incoterm["xollsp_name"]} - {Incoterm["xollsp_description"]}','{Incoterm["xollsp_name"]} - {Incoterm["xollsp_description"]}')");
        //                //Getting the ID of the last inserted record
        //                id = objCCustomizedDBCall.ExecuteQuery_Array($"select id from Incoterms order by id desc")[0];
        //                NumberOfA_JVDetails = objCCustomizedDBCall.CallStringFunction($"insert into crmIDs(crmTableName,ForwardingTableName,crmID,ForwardingID) values('xollsp_incoterms','Incoterms','{Incoterm["xollsp_incotermid"]}',{id})");
        //            }
        //        }
        //        return Data;
        //    }
        //    catch (Exception error)
        //    {
        //        return error.Message;
        //    }
        //} 
        public int GetMasterData(string pMasterDataName , string pcrmID)
        {
            int FowardingID = 0;
            var ExistingID = new List<string>();
            CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
            string access_token = Auth();
            var client = new RestClient($"{url}");
            var request = new RestRequest();
            request.AddHeader("Accept", "application/json");
            client.Authenticator = new JwtAuthenticator(access_token);
            switch (pMasterDataName)
            {
                #region Branch
                case "Branch":
                    ExistingID = objCCustomizedDBCall.ExecuteQuery_Array($"select ID from vwcrmBranches where crmID = '{pcrmID}'");
                    if(ExistingID.Count == 0)
                    {
                        request = new RestRequest($"/api/data/v9.2.23022.00178/businessunits({pcrmID})", Method.GET);
                        try
                        {
                            var response = client.Execute(request);
                            var Data = (JObject)JsonConvert.DeserializeObject(response.Content);
                            objCCustomizedDBCall.CallStringFunction($"insert into Branches(name,LocalName,Code) values('{Data["name"]}','{Data["name"]}','{Data["name"].ToString().Substring(0, 3)}')");
                            FowardingID = int.Parse(objCCustomizedDBCall.ExecuteQuery_Array($"select MAX(ID)ID from Branches")[0]);
                            objCCustomizedDBCall.CallStringFunction($"insert into crmIDs(crmTableName,ForwardingTableName,crmID,ForwardingID) values('businessunits','Branches','{Data["businessunitid"]}',{FowardingID})");
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine(error.Message);
                        }
                    }
                    else
                    {
                        FowardingID = int.Parse(ExistingID[0].ToString());
                    }
                    break;
                #endregion

                #region User
                case "User":
                    ExistingID = objCCustomizedDBCall.ExecuteQuery_Array($"select ID from vwcrmUsers where crmID = '{pcrmID}'");
                    if (ExistingID.Count == 0)
                    {
                        request = new RestRequest($"/api/data/v9.2.23022.00178/systemusers({pcrmID})", Method.GET);
                        try
                        {
                            var response = client.Execute(request);
                            var Data = (JObject)JsonConvert.DeserializeObject(response.Content);
                            objCCustomizedDBCall.CallStringFunction($"insert into users(Username,Name,LocalName) values('{Data["fullname"]}','{Data["fullname"]}','{Data["fullname"]}')");
                            FowardingID = int.Parse(objCCustomizedDBCall.ExecuteQuery_Array($"select MAX(ID)ID from users")[0]);
                            objCCustomizedDBCall.CallStringFunction($"insert into crmIDs(crmTableName,ForwardingTableName,crmID,ForwardingID) values('systemusers','Users','{Data["systemuserid"]}',{FowardingID})");
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine(error.Message);
                        }
                    }
                    else
                    {
                        FowardingID = int.Parse(ExistingID[0].ToString());
                    }
                    break;
                #endregion

                #region Customer
                case "Customer":
                    ExistingID = objCCustomizedDBCall.ExecuteQuery_Array($"select ID from vwcrmCustomers where crmID = '{pcrmID}'");
                    if (ExistingID.Count == 0)
                    {
                        request = new RestRequest($"/api/data/v9.2.23022.00178/accounts({pcrmID})", Method.GET);
                        try
                        {
                            var response = client.Execute(request);
                            var Data = (JObject)JsonConvert.DeserializeObject(response.Content);
                            objCCustomizedDBCall.CallStringFunction($"insert into customers(name,LocalName,VATNumber,notes,IsInternalCustomer,ManagerRoleID,AdministratorRoleID,IsConsignee,IsShipper) values(N'{Data["name"]}', N'{(Data["mesco_arabicname"].ToString() == "" ? Data["name"] : Data["mesco_arabicname"])}','{(string)Data["xollsp_vatnumber"]}','{(string)Data["accountid"]}', 1, 5, 1, 1, 1)");
                            FowardingID = int.Parse(objCCustomizedDBCall.ExecuteQuery_Array($"select MAX(ID)ID from Customers")[0]);
                            objCCustomizedDBCall.CallStringFunction($"insert into crmIDs(crmTableName,ForwardingTableName,crmID,ForwardingID) values('accounts','Customers','{Data["accountid"]}',{FowardingID})");
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine(error.Message);
                        }
                    }
                    else
                    {
                        FowardingID = int.Parse(ExistingID[0].ToString());
                    }
                    break;
                #endregion

                #region Incoterm
                case "Incoterm":
                    request = new RestRequest($"/api/data/v9.2.23022.00178/xollsp_incoterms({pcrmID})", Method.GET);
                    try
                    {
                        var response = client.Execute(request);
                        var Data = (JObject)JsonConvert.DeserializeObject(response.Content);
                        var ExistingCode = objCCustomizedDBCall.ExecuteQuery_Array($"select Code from Incoterms");
                        if (!ExistingCode.Contains(Data["xollsp_name"].ToString()))
                        {
                            objCCustomizedDBCall.CallStringFunction($"insert into Incoterms(code,name,LocalName) values('{Data["xollsp_name"]}','{Data["xollsp_name"]} - {Data["xollsp_description"]}','{Data["xollsp_name"]} - {Data["xollsp_description"]}')");
                            FowardingID = int.Parse(objCCustomizedDBCall.ExecuteQuery_Array($"select MAX(ID)ID from Incoterms")[0]);
                            objCCustomizedDBCall.CallStringFunction($"insert into crmIDs(crmTableName,ForwardingTableName,crmID,ForwardingID) values('xollsp_incoterms','Incoterms','{Data["xollsp_incotermid"]}',{FowardingID})");
                        }
                        else
                        {
                            FowardingID = int.Parse(objCCustomizedDBCall.ExecuteQuery_Array($"select ID from Incoterms where Code = '{Data["xollsp_name"]}'")[0]);
                        }
                        
                    }
                    catch (Exception error)
                    {
                        Console.WriteLine(error.Message);
                    }
                    
                    break;
                #endregion

                #region Commodity
                case "Commodity":
                    ExistingID = objCCustomizedDBCall.ExecuteQuery_Array($"select ID from vwcrmCommodities where crmID = '{pcrmID}'");
                    if (ExistingID.Count == 0)
                    {
                        request = new RestRequest($"/api/data/v9.2.23022.00178/xollsp_commoditygroups({pcrmID})", Method.GET);
                        try
                        {
                            var response = client.Execute(request);
                            var Data = (JObject)JsonConvert.DeserializeObject(response.Content);
                            objCCustomizedDBCall.CallStringFunction($"insert into Commodities(Name,LocalName) values('{Data["xollsp_name"]}','{Data["xollsp_name"]}')");
                            FowardingID = int.Parse(objCCustomizedDBCall.ExecuteQuery_Array($"select MAX(ID)ID from Commodities")[0]);
                            objCCustomizedDBCall.CallStringFunction($"insert into crmIDs(crmTableName,ForwardingTableName,crmID,ForwardingID) values('xollsp_commoditygroups','Commodities','{Data["xollsp_commoditygroupid"]}',{FowardingID})");
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine(error.Message);
                        }
                    }
                    else
                    {
                        FowardingID = int.Parse(ExistingID[0].ToString());
                    }
                    break;
                #endregion

                #region Port
                case "Port":
                    ExistingID = objCCustomizedDBCall.ExecuteQuery_Array($"select ID from vwcrmPorts where crmID = '{pcrmID}'");
                    var Code = int.Parse(objCCustomizedDBCall.ExecuteQuery_Array($"select MAX(ID)ID from Ports")[0]);
                    if (ExistingID.Count == 0)
                    {
                        request = new RestRequest($"/api/data/v9.2.23022.00178/xollsp_addresses({pcrmID})", Method.GET);
                        try
                        {
                            var response = client.Execute(request);
                            var Data = (JObject)JsonConvert.DeserializeObject(response.Content);
                            //To keep unique code as dynamics CRM doesn't have a port code                  
                            if (Data["_xollsp_countryid_value"].ToString() == "")
                                objCCustomizedDBCall.CallStringFunction($"insert into ports(name,code,IsPort) values('{Data["xollsp_name"].ToString().ToUpper()}','{++Code}crm',1)");
                            else
                            {
                                int CountryID = GetMasterData("Country", pcrmID);
                                objCCustomizedDBCall.CallStringFunction($"insert into ports(name,code,IsPort,CountryID) values('{Data["xollsp_name"].ToString().ToUpper()}','{++Code}crm',1,{CountryID})");
                            }
                            FowardingID = int.Parse(objCCustomizedDBCall.ExecuteQuery_Array($"select MAX(ID)ID from Ports")[0]);
                            objCCustomizedDBCall.CallStringFunction($"insert into crmIDs(crmTableName,ForwardingTableName,crmID,ForwardingID) values('xollsp_addresses','Ports','{Data["xollsp_addressid"]}',{FowardingID})");
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine(error.Message);
                        }
                    }
                    else
                    {
                        FowardingID = int.Parse(ExistingID[0].ToString());
                    }
                    break;
                #endregion

                #region Country
                case "Country":
                    request = new RestRequest($"/api/data/v9.2.23022.00178/xollsp_addresses({pcrmID})", Method.GET);
                    Code = int.Parse(objCCustomizedDBCall.ExecuteQuery_Array($"select MAX(ID)ID from Countries")[0]);
                    try
                    {
                        var response = client.Execute(request);
                        var Data = (JObject)JsonConvert.DeserializeObject(response.Content);
                        pcrmID = Data["_xollsp_countryid_value"].ToString();
                    }
                    catch (Exception error)
                    {
                        Console.WriteLine(error.Message);
                    }
                    request = new RestRequest($"/api/data/v9.2.23022.00178/xollsp_countries({pcrmID})", Method.GET);
                    try
                    {
                        var response = client.Execute(request);
                        var Data = (JObject)JsonConvert.DeserializeObject(response.Content);
                        var ExistingName = objCCustomizedDBCall.ExecuteQuery_Array($"select name from Countries");
                        if (!ExistingName.Contains(Data["xollsp_name"].ToString().ToUpper()))
                        {
                            objCCustomizedDBCall.CallStringFunction($"insert into countries(name,RegionID,Code) values('{Data["xollsp_name"].ToString().ToUpper()}',1,'{++Code}')");
                            FowardingID = int.Parse(objCCustomizedDBCall.ExecuteQuery_Array($"select MAX(ID)ID from Countries")[0]);
                            objCCustomizedDBCall.CallStringFunction($"insert into crmIDs(crmTableName,ForwardingTableName,crmID,ForwardingID) values('xollsp_countries','Countries','{Data["xollsp_countryid"]}',{FowardingID})");
                        }
                        else
                        {
                            FowardingID = int.Parse(objCCustomizedDBCall.ExecuteQuery_Array($"select ID from Countries where Name = '{Data["xollsp_name"].ToString().ToUpper()}'")[0]);
                        }

                    }
                    catch (Exception error)
                    {
                        Console.WriteLine(error.Message);
                    }

                    break;
                #endregion

                #region LogisticService
                case "LogisticService":
                    ExistingID = objCCustomizedDBCall.ExecuteQuery_Array($"select ID from vwcrmMoveTypes where crmID = '{pcrmID}'");
                    if (ExistingID.Count == 0)
                    {
                        request = new RestRequest($"/api/data/v9.2.23022.00178/xollsp_servicedefinitions({pcrmID})", Method.GET);
                        try
                        {
                            var response = client.Execute(request);
                            var Data = (JObject)JsonConvert.DeserializeObject(response.Content);
                            objCCustomizedDBCall.CallStringFunction($"insert into MoveTypes(Name,LocalName,IsWarehousing,IsOcean,IsAir,IsInland,IsCustomsClearance) values('{Data["xollsp_name"]}','{Data["xollsp_name"]}',0,1,1,1,0)");
                            FowardingID = int.Parse(objCCustomizedDBCall.ExecuteQuery_Array($"select MAX(ID)ID from MoveTypes")[0]);
                            objCCustomizedDBCall.CallStringFunction($"insert into crmIDs(crmTableName,ForwardingTableName,crmID,ForwardingID) values('xollsp_servicedefinitions','MoveTypes','{Data["xollsp_servicedefinitionid"]}',{FowardingID})");
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine(error.Message);
                        }
                    }
                    else
                    {
                        FowardingID = int.Parse(ExistingID[0].ToString());
                    }
                    break;
                #endregion

                #region Currency
                case "Currency":
                    request = new RestRequest($"/api/data/v9.2.23022.00178/transactioncurrencies({pcrmID})", Method.GET);
                    try
                    {
                        var response = client.Execute(request);
                        var Data = (JObject)JsonConvert.DeserializeObject(response.Content);
                        var ExistingCode = objCCustomizedDBCall.ExecuteQuery_Array($"select Code from Currencies");
                        if (!ExistingCode.Contains(Data["isocurrencycode"].ToString()))
                        {
                            objCCustomizedDBCall.CallStringFunction($"insert into Currencies(Code,Name,LocalName) values('{Data["isocurrencycode"]}','{Data["currencyname"]}','{Data["currencyname"]}')");
                            FowardingID = int.Parse(objCCustomizedDBCall.ExecuteQuery_Array($"select MAX(ID)ID from Currencies")[0]);
                            objCCustomizedDBCall.CallStringFunction($"insert into crmIDs(crmTableName,ForwardingTableName,crmID,ForwardingID) values('transactioncurrencies','Currencies','{Data["transactioncurrencyid"]}',{FowardingID})");
                            

                        }
                        else
                        {
                            FowardingID = int.Parse(objCCustomizedDBCall.ExecuteQuery_Array($"select ID from Currencies where Code = '{Data["isocurrencycode"]}'")[0]);
                        }

                    }
                    catch (Exception error)
                    {
                        Console.WriteLine(error.Message);
                    }
                    break;
                #endregion

                #region ChargeType
                case "ChargeType":
                    request = new RestRequest($"/api/data/v9.2.23022.00178/xollsp_servicedefinitions({pcrmID})", Method.GET);
                    try
                    {
                        var response = client.Execute(request);
                        var Data = (JObject)JsonConvert.DeserializeObject(response.Content);
                        var ExistingCode = objCCustomizedDBCall.ExecuteQuery_Array($"select Name from ChargeTypes");
                        if (!ExistingCode.Contains(Data["xollsp_name"].ToString().ToUpper()))
                        {
                            objCCustomizedDBCall.CallStringFunction($"insert into ChargeTypes(Code,Name,LocalName,MeasurementID,IsOperationChargeType,IsTank) values('{Data["xollsp_name"]}','{Data["xollsp_name"]}','{Data["xollsp_name"]}',3,1,0)");
                            FowardingID = int.Parse(objCCustomizedDBCall.ExecuteQuery_Array($"select MAX(ID)ID from ChargeTypes")[0]);
                            objCCustomizedDBCall.CallStringFunction($"insert into crmIDs(crmTableName,ForwardingTableName,crmID,ForwardingID) values('xollsp_servicedefinitions','ChargeTypes','{Data["xollsp_servicedefinitionid"]}',{FowardingID})");
                        }
                        else
                        {
                            FowardingID = int.Parse(objCCustomizedDBCall.ExecuteQuery_Array($"select ID from ChargeTypes where Name = '{Data["xollsp_name"]}'")[0]);
                        }

                    }
                    catch (Exception error)
                    {
                        Console.WriteLine(error.Message);
                    }
                    break;
                #endregion

                #region Package Type
                case "PackageType":
                    request = new RestRequest($"/api/data/v9.2.23022.00178/xollsp_unitsofmeasures({pcrmID})", Method.GET);
                    try
                    {
                        var response = client.Execute(request);
                        var Data = (JObject)JsonConvert.DeserializeObject(response.Content);
                        var ExistingCode = objCCustomizedDBCall.ExecuteQuery_Array($"select Name from PackageTypes");
                        if (!ExistingCode.Contains(Data["xollsp_name"].ToString().ToUpper()))
                        {
                            var PackageType = Data["xollsp_name"].ToString().ToUpper();
                            objCCustomizedDBCall.CallStringFunction($"insert into PackageTypes(Code,Name,LocalName,PrintAs,IsOcean,IsAir,IsInland) Values('{PackageType}','{PackageType}','{PackageType}','{PackageType}',1,1,1)");
                            FowardingID = int.Parse(objCCustomizedDBCall.ExecuteQuery_Array($"select MAX(ID)ID from PackageTypes")[0]);
                            objCCustomizedDBCall.CallStringFunction($"insert into crmIDs(crmTableName,ForwardingTableName,crmID,ForwardingID) values('xollsp_unitsofmeasures','PackageTypes','{Data["xollsp_unitsofmeasureid"]}',{FowardingID})");
                        }
                        else
                        {
                            FowardingID = int.Parse(objCCustomizedDBCall.ExecuteQuery_Array($"select ID from PackageTypes where Name = '{Data["xollsp_name"].ToString().ToUpper()}'")[0]);
                        }

                    }
                    catch (Exception error)
                    {
                        Console.WriteLine(error.Message);
                    }
                    break;
                #endregion

                #region ShippingLine
                case "ShippingLine":
                    ExistingID = objCCustomizedDBCall.ExecuteQuery_Array($"select ID from vwcrmShippingLines where crmID = '{pcrmID}'");
                    Code = int.Parse(objCCustomizedDBCall.ExecuteQuery_Array($"select MAX(ID)ID from ShippingLines")[0]);
                    if (ExistingID.Count == 0)
                    {
                        request = new RestRequest($"/api/data/v9.2.23022.00178/accounts({pcrmID})", Method.GET);
                        try
                        {
                            var response = client.Execute(request);
                            var Data = (JObject)JsonConvert.DeserializeObject(response.Content);
                            objCCustomizedDBCall.CallStringFunction($"insert into ShippingLines(Code,Name) Values('{++Code}','{Data["name"]}')");
                            FowardingID = int.Parse(objCCustomizedDBCall.ExecuteQuery_Array($"select MAX(ID)ID from ShippingLines")[0]);
                            objCCustomizedDBCall.CallStringFunction($"insert into crmIDs(crmTableName,ForwardingTableName,crmID,ForwardingID) values('accounts','ShippingLines','{Data["accountid"]}',{FowardingID})");
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine(error.Message);
                        }
                    }
                    else
                    {
                        FowardingID = int.Parse(ExistingID[0].ToString());
                    }
                    break;
                #endregion

                #region AirLine
                case "AirLine":
                    ExistingID = objCCustomizedDBCall.ExecuteQuery_Array($"select ID from vwcrmAirLines where crmID = '{pcrmID}'");
                    Code = int.Parse(objCCustomizedDBCall.ExecuteQuery_Array($"select MAX(ID)ID from AirLines")[0]);
                    if (ExistingID.Count == 0)
                    {
                        request = new RestRequest($"/api/data/v9.2.23022.00178/accounts({pcrmID})", Method.GET);
                        try
                        {
                            var response = client.Execute(request);
                            var Data = (JObject)JsonConvert.DeserializeObject(response.Content);
                            objCCustomizedDBCall.CallStringFunction($"insert into AirLines(Code,Name) Values('{++Code}','{Data["name"]}')");
                            FowardingID = int.Parse(objCCustomizedDBCall.ExecuteQuery_Array($"select MAX(ID)ID from AirLines")[0]);
                            objCCustomizedDBCall.CallStringFunction($"insert into crmIDs(crmTableName,ForwardingTableName,crmID,ForwardingID) values('accounts','AirLines','{Data["accountid"]}',{FowardingID})");
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine(error.Message);
                        }
                    }
                    else
                    {
                        FowardingID = int.Parse(ExistingID[0].ToString());
                    }
                    break;
                #endregion

                #region Trucker
                case "Trucker":
                    ExistingID = objCCustomizedDBCall.ExecuteQuery_Array($"select ID from vwcrmTruckers where crmID = '{pcrmID}'");
                    if (ExistingID.Count == 0)
                    {
                        request = new RestRequest($"/api/data/v9.2.23022.00178/accounts({pcrmID})", Method.GET);
                        try
                        {
                            var response = client.Execute(request);
                            var Data = (JObject)JsonConvert.DeserializeObject(response.Content);
                            objCCustomizedDBCall.CallStringFunction($"insert into Truckers(Name) Values('{Data["name"]}')");
                            FowardingID = int.Parse(objCCustomizedDBCall.ExecuteQuery_Array($"select MAX(ID)ID from Truckers")[0]);
                            objCCustomizedDBCall.CallStringFunction($"insert into crmIDs(crmTableName,ForwardingTableName,crmID,ForwardingID) values('accounts','Truckers','{Data["accountid"]}',{FowardingID})");
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine(error.Message);
                        }
                    }
                    else
                    {
                        FowardingID = int.Parse(ExistingID[0].ToString());
                    }
                    break;
                    #endregion
            }
            return FowardingID;
        }
        [System.Web.Http.HttpGet]
        public object GetQuotationByQuotationNumber(string QuotationNumber) //Strating point to get the quotation from CRM
        {
            try
            {
                
                string access_token = Auth();
                var client = new RestClient($"{url}");
                client.Authenticator = new JwtAuthenticator(access_token);
                List<string> ExistingRecords = new List<string>();
                CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();

                //Quotation Header
                var request = new RestRequest($"/api/data/v9.2.23022.00178/xollsp_tariffquotes?$filter=xollsp_quotenumber eq '{QuotationNumber}'", Method.GET);
                request.AddHeader("Accept", "application/json");
                var response = client.Execute(request);
                var Data = (JObject)JsonConvert.DeserializeObject(response.Content);
                var Quotation = Data["value"];    //Quotation Header
                ExistingRecords = objCCustomizedDBCall.ExecuteQuery_Array($"select crmID from vwcrmQuotations");
                //Check if the Quotation exists
                if (!ExistingRecords.Contains(Quotation[0]["xollsp_tariffquoteid"].ToString()))
                {
                    bool IsValidQuotation = false;
                    //Main Carriage Route & Quotation Cost Lines
                    request = new RestRequest($"/api/data/v9.2.23022.00178/xollsp_quotecostlines?$filter=_xollsp_tariffquote_value eq '{Quotation[0]["xollsp_tariffquoteid"]}'", Method.GET);
                    response = client.Execute(request);
                    Data = (JObject)JsonConvert.DeserializeObject(response.Content);
                    var QuoteCostLines = Data.SelectToken("value");

                    //Quotation Sales Lines
                    request = new RestRequest($"/api/data/v9.2.23022.00178/xollsp_quotesaleslines?$filter=_xollsp_tariffquote_value eq '{Quotation[0]["xollsp_tariffquoteid"]}'", Method.GET);
                    response = client.Execute(request);
                    Data = (JObject)JsonConvert.DeserializeObject(response.Content);
                    var QuoteSalesLines = Data.SelectToken("value");

                    //Validation Check
                    IsValidQuotation = ValidationCheck(Quotation[0], QuoteCostLines, QuoteSalesLines);

                    if (IsValidQuotation)   //Insert Procedure
                    {
                        InsertIntoQuotations(Quotation[0]);
                        InsertMainRoute(QuoteCostLines[0], Quotation[0]);
                        for (int i = 0; i < QuoteCostLines.Count(); i++)
                        {
                            InsertQuotationCharges(QuoteCostLines[i], QuoteSalesLines[i], Quotation[0]);
                            //insert as a package if there is a commodity in the cost line
                            if (QuoteCostLines[i]["xollsp_commoditydescription"].ToString() != "")
                                InsertQuotationContainersAndPackages(QuoteCostLines[i], Quotation[0]);
                        }
                        return Data;
                    }
                    else return null;
                }
                else return null;
            }
            catch (Exception error)
            {
                return error.Message;
            }
        }

        public bool ValidationCheck(JToken QuotationHeader, JToken CostLines, JToken SalesLines) //To be continued with the required validations
        {
            bool IsValidQuotation = true;
            string missingFields = "";
            if (QuotationHeader["xollsp_importexport"] == null) //DirectionType
                missingFields += "DirectionType-";
            else if(QuotationHeader["xollsp_importexport"].ToString() == "300000001" && QuotationHeader["_xollsp_shipper_value"].ToString() == "" && QuotationHeader["_xollsp_customer_value"].ToString() == "")    //Shipper in case of export
                missingFields += "Shipper-";
            else if (QuotationHeader["xollsp_importexport"].ToString() == "300000000" && QuotationHeader["_xollsp_consignee_value"].ToString() == "" && QuotationHeader["_xollsp_customer_value"].ToString() == "")     //Consignee in case of import 
                missingFields += "Consignee-";

            if (QuotationHeader["xollsp_transporttype"] == null) missingFields += "TransportType-"; //TransportType
            if (QuotationHeader["xollsp_loadtype"] == null) missingFields += "ShipmentType-";   //ShipmentType
            if (QuotationHeader["_owningbusinessunit_value"].ToString() == "") missingFields += "Branch-";  //Branch
            if (QuotationHeader["_owninguser_value"].ToString() == "") missingFields += "Salesman-";    //Salesman
            if (QuotationHeader["createdon"].ToString() == "") missingFields += "OpenDate-";    //OpenDate
            if (CostLines[0]["_xollsp_vendor_value"].ToString() == "" && CostLines[0]["_mesco_vendor2_value"].ToString() == "")     //AirLine-ShippingLine-Trucker
                missingFields += "Line-";
            if (QuotationHeader["_xollsp_from_value"].ToString() == "") missingFields += "POL-";    //POL
            if (QuotationHeader["_xollsp_to_value"].ToString() == "") missingFields += "POD-";    //POD
            if (QuotationHeader["_xollsp_incoterm_value"].ToString() == "") missingFields += "Incoterm-";    //Incoterm
            //if (QuotationHeader["_xollsp_commoditygroup_value"].ToString() == "") missingFields += "Commodity-";    //Commodity

            if (missingFields != "") IsValidQuotation = false;
            if (!IsValidQuotation)
            {   //Insert Into CRM Log if Quotation is not valid
                InsertIntoCRMLog(2, QuotationHeader["xollsp_tariffquoteid"].ToString(), 0, missingFields, 0, 0);
                var NotificationContent = new
                {
                    Subject = $"Quotation {QuotationHeader["xollsp_quotenumber"]} in Dynamics CRM Has missing Fields",
                    Body = $"Quotation {QuotationHeader["xollsp_quotenumber"]} missing fields are {missingFields} For further info see Dynamics CRM Log report"
                };
                SendNotification(NotificationContent.Subject, NotificationContent.Body, 0);
            }


            return IsValidQuotation;
        }

        public void InsertIntoQuotations(JToken Quotation)
        {
            string crmID;
            string Code = Quotation["xollsp_quotenumber"].ToString();
            CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
            //Branch
            int? BranchID = null;
            if (Quotation["_owningbusinessunit_value"].ToString() != "")
            {
                crmID = Quotation["_owningbusinessunit_value"].ToString();
                BranchID = GetMasterData("Branch", crmID);
            }
            //Salesman
            int? SalesmanID = null;
            if (Quotation["_owninguser_value"].ToString() != "")
            {
                crmID = Quotation["_owninguser_value"].ToString();
                SalesmanID = GetMasterData("User", crmID);
            }
            //Direction Type
            int DirectionType = 1; //import
            string DirectionIconName = "fa-arrow-circle-down";
            string DirectionIconStyle = "import-icon-style";
            if (Quotation["xollsp_importexport"].ToString() == "300000001")
            {
                DirectionType = 2; //export
                DirectionIconName = "fa-external-link";
                DirectionIconStyle = "export-icon-style";
            }
            //Transport Type
            int TransportType = 1; //Ocean
            string TransportIconName = "fa-anchor";
            string TransportIconStyle = "ocean-icon-style";
            switch (Quotation["xollsp_transporttype"].ToString())
            {
                case "300000002":   //air
                    TransportType = 2;
                    TransportIconName = "fa-plane";
                    TransportIconStyle = "air-icon-style";
                    break;
                case "300000001":   //Inland
                    TransportType = 3;
                    TransportIconName = "fa-truck";
                    TransportIconStyle = "inland-icon-style";
                    break;
            }
            //Shipment Type
            int ShipmentType = 1; //FCL
            switch (Quotation["xollsp_loadtype"].ToString())
            {
                case "300000001":   //LCL
                    ShipmentType = 2;
                    break;
            }
            //shipper
            int? ShipperID = null;
            if (DirectionType == 2)
            {
                string ShippercrmID = Quotation["_xollsp_shipper_value"].ToString() == "" ? Quotation["_xollsp_customer_value"].ToString() : Quotation["_xollsp_shipper_value"].ToString();
                if (ShippercrmID != "")
                {
                    crmID = ShippercrmID;
                    ShipperID = GetMasterData("Customer", crmID);
                }
            }
            
            //Consignee
            int? ConsigneeID = null;
            if (DirectionType == 1)
            {
                string ConsigneecrmID = Quotation["_xollsp_consignee_value"].ToString() == "" ? Quotation["_xollsp_customer_value"].ToString() : Quotation["_xollsp_consignee_value"].ToString();
                if (ConsigneecrmID != "")
                {
                    crmID = ConsigneecrmID;
                    ConsigneeID = GetMasterData("Customer", crmID);
                }
            }
            //Agent
            int? AgentID = null;
            if (Quotation["_xollsp_agent_value"].ToString() != "")
            {
                crmID = Quotation["_xollsp_agent_value"].ToString();
                AgentID = GetMasterData("Customer", crmID);
            }
            //Customer
            int? CustomerID = null;
            if (Quotation["_xollsp_customer_value"].ToString() != "")
            {
                crmID = Quotation["_xollsp_customer_value"].ToString();
                CustomerID = GetMasterData("Customer", crmID);
            }
            //Incoterm
            int? IncotermID = null;
            if (Quotation["_xollsp_incoterm_value"].ToString() != "")
            {
                crmID = Quotation["_xollsp_incoterm_value"].ToString();
                IncotermID = GetMasterData("Incoterm", crmID);
            }
            //Commodity
            int? CommodityID = null;
            if (Quotation["_xollsp_commoditygroup_value"].ToString() != "")
            {
                crmID = Quotation["_xollsp_commoditygroup_value"].ToString();
                CommodityID = GetMasterData("Commodity", crmID);
            }
            //TransitTime
            int? TransientTime = null;
            if (Quotation["new_transittime"].ToString() != "") {
                TransientTime = int.Parse(Quotation["new_transittime"].ToString());
            }
            //FreeTime
            int? FreeTime = null;
            if (Quotation["mesco_freedays"].ToString() != "")
            {
                FreeTime = int.Parse(Quotation["mesco_freedays"].ToString());
            }
            DateTime OpenDate = (DateTime)Quotation["createdon"];   //OpenDate
            DateTime ExpirationDate = (DateTime)Quotation["xollsp_effectiveto"];   //ExpirationDate
            string DescriptionOfGoods = (string)Quotation["xollsp_commoditydetails"];   //DescriptionOfGoods

            //Insert into Quotations table
            string InsertQuery = "insert into quotations ("
                + (Code == null ? "" : "Code,")
                + (BranchID == null ? "" : "BranchID,")
                + (SalesmanID == null ? "" : "SalesmanID,")
                + "DirectionType,"
                + (DirectionIconName == null ? "" : "DirectionIconName,")
                + (DirectionIconStyle == null ? "" : "DirectionIconStyle,")
                + "TransportType,"
                + (TransportIconName == null ? "" : "TransportIconName,")
                + (TransportIconStyle == null ? "" : "TransportIconStyle,")
                + "ShipmentType,"
                + "IncludePickup,"
                + "IncludeDelivery,"
                + "IsDangerousGoods,"
                + (ShipperID == null ? "" : "ShipperID,")
                + (ConsigneeID == null ? "" : "ConsigneeID,")
                + (AgentID == null ? "" : "AgentID,")
                + (CustomerID == null ? "" : "CustomerID,")
                + (IncotermID == null ? "" : "IncotermID,")
                + (CommodityID == null ? "" : "CommodityID,")
                + (TransientTime == null ? "" : "TransientTime,")
                + (FreeTime == null ? "" : "FreeTime,")
                + (OpenDate == null ? "" : "OpenDate,")
                + (ExpirationDate == null ? "" : "ExpirationDate,")
                + (DescriptionOfGoods == null ? "" : "DescriptionOfGoods,")
                + "QuotationStageID,"
                + (SalesmanID == null ? "" : "CreatorUserID,")    //Salesman is the same as creator user in dynamics crm
                + "CreationDate"
                + ") ";
            //Values
            InsertQuery += "Values ("
                + (Code == null ? "" : $"'{Code}',")
                + (BranchID == null ? "" : $"{BranchID},")
                + (SalesmanID == null ? "" : $"{SalesmanID},")
                + $"{DirectionType},"
                + (DirectionIconName == null ? "" : $"'{DirectionIconName}',")
                + (DirectionIconStyle == null ? "" : $"'{DirectionIconStyle}',")
                + $"{TransportType},"
                + (TransportIconName == null ? "" : $"'{TransportIconName}',")
                + (TransportIconStyle == null ? "" : $"'{TransportIconStyle}',")
                + $"{ShipmentType},"
                + "0,"  //IncludePickup (Mandatory field)
                + "0,"  //IncludeDelivery (Mandatory field)
                + "0,"  //IsDangerousGoods (Mandatory field)
                + (ShipperID == null ? "" : $"{ShipperID},")
                + (ConsigneeID == null ? "" : $"{ConsigneeID},")
                + (AgentID == null ? "" : $"{AgentID},")
                + (CustomerID == null ? "" : $"{CustomerID},")
                + (IncotermID == null ? "" : $"{IncotermID},")
                + (CommodityID == null ? "" : $"{CommodityID},")
                + (TransientTime == null ? "" : $"{TransientTime},")
                + (FreeTime == null ? "" : $"{FreeTime},")
                + (OpenDate == null ? "" : $"CONVERT(VARCHAR, '{OpenDate}', 103),")
                + (ExpirationDate == null ? "" : $"CONVERT(VARCHAR, '{ExpirationDate}', 103),")
                + (DescriptionOfGoods == null ? "" : $"'{DescriptionOfGoods}',")
                + "4,"  //Quotation Stage (Accepted = 4)
                + (SalesmanID == null ? "" : $"{SalesmanID},")
                + $"CONVERT(VARCHAR, '{DateTime.Now}', 103)"
                + ")";

            //Executing insert query
            try
            {
                string id;
                objCCustomizedDBCall.CallStringFunction(InsertQuery);
                id = objCCustomizedDBCall.ExecuteQuery_Array($"select MAX(ID)ID from Quotations")[0];
                objCCustomizedDBCall.CallStringFunction($"insert into crmIDs(crmTableName,ForwardingTableName,crmID,ForwardingID) values('xollsp_tariffquotes','Quotations','{Quotation["xollsp_tariffquoteid"]}',{id})");
                InsertIntoCRMLog(1, Quotation["xollsp_tariffquoteid"].ToString(), int.Parse(id), "", 0, 0);
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }

        }

        public void InsertMainRoute(JToken CostLine, JToken QuotationHeader)
        {
            string crmID;
            CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
            //QuotationID
            int QuotationID = int.Parse(objCCustomizedDBCall.ExecuteQuery_Array($"select MAX(ID)ID from Quotations")[0].ToString());
            //RoutingTypeID
            const int RoutingTypeID = 30;     //Main carriage route
            //POL
            int? POL = null;
            if (QuotationHeader["_xollsp_from_value"].ToString() != "")
            {
                crmID = QuotationHeader["_xollsp_from_value"].ToString();
                POL = GetMasterData("Port", crmID);
            }
            //POLCountryID
            int? POLCountryID = null;
            if (QuotationHeader["_xollsp_from_value"].ToString() != "")
            {
                crmID = QuotationHeader["_xollsp_from_value"].ToString();
                POLCountryID = GetMasterData("Country", crmID);
            }
            //POD
            int? POD = null;
            if (QuotationHeader["_xollsp_to_value"].ToString() != "")
            {
                crmID = QuotationHeader["_xollsp_to_value"].ToString();
                POD = GetMasterData("Port", crmID);
            }
            //PODCountryID
            int? PODCountryID = null;
            if (QuotationHeader["_xollsp_to_value"].ToString() != "")
            {
                crmID = QuotationHeader["_xollsp_to_value"].ToString();
                PODCountryID = GetMasterData("Country", crmID);
            }
            //MoveTypeID
            int? MoveTypeID = null;
            if (CostLine["_xollsp_logisticservice_value"].ToString() != "")
            {
                crmID = CostLine["_xollsp_logisticservice_value"].ToString();
                MoveTypeID = GetMasterData("LogisticService", crmID);
            }
            //ExpirationDate
            DateTime ExpirationDate = (DateTime)QuotationHeader["xollsp_effectiveto"];
            //ShippingLineID - AirLineID - TruckerID
            int? TruckerID = null;
            int? AirLineID = null;
            int? ShippingLineID = null;
            string LineID = CostLine["_xollsp_vendor_value"].ToString() == "" ? CostLine["_mesco_vendor2_value"].ToString() : CostLine["_xollsp_vendor_value"].ToString();
            if (LineID != "")
            {
                crmID = LineID;
                switch (QuotationHeader["xollsp_transporttype"].ToString())
                {
                    case "300000000":   //Ocean
                        ShippingLineID = GetMasterData("ShippingLine", crmID);
                        break;
                    case "300000001":   //Inland
                        AirLineID = GetMasterData("AirLine", crmID);
                        break;
                    case "300000002":   //air
                        TruckerID = GetMasterData("Trucker", crmID);
                        break;

                }  
            }
            //Transit Time
            int? TransientTime = null;
            if (CostLine["xollsp_transittime"].ToString() != "")
            {
                TransientTime = int.Parse(CostLine["xollsp_transittime"].ToString());
            }
            //FreeTime
            int? FreeTime = null;
            if (CostLine["mesco_freedays"].ToString() != "")
            {
                FreeTime = int.Parse(CostLine["mesco_freedays"].ToString());
            }
            //CreatorUserID
            int? CreatorUserID = null;
            if (QuotationHeader["_owninguser_value"].ToString() != "")
            {
                crmID = QuotationHeader["_owninguser_value"].ToString();
                CreatorUserID = GetMasterData("User", crmID);
            }
            //CreationDate
            DateTime CreationDate = (DateTime)QuotationHeader["createdon"];   //OpenDate
            //CommodityID
            int? CommodityID = null;
            if (QuotationHeader["_xollsp_commoditygroup_value"].ToString() != "")
            {
                crmID = QuotationHeader["_xollsp_commoditygroup_value"].ToString();
                CommodityID = GetMasterData("Commodity", crmID);
            }
            //IncotermID
            int? IncotermID = null;
            if (QuotationHeader["_xollsp_incoterm_value"].ToString() != "")
            {
                crmID = QuotationHeader["_xollsp_incoterm_value"].ToString();
                IncotermID = GetMasterData("Incoterm", crmID);
            }
            //POrC
            int? POrC = null;
            string FreightTerm = CostLine["new_freightterm"].ToString();
            switch (FreightTerm)
            {
                case "100000000":   //prepaid
                    POrC = 3;
                    break;
                case "100000001":   //collect
                    POrC = 1;
                    break;
            }

            //Insert into QuotationRoute Table
            string InsertQuery = "insert into QuotationRoute ("
                + "QuotationID,"
                + "RoutingTypeID,"
                + (POL == null ? "" : "POL,")
                + (POLCountryID == null ? "" : "POLCountryID,")
                + (POD == null ? "" : "POD,")
                + (PODCountryID == null ? "" : "PODCountryID,")
                + (MoveTypeID == null ? "" : "MoveTypeID,")
                + (ExpirationDate == null ? "" : "ExpirationDate,")
                + (TruckerID == null ? "" : "TruckerID,")
                + (AirLineID == null ? "" : "AirLineID,")
                + (ShippingLineID == null ? "" : "ShippingLineID,")
                + (TransientTime == null ? "" : "TransientTime,")
                + (FreeTime == null ? "" : "FreeTime,")
                + (CreatorUserID == null ? "" : "CreatorUserID,")
                + (POrC == null ? "" : "POrC,")
                + (CommodityID == null ? "" : "CommodityID,")
                + (IncotermID == null ? "" : "IncotermID,")
                + "QuotationStageID,"
                + (CreationDate == null ? "" : "CreationDate")
                + ") ";
            //Values
            InsertQuery += "Values ("
                + $"{QuotationID},"
                + $"{RoutingTypeID},"
                + (POL == null ? "" : $"{POL},")
                + (POLCountryID == null ? "" : $"{POLCountryID},")
                + (POD == null ? "" : $"{POD} ,")
                + (PODCountryID == null ? "" : $"{PODCountryID} ,")
                + (MoveTypeID == null ? "" : $"{MoveTypeID} ,")
                + (ExpirationDate == null ? "" : $"CONVERT(VARCHAR, '{ExpirationDate}', 103),")
                + (TruckerID == null ? "" : $"{TruckerID},")
                + (AirLineID == null ? "" : $"{AirLineID},")
                + (ShippingLineID == null ? "" : $"{ShippingLineID},")
                + (TransientTime == null ? "" : $"{TransientTime},")
                + (FreeTime == null ? "" : $"{FreeTime},")
                + (CreatorUserID == null ? "" : $"{CreatorUserID},")
                + (POrC == null ? "" : $"{POrC},")
                + (CommodityID == null ? "" : $"{CommodityID},")
                + (IncotermID == null ? "" : $"{IncotermID} ,")
                + $"4," //Quotation Stage (Accepted)
                + (CreationDate == null ? "" : $"CONVERT(VARCHAR, '{CreationDate}', 103)")
                + ")";

            //Executing insert query
            try {
                objCCustomizedDBCall.CallStringFunction(InsertQuery);
                int id = int.Parse(objCCustomizedDBCall.ExecuteQuery_Array($"select MAX(ID)ID from QuotationRoute")[0].ToString());
                var NotificationContent = new
                {
                    Subject = $"Quotation {QuotationHeader["xollsp_quotenumber"]} is added from Dynamics CRM",
                    Body = $"Quotation {QuotationHeader["xollsp_quotenumber"]} is added from Dynamics CRM"
                };
                SendNotification(NotificationContent.Subject, NotificationContent.Body, id);
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }

        }

        public void InsertQuotationCharges(JToken CostLine, JToken SalesLine, JToken QuotationHeader)
        {
            string crmID;
            CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
            //QuotationRouteID
            int QuotationRouteID = int.Parse(objCCustomizedDBCall.ExecuteQuery_Array($"select MAX(ID)ID from QuotationRoute")[0].ToString());
            //ChargeTypeID
            int? ChargeTypeID = null;
            if (CostLine["_xollsp_logisticservice_value"].ToString() != "")
            {
                crmID = CostLine["_xollsp_logisticservice_value"].ToString();
                ChargeTypeID = GetMasterData("ChargeType", crmID);
            }
            //MeasurementID
            const int MeasurementID = 3;
            //CostQuantity & SaleQuantity
            float? CostQuantity = null;
            float? SaleQuantity = null;
            if (CostLine["xollsp_quantity"].ToString() != "")
            {
                CostQuantity = float.Parse(CostLine["xollsp_quantity"].ToString());
            }
            if (SalesLine["xollsp_quantity"].ToString() != "")
            {
                SaleQuantity = float.Parse(SalesLine["xollsp_quantity"].ToString());
            }
            //CostPrice
            float? CostPrice = null;
            if (CostLine["xollsp_unitamount"].ToString() != "")
            {
                CostPrice = float.Parse(CostLine["xollsp_unitamount"].ToString());
            }
            //CostAmount
            float? CostAmount = null;
            if (CostLine["xollsp_totalamount"].ToString() != "")
            {
                CostAmount = float.Parse(CostLine["xollsp_totalamount"].ToString());
            }
            //CostCurrencyID
            int? CostCurrencyID = null;
            if (CostLine["_xollsp_currency_value"].ToString() != "")
            {
                crmID = CostLine["_xollsp_currency_value"].ToString();
                CostCurrencyID = GetMasterData("Currency", crmID);
            }
            //CostExchangeRate
            float? CostExchangeRate = null;
            if (CostLine["xollsp_exchangerate"].ToString() != "")
            {
                CostExchangeRate = 1/float.Parse(CostLine["xollsp_exchangerate"].ToString());
                var LastFromDate = DateTime.Parse(objCCustomizedDBCall.ExecuteQuery_Array($"select FromDate from CurrencyDetails where Currency_ID = {CostCurrencyID} order by id desc")[0]);
                var LastToDate = DateTime.Parse(objCCustomizedDBCall.ExecuteQuery_Array($"select ToDate from CurrencyDetails where Currency_ID = {CostCurrencyID} order by id desc")[0]);
                if(!(DateTime.Compare(DateTime.Now, LastFromDate) >= 0 && DateTime.Compare(DateTime.Now, LastToDate) <= 0))
                {
                    CurrenciesController CurrencyDetails = new CurrenciesController();
                    CurrencyDetails.CurrencyDetails_Save(0, (int)CostCurrencyID, LastToDate, DateTime.Now.AddDays(6), (decimal)CostExchangeRate, (decimal)CostExchangeRate);
                }  
            }
            //SalePrice
            float? SalePrice = null;
            if (SalesLine["xollsp_unitamount"].ToString() != "")
            {
                SalePrice = float.Parse(SalesLine["xollsp_unitamount"].ToString());
            }
            //SaleAmount
            float? SaleAmount = null;
            if (SalesLine["xollsp_totalamount"].ToString() != "")
            {
                SaleAmount = float.Parse(SalesLine["xollsp_totalamount"].ToString());
            }
            //SaleCurrencyID
            int? SaleCurrencyID = null;
            if (SalesLine["_xollsp_currency_value"].ToString() != "")
            {
                crmID = SalesLine["_xollsp_currency_value"].ToString();
                SaleCurrencyID = GetMasterData("Currency", crmID);
            }
            //SaleExchangeRate
            float? SaleExchangeRate = null;
            if (SalesLine["xollsp_exchangerate"].ToString() != "")
            {
                SaleExchangeRate = float.Parse(SalesLine["xollsp_exchangerate"].ToString());
            }
            //SupplierID
            int? SupplierID = null;
            if (CostLine["_xollsp_vendor_value"].ToString() != "")
            {
                crmID = CostLine["_xollsp_vendor_value"].ToString();
                SupplierID = GetMasterData("Customer", crmID);
            }
            //CreatorUserID
            int? CreatorUserID = null;
            if (QuotationHeader["_owninguser_value"].ToString() != "")
            {
                crmID = QuotationHeader["_owninguser_value"].ToString();
                CreatorUserID = GetMasterData("User", crmID);
            }
            //CreationDate
            DateTime CreationDate = (DateTime)QuotationHeader["createdon"];   //OpenDate

            //Insert into QuotationRoute Table
            string InsertQuery = "insert into QuotationCharges ("
                + "QuotationRouteID,"
                + (ChargeTypeID == null ? "" : "ChargeTypeID,")
                + "MeasurementID,"
                + (CostQuantity == null ? "" : "CostQuantity,")
                + (SaleQuantity == null ? "" : "SaleQuantity,")
                + (CostPrice == null ? "" : "CostPrice,")
                + (CostAmount == null ? "" : "CostAmount,")
                + (CostCurrencyID == null ? "" : "CostCurrencyID,")
                + "CostExchangeRate,"
                + (SalePrice == null ? "" : "SalePrice,")
                + (SaleAmount == null ? "" : "SaleAmount,")
                + (SaleCurrencyID == null ? "" : "SaleCurrencyID,")
                + "SaleExchangeRate,"
                + (SupplierID == null ? "" : "SupplierID,")
                + (CreatorUserID == null ? "" : "CreatorUserID,")
                + (CreationDate == null ? "" : "CreationDate")
                + ") ";
            //Values
            InsertQuery += "Values ("
                + $"{QuotationRouteID},"
                + (ChargeTypeID == null ? "" : $"{ChargeTypeID},")
                + $"{MeasurementID},"
                + (CostQuantity == null ? "" : $"{CostQuantity},")
                + (SaleQuantity == null ? "" : $"{SaleQuantity},")
                + (CostPrice == null ? "" : $"{CostPrice},")
                + (CostAmount == null ? "" : $"{CostAmount},")
                + (CostCurrencyID == null ? "" : $"{CostCurrencyID},")
                + $"{CostExchangeRate},"
                + (SalePrice == null ? "" : $"{SalePrice},")
                + (SaleAmount == null ? "" : $"{SaleAmount},")
                + (SaleCurrencyID == null ? "" : $"{SaleCurrencyID},")
                + $"{SaleExchangeRate},"
                + (SupplierID == null ? "" : $"{SupplierID},")
                + (CreatorUserID == null ? "" : $"{CreatorUserID},")
                + (CreationDate == null ? "" : $"CONVERT(VARCHAR, '{CreationDate}', 103)")
                + ")";

            //Executing insert query
            try
            {
                objCCustomizedDBCall.CallStringFunction(InsertQuery);
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }

        }

        public void InsertQuotationContainersAndPackages(JToken CostLine, JToken QuotationHeader)
        {
            string crmID;
            CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
            //QuotationID
            int QuotationID = int.Parse(objCCustomizedDBCall.ExecuteQuery_Array($"select MAX(ID)ID from Quotations")[0].ToString());
            //PackageTypeID
            int? PackageTypeID = null;
            if (CostLine["_xollsp_unitofmeasure_value"].ToString() != "")
            {
                crmID = CostLine["_xollsp_unitofmeasure_value"].ToString();
                PackageTypeID = GetMasterData("PackageType", crmID);
            }
            //Length
            float? Length = null;
            if (CostLine["xollsp_lenghtm"].ToString() != "")
            {
                Length = float.Parse(CostLine["xollsp_lenghtm"].ToString());
            }
            //Width
            float? Width = null;
            if (CostLine["xollsp_widthm"].ToString() != "")
            {
                Width = float.Parse(CostLine["xollsp_widthm"].ToString());
            }
            //Height
            float? Height = null;
            if (CostLine["xollsp_heightm"].ToString() != "")
            {
                Height = float.Parse(CostLine["xollsp_heightm"].ToString());
            }
            //Volume
            float? Volume = null;
            if (CostLine["xollsp_volumem3"].ToString() != "")
            {
                Volume = float.Parse(CostLine["xollsp_volumem3"].ToString());
            }
            //GrossWeight - ChargableWeight
            float? GrossWeight = null;
            float? ChargeableWeight = null;
            if (CostLine["xollsp_grossweightkg"].ToString() != "")
            {
                GrossWeight = float.Parse(CostLine["xollsp_grossweightkg"].ToString());
                ChargeableWeight = GrossWeight;      //They are the same in dynamics CRM
            }
            //Quantity
            float? Quantity = null;
            if (CostLine["xollsp_noofunits"].ToString() != "")
            {
                Quantity = float.Parse(CostLine["xollsp_noofunits"].ToString());
            }
            //QuotationRouteID
            int QuotationRouteID = int.Parse(objCCustomizedDBCall.ExecuteQuery_Array($"select MAX(ID)ID from QuotationRoute")[0].ToString());
            //CreatorUserID
            int? CreatorUserID = null;
            if (QuotationHeader["_owninguser_value"].ToString() != "")
            {
                crmID = QuotationHeader["_owninguser_value"].ToString();
                CreatorUserID = GetMasterData("User", crmID);
            }
            //CreationDate
            DateTime CreationDate = (DateTime)QuotationHeader["createdon"];   //OpenDate

            //Insert into QuotationRoute Table
            string InsertQuery = "insert into QuotationContainersAndPackages ("
                + "QuotationID,"
                + (PackageTypeID == null ? "" : "PackageTypeID,")
                + (Length == null ? "" : "Length,")
                + (Width == null ? "" : "Width,")
                + (Height == null ? "" : "Height,")
                + (Volume == null ? "" : "Volume,")
                + (GrossWeight == null ? "" : "GrossWeight,")
                + (ChargeableWeight == null ? "" : "ChargeableWeight,")
                + (Quantity == null ? "" : "Quantity,")
                + "QuotationRouteID,"
                + (CreatorUserID == null ? "" : "CreatorUserID,")
                + (CreationDate == null ? "" : "CreationDate")
                + ") ";
            //Values
            InsertQuery += "Values ("
                + $"{QuotationID},"
                + (PackageTypeID == null ? "" : $"{PackageTypeID},")
                + (Length == null ? "" : $"{Length},")
                + (Width == null ? "" : $"{Width} ,")
                + (Height == null ? "" : $"{Height} ,")
                + (Volume == null ? "" : $"{Volume} ,")
                + (GrossWeight == null ? "" : $"{GrossWeight},")
                + (ChargeableWeight == null ? "" : $"{ChargeableWeight},")
                + (Quantity == null ? "" : $"{Quantity},")
                + $"{QuotationRouteID},"
                + (CreatorUserID == null ? "" : $"{CreatorUserID},")
                + (CreationDate == null ? "" : $"CONVERT(VARCHAR, '{CreationDate}', 103)")
                + ")";

            //Executing insert query
            try
            {
                objCCustomizedDBCall.CallStringFunction(InsertQuery);
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }

        }

        private void InsertIntoCRMLog(int StatusCode, string crmQuotationID, int ForwardingQuotationID, string MissingFields, int UserID, int OperationID)
        {
            var record = new CVarcrmLog()
            {
                CreatedON = DateTime.Now,
                StatusCode = StatusCode,
                crmQuotationID = crmQuotationID,
                ForwardingQuotationID = ForwardingQuotationID,
                MissingFields = MissingFields,
                UserID = UserID,
                OperationID = OperationID
            };
            var recordList = new List<CVarcrmLog>();
            recordList.Add(record);
            var log = new CcrmLog();
            log.SaveMethod(recordList);
        }

        private void SendNotification(string Subject,String Body, int QuotationRouteID)
        {
            var userIDs = new UsersController().LoadAllIDs(" WHERE 1=1");
            var Alarm = new LocalEmailsController();
            Alarm.SendEmail(userIDs,Subject,Body,QuotationRouteID,0,0,0,true,"0",80,false," Where 1=1",25,1, "ID DESC");
        }
    }
}