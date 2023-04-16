//1: Excel File
//2: Excel File Without Formatting
//3: PDF File
//4: Rich Text Format Document
//5: Word Document
function ProfitStatistics_Initialize() {
    LoadView("/Reports/ProfitStatistics", "div-content", function () {
        if (pDefaults.UnEditableCompanyName == "ELI")
            $(".classShowForELI").removeClass("hide");
        else if (pDefaults.UnEditableCompanyName == "ELC")
            $(".classShowForELC").removeClass("hide");
        CallGETFunctionWithParameters("/api/ProfitStatistics/GetStatisticsFilter", null
            , function (data) {
                //data[0]:Salesman //data[1]:Branches //data[2]:Customers //data[3]:Currencies //data[4]:Oper.States
                debugger;
                //FillListFromObject(pID, pCodeOrName/*1-Code 2-Name*/, pStrFirstRow, pSlName, pData, callback)
                FillListFromObject(null, 2, "<--All-->", "slSalesman", data[0], null);
                FillListFromObject(null, 2, "<--All-->", "slBranch", data[1], null);
                //FillListFromObject(null, 2, "All Customers", "slCustomer", data[2], null);
                if (pDefaults.UnEditableCompanyName == "GBL")
                    CallGETFunctionWithParameters("/api/Customers/LoadAllWithMinimalColumns"
                        , { pWhereClauseWithMinimalColumns: "WHERE  1=1 ", pOrderBy: "Name" }
                        , function (pData) {
                            FillListFromObject(null, 2, TranslateString("SelectFromMenu"), "slCustomer", pData[0], null);
                            //Fill_SelectInputAfterLoadData_WithMultiAttrAndMultiFields(pData[0], "ID", "Name", '', TranslateString("SelectFromMenu"), "#hReadySlCustomers", null, "IsInactive", null);
                        }
                    , null);
                else
                    $("#slCustomer").html($("#hReadySlCustomers").html());
                //FillListFromObject(null, 2, "All Booking Parties", "slBookingParty", data[2], null);
                $("#slBookingParty").html($("#hReadySlCustomers").html());
                FillListFromObject(null, 1, "<--All-->", "slCurrency", data[3], null);
                FillListFromObject(null, 2, "<--All-->", "slOperationStages", data[4], null);
                //FillListFromObject(null, 1, "All Operations", "slOperation", data[5], null);
                FillListFromObject(null, 2, "<--All-->", "slMoveType", data[6], null);
                 FillListFromObject(null, 2, "<--All-->", "slChargeType", data[7], null);
                FillDivWithCheckboxes_DynamicWithMultiFields("divCbChargesTypes", data[7], "nameCbChargesTypes", "Name", null);
                FillListFromObject(null, 2, "<--All-->", "slInvoiceType", data[8], null);
                FillListFromObject(null, 2, "<--All-->", "slAgent", data[9], null);
                FillListFromObject(null, 2, "<--All-->", "slPartnerType", data[11], null);
                FillListFromObject(null, 2, "<--All-->", "slShippingLines", data[12], null);
                FillListFromObject(null, 2, "<--All-->", "slCountryPOL", data[10], function () { $("#slCountryPOD").html($("#slCountryPOL").html()); });
                $("#txtFromOpenDate").val(getTodaysDateInddMMyyyyFormat());
                $("#txtToOpenDate").val(getTodaysDateInddMMyyyyFormat());
                $("#txtFromInvoiceDate").val("01/01/2000");
                $("#txtToInvoiceDate").val("01/01/2030");
                $("#txtFromCloseDate").val(getTodaysDateInddMMyyyyFormat());
                $("#txtToCloseDate").val(getTodaysDateInddMMyyyyFormat());
                $("#txtFromDateSelectOperations").val(getTodaysDateInddMMyyyyFormat());
                $("#txtToDateSelectOperations").val(getTodaysDateInddMMyyyyFormat());
            }
            , function () { FadePageCover(false); $("#hl-menu-Reports").parent().addClass("active"); });
        //parameters (pStrFnName, pStrFirstRow, pListName, pWhereClause)
        //FillListWithNames("/api/NoAccessQuoteAndOperStages/LoadAll", "ALL STATES", "ulOperationStages", " WHERE IsOperationStage = 1 AND IsInActive = 0 ORDER BY ViewOrder ");
        ////FillListWithNames("/api/NoAccessReportTypes/LoadAll", "Select Report Type", "ulReportTypes", " ORDER BY ViewOrder ");
        //FillListWithNamesWithoutFirstRow(constPdfReportTypeID, "/api/NoAccessReportTypes/LoadAll", "ulReportTypes", " ORDER BY ViewOrder ");
    });
}
function ProfitStatistics_Print(pOutputTo) {
    debugger;
    if (
        ($("#txtFromOpenDate").val().trim() == "" || isValidDate($("#txtFromOpenDate").val(), 1))
        && ($("#txtToOpenDate").val().trim() == "" || isValidDate($("#txtToOpenDate").val(), 1))
        && ($("#txtFromCloseDate").val().trim() == "" || isValidDate($("#txtFromCloseDate").val(), 1))
        && ($("#txtToCloseDate").val().trim() == "" || isValidDate($("#txtToCloseDate").val(), 1))
    ) {
        FadePageCover(true);
        var pWhereClause = ProfitStatistics_GetFilterWhereClause();
        //if ($('#ulReportTypes .active').val() == 0)
        //    swal(strSorry, "Please, Select a report type.");
        //else {
        var pParametersWithValues = {
            pWhereClause: pWhereClause
            , pCurrencyID: ($("#slCurrency").val() == "" ? 0 : $("#slCurrency").val())
            , pPartnerTypeID: ($("#slPartnerType").val() == "" ? 0 : $("#slPartnerType").val())
            , pSupplierID: ($("#slPartner").val() == "" ? 0 : $("#slPartner").val())
            , pSupplierInvoiceNumber: ($("#txtSupplierInvoiceNumber").val().trim() == "" ? 0 : $("#txtSupplierInvoiceNumber").val().trim())
            , pIncludeCurrenciesDetails: $("#cbIncludeCurrenciesDetails").prop("checked")
            , pIncludeChargeDetails: $("#cbIncludeChargeDetails").prop("checked")
            , pGroupByOperations: $("#cbGroupByOperations").prop("checked")
            , pChargeTypeID: $("#slChargeType").val() == "" ? 0 : $("#slChargeType").val()
            , pWithVAT: $("#cbWithVAT").prop("checked")
            , pIsUsedInOperationStatement: $("#cbIsUsedInOperationStatement").prop("checked") || pDefaults.UnEditableCompanyName == "ELI" || pDefaults.UnEditableCompanyName == "ELC" ? true : false
            , pIsMarginRegardingRevenue: $("#cbIsMarginRegardingRevenue").prop("checked")
            , pIsOfficial: $("#cbIsOfficial").prop("checked")
            , pOption:// pOutputTo != "Excel"  && 
                ($("#cbIncludeChargeDetails").prop("checked"))
                    && (pDefaults.UnEditableCompanyName == "NIL" || pDefaults.UnEditableCompanyName == "EXP" || pDefaults.UnEditableCompanyName == "KDS")
                    ? 2 //With Invoice Nos 
                    : 1 //Without InvNos
            //, pDirectionType: ($("#lbl-filter-import").hasClass('active') ? "Import"
            //    : $("#lbl-filter-export").hasClass('active') ? "Export"
            //    : $("#lbl-filter-domestic").hasClass('active') ? "Domestic"
            //    : "ALL")
            //, pTransPortType: ($("#lbl-filter-ocean").hasClass('active') ? "Ocean"
            //    : $("#lbl-filter-air").hasClass('active') ? "Air"
            //    : $("#lbl-filter-inland").hasClass('active') ? "Inland"
            //    : "ALL")
        };
        CallGETFunctionWithParameters("/api/ProfitStatistics/LoadData"
            , pParametersWithValues
            , function (data) {
                if (!data[0]) //pRecordsExist doesn't exist
                    swal(strSorry, "Please, Try another search criteria.");
                else if (data[0] && $("#cbIncludeCurrenciesDetails").prop("checked"))
                    ProfitStatistics_DrawReport(data, pOutputTo);
                else if (data[0] && $("#cbIncludeChargeDetails").prop("checked"))
                    ProfitStatistics_DrawReport_WithChargeDetails(data, pOutputTo);
                else if (data[0] && $("#cbGroupByOperations").prop("checked") && pOutputTo == "Excel")
                    ProfitStatistics_DrawReport_GroupByOperations_Excel(data, pOutputTo)
                else if (data[0] && $("#cbGroupByOperations").prop("checked") && pOutputTo != "Excel")
                    ProfitStatistics_DrawReport_GroupByOperations(data, pOutputTo);
                FadePageCover(false);
            });
        //}
    }
    else
        swal(strSorry, "Please make sure that date format is dd/MM/yyyy.");
}
function ProfitStatistics_GetFilterWhereClause() {
    var pWhereClause = "WHERE 1=1" + " \n";
    var pTransportFilter = "";
    var pDirectionFilter = "";
    var pBLTypeFilter = "";
    var pSalesmanFilter = "";
    var pBranchFilter = "";
    var pMoveTypeFilter = "";
    var pChargeTypeFilter = "";
    var pInvoiceTypeFilter = "";
    var pOperationIDsFilter = "";
    var pCustomerFilter = "";
    var pAgentFilter = "";
    var pBookingPartyFilter = "";
    var pChargesTypes = "";
    var pCountryPOLFilter = "";
    var pPOLFilter = "";
    var pCountryPODFilter = "";
    var pPODFilter = "";

    var pFromOpenDateFilter = "";
    var pToOpenDateFilter = "";
    var pFromInvoiceDateFilter = "";
    var pToInvoiceDateFilter = "";
    var pFromCloseDateFilter = "";
    var pToCloseDateFilter = "";
    var pPOL = "";
    var pPOD = "";
    var pShippingLine = "";
    //var pOperationStageFilter = ($("#ulOperationStages li[class=active]").val() == 0 ? "" : " ( OperationStageID = " + $("#ulOperationStages li[class=active]").val() + ")"); //if 0 then all stages
    var pOperationStageFilter = ($("#slOperationStages").val() == 0
        ? "" //if 0 then all stages
        : ($("#slOperationStages").val() == ClosedQuoteAndOperStageID.toString() ? (" (CAST(CloseDate AS date) <= GETDATE() AND OperationStageID <> " + /*This is to handle case of auto close*/CancelledQuoteAndOperStageID.toString() + ") ") : (" OperationStageID = " + $("#slOperationStages").val() + " AND CloseDate > GETDATE() "))
    );

    pDirectionFilter += ($("#lbl-filter-import").hasClass('active') ? (pDirectionFilter == "" ? " (DirectionType = 1 " : " OR DirectionType = 1 ") : "");
    pDirectionFilter += ($("#lbl-filter-export").hasClass('active') ? (pDirectionFilter == "" ? " (DirectionType = 2 " : " OR DirectionType = 2 ") : "");
    pDirectionFilter += ($("#lbl-filter-domestic").hasClass('active') ? (pDirectionFilter == "" ? " (DirectionType = 3 " : " OR DirectionType = 3 ") : "");
    pDirectionFilter += ($("#lbl-filter-crossbooking").hasClass('active') ? (pDirectionFilter == "" ? " (DirectionType = 4 " : " OR DirectionType = 4 ") : "");
    pDirectionFilter += ($("#lbl-filter-reexport").hasClass('active') ? (pDirectionFilter == "" ? " (DirectionType = 5 " : " OR DirectionType = 4 ") : "");
    pDirectionFilter += (pDirectionFilter == "" ? "" : ") ");

    pTransportFilter += ($("#lbl-filter-ocean").hasClass('active') ? (pTransportFilter == "" ? " (TransportType = 1 " : " OR TransportType = 1 ") : "");
    pTransportFilter += ($("#lbl-filter-air").hasClass('active') ? (pTransportFilter == "" ? " (TransportType = 2 " : " OR TransportType = 2 ") : "");
    pTransportFilter += ($("#lbl-filter-inland").hasClass('active') ? (pTransportFilter == "" ? " (TransportType = 3 " : " OR TransportType = 3 ") : "");
    pTransportFilter += (pTransportFilter == "" ? "" : ") ");

    pBLTypeFilter += ($("#lbl-filter-direct").hasClass('active') ? (pBLTypeFilter == "" ? " (BLType = " + constDirectBLType : " OR BLType = " + constDirectBLType) : "");
    pBLTypeFilter += ($("#lbl-filter-house").hasClass('active') ? (pBLTypeFilter == "" ? " (BLType = " + constHouseBLType : " OR BLType = " + constHouseBLType) : "");
    pBLTypeFilter += ($("#lbl-filter-master").hasClass('active') ? (pBLTypeFilter == "" ? " (BLType = " + constMasterBLType : " OR BLType = " + constMasterBLType) : "");
    pBLTypeFilter += (pBLTypeFilter == "" ? "" : ") ");

    if (pDirectionFilter != "" && pTransportFilter == "")
        pWhereClause = " WHERE " + pDirectionFilter;
    else
        if (pDirectionFilter == "" && pTransportFilter != "")
            pWhereClause = " WHERE " + pTransportFilter;
        else
            if (pDirectionFilter != "" && pTransportFilter != "")
                pWhereClause = " WHERE " + pDirectionFilter + " AND " + pTransportFilter;

    if (pBLTypeFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pBLTypeFilter;
    else
        if (pBLTypeFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pBLTypeFilter;

    if (pOperationStageFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pOperationStageFilter;
    else
        if (pOperationStageFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pOperationStageFilter;

    pBranchFilter = ($("#slBranch").val() == "" ? "" : " BranchID = " + $("#slBranch").val());
    if (pBranchFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pBranchFilter;
    else
        if (pBranchFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pBranchFilter;

    pMoveTypeFilter = ($("#slMoveType").val() == "" ? "" : " MoveTypeID = " + $("#slMoveType").val());
    if (pMoveTypeFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pMoveTypeFilter;
    else
        if (pMoveTypeFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pMoveTypeFilter;

    ////This will get all records on any operation that has the selected invoice type
    //pInvoiceTypeFilter = ($("#slInvoiceType").val() == "" ? "" : " InvoiceNumbers LIKE '%" + $("#slInvoiceType option:selected").text() + "%'");
    //if (pInvoiceTypeFilter != "" && pWhereClause != "")
    //    pWhereClause += " AND " + pInvoiceTypeFilter;
    //else
    //    if (pInvoiceTypeFilter != "" && pWhereClause == "")
    //        pWhereClause += " WHERE " + pInvoiceTypeFilter;

    //pOperationIDsFilter = ($("#slOperation").val() == "" ? "" : " ID = " + $("#slOperation").val());
    //if (pOperationIDsFilter != "" && pWhereClause != "")
    //    pWhereClause += " AND " + pOperationIDsFilter;
    //else
    //    if (pOperationIDsFilter != "" && pWhereClause == "")
    //        pWhereClause += " WHERE " + pOperationIDsFilter;
    var pModalName = "CheckboxesListModal";
    var pCheckboxNameAttr = "cbAddedItemID";
    var pSelectedItemsIDs = GetAllSelectedIDsAsStringWithNameAttr(pCheckboxNameAttr);
    pOperationIDsFilter = (pSelectedItemsIDs == "" ? "" : " ID IN (" + pSelectedItemsIDs + ")");
    if (pOperationIDsFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pOperationIDsFilter;
    else
        if (pOperationIDsFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pOperationIDsFilter;

    pSalesmanFilter = ($("#slSalesman").val() == "" ? "" : " SalesmanID = " + $("#slSalesman").val());
    if (pSalesmanFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pSalesmanFilter;
    else
        if (pSalesmanFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pSalesmanFilter;

    pCustomerFilter = ($("#slCustomer").val() == "" ? "" : " ClientID = " + $("#slCustomer").val());
    if (pCustomerFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pCustomerFilter;
    else
        if (pCustomerFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pCustomerFilter;

    


    //var cbChargesTypes = IsNull(($("#cbCheckAllChargesTypes").prop("checked") == true ? "-1" : "" + GetAllSelectedIDsAsStringWithNameAttr("nameCbChargesTypes") + ""), "-1");
    //console.log('cbChargesTypes : ' + cbChargesTypes);
    //pChargeTypeFilter = (cbChargesTypes == "-1" ? " -1 = -1 " : " ChargeTypeID in(" + cbChargesTypes + ")");
    //if (pChargeTypeFilter != "" && pWhereClause != "")
    //    pWhereClause += " AND " + pChargeTypeFilter;
    //else
    //    if (pChargeTypeFilter != "" && pWhereClause == "")
    //        pWhereClause += " WHERE " + pChargeTypeFilter;
    


    pAgentFilter = ($("#slAgent").val() == "" ? "" : " AgentID = " + $("#slAgent").val());
    if (pAgentFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pAgentFilter;
    else
        if (pAgentFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pAgentFilter;




    //pShippingLine = ($("#slShippingLines").val() == "" ? "" : " (SupplierPartnerTypeID = 5 and PartnerSupplierID = " + $("#slShippingLines").val() + ")");
    pShippingLine = ($("#slShippingLines").val() == "" ? "" : " (LineName = N'" + $("#slShippingLines option:selected").text() + "')");
    if (pShippingLine != "" && pWhereClause != "")
        pWhereClause += " AND " + pShippingLine;
    else
        if (pShippingLine != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pShippingLine;



    //if ($("#slPartner").val() != "")
    //    pWhereClause += " AND ((ISNULL((SELECT COUNT(op.ID) FROM dbo.OperationPartners AS op WHERE dbo.vwOperations.ID = op.OperationID AND op.SupplierID = " + $("#slSupplier").val() + "), 0)) > 0)";

    pBookingPartyFilter = ($("#slBookingParty").val() == "" ? "" : " BookingPartyID = " + $("#slBookingParty").val());
    if (pBookingPartyFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pBookingPartyFilter;
    else
        if (pBookingPartyFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pBookingPartyFilter;

    pCountryPOLFilter = ($("#slCountryPOL").val() == "" ? "" : " POLCountryID = " + $("#slCountryPOL").val());
    if (pCountryPOLFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pCountryPOLFilter;
    else
        if (pCountryPOLFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pCountryPOLFilter;
    pPOLFilter = ($("#slPOL").val() == "" ? "" : " POL = " + $("#slPOL").val());
    if (pPOLFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pPOLFilter;
    else
        if (pPOLFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pPOLFilter;

    pCountryPODFilter = ($("#slCountryPOD").val() == "" ? "" : " PODCountryID = " + $("#slCountryPOD").val());
    if (pCountryPODFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pCountryPODFilter;
    else
        if (pCountryPODFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pCountryPODFilter;
    pPODFilter = ($("#slPOD").val() == "" ? "" : " POD = " + $("#slPOD").val());
    if (pPODFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pPODFilter;
    else
        if (pPODFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pPODFilter;

    ////2nd parameter in isValidDate: 1-dd/mm/yyyy 2-mm/dd/yyyy
    //if (isValidDate($("#txtFromOpenDate").val().trim(), 1) && $("#txtFromOpenDate").val().trim() != ""
    //    && isValidDate($("#txtToOpenDate").val().trim(), 1) && $("#txtToOpenDate").val().trim() != ""
    //    )
    //    pWhereClause += " AND CAST(OpenDate AS DATE) BETWEEN '" + GetDateWithFormatyyyyMMdd($("#txtFromOpenDate").val().trim()) + "' AND '" + GetDateWithFormatyyyyMMdd($("#txtToOpenDate").val().trim()) + " 23:59:59' " + " \n";


    //2nd parameter in isValidDate: 1-dd/mm/yyyy 2-mm/dd/yyyy
    if (isValidDate($("#txtFromOpenDate").val().trim(), 1) && $("#txtFromOpenDate").val().trim() != ""
        && isValidDate($("#txtToOpenDate").val().trim(), 1) && $("#txtToOpenDate").val().trim() != "") {
        if ($("#cbFilterInvoiceDate").prop("checked")) //filter by invoice date
            pWhereClause += " AND vwOperations.ID IN (SELECT ISNULL(vwInvoices.MasterOperationID,vwInvoices.OperationID) FROM vwInvoices WHERE IsDeleted=0 AND CAST(vwInvoices.InvoiceDate AS DATE) BETWEEN '" + GetDateWithFormatyyyyMMdd($("#txtFromOpenDate").val().trim()) + "' AND '" + GetDateWithFormatyyyyMMdd($("#txtToOpenDate").val().trim()) + " 23:59:59') " + " \n";
        else //filter by OpenDate
            pWhereClause += " AND CAST(OpenDate AS DATE) BETWEEN '" + GetDateWithFormatyyyyMMdd($("#txtFromOpenDate").val().trim()) + "' AND '" + GetDateWithFormatyyyyMMdd($("#txtToOpenDate").val().trim()) + " 23:59:59' " + " \n";
    }

    if (pDefaults.UnEditableCompanyName == "ELI" || pDefaults.UnEditableCompanyName == "ELC") { //only elite sees the invoice date filter
        //2nd parameter in isValidDate: 1-dd/mm/yyyy 2-mm/dd/yyyy
        if (isValidDate($("#txtFromInvoiceDate").val().trim(), 1) && $("#txtFromInvoiceDate").val().trim() != "") {
            pFromInvoiceDateFilter = " FirstInvoiceDate >= '" + GetDateWithFormatyyyyMMdd($("#txtFromInvoiceDate").val().trim()) + "'";
            if (pFromInvoiceDateFilter != "" && pWhereClause != "")
                pWhereClause += " AND " + pFromInvoiceDateFilter;
            else
                if (pFromInvoiceDateFilter != "" && pWhereClause == "")
                    pWhereClause += " WHERE " + pFromInvoiceDateFilter;
        }
        //2nd parameter in isValidDate: 1-dd/mm/yyyy 2-mm/dd/yyyy
        if (isValidDate($("#txtToInvoiceDate").val().trim(), 1) && $("#txtToInvoiceDate").val().trim() != "") {
            pToInvoiceDateFilter = " CAST(FirstInvoiceDate AS date) <= '" + GetDateWithFormatyyyyMMdd($("#txtToInvoiceDate").val().trim()) + "'";
            if (pToInvoiceDateFilter != "" && pWhereClause != "")
                pWhereClause += " AND " + pToInvoiceDateFilter;
            else
                if (pToInvoiceDateFilter != "" && pWhereClause == "")
                    pWhereClause += " WHERE " + pToInvoiceDateFilter;
        }
    }
    if ($("#slOperationStages option:selected").text() == "CLOSED") {
        //2nd parameter in isValidDate: 1-dd/mm/yyyy 2-mm/dd/yyyy
        if (isValidDate($("#txtFromCloseDate").val().trim(), 1) && $("#txtFromCloseDate").val().trim() != "") {
            pFromCloseDateFilter = " CloseDate >= '" + GetDateWithFormatyyyyMMdd($("#txtFromCloseDate").val().trim()) + "'";
            if (pFromCloseDateFilter != "" && pWhereClause != "")
                pWhereClause += " AND " + pFromCloseDateFilter;
            else
                if (pFromCloseDateFilter != "" && pWhereClause == "")
                    pWhereClause += " WHERE " + pFromCloseDateFilter;
        }
        //2nd parameter in isValidDate: 1-dd/mm/yyyy 2-mm/dd/yyyy
        if (isValidDate($("#txtToCloseDate").val().trim(), 1) && $("#txtToCloseDate").val().trim() != "") {
            pToCloseDateFilter = " CAST(CloseDate AS date) <= '" + GetDateWithFormatyyyyMMdd($("#txtToCloseDate").val().trim()) + "'";
            if (pToCloseDateFilter != "" && pWhereClause != "")
                pWhereClause += " AND " + pToCloseDateFilter;
            else
                if (pToCloseDateFilter != "" && pWhereClause == "")
                    pWhereClause += " WHERE " + pToCloseDateFilter;
        }
    }

    pWhereClause = (pWhereClause == "" ? " WHERE BLType<>2 " : (pWhereClause + " AND BLType<>2 ")) + " ORDER BY ID ";
    return pWhereClause;
}
function ProfitStatistics_ClearPorts(pPortControlName) {
    debugger;
    //$("#" + pPortControlName).html("");
    $("#" + pPortControlName).html("<option value=''><--All--></option>");
}
function ProfitStatistics_RefereshPorts(pCountryControlName, pPortControlName) {
    debugger;
    FadePageCover(true);
    $("#" + pPortControlName).html("<option value=''><--Select--><option>");
    if ($("#" + pCountryControlName).val() == "") {
        FadePageCover(false);
    }
    else {
        CallGETFunctionWithParameters("/api/Ports/LoadAll"
            , { pWhereClause: "WHERE CountryID=" + $("#" + pCountryControlName).val() }
            , function (pData) {
                FillListFromObject(null, 2, TranslateString("SelectFromMenu"), pPortControlName, pData[0], null);
                FadePageCover(false);
            }
            , null);
    }
}
function ProfitStatistics_DrawReport(data, pOutputTo) {
    debugger;
    var pReportRows = JSON.parse(data[1]);
    var pPayablesCurrenciesSummary = data[2];
    var pReceivablesOrInvoicesCurrenciesSummary = data[3];
    var pProfitCurrenciesSummary = data[4];
    var pMarginSummary = data[5];

    var pReportTitle = "Profit Statistics";

    var TodaysDateddMMyyyy = getTodaysDateInddMMyyyyFormat();
    var pTablesHTML = "";
    //pTablesHTML += '                         <table id="tblProfitStatistics" class="table table-striped text-sm table-hover b-t b-r b-b b-l ">';//remove t1 class to remove row numbers
    pTablesHTML += '                         <table id="tblProfitStatistics" class="table table-striped text-sm table-bordered print " style="border:solid #999 !important;">';//remove t1 class to remove row numbers
    pTablesHTML += '                             <thead>';
    pTablesHTML += '                                 <tr class="" style="background-color:whitesmoke;font-size:95%;">';
    pTablesHTML += '                                     <th>Ser.</th>';
    pTablesHTML += '                                     <th>' + (pDefaults.UnEditableCompanyName == "SAF" ? 'Job No' : 'Operation Code') + '</th>';
    pTablesHTML += '                                     <th>Oper. Date</th>';
    pTablesHTML += '                                     <th>Client</th>';
    pTablesHTML += '                                     <th>Cargo</th>';
    if (pDefaults.UnEditableCompanyName == "BED")
        pTablesHTML += '                                     <th>Est.Cost</th>';
    pTablesHTML += '                                     <th>Pay.</th>';
    pTablesHTML += '                                     <th>' + ($("#cbIsUsedInOperationStatement").prop("checked") ? 'Inv.' : 'Rec.') + '</th>';
    pTablesHTML += '                                     <th>Cur.</th>';
    if ($("#cbIncludeCurrenciesDetails").prop("checked"))
        pTablesHTML += '                                 <th>Profit/Cur</th>';
    pTablesHTML += '                                     <th>Profit</th>';
    pTablesHTML += '                                     <th>Margin</th>';
    pTablesHTML += '                                 </tr>';
    pTablesHTML += '                             </thead>';
    pTablesHTML += '                             <tbody>';
    debugger;
    //$.each(JSON.parse(data[3]), function (i, item) { debugger; });
    var serial = 0;
    var _TempClientName = "";
    $.each((pReportRows), function (i, item) {
        if (item.ClientName != null)
            _TempClientName = item.ClientName;
        pTablesHTML += '                                     <tr class="' + (item.Cargo == "Totals In Default Currency:" ? "font-bold text-ul" : "") + '" style="font-size:95%; ' + (item.Cargo == "Total:" ? "background-color: #f9f9f9;!Important" : "") + '">' + (item.Code == "Total" ? "<b>" : "");
        pTablesHTML += '                                         <td>' + (item.Code == 0 || item.Code == null ? "" : ++serial) + '</td>';
        pTablesHTML += '                                         <td>' + (item.Code == 0 || item.Code == null ? "" : item.Code) + '</td>';
        pTablesHTML += '                                         <td>' + (item.OpenDate == 0 || item.OpenDate == null ? "" : (item.OpenDate + '\u2008')) + '</td>';
        //pTablesHTML += '                                         <td>' + (item.ClientName == 0 || item.ClientName == null ? "" : item.ClientName) + '</td>';
        pTablesHTML += '                                         <td>' + (item.ClientName == 0 || item.ClientName == null ? (pOutputTo == "Excel" ? _TempClientName : "") : item.ClientName) + '</td>';
        pTablesHTML += '                                         <td>' + (item.Cargo == 0 || item.Cargo == null ? "" : item.Cargo) + '</td>';
        if (pDefaults.UnEditableCompanyName == "BED")
            pTablesHTML += '                                         <td>' + (item.Code == null || !$("#cbIncludeCurrenciesDetails").prop("checked") ? item.QuotationCost : "") + '</td>';
        pTablesHTML += '                                         <td>' + (item.Code == null || !$("#cbIncludeCurrenciesDetails").prop("checked") ? item.Payables : "") + '</td>';
        pTablesHTML += '                                         <td>' + (item.Code == null || !$("#cbIncludeCurrenciesDetails").prop("checked") ? item.Invoices : "") + '</td>';
        pTablesHTML += '                                         <td>' + (item.Code == null || !$("#cbIncludeCurrenciesDetails").prop("checked") ? (item.Currency == null ? $("#hDefaultCurrencyCode").val() : item.Currency) : "") + '</td>';
        if ($("#cbIncludeCurrenciesDetails").prop("checked"))
            pTablesHTML += '                                     <td>' + (item.Code == null || !$("#cbIncludeCurrenciesDetails").prop("checked") ? item.ProfitPerCurrency : "") + '</td>';
        pTablesHTML += '                                         <td>' + (item.Code == null || !$("#cbIncludeCurrenciesDetails").prop("checked") ? item.Profit : "") + '</td>';
        pTablesHTML += '                                         <td>' + (item.Code == null || !$("#cbIncludeCurrenciesDetails").prop("checked") ? item.Margin + '%' : "") + '</td>';
        //pTablesHTML += '                                         <td>' + item.CostAmountSubTotal.toFixed(2) + '</td>';
        pTablesHTML += '                                     </tr>';
    });
    pTablesHTML += '                             </tbody>';
    pTablesHTML += '                         </table>';
    $("#hExportedTable").html(pTablesHTML); //in all cases i put the tables in hidden div so i select each table separately

    if (pOutputTo == "Excel") {
        /*********************Append table summaries*************************/
        var pTableSummary = "";
        pTableSummary += '                                     <tr>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        if (pDefaults.UnEditableCompanyName == "BED")
            pTableSummary += '                                         <td></td>';
        if ($("#cbIncludeCurrenciesDetails").prop("checked"))
            pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                     </tr>';
        pTableSummary += '                                     <tr>';
        pTableSummary += '                                         <td>Payables Summary :</td>';
        pTableSummary += '                                         <td>' + pPayablesCurrenciesSummary + '</td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        if (pDefaults.UnEditableCompanyName == "BED")
            pTableSummary += '                                         <td></td>';
        if ($("#cbIncludeCurrenciesDetails").prop("checked"))
            pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                     </tr>';
        pTableSummary += '                                     <tr>';
        pTableSummary += '                                         <td>' + ($("#cbIsUsedInOperationStatement").prop("checked") ? 'Invoices' : 'Receivables') + ' Summary :</td>';
        pTableSummary += '                                         <td>' + pReceivablesOrInvoicesCurrenciesSummary + '</td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        if (pDefaults.UnEditableCompanyName == "BED")
            pTableSummary += '                                         <td></td>';
        if ($("#cbIncludeCurrenciesDetails").prop("checked"))
            pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                     </tr>';
        pTableSummary += '                                     <tr>';
        pTableSummary += '                                         <td>Profit Summary :</td>';
        pTableSummary += '                                         <td>' + pProfitCurrenciesSummary + '</td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        if (pDefaults.UnEditableCompanyName == "BED")
            pTableSummary += '                                         <td></td>';
        if ($("#cbIncludeCurrenciesDetails").prop("checked"))
            pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                     </tr>';
        pTableSummary += '                                     <tr>'; 
        pTableSummary += '                                         <td>Profit Margin :</td>'; 
        pTableSummary += '                                         <td>' + pMarginSummary + '</td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        if (pDefaults.UnEditableCompanyName == "BED")
            pTableSummary += '                                         <td></td>';
        if ($("#cbIncludeCurrenciesDetails").prop("checked"))
            pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                     </tr>';
        $("#tblProfitStatistics" + " tbody").append(pTableSummary);
        ExportHtmlTableToCsv_RemovingCommasForNumbers("tblProfitStatistics", "Profit Statistics");
    }
    else {
        var mywindow = window.open('', '_blank');
        var ReportHTML = '';
        //<div style="width:500px;height:100px;border:1px solid #000;">This is a rectangle!</div>
        ReportHTML += '<html>';
        ReportHTML += '     <head><title>' + pReportTitle + '</title><link rel="stylesheet" href="/Content/CSS/app.v2.css" type="text/css" /></head>';
        ReportHTML += '         <body style="background-color:white;">';
        ReportHTML += '             <div class="col-xs-12 text-center"><img src="/Content/Images/CompanyHeader.jpg" alt="logo"/></div> </br>';
        ReportHTML += '             <div class="col-xs-12 text-center text-ul m-t-n"><h3>' + pReportTitle + '</h3></div> </br>';

        ReportHTML += '             <div class="col-xs-3"><b>Printed On :</b> ' + TodaysDateddMMyyyy + '</div>';
        ReportHTML += '             <div class="col-xs-3"><b>Transport Type :</b> ' + $("#divTransportFilter label.active").attr("title") + '</div>';
        ReportHTML += '             <div class="col-xs-3"><b>Direction Type :</b> ' + $("#divDirectionFilter label.active").attr("title") + '</div>';
        ReportHTML += '             <div class="col-xs-3"><b>B/L Type :</b> ' + $("#divBLTypeFilter label.active").attr("title") + '</div>';
        ReportHTML += '             <div class="col-xs-3"><b>Branch :</b> ' + $("#slBranch option:selected").text() + '</div>';
        ReportHTML += '             <div class="col-xs-3"><b>Salesman :</b> ' + $("#slSalesman option:selected").text() + '</div>';
        ReportHTML += '             <div class="col-xs-3"><b>Customer :</b> ' + $("#slCustomer option:selected").text() + '</div>';
        ReportHTML += '             <div class="col-xs-3"><b>Agent :</b> ' + $("#slAgent option:selected").text() + '</div>';
        //ReportHTML += '             <div class="col-xs-3"><b>Currency :</b> ' + $("#slCurrency option:selected").text() + '</div>';
        ReportHTML += '             <div class="col-xs-3"><b>Oper. Status :</b> ' + $("#slOperationStages option:selected").text() + '</div>';

        ReportHTML += '             <div class="col-xs-3"><b>POLCountry :</b> ' + $("#slCountryPOL option:selected").text() + '</div>';
        ReportHTML += '             <div class="col-xs-3"><b>POL :</b> ' + $("#slPOL option:selected").text() + '</div>';
        ReportHTML += '             <div class="col-xs-3"><b>PODCountry :</b> ' + $("#slCountryPOD option:selected").text() + '</div>';
        ReportHTML += '             <div class="col-xs-3"><b>POD :</b> ' + $("#slPOD option:selected").text() + '</div>';

        ReportHTML += '             <div class="col-xs-6"><b>Open Date :</b> ' + ($("#txtFromOpenDate").val() == "" && $("#txtToOpenDate").val() == ""
            ? "All Dates"
            : ($("#txtFromOpenDate").val() == "" ? "" : "From " + $("#txtFromOpenDate").val() + " ") + ($("#txtToOpenDate").val() == "" ? "" : "To " + $("#txtToOpenDate").val())) + '</div>';
        if (pDefaults.UnEditableCompanyName == "ELI" || pDefaults.UnEditableCompanyName == "ELC") {
            ReportHTML += '             <div class="col-xs-6"><b>Invoice Date :</b> ' + ($("#txtFromInvoiceDate").val() == "" && $("#txtToInvoiceDate").val() == ""
                ? "All Dates"
                : ($("#txtFromInvoiceDate").val() == "" ? "" : "From " + $("#txtFromInvoiceDate").val() + " ") + ($("#txtToInvoiceDate").val() == "" ? "" : "To " + $("#txtToInvoiceDate").val())) + '</div>';
        }
        if ($("#slOperationStages option:selected").text() == "CLOSED")
            ReportHTML += '             <div class="col-xs-6"><b>Close Date :</b> ' + ($("#txtFromCloseDate").val() == "" && $("#txtToCloseDate").val() == ""
                ? "All Dates"
                : ($("#txtFromCloseDate").val() == "" ? "" : "From " + $("#txtFromCloseDate").val() + " ") + ($("#txtToCloseDate").val() == "" ? "" : "To " + $("#txtToCloseDate").val())) + '</div>';

        ////ReportHTML += '                 <section class="panel panel-default">';
        //ReportHTML += '                     <div class="table-responsive">';
        ReportHTML += '                         <div> &nbsp; </div>'
        
        ReportHTML += pTablesHTML;
        //ReportHTML += '                     </div>';//of table-responsive
        ReportHTML += '             <div class="col-xs-12"><b>No of Operations :</b> ' + serial + '</div>';
        ReportHTML += '             <div class="col-xs-12"><b>Payables Summary :</b> ' + pPayablesCurrenciesSummary + '</div>';
        ReportHTML += '             <div class="col-xs-12"><b>' + ($("#cbIsUsedInOperationStatement").prop("checked") ? 'Invoices' : 'Receivables') + ' Summary :</b> ' + pReceivablesOrInvoicesCurrenciesSummary + '</div>';
        ReportHTML += '             <div class="col-xs-12"><b>Profit Summary :</b> ' + pProfitCurrenciesSummary + '</div>';
        ReportHTML += '             <div class="col-xs-12"><b>Profit Margin :</b> ' + pMarginSummary + '%</div>';

        ReportHTML += '         </body>';
        ReportHTML += '     <footer class="footer col-xs-12" style="width:100%; position:absolute; bottom:0;">';
        ////ReportHTML += '         <div class="row text-right m-r">الشركه خاضعه لنظام الدفعات المقدمه تطبيقا لأحكام الماده 62 من القانون رقم 91 لسنة 2005 و لا يجوز الخصم عليها</div>';
        //if ($("#tblDocsOut tr[id=" + pDocumentTypeID + "] td.IsPrintISOCode input[type=checkbox]").prop("checked"))
        //    ReportHTML += '     <div class="row m-l">' + $("#tblDocsOut tr[id=" + pDocumentTypeID + "] td.ISOCode").text() + '</div>';
        //ReportHTML += '         <div class="row text-center"><img src="/Content/Images/CompanyFooter.jpg" alt="footer"/></div>';
        ReportHTML += '     </footer>';

        ReportHTML += '</html>';
        mywindow.document.write(ReportHTML);
        mywindow.document.close();
    }
}
function ProfitStatistics_DrawReport_WithChargeDetails(data, pOutputTo) {
    debugger;
    var pReportRows = JSON.parse(data[1]);
    var pPayablesCurrenciesSummary = data[2];
    var pReceivablesOrInvoicesCurrenciesSummary = data[3];
    var pProfitCurrenciesSummary = data[4];
    var pMarginSummary = data[5];

    var pReportTitle = "Profit Statistics";

    var TodaysDateddMMyyyy = getTodaysDateInddMMyyyyFormat();
    var pTablesHTML = "";
    //pTablesHTML += '                         <table id="tblProfitStatistics" class="table table-striped text-sm table-hover b-t b-r b-b b-l ">';//remove t1 class to remove row numbers
    pTablesHTML += '                         <table id="tblProfitStatistics" class="table table-striped text-sm table-bordered print " style="border:solid #999 !important;">';//remove t1 class to remove row numbers
    pTablesHTML += '                             <thead>';
    pTablesHTML += '                                 <tr class="" style="background-color:whitesmoke;font-size:95%;">';
    pTablesHTML += '                                     <th>Ser.</th>';
    pTablesHTML += '                                     <th class="' + ($("#cbOperationCode").prop("checked") ? '' : 'hide') + '">' + (pDefaults.UnEditableCompanyName == "SAF" ? 'Job No' : 'Oper.Code') + '</th>';
    pTablesHTML += '                                     <th class="' + ($("#cbOpenDate").prop("checked") ? '' : 'hide') + '">Oper.Date</th>';
    pTablesHTML += '                                     <th class="' + ($("#cbBookingParty").prop("checked") ? '' : 'hide') + '">Booking Party</th>';
    pTablesHTML += '                                     <th class="' + ($("#cbClient").prop("checked") ? '' : 'hide') + '">Client</th>';
    pTablesHTML += '                                     <th class="' + ($("#cbSupplier").prop("checked") ? '' : 'hide') + '">Supplier</th>';
    pTablesHTML += '                                     <th class="' + ($("#cbSupplierInvoiceNo").prop("checked") ? '' : 'hide') + '">SupplierInv.</th>';
    pTablesHTML += '                                     <th class="' + ($("#cbPOL").prop("checked") ? '' : 'hide') + '">POL</th>';
    pTablesHTML += '                                     <th class="' + ($("#cbPOD").prop("checked") ? '' : 'hide') + '">POD</th>';
    pTablesHTML += '                                     <th class="' + ($("#cbReleaseNumber").prop("checked") ? '' : 'hide') + '">ReleaseNumber</th>';
    pTablesHTML += '                                     <th class="' + ($("#cbChargeableWeight").prop("checked") ? '' : 'hide') + '">Chg.Wgt</th>';
    //pTablesHTML += '                                     <th>ATD</th>';
    pTablesHTML += '                                     <th class="' + ($("#cbContainerType").prop("checked") ? '' : 'hide') + '">Cont.Types</th>';
    pTablesHTML += '                                     <th class="' + ($("#cbChargeType").prop("checked") ? '' : 'hide') + '">ChargeType</th>';
    if (pDefaults.UnEditableCompanyName == "BED")
        pTablesHTML += '                                     <th>Est.Cost</th>';
    pTablesHTML += '                                     <th>Pay.</th>';
    pTablesHTML += '                                     <th class="' + ($("#cbPayableIssueDate").prop("checked") ? '' : 'hide') + '">Payable Date</th>';

    pTablesHTML += '                                     <th>Cur</th>';
    pTablesHTML += '                                     <th class="' + ($("#cbExchangeRate").prop("checked") ? '' : 'hide') + '">Pay. Ex. Rate</th>';


    pTablesHTML += '                                     <th>Paid</th>';
    pTablesHTML += '                                     <th>Remaining</th>';
    pTablesHTML += '                                     <th>Paid Statue</th>';
    pTablesHTML += '                                     <th>' + ($("#cbIsUsedInOperationStatement").prop("checked") ? 'Inv.' : 'Rec.') + '</th>';
    pTablesHTML += '                                     <th class="' + ($("#cbExchangeRateInvoice").prop("checked") ? '' : 'hide') + '">Inv. Ex. Rate</th>';
    pTablesHTML += '                                     <th class="' + ($("#cbInvoiceIssueDate").prop("checked") ? '' : 'hide') + '">Invoice Date</th>';
    pTablesHTML += '                                     <th>Profit</th>';
    pTablesHTML += '                                     <th class="' + ($("#cbConsignee").prop("checked") ? '' : 'hide') + '">Consignee</th>';
    if (pDefaults.UnEditableCompanyName == "NIL" || pDefaults.UnEditableCompanyName == "EXP" || pDefaults.UnEditableCompanyName == "KDS")
        pTablesHTML += '                                     <th>InvNo.s</th>';
    if (pDefaults.UnEditableCompanyName == "NIL" || pDefaults.UnEditableCompanyName == "EXP") {
        pTablesHTML += '                                     <th>InvDate</th>';
        pTablesHTML += '                                     <th>PO Value</th>';
        pTablesHTML += '                                     <th>PO Date</th>';
        pTablesHTML += '                                     <th>Shipper</th>';
        pTablesHTML += '                                     <th>Service</th>';
        pTablesHTML += '                                     <th>OperState</th>';
    }
    pTablesHTML += '                                 </tr>';
    pTablesHTML += '                             </thead>';
    pTablesHTML += '                             <tbody>';
    debugger;
    //$.each(JSON.parse(data[3]), function (i, item) { debugger; });
    var serial = 0;
    $.each((pReportRows), function (i, item) {
        //pTablesHTML += '                                     <tr class="' + (item.Cargo == "Totals In Default Currency:" ? "font-bold text-ul" : "") + '" style="font-size:95%; ' + (item.Cargo == "Total:" ? "background-color: #f9f9f9;!Important" : "") + '">' + (item.Code == "Total" ? "<b>" : "");
        pTablesHTML += '                                     <tr class="' + '" style="font-size:95%;">';
        pTablesHTML += '                                         <td>' + ++serial + '</td>';
        pTablesHTML += '                                         <td class=' + ($("#cbOperationCode").prop("checked") ? '' : ' hide ') + item.OperationID + '>' + item.OperationCode + '</td>';
        pTablesHTML += '                                         <td class="' + ($("#cbOpenDate").prop("checked") ? '' : 'hide') + '">' + (ConvertDateFormat(GetDateWithFormatMDY(item.OpenDate)) + '\u2008') + '</td>';
        pTablesHTML += '                                         <td class="' + ($("#cbBookingParty").prop("checked") ? '' : 'hide') + '">' + (item.BookingPartyName == 0 || item.BookingPartyName == null ? "" : item.BookingPartyName) + '</td>';
        pTablesHTML += '                                         <td class="' + ($("#cbClient").prop("checked") ? '' : 'hide') + '">' + (item.ClientName == 0 || item.ClientName == null ? "" : item.ClientName) + '</td>';
        pTablesHTML += '                                         <td class="' + ($("#cbSupplier").prop("checked") ? '' : 'hide') + '">' + (item.PartnerSupplierName == 0 ? "" : item.PartnerSupplierName) + '</td>';
        pTablesHTML += '                                         <td class="' + ($("#cbSupplierInvoiceNo").prop("checked") ? '' : 'hide') + '">' + (item.InvoiceNumbers == 0 ? "" : item.InvoiceNumbers) + '</td>';
        pTablesHTML += '                                         <td class="' + ($("#cbPOL").prop("checked") ? '' : 'hide') + '">' + item.POLCode + '</td>';
        pTablesHTML += '                                         <td class="' + ($("#cbPOD").prop("checked") ? '' : 'hide') + '">' + item.PODCode + '</td>';
        pTablesHTML += '                                         <td class="' + ($("#cbReleaseNumber").prop("checked") ? '' : 'hide') + '">' + (item.ReleaseNumber == 0 ? "" : item.ReleaseNumber) + '</td>';
        pTablesHTML += '                                         <td class="' + ($("#cbChargeableWeight").prop("checked") ? '' : 'hide') + '">' + item.ChargeableWeightSum.toFixed(2) + '</td>';
        //pTablesHTML += '                                         <td>' + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.ActualDeparture)) > 0 ? ConvertDateFormat(GetDateWithFormatMDY(item.ActualDeparture)) : "") + '</td>';
        pTablesHTML += '                                         <td class="' + ($("#cbContainerType").prop("checked") ? '' : 'hide') + '">' + item.ContainerTypes + '</td>';
        pTablesHTML += '                                         <td class="' + ($("#cbChargeType").prop("checked") ? '' : 'hide') + '">' + item.ChargeTypeName + (item.PaymentDate != undefined && Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.PaymentDate)) > 0 ? (" (" + (ConvertDateFormat(GetDateWithFormatMDY(item.PaymentDate)) + '\u2008') + ")") : "") + '</td>';
        if (pDefaults.UnEditableCompanyName == "BED")
            pTablesHTML += '                                         <td>' + item.QuotationCost + '</td>';
        pTablesHTML += '                                         <td>' + ($("#cbWithVAT").prop("checked") ? item.PayablesWithVAT : item.PayablesWithoutVAT).toFixed(2) + '</td>';
        pTablesHTML += '                                         <td class="' + ($("#cbPayableIssueDate").prop("checked") ? '' : 'hide') + '">' + (item.PayableDate != undefined && Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.PayableDate)) > 0 ? (ConvertDateFormat(GetDateWithFormatMDY(item.PayableDate)) + '\u2008') : "") + '</td>';

        pTablesHTML += '                                         <td>' + item.CurrencyCode + '</td>';
        pTablesHTML += '                                         <td class="' + ($("#cbExchangeRate").prop("checked") ? '' : 'hide') + '">' + item.ExchangeRate + '</td>';

        //pTablesHTML += '                                         <td>' + item.CostAmount + '</td>';
        pTablesHTML += '                                         <td>' + item.PaidAmount + '</td>';
        pTablesHTML += '                                         <td>' + item.RemainingAmount + '</td>';
        pTablesHTML += '                                         <td>' + item.PayableStatus + '</td>';
        pTablesHTML += '                                         <td>' + ($("#cbWithVAT").prop("checked") ? item.ReceivablesWithVAT : item.ReceivablesWithoutVAT).toFixed(2) + '</td>';
        pTablesHTML += '                                         <td class="' + ($("#cbExchangeRateInvoice").prop("checked") ? '' : 'hide') + '">' + (item.InvExchangeRate == undefined ? "" : item.InvExchangeRate) + '</td>';
        pTablesHTML += '                                         <td class="' + ($("#cbInvoiceIssueDate").prop("checked") ? '' : 'hide') + '">' + (item.InvoiceDate != undefined && Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.InvoiceDate)) > 0 ? (ConvertDateFormat(GetDateWithFormatMDY(item.InvoiceDate)) + '\u2008') : "") + '</td>';
        pTablesHTML += '                                         <td>' + ($("#cbWithVAT").prop("checked") ? (item.ReceivablesWithVAT - item.PayablesWithVAT) : (item.ReceivablesWithoutVAT - item.PayablesWithoutVAT)).toFixed(2) + '</td>';
        pTablesHTML += '                                         <td class="' + ($("#cbConsignee").prop("checked") ? '' : 'hide') + '">' + (item.ConsigneeName == 0 ? "" : item.ConsigneeName) + '</td>';
        if (pDefaults.UnEditableCompanyName == "NIL" || pDefaults.UnEditableCompanyName == "EXP" || pDefaults.UnEditableCompanyName == "KDS")
            pTablesHTML += '                                         <td>' + (item.InvoiceNumbers == 0 ? "" : item.InvoiceNumbers) + '</td>';
        if (pDefaults.UnEditableCompanyName == "NIL" || pDefaults.UnEditableCompanyName == "EXP") {
            //pTablesHTML += '                                         <td>' + (item.FirstInvoiceDate == 0 ? "" : item.FirstInvoiceDate) + '</td>';
            pTablesHTML += '                                         <td>' + (item.FirstInvoiceDate == 0 ? "" : (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.FirstInvoiceDate)) > 0 ? (ConvertDateFormat(GetDateWithFormatMDY(item.FirstInvoiceDate)) + '\u2008') : "")) + '</td>';
            pTablesHTML += '                                         <td>' + (item.POValue == 0 ? "" : item.POValue) + '</td>';
            pTablesHTML += '                                         <td>' + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.PODate)) > 0 ? (ConvertDateFormat(GetDateWithFormatMDY(item.PODate)) + '\u2008') : "") + '</td>';
            pTablesHTML += '                                         <td>' + (item.ShipperName == 0 ? "" : item.ShipperName) + '</td>';
            pTablesHTML += '                                         <td>' + (item.MoveTypeName == 0 ? "" : item.MoveTypeName) + '</td>';
            pTablesHTML += '                                         <td>' + (item.OperationStageName == 0 ? "" : item.OperationStageName) + '</td>';
        }
        //pTablesHTML += '                                         <td>' + item.CostAmountSubTotal.toFixed(2) + '</td>';
        pTablesHTML += '                                     </tr>';
    });
    pTablesHTML += '                             </tbody>';
    pTablesHTML += '                         </table>';
    $("#hExportedTable").html(pTablesHTML); //in all cases i put the tables in hidden div so i select each table separately

    if (pOutputTo == "Excel") {
        /*********************Append table summaries*************************/
        var pTableSummary = "";
        pTableSummary += '                                     <tr>';
        pTableSummary += '                                         <td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        if (pDefaults.UnEditableCompanyName == "BED")
            pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        if (pDefaults.UnEditableCompanyName == "NIL" || pDefaults.UnEditableCompanyName == "EXP" || pDefaults.UnEditableCompanyName == "KDS")
            pTableSummary += '                                         <td></td>';
        //if (pDefaults.UnEditableCompanyName == "NIL" || pDefaults.UnEditableCompanyName == "EXP") {
        //    pTableSummary += '                                         <td></td>';
        //    pTableSummary += '                                         <td></td>';
        //    pTableSummary += '                                         <td></td>';
        //    pTableSummary += '                                         <td></td>';
        //    pTableSummary += '                                         <td></td>';
        //    pTableSummary += '                                         <td></td>';
        //}
        pTableSummary += '                                     </tr>';
        pTableSummary += '                                     <tr>';
        pTableSummary += '                                         <td>Payables Summary :</td>';
        pTableSummary += '                                         <td>' + pPayablesCurrenciesSummary + '</td>';
        pTableSummary += '                                         <td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        if (pDefaults.UnEditableCompanyName == "NIL" || pDefaults.UnEditableCompanyName == "EXP" || pDefaults.UnEditableCompanyName == "KDS")
            pTableSummary += '                                         <td></td>';
        //if (pDefaults.UnEditableCompanyName == "NIL" || pDefaults.UnEditableCompanyName == "EXP") {
        //    pTableSummary += '                                         <td></td>';
        //    pTableSummary += '                                         <td></td>';
        //    pTableSummary += '                                         <td></td>';
        //    pTableSummary += '                                         <td></td>';
        //    pTableSummary += '                                         <td></td>';
        //    pTableSummary += '                                         <td></td>';
        //}
        pTableSummary += '                                     </tr>';
        pTableSummary += '                                     <tr>';
        pTableSummary += '                                         <td>' + ($("#cbIsUsedInOperationStatement").prop("checked") ? 'Invoices' : 'Receivables') + ' Summary :</td>';
        pTableSummary += '                                         <td>' + pReceivablesOrInvoicesCurrenciesSummary + '</td>';
        pTableSummary += '                                         <td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        if (pDefaults.UnEditableCompanyName == "NIL" || pDefaults.UnEditableCompanyName == "EXP" || pDefaults.UnEditableCompanyName == "KDS")
            pTableSummary += '                                         <td></td>';
        //if (pDefaults.UnEditableCompanyName == "NIL" || pDefaults.UnEditableCompanyName == "EXP") {
        //    pTableSummary += '                                         <td></td>';
        //    pTableSummary += '                                         <td></td>';
        //    pTableSummary += '                                         <td></td>';
        //    pTableSummary += '                                         <td></td>';
        //    pTableSummary += '                                         <td></td>';
        //    pTableSummary += '                                         <td></td>';
        //}
        pTableSummary += '                                     </tr>';
        pTableSummary += '                                     <tr>';
        pTableSummary += '                                         <td>Profit Summary :</td>';
        pTableSummary += '                                         <td>' + pProfitCurrenciesSummary + '</td>';
        pTableSummary += '                                         <td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        //pTableSummary += '                                         <td></td>'; //ATD
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        if (pDefaults.UnEditableCompanyName == "NIL" || pDefaults.UnEditableCompanyName == "EXP" || pDefaults.UnEditableCompanyName == "KDS")
            pTableSummary += '                                         <td></td>';
        //if (pDefaults.UnEditableCompanyName == "NIL" || pDefaults.UnEditableCompanyName == "EXP") {
        //    pTableSummary += '                                         <td></td>';
        //    pTableSummary += '                                         <td></td>';
        //    pTableSummary += '                                         <td></td>';
        //    pTableSummary += '                                         <td></td>';
        //    pTableSummary += '                                         <td></td>';
        //    pTableSummary += '                                         <td></td>';
        //}
        pTableSummary += '                                     </tr>';
        pTableSummary += '                                     <tr>';
        pTableSummary += '                                         <td>Profit Margin :</td>'; 
        pTableSummary += '                                         <td>' + pMarginSummary + '</td>';
        pTableSummary += '                                         <td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        //pTableSummary += '                                         <td></td>'; //ATD
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        if (pDefaults.UnEditableCompanyName == "NIL" || pDefaults.UnEditableCompanyName == "EXP" || pDefaults.UnEditableCompanyName == "KDS")
            pTableSummary += '                                         <td></td>';
        //if (pDefaults.UnEditableCompanyName == "NIL" || pDefaults.UnEditableCompanyName == "EXP") {
        //    pTableSummary += '                                         <td></td>';
        //    pTableSummary += '                                         <td></td>';
        //    pTableSummary += '                                         <td></td>';
        //    pTableSummary += '                                         <td></td>';
        //    pTableSummary += '                                         <td></td>';
        //    pTableSummary += '                                         <td></td>';
        //}
        pTableSummary += '                                     </tr>';
        $("#tblProfitStatistics" + " tbody").append(pTableSummary);
        ExportHtmlTableToCsv_RemovingCommasForNumbers("tblProfitStatistics", "Profit Statistics");
    }
    else {
        var mywindow = window.open('', '_blank');
        var ReportHTML = '';
        //<div style="width:500px;height:100px;border:1px solid #000;">This is a rectangle!</div>
        ReportHTML += '<html>';
        ReportHTML += '     <head><title>' + pReportTitle + '</title><link rel="stylesheet" href="/Content/CSS/app.v2.css" type="text/css" /></head>';
        ReportHTML += '         <body style="background-color:white;">';
        ReportHTML += '             <div class="col-xs-12 text-center"><img src="/Content/Images/CompanyHeader.jpg" alt="logo"/></div> </br>';
        ReportHTML += '             <div class="col-xs-12 text-center text-ul m-t-n"><h3>' + pReportTitle + '</h3></div> </br>';

        ReportHTML += '             <div class="col-xs-3"><b>Printed On :</b> ' + TodaysDateddMMyyyy + '</div>';
        ReportHTML += '             <div class="col-xs-3"><b>Transport Type :</b> ' + $("#divTransportFilter label.active").attr("title") + '</div>';
        ReportHTML += '             <div class="col-xs-3"><b>Direction Type :</b> ' + $("#divDirectionFilter label.active").attr("title") + '</div>';
        ReportHTML += '             <div class="col-xs-3"><b>B/L Type :</b> ' + $("#divBLTypeFilter label.active").attr("title") + '</div>';
        ReportHTML += '             <div class="col-xs-3"><b>Branch :</b> ' + $("#slBranch option:selected").text() + '</div>';
        ReportHTML += '             <div class="col-xs-3"><b>Salesman :</b> ' + $("#slSalesman option:selected").text() + '</div>';
        ReportHTML += '             <div class="col-xs-3"><b>Customer :</b> ' + $("#slCustomer option:selected").text() + '</div>';
        ReportHTML += '             <div class="col-xs-3"><b>Agent :</b> ' + $("#slAgent option:selected").text() + '</div>';
        //ReportHTML += '             <div class="col-xs-3"><b>Currency :</b> ' + $("#slCurrency option:selected").text() + '</div>';
        ReportHTML += '             <div class="col-xs-3"><b>Oper. Status :</b> ' + $("#slOperationStages option:selected").text() + '</div>';

        ReportHTML += '             <div class="col-xs-3"><b>POLCountry :</b> ' + $("#slCountryPOL option:selected").text() + '</div>';
        ReportHTML += '             <div class="col-xs-3"><b>POL :</b> ' + $("#slPOL option:selected").text() + '</div>';
        ReportHTML += '             <div class="col-xs-3"><b>PODCountry :</b> ' + $("#slCountryPOD option:selected").text() + '</div>';
        ReportHTML += '             <div class="col-xs-3"><b>POD :</b> ' + $("#slPOD option:selected").text() + '</div>';

        ReportHTML += '             <div class="col-xs-6"><b>Open Date :</b> ' + ($("#txtFromOpenDate").val() == "" && $("#txtToOpenDate").val() == ""
            ? "All Dates"
            : ($("#txtFromOpenDate").val() == "" ? "" : "From " + $("#txtFromOpenDate").val() + " ") + ($("#txtToOpenDate").val() == "" ? "" : "To " + $("#txtToOpenDate").val())) + '</div>';
        if ($("#slOperationStages option:selected").text() == "CLOSED")
            ReportHTML += '             <div class="col-xs-6"><b>Close Date :</b> ' + ($("#txtFromCloseDate").val() == "" && $("#txtToCloseDate").val() == ""
                ? "All Dates"
                : ($("#txtFromCloseDate").val() == "" ? "" : "From " + $("#txtFromCloseDate").val() + " ") + ($("#txtToCloseDate").val() == "" ? "" : "To " + $("#txtToCloseDate").val())) + '</div>';

        ////ReportHTML += '                 <section class="panel panel-default">';
        //ReportHTML += '                     <div class="table-responsive">';
        //ReportHTML += '                         <div> &nbsp; </div>'

        ReportHTML += pTablesHTML;
        //ReportHTML += '                     </div>';//of table-responsive
        //ReportHTML += '             <div class="col-xs-12"><b>No of Operations :</b> ' + serial + '</div>';
        ReportHTML += '             <div class="col-xs-12"><b>Payables Summary :</b> ' + pPayablesCurrenciesSummary + '</div>';
        ReportHTML += '             <div class="col-xs-12"><b>' + ($("#cbIsUsedInOperationStatement").prop("checked") ? 'Invoices' : 'Receivables') + ' Summary :</b> ' + pReceivablesOrInvoicesCurrenciesSummary + '</div>';
        ReportHTML += '             <div class="col-xs-12"><b>Profit Summary :</b> ' + pProfitCurrenciesSummary + '</div>';
        ReportHTML += '             <div class="col-xs-12"><b>Profit Margin :</b> ' + pMarginSummary + '%</div>'; 

        ReportHTML += '         </body>';
        ReportHTML += '     <footer class="footer col-xs-12" style="width:100%; position:absolute; bottom:0;">';
        ////ReportHTML += '         <div class="row text-right m-r">الشركه خاضعه لنظام الدفعات المقدمه تطبيقا لأحكام الماده 62 من القانون رقم 91 لسنة 2005 و لا يجوز الخصم عليها</div>';
        //if ($("#tblDocsOut tr[id=" + pDocumentTypeID + "] td.IsPrintISOCode input[type=checkbox]").prop("checked"))
        //    ReportHTML += '     <div class="row m-l">' + $("#tblDocsOut tr[id=" + pDocumentTypeID + "] td.ISOCode").text() + '</div>';
        //ReportHTML += '         <div class="row text-center"><img src="/Content/Images/CompanyFooter.jpg" alt="footer"/></div>';
        ReportHTML += '     </footer>';

        ReportHTML += '</html>';
        mywindow.document.write(ReportHTML);
        mywindow.document.close();
    }
}
function ProfitStatistics_DrawReport_GroupByOperations(data, pOutputTo) {
    debugger;
    var pReportRows = JSON.parse(data[1]);
    var pPayablesCurrenciesSummary = data[2].split("=")[0];
    var pReceivablesOrInvoicesCurrenciesSummary = data[3].split("=")[0];
    var pProfitCurrenciesSummary = data[4].split("=")[0];
    var pMarginSummary = data[5];
    var pOperationIDs = data[6];

    var _BranchIDs = "";
    for (var i = 1; i < ($("#slBranch option").length + 1); i++)
        _BranchIDs += _BranchIDs == "" ? $("#slBranch option:nth-child(" + (i) + ")").val() : ("," + $("#slBranch option:nth-child(" + (i) + ")").val());
    var ArrBranchIDs = _BranchIDs.split(",");
    var ArrOperationIDs = pOperationIDs.split(",");

    var pReportTitle = "Profit Statistics";

    var TodaysDateddMMyyyy = getTodaysDateInddMMyyyyFormat();
    var pTablesHTML = "";

    //for (var y = 0; y < ArrBranchIDs.length; y++) {
    //    var pCurrentBranchID = ArrBranchIDs[y];

    for (var z = 0; z < ArrOperationIDs.length; z++) {
        debugger;

        var pCurrentOperationID = ArrOperationIDs[z];
        var pIsHeaderPrinted = false; //Header of the current operation
        //var CurrentOperationRows = pReportRows.filter(x=>x.OperationID==ArrOperationIDs[z] || x.MasterOperationID==ArrOperationIDs[z]);
        //var CurrentOperationRows = pReportRows.filter(x=>x.OperationID==pCurrentOperationID && x.BranchID==pCurrentBranchID);
        var CurrentOperationRows = pReportRows.filter(x => x.OperationID == pCurrentOperationID);
        if (CurrentOperationRows.length > 0) { //to make sure it has charges
            /***************************Print Operation Header******************************/
            //pTablesHTML += '                         <table id="tblOperationHeader' + pCurrentOperationID + '" class="table table-striped text-sm table-bordered print " style="border:solid #999 !important;">';//remove t1 class to remove row numbers
            //pTablesHTML += '                             <thead>';
            //pTablesHTML += '                                 <tr class="" style="background-color:whitesmoke;font-size:95%;">';
            //pTablesHTML += '                                     <th>' + CurrentOperationRows[0].OperationCode + '</th>';
            ////pTablesHTML += '                                     <th>' + ConvertDateFormat(GetDateWithFormatMDY(CurrentOperationRows[0].OpenDate)) + '</th>';
            ////pTablesHTML += '                                     <th>' + (CurrentOperationRows[0].BookingPartyName == 0 || CurrentOperationRows[0].BookingPartyName == null ? "" : CurrentOperationRows[0].BookingPartyName) + '</th>';
            ////pTablesHTML += '                                     <th>' + (CurrentOperationRows[0].ClientName == 0 || CurrentOperationRows[0].ClientName == null ? "" : CurrentOperationRows[0].ClientName) + '</th>';
            ////pTablesHTML += '                                     <th>' + CurrentOperationRows[0].POLCode + '</th>';
            ////pTablesHTML += '                                     <th>' + CurrentOperationRows[0].PODCode + '</th>';
            ////pTablesHTML += '                                     <th>' + ConvertDateFormat(GetDateWithFormatMDY(CurrentOperationRows[0].ActualDeparture)) + '</th>';
            ////pTablesHTML += '                                     <th>' + CurrentOperationRows[0].ContainerTypes + '</th>';
            //pTablesHTML += '                                 </tr>';
            //pTablesHTML += '                             </thead>';
            //pTablesHTML += '                             <tbody>';
            //pTablesHTML += '                             </tbody>';
            //pTablesHTML += '                         </table>';
            debugger;
            pTablesHTML += '                         <table id="tblProfitStatistics' + pCurrentOperationID + '" class="table table-striped text-sm table-bordered print " style="border:solid #999 !important;">';//remove t1 class to remove row numbers
            pTablesHTML += '                             <thead>';
            pTablesHTML += '                                 <tr style="background-color:whitesmoke;font-size:95%;">';
            pTablesHTML += '                                     <th colspan=6 style="text-align:left;">';
            //pTablesHTML += '                                        <u>Oper.Code:</u>' + CurrentOperationRows[0].OperationCode;
            //pTablesHTML += '                                        &emsp;<u>Oper.Date:</u>' + ConvertDateFormat(GetDateWithFormatMDY(CurrentOperationRows[0].OpenDate));
            //if ((CurrentOperationRows[0].BookingPartyName != 0 && CurrentOperationRows[0].BookingPartyName != null))
            //    pTablesHTML += '                                    &emsp;<u>Booking Party:</u>' + CurrentOperationRows[0].BookingPartyName;
            //if ((CurrentOperationRows[0].ClientName != 0 && CurrentOperationRows[0].ClientName != null))
            //    pTablesHTML += '                                    &emsp;<u>Client:</u>' + CurrentOperationRows[0].ClientName;
            //pTablesHTML += '                                        &emsp;<u>POL:</u>' + CurrentOperationRows[0].POLCode;
            //pTablesHTML += '                                        &emsp;<u>POD:</u>' + CurrentOperationRows[0].PODCode;
            //if (GetDateWithFormatMDY(CurrentOperationRows[0].ActualDeparture) != "01/01/1900")
            //    pTablesHTML += '                                        &emsp;<u>ATD:</u>' + ConvertDateFormat(GetDateWithFormatMDY(CurrentOperationRows[0].ActualDeparture));
            //if (CurrentOperationRows[0].ContainerTypes != "")
            //    pTablesHTML += '                                    &emsp;<u>Containers:</u>' + CurrentOperationRows[0].ContainerTypes;
            pTablesHTML += '                                        ' + (pDefaults.UnEditableCompanyName == "SAF" ? 'Job No' : 'Oper.Code') + ':' + CurrentOperationRows[0].OperationCode;
            pTablesHTML += '                                        &emsp;Oper.Date:' + (ConvertDateFormat(GetDateWithFormatMDY(CurrentOperationRows[0].OpenDate)) + '\u2008');
            if ((CurrentOperationRows[0].BookingPartyName != 0 && CurrentOperationRows[0].BookingPartyName != null))
                pTablesHTML += '                                    &emsp;Booking Party:' + CurrentOperationRows[0].BookingPartyName;
            if ((CurrentOperationRows[0].ClientName != 0 && CurrentOperationRows[0].ClientName != null))
                pTablesHTML += '                                    &emsp;Client:' + CurrentOperationRows[0].ClientName;
            pTablesHTML += '                                        &emsp;POL:' + CurrentOperationRows[0].POLCode;
            pTablesHTML += '                                        &emsp;POD:' + CurrentOperationRows[0].PODCode;
            //if (GetDateWithFormatMDY(CurrentOperationRows[0].ActualDeparture) != "01/01/1900")
            //    pTablesHTML += '                                        &emsp;ATD:' + ConvertDateFormat(GetDateWithFormatMDY(CurrentOperationRows[0].ActualDeparture));
            if (CurrentOperationRows[0].ContainerTypes != "")
                pTablesHTML += '                                    &emsp;Containers:' + CurrentOperationRows[0].ContainerTypes;
            if (pDefaults.UnEditableCompanyName == "NIL" || pDefaults.UnEditableCompanyName == "EXP" || pDefaults.UnEditableCompanyName == "KDS")
                pTablesHTML += '                                    &emsp;Inv.Nos:' + (CurrentOperationRows[0].InvoiceNumbers == 0 ? "None" : CurrentOperationRows[0].InvoiceNumbers);
            pTablesHTML += '                                    &emsp;ConsigneeName:' + (CurrentOperationRows[0].ConsigneeName == 0 ? "None" : CurrentOperationRows[0].ConsigneeName);
            if (pDefaults.UnEditableCompanyName == "NIL" || pDefaults.UnEditableCompanyName == "EXP") {
                pTablesHTML += '                                    &emsp;Inv.Date:' + (CurrentOperationRows[0].FirstInvoiceDate == 0 ? "None" : CurrentOperationRows[0].FirstInvoiceDate);
                pTablesHTML += '                                    &emsp;PO Value:' + (CurrentOperationRows[0].POValue == 0 ? "None" : CurrentOperationRows[0].POValue);
                pTablesHTML += '                                    &emsp;PO Date:' + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(CurrentOperationRows[0].PODate)) > 0 ? ConvertDateFormat(GetDateWithFormatMDY(CurrentOperationRows[0].PODate)) : "None");
                pTablesHTML += '                                    &emsp;ShipperName:' + (CurrentOperationRows[0].ShipperName == 0 ? "None" : CurrentOperationRows[0].ShipperName);
                pTablesHTML += '                                    &emsp;Service:' + (CurrentOperationRows[0].MoveTypeName == 0 ? "None" : CurrentOperationRows[0].MoveTypeName);
                pTablesHTML += '                                    &emsp;OperationStageName:' + (CurrentOperationRows[0].OperationStageName == 0 ? "None" : CurrentOperationRows[0].OperationStageName);
            }
            pTablesHTML += '                                     </th>';
            //pTablesHTML += '                                     <th>' + CurrentOperationRows[0].OperationCode + '</th>';
            //pTablesHTML += '                                     <th>' + ConvertDateFormat(GetDateWithFormatMDY(CurrentOperationRows[0].OpenDate)) + '</th>';
            //pTablesHTML += '                                     <th>' + (CurrentOperationRows[0].BookingPartyName == 0 || CurrentOperationRows[0].BookingPartyName == null ? "" : CurrentOperationRows[0].BookingPartyName) + '</th>';
            //pTablesHTML += '                                     <th>' + (CurrentOperationRows[0].ClientName == 0 || CurrentOperationRows[0].ClientName == null ? "" : CurrentOperationRows[0].ClientName) + '</th>';
            //pTablesHTML += '                                     <th>' + CurrentOperationRows[0].POLCode + '</th>';
            //pTablesHTML += '                                     <th>' + CurrentOperationRows[0].PODCode + '</th>';
            //pTablesHTML += '                                     <th>' + ConvertDateFormat(GetDateWithFormatMDY(CurrentOperationRows[0].ActualDeparture)) + '</th>';
            //pTablesHTML += '                                     <th>' + CurrentOperationRows[0].ContainerTypes + '</th>';
            pTablesHTML += '                                 </tr>';
            pTablesHTML += '                             </thead>';


            pTablesHTML += '                             <tbody>';
            pTablesHTML += '                                 <tr class="" style="background-color:whitesmoke;font-size:95%;">';
            //pTablesHTML += '                                     <th>Ser.</th>';
            //pTablesHTML += '                                     <th style="width:20%;">Branch</th>';
            pTablesHTML += '                                     <th style="width:35%;">ChargeType</th>';
            pTablesHTML += '                                     <th style="width:20%;">Est.Cost</th>';
            pTablesHTML += '                                     <th style="width:20%;">Pay.</th>';
            pTablesHTML += '                                     <th style="width:20%;">' + ($("#cbIsUsedInOperationStatement").prop("checked") ? 'Inv.' : 'Rec.') + '</th>';
            pTablesHTML += '                                     <th style="width:20%;">Profit</th>';
            pTablesHTML += '                                     <th style="width:5%;">Cur</th>';
            pTablesHTML += '                                 </tr>';
            var serial = 0;
            $.each((CurrentOperationRows), function (i, item) {
                //debugger;
                //pTablesHTML += '                                     <tr class="' + (item.Cargo == "Totals In Default Currency:" ? "font-bold text-ul" : "") + '" style="font-size:95%; ' + (item.Cargo == "Total:" ? "background-color: #f9f9f9;!Important" : "") + '">' + (item.Code == "Total" ? "<b>" : "");
                pTablesHTML += '                                     <tr class="' + '" style="font-size:95%;">';
                //pTablesHTML += '                                         <td>' + ++serial + '</td>';
                //pTablesHTML += '                                         <td>' + item.BranchName + '</td>';
                pTablesHTML += '                                         <td>' + item.ChargeTypeName
                    //+ (item.PaymentDate != undefined && Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.PaymentDate)) > 0 ? (" ( *PaymentDate:" + ConvertDateFormat(GetDateWithFormatMDY(item.PaymentDate)) + ")") : "")
                    + ((pDefaults.UnEditableCompanyName == "ILS" || pDefaults.UnEditableCompanyName == "ILSEG") && item.PayableDate != undefined && Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.PayableDate)) > 0 ? (" ( *PayableDate:" + ConvertDateFormat(GetDateWithFormatMDY(item.PayableDate)) + ")") : "")
                    //+ (item.InvoiceDate != undefined && Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.InvoiceDate)) > 0 ? (" ( *InvoiceDate:" + ConvertDateFormat(GetDateWithFormatMDY(item.InvoiceDate)) + ")") : "") + '</td>';
                pTablesHTML += '                                         <td class="QuotationCost' + item.CurrencyCode + '">' + item.QuotationCost.toFixed(2) + '</td>';
                pTablesHTML += '                                         <td class="Payable' + item.CurrencyCode + '">' + ($("#cbWithVAT").prop("checked") ? item.PayablesWithVAT.toFixed(2) : item.PayablesWithoutVAT.toFixed(2)) + '</td>';
                pTablesHTML += '                                         <td class="Receivable' + item.CurrencyCode + '">' + ($("#cbWithVAT").prop("checked") ? item.ReceivablesWithVAT.toFixed(2) : item.ReceivablesWithoutVAT.toFixed(2)) + '</td>';
                pTablesHTML += '                                         <td class="Profit' + item.CurrencyCode + '">' + ($("#cbWithVAT").prop("checked") ? (item.ReceivablesWithVAT - item.PayablesWithVAT).toFixed(2) : (item.ReceivablesWithoutVAT - item.PayablesWithoutVAT).toFixed(2)) + '</td>';
                pTablesHTML += '                                         <td>' + item.CurrencyCode + '</td>';
                pTablesHTML += '                                     </tr>';
            });
            pTablesHTML += '                             </tbody>';
            pTablesHTML += '                         </table>';
        } //if (CurrentOperationRows.length > 0)
    } //for (var z = 0; z < ArrOperationIDs.length; z++) {
    //} //for (var y = 0; y < ArrBranchIDs.length; y++) {
    $("#hExportedTable").html(pTablesHTML); //in all cases i put the tables in hidden div so i select each table separately
    /******************************Get Tables Summary*********************************/
    for (var z = 0; z < ArrOperationIDs.length; z++) {
        var pQuotationCostEGPSum = 0; var pQuotationCostUSDSum = 0; var pQuotationCostEURSum = 0; var pQuotationCostGBPSum = 0; var pQuotationCostSARSum = 0;
        var pPayableEGPSum = 0; var pPayableUSDSum = 0; var pPayableEURSum = 0; var pPayableGBPSum = 0; var pPayableSARSum = 0;
        var pReceivableEGPSum = 0; var pReceivableUSDSum = 0; var pReceivableEURSum = 0; var pReceivableGBPSum = 0; var pReceivableSARSum = 0;
        var pProfitEGPSum = 0; var pProfitUSDSum = 0; var pProfitEURSum = 0; var pProfitGBPSum = 0; var pProfitSARSum = 0;
        pQuotationCostEGPSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "QuotationCostEGP");
        pQuotationCostUSDSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "QuotationCostUSD");
        pQuotationCostEURSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "QuotationCostEUR");
        pQuotationCostGBPSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "QuotationCostGBP");
        pQuotationCostSARSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "QuotationCostSAR");

        pPayableEGPSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "PayableEGP");
        pPayableUSDSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "PayableUSD");
        pPayableEURSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "PayableEUR");
        pPayableGBPSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "PayableGBP");
        pPayableSARSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "PayableSAR");

        pReceivableEGPSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "ReceivableEGP");
        pReceivableUSDSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "ReceivableUSD");
        pReceivableEURSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "ReceivableEUR");
        pReceivableGBPSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "ReceivableGBP");
        pReceivableSARSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "ReceivableSAR");

        pProfitEGPSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "ProfitEGP");
        pProfitUSDSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "ProfitUSD");
        pProfitEURSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "ProfitEUR");
        pProfitGBPSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "ProfitGBP");
        pProfitSARSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "ProfitSAR");
        var pTableSummary = "";
        pTableSummary += '         <tr class="font-bold" style="font-size:95%;">';
        pTableSummary += '             <td colspan=1><u>' + "Total" + '</u></td>';
        pTableSummary += '             <td class="QuotationCostSummary">'
            + (pQuotationCostEGPSum != 0 ? (pQuotationCostEGPSum.toFixed(2) + 'EGP ') : "")
            + (pQuotationCostUSDSum != 0 ? (pQuotationCostUSDSum.toFixed(2) + 'USD ') : "")
            + (pQuotationCostEURSum != 0 ? (pQuotationCostEURSum.toFixed(2) + 'EUR ') : "")
            + (pQuotationCostGBPSum != 0 ? (pQuotationCostGBPSum.toFixed(2) + 'GBP ') : "")
            + (pQuotationCostSARSum != 0 ? (pQuotationCostSARSum.toFixed(2) + 'SAR ') : "") + '</td>';
        pTableSummary += '             <td class="PayableSummary">'
            + (pPayableEGPSum != 0 ? (pPayableEGPSum.toFixed(2) + 'EGP ') : "")
            + (pPayableUSDSum != 0 ? (pPayableUSDSum.toFixed(2) + 'USD ') : "")
            + (pPayableEURSum != 0 ? (pPayableEURSum.toFixed(2) + 'EUR ') : "")
            + (pPayableGBPSum != 0 ? (pPayableGBPSum.toFixed(2) + 'GBP ') : "")
            + (pPayableSARSum != 0 ? (pPayableSARSum.toFixed(2) + 'SAR ') : "") + '</td>';
        pTableSummary += '             <td class="ReceivableSummary">'
            + (pReceivableEGPSum != 0 ? (pReceivableEGPSum.toFixed(2) + 'EGP ') : "")
            + (pReceivableUSDSum != 0 ? (pReceivableUSDSum.toFixed(2) + 'USD ') : "")
            + (pReceivableEURSum != 0 ? (pReceivableEURSum.toFixed(2) + 'EUR ') : "")
            + (pReceivableGBPSum != 0 ? (pReceivableGBPSum.toFixed(2) + 'GBP ') : "")
            + (pReceivableSARSum != 0 ? (pReceivableSARSum.toFixed(2) + 'SAR ') : "") + '</td>';
        pTableSummary += '             <td class="ProfitSummary">'
            + (pProfitEGPSum != 0 ? (pProfitEGPSum.toFixed(2) + 'EGP ') : "")
            + (pProfitUSDSum != 0 ? (pProfitUSDSum.toFixed(2) + 'USD ') : "")
            + (pProfitEURSum != 0 ? (pProfitEURSum.toFixed(2) + 'EUR ') : "")
            + (pProfitGBPSum != 0 ? (pProfitGBPSum.toFixed(2) + 'GBP ') : "")
            + (pProfitSARSum != 0 ? (pProfitSARSum.toFixed(2) + 'SAR ') : "") + '</td>';
        pTableSummary += '             <td>' + '' + '</td>';
        pTableSummary += '         </tr>';
        $("#tblProfitStatistics" + ArrOperationIDs[z] + " tbody").append(pTableSummary);
    }
    if (pOutputTo == "Excel") {
        //$("#tblProfitStatistics" + " tbody").append(pTableSummary);
        //ExportHtmlTableToCsv_RemovingCommasForNumbers("tblProfitStatistics", "Profit Statistics");
    }
    else {
        var mywindow = window.open('', '_blank');
        var ReportHTML = '';
        //<div style="width:500px;height:100px;border:1px solid #000;">This is a rectangle!</div>
        ReportHTML += '<html>';
        ReportHTML += '     <head><title>' + pReportTitle + '</title><link rel="stylesheet" href="/Content/CSS/app.v2.css" type="text/css" /></head>';
        ReportHTML += '         <body style="background-color:white;">';
        ReportHTML += '             <div class="col-xs-12 text-center"><img src="/Content/Images/CompanyHeader.jpg" alt="logo"/></div> </br>';
        ReportHTML += '             <div class="col-xs-12 text-center text-ul m-t-n"><h3>' + pReportTitle + '</h3></div> </br>';

        //ReportHTML += '             <div class="col-xs-3"><b>Transport Type :</b> ' + $("#divTransportFilter label.active").attr("title") + '</div>';
        //ReportHTML += '             <div class="col-xs-3"><b>Direction Type :</b> ' + $("#divDirectionFilter label.active").attr("title") + '</div>';
        //ReportHTML += '             <div class="col-xs-3"><b>B/L Type :</b> ' + $("#divBLTypeFilter label.active").attr("title") + '</div>';
        ReportHTML += '             <div class="col-xs-6 m-l-n"><b>Printed On :</b> ' + TodaysDateddMMyyyy + '</div>';
        ReportHTML += '             <div class="col-xs-6"><b>Branch :</b> ' + $("#slBranch option:selected").text() + '</div>';
        //ReportHTML += '             <div class="col-xs-3"><b>Salesman :</b> ' + $("#slSalesman option:selected").text() + '</div>';
        //ReportHTML += '             <div class="col-xs-3"><b>Customer :</b> ' + $("#slCustomer option:selected").text() + '</div>';
        ////ReportHTML += '             <div class="col-xs-3"><b>Currency :</b> ' + $("#slCurrency option:selected").text() + '</div>';
        //ReportHTML += '             <div class="col-xs-3"><b>Oper. Status :</b> ' + $("#slOperationStages option:selected").text() + '</div>';
        ReportHTML += '             <div class="col-xs-6 m-l-n"><b>Open Date :</b> ' + ($("#txtFromOpenDate").val() == "" && $("#txtToOpenDate").val() == ""
            ? "All Dates"
            : ($("#txtFromOpenDate").val() == "" ? "" : "From " + $("#txtFromOpenDate").val() + " ") + ($("#txtToOpenDate").val() == "" ? "" : "To " + $("#txtToOpenDate").val())) + '</div>';
        if (pDefaults.UnEditableCompanyName == "ELI" || pDefaults.UnEditableCompanyName == "ELC") {
            ReportHTML += '             <div class="col-xs-6"><b>Invoice Date :</b> ' + ($("#txtFromInvoiceDate").val() == "" && $("#txtToInvoiceDate").val() == ""
                ? "All Dates"
                : ($("#txtFromInvoiceDate").val() == "" ? "" : "From " + $("#txtFromInvoiceDate").val() + " ") + ($("#txtToInvoiceDate").val() == "" ? "" : "To " + $("#txtToInvoiceDate").val())) + '</div>';
        }
        if ($("#slOperationStages option:selected").text() == "CLOSED")
            ReportHTML += '             <div class="col-xs-6"><b>Close Date :</b> ' + ($("#txtFromCloseDate").val() == "" && $("#txtToCloseDate").val() == ""
                ? "All Dates"
                : ($("#txtFromCloseDate").val() == "" ? "" : "From " + $("#txtFromCloseDate").val() + " ") + ($("#txtToCloseDate").val() == "" ? "" : "To " + $("#txtToCloseDate").val())) + '</div>';


        ////ReportHTML += '                 <section class="panel panel-default">';
        //ReportHTML += '                     <div class="table-responsive">';
        //ReportHTML += '                         <div> &nbsp; </div>'

        ReportHTML += $("#hExportedTable").html();//pTablesHTML;
        //ReportHTML += '                     </div>';//of table-responsive
        //ReportHTML += '             <div class="col-xs-12"><b>No of Operations :</b> ' + serial + '</div>';
        ReportHTML += '             <div class="col-xs-12"><b>Payables Summary :</b> ' + pPayablesCurrenciesSummary + '</div>';
        ReportHTML += '             <div class="col-xs-12"><b>' + ($("#cbIsUsedInOperationStatement").prop("checked") ? 'Invoices' : 'Receivables') + ' Summary :</b> ' + pReceivablesOrInvoicesCurrenciesSummary + '</div>';
        ReportHTML += '             <div class="col-xs-12"><b>Profit Summary :</b> ' + pProfitCurrenciesSummary + '</div>';
        //ReportHTML += '             <div class="col-xs-12"><b>Profit Margin :</b> ' + pMarginSummary + '%</div>';

        ReportHTML += '         </body>';
        ReportHTML += '     <footer class="footer col-xs-12" style="width:100%; position:absolute; bottom:0;">';
        ////ReportHTML += '         <div class="row text-right m-r">الشركه خاضعه لنظام الدفعات المقدمه تطبيقا لأحكام الماده 62 من القانون رقم 91 لسنة 2005 و لا يجوز الخصم عليها</div>';
        //if ($("#tblDocsOut tr[id=" + pDocumentTypeID + "] td.IsPrintISOCode input[type=checkbox]").prop("checked"))
        //    ReportHTML += '     <div class="row m-l">' + $("#tblDocsOut tr[id=" + pDocumentTypeID + "] td.ISOCode").text() + '</div>';
        //ReportHTML += '         <div class="row text-center"><img src="/Content/Images/CompanyFooter.jpg" alt="footer"/></div>';
        ReportHTML += '     </footer>';

        ReportHTML += '</html>';
        mywindow.document.write(ReportHTML);
        mywindow.document.close();
    }
}
function ProfitStatistics_DrawReport_GroupByOperations_Excel(data, pOutputTo) {
    debugger;
    var pReportRows = JSON.parse(data[1]);
    var pPayablesCurrenciesSummary = data[2].split("=")[0];
    var pReceivablesOrInvoicesCurrenciesSummary = data[3].split("=")[0];
    var pProfitCurrenciesSummary = data[4].split("=")[0];
    var pMarginSummary = data[5];
    var pOperationIDs = data[6];

    var _BranchIDs = "";
    for (var i = 1; i < ($("#slBranch option").length + 1); i++)
        _BranchIDs += _BranchIDs == "" ? $("#slBranch option:nth-child(" + (i) + ")").val() : ("," + $("#slBranch option:nth-child(" + (i) + ")").val());
    var ArrBranchIDs = _BranchIDs.split(",");
    var ArrOperationIDs = pOperationIDs.split(",");

    var pReportTitle = "Profit Statistics";

    var TodaysDateddMMyyyy = getTodaysDateInddMMyyyyFormat();
    var pTablesHTML = "";
    for (var z = 0; z < ArrOperationIDs.length; z++) {
        //debugger;
        var pCurrentOperationID = ArrOperationIDs[z];
        var pIsHeaderPrinted = false; //Header of the current operation
        //var CurrentOperationRows = pReportRows.filter(x=>x.OperationID==ArrOperationIDs[z] || x.MasterOperationID==ArrOperationIDs[z]);
        var CurrentOperationRows = pReportRows.filter(x => x.OperationID == ArrOperationIDs[z]);
        if (CurrentOperationRows.length > 0) { //to make sure it has charges
            /***************************Print Operation Header******************************/
            //debugger;
            pTablesHTML += '                         <table id="tblProfitStatistics' + pCurrentOperationID + '" class="table table-striped text-sm table-bordered print " style="border:solid #999 !important;">';//remove t1 class to remove row numbers

            pTablesHTML += '                             <tbody>';
            pTablesHTML += '                                 <tr style="background-color:whitesmoke;font-size:95%;">';
            pTablesHTML += '                                     <td colspan=6 style="text-align:left;">';
            pTablesHTML += '                                        <u>Oper.Code:</u>' + CurrentOperationRows[0].OperationCode;
            //pTablesHTML += '                                        &emsp;<u>Oper.Date:</u>' + ConvertDateFormat(GetDateWithFormatMDY(CurrentOperationRows[0].OpenDate));
            if ((CurrentOperationRows[0].BookingPartyName != 0 && CurrentOperationRows[0].BookingPartyName != null))
                pTablesHTML += '                                    &emsp;<u>Booking Party:</u>' + CurrentOperationRows[0].BookingPartyName;
            //if ((CurrentOperationRows[0].ClientName != 0 && CurrentOperationRows[0].ClientName != null))
            //    pTablesHTML += '                                    &emsp;<u>Client:</u>' + CurrentOperationRows[0].ClientName;
            pTablesHTML += '                                        &emsp;POL:' + CurrentOperationRows[0].POLCode;
            pTablesHTML += '                                        &emsp;POD:' + CurrentOperationRows[0].PODCode;
            //if (GetDateWithFormatMDY(CurrentOperationRows[0].ActualDeparture) != "01/01/1900")
            //    pTablesHTML += '                                        &emsp;ATD:' + ConvertDateFormat(GetDateWithFormatMDY(CurrentOperationRows[0].ActualDeparture));
            //if (CurrentOperationRows[0].ContainerTypes != "")
            //    pTablesHTML += '                                    &emsp;Containers:' + CurrentOperationRows[0].ContainerTypes;
            pTablesHTML += '                                    &emsp;ConsigneeName:' + (CurrentOperationRows[0].ConsigneeName == 0 ? "None" : CurrentOperationRows[0].ConsigneeName);
            if (pDefaults.UnEditableCompanyName == "NIL" || pDefaults.UnEditableCompanyName == "EXP" || pDefaults.UnEditableCompanyName == "KDS")
                pTablesHTML += '                                    &emsp;Inv.Nos:' + (CurrentOperationRows[0].InvoiceNumbers == 0 ? "None" : CurrentOperationRows[0].InvoiceNumbers);
            if (pDefaults.UnEditableCompanyName == "NIL" || pDefaults.UnEditableCompanyName == "EXP") {
                pTablesHTML += '                                    &emsp;Inv.Date:' + (CurrentOperationRows[0].FirstInvoiceDate == 0 ? "None" : CurrentOperationRows[0].FirstInvoiceDate);
                pTablesHTML += '                                    &emsp;PO Value:' + (CurrentOperationRows[0].POValue == 0 ? "None" : CurrentOperationRows[0].POValue);
                pTablesHTML += '                                    &emsp;PO Date:' + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(CurrentOperationRows[0].PODate)) > 0 ? (ConvertDateFormat(GetDateWithFormatMDY(CurrentOperationRows[0].PODate)) + '\u2008') : "None");
                pTablesHTML += '                                    &emsp;ShipperName:' + (CurrentOperationRows[0].ShipperName == 0 ? "None" : CurrentOperationRows[0].ShipperName);
                pTablesHTML += '                                    &emsp;Service:' + (CurrentOperationRows[0].MoveTypeName == 0 ? "None" : CurrentOperationRows[0].MoveTypeName);
                pTablesHTML += '                                    &emsp;OperationStageName:' + (CurrentOperationRows[0].OperationStageName == 0 ? "None" : CurrentOperationRows[0].OperationStageName);
            }
            pTablesHTML += '                                     </td>';
            pTablesHTML += '                                     <td></td>';
            pTablesHTML += '                                     <td></td>';
            pTablesHTML += '                                     <td></td>';
            pTablesHTML += '                                     <td></td>';
            pTablesHTML += '                                     <td></td>';
            pTablesHTML += '                                 </tr>';

            pTablesHTML += '                                 <tr class="" style="background-color:whitesmoke;font-size:95%;">';
            //pTablesHTML += '                                     <th>Ser.</th>';
            //pTablesHTML += '                                     <td style="width:20%;">Branch</td>';
            pTablesHTML += '                                     <td style="width:35%;">ChargeType</td>';
            pTablesHTML += '                                     <td style="width:20%;">Est.Cost</td>';
            pTablesHTML += '                                     <td style="width:20%;">Pay.</td>';
            pTablesHTML += '                                     <td style="width:20%;">' + ($("#cbIsUsedInOperationStatement").prop("checked") ? 'Inv.' : 'Rec.') + '</td>';
            pTablesHTML += '                                     <td style="width:20%;">Profit</td>';
            pTablesHTML += '                                     <td style="width:5%;">Cur</td>';
            pTablesHTML += '                                 </tr>';
            var serial = 0;
            $.each((CurrentOperationRows), function (i, item) {
                //debugger;
                //pTablesHTML += '                                     <tr class="' + (item.Cargo == "Totals In Default Currency:" ? "font-bold text-ul" : "") + '" style="font-size:95%; ' + (item.Cargo == "Total:" ? "background-color: #f9f9f9;!Important" : "") + '">' + (item.Code == "Total" ? "<b>" : "");
                pTablesHTML += '                                     <tr class="' + '" style="font-size:95%;">';
                //pTablesHTML += '                                         <td>' + ++serial + '</td>';
                //pTablesHTML += '                                         <td>' + item.BranchName + '</td>';
                pTablesHTML += '                                         <td>' + item.ChargeTypeName
                    //+ (item.PaymentDate != undefined && Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.PaymentDate)) > 0 ? (" ( *PaymentDate" + ConvertDateFormat(GetDateWithFormatMDY(item.PaymentDate)) + ")") : "")
                    + ((pDefaults.UnEditableCompanyName == "ILS" || pDefaults.UnEditableCompanyName == "ILSEG") && item.PayableDate != undefined && Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.PayableDate)) > 0 ? (" ( *PayableDate:" + (ConvertDateFormat(GetDateWithFormatMDY(item.PayableDate)) + '\u2008') + ")") : "")
                    //+ (item.InvoiceDate != undefined && Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.InvoiceDate)) > 0 ? (" ( *InvoiceDate:" + ConvertDateFormat(GetDateWithFormatMDY(item.InvoiceDate)) + ")") : "") + '</td>';
                pTablesHTML += '                                         <td class="QuotationCost' + item.CurrencyCode + '">' + item.QuotationCost.toFixed(2) + '</td>';
                pTablesHTML += '                                         <td class="Payable' + item.CurrencyCode + '">' + ($("#cbWithVAT").prop("checked") ? item.PayablesWithVAT.toFixed(2) : item.PayablesWithoutVAT.toFixed(2)) + '</td>';
                pTablesHTML += '                                         <td class="Receivable' + item.CurrencyCode + '">' + ($("#cbWithVAT").prop("checked") ? item.ReceivablesWithVAT.toFixed(2) : item.ReceivablesWithoutVAT.toFixed(2)) + '</td>';
                pTablesHTML += '                                         <td class="Profit' + item.CurrencyCode + '">' + ($("#cbWithVAT").prop("checked") ? (item.ReceivablesWithVAT - item.PayablesWithVAT).toFixed(2) : (item.ReceivablesWithoutVAT - item.PayablesWithoutVAT).toFixed(2)) + '</td>';
                pTablesHTML += '                                         <td>' + item.CurrencyCode + '</td>';
                pTablesHTML += '                                     </tr>';
            });
            pTablesHTML += '                             </tbody>';
            pTablesHTML += '                         </table>';
        } //if (CurrentOperationRows.length > 0)
    } //for (var z = 0; z < ArrOperationIDs.length; z++) {

    $("#hExportedTable").html(pTablesHTML); //in all cases i put the tables in hidden div so i select each table separately
    /******************************Get Tables Summary*********************************/
    for (var z = 0; z < ArrOperationIDs.length; z++) {
        var pQuotationCostEGPSum = 0; var pQuotationCostUSDSum = 0; var pQuotationCostEURSum = 0; var pQuotationCostGBPSum = 0; var pQuotationCostSARSum = 0;
        var pPayableEGPSum = 0; var pPayableUSDSum = 0; var pPayableEURSum = 0; var pPayableGBPSum = 0; var pPayableSARSum = 0;
        var pReceivableEGPSum = 0; var pReceivableUSDSum = 0; var pReceivableEURSum = 0; var pReceivableGBPSum = 0; var pReceivableSARSum = 0;
        var pProfitEGPSum = 0; var pProfitUSDSum = 0; var pProfitEURSum = 0; var pProfitGBPSum = 0; var pProfitSARSum = 0;

        pQuotationCostEGPSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "QuotationCostEGP");
        pQuotationCostUSDSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "QuotationCostUSD");
        pQuotationCostEURSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "QuotationCostEUR");
        pQuotationCostGBPSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "QuotationCostGBP");
        pQuotationCostSARSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "QuotationCostSAR");

        pPayableEGPSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "PayableEGP");
        pPayableUSDSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "PayableUSD");
        pPayableEURSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "PayableEUR");
        pPayableGBPSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "PayableGBP");
        pPayableSARSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "PayableSAR");

        pReceivableEGPSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "ReceivableEGP");
        pReceivableUSDSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "ReceivableUSD");
        pReceivableEURSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "ReceivableEUR");
        pReceivableGBPSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "ReceivableGBP");
        pReceivableSARSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "ReceivableSAR");

        pProfitEGPSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "ProfitEGP");
        pProfitUSDSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "ProfitUSD");
        pProfitEURSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "ProfitEUR");
        pProfitGBPSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "ProfitGBP");
        pProfitSARSum = GetColumnSum("tblProfitStatistics" + ArrOperationIDs[z], "ProfitSAR");
        var pTableSummary = "";
        pTableSummary += '         <tr class="font-bold" style="font-size:95%;">';
        pTableSummary += '             <td colspan=1><u>' + "Total" + '</u></td>';
        pTableSummary += '             <td class="QuotationCostSummary">'
            + (pQuotationCostEGPSum != 0 ? (pQuotationCostEGPSum.toFixed(2) + 'EGP ') : "")
            + (pQuotationCostUSDSum != 0 ? (pQuotationCostUSDSum.toFixed(2) + 'USD ') : "")
            + (pQuotationCostEURSum != 0 ? (pQuotationCostEURSum.toFixed(2) + 'EUR ') : "")
            + (pQuotationCostGBPSum != 0 ? (pQuotationCostGBPSum.toFixed(2) + 'GBP ') : "")
            + (pQuotationCostSARSum != 0 ? (pQuotationCostSARSum.toFixed(2) + 'SAR ') : "") + '</td>';
        pTableSummary += '             <td class="PayableSummary">'
            + (pPayableEGPSum != 0 ? (pPayableEGPSum.toFixed(2) + 'EGP ') : "")
            + (pPayableUSDSum != 0 ? (pPayableUSDSum.toFixed(2) + 'USD ') : "")
            + (pPayableEURSum != 0 ? (pPayableEURSum.toFixed(2) + 'EUR ') : "")
            + (pPayableGBPSum != 0 ? (pPayableGBPSum.toFixed(2) + 'GBP ') : "")
            + (pPayableSARSum != 0 ? (pPayableSARSum.toFixed(2) + 'SAR ') : "") + '</td>';
        pTableSummary += '             <td class="ReceivableSummary">'
            + (pReceivableEGPSum != 0 ? (pReceivableEGPSum.toFixed(2) + 'EGP ') : "")
            + (pReceivableUSDSum != 0 ? (pReceivableUSDSum.toFixed(2) + 'USD ') : "")
            + (pReceivableEURSum != 0 ? (pReceivableEURSum.toFixed(2) + 'EUR ') : "")
            + (pReceivableGBPSum != 0 ? (pReceivableGBPSum.toFixed(2) + 'GBP ') : "")
            + (pReceivableSARSum != 0 ? (pReceivableSARSum.toFixed(2) + 'SAR ') : "") + '</td>';
        pTableSummary += '             <td class="ProfitSummary">'
            + (pProfitEGPSum != 0 ? (pProfitEGPSum.toFixed(2) + 'EGP ') : "")
            + (pProfitUSDSum != 0 ? (pProfitUSDSum.toFixed(2) + 'USD ') : "")
            + (pProfitEURSum != 0 ? (pProfitEURSum.toFixed(2) + 'EUR ') : "")
            + (pProfitGBPSum != 0 ? (pProfitGBPSum.toFixed(2) + 'GBP ') : "")
            + (pProfitSARSum != 0 ? (pProfitSARSum.toFixed(2) + 'SAR ') : "") + '</td>';
        pTableSummary += '             <td>' + '' + '</td>';
        pTableSummary += '         </tr>';
        $("#tblProfitStatistics" + ArrOperationIDs[z] + " tbody").append(pTableSummary);
    }
    if (pOutputTo == "Excel") {
        var pExcelTable = "";
        pExcelTable += '                         <table id="tblProfitStatisticsExcel" class="table table-striped text-sm table-bordered print " style="border:solid #999 !important;">';//remove t1 class to remove row numbers
        pExcelTable += '                             <thead>';
        pExcelTable += '                                 <tr class="" style="background-color:whitesmoke;font-size:95%;">';
        //pExcelTable += '                                     <th>Ser.</th>';
        //pExcelTable += '                                     <th style="width:20%;"></th>';
        pExcelTable += '                                     <th style="width:35%;"></th>';
        pExcelTable += '                                     <th style="width:20%;"></th>';
        pExcelTable += '                                     <th style="width:20%;"></th>';
        pExcelTable += '                                     <th style="width:20%;"></th>';
        pExcelTable += '                                     <th style="width:20%;"></th>';
        pExcelTable += '                                     <th style="width:5%;"></th>';
        pExcelTable += '                                 </tr>';
        pExcelTable += '                             </thead>';
        pExcelTable += '                             <tbody>';
        pExcelTable += '                             </tbody>';
        pExcelTable += '                        </table>';
        debugger;
        $("#hExportedTable").append(pExcelTable);
        for (var z = 0; z < ArrOperationIDs.length; z++) {
            $("#tblProfitStatisticsExcel tbody").append($("#tblProfitStatistics" + ArrOperationIDs[z] + " tbody").html());
            $("#tblProfitStatisticsExcel tbody").append("<tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
        }
        $("#tblProfitStatisticsExcel tbody").append("<tr><td>Total Payables : </td><td>" + pPayablesCurrenciesSummary + "</td><td></td><td></td><td></td><td></td></tr>");
        $("#tblProfitStatisticsExcel tbody").append("<tr><td>Total " + ($("#cbIsUsedInOperationStatement").prop("checked") ? "Invoices" : "Receivables") + " : </td><td>" + pReceivablesOrInvoicesCurrenciesSummary + "</td><td></td><td></td><td></td><td></td></tr>");
        $("#tblProfitStatisticsExcel tbody").append("<tr><td>Total Profit : </td><td>" + pProfitCurrenciesSummary + "</td><td></td><td></td><td></td><td></td></tr>");
        ExportHtmlTableToCsv_RemovingCommasForNumbers("tblProfitStatisticsExcel", "Profit Statistics");
    }
}
function ProfitStatistics_OperationStatusChanged() {
    debugger;
    if ($("#slOperationStages option:selected").text() == "CLOSED")
        $(".classShowForClosed").removeClass("hide");
    else
        $(".classShowForClosed").addClass("hide");
}
function ProfitStatistics_SelectColumns() {
    jQuery("#ModalSelectColumns").modal("show");
}
function ProfitStatistics_SelectOperations() {
    jQuery("#CheckboxesListModal").modal("show");
}
function ProfitStatistics_ReportOptionChanged() {
    debugger;
    if ($("#cbIncludeChargeDetails").prop("checked")) {
        $(".classShowForIncludeChargeDetails").removeClass("hide");
    }
    else {
        $(".classShowForIncludeChargeDetails").addClass("hide");
        $("#slPartner").val("");
        $("#txtSupplierInvoiceNumber").val("");
        $("#slCurrency").val("");
        $("#cbIsShowSupplierData").prop("checked", false);
        $("#cbIsOfficial").prop("checked", false);
    }
}
function OperationsStatistics_FilterOperationsModal() {
    debugger;
    FadePageCover(true);
    var pWhereClause = "WHERE BLType<>2 \n";
    if ($("#txtSearchItems").val().trim() != "")
        pWhereClause += "AND SUBSTRING(Code,12,10)='" + $("#txtSearchItems").val().trim() + "' \n";
    //2nd parameter in isValidDate: 1-dd/mm/yyyy 2-mm/dd/yyyy
    if (isValidDate($("#txtFromDateSelectOperations").val().trim(), 1) && $("#txtFromDateSelectOperations").val().trim() != "") {
        pWhereClause += " AND OpenDate >= '" + GetDateWithFormatyyyyMMdd($("#txtFromDateSelectOperations").val().trim()) + "'";
        $("#txtFromOpenDate").val($("#txtFromDateSelectOperations").val());
    }
    if (isValidDate($("#txtToDateSelectOperations").val().trim(), 1) && $("#txtToDateSelectOperations").val().trim() != "") {
        pWhereClause += " AND CAST(OpenDate AS date) <= '" + GetDateWithFormatyyyyMMdd($("#txtToDateSelectOperations").val().trim()) + "'";
        $("#txtToOpenDate").val($("#txtToDateSelectOperations").val());
    }

    GetListAsCheckboxesWithVariousParameters("/api/Operations/LoadAll"
        , { pWhereClause: pWhereClause }
        , "divCheckboxesList"
        , "cbAddedItemID"
        , function () { FadePageCover(false); }
        , 2
        , "col-sm-2");
}
function OperationsStatistics_ClearAllOperations() {
    $('input[name="cbAddedItemID"]').prop("checked", false);
}
function InvoicesReports_PartnerTypeChanged() {
    //debugger;
    //$("#slPartner").val("");
    //$("#slPartner option").removeClass("hide");
    //if ($("#slPartnerType").val() != "") //handle show all partners
    //    $("#slPartner option[PartnerTypeID!=" + $("#slPartnerType").val() + "][value!=''" + "]").addClass("hide");
    debugger;
    $("#slPartner").html("<option value=''>" + TranslateString("SelectFromMenu") + "</option>");//to quickly empty
    if ($("#slPartnerType").val() != "") {
        if ($("#slPartnerType").val() == constCustomerPartnerTypeID) {
            $("#slPartner").html($("#hReadySlCustomers").html());
        }
        else {
            FadePageCover(true);
            CallGETFunctionWithParameters("/api/InvoicesReports/FillPartners", { pPartnerTypeID: $("#slPartnerType").val() }
                , function (pData) {
                    FillListFromObject(null, 2/*pCodeOrName*/, "<--All-->", "slPartner", pData[0], null);
                    FadePageCover(false);
                }
                , null);
        }
    }
}

function cbCheckAllChargesTypesChanged() {
    debugger;
    if ($("#cbCheckAllChargesTypes").prop("checked"))
        $("#divCbChargesTypes input[name=nameCbChargesTypes]").prop("checked", true);
    else
        $("#divCbChargesTypes input[name=nameCbChargesTypes]").prop("checked", false);
}