
function RunnerTransactions_Initialize() {
    $('#DomesticAWBModal input[type="text"]').on('keypress', function (e) {
        if (e.keyCode === 13) {
            e.preventDefault();
        }
    });

    strBindTableRowsFunctionName = "DomesticAWB_BindTableRows";
    strLoadWithPagingFunctionName = "/api/Domestic_AWB/RunnerTransactionLoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned";

    var pWhereClause = " WHERE 1=1 ";
    var pOrderBy = "ID DESC";
    var pPageNumber = 1;
    var pPageSize = 10;
    var pControllerParameters = { pIsLoadArrayOfObjects: true, pLanguage: $("[id$='hf_ChangeLanguage']").val(), pPageNumber: pPageNumber, pPageSize: pPageSize, pWhereClause: pWhereClause, pOrderBy: pOrderBy, pTrackingStageID:0 }
    LoadView("/CourierAndLastMile/Orders/RunnerTransactions", "div-content", function () {

        LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned("div-Pager", "select-page-size", "spn-first-page-row", "spn-last-page-row", "spn-total-count", "div-Text-Total", strLoadWithPagingFunctionName, pWhereClause, pOrderBy, pPageNumber, pPageSize, pControllerParameters
            , function (pData) {
                debugger;
                var pCities = pData[2];
                var pRegions = pData[3];
                var TrackingStage = pData[4];
                var WH_MainWarehouses = pData[5];
                //var pRates = pData[7];

                FillListFromObject(null, 2, "<--Select-->", "slCityFromShipper", pCities, null);
                FillListFromObject(null, 2, "<--Select-->", "slCityToConsignee", pCities, null);
                FillListFromObject(null, 2, "<--Select-->", "slRegionFromShipper", pRegions, null);
                FillListFromObject(null, 2, "<--Select-->", "slRegionToConsignee", pRegions, null);
                FillListFromObject(null, 2, "<--Select Tracking Stage-->", "slTrackingStageID", TrackingStage, null);
                FillListFromObject(null, 2, "<--Select Store-->", "slStore", WH_MainWarehouses, null);
                //FillListFromObject(null, 2, "<--Select Rate-->", "slRatesToCustomer", pRates, null);
                $('#RunnerAWBsTransactionsslTrackingStageID').html($('#slTrackingStageID').html());
                FillListFromObject(null, 2, "<--Select-->", "slRegionBase", pRegions, null);
                FillListFromObject(null, 2, "<--Select-->", "slCityBase", pCities, null);
                $('#slCityFromShipper_New').html($('#slCityBase').html());
                $('#slRegionFromShipper_New').html($('#slRegionBase').html());

                //DomesticAWB_BindTableRows(JSON.parse(pData[0]));
                var pFormattedTodaysDate = getTodaysDateInddMMyyyyFormat();

                $("#txtSearchFrom").val("01/01/2000");
                $("#txtSearchTo").val(pFormattedTodaysDate);
            });
        if ($("[id$='hf_ChangeLanguage']").val() == "ar") $(".swapChildrenClass:not(.reversed)").reverseChildren();
    },
        function () { DomesticAWB_ClearAllControls(); },
        function () { DomesticAWB_DeleteList('F'); }); //the parameter 'F':NotPermanentDelete, 'D':PermanentDelete
}

function ShowRunnerAWBsModal()
{
    debugger;
    jQuery("#RunnerAWBsTransactionsModal").modal("show");
}

function RunnerTransactionDomesticAWB_LoadingWithPaging()
{
    debugger;
    var pWhereClause = RunnerTransactionDomesticAWB_GetWhereClause();
    var pPageSize = $('#select-page-size').val();
    var pPageNumber = ($("#div-Pager li.active a").text() == "" ? 1 : $("#div-Pager li.active a").text());
    var pOrderBy = "SecondTime";
    var pControllerParameters = { pIsLoadArrayOfObjects: false, pLanguage: $("[id$='hf_ChangeLanguage']").val(), pPageNumber: pPageNumber, pPageSize: pPageSize, pWhereClause: pWhereClause, pOrderBy: pOrderBy,pTrackingStageID:$('#slTrackingStageID').val() }
    LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned("div-Pager", "select-page-size", "spn-first-page-row", "spn-last-page-row", "spn-total-count", "div-Text-Total", strLoadWithPagingFunctionName, pWhereClause, pOrderBy, pPageNumber, pPageSize, pControllerParameters,
        function (pData) {
           // RunnerTransactionDomesticAWB_BindTableRows(JSON.parse(pData[0]));
            TrackingStages_RunnerTransactionDomesticAWB_BindTableRows(pData);
        });
}

function ReloadAWBsModal() {
    debugger;
    var pWhereClause = RunnerTransactionDomesticAWB_GetWhereClause();
  //LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned("div-Pager", "select-page-size", "spn-first-page-row", "spn-last-page-row", "spn-total-count", "div-Text-Total", "/api/Domestic_AWB/ReloadAWBsTrackingStages", pWhereClause, pOrderBy, pPageNumber, pPageSize, pControllerParameters,
  //      function (pData) {
  //          TrackingStage_AWBs_BindTableRows(pData);
  //      });

    FadePageCover(true);
    CallGETFunctionWithParameters("/api/Domestic_AWB/ReloadAWBsTrackingStages"
        , { pWhereClause: pWhereClause }
        , function (pData) {
            debugger;
            TrackingStage_AWBs_BindTableRows(pData);
            FadePageCover(false);
        }, null);

}
function RunnerTransactionDomesticAWB_GetWhereClause() {
    debugger;
    var pWhereClause = "WHERE 1=1 ";
    if ($("#txt-Search").val().trim() != "") {
        pWhereClause += " AND (";
        pWhereClause += " AWBNumber like N'%" + $("#txt-Search").val().trim() + "%' ";
        pWhereClause += " OR ShipperName like N'%" + $("#txt-Search").val().trim() + "%' ";
        pWhereClause += " OR ConsigneeName like N'%" + $("#txt-Search").val().trim() + "%' "; 
        pWhereClause += " OR ShipperAccountNo like N'%" + $("#txt-Search").val().trim() + "%' ";
        pWhereClause += ")";
      
    }
    if ($('#slTrackingStageID').val() != "" && $('#slTrackingStageID').val() != 0) {
        pWhereClause += " AND TrackingStageID = " + $("#slTrackingStageID").val() + "";
    }

    return pWhereClause;
}
function TrackingStages_RunnerTransactionDomesticAWB_BindTableRows(pData)
{
    if((pData[1]) > 0)
    {
        ClearAllTableRows("tblTrackingStages_RunnerTransaction_DomesticAWB");
        //$.each(pDomesticAWB, function (i, item) {
        AppendRowtoTable("tblTrackingStages_RunnerTransaction_DomesticAWB",//ID, AWBNumber, ShipperID, ConsigneeID, ActWgt, DimWgt, ChgWgt, Pcs, CODAmount, Description, Remarks
                ("<tr ID='" + pData[2] + "' ondblclick='RunnerTransaction_DomesticAWBs_FillControls(\"" + pData[2] + "\"," + (JSON.parse(pData[0])[0].TrackingStageOrder) + ");'>"
                        + "<td class='AWBIDs hide'> " + pData[2] + "</td>"
                        + "<td class='TrackingStageName'>" + ($('#slTrackingStageID option:selected').text()) + "</td>"
                        + "<td class='TrackingStageID hide'>" + ($('#slTrackingStageID').val()) + "</td>"
                        + "<td class='TrackingStageOrder hide'>" + (JSON.parse(pData[0])[0].TrackingStageOrder) + "</td>"
                        + "<td class='NumOfAWBs'>" + (pData[1]) + "</td></tr>"));
        //});
        ApplyPermissions();
        BindAllCheckboxonTable("tblTrackingStages_RunnerTransaction_DomesticAWB", "ID", "cb-CheckAll");//Parameters(pTableName, pCheckBoxColumnClassName, pCheckBoxHeaderID)
        CheckAllCheckbox("ID");
        //HighlightText("#tblTrackingStages_RunnerTransaction_DomesticAWB>tbody>tr", $("#txt-Search").val().trim());
        if (showDeleteFailMessage) {//sherif: if deletion failed due to foreign key, then this message is shown
            swal(strSorry, strDeleteFailMessage);
            showDeleteFailMessage = false;
            strDeleteFailMessage = "This command is not completed because of dependencies existance."
        }
        $("#hl-homepage").on("click", function () { LoadViews("hl-homepage"); });
    }
    else
        ClearAllTableRows("tblTrackingStages_RunnerTransaction_DomesticAWB");
}

function TrackingStage_AWBs_BindTableRows(pData) {
    debugger;
        ClearAllTableRows("tblTrackingStages_RunnerTransaction_DomesticAWB");
        $.each(JSON.parse(pData[3]), function (i, item) {
        AppendRowtoTable("tblTrackingStages_RunnerTransaction_DomesticAWB",//ID, AWBNumber, ShipperID, ConsigneeID, ActWgt, DimWgt, ChgWgt, Pcs, CODAmount, Description, Remarks
                ("<tr ID='" + item.AWBIDs + "' ondblclick='RunnerTransaction_DomesticAWBs_FillControls(\"" + item.AWBIDs + "\"," + (item.TrackingStageOrder) + ");'>"
                        + "<td class='AWBIDs hide'> " + item.AWBIDs + "</td>"
                        + "<td class='TrackingStageName'>" + (item.TrackingStageName) + "</td>"
                        + "<td class='TrackingStageID hide'>" + item.TrackingStageID + "</td>"
                        + "<td class='TrackingStageOrder hide'>" + (item.TrackingStageOrder) + "</td>"
                        + "<td class='NumOfAWBs'>" + (item.AWBsCount) + "</td></tr>"));
            //TrackingStageID, g.Key.TrackingStageName, g.Key.TrackingStageOrder, AWBsCount = g.Count() , AWBIDs 
        });
        //});
        //ApplyPermissions();
        BindAllCheckboxonTable("tblTrackingStages_RunnerTransaction_DomesticAWB", "ID", "cb-CheckAll");//Parameters(pTableName, pCheckBoxColumnClassName, pCheckBoxHeaderID)
        CheckAllCheckbox("ID");
        //HighlightText("#tblTrackingStages_RunnerTransaction_DomesticAWB>tbody>tr", $("#txt-Search").val().trim());
        if (showDeleteFailMessage) {//sherif: if deletion failed due to foreign key, then this message is shown
            swal(strSorry, strDeleteFailMessage);
            showDeleteFailMessage = false;
            strDeleteFailMessage = "This command is not completed because of dependencies existance."
        }
        $("#hl-homepage").on("click", function () { LoadViews("hl-homepage"); });
   
}

var counterRunnerTransactionDomesticAWB = 0;
function RunnerTransactionDomesticAWB_BindTableRows(pDomesticAWB, TrackingStageOrder) {
    debugger;
    counterRunnerTransactionDomesticAWB++;
    var copyControlsText = " class='btn btn-xs btn-rounded btn-info float-right' > <i class='fa fa-copy' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>Deliverd To Warehouse</span>";

    ClearAllTableRows("tblRunnerAWBsTransactions");
    $.each(pDomesticAWB, function (i, item) {
        counterRunnerTransactionDomesticAWB++;
        AppendRowtoTable("tblRunnerAWBsTransactions",//ID, AWBNumber, ShipperID, ConsigneeID, ActWgt, DimWgt, ChgWgt, Pcs, CODAmount, Description, Remarks
            ("<tr ID='" + item.ID + "' RunnerTransactionDomesticAWB=" + counterRunnerTransactionDomesticAWB + " >"//ondblclick='DomesticAWB_FillControls(" + item.ID + ");'
                    + "<td class='ID'> <input " + " onchange='VerifyAWB(" + item.ID + "," + item.AWBNumber + ");' name='Delete'" + " type='checkbox' class='RowIDInApply'  value='" + item.ID + "' /></td>"
                    + "<td class='AWBNumber'>" + (item.AWBNumber == 0 ? "" : item.AWBNumber) + "</td>"
                    + "<td class='VerifyAWBNumber'><input disabled id='txtVerifyAWBNumber" + item.ID + "'  type='text'> </td>"
                    + "<td class='tdStockTrackingStage'> <select id='slTrackingStageID_Runner" + counterRunnerTransactionDomesticAWB + "' name='slTrackingStageID_Runner'  data-required='false' class='form-control m-b StockTrackingStage'></select></td>"

                    + "<td class='creationDate hide'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.creationDate)) < 1 ? "" : ConvertDateFormat(GetDateWithFormatMDY(item.creationDate))) + "</td>"
                    + "<td class='ShipperID hide' val='" + item.ShipperID + "'>" + (item.ShipperName == 0 ? "" : item.ShipperName) + "</td>"
                    + "<td class='ConsigneeID hide' val='" + item.ConsigneeID + "'>" + (item.ConsigneeName == 0 ? "" : item.ConsigneeName) + "</td>"
                    + "<td class='ActWgt hide'>" + (item.ActWgt == 0 ? "" : item.ActWgt) + "</td>"
                    + "<td class='DimWgt hide'>" + (item.DimWgt == 0 ? "" : item.DimWgt) + "</td>"
                    + "<td class='ChgWgt hide'>" + (item.ChgWgt == 0 ? "" : item.ChgWgt) + "</td>"
                    + "<td class='Pcs hide'>" + (item.Pcs == 0 ? "" : item.Pcs) + "</td>"
                    + "<td class='CODAmount hide'>" + (item.CODAmount == 0 ? "" : item.CODAmount) + "</td>"
                    + "<td class='Description hide'>" + (item.Description == 0 ? "" : item.Description) + "</td>"

                    + "<td class='ConsigneeName hide'>" + (item.ConsigneeName == 0 ? "" : item.ConsigneeName) + "</td>"
                    + "<td class='ConsigneeCityID hide'>" + (item.ConsigneeCityID == 0 ? "" : item.ConsigneeCityID) + "</td>"
                    + "<td class='ConsigneeRegionID hide'>" + (item.ConsigneeRegionID == 0 ? "" : item.ConsigneeRegionID) + "</td>"
                    + "<td class='ConsigneePhone1 hide'>" + (item.ConsigneePhone1 == 0 ? "" : item.ConsigneePhone1) + "</td>"
                    + "<td class='ConsigneePhone2 hide'>" + (item.ConsigneePhone2 == 0 ? "" : item.ConsigneePhone2) + "</td>"
                    + "<td class='ConsigneeCompanyName hide'>" + (item.ConsigneeCompanyName == 0 ? "" : item.ConsigneeCompanyName) + "</td>"
                    + "<td class='ConsigneeSenderName hide'>" + (item.ConsigneeSenderName == 0 ? "" : item.ConsigneeSenderName) + "</td>"
                    + "<td class='ConsigneeAccountNo hide'>" + (item.ConsigneeAccountNo == 0 ? "" : item.ConsigneeAccountNo) + "</td>"
                    + "<td class='ConsigneeCity hide'>" + (item.ConsigneeCity == 0 ? "" : item.ConsigneeCity) + "</td>"
                    + "<td class='ConsigneeAddress hide'>" + (item.ConsigneeAddress == 0 ? "" : item.ConsigneeAddress) + "</td>"
                    //ShipperName, ShipperCityID, ShipperRegionID, ShipperPhone1, ShipperPhone2, ShipperCompanyName, ShipperSenderName, ShipperAccountNo, ShipperCity, ShipperAddress

                    + "<td class='ShipperName hide'>" + (item.ShipperName == 0 ? "" : item.ShipperName) + "</td>"
                    + "<td class='ShipperCityID hide'>" + (item.ShipperCityID == 0 ? "" : item.ShipperCityID) + "</td>"
                    + "<td class='ShipperRegionID hide'>" + (item.ShipperRegionID == 0 ? "" : item.ShipperRegionID) + "</td>"
                    + "<td class='ShipperPhone1 hide'>" + (item.ShipperPhone1 == 0 ? "" : item.ShipperPhone1) + "</td>"
                    + "<td class='ShipperPhone2 hide'>" + (item.ShipperPhone2 == 0 ? "" : item.ShipperPhone2) + "</td>"
                    + "<td class='ShipperCompanyName hide'>" + (item.ShipperCompanyName == 0 ? "" : item.ShipperCompanyName) + "</td>"
                    + "<td class='ShipperSenderName hide'>" + (item.ShipperSenderName == 0 ? "" : item.ShipperSenderName) + "</td>"
                    + "<td class='ShipperAccountNo hide'>" + (item.ShipperAccountNo == 0 ? "" : item.ShipperAccountNo) + "</td>"
                    + "<td class='ShipperCity hide'>" + (item.ShipperCity == 0 ? "" : item.ShipperCity) + "</td>"
                    + "<td class='ShipperAddress hide'>" + (item.ShipperAddress == 0 ? "" : item.ShipperAddress) + "</td>"

                    + "<td class='PaymentTypeID hide'>" + (item.PaymentTypeID == 0 ? "" : item.PaymentTypeID) + "</td>"
                    + "<td class='StoreID hide'>" + (item.StoreID == 0 ? "" : item.StoreID) + "</td>"
                    + "<td class='StoreName hide'>" + (item.StoreName == 0 ? "" : item.StoreName) + "</td>"
                    + "<td class='PickupAddress'>" + (item.PickupAddress == 0 ? "" : item.PickupAddress) + "</td>"
                    + "<td class='EstimatedReceivedDate_Custody ViewOrder2' >" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.EstimatedReceivedDate_Custody)) < 1 ? "" : GetFullDateTime_DD_MM_YY_LastMile(item.EstimatedReceivedDate_Custody)) + "</td>"
                    
                    //+ "<td class='ActualReceivedDate_Custody '>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.ActualReceivedDate_Custody)) < 1 ? "" : GetFullDateTime_DD_MM_YY_LastMile(item.ActualReceivedDate_Custody)) + "</td>"
                    + '<td class="ActualReceivedDate_Custody ViewOrder2"><div class="input-group date datetimepicker-input" data-date-format="dd/mm/yyyy hh:ii" data-link-field="dtp_input1"><input id="txtActualReceivedDate_Custody' + counterRunnerTransactionDomesticAWB + '" onchange="" data-required="true" class="form-control StockActualReceivedDate_Custody" size="16" type="text" value=""><span class="input-group-addon"><span class="glyphicon glyphicon-th"></span></span></div></td>'
                    
                    + "<td class='EstimatedArrivalDateToStore ViewOrder3 " + (item.TrackingStageID == 6 ? "hide" : item.TrackingStageID) + "'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.EstimatedArrivalDateToStore)) < 1 ? "" : GetFullDateTime_DD_MM_YY_LastMile(item.EstimatedArrivalDateToStore)) + "</td>"
                    + '<td class="ActualArrivalDateToStore hide ViewOrder3 "><div class="input-group date datetimepicker-input" data-date-format="dd/mm/yyyy hh:ii" data-link-field="dtp_input1"><input id="txtActualArrivalDateToStore' + counterRunnerTransactionDomesticAWB + '" onchange="" data-required="true" class="form-control StockActualArrivalDateToStore" size="16" type="text" value=""><span class="input-group-addon"><span class="glyphicon glyphicon-th"></span></span></div></td>'
                    + "<td class='EstimatedDeliveryDateFrom hide ViewOrder6'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.EstimatedDeliveryDateFrom)) < 1 ? "" : GetFullDateTime_DD_MM_YY_LastMile(item.EstimatedDeliveryDateFrom)) + "</td>"
                    + "<td class='EstimatedDeliveryDateTo hide ViewOrder6 '>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.EstimatedDeliveryDateTo)) < 1 ? "" : GetFullDateTime_DD_MM_YY_LastMile(item.EstimatedDeliveryDateTo)) + "</td>"
                    + '<td class="ActualDeliveryDate hide ViewOrder6 "><div class="input-group date datetimepicker-input" data-date-format="dd/mm/yyyy hh:ii" data-link-field="dtp_input1"><input id="txtActualDeliveryDate' + counterRunnerTransactionDomesticAWB + '" onchange="" data-required="true" class="form-control StockActualDeliveryDate" size="16" type="text" value=""><span class="input-group-addon"><span class="glyphicon glyphicon-th"></span></span></div></td>'
                     + "<td class='RunnerFromShipperID hide'>" + (item.RunnerFromShipperID == 0 ? "" : item.RunnerFromShipperID) + "</td>"
                    + "<td class='RunnerFromShipperName hide'>" + (item.RunnerFromShipperName == 0 ? "" : item.RunnerFromShipperName) + "</td>"
                    + "<td class='RunnerToConsigneeID hide'>" + (item.RunnerToConsigneeID == 0 ? "" : item.RunnerToConsigneeID) + "</td>"
                    + "<td class='RunnerToConsigneeName hide'>" + (item.RunnerToConsigneeName == 0 ? "" : item.RunnerToConsigneeName) + "</td>"

                    //+ "<td class='Remarks '>" + (item.Remarks == 0 ? "" : item.Remarks) + "</td>"
                    + "<td class='Remarks '><textarea id='txtRemarks" + counterRunnerTransactionDomesticAWB + "' type='text' class='form-control parsley-validated input-sm StockRemarks' placeholder='Remarks'></textarea></td>"

                    + "<td class='TrackingStageID hide'>" + (item.TrackingStageID == 0 ? "" : item.TrackingStageID) + "</td>"
                    + "<td class='TrackingStageName hide'>" + (item.TrackingStageName == 0 ? "" : item.TrackingStageName) + "</td>"
                    + "<td class='TrackingStageOrder hide'>" + (item.TrackingStageOrder == 0 ? "" : item.TrackingStageOrder) + "</td>"

                     + "<td class='SaveAWB_Runner'> <button type='button' id='btnSaveAWB_Runner' class='btn btn-primary btn-sm' onclick='SaveAWB_Runner(" + item.ID + "," + counterRunnerTransactionDomesticAWB + "," + TrackingStageOrder + ");'>Save </button></td>"

                    + "<td class='hide'><a href='#DomesticAWBModal' data-toggle='modal'  " + editControlsText + "</a></td></tr>"));//onclick='DomesticAWB_FillControls(" + item.ID + ");'
                    ///////////////////////////////
        $("#slTrackingStageID_Runner" + counterRunnerTransactionDomesticAWB + "").html($('#slTrackingStageID').html());

                    var TrackingStageIDval = (item.TrackingStageID == 0 ? "" : item.TrackingStageID)
                    $("#slTrackingStageID_Runner" + counterRunnerTransactionDomesticAWB + "").val(TrackingStageIDval);

                    ///////////
                    if(Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.ActualReceivedDate_Custody)) > 0 )
                    {
                        $("#txtActualReceivedDate_Custody" + counterRunnerTransactionDomesticAWB + "").val(GetFullDateTime_DD_MM_YY_LastMile(item.ActualReceivedDate_Custody))
                    }
                    $("#txtActualReceivedDate_Custody" + counterRunnerTransactionDomesticAWB + "").datetimepicker({ dateFormat: "dd/mm/yyyy hh:ii" });
                    if ($("#txtActualReceivedDate_Custody" + counterRunnerTransactionDomesticAWB + "").val() != "")
                    {
                        $("#txtActualReceivedDate_Custody" + counterRunnerTransactionDomesticAWB + "").prop('disabled', true);
                    }
                    //////////////////////////////////////////////////

                    if (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.ActualArrivalDateToStore)) > 0)
                    {
                        $("#txtActualArrivalDateToStore" + counterRunnerTransactionDomesticAWB + "").val(GetFullDateTime_DD_MM_YY_LastMile(item.ActualArrivalDateToStore))
                    }
                    $("#txtActualArrivalDateToStore" + counterRunnerTransactionDomesticAWB + "").datetimepicker({ dateFormat: "dd/mm/yyyy hh:ii" });
                    if ($("#txtActualArrivalDateToStore" + counterRunnerTransactionDomesticAWB + "").val() != "")
                    {
                        $("#txtActualArrivalDateToStore" + counterRunnerTransactionDomesticAWB + "").prop('disabled', true);
                    }
                    /////////////////////////////////////////////////////////
                    if (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.ActualDeliveryDate)) > 0)
                    {
                        $("#txtActualDeliveryDate" + counterRunnerTransactionDomesticAWB + "").val(GetFullDateTime_DD_MM_YY_LastMile(item.ActualDeliveryDate))
                    }
                    $("#txtActualDeliveryDate" + counterRunnerTransactionDomesticAWB + "").datetimepicker({ dateFormat: "dd/mm/yyyy hh:ii" });
                    if ($("#txtActualDeliveryDate" + counterRunnerTransactionDomesticAWB + "").val() != "")
                    {
                        $("#txtActualDeliveryDate" + counterRunnerTransactionDomesticAWB + "").prop('disabled', true);
                    }
                    //////////////////////////////////////////////
        });

    $('.ViewOrder2').addClass('hide'); 
    $('.ViewOrder3').addClass('hide');
    $('.ViewOrder4').addClass('hide');
    $('.ViewOrder6').addClass('hide');
    
    $('.ViewOrder' + TrackingStageOrder + '').removeClass('hide');

    ApplyPermissions();
    BindAllCheckboxonTable("tblRunnerAWBsTransactions", "ID", "cb-CheckAll-Apply");//Parameters(pTableName, pCheckBoxColumnClassName, pCheckBoxHeaderID)
    CheckAllCheckbox("ID");
    //HighlightText("#tblRunnerAWBsTransactions>tbody>tr", $("#txt-Search").val().trim());
    if (showDeleteFailMessage) {//sherif: if deletion failed due to foreign key, then this message is shown
        swal(strSorry, strDeleteFailMessage);
        showDeleteFailMessage = false;
        strDeleteFailMessage = "This command is not completed because of dependencies existance."
    }
    $("#hl-homepage").on("click", function () { LoadViews("hl-homepage"); });
}

function VerifyAWB(pID, AWBNumber)
{
    debugger;
    $('#txtVerifyAWBNumber' + pID).val(AWBNumber);
}

function SaveReceivedAWBs()
{
    debugger;
    ReturnMMDDYYYFromDateTimeReplaceDash(datett);
}
function SaveAWB_Runner(ID, Counter, TrackingStageOrder)
{
    debugger;
    var tr = $("#tblRunnerAWBsTransactions tr[runnertransactiondomesticawb='" + Counter + "']");
    //$("#slCustomerShipperID").val($(tr).find("td.ShipperID").attr('val'));
    //$("#txtRemarks" + Counter + "").val(ReturnMMDDYYYFromDateTimeReplaceDash($(tr).find("td.ActualReceivedDate_Custody input").val() ));
    if ($('#RunnerAWBsTransactionsslTrackingStageID').val() > 0 || $('#RunnerAWBsTransactionsslTrackingStageID').val() != "")
    {
        var pParametersWithValues = {
            pID: ID
            , pRemarks: ($('#txtRemarks' + Counter + '').val() == "" ? "0" : $('#txtRemarks' + Counter + '').val())
            , pTrackingStageID: $('#RunnerAWBsTransactionsslTrackingStageID').val()
            , pUpdateTrackingStageIdOnly : 1
        };
        CallPOSTFunctionWithParameters("/api/Domestic_AWB/Runner_AWBTrackingStage_Save"
          , pParametersWithValues
          , function (pData)
          {
              debugger;
              var pMessageReturned = pData[1];
              if (pMessageReturned != true) {
                  swal("Sorry", pMessageReturned);
                  FadePageCover(false);
              }
              else {
                  swal("Success", "Saved successfully.");
              }
          }
          , null);
    }
    if (TrackingStageOrder == 2)
    {
        
        var pParametersWithValues = {
            pID: ID
             , pRemarks: ($('#txtRemarks' + Counter + '').val() == "" ? "0" : $('#txtRemarks' + Counter + '').val())
             , pActualReceivedDate_Custody:($('#txtActualReceivedDate_Custody' + Counter + '').val().indexOf("/") >= 0)? ConvertDateFormat($('#txtActualReceivedDate_Custody' + Counter + '').val() == "" ? "01/01/1900" : $('#txtActualReceivedDate_Custody' + Counter + '').val()) :  ReturnMMDDYYYFromDateTimeReplaceDash($('#txtActualReceivedDate_Custody' + Counter + '').val() == "" ? "01/01/1900" : $('#txtActualReceivedDate_Custody' + Counter + '').val())

        };
        CallPOSTFunctionWithParameters("/api/Domestic_AWB/RunnerWhenReceiveFromShipper_Save"
      , pParametersWithValues
      , function (pData) {
          debugger;
          var pMessageReturned = pData[1];
          if (pMessageReturned != true) {
              swal("Sorry", pMessageReturned);
              FadePageCover(false);
          }
          else {
              swal("Success", "Saved successfully.");
          }
      }
      , null);
    }
    else if (TrackingStageOrder == 3) {
        var pParametersWithValues = {
                pID: ID
             , pRemarks: ($('#txtRemarks' + Counter + '').val() == "" ? "0" : $('#txtRemarks' + Counter + '').val())
             , pActualArrivalDateToStore: ($('#txtActualArrivalDateToStore' + Counter + '').val().indexOf("/") >= 0) ? ConvertDateFormat($('#txtActualArrivalDateToStore' + Counter + '').val() == "" ? "01/01/1900" : $('#txtActualArrivalDateToStore' + Counter + '').val()) : ReturnMMDDYYYFromDateTimeReplaceDash($('#txtActualArrivalDateToStore' + Counter + '').val() == "" ? "01/01/1900" : $('#txtActualArrivalDateToStore' + Counter + '').val())
            //, pActualArrivalDateToStore: ReturnMMDDYYYFromDateTimeReplaceDash($('#txtActualArrivalDateToStore' + Counter + '').val() == "" ? "01/01/1900" : $('#txtActualArrivalDateToStore' + Counter + '').val())

        };
        CallPOSTFunctionWithParameters("/api/Domestic_AWB/RunnerActualArrivalDateToStore_Save"
      , pParametersWithValues
      , function (pData) {
          debugger;
          var pMessageReturned = pData[1];
          if (pMessageReturned != true) {
              swal("Sorry", pMessageReturned);
              FadePageCover(false);
          }
          else {
              swal("Success", "Saved successfully.");
          }
      }
      , null);
    }
    else if (TrackingStageOrder == 6) {
        var pParametersWithValues = {
            pID: ID
          , pRemarks: ($('#txtRemarks' + Counter + '').val() == "" ? "0" : $('#txtRemarks' + Counter + '').val())
          , pActualDeliveryDate: ($('#txtActualDeliveryDate' + Counter + '').val().indexOf("/") >= 0) ? ConvertDateFormat($('#txtActualDeliveryDate' + Counter + '').val() == "" ? "01/01/1900" : $('#txtActualDeliveryDate' + Counter + '').val()) : ReturnMMDDYYYFromDateTimeReplaceDash($('#txtActualDeliveryDate' + Counter + '').val() == "" ? "01/01/1900" : $('#txtActualDeliveryDate' + Counter + '').val())
          //, pActualDeliveryDate: ReturnMMDDYYYFromDateTimeReplaceDash($('#txtActualDeliveryDate' + Counter + '').val() == "" ? "01/01/1900" : $('#txtActualDeliveryDate' + Counter + '').val())

        };
        CallPOSTFunctionWithParameters("/api/Domestic_AWB/RunnerActualDeliveryDate_Save"
      , pParametersWithValues
      , function (pData) {
          debugger;
          var pMessageReturned = pData[1];
          if (pMessageReturned != true) {
              swal("Sorry", pMessageReturned);
              FadePageCover(false);
          }
          else {
              swal("Success", "Saved successfully.");
          }
      }
      , null);
    }

}
function ApplyBaseOnRows()
{
    debugger;
    //$('#txtBaseDate').val()
    var pAWBsIDs= GetAllSelectedIDsAsString('tblRunnerAWBsTransactions');
    
    $.each(pAWBsIDs.split(','), function (i, item) {
        //var $tr = $(this).parents("tr");
        //var tr = $("#tblRunnerAWBsTransactions tr[runnertransactiondomesticawb='" + Counter + "']");
        var tr = $("#tblRunnerAWBsTransactions tr[ID='" + item + "']");
        var RowNum = $(tr).attr('runnertransactiondomesticawb')
        if ($(tr).find("td.TrackingStageOrder").text() == 2)
            $('#txtActualReceivedDate_Custody' + RowNum + '').val($('#txtBaseDate').val())
        if ($(tr).find("td.TrackingStageOrder").text() == 3)
            $('#txtActualArrivalDateToStore' + RowNum + '').val($('#txtBaseDate').val())
        if ($(tr).find("td.TrackingStageOrder").text() == 6)
            $('#txtActualDeliveryDate' + RowNum + '').val($('#txtBaseDate').val())

        $('#slTrackingStageID_Runner' + RowNum + '').val($('#RunnerAWBsTransactionsslTrackingStageID').val())
        //$("#slRunnerFromShipper").val($(tr).find("td.RunnerFromShipperID").text());
    });
}
function RunnerTransaction_DomesticAWBs_FillControls(AWBIDs, TrackingStageOrder)
{
    debugger;
    FadePageCover(true);
    CallGETFunctionWithParameters("/api/Domestic_AWB/LoadRunnerTransaction_AWBsByIDs"
        ,{pWhereClause: " Where ID IN(" + AWBIDs+")"}
        , function (pData) {
            debugger;
            ClearAll('#RunnerAWBsTransactionsModal')
            jQuery("#RunnerAWBsTransactionsModal").modal("show");
            RunnerTransactionDomesticAWB_BindTableRows(JSON.parse(pData), TrackingStageOrder);
            FadePageCover(false);
        }, null);
}
function CheckAllRows_Apply() {
    debugger;
    //RowIDInApply
    if ($('#cb-CheckAll-Apply').prop('checked'))
        $('.RowIDInApply').prop('checked', true)
    else
        $('.RowIDInApply').prop('checked', false)
}
function SaveAllBaseOnRows_Runner() {
    debugger;
    if ($('#tblRunnerAWBsTransactions > tbody > tr').length > 0) {
        var st_AWBIDs = "";
        var st_TrackingStageIDs = "";
        var st_ActualArrivalDateToStore = "";
        var st_ActualReceivedDate_Custody = "";
        var st_ActualDeliveryDate = "";
        var st_Remarks = "";
        $($('#tblRunnerAWBsTransactions > tbody > tr')).each(function (i, tr) {
            debugger;
            if ($(tr).find('td.ID input[type="checkbox"]').prop('checked')) {
                st_AWBIDs += IsNull($(tr).find('td.ID input[type="checkbox"]').val(), "0") + ",";
                st_TrackingStageIDs += IsNull($(tr).find('td.tdStockTrackingStage').find('.StockTrackingStage').val(), "0") + ",";
                //st_ActualArrivalDateToStore += IsNull($(tr).find('td.ActualArrivalDateToStore').find('.StockActualArrivalDateToStore').val(), "0") + ",";
                //st_ActualReceivedDate_Custody += IsNull($(tr).find('td.ActualReceivedDate_Custody').find('.StockActualReceivedDate_Custody').val(), "0") + ",";
                //st_ActualDeliveryDate += IsNull( $(tr).find('td.ActualDeliveryDate').find('.StockActualDeliveryDate').val() , "0") + ",";
                st_Remarks += IsNull($(tr).find('td.Remarks').find('.StockRemarks').val(), "0") + "+";
                //StockActualReceivedDate_Custody StockActualArrivalDateToStore StockActualDeliveryDate

                st_ActualArrivalDateToStore += ($(tr).find('td.ActualArrivalDateToStore').find('.StockActualArrivalDateToStore').val().indexOf("/") >= 0) ? ConvertDateFormat($(tr).find('td.ActualArrivalDateToStore').find('.StockActualArrivalDateToStore').val() == "" ? "01/01/1900" : $(tr).find('td.ActualArrivalDateToStore').find('.StockActualArrivalDateToStore').val()) : ReturnMMDDYYYFromDateTimeReplaceDash($(tr).find('td.ActualArrivalDateToStore').find('.StockActualArrivalDateToStore').val() == "" ? "01/01/1900" : $(tr).find('td.ActualArrivalDateToStore').find('.StockActualArrivalDateToStore').val()) + ",";
                st_ActualReceivedDate_Custody += ($(tr).find('td.ActualReceivedDate_Custody').find('.StockActualReceivedDate_Custody').val().indexOf("/") >= 0) ? ConvertDateFormat($(tr).find('td.ActualReceivedDate_Custody').find('.StockActualReceivedDate_Custody').val() == "" ? "01/01/1900" : $(tr).find('td.ActualReceivedDate_Custody').find('.StockActualReceivedDate_Custody').val()) : ReturnMMDDYYYFromDateTimeReplaceDash($(tr).find('td.ActualReceivedDate_Custody').find('.StockActualReceivedDate_Custody').val() == "" ? "01/01/1900" : $(tr).find('td.ActualReceivedDate_Custody').find('.StockActualReceivedDate_Custody').val()) + ",";
                st_ActualDeliveryDate += ($(tr).find('td.ActualDeliveryDate').find('.StockActualDeliveryDate').val().indexOf("/") >= 0) ? ConvertDateFormat($(tr).find('td.ActualDeliveryDate').find('.StockActualDeliveryDate').val() == "" ? "01/01/1900" : $(tr).find('td.ActualDeliveryDate').find('.StockActualDeliveryDate').val()) : ReturnMMDDYYYFromDateTimeReplaceDash($(tr).find('td.ActualDeliveryDate').find('.StockActualDeliveryDate').val() == "" ? "01/01/1900" : $(tr).find('td.ActualDeliveryDate').find('.StockActualDeliveryDate').val()) + ",";

            }
        });

        if (st_AWBIDs.length > 0) {
            st_AWBIDs = st_AWBIDs.substring(0, st_AWBIDs.length - 1);
            st_TrackingStageIDs = st_TrackingStageIDs.substring(0, st_TrackingStageIDs.length - 1);
            st_ActualArrivalDateToStore = st_ActualArrivalDateToStore.substring(0, st_ActualArrivalDateToStore.length - 1);
            st_ActualReceivedDate_Custody = st_ActualReceivedDate_Custody.substring(0, st_ActualReceivedDate_Custody.length - 1);
            st_ActualDeliveryDate = st_ActualDeliveryDate.substring(0, st_ActualDeliveryDate.length - 1);
            st_Remarks = st_Remarks.substring(0, st_Remarks.length - 1);
        }
        debugger;
        CallGETFunctionWithParameters("/api/Domestic_AWB/SaveAllBaseOnRows_Runner"
      , {
          pst_AWBIDs: st_AWBIDs,
          pst_TrackingStageIDs: st_TrackingStageIDs,
          pActualReceivedDate_Custody: ConvertDateFormat($('#applytxtActualReceivedDate_Custody').val() == "" ? "01/01/1900" : $('#applytxtActualReceivedDate_Custody').val()),
          pActualArrivalDateToStore: ConvertDateFormat($('#applytxtActualArrivalDateToStore').val() == "" ? "01/01/1900" : $('#applytxtActualArrivalDateToStore').val()),
          pActualDeliveryDate: ConvertDateFormat($('#applytxtActualDeliveryDate').val() == "" ? "01/01/1900" : $('#applytxtActualDeliveryDate').val()),
          pst_Remarks: st_Remarks,

      }
      , function (pData) {
          if (pData[0] == "")
              swal("Success", "AWBs Saved Successfully.");
          else
              swal("Sorry", "There is Something error.");

      }, null);
    }

}

function PaymentDetailClass() {
    debugger;
    this.ID = 0;
    this.PaymentHeaderID = 0;
    this.PaymentTypeID = 0;
    this.DueDate = new Date;
    this.Amount = 0;
    this.ReferenceNumber = "";
    this.CheckBankID = 0;
    this.CurrencyID = 0;
    this.Remarks = "";
    this.CashSafeID = 0;

}