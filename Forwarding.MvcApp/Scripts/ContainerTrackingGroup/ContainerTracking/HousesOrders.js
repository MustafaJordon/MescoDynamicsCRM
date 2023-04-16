

var TodaysDate = new Date();
var FormattedTodaysDate = TodaysDate.toLocaleDateString();

var pIsOperationDisabled = false;
var pIsFCL = false;
var pIsFTL = false;

function HousesOrders_Initialize() {
    debugger;
    FormID = constOperationsFormID; //to get privilage of operations form for get permissions
    strLoadWithPagingFunctionName = "/api/Operations/LoadWithWhereClause";

    IntializeOperationAutoCompleteSearch();
    ConfigureAfterOperationChangeEvent();

    $('.classSearchOperations').on('keypress', function (args) {
        if (args.keyCode == 13) {
            $("#btnFilterOperationsApply").click();
            return false;
        }
    });

    $.getScript(strServerURL + '/Scripts/Operations/Operations/DocsOut.js', function () {
        //DocsOut_LoadAll(0, "slDocsOutTypesOutsideModal", false);
    });

    if (glbCallingControl == "ShippingOrders") {
        $("#h3Operations").text('Shipping Orders');
    } else if (glbCallingControl == "RoutingOrders") {
        $("#h3Operations").text('Routing Orders');
    }

    var pControllerParameters = {
        pIsLoadArrayOfObjects: true
        , pPageNumber: 1
        , pPageSize: $("#select-page-size option:selected").text()
        , pWhereClause: Operations_GetFilterWhereClause()
        , pIsBindTableRows: false
        , pWhereClause_Routings: "0"
        , pOrderBy: "ID DESC"
    };
    LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned("div-Pager", "select-page-size", "spn-first-page-row", "spn-last-page-row", "spn-total-count", "div-Text-Total", strLoadWithPagingFunctionName, /*pWhereClause*/"dummy", "ID DESC", 1, 25, pControllerParameters
        , function (pData) {
            strBindTableRowsFunctionName = "HousesOrders_BindTableRows";
            HousesOrders_BindTableRows(JSON.parse(pData[0]));

            var pCustomers = pData[2]; var pAgents = pData[3]; var pVessels = pData[4]; var pContainerTypes = pData[5]; var pCountry = pData[6]; var pMoveTypes = pData[7]; var pShippingLines = pData[8];
            var pTruckers = pData[9]; var pUsers = pData[10]; var pCommodity = pData[11]; var pTypeOfStock = pData[12]; var pCCA = pData[13]; var pAirlines = pData[14];


            $("#slFilterBranch").html("<option value=''><--Select--></option>");
            $("#slFilterBranch").append($("#hReadySlBranches").html());
            $("#slFilterBranch").val("");
            ////FillListFromObject(null, 2, "<--All-->", "slFilterShipper", pCustomers, function(){ $("#slFilterConsignee").html($("#slFilterShipper").html());$("#slFilterNotify").html($("#slFilterShipper").html());});

            $("#slFilterShipper").html($("#hReadySlCustomers").html());
            $("#slFilterConsignee").html($("#hReadySlCustomers").html());
            $("#slFilterBookingParty").html($("#hReadySlCustomers").html());

            //$("#slFilterNotify").html($("#hReadySlCustomers").html());

            GetListWithNameAndWhereClause((glbOperationStageFilter == "" ? null : glbOperationStageFilter), "/api/NoAccessQuoteAndOperStages/LoadAll", "<--Select-->", "ulOperationStages", " WHERE IsOperationStage = 1  AND IsInActive = 0 ORDER BY ViewOrder ");
            ApplySelectListSearch();

            //GetListWithCodeAndWhereClause(null, "/api/Operations/LoadAll", "Select Master Operation", "slMasterOperations", " WHERE 1=1 AND BLType=" + constMasterBLType + " ");
            GetListWithCodeAndWhereClauseWithMultiAttrs(null, "POL,POD,TransportType,ShipmentType", "/api/Operations/LoadAll", "Select Master Operation"
                , "hslMasterOperations"
                , " WHERE 1=1 AND BLType=" + constMasterBLType + " "
                , null);

            CallGETFunctionWithParameters("/api/Operations/LoadFilters", { pDummyParameter: 0 }
                , function (pData) {

                    pAgents = pData[0]; var pVessels = pData[1]; var pContainerTypes = pData[2]; var pCountry = pData[3]; var pMoveTypes = pData[4]; var pShippingLines = pData[5];
                    var pTruckers = pData[6]; var pUsers = pData[7]; var pCommodity = pData[8]; var pTypeOfStock = pData[9]; var pCCA = pData[10]; var pAirlines = pData[11];

                    var _Salesmentemp = JSON.parse(pUsers);
                    var pSalesmen = jQuery.grep(_Salesmentemp, function (_Salesmentemp) {
                        return _Salesmentemp.IsSalesman == true;
                    });

                    //FillListFromObject(null, 2, "<--Select-->", "slVesselCCModule", pVessels, null);
                    //FillListFromObject(null, 2, "<--Select-->", "slCommodityCCModule", pCommodity, null);
                    //FillListFromObject(null, 2, "<--Select-->", "slOriginCountryCCModule", pCountry, null);

                    FillListFromObject(null, 2, "<--Select-->", "slFilterPOLCountry", pCountry, function () {
                        $("#slFilterPODCountry").html($("#slFilterPOLCountry").html());
                        $("#slOriginCountryCCModule").html($("#slFilterPOLCountry").html());
                    });
                    FillListFromObject(null, 2, "<--Select-->", "slFilterAgent", pAgents, null);
                    FillListFromObject(null, 2, "<--Select-->", "slFilterMoveType", pMoveTypes, null);
                    $("#slFilterPOLCountry").attr("onchange"
                        , 'FilterListByAnotherListID(null, "/api/Ports/LoadAll", "<--Select-->", "slFilterPOL", "slFilterPOLCountry", "CountryID");');
                    $("#slFilterPODCountry").attr("onchange"
                        , 'FilterListByAnotherListID(null, "/api/Ports/LoadAll", "<--Select-->", "slFilterPOD", "slFilterPODCountry", "CountryID");');
                    FillListFromObject(null, 2, "<--Select-->", "slOperationVessels", pVessels, function () {
                        $("#slFilterVessel").html($("#slOperationVessels").html());
                        $("#slVesselCCModule").html($("#slOperationVessels").html());
                    });
                    FillListFromObject(null, 1, "<--Select-->", "slOperationContainerType", pContainerTypes, function () { $("#slOperationContainerType2").html($("#slOperationContainerType").html()); $("#slOperationContainerType3").html($("#slOperationContainerType").html()); });
                    FillListFromObject(null, 2, "<--Select-->", "slFilterShippingLine", pShippingLines, function () { $("#slLineCCModule").html($("#slFilterShippingLine").html()) });
                    FillListFromObject(null, 2, "<--Select-->", "slFilterAirline", pAirlines, null);
                    FillListFromObject(null, 2, "<--Select-->", "slFilterTrucker", pTruckers, null);
                    FillListFromObject(null, 2, "<--Select-->", "slFilterCCA", pCCA, function () { $("#slOperationCCA").html($("#slFilterCCA").html()) });
                    FillListFromObject(null, 2, "<--Select-->", "slCommodity", pCommodity, function () { $("#slCommodityCCModule").html($("#slCommodity").html()); $("#slCertificateCommodity").html($("#slCommodity").html()); });
                    FillListFromObject(null, 2, "<--Select-->", "slTypeOfStock", pTypeOfStock, null);
                    FillListFromObject(null, 2, "<--Select-->", "slFilterCreator", pUsers, null);
                    FillListFromObject(null, 2, "<--Select-->", "slFilterOperationMan", pUsers, null);
                    FillListFromObject(null, 2, "<--Select-->", "slFilterSalesman", JSON.stringify(pSalesmen), function () { $("#slOperationSalesman").html($("#slFilterSalesman").html()); });

                    //FadePageCover(false);

                    $("#slClientCCModule").html($("#hReadySlCustomers").html());
                    GetListWithNameAndWhereClauseWithMultiAttrs(null, "Notes", "/api/TrackingStage/LoadAll", TranslateString("SelectFromMenu")
                        , "slStatusCCModule"
                        , "WHERE 1=1 AND IsClearance=1"
                        , null);
                    CallGETFunctionWithParameters("/api/Custody/LoadAll", { pWhereClause: 'WHERE 1=1' }
                        , function (pData) {
                            FillListFromObject(null, 2, "<--Select-->", "slCustodyCCModule", pData[0], null);
                        }, null);
                    // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
                    if (IsNull(pDefaults.ShowUserSalesmen, "false") == true) {
                        $("#slConsignees").html("");
                        $("#slShippers").html("");
                        $('#slOperationSalesman').on('change', function () {
                            //FadePageCover(true);

                            CallGETFunctionWithParameters("/api/Customers/LoadAllWithMinimalColumns"
                                , {
                                    pWhereClauseWithMinimalColumns: (($("#sp-LoginName").text() == "BG EGYPT" ? "WHERE Name=N'BG EGYPT'" : "WHERE 1=1") + "  AND SalesmanID = " + $('#slOperationSalesman').val() + " ")
                                    , pOrderBy: "Name"
                                }
                                , function (pData) {
                                    FillListFromObject(null, 2, TranslateString("SelectFromMenu"), "hReadySlCustomers", pData[0], function () {
                                        try {
                                            $("#slConsignees").prop('disabled', false);
                                            $("#slShippers").prop('disabled', false);
                                            $("#slConsignees").html($("#hReadySlCustomers").html());
                                            $("#slShippers").html($("#hReadySlCustomers").html());
                                            FadePageCover(false)
                                        }
                                        catch (ex) {
                                            console.log('Consignee or shipper is not define')
                                            FadePageCover(false)
                                        }
                                    });

                                }
                                , null);
                        });


                    }
                    //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

                }
                , null);
            //FillListFromObject(null, 2, TranslateString("SelectFromMenu"), "slOperationMoveTypes", pMoveTypes, null);
            //$("#asideSearch").removeClass("hide");
        }, true);
    if ($("[id$='hf_ChangeLanguage']").val() == "ar") $(".swapChildrenClass:not(.reversed)").reverseChildren();
    $("#div-main-options").width($("#mainForm").width() - 185);
    $("#divTblOperations").width($("#mainForm").width() - 185);
    //$("#divTblOperations").height($("#mainForm").height() - 360);
}

function ConfigureAfterOperationChangeEvent() {
    $("#slMasterOperations").off('change').on('change', function () {

        if ($("#slMasterOperations").val() == $("#slMasterOperations option:selected").text()) {
            swal('اختيار عملية خاطئء - Is Not Correct Selected Operation');
            window.CurrentOperationID = null;
            //CLearOperationInfo();
        }
        else {
            console.log("operation : " + $("#slMasterOperations").val() + " Is Selected");
            window.CurrentOperationID = $("#slMasterOperations").val();

            $("#hslMasterOperations").val($("#slMasterOperations").val());

            //GetOperationInfoByID($("#slMasterOperations").val(),
            //    function (Operation) {
            //        FillOperationInfo(Operation, true);
            //    });
        }
    }
    );

}

function IntializeOperationAutoCompleteSearch() {
    debugger;
    $("#slMasterOperations").css({ 'width': '100%' }).select2({
        minimumInputLength: 1,
        tags: [],
        ajax: {
            url: strServerURL + "/api/Operations/GetMasterOperationsByCode",
            dataType: 'json',
            type: "GET",
            contentType: "application/json; charset=utf-8",
            quietMillis: 50,
            data: function (params) {
                var query = {
                    term: params.term
                }

                return query;
            },
            processResults: function (data) {

                var d = JSON.parse(data[0]);
                return {
                    results: $.map(d, function (item) {
                        return {
                            text: item.Code,
                            id: item.ID,
                            value: item.ID,
                            pol: item.POL,
                            pod: item.POD
                        };
                    })
                };
            }
        }
    });
}


function Operations_LoadingWithPaging() {
    debugger;
    let pWhereClause = Operations_GetFilterWhereClause();
    let pOrderBy = " ID DESC ";
    let pControllerParameters = {
        pIsLoadArrayOfObjects: false
        , pPageNumber: 1
        , pPageSize: $("#select-page-size option:selected").text()
        , pWhereClause: pWhereClause
        , pIsBindTableRows: false
        , pWhereClause_Routings: "0"
        , pOrderBy: "ID DESC"
    }
    LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned("div-Pager", "select-page-size", "spn-first-page-row", "spn-last-page-row", "spn-total-count", "div-Text-Total", "api/Operations/LoadWithWhereClause", pWhereClause, "ID DESC", 1, 25, pControllerParameters
        , function (pData) {
            HousesOrders_BindTableRows(JSON.parse(pData[0]));
        });
    //HighlightText("#tblOperations>tbody>tr", $("#txt-Search").val().trim());
}
function Operations_GetFilterWhereClause() {
    debugger;
    let pWhereClause = "WHERE 1=1 " + " \n";

    pWhereClause += "AND BLType=2 " + " \n";                        // Houses Only
    pWhereClause += "AND MasterOperationID IS NULL " + " \n";       // Not Connected Yet

    if (glbCallingControl == "ShippingOrders") {
        pWhereClause += "AND DirectionType=2 " + " \n";    // Export
    } else if (glbCallingControl == "RoutingOrders") {
        pWhereClause += "AND DirectionType=1 " + " \n";    // Import
    }


    let pTransportFilter = "";
    let pDirectionFilter = "";
    let pBLTypeFilter = "";
    let pOperationStageFilter = ($("#ulOperationStages").val() == "" || $("#ulOperationStages option:selected").text() == "" ? "" : " ( OperationStageName=N'" + $("#ulOperationStages option:selected").text() + "')"); //if 0 then all stages

    if (pOperationStageFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pOperationStageFilter;

    if ($("#txtFilterOperationCode").val().trim() != "") {
        pWhereClause += " AND (CodeSerial=" + $("#txtFilterOperationCode").val().trim();
        pWhereClause += "       OR MasterOperationCodeSerial=" + $("#txtFilterOperationCode").val().trim();
        pWhereClause += ")";
    }
    if ($("#slFilterBranch").val() != null && $("#slFilterBranch").val() != "")
        pWhereClause += " AND BranchID =" + $("#slFilterBranch").val();
    if ($("#slFilterCreator").val() != null && $("#slFilterCreator").val() != "")
        pWhereClause += " AND CreatorUserID =" + $("#slFilterCreator").val();
    if ($("#slFilterOperationMan").val() != null && $("#slFilterOperationMan").val() != "")
        pWhereClause += " AND OperationManID =" + $("#slFilterOperationMan").val();
    if ($("#slFilterSalesman").val() != null && $("#slFilterSalesman").val() != "")
        pWhereClause += " AND SalesmanID =" + $("#slFilterSalesman").val();
    if ($("#txtFilterInvoiceNumber").val().trim() != "" && pWhereClause !== "")
        pWhereClause += " AND (InvoiceNumbers like '" + $("#txtFilterInvoiceNumber").val().trim().toUpperCase() + "/%' OR InvoiceNumbers like '%," + $("#txtFilterInvoiceNumber").val().trim().toUpperCase() + "/%')";

    if ($("#slFilterDirection").val() != null && $("#slFilterDirection").val() != "" && pWhereClause !== "")
        pWhereClause += " AND DirectionType =" + $("#slFilterDirection").val();
    if ($("#slFilterTransport").val() != null && $("#slFilterTransport").val() != "" && pWhereClause !== "")
        pWhereClause += " AND TransportType =" + $("#slFilterTransport").val();
    if ($("#slFilterShipmentType").val() != null && $("#slFilterShipmentType").val() != "" && pWhereClause !== "")
        pWhereClause += " AND ShipmentType =" + $("#slFilterShipmentType").val();

    if ($("#txtFilterClientName").val().trim() != "" && pWhereClause !== "")
        pWhereClause += " AND ClientName like '%" + $("#txtFilterClientName").val().trim().toUpperCase() + "%' ";
    if ($("#txtFilterGrossWeight").val().trim() != "" && pWhereClause !== "")
        pWhereClause += " AND GrossWeightSum = '" + $("#txtFilterGrossWeight").val().trim().toUpperCase() + "' ";
    if ($("#txtFilterContainerNumber").val().trim() != "" && pWhereClause !== "") {
        pWhereClause += " AND (ContainerNumber like '%" + $("#txtFilterContainerNumber").val().trim().toUpperCase() + "%' ";
        pWhereClause += "      OR ContainerNumbers like '%" + $("#txtFilterContainerNumber").val().trim().toUpperCase() + "%') ";
    }
    if ($("#slFilterShipper").val() != null && $("#slFilterShipper").val() != "" && pWhereClause !== "")
        pWhereClause += " AND ShipperID =" + $("#slFilterShipper").val();
    if ($("#slFilterCCA").val() != null && $("#slFilterCCA").val() != "" && pWhereClause !== "")
        pWhereClause += " AND CustomsClearanceAgentID =" + $("#slFilterCCA").val();
    if ($("#slFilterConsignee").val() != null && $("#slFilterConsignee").val() != "" && pWhereClause !== "")
        pWhereClause += " AND ConsigneeID =" + $("#slFilterConsignee").val();
    if ($("#slFilterAgent").val() != null && $("#slFilterAgent").val() != "" && pWhereClause !== "")
        pWhereClause += " AND AgentID =" + $("#slFilterAgent").val();
    if ($("#slFilterBookingParty").val() != null && $("#slFilterBookingParty").val() != "" && pWhereClause !== "")
        pWhereClause += " AND BookingPartyID =" + $("#slFilterBookingParty").val();
    if ($("#slFilterMoveType").val() != null && $("#slFilterMoveType").val() != "" && pWhereClause !== "")
        pWhereClause += " AND MoveTypeID =" + $("#slFilterMoveType").val();
    if ($("#slFilterAirline").val() != null && $("#slFilterAirline").val() != "" && pWhereClause !== "")
        pWhereClause += " AND AirlineID =" + $("#slFilterAirline").val();
    if ($("#slFilterShippingLine").val() != null && $("#slFilterShippingLine").val() != "" && pWhereClause !== "")
        pWhereClause += " AND ShippingLineID =" + $("#slFilterShippingLine").val();
    if ($("#slFilterVessel").val() != null && $("#slFilterVessel").val() != "" && pWhereClause !== "")
        pWhereClause += " AND VesselID =" + $("#slFilterVessel").val();
    if ($("#slFilterTrucker").val() != null && $("#slFilterTrucker").val() != "" && pWhereClause !== "")
        pWhereClause += " AND ((ISNULL((SELECT COUNT(op.ID) FROM dbo.OperationPartners AS op WHERE dbo.vwOperations.ID = op.OperationID AND op.TruckerID = " + $("#slFilterTrucker").val().trim().toUpperCase() + "), 0)) > 0)";
    if ($("#slFilterPOLCountry").val() != null && $("#slFilterPOLCountry").val() != "" && pWhereClause !== "")
        pWhereClause += " AND POLCountryID =" + $("#slFilterPOLCountry").val();
    if ($("#slFilterPOL").val() != null && $("#slFilterPOL").val() != "" && pWhereClause !== "")
        pWhereClause += " AND POL =" + $("#slFilterPOL").val();
    if ($("#slFilterPODCountry").val() != null && $("#slFilterPODCountry").val() != "" && pWhereClause !== "")
        pWhereClause += " AND PODCountryID =" + $("#slFilterPODCountry").val();
    if ($("#slFilterPOD").val() != null && $("#slFilterPOD").val() != "" && pWhereClause !== "")
        pWhereClause += " AND POD =" + $("#slFilterPOD").val();
    if ($("#slFilterIsVesselArrived").val() != null && $("#slFilterIsVesselArrived").val() != "" && pWhereClause != "")
        pWhereClause += " AND IsClearance=" + $("#slFilterIsVesselArrived").val() + " \n";
    if ($("#slFilterIsForm13Delivered").val() != null && $("#slFilterIsForm13Delivered").val() != "" && pWhereClause != "")
        pWhereClause += " AND IsDelivered=" + $("#slFilterIsForm13Delivered").val() + " \n";

    if ($("#txtFilterCustomerReference").val().trim() != "") {
        if (pDefaults.UnEditableCompanyName == "OAO" || pDefaults.UnEditableCompanyName == "ELI")
            pWhereClause += " AND CustomerReference LIKE N'%" + $("#txtFilterCustomerReference").val().trim().toUpperCase() + "%' \n";
        else
            pWhereClause += " AND CustomerReference =N'" + $("#txtFilterCustomerReference").val().trim().toUpperCase() + "' \n";
    }

    if ($("#txtFilterMasterBL").val().trim() != "" && pWhereClause !== "") {
        //if (pDefaults.UnEditableCompanyName == "ALF" || pDefaults.UnEditableCompanyName == "NIS" || pDefaults.UnEditableCompanyName == "DGL" || pDefaults.UnEditableCompanyName == "ELI")
        pWhereClause += " AND MasterBL LIKE N'%" + $("#txtFilterMasterBL").val().trim().toUpperCase() + "%' ";
        //else
        //    pWhereClause += " AND MasterBL = N'" + $("#txtFilterMasterBL").val().trim().toUpperCase() + "' ";
    }

    if ($("#txtFilterHouseBLs").val().trim() != "" && pWhereClause !== "") {
        if (pDefaults.UnEditableCompanyName == "ALF" || pDefaults.UnEditableCompanyName == "NIS" || pDefaults.UnEditableCompanyName == "DGL" || pDefaults.UnEditableCompanyName == "ELI" || pDefaults.UnEditableCompanyName == "DYN" || pDefaults.UnEditableCompanyName == "TEU" || pDefaults.UnEditableCompanyName == "SWI") {
            pWhereClause += " AND (" + " \n";
            pWhereClause += "       HouseNumber LIKE N'%" + $("#txtFilterHouseBLs").val().trim().toUpperCase() + "%' ";
            pWhereClause += "       OR ((ISNULL((SELECT COUNT(op.ID) FROM dbo.Operations AS op WHERE dbo.vwOperations.ID = op.MasterOperationID AND op.HouseNumber = N'%" + $("#txtFilterHouseBLs").val().trim().toUpperCase() + "%'), 0)) > 0)";
            pWhereClause += "     ) \n ";
        }
        else {
            pWhereClause += " AND (" + " \n";
            pWhereClause += "       HouseNumber=N'" + $("#txtFilterHouseBLs").val().trim().toUpperCase() + "' ";
            pWhereClause += "       OR ((ISNULL((SELECT COUNT(op.ID) FROM dbo.Operations AS op WHERE dbo.vwOperations.ID = op.MasterOperationID AND op.HouseNumber = N'" + $("#txtFilterHouseBLs").val().trim().toUpperCase() + "'), 0)) > 0)";
            pWhereClause += "     ) \n ";
        }
        //pWhereClause += " AND (HouseBLs like '%" + $("#txtFilterHouseBLs").val().trim().toUpperCase() + "%' OR HouseNumber LIKE N'%" + $("#txtFilterHouseBLs").val().trim().toUpperCase() + "%') ";
        ////pWhereClause += " AND HouseNumber=N'" + $("#txtFilterHouseBLs").val().trim().toUpperCase() + "' ";
    }



    if ($("#txtFilterFlexi").val().trim() != "" && pWhereClause !== "") {
        //pWhereClause += " AND ((ISNULL((SELECT COUNT(f.ID) FROM dbo.vwFlexiSerial AS f WHERE dbo.vwOperations.ID IN(IsNull(f.ExportOperationID, 0), ISNULL(f.ImportOperationID, 0)) AND f.Code LIKE '%" + $("#txtFilterFlexi").val().trim().toUpperCase() + "%'), 0)) > 0)";
        pWhereClause += " AND ((ISNULL((SELECT COUNT(OCP.ID) FROM OperationContainersAndPackages AS OCP WHERE dbo.vwOperations.ID=OCP.OperationID AND OCP.TankOrFlexiNumber = N'" + $("#txtFilterFlexi").val().trim().toUpperCase() + "'), 0)) > 0)";
    }
    if (isValidDate($("#txtFilterLoadingDate").val().trim(), 1)) {
        if ($("#txtFilterLoadingDate").val() != null && $("#txtFilterLoadingDate").val() != "" && pWhereClause !== "")
            pWhereClause += " AND ((ISNULL((SELECT COUNT(o.ID) FROM dbo.Routings AS o WHERE dbo.vwOperations.ID = o.OperationID AND CONVERT(DATE , o.StuffingDate) = CONVERT(DATE ,'" + GetDateWithFormatyyyyMMdd($("#txtFilterLoadingDate").val()) + " 00:00:00.000')" + "), 0)) > 0)";
    }
    if ($("#txtFilterQuotation").val().trim() != "" && pWhereClause !== "") {
        pWhereClause += " AND (QuotationCode like N'%" + $("#txtFilterQuotation").val().trim().toUpperCase() + "%') ";
    }
    if ($("#txtFilterForm13Number").val().trim() != "" && pWhereClause !== "") {
        pWhereClause += " AND (Form13Number=N'" + $("#txtFilterForm13Number").val().trim().toUpperCase() + "') ";
    }
    if ($("#txtFilterACIDNumber").val().trim() != "" && pWhereClause !== "") {
        pWhereClause += " AND (ACIDNumber=N'" + $("#txtFilterACIDNumber").val().trim().toUpperCase() + "') ";
    }


    /*****************Side Controls***************************/

    pWhereClause = (pWhereClause == "" ? " WHERE (1=1) " : pWhereClause);
    return pWhereClause;
}
function ApplySelectListSearch() {
    debugger;
    //if (pDefaults.UnEditableCompanyName == "GBL") {
    $("#slFilterShipper").css({ "width": "100%" }).select2();
    $("#slFilterShipper").trigger("change");

    $("#slFilterConsignee").css({ "width": "100%" }).select2();
    $("#slFilterConsignee").trigger("change");

    $("#slFilterAgent").css({ "width": "100%" }).select2();
    $("#slFilterAgent").trigger("change");

    $("#slShippers").css({ "width": "100%" }).select2();
    $("#slShippers").trigger("change");

    $("#slConsignees").css({ "width": "100%" }).select2();
    $("#slConsignees").trigger("change");

    $("#slAgents").css({ "width": "100%" }).select2();
    $("#slAgents").trigger("change");

    $("#slOperationPartnerTypes").css({ "width": "100%" }).select2();
    $("#slOperationPartnerTypes").trigger("change");

    $("#slPartners").css({ "width": "100%" }).select2();
    $("#slPartners").trigger("change");
    $("#slPartners_Customers").css({ "width": "100%" }).select2();
    $("#slPartners_Customers").trigger("change");

    $("#slPOLCountries").css({ "width": "100%" }).select2();
    $("#slPOLCountries").trigger("change");
    $("#slPODCountries").css({ "width": "100%" }).select2();
    $("#slPODCountries").trigger("change");

    $("#slPOL").css({ "width": "100%" }).select2();
    $("#slPOL").trigger("change");
    $("#slPOD").css({ "width": "100%" }).select2();
    $("#slPOD").trigger("change");

    $("#slRoutingsPOLCountries").css({ "width": "100%" }).select2();
    $("#slRoutingsPOLCountries").trigger("change");
    $("#slRoutingsPODCountries").css({ "width": "100%" }).select2();
    $("#slRoutingsPODCountries").trigger("change");

    $("#slRoutingsPOL").css({ "width": "100%" }).select2();
    $("#slRoutingsPOL").trigger("change");
    $("#slRoutingsPOD").css({ "width": "100%" }).select2();
    $("#slRoutingsPOD").trigger("change");

    $("#slRoutingsLines").css({ "width": "100%" }).select2();
    $("#slRoutingsLines").trigger("change");

    $("#slQuotationRoutes").css({ "width": "100%" }).select2();
    $("#slQuotationRoutes").trigger("change");

    $("#slFilterPOLCountry").css({ "width": "100%" }).select2();
    $("#slFilterPOLCountry").trigger("change");
    $("#slFilterPODCountry").css({ "width": "100%" }).select2();
    $("#slFilterPODCountry").trigger("change");

    $("#slFilterPOL").css({ "width": "100%" }).select2();
    $("#slFilterPOL").trigger("change");
    $("#slFilterPOD").css({ "width": "100%" }).select2();
    $("#slFilterPOD").trigger("change");

    $("#slOperationToSetHBLCertificate").css({ "width": "100%" }).select2();
    $("#slOperationToSetHBLCertificate").trigger("change");

    $("#slPayableBill").css({ "width": "100%" }).select2();
    $("#slPayableBill").trigger("change");

    //$("#slFilterCCA").css({ "width": "100%" }).select2();
    //$("#slFilterCCA").trigger("change");

    //$("#slFilterBookingParty").css({ "width": "100%" }).select2();
    //$("#slFilterBookingParty").trigger("change");

    //}
    $("div[tabindex='-1']").removeAttr('tabindex');
}
function HousesOrders_BindTableRows(pOperations) {
    ClearAllTableRows("tblOperations");
    debugger;
    let copyControlsText = " class='btn btn-xs btn-rounded btn-info float-right' > <i class='fa fa-copy' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + "Copy" + "</span>";
    let printControlsText = " class='btn btn-xs btn-rounded btn-lightblue float-right' > <i class='fa fa-print' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + "Print" + "</span>";
    let transferControlsText = " class='btn btn-xs btn-rounded btn-info float-right " + (OEDoc ? "" : " hide ") + "' > <i class='fa fa-exchange' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + "Transfer" + "</span>";
    $.each(pOperations, function (i, item) {
        if (item.HouseParentID == 0) {

            AppendRowtoTable("tblOperations",

                ("<tr ID='" + item.ID + "' ondblclick='Shipments_FillControlsFromBLDocuments(" + item.ID + "," + item.MasterOperationID + ");' class='"
                    + (item.OperationStageID == CancelledQuoteAndOperStageID
                        ? "static-text-danger" //cancelled operation
                        : (item.OperationStageName == "CLOSED" //(Date.prototype.compareDates(FormattedTodaysDate, GetDateWithFormatMDY(item.CloseDate)) <= 0
                            ? "static-text-primary" //closed operation
                            : "")
                    )
                    + "'>"//of tr
                    + "<td class='ID2'> <input name='Delete' type='checkbox' value='" + item.ID + "' /></td>"
                    + (IsMESCOCompany ? "<td><i class='fa fa-plus' onclick='ShowHide(" + item.ID + ");'></i></td>" : "")
                    //+ "<td class='ID '> <input type='checkbox' value='" + item.ID + "' " + (item.MasterOperationID == 0 && item.NumberOfHousesConnected == 0 ? "name='Delete'" : " disabled ") + " /></td>"
                    + "<td class='QuotationID hide' val='" + item.QuotationID + "'>" + item.QuotationID + "</td>"
                    + "<td class='MasterOperationID hide' val='" + item.MasterOperationID + "'>" + item.MasterOperationID + "</td>"
                    + "<td class='shownBLTypeIconName hide'><i class= 'fa " + item.BLTypeIconName + " " + item.BLTypeIconStyle + " fa-2x'/></td>"
                    + "<td class='DirectionType hide'>" + item.DirectionType + "</td>"
                    + "<td class='shownDirectionIconName'><i class= 'fa " + item.DirectionIconName + " " + item.DirectionIconStyle + " fa-2x'/><i class= 'fa " + item.TransportIconName + " " + item.TransportIconStyle + " fa-2x'/></td>"
                    + "<td class='TransportType hide'>" + item.TransportType + "</td>"
                    + "<td class='Code'>" + (item.Code == 0 ? item.MasterOperationCode : item.Code) + "</td>"
                    + "<td class='MasterBL'>" + (item.MasterBL == 0 ? "" : item.MasterBL) + "</td>"
                    + "<td class='HouseNumber'>" + (item.HouseNumber == 0 ? "" : item.HouseNumber) + "</td>"
                    + "<td class='OpenedBy " + (1 == 1 ? " hide " : "  ") + "' val='" + item.CreatorUserID + "'>" + item.OpenedBy + "</td>"
                    + "<td class='shownOpenDate hide'>"
                    + GetShownDate(FormattedTodaysDate, GetDateWithFormatMDY(item.OpenDate))
                    + "</td>"
                    + "<td class='Reference hide'>" + (pDefaults.UnEditableCompanyName == "KDM" ? (item.ReleaseNumber == 0 ? "" : item.ReleaseNumber) : (item.Reference == 0 ? "" : item.Reference)) + "</td>"
                    + "<td class='Client'>" + (item.BookingPartyName != 0 && item.BLType == constMasterBLType ? item.BookingPartyName : (item.ClientName == 0 ? "" : item.ClientName)) + "</td>"
                    + "<td class='Shipper hide' val='" + item.ShipperID + "'>" + item.ShipperName + "</td>"
                    + "<td class='ShipperAddress hide' val='" + item.ShipperAddressID + "'>" + item.ShipperAddressID + "</td>"
                    + "<td class='ShipperContact hide' val='" + item.ShipperContactID + "'>" + item.ShipperContactID + "</td>"
                    + "<td class='Consignee hide' val='" + item.ConsigneeID + "'>" + item.ConsigneeName + "</td>"
                    + "<td class='ConsigneeAddress hide' val='" + item.ConsigneeAddressID + "'>" + item.ConsigneeAddressID + "</td>"
                    + "<td class='ConsigneeContact hide' val='" + item.ConsigneeContactID + "'>" + item.ConsigneeContactID + "</td>"
                    + "<td class='Agent hide' val='" + item.AgentID + "'>" + item.AgentName + "</td>"
                    + "<td class='AgentAddress hide' val='" + item.AgentAddressID + "'>" + item.AgentAddressID + "</td>"
                    + "<td class='AgentContact hide' val='" + item.AgentContactID + "'>" + item.AgentContactID + "</td>"

                    + "<td class='Carrier hide' val='" + (item.TransportType == "1" ? item.ShippingLineID //Ocean
                        : (item.TransportType == "2" ? item.AirlineID //Air
                            : item.TruckerID) //Inland
                    ) //EOF getting the carrier ID val
                    + "'>" + (item.TransportType == "1" ? (item.ShippingLineName == 0 ? "" : item.ShippingLineName) //Ocean
                        : (item.TransportType == "2" ? (item.AirlineName == 0 ? "" : item.AirlineName)//Air
                            : (item.TruckerName == 0 ? "" : item.TruckerName)) //Inland
                    )
                    + "</td>"
                    + "<td class='Routing'>" + (item.POLName + " > " + item.PODName) + "</td>"
                    + "<td class='POL hide' val='" + item.POL + "'>" + item.POLCode + "</td>"
                    + "<td class='POD hide' val='" + item.POD + "'>" + item.PODCode + "</td>"
                    + "<td class='Incoterm hide' val='" + item.IncotermID + "'>" + item.IncotermName + "</td>"
                    + "<td class='OperationPOrC hide' val='" + item.POrC + "'>" + item.POrC + "</td>"
                    + "<td class='Commodity hide' val='" + item.CommodityID + "'>" + item.CommoditymName + "</td>"
                    + "<td class='TransientTime hide'>" + item.TransientTime + "</td>"
                    + "<td class='CloseDate hide'>" + ConvertDateFormat(GetDateWithFormatMDY(item.CloseDate)) + "</td>"
                    //ShipmentType : 1-FCL 2-LCL 3-FTL 4-LTL
                    + "<td class='ShipmentType' val='" + item.ShipmentType+"'>" + GetShipmentType(item.ShipmentType) + " " + item.RepBLTypeShown + "</td>"
                    + "<td class='shownCutOffDate hide'>" + "<span class='pull-left thumb-sm  calendar-icon-style'>"
                    + " <i class='fa fa-calendar'></i>"
                    //+ " <small class='text-muted'>" + ConvertDateFormat(GetDateWithFormatMDY(item.CutOffDate)) + "</small>"
                    + " <small>" + GetShownDate(FormattedTodaysDate, GetDateWithFormatMDY(item.CutOffDate)) + "</small>"
                    + "</span>"
                    + "</td>"
                    + "<td class='CutOffDate hide'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.CutOffDate)) < 1 ? "" : ConvertDateFormat(GetDateWithFormatMDY(item.CutOffDate))) + "</td>"
                    + "<td class='Volume hide'>" + item.Volume + "</td>"
                    + "<td class='IncludePickup hide'> <input type='checkbox' disabled='disabled' val='" + (item.IncludePickup == true ? "true' checked='checked'" : "'") + " /></td>"
                    + "<td class='PickupAddress hide' val='" + item.PickupAddressID + "'>" + item.PickupAddressID + "</td>"
                    + "<td class='IncludeDelivery hide'> <input type='checkbox' disabled='disabled' val='" + (item.IncludeDelivery == true ? "true' checked='checked'" : "'") + " /></td>"
                    + "<td class='DeliveryAddress hide' val='" + item.DeliveryAddressID + "'>" + item.DeliveryAddressID + "</td>"

                    + "<td class='GrossWeight'>" + item.GrossWeight + "</td>"
                    + "<td class='Volume hide'>" + item.Volume + "</td>"
                    + "<td class='ChargeableWeight hide'>" + item.ChargeableWeight + "</td>" //shown as Wt/Msr(MT) incase of ocean or inland
                    + "<td class='IsDangerousGoods hide'> <input type='checkbox' disabled='disabled' val='" + (item.IsDangerousGoods == true ? "true' checked='checked'" : "'") + " /></td>"
                    + "<td class='Notes hide'>" + (item.Notes == 0 ? "" : item.Notes) + "</td>"
                    + "<td class='BookingNumbers hide'>" + item.BookingNumbers + "</td>"
                    + "<td class='NumberOfPackages hide'>" + (item.ContainerTypes == 0 ? (item.PackageTypes == 0 ? "" : item.PackageTypes) : item.ContainerTypes) + "</td>"
                    + "<td class='MoveType " + (1 == 1 ? "hide" : "") + "'>" + (item.MoveTypeCode == 0 ? "" : item.MoveTypeCode) + "</td>"
                    + "<td class='OperationStage hide' val='" + item.OperationStageID + "'>"
                    + (item.OperationStageID == CancelledQuoteAndOperStageID
                        ? item.OperationStageName //cancelled operation
                        : (Date.prototype.compareDates(FormattedTodaysDate, GetDateWithFormatMDY(item.CloseDate)) <= 0
                            ? "CLOSED" //closed operation
                            : item.OperationStageName)
                    ) + "</td>"
                    + "<td class='Salesman hide' val='" + item.SalesmanID + "'>" + item.Salesman + "</td>"
                    + "<td class='OperationMan hide' val='" + item.OperationManID + "'>" + item.OperationMan + "</td>"
                    + "<td class='AgreedRate hide'>" + (item.AgreedRate == "0" ? "" : item.AgreedRate) + "</td>"
                    + "<td class='CustomerReference hide'>" + (item.CustomerReference == "0" ? "" : item.CustomerReference) + "</td>"
                    + "<td class='SupplierReference hide'>" + (item.SupplierReference == "0" ? "" : item.SupplierReference) + "</td>"
                    + "<td class='PONumber hide'>" + (item.PONumber == "0" ? "" : item.PONumber) + "</td>"
                    + "<td class='CertificateNumber hide'>" + (item.CertificateNumber == "0" ? "" : item.CertificateNumber) + "</td>"
                    + "<td class='IsAWB hide'> <input type='checkbox' id='cbIsAWB" + item.ID + "' disabled='disabled' " + (item.IsAWB ? " checked='checked' " : "") + " /></td>"
                    + "<td class='QuotationRouteID hide' val='" + item.QuotationRouteID + "'>" + (item.QuotationRouteID == 0 ? "" : item.QuotationCode.substr(8, 9)) + "</td>"
                    + "<td class='AWB " + (pDefaults.UnEditableCompanyName == "VEN" ? "" : "hide") + "'>" + (item.MAWBSuffix == "0" ? "" : item.AirlinePrefix + ">" + item.MAWBSuffix) + "</td>"
                    //+ "<td class='hide'><a onclick='SwitchToOperationsEditView(" + item.ID + ");' " + editControlsText + "</a></td>"
                    + "<td class='InvoiceNumbers'>" + (item.InvoiceNumbers == 0 ? "" : item.InvoiceNumbers) + "</td>"
                    + "<td class='ContainerTypes20 hide'>" + (item.ContainerTypes20 == "0" ? "" : item.ContainerTypes20) + "</td>"
                    + "<td class='ContainerTypes40 hide'>" + (item.ContainerTypes40 == "0" ? "" : item.ContainerTypes40) + "</td>"
                    + "<td class=''>"
                    + (glbCallingControl != "TransferHouse" ? ("<a href='#' data-toggle='modal' onclick='Operations_FillPrintDocsOutModal(" + item.ID + ");' " + printControlsText + "</a>") : "")
                    + (glbCallingControl == "TransferHouse" ? ("<a href='#' data-toggle='modal' onclick='Operations_FillTransferHouseModal(" + item.ID + ");' " + transferControlsText + "</a>") : "")
                    //+ "<a href='#CopyOperationModal' data-toggle='modal' onclick='Operations_FillCopyOperationModal(" + item.ID + ");' " + copyControlsText + "</a>"
                    + "</td>"
                    + "</tr>"));


            AppendRowtoTable("tblOperations",
                '<tr id="DaughterOperations' + item.ID + '" style="display: none;">' +
                '<td colspan="12" style="padding-left: 110px;padding-right: 80px;" ><table  id="tblDaughterOperations' + item.ID + '" class="table text-sm table-hover">' +
                '<thead>' +
                '<tr style="background-color:#e9e9cc">' +
                '<th><input id="cb-CheckAll" type="checkbox" /></th>' +
                '<th></th>' +
                '<th>Code</th>' +
                '<th>MasterBL</th>' +
                '<th>HouseNo</th>' +
                '<th>Client</th>' +
                '<th>Route</th>' +
                '<th>Shipment</th>' +
                '<th>GrossWt</th>' +
                '<th>Invoices</th>' +
                '<th class="rounded-right"></th>' +
                '</tr>' +
                '</thead>' +
                '<tbody ></tbody></table></td>' +
                '</tr>'
            );
        }
    });

    $.each(pOperations, function (i, house) {
        debugger;
        if (house.HouseParentID == 0) {
            $.each(pOperations, function (y, item) {
                if (house.ID == item.HouseParentID) {
                    AppendRowtoNestedTable("tblDaughterOperations" + house.ID,
                        ("<tr style='height:15px;background-color:#e9e9cc' ID='" + item.ID + "' ondblclick='Shipments_FillControlsFromBLDocuments(" + item.ID + "," + item.MasterOperationID + ");' class='"
                            + (item.OperationStageID == CancelledQuoteAndOperStageID
                                ? "static-text-danger" //cancelled operation
                                : (item.OperationStageName == "CLOSED" //(Date.prototype.compareDates(FormattedTodaysDate, GetDateWithFormatMDY(item.CloseDate)) <= 0
                                    ? "static-text-primary" //closed operation
                                    : "")
                            )
                            + "'>"//of tr
                            + "<td class='ID2'> <input name='Delete' type='checkbox' value='" + item.ID + "' /></td>"
                            //+ "<td class='ID '> <input type='checkbox' value='" + item.ID + "' " + (item.MasterOperationID == 0 && item.NumberOfHousesConnected == 0 ? "name='Delete'" : " disabled ") + " /></td>"
                            + "<td class='QuotationID hide' val='" + item.QuotationID + "'>" + item.QuotationID + "</td>"
                            ////BLType : 1-Direct 2-House 3-Master
                            //+ "<td class='BLType hide'>" + item.RepBLTypeShown + "</td>"
                            + "<td class='shownBLTypeIconName hide'><i class= 'fa " + item.BLTypeIconName + " " + item.BLTypeIconStyle + " fa-2x'/></td>"
                            //+ "<td class='BLTypeIconName hide'>" + item.BLTypeIconName + "</td>"//this line is to get the value of the IconName to fill it in the modal 
                            //+ "<td class='BLTypeIconStyle hide'>" + item.BLTypeIconStyle + "</td>"//this line is to get the value of the IconStyle to fill it in the modal 
                            ////DirectionType : 1-Import 2-Export 3-Domestic
                            //+ "<td class='DirectionType hide'>" + item.RepDirectionTypeShown + "</td>"
                            + "<td class='DirectionType hide'>" + item.DirectionType + "</td>"
                            //+ "<td class='shownDirectionIconName'><i class= 'fa " + item.DirectionIconName + " " + item.DirectionIconStyle + " fa-2x'/></td>"
                            + "<td class='shownDirectionIconName'><i class= 'fa " + item.DirectionIconName + " " + item.DirectionIconStyle + " fa-2x'/><i class= 'fa " + item.TransportIconName + " " + item.TransportIconStyle + " fa-2x'/></td>"
                            //+ "<td class='DirectionIconName hide'>" + item.DirectionIconName + "</td>"//this line is to get the value of the IconName to fill it in the modal 
                            //+ "<td class='DirectionIconStyle hide'>" + item.DirectionIconStyle + "</td>"//this line is to get the value of the IconStyle to fill it in the modal 
                            ////TransportType : 1-Ocean 2-Air 3-Inland
                            //+ "<td class='TransportType hide'> <input type='text' id='txtTransportType" + item.ID + "' disabled='disabled' value=" + item.TransportType + " /></td>"
                            //+ "<td class='TransportType hide'>" + item.RepTransportTypeShown + "</td>"
                            + "<td class='TransportType hide'>" + item.TransportType + "</td>"
                            //+ "<td class='shownTransportIconName' ><i class= 'fa " + item.TransportIconName + " " + item.TransportIconStyle + " fa-2x'/></td>"
                            //+ "<td class='TransportIconName hide'>" + item.TransportIconName + "</td>"//this line is to get the value of the IconName to fill it in the modal 
                            //+ "<td class='TransportIconStyle hide'>" + item.TransportIconStyle + "</td>"//this line is to get the value of the IconStyle to fill it in the modal 

                            + "<td class='Code'>" + (item.Code == 0 ? item.MasterOperationCode : item.Code) + "</td>"
                            + "<td class='MasterBL'>" + (item.MasterBL == 0 ? "" : item.MasterBL) + "</td>"
                            + "<td class='HouseNumber'>" + (item.HouseNumber == 0 ? "" : item.HouseNumber) + "</td>"
                            + "<td class='OpenedBy " + (1 == 1/*/pDefaults.UnEditableCompanyName == "FRE"*/ ? " hide " : "  ") + "' val='" + item.CreatorUserID + "'>" + item.OpenedBy + "</td>"
                            //the next line differs from the preceeding one that date could be shown as today, tomorrow, yesterday
                            + "<td class='shownOpenDate hide'>" //+ "<span class='pull-left thumb-sm  calendar-icon-style'>"
                            //+ " <i class='fa fa-calendar'></i>"
                            //+ " <small class='text-muted'>" + ConvertDateFormat(GetDateWithFormatMDY(item.CreationDate)) + "</small>"
                            //+ " <small>" + GetShownDate(FormattedTodaysDate, GetDateWithFormatMDY(item.OpenDate)) + "</small>"
                            + GetShownDate(FormattedTodaysDate, GetDateWithFormatMDY(item.OpenDate))
                            //+ "</span>"
                            + "</td>"
                            //+ "<td class='OpenDate hide'>" + ConvertDateFormat(GetDateWithFormatMDY(item.OpenDate)) + "</td>"
                            //+ "<td class='Branch' val='" + item.BranchID + "'>" + (item.BranchID == 0 ? "" : item.BranchName) + "</td>"
                            + "<td class='Reference hide'>" + (pDefaults.UnEditableCompanyName == "KDM" ? (item.ReleaseNumber == 0 ? "" : item.ReleaseNumber) : (item.Reference == 0 ? "" : item.Reference)) + "</td>"
                            //+ "<td class='CodeSerial hide'>" + item.CodeSerial + "</td>"
                            //+ "<td class='Client'>" + (item.BLType == constMasterBLType
                            //                            ? (item.AgentName == 0 ? "" : item.AgentName)
                            //                            : (item.DirectionType == constImportDirectionType ? (item.ConsigneeName == 0 ? "" : item.ConsigneeName) : (item.ShipperName == 0 ? "" : item.ShipperName))) + "</td>"
                            //+ "<td class='Client'>" + (item.ClientName == 0 ? "" : item.ClientName) + "</td>"
                            + "<td class='Client'>" + (item.BookingPartyName != 0 && item.BLType == constMasterBLType ? item.BookingPartyName : (item.ClientName == 0 ? "" : item.ClientName)) + "</td>"
                            + "<td class='Shipper hide' val='" + item.ShipperID + "'>" + item.ShipperName + "</td>"
                            + "<td class='ShipperAddress hide' val='" + item.ShipperAddressID + "'>" + item.ShipperAddressID + "</td>"
                            + "<td class='ShipperContact hide' val='" + item.ShipperContactID + "'>" + item.ShipperContactID + "</td>"
                            + "<td class='Consignee hide' val='" + item.ConsigneeID + "'>" + item.ConsigneeName + "</td>"
                            + "<td class='ConsigneeAddress hide' val='" + item.ConsigneeAddressID + "'>" + item.ConsigneeAddressID + "</td>"
                            + "<td class='ConsigneeContact hide' val='" + item.ConsigneeContactID + "'>" + item.ConsigneeContactID + "</td>"
                            + "<td class='Agent hide' val='" + item.AgentID + "'>" + item.AgentName + "</td>"
                            + "<td class='AgentAddress hide' val='" + item.AgentAddressID + "'>" + item.AgentAddressID + "</td>"
                            + "<td class='AgentContact hide' val='" + item.AgentContactID + "'>" + item.AgentContactID + "</td>"

                            + "<td class='Carrier hide' val='" + (item.TransportType == "1" ? item.ShippingLineID //Ocean
                                : (item.TransportType == "2" ? item.AirlineID //Air
                                    : item.TruckerID) //Inland
                            ) //EOF getting the carrier ID val
                            + "'>" + (item.TransportType == "1" ? (item.ShippingLineName == 0 ? "" : item.ShippingLineName) //Ocean
                                : (item.TransportType == "2" ? (item.AirlineName == 0 ? "" : item.AirlineName)//Air
                                    : (item.TruckerName == 0 ? "" : item.TruckerName)) //Inland
                            )
                            + "</td>"
                            + "<td class='Routing'>" + (item.POLName + " > " + item.PODName) + "</td>"
                            + "<td class='POL hide' val='" + item.POL + "'>" + item.POLCode + "</td>"
                            + "<td class='POD hide' val='" + item.POD + "'>" + item.PODCode + "</td>"
                            + "<td class='Incoterm hide' val='" + item.IncotermID + "'>" + item.IncotermName + "</td>"
                            + "<td class='OperationPOrC hide' val='" + item.POrC + "'>" + item.POrC + "</td>"
                            + "<td class='Commodity hide' val='" + item.CommodityID + "'>" + item.CommoditymName + "</td>"
                            + "<td class='TransientTime hide'>" + item.TransientTime + "</td>"
                            + "<td class='CloseDate hide'>" + ConvertDateFormat(GetDateWithFormatMDY(item.CloseDate)) + "</td>"
                            //ShipmentType : 1-FCL 2-LCL 3-FTL 4-LTL
                            //+ "<td class='ShipmentType'>" + GetShipmentType(item.ShipmentType) + " " + GetBLType(item.BLType) + "</td>"
                            + "<td class='ShipmentType' val='" + item.ShipmentType +"'>" + GetShipmentType(item.ShipmentType) + " " + item.RepBLTypeShown + "</td>"
                            + "<td class='shownCutOffDate hide'>" + "<span class='pull-left thumb-sm  calendar-icon-style'>"
                            + " <i class='fa fa-calendar'></i>"
                            //+ " <small class='text-muted'>" + ConvertDateFormat(GetDateWithFormatMDY(item.CutOffDate)) + "</small>"
                            + " <small>" + GetShownDate(FormattedTodaysDate, GetDateWithFormatMDY(item.CutOffDate)) + "</small>"
                            + "</span>"
                            + "</td>"
                            + "<td class='CutOffDate hide'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.CutOffDate)) < 1 ? "" : ConvertDateFormat(GetDateWithFormatMDY(item.CutOffDate))) + "</td>"
                            + "<td class='Volume hide'>" + item.Volume + "</td>"
                            + "<td class='IncludePickup hide'> <input type='checkbox' disabled='disabled' val='" + (item.IncludePickup == true ? "true' checked='checked'" : "'") + " /></td>"
                            + "<td class='PickupAddress hide' val='" + item.PickupAddressID + "'>" + item.PickupAddressID + "</td>"
                            + "<td class='IncludeDelivery hide'> <input type='checkbox' disabled='disabled' val='" + (item.IncludeDelivery == true ? "true' checked='checked'" : "'") + " /></td>"
                            + "<td class='DeliveryAddress hide' val='" + item.DeliveryAddressID + "'>" + item.DeliveryAddressID + "</td>"

                            + "<td class='GrossWeight'>" + item.GrossWeight + "</td>"
                            + "<td class='Volume hide'>" + item.Volume + "</td>"
                            + "<td class='ChargeableWeight hide'>" + item.ChargeableWeight + "</td>" //shown as Wt/Msr(MT) incase of ocean or inland
                            + "<td class='IsDangerousGoods hide'> <input type='checkbox' disabled='disabled' val='" + (item.IsDangerousGoods == true ? "true' checked='checked'" : "'") + " /></td>"
                            + "<td class='Notes hide'>" + (item.Notes == 0 ? "" : item.Notes) + "</td>"
                            + "<td class='BookingNumbers hide'>" + item.BookingNumbers + "</td>"
                            + "<td class='NumberOfPackages hide'>" + (item.ContainerTypes == 0 ? (item.PackageTypes == 0 ? "" : item.PackageTypes) : item.ContainerTypes) + "</td>"
                            + "<td class='MoveType " + (1 == 1 ? "hide" : "") + "'>" + (item.MoveTypeCode == 0 ? "" : item.MoveTypeCode) + "</td>"
                            + "<td class='OperationStage hide' val='" + item.OperationStageID + "'>"
                            + (item.OperationStageID == CancelledQuoteAndOperStageID
                                ? item.OperationStageName //cancelled operation
                                : (Date.prototype.compareDates(FormattedTodaysDate, GetDateWithFormatMDY(item.CloseDate)) <= 0
                                    ? "CLOSED" //closed operation
                                    : item.OperationStageName)
                            ) + "</td>"
                            + "<td class='Salesman hide' val='" + item.SalesmanID + "'>" + item.Salesman + "</td>"
                            + "<td class='OperationMan hide' val='" + item.OperationManID + "'>" + item.OperationMan + "</td>"
                            + "<td class='AgreedRate hide'>" + (item.AgreedRate == "0" ? "" : item.AgreedRate) + "</td>"
                            + "<td class='CustomerReference hide'>" + (item.CustomerReference == "0" ? "" : item.CustomerReference) + "</td>"
                            + "<td class='SupplierReference hide'>" + (item.SupplierReference == "0" ? "" : item.SupplierReference) + "</td>"
                            + "<td class='PONumber hide'>" + (item.PONumber == "0" ? "" : item.PONumber) + "</td>"
                            + "<td class='CertificateNumber hide'>" + (item.CertificateNumber == "0" ? "" : item.CertificateNumber) + "</td>"
                            + "<td class='IsAWB hide'> <input type='checkbox' id='cbIsAWB" + item.ID + "' disabled='disabled' " + (item.IsAWB ? " checked='checked' " : "") + " /></td>"
                            + "<td class='QuotationRouteID hide' val='" + item.QuotationRouteID + "'>" + (item.QuotationRouteID == 0 ? "" : item.QuotationCode.substr(8, 9)) + "</td>"
                            + "<td class='AWB " + (pDefaults.UnEditableCompanyName == "VEN" ? "" : "hide") + "'>" + (item.MAWBSuffix == "0" ? "" : item.AirlinePrefix + ">" + item.MAWBSuffix) + "</td>"
                            //+ "<td class='hide'><a onclick='SwitchToOperationsEditView(" + item.ID + ");' " + editControlsText + "</a></td>"
                            + "<td class='InvoiceNumbers'>" + (item.InvoiceNumbers == 0 ? "" : item.InvoiceNumbers) + "</td>"
                            + "<td class='ContainerTypes20 hide'>" + (item.ContainerTypes20 == "0" ? "" : item.ContainerTypes20) + "</td>"
                            + "<td class='ContainerTypes40 hide'>" + (item.ContainerTypes40 == "0" ? "" : item.ContainerTypes40) + "</td>"
                            + "<td class=''>"
                            + (glbCallingControl != "TransferHouse" ? ("<a href='#' data-toggle='modal' onclick='Operations_FillPrintDocsOutModal(" + item.ID + ");' " + printControlsText + "</a>") : "")
                            + (glbCallingControl == "TransferHouse" ? ("<a href='#' data-toggle='modal' onclick='Operations_FillTransferHouseModal(" + item.ID + ");' " + transferControlsText + "</a>") : "")
                            //+ "<a href='#CopyOperationModal' data-toggle='modal' onclick='Operations_FillCopyOperationModal(" + item.ID + ");' " + copyControlsText + "</a>"
                            + "</td>"
                            + "</tr>")
                    );
                    // $("#DaughterHouse" + house.ID).show();
                }

            });
        }
    });

    ////ApplyPermissions();
    //if (OA) { $(".OperationCopy").removeClass("hide"); }
    //else { $(".OperationCopy").addClass("hide"); };
    if (OD) $("#btn-Delete").removeClass("hide"); else $("#btn-Delete").addClass("hide");

    BindAllCheckboxonTable("tblOperations", "ID2");//it attaches function to the checkboxes so when all are checked then check cb-checkall too
    CheckAllCheckbox("ID2");
    //HighlightText("#tblOperations>tbody>tr", $("#txt-Search").val().trim());
    if (showDeleteFailMessage) { //sherif: if deletion failed due to foreign key, then this message is shown
        swal(strSorry, strDeleteFailMessage, "warning");
        showDeleteFailMessage = false;
        strDeleteFailMessage = "This command is not completed because of dependencies existance."
    }
    $("#hl-homepage").on("click", function () { LoadViews("hl-homepage"); });
    FadePageCover(false); //to quickly fade before filters are filled(user psycology)
    //i put FillListWithNames in the LoadView so the value remains unchanged
    ////parameters (pStrFnName, pStrFirstRow, pListName)
    //FillListWithNames("/api/NoAccessQuoteAndOperStages/LoadAll", "ALL STAGES", "ulOperationStages");

    //GetListWithNameAndWhereClause(null, "/api/DocumentTypes/LoadAll", "<-- Select Document Type -->", "slDocsOutTypesOutsideModal", " WHERE 1=1 AND Name <> 'HBL' AND IsDocOut = 1 ORDER BY ViewOrder,TableOrViewName ");
    //DocsOut_LoadAll(0, "slDocsOutTypesOutsideModal");

    if (IsMESCOCompany) {
        $(".ShowForMescoCompaniesOnly").removeClass("hide");
    } else {
        $(".ShowForMescoCompaniesOnly").addClass("hide");
    }

}
function ShowHide(id) {
    debugger;
    $('#DaughterOperations' + id + '').toggle();
    if ($('#DaughterOperations' + id).css('display') == 'none') {
        $('#' + id).css('background-color', '');
        $('#DaughterOperations' + id).css('background-color', '');
    } else {
        $('#' + id).css('background-color', 'rgb(153, 153, 153)');
        $('#DaughterOperations' + id).css('background-color', '#e9e9cc');

    }

    // $('#tblbodyDaughterHouse  tr').css('background-color','#e9e9cc')
    $('#' + id).css('padding', '0px')
    $('#' + id).css('padding', '0px')

}
function Shipments_ClearAllControls(pHouseParentID) { //called when adding a new house shipment to a master to clear the ShipmentModal
    debugger;
    $(".classShowForUpdate").addClass("hide");
    //if (pDefaults.UnEditableCompanyName == "GLS" && !$("#cbIsAir").prop("checked") && $("#cbIsExport").prop("checked")) {
    $("#txtShipmentHouseNumber").attr("data-required", false);
    //}
    //else {
    //    $("#txtShipmentHouseNumber").attr("data-required", true);
    //}
    if ($("#hIsOperationDisabled").val() == false) {
        $("#divShipmentPackageBtns").removeClass("hide");
        //$("#divShipmentPackage").removeClass("hide");
    }
    else {
        $("#divShipmentPackageBtns").addClass("hide");
        //$("#divShipmentPackage").addClass("hide");
    }
    if ($("#cbIsConsolidation").prop("checked")) {
        $("#divShipmentContainers").removeClass("hide"); //in EditPackageModal 
    }
    else {
        $("#divShipmentContainers").addClass("hide"); //in EditPackageModal
    }
    FadePageCover(true);
    $("#tblShipmentPackage tbody").html("");
    ClearAll("#ShipmentModal");
    ClearAll("#ClearancePropertiesModal");
    jQuery("#ShipmentModal").modal("show");
    $("#slClearanceCurrency").val(pDefaults.CurrencyID);
    $("#lblShownShipment").html("");
    $("#txtOperationOpenDateForBLDocuments").val(getTodaysDateInddMMyyyyFormat());
    $("#txtOperationCloseDateForBLDocuments").val('10/10/2050');
    if (pDefaults.UnEditableCompanyName == "KDM")
        $("#txtShipmentHouseNumber").val($("#txtOperationReleaseNumber").val());
    else if (pDefaults.UnEditableCompanyName == "VER" || pDefaults.UnEditableCompanyName == "ELI") //mostaa
        $("#txtShipmentHouseNumber").val($("#hOperationCode").val());
    $("#txtShipmentHouseNumber").removeAttr("disabled");
    //DeliveryCity_GetList(null, $("#hPODCountryID").val());
    $("#slShipmentPOrC").html($("#slOperationPOrC").html());
    $("#slShipmentPOrC").val(3);

    var pParametersWithValues = { pMasterOperationID: "0", pShipmentID: 0 };

    CallGETFunctionWithParameters("/api/Operations/FillShipmentControls", pParametersWithValues
        , function (pData) {
            var pCustomers = pData[1];
            var pAgents = pData[2];
            var pPackageTypes = pData[4];
            var pFinalDestination = pData[5];
            var pContainers = pData[6];
            var pNotifyID = pData[7];
            var pPickupCityList = pData[12];
            var pCountryList = pData[13];
            var pVessels = pData[18];

            var pIncotermsList = pData[14];
            var pMoveTypesList = pData[15];
            var pNoAccessFreightTypesList = pData[16];
            var pUsers = pData[19];

            var _Salesmentemp = JSON.parse(pUsers);
            var pSalesmen = jQuery.grep(_Salesmentemp, function (_Salesmentemp) {
                return _Salesmentemp.IsSalesman == true;
            });
            FillListFromObject(null, 2, "<--Select-->", "slOperationSalesmanForBLDocuments", JSON.stringify(pSalesmen), function () {
                $("#slOperationSalesmanForBLDocuments").val($("#hLoggedUserID").val());
            });


            $("#slConsigneesForBLDocuments").html($("#hReadySlCustomers").html());
            $("#slConsignees2").html($("#hReadySlCustomers").html());
            $("#slShippersForBLDocuments").html($("#hReadySlCustomers").html());
            $("#slNotifyForBLDocuments").html($("#hReadySlCustomers").html());
            FillListFromObject(null, 2, "<--Select-->", "slAgentsForBLDocuments", pAgents, null);
            $("#slPackageTypes").val("");  //FillListFromObject(null, 2, "<--Select-->", "slPackageTypes", pPackageTypes, null);
            //$("#slShipmentPackageTypes").html($("#slPackageTypes").html());
            FillListFromObject(null, 4, "<--Select-->", "slDeliveryCity", pFinalDestination, null);
            FillListFromObject(null, 4, "<--Select-->", "slPickupCity", pPickupCityList, null);
            FillListFromObject($("#hPOLCountryID").val(), 2, "<--Select-->", "slPickupCountry", pCountryList, function () {
                $("#slDeliveryCountry").html($("#slPickupCountry").html());
                $("#slDeliveryCountry").val($("#hPODCountryID").val());
                if (glbCallingControl == "ShippingOrders") {
                    $("#slPickupCountry").val(1176).trigger('change'); // Egypt
                    $("#slPickupCountry").attr("disabled", "disabled");
                } else if (glbCallingControl == "RoutingOrders") {
                    $("#slDeliveryCountry").val(1176).trigger('change'); // Egypt
                    $("#slDeliveryCountry").attr("disabled", "disabled");
                }
            });
            FillListFromObject(null, 11, "<--Select-->", "slShipmentContainers", pContainers, null); //to be used when open PackagesModal
            FillListFromObject(null, 2, "<--Select-->", "slOperationVesselID", pVessels, null);
            FillListFromObject(null, 2, "<--Select-->", "slOperationOperationManID", pUsers, null);

            FillListFromObject(null, 2, "Select Incoterm", "slShipmentIncoterm", pIncotermsList, null);
            FillListFromObject(null, 2, "Select Service Scope", "slOperationMoveTypesForBLDocuments", pMoveTypesList, null);
            FillListFromObject(null, 2, "Select Freight Type", "slShipmentPOrC", pNoAccessFreightTypesList, null);

            FadePageCover(false);
        }
        , null);




    $("#slOperationBranchForBLDocuments").html($("#hReadySlBranches").html());

    //if ($("#hDirectionType").val() == constImportDirectionType) {
    //    $("#slConsigneesForBLDocuments").attr("data-required", true);
    //    $("#slShippersForBLDocuments").attr("data-required", false);
    //}
    //else { //Export or Domestic
    //    $("#slShippersForBLDocuments").attr("data-required", true);
    //    $("#slConsigneesForBLDocuments").attr("data-required", false);
    //}
    $("#slShippersForBLDocuments").attr("data-required", true);
    $("#slConsigneesForBLDocuments").attr("data-required", true);


    //parameter in the next 3 lines are 1:Quotations call, 2:Operations call
    $("#btn-NewAddShipperForBLDocuments").attr("onclick", "Customers_ClearAllControls(1);");
    $("#btn-NewAddConsigneeForBLDocuments").attr("onclick", "Customers_ClearAllControls(1);");
    $("#btn-NewAddNotify").attr("onclick", "Customers_ClearAllControls(1);");
    $("#btn-NewAddAgentForBLDocuments").attr("onclick", "Agents_ClearAllControls(1);");

    $("#btn-EditShipperForBLDocuments").attr("onclick", "Customers_FillControlsFromOperations($('#slShippersForBLDocuments option:selected').val(), null, 1);");
    $("#btn-EditConsigneeForBLDocuments").attr("onclick", "Customers_FillControlsFromOperations($('#slConsigneesForBLDocuments option:selected').val(), null, 1);");
    $("#btn-EditNotify").attr("onclick", "Customers_FillControlsFromOperations($('#slNotifyForBLDocuments option:selected').val(), null, 1);");
    $("#btn-EditAgentForBLDocuments").attr("onclick", "Agents_FillControlsFromOperations($('#slAgentsForBLDocuments option:selected').val(), null, 1);");

    //$("#btnSaveShipment").attr("onclick", "Operations_InsertFromBLDocuments(true,true, function(pData) { if (pData[3] != '') swal('Sorry', pData[3]); else { Shipments_ConnectOrDisconnect(pData[1], true, true); $('#hShipmentID').val(pData[1]); $('#txtShipmentHouseNumber').val(pData[2]); $('#btnSaveShipment').attr('onclick','Shipment_Update();'); swal('Success', 'Saved Successfully.'); } });");
    //$("#btnSaveClearanceProperties").attr("onclick", "Operations_InsertFromBLDocuments(true,true, function(pData) { if (pData[3] != '') swal('Sorry', pData[3]); else { Shipments_ConnectOrDisconnect(pData[1], true, true); $('#hShipmentID').val(pData[1]); $('#txtShipmentHouseNumber').val(pData[2]); $('#btnSaveClearanceProperties').attr('onclick','Shipment_Update();'); swal('Success', 'Saved Successfully.'); } });");
    $("#btnSaveShipment").attr("onclick", "Operations_InsertFromBLDocuments(true,true, function(pData) { if (pData[3] != '') swal('Sorry', pData[3]); else { $('#hShipmentID').val(pData[1]); $('#txtShipmentHouseNumber').val(pData[2]); Operations_LoadingWithPaging(); Shipments_ClearAllControls(); swal('Success', 'Saved Successfully.'); } });");


    if (pHouseParentID !== "") $("#hHouseParentID").val(pHouseParentID);
    //$("#btnSaveandNewShipment").attr("onclick", "Operations_InsertFromBLDocuments(true);");
}
function Shipments_FillControlsFromBLDocuments(pID, MasterOperationID) {
    debugger;

    //if (pDefaults.UnEditableCompanyName == "GLS" && !$("#cbIsAir").prop("checked") && $("#cbIsExport").prop("checked")) {
    $("#txtShipmentHouseNumber").attr("data-required", false);
    //}
    //else {
    //$("#txtShipmentHouseNumber").attr("data-required", true);
    //}

    if ($("#cbIsConsolidation").prop("checked")) {
        $("#divShipmentContainers").removeClass("hide"); //in EditPackageModal 
    }
    else {
        $("#divShipmentContainers").addClass("hide"); //in EditPackageModal
    }
    FadePageCover(true);
    $("#tblShipmentPackage tbody").html("");
    ClearAll("#ShipmentModal");
    ClearAll("#ClearancePropertiesModal");
    jQuery("#ShipmentModal").modal("show");
    var pParametersWithValues = { pMasterOperationID: MasterOperationID/*$("#hOperationID").val()*/, pShipmentID: pID };

    CallGETFunctionWithParameters("/api/Operations/FillShipmentControls", pParametersWithValues
        , function (pData) {
            var pShipmentHeader = JSON.parse(pData[0]);
            var pCustomers = pData[1];
            var pAgents = pData[2];
            var pShipmentPackages = pData[3];
            var pPackageTypes = pData[4];
            var pFinalDestination = pData[5];
            var pContainers = pData[6];
            var pNotifyID = pData[7];
            var pMainRoute = JSON.parse(pData[8]);
            var pPickupCityList = pData[12];
            var pCountryList = pData[13];
            var pIncotermsList = pData[14];
            var pMoveTypesList = pData[15];
            var pNoAccessFreightTypesList = pData[16];

            var pIsOperationClosed = pData[17];
            var pVessels = pData[18];
            var pUsers = pData[19];

            var _Salesmentemp = JSON.parse(pUsers);
            var pSalesmen = jQuery.grep(_Salesmentemp, function (_Salesmentemp) {
                return _Salesmentemp.IsSalesman == true;
            });
            FillListFromObject(null, 2, "<--Select-->", "slOperationSalesmanForBLDocuments", JSON.stringify(pSalesmen), function () {
                $("#slOperationSalesmanForBLDocuments").val(pShipmentHeader.SalesmanID);
            });

            pIsOperationDisabled = pIsOperationClosed == true || pShipmentHeader.OperationStageID == CancelledQuoteAndOperStageID ? 1 : 0;
            //if (OAPac && $("#hIsOperationDisabled").val() == false) { $(".classSetCargoProperties").removeClass("hide"); $(".classSetClearanceProperties").removeClass("hide"); } else { $(".classSetCargoProperties").addClass("hide"); $(".classSetClearanceProperties").addClass("hide"); }
            if (/*OAPac && */pIsOperationDisabled == false) { $(".classSetCargoProperties").removeClass("hide"); $(".classSetClearanceProperties").removeClass("hide"); } else { $(".classSetCargoProperties").addClass("hide"); $(".classSetClearanceProperties").addClass("hide"); }

            if (/*$("#hIsOperationDisabled").val()*/pIsOperationDisabled == false) {
                $("#divShipmentPackageBtns").removeClass("hide");
                //$("#divShipmentPackage").removeClass("hide");
            } else {
                $("#divShipmentPackageBtns").addClass("hide");
                //$("#divShipmentPackage").addClass("hide");
            }


            if (pShipmentHeader.ShipmentType == constFCLShipmentType)
                pIsFCL = true;
            else if (pShipmentHeader.ShipmentType == constFTLShipmentType)
                pIsFTL = true;

            if ($("#slClearanceCurrency option").length == 0)
                $("#slClearanceCurrency").html($("#hReadySlCurrencies").html());




            $("#hShipmentID").val(pID);
            $("#hOperationID").val(pShipmentHeader.MasterOperationID);

            $("#slTransportType").val(pShipmentHeader.TransportType);
            $("#slShipmentType").val(pShipmentHeader.ShipmentType);


            if (/*$("#hDirectionType").val()*/pShipmentHeader.DirectionType == constImportDirectionType) {
                $("#slConsigneesForBLDocuments").attr("data-required", true);
                $("#slShippersForBLDocuments").attr("data-required", false);
            }
            else { //Export or Domestic
                $("#slShippersForBLDocuments").attr("data-required", true);
                $("#slConsigneesForBLDocuments").attr("data-required", false);
            }

            //FillListFromObject(pShipmentHeader.ShipperID, 2, "<--Select-->", "slShippersForBLDocuments", pCustomers
            //    , function () {
            //        $("#slConsigneesForBLDocuments").html($("#slShippersForBLDocuments").html()); $("#slConsigneesForBLDocuments").val(pShipmentHeader.ConsigneeID == 0 ? "" : pShipmentHeader.ConsigneeID);
            //        $("#slNotifyForBLDocuments").html($("#slShippersForBLDocuments").html()); $("#slNotifyForBLDocuments").val(pNotifyID == 0 ? "" : pNotifyID);
            //    });
            $("#slConsigneesForBLDocuments").html($("#hReadySlCustomers").html()); $("#slConsigneesForBLDocuments").val(pShipmentHeader.ConsigneeID == 0 ? "" : pShipmentHeader.ConsigneeID);
            $("#slConsignees2").html($("#hReadySlCustomers").html()); $("#slConsignees2").val(pShipmentHeader.ConsigneeID2 == 0 ? "" : pShipmentHeader.ConsigneeID2);
            $("#slShippersForBLDocuments").html($("#hReadySlCustomers").html()); $("#slShippersForBLDocuments").val(pShipmentHeader.ShipperID == 0 ? "" : pShipmentHeader.ShipperID);
            $("#slNotifyForBLDocuments").html($("#hReadySlCustomers").html()); $("#slNotifyForBLDocuments").val(pNotifyID == 0 ? "" : pNotifyID);

            FillListFromObject(pShipmentHeader.AgentID, 2, "<--Select-->", "slAgentsForBLDocuments", pAgents, null);
            FillListFromObject(null, 2, "<--Select-->", "slPackageTypes", pPackageTypes, null); //to be used when open PackagesModal
            FillListFromObject(pShipmentHeader.DeliveryCityID, 4, "<--Select-->", "slDeliveryCity", pFinalDestination, null);
            FillListFromObject(pShipmentHeader.PickupCityID, 4, "<--Select-->", "slPickupCity", pPickupCityList, null);
            FillListFromObject(pShipmentHeader.POD, 4, "<--Select-->", "slPODForBLDocuments", pFinalDestination, null);
            FillListFromObject(pShipmentHeader.POL, 4, "<--Select-->", "slPOLForBLDocuments", pPickupCityList, null);

            FillListFromObject(pShipmentHeader.POLCountryID, 2, "<--Select-->", "slPickupCountry", pCountryList, function () {
                $("#slDeliveryCountry").html($("#slPickupCountry").html());
                $("#slDeliveryCountry").val(pShipmentHeader.PODCountryID);
            });
            FillListFromObject(null, 11, "<--Select-->", "slShipmentContainers", pContainers, null); //to be used when open PackagesModal

            FillListFromObject(null, 2, "Select Incoterm", "slShipmentIncoterm", pIncotermsList, null);
            FillListFromObject(null, 2, "Select Service Scope", "slOperationMoveTypesForBLDocuments", pMoveTypesList, null);
            FillListFromObject(null, 2, "Select Freight Type", "slShipmentPOrC", pNoAccessFreightTypesList, null);
            FillListFromObject(pShipmentHeader.VesselID, 2, "Select Vessel", "slOperationVesselID", pVessels, null);
            FillListFromObject(pShipmentHeader.OperationManID, 2, "<--Select-->", "slOperationOperationManID", pUsers, null);



            //$("#slShipmentPackageTypes").html($("#slPackageTypes").html());
            $("#slOperationBranchForBLDocuments").html($("#hReadySlBranches").html()); $("#slOperationBranchForBLDocuments").val(pShipmentHeader.BranchID);
            $("#slOperationMoveTypesForBLDocuments").val(pShipmentHeader.MoveTypeID == 0 ? "" : pShipmentHeader.MoveTypeID);
            $("#slShipmentIncoterm").val(pShipmentHeader.IncotermID == 0 ? "" : pShipmentHeader.IncotermID);

            $("#slShipmentPOrC").val(pShipmentHeader.POrC == 0 ? "" : pShipmentHeader.POrC);
            $("#txtShipmentDeliveryOrderNumber").val(pShipmentHeader.ID);
            $("#txtShipmentDeliveryDate").val(Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(pMainRoute.DeliveryDate)) < 1 ? "" : ConvertDateFormat(GetDateWithFormatMDY(pMainRoute.DeliveryDate)));

            $("#txtShipmentReleaseDate").val(Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(pShipmentHeader.ReleaseDate)) < 1 ? "" : ConvertDateFormat(GetDateWithFormatMDY(pShipmentHeader.ReleaseDate)));

            $("#txtShipmentBLDate").val(Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(pShipmentHeader.BLDate)) < 1 ? "" : ConvertDateFormat(GetDateWithFormatMDY(pShipmentHeader.BLDate)));
            $("#txtShipmentShippedOnBoardDate").val(Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(pShipmentHeader.ShippedOnBoardDate)) < 1 ? "" : ConvertDateFormat(GetDateWithFormatMDY(pShipmentHeader.ShippedOnBoardDate)));
            $("#txtShipmentFreightPayableAt").val(pShipmentHeader.FreightPayableAt == 0 ? "" : pShipmentHeader.FreightPayableAt);
            $("#txtShipmentACIDNumber").val(pShipmentHeader.ACIDNumber == 0 ? "" : pShipmentHeader.ACIDNumber);
            $("#txtShipmentACIDDetails").val(pShipmentHeader.ACIDDetails == 0 ? "" : pShipmentHeader.ACIDDetails);
            $("#txtShipmentBookingNumber").val(pShipmentHeader.BookingNumber == 0 ? "" : pShipmentHeader.BookingNumber);
            $("#txtShipmentNumberOfOriginalBills").val(pShipmentHeader.NumberOfOriginalBills == 0 ? "" : pShipmentHeader.NumberOfOriginalBills);


            ShipmentPackage_BindTableRows(JSON.parse(pShipmentPackages));

            $("#lblShownShipment").html(pShipmentHeader.HouseNumber == 0 ? "" : pShipmentHeader.HouseNumber);
            $("#txtShipmentHouseNumber").val(pShipmentHeader.HouseNumber == 0 ? "" : pShipmentHeader.HouseNumber);
            $("#txtShipmentCustomerReference").val(pShipmentHeader.CustomerReference == 0 ? "" : pShipmentHeader.CustomerReference);
            $("#txtShipmentSupplierReference").val(pShipmentHeader.SupplierReference == 0 ? "" : pShipmentHeader.SupplierReference);
            $("#txtShipmentPONumber").val(pShipmentHeader.PONumber == 0 ? "" : pShipmentHeader.PONumber);
            $("#divNotesForBLDocuments").val(pShipmentHeader.Notes == 0 ? "" : pShipmentHeader.Notes);
            $("#txtOperationOpenDateForBLDocuments").val(ConvertDateFormat(GetDateWithFormatMDY(pShipmentHeader.OpenDate)));
            $("#txtOperationCloseDateForBLDocuments").val(ConvertDateFormat(GetDateWithFormatMDY(pShipmentHeader.CloseDate)));

            $("#cbIsDelivered").prop("checked", pShipmentHeader.IsDelivered);
            $("#cbIsReceivedFromShipper").prop("checked", pShipmentHeader.IsReceivedFromShipper);
            if (pDefaults.UnEditableCompanyName == "ELI") {
                cbIsReceivedFromShipperChanged();
            }

            $("#txtShipmentCertificateNumber").val(pShipmentHeader.CertificateNumber == 0 ? "" : pShipmentHeader.CertificateNumber);
            $("#txtShipmentCountryOfOrigin").val(pShipmentHeader.CountryOfOrigin == 0 ? "" : pShipmentHeader.CountryOfOrigin);
            $("#txtShipmentInvoiceValue").val(pShipmentHeader.InvoiceValue == 0 ? "" : pShipmentHeader.InvoiceValue);
            $("#slClearanceCurrency").val(pShipmentHeader.CurrencyID);

            $("#txtOperationUNNumber").val(pShipmentHeader.UNNumber == 0 ? "" : pShipmentHeader.UNNumber);
            $("#txtOperationIMOClass").val(pShipmentHeader.IMOClass == 0 ? "" : pShipmentHeader.IMOClass);

            FadePageCover(false);
        }
        , null);


    //parameter in the next 3 lines are 1:Quotations call, 2:Operations call
    $("#btn-NewAddShipperForBLDocuments").attr("onclick", "Customers_ClearAllControls(1);");
    $("#btn-NewAddConsigneeForBLDocuments").attr("onclick", "Customers_ClearAllControls(1);");
    $("#btn-NewAddNotifyForBLDocuments").attr("onclick", "Customers_ClearAllControls(1);");
    $("#btn-NewAddAgentForBLDocuments").attr("onclick", "Agents_ClearAllControls(1);");

    $("#btn-EditShipperForBLDocuments").attr("onclick", "Customers_FillControlsFromOperations($('#slShippersForBLDocuments option:selected').val(), null, 1);");
    $("#btn-EditConsigneeForBLDocuments").attr("onclick", "Customers_FillControlsFromOperations($('#slConsigneesForBLDocuments option:selected').val(), null, 1);");
    $("#btn-EditNotifyForBLDocuments").attr("onclick", "Customers_FillControlsFromOperations($('#slNotifyForBLDocuments option:selected').val(), null, 1);");
    $("#btn-EditAgentForBLDocuments").attr("onclick", "Agents_FillControlsFromOperations($('#slAgentsForBLDocuments option:selected').val(), null, 1);");

    $("#btnSaveShipment").attr("onclick", "Shipment_Update();");
    $("#btnSaveClearanceProperties").attr("onclick", "Shipment_Update();");
}
function Shipment_Update() {
    debugger;
    if (ValidateForm("form", "ShipmentModal")) {
        FadePageCover(true);
        var pParametersWithValues = {
            pShipmentID: $("#hShipmentID").val()
            , pBranchID: $('#slOperationBranchForBLDocuments option:selected').val()
            , pSalesmanID: $('#slOperationSalesmanForBLDocuments option:selected').val()
            , pOpenDate: ConvertDateFormat($("#txtOperationOpenDateForBLDocuments").val().trim())
            , pHouseNumber: $("#txtShipmentHouseNumber").val().trim() == "" ? "0" : $("#txtShipmentHouseNumber").val().trim().toUpperCase()
            , pPickupCityID: ($("#slPickupCity").val() == "" || $("#slPickupCity").val() == undefined || $("#slPickupCity").val() == null ? 0 : $("#slPickupCity").val())
            , pDeliveryCityID: ($("#slDeliveryCity").val() == "" || $("#slDeliveryCity").val() == undefined || $("#slDeliveryCity").val() == null ? 0 : $("#slDeliveryCity").val())
            , pShipperID: $("#slShippersForBLDocuments").val() == "" ? 0 : $("#slShippersForBLDocuments").val()
            , pConsigneeID: $("#slConsigneesForBLDocuments").val() == "" ? 0 : $("#slConsigneesForBLDocuments").val()
            , pAgentID: $("#slAgentsForBLDocuments").val() == "" ? 0 : $("#slAgentsForBLDocuments").val()
            , pNotifyID: $("#slNotifyForBLDocuments").val() == "" || $("#slNotifyForBLDocuments").val() == null ? 0 : $("#slNotifyForBLDocuments").val()
            , pIncotermID: $("#slShipmentIncoterm").val() == "" ? 0 : $("#slShipmentIncoterm").val()
            , pMoveTypeID: ($('#slOperationMoveTypesForBLDocuments option:selected').val() == "" ? 0 : $('#slOperationMoveTypesForBLDocuments option:selected').val())
            , pNotes: $("#divNotesForBLDocuments").val().trim() == "" ? "0" : $("#divNotesForBLDocuments").val().trim().toUpperCase()

            , pCommodityID: 0
            , pCustomerReference: $("#txtShipmentCustomerReference").val().trim() == "" ? "0" : $("#txtShipmentCustomerReference").val().trim().toUpperCase()
            , pSupplierReference: $("#txtShipmentSupplierReference").val().trim() == "" ? "0" : $("#txtShipmentSupplierReference").val().trim().toUpperCase()
            , pPONumber: $("#txtShipmentPONumber").val().trim() == "" ? "0" : $("#txtShipmentPONumber").val().trim().toUpperCase()
            , pPOrC: ($("#slShipmentPOrC").val() == "" ? 0 : $("#slShipmentPOrC").val())
            , pDeliveryOrderNumber: ($("#txtShipmentDeliveryOrderNumber").val().trim() == "" ? 0 : $("#txtShipmentDeliveryOrderNumber").val().trim().toUpperCase())
            , pDeliveryDate: ($("#txtShipmentDeliveryDate").val().trim() == "" ? "01/01/1900" : ConvertDateFormat($("#txtShipmentDeliveryDate").val()))

            , pIsDelivered: $("#cbIsDelivered").prop("checked")
            , pIsReceivedFromShipper: $("#cbIsReceivedFromShipper").prop("checked")
            , pConsigneeID2: $("#slConsignees2").val() == "" ? 0 : $("#slConsignees2").val()
            , pReleaseDate: ($("#txtShipmentReleaseDate").val().trim() == "" ? "01/01/1900" : ConvertDateFormat($("#txtShipmentReleaseDate").val()))

            , pBLDate: ($("#txtShipmentBLDate").val().trim() == "" ? "01/01/1900" : ConvertDateFormat($("#txtShipmentBLDate").val()))
            , pShippedOnBoardDate: ($("#txtShipmentShippedOnBoardDate").val().trim() == "" ? "01/01/1900" : ConvertDateFormat($("#txtShipmentShippedOnBoardDate").val()))
            , pFreightPayableAt: $("#txtShipmentFreightPayableAt").val().trim() == "" ? "0" : $("#txtShipmentFreightPayableAt").val().trim().toUpperCase()
            , pACIDNumber: $("#txtShipmentACIDNumber").val().trim() == "" ? "0" : $("#txtShipmentACIDNumber").val().trim().toUpperCase()
            , pACIDNumberDetails: $("#txtShipmentACIDDetails").val() == "" ? "0" : $("#txtShipmentACIDDetails").val()
            , pBookingNumber: $("#txtShipmentBookingNumber").val() == "" ? "0" : $("#txtShipmentBookingNumber").val()
            , pNumberOfOriginalBills: $("#txtShipmentNumberOfOriginalBills").val().trim() == "" ? "0" : $("#txtShipmentNumberOfOriginalBills").val().trim().toUpperCase()

            , pCertificateNumber: 0
            , pCountryOfOrigin: 0
            , pInvoiceValue: 0
            , pCurrencyID: 0

            , pUNNumber: $("#txtOperationUNNumber").val() == "" || $("#txtOperationUNNumber").val() == null ? 0 : $("#txtOperationUNNumber").val()
            , pIMOClass: $("#txtOperationIMOClass").val() == "" || $("#txtOperationIMOClass").val() == null ? 0 : $("#txtOperationIMOClass").val()
            , pVesselID: $("#slOperationVesselID").val() == "" || $("#slOperationVesselID").val() == null ? 0 : $("#slOperationVesselID").val()
            , pOperationManID: $("#slOperationOperationManID").val() == "" || $("#slOperationOperationManID").val() == null ? 0 : $("#slOperationOperationManID").val()

            , pPOLCountryID: $("#slPickupCountry").val()
            , pPOL: $("#slPOLForBLDocuments").val()
            , pPODCountryID: $("#slDeliveryCountry").val()
            , pPOD: $("#slPODForBLDocuments").val()

        };
        CallGETFunctionWithParameters("/api/Operations/Shipment_Update"
            , pParametersWithValues
            , function (pData) {
                var _MessageReturned = pData[1];
                if (_MessageReturned != "")
                    swal("Success", _MessageReturned);
                else if (pData[0]) {
                    swal("Success", "Saved successfully.");
                    if (pDefaults.UnEditableCompanyName == "NEW")
                        Certificate_GetCertificateHousesAndGrossWeight(MasterOperationID/*$("#hOperationID").val()*/);
                }
                else
                    swal("Sorry", "Connection failed, please try again.");

                jQuery("#ShipmentModal").modal("hide");
                Operations_LoadingWithPaging();//Shipments_LoadAvailableShipments();
                FadePageCover(false);
            }
            , null);
    }
}
function ShipmentPackage_BindTableRows(pTableRows) {
    debugger;
    var editControlsText = " class='btn btn-xs btn-rounded btn-lightblue float-right' title='Edit'> <i class='fa fa-pencil' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + TranslateString("Edit") + "</span>";
    $("#tblShipmentPackage tbody").html("");
    var pHTMLHeaderColumns = "";
    var transferControlsText = " class='btn btn-xs btn-rounded btn-info float-right' > <i class='fa fa-pencil' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + "Transfer" + "</span>";
    //isa i am sure its master coz shipment is opened

    //if ($("#cbIsFCLForBLDocuments").prop("checked") || $("#cbIsFTLForBLDocuments").prop("checked")) {
    if (pIsFCL || pIsFTL) {
        pHTMLHeaderColumns += '     <tr>';
        pHTMLHeaderColumns += '         <th id="HeaderDeleteShipmentPackageID">';
        pHTMLHeaderColumns += '             <input id="cbShipmentPackageDeleteHeader" type="checkbox" />';
        pHTMLHeaderColumns += '         </th>';
        pHTMLHeaderColumns += '         <th>Container Type</th>';
        pHTMLHeaderColumns += '         <th>Container No</th>';
        pHTMLHeaderColumns += '         <th>Carrier Seal</th>';
        pHTMLHeaderColumns += '         <th>TareWt(KG)</th>';
        pHTMLHeaderColumns += '         <th>Vol.(CBM)';
        pHTMLHeaderColumns += '         <th>Net Wt(KG)</th>';
        pHTMLHeaderColumns += '         <th>GrossWt(KG)</th>';
        pHTMLHeaderColumns += '         <th>VGM(KG)</th>';
        pHTMLHeaderColumns += '         <th class="rounded-right hide"></th>';
        pHTMLHeaderColumns += '     </tr>';
    }
    else {
        pHTMLHeaderColumns += '     <tr>';
        pHTMLHeaderColumns += '         <th id="HeaderDeleteShipmentPackageID">';
        pHTMLHeaderColumns += '             <input id="cbShipmentPackageDeleteHeader" type="checkbox" />';
        pHTMLHeaderColumns += '         </th>';
        pHTMLHeaderColumns += '         <th>Package Type</th>';
        pHTMLHeaderColumns += '         <th>Container</th>';
        pHTMLHeaderColumns += '         <th>Quantity</th>';
        pHTMLHeaderColumns += '         <th>Net Wt(KG)</th>';
        pHTMLHeaderColumns += '         <th>Vol.(CBM)';
        pHTMLHeaderColumns += '         <th>GrossWt(KG)</th>';
        pHTMLHeaderColumns += '         <th class="rounded-right hide"></th>';
        pHTMLHeaderColumns += '     </tr>';
    }
    $("#tblShipmentPackage thead").html(pHTMLHeaderColumns);
    if ($("#cbIsFCLForBLDocuments").prop("checked") || $("#cbIsFTLForBLDocuments").prop("checked")) {
        $.each(pTableRows, function (i, item) {
            AppendRowtoTable("tblShipmentPackage",
                ("<tr ID='" + item.ID + "' " + (/*OEPac && $("#hIsOperationDisabled").val()*/pIsOperationDisabled == false ? ("ondblclick='ShipmentPackage_FillControls(" + item.ID + ");'") : "") + ">"
                    + "<td class='ID'> <input name='Delete' type='checkbox' value='" + item.ID + "' /></td>"
                    + "<td class='Container' val='" + item.ContainerTypeID + "'>" + item.ContainerTypeCode + "</td>"
                    + "<td class='ContainerNumber'>" + (item.ContainerNumber == 0 ? "" : item.ContainerNumber) + "</td>"
                    + "<td class='CarrierSeal'>" + (item.CarrierSeal == 0 ? "" : item.CarrierSeal) + "</td>"
                    + "<td class='ShipperSeal hide'>" + (item.ShipperSeal == 0 ? "" : item.ShipperSeal) + "</td>"
                    + "<td class='TareWeight'>" + (item.TareWeight == 0 ? "" : item.TareWeight) + "</td>"
                    + "<td class='ContainerVolume'>" + (item.Volume == 0 ? "" : item.Volume) + "</td>"
                    + "<td class='ContainerNetWeight'>" + (item.NetWeight == 0 ? "" : item.NetWeight) + "</td>"
                    + "<td class='ContainerGrossWeight'>" + (item.GrossWeight == 0 ? "" : item.GrossWeight) + "</td>"
                    + "<td class='VGM'>" + (item.VGM == 0 ? "" : item.VGM) + "</td>"
                    + "<td class='IsReefer hide'> <input type='checkbox' disabled='disabled' val='" + (item.IsReefer == true ? "true' checked='checked'" : "'") + " /></td>"
                    + "<td class='IsNOR hide'> <input type='checkbox' disabled='disabled' val='" + (item.IsNOR == true ? "true' checked='checked'" : "'") + " /></td>"
                    + "<td class='IsSentToWarehouse hide'> <input type='checkbox' disabled='disabled' val='" + (item.IsSentToWarehouse == true ? "true' checked='checked'" : "'") + " /></td>"
                    + "<td class='IsLoaded hide'> <input type='checkbox' disabled='disabled' val='" + (item.IsLoaded == true ? "true' checked='checked'" : "'") + " /></td>"
                    + "<td class='MinTemp hide'>" + item.MinTemp + "</td>"
                    + "<td class='Humidity hide'>" + item.Humidity + "</td>"
                    + "<td class='Ventilation hide'>" + item.Ventilation + "</td>"
                    + "<td class='PackageTypeOnContainer hide' val='" + item.PackageTypeIDOnContainer + "'>" + (item.PackageTypeNameOnContainer == 0 ? "" : item.PackageTypeNameOnContainer) + "</td>"
                    + "<td class='LotNumber hide'>" + (item.LotNumber == 0 ? "" : item.LotNumber) + "</td>"
                    + "<td class='MaxTemp hide'>" + item.MaxTemp + "</td>"
                    + "<td class='IsIMO hide'> <input type='checkbox' disabled='disabled' val='" + (item.IsIMO == true ? "true' checked='checked'" : "'") + " /></td>"
                    + "<td class='IMOClass hide'>" + item.IMOClass + "</td>"
                    + "<td class='UNNumber hide'>" + item.UNNumber + "</td>"
                    + "<td class='FlashPoint hide'>" + item.FlashPoint + "</td>"

                    + "<td class='DescriptionOfGoods hide'>" + (item.DescriptionOfGoods == 0 ? "" : item.DescriptionOfGoods) + "</td>"
                    //+ "<td class='hide'><a href='#EditContainerModal' data-toggle='modal' onclick='ShipmentPackage_FillControls(" + item.ID + ");' " + editControlsText + "</a></td>"
                    + "<td class='" + (/*OAPac && ODPac && $("#hIsOperationDisabled").val()*/ pIsOperationDisabled == false ? "hide" : "hide") + "'><a href='#' data-toggle='modal' onclick='ShipmentPackage_TransferContainerModal(" + item.ID + "," + item.OperationID + ");' " + transferControlsText + "</a></td>"
                    + "</tr>"));

            //+ ($("#cbIsFCLForBLDocuments").prop('checked') || $("#cbIsFTLForBLDocuments").prop('checked') || $("#cbIsConsolidation").prop('checked')
            //    ? ("<td class='hide'><a href='#EditContainerModal' data-toggle='modal' onclick='OperationContainersAndPackages_FillControls(" + item.ID + ");' " + editControlsText + "</a></td></tr>")
            //    : ("<td class='hide'><a href='#EditPackageModal' data-toggle='modal' onclick='OperationContainersAndPackages_FillControls(" + item.ID + ");' " + editControlsText + "</a></td></tr>")
            //    )));
        });
    }
    else {
        $.each(pTableRows, function (i, item) {
            AppendRowtoTable("tblShipmentPackage",
                ("<tr ID='" + item.ID + "' " + (/*OEPac && $("#hIsOperationDisabled").val()*/pIsOperationDisabled == false ? ("ondblclick='ShipmentPackage_FillControls(" + item.ID + ");'") : "") + ">"
                    + "<td class='ID'> <input name='Delete' type='checkbox' value='" + item.ID + "' /></td>"
                    + "<td class='Package' val='" + item.PackageTypeID + "'>" + (item.PackageTypeName == 0 ? "" : item.PackageTypeName) + "</td>"
                    //+ "<td class='Container' val='" + item.ContainerTypeID + "'>" + (item.ContainerTypeName == 0 ? "" : item.ContainerTypeName) + "</td>"
                    + "<td class='PlacedOnOCPID' val='" + item.PlacedOnOCPID + "'>" + (item.PlacedOnOCPID == 0 ? "" : item.ConsolContainerNumber) + "</td>"
                    + "<td class='Quantity'>" + item.Quantity + "</td>"
                    + "<td class='Length hide'>" + item.Length + "</td>"
                    + "<td class='Width hide'>" + item.Width + "</td>"
                    + "<td class='Height hide'>" + item.Height + "</td>"
                    + "<td class='NetWeight'>" + item.NetWeight + "</td>"
                    + "<td class='Volume'>" + item.Volume + "</td>"
                    + "<td class='GrossWeight'>" + item.GrossWeight + "</td>"
                    + "<td class='VolumetricWeight hide'>" + item.VolumetricWeight + "</td>"
                    + "<td class='ChargeableWeight hide'>" + item.ChargeableWeight + "</td>"
                    + "<td class='DescriptionOfGoods hide'>" + (item.DescriptionOfGoods == 0 ? "" : item.DescriptionOfGoods) + "</td>"
                    + "<td class='hide'><a href='#EditPackageModal' data-toggle='modal' onclick='ShipmentPackage_FillControls(" + item.ID + ");' " + editControlsText + "</a></td>"
                    + "</tr>"));

        });
    }
    BindAllCheckboxonTable("tblShipmentPackage", "ID", "cbShipmentPackageDeleteHeader");
    CheckAllCheckbox("HeaderDeleteShipmentPackageID");
}
function Operations_InsertFromBLDocuments(pSaveandAddNew, pIsShipment, callback, pIsAWB) { //pIsShipment: is true if this fn is called from adding new house op from consolidation
    debugger;
    //var varExpirationDate = ($("#txtOperationCutOffDate").val().trim() == "" ? "" : $("#txtOperationCutOffDate").val().trim());
    //if (!isValidDate($("#txtOperationOpenDateForBLDocuments").val().trim(), 1) || !isValidDate(varExpirationDate, 1))
    $(".validation-error").removeClass("validation-error");
    FadePageCover(true);
    if (pDefaults.UnEditableCompanyName == "FEL" && $("#slAgentsForBLDocuments").val() == "")
        swal("Sorry", "Please, select agent.");
    if (!isValidDate($("#txtOperationOpenDateForBLDocuments").val().trim(), 1)) {
        swal(strSorry, strCheckDates);
        FadePageCover(false);
    }
    //else
    //    if (Date.prototype.compareDates(FormattedTodaysDate, ConvertDateFormat($("#txtOperationOpenDateForBLDocuments").val().trim())) < 0)
    //        swal(strSorry, "Check the open date.");
    else if (Date.prototype.compareDates(ConvertDateFormat($("#txtOperationOpenDateForBLDocuments").val().trim()), ConvertDateFormat($("#txtOperationCloseDateForBLDocuments").val().trim())) <= 0) {
        swal(strSorry, "Close date must be after open date.");
        FadePageCover(false);
    }
    //else if (!isValidDate($("#txtOperationCutOffDate").val().trim(), 1) && $("#txtOperationCutOffDate").val().trim() != "")
    //    swal("Sorry", "Please, Check Cut Off Date.");
    else if (isValidDate($("#txtOperationExpectedDepartureForBLDocuments").val().trim(), 1) && isValidDate($("#txtOperationExpectedArrivalForBLDocuments").val().trim(), 1)
        && Date.prototype.compareDates(ConvertDateFormat($("#txtOperationExpectedDepartureForBLDocuments").val().trim()), ConvertDateFormat($("#txtOperationExpectedArrivalForBLDocuments").val().trim())) < 0) {
        swal(strSorry, "ETA must be after ETD date.");
        FadePageCover(false);
    }
    else if ($('#slPOLForBLDocuments option:selected').val() == $('#slPODForBLDocuments option:selected').val() && $('#slPOLForBLDocuments option:selected').val() != "" && $('#slPOLForBLDocuments option:selected').val() != undefined && !$("#cbIsDomestic").prop("checked")) {//check different ports
        swal(strSorry, strPOLEqualPODWarning);
        FadePageCover(false);
    }
    //    //check Domestic with POLCountry = PODCountry
    //else if ($('input[name=cbDirectionType]:checked').val() == 3 && $('#slPOLCountriesForBLDocuments option:selected').val() != $('#slPODCountriesForBLDocuments option:selected').val() && $('#slPOLCountriesForBLDocuments option:selected').val() != undefined) {
    //    swal(strSorry, strDomesticWithDifferentCountriesWarning);
    //    FadePageCover(false);
    //}

    else if ($("#txtOperationNumberOfContainers").val() > 50 && $("#txtOperationNumberOfContainers").val() != "") {
        swal("Sorry", "You can not enter more than 50 containers at the first time.");
        FadePageCover(false);
    }
    else { //Ports are OK

        var DirectionTypeID = 0;
        var DirectionIconName = 0;
        var DirectionIconStyle = 0;

        if (glbCallingControl == "ShippingOrders") {
            DirectionTypeID = constExportDirectionType; DirectionIconName = ExportIconName; DirectionIconStyle = strExportIconStyleClassName;    // Export
        } else if (glbCallingControl == "RoutingOrders") {
            DirectionTypeID = constImportDirectionType; DirectionIconName = ImportIconName; DirectionIconStyle = strImportIconStyleClassName;    // Import
        }


        var TransportIconName = 0;
        var TransportIconStyle = 0;

        if ($("#slTransportType").val() == OceanTransportType) {
            TransportIconName = OceanIconName; TransportIconStyle = strOceanIconStyleClassName;
        }
        else if ($("#slTransportType").val() == AirTransportType) {
            TransportIconName = AirIconName; TransportIconStyle = strAirIconStyleClassName;
        }
        else if ($("#slTransportType").val() == InlandTransportType) {
            TransportIconName = InlandIconName; TransportIconStyle = strInlandIconStyleClassName;
        }

        
        var parameters = {
            //if HouseNumber is not null then its entered manually
            "pIsShipment": pIsShipment,
            "pCodeSerial": 0 /*generated automatically*/,
            //"pCode": "O" + $("#txtOperationOpenDateForBLDocuments").val().substring(10, 8) + "-" + GetDirectionType($('input[name=cbDirectionType]:checked').val()).substring(0, 3) + "-"
            //             + GetTransportType($('input[name=cbTransportType]:checked').val()).substring(0, 2) + "-",
            "pCode": (pIsShipment || $('input[name=cbBLType]:checked').val() == constHouseBLType)
                ? "0"
                : ("O" + $("#txtOperationOpenDateForBLDocuments").val().substring(10, 8) + "-" + GetDirectionType($('input[name=cbDirectionType]:checked').val()).substring(0, 3) + "-"
                    + GetTransportType($('input[name=cbTransportType]:checked').val()).substring(0, 2) + "-"),
            "pHouseNumber": pIsShipment
                ? ($("#txtShipmentHouseNumber").val().trim().toUpperCase() == "" ? 0 : $("#txtShipmentHouseNumber").val().trim().toUpperCase())
                : "0",//if "0" then auto generated
            "pBranchID": $('#slOperationBranchForBLDocuments option:selected').val(),
            "pSalesmanID": $('#slOperationSalesmanForBLDocuments option:selected').val(),
            //"pOperationManID": $('#slOperationOperationMan option:selected').val(),

            "pBLType": pIsShipment ? constHouseBLType : $('input[name=cbBLType]:checked').val(),
            "pBLTypeIconName": HouseIconName,
            "pBLTypeIconStyle": strHouseIconStyleClassName,

            "pDirectionType": DirectionTypeID,//$('input[name=cbDirectionType]:checked').val(),
            "pDirectionIconName": DirectionIconName,
            "pDirectionIconStyle": DirectionIconStyle,

            "pTransportType": $("#slTransportType").val(),//$('input[name=cbTransportType]:checked').val(),
            "pTransportIconName": TransportIconName,
            "pTransportIconStyle": TransportIconStyle,

            //"pShipmentType": pIsShipment
            //    ? ($("#cbIsOcean").prop("checked") || $("#cbIsInland").prop("checked")
            //            ? ($('input[name=cbShipmentType]:checked').val() == constConsolidationShipmentType ? ($("#cbIsOcean").prop("checked") ? constLCLShipmentType : constLTLShipmentType) : $('input[name=cbShipmentType]:checked').val())
            //            : 0 //air and house
            //    )/*No air in this case coz air has no consolidation*/
            //    : ($('input[name=cbTransportType]:checked').val() == AirTransportType ? 0 : $('input[name=cbShipmentType]:checked').val()),
            "pShipmentType": $("#slShipmentType").val(),
            "pMasterBL": pIsShipment
                ? "0"
                : ($("#txtOperationMasterBL").val().trim() == "" ? "0" : $("#txtOperationMasterBL").val().trim().toUpperCase()),
            "pShipperID": $('#slShippersForBLDocuments option:selected').val() == "" || $('#slShippersForBLDocuments option:selected').val() == null || $('#slShippersForBLDocuments option:selected').val() == undefined
                ? 0 : $('#slShippersForBLDocuments option:selected').val(),
            "pShipperAddressID": 0,
            "pShipperContactID": 0,
            //"pConsigneeID": (($('#slConsigneesForBLDocuments option:selected').val() == "" || $('input[name=cbDirectionType]:checked').val() == 2 || $('input[name=cbDirectionType]:checked').val() == 3)
            "pConsigneeID": $('#slConsigneesForBLDocuments option:selected').val() == "" || $('#slConsigneesForBLDocuments option:selected').val() == null || $('#slConsigneesForBLDocuments option:selected').val() == undefined
                ? 0 : $('#slConsigneesForBLDocuments option:selected').val(),
            "pConsigneeAddressID": 0,
            "pConsigneeContactID": 0,
            "pNotifyID": $('#slNotifyForBLDocuments option:selected').val() == "" || $('#slNotifyForBLDocuments option:selected').val() == null || $('#slNotifyForBLDocuments option:selected').val() == undefined
                ? 0 : $('#slNotifyForBLDocuments option:selected').val(),
            "pAgentID": $('#slAgentsForBLDocuments').val() == "" || $('#slAgentsForBLDocuments').val() == null
                ? 0 : $('#slAgentsForBLDocuments option:selected').val(),
            "pAgentAddressID": 0,
            "pAgentContactID": 0,
            "pIncotermID": pIsShipment ? ($("#slShipmentIncoterm").val() == "" ? 0 : $("#slShipmentIncoterm").val()) : 0,
            //"pPOrC": pDefaults.UnEditableCompanyName == "CQL" ? 3 : ($("#radIsConfirm1").prop('checked') ? 1 : 3),
            "pPOrC": pIsShipment
                ? ($("#slShipmentPOrC").val() == "" ? 0 : $("#slShipmentPOrC").val())
                : ($("#radIsConfirm1").prop('checked') ? 1 : 3),
            "pMoveTypeID": $("#slOperationMoveTypesForBLDocuments").val() == "" ? 0 : $("#slOperationMoveTypesForBLDocuments").val(),
            "pCommodityID": pIsShipment ? 0 : ($('#slCommodity').val() == "" ? 0 : $('#slCommodity').val()),
            "pTransientTime": 0,
            "pOpenDate": ConvertDateFormat($("#txtOperationOpenDateForBLDocuments").val().trim()),
            //"pCloseDate": null,
            "pCloseDate": ConvertDateFormat($("#txtOperationCloseDateForBLDocuments").val().trim()),
            "pCutOffDate": "01/01/1900",
            "pIncludePickup": false,
            "pPickupCityID": pIsShipment ? ($("#slPickupCity").val() == "" || $("#slPickupCity").val() == undefined || $("#slPickupCity").val() == null ? 0 : $("#slPickupCity").val()) : 0,
            "pPickupAddressID": 0,
            "pPOLCountryID": $('#slPickupCountry option:selected').val(),
            "pPOL": $('#slPOLForBLDocuments option:selected').val(),
            "pPODCountryID": $('#slDeliveryCountry option:selected').val(),
            "pPOD": $('#slPODForBLDocuments option:selected').val(),
            "pShippingLineID": (pIsShipment/*means its house*/ || $('input[name=cbBLType]:checked').val() == constHouseBLType || $('#slLines option:selected').val() == "")
                ? 0
                : ($('input[name=cbTransportType]:checked').val() == OceanTransportType ? $('#slLines option:selected').val() : 0),
            "pAirlineID": (pIsShipment/*means its house*/ || $('input[name=cbBLType]:checked').val() == constHouseBLType || $('#slLines option:selected').val() == "")
                ? 0
                : ($('input[name=cbTransportType]:checked').val() == AirTransportType ? $('#slLines option:selected').val() : 0),
            "pTruckerID": (pIsShipment/*means its house*/ || $('input[name=cbBLType]:checked').val() == constHouseBLType || $('#slLines option:selected').val() == "")
                ? 0
                : ($('input[name=cbTransportType]:checked').val() == InlandTransportType ? $('#slLines option:selected').val() : 0),
            "pIncludeDelivery": false,
            "pDeliveryZipCode": 0,
            "pDeliveryCityID": (pIsShipment
                ? ($("#slDeliveryCity").val() == "" || $("#slDeliveryCity").val() == undefined || $("#slDeliveryCity").val() == null ? 0 : $("#slDeliveryCity").val())
                : 0),
            "pDeliveryCountryID": 0,//i am leaving it the same as PODCountryID
            "pNetWeight": pIsShipment ?
                ($("#txtShipmentNetWeight").val() == "" ? 0 : $("#txtShipmentNetWeight").val())
                : 0,
            "pGrossWeight": pIsShipment ?
                ($("#txtShipmentGrossWeight").val() == "" ? 0 : $("#txtShipmentGrossWeight").val())
                : ($("#txtGrossWeight").val() == "" ? 0 : $("#txtGrossWeight").val()),
            "pVolume": pIsShipment ?
                ($("#txtShipmentVolume").val() == "" ? 0 : $("#txtShipmentVolume").val())
                : 0,
            "pChargeableWeight": pIsShipment ?
                0
                : ($("#txtChargeableWeight").val() == "" ? 0 : $("#txtChargeableWeight").val()),
            "pPackageTypeID": pIsShipment ?
                $("#slShipmentPackageTypes").val() == "" || $("#slShipmentPackageTypes").val() == null || $("#slShipmentPackageTypes").val() == undefined ? 0 : $("#slShipmentPackageTypes").val()
                : 0,
            "pNumberOfPackages": pIsShipment ?
                ($("#txtShipmentNumberOfPackages").val() == "" || $("#txtShipmentNumberOfPackages").val() == 0 ? 1 : $("#txtShipmentNumberOfPackages").val())
                : 1,
            "pIsDangerousGoods": $("#cbDangerousGoods").prop("checked"),
            "pNotes": $("#divNotesForBLDocuments").val().trim().toUpperCase(),
            "pIsDelivered": pIsShipment ? $("#cbIsDelivered").prop("checked") : false,
            "pIsTrucking": false, //$("#cbIsTrucking").prop("checked"),
            "pIsInsurance": false, //$("#cbIsInsurance").prop("checked"),
            "pIsClearance": false, //$("#cbIsClearance").prop("checked"),
            "pIsGenset": false, //$("#cbIsGenset").prop("checked"),
            "pIsCourrier": false, //$("#cbIsCourrier").prop("checked"),
            "pIsTelexRelease": false, //$("#cbIsTelexRelease").prop("checked"),
            "pCustomerReference": pIsShipment ?
                ($("#txtShipmentCustomerReference").val() == "" ? 0 : $("#txtShipmentCustomerReference").val())
                : "0",
            "pSupplierReference": pIsShipment ?
                ($("#txtShipmentSupplierReference").val() == "" ? 0 : $("#txtShipmentSupplierReference").val())
                : "0",
            "pPONumber": pIsShipment ?
                ($("#txtShipmentPONumber").val() == "" ? 0 : $("#txtShipmentPONumber").val())
                : "0",
            "pAgreedRate": "0",
            "pOperationStageID": 60, //this means Order
            "pNumberOfHousesConnected": 0,
            "pExpectedDeparture": pIsShipment
                ? "01/01/1900"
                : ($("#txtOperationExpectedDepartureForBLDocuments").val().trim() == "" ? "01/01/1900" : $("#txtOperationExpectedDepartureForBLDocuments").val().trim().toUpperCase()),
            "pExpectedArrival": pIsShipment
                ? "01/01/1900"
                : ($("#txtOperationExpectedArrivalForBLDocuments").val().trim() == "" ? "01/01/1900" : $("#txtOperationExpectedArrivalForBLDocuments").val().trim().toUpperCase()),
            "pVoyageOrTruckNumber": pIsShipment
                ? "0"
                : ($("#txtOperationVoyageOrTruckNumber").val().trim() == "" ? "0" : $("#txtOperationVoyageOrTruckNumber").val().trim().toUpperCase()),
            "pVesselID": pIsShipment
                ? "0"
                : ($("#slOperationVessels").val() == "" ? "0" : $("#slOperationVessels").val()),
            "pContainerTypeID": pIsShipment
                ? "0"
                : ($("#slOperationContainerType").val() == "" ? "0" : $("#slOperationContainerType").val()),
            "pNumberOfContainers": pIsShipment
                ? "0"
                : ($("#txtOperationNumberOfContainers").val() == "" ? "0" : $("#txtOperationNumberOfContainers").val()),

            "pContainerTypeID2": pIsShipment
                ? "0"
                : ($("#slOperationContainerType2").val() == "" ? "0" : $("#slOperationContainerType2").val()),
            "pNumberOfContainers2": pIsShipment
                ? "0"
                : ($("#txtOperationNumberOfContainers2").val() == "" ? "0" : $("#txtOperationNumberOfContainers2").val()),

            "pContainerTypeID3": pIsShipment
                ? "0"
                : ($("#slOperationContainerType3").val() == "" ? "0" : $("#slOperationContainerType3").val()),
            "pNumberOfContainers3": pIsShipment
                ? "0"
                : ($("#txtOperationNumberOfContainers3").val() == "" ? "0" : $("#txtOperationNumberOfContainers3").val()),
            //"pIsInactive": $("#cbIsInactive").prop('checked'),
            /***************************Venus Fields: A.Medra****************************/
            "pBLDate":
                pIsShipment
                    ? "01/01/1900"
                    :
                    ($("#txtOperationBLDate").val().trim() == "" ? "01/01/1900" : $("#txtOperationBLDate").val().trim()),
            "pMAWBStockID": $("#slMAWBStock").val() == "" ? 0 : $("#slMAWBStock").val(),
            "pTypeOfStockID": $('#slTypeOfStock  option:selected').val() == "" ? 0 : $('#slTypeOfStock option:selected').val(),
            "pMAWBSuffix": pIsShipment ? "0" : ($("#slMAWBStock").val() == "" ? "0" : $("#slMAWBStock option:selected").text()),
            "pFlightNo": pIsShipment ? "0" : ($("#txtFlightNo").val().trim() == "" ? 0 : $("#txtFlightNo").val().toUpperCase()),
            "pIsAWB": pIsShipment ? false : $("#cbIsAWB").prop("checked"),
            "pConsigneeID2": $('#slConsignees2 option:selected').val() == "" || $('#slConsignees2 option:selected').val() == null || $('#slConsignees2 option:selected').val() == undefined
                ? 0 : $('#slConsignees2 option:selected').val(),
            "pReleaseDate": pIsShipment
                ? ($("#txtShipmentReleaseDate").val().trim() == "" ? "01/01/1900" : $("#txtShipmentReleaseDate").val().trim())
                : "01/01/1900",
            "pCertificateNumber": 0,
            "pCountryOfOrigin": 0,
            "pInvoiceValue": 0,
            "pCurrencyID": 83,//pIsShipment ? ($("#slClearanceCurrency").val() == "" ? 0 : $("#slClearanceCurrency").val()) : "0",
            "pACIDNumber": $("#txtShipmentACIDNumber").val().trim() == "" ? "0" : $("#txtShipmentACIDNumber").val().trim().toUpperCase(),
            "pACIDNumberDetails": $("#txtShipmentACIDDetails").val() == "" ? "0" : $("#txtShipmentACIDDetails").val(),
            "pBookingNumber": $("#txtShipmentBookingNumber").val() == "" ? "0" : $("#txtShipmentBookingNumber").val(),
            "pUNNumber": pIsShipment ? ($("#txtShipmentUNNumber").val() == "" || $("#txtShipmentUNNumber").val() == null ? 0 : $("#txtShipmentUNNumber").val())
                : ($("#txtOperationUNNumber").val() == "" || $("#txtOperationUNNumber").val() == null ? 0 : $("#txtOperationUNNumber").val()),
            "pIMOClass": pIsShipment ? ($("#txtShipmentIMOClass").val() == "" || $("#txtShipmentIMOClass").val() == null ? 0 : $("#txtShipmentIMOClass").val())
                : ($("#txtOperationIMOClass").val() == "" || $("#txtOperationIMOClass").val() == null ? 0 : $("#txtOperationIMOClass").val()),
            "pVesselID": pIsShipment ? ($("#slShipmentVesselID").val() == "" || $("#slShipmentVesselID").val() == null ? 0 : $("#slShipmentVesselID").val())
                : ($("#txtOperationVesselID").val() == "" || $("#txtOperationVesselID").val() == null ? 0 : $("#txtOperationVesselID").val())
        };
        PostInsertUpdateFunction("form", "/api/Operations/Insert", parameters, pSaveandAddNew, pIsShipment ? "ShipmentModal" : "OperationModal", callback);
    }
}

function Operations_TransferHousesFromBLDocuments() {
    debugger;
    if ($("#hslMasterOperations").val() == "")
        swal("Sorry", "Please, select The Master operation.");
    else if (GetAllSelectedIDsAsString('tblOperations') == "") {
        swal(strSorry, "Please Select at least one House");
    } else {
        // loop through Houses and make sure they all have the same POL and POD of the selected master operation

        let POD = $("#hslMasterOperations option:selected").attr("pod");
        let POL = $("#hslMasterOperations option:selected").attr("pol");
        let IsPortsOK = true;

        let TransportType = $("#hslMasterOperations option:selected").attr("transporttype");
        let IsTransportTypeOK = true;

        let ShipmentType = $("#hslMasterOperations option:selected").attr("shipmenttype");
        let IsShipmentTypeOK = true;

        let RowPOD = 0;
        let RowPOL = 0;
        let RowTransportType = 0;
        let RowShipmentType = 0;
        GetAllSelectedIDsAsString('tblOperations').split(",").forEach((SelectedRowID) => {
            RowPOD = $("#tblOperations tbody #" + SelectedRowID + " .POD").attr("val");
            RowPOL = $("#tblOperations tbody #" + SelectedRowID + " .POL").attr("val");
            RowTransportType = $("#tblOperations tbody #" + SelectedRowID + " .TransportType").text().trim();
            RowShipmentType = $("#tblOperations tbody #" + SelectedRowID + " .ShipmentType").attr('val').trim();
            if (RowPOD != POD || RowPOL != POL) {
                IsPortsOK = false;
            }
            if (RowTransportType != TransportType) {
                IsTransportTypeOK = false;
            }
            if (RowShipmentType != ShipmentType) {
                IsShipmentTypeOK = false;
            }
        });

        if (!IsPortsOK) {
            swal(strSorry, "POD and POL are not the same")
        } else if (!IsTransportTypeOK) {
            swal(strSorry, "Transport Types must be the same")
        } else if (!IsShipmentTypeOK) {
            swal(strSorry, "Shipment Types must be the same")
        } else {
            swal({
                title: "Are you sure?",
                text: "House will be transfered.",
                type: "",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Apply",
                closeOnConfirm: false
            },
                //callback function in case of confirm delete
                function () {
                    FadePageCover(true);
                    var pParametersWithValues = {
                        pMasterOperationID: $("#hslMasterOperations").val()
                        , pIsHouseConnected: true
                        , pMasterOperationIDFieldInHouse: $("#hslMasterOperations").val()
                        , pHouseOperationsIDs: GetAllSelectedIDsAsString('tblOperations')
                        , pHouseParentID: 0
                    };

                    CallGETFunctionWithParameters("/api/Operations/ConnectOrDisconnectMultiple", pParametersWithValues
                        , function (DataArray) {
                            var IsError = false;
                            var ErrorMessage = "";
                            DataArray.forEach((pData, index) => {
                                let pReturnedMessage = pData[1];
                                if (pReturnedMessage != "") {
                                    IsError = true;
                                    ErrorMessage = pReturnedMessage;
                                }
                            });
                            if (!IsError) {
                                swal("Success", "Transferred successsfully.");
                                Operations_LoadingWithPaging();
                            }
                            else {
                                swal("Sorry", ErrorMessage);
                                FadePageCover(false);
                            }
                        }
                        , null);
                });
        }


    }

}
function Operations_CreateMasterOperation() {
    debugger;
    if (GetAllSelectedIDsAsString('tblOperations') == "") {
        swal(strSorry, "Please Select at least one House");
    } else {
        // loop through Houses and make sure they all have the same POL and POD
        // and make sure they all are not connected to operations

        let FirstSelectedRow = GetAllSelectedIDsAsString('tblOperations').split(",")[0];

        let POD = $("#tblOperations tbody #" + FirstSelectedRow + " .POD").attr("val");
        let POL = $("#tblOperations tbody #" + FirstSelectedRow + " .POL").attr("val");
        let IsPortsOK = true;

        let TransportType = $("#tblOperations tbody #" + FirstSelectedRow + " .TransportType").text().trim();
        let IsTransportTypeOK = true;

        let ShipmentType = $("#tblOperations tbody #" + FirstSelectedRow + " .ShipmentType").attr('val').trim();
        let IsShipmentTypeOK = true;

        let IsWithoutMasterOperation = true;

        let RowPOD = 0;
        let RowPOL = 0;
        let RowTransportType = 0;
        let RowShipmentType = 0;
        GetAllSelectedIDsAsString('tblOperations').split(",").forEach((SelectedRowID) => {
            RowPOD = $("#tblOperations tbody #" + SelectedRowID + " .POD").attr("val");
            RowPOL = $("#tblOperations tbody #" + SelectedRowID + " .POL").attr("val");
            RowTransportType = $("#tblOperations tbody #" + SelectedRowID + " .TransportType").text().trim();
            RowShipmentType = $("#tblOperations tbody #" + SelectedRowID + " .ShipmentType").text().trim();
            RowOperationCode = $("#tblOperations tbody #" + SelectedRowID + " .Code").text().trim();
            if (RowPOD != POD || RowPOL != POL) {
                IsPortsOK = false;
            }
            if (RowTransportType != TransportType) {
                IsTransportTypeOK = false;
            }
            if (RowShipmentType != ShipmentType) {
                IsShipmentTypeOK = false;
            }
            if (RowOperationCode != 0) {
                IsWithoutMasterOperation = false;
            }

        });

        if (!IsPortsOK) {
            swal(strSorry, "POD and POL are not the same")
        } else if (!IsTransportTypeOK) {
            swal(strSorry, "Transport Types must be the same")
        } else if (!IsShipmentTypeOK) {
            swal(strSorry, "Shipment Types must be the same")
        } else if (!IsWithoutMasterOperation) {
            swal(strSorry, "Must be Without a Master Operation")
        } else {
            // save a master operation ////////////////////////////////////////////////////////////////
            var DirectionTypeID = 0;
            var DirectionIconName = 0;
            var DirectionIconStyle = 0;

            if (glbCallingControl == "ShippingOrders") {
                DirectionTypeID = constExportDirectionType; DirectionIconName = ExportIconName; DirectionIconStyle = strExportIconStyleClassName;    // Export
            } else if (glbCallingControl == "RoutingOrders") {
                DirectionTypeID = constImportDirectionType; DirectionIconName = ImportIconName; DirectionIconStyle = strImportIconStyleClassName;    // Import
            }


            var TransportIconName = 0;
            var TransportIconStyle = 0;

            if (TransportType == OceanTransportType) {
                TransportIconName = OceanIconName; TransportIconStyle = strOceanIconStyleClassName;
            }
            else if (TransportType == AirTransportType) {
                TransportIconName = AirIconName; TransportIconStyle = strAirIconStyleClassName;
            }
            else if (TransportType == InlandTransportType) {
                TransportIconName = InlandIconName; TransportIconStyle = strInlandIconStyleClassName;
            }

            // get POLCountryID and POLCountryID
            debugger;
            CallGETFunctionWithParameters("/api/Ports/GetCountries", { pPortsList: `${POD},${POL}` }
                , function (CountriesList) {
                    var PODCountryID = CountriesList.split(',')[0];
                    var POLCountryID = CountriesList.split(',')[1];
                    var ErrorMessage = "";

                    var pIsShipment = false;
                    var parameters = {
                        //if HouseNumber is not null then its entered manually
                        "pIsShipment": false,
                        "pCodeSerial": 0 /*generated automatically*/,
                        "pCode": ("O" + FormattedTodaysDate.substring(10, 8) + "-" + GetDirectionType(DirectionTypeID).substring(0, 3) + "-"
                            + GetTransportType(TransportType).substring(0, 2) + "-"),
                        "pHouseNumber": "0",
                        "pBranchID": 0,
                        "pSalesmanID": 0,

                        "pBLType": constMasterBLType,
                        "pBLTypeIconName": HouseIconName,
                        "pBLTypeIconStyle": strHouseIconStyleClassName,

                        "pDirectionType": DirectionTypeID,//$('input[name=cbDirectionType]:checked').val(),
                        "pDirectionIconName": DirectionIconName,
                        "pDirectionIconStyle": DirectionIconStyle,

                        "pTransportType": TransportType,//$('input[name=cbTransportType]:checked').val(),
                        "pTransportIconName": TransportIconName,
                        "pTransportIconStyle": TransportIconStyle,

                        //"pShipmentType": pIsShipment
                        //    ? ($("#cbIsOcean").prop("checked") || $("#cbIsInland").prop("checked")
                        //            ? ($('input[name=cbShipmentType]:checked').val() == constConsolidationShipmentType ? ($("#cbIsOcean").prop("checked") ? constLCLShipmentType : constLTLShipmentType) : $('input[name=cbShipmentType]:checked').val())
                        //            : 0 //air and house
                        //    )/*No air in this case coz air has no consolidation*/
                        //    : ($('input[name=cbTransportType]:checked').val() == AirTransportType ? 0 : $('input[name=cbShipmentType]:checked').val()),
                        "pShipmentType": ShipmentType,
                        "pMasterBL": "0",
                        "pShipperID": 0,
                        "pShipperAddressID": 0,
                        "pShipperContactID": 0,
                        "pConsigneeID": 0,
                        "pConsigneeAddressID": 0,
                        "pConsigneeContactID": 0,
                        "pNotifyID": 0,
                        "pAgentID": 0,
                        "pAgentAddressID": 0,
                        "pAgentContactID": 0,
                        "pIncotermID": 0,
                        "pPOrC": 1, // prepaid or 3 for collect
                        "pMoveTypeID": 0,
                        "pCommodityID": 0,
                        "pTransientTime": 0,
                        "pOpenDate": ConvertDateFormat(getTodaysDateInddMMyyyyFormat()),
                        //"pCloseDate": null,
                        "pCloseDate": ConvertDateFormat('10/10/2050'),
                        "pCutOffDate": "01/01/1900",
                        "pIncludePickup": false,
                        "pPickupCityID": 0,
                        "pPickupAddressID": 0,
                        "pPOLCountryID": POLCountryID,
                        "pPOL": POL,
                        "pPODCountryID": PODCountryID,
                        "pPOD": POD,
                        "pShippingLineID": 0,
                        "pAirlineID": 0,
                        "pTruckerID": 0,
                        "pIncludeDelivery": false,
                        "pDeliveryZipCode": 0,
                        "pDeliveryCityID": 0,
                        "pDeliveryCountryID": 0,//i am leaving it the same as PODCountryID
                        "pNetWeight": 0,
                        "pGrossWeight": 0,
                        "pVolume": 0,
                        "pChargeableWeight": 0,
                        "pPackageTypeID": 0,
                        "pNumberOfPackages": 1,
                        "pIsDangerousGoods": false,
                        "pNotes": "0",
                        "pIsDelivered": false,
                        "pIsTrucking": false, //$("#cbIsTrucking").prop("checked"),
                        "pIsInsurance": false, //$("#cbIsInsurance").prop("checked"),
                        "pIsClearance": false, //$("#cbIsClearance").prop("checked"),
                        "pIsGenset": false, //$("#cbIsGenset").prop("checked"),
                        "pIsCourrier": false, //$("#cbIsCourrier").prop("checked"),
                        "pIsTelexRelease": false, //$("#cbIsTelexRelease").prop("checked"),
                        "pCustomerReference": "0",
                        "pSupplierReference": "0",
                        "pPONumber": "0",
                        "pAgreedRate": "0",
                        "pOperationStageID": 60, //this means Order
                        "pNumberOfHousesConnected": 0,
                        "pExpectedDeparture": "01/01/1900",
                        "pExpectedArrival": "01/01/1900",
                        "pVoyageOrTruckNumber": "0",
                        "pVesselID": "0",
                        "pContainerTypeID": "0",
                        "pNumberOfContainers": "0",

                        "pContainerTypeID2": "0",
                        "pNumberOfContainers2": "0",

                        "pContainerTypeID3": "0",
                        "pNumberOfContainers3": "0",
                        //"pIsInactive": $("#cbIsInactive").prop('checked'),
                        /***************************Venus Fields: A.Medra****************************/
                        "pBLDate": "01/01/1900",
                        "pMAWBStockID": 0,
                        "pTypeOfStockID": 0,
                        "pMAWBSuffix": "0",
                        "pFlightNo": "0",
                        "pIsAWB": false,
                        "pConsigneeID2": 0,
                        "pReleaseDate": "01/01/1900",
                        "pCertificateNumber": 0,
                        "pCountryOfOrigin": 0,
                        "pInvoiceValue": 0,
                        "pCurrencyID": 83,//pIsShipment ? ($("#slClearanceCurrency").val() == "" ? 0 : $("#slClearanceCurrency").val()) : "0",
                        "pACIDNumber": 0,
                        "pACIDNumberDetails": "0",
                        "pBookingNumber": "0",
                        "pUNNumber": 0,
                        "pIMOClass": 0,
                        "pVesselID": 0
                    };
                    PostInsertUpdateFunction("form", "/api/Operations/Insert", parameters, false, "OperationModal", function (pData) {
                        if (pData[3] != '')
                            swal('Sorry', pData[3]);
                        else {
                            //$('#hShipmentID').val(pData[1]);
                            //$('#txtShipmentHouseNumber').val(pData[2]);
                            //Operations_LoadingWithPaging();
                            //Shipments_ClearAllControls();
                            //swal('Success', 'Saved Successfully.');


                            // then connect selected houses
                            let MasterOperationID = pData[1];
                            var pParametersWithValues = {
                                pMasterOperationID: MasterOperationID
                                , pIsHouseConnected: true
                                , pMasterOperationIDFieldInHouse: MasterOperationID
                                , pHouseOperationsIDs: GetAllSelectedIDsAsString('tblOperations')
                                , pHouseParentID: 0
                            };

                            CallGETFunctionWithParameters("/api/Operations/ConnectOrDisconnectMultiple", pParametersWithValues
                                , function (DataArray) {
                                    var IsError = false;
                                    var ErrorMessage = "";
                                    DataArray.forEach((pData, index) => {
                                        let pReturnedMessage = pData[1];
                                        if (pReturnedMessage != "") {
                                            IsError = true;
                                            ErrorMessage = pReturnedMessage;
                                        }
                                    });
                                    if (!IsError) {
                                        swal("Success");
                                        //Operations_LoadingWithPaging();
                                        // then go to operations edit screen
                                        SwitchToOperationsEditView(MasterOperationID);
                                    }
                                    else {
                                        swal("Sorry", ErrorMessage);
                                        FadePageCover(false);
                                    }
                                }
                                , null);
                        }
                    });

                }
                , null);










        }


    }

}
function SwitchToOperationsEditView(pID) {
    debugger;
    LoadViews("OperationsEdit", null, pID);
    LoadOperationsSubMenu($("#cbIsAWB" + pID).prop("checked"));
}
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////CargoProperties Fns////////////////////////////////////////////////////////////
//pOption 10:Master, 22:House
function CargoProperties_FillControls(pOption) {
    debugger;
    var pOperationCargoID = 0;
    if (pOption == 10)
        pOperationCargoID = $("#hOperationID").val();
    else if (pOption == 20)
        pOperationCargoID = $("#hShipmentID").val();
    if (pOperationCargoID == "")
        swal("Sorry", "Please, save house first.");
    else {
        ClearAll("#CargoPropertiesModal");
        FadePageCover(true);
        jQuery("#CargoPropertiesModal").modal("show");
        var pParametersWithValues = {
            pOperationIDForCargo: pOperationCargoID
            , pMasterOperationID: $("#hOperationID").val() //to always containrs on master
            , pOrderBy: "ID"
        };
        CallGETFunctionWithParameters("/api/Operations/LoadCargoProperties"
            , pParametersWithValues
            , function (pData) {
                $("#btn-SaveCargoProperties").attr("onclick", "CargoProperties_Save(" + pOperationCargoID + ");")
                var pOperationHeader = JSON.parse(pData[0]);
                var pOperationContainersAndPackages = pData[1];
                var pPackageType = pData[2];
                FillListFromObject(pOperationHeader.PackageTypeID, 2, "<--Select-->", "slCargoPackageTypes", pPackageType, null);
                FillListFromObject(pOperationHeader.PlacedOnOperationContainersAndPackagesID, 11, "<--Select-->", "slCargoPlacedOnContainer", pOperationContainersAndPackages, null);
                $("#txtCargoTareWeight").val(pOperationHeader.TareWeight);
                $("#txtCargoVolume").val(pOperationHeader.Volume);
                $("#txtCargoNetWeight").val(pOperationHeader.NetWeight);
                $("#txtCargoNetWeightTON").val(pOperationHeader.NetWeightTON);
                $("#txtCargoGrossWeight").val(pOperationHeader.GrossWeight);
                $("#txtCargoGrossWeightTON").val(pOperationHeader.GrossWeightTON);
                $("#txtCargoVGM").val(pOperationHeader.VGM);
                $("#txtCargoChargeableWeight").val(pOperationHeader.ChargeableWeight);
                $("#txtCargoVolumetricWeight").val(pOperationHeader.VolumetricWeight);
                $("#divCargoMarksAndNumbers").val(pOperationHeader.MarksAndNumbers == 0 ? "" : pOperationHeader.MarksAndNumbers);
                $("#divCargoGoodsDescription").val(pOperationHeader.Description == 0 ? "" : pOperationHeader.Description);
                $("#txtCargoNumberOfPackages").val(pOperationHeader.NumberOfPackages);
                FadePageCover(false);
            }
            , null);
    }
}
function CargoProperties_Save(pOperationCargoID) {
    debugger;
    FadePageCover(true);
    var pParametersWithValues = {
        pOperationID: pOperationCargoID
        , pTareWeight: $("#txtCargoTareWeight").val() == "" ? 0 : $("#txtCargoTareWeight").val()
        , pGrossWeight: $("#txtCargoGrossWeight").val() == "" ? 0 : $("#txtCargoGrossWeight").val()
        , pGrossWeightTON: $("#txtCargoGrossWeightTON").val() == "" ? 0 : $("#txtCargoGrossWeightTON").val()
        , pVGM: $("#txtCargoVGM").val() == "" ? 0 : $("#txtCargoVGM").val()
        , pNetWeight: $("#txtCargoNetWeight").val() == "" ? 0 : $("#txtCargoNetWeight").val()
        , pNetWeightTON: $("#txtCargoNetWeightTON").val() == "" ? 0 : $("#txtCargoNetWeightTON").val()
        , pVolume: $("#txtCargoVolume").val() == "" ? 0 : $("#txtCargoVolume").val()
        , pChargeableWeight: $("#txtCargoChargeableWeight").val() == "" ? 0 : $("#txtCargoChargeableWeight").val()
        , pVolumetricWeight: $("#txtCargoVolumetricWeight").val() == "" ? 0 : $("#txtCargoVolumetricWeight").val()
        , pNumberOfPackages: $("#txtCargoNumberOfPackages").val() == "" ? 0 : $("#txtCargoNumberOfPackages").val()
        , pPackageTypeID: $("#slCargoPackageTypes").val() == "" ? 0 : $("#slCargoPackageTypes").val()
        , pPlacedOnOperationContainersAndPackagesID: $("#slCargoPlacedOnContainer").val() == "" ? 0 : $("#slCargoPlacedOnContainer").val()
        , pMarksAndNumbers: $("#divCargoMarksAndNumbers").val().trim() == "" ? 0 : $("#divCargoMarksAndNumbers").val().trim().toUpperCase()
        , pDescription: $("#divCargoGoodsDescription").val().trim() == "" ? 0 : $("#divCargoGoodsDescription").val().trim().toUpperCase()
    };
    CallGETFunctionWithParameters("/api/OperationContainersAndPackages/CargoProperties_Save"
        , pParametersWithValues
        , function (pData) {
            var _MessageReturned = pData[0];
            if (_MessageReturned == "") {
                swal("Success", "Saved successfully");
                var pOperationMasterOrDirect = JSON.parse(pData[2]);
                jQuery("#CargoPropertiesModal").modal("hide");
                //if (parseFloat($("#txtCargoChargeableWeight").val()) != 0 && $("#txtCargoChargeableWeight").val() != "")
                //    $("#lblChargeableWeight").html(": " + parseFloat($("#txtCargoChargeableWeight").val()).toFixed(2));
                //else
                //    OperationContainersAndPackages_CalculateSummary();
                OperationContainersAndPackages_FillLabels(pOperationMasterOrDirect);
            }
            else
                swal("Sorry", _MessageReturned);
            FadePageCover(false);
        }
        , null);
}
function CargoProperties_CalculateVGM() {
    debugger;
    var pTareWeight = isNaN(parseFloat($("#txtCargoTareWeight").val())) ? 0 : parseFloat($("#txtCargoTareWeight").val());
    var pGrossWeight = isNaN(parseFloat($("#txtCargoGrossWeight").val())) ? 0 : parseFloat($("#txtCargoGrossWeight").val());
    $("#txtCargoVGM").val(pTareWeight + pGrossWeight);
}
function OperationContainersAndPackages_CalculateVGM(pTareWeightControl, pGrossWeightControl, pVGMControl) {
    debugger;
    var pTareWeight = isNaN(parseFloat($("#" + pTareWeightControl).val())) ? 0 : parseFloat($("#" + pTareWeightControl).val());
    var pGrossWeight = isNaN(parseFloat($("#" + pGrossWeightControl).val())) ? 0 : parseFloat($("#" + pGrossWeightControl).val());
    $("#" + pVGMControl).val(pTareWeight + pGrossWeight);
}
function OperationContainersAndPackages_FillLabels(pOperationMasterOrDirect) {
    debugger;
    $("#lblTotalGrossWeight").html(": " + pOperationMasterOrDirect.GrossWeightSum);
    $("#lblTotalVolume").html(": " + pOperationMasterOrDirect.VolumeSum);
    $("#lblChargeableWeight").html(": " + pOperationMasterOrDirect.ChargeableWeightSum);
    $("#lblTotalNumberOfPackages").html(": " + pOperationMasterOrDirect.NumberOfPackages);
}
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////ContainerPackages Fns////////////////////////////////////////////////////////////



function Operations_FillPrintDocsOutModal(pOperationID) {
    debugger;
    $("#slDocsOutOperationsFromSearch").html("");
    FadePageCover(true);
    var tr = $("#tblOperations tbody tr[id=" + pOperationID + "]");
    if (tr.find("td.DirectionType").text() == constImportDirectionType)
        $("#cbIsImport").prop("checked", true);
    else if (tr.find("td.DirectionType").text() == constExportDirectionType)
        $("#cbIsExport").prop("checked", true);
    else if (tr.find("td.DirectionType").text() == constDomesticDirectionType)
        $("#cbIsDomestic").prop("checked", true);

    if (tr.find("td.TransportType").text() == OceanTransportType)
        $("#cbIsOcean").prop("checked", true);
    else if (tr.find("td.TransportType").text() == AirTransportType)
        $("#cbIsAir").prop("checked", true);
    else if (tr.find("td.TransportType").text() == InlandTransportType)
        $("#cbIsInland").prop("checked", true);
    jQuery("#SelectOperationAndDocumentModal").modal("show");
    var pParametersWithValues = {
        pOperationIDToPrintDocsOut: pOperationID
    };
    $("#hOperationID").val(pOperationID);
    DocsOut_LoadAll(pOperationID, "slDocsOutTypes");
    GetListWithOpCodeAndHouseNoAndClientEmailAttr(pOperationID, "/api/Operations/LoadWithParameters", null, "slDocsOutOperationsFromSearch"
        , { pPageNumber: 1, pPageSize: 99999, pWhereClause: " WHERE ID = " + pOperationID + " OR MasterOperationID = " + pOperationID, pOrderBy: "HouseNumber" }
        , function () { FadePageCover(false); });
}
