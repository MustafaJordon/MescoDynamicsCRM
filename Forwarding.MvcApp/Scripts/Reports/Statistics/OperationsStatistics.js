//1: Excel File
//2: Excel File Without Formatting
//3: PDF File
//4: Rich Text Format Document
//5: Word Document
function OperationsStatistics_Initialize() {
    debugger;
    LoadView("/Reports/OperationsStatistics", "div-content", function () {
        var _TodaysDate = getTodaysDateInddMMyyyyFormat();
        if (glbCallingControl == "OperationsStatistics") {
            $("#liFormName").text("Operations Statistics");
            //$("#liTabName").attr("onclick", "LoadViews('Invoicing')");
            $(".classShowForOperationsStatistics").removeClass("hide");
            if (pDefaults.UnEditableCompanyName == "NIL") //to show Courier(i.e. MainRoute Notes) if not NIL
                $(".classShowForNIL").removeClass("hide");


            if (pLoggedUser.IsSalesman == true && pLoggedUser.IsHideOthersRecords == true)
                $(".classShowForUnSalemen").addClass("hide");
            else
                $(".classShowForUnSalemen").removeClass("hide");
            if (pDefaults.UnEditableCompanyName != "MED")
                $("#txtFromDate").val(_TodaysDate);
        }
        else { //BLStatistics
            $("#txtFromDate").val("01/01/2020");
            $("#liFormName").text("HBLs Statistics");
            //$("#liTabName").attr("onclick", "LoadViews('Warehousing')");
            $(".classShowForBLStatistics").removeClass("hide");
            $("#slNotify").html($("#hReadySlCustomers").html());
        }

        if (pDefaults.UnEditableCompanyName == "MED") {
            SetSelectedColumns(false);
            $("#cbClient").prop("checked", true);
            $("#cbShipper").prop("checked", true);
            $("#cbConsignee").prop("checked", true);
            $("#cbLine").prop("checked", true);
            $("#cbCommodity").prop("checked", true);

            $("#cbContainerTypes").prop("checked", true);
            $("#cbPackages").prop("checked", true);
            $("#cbInvoices").prop("checked", true);
            $("#cbMBL").prop("checked", true);
            $("#cbPOL").prop("checked", true);

            $("#cbPOD").prop("checked", true);
            $("#cbATDPOL").prop("checked", true);
            $("#cbATAPOD").prop("checked", true);
            $("#cbShipmentType").prop("checked", true);
            $("#cbACIDNo").prop("checked", true);

            $("#cbReceivingDate").prop("checked", true);
            $("#cbDODate").prop("checked", true);
            $("#cbFreeTime").prop("checked", true);
            $("#cbNotes").prop("checked", true);
        }

        if ($("#hf_CanEdit").val() == 1) {
            $("#btn-ExportToExcel_WithCurrencies").removeClass("hide");
            $("#btn-ExportToExcel_WithCurrencies-FromSelectOperations").removeClass("hide");
            $("#btn-ExportToExcel_WithCurrencies-FromSelect").removeClass("hide");
            $("#cbReceivables").parent().parent().removeClass("hide");
            $("#cbPayables").parent().parent().removeClass("hide");
            //$("#cbReceivablesWithCurrencies").parent().parent().removeClass("hide");
            //$("#cbPayablesWithCurrencies").parent().parent().removeClass("hide");
            $("#cbProfit").parent().parent().removeClass("hide");
            $("#cbMargin").parent().parent().removeClass("hide");
            $("#cbInvoices").parent().parent().removeClass("hide");
        } else {
            $("#btn-ExportToExcel_WithCurrencies").addClass("hide");
            $("#btn-ExportToExcel_WithCurrencies-FromSelectOperations").addClass("hide");
            $("#btn-ExportToExcel_WithCurrencies-FromSelect").addClass("hide");
            $("#cbReceivables").prop("checked", false);
            $("#cbPayables").prop("checked", false);
            //$("#cbReceivablesWithCurrencies").prop("checked", false);
            //$("#cbPayablesWithCurrencies").prop("checked", false);
            $("#cbProfit").prop("checked", false);
            $("#cbMargin").prop("checked", false);
            $("#cbInvoices").prop("checked", false);
        }


        CallGETFunctionWithParameters("/api/OperationsStatistics/GetOperationsStatisticsFilter", null
            , function (data) {
                //data[0]:Users //data[1]:Branches //data[2]:Customers //data[3]:Currencies //data[4]:Oper.States
                debugger;
                //FillListFromObject(pID, pCodeOrName/*1-Code 2-Name*/, pStrFirstRow, pSlName, pData, callback)

                var _Salesmentemp = JSON.parse(data[0]);
                var pSalesmen = jQuery.grep(_Salesmentemp, function (_Salesmentemp) {
                    return _Salesmentemp.IsSalesman == true;
                });

                FillListFromObject(null, 2, TranslateString("SelectFromMenu"), "slSalesman", JSON.stringify(pSalesmen), null);
                FillListFromObject(null, 2, TranslateString("SelectFromMenu"), "slCreator", data[0], null);
                FillListFromObject(null, 2, TranslateString("SelectFromMenu"), "slOperationMan", data[0], null);
                FillListFromObject(null, 2, TranslateString("SelectFromMenu"), "slBranch", data[1], null);
                FillListFromObject(null, 2, TranslateString("SelectFromMenu"), "slCountryPOL", data[12]
                    , function () { $("#slCountryPOD").html($("#slCountryPOL").html()); });
                //FillListFromObject(null, 2, "All Customers", "slCustomer", data[2], null);
                if (pDefaults.UnEditableCompanyName == "GBL")
                    CallGETFunctionWithParameters("/api/Customers/LoadAllWithMinimalColumns"
                        , { pWhereClauseWithMinimalColumns: "WHERE 1=1 ", pOrderBy: "Name" }
                        , function (pData) {
                            FillListFromObject(null, 2, TranslateString("SelectFromMenu"), "slCustomer", pData[0], null);
                            //Fill_SelectInputAfterLoadData_WithMultiAttrAndMultiFields(pData[0], "ID", "Name", '', TranslateString("SelectFromMenu"), "#hReadySlCustomers", null, "IsInactive", null);
                        }
                        , null);
                else
                    $("#slCustomer").html($("#hReadySlCustomers").html());
                //FillListFromObject(null, 2, "All Booking Parties", "slBookingParty", data[2], null);
                $("#slBookingParty").html($("#hReadySlCustomers").html());
                //GetListYears(null, null, "slYearsOperationStatistics", null,null);
                FillListFromObject(null, 1, "All Currencies", "slCurrency", data[3], null);
                FillListFromObject(null, 2, TranslateString("SelectFromMenu"), "slOperationStages", data[4], null);
                FillListFromObject(null, 2, TranslateString("SelectFromMenu"), "slShippingLine", data[5], null);
                FillListFromObject(null, 2, TranslateString("SelectFromMenu"), "slVessel", data[6], null);
                FillListFromObject(null, 2, TranslateString("SelectFromMenu"), "slTrackingStage", data[7], null);

                let CodeOrNameforslCommodities = 4;     // NameAndCode
                FillListFromObject(null, CodeOrNameforslCommodities, TranslateString("SelectFromMenu"), "slCommodity", data[8], null);
                $("#slCommodity").css({ "width": "100%" }).select2();
                $("#slCommodity").trigger("change");
                $("div[tabindex='-1']").removeAttr('tabindex');

                FillListFromObject(null, 2, TranslateString("SelectFromMenu"), "slAgent", data[9], null);
                FillListFromObject(null, 2, TranslateString("SelectFromMenu"), "slNetwork", data[10], null);
                // For Filter By Name 
                Fill_SelectInputAfterLoadData(data[10], "Name", "Name", TranslateString("SelectFromMenu"), "#slCustomerNetwork", null)


                FillListFromObject(null, 2, TranslateString("SelectFromMenu"), "slMoveType", data[11], null);
                FillListFromObject(null, 2, TranslateString("SelectFromMenu"), "slCustomsClearanceAgent", data[13], null);
                FillListFromObject(null, 2, TranslateString("SelectFromMenu"), "slFilterTrucker", data[14], null);
                FillListFromObject(null, 2, TranslateString("SelectFromMenu"), "slAirline", data[16], null);
                // CTruckers objCTruckers = new CTruckers();
                if (pDefaults.UnEditableCompanyName != "MED")
                    $("#txtToDate").val(_TodaysDate);
                $("#txtFromCloseDate").val(_TodaysDate);
                $("#txtToCloseDate").val(_TodaysDate);
                $("#txtFromDateSelectOperations").val(_TodaysDate);
                $("#txtToDateSelectOperations").val(_TodaysDate);
                $("#txtFromInvoiceDate").val(_TodaysDate);
                $("#txtToInvoiceDate").val(_TodaysDate);
            }
            , function () { FadePageCover(false); $("#hl-menu-Reports").parent().addClass("active"); }
        );
        if (pDefaults.UnEditableCompanyName == "KDM")
            $("#spanKDMReference").text("Ref.No.");
        else if (pDefaults.UnEditableCompanyName == "GLS")
            $("#spanCbPODate").text("Storing Date");
    });
}
function OperationsStatistics_Print(pOutputTo, IsWithCurrenciesDetailed) {
    debugger;
    if (
        ($("#txtFromInvoiceDate").val().trim() == "" || isValidDate($("#txtFromInvoiceDate").val(), 1))
        && ($("#txtToInvoiceDate").val().trim() == "" || isValidDate($("#txtToInvoiceDate").val(), 1))
        && ($("#txtFromDate").val().trim() == "" || isValidDate($("#txtFromDate").val(), 1))
        && ($("#txtToDate").val().trim() == "" || isValidDate($("#txtToDate").val(), 1))
        && ($("#txtFromCloseDate").val().trim() == "" || isValidDate($("#txtFromCloseDate").val(), 1))
        && ($("#txtToCloseDate").val().trim() == "" || isValidDate($("#txtToCloseDate").val(), 1))
    ) {
        FadePageCover(true);
        var pWhereClause = OperationsStatistics_GetFilterWhereClause();
        //if ($('#ulReportTypes .active').val() == 0)
        //    swal(strSorry, "Please, Select a report type.");
        //else {
        var pParametersWithValues = {
            pWhereClause: pWhereClause
            , pIsFilterInvoices: $("#cbFilterInvoices").prop("checked")
            , pFromInvoiceDate: GetDateWithFormatyyyyMMdd($("#txtFromInvoiceDate").val().trim())
            , pToInvoiceDate: GetDateWithFormatyyyyMMdd($("#txtToInvoiceDate").val().trim()) + " 23:59:59"
            , pIsWithCurrenciesDetailed: IsWithCurrenciesDetailed
            //, pDirectionType: ($("#lbl-filter-import").hasClass('active') ? "Import"
            //    : $("#lbl-filter-export").hasClass('active') ? "Export"
            //    : $("#lbl-filter-domestic").hasClass('active') ? "Domestic"
            //    : "ALL")
            //, pTransPortType: ($("#lbl-filter-ocean").hasClass('active') ? "Ocean"
            //    : $("#lbl-filter-air").hasClass('active') ? "Air"
            //    : $("#lbl-filter-inland").hasClass('active') ? "Inland"
            //    : "ALL")
        };
        CallGETFunctionWithParameters("/api/OperationsStatistics/LoadData"
            , pParametersWithValues
            , function (data) {
                if (data[0]) //pRecordsExist
                {
                    if (glbCallingControl == "BLStatistics")
                        BLStatistics_DrawReport(data, pOutputTo);
                    else if ($("#cbIsWorkFlow").prop("checked"))
                        OperationsStatistics_WorkFlow_DrawReport(data, pOutputTo);
                    else
                        OperationsStatistics_DrawReport(data, pOutputTo, IsWithCurrenciesDetailed);
                }
                else
                    swal(strSorry, "Please, No records exist for that search criteria.");
                FadePageCover(false);
            });
        //}
    }
    else
        swal(strSorry, "Please make sure that date format is dd/MM/yyyy.");
}
function OperationsStatistics_GetFilterWhereClause() {
    debugger;
    //var pWhereClause = "";
    var pWhereClause = glbCallingControl == "OperationsStatistics" ? "WHERE BLType<>2 " : "WHERE BLType=2 AND MasterOperationID IS NOT NULL ";
    var pTransportFilter = "";
    var pDirectionFilter = "";
    var pBLTypeFilter = "";
    var pSalesmanFilter = "";
    var pCreatorFilter = "";
    var pBranchFilter = "";

    var pCountryPOLFilter = "";
    var pPOLFilter = "";
    var pCountryPODFilter = "";
    var pPODFilter = "";
    var pMatchFilter = "";
    var pInvoiceNumbersFilter = "";
    var pCustomerReferenceFilter = "";
    var pPONumberFilter = "";
    var pGrossWeightFilter = "";
    var pCertificateNumberFilter = "";
    var pQasimaNumberFilter = "";
    var pForm13NumberFilter = "";
    var pMainRouteNotes = "";
    var pSupplierReferenceFilter = "";
    var pMasterBLFilter = "";
    var pHouseNumberFilter = "";
    var pVoyageOrTruckNumberFilter = "";
    var pBookingNumbersFilter = "";
    var pCommodityFilter = "";
    var pShippingLineFilter = "";
    var pAirlineFilter = "";
    var pVesselFilter = "";
    var pShipmentTypeFilter = "";
    var pTrackingStageFilter = "";
    var pNetworkFilter = "";
    var pMoveTypeFilter = "";
    var pCustomerFilter = "";
    var pAgentFilter = "";
    var pOperationIDsFilter = "";
    var pBookingPartyFilter = "";
    var pCustomsClearanceAgentFilter = "";
    var pFromOpenDateFilter = "";
    var pToOpenDateFilter = "";

    var pFromETAFilter = "";
    var pToETAFilter = "";
    var pFromETDFilter = "";
    var pToETDFilter = "";

    var pFromCloseDateFilter = "";
    var pToCloseDateFilter = "";
    var pPOL = "";
    var pPOD = "";
    var pOperationsWOInvoicesFilter = "";
    //var pOperationStageFilter = ($("#slOperationStages").val() == 0
    //                            ? "" //if 0 then all stages
    //                            : ($("#slOperationStages").val() == ClosedQuoteAndOperStageID.toString() ? (" (CAST(CloseDate AS date) <= GETDATE() AND OperationStageID <> " + /*This is to handle case of auto close*/CancelledQuoteAndOperStageID.toString() + ") ") : (" OperationStageID = " + $("#slOperationStages").val() + " AND CloseDate > GETDATE() "))
    //                            );
    //var pOperationStageFilter =
    //    ($("#slOperationStages").val() == 0
    //    ? "" //if 0 then all status
    //    : ($("#slOperationStages").val() == ClosedQuoteAndOperStageID.toString()
    //        ? " OperationStageName=N'CLOSED' "//(" (CAST(CloseDate AS date) <= GETDATE() AND OperationStageID <> " + /*This is to handle case of auto close*/CancelledQuoteAndOperStageID.toString() + ") ")
    //        : (" OperationStageID = " + $("#slOperationStages").val() + " AND CloseDate > CAST(GETDATE() AS date) ")
    //        )
    //    );
    var pOperationStageFilter =
        ($("#slOperationStages").val() == 0
            ? "" //if 0 then all status
            : " OperationStageName=N'" + $("#slOperationStages option:selected").text() + "'");
    var pModalName = "CheckboxesListModal";
    var pCheckboxNameAttr = "cbAddedItemID";
    var pSelectedItemsIDs = GetAllSelectedIDsAsStringWithNameAttr(pCheckboxNameAttr);
    //pOperationIDsFilter = (pSelectedItemsIDs == "" ? "" : " ID IN (" + pSelectedItemsIDs + ")");

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
        pWhereClause += " AND " + pDirectionFilter;
    else
        if (pDirectionFilter == "" && pTransportFilter != "")
            pWhereClause += " AND " + pTransportFilter;
        else
            if (pDirectionFilter != "" && pTransportFilter != "")
                pWhereClause += " AND " + pDirectionFilter + " AND " + pTransportFilter;

    if (pBLTypeFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pBLTypeFilter;
    else
        if (pBLTypeFilter != "" && pWhereClause == "")
            pWhereClause += " AND " + pBLTypeFilter;

    if (pOperationStageFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pOperationStageFilter;
    else
        if (pOperationStageFilter != "" && pWhereClause == "")
            pWhereClause += " AND " + pOperationStageFilter;

    if ($("#slNotify").val() != "")
        pWhereClause += " AND Notify1Name=N'" + $("#slNotify option:selected").text() + "'" + "\n";

    pBranchFilter = ($("#slBranch").val() == "" ? "" : " BranchID = " + $("#slBranch").val());
    if (pBranchFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pBranchFilter;
    else
        if (pBranchFilter != "" && pWhereClause == "")
            pWhereClause = " WHERE " + pBranchFilter;

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

    if ($("#cbIsMatch").prop("checked")) {
        pMatchFilter = " Match = 1 ";
        if (pWhereClause != "")
            pWhereClause += " AND " + pMatchFilter;
        else
            pWhereClause += " WHERE " + pMatchFilter;
    }
    pInvoiceNumbersFilter = ($("#txtInvoiceNumbers").val().trim() == "" ? "" : " InvoiceNumbers LIKE N'%" + $("#txtInvoiceNumbers").val().trim() + "%'");
    if (pInvoiceNumbersFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pInvoiceNumbersFilter;
    else
        if (pInvoiceNumbersFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pInvoiceNumbersFilter;

    pCustomerReferenceFilter = ($("#txtCustomerReference").val().trim() == "" ? "" : " CustomerReference LIKE N'%" + $("#txtCustomerReference").val().trim() + "%'");
    if (pCustomerReferenceFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pCustomerReferenceFilter;
    else
        if (pCustomerReferenceFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pCustomerReferenceFilter;

    pPONumberFilter = ($("#txtPONumber").val().trim() == "" ? "" : " PONumber LIKE N'%" + $("#txtPONumber").val().trim() + "%'");
    if (pPONumberFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pPONumberFilter;
    else
        if (pPONumberFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pPONumberFilter;

    pGrossWeightFilter = ($("#txtGrossWeight").val().trim() == "" ? "" : " (GrossWeightSum = '" + $("#txtGrossWeight").val().trim() + "' OR GrossWeightTONSum = '" + $("#txtGrossWeight").val().trim() + "')");
    if (pGrossWeightFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pGrossWeightFilter;
    else
        if (pGrossWeightFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pGrossWeightFilter;

    pCertificateNumberFilter = ($("#txtCertificateNumber").val().trim() == "" ? "" : " CertificateNumber LIKE N'%" + $("#txtCertificateNumber").val().trim() + "%'");
    if (pCertificateNumberFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pCertificateNumberFilter;
    else
        if (pCertificateNumberFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pCertificateNumberFilter;

    pQasimaNumberFilter = ($("#txtQasimaNumber").val().trim() == "" ? "" : " QasimaNumber LIKE N'%" + $("#txtQasimaNumber").val().trim() + "%'");
    if (pQasimaNumberFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pQasimaNumberFilter;
    else
        if (pQasimaNumberFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pQasimaNumberFilter;

    pForm13NumberFilter = ($("#txtForm13Number").val().trim() == "" ? "" : " Form13Number LIKE N'%" + $("#txtForm13Number").val().trim() + "%'");
    if (pForm13NumberFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pForm13NumberFilter;
    else
        if (pForm13NumberFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pForm13NumberFilter;

    pMainRouteNotes = ($("#txtMainRouteNotes").val().trim() == "" ? "" : " MainRouteNotes = N'" + $("#txtMainRouteNotes").val().trim() + "'");
    if (pMainRouteNotes != "" && pWhereClause != "")
        pWhereClause += " AND " + pMainRouteNotes;
    else
        if (pMainRouteNotes != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pMainRouteNotes;

    pSupplierReferenceFilter = ($("#txtSupplierReference").val().trim() == "" ? "" : " SupplierReference LIKE N'%" + $("#txtSupplierReference").val().trim() + "%'");
    if (pSupplierReferenceFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pSupplierReferenceFilter;
    else
        if (pSupplierReferenceFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pSupplierReferenceFilter;

    //pOperationsWOInvoicesFilter = ($("#cbOperationsWOInvoices").prop("checked") ? " FirstInvoiceDate IS NULL " : "");
    pOperationsWOInvoicesFilter = ($("#cbOperationsWOInvoices").prop("checked") ? " InvoiceNumbers IS NULL AND OperationStageName <> N'CANCELLED' " : "");
    if (pOperationsWOInvoicesFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pOperationsWOInvoicesFilter;
    else
        if (pOperationsWOInvoicesFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pOperationsWOInvoicesFilter;

    pMasterBLFilter = ($("#txtMasterBL").val().trim() == "" ? "" : " MasterBL LIKE N'%" + $("#txtMasterBL").val().trim() + "%'");
    if (pMasterBLFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pMasterBLFilter;
    else
        if (pMasterBLFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pMasterBLFilter;

    pHouseNumberFilter = ($("#txtHouseNumber").val().trim() == "" ? "" : " HouseNumber = N'" + $("#txtHouseNumber").val().trim() + "'");
    if (pHouseNumberFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pHouseNumberFilter;
    else
        if (pHouseNumberFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pHouseNumberFilter;

    pVoyageOrTruckNumberFilter = ($("#txtVoyageOrTruckNumber").val().trim() == "" ? "" : " VoyageOrTruckNumber LIKE N'%" + $("#txtVoyageOrTruckNumber").val().trim() + "%'");
    if (pVoyageOrTruckNumberFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pVoyageOrTruckNumberFilter;
    else
        if (pVoyageOrTruckNumberFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pVoyageOrTruckNumberFilter;

    pBookingNumbersFilter = ($("#txtBookingNumbers").val().trim() == "" ? "" : " BookingNumbers LIKE N'%" + $("#txtBookingNumbers").val().trim() + "%'");
    if (pBookingNumbersFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pBookingNumbersFilter;
    else
        if (pBookingNumbersFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pBookingNumbersFilter;

    pShippingLineFilter = ($("#slShippingLine").val() == "" ? "" : " ShippingLineID = " + $("#slShippingLine").val());
    if (pShippingLineFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pShippingLineFilter;
    else
        if (pShippingLineFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pShippingLineFilter;

    pAirlineFilter = ($("#slAirline").val() == "" ? "" : " AirlineID = " + $("#slAirline").val());
    if (pAirlineFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pAirlineFilter;
    else
        if (pAirlineFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pAirlineFilter;

    pCommodityFilter = ($("#slCommodity").val() == "" ? "" : " (CommodityID = " + $("#slCommodity").val() + " OR CommodityID2 = " + $("#slCommodity").val() + " OR CommodityID3 = " + $("#slCommodity").val() + ")");

    if (pCommodityFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pCommodityFilter;
    else
        if (pCommodityFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pCommodityFilter;

    pVesselFilter = ($("#slVessel").val() == "" ? "" : " VesselID = " + $("#slVessel").val());
    if (pVesselFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pVesselFilter;
    else
        if (pVesselFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pVesselFilter;

    pShipmentTypeFilter = ($("#slShipmentType").val() == "" ? "" : " ShipmentType = " + $("#slShipmentType").val());
    if (pShipmentTypeFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pShipmentTypeFilter;
    else
        if (pShipmentTypeFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pShipmentTypeFilter;

    pNetworkFilter = ($("#slNetwork").val() == "" ? "" : " NetworkID = " + $("#slNetwork").val());
    if (pNetworkFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pNetworkFilter;
    else
        if (pNetworkFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pNetworkFilter;


    pCustomerNetworkFilter = ($("#slCustomerNetwork").val() == "" || $("#slCustomerNetwork").val() == 0 ? "" : " NetworksNames LIKE'%" + $("#slCustomerNetwork").val() + "%'");
    if (pCustomerNetworkFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pCustomerNetworkFilter;
    else
        if (pCustomerNetworkFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pCustomerNetworkFilter;





    pMoveTypeFilter = ($("#slMoveType").val() == "" ? "" : " MoveTypeID = " + $("#slMoveType").val());
    if (pMoveTypeFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pMoveTypeFilter;
    else
        if (pMoveTypeFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pMoveTypeFilter;

    //14-2-2023 ------ Mustafa Mahmoud: if you choose a specific task
    if ($("#slTrackingStage").val() != "") {
        //Case Clearance is checked
        if ($("#cbIsClearanceTask").prop("checked"))
            pWhereClause += " AND EXISTS (SELECT top 1 (1) FROM vwCustomClearanceTracking OT WHERE OT.TrackingStageID=" + $("#slTrackingStage").val()
                + " AND OT.OperationID = vwOperationsStatistics.ID "
                //Task status whether done or not
                + ($("#slTrackingStageStatus").val() != "" ? (" AND OT.Done=" + $("#slTrackingStageStatus").val()) : "")
                + " ORDER BY OT.ID DESC ) " + " \n";
        //Case Clearance is not checked
        else
            pWhereClause += " AND EXISTS (SELECT top 1 (1) FROM vwOperationTracking OT WHERE OT.TrackingStageID=" + $("#slTrackingStage").val()
                + " AND OT.OperationID = vwOperationsStatistics.ID "
                + ($("#slTrackingStageStatus").val() != "" ? (" AND OT.Done=" + $("#slTrackingStageStatus").val()) : "")
                + " ORDER BY OT.ID DESC ) " + " \n";
    }

    //in case of selecting all tasks
    if ($("#slTrackingStage").val() == "") {
        //Case Clearance is checked
        if ($("#cbIsClearanceTask").prop("checked"))
            pWhereClause += " AND EXISTS (SELECT top 1 (1) FROM vwCustomClearanceTracking OT WHERE OT.OperationID = vwOperationsStatistics.ID "
                //Task status whether done or not
                + ($("#slTrackingStageStatus").val() != "" ? (" AND OT.Done=" + $("#slTrackingStageStatus").val()) : "")
                + " ORDER BY OT.ID DESC ) " + " \n";
        //Case Clearance is not checked
        else
            pWhereClause += ($("#slTrackingStageStatus").val() != "" ?
                (" AND EXISTS (SELECT top 1 (1) FROM vwOperationTracking OT WHERE OT.OperationID = vwOperationsStatistics.ID AND OT.Done=" + $("#slTrackingStageStatus").val() + ") \n")
                : "")
    }

    //pWhereClause += " AND (SELECT TOP 1 OT.TrackingStageID FROM vwOperationTracking OT WHERE  OT.OperationID = vwOperationsStatistics.ID ORDER BY ID DESC) = " + $("#slTrackingStage").val() + " \n";
    //if ($("#slTrackingStage").val() == "" && $("#slTrackingStageStatus").val() != "")
    //    pWhereClause += " AND EXISTS (SELECT top 1 (1) FROM vwOperationTracking OT WHERE OT.OperationID = vwOperationsStatistics.ID AND OT.Done=" + $("#slTrackingStageStatus").val()
    //        //+ ($("#cbIsClearanceTask").prop("checked") ? (" AND IsClearance=" + ($("#cbIsClearanceTask").prop("checked") ? 1 : "")) : "")
    //        + " ORDER BY OT.ID DESC ) " + " \n";

    //if ($("#slTrackingStage").val() == "")
    //    pWhereClause += " AND EXISTS (SELECT top 1 (1) FROM vwOperationTracking OT WHERE OT.OperationID > 1 "
    //    + ($("#slTrackingStageStatus").val() != "" ? (" AND OT.Done=" + $("#slTrackingStageStatus").val()) : "")
    //        + ($("#cbIsClearanceTask").prop("checked") ? (" AND IsClearance=" + ($("#cbIsClearanceTask").prop("checked") ? 1 : "")) : "")
    //        + " ORDER BY OT.ID DESC ) " + " \n";
    console.log(pWhereClause);
    if ($("#txtTankOrFlexiNumber").val() != "")
        pWhereClause += " AND EXISTS (SELECT top 1 (1) FROM OperationContainersAndPackages OCP WHERE vwOperationsStatistics.ID=OCP.OperationID AND OCP.TankOrFlexiNumber=N'" + $("#txtTankOrFlexiNumber").val() + "')" + " \n";


    //-------------

    //if (pLoggedUser.IsSalesman == true && pLoggedUser.IsHideOthersRecords == true && glbCallingControl == "OperationsStatistics" )
    //   pSalesmanFilter = " SalesmanID = " + pLoggedUser.ID + "";
    //else
    pSalesmanFilter = ($("#slSalesman").val() == "" ? "" : " SalesmanID = " + $("#slSalesman").val());


    if (pSalesmanFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pSalesmanFilter;
    else
        if (pSalesmanFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pSalesmanFilter;




    pCreatorFilter = ($("#slCreator").val() == "" ? "" : " CreatorUserID = " + $("#slCreator").val());
    if (pCreatorFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pCreatorFilter;
    else
        if (pCreatorFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pCreatorFilter;

    pOperationManFilter = ($("#slOperationMan").val() == "" ? "" : " OperationManID = " + $("#slOperationMan").val());
    if (pOperationManFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pOperationManFilter;
    else
        if (pOperationManFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pOperationManFilter;

    pCustomerFilter = ($("#slCustomer").val() == "" ? "" : " ClientID = " + $("#slCustomer").val());
    if (pCustomerFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pCustomerFilter;
    else
        if (pCustomerFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pCustomerFilter;

    pAgentFilter = ($("#slAgent").val() == "" ? "" : " AgentID = " + $("#slAgent").val());
    if (pAgentFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pAgentFilter;
    else
        if (pAgentFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pAgentFilter;

    pBookingPartyFilter = ($("#slBookingParty").val() == "" ? "" : " BookingPartyID = " + $("#slBookingParty").val());
    if (pBookingPartyFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pBookingPartyFilter;
    else
        if (pBookingPartyFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pBookingPartyFilter;

    //pCustomsClearanceAgentFilter = ($("#slCustomsClearanceAgent").val() == "" ? "" : " CustomsClearanceAgentID = " + $("#slCustomsClearanceAgent").val());
    //if (pCustomsClearanceAgentFilter != "" && pWhereClause != "")
    //    pWhereClause += " AND " + pCustomsClearanceAgentFilter;
    //else
    //    if (pCustomsClearanceAgentFilter != "" && pWhereClause == "")
    //        pWhereClause += " WHERE " + pCustomsClearanceAgentFilter;


    if ($("#slFilterTrucker").val() != null && $("#slFilterTrucker").val() != "" && pWhereClause !== "")
        pWhereClause += " AND ((ISNULL((SELECT COUNT(op.ID) FROM dbo.OperationPartners AS op WHERE dbo.vwOperationsStatistics.ID = op.OperationID AND op.TruckerID = " + $("#slFilterTrucker").val().trim().toUpperCase() + "), 0)) > 0)";

    if ($("#slCustomsClearanceAgent").val() != null && $("#slCustomsClearanceAgent").val() != "" && pWhereClause !== "")
        pWhereClause += " AND ((ISNULL((SELECT COUNT(op.ID) FROM dbo.OperationPartners AS op WHERE dbo.vwOperationsStatistics.ID = op.OperationID AND op.CustomsClearanceAgentID = " + $("#slCustomsClearanceAgent").val().trim().toUpperCase() + "), 0)) > 0)";


    pslCC_ClearanceTypeFilter = ($("#slCC_ClearanceType").val() == "" ? "" : " CC_ClearanceTypeID = " + $("#slCC_ClearanceType").val());
    if (pslCC_ClearanceTypeFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pslCC_ClearanceTypeFilter;
    else
        if (pslCC_ClearanceTypeFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pslCC_ClearanceTypeFilter;

    if ($("#txtFilterQuotation").val().trim() != "" && pWhereClause !== "") {
        pWhereClause += " AND (QuotationCode like N'%" + $("#txtFilterQuotation").val().trim().toUpperCase() + "%') ";
    }

    //2nd parameter in isValidDate: 1-dd/mm/yyyy 2-mm/dd/yyyy
    if (isValidDate($("#txtFromDate").val().trim(), 1) && $("#txtFromDate").val().trim() != "" && !$("#cbFilterInvoices").prop("checked")) {
        pFromOpenDateFilter = " OpenDate >= '" + GetDateWithFormatyyyyMMdd($("#txtFromDate").val().trim()) + "'";
        if (pFromOpenDateFilter != "" && pWhereClause != "")
            pWhereClause += " AND " + pFromOpenDateFilter;
        else
            if (pFromOpenDateFilter != "" && pWhereClause == "")
                pWhereClause += " WHERE " + pFromOpenDateFilter;
    }
    if (isValidDate($("#txtToDate").val().trim(), 1) && $("#txtToDate").val().trim() != "" && !$("#cbFilterInvoices").prop("checked")) {
        pToOpenDateFilter = " CAST(OpenDate AS date) <= '" + GetDateWithFormatyyyyMMdd($("#txtToDate").val().trim()) + "'";
        if (pToOpenDateFilter != "" && pWhereClause != "")
            pWhereClause += " AND " + pToOpenDateFilter;
        else
            if (pToOpenDateFilter != "" && pWhereClause == "")
                pWhereClause += " WHERE " + pToOpenDateFilter;
    }

    if (isValidDate($("#txtFromDate_ETD").val().trim(), 1) && $("#txtFromDate_ETD").val().trim() != "" && !$("#cbFilterInvoices").prop("checked")) {
        pFromETDFilter = " ExpectedDeparture >= '" + GetDateWithFormatyyyyMMdd($("#txtFromDate_ETD").val().trim()) + "'";
        if (pFromETDFilter != "" && pWhereClause != "")
            pWhereClause += " AND " + pFromETDFilter;
        else
            if (pFromETDFilter != "" && pWhereClause == "")
                pWhereClause += " WHERE " + pFromETDFilter;
    }
    if (isValidDate($("#txtToDate_ETD").val().trim(), 1) && $("#txtToDate_ETD").val().trim() != "" && !$("#cbFilterInvoices").prop("checked")) {
        pToETDFilter = " CAST(ExpectedDeparture AS date) <= '" + GetDateWithFormatyyyyMMdd($("#txtToDate_ETD").val().trim()) + "'";
        if (pToETDFilter != "" && pWhereClause != "")
            pWhereClause += " AND " + pToETDFilter;
        else
            if (pToETDFilter != "" && pWhereClause == "")
                pWhereClause += " WHERE " + pToETDFilter;
    }

    if (isValidDate($("#txtFromDate_ETA").val().trim(), 1) && $("#txtFromDate_ETA").val().trim() != "" && !$("#cbFilterInvoices").prop("checked")) {
        pFromETAFilter = " ExpectedArrival >= '" + GetDateWithFormatyyyyMMdd($("#txtFromDate_ETA").val().trim()) + "'";
        if (pFromETAFilter != "" && pWhereClause != "")
            pWhereClause += " AND " + pFromETAFilter;
        else
            if (pFromETAFilter != "" && pWhereClause == "")
                pWhereClause += " WHERE " + pFromETAFilter;
    }
    if (isValidDate($("#txtToDate_ETA").val().trim(), 1) && $("#txtToDate_ETA").val().trim() != "" && !$("#cbFilterInvoices").prop("checked")) {
        pToETAFilter = " CAST(ExpectedArrival AS date) <= '" + GetDateWithFormatyyyyMMdd($("#txtToDate_ETA").val().trim()) + "'";
        if (pToETAFilter != "" && pWhereClause != "")
            pWhereClause += " AND " + pToETAFilter;
        else
            if (pToETAFilter != "" && pWhereClause == "")
                pWhereClause += " WHERE " + pToETAFilter;
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

    //pPOL = ($("#slPOL option:selected").val() == "" ? "" : " POL = " + $("#slPOL option:selected").val());
    //if (pPOL != "" && pWhereClause != "")
    //    pWhereClause += " AND " + pPOL;
    //else
    //    if (pPOL != "" && pWhereClause == "")
    //        pWhereClause += " WHERE " + pPOL;
    //pPOD = ($("#slPOD option:selected").val() == "" ? "" : " POD = " + $("#slPOD option:selected").val());
    //if (pPOD != "" && pWhereClause != "")
    //    pWhereClause += " AND " + pPOD;
    //else
    //    if (pPOD != "" && pWhereClause == "")
    //        pWhereClause += " WHERE " + pPOD;

    //pOperationIDsFilter = (pSelectedItemsIDs == "" ? "" : " ID IN (" + pSelectedItemsIDs + ") OR (MasterOperationID IN (" + pSelectedItemsIDs + ") " + (pOperationStageFilter == "" ? "" : " AND " + pOperationStageFilter) + " )");
    pOperationIDsFilter = (pSelectedItemsIDs == "" ? "" : " ID IN (" + pSelectedItemsIDs + ")" + (pOperationStageFilter == "" ? "" : " AND " + pOperationStageFilter));
    if (pOperationIDsFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pOperationIDsFilter;
    else
        if (pOperationIDsFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pOperationIDsFilter;

    if (isValidDate($("#txtFromInvoiceDate").val().trim(), 1) && $("#txtFromInvoiceDate").val().trim() != ""
        && isValidDate($("#txtToInvoiceDate").val().trim(), 1) && $("#txtToInvoiceDate").val().trim() != ""
        && $("#cbFilterInvoices").prop("checked"))
        pWhereClause += " AND vwOperationsStatistics.ID IN (SELECT ISNULL(vwInvoices.MasterOperationID,vwInvoices.OperationID) FROM vwInvoices WHERE IsDeleted=0 AND CAST(vwInvoices.InvoiceDate AS DATE) BETWEEN '" + GetDateWithFormatyyyyMMdd($("#txtFromInvoiceDate").val().trim()) + "' AND '" + GetDateWithFormatyyyyMMdd($("#txtToInvoiceDate").val().trim()) + " 23:59:59') " + " \n";
    //if (isValidDate($("#txtToInvoiceDate").val().trim(), 1) && $("#txtToInvoiceDate").val().trim() != "" && $("#cbFilterInvoices").prop("checked"))
    //    pWhereClause += " AND vwOperationsStatistics.ID IN (SELECT ISNULL(vwInvoices.MasterOperationID,vwInvoices.OperationID) FROM vwInvoices WHERE IsDeleted=0 AND CAST(vwInvoices.InvoiceDate AS DATE) <= '" + GetDateWithFormatyyyyMMdd($("#txtToInvoiceDate").val().trim()) + " 23:59:59') " + " \n";

    pWhereClause = (pWhereClause == "" ? " WHERE (1=1) " : pWhereClause);// + " ORDER BY ID DESC ";
    return pWhereClause;
}
function OperationsStatistics_ClearPorts(pPortControlName) {
    debugger;
    $("#" + pPortControlName).html("<option value=''><--All--></option>");
}
function OperationsStatistics_RefereshPorts(pCountryControlName, pPortControlName) {
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
function OperationsStatistics_OperationStatusChanged() {
    debugger;
    if ($("#slOperationStages option:selected").text() == "CLOSED")
        $(".classShowForClosed").removeClass("hide");
    else
        $(".classShowForClosed").addClass("hide");
}
function OperationsStatistics_SelectColumns() {

    if (glbCallingControl == "OperationsStatistics")
        jQuery("#ModalSelectColumns").modal("show"); //Operations Statistics
    else if (glbCallingControl == "BLStatistics")
        jQuery("#ModalSelectColumns_BLStatistics").modal("show");  //HBL Statistics
}
function OperationsStatistics_SelectOperations() {
    debugger;
    jQuery("#CheckboxesListModal").modal("show");
}
function OperationsStatistics_ClearAllOperations() {
    debugger;
    $('input[name="cbAddedItemID"]').prop("checked", false);
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
        $("#txtFromDate").val($("#txtFromDateSelectOperations").val());
    }
    if (isValidDate($("#txtToDateSelectOperations").val().trim(), 1) && $("#txtToDateSelectOperations").val().trim() != "") {
        pWhereClause += " AND CAST(OpenDate AS date) <= '" + GetDateWithFormatyyyyMMdd($("#txtToDateSelectOperations").val().trim()) + "'";
        $("#txtToDate").val($("#txtToDateSelectOperations").val());
    }

    GetListAsCheckboxesWithVariousParameters("/api/Operations/LoadAll"
        , { pWhereClause: pWhereClause }
        , "divCheckboxesList"
        , "cbAddedItemID"
        , function () { FadePageCover(false); }
        , 2
        , "col-sm-2");
}
function OperationsStatistics_cbIsWorkFlowChanged() {
    debugger;
    if ($("#cbIsWorkFlow").prop("checked"))
        $("#btn-SelectColumns").addClass("hide");
    else
        $("#btn-SelectColumns").removeClass("hide");
}
function OperationsStatistics_cbFilterInvoicesChanged() {
    debugger;
    if ($("#cbFilterInvoices").prop("checked"))
        $(".classShowForFilterInvoices").removeClass("hide");
    else {
        $(".classShowForFilterInvoices").addClass("hide");
    }
}
function OperationsStatistics_DrawReport(data, pOutputTo, IsWithCurrenciesDetailed) {
    debugger;
    var pReportRows = JSON.parse(data[1]);
    var pContainerTotals = data[2];
    var pIncotermTotals = data[3];
    var pReportRowsWithDetailedCurrencies = "CurrenciesNotSelected";
    if (IsWithCurrenciesDetailed) {
        pReportRowsWithDetailedCurrencies = JSON.parse(data[4]);
    }


    var pReportTitle = "Operations Statistics";
    var TodaysDateddMMyyyy = getTodaysDateInddMMyyyyFormat();
    var _BranchIDs = "";
    for (var i = 1; i < ($("#slBranch option").length + 1); i++)
        _BranchIDs += _BranchIDs == "" ? $("#slBranch option:nth-child(" + (i) + ")").val() : ("," + $("#slBranch option:nth-child(" + (i) + ")").val());
    var ArrBranchIDs = _BranchIDs.split(",");

    var pTablesHTML = "";
    //ReportHTML += '                         <table id="tblOperationsStatistics" class="table table-striped text-sm table-hover b-t b-r b-b b-l print">';//remove t1 class to remove row numbers
    pTablesHTML += '                         <table id="tblOperationsStatistics" class="table table-striped text-sm table-bordered  " style="border:solid #000 !important;">';//style="border-left:solid #999 !important;border-top:solid #999 !important;"
    pTablesHTML += '                             <thead>';
    pTablesHTML += '                                 <tr class="" style="font-size:90%;">';
    pTablesHTML += '                                     <th>' + (pDefaults.UnEditableCompanyName == "SAF" ? 'Job No' : 'OperationNo') + '</th>';

    if ($("#cbOperationWithInvoiceSerial").prop("checked"))
        pTablesHTML += '                                     <th>Oper.WithInv.Ser.</th>';
    if ($("#cbBranch").prop("checked"))
        pTablesHTML += '                                     <th>Branch</th>';
    if ($("#cbSalesman").prop("checked"))
        pTablesHTML += '                                     <th>Salesman</th>';
    if ($("#cbIssueDate").prop("checked"))
        pTablesHTML += '                                     <th>Issue Date</th>';
    if ($("#cbOperState").prop("checked"))
        pTablesHTML += '                                     <th>Oper. State</th>';
    //pTablesHTML += '                                     <th>Oper. Type</th>';
    if ($("#cbBookingParty").prop("checked"))
        pTablesHTML += '                                     <th>BookingParty</th>';
    if ($("#cbClient").prop("checked"))
        pTablesHTML += '                                     <th>Client</th>';
    if ($("#cbShipper").prop("checked"))
        pTablesHTML += '                                     <th>Shipper</th>';
    if ($("#cbConsignee").prop("checked"))
        pTablesHTML += '                                     <th>Consignee</th>';
    if ($("#cbEndUser").prop("checked"))
        pTablesHTML += '                                     <th>EndUser</th>';
    if ($("#cbAgent").prop("checked"))
        pTablesHTML += '                                     <th>Agent</th>';
    if ($("#cbCustomsClearanceAgent").prop("checked"))
        pTablesHTML += '                                     <th>CustomsClr.Agent</th>';
    if ($("#cbLine").prop("checked"))
        pTablesHTML += '                                     <th>Line</th>';
    if ($("#cbCommodity").prop("checked"))
        pTablesHTML += '                                     <th>Commodity</th>';
    if ($("#cbIncoterm").prop("checked"))
        pTablesHTML += '                                     <th>Incoterm</th>';
    if ($("#cbQuotation").prop("checked"))
        pTablesHTML += '                                     <th>Quotation</th>';
    if ($("#cbContainerTypes").prop("checked"))
        pTablesHTML += '                                     <th>ContainerTypes</th>';
    if ($("#cbContainerNumbers").prop("checked"))
        pTablesHTML += '                                     <th>Container Nos</th>';
    if ($("#cbPackages").prop("checked"))
        pTablesHTML += '                                     <th>Packages</th>';
    if ($("#cbDescriptionOfGoods").prop("checked"))
        pTablesHTML += '                                     <th>DescriptionOfGoods</th>';
    if ($("#cbInvoices").prop("checked"))
        pTablesHTML += '                                     <th>Invoices</th>';
    if ($("#cbInvoiceDate").prop("checked")) //First Invoice
        pTablesHTML += '                                     <th>Inv.Date</th>';
    ////pTablesHTML += '                                     <th>Cntrs/Pkgs</th>';
    if ($("#cbCustomerRef").prop("checked"))
        pTablesHTML += '                                     <th>' + (pDefaults.UnEditableCompanyName == "SWI" ? 'Swift Ref' : 'CustomerRef') + '</th>';

    if ($("#cbPONo").prop("checked"))
        pTablesHTML += '                                     <th>PO No</th>';
    if ($("#cbPODate").prop("checked"))
        pTablesHTML += '                                     <th>PO Date</th>';
    if ($("#cbPOValue").prop("checked"))
        pTablesHTML += '                                     <th>PO Value</th>';
    if ($("#cbReleaseNo").prop("checked"))
        pTablesHTML += '                                     <th>' + (pDefaults.UnEditableCompanyName == 'KDM' ? 'Ref.No.' : 'Release No') + '</th>';
    if ($("#cbReleaseDate").prop("checked"))
        pTablesHTML += '                                     <th>Release Date</th>';
    if ($("#cbReleaseValue").prop("checked"))
        pTablesHTML += '                                     <th>Release Value</th>';

    if ($("#cbSupplierRef").prop("checked"))
        pTablesHTML += '                                     <th>SupplierRef</th>';
    if ($("#cbMBL").prop("checked"))
        pTablesHTML += '                                     <th>M B/L(MAWB)</th>';
    if ($("#cbHBL").prop("checked"))
        pTablesHTML += '                                     <th>HBL(HAWB)</th>';
    if ($("#cbCarrier").prop("checked"))
        pTablesHTML += '                                     <th>Carrier</th>';
    if ($("#cbTrucker").prop("checked"))
        pTablesHTML += '                                     <th>Truckers</th>';
    if ($("#cbBookingNo").prop("checked"))
        pTablesHTML += '                                     <th>BookingNo</th>';
    if ($("#cbTEUs").prop("checked"))
        pTablesHTML += '                                     <th>TEUs</th>';
    if ($("#cbVessel").prop("checked"))
        pTablesHTML += '                                     <th>Vessel</th>';
    if ($("#cbVoy").prop("checked"))
        pTablesHTML += '                                     <th>Voy</th>';
    //pTablesHTML += '                                     <th>Imp/Exp</th>';
    if ($("#cbPOL").prop("checked"))
        pTablesHTML += '                                     <th>POL</th>';
    if ($("#cbPOD").prop("checked"))
        pTablesHTML += '                                     <th>POD</th>';
    if ($("#cbCutOffDate").prop("checked"))
        pTablesHTML += '                                     <th>CutOffDate</th>';
    //pTablesHTML += '                                     <th>Open Date</th>';
    //pTablesHTML += '                                     <th>Close Date</th>';
    //pTablesHTML += '                                     <th>Arrival Date</th>';
    if ($("#cbETAPOL").prop("checked"))
        pTablesHTML += '                                     <th>ETA POL</th>';
    if ($("#cbETDPOL").prop("checked"))
        pTablesHTML += '                                     <th>ETD POL</th>';
    if ($("#cbATDPOL").prop("checked"))
        pTablesHTML += '                                     <th>ATD POL</th>';
    if ($("#cbETAPOD").prop("checked"))
        pTablesHTML += '                                     <th>ETA POD</th>';
    if ($("#cbATAPOD").prop("checked"))
        pTablesHTML += '                                     <th>ATA POD</th>';
    if ($("#cbTask").prop("checked"))
        pTablesHTML += '                                     <th>Task</th>';
    if ($("#cbAllTask").prop("checked")) {
        if ($("#slTrackingStageStatus").val() == "" && !($("#cbIsClearanceTask").prop("checked")))
            pTablesHTML += '                                     <th style="width:20%;">All Tasks</th>';
        else if ($("#slTrackingStageStatus").val() == 0 && !($("#cbIsClearanceTask").prop("checked")))
            pTablesHTML += '                                     <th style="width:20%;">Not Done Tasks</th>';
        else if ($("#slTrackingStageStatus").val() == 1 && !($("#cbIsClearanceTask").prop("checked")))
            pTablesHTML += '                                     <th style="width:20%;">Done Tasks</th>';
        else if ($("#slTrackingStageStatus").val() == "" && $("#cbIsClearanceTask").prop("checked"))
            pTablesHTML += '                                     <th style="width:20%;">All Clearance Tasks</th>';
        else if ($("#slTrackingStageStatus").val() == 0 && $("#cbIsClearanceTask").prop("checked"))
            pTablesHTML += '                                     <th style="width:20%;">Clearance Tasks Not Done</th>';
        else if ($("#slTrackingStageStatus").val() == 1 && $("#cbIsClearanceTask").prop("checked"))
            pTablesHTML += '                                     <th style="width:20%;">Clearance Tasks Done</th>';
    }
    if ($("#cbNetwork").prop("checked"))
        pTablesHTML += '                                     <th>Agent Network</th>';

    if ($("#cbMoveTypeName").prop("checked"))
        pTablesHTML += '                                     <th>Service Type</th>';
    if ($("#cbGrossWeightSum").prop("checked")) {
        pTablesHTML += '                                     <th>Gross(KG)</th>';
        pTablesHTML += '                                     <th>Gross(TON)</th>';
    }
    if ($("#cbNetWeightSum").prop("checked")) {
        pTablesHTML += '                                     <th>Net(KG)</th>';
        pTablesHTML += '                                     <th>Net(TON)</th>';
    }
    if ($("#cbVolumeSum").prop("checked"))
        pTablesHTML += '                                     <th>Vol(CBM)</th>';
    if ($("#cbChargeableWeightSum").prop("checked"))
        pTablesHTML += '                                     <th>Chg.Wt</th>';
    if ($("#cbCertificateNumber").prop("checked"))
        pTablesHTML += '                                     <th>Cert.No.</th>';
    if ($("#cbCertificateValue").prop("checked"))
        pTablesHTML += '                                     <th>Cert.Val.</th>';
    if ($("#cbCertificateDate").prop("checked"))
        pTablesHTML += '                                     <th>Cert.Date</th>';
    if ($("#cbQasimaNumber").prop("checked"))
        pTablesHTML += '                                     <th>Qasima No</th>';
    if ($("#cbQasimaDate").prop("checked"))
        pTablesHTML += '                                     <th>QasimaDate</th>';
    if ($("#cbForm13Number").prop("checked"))
        pTablesHTML += '                                     <th>Form13</th>';
    if ($("#cbCloseDate").prop("checked"))
        pTablesHTML += '                                     <th>CloseDate</th>';
    if ($("#cbCreator").prop("checked"))
        pTablesHTML += '                                     <th>Created By</th>';

    //to be removed in case of slow report
    if ($("#cbCustomerNetwork").prop("checked"))
        pTablesHTML += '                                     <th>Customer Network</th>';
    if ($("#cbClearanceType").prop("checked"))
        pTablesHTML += '                                     <th>C.Clearance Type</th>';

    if ($("#cbOperationMan").prop("checked"))
        pTablesHTML += '                                     <th>Operation Man</th>';
    if ($("#cbReceivables").prop("checked") || ($("#cbMargin").prop("checked") && pOutputTo == "Excel"))
        pTablesHTML += '                                     <th>Rec./Revenue</th>';
    if ($("#cbPayables").prop("checked") || pDefaults.UnEditableCompanyName == "MIL" || ($("#cbMargin").prop("checked") && pOutputTo == "Excel"))
        pTablesHTML += '                                     <th>Payables/Cost</th>';

    
    // get all different currencies
    var CurrenciesArray = [];
    if (IsWithCurrenciesDetailed) {
        debugger;
        $.each((pReportRowsWithDetailedCurrencies), function (i, item) {
            var AllCurrencies = item.AllCurrencies;
            if (AllCurrencies != "") {
                var NewCurrenciesArray = AllCurrencies.split("*");
                for (let j = 0; j < NewCurrenciesArray.length; j++) {
                    if (!CurrenciesArray.includes(NewCurrenciesArray[j])) {
                        CurrenciesArray.push(NewCurrenciesArray[j]);
                    }
                }

            }
        })
    }

    //if (IsWithCurrenciesDetailed && ($("#cbReceivablesWithCurrencies").prop("checked") || ($("#cbMargin").prop("checked") && pOutputTo == "Excel")))
    //    pTablesHTML += '                                     <th>Receivables With Currencies</th>';
    //if (IsWithCurrenciesDetailed && ($("#cbPayablesWithCurrencies").prop("checked") || pDefaults.UnEditableCompanyName == "MIL" || ($("#cbMargin").prop("checked") && pOutputTo == "Excel")))
    //    pTablesHTML += '                                     <th>Payables With Currencies</th>';

    if (IsWithCurrenciesDetailed/* && ($("#cbReceivablesWithCurrencies").prop("checked") || ($("#cbMargin").prop("checked") && pOutputTo == "Excel"))*/) {
        $.each((CurrenciesArray), function (i, item) {
            pTablesHTML += '                                     <th>Receivables ' + item +'</th>';
        })
    }
    if (IsWithCurrenciesDetailed/* && ($("#cbPayablesWithCurrencies").prop("checked") || pDefaults.UnEditableCompanyName == "MIL" || ($("#cbMargin").prop("checked") && pOutputTo == "Excel"))*/) {
        $.each((CurrenciesArray), function (i, item) {
            pTablesHTML += '                                     <th>Payables ' + item + '</th>';
        })
    }




    if ($("#cbProfit").prop("checked") || ($("#cbMargin").prop("checked") && pOutputTo == "Excel"))
        pTablesHTML += '                                     <th>Profit</th>';

    if ($("#cbMargin").prop("checked"))
        pTablesHTML += '                                     <th>Margin(%)</th>';
    if (pDefaults.UnEditableCompanyName == "NIL")
        pTablesHTML += '                                     <th>CourierNo</th>';

    if ($("#cbShipmentType").prop("checked"))
        pTablesHTML += '                                     <th>Shipment Type</th>';
    if ($("#cbACIDNo").prop("checked"))
        pTablesHTML += '                                     <th>ACID Number</th>';
    if ($("#cbReceivingDate").prop("checked"))
        pTablesHTML += '                                     <th>Receiving Date</th>';
    if ($("#cbDODate").prop("checked"))
        pTablesHTML += '                                     <th>D/O Date</th>';
    if ($("#cbFreeTime").prop("checked"))
        pTablesHTML += '                                     <th>Free Days</th>';
    if ($("#cbNotes").prop("checked"))
        pTablesHTML += '                                     <th>Daily Update</th>';


    pTablesHTML += '                                 </tr>';
    pTablesHTML += '                             </thead>';
    pTablesHTML += '                             <tbody>';
    //debugger;
    //$.each(JSON.parse(pReportRows), function (i, item) { debugger; });
    $.each((pReportRows), function (i, item) {
        pTablesHTML += '                                     <tr style="font-size:90%;">';
        pTablesHTML += '                                         <td class="' + (item.BLType != constHouseBLType ? 'classNotHouse' : '') + '">' + (item.Code == 0 ? "" : item.Code) + '</td>';



        if ($("#cbOperationWithInvoiceSerial").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.OperationWithInvoiceSerial == 0 ? "" : item.OperationWithInvoiceSerial) + '</td>';
        if ($("#cbBranch").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.BranchName == 0 ? "" : item.BranchName) + '</td>';
        if ($("#cbSalesman").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.Salesman == 0 ? "" : item.Salesman) + '</td>';
        if ($("#cbIssueDate").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.OpenDate == "/Date(-2208996000000)/" ? "" : (ConvertDateFormat(GetDateWithFormatMDY(item.OpenDate)) + '\u2008')) + '</td>'; // I added Punctuation Space '\u2008' to force excel to deal it as a string
        if ($("#cbOperState").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.OperationStageName == 0 ? "" : item.OperationStageName) + '</td>';
        //pTablesHTML += '                                         <td>' + (item.RepTransportTypeShown == 0 ? "" : item.RepTransportTypeShown) + ' ' + (item.ShipmentTypeCode == 0 ? "" : item.ShipmentTypeCode) + '</td>';
        if ($("#cbBookingParty").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.BookingPartyName == 0 ? "" : item.BookingPartyName) + '</td>';
        if ($("#cbClient").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.ClientName == 0 ? "" : item.ClientName) + '</td>';
        if ($("#cbShipper").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.ShipperName == 0 ? "" : item.ShipperName) + '</td>';
        if ($("#cbConsignee").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.ConsigneeName == 0 ? "" : item.ConsigneeName) + '</td>';
        if ($("#cbEndUser").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.EndUserName == 0 ? "" : item.EndUserName) + '</td>';
        if ($("#cbAgent").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.AgentName == 0 ? "" : item.AgentName) + '</td>';
        if ($("#cbCustomsClearanceAgent").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.CustomsClearanceAgentName == 0 ? "" : item.CustomsClearanceAgentName) + '</td>';
        if ($("#cbLine").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.LineName == 0 ? "" : item.LineName) + '</td>';
        if ($("#cbCommodity").prop("checked")) {
            pTablesHTML += '                                         <td>' + (item.CommodityName == 0 ? "" : item.CommodityName) + " - " + (item.CommodityName2 == 0 ? "" : item.CommodityName2) + " - " + (item.CommodityName3 == 0 ? "" : item.CommodityName3) + '</td>';
        }

        if ($("#cbIncoterm").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.IncotermName == 0 ? "" : item.IncotermName) + '</td>';
        if ($("#cbQuotation").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.QuotationRouteCode == 0 ? "" : item.QuotationRouteCode) + '</td>';
        if ($("#cbContainerTypes").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.ContainerTypes == 0 ? "" : item.ContainerTypes) + '</td>';
        if ($("#cbContainerNumbers").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.ContainerNumbers == 0 ? "" : item.ContainerNumbers) + '</td>';
        if ($("#cbPackages").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.PackageTypes == 0 ? (item.PackageTypesOnContainersTotals == 0 ? "" : item.PackageTypesOnContainersTotals) : item.PackageTypes) + '</td>';
        if ($("#cbDescriptionOfGoods").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.DescriptionOfGoods == 0 ? "" : item.DescriptionOfGoods) + '</td>';
        if ($("#cbInvoices").prop("checked"))
            pTablesHTML += '                                         <td style="font-size:90%;text-align:left;">' + (item.InvoiceNumbers == 0 ? "" : item.InvoiceNumbers.replace(/\,/g, "<br/>")) + '</td>';
        if ($("#cbInvoiceDate").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.FirstInvoiceDate == 0 ? "" : (item.FirstInvoiceDate + '\u2008')) + '</td>';
        //if (item.RepTransportTypeShown == "AIR" || item.ShipmentTypeCode == "LCL" || item.ShipmentTypeCode == "LTL")
        //    pTablesHTML += '                                     <td>' + (item.PackageTypes == 0 ? "" : item.PackageTypes) + '</td>';
        //else
        //    pTablesHTML += '                                     <td>' + (item.ContainerTypes == 0 ? "" : item.ContainerTypes) + '</td>';
        if ($("#cbCustomerRef").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.CustomerReference == 0 ? "" : item.CustomerReference) + '</td>';

        if ($("#cbPONo").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.PONumber == 0 ? "" : item.PONumber) + '</td>';
        if ($("#cbPODate").prop("checked"))
            pTablesHTML += '                                         <td>' + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.PODate)) < 1 ? "" : (ConvertDateFormat(GetDateWithFormatMDY(item.PODate)) + '\u2008')) + '</td>';
        if ($("#cbPOValue").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.POValue == 0 ? "" : item.POValue) + '</td>';
        if ($("#cbReleaseNo").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.ReleaseNumber == 0 ? "" : item.ReleaseNumber) + '</td>';
        if ($("#cbReleaseDate").prop("checked"))
            pTablesHTML += '                                         <td>' + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.ReleaseDate)) < 1 ? "" : (ConvertDateFormat(GetDateWithFormatMDY(item.ReleaseDate)) + '\u2008')) + '</td>';
        if ($("#cbReleaseValue").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.ReleaseValue == 0 ? "" : item.ReleaseValue) + '</td>';

        if ($("#cbSupplierRef").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.SupplierReference == 0 ? "" : item.SupplierReference) + '</td>';
        if ($("#cbMBL").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.MasterBL == 0 ? "" : item.MasterBL) + '</td>';
        if ($("#cbHBL").prop("checked"))
            //pTablesHTML += '                                         <td>' + (item.HouseNumber == 0 ? "" : item.HouseNumber) + '</td>';
            pTablesHTML += '                                         <td>' + (item.HouseNumber == 0 ? item.HouseBLs : item.HouseNumber) + '</td>';
        if ($("#cbCarrier").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.LineName == 0 ? "" : item.LineName) + '</td>';
        if ($("#cbTrucker").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.TruckersName == 0 ? "" : item.TruckersName) + '</td>';
        if ($("#cbBookingNo").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.BookingNumbers == 0 ? "" : item.BookingNumbers) + '</td>';
        if ($("#cbTEUs").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.TEUs == 0 || item.TransportType == 2 ? "" : item.TEUs) + '</td>';
        if ($("#cbVessel").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.VesselName == 0 ? "" : item.VesselName) + '</td>';
        if ($("#cbVoy").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.VoyageOrTruckNumber == 0 ? "" : item.VoyageOrTruckNumber) + '</td>';
        if ($("#cbPOL").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.POLCountryCode + ": " + item.POLName) + '</td>';
        if ($("#cbPOD").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.PODCountryCode + ": " + item.PODName) + '</td>';
        if ($("#cbCutOffDate").prop("checked"))
            pTablesHTML += '                                         <td>' + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.CutOffDate)) < 1 ? "" : (ConvertDateFormat(GetDateWithFormatMDY(item.CutOffDate)) + '\u2008')) + '</td>';
        //pTablesHTML += '                                         <td>' + (item.OpenDate == "/Date(-2208996000000)/" ? "" : ConvertDateFormat(GetDateWithFormatMDY(item.OpenDate))) + '</td>';
        //pTablesHTML += '                                         <td>' + (item.CloseDate == "/Date(-2208996000000)/" ? "" : ConvertDateFormat(GetDateWithFormatMDY(item.CloseDate))) + '</td>';
        //pTablesHTML += '                                         <td>' + (item.ActualArrival == "/Date(-2208996000000)/" ? "" : ConvertDateFormat(GetDateWithFormatMDY(item.ActualArrival))) + '</td>';
        if ($("#cbETAPOL").prop("checked"))
            pTablesHTML += '                                         <td>' + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.ETAPOLDate)) < 1 ? "" : (ConvertDateFormat(GetDateWithFormatMDY(item.ETAPOLDate)) + '\u2008')) + '</td>';
        if ($("#cbETDPOL").prop("checked"))
            pTablesHTML += '                                         <td>' + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.ExpectedDeparture)) < 1 ? "" : (ConvertDateFormat(GetDateWithFormatMDY(item.ExpectedDeparture)) + '\u2008')) + '</td>';
        if ($("#cbATDPOL").prop("checked"))
            pTablesHTML += '                                         <td>' + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.ActualDeparture)) < 1 ? "" : (ConvertDateFormat(GetDateWithFormatMDY(item.ActualDeparture)) + '\u2008')) + '</td>';
        if ($("#cbETAPOD").prop("checked"))
            pTablesHTML += '                                         <td>' + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.ExpectedArrival)) < 1 ? "" : (ConvertDateFormat(GetDateWithFormatMDY(item.ExpectedArrival)) + '\u2008')) + '</td>';
        if ($("#cbATAPOD").prop("checked"))
            pTablesHTML += '                                         <td>' + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.ActualArrival)) < 1 ? "" : (ConvertDateFormat(GetDateWithFormatMDY(item.ActualArrival)) + '\u2008')) + '</td>';
        if ($("#cbTask").prop("checked"))
            pTablesHTML += '                                         <td>' + ($("#slTrackingStage").val() == "" ? (item.TrackingStageName == 0 ? "" : item.TrackingStageName) : $("#slTrackingStage option:selected").text()) + '</td>';
        if ($("#cbAllTask").prop("checked")) {
            if ($("#slTrackingStageStatus").val() == "" && !($("#cbIsClearanceTask").prop("checked")))
                pTablesHTML += '                                         <td style="font-size:90%;text-align:left;">' + (item.AllTrackingStages == 0 ? "" : item.AllTrackingStages.replace(/\,/g, "<br/>").replace(/BoldStart/g, "<b><u>").replace(/BoldEnd/g, "</u></b>")) + '</td>';
            else if ($("#slTrackingStageStatus").val() == 0 && !($("#cbIsClearanceTask").prop("checked")))
                pTablesHTML += '                                         <td style="font-size:90%;text-align:left;">' + (item.AllTrackingStages_NotDone == 0 ? "" : item.AllTrackingStages_NotDone.replace(/\,/g, "<br/>").replace(/BoldStart/g, "<b><u>").replace(/BoldEnd/g, "</u></b>")) + '</td>';
            else if ($("#slTrackingStageStatus").val() == 1 && !($("#cbIsClearanceTask").prop("checked")))
                pTablesHTML += '                                         <td style="font-size:90%;text-align:left;">' + (item.AllTrackingStages_Done == 0 ? "" : item.AllTrackingStages_Done.replace(/\,/g, "<br/>").replace(/BoldStart/g, "<b><u>").replace(/BoldEnd/g, "</u></b>")) + '</td>';
            else if ($("#slTrackingStageStatus").val() == "" && $("#cbIsClearanceTask").prop("checked"))
                pTablesHTML += '                                         <td style="font-size:90%;text-align:left;">' + (item.AllTrackingStages_CC == 0 ? "" : item.AllTrackingStages_CC.replace(/\,/g, "<br/>").replace(/BoldStart/g, "<b><u>").replace(/BoldEnd/g, "</u></b>")) + '</td>';
            else if ($("#slTrackingStageStatus").val() == 0 && $("#cbIsClearanceTask").prop("checked"))
                pTablesHTML += '                                         <td style="font-size:90%;text-align:left;">' + (item.AllTrackingStages_CCNotDone == 0 ? "" : item.AllTrackingStages_CCNotDone.replace(/\,/g, "<br/>").replace(/BoldStart/g, "<b><u>").replace(/BoldEnd/g, "</u></b>")) + '</td>';
            else if ($("#slTrackingStageStatus").val() == 1 && $("#cbIsClearanceTask").prop("checked"))
                pTablesHTML += '                                         <td style="font-size:90%;text-align:left;">' + (item.AllTrackingStages_CCDone == 0 ? "" : item.AllTrackingStages_CCDone.replace(/\,/g, "<br/>").replace(/BoldStart/g, "<b><u>").replace(/BoldEnd/g, "</u></b>")) + '</td>';
        }
        if ($("#cbNetwork").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.NetworkName == 0 ? "" : item.NetworkName) + '</td>';

        if ($("#cbMoveTypeName").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.MoveTypeName == 0 ? "" : item.MoveTypeName) + '</td>';
        if ($("#cbGrossWeightSum").prop("checked")) {
            pTablesHTML += '                                         <td class="' + (item.BLType == constHouseBLType ? '' : 'GrossWeightSum') + '">' + (item.GrossWeightSum == 0 ? "" : item.GrossWeightSum) + '</td>';
            pTablesHTML += '                                         <td class="' + (item.BLType == constHouseBLType ? '' : 'GrossWeightTONSum') + '">' + (item.GrossWeightTONSum == 0 ? "" : item.GrossWeightTONSum) + '</td>';
        }
        if ($("#cbNetWeightSum").prop("checked")) {
            pTablesHTML += '                                         <td class="' + (item.BLType == constHouseBLType ? '' : 'NetWeightSum') + '">' + (item.NetWeightSum == 0 ? "" : item.NetWeightSum) + '</td>';
            pTablesHTML += '                                         <td class="' + (item.BLType == constHouseBLType ? '' : 'NetWeightTONSum') + '">' + (item.NetWeightTONSum == 0 ? "" : item.NetWeightTONSum) + '</td>';
        }
        if ($("#cbVolumeSum").prop("checked"))
            pTablesHTML += '                                         <td class="' + (item.BLType == constHouseBLType ? '' : 'VolumeSum') + '">' + (item.VolumeSum == 0 ? "" : item.VolumeSum) + '</td>';
        if ($("#cbChargeableWeightSum").prop("checked"))
            pTablesHTML += '                                         <td class="' + (item.BLType == constHouseBLType ? '' : 'ChargeableWeightSum') + '">' + (item.ChargeableWeight == 0 ? "" : item.ChargeableWeight) + '</td>';
        if ($("#cbCertificateNumber").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.CertificateNumber == 0 ? "" : item.CertificateNumber) + '</td>';
        if ($("#cbCertificateValue").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.CertificateValue == 0 ? "" : item.CertificateValue) + '</td>';
        if ($("#cbCertificateDate").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.CertificateDate == 0 ? "" : (item.CertificateDate + '\u2008')) + '</td>';
        if ($("#cbQasimaNumber").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.QasimaNumber == 0 ? "" : item.QasimaNumber) + '</td>';
        if ($("#cbQasimaDate").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.QasimaDate == 0 ? "" : (item.QasimaDate + '\u2008')) + '</td>';
        if ($("#cbForm13Number").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.Form13Number == 0 ? "" : item.Form13Number) + '</td>';
        if ($("#cbCloseDate").prop("checked"))
            pTablesHTML += '                                         <td>' + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.CloseDate)) < 1 ? "" : (ConvertDateFormat(GetDateWithFormatMDY(item.CloseDate)) + '\u2008')) + '</td>';
        if ($("#cbCreator").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.CreatorName == 0 ? "" : item.CreatorName) + '</td>';

        //to be removed in case of slow report
        if ($("#cbCustomerNetwork").prop("checked"))
            pTablesHTML += '                                         <td>' + IsNull(item.NetworksNames, "") + '</td>';
        if ($("#cbClearanceType").prop("checked"))
            pTablesHTML += '                                         <td>' + IsNull(item.CC_ClearanceTypeName, "") + '</td>';

        if ($("#cbOperationMan").prop("checked"))
            pTablesHTML += '                                         <td class="classOperationMan">' + (item.OperationManName == 0 ? "" : item.OperationManName) + '</td>';
        //if ($("#cbReceivables").prop("checked"))
        if ($("#cbReceivables").prop("checked") || ($("#cbMargin").prop("checked") && pOutputTo == "Excel"))
            //pTablesHTML += '                                         <td class="classReceivables ' + ($("#cbReceivables").prop("checked") || ($("#cbMargin").prop("checked") && pOutputTo == "Excel") ? "" : "hide") + '">'
            pTablesHTML += '                                         <td class="classReceivables">'
                + ($("#cbIsWithoutVAT").prop("checked")
                    ? (item.ReceivablesWithoutVAT - item.FixedDiscount).toFixed(2)
                    : (item.Receivables - item.FixedDiscount).toFixed(2)
                )
                + '</td>';
        //if ($("#cbPayables").prop("checked"))
        if ($("#cbPayables").prop("checked") || pDefaults.UnEditableCompanyName == "MIL" || ($("#cbMargin").prop("checked") && pOutputTo == "Excel"))
            //pTablesHTML += '                                         <td class="classPayables ' + ($("#cbPayables").prop("checked") || pDefaults.UnEditableCompanyName == "MIL" || ($("#cbMargin").prop("checked") && pOutputTo == "Excel") ? "" : "hide") + '">'
            pTablesHTML += '                                         <td class="classPayables">'
                + ($("#cbIsWithoutVAT").prop("checked")
                    ? item.PayablesWithoutVAT.toFixed(2)
                    : item.Payables.toFixed(2)
                )
                + '</td>';


        

        //////////////////////////


        //if (IsWithCurrenciesDetailed && ($("#cbReceivablesWithCurrencies").prop("checked") || ($("#cbMargin").prop("checked") && pOutputTo == "Excel")))
        //    pTablesHTML += '                                         <td class="classReceivables">'
        //        + ($("#cbIsWithoutVAT").prop("checked")
        //            ? (pReportRowsWithDetailedCurrencies[i].ReceivablesWithoutVATCurrencies/* - item.FixedDiscount*/)
        //            : (pReportRowsWithDetailedCurrencies[i].ReceivablesWithVATSumCurrencies/* - item.FixedDiscount*/)
        //        )
        //        + '</td>';

        //if (IsWithCurrenciesDetailed && ($("#cbPayablesWithCurrencies").prop("checked") || pDefaults.UnEditableCompanyName == "MIL" || ($("#cbMargin").prop("checked") && pOutputTo == "Excel")))
        //    pTablesHTML += '                                         <td class="classPayables">'
        //        + ($("#cbIsWithoutVAT").prop("checked")
        //            ? pReportRowsWithDetailedCurrencies[i].PayablesWithoutVATSumCurrencies
        //            : pReportRowsWithDetailedCurrencies[i].PayablesWithVATSumCurrencies
        //        )
        //        + '</td>';
        debugger
        if (IsWithCurrenciesDetailed/* && ($("#cbReceivablesWithCurrencies").prop("checked") || ($("#cbMargin").prop("checked") && pOutputTo == "Excel"))*/) {
            var ReceivablesDetailedCurrencies = $("#cbIsWithoutVAT").prop("checked")
                ? (pReportRowsWithDetailedCurrencies[i].ReceivablesWithoutVATCurrencies/* - item.FixedDiscount*/)
                : (pReportRowsWithDetailedCurrencies[i].ReceivablesWithVATSumCurrencies/* - item.FixedDiscount*/);
            // extract values for each Currency from ReceivablesDetailedCurrencies ex. **EGP*114.00000*EGP***EUR*200.00000*EUR***GBP*300.00000*GBP***USD*400.00000*USD
            $.each((CurrenciesArray), function (i, cur) {
                var mySubString = ReceivablesDetailedCurrencies.substring(
                    ReceivablesDetailedCurrencies.indexOf(cur + "*") + 4,
                    ReceivablesDetailedCurrencies.lastIndexOf("*" + cur)
                );
                if (isNaN(parseFloat(mySubString))) {
                    pTablesHTML += '                                     <td class="' + cur + '">' + (0) + '</td>';
                } else {
                    pTablesHTML += '                                     <td class="' + cur + '">' + (parseFloat(mySubString)) + '</td>';
                }
            })
        }

        if (IsWithCurrenciesDetailed/* && ($("#cbPayablesWithCurrencies").prop("checked") || pDefaults.UnEditableCompanyName == "MIL" || ($("#cbMargin").prop("checked") && pOutputTo == "Excel"))*/) {
            var PayablesDetailedCurrencies = $("#cbIsWithoutVAT").prop("checked")
                ? (pReportRowsWithDetailedCurrencies[i].PayablesWithoutVATSumCurrencies)
                : (pReportRowsWithDetailedCurrencies[i].PayablesWithVATSumCurrencies);
            // extract values for each Currency from PayablesDetailedCurrencies ex. **EGP*114.00000*EGP***EUR*200.00000*EUR***GBP*300.00000*GBP***USD*400.00000*USD
            $.each((CurrenciesArray), function (i, cur) {
                var mySubString = PayablesDetailedCurrencies.substring(
                    PayablesDetailedCurrencies.indexOf(cur + "*") + 4,
                    PayablesDetailedCurrencies.lastIndexOf("*" + cur)
                );
                if (isNaN(parseFloat(mySubString))) {
                    pTablesHTML += '                                     <td class="' + cur + '">' + (0) + '</td>';
                } else {
                    pTablesHTML += '                                     <td class="' + cur + '">' + (parseFloat(mySubString)) + '</td>';
                }
            })
        }






        //if ($("#cbProfit").prop("checked"))
        pTablesHTML += '                                         <td class="classProfit ' + ($("#cbProfit").prop("checked") || ($("#cbMargin").prop("checked") && pOutputTo == "Excel") ? "" : "hide") + '">'
            + ($("#cbIsWithoutVAT").prop("checked")
                ? (item.ReceivablesWithoutVAT - item.PayablesWithoutVAT - item.FixedDiscount).toFixed(2)
                : (item.Receivables - item.Payables - item.FixedDiscount).toFixed(2)
            )
            + '</td>';
        var _Margin = 0;
        if ($("#cbIsWithoutVAT").prop("checked"))
            _Margin = item.ReceivablesWithoutVAT == 0 ? 0 : (100 * (item.ReceivablesWithoutVAT - item.PayablesWithoutVAT - item.FixedDiscount) / item.ReceivablesWithoutVAT);
        else
            _Margin = item.Receivables == 0 ? 0 : (100 * (item.Receivables - item.Payables - item.FixedDiscount) / item.Receivables);
        pTablesHTML += '                                         <td class="classMargin ' + ($("#cbMargin").prop("checked") ? "" : "hide") + '">' + (_Margin == 0 ? 'N/A' : _Margin.toFixed(2)) + '</td>';

        if (pDefaults.UnEditableCompanyName == "NIL")
            pTablesHTML += '                                         <td>' + (item.MainRouteNotes == 0 ? "" : item.MainRouteNotes) + '</td>';
        pTablesHTML += '                                             <td class="hide ' + (item.BLType == constHouseBLType ? "" : ('BranchID' + item.BranchID)) + '">' + '' + '</td>';
        pTablesHTML += '                                             <td class="hide ' + (item.BLType == constHouseBLType ? "" : (' TransportType' + item.TransportType + ' DirectionType' + item.DirectionType)) + '">' + '' + '</td>';
        //pTablesHTML += '                                         <td>' + item.CostAmountSubTotal.toFixed(2) + '</td>';

        if ($("#cbShipmentType").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.ShipmentTypeCode == 0 ? "" : item.ShipmentTypeCode) + '</td>';
        if ($("#cbACIDNo").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.ACIDNumber == 0 ? "" : item.ACIDNumber) + '</td>';
        if ($("#cbReceivingDate").prop("checked"))
            pTablesHTML += '                                         <td>' + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.CCADocumentReceiveDate)) < 1 ? "" : (ConvertDateFormat(GetDateWithFormatMDY(item.CCADocumentReceiveDate)) + '\u2008')) + '</td>';
        if ($("#cbDODate").prop("checked"))
            pTablesHTML += '                                         <td>' + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.DeliveryDate)) < 1 ? "" : (ConvertDateFormat(GetDateWithFormatMDY(item.DeliveryDate)) + '\u2008')) + '</td>';
        if ($("#cbFreeTime").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.FreeTime == 0 ? "" : item.FreeTime) + '</td>';
        if ($("#cbNotes").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.Notes == 0 ? "" : item.Notes) + '</td>';


        pTablesHTML += '                                     </tr>';
    });
    pTablesHTML += '                             </tbody>';
    pTablesHTML += '                         </table>';
    $("#hExportedTable").html(pTablesHTML); //in all cases i put the tables in hidden div so i select each table separately
    if (pOutputTo == "Excel") {
        var pBranchSummary = "";
        /********************************Adding Summary*************************************/
        for (i = 0; i < ArrBranchIDs.length; i++) {
            if ($("#tblOperationsStatistics tbody tr").find("td.BranchID" + ArrBranchIDs[i]).length != 0)
                pBranchSummary += $("#slBranch option[value=" + ArrBranchIDs[i] + "]").text() + " branch: " + $("#tblOperationsStatistics tbody tr").find("td.BranchID" + ArrBranchIDs[i]).length + " Operations" + "<br>";
        }
        debugger;
        if (pOutputTo == "Excel") {
            var pExcelSummary = "<tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>";
            pExcelSummary += "<tr><td>Import:" + $("#tblOperationsStatistics tbody tr").find("td.DirectionType1").length + " operations" + "</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>";
            pExcelSummary += "<tr><td>Export:" + $("#tblOperationsStatistics tbody tr").find("td.DirectionType2").length + " operations" + "</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>";
            pExcelSummary += "<tr><td>Domestic:" + $("#tblOperationsStatistics tbody tr").find("td.DirectionType3").length + " operations" + "</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>";
            pExcelSummary += "<tr><td>Ocean:" + $("#tblOperationsStatistics tbody tr").find("td.TransportType1").length + " operations" + "</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>";
            pExcelSummary += "<tr><td>Air:" + $("#tblOperationsStatistics tbody tr").find("td.TransportType2").length + " operations" + "</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>";
            pExcelSummary += "<tr><td>Inland:" + $("#tblOperationsStatistics tbody tr").find("td.TransportType3").length + " operations" + "</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>";
            pExcelSummary += "<tr><td>" + pBranchSummary + "</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>";
            //pExcelSummary += "<tr><td>Total:" + $("#tblOperationsStatistics tbody tr").length + " operations" + "</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>";
            pExcelSummary += "<tr><td>Total:" + $("#tblOperationsStatistics tbody tr").find("td.classNotHouse").length + " operations" + "</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>";
            pExcelSummary += "<tr><td>Containers Total : " + pContainerTotals + "</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>";
            pExcelSummary += "<tr><td>Incoterms Total : " + pIncotermTotals + "</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>";
            if (pDefaults.UnEditableCompanyName != "NIL" && $("#hf_CanEdit").val() == 1) {
                if ($("#cbPayables").prop("checked") || pDefaults.UnEditableCompanyName == "MIL" || ($("#cbMargin").prop("checked") && pOutputTo == "Excel"))
                    pExcelSummary += '<tr><td>Payables Summary : ' + parseFloat(GetColumnSum("tblOperationsStatistics", "classPayables")).toFixed(2) + '</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>';
                if ($("#cbReceivables").prop("checked") || ($("#cbMargin").prop("checked") && pOutputTo == "Excel"))
                    pExcelSummary += '<tr><td>Receivables Summary : ' + parseFloat(GetColumnSum("tblOperationsStatistics", "classReceivables")).toFixed(2) + '</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>';
                if ($("#cbPayables").prop("checked") && $("#cbReceivables").prop("checked"))
                    pExcelSummary += '<tr><td>Profit Summary : ' + parseFloat(GetColumnSum("tblOperationsStatistics", "classProfit")).toFixed(2) + '</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>';
            }
            $("#tblOperationsStatistics tbody").append(pExcelSummary);
        }

        //ExportHtmlTableToCsv_RemovingCommasForNumbers("tblOperationsStatistics", pReportTitle);
        ExportHtmlTableToCsv("tblOperationsStatistics", pReportTitle);
    }
    else {
        var mywindow = null;
        if (pOutputTo != "Email")
            mywindow = window.open('', '_blank');
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
        ////ReportHTML += '             <div class="col-xs-3"><b>Currency :</b> ' + $("#slCurrency option:selected").text() + '</div>';
        ReportHTML += '             <div class="col-xs-3"><b>Oper. Status :</b> ' + $("#slOperationStages option:selected").text() + '</div>';
        ReportHTML += '             <div class="col-xs-6"><b>Open Date :</b> ' + ($("#txtFromDate").val() == "" && $("#txtToDate").val() == ""
            ? "All Dates"
            : ($("#txtFromDate").val() == "" ? "" : "From " + $("#txtFromDate").val() + " ") + ($("#txtToDate").val() == "" ? "" : "To " + $("#txtToDate").val())) + '</div>';
        if ($("#slOperationStages option:selected").text() == "CLOSED")
            ReportHTML += '             <div class="col-xs-6"><b>Close Date :</b> ' + ($("#txtFromCloseDate").val() == "" && $("#txtToCloseDate").val() == ""
                ? "All Dates"
                : ($("#txtFromCloseDate").val() == "" ? "" : "From " + $("#txtFromCloseDate").val() + " ") + ($("#txtToCloseDate").val() == "" ? "" : "To " + $("#txtToCloseDate").val())) + '</div>';
        ////ReportHTML += '                 <section class="panel panel-default">';
        //ReportHTML += '                     <div class="table-responsive">';
        ReportHTML += '                         <div> &nbsp; </div>'

        ReportHTML += pTablesHTML;

        ReportHTML += '             <div class="col-xs-4"><b>Import :</b> ' + $("#tblOperationsStatistics tbody tr").find("td.DirectionType1").length + " Operations"
            + '<br><b>Export :</b> ' + $("#tblOperationsStatistics tbody tr").find("td.DirectionType2").length + " Operations"
            + '<br><b>Domestic :</b> ' + $("#tblOperationsStatistics tbody tr").find("td.DirectionType3").length + " Operations"
            + '</div>';

        ReportHTML += '             <div class="col-xs-4"><b>Ocean :</b> ' + $("#tblOperationsStatistics tbody tr").find("td.TransportType1").length + " Operations"
            + '<br><b>Air :</b> ' + $("#tblOperationsStatistics tbody tr").find("td.TransportType2").length + " Operations"
            + '<br><b>Inland :</b> ' + $("#tblOperationsStatistics tbody tr").find("td.TransportType3").length + " Operations"
            + '</div>';

        ReportHTML += '             <div class="col-xs-4">'
            + ($("#cbGrossWeightSum").prop("checked") ? ('Total Gross :</b> ' + GetColumnSum("tblOperationsStatistics", "GrossWeightSum") + " KGM<br>") : "")
            + ($("#cbNetWeightSum").prop("checked") ? ('Total Net :</b> ' + parseFloat(GetColumnSum("tblOperationsStatistics", "NetWeightSum")).toFixed(2) + " KGM<br>") : "")
            + ($("#cbVolumeSum").prop("checked") ? ('<b>Total Vol :</b> ' + GetColumnSum("tblOperationsStatistics", "VolumeSum") + " CBM<br>") : "")
            + ($("#cbChargeableWeightSum").prop("checked") ? ('<b>Total Chg.Wt :</b> ' + GetColumnSum("tblOperationsStatistics", "ChargeableWeightSum") + " <br>") : "")
            + '</div>';

        for (i = 0; i < ArrBranchIDs.length; i++) {
            if ($("#tblOperationsStatistics tbody tr").find("td.BranchID" + ArrBranchIDs[i]).length != 0)
                ReportHTML += '             <div class="col-xs-12" style="clear:both;"><b>' + $("#slBranch option[value=" + ArrBranchIDs[i] + "]").text() + " branch: " + '</b> ' + $("#tblOperationsStatistics tbody tr").find("td.BranchID" + ArrBranchIDs[i]).length + " Operations" + '</div>';
        }
        //ReportHTML += '             <div class="col-xs-12"><b>Total :</b> ' + $("#tblOperationsStatistics tbody tr").length + " operations" + '</div>';
        ReportHTML += '             <div class="col-xs-12"><b>Total :</b> ' + $("#tblOperationsStatistics tbody tr").find("td.classNotHouse").length + " operations" + '</div>';
        ReportHTML += '             <div class="col-xs-12"><b>Containers Totals :</b> ' + pContainerTotals + '</div>';
        if (pDefaults.UnEditableCompanyName != "ALL")
            ReportHTML += '             <div class="col-xs-12"><b>Incoterms Totals :</b> ' + pIncotermTotals + '</div>';
        //ReportHTML += '                     </div>';//of table-responsive
        if (pDefaults.UnEditableCompanyName != "NIL" && pDefaults.UnEditableCompanyName != "ALL" && $("#hf_CanEdit").val() == 1) {
            ReportHTML += '             <div class="col-xs-12"><b>Payables Summary :</b> ' + parseFloat(GetColumnSum("tblOperationsStatistics", "classPayables")).toFixed(2) + '</div>';
            ReportHTML += '             <div class="col-xs-12"><b>Receivables Summary :</b> ' + parseFloat(GetColumnSum("tblOperationsStatistics", "classReceivables")).toFixed(2) + '</div>';
            ReportHTML += '             <div class="col-xs-12"><b>Profit Summary :</b> ' + parseFloat(GetColumnSum("tblOperationsStatistics", "classProfit")).toFixed(2) + '</div>';
        }

        ReportHTML += '         </body>';
        ReportHTML += '     <footer class="footer col-xs-12" style="width:100%; position:absolute; bottom:0;">';
        ////ReportHTML += '         <div class="row text-right m-r">الشركه خاضعه لنظام الدفعات المقدمه تطبيقا لأحكام الماده 62 من القانون رقم 91 لسنة 2005 و لا يجوز الخصم عليها</div>';
        //if ($("#tblDocsOut tr[id=" + pDocumentTypeID + "] td.IsPrintISOCode input[type=checkbox]").prop("checked"))
        //    ReportHTML += '     <div class="row m-l">' + $("#tblDocsOut tr[id=" + pDocumentTypeID + "] td.ISOCode").text() + '</div>';
        //ReportHTML += '         <div class="row text-center"><img src="/Content/Images/CompanyFooter.jpg" alt="footer"/></div>';
        ReportHTML += '     </footer>';

        ReportHTML += '</html>';
        if (pOutputTo != "Email") {
            mywindow.document.write(ReportHTML);
            mywindow.document.close();
        }
        else {
            SendPDF_ReportByEmail($('#txtToEmail').val(), ReportHTML, pReportTitle, true);
        }
    }
}
function OperationsStatistics_WorkFlow_DrawReport(data, pOutputTo) {
    debugger;
    var pReportRows = JSON.parse(data[1]);

    var pReportTitle = $("#txtToDate").val() + "  حركة العمل من  " + $("#txtFromDate").val() + "  إلى  ";

    var TodaysDateddMMyyyy = getTodaysDateInddMMyyyyFormat();
    var pTablesHTML = "";
    //ReportHTML += '                         <table id="tblWorkFlow" class="table table-striped text-sm table-hover b-t b-r b-b b-l print">';//remove t1 class to remove row numbers
    pTablesHTML += '                         <table id="tblWorkFlow" class="table table-striped text-sm table-bordered  " style="border:solid #000 !important;">';//style="border-left:solid #999 !important;border-top:solid #999 !important;"
    pTablesHTML += '                             <thead>';
    pTablesHTML += '                                 <tr class="" style="font-size:90%;">';
    pTablesHTML += '                                     <th>OperationNo. - رقم العملية  </th>';
    pTablesHTML += '                                     <th>Client -  العميل </th>';
    pTablesHTML += '                                     <th>Containers 20" -  حاوية20قدم </th>';
    pTablesHTML += '                                     <th>Containers 40" -  حاوية40قدم </th>';
    pTablesHTML += '                                     <th>Containers 45" -  حاوية45قدم </th>';
    pTablesHTML += '                                     <th>Reefer 20" -  ثلاجة20قدم </th>';
    pTablesHTML += '                                     <th>Reefer 40" -  ثلاجة40قدم </th>';
    pTablesHTML += '                                     <th>General Cargo -  بضائع عامة </th>';
    pTablesHTML += '                                     <th>Containers Numbers -  أرقام الحاويات </th>';
    pTablesHTML += '                                     <th>Line -  الخط </th>';
    pTablesHTML += '                                     <th>Client Address -  عنوان العميل </th>';
    pTablesHTML += '                                     <th>Client Phones -  تليفون العميل </th>';
    pTablesHTML += '                                 </tr>';
    pTablesHTML += '                             </thead>';
    pTablesHTML += '                             <tbody>';
    //debugger;
    //$.each(JSON.parse(pReportRows), function (i, item) { debugger; });
    $.each((pReportRows), function (i, item) {
        pTablesHTML += '                                     <tr style="font-size:90%;">';
        pTablesHTML += '                                         <td>' + (item.Code == 0 ? "" : item.Code) + '</td>';
        //pTablesHTML += '                                         <td>' + (item.OpenDate == "/Date(-2208996000000)/" ? "" : ConvertDateFormat(GetDateWithFormatMDY(item.OpenDate))) + '</td>';
        pTablesHTML += '                                         <td>' + (item.ClientName == 0 ? "" : item.ClientName) + '</td>';
        pTablesHTML += '                                         <td>' + (item.ContainerTypes20 == 0 || item.TransportType == 2 ? "" : item.ContainerTypes20) + '</td>';
        pTablesHTML += '                                         <td>' + (item.ContainerTypes40 == 0 || item.TransportType == 2 ? "" : item.ContainerTypes40) + '</td>';
        pTablesHTML += '                                         <td>' + (item.ContainerTypes45 == 0 || item.TransportType == 2 ? "" : item.ContainerTypes45) + '</td>';
        pTablesHTML += '                                         <td>' + (item.ContainerTypesReefer20 == 0 || item.TransportType == 2 ? "" : item.ContainerTypesReefer20) + '</td>';
        pTablesHTML += '                                         <td>' + (item.ContainerTypesReefer40 == 0 || item.TransportType == 2 ? "" : item.ContainerTypesReefer40) + '</td>';
        pTablesHTML += '                                         <td>' + (item.GrossWeightSum == 0 ? "" : (item.GrossWeightSum + ' KGM')) + '</td>';
        pTablesHTML += '                                         <td>' + (item.ContainerNumbers == 0 ? "" : item.ContainerNumbers) + '</td>';
        pTablesHTML += '                                         <td>' + (item.LineName == 0 ? "" : item.LineName) + '</td>';
        pTablesHTML += '                                         <td>' + (item.ClientAddress == 0 ? "" : item.ClientAddress) + '</td>';
        pTablesHTML += '                                         <td>' + (item.ClientPhonesAndFaxes == 0 ? "" : item.ClientPhonesAndFaxes) + '</td>';
        ////if (item.RepTransportTypeShown == "AIR" || item.ShipmentTypeCode == "LCL" || item.ShipmentTypeCode == "LTL")
        ////    pTablesHTML += '                                     <td>' + (item.PackageTypes == 0 ? "" : item.PackageTypes) + '</td>';
        ////else
        ////    pTablesHTML += '                                     <td>' + (item.ContainerTypes == 0 ? "" : item.ContainerTypes) + '</td>';
        ////if ($("#cbTEUs").prop("checked"))
        //    pTablesHTML += '                                         <td>' + (item.TEUs == 0 ? "" : item.TEUs) + '</td>';
        pTablesHTML += '                                     </tr>';
    });
    pTablesHTML += '                             </tbody>';
    pTablesHTML += '                         </table>';
    $("#hExportedTable").html(pTablesHTML); //in all cases i put the tables in hidden div so i select each table separately
    if (pOutputTo == "Excel")
        ExportHtmlTableToCsv_RemovingCommasForNumbers("tblWorkFlow", "Work Flow");
    else {
        var mywindow = null;
        if (pOutputTo != "Email")
            mywindow = window.open('', '_blank');
        //  var mywindow = window.open('', '_blank');
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
        ////ReportHTML += '             <div class="col-xs-3"><b>Currency :</b> ' + $("#slCurrency option:selected").text() + '</div>';
        ReportHTML += '             <div class="col-xs-3"><b>Oper. Status :</b> ' + $("#slOperationStages option:selected").text() + '</div>';
        ReportHTML += '             <div class="col-xs-6"><b>Open Date :</b> ' + ($("#txtFromDate").val() == "" && $("#txtToDate").val() == ""
            ? "All Dates"
            : ($("#txtFromDate").val() == "" ? "" : "From " + $("#txtFromDate").val() + " ") + ($("#txtToDate").val() == "" ? "" : "To " + $("#txtToDate").val())) + '</div>';
        if ($("#slOperationStages option:selected").text() == "CLOSED")
            ReportHTML += '             <div class="col-xs-6"><b>Close Date :</b> ' + ($("#txtFromCloseDate").val() == "" && $("#txtToCloseDate").val() == ""
                ? "All Dates"
                : ($("#txtFromCloseDate").val() == "" ? "" : "From " + $("#txtFromCloseDate").val() + " ") + ($("#txtToCloseDate").val() == "" ? "" : "To " + $("#txtToCloseDate").val())) + '</div>';

        ////ReportHTML += '                 <section class="panel panel-default">';
        //ReportHTML += '                     <div class="table-responsive">';
        ReportHTML += '                         <div> &nbsp; </div>'

        ReportHTML += pTablesHTML;

        //ReportHTML += '                     </div>';//of table-responsive
        //ReportHTML += '             <div class="col-xs-12"><b>Payables Summary :</b> ' + pPayablesCurrenciesSummary + '</div>';
        //ReportHTML += '             <div class="col-xs-12"><b>Invoices Summary :</b> ' + pReceivablesOrInvoicesCurrenciesSummary + '</div>';
        //ReportHTML += '             <div class="col-xs-12"><b>Profit Summary :</b> ' + pProfitCurrenciesSummary + '</div>';
        //ReportHTML += '             <div class="col-xs-12"><b>Profit Margin :</b> ' + pMarginSummary + '%</div>';

        ReportHTML += '         </body>';
        ReportHTML += '     <footer class="footer col-xs-12" style="width:100%; position:absolute; bottom:0;">';
        ////ReportHTML += '         <div class="row text-right m-r">الشركه خاضعه لنظام الدفعات المقدمه تطبيقا لأحكام الماده 62 من القانون رقم 91 لسنة 2005 و لا يجوز الخصم عليها</div>';
        //if ($("#tblDocsOut tr[id=" + pDocumentTypeID + "] td.IsPrintISOCode input[type=checkbox]").prop("checked"))
        //    ReportHTML += '     <div class="row m-l">' + $("#tblDocsOut tr[id=" + pDocumentTypeID + "] td.ISOCode").text() + '</div>';
        //ReportHTML += '         <div class="row text-center"><img src="/Content/Images/CompanyFooter.jpg" alt="footer"/></div>';
        ReportHTML += '     </footer>';

        ReportHTML += '</html>';
        if (pOutputTo != "Email") {
            mywindow.document.write(ReportHTML);
            mywindow.document.close();
        }
        else {
            SendPDF_ReportByEmail($('#txtToEmail').val(), ReportHTML, "Work Flow From" + $("#txtToDate").val() + " To " + $("#txtFromDate").val(), true);
        }

    }
}
/*************************BL Statistics***************************/
function BLStatistics_DrawReport(data, pOutputTo) {
    debugger;
    var pReportRows = JSON.parse(data[1]);
    var pReportTitle = "HBLs Statistics";
    var TodaysDateddMMyyyy = getTodaysDateInddMMyyyyFormat();
    var _BranchIDs = "";
    for (var i = 1; i < ($("#slBranch option").length + 1); i++)
        _BranchIDs += _BranchIDs == "" ? $("#slBranch option:nth-child(" + (i) + ")").val() : ("," + $("#slBranch option:nth-child(" + (i) + ")").val());
    var ArrBranchIDs = _BranchIDs.split(",");

    var pTablesHTML = "";
    //ReportHTML += '                         <table id="tblOperationsStatistics" class="table table-striped text-sm table-hover b-t b-r b-b b-l print">';//remove t1 class to remove row numbers
    pTablesHTML += '                         <table id="tblOperationsStatistics" class="table table-striped text-sm table-bordered  " style="border:solid #000 !important;">';//style="border-left:solid #999 !important;border-top:solid #999 !important;"
    pTablesHTML += '                             <thead>';
    pTablesHTML += '                                 <tr class="" style="font-size:90%;">';
    if ($("#cbBLStatisticsHouseNumber").prop("checked"))
        pTablesHTML += '                                     <th>HouseNo</th>';
    if ($("#cbBLStatisticsDismissalPermissionSerial").prop("checked"))
        pTablesHTML += '                                     <th>DismissalNo.</th>';
    if (pDefaults.UnEditableCompanyName == "GLS")
        pTablesHTML += '                                     <th>صرف بتاريخ</th>';
    if ($("#cbBLStatisticsClient").prop("checked"))
        pTablesHTML += '                                     <th>Client</th>';
    if ($("#cbBLStatisticsPOrC").prop("checked"))
        pTablesHTML += '                                     <th>P/C</th>';
    if ($("#cbBLStatisticsCustomerReference").prop("checked"))
        pTablesHTML += '                                     <th>CustomerRef</th>';
    if ($("#cbBLStatisticsForwarderOrNotify").prop("checked"))
        pTablesHTML += '                                     <th>Forwarder/Notify</th>';
    if ($("#cbBLStatisticsGrossWeight").prop("checked"))
        pTablesHTML += '                                     <th>Gross(KG)</th>';
    if ($("#cbBLStatisticsVolume").prop("checked"))
        pTablesHTML += '                                     <th>Vol(CBM)</th>';
    if ($("#cbBLStatisticsPackages").prop("checked"))
        pTablesHTML += '                                     <th>Packages</th>';
    //if ($("#cbBLStatisticsContainerTypes").prop("checked"))
    //    pTablesHTML += '                                     <th class="hide">ContainerTypes</th>';
    if ($("#cbBLStatisticsContainerTypes").prop("checked"))
        pTablesHTML += '                                     <th>Container Nos</th>';
    if ($("#cbBLStatisticsGoodsDescription").prop("checked"))
        pTablesHTML += '                                     <th>Goods Desc.</th>';
    if ($("#cbBLStatisticsMovementType").prop("checked"))
        pTablesHTML += '                                     <th>Service Type</th>';
    if ($("#cbBLStatisticsSalesman").prop("checked"))
        pTablesHTML += '                                     <th>Salesman</th>';
    if ($("#cbBLStatisticsETA").prop("checked"))
        pTablesHTML += '                                     <th>ETA</th>';
    if ($("#cbBLStatisticsATA").prop("checked"))
        pTablesHTML += '                                     <th>ATA</th>';
    //pTablesHTML += '                                     <th class="hide">Final Destination</th>';
    if ($("#cbBLStatisticsInvoices").prop("checked"))
        pTablesHTML += '                                     <th>Invoices</th>';
    if ($("#cbBLStatisticsVessel").prop("checked"))
        pTablesHTML += '                                     <th>Vessel</th>';
    if ($("#cbBLStatisticsPOL").prop("checked"))
        pTablesHTML += '                                     <th>POL</th>';
    if ($("#cbBLStatisticsReleaseDate").prop("checked"))
        pTablesHTML += '                                     <th>ReleaseDate</th>';
    if ($("#cbBLStatisticsWarehouse").prop("checked"))
        pTablesHTML += '                                     <th>Warehouse</th>';
    if ($("#cbBLStatisticsPODate").prop("checked"))
        pTablesHTML += '                                     <th>' + (pDefaults.UnEditableCompanyName == "GLS" ? 'StoringDate' : 'PODate') + '</th>';
    if ($("#cbBLStatisticsNotes").prop("checked"))
        pTablesHTML += '                                     <th>Notes</th>';
    pTablesHTML += '                                 </tr>';
    pTablesHTML += '                             </thead>';
    pTablesHTML += '                             <tbody>';
    $.each((pReportRows), function (i, item) {
        pTablesHTML += '                                     <tr style="font-size:90%;">';
        //pTablesHTML += '                                         <td class="' + (item.BLType != constHouseBLType ? 'classNotHouse' : '') + '">' + (item.Code == 0 ? "" : item.Code) + '</td>';
        if ($("#cbBLStatisticsHouseNumber").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.HouseNumber == 0 ? "" : item.HouseNumber) + '</td>';
        if ($("#cbBLStatisticsDismissalPermissionSerial").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.DismissalPermissionSerial == 0 ? "" : item.DismissalPermissionSerial) + '</td>';
        if (pDefaults.UnEditableCompanyName == "GLS")
            pTablesHTML += '                                         <td>' + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.ReleaseDate)) < 1 ? "" : (ConvertDateFormat(GetDateWithFormatMDY(item.ReleaseDate)) + '\u2008')) + '</td>';
        if ($("#cbBLStatisticsClient").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.ClientName == 0 ? "" : item.ClientName) + '</td>';
        if ($("#cbBLStatisticsPOrC").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.POrC == 1 ? "C" : (item.POrC == 3 ? "P" : "")) + '</td>';
        if ($("#cbBLStatisticsCustomerReference").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.CustomerReference == 0 ? "" : item.CustomerReference) + '</td>';
        if ($("#cbBLStatisticsForwarderOrNotify").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.Notify1Name == 0 ? "" : item.Notify1Name) + '</td>';
        if ($("#cbBLStatisticsGrossWeight").prop("checked"))
            pTablesHTML += '                                         <td class="' + 'GrossWeightSum' + '">' + (item.GrossWeightSum == 0 ? "" : item.GrossWeightSum) + '</td>';
        if ($("#cbBLStatisticsVolume").prop("checked"))
            pTablesHTML += '                                         <td class="' + 'VolumeSum' + '">' + (item.VolumeSum == 0 ? "" : item.VolumeSum) + '</td>';
        if ($("#cbBLStatisticsPackages").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.NumberOfPackages + 'x' + item.PackageTypeName) + '</td>';
        //if ($("#cbBLStatisticsContainerTypes").prop("checked"))
        //    pTablesHTML += '                                         <td class="hide">' + (item.ContainerTypes == 0 ? "" : item.ContainerTypes) + '</td>';
        if ($("#cbBLStatisticsContainerTypes").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.ContainerNumbers == 0 ? "" : item.ContainerNumbers) + '</td>';
        if ($("#cbBLStatisticsGoodsDescription").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.DescriptionOfGoods == 0 ? "" : item.DescriptionOfGoods) + '</td>';
        if ($("#cbBLStatisticsMovementType").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.MoveTypeName == 0 ? "" : item.MoveTypeName) + '</td>';
        if ($("#cbBLStatisticsSalesman").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.Salesman == 0 ? "" : item.Salesman) + '</td>';
        if ($("#cbBLStatisticsETA").prop("checked"))
            pTablesHTML += '                                         <td>' + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.ExpectedArrival)) < 1 ? "" : (ConvertDateFormat(GetDateWithFormatMDY(item.ExpectedArrival)) + '\u2008')) + '</td>';
        if ($("#cbBLStatisticsATA").prop("checked"))
            pTablesHTML += '                                         <td>' + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.ActualArrival)) < 1 ? "" : (ConvertDateFormat(GetDateWithFormatMDY(item.ActualArrival)) + '\u2008')) + '</td>';
        //pTablesHTML += '                                         <td class="hide">' + (item.DeliveryCityName == 0 ? "" : item.DeliveryCityName) + '</td>';
        if ($("#cbBLStatisticsInvoices").prop("checked"))
            pTablesHTML += '                                         <td style="font-size:90%;text-align:left;">' + (item.InvoiceNumbers == 0 ? "" : item.InvoiceNumbers.replace(/\,/g, "<br/>")) + '</td>';
        if ($("#cbBLStatisticsVessel").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.VesselName == 0 ? "" : item.VesselName) + '</td>';
        if ($("#cbBLStatisticsPOL").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.POLName == 0 ? "" : item.POLName) + '</td>';
        if ($("#cbBLStatisticsReleaseDate").prop("checked"))
            pTablesHTML += '                                         <td>' + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.ReleaseDate)) < 1 ? "" : (ConvertDateFormat(GetDateWithFormatMDY(item.ReleaseDate)) + '\u2008')) + '</td>';
        if ($("#cbBLStatisticsWarehouse").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.MainRouteWarehouse == 0 ? "" : item.MainRouteWarehouse) + '</td>';
        if ($("#cbBLStatisticsPODate").prop("checked"))
            pTablesHTML += '                                         <td>' + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.PODate)) < 1 ? "" : (ConvertDateFormat(GetDateWithFormatMDY(item.PODate)) + '\u2008')) + '</td>';
        if ($("#cbBLStatisticsNotes").prop("checked"))
            pTablesHTML += '                                         <td>' + (item.Notes == 0 ? "" : item.Notes) + '</td>';
        pTablesHTML += '                                         <td class="hide ' + ('BranchID' + item.BranchID) + '">' + '' + '</td>';
        pTablesHTML += '                                         <td class="hide ' + ('TransportType' + item.TransportType + ' DirectionType' + item.DirectionType) + '">' + '' + '</td>';
        pTablesHTML += '                                     </tr>';
    });
    pTablesHTML += '                             </tbody>';
    pTablesHTML += '                         </table>';
    $("#hExportedTable").html(pTablesHTML); //in all cases i put the tables in hidden div so i select each table separately
    if (pOutputTo == "Excel") {
        var pBranchSummary = "";
        /********************************Adding Summary*************************************/
        for (i = 0; i < ArrBranchIDs.length; i++) {
            if ($("#tblOperationsStatistics tbody tr").find("td.BranchID" + ArrBranchIDs[i]).length != 0)
                pBranchSummary += $("#slBranch option[value=" + ArrBranchIDs[i] + "]").text() + " branch: " + $("#tblOperationsStatistics tbody tr").find("td.BranchID" + ArrBranchIDs[i]).length + " Operations" + "<br>";
        }
        debugger;
        if (pOutputTo == "Excel") {
            var pExcelSummary = "<tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>";
            pExcelSummary += "<tr><td>Import:" + $("#tblOperationsStatistics tbody tr").find("td.DirectionType1").length + " operations" + "</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>";
            pExcelSummary += "<tr><td>Export:" + $("#tblOperationsStatistics tbody tr").find("td.DirectionType2").length + " operations" + "</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>";
            pExcelSummary += "<tr><td>Domestic:" + $("#tblOperationsStatistics tbody tr").find("td.DirectionType3").length + " operations" + "</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>";
            pExcelSummary += "<tr><td>Ocean:" + $("#tblOperationsStatistics tbody tr").find("td.TransportType1").length + " operations" + "</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>";
            pExcelSummary += "<tr><td>Air:" + $("#tblOperationsStatistics tbody tr").find("td.TransportType2").length + " operations" + "</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>";
            pExcelSummary += "<tr><td>Inland:" + $("#tblOperationsStatistics tbody tr").find("td.TransportType3").length + " operations" + "</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>";
            pExcelSummary += "<tr><td>" + pBranchSummary + "</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>";
            //pExcelSummary += "<tr><td>Total:" + $("#tblOperationsStatistics tbody tr").length + " operations" + "</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>";
            pExcelSummary += "<tr><td>Total:" + $("#tblOperationsStatistics tbody tr").find("td.classNotHouse").length + " operations" + "</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>";
            //$("#tblOperationsStatistics tbody").append(pExcelSummary);
        }

        //ExportHtmlTableToCsv_RemovingCommasForNumbers("tblOperationsStatistics", pReportTitle);
        ExportHtmlTableToCsv("tblOperationsStatistics", pReportTitle);
    }
    else {

        var mywindow = null;
        if (pOutputTo != "Email")
            mywindow = window.open('', '_blank');
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
        ////ReportHTML += '             <div class="col-xs-3"><b>Currency :</b> ' + $("#slCurrency option:selected").text() + '</div>';
        ReportHTML += '             <div class="col-xs-3"><b>Oper. Status :</b> ' + $("#slOperationStages option:selected").text() + '</div>';
        ReportHTML += '             <div class="col-xs-6"><b>Open Date :</b> ' + ($("#txtFromDate").val() == "" && $("#txtToDate").val() == ""
            ? "All Dates"
            : ($("#txtFromDate").val() == "" ? "" : "From " + $("#txtFromDate").val() + " ") + ($("#txtToDate").val() == "" ? "" : "To " + $("#txtToDate").val())) + '</div>';
        if ($("#slOperationStages option:selected").text() == "CLOSED")
            ReportHTML += '             <div class="col-xs-6"><b>Close Date :</b> ' + ($("#txtFromCloseDate").val() == "" && $("#txtToCloseDate").val() == ""
                ? "All Dates"
                : ($("#txtFromCloseDate").val() == "" ? "" : "From " + $("#txtFromCloseDate").val() + " ") + ($("#txtToCloseDate").val() == "" ? "" : "To " + $("#txtToCloseDate").val())) + '</div>';
        ////ReportHTML += '                 <section class="panel panel-default">';
        //ReportHTML += '                     <div class="table-responsive">';
        ReportHTML += '                         <div> &nbsp; </div>'

        ReportHTML += pTablesHTML;

        ReportHTML += '             <div class="col-xs-4 hide"><b>Import :</b> ' + $("#tblOperationsStatistics tbody tr").find("td.DirectionType1").length + " Operations"
            + '<br><b>Export :</b> ' + $("#tblOperationsStatistics tbody tr").find("td.DirectionType2").length + " Operations"
            + '<br><b>Domestic :</b> ' + $("#tblOperationsStatistics tbody tr").find("td.DirectionType3").length + " Operations"
            + '</div>';

        ReportHTML += '             <div class="col-xs-4 hide"><b>Ocean :</b> ' + $("#tblOperationsStatistics tbody tr").find("td.TransportType1").length + " Operations"
            + '<br><b>Air :</b> ' + $("#tblOperationsStatistics tbody tr").find("td.TransportType2").length + " Operations"
            + '<br><b>Inland :</b> ' + $("#tblOperationsStatistics tbody tr").find("td.TransportType3").length + " Operations"
            + '</div>';

        ReportHTML += '             <div class="col-xs-4 hide">'
            + ($("#cbGrossWeightSum").prop("checked") ? ('Total Gross :</b> ' + GetColumnSum("tblOperationsStatistics", "GrossWeightSum") + " KGM<br>") : "")
            + ($("#cbNetWeightSum").prop("checked") ? ('Total Net :</b> ' + parseFloat(GetColumnSum("tblOperationsStatistics", "NetWeightSum")).toFixed(2) + " KGM<br>") : "")
            + ($("#cbVolumeSum").prop("checked") ? ('<b>Total Vol :</b> ' + GetColumnSum("tblOperationsStatistics", "VolumeSum") + " CBM<br>") : "")
            + ($("#cbChargeableWeightSum").prop("checked") ? ('<b>Total Chg.Wt :</b> ' + GetColumnSum("tblOperationsStatistics", "ChargeableWeightSum") + " <br>") : "")
            + '</div>';

        for (i = 0; i < ArrBranchIDs.length; i++) {
            if ($("#tblOperationsStatistics tbody tr").find("td.BranchID" + ArrBranchIDs[i]).length != 0)
                ReportHTML += '             <div class="col-xs-12" style="clear:both;"><b>' + $("#slBranch option[value=" + ArrBranchIDs[i] + "]").text() + " branch: " + '</b> ' + $("#tblOperationsStatistics tbody tr").find("td.BranchID" + ArrBranchIDs[i]).length + " B/Ls" + '</div>';
        }
        //ReportHTML += '             <div class="col-xs-12"><b>Total :</b> ' + $("#tblOperationsStatistics tbody tr").length + " operations" + '</div>';
        ReportHTML += '             <div class="col-xs-12 hide"><b>Total :</b> ' + $("#tblOperationsStatistics tbody tr").find("td.classNotHouse").length + " operations" + '</div>';
        //ReportHTML += '                     </div>';//of table-responsive
        //ReportHTML += '             <div class="col-xs-12"><b>Payables Summary :</b> ' + pPayablesCurrenciesSummary + '</div>';
        //ReportHTML += '             <div class="col-xs-12"><b>Invoices Summary :</b> ' + pReceivablesOrInvoicesCurrenciesSummary + '</div>';
        //ReportHTML += '             <div class="col-xs-12"><b>Profit Summary :</b> ' + pProfitCurrenciesSummary + '</div>';
        //ReportHTML += '             <div class="col-xs-12"><b>Profit Margin :</b> ' + pMarginSummary + '%</div>';

        ReportHTML += '         </body>';
        ReportHTML += '     <footer class="footer col-xs-12" style="width:100%; position:absolute; bottom:0;">';
        ////ReportHTML += '         <div class="row text-right m-r">الشركه خاضعه لنظام الدفعات المقدمه تطبيقا لأحكام الماده 62 من القانون رقم 91 لسنة 2005 و لا يجوز الخصم عليها</div>';
        //if ($("#tblDocsOut tr[id=" + pDocumentTypeID + "] td.IsPrintISOCode input[type=checkbox]").prop("checked"))
        //    ReportHTML += '     <div class="row m-l">' + $("#tblDocsOut tr[id=" + pDocumentTypeID + "] td.ISOCode").text() + '</div>';
        //ReportHTML += '         <div class="row text-center"><img src="/Content/Images/CompanyFooter.jpg" alt="footer"/></div>';
        ReportHTML += '     </footer>';

        ReportHTML += '</html>';
        if (pOutputTo != "Email") {
            mywindow.document.write(ReportHTML);
            mywindow.document.close();
        }
        else {
            SendPDF_ReportByEmail($('#txtToEmail').val(), ReportHTML, pReportTitle, true);
        }
    }
}
function SetSelectedColumns(pFlag) {
    debugger;
    $(".classCheckColumn:not(.hide)").prop("checked", pFlag);
}