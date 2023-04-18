using System.Web.Http;

namespace Forwarding.MvcApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            //config.Routes.MapHttpRoute(
            //            name: "RegionsPrint",
            //            routeTemplate: "api/Regions/PrintReport",
            //            defaults: new { controller = "Regions", action = "PrintReport" }
            //        );
            #region RS_RealEstate
            config.Routes.MapHttpRoute(
               name: "RS_deleteAllSelectedDetailesIDs",
               routeTemplate: "api/RS_Projects/deleteAllSelectedDetailesIDs",
               defaults: new { controller = "RS_Projects", action = "deleteAllSelectedDetailesIDs" }
               );

            config.Routes.MapHttpRoute(
              name: "RS_RealEstateLoadDetails",
              routeTemplate: "api/RS_Projects/LoadDetails",
              defaults: new { controller = "RS_Projects", action = "LoadDetails" }
              );

            config.Routes.MapHttpRoute(
            name: "RS_RealEstateLoadFloors",
            routeTemplate: "api/RS_Projects/LoadFloors",
            defaults: new { controller = "RS_Projects", action = "LoadFloors" }
            );

            config.Routes.MapHttpRoute(
           name: "RS_RealEstateInsertUnits",
           routeTemplate: "api/RS_Projects/InsertUnits",
           defaults: new { controller = "RS_Projects", action = "InsertUnits" }
           );

            config.Routes.MapHttpRoute(
           name: "RS_RealEstateInsertFloors",
           routeTemplate: "api/RS_Projects/InsertFloors",
           defaults: new { controller = "RS_Projects", action = "InsertFloors" }
           );

            config.Routes.MapHttpRoute(
           name: "RS_RealEstateDELETEfLOORS",
           routeTemplate: "api/RS_Projects/deleteAllSelectedFloorsIDs",
           defaults: new { controller = "RS_Projects", action = "deleteAllSelectedFloorsIDs" }
           );
            #endregion
            #region Dashboard
            config.Routes.MapHttpRoute(
                    name: "FlotLine",
                    routeTemplate: "api/Dashboard/LoadAll_FlotLine/{pWhereClauseFlotLine}",
                    defaults: new { controller = "Dashboard", action = "LoadAll_FlotLine", pWhereClauseFlotLine = RouteParameter.Optional }
                );
            #endregion

            #region DynamicsCRM
            config.Routes.MapHttpRoute(
            name: "DynamicsCRMAuth",
            routeTemplate: "api/DynamicsCRM/Auth",
            defaults: new { controller = "DynamicsCRM", action = "Auth" }
            );

            config.Routes.MapHttpRoute(
            name: "DynamicsCRMGetData",
            routeTemplate: "api/DynamicsCRM/GetData",
            defaults: new { controller = "DynamicsCRM", action = "GetData" }
            );

            config.Routes.MapHttpRoute(
            name: "DynamicsCRMGetCustomers",
            routeTemplate: "api/DynamicsCRM/GetCustomers",
            defaults: new { controller = "DynamicsCRM", action = "GetCustomers" }
            );

            config.Routes.MapHttpRoute(
            name: "DynamicsCRMGetBranches",
            routeTemplate: "api/DynamicsCRM/GetBranches",
            defaults: new { controller = "DynamicsCRM", action = "GetBranches" }
            );

            config.Routes.MapHttpRoute(
            name: "DynamicsCRMGetUsers",
            routeTemplate: "api/DynamicsCRM/GetUsers",
            defaults: new { controller = "DynamicsCRM", action = "GetUsers" }
            );

            config.Routes.MapHttpRoute(
            name: "DynamicsCRMGetCommodityGroups",
            routeTemplate: "api/DynamicsCRM/GetCommodityGroups",
            defaults: new { controller = "DynamicsCRM", action = "GetCommodityGroups" }
            );
            config.Routes.MapHttpRoute(
            name: "DynamicsCRMGetLogisticServices",
            routeTemplate: "api/DynamicsCRM/GetLogisticServices",
            defaults: new { controller = "DynamicsCRM", action = "GetLogisticServices" }
            );
            config.Routes.MapHttpRoute(
            name: "DynamicsCRMGetCountries",
            routeTemplate: "api/DynamicsCRM/GetCountries",
            defaults: new { controller = "DynamicsCRM", action = "GetCountries" }
            );
            config.Routes.MapHttpRoute(
            name: "DynamicsCRMGetPorts",
            routeTemplate: "api/DynamicsCRM/GetPorts",
            defaults: new { controller = "DynamicsCRM", action = "GetPorts" }
            );
            config.Routes.MapHttpRoute(
            name: "DynamicsCRMGetIncoterms",
            routeTemplate: "api/DynamicsCRM/GetIncoterms",
            defaults: new { controller = "DynamicsCRM", action = "GetIncoterms" }
            );
            config.Routes.MapHttpRoute(
            name: "DynamicsCRMGetQuotationByQuotationNumber",
            routeTemplate: "api/DynamicsCRM/GetQuotationByQuotationNumber",
            defaults: new { controller = "DynamicsCRM", action = "GetQuotationByQuotationNumber" }
            );
            config.Routes.MapHttpRoute(
            name: "DynamicsCRMLog",
            routeTemplate: "api/DynamicsCRMLog/LoadData",
            defaults: new { controller = "DynamicsCRMLog", action = "LoadData" }
            );
            #endregion

            #region Regions
            config.Routes.MapHttpRoute(
                name: "RegionsCheckRow",
                routeTemplate: "api/Regions/CheckRow",
                defaults: new { controller = "Regions", action = "CheckRow", pRegionID = RouteParameter.Optional }
                );

            config.Routes.MapHttpRoute(
                    name: "RegionsUnlockRecord",
                    routeTemplate: "api/Regions/UnlockRecord/{pRegionID}",
                    defaults: new { controller = "Regions", action = "UnlockRecord", pRegionID = RouteParameter.Optional }
                );

            config.Routes.MapHttpRoute(
                    name: "RegionsInsertFromXML",
                    routeTemplate: "api/Regions/InsertFromXML",
                    defaults: new { controller = "Regions", action = "InsertFromXML" }
                );

            #endregion
            #region Rates
            config.Routes.MapHttpRoute(
                 name: "RatesInsert_Update",
                 routeTemplate: "api/Rates/Insert_Update",
                 defaults: new { controller = "Rates", action = "Insert_Update" }
             );
            config.Routes.MapHttpRoute(
                 name: "RateRegionsInsert_Update",
                 routeTemplate: "api/Rates/RateRegionsInsert_Update",
                 defaults: new { controller = "Rates", action = "RateRegionsInsert_Update" }
             );
            config.Routes.MapHttpRoute(
                 name: "RateRateRegions_LoadingWithPaging",
                 routeTemplate: "api/Rates/RateRegions_LoadingWithPaging",
                 defaults: new { controller = "Rates", action = "RateRegions_LoadingWithPaging" }
             );
            config.Routes.MapHttpRoute(
                 name: "RateLoadWithPaging",
                 routeTemplate: "api/Rates/LoadWithPaging",
                 defaults: new { controller = "Rates", action = "LoadWithPaging" }
             );
            //Delete DeleteRateRegions
            config.Routes.MapHttpRoute(
                 name: "RateDelete",
                 routeTemplate: "api/Rates/Delete",
                 defaults: new { controller = "Rates", action = "Delete" }
             );
            config.Routes.MapHttpRoute(
                 name: "RateDeleteRateRegions",
                 routeTemplate: "api/Rates/DeleteRateRegions",
                 defaults: new { controller = "Rates", action = "DeleteRateRegions" }
             );
            config.Routes.MapHttpRoute(
                 name: "RateRegions_LoadingWithPagingByRateID",
                 routeTemplate: "api/Rates/RateRegions_LoadingWithPagingByRateID",
                 defaults: new { controller = "Rates", action = "RateRegions_LoadingWithPagingByRateID" }
             );

            config.Routes.MapHttpRoute(
                 name: "RatesLoadRatesToCustomer",
                 routeTemplate: "api/Rates/LoadRatesToCustomer",
                 defaults: new { controller = "Rates", action = "LoadRatesToCustomer" }
             );
            config.Routes.MapHttpRoute(
            name: "RatesDeleteLM_Customer_Rates",
            routeTemplate: "api/Rates/DeleteLM_Customer_Rates",
            defaults: new { controller = "Rates", action = "DeleteLM_Customer_Rates" }
            );
            config.Routes.MapHttpRoute(
                 name: "RatesLoadCustomerRates",
                 routeTemplate: "api/Rates/LoadCustomerRates",
                 defaults: new { controller = "Rates", action = "LoadCustomerRates" }
             );
            #endregion

            #region Reports
            config.Routes.MapHttpRoute(
                 name: "Report_DocsOut",
                 routeTemplate: "api/Reports/Report_DocsOut",
                 defaults: new { controller = "Reports", action = "Report_DocsOut" }
                  );
            config.Routes.MapHttpRoute(
                 name: "Report_DocsOut_Multiple",
                 routeTemplate: "api/Reports/Report_DocsOut_Multiple",
                 defaults: new { controller = "Reports", action = "Report_DocsOut_Multiple" }
                  );

            config.Routes.MapHttpRoute(
                 name: "Report_Invoice",
                 routeTemplate: "api/Reports/Report_Invoice",
                 defaults: new { controller = "Reports", action = "Report_Invoice" }
                  );
            config.Routes.MapHttpRoute(
                 name: "Report_Invoice_Multiple",
                 routeTemplate: "api/Reports/Report_Invoice_Multiple",
                 defaults: new { controller = "Reports", action = "Report_Invoice_Multiple" }
                  );

            #endregion

            #region Ports
            config.Routes.MapHttpRoute(
            name: "PortsCheckRow",
            routeTemplate: "api/Ports/CheckRow",
            defaults: new { controller = "Ports", action = "CheckRow", pPortID = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                    name: "PortsUnlockRecord",
                    routeTemplate: "api/Ports/UnlockRecord/{pPortID}",
                    defaults: new { controller = "Ports", action = "UnlockRecord", pPortID = RouteParameter.Optional }
                );
            config.Routes.MapHttpRoute(
            name: "PortsLoadWithPaging",
            routeTemplate: "api/Ports/LoadWithPaging",
            defaults: new { controller = "Ports", action = "LoadWithPaging" }
            );
            config.Routes.MapHttpRoute(
            name: "PortsLoadWithPaging_Factories",
            routeTemplate: "api/Ports/LoadWithPaging_Factories",
            defaults: new { controller = "Ports", action = "LoadWithPaging_Factories" }
            );
            #endregion
            #region CreditCardTypes
            config.Routes.MapHttpRoute(
            name: "CreditCardTypesCheckRow",
            routeTemplate: "api/CreditCardTypes/CheckRow",
            defaults: new { controller = "CreditCardTypes", action = "CheckRow", pCreditCardTypeID = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                    name: "CreditCardTypesUnlockRecord",
                    routeTemplate: "api/CreditCardTypes/UnlockRecord/{pCreditCardTypeID}",
                    defaults: new { controller = "CreditCardTypes", action = "UnlockRecord", pCreditCardTypeID = RouteParameter.Optional }
                );
            #endregion
            #region PaymentTerms
            config.Routes.MapHttpRoute(
            name: "PaymentTermsCheckRow",
            routeTemplate: "api/PaymentTerms/CheckRow",
            defaults: new { controller = "PaymentTerms", action = "CheckRow", pPaymentTermID = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                    name: "PaymentTermsUnlockRecord",
                    routeTemplate: "api/PaymentTerms/UnlockRecord/{pPaymentTermID}",
                    defaults: new { controller = "PaymentTerms", action = "UnlockRecord", pPaymentTermID = RouteParameter.Optional }
                );
            #endregion
            #region Incoterms
            config.Routes.MapHttpRoute(
            name: "IncotermsCheckRow",
            routeTemplate: "api/Incoterms/CheckRow",
            defaults: new { controller = "Incoterms", action = "CheckRow", pIncotermID = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                    name: "IncotermsUnlockRecord",
                    routeTemplate: "api/Incoterms/UnlockRecord/{pIncotermID}",
                    defaults: new { controller = "Incoterms", action = "UnlockRecord", pIncotermID = RouteParameter.Optional }
                );
            #endregion
            #region Currencies
            config.Routes.MapHttpRoute(
            name: "CurrenciesCheckRow",
            routeTemplate: "api/Currencies/CheckRow",
            defaults: new { controller = "Currencies", action = "CheckRow", pCurrencyID = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                    name: "CurrenciesUnlockRecord",
                    routeTemplate: "api/Currencies/UnlockRecord/{pCurrencyID}",
                    defaults: new { controller = "Currencies", action = "UnlockRecord", pCurrencyID = RouteParameter.Optional }
                );
            #endregion
            #region PurchaseItem
            config.Routes.MapHttpRoute(
                name: "InsertListPurchaseItem",
                routeTemplate: "api/PurchaseItem/InsertList",
                defaults: new { controller = "PurchaseItem", action = "InsertList" }
                );
            config.Routes.MapHttpRoute(
                name: "UploadFile",
                routeTemplate: "api/PurchaseItem/UploadFile",
                defaults: new { controller = "PurchaseItem", action = "UploadFile" }
                );
            #endregion PurchaseItem
            #region TaxeTypes
            config.Routes.MapHttpRoute(
            name: "TaxeTypesCheckRow",
            routeTemplate: "api/TaxeTypes/CheckRow",
            defaults: new { controller = "TaxeTypes", action = "CheckRow", pTaxeTypeID = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                    name: "TaxeTypesUnlockRecord",
                    routeTemplate: "api/TaxeTypes/UnlockRecord/{pTaxeTypeID}",
                    defaults: new { controller = "TaxeTypes", action = "UnlockRecord", pTaxeTypeID = RouteParameter.Optional }
                );

            //config.Routes.MapHttpRoute(
            //        name: "LoadLogTaxesPercentages",
            //        routeTemplate: "api/LogTaxesPercentages/LoadWithPaging/{pPageNumber}/{pPageSize}/{pSearchID}",
            //        defaults: new { controller = "LogTaxesPercentages", action = "LoadWithPaging", pPageSize = RouteParameter.Optional, pSearchID = RouteParameter.Optional }
            //    );

            #endregion
            #region ChargeTypes
            config.Routes.MapHttpRoute(
            name: "ChargeTypesCheckRow",
            routeTemplate: "api/ChargeTypes/CheckRow",
            defaults: new { controller = "ChargeTypes", action = "CheckRow", pChargeTypeID = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                    name: "ChargeTypesUnlockRecord",
                    routeTemplate: "api/ChargeTypes/UnlockRecord/{pChargeTypeID}",
                    defaults: new { controller = "ChargeTypes", action = "UnlockRecord", pChargeTypeID = RouteParameter.Optional }
                );
            #endregion
            #region WarehousingChargeTypes
            config.Routes.MapHttpRoute(
            name: "WarehousingChargeTypesCheckRow",
            routeTemplate: "api/WarehousingChargeTypes/CheckRow",
            defaults: new { controller = "WarehousingChargeTypes", action = "CheckRow", pWarehousingChargeTypesID = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                    name: "WarehousingChargeTypesUnlockRecord",
                    routeTemplate: "api/WarehousingChargeTypes/UnlockRecord/{pWarehousingChargeTypesID}",
                    defaults: new { controller = "WarehousingChargeTypes", action = "UnlockRecord", pChargeTypeID = RouteParameter.Optional }
                );
            #endregion
            #region ContainerTypes
            config.Routes.MapHttpRoute(
            name: "ContainerTypesCheckRow",
            routeTemplate: "api/ContainerTypes/CheckRow",
            defaults: new { controller = "ContainerTypes", action = "CheckRow", pContainerTypeID = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                    name: "ContainerTypesUnlockRecord",
                    routeTemplate: "api/ContainerTypes/UnlockRecord/{pContainerTypeID}",
                    defaults: new { controller = "ContainerTypes", action = "UnlockRecord", pContainerTypeID = RouteParameter.Optional }
                );
            #endregion
            #region PackageTypes
            config.Routes.MapHttpRoute(
            name: "PackageTypesCheckRow",
            routeTemplate: "api/PackageTypes/CheckRow",
            defaults: new { controller = "PackageTypes", action = "CheckRow", pPackageTypeID = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                    name: "PackageTypesUnlockRecord",
                    routeTemplate: "api/PackageTypes/UnlockRecord/{pPackageTypeID}",
                    defaults: new { controller = "PackageTypes", action = "UnlockRecord", pPackageTypeID = RouteParameter.Optional }
                );
            #endregion
            #region Commodities
            config.Routes.MapHttpRoute(
            name: "CommoditiesCheckRow",
            routeTemplate: "api/Commodities/CheckRow",
            defaults: new { controller = "Commodities", action = "CheckRow", pCommodityID = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                    name: "CommoditiesUnlockRecord",
                    routeTemplate: "api/Commodities/UnlockRecord/{pCommodityID}",
                    defaults: new { controller = "Commodities", action = "UnlockRecord", pCommodityID = RouteParameter.Optional }
                );
            #endregion
            #region MoveTypes
            config.Routes.MapHttpRoute(
            name: "MoveTypesCheckRow",
            routeTemplate: "api/MoveTypes/CheckRow",
            defaults: new { controller = "MoveTypes", action = "CheckRow", pMoveTypeID = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                    name: "MoveTypesUnlockRecord",
                    routeTemplate: "api/MoveTypes/UnlockRecord/{pMoveTypeID}",
                    defaults: new { controller = "MoveTypes", action = "UnlockRecord", pMoveTypeID = RouteParameter.Optional }
                );
            #endregion
            #region Vessels
            config.Routes.MapHttpRoute(
            name: "VesselsCheckRow",
            routeTemplate: "api/Vessels/CheckRow",
            defaults: new { controller = "Vessels", action = "CheckRow", pVesselID = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                    name: "VesselsUnlockRecord",
                    routeTemplate: "api/Vessels/UnlockRecord/{pVesselID}",
                    defaults: new { controller = "Vessels", action = "UnlockRecord", pVesselID = RouteParameter.Optional }
                );
            #endregion
            #region TypeOfStock
            config.Routes.MapHttpRoute(
                name: "TypeOfStock_Insert",
                routeTemplate: "api/TypeOfStock/TypeOfStock_Insert",
                defaults: new { controller = "TypeOfStock", action = "TypeOfStock_Insert" }
                );

            config.Routes.MapHttpRoute(
                name: "TypeOfStock_Update",
                routeTemplate: "api/TypeOfStock/TypeOfStock_Update",
                defaults: new { controller = "TypeOfStock", action = "TypeOfStock_Update" }
                );
            #endregion TypeOfStock
            #region Template
            config.Routes.MapHttpRoute(
                name: "InsertTemplate",
                routeTemplate: "api/Template/Insert",
                defaults: new { controller = "Template", action = "Insert" }
                );

            config.Routes.MapHttpRoute(
                name: "UpdateTemplate",
                routeTemplate: "api/Template/Update",
                defaults: new { controller = "Template", action = "Update" }
                );
            #endregion Template
            #region BankTemplate
            config.Routes.MapHttpRoute(
                name: "InsertBankTemplate",
                routeTemplate: "api/BankTemplate/Insert",
                defaults: new { controller = "BankTemplate", action = "Insert" }
                );

            config.Routes.MapHttpRoute(
                name: "UpdateBankTemplate",
                routeTemplate: "api/BankTemplate/Update",
                defaults: new { controller = "BankTemplate", action = "Update" }
                );
            #endregion BankTemplate
            #region Customers // PartnerTypeID = 1
            config.Routes.MapHttpRoute(
            name: "CustomersCheckRow",
            routeTemplate: "api/Customers/CheckRow",
            defaults: new { controller = "Customers", action = "CheckRow", pCustomerID = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
            name: "CustomersLoadAll",
            routeTemplate: "api/Customers/LoadAll",
            defaults: new { controller = "Customers", action = "LoadAll" }
            );

            config.Routes.MapHttpRoute(
            name: "CustomersLoadAllForTopManagement",
            routeTemplate: "api/Customers/LoadAllForTopManagement",
            defaults: new { controller = "Customers", action = "LoadAllForTopManagement" }
            );

            config.Routes.MapHttpRoute(
            name: "CustomersLoadAll_Companies",
            routeTemplate: "api/Customers/LoadAll_Companies",
            defaults: new { controller = "Customers", action = "LoadAll_Companies" }
            );
            


            config.Routes.MapHttpRoute(
                    name: "CustomersUnlockRecord",
                    routeTemplate: "api/Customers/UnlockRecord/{pCustomerID}",
                    defaults: new { controller = "Customers", action = "UnlockRecord", pCustomerID = RouteParameter.Optional }
                );

            config.Routes.MapHttpRoute(
         name: "CustomerSetAutomailPeriod",
         routeTemplate: "api/Customers/SetAutomailPeriod",
          defaults: new { controller = "Customers", action = "SetAutomailPeriod" }
         );
            config.Routes.MapHttpRoute(
name: "CustomerLoadAutomailCustomers",
routeTemplate: "api/Customers/LoadAutomailCustomers",
defaults: new { controller = "Customers", action = "LoadAutomailCustomers" }
);

            config.Routes.MapHttpRoute(
name: "CreateSalesLeadFromCustomers",
routeTemplate: "api/Customers/CreateSalesLeadFromCustomers",
defaults: new { controller = "Customers", action = "CreateSalesLeadFromCustomers" }
);

            config.Routes.MapHttpRoute(
            name: "DeleteCustomer",
            routeTemplate: "api/Customers/Delete",
            defaults: new { controller = "Customers", action = "Delete" }
            );

            config.Routes.MapHttpRoute(
            name: "CustomerSL_SalesManInsert",
            routeTemplate: "api/CustomerSL_SalesMan/Insert",
            defaults: new { controller = "CustomerSL_SalesMan", action = "Insert" }
            );

            config.Routes.MapHttpRoute(
            name: "CustomerLoadSalesManByCustomerID",
            routeTemplate: "api/CustomerSL_SalesMan/LoadSalesManByCustomerID",
            defaults: new { controller = "CustomerSL_SalesMan", action = "LoadSalesManByCustomerID" }
            );

            config.Routes.MapHttpRoute(
            name: "SL_ReportsLoadInvoiceDetails",
            routeTemplate: "api/SL_Reports/LoadInvoiceDetails",
            defaults: new { controller = "SL_Reports", action = "LoadInvoiceDetails" }
            );
            config.Routes.MapHttpRoute(
name: "SL_ReportsLoadInvoiceDetailsForClientPrint",
routeTemplate: "api/SL_Reports/LoadInvoiceDetailsForClientPrint",
defaults: new { controller = "SL_Reports", action = "LoadInvoiceDetailsForClientPrint" }
);

            #endregion

            #region Agents // PartnerTypeID = 2
            config.Routes.MapHttpRoute(
            name: "AgentsCheckRow",
            routeTemplate: "api/Agents/CheckRow",
            defaults: new { controller = "Agents", action = "CheckRow", pAgentID = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                    name: "AgentsUnlockRecord",
                    routeTemplate: "api/Agents/UnlockRecord/{pAgentID}",
                    defaults: new { controller = "Agents", action = "UnlockRecord", pAgentID = RouteParameter.Optional }
                );
            #endregion
            #region ShippingAgents // PartnerTypeID = 3
            config.Routes.MapHttpRoute(
            name: "ShippingAgentsCheckRow",
            routeTemplate: "api/ShippingAgents/CheckRow",
            defaults: new { controller = "ShippingAgents", action = "CheckRow", pShippingAgentID = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                    name: "ShippingAgentsUnlockRecord",
                    routeTemplate: "api/ShippingAgents/UnlockRecord/{pShippingAgentID}",
                    defaults: new { controller = "ShippingAgents", action = "UnlockRecord", pShippingAgentID = RouteParameter.Optional }
                );
            #endregion
            #region CustomsClearanceAgents // PartnerTypeID = 4
            config.Routes.MapHttpRoute(
            name: "CustomsClearanceAgentsCheckRow",
            routeTemplate: "api/CustomsClearanceAgents/CheckRow",
            defaults: new { controller = "CustomsClearanceAgents", action = "CheckRow", pCustomsClearanceAgentID = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                    name: "CustomsClearanceAgentsUnlockRecord",
                    routeTemplate: "api/CustomsClearanceAgents/UnlockRecord/{pCustomsClearanceAgentID}",
                    defaults: new { controller = "CustomsClearanceAgents", action = "UnlockRecord", pCustomsClearanceAgentID = RouteParameter.Optional }
                );
            #endregion
            #region ShippingLines // PartnerTypeID = 8
            config.Routes.MapHttpRoute(
            name: "ShippingLinesCheckRow",
            routeTemplate: "api/ShippingLines/CheckRow",
            defaults: new { controller = "ShippingLines", action = "CheckRow", pShippingLineID = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                    name: "ShippingLinesUnlockRecord",
                    routeTemplate: "api/ShippingLines/UnlockRecord/{pShippingLineID}",
                    defaults: new { controller = "ShippingLines", action = "UnlockRecord", pShippingLineID = RouteParameter.Optional }
                );
            #endregion
            #region Airlines // PartnerTypeID = 6
            config.Routes.MapHttpRoute(
            name: "AirlinesCheckRow",
            routeTemplate: "api/Airlines/CheckRow",
            defaults: new { controller = "Airlines", action = "CheckRow", pAirlineID = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                    name: "AirlinesUnlockRecord",
                    routeTemplate: "api/Airlines/UnlockRecord/{pAirlineID}",
                    defaults: new { controller = "Airlines", action = "UnlockRecord", pAirlineID = RouteParameter.Optional }
                );
            #endregion
            #region Truckers // PartnerTypeID = 7
            config.Routes.MapHttpRoute(
            name: "TruckersCheckRow",
            routeTemplate: "api/Truckers/CheckRow",
            defaults: new { controller = "Truckers", action = "CheckRow", pTruckerID = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                    name: "TruckersUnlockRecord",
                    routeTemplate: "api/Truckers/UnlockRecord/{pTruckerID}",
                    defaults: new { controller = "Truckers", action = "UnlockRecord", pTruckerID = RouteParameter.Optional }
                );
            #endregion
            #region Suppliers // PartnerTypeID = 8
            config.Routes.MapHttpRoute(
            name: "SuppliersCheckRow",
            routeTemplate: "api/Suppliers/CheckRow",
            defaults: new { controller = "Suppliers", action = "CheckRow", pSupplierID = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                    name: "SuppliersUnlockRecord",
                    routeTemplate: "api/Suppliers/UnlockRecord/{pSupplierID}",
                    defaults: new { controller = "Suppliers", action = "UnlockRecord", pSupplierID = RouteParameter.Optional }
                );

            config.Routes.MapHttpRoute(
                name: "LoadSupplierSites",
                routeTemplate: "api/Suppliers/LoadSupplierSites",
                defaults: new { controller = "Suppliers", action = "LoadSupplierSites" }
            );

            config.Routes.MapHttpRoute(
                name: "LoadAllSuppliers",
                routeTemplate: "api/Suppliers/LoadAll",
                defaults: new { controller = "Suppliers", action = "LoadAll" }
            );


            #endregion
            #region CustomsClearance
            config.Routes.MapHttpRoute(
                  name: "CustomsItems_Insert",
                  routeTemplate: "api/CustomsItems/CustomsItems_Insert",
                  defaults: new { controller = "CustomsItems", action = "CustomsItems_Insert" }
              );
            config.Routes.MapHttpRoute(
                 name: "CustomsItems_CustomsItems_Insert_Update",
                 routeTemplate: "api/CustomsItems/CustomsItems_Insert_Update",
                 defaults: new { controller = "CustomsItems", action = "CustomsItems_Insert_Update" }
             );

            config.Routes.MapHttpRoute(
           name: "MasterData_CustomsItems_Insert_Update",
           routeTemplate: "api/MasterData/CustomsItems_Insert_Update",
           defaults: new { controller = "MasterData", action = "CustomsItems_Insert_Update" }
            );
            #endregion

            config.Routes.MapHttpRoute(
                name: "TRCK_Equipments_LoadAll",
                routeTemplate: "api/TRCK_Equipments/LoadAll",
                defaults: new { controller = "TRCK_Equipments", action = "LoadAll" }
                 );
            config.Routes.MapHttpRoute(
                 name: "TRCK_Equipments_Delete",
                 routeTemplate: "api/TRCK_Equipments/Delete",
                 defaults: new { controller = "TRCK_Equipments", action = "Delete" }
                  );
            config.Routes.MapHttpRoute(
                 name: "TRCK_Equipments_CheckRow",
                 routeTemplate: "api/TRCK_Equipments/CheckRow",
                 defaults: new { controller = "TRCK_Equipments", action = "CheckRow" }
                  );
            config.Routes.MapHttpRoute(
                 name: "TRCK_Equipments_UnlockRecord",
                 routeTemplate: "api/TRCK_Equipments/UnlockRecord",
                 defaults: new { controller = "TRCK_Equipments", action = "UnlockRecord" }
                  );
            config.Routes.MapHttpRoute(
                name: "TRCK_Equipments_LoadTRCK_Equipment_Items",
                routeTemplate: "api/TRCK_Equipments/LoadTRCK_Equipment_Items",
                defaults: new { controller = "TRCK_Equipments", action = "LoadTRCK_Equipment_Items" }
                 );
            config.Routes.MapHttpRoute(
                name: "TRCK_Equipments_LoadTRCK_Equipment_ItemsWheels",
                routeTemplate: "api/TRCK_Equipments/LoadTRCK_Equipment_ItemsWheels",
                defaults: new { controller = "TRCK_Equipments", action = "LoadTRCK_Equipment_ItemsWheels" }
                 );

            config.Routes.MapHttpRoute(
                name: "TRCK_EquipmentsInsert",
                routeTemplate: "api/TRCK_Equipments/Insert",
                defaults: new { controller = "TRCK_Equipments", action = "Insert" }
                );
            config.Routes.MapHttpRoute(
                name: "TRCK_EquipmentsUpdate",
                routeTemplate: "api/TRCK_Equipments/Update",
                defaults: new { controller = "TRCK_Equipments", action = "Update" }
                );

            #region Users
            config.Routes.MapHttpRoute(
                name: "InsertUser",
                routeTemplate: "api/Users/Insert",
                defaults: new { controller = "Users", action = "Insert" }
                );
            config.Routes.MapHttpRoute(
                name: "LoadAllIDs",
                routeTemplate: "api/Users/LoadAllIDs",
                defaults: new { controller = "Users", action = "LoadAllIDs" }
                );
            config.Routes.MapHttpRoute(
                name: "UpdateUser",
                routeTemplate: "api/Users/Update",
                defaults: new { controller = "Users", action = "Update" }
                );

            config.Routes.MapHttpRoute(
    name: "LoadAll_users",
    routeTemplate: "api/Users/LoadAll",
    defaults: new { controller = "Users", action = "LoadAll" }
    );
            config.Routes.MapHttpRoute(
    name: "LoadAllUserSalesmen_users",
    routeTemplate: "api/Users/LoadAllUserSalesmen",
    defaults: new { controller = "Users", action = "LoadAllUserSalesmen" }
    );
            config.Routes.MapHttpRoute(
                name: "ChangePassword",
                routeTemplate: "api/Users/ChangePassword",
                defaults: new { controller = "Users", action = "ChangePassword" }
                );

            #endregion
            #region UserPrivileges //UserForms
            config.Routes.MapHttpRoute(
                name: "GetPermissions",
                routeTemplate: "api/UserPrivileges/LoadAll",
                defaults: new { controller = "UserPrivileges", action = "LoadAll" }
                );

            config.Routes.MapHttpRoute(
    name: "GetBranchUsers",
    routeTemplate: "api/Branches/GetBranchUsers",
    defaults: new { controller = "Branches", action = "GetBranchUsers" }
    );
            config.Routes.MapHttpRoute(
name: "UpdateBranchUsers",
routeTemplate: "api/Branches/UpdateBranchUsers",
defaults: new { controller = "Branches", action = "UpdateBranchUsers" }
);


            config.Routes.MapHttpRoute(
name: "GetUserBranches",
routeTemplate: "api/Users/GetUserBranches",
defaults: new { controller = "Users", action = "GetUserBranches" }
);
            config.Routes.MapHttpRoute(
name: "UpdateUserBranches",
routeTemplate: "api/Users/UpdateUserBranches",
defaults: new { controller = "Users", action = "UpdateUserBranches" }
);
            #endregion
            #region Quotations
            config.Routes.MapHttpRoute(
                name: "InsertQuotation",
                routeTemplate: "api/Quotations/Insert",
                defaults: new { controller = "Quotations", action = "Insert" }
                );

            config.Routes.MapHttpRoute(
                name: "UpdateQuotation",
                routeTemplate: "api/Quotations/Update",
                defaults: new { controller = "Quotations", action = "Update" }
                );
            config.Routes.MapHttpRoute(
                name: "FleetQuotation_Update",
                routeTemplate: "api/Quotations/FleetQuotation_Update",
                defaults: new { controller = "Quotations", action = "FleetQuotation_Update" }
                );
            config.Routes.MapHttpRoute(
                name: "FleetQuotation_CreateCutoffInvoice",
                routeTemplate: "api/Quotations/FleetQuotation_CreateCutoffInvoice",
                defaults: new { controller = "Quotations", action = "FleetQuotation_CreateCutoffInvoice" }
                );
            config.Routes.MapHttpRoute(
                name: "FleetApprovedTransportOrder_RemoveInvoicedOrders",
                routeTemplate: "api/Quotations/FleetApprovedTransportOrder_RemoveInvoicedOrders",
                defaults: new { controller = "Quotations", action = "FleetApprovedTransportOrder_RemoveInvoicedOrders" }
                );
            config.Routes.MapHttpRoute(
                name: "FleetApprovedTransportOrder_AddInvoicedOrders",
                routeTemplate: "api/Quotations/FleetApprovedTransportOrder_AddInvoicedOrders",
                defaults: new { controller = "Quotations", action = "FleetApprovedTransportOrder_AddInvoicedOrders" }
                );
            config.Routes.MapHttpRoute(
                name: "CreateOperationFromQuotation",
                routeTemplate: "api/Quotations/CreateOperationFromQuotation",
                defaults: new { controller = "Quotations", action = "CreateOperationFromQuotation" }
                );
            config.Routes.MapHttpRoute(
                name: "GetModalControls",
                routeTemplate: "api/Quotations/GetModalControls",
                defaults: new { controller = "Quotations", action = "GetModalControls" }
                );

            config.Routes.MapHttpRoute(
                name: "QuotationCharges_UpdateList",
                routeTemplate: "api/QuotationCharges/UpdateList",
                defaults: new { controller = "QuotationCharges", action = "UpdateList" }
                );
            #endregion

            #region Operations
            config.Routes.MapHttpRoute(
                name: "InsertOperation",
                routeTemplate: "api/Operations/Insert",
                defaults: new { controller = "Operations", action = "Insert" }
                );

            config.Routes.MapHttpRoute(
                name: "UpdateOperation",
                routeTemplate: "api/Operations/Update",
                defaults: new { controller = "Operations", action = "Update" }
                );
            config.Routes.MapHttpRoute(
                name: "InsertShipmentAWB",
                routeTemplate: "api/Operations/InsertShipmentAWB",
                defaults: new { controller = "Operations", action = "InsertShipmentAWB" }
                );
            config.Routes.MapHttpRoute(
                name: "UpdateShipmentAWB",
                routeTemplate: "api/Operations/UpdateShipmentAWB",
                defaults: new { controller = "Operations", action = "UpdateShipmentAWB" }
                );
            config.Routes.MapHttpRoute(
                name: "Shipment_SetCertificateNumber",
                routeTemplate: "api/Operations/Shipment_SetCertificateNumber",
                defaults: new { controller = "Operations", action = "Shipment_SetCertificateNumber" }
                );
            config.Routes.MapHttpRoute(
                name: "ShipmentDates_SaveList",
                routeTemplate: "api/Operations/ShipmentDates_SaveList",
                defaults: new { controller = "Operations", action = "ShipmentDates_SaveList" }
                );
            config.Routes.MapHttpRoute(
                name: "Receivables_UpdateList",
                routeTemplate: "api/Receivables/UpdateList",
                defaults: new { controller = "Receivables", action = "UpdateList" }
                );
            config.Routes.MapHttpRoute(
                name: "ConnectOrDisconnect",
                routeTemplate: "api/Operations/ConnectOrDisconnect",
                defaults: new { controller = "Operations", action = "ConnectOrDisconnect" }
                );
            config.Routes.MapHttpRoute(
                name: "ConnectOrDisconnectMultiple",
                routeTemplate: "api/Operations/ConnectOrDisconnectMultiple",
                defaults: new { controller = "Operations", action = "ConnectOrDisconnectMultiple" }
                );
            config.Routes.MapHttpRoute(
                name: "FindNumberOfUnApproved_Invoices_AccNotes_Payables",
                routeTemplate: "api/Operations/FindNumberOfUnApproved_Invoices_AccNotes_Payables",
                defaults: new { controller = "Operations", action = "FindNumberOfUnApproved_Invoices_AccNotes_Payables" }
                );
            config.Routes.MapHttpRoute(
                name: "FindCertificateNumberAndCertificateDate",
                routeTemplate: "api/Operations/FindCertificateNumberAndCertificateDate",
                defaults: new { controller = "Operations", action = "FindCertificateNumberAndCertificateDate" }
                );
            #endregion
            #region OperationContainersAndPackages
            config.Routes.MapHttpRoute(
                name: "InsertListFromExcel",
                routeTemplate: "api/OperationContainersAndPackages/InsertListFromExcel",
                defaults: new { controller = "OperationContainersAndPackages", action = "InsertListFromExcel" }
                );
            config.Routes.MapHttpRoute(
          name: "InsertListFromExcelTanks",
          routeTemplate: "api/TankPayablesAndReceivables/InsertListFromExcelTanks",
          defaults: new { controller = "TankPayablesAndReceivables", action = "InsertListFromExcelTanks" }
          );

            config.Routes.MapHttpRoute(
    name: "TankPayablesAndReceivablesUploadExcelFileData",
    routeTemplate: "api/TankPayablesAndReceivables/UploadExcelFileData",
    defaults: new { controller = "TankPayablesAndReceivables", action = "UploadExcelFileData" }
    );

            #endregion OperationContainersAndPackages
            #region Payables
            config.Routes.MapHttpRoute(
                name: "LoadWithWhereClause",
                routeTemplate: "api/Payables/LoadWithWhereClause",
                defaults: new { controller = "Payables", action = "LoadWithWhereClause" }
                );
            config.Routes.MapHttpRoute(
               name: "PayablesApproveOrUnApproveTAX",
               routeTemplate: "api/Payables/ApproveOrUnApproveTAX",
               defaults: new { controller = "Payables", action = "ApproveOrUnApproveTAX" }
               );
            config.Routes.MapHttpRoute(
               name: "PayablesApproveOrUnApprove",
               routeTemplate: "api/Payables/ApproveOrUnApprove",
               defaults: new { controller = "Payables", action = "ApproveOrUnApprove" }
               );
            config.Routes.MapHttpRoute(
                name: "LoadAirLineWithWhereClause",
                routeTemplate: "api/Payables/LoadAirLineWithWhereClause",
                defaults: new { controller = "Payables", action = "LoadAirLineWithWhereClause" }
                );
            config.Routes.MapHttpRoute(
                name: "UpdateListPayables",
                routeTemplate: "api/Payables/UpdateList",
                defaults: new { controller = "Payables", action = "UpdateList" }
                );
            #region AccNote
            config.Routes.MapHttpRoute(
              name: "AccNoteApproveOrUnApprove",
              routeTemplate: "api/AccNote/ApproveOrUnApprove",
              defaults: new { controller = "AccNote", action = "ApproveOrUnApprove" }
              );
            config.Routes.MapHttpRoute(
              name: "AccNoteApproveOrUnApproveTax",
              routeTemplate: "api/AccNote/ApproveOrUnApproveTax",
              defaults: new { controller = "AccNote", action = "ApproveOrUnApproveTax" }
              );
            #endregion
            #endregion Payables
            #region Routings
            config.Routes.MapHttpRoute(
                name: "InsertRouting",
                routeTemplate: "api/Routings/Insert",
                defaults: new { controller = "Routings", action = "Insert" }
                );

            config.Routes.MapHttpRoute(
                name: "UpdateRouting",
                routeTemplate: "api/Routings/Update",
                defaults: new { controller = "Routings", action = "Update" }
                );
            config.Routes.MapHttpRoute(
                name: "Save_FleetDistribution",
                routeTemplate: "api/Routings/Save_FleetDistribution",
                defaults: new { controller = "Routings", action = "Save_FleetDistribution" }
                );

            config.Routes.MapHttpRoute(
                name: "Vehicle_SaveRouting",
                routeTemplate: "api/Routings/Vehicle_Save",
                defaults: new { controller = "Routings", action = "Vehicle_Save" }
                );

            config.Routes.MapHttpRoute(
            name: "LoadCargoWithWhereClauseRouting",
            routeTemplate: "api/Routings/LoadCargoWithWhereClause",
            defaults: new { controller = "Routings", action = "LoadCargoWithWhereClause" }
            );
            
            config.Routes.MapHttpRoute(
            name: "GetLog",
            routeTemplate: "api/Routings/GetLog",
            defaults: new { controller = "Routings", action = "GetLog" }
            );
            #endregion Routings
            #region PurchaseInvoice/Flexi
            config.Routes.MapHttpRoute(
                name: "FlexiImport_SaveList",
                routeTemplate: "api/PurchaseInvoice/FlexiImport_SaveList",
                defaults: new { controller = "PurchaseInvoice", action = "FlexiImport_SaveList" }
                );
            config.Routes.MapHttpRoute(
                name: "FlexiExport_SaveList",
                routeTemplate: "api/PurchaseInvoice/FlexiExport_SaveList",
                defaults: new { controller = "PurchaseInvoice", action = "FlexiExport_SaveList" }
                );
            config.Routes.MapHttpRoute(
    name: "PurchaseInvoice_Delete",
    routeTemplate: "api/PurchaseInvoice/PurchaseInvoice_Delete",
    defaults: new { controller = "PurchaseInvoice", action = "PurchaseInvoice_Delete" }
    );
            config.Routes.MapHttpRoute(
name: "PurchaseInvoice_OpeningBalance_Delete",
routeTemplate: "api/PurchaseInvoice/PurchaseInvoice_OpeningBalance_Delete",
defaults: new { controller = "PurchaseInvoice", action = "PurchaseInvoice_OpeningBalance_Delete" }
);
            #endregion PurchaseInvoice/Flexi
            #region SubAccountLedger
            config.Routes.MapHttpRoute(
                name: "PrintSubAccountLedger",
                routeTemplate: "api/SubAccountLedger/GetPrintedData",
                defaults: new { controller = "SubAccountLedger", action = "GetPrintedData" }
                );
            config.Routes.MapHttpRoute(
             name: "GetPrintedData",
             routeTemplate: "api/SubAccountBalanceByCurrency/GetPrintedData",
             defaults: new { controller = "SubAccountBalanceByCurrency", action = "GetPrintedData" }
             );


            config.Routes.MapHttpRoute(
                      name: "FillSearchControls",
                      routeTemplate: "api/SubAccountBalanceByCurrency/FillSearchControls",
                      defaults: new { controller = "SubAccountBalanceByCurrency", action = "FillSearchControls" }
                      );


            config.Routes.MapHttpRoute(
               name: "FillSearchControlsTrialBalance",
               routeTemplate: "api/SubAccountTrialBalance/FillSearchControls",
               defaults: new { controller = "SubAccountTrialBalance", action = "FillSearchControls" }
               );

            config.Routes.MapHttpRoute(
                name: "PrintSubAccountTrialBalance",
                routeTemplate: "api/SubAccountTrialBalance/GetPrintedData",
                defaults: new { controller = "SubAccountTrialBalance", action = "GetPrintedData" }
                );
            #endregion SubAccountLedger
            #region Accounting Budget
            config.Routes.MapHttpRoute(
                name: "ChartOfLinkingAccountsFillUpdateSubAccount",
                routeTemplate: "api/ChartOfLinkingAccounts/FillUpdateSubAccount",
                defaults: new { controller = "ChartOfLinkingAccounts", action = "FillUpdateSubAccount" }
                );

            config.Routes.MapHttpRoute(
            name: "ChartOfLinkingAccountsGetModalData",
            routeTemplate: "api/ChartOfLinkingAccounts/GetModalData",
            defaults: new { controller = "ChartOfLinkingAccounts", action = "GetModalData" }
            );

            config.Routes.MapHttpRoute(
            name: "LoadBudgetsFiscalDetails",
            routeTemplate: "api/BudgetsFiscal/LoadBudgetsFiscalDetails",
            defaults: new { controller = "BudgetsFiscal", action = "LoadBudgetsFiscalDetails" }
            );



            config.Routes.MapHttpRoute(
                name: "BudgetsFiscalInsertItems",
                routeTemplate: "api/BudgetsFiscal/InsertItems",
                defaults: new { controller = "BudgetsFiscal", action = "InsertItems" }
                );
            config.Routes.MapHttpRoute(
                name: "BudgetsLoadAccounts",
                routeTemplate: "api/BudgetsFiscal/LoadAccounts",
                defaults: new { controller = "BudgetsFiscal", action = "LoadAccounts" }
                );
            config.Routes.MapHttpRoute(
                name: "BudgetsLoadFiscalYears",
                routeTemplate: "api/BudgetsFiscal/LoadFiscalYears",
                defaults: new { controller = "BudgetsFiscal", action = "LoadFiscalYears" }
                );




            config.Routes.MapHttpRoute
            (
            name: "LoadFA_AssetsGroupDestructions",
            routeTemplate: "api/FA_AssetsGroups/LoadFA_AssetsGroupDestructions",
            defaults: new { controller = "FA_AssetsGroups", action = "LoadFA_AssetsGroupDestructions" }
            );



            config.Routes.MapHttpRoute(
                name: "LoadFA_AssetsGroupDestructionsInsertItems",
                routeTemplate: "api/FA_AssetsGroups/InsertItems",
                defaults: new { controller = "FA_AssetsGroups", action = "InsertItems" }
                );




            config.Routes.MapHttpRoute
            (
            name: "LoadFA_AssetsDestructions",
            routeTemplate: "api/FA_Assets/LoadFA_AssetsDestructions",
            defaults: new { controller = "FA_Assets", action = "LoadFA_AssetsDestructions" }
            );



            config.Routes.MapHttpRoute(
                name: "LoadFA_AssetsDestructionsInsertItems",
                routeTemplate: "api/FA_Assets/InsertItems",
                defaults: new { controller = "FA_Assets", action = "InsertItems" }
                );



            #endregion Accounting Budget
            #region VoucherHeader
            config.Routes.MapHttpRoute(
                name: "VoucherHeader_Save",
                routeTemplate: "api/Voucher/VoucherHeader_Save",
                defaults: new { controller = "Voucher", action = "VoucherHeader_Save" }
                );
            config.Routes.MapHttpRoute(
              name: "VoucherGetSalesIDBySupAccountID",
              routeTemplate: "api/Voucher/GetSalesIDBySupAccountID",
              defaults: new { controller = "Voucher", action = "GetSalesIDBySupAccountID" }
              );
            #endregion VoucherHeader
            #region VoucherDetails
            config.Routes.MapHttpRoute(
                name: "VoucherDetails_Save",
                routeTemplate: "api/Voucher/VoucherDetails_Save",
                defaults: new { controller = "Voucher", action = "VoucherDetails_Save" }
                );

            config.Routes.MapHttpRoute(
                name: "VoucherDetails_InsertItems",
                routeTemplate: "api/Voucher/InsertItems",
                defaults: new { controller = "Voucher", action = "InsertItems" }
                );

            config.Routes.MapHttpRoute(
                name: "VoucherDetails_GetInvoiceAccounts",
                routeTemplate: "api/Voucher/GetInvoiceAccounts",
                defaults: new { controller = "Voucher", action = "GetInvoiceAccounts" }
                );

            #endregion VoucherDetails
            #region Voucher InsertOperationsPayment
            config.Routes.MapHttpRoute(
                name: "InsertOperationsPayment",
                routeTemplate: "api/Voucher/InsertOperationsPayment",
                defaults: new { controller = "Voucher", action = "InsertOperationsPayment" }
                );
            config.Routes.MapHttpRoute(
       name: "GetChargeAccountsList",
       routeTemplate: "api/Voucher/GetChargeAccountsList",
       defaults: new { controller = "Voucher", action = "GetChargeAccountsList" }
       );
            #endregion Voucher InsertOperationsPayment
            #region OperationVehicle
            config.Routes.MapHttpRoute(
                name: "InsertListFromExcel_Vehicle",
                routeTemplate: "api/OperationVehicle/InsertListFromExcel_Vehicle",
                defaults: new { controller = "OperationVehicle", action = "InsertListFromExcel_Vehicle" }
                );
            config.Routes.MapHttpRoute(
                name: "VehicleAction_Save",
                routeTemplate: "api/OperationVehicle/VehicleAction_Save",
                defaults: new { controller = "OperationVehicle", action = "VehicleAction_Save" }
                );
            config.Routes.MapHttpRoute(
                name: "VehicleActionDetails_Save",
                routeTemplate: "api/OperationVehicle/VehicleActionDetails_Save",
                defaults: new { controller = "OperationVehicle", action = "VehicleActionDetails_Save" }
                );
            #endregion OperationVehicle
            #region Allocation With Voucher

            config.Routes.MapHttpRoute(
                    name: "LoadVoucher",
                    routeTemplate: "api/A_ARAllocationWithVoucher/LoadVoucher",
                    defaults: new { controller = "A_ARAllocationWithVoucher", action = "LoadVoucher" }
                    );

            config.Routes.MapHttpRoute(
        name: "LoadInvoices",
        routeTemplate: "api/A_ARAllocationWithVoucher/LoadInvoices",
        defaults: new { controller = "A_ARAllocationWithVoucher", action = "LoadInvoices" }
        );

            config.Routes.MapHttpRoute(
                   name: "LoadVoucherA_APAllocationWithVoucher",
                   routeTemplate: "api/A_APAllocationWithVoucher/LoadVoucher",
                   defaults: new { controller = "A_APAllocationWithVoucher", action = "LoadVoucher" }
                   );

            config.Routes.MapHttpRoute(
        name: "LoadInvoicesA_APAllocationWithVoucher",
        routeTemplate: "api/A_APAllocationWithVoucher/LoadInvoices",
        defaults: new { controller = "A_APAllocationWithVoucher", action = "LoadInvoices" }
        );
            #endregion

            #region OperationTracking
            config.Routes.MapHttpRoute(
                name: "OperationTrackingSendAlarm",
                routeTemplate: "api/OperationTracking/SendAlarm",
                defaults: new { controller = "OperationTracking", action = "SendAlarm" }
            );
            config.Routes.MapHttpRoute(
                name: "OperationTrackingSendAlarmToGroup",
                routeTemplate: "api/OperationTracking/SendAlarmToGroup",
                defaults: new { controller = "OperationTracking", action = "SendAlarmToGroup" }
            );

            #endregion

            #region Delivery
            config.Routes.MapHttpRoute(
                name: "DeliverySave",
                routeTemplate: "api/Delivery/Save",
                defaults: new { controller = "Delivery", action = "Save" }
            );
            config.Routes.MapHttpRoute(
                name: "DeliveryUploadOperationDocuments",
                routeTemplate: "api/Delivery/UploadOperationDocuments",
                defaults: new { controller = "Delivery", action = "UploadOperationDocuments" }
            );
            config.Routes.MapHttpRoute(
                name: "DeliveryLoadDeliveryDocuments",
                routeTemplate: "api/Delivery/LoadDeliveryDocuments",
                defaults: new { controller = "Delivery", action = "LoadDeliveryDocuments" }
            );
            config.Routes.MapHttpRoute(
                name: "DeliveryDeleteList",
                routeTemplate: "api/Delivery/DeleteList",
                defaults: new { controller = "Delivery", action = "DeleteList" }
            );
            config.Routes.MapHttpRoute(
                name: "DeliveryLoadLists",
                routeTemplate: "api/Delivery/LoadLists",
                defaults: new { controller = "Delivery", action = "LoadLists" }
            );

            config.Routes.MapHttpRoute(
                name: "DeliveryUploadTruckingOrderDocuments",
                routeTemplate: "api/Delivery/UploadTruckingOrderDocuments",
                defaults: new { controller = "Delivery", action = "UploadTruckingOrderDocuments" }
            );
            config.Routes.MapHttpRoute(
                name: "Delivery_DeleteImage",
                routeTemplate: "api/Delivery/Delivery_DeleteImage",
                defaults: new { controller = "Delivery", action = "Delivery_DeleteImage" }
            );
            config.Routes.MapHttpRoute(
               name: "Trucking_DeleteImage",
               routeTemplate: "api/Delivery/Trucking_DeleteImage",
               defaults: new { controller = "Delivery", action = "Trucking_DeleteImage" }
           );


            #endregion

            #region Voucher InsertA_VoucherInvoicesPayment
            config.Routes.MapHttpRoute(
                name: "InsertA_VoucherInvoicesPayment",
                routeTemplate: "api/Voucher/InsertA_VoucherInvoicesPayment",
                defaults: new { controller = "Voucher", action = "InsertA_VoucherInvoicesPayment" }
                );
            config.Routes.MapHttpRoute(
            name: "InsertA_VoucherPayableAllocationPayment",
            routeTemplate: "api/Voucher/InsertA_VoucherPayableAllocationPayment",
            defaults: new { controller = "Voucher", action = "InsertA_VoucherPayableAllocationPayment" }
            );
            config.Routes.MapHttpRoute(
             name: "InsertA_VoucherPayableClientPayment",
             routeTemplate: "api/Voucher/InsertA_VoucherPayableClientPayment",
             defaults: new { controller = "Voucher", action = "InsertA_VoucherPayableClientPayment" }
             );
            config.Routes.MapHttpRoute(
            name: "A_voucherSetPostField",
            routeTemplate: "api/Voucher/SetPostField",
            defaults: new { controller = "Voucher", action = "SetPostField" }
            );
            config.Routes.MapHttpRoute(
            name: "A_voucherSetPostFieldTax",
            routeTemplate: "api/Voucher/SetPostFieldTax",
            defaults: new { controller = "Voucher", action = "SetPostFieldTax" }
            );

            config.Routes.MapHttpRoute(
             name: "A_JVSetPostField",
            routeTemplate: "api/JournalVouchers/SetPostField",
            defaults: new { controller = "JournalVouchers", action = "SetPostField" }
            );
            config.Routes.MapHttpRoute(
            name: "A_JVSetPostFieldTax",
            routeTemplate: "api/JournalVouchers/SetPostFieldTax",
            defaults: new { controller = "JournalVouchers", action = "SetPostFieldTax" }
            );
            #endregion InsertA_VoucherInvoicesPayment
            #region 
            config.Routes.MapHttpRoute(
               name: "A_ARAllocationvwAccPartnerBalanceUnapproving_LoadWithWhereClause",
               routeTemplate: "api/A_ARAllocation/vwAccPartnerBalanceUnapproving_LoadWithWhereClause",
               defaults: new { controller = "A_ARAllocation", action = "vwAccPartnerBalanceUnapproving_LoadWithWhereClause" }
               );
            config.Routes.MapHttpRoute(
               name: "A_ARAllocationvwA_PayableAllocation_Unapproving_LoadWithWhereClause",
               routeTemplate: "api/A_ARAllocation/vwA_PayableAllocation_Unapproving_LoadWithWhereClause",
               defaults: new { controller = "A_ARAllocation", action = "vwA_PayableAllocation_Unapproving_LoadWithWhereClause" }
               );
            #endregion
            #region DeleteVoucher
            config.Routes.MapHttpRoute(
                name: "VoucherDeleteCashVoucher",
                routeTemplate: "api/Voucher/DeleteCashVoucher",
                defaults: new { controller = "Voucher", action = "DeleteCashVoucher" }
            );
            config.Routes.MapHttpRoute(
              name: "VoucherDelete",
              routeTemplate: "api/Voucher/Delete",
              defaults: new { controller = "Voucher", action = "Delete" }
          );
            #endregion
            #region DeleteApprovePaymentRequest
            config.Routes.MapHttpRoute(
                name: "PaymentRequestApprove",
                routeTemplate: "api/PaymentRequest/Approve",
                defaults: new { controller = "PaymentRequest", action = "Approve" }
            );
            config.Routes.MapHttpRoute(
                name: "PaymentRequestUnApprove",
                routeTemplate: "api/PaymentRequest/UnApprove",
                defaults: new { controller = "PaymentRequest", action = "UnApprove" }
            );
            config.Routes.MapHttpRoute(
              name: "PaymentRequestDelete",
              routeTemplate: "api/PaymentRequest/Delete",
              defaults: new { controller = "PaymentRequest", action = "Delete" }
          );
            config.Routes.MapHttpRoute(
                          name: "PaymentRequestGetChargeTypes",
                          routeTemplate: "api/PaymentRequest/GetChargeTypes",
                          defaults: new { controller = "PaymentRequest", action = "GetChargeTypes" }
                      );

            config.Routes.MapHttpRoute(
                          name: "PaymentRequestGetInitialValue",
                          routeTemplate: "api/PaymentRequest/GetInitialValue",
                          defaults: new { controller = "PaymentRequest", action = "GetInitialValue" }
                      );

            config.Routes.MapHttpRoute(
                    name: "PaymentRequestGetUserIDFromCustudy",
                    routeTemplate: "api/PaymentRequest/GetUserIDFromCustudy",
                    defaults: new { controller = "PaymentRequest", action = "GetUserIDFromCustudy" }
                );

            config.Routes.MapHttpRoute(
                   name: "PaymentRequestGetCustudy",
                   routeTemplate: "api/PaymentRequest/GetCustudy",
                   defaults: new { controller = "PaymentRequest", action = "GetCustudy" }
                );
            config.Routes.MapHttpRoute(
                  name: "PaymentRequestGetIssuedToBySupplier",
                  routeTemplate: "api/PaymentRequest/GetIssuedToBySupplier",
                  defaults: new { controller = "PaymentRequest", action = "GetIssuedToBySupplier" }
               );
            config.Routes.MapHttpRoute(
                 name: "PaymentRequestSave",
                 routeTemplate: "api/PaymentRequest/Save",
                 defaults: new { controller = "PaymentRequest", action = "Save" }
              );
            config.Routes.MapHttpRoute(
                name: "ExchangeMovement_Save",
                routeTemplate: "api/ExchangeMovement/ExchangeMovement_Save",
                defaults: new { controller = "ExchangeMovement", action = "ExchangeMovement_Save" }
            );
            #endregion
            #region Security
            config.Routes.MapHttpRoute(
                name: "SecurityModulesLoadAll",
                routeTemplate: "api/Security/Modules_LoadAll",
                defaults: new { controller = "Security", action = "Modules_LoadAll" }
            );
            #endregion
            #region NoAccessDepartments
            config.Routes.MapHttpRoute(
                name: "NoAccessDepartments_Save",
                routeTemplate: "api/NoAccessDepartments/Save",
                defaults: new { controller = "NoAccessDepartments", action = "Save" }
            );
            config.Routes.MapHttpRoute(
                name: "DepartmentCharge_Save",
                routeTemplate: "api/NoAccessDepartments/DepartmentCharge_Save",
                defaults: new { controller = "NoAccessDepartments", action = "DepartmentCharge_Save" }
            );
            #endregion NoAccessDepartments

            #region Warehousing
            config.Routes.MapHttpRoute(
                name: "RowLocation_DeleteList",
                routeTemplate: "api/Row/RowLocation_DeleteList",
                defaults: new { controller = "Row", action = "RowLocation_DeleteList" }
                );
            config.Routes.MapHttpRoute(
                name: "RowLocation_ImportFromExcel",
                routeTemplate: "api/Row/RowLocation_ImportFromExcel",
                defaults: new { controller = "Row", action = "RowLocation_ImportFromExcel" }
                );
            config.Routes.MapHttpRoute(
                name: "ReceiveDetailsSerial_Save",
                routeTemplate: "api/Receive/ReceiveDetailsSerial_Save",
                defaults: new { controller = "Receive", action = "ReceiveDetailsSerial_Save" }
                );
            config.Routes.MapHttpRoute(
                name: "ReceiveDetailsSerial_DeleteList",
                routeTemplate: "api/Receive/ReceiveDetailsSerial_DeleteList",
                defaults: new { controller = "Receive", action = "ReceiveDetailsSerial_DeleteList" }
                );
            config.Routes.MapHttpRoute(
                name: "ReceiveDetailsSerial_SaveFromPickup",
                routeTemplate: "api/Receive/ReceiveDetailsSerial_SaveFromPickup",
                defaults: new { controller = "Receive", action = "ReceiveDetailsSerial_SaveFromPickup" }
                );
            config.Routes.MapHttpRoute(
                name: "ReceiveDetailsSerial_DeleteFromPickup",
                routeTemplate: "api/Receive/ReceiveDetailsSerial_DeleteFromPickup",
                defaults: new { controller = "Receive", action = "ReceiveDetailsSerial_DeleteFromPickup" }
                );
            config.Routes.MapHttpRoute(
                name: "ReceiveDetails_ImportFromExcel",
                routeTemplate: "api/Receive/ReceiveDetails_ImportFromExcel",
                defaults: new { controller = "Receive", action = "ReceiveDetails_ImportFromExcel" }
                );
            config.Routes.MapHttpRoute(
                name: "ReceiveDetails_Delete",
                routeTemplate: "api/Receive/ReceiveDetails_Delete",
                defaults: new { controller = "Receive", action = "ReceiveDetails_Delete" }
                );
            config.Routes.MapHttpRoute(
                name: "ReceiveDetailsSerial",
                routeTemplate: "api/Pickup/ReceiveDetailsSerial",
                defaults: new { controller = "Pickup", action = "ReceiveDetailsSerial" }
                );
            config.Routes.MapHttpRoute(
                name: "PickupDetails_Save",
                routeTemplate: "api/Pickup/PickupDetails_Save",
                defaults: new { controller = "Pickup", action = "PickupDetails_Save" }
                );
            config.Routes.MapHttpRoute(
                name: "PickupDetailsSerial_Save",
                routeTemplate: "api/Pickup/PickupDetailsSerial_Save",
                defaults: new { controller = "Pickup", action = "PickupDetailsSerial_Save" }
                );

            config.Routes.MapHttpRoute(
                name: "PickupDetails_ImportFromExcel",
                routeTemplate: "api/Pickup/PickupDetails_ImportFromExcel",
                defaults: new { controller = "Pickup", action = "PickupDetails_ImportFromExcel" }
                );


            config.Routes.MapHttpRoute(
                name: "TransferProducts_ImportFromExcel",
                routeTemplate: "api/TransferProducts/TransferProducts_ImportFromExcel",
                defaults: new { controller = "TransferProducts", action = "TransferProducts_ImportFromExcel" }
                );

            config.Routes.MapHttpRoute(
                name: "AreaLocationsChart_LoadData",
                routeTemplate: "api/AreaLocationsChart/LoadData",
                defaults: new { controller = "AreaLocationsChart", action = "LoadData" }
                );
            config.Routes.MapHttpRoute(
                name: "AreaLocationsChart_GetAreaFromWarehouse",
                routeTemplate: "api/AreaLocationsChart/GetAreaFromWarehouse",
                defaults: new { controller = "AreaLocationsChart", action = "GetAreaFromWarehouse" }
                );

            #endregion Warehousing
            #region CRM By Mostafa Hany
            config.Routes.MapHttpRoute(
                    name: "CRM_ClientsFillInputPort",
                    routeTemplate: "api/CRM_Clients/FillInputPort",
                    defaults: new { controller = "CRM_Clients", action = "FillInputPort" }
                );


            config.Routes.MapHttpRoute(
                name: "IntializeDataSC_DepartmentReturnsVoucher",
                routeTemplate: "api/SC_Transactions/IntializeData",
                defaults: new { controller = "SC_Transactions", action = "IntializeData" }
            );

            config.Routes.MapHttpRoute(
              name: "LoadItemsSC_DepartmentReturnsVoucher",
              routeTemplate: "api/SC_Transactions/LoadItems",
              defaults: new { controller = "SC_Transactions", action = "LoadItems" }
          );



            config.Routes.MapHttpRoute(
                  name: "CRM_ClientsIntializeData",
                  routeTemplate: "api/CRM_Clients/IntializeData",
                  defaults: new { controller = "CRM_Clients", action = "IntializeData" }
              );

            config.Routes.MapHttpRoute(
              name: "CRM_ClientsLoadModalData",
              routeTemplate: "api/CRM_Clients/LoadModalData",
              defaults: new { controller = "CRM_Clients", action = "LoadModalData" }
          );
            config.Routes.MapHttpRoute(
                  name: "CRM_ClientsGetDetailsWithPercent",
                  routeTemplate: "api/CRM_Clients/GetDetailsWithPercent",
                  defaults: new { controller = "CRM_Clients", action = "GetDetailsWithPercent" }
              );
            config.Routes.MapHttpRoute(
                  name: "CRM_ClientshideWithCustomers",
                  routeTemplate: "api/CRM_Clients/hideWithCustomers",
                  defaults: new { controller = "CRM_Clients", action = "hideWithCustomers" }
              );
            config.Routes.MapHttpRoute(
                name: "CRM_ClientsComplaint_FillModal",
                routeTemplate: "api/CRM_Clients/Complaint_FillModal",
                defaults: new { controller = "CRM_Clients", action = "Complaint_FillModal" }
            );
            config.Routes.MapHttpRoute(
                name: "CRM_ClientsComplaintDetailsResponses_FillModal",
                routeTemplate: "api/CRM_Clients/ComplaintDetailsResponses_FillModal",
                defaults: new { controller = "CRM_Clients", action = "ComplaintDetailsResponses_FillModal" }
            );
            config.Routes.MapHttpRoute(
               name: "CRM_ClientsComplaintDetails_FillModal",
               routeTemplate: "api/CRM_Clients/ComplaintDetails_FillModal",
               defaults: new { controller = "CRM_Clients", action = "ComplaintDetails_FillModal" }
           );
            config.Routes.MapHttpRoute(
                 name: "CRM_ClientsComplaint_Delete",
                 routeTemplate: "api/CRM_Clients/Complaint_Delete",
                 defaults: new { controller = "CRM_Clients", action = "Complaint_Delete" }
             );
            config.Routes.MapHttpRoute(
                 name: "CRM_ClientsComplaintDetails_Delete",
                 routeTemplate: "api/CRM_Clients/ComplaintDetails_Delete",
                 defaults: new { controller = "CRM_Clients", action = "ComplaintDetails_Delete" }
             );
            config.Routes.MapHttpRoute(
                name: "CRM_ClientsComplaintDetailsResponses_Delete",
                routeTemplate: "api/CRM_Clients/ComplaintDetailsResponses_Delete",
                defaults: new { controller = "CRM_Clients", action = "ComplaintDetailsResponses_Delete" }
            );

            config.Routes.MapHttpRoute(
               name: "CRM_ClientsComplaint_Save",
               routeTemplate: "api/CRM_Clients/Complaint_Save",
               defaults: new { controller = "CRM_Clients", action = "Complaint_Save" }
           );
            config.Routes.MapHttpRoute(
               name: "CRM_ClientsComplaintDetails_Save",
               routeTemplate: "api/CRM_Clients/ComplaintDetails_Save",
               defaults: new { controller = "CRM_Clients", action = "ComplaintDetails_Save" }
           );
            config.Routes.MapHttpRoute(
               name: "CRM_ClientsComplaintDetailsResponses_Save",
               routeTemplate: "api/CRM_Clients/ComplaintDetailsResponses_Save",
               defaults: new { controller = "CRM_Clients", action = "ComplaintDetailsResponses_Save" }
           );

            config.Routes.MapHttpRoute(
               name: "CRM_ClientsGetEquipmentsByActivity",
               routeTemplate: "api/CRM_Clients/GetEquipmentsByActivity",
               defaults: new { controller = "CRM_Clients", action = "GetEquipmentsByActivity" }
           );
            config.Routes.MapHttpRoute(
               name: "CRM_ClientsSetubSalesLead_Calculate",
               routeTemplate: "api/CRM_Clients/SetubSalesLead_Calculate",
               defaults: new { controller = "CRM_Clients", action = "SetubSalesLead_Calculate" }
           );


            config.Routes.MapHttpRoute(
                 name: "CRM_FollowUpIntializeData",
                 routeTemplate: "api/CRM_FollowUp/IntializeData",
                 defaults: new { controller = "CRM_FollowUp", action = "IntializeData" }
             );


            config.Routes.MapHttpRoute(
               name: "CRM_SalesMenTarget",
               routeTemplate: "api/CRM_SalesMenTarget/LoadActions",
               defaults: new { controller = "CRM_SalesMenTarget", action = "LoadActions" }
            );
            config.Routes.MapHttpRoute(
            name: "vwCRM_ClientsFollowReport",
            routeTemplate: "api/vwCRM_ClientsFollowReport/LoadFollowUpReport",
            defaults: new { controller = "vwCRM_ClientsFollowReport", action = "LoadFollowUpReport" }
            );
            config.Routes.MapHttpRoute(
            name: "LoadTargetReport",
            routeTemplate: "api/vwCRM_ClientsFollowReport/LoadTargetReport",
            defaults: new { controller = "vwCRM_ClientsFollowReport", action = "LoadTargetReport" }
            );
            #endregion CRM By Mostafa Hany
            #region OperationContainersAndPackages
            config.Routes.MapHttpRoute(
                name: "getEirSerial",
                routeTemplate: "api/OperationContainersAndPackages/getEirSerial",
                defaults: new { controller = "OperationContainersAndPackages", action = "getEirSerial" }
                );
            config.Routes.MapHttpRoute(
            name: "OperationContainersAndPackagesGetLog",
            routeTemplate: "api/OperationContainersAndPackages/GetLog",
            defaults: new { controller = "OperationContainersAndPackages", action = "GetLog" }
            );
            #endregion OperationContainersAndPackages
            #region Mostafa Hany
            config.Routes.MapHttpRoute(
                    name: "SC_LoadIntializeData",
                    routeTemplate: "api/SC_Stores/IntializeData",
                    defaults: new { controller = "SC_Stores", action = "IntializeData", pStoresNamesOnly = "" }
                );
            config.Routes.MapHttpRoute(
                  name: "SC_LoadWithPaging",
                  routeTemplate: "api/SC_StoresController/LoadWithPaging",
                  defaults: new { controller = "SC_StoresController", action = "LoadWithPaging" }
              );
            config.Routes.MapHttpRoute(
                  name: "SC_Insert",
                  routeTemplate: "api/SC_StoresController/Insert",
                  defaults: new { controller = "SC_StoresController", action = "Insert" }
              );
            config.Routes.MapHttpRoute(
                  name: "SC_Update",
                  routeTemplate: "api/SC_StoresController/Update",
                  defaults: new { controller = "SC_StoresController", action = "Update" }
              );
            config.Routes.MapHttpRoute(
                 name: "SC_Delete",
                 routeTemplate: "api/SC_StoresController/Delete",
                 defaults: new { controller = "SC_StoresController", action = "Delete" }
             );

            config.Routes.MapHttpRoute(
               name: "I_ItemsGroupsInsertItems",
               routeTemplate: "api/I_ItemsGroups/InsertItems",
               defaults: new { controller = "I_ItemsGroups", action = "InsertItems" }
           );

            config.Routes.MapHttpRoute(
             name: "I_ItemsGroupsInsertList",
             routeTemplate: "api/I_ItemsGroups/InsertList",
             defaults: new { controller = "I_ItemsGroups", action = "InsertList" }
         );


            config.Routes.MapHttpRoute(
             name: "I_ItemsGroupsGetAllItemsFromGroup",
             routeTemplate: "api/I_ItemsGroups/GetAllItemsFromGroup",
             defaults: new { controller = "I_ItemsGroups", action = "GetAllItemsFromGroup" }
            );






            config.Routes.MapHttpRoute(
             name: "ItemsInquiryIntializeData",
             routeTemplate: "api/ItemsInquiry/IntializeData",
             defaults: new { controller = "ItemsInquiry", action = "IntializeData" }
         );
            config.Routes.MapHttpRoute(
 name: "ItemsInquiryLoadAll",
 routeTemplate: "api/ItemsInquiry/LoadAll",
 defaults: new { controller = "ItemsInquiry", action = "LoadAll" }
);


            config.Routes.MapHttpRoute(
             name: "LoadWithWhereClauseEGL",
             routeTemplate: "api/SC_Transactions/LoadWithWhereClauseEGL",
             defaults: new { controller = "SC_Transactions", action = "LoadWithWhereClauseEGL" }
          );

            config.Routes.MapHttpRoute(
             name: "SC_TransactionsLoadWithWhereClause",
             routeTemplate: "api/SC_Transactions/LoadWithWhereClause",
             defaults: new { controller = "SC_Transactions", action = "LoadWithWhereClause" }
          );

            config.Routes.MapHttpRoute(
            name: "SC_Approving_UpdateUserSC_TransactionTypesApproval",
            routeTemplate: "api/SC_Approving/UpdateUserSC_TransactionTypesApproval",
            defaults: new { controller = "SC_Approving", action = "UpdateUserSC_TransactionTypesApproval" }
            );
            config.Routes.MapHttpRoute(
          name: "SC_ApprovingApproveTax",
          routeTemplate: "api/SC_Approving/ApproveTax",
          defaults: new { controller = "SC_Approving", action = "ApproveTax" }
          );
            config.Routes.MapHttpRoute(
          name: "SC_ApprovingApprove",
          routeTemplate: "api/SC_Approving/Approve",
          defaults: new { controller = "SC_Approving", action = "Approve" }
          );

            config.Routes.MapHttpRoute(
                 name: "CheckIsForInvoice",
                 routeTemplate: "api/Voucher/CheckIsForInvoice",
                 defaults: new { controller = "Voucher", action = "CheckIsForInvoice" }
             );
            config.Routes.MapHttpRoute(
               name: "InsertItems",
               routeTemplate: "api/SC_Transactions/InsertItems",
               defaults: new { controller = "SC_Transactions", action = "InsertItems" }
            );
            config.Routes.MapHttpRoute(
               name: "InsertPayablesItems",
               routeTemplate: "api/SC_Transactions/InsertPayablesItems",
               defaults: new { controller = "SC_Transactions", action = "InsertPayablesItems" }
            );
            config.Routes.MapHttpRoute(
   name: "SC_TransactionsLoadPartners",
   routeTemplate: "api/SC_Transactions/LoadPartners",
   defaults: new { controller = "SC_Transactions", action = "LoadPartners" }
);
            config.Routes.MapHttpRoute(
   name: "SC_TransactionsInsertItems",
   routeTemplate: "api/SC_Transactions/InsertItems",
   defaults: new { controller = "SC_Transactions", action = "InsertItems" }
);
            config.Routes.MapHttpRoute(
name: "SC_TransactionsInsertExpenses",
routeTemplate: "api/SC_Transactions/InsertExpenses",
defaults: new { controller = "SC_Transactions", action = "InsertExpenses" }
);

            config.Routes.MapHttpRoute(
                name: "GetInvoicesTotalsData",
                routeTemplate: "api/SL_Reports/GetInvoicesTotalsData",
                defaults: new { controller = "SL_Reports", action = "GetInvoicesTotalsData" }
            );
            config.Routes.MapHttpRoute(
               name: "GetPrintedTotalItemsData",
               routeTemplate: "api/SL_Reports/GetPrintedTotalItemsData",
               defaults: new { controller = "SL_Reports", action = "GetPrintedTotalItemsData" }
           );

            config.Routes.MapHttpRoute(
               name: "GetPSInvoicesTotalsData",
               routeTemplate: "api/PS_Reports/GetInvoicesTotalsData",
               defaults: new { controller = "PS_Reports", action = "GetInvoicesTotalsData" }
           );
            config.Routes.MapHttpRoute(
               name: "GetPSPrintedTotalItemsData",
               routeTemplate: "api/PS_Reports/GetPrintedTotalItemsData",
               defaults: new { controller = "PS_Reports", action = "GetPrintedTotalItemsData" }
           );


            config.Routes.MapHttpRoute(
name: "DeleteItems_PurchasingRequestHeader",
routeTemplate: "api/PS_PurchasingRequest/DeleteItems",
defaults: new { controller = "PS_PurchasingRequest", action = "DeleteItems" }
);

            config.Routes.MapHttpRoute(
name: "Delete_PurchasingRequestHeader",
routeTemplate: "api/PS_PurchasingRequest/Delete",
defaults: new { controller = "PS_PurchasingRequest", action = "Delete" }
);
            config.Routes.MapHttpRoute(
   name: "SavePS_PurchasingRequestHeader",
   routeTemplate: "api/PS_PurchasingRequest/Save",
   defaults: new { controller = "PS_PurchasingRequest", action = "Save" }
);

            config.Routes.MapHttpRoute(
name: "InsertItemsPS_PurchasingRequest",
routeTemplate: "api/PS_PurchasingRequest/InsertItems",
defaults: new { controller = "PS_PurchasingRequest", action = "InsertItems" }
);
            //-------------------------------------------------------------------------------------------

            config.Routes.MapHttpRoute(
name: "DeleteItems_PS_QuotationsHeader",
routeTemplate: "api/PS_Quotations/DeleteItems",
defaults: new { controller = "PS_Quotations", action = "DeleteItems" }
);

            config.Routes.MapHttpRoute(
name: "Delete_PS_QuotationsHeader",
routeTemplate: "api/PS_Quotations/Delete",
defaults: new { controller = "PS_Quotations", action = "Delete" }
);
            config.Routes.MapHttpRoute(
   name: "SavePS_QuotationsHeader",
   routeTemplate: "api/PS_Quotations/Save",
   defaults: new { controller = "PS_Quotations", action = "Save" }
);

            config.Routes.MapHttpRoute(
name: "InsertItemsPS_Quotations",
routeTemplate: "api/PS_Quotations/InsertItems",
defaults: new { controller = "PS_Quotations", action = "InsertItems" }
);




            config.Routes.MapHttpRoute(
name: "Delete_PS_PurchasingOrdersHeader",
routeTemplate: "api/PS_PurchasingOrders/Delete",
defaults: new { controller = "PS_PurchasingOrders", action = "Delete" }
);
            config.Routes.MapHttpRoute(
   name: "SavePS_PurchasingOrdersHeader",
   routeTemplate: "api/PS_PurchasingOrders/Save",
   defaults: new { controller = "PS_PurchasingOrders", action = "Save" }
);

            config.Routes.MapHttpRoute(
name: "InsertItemsPS_PurchasingOrders",
routeTemplate: "api/PS_PurchasingOrders/InsertItems",
defaults: new { controller = "PS_PurchasingOrders", action = "InsertItems" }
);

            config.Routes.MapHttpRoute(
name: "Delete_PS_SupplyOrdersHeader",
routeTemplate: "api/PS_SupplyOrders/Delete",
defaults: new { controller = "PS_SupplyOrders", action = "Delete" }
);
            config.Routes.MapHttpRoute(
   name: "SavePS_SupplyOrdersHeader",
   routeTemplate: "api/PS_SupplyOrders/Save",
   defaults: new { controller = "PS_SupplyOrders", action = "Save" }
);

            config.Routes.MapHttpRoute(
name: "InsertItemsPS_SupplyOrders",
routeTemplate: "api/PS_SupplyOrders/InsertItems",
defaults: new { controller = "PS_SupplyOrders", action = "InsertItems" }
);

            #region PS_Approve
            config.Routes.MapHttpRoute(
            name: "PS_ApprovingApproveTax",
            routeTemplate: "api/PS_Approving/ApproveTax",
            defaults: new { controller = "PS_Approving", action = "ApproveTax" }
            );
            config.Routes.MapHttpRoute(
           name: "PS_ApprovingApprove",
           routeTemplate: "api/PS_Approving/Approve",
           defaults: new { controller = "PS_Approving", action = "Approve" }
           );
            #endregion


            config.Routes.MapHttpRoute(
     name: "LoadingandDischargingData_GetOperationByCode",
     routeTemplate: "api/LoadingandDischargingData/GetOperationByCode",
     defaults: new { controller = "LoadingandDischargingData", action = "GetOperationByCode" }
      );
            config.Routes.MapHttpRoute(
name: "LoadingandDischargingData_InsertItems",
routeTemplate: "api/LoadingandDischargingData/InsertItems",
defaults: new { controller = "LoadingandDischargingData", action = "InsertItems" }
);

            config.Routes.MapHttpRoute(
name: "LoadingandDischargingData_GetOperationInfoByID",
routeTemplate: "api/LoadingandDischargingData/GetOperationInfoByID",
defaults: new { controller = "LoadingandDischargingData", action = "GetOperationInfoByID" }
);
            config.Routes.MapHttpRoute(
name: "LoadingandDischargingData_IntializeData",
routeTemplate: "api/LoadingandDischargingData/IntializeData",
defaults: new { controller = "LoadingandDischargingData", action = "IntializeData" }
);

            config.Routes.MapHttpRoute(
name: "LoadingandDischargingData_InsertLoadingAndDischargingHeaderTruckersDetailsFromExcel",
routeTemplate: "api/LoadingandDischargingData/InsertLoadingAndDischargingHeaderTruckersDetailsFromExcel",
defaults: new { controller = "LoadingandDischargingData", action = "InsertLoadingAndDischargingHeaderTruckersDetailsFromExcel" }
);

            config.Routes.MapHttpRoute(
name: "LoadingandDischargingData_LoadLoadingAndDischargingHeaderTruckers",
routeTemplate: "api/LoadingandDischargingData/LoadLoadingAndDischargingHeaderTruckers",
defaults: new { controller = "LoadingandDischargingData", action = "LoadLoadingAndDischargingHeaderTruckers" }
);

            config.Routes.MapHttpRoute(
name: "LoadingandDischargingData_LoadLoadingAndDischargingHeaderCranes",
routeTemplate: "api/LoadingandDischargingData/LoadLoadingAndDischargingHeaderCranes",
defaults: new { controller = "LoadingandDischargingData", action = "LoadLoadingAndDischargingHeaderCranes" }
);


            config.Routes.MapHttpRoute(
name: "LoadingandDischargingData_InsertLoadingAndDischargingHeaderCranesDetailsFromExcel",
routeTemplate: "api/LoadingandDischargingData/InsertLoadingAndDischargingHeaderCranesDetailsFromExcel",
defaults: new { controller = "LoadingandDischargingData", action = "InsertLoadingAndDischargingHeaderCranesDetailsFromExcel" }
);
            //
            config.Routes.MapHttpRoute(
       name: "LD_Storage_GetOperationByCode",
       routeTemplate: "api/LD_Storage/GetOperationByCode",
       defaults: new { controller = "LD_Storage", action = "GetOperationByCode" }
        );
            config.Routes.MapHttpRoute(
name: "LD_Storage_InsertItems",
routeTemplate: "api/LD_Storage/InsertItems",
defaults: new { controller = "LD_Storage", action = "InsertItems" }
);

            config.Routes.MapHttpRoute(
name: "LD_Storage_GetOperationInfoByID",
routeTemplate: "api/LD_Storage/GetOperationInfoByID",
defaults: new { controller = "LD_Storage", action = "GetOperationInfoByID" }
);
            config.Routes.MapHttpRoute(
name: "LD_Storage_IntializeData",
routeTemplate: "api/LD_Storage/IntializeData",
defaults: new { controller = "LD_Storage", action = "IntializeData" }
);
           

            config.Routes.MapHttpRoute(
name: "LD_Storage_LoadStorageTransactions",
routeTemplate: "api/LD_Storage/LoadStorageTransactions",
defaults: new { controller = "LD_Storage", action = "LoadStorageTransactions" }
);

            config.Routes.MapHttpRoute(
name: "LD_Storage_InsertStorageTransactionsFromExcel",
routeTemplate: "api/LD_Storage/InsertStorageTransactionsFromExcel",
defaults: new { controller = "LD_Storage", action = "InsertStorageTransactionsFromExcel" }
);



            //-------


            config.Routes.MapHttpRoute(
     name: "LD_Workers_GetOperationByCode",
     routeTemplate: "api/LD_Workers/GetOperationByCode",
     defaults: new { controller = "LD_Workers", action = "GetOperationByCode" }
      );
            config.Routes.MapHttpRoute(
name: "LD_Workers_InsertItems",
routeTemplate: "api/LD_Workers/InsertItems",
defaults: new { controller = "LD_Workers", action = "InsertItems" }
);

            config.Routes.MapHttpRoute(
name: "LD_Workers_GetOperationInfoByID",
routeTemplate: "api/LD_Workers/GetOperationInfoByID",
defaults: new { controller = "LD_Workers", action = "GetOperationInfoByID" }
);
            config.Routes.MapHttpRoute(
name: "LD_Workers_IntializeData",
routeTemplate: "api/LD_Workers/IntializeData",
defaults: new { controller = "LD_Workers", action = "IntializeData" }
);





            config.Routes.MapHttpRoute(
name: "LD_Workers_LoadLoadingAndDischargingHeaderWorkers",
routeTemplate: "api/LD_Workers/LoadLoadingAndDischargingHeaderWorkers",
defaults: new { controller = "LD_Workers", action = "LoadLoadingAndDischargingHeaderWorkers" }
);


            config.Routes.MapHttpRoute(
name: "LD_Workers_InsertLoadingAndDischargingHeaderWorkersDetailsFromExcel",
routeTemplate: "api/LD_Workers/InsertLoadingAndDischargingHeaderWorkersDetailsFromExcel",
defaults: new { controller = "LD_Workers", action = "InsertLoadingAndDischargingHeaderWorkersDetailsFromExcel" }
);







            //--------------------------------------------------------------------------------------------
            config.Routes.MapHttpRoute(
                      name: "SL_InvoicesLoadWithPaging",
                      routeTemplate: "api/SL_Invoices/LoadWithPaging",
                      defaults: new { controller = "SL_Invoices", action = "LoadWithPaging" }
                     );
            config.Routes.MapHttpRoute(
           name: "SL_InvoicesLoadWithWhereClause",
           routeTemplate: "api/SL_Invoices/LoadWithWhereClause",
           defaults: new { controller = "SL_Invoices", action = "LoadWithWhereClause" }
          );

            config.Routes.MapHttpRoute(
             name: "SL_InvoicesIntializeData",
             routeTemplate: "api/SL_Invoices/IntializeData",
             defaults: new { controller = "SL_Invoices", action = "IntializeData" }
             );
            config.Routes.MapHttpRoute(
  name: "SL_InvoicesInsertItems",
  routeTemplate: "api/SL_Invoices/InsertItems",
  defaults: new { controller = "SL_Invoices", action = "InsertItems" }
 );

            config.Routes.MapHttpRoute(
           name: "SL_InvoicesSave",
           routeTemplate: "api/SL_Invoices/Save",
           defaults: new { controller = "SL_Invoices", action = "Save" }
          );

            config.Routes.MapHttpRoute(
           name: "SL_InvoicesLoadDetails",
           routeTemplate: "api/SL_Invoices/LoadDetails",
           defaults: new { controller = "SL_Invoices", action = "LoadDetails" }
           );

            config.Routes.MapHttpRoute(
            name: "SL_InvoicesLoadWithPagingItems",
            routeTemplate: "api/SL_Invoices/LoadWithPagingItems",
            defaults: new { controller = "SL_Invoices", action = "LoadWithPagingItems" }
            );

            config.Routes.MapHttpRoute(
            name: "SL_InvoicesClientDbtCrdtNotesLoadDetails",
            routeTemplate: "api/ClientDbtCrdtNotes/LoadDetails",
            defaults: new { controller = "ClientDbtCrdtNotes", action = "LoadDetails" }
           );

            config.Routes.MapHttpRoute(
           name: "SL_InvoicesClientDbtCrdtNotesFillInvoiceByClient",
           routeTemplate: "api/ClientDbtCrdtNotes/FillInvoiceByClient",
           defaults: new { controller = "ClientDbtCrdtNotes", action = "FillInvoiceByClient" }
          );

            config.Routes.MapHttpRoute(
        name: "SL_InvoicesClientDbtCrdtNotesServicesLoadInvoicesDetailsByInvoiceID",
        routeTemplate: "api/ClientDbtCrdtNotes/LoadInvoicesDetailsByInvoiceID",
        defaults: new { controller = "ClientDbtCrdtNotes", action = "LoadInvoicesDetailsByInvoiceID" }
        );

            config.Routes.MapHttpRoute(
            name: "SL_InvoicesClientDbtCrdtNotesDelete",
            routeTemplate: "api/ClientDbtCrdtNotes/Delete",
            defaults: new { controller = "ClientDbtCrdtNotes", action = "Delete" }
            );
            config.Routes.MapHttpRoute(
        name: "SL_InvoicesClientDbtCrdtNotesPrint",
        routeTemplate: "api/ClientDbtCrdtNotes/Print",
        defaults: new { controller = "ClientDbtCrdtNotes", action = "Print" }
        );
            config.Routes.MapHttpRoute(
          name: "PaymentsLoadWithPaging",
          routeTemplate: "api/SL_Payments/LoadWithPaging",
          defaults: new { controller = "SL_Payments", action = "LoadWithPaging" }
          );
            config.Routes.MapHttpRoute(
           name: "SL_PaymentsLoadInvoicesDbtCrdtNote",
           routeTemplate: "api/SL_Payments/LoadInvoicesDbtCrdtNote",
           defaults: new { controller = "SL_Payments", action = "LoadInvoicesDbtCrdtNote" }
           );
            config.Routes.MapHttpRoute(
          name: "SL_PaymentsLoadInvoices",
          routeTemplate: "api/SL_Payments/LoadInvoices",
          defaults: new { controller = "SL_Payments", action = "LoadInvoices" }
          );
            config.Routes.MapHttpRoute(
           name: "PaymentsLoadAllCurrencyByCurrencyID",
           routeTemplate: "api/SL_Payments/LoadAllCurrencyByCurrencyID",
           defaults: new { controller = "SL_Payments", action = "LoadAllCurrencyByCurrencyID" }
           );
            config.Routes.MapHttpRoute(
              name: "SLPaymentsGetRefundAccountIDAndSubAccountID",
              routeTemplate: "api/SL_Payments/GetRefundAccountIDAndSubAccountID",
              defaults: new { controller = "SL_Payments", action = "GetRefundAccountIDAndSubAccountID" }
          );
            config.Routes.MapHttpRoute(
            name: "SLPaymentsLoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned",
            routeTemplate: "api/SL_Payments/LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned",
            defaults: new { controller = "SL_Payments", action = "LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned" }
        );

            config.Routes.MapHttpRoute(
            name: "SL_PaymentsGetAccountIDAndSubAccountID",
            routeTemplate: "api/SL_Payments/GetAccountIDAndSubAccountID",
            defaults: new { controller = "SL_Payments", action = "GetAccountIDAndSubAccountID" }
            );
            config.Routes.MapHttpRoute(
                name: "TaxeTypesLoadAll",
                routeTemplate: "api/TaxeTypes/LoadAll",
                defaults: new { controller = "TaxeTypes", action = "LoadAll" }
                );
            //*******************************************************************************************************************
            config.Routes.MapHttpRoute(
                name: "LoadAllCurrencyByName",
                routeTemplate: "api/SL_Payments/LoadAllCurrencyByName",
                defaults: new { controller = "SL_Payments", action = "LoadAllCurrencyByName" }
                );

            config.Routes.MapHttpRoute(
               name: "SL_PaymentsInsert",
               routeTemplate: "api/SL_Payments/Insert",
               defaults: new { controller = "SL_Payments", action = "Insert" }
               );

            #region PAYMENTS EGL
            //a_payments
            config.Routes.MapHttpRoute(
           name: "A_PaymentsLoadWithPagingEGL",
           routeTemplate: "api/A_PaymentsEGL/LoadWithPaging",
           defaults: new { controller = "A_PaymentsEGL", action = "LoadWithPaging" }
           );

            config.Routes.MapHttpRoute(
          name: "A_PaymentsLoadInvoicesEGL",
          routeTemplate: "api/A_PaymentsEGL/LoadInvoices",
          defaults: new { controller = "A_PaymentsEGL", action = "LoadInvoices" }
          );
            config.Routes.MapHttpRoute(
           name: "A_PaymentsLoadAllCurrencyByCurrencyIDEGL",
           routeTemplate: "api/A_PaymentsEGL/LoadAllCurrencyByCurrencyID",
           defaults: new { controller = "A_PaymentsEGL", action = "LoadAllCurrencyByCurrencyID" }
           );
            config.Routes.MapHttpRoute(
              name: "A_PaymentsGetRefundAccountIDAndSubAccountIDEGL",
              routeTemplate: "api/A_PaymentsEGL/GetRefundAccountIDAndSubAccountID",
              defaults: new { controller = "A_PaymentsEGL", action = "GetRefundAccountIDAndSubAccountID" }
          );
            config.Routes.MapHttpRoute(
            name: "A_PaymentsLoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturnedEGL",
            routeTemplate: "api/A_PaymentsEGL/LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned",
            defaults: new { controller = "A_PaymentsEGL", action = "LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned" }
        );

            config.Routes.MapHttpRoute(
            name: "A_PaymentsGetAccountIDAndSubAccountIDEGL",
            routeTemplate: "api/A_PaymentsEGL/GetAccountIDAndSubAccountID",
            defaults: new { controller = "A_PaymentsEGL", action = "GetAccountIDAndSubAccountID" }
            );

            //*******************************************************************************************************************
            config.Routes.MapHttpRoute(
                name: "A_LoadAllCurrencyByNameEGL",
                routeTemplate: "api/A_PaymentsEGL/LoadAllCurrencyByName",
                defaults: new { controller = "A_PaymentsEGL", action = "LoadAllCurrencyByName" }
                );
            config.Routes.MapHttpRoute(
               name: "A_PaymentsLoadAllClientsByNameEGL",
               routeTemplate: "api/A_PaymentsEGL/LoadAllClientsByName",
               defaults: new { controller = "A_PaymentsEGL", action = "LoadAllClientsByName" }
               );
            config.Routes.MapHttpRoute(
               name: "A__PaymentsInsertEGL",
               routeTemplate: "api/A_PaymentsEGL/Insert",
               defaults: new { controller = "A_PaymentsEGL", action = "Insert" }
               );
            config.Routes.MapHttpRoute(
              name: "A_PaymentsLoadAllClientsByNameWithInvoiceEGL",
              routeTemplate: "api/A_PaymentsEGL/LoadAllClientsByNameWithInvoice",
              defaults: new { controller = "A_PaymentsEGL", action = "LoadAllClientsByNameWithInvoice" }
              );
            #endregion
            //a_payments
            config.Routes.MapHttpRoute(
           name: "A_PaymentsLoadWithPaging",
           routeTemplate: "api/A_Payments/LoadWithPaging",
           defaults: new { controller = "A_Payments", action = "LoadWithPaging" }
           );

            config.Routes.MapHttpRoute(
          name: "A_PaymentsLoadInvoices",
          routeTemplate: "api/A_Payments/LoadInvoices",
          defaults: new { controller = "A_Payments", action = "LoadInvoices" }
          );
            config.Routes.MapHttpRoute(
           name: "A_PaymentsLoadAllCurrencyByCurrencyID",
           routeTemplate: "api/A_Payments/LoadAllCurrencyByCurrencyID",
           defaults: new { controller = "A_Payments", action = "LoadAllCurrencyByCurrencyID" }
           );
            config.Routes.MapHttpRoute(
              name: "A_PaymentsGetRefundAccountIDAndSubAccountID",
              routeTemplate: "api/A_Payments/GetRefundAccountIDAndSubAccountID",
              defaults: new { controller = "A_Payments", action = "GetRefundAccountIDAndSubAccountID" }
          );
            config.Routes.MapHttpRoute(
            name: "A_PaymentsLoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned",
            routeTemplate: "api/A_Payments/LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned",
            defaults: new { controller = "A_Payments", action = "LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned" }
        );

            config.Routes.MapHttpRoute(
            name: "A_PaymentsGetAccountIDAndSubAccountID",
            routeTemplate: "api/A_Payments/GetAccountIDAndSubAccountID",
            defaults: new { controller = "A_Payments", action = "GetAccountIDAndSubAccountID" }
            );

            //*******************************************************************************************************************
            config.Routes.MapHttpRoute(
                name: "A_LoadAllCurrencyByName",
                routeTemplate: "api/A_Payments/LoadAllCurrencyByName",
                defaults: new { controller = "A_Payments", action = "LoadAllCurrencyByName" }
                );
            config.Routes.MapHttpRoute(
               name: "A_PaymentsLoadAllClientsByName",
               routeTemplate: "api/A_Payments/LoadAllClientsByName",
               defaults: new { controller = "A_Payments", action = "LoadAllClientsByName" }
               );
            config.Routes.MapHttpRoute(
               name: "A__PaymentsInsert",
               routeTemplate: "api/A_Payments/Insert",
               defaults: new { controller = "A_Payments", action = "Insert" }
               );
            config.Routes.MapHttpRoute(
              name: "A_PaymentsLoadAllClientsByNameWithInvoice",
              routeTemplate: "api/A_Payments/LoadAllClientsByNameWithInvoice",
              defaults: new { controller = "A_Payments", action = "LoadAllClientsByNameWithInvoice" }
              );
            config.Routes.MapHttpRoute(
               name: "LoadAllTransactionTypes",
               routeTemplate: "api/Invoices/LoadAllTransactionTypes",
               defaults: new { controller = "Invoices", action = "LoadAllTransactionTypes" }
               );
            config.Routes.MapHttpRoute(
              name: "ApproveOrUnApproveTax",
              routeTemplate: "api/Invoices/ApproveOrUnApproveTax",
              defaults: new { controller = "Invoices", action = "ApproveOrUnApproveTax" }
              );
            config.Routes.MapHttpRoute(
             name: "ApproveOrUnApprove",
             routeTemplate: "api/Invoices/ApproveOrUnApprove",
             defaults: new { controller = "Invoices", action = "ApproveOrUnApprove" }
             );

            config.Routes.MapHttpRoute(
               name: "Update",
               routeTemplate: "api/Invoices/Update",
               defaults: new { controller = "Invoices", action = "Update" }
               );
            config.Routes.MapHttpRoute(
               name: "ImportFromExcelFile",
               routeTemplate: "api/Invoices/ImportFromExcelFile",
               defaults: new { controller = "Invoices", action = "ImportFromExcelFile" }
               );

            config.Routes.MapHttpRoute(
              name: "InvoicesgetInvoiceDetailsForVoucher",
              routeTemplate: "api/Invoices/getInvoiceDetailsForVoucher",
              defaults: new { controller = "Invoices", action = "getInvoiceDetailsForVoucher" }
              );
            // LoadAllCurrencyByName()



            //    config.Routes.MapHttpRoute(
            //name: "FA_AssetsIntializeData",
            //routeTemplate: "api/FA_Assets/IntializeData",
            //defaults: new { controller = "FA_Assets", action = "IntializeData" }
            //);




            #endregion Mostafa Hany
            #region Accounting

            config.Routes.MapHttpRoute(
              name: "GetPrintedDataIncomeStatement",
              routeTemplate: "api/IncomeStatement/GetPrintedData",
              defaults: new { controller = "IncomeStatement", action = "GetPrintedData" }
          );

            config.Routes.MapHttpRoute(
  name: "FillSearchControlsIncomeStatement",
  routeTemplate: "api/IncomeStatement/FillSearchControls",
  defaults: new { controller = "IncomeStatement", action = "FillSearchControls" }
);

            config.Routes.MapHttpRoute(
            name: "InvestmentcostcentersLoadWithPaging",
            routeTemplate: "api/Investmentcostcenters/LoadWithPaging",
            defaults: new { controller = "Investmentcostcenters", action = "LoadWithPaging" }
            );

            config.Routes.MapHttpRoute(
                name: "InvestmentcostcentersInsertItems",
                routeTemplate: "api/Investmentcostcenters/InsertItems",
                defaults: new { controller = "Investmentcostcenters", action = "InsertItems" }
                );

            config.Routes.MapHttpRoute(
             name: "InvestmentcostcentersFillComboDet",
             routeTemplate: "api/Investmentcostcenters/FillComboDet",
             defaults: new { controller = "Investmentcostcenters", action = "FillComboDet" }
             );
            config.Routes.MapHttpRoute(
             name: "InvestmentcostcentersGetTotalCS",
             routeTemplate: "api/Investmentcostcenters/GetTotalCS",
             defaults: new { controller = "Investmentcostcenters", action = "GetTotalCS" }
             );



            #endregion

            #region CashPositionReport

            config.Routes.MapHttpRoute(
              name: "GetPrintedDataCashPosition",
              routeTemplate: "api/CashPosition/GetPrintedData",
              defaults: new { controller = "CashPosition", action = "GetPrintedData" }
            );

            config.Routes.MapHttpRoute(
      name: "FillFilteraCashPosition",
      routeTemplate: "api/CashPosition/FillFilter",
      defaults: new { controller = "CashPosition", action = "FillFilter" }
    );
            #endregion


            #region Container Freight Station

            #region WH_CFS_Transactions
            config.Routes.MapHttpRoute(
              name: "SaveRoadNumber",
              routeTemplate: "api/WH_ManifestReport/SaveRoadNumber",
              defaults: new { controller = "WH_ManifestReport", action = "SaveRoadNumber" }
          );
            config.Routes.MapHttpRoute(
             name: "WH_CFS_GateIn_Insert",
             routeTemplate: "api/WH_CFS_GateIn/WH_CFS_GateIn_Insert",
             defaults: new { controller = "WH_CFS_GateIn", action = "WH_CFS_GateIn_Insert" }
         );
            config.Routes.MapHttpRoute(
             name: "WH_CFS_GateInInventory_Update",
             routeTemplate: "api/WH_CFS_GateInInventory/WH_CFS_GateInInventory_Update",
             defaults: new { controller = "WH_CFS_GateInInventory", action = "WH_CFS_GateInInventory_Update" }
         );
            config.Routes.MapHttpRoute(
              name: "WH_CFS_Invoices_LoadItem",
              routeTemplate: "api/WH_CFS_Invoices/WH_CFS_Invoices_LoadItem",
              defaults: new { controller = "WH_CFS_Invoices", action = "WH_CFS_Invoices_LoadItem" }
          );
            config.Routes.MapHttpRoute(
              name: "GenerateReceivables",
              routeTemplate: "api/WH_CFS_Invoices/GenerateReceivables",
              defaults: new { controller = "WH_CFS_Invoices", action = "GenerateReceivables" }
          );
            config.Routes.MapHttpRoute(
              name: "Insert",
              routeTemplate: "api/OperationContainersAndPackages/Insert",
              defaults: new { controller = "OperationContainersAndPackages", action = "Insert" }
          );
            config.Routes.MapHttpRoute(
              name: "UpdateInvoice",
              routeTemplate: "api/OperationContainersAndPackages/Update",
              defaults: new { controller = "OperationContainersAndPackages", action = "Update" }
          );
            config.Routes.MapHttpRoute(
              name: "WH_CFS_ReleaseOrders_Insert",
              routeTemplate: "api/WH_CFS_ReleaseOrders/WH_CFS_ReleaseOrders_Insert",
              defaults: new { controller = "WH_CFS_ReleaseOrders", action = "WH_CFS_ReleaseOrders_Insert" }
          );
            config.Routes.MapHttpRoute(
              name: "WH_CFS_ReleaseOrders_Update",
              routeTemplate: "api/WH_CFS_ReleaseOrders/WH_CFS_ReleaseOrders_Update",
              defaults: new { controller = "WH_CFS_ReleaseOrders", action = "WH_CFS_ReleaseOrders_Update" }
          );
            config.Routes.MapHttpRoute(
              name: "WH_CFS_ReleaseOrderNotes_Save",
              routeTemplate: "api/WH_CFS_ReleaseOrders/WH_CFS_ReleaseOrderNotes_Save",
              defaults: new { controller = "WH_CFS_ReleaseOrders", action = "WH_CFS_ReleaseOrderNotes_Save" }
          );
            config.Routes.MapHttpRoute(
              name: "GetReleaseNumber",
              routeTemplate: "api/WH_CFS_ReleaseOrders/GetReleaseNumber",
              defaults: new { controller = "WH_CFS_ReleaseOrders", action = "GetReleaseNumber" }
          );
            config.Routes.MapHttpRoute(
              name: "GetNoteDetails",
              routeTemplate: "api/WH_CFS_ReleaseOrders/GetNoteDetails",
              defaults: new { controller = "WH_CFS_ReleaseOrders", action = "GetNoteDetails" }
          );
            #endregion WH_CFS_Transactions

            #region WH_FCL_Tariff
            config.Routes.MapHttpRoute(
                name: "WH_FCL_Tariff_LoadAll",
                routeTemplate: "api/WH_FCL_Tariffs/WH_FCL_Tariff_LoadAll",
                defaults: new { controller = "WH_FCL_Tariffs", action = "WH_FCL_Tariff_LoadAll" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_FCL_Tariff_LoadItem",
                routeTemplate: "api/WH_FCL_Tariffs/WH_FCL_Tariff_LoadItem",
                defaults: new { controller = "WH_FCL_Tariffs", action = "WH_FCL_Tariff_LoadItem" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_FCL_Tariffs_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned",
                routeTemplate: "api/WH_FCL_Tariffs/WH_FCL_Tariffs_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned",
                defaults: new { controller = "WH_FCL_Tariffs", action = "WH_FCL_Tariffs_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_FCL_Tariff_Save",
                routeTemplate: "api/WH_FCL_Tariffs/WH_FCL_Tariff_Save",
                defaults: new { controller = "WH_FCL_Tariffs", action = "WH_FCL_Tariff_Save" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_FCL_Tariff_DeleteList",
                routeTemplate: "api/WH_FCL_Tariffs/WH_FCL_Tariff_DeleteList",
                defaults: new { controller = "WH_FCL_Tariffs", action = "WH_FCL_Tariff_DeleteList" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_FCL_Tariff_Details_LoadItem",
                routeTemplate: "api/WH_FCL_Tariffs/WH_FCL_Tariff_Details_LoadItem",
                defaults: new { controller = "WH_FCL_Tariffs", action = "WH_FCL_Tariff_Details_LoadItem" }
                );
            config.Routes.MapHttpRoute(
                name: "GetCalcTypesID",
                routeTemplate: "api/WH_FCL_Tariffs/GetCalcTypesID",
                defaults: new { controller = "WH_FCL_Tariffs", action = "GetCalcTypesID" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_FCL_Tariff_Details_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned",
                routeTemplate: "api/WH_FCL_Tariffs/WH_FCL_Tariff_Details_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned",
                defaults: new { controller = "WH_FCL_Tariffs", action = "WH_FCL_Tariff_Details_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_FCL_Tariff_Details_Save",
                routeTemplate: "api/WH_FCL_Tariffs/WH_FCL_Tariff_Details_Save",
                defaults: new { controller = "WH_FCL_Tariffs", action = "WH_FCL_Tariff_Details_Save" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_FCL_Tariff_Details_DeleteList",
                routeTemplate: "api/WH_FCL_Tariffs/WH_FCL_Tariff_Details_DeleteList",
                defaults: new { controller = "WH_FCL_Tariffs", action = "WH_FCL_Tariff_Details_DeleteList" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_FCL_Tariff_Imo_LoadItem",
                routeTemplate: "api/WH_FCL_Tariffs/WH_FCL_Tariff_Imo_LoadItem",
                defaults: new { controller = "WH_FCL_Tariffs", action = "WH_FCL_Tariff_Imo_LoadItem" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_FCL_Tariff_Imo_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned",
                routeTemplate: "api/WH_FCL_Tariffs/WH_FCL_Tariff_Imo_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned",
                defaults: new { controller = "WH_FCL_Tariffs", action = "WH_FCL_Tariff_Imo_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_FCL_Tariff_Imo_Save",
                routeTemplate: "api/WH_FCL_Tariffs/WH_FCL_Tariff_Imo_Save",
                defaults: new { controller = "WH_FCL_Tariffs", action = "WH_FCL_Tariff_Imo_Save" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_FCL_Tariff_Imo_DeleteList",
                routeTemplate: "api/WH_FCL_Tariffs/WH_FCL_Tariff_Imo_DeleteList",
                defaults: new { controller = "WH_FCL_Tariffs", action = "WH_FCL_Tariff_Imo_DeleteList" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_FCL_Tariff_Details_Periods_LoadItem",
                routeTemplate: "api/WH_FCL_Tariffs/WH_FCL_Tariff_Details_Periods_LoadItem",
                defaults: new { controller = "WH_FCL_Tariffs", action = "WH_FCL_Tariff_Details_Periods_LoadItem" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_FCL_Tariff_Details_Periods_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned",
                routeTemplate: "api/WH_FCL_Tariffs/WH_FCL_Tariff_Details_Periods_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned",
                defaults: new { controller = "WH_FCL_Tariffs", action = "WH_FCL_Tariff_Details_Periods_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_FCL_Tariff_Details_Periods_Save",
                routeTemplate: "api/WH_FCL_Tariffs/WH_FCL_Tariff_Details_Periods_Save",
                defaults: new { controller = "WH_FCL_Tariffs", action = "WH_FCL_Tariff_Details_Periods_Save" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_FCL_Tariff_Details_Periods_DeleteList",
                routeTemplate: "api/WH_FCL_Tariffs/WH_FCL_Tariff_Details_Periods_DeleteList",
                defaults: new { controller = "WH_FCL_Tariffs", action = "WH_FCL_Tariff_Details_Periods_DeleteList" }
                );
            #endregion WH_FCL_Tariff

            #region WH_CSL_Tariff
            config.Routes.MapHttpRoute(
                name: "WH_CSL_Tariff_LoadAll",
                routeTemplate: "api/WH_CSL_Tariffs/WH_CSL_Tariff_LoadAll",
                defaults: new { controller = "WH_CSL_Tariffs", action = "WH_CSL_Tariff_LoadAll" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_CSL_Tariff_LoadItem",
                routeTemplate: "api/WH_CSL_Tariffs/WH_CSL_Tariff_LoadItem",
                defaults: new { controller = "WH_CSL_Tariffs", action = "WH_CSL_Tariff_LoadItem" }
                );
            config.Routes.MapHttpRoute(
    name: "WH_CSL_Tariffs_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned",
    routeTemplate: "api/WH_CSL_Tariffs/WH_CSL_Tariffs_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned",
    defaults: new { controller = "WH_CSL_Tariffs", action = "WH_CSL_Tariffs_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned" }
    );
            config.Routes.MapHttpRoute(
name: "WH_CSL_Tariff_Save",
routeTemplate: "api/WH_CSL_Tariffs/WH_CSL_Tariff_Save",
defaults: new { controller = "WH_CSL_Tariffs", action = "WH_CSL_Tariff_Save" }
);
            config.Routes.MapHttpRoute(
name: "WH_CSL_Tariff_DeleteList",
routeTemplate: "api/WH_CSL_Tariffs/WH_CSL_Tariff_DeleteList",
defaults: new { controller = "WH_CSL_Tariffs", action = "WH_CSL_Tariff_DeleteList" }
);
            config.Routes.MapHttpRoute(
name: "WH_CSL_Tariff_Details_LoadItem",
routeTemplate: "api/WH_CSL_Tariffs/WH_CSL_Tariff_Details_LoadItem",
defaults: new { controller = "WH_CSL_Tariffs", action = "WH_CSL_Tariff_Details_LoadItem" }
);
            config.Routes.MapHttpRoute(
    name: "GetCalcTypesIDCSL",
    routeTemplate: "api/WH_CSL_Tariffs/GetCalcTypesID",
    defaults: new { controller = "WH_CSL_Tariffs", action = "GetCalcTypesID" }
    );
            config.Routes.MapHttpRoute(
name: "WH_CSL_Tariff_Details_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned",
routeTemplate: "api/WH_CSL_Tariffs/WH_CSL_Tariff_Details_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned",
defaults: new { controller = "WH_CSL_Tariffs", action = "WH_CSL_Tariff_Details_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned" }
);
            config.Routes.MapHttpRoute(
name: "WH_CSL_Tariff_Details_Save",
routeTemplate: "api/WH_CSL_Tariffs/WH_CSL_Tariff_Details_Save",
defaults: new { controller = "WH_CSL_Tariffs", action = "WH_CSL_Tariff_Details_Save" }
);
            config.Routes.MapHttpRoute(
name: "WH_CSL_Tariff_Details_DeleteList",
routeTemplate: "api/WH_CSL_Tariffs/WH_CSL_Tariff_Details_DeleteList",
defaults: new { controller = "WH_CSL_Tariffs", action = "WH_CSL_Tariff_Details_DeleteList" }
);
            config.Routes.MapHttpRoute(
name: "WH_CSL_Tariff_Imo_LoadItem",
routeTemplate: "api/WH_CSL_Tariffs/WH_CSL_Tariff_Imo_LoadItem",
defaults: new { controller = "WH_CSL_Tariffs", action = "WH_CSL_Tariff_Imo_LoadItem" }
);
            config.Routes.MapHttpRoute(
name: "WH_CSL_Tariff_Imo_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned",
routeTemplate: "api/WH_CSL_Tariffs/WH_CSL_Tariff_Imo_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned",
defaults: new { controller = "WH_CSL_Tariffs", action = "WH_CSL_Tariff_Imo_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned" }
);
            config.Routes.MapHttpRoute(
name: "WH_CSL_Tariff_Imo_Save",
routeTemplate: "api/WH_CSL_Tariffs/WH_CSL_Tariff_Imo_Save",
defaults: new { controller = "WH_CSL_Tariffs", action = "WH_CSL_Tariff_Imo_Save" }
);
            config.Routes.MapHttpRoute(
name: "WH_CSL_Tariff_Imo_DeleteList",
routeTemplate: "api/WH_CSL_Tariffs/WH_CSL_Tariff_Imo_DeleteList",
defaults: new { controller = "WH_CSL_Tariffs", action = "WH_CSL_Tariff_Imo_DeleteList" }
);
            config.Routes.MapHttpRoute(
name: "WH_CSL_Tariff_Details_Periods_LoadItem",
routeTemplate: "api/WH_CSL_Tariffs/WH_CSL_Tariff_Details_Periods_LoadItem",
defaults: new { controller = "WH_CSL_Tariffs", action = "WH_CSL_Tariff_Details_Periods_LoadItem" }
);
            config.Routes.MapHttpRoute(
name: "WH_CSL_Tariff_Details_Periods_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned",
routeTemplate: "api/WH_CSL_Tariffs/WH_CSL_Tariff_Details_Periods_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned",
defaults: new { controller = "WH_CSL_Tariffs", action = "WH_CSL_Tariff_Details_Periods_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned" }
);
            config.Routes.MapHttpRoute(
name: "WH_CSL_Tariff_Details_Periods_Save",
routeTemplate: "api/WH_CSL_Tariffs/WH_CSL_Tariff_Details_Periods_Save",
defaults: new { controller = "WH_CSL_Tariffs", action = "WH_CSL_Tariff_Details_Periods_Save" }
);
            config.Routes.MapHttpRoute(
name: "WH_CSL_Tariff_Details_Periods_DeleteList",
routeTemplate: "api/WH_CSL_Tariffs/WH_CSL_Tariff_Details_Periods_DeleteList",
defaults: new { controller = "WH_CSL_Tariffs", action = "WH_CSL_Tariff_Details_Periods_DeleteList" }
);
            #endregion WH_CSL_Tariff


            #endregion

            #region Container Yard

            #region WH_MTY_Tariff
            config.Routes.MapHttpRoute(
                name: "WH_MTY_Tariff_LoadAll",
                routeTemplate: "api/WH_MTY_Tariffs/WH_MTY_Tariff_LoadAll",
                defaults: new { controller = "WH_MTY_Tariffs", action = "WH_MTY_Tariff_LoadAll" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_MTY_Tariff_LoadItem",
                routeTemplate: "api/WH_MTY_Tariffs/WH_MTY_Tariff_LoadItem",
                defaults: new { controller = "WH_MTY_Tariffs", action = "WH_MTY_Tariff_LoadItem" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_MTY_Tariffs_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned",
                routeTemplate: "api/WH_MTY_Tariffs/WH_MTY_Tariffs_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned",
                defaults: new { controller = "WH_MTY_Tariffs", action = "WH_MTY_Tariffs_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_MTY_Tariff_Save",
                routeTemplate: "api/WH_MTY_Tariffs/WH_MTY_Tariff_Save",
                defaults: new { controller = "WH_MTY_Tariffs", action = "WH_MTY_Tariff_Save" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_MTY_Tariff_DeleteList",
                routeTemplate: "api/WH_MTY_Tariffs/WH_MTY_Tariff_DeleteList",
                defaults: new { controller = "WH_MTY_Tariffs", action = "WH_MTY_Tariff_DeleteList" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_MTY_Tariff_Details_LoadItem",
                routeTemplate: "api/WH_MTY_Tariffs/WH_MTY_Tariff_Details_LoadItem",
                defaults: new { controller = "WH_MTY_Tariffs", action = "WH_MTY_Tariff_Details_LoadItem" }
                );
            config.Routes.MapHttpRoute(
name: "GetCalcTypesIDMTY",
routeTemplate: "api/WH_MTY_Tariffs/GetCalcTypesID",
defaults: new { controller = "WH_MTY_Tariffs", action = "GetCalcTypesID" }
);
            config.Routes.MapHttpRoute(
                name: "WH_MTY_Tariff_Details_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned",
                routeTemplate: "api/WH_MTY_Tariffs/WH_MTY_Tariff_Details_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned",
                defaults: new { controller = "WH_MTY_Tariffs", action = "WH_MTY_Tariff_Details_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_MTY_Tariff_Details_Save",
                routeTemplate: "api/WH_MTY_Tariffs/WH_MTY_Tariff_Details_Save",
                defaults: new { controller = "WH_MTY_Tariffs", action = "WH_MTY_Tariff_Details_Save" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_MTY_Tariff_Details_DeleteList",
                routeTemplate: "api/WH_MTY_Tariffs/WH_MTY_Tariff_Details_DeleteList",
                defaults: new { controller = "WH_MTY_Tariffs", action = "WH_MTY_Tariff_Details_DeleteList" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_MTY_Tariff_Details_Periods_LoadItem",
                routeTemplate: "api/WH_MTY_Tariffs/WH_MTY_Tariff_Details_Periods_LoadItem",
                defaults: new { controller = "WH_MTY_Tariffs", action = "WH_MTY_Tariff_Details_Periods_LoadItem" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_MTY_Tariff_Details_Periods_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned",
                routeTemplate: "api/WH_MTY_Tariffs/WH_MTY_Tariff_Details_Periods_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned",
                defaults: new { controller = "WH_MTY_Tariffs", action = "WH_MTY_Tariff_Details_Periods_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_MTY_Tariff_Details_Periods_Save",
                routeTemplate: "api/WH_MTY_Tariffs/WH_MTY_Tariff_Details_Periods_Save",
                defaults: new { controller = "WH_MTY_Tariffs", action = "WH_MTY_Tariff_Details_Periods_Save" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_MTY_Tariff_Details_Periods_DeleteList",
                routeTemplate: "api/WH_MTY_Tariffs/WH_MTY_Tariff_Details_Periods_DeleteList",
                defaults: new { controller = "WH_MTY_Tariffs", action = "WH_MTY_Tariff_Details_Periods_DeleteList" }
                );
            #endregion WH_MTY_Tariff

            #region WH_CntrStock
            config.Routes.MapHttpRoute(
                name: "WH_CntrStock_LoadAll",
                routeTemplate: "api/WH_CntrStocks/WH_CntrStock_LoadAll",
                defaults: new { controller = "WH_CntrStocks", action = "WH_CntrStock_LoadAll" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_CntrStock_LoadItem",
                routeTemplate: "api/WH_CntrStocks/WH_CntrStock_LoadItem",
                defaults: new { controller = "WH_CntrStocks", action = "WH_CntrStock_LoadItem" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_CntrStocks_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned",
                routeTemplate: "api/WH_CntrStock/WH_CntrStocks_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned",
                defaults: new { controller = "WH_CntrStocks", action = "WH_CntrStocks_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_CntrStock_Save",
                routeTemplate: "api/WH_CntrStocks/WH_CntrStock_Save",
                defaults: new { controller = "WH_CntrStocks", action = "WH_CntrStock_Save" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_CntrStock_DeleteList",
                routeTemplate: "api/WH_CntrStocks/WH_CntrStock_DeleteList",
                defaults: new { controller = "WH_CntrStocks", action = "WH_CntrStock_DeleteList" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_Hire_LoadAll",
                routeTemplate: "api/WH_CntrStocks/WH_Hire_LoadAll",
                defaults: new { controller = "WH_CntrStocks", action = "WH_Hire_LoadAll" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_Hire_LoadItem",
                routeTemplate: "api/WH_CntrStocks/WH_Hire_LoadItem",
                defaults: new { controller = "WH_CntrStocks", action = "WH_Hire_LoadItem" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_Hire_Save",
                routeTemplate: "api/WH_CntrStocks/WH_Hire_Save",
                defaults: new { controller = "WH_CntrStocks", action = "WH_Hire_Save" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_Hire_DeleteList",
                routeTemplate: "api/WH_CntrStocks/WH_Hire_DeleteList",
                defaults: new { controller = "WH_CntrStocks", action = "WH_Hire_DeleteList" }
                );
            config.Routes.MapHttpRoute(
                name: "GetWH_CntrStockID",
                routeTemplate: "api/WH_CntrStocks/GetWH_CntrStockID",
                defaults: new { controller = "WH_CntrStocks", action = "GetWH_CntrStockID" }
                );
            #endregion
            #region WH_MTY_GateIn
            config.Routes.MapHttpRoute(
                name: "WH_MTY_GateIn_LoadAll",
                routeTemplate: "api/WH_MTY_GateIn/WH_MTY_GateIn_LoadAll",
                defaults: new { controller = "WH_MTY_GateIn", action = "WH_MTY_GateIn_LoadAll" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_MTY_GateIn_LoadItem",
                routeTemplate: "api/WH_MTY_GateIn/WH_MTY_GateIn_LoadItem",
                defaults: new { controller = "WH_MTY_GateIn", action = "WH_MTY_GateIn_LoadItem" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_MTY_GateIn_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned",
                routeTemplate: "api/WH_MTY_GateIn/WH_MTY_GateIn_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned",
                defaults: new { controller = "WH_MTY_GateIn", action = "WH_MTY_GateIn_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_MTY_GateIn_Save",
                routeTemplate: "api/WH_MTY_GateIn/WH_MTY_GateIn_Save",
                defaults: new { controller = "WH_MTY_GateIn", action = "WH_MTY_GateIn_Save" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_MTY_GateIn_DeleteList",
                routeTemplate: "api/WH_MTY_GateIn/WH_MTY_GateIn_DeleteList",
                defaults: new { controller = "WH_MTY_GateIn", action = "WH_MTY_GateIn_DeleteList" }
                );
            config.Routes.MapHttpRoute(
                name: "GetWarehouseAreas",
                routeTemplate: "api/WH_MTY_GateIn/GetWarehouseAreas",
                defaults: new { controller = "WH_MTY_GateIn", action = "GetWarehouseAreas" }
                );
            config.Routes.MapHttpRoute(
                name: "GetAreaRows",
                routeTemplate: "api/WH_MTY_GateIn/GetAreaRows",
                defaults: new { controller = "WH_MTY_GateIn", action = "GetAreaRows" }
                );
            config.Routes.MapHttpRoute(
                name: "GetRowLocations",
                routeTemplate: "api/WH_MTY_GateIn/GetRowLocations",
                defaults: new { controller = "WH_MTY_GateIn", action = "GetRowLocations" }
                );
            #endregion
            #region WH_MTY_Inventory
            config.Routes.MapHttpRoute(
                name: "WH_MTY_Inventory_LoadAll",
                routeTemplate: "api/WH_MTY_Inventory/WH_MTY_Inventory_LoadAll",
                defaults: new { controller = "WH_MTY_Inventory", action = "WH_MTY_Inventory_LoadAll" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_MTY_Inventory_LoadItem",
                routeTemplate: "api/WH_MTY_Inventory/WH_MTY_Inventory_LoadItem",
                defaults: new { controller = "WH_MTY_Inventory", action = "WH_MTY_Inventory_LoadItem" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_MTY_Inventory_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned",
                routeTemplate: "api/WH_MTY_Inventory/WH_MTY_Inventory_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned",
                defaults: new { controller = "WH_MTY_Inventory", action = "WH_MTY_Inventory_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_MTY_Inventory_Save",
                routeTemplate: "api/WH_MTY_Inventory/WH_MTY_Inventory_Save",
                defaults: new { controller = "WH_MTY_Inventory", action = "WH_MTY_Inventory_Save" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_MTY_Inventory_DeleteList",
                routeTemplate: "api/WH_MTY_Inventory/WH_MTY_Inventory_DeleteList",
                defaults: new { controller = "WH_MTY_Inventory", action = "WH_MTY_Inventory_DeleteList" }
                );
            config.Routes.MapHttpRoute(
                name: "GetWarehouseAreasI",
                routeTemplate: "api/WH_MTY_Inventory/GetWarehouseAreas",
                defaults: new { controller = "WH_MTY_Inventory", action = "GetWarehouseAreas" }
                );
            config.Routes.MapHttpRoute(
                name: "GetAreaRowsI",
                routeTemplate: "api/WH_MTY_Inventory/GetAreaRows",
                defaults: new { controller = "WH_MTY_Inventory", action = "GetAreaRows" }
                );
            config.Routes.MapHttpRoute(
                name: "GetRowLocationsI",
                routeTemplate: "api/WH_MTY_Inventory/GetRowLocations",
                defaults: new { controller = "WH_MTY_Inventory", action = "GetRowLocations" }
                );
            config.Routes.MapHttpRoute(
                name: "CalculateInvItms",
                routeTemplate: "api/WH_MTY_Inventory/CalculateInvItms",
                defaults: new { controller = "WH_MTY_Inventory", action = "CalculateInvItms" }
                );
            #endregion
            #region WH_MTY_GateOut
            config.Routes.MapHttpRoute(
                name: "WH_MTY_GateOut_LoadAll",
                routeTemplate: "api/WH_MTY_GateOut/WH_MTY_GateOut_LoadAll",
                defaults: new { controller = "WH_MTY_GateOut", action = "WH_MTY_GateOut_LoadAll" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_MTY_GateOut_LoadItem",
                routeTemplate: "api/WH_MTY_GateOut/WH_MTY_GateOut_LoadItem",
                defaults: new { controller = "WH_MTY_GateOut", action = "WH_MTY_GateOut_LoadItem" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_MTY_GateOut_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned",
                routeTemplate: "api/WH_MTY_GateOut/WH_MTY_GateOut_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned",
                defaults: new { controller = "WH_MTY_GateOut", action = "WH_MTY_GateOut_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_MTY_GateOut_Save",
                routeTemplate: "api/WH_MTY_GateOut/WH_MTY_GateOut_Save",
                defaults: new { controller = "WH_MTY_GateOut", action = "WH_MTY_GateOut_Save" }
                );
            config.Routes.MapHttpRoute(
                name: "WH_MTY_GateOut_DeleteList",
                routeTemplate: "api/WH_MTY_GateOut/WH_MTY_GateOut_DeleteList",
                defaults: new { controller = "WH_MTY_GateOut", action = "WH_MTY_GateOut_DeleteList" }
                );
            config.Routes.MapHttpRoute(
                name: "GetWarehouseAreasOut",
                routeTemplate: "api/WH_MTY_GateOut/GetWarehouseAreas",
                defaults: new { controller = "WH_MTY_GateOut", action = "GetWarehouseAreas" }
                );
            config.Routes.MapHttpRoute(
                name: "GetAreaRowsOut",
                routeTemplate: "api/WH_MTY_GateOut/GetAreaRows",
                defaults: new { controller = "WH_MTY_GateOut", action = "GetAreaRows" }
                );
            config.Routes.MapHttpRoute(
                name: "GetRowLocationsOut",
                routeTemplate: "api/WH_MTY_GateOut/GetRowLocations",
                defaults: new { controller = "WH_MTY_GateOut", action = "GetRowLocations" }
                );
            config.Routes.MapHttpRoute(
                name: "GetCntrInfoOut",
                routeTemplate: "api/WH_MTY_GateOut/GetCntrInfo",
                defaults: new { controller = "WH_MTY_GateOut", action = "GetCntrInfo" }
                );
            #endregion
            #endregion
            #region

            #endregion


            //        config.Routes.MapHttpRoute(
            //name: "SendTestEmail",
            //routeTemplate: "api/Defaults/EmailSending",
            //defaults: new { controller = "Defaults", action = "EmailSending" }
            //);

            config.Routes.MapHttpRoute(
                name: "UpdateDefaults",
                routeTemplate: "api/Defaults/Update",
                defaults: new { controller = "Defaults", action = "Update" }
                );

            config.Routes.MapHttpRoute(
                name: "SendTestEmailDefaults",
                routeTemplate: "api/Defaults/SendTestEmail",
                defaults: new { controller = "Defaults", action = "SendTestEmail" }
                );

            config.Routes.MapHttpRoute(
                name: "SendPDFEmailDefaults",
                routeTemplate: "api/Defaults/SendPDFEmail",
                defaults: new { controller = "Defaults", action = "SendPDFEmail" }
                );

            config.Routes.MapHttpRoute(
                name: "SendPDFEmailWithBodyTemplateDefaults",
                routeTemplate: "api/Defaults/SendPDFEmailWithBodyTemplate",
                defaults: new { controller = "Defaults", action = "SendPDFEmailWithBodyTemplate" }
                );
            config.Routes.MapHttpRoute(
    name: "SendUrlEmail_General",
    routeTemplate: "api/Defaults/SendUrlEmail_General",
    defaults: new { controller = "Defaults", action = "SendUrlEmail_General" }
    );
            config.Routes.MapHttpRoute(
                name: "SendPDFEmail_General",
                routeTemplate: "api/Defaults/SendPDFEmail_General",
                defaults: new { controller = "Defaults", action = "SendPDFEmail_General" }
                );

            config.Routes.MapHttpRoute(
                name: "SendEmailWithAttachment",
                routeTemplate: "api/Defaults/SendEmailWithAttachment",
                defaults: new { controller = "Defaults", action = "SendEmailWithAttachment" }
                );

            config.Routes.MapHttpRoute(
    name: "LicenseExpireDateAlarmsUsers_UpdateLicenseExpireDateAlarmsUsers",
    routeTemplate: "api/LicenseExpireDateAlarmsUsers/UpdateLicenseExpireDateAlarmsUsers",
    defaults: new { controller = "LicenseExpireDateAlarmsUsers", action = "UpdateLicenseExpireDateAlarmsUsers" }
    );
            config.Routes.MapHttpRoute(
name: "LicenseExpireDateAlarmsUsers_GetUsers",
routeTemplate: "api/LicenseExpireDateAlarmsUsers/GetUsers",
defaults: new { controller = "LicenseExpireDateAlarmsUsers", action = "GetUsers" }
);
            config.Routes.MapHttpRoute(
name: "LicenseExpireDateAlarmsUsers_SendEmailAndAlarmForLicenseExpireDates",
routeTemplate: "api/LicenseExpireDateAlarmsUsers/SendEmailAndAlarmForLicenseExpireDates",
defaults: new { controller = "LicenseExpireDateAlarmsUsers", action = "SendEmailAndAlarmForLicenseExpireDates" }
);
            #region InterServices
            config.Routes.MapHttpRoute(
               name: "InterServicesRequests_LoadChargeTypes",
               routeTemplate: "api/InterServicesRequests/LoadChargeTypes",
               defaults: new { controller = "InterServicesRequests", action = "LoadChargeTypes", pWhereClause = RouteParameter.Optional }
               );
            config.Routes.MapHttpRoute(
              name: "InterServicesRequests_LoadAllOperations",
              routeTemplate: "api/InterServicesRequests/LoadAllOperations",
              defaults: new { controller = "InterServicesRequests", action = "LoadAllOperations", pWhereClause = RouteParameter.Optional }
              );
            config.Routes.MapHttpRoute(
                name: "InterServicesRequests_LoadAll",
                routeTemplate: "api/InterServicesRequests/LoadAll",
                defaults: new { controller = "InterServicesRequests", action = "LoadAll", pWhereClause = RouteParameter.Optional }
                );
            #endregion

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
