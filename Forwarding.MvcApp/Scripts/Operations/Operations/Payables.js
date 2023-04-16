﻿//$(pEntryDateControl).val(ConvertDateFormat(FormattedTodaysDate));
function Payables_SubmenuTabClicked() {
    debugger;
    if ($("#tblPayables tbody tr").length == 0 && $("#tblReceivables tbody tr").length == 0) {
        Payables_LoadWithPagingWithWhereClause($("#hOperationID").val()
            , function () {
                LoadWithPagingWithWhereClause("div-Pager", "select-page-size", "spn-first-page-row", "spn-last-page-row", "spn-total-count", "div-Text-Total", "/api/Receivables/LoadWithWhereClause", "WHERE IsDeleted=0 AND OperationID=" + $("#hOperationID").val(), 0, 1000, function (pTabelRows) { Receivables_BindTableRows(pTabelRows); }, false);
            });


        $("#txtPayableIssueDate").datepicker().on('changeDate'
            , function () {
                $(this).datepicker('hide');
                RecalculatePayableExchangeRate();


            });
        $("#txtPayableIssueDate").datepicker().on('keydown', function (ev) { if (ev.keyCode == 9) $(this).datepicker('hide'); });


    }
    if (pDefaults.UnEditableCompanyName == "GBL") {
        $("#h6ChargesHeader_Quotation").html($("#lblDirection").parent().html());
        $("#h6ChargesHeader_Quotation").removeClass("hide");
    }
    if (pDefaults.UnEditableCompanyName == "MESCO") {
        $(".classShowForMESCO").removeClass("hide");
    }
}

function RecalculatePayableExchangeRate() {
    var currencyid = parseInt( IsNull(  $('#slPayableCurrency').val() , "0") );

    $.ajax({
        type: "GET",
        url: strServerURL + "/api/SL_Invoices/GetCurrencyExchangeRateWithDate",
        data: { pDate: ConvertDateFormat($('#txtPayableIssueDate').val()), pCurrencyID: currencyid },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (d) {
            $("#txtPayableExchangeRate").val(d);
            FadePageCover(false);
        },
        error: function (jqXHR, exception) {
            debugger;
            swal("Oops!", "Please, contact your technical support!  this is print region !", "error");
            FadePageCover(false);
        }
    });

}


function Payables_BindTableRows(pPayables) {
    if ($("#tblDocsOut tbody tr").length == 0) {
        DocsOut_LoadAll(null, null, false);
    }
    debugger;
    var varAWBSuffix = "";
    var OtherChargesDueCarrier = 0;
    var TaxCode_SH = 0;
    if ($("#cbIsAWB").prop("checked")) {
        varAWBSuffix = "AWB";
    }
    ClearAllTableRows("tblPayables" + varAWBSuffix);
    var editControlsText = " class='btn btn-xs btn-rounded btn-info float-right' > <i class='fa fa-pencil' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + TranslateString("Edit") + "</span>";
    var copyControlsText = " class='btn btn-xs btn-rounded btn-info float-right' > <i class='fa fa-copy' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + TranslateString("Copy") + "</span>";
    $.each(pPayables, function (i, item) {
        debugger;
        AppendRowtoTable("tblPayables" + varAWBSuffix,
            ("<tr ID='" + item.ID + "' " + (OEPay && $("#hIsOperationDisabled").val() == false /*&& !item.IsApproved*/ && item.AccNoteID == 0 && (item.InterServiceProviderDepartmentID == 0 || (item.InterServiceProviderDepartmentID > 0 && item.CreatorUserID != pLoggedUser.ID)) ? (" ondblclick='Payables_EditByDblClick(" + item.ID + ");'") : (" ondblclick=''"))//editale when logged user is the determinator of price
                        + " class='"
                            + (item.IsApproved ? " text-primary " : "")
                            + (pDefaults.UnEditableCompanyName == "GBL" && (item.TruckingOrderID != 0 && item.IsOwnedByCompany) && item.CostAmount == 0 && (item.SupplierInvoiceNo == 0 || item.SupplierInvoiceNo == "N/A") ? " hide " : "")
                        + " '>"
                + "<td class='PayableID'> <input " + (/*item.SupplierInvoiceNo == 0 &&*/ !item.IsApproved && item.AccNoteID == 0 && item.OperationVehicleID == 0 ? "name='Delete'" : "disabled='disabled'") + " type='checkbox' value='" + item.ID + "'></td>"
                //+ "<td class='Payable' val='" + item.ChargeTypeID + "'>" + item.ChargeTypeCode + " (" + item.ChargeTypeName + ")" + "</td>"
                + "<td class='Payable' val='" + item.ChargeTypeID + "'>" + item.ChargeTypeName + ((pDefaults.IsRepeatChargeTypeName ? "(" + item.ChargeTypeCode + ")" : "")) + "</td>"
                + "<td class='PayablePOrC hide' val='" + item.POrC + "'>" + (item.POrC == 0 ? "" : item.POrCCode) + "</td>"
                //the next line its PartnerSupplierID comes from table OperationPartners
                + "<td class='PayableSupplier " + ($("#cbIsAWB").prop("checked") ? " hide " : "") + "' val='" + item.SupplierOperationPartnerID + "'>" + (item.PartnerSupplierID == 0 ? "" : item.PartnerSupplierName) + "</td>"
                + "<td class='SupplierSiteID hide'>" + item.SupplierSiteID + "</td>"
                + "<td class='TruckingOrderID hide'>" + item.TruckingOrderID + "</td>"
                + "<td class='PayableQuotationCost " + (1 == 1 ? "" : " hide ") + "'>" + item.QuotationCost.toFixed(2) + "</td>" //TotalAmount
                + "<td class='ChargeTypeCost hide'>" + item.ChargeTypeCost.toFixed(2) + "</td>" //TotalAmount
                //+ ($("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') ? ("<td class='PayableUOM' val='" + item.ContainerTypeID + "'>" + (item.ContainerTypeCode == 0 ? "" : item.ContainerTypeCode) + "</td>") : "")
                //+ ($("#cbIsLCL").prop('checked') || $("#cbIsLTL").prop('checked') || $("#cbIsAir").prop('checked') ? ("<td class='PayableUOM' val='" + item.PackageTypeID + "'>" + (item.PackageTypeName == 0 ? "" : item.PackageTypeName) + "</td>") : "")
                + "<td class='PayableUOM hide' val='" + item.MeasurementID + "'>" + (item.MeasurementID == 0 ? "" : item.MeasurementCode) + "</td>"
                + "<td class='PayableQuantity " + ($("#cbIsAWB").prop("checked") ? " hide " : "") + "'>" + item.Quantity + "</td>"
                + "<td class='PayableCostPrice'>" + (item.InterServiceProviderDepartmentID > 0 && item.InterServiceProviderDepartmentID != pLoggedUser.DepartmentID? item.InterTransitionalPrice.toFixed(2): item.CostPrice.toFixed(2)) + "</td>"
               
            + "<td class='IsApproved hide'>" + item.IsApproved+ "</td>"
            + "<td class='AccNoteID hide'>" + item.AccNoteID + "</td>"
                + "<td class='PayableAmountWithoutVAT hide'>" + item.AmountWithoutVAT + "</td>"
                + "<td class='PayableTaxTypeID hide' val=" + item.TaxTypeID + ">" + (item.TaxTypeID == 0 ? "" : item.TaxTypeName) + "</td>"
                + "<td class='PayableTaxPercentage hide'>" + item.TaxPercentage + "</td>"
                + "<td class='PayableTaxAmount hide'>" + item.TaxAmount + "</td>"
                + "<td class='PayableDiscountTypeID hide' val=" + item.DiscountTypeID + ">" + (item.DiscountTypeID == 0 ? "" : item.DiscountTypeName) + "</td>"
                + "<td class='PayableDiscountPercentage hide'>" + item.DiscountPercentage + "</td>"
                + "<td class='PayableDiscountAmount hide'>" + item.DiscountAmount + "</td>"

                + "<td class='PayableCostAmount " + ((item.GeneratingQRID != 0 && item.QuotationCost.toFixed(2) != item.AmountWithoutVAT.toFixed(2)) ? "static-text-danger" : "") + ($("#cbIsAWB").prop("checked") ? " hide " : "") + "'>" + (item.InterServiceProviderDepartmentID > 0 && item.InterServiceProviderDepartmentID != pLoggedUser.DepartmentID? item.InterTransitionalPrice.toFixed(2): item.CostAmount.toFixed(2)) + "</td>" //TotalAmount
                 + "<td class='InterTransitionalPrice " + (pDefaults.UnEditableCompanyName == "MESCO"? "" : " hide ") + "'>" + (item.InterTransitionalPrice == 0 || pLoggedUser.DepartmentID != item.InterServiceProviderDepartmentID ? "" : item.InterTransitionalPrice.toFixed(2)) + "</td>"
                + "<td class='PayableInitialSalePrice " + ($("#cbIsAWB").prop("checked") ? " hide " : "") + "'>" + (item.InitialSalePrice == 0 ? "" : item.InitialSalePrice.toFixed(2)) + "</td>"
               + "<td class='PayableSupplierInvoiceNo " + ($("#cbIsAWB").prop("checked") ? " hide " : "") + "'>" + (item.SupplierInvoiceNo == 0 ? "" : item.SupplierInvoiceNo) + "</td>"
                + "<td class='PayableSupplierReceiptNo " + ($("#cbIsAWB").prop("checked") ? " hide " : "") + "'>" + (item.SupplierReceiptNo == 0 ? "" : item.SupplierReceiptNo) + "</td>"
                + "<td class='PayableBillID hide'>" + item.BillID + "</td>"

                + "<td class='PayableChargeTypeGroupID hide'>" + item.ChargeTypeGroupID + "</td>"
                + "<td class='PayableChargeTypeGroupName hide'>" + item.ChargeTypeGroupName + "</td>"

                + "<td val='" + item.PartnerSupplierBankName+"' class='PartnerSupplierBankName hide'>" + item.PartnerSupplierBankName + "</td>"
                + "<td val='" + item.PartnerSupplierBankAccountNumber + "' class='PartnerSupplierBankAccountNumber hide'>" + item.PartnerSupplierBankAccountNumber + "</td>"

                + "<td class='PayableExchangeRate hide'>" + item.ExchangeRate.toFixed(2) + "</td>"
                + "<td class='PayableCurrency " + ($("#cbIsAWB").prop("checked") ? " hide " : "") + "' val='" + item.CurrencyID + "'>" + (item.CurrencyID == 0 ? "" : item.CurrencyCode) + "</td>"
                + "<td class='PayablePaidAmount " + ($("#cbIsAWB").prop("checked") ? " hide " : "") + "'>" + item.PaidAmount + "</td>"
                + "<td class='PayableCustodyID hide' val=" + item.CustodyID + ">" + (item.CustodyID == 0 ? "" : item.CustodyName) + "</td>"
                + "<td class='PayableAccNote " + ($("#cbIsAWB").prop("checked") ? " hide " : "") + "' val='" + item.AccNoteID + "'>" + (item.AccNoteID == 0 ? "" : item.AccNoteCode) + "</td>"
                + "<td class='PayableOperation hide' val='" + item.OperationID + "'>" + (item.OperationID == 0 ? "" : item.OperationCode) + "</td>"
                + "<td class='PayableMasterOperation hide' val='" + item.MasterOperationID + "'>" + (item.MasterOperationID == 0 ? "" : item.MasterOperationCode) + "</td>"
                + "<td class='HouseNumber'>" + (item.HouseNumber == 0 ? "" : item.HouseNumber) + (item.CertificateNumber == 0 ? "" : (" / " + item.CertificateNumber)) + "</td>"
                + "<td class='PayableOperationVehicleID hide'>" + (item.OperationVehicleID == "0" ? "" : item.OperationVehicleID) + "</td>"
                + "<td class='PayableChassisNumber " + (pDefaults.UnEditableCompanyName == "GBL" ? "" : " hide ") + "'>" + (item.ChassisNumber == "0" ? "" : item.ChassisNumber) + "</td>"
                + "<td class='PayableTruckID hide'>" + item.TruckID + "</td>"
                + "<td class='PayableTruckNumber " + (pDefaults.UnEditableCompanyName == "GBL" ? "" : " hide ") + "'>" + (item.TruckNumber == "0" ? "" : item.TruckNumber) + "</td>"
                + "<td class='PayableOperationContainersAndPackagesID hide'>" + item.OperationContainersAndPackagesID + "</td>"
                + "<td class='PayableTankOrFlexiNumber " + (/*$("#cbIsTank").prop('checked')*/1 == 1 ? "" : " hide ") + "'>" + (item.TankOrFlexiNumber == "0" ? (item.ContainerNumber == "0" ? "" : item.ContainerNumber) : item.TankOrFlexiNumber) + "</td>"

                + "<td class='PayableBillTo hide' val='" + item.BillTo + "'>" + (item.BillTo == 0 ? "" : item.BillToName) + "</td>"
                + "<td class='ShownPayableNotes' title='" + (item.Notes == 0 ? "" : item.Notes) + "'>" + (item.Notes == 0 ? "" : (item.Notes.substring(0, 12) + '...')) + "</td>"
                + "<td class='PayableNotes hide'>" + (item.Notes == 0 ? "" : item.Notes) + "</td>"
                + "<td class='PayableCreatorName hide'>" + item.CreatorName + "</td>"
                //+ "<td class='PayableCreationDate hide'>" + item.CreationDate + "</td>"
                + "<td class='PayableCreationDate hide'>" + "<span class='pull-left thumb-sm  calendar-icon-style'>"
                                            + " <i class='fa fa-calendar'></i>"
                                            //+ " <small class='text-muted'>" + ConvertDateFormat(GetDateWithFormatMDY(item.CreationDate)) + "</small>"
                                            + " <small>" + GetShownDate(FormattedTodaysDate, GetDateWithFormatMDY(item.CreationDate)) + "</small>"
                                            + "</span>"
                                            + "</td>"
                + "<td class='PayableModificatorName hide'>" + item.ModificatorName + "</td>"
                //+ "<td class='PayableModificationDate hide'>" + item.ModificationDate + "</td>"
                + "<td class='PayableModificationDate hide'>" + "<span class='pull-left thumb-sm  calendar-icon-style'>"
                                            + " <i class='fa fa-calendar'></i>"
                                            //+ " <small class='text-muted'>" + ConvertDateFormat(GetDateWithFormatMDY(item.ModificationDate)) + "</small>"
                                            + " <small>" + GetShownDate(FormattedTodaysDate, GetDateWithFormatMDY(item.ModificationDate)) + "</small>"
                                            + "</span>"
                                            + "</td>"
                + "<td class='IsPrinted " + (pDefaults.UnEditableCompanyName == "MIL" ? "" : "hide") + "'> <input type='checkbox' disabled='disabled' val='" + (item.IsPrinted ? "true' checked='checked'" : "'") + " /></td>"
                + "<td class='PrintPayableID " + ($("#cbIsAWB").prop("checked") ? " hide " : "") + "'> <input " + (/*item.SupplierInvoiceNo == 0 && !item.IsApproved && item.AccNoteID == 0*/ 1 == 1 ? "name='PrintPayable'" : "disabled='disabled'") + " type='checkbox' value='" + item.ID + "'></td>"

                + "<td class='PayableIssueDate'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.IssueDate)) > 0 ? ConvertDateFormat(GetDateWithFormatMDY(item.IssueDate)) : "") + "</td>"
                + "<td class='PayableEntryDate " + (pDefaults.UnEditableCompanyName == "OCE" || pDefaults.UnEditableCompanyName == "CHM" || pDefaults.UnEditableCompanyName == "IST" || pDefaults.UnEditableCompanyName == "NEW" ? "" : " hide ") + "'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.EntryDate)) > 0 ? ConvertDateFormat(GetDateWithFormatMDY(item.EntryDate)) : "") + "</td>"
                + "<td class='PayableStatus hide'>" + item.PayableStatus + "</td>"

                + "<td>"
                    //+ "<a href='#Edit" + varAWBSuffix + "PayableModal data-toggle='modal' onclick='Payables_FillControls(" + item.ID + ");' " + editControlsText + "</a>"
                    + "<a " + (OAPay && $("#hIsOperationDisabled").val() == false ? "" : " disabled='disabled' ") + " href='#CopyChargeModal' data-toggle='modal' " + 'onclick="Payables_OpenCopyChargeModal(' + item.ID + ",'" + item.ChargeTypeName + "'" + ');" ' + copyControlsText + "</a>"
                    //+ "<a class='" + (OAPay && $("#hIsOperationDisabled").val() == false ? "" : "hide") + "' href='#'" + " onclick='Payables_Copy(" + item.ID + ");' " + copyControlsText + "</a>"
                + "</td>"
        + "</tr>"));
        OtherChargesDueCarrier = (item.ChargeTypeCode.toUpperCase() == "SH" ? (OtherChargesDueCarrier + 0) : (OtherChargesDueCarrier + Number(item.CostPrice.toFixed(2))));
        TaxCode_SH = (item.ChargeTypeCode.toUpperCase() == "SH" ? (TaxCode_SH + Number(item.CostAmount.toFixed(2))) : (TaxCode_SH + 0));
    });
    //ApplyPermissions();
    //if ($("#cbIsAWB").prop("checked")) {
    $("#txtOtherChargesDueCarrier").val(OtherChargesDueCarrier);
    $("#txtTax").val(TaxCode_SH);
    //}
    if (OAPay && $("#hIsOperationDisabled").val() == false) { $("#btn-Add" + varAWBSuffix + "Payables").removeClass("hide"); $("#btn-PricingRecommendation").removeClass("hide");/*$("#btn-GenerateDefaultPayables").removeClass("hide"); $("#btn-GeneratePayablesFromQuotation").removeClass("hide");*/ }
    else { $("#btn-Add" + varAWBSuffix + "Payables").addClass("hide"); $("#btn-PricingRecommendation").addClass("hide"); $("#btn-Add" + varAWBSuffix + "FlexiPayables").addClass("hide"); /*$("#btn-GenerateDefaultPayables").addClass("hide"); $("#btn-GeneratePayablesFromQuotation").addClass("hide");*/ }
    if (ODPay && $("#hIsOperationDisabled").val() == false) $("#btn-Delete" + varAWBSuffix + "Payable").removeClass("hide"); else $("#btn-Delete" + varAWBSuffix + "Payable").addClass("hide");
    if (OEPay && $("#hIsOperationDisabled").val() == false) {
        $("#btn-MultiRowEditPayables").removeClass("hide");
        //if (pDefaults.UnEditableCompanyName == "GBL")
            $("#btn-ResetPayablesQuantities").removeClass("hide");
    }
    else {
        $("#btn-MultiRowEditPayables").addClass("hide");
        //if (pDefaults.UnEditableCompanyName == "GBL")
            $("#btn-ResetPayablesQuantities").addClass("hide");
    }
    if (OANot && $("#hIsOperationDisabled").val() == false) { $("#btn-NewDebitNote").removeClass("hide"); $("#btn-NewCreditNote").removeClass("hide"); } else { $("#btn-NewDebitNote").addClass("hide"); $("#btn-NewCreditNote").addClass("hide"); }
    if (ODNot && $("#hIsOperationDisabled").val() == false) $("#btn-DeleteAccNote").removeClass("hide"); else $("#btn-DeleteAccNote").addClass("hide");

    BindAllCheckboxonTable("tbl" + varAWBSuffix + "Payables", "PayableID", "cb-CheckAll-Payables" + ($("#cbIsAWB").prop("checked") ? "-AWB" : ""));
    CheckAllCheckbox("HeaderDelete" + varAWBSuffix + "PayableID");
    HighlightText("#tbl" + varAWBSuffix + "Payables>tbody>tr", $("#txt-Search").val().trim());
    if (showDeleteFailMessage) {//sherif: if deletion failed due to foreign key, then this message is shown
        swal(strSorry, strDeleteFailMessage, "warning");
        showDeleteFailMessage = false;
    }
    if (!$("#cbIsAWB").prop("checked") && OEPay) {
        Payables_CalculateSubtotals(0); //the parameter is the pSelectedPayableIDs (0 unless called from Docs when need to print only selected payables)
        PayablesAndReceivables_CalculateSummary();
    }
}
function Payables_EditByDblClick(pID) {
    jQuery("#EditPayableModal").modal("show");
    Payables_FillControls(pID);
}
function Payables_LoadWithPagingWithWhereClause(pOperationID, pCallback, pFadePageCover) {
    pFadePageCover = (pFadePageCover != undefined && pFadePageCover != null ? pFadePageCover : true);
    if (pOperationID == 0)
        pOperationID = $("#hOperationID").val();
    var pWhereClause = "WHERE IsDeleted = 0";
    if ($("#cbIsAWB").prop("checked"))
        pWhereClause += "  AND (OperationID = " + pOperationID + ") ";// + " AND IsDeleted = 0 ";
    else
        pWhereClause += " AND OperationID = " + pOperationID + " \n";  //pWhereClause = " WHERE (OperationID = " + pOperationID + " OR MasterOperationID = " + pOperationID + ") ";// + " AND IsDeleted = 0 ";
    if ($("#slSearchTankInPayables").val() != "")
        pWhereClause += " AND OperationContainersAndPackagesID=" + $("#slSearchTankInPayables").val() + " \n";
    if ($("#txtSearchPayables").val().trim() == "0")
        pWhereClause += "AND (CostAmount IS NULL OR CostAmount=0)" + " \n";
    else if ($("#txtSearchPayables").val().trim() != "") {
        pWhereClause += "AND (" + " \n";
        pWhereClause += "   HouseNumber=N'" + $("#txtSearchPayables").val().trim() + "'" + " \n";
        pWhereClause += "   OR CertificateNumber=N'" + $("#txtSearchPayables").val().trim() + "'" + " \n";
        if (pDefaults.UnEditableCompanyName == "BED")
            pWhereClause += "   OR PartnerSupplierName LIKE N'%" + $("#txtSearchPayables").val().trim() + "%'" + " \n";
        pWhereClause += ")" + "\n";
        
    }
    LoadWithPagingWithWhereClause("div-Pager", "select-page-size", "spn-first-page-row", "spn-last-page-row", "spn-total-count", "div-Text-Total", "/api/Payables/LoadWithWhereClause", pWhereClause, 0, 1000
        , function (pTabelRows) {
            Payables_BindTableRows(pTabelRows);
            if (pCallback != null && pCallback != undefined)
                pCallback();
        }
        , pFadePageCover);
}
function Payables_DeleteList(callback) {
    //Confirmation message to delete
    var AWBSuffix = "";
    if ($("#cbIsAWB").prop("checked")) {
        AWBSuffix = "AWB";
    }
    if (GetAllSelectedIDsAsString('tblPayables' + AWBSuffix) != "")
        swal({
            title: "Are you sure?",
            text: "The selected records will be deleted permanently!",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, delete!",
            closeOnConfirm: true
        },
        //callback function in case of confirm delete
        function () {
            FadePageCover(true);
            DeleteListFunction("/api/Payables/Delete"
                , { pPayablesIDs: GetAllSelectedIDsAsString('tblPayables' + AWBSuffix), pOperationID: $("#hOperationID" + AWBSuffix).val() }
                , function () {
                    Payables_LoadWithPagingWithWhereClause($("#hOperationID" + AWBSuffix).val());
                    OperationPartners_LoadWithPagingWithWhereClause($("#hOperationID" + AWBSuffix).val());
                });
        });
    //DeleteListFunction("/api/Payables/Delete", { "pPayablesIDs": GetAllSelectedIDsAsString('tblPayables') }, function () { LoadViews("OperationsEdit", null, $("#hOperationID").val()); });
}
///////////////////////The next 3 fns are for the first 3 buttons in the payable screen/////////////////
function Payables_GetAvailableCharges() {
    debugger;
    FadePageCover(true);
    var pStrFnName = "/api/ChargeTypes/LoadAll";
    var pDivName = "divSelectCharges";
    var ptblModalName = "tblModalPayables";
    var pCheckboxNameAttr = "cbSelectPayables";
    var pWhereClause = "";
    pWhereClause += " WHERE IsUsedInPayable = 1 AND IsInactive = 0 AND IsOperationChargeType=1 ";
    pWhereClause += ($("#cbIsOcean").prop('checked') == true ? " AND IsOcean = 1 " : "");
    pWhereClause += ($("#cbIsAir").prop('checked') == true ? " AND IsAir = 1 " : "");
    pWhereClause += ($("#cbIsInland").prop('checked') == true ? " AND IsInland = 1 " : "");
    pWhereClause += " AND ( Code LIKE N'%" + $("#txtSearchCharges").val().trim().toUpperCase() + "%' OR Name LIKE N'%" + $("#txtSearchCharges").val().trim().toUpperCase() + "%') \n";
    //pWhereClause += (pDefaults.IsDepartmentOption && !pLoggedUser.IsAccessAllCharges
    //                ? " AND ID IN (SELECT ChargeTypeID FROM DepartmentCharge WHERE DepartmentID=" + pLoggedUser.DepartmentID + ") \n"
    //                : "");
    //pWhereClause += " AND ID NOT IN (SELECT ChargeTypeID from Payables ";
    //pWhereClause += "                WHERE OperationID = " + $("#hOperationID").val() + ") ";
    GetListAsCheckboxesWithVariousParameters(pStrFnName, { pWhereClause: pWhereClause }, pDivName, pCheckboxNameAttr
        , function () {
            HighlightText("#divSelectCharges", $("#txtSearchCharges").val().trim().toUpperCase());
            FadePageCover(false);
        }
        , pDefaults.UnEditableCompanyName == "MAR"
            ? 2
            : (pDefaults.IsRepeatChargeTypeName ? 3 : 1) //pCodeOrName
        , "col-sm-3"/*pColumnSize*/);
    $("#btn-SearchCharges").attr("onclick", "Payables_GetAvailableCharges();");
    $("#btnSelectChargesApply").attr("onclick", "Payables_InsertListWithoutValues(false);");

    //FillPayablesModalTableControls(pStrFnName, pWhereClause, pDivName, ptblModalName, pCheckboxNameAttr, true //pIsInsert
    //    , function () {
    //        HighlightText("#divSelectCharges", $("#txtSearchCharges").val().trim().toUpperCase());
    //        $("#btnSelectChargesApply").attr("onclick", "Payables_InsertListWithValues(false);");
    //    });
    //$("#btn-SearchCharges").attr("onclick", "Payables_GetAvailableCharges();");
}
function Payables_GenerateDefaults() {
    swal({
        title: strAreYouSure,
        text: "The Default Payables For Operation '" + $("#hOperationCode").val() + "' Will Be Applied.",
        //type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes, Apply!",
        closeOnConfirm: true
    },
    //callback function in case of success
    function () {
        FadePageCover(true);
        var pWhereClause = " WHERE ";
        pWhereClause += " IsUsedInPayable = 1 AND IsDefaultInOperations = 1 AND IsInactive = 0 ";
        pWhereClause += ($("#cbIsOcean").prop('checked') == true ? " AND IsOcean = 1 " : "");
        pWhereClause += ($("#cbIsAir").prop('checked') == true ? " AND IsAir = 1 " : "");
        pWhereClause += ($("#cbIsInland").prop('checked') == true ? " AND IsInland = 1 " : "");
        debugger;
        CallGETFunctionWithParameters("/api/Payables/ApplyDefaultPayables"
            , {
                pOperationID: $("#hOperationID").val()
                , pWhereClause: pWhereClause
                , pTruckingOrderID: 0
                , pCustomerID: 0
                , pSearchKeyword: 0
            }
            , function () {
                Payables_LoadWithPagingWithWhereClause($("#hOperationID").val());
            });
    });
}
function Payables_GenerateFromQuotation() {
    if ($("#hQuotationRouteID").val() == 0)
        swal(strSorry, "This Operation is not created from a quotation.", "warning");
    else
        swal({
            title: strAreYouSure,
            text: "The Payables For Operation '" + $("#hOperationCode").val() + "' Will Be Generated From Quotation.",
            //type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, Apply!",
            closeOnConfirm: true
        },
        //callback function in case of success
        function () {
            FadePageCover(true);
            var pWhereClause = " WHERE QuotationRouteID = " + $("#hQuotationRouteID").val();
            debugger;
            CallGETFunctionWithParameters("/api/Payables/GeneratePayablesFromQuotation"
                , { pOperationID: $("#hOperationID").val(), pWhereClause1: pWhereClause, pQuotationRouteID: $("#hQuotationRouteID").val() } // i called the pWhereClause1 instead of pWhereClause to avoid the error of routing when 2 actions have the same signature
                , function () {
                    Payables_LoadWithPagingWithWhereClause($("#hOperationID").val());
                });
        });
}
///////////////////////The previous 3 fns are for the first 3 buttons in the payable screen/////////////////
/////////////////////////////Saving fns//////////////////////////////////
function Payables_InsertListWithoutValues(pSaveandAddNew) {
    var AWBSuffix = "";
    if ($("#cbIsAWB").prop("checked")) {
        AWBSuffix = "AWB";
    }
    var pSelectedIDs = GetAllSelectedIDsAsStringWithNameAttr("cbSelectPayables");//returns string array of IDs
    debugger;
    if (pSelectedIDs != "")
        FadePageCover(true);
    InsertSelectedCheckboxItems("/api/Payables/InsertListWithoutValues"
        , {
            pOperationID: $("#hOperationID" + AWBSuffix).val()
            , pSelectedIDs: pSelectedIDs, pQuotationRouteID: 0, pOperationContainersAndPackagesID: 0
            , pOperationVehicleID: 0, pTruckingOrderID: 0
        }
        , pSaveandAddNew
        , "SelectChargesModal" //pModalID
        , null //function () { Payables_GetAvailableCharges(); }
        , function () {
            Payables_LoadWithPagingWithWhereClause($("#hOperationID" + AWBSuffix).val());
        });
}
function Payables_InsertListWithValues(pSaveandAddNew) {
    debugger;
    var pSelectedIDs = GetAllSelectedIDsAsStringWithNameAttr("cbSelectPayables");//returns string array of IDs
    var pPOrCList = "";
    var pSupplierList = "";
    var pUOMList = "";
    var pQuantityList = "";
    var pCostPriceList = "";
    var pCostAmountList = "";
    var pInitialSalePriceList = "";
    var pSupplierInvoiceNumberList = "";
    var pSupplierReceiptNumberList = "";
    var pExchangeRateList = "";
    var pCurrencyList = "";
    if (pSelectedIDs != "") {
        debugger;
        FadePageCover(true);
        var NumberOfSelectRows = pSelectedIDs.split(',').length;
        for (i = 0; i < NumberOfSelectRows; i++) {
            var currentRowID = pSelectedIDs.split(",")[i];

            pPOrCList += ((pPOrCList == "") ? "" : ",") + ($("#slPayablePOrC" + currentRowID).val() == undefined || $("#slPayablePOrC" + currentRowID).val() == "" ? 0 : $("#slPayablePOrC" + currentRowID).val());
            pSupplierList += ((pSupplierList == "") ? "" : ",") + ($("#slPayableSupplier" + currentRowID).val() == undefined || $("#slPayableSupplier" + currentRowID).val() == "" ? 0 : $("#slPayableSupplier" + currentRowID).val());
            pUOMList += ((pUOMList == "") ? "" : ",") + ($("#slPayableUOM" + currentRowID).val() == undefined || $("#slPayableUOM" + currentRowID).val() == "" ? 0 : $("#slPayableUOM" + currentRowID).val());
            pQuantityList += ((pQuantityList == "") ? "" : ",") + ($("#txtTblModalPayableQuantity" + currentRowID).val() == undefined || $("#txtTblModalPayableQuantity" + currentRowID).val() == "" ? 0 : $("#txtTblModalPayableQuantity" + currentRowID).val());
            pCostPriceList += ((pCostPriceList == "") ? "" : ",") + ($("#txtTblModalPayableCostPrice" + currentRowID).val() == undefined || $("#txtTblModalPayableCostPrice" + currentRowID).val() == "" ? 0 : $("#txtTblModalPayableCostPrice" + currentRowID).val());
            pCostAmountList += ((pCostAmountList == "") ? "" : ",") + ($("#txtTblModalPayableCostAmount" + currentRowID).val() == undefined || $("#txtTblModalPayableCostAmount" + currentRowID).val() == "" ? 0 : $("#txtTblModalPayableCostAmount" + currentRowID).val());
            pInitialSalePriceList += ((pInitialSalePriceList == "") ? "" : ",") + ($("#txtTblModalPayableInitialSalePrice" + currentRowID).val() == undefined || $("#txtTblModalPayableInitialSalePrice" + currentRowID).val() == "" ? 0 : $("#txtTblModalPayableInitialSalePrice" + currentRowID).val());
            pSupplierInvoiceNumberList += ((pSupplierInvoiceNumberList == "") ? "" : ",") + ($("#txtTblModalPayableSupplierInvoiceNo" + currentRowID).val() == undefined || $("#txtTblModalPayableSupplierInvoiceNo" + currentRowID).val().trim() == "" ? 0 : $("#txtTblModalPayableSupplierInvoiceNo" + currentRowID).val().trim().toUpperCase());
            pSupplierReceiptNumberList += ((pSupplierReceiptNumberList == "") ? "" : ",") + ($("#txtTblModalPayableSupplierReceiptNo" + currentRowID).val() == undefined || $("#txtTblModalPayableSupplierReceiptNo" + currentRowID).val().trim() == "" ? 0 : $("#txtTblModalPayableSupplierReceiptNo" + currentRowID).val().trim().toUpperCase());
            pExchangeRateList += ((pExchangeRateList == "") ? "" : ",") + ($("#txtTblModalPayableExchangeRate" + currentRowID).val() == undefined || $("#txtTblModalPayableExchangeRate" + currentRowID).val() == "" ? 0 : $("#txtTblModalPayableExchangeRate" + currentRowID).val());
            pCurrencyList += ((pCurrencyList == "") ? "" : ",") + ($("#slPayableCurrency" + currentRowID).val() == undefined || $("#slPayableCurrency" + currentRowID).val() == "" ? 0 : $("#slPayableCurrency" + currentRowID).val());
        }
        debugger;
    }
    if (pSelectedIDs != "")
        InsertSelectedCheckboxItems("/api/Payables/InsertListWithValues"
            , {
                "pOperationID": $("#hOperationID").val()
                , "pSelectedIDs": pSelectedIDs
                , "pPOrCList": pPOrCList
                , "pSupplierList": pSupplierList
                , "pUOMList": pUOMList
                , "pQuantityList": pQuantityList
                , "pCostPriceList": pCostPriceList
                , "pCostAmountList": pCostAmountList
                , "pInitialSalePriceList": pInitialSalePriceList
                , "pSupplierInvoiceNumberList": pSupplierInvoiceNumberList
                , "pSupplierReceiptNumberList": pSupplierReceiptNumberList
                , "pExchangeRateList": pExchangeRateList
                , "pCurrencyList": pCurrencyList
            }
            , pSaveandAddNew
            , "SelectChargesModal" //pModalID
            , function () { Payables_GetAvailableCharges(); }
            , function () {
                Payables_LoadWithPagingWithWhereClause($("#hOperationID").val());
            });
}
function Payables_Update(pSaveandAddNew) {
    debugger;
    var AWBSuffix = "";
    if ($("#cbIsAWB").prop("checked")) {
        AWBSuffix = "AWB";
    }
    var tr = $("#tblPayables" + AWBSuffix + " tr[ID='" + $("#hPayableID").val() + "']");

    let chargeTypeCost = parseFloat($(tr).find("td.ChargeTypeCost").text());
    let msgChargeTypeCostWarning = "";
    if (pDefaults.UnEditableCompanyName == "COS" && $("#txtPayableUnitPrice").val() > chargeTypeCost && chargeTypeCost > 0)
        msgChargeTypeCostWarning = "Cost exceeds charge type cost defined in master data.";

    if (pLoggedUser.Username == "BISHOY DEIF" && $(tr).find("td.IsApproved").text() == "true") {
        FadePageCover(true);
        CallGETFunctionWithParameters("/api/Payables/UpdateApprovedPayable"
            , {
                pApprovedPayableID: $("#hPayableID").val()
                , pSupplierInvoiceNo: ($("#txtPayableSupplierInvoiceNo").val().trim() == "" ? 0 : $("#txtPayableSupplierInvoiceNo").val().trim().toUpperCase())
            }
            , function (pData) {
                if (pData[0] == "")
                    swal("Success", "Saved successfully, refresh to ensure.");
                else
                    swal("Sorry", pData[0]);
                FadePageCover(false);
            }
            , null, true/*Default true*/);
    }
    else {
        if (
                (!isValidDate($("#txtPayableEntryDate").val().trim(), 1) && $("#txtPayableEntryDate").val().trim() != "")
                || (!isValidDate($("#txtPayableIssueDate").val().trim(), 1) && $("#txtPayableIssueDate").val().trim() != "")
            )
            swal(strSorry, strCheckDates);
        else if (parseFloat($("#txtPayableExchangeRate").val()) == 0
            || (parseFloat($("#txtPayableExchangeRate").val()) == 1 && pDefaults.CurrencyID != ($("#slPayableCurrency").val())))
            swal("Sorry", "Please, check exchange rate.");
            //else if (pDefaults.UnEditableCompanyName == "GBL" && $("#slSites").val() == '')
            //    swal("Sorry", "Please, select site.");
        else if (IsNull($("#txtPayableSupplierInvoiceNo").val(), "0") == "0"
            && (pDefaults.UnEditableCompanyName == "MIL" || pDefaults.UnEditableCompanyName == "COS"))
            swal("Sorry", "Please, Insert  Supplier Invoice No.");
        else {
            InsertUpdateFunction("form", "/api/Payables/Update", {
                pSavedFrom: 10 //pSavedFrom=10 : saved from Operations
                , pOperationContainersAndPackagesID: $("#slPayableTank").val() == "" ? 0 : $("#slPayableTank").val()
                , pID: $("#hPayableID").val()
                //, pMeasurementID: $("#txtCalculationType").attr("MeasurementID")
                , pOperationID: $("#hOperationID" + AWBSuffix).val()
                , pChargeTypeID: $("#slPayableChargeType").val() == "" ? 0 : $("#slPayableChargeType").val() //$("#txtPayableType").attr("ChargeTypeID")
                , pMeasurementID: $('#slPayableUOM option:selected').val() != ""
                    ? $('#slPayableUOM option:selected').val()
                    : 0
                //, pContainerTypeID: ((($("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked')) && $('#slPayableUOM option:selected').val() != "")
                //    ? $('#slPayableUOM option:selected').val()
                //    : 0)
                , pContainerTypeID: 0
                //, pPackageTypeID: ((($("#cbIsLCL").prop('checked') || $("#cbIsLTL").prop('checked') || $("#cbIsAir").prop('checked')) && $('#slPayableUOM option:selected').val() != "")
                //    ? $('#slPayableUOM option:selected').val()
                //    : 0)
                , pPOrC: ($('#slPayablePOrC option:selected').val() == "" ? 0 : $('#slPayablePOrC option:selected').val())
                , pSupplierOperationPartnerID: ($('#slPayableSupplier option:selected').val() == "" ? 0 : $('#slPayableSupplier option:selected').val())
                , pQuantity: ($("#txtPayableQuantity").val().trim() == "" ? 0 : $("#txtPayableQuantity").val().trim())
                , pCostPrice: ($("#txtPayableUnitPrice").val().trim() == "" ? 0 : $("#txtPayableUnitPrice").val().trim())

                , pAmountWithoutVAT: $("#txtPayableAmountWithoutVAT").val() == "" ? 0 : $("#txtPayableAmountWithoutVAT").val()
                , pTaxTypeID: $("#slPayableTax").val() == "" ? 0 : $("#slPayableTax").val()
                , pTaxPercentage: $("#txtPayableTaxPercentage").val() == "" ? 0 : $("#txtPayableTaxPercentage").val()
                , pTaxAmount: $("#txtPayableTaxAmount").val() == "" ? 0 : $("#txtPayableTaxAmount").val()
                , pDiscountTypeID: $("#slPayableDiscount").val() == "" ? 0 : $("#slPayableDiscount").val()
                , pDiscountPercentage: $("#txtPayableDiscountPercentage").val() == "" ? 0 : $("#txtPayableDiscountPercentage").val()
                , pDiscountAmount: $("#txtPayableDiscountAmount").val() == "" ? 0 : $("#txtPayableDiscountAmount").val()

                , pCostAmount: ($("#txtPayableAmount").val().trim() == "" ? 0 : $("#txtPayableAmount").val().trim())
                , pInitialSalePrice: ($("#txtPayableInitialSalePrice").val().trim() == "" ? 0 : $("#txtPayableInitialSalePrice").val().trim())
                , pSupplierInvoiceNo: ($("#txtPayableSupplierInvoiceNo").val().trim() == "" ? 0 : $("#txtPayableSupplierInvoiceNo").val().trim().toUpperCase())
                , pSupplierReceiptNo: ($("#txtPayableSupplierReceiptNo").val().trim() == "" ? 0 : $("#txtPayableSupplierReceiptNo").val().trim().toUpperCase())
                , pEntryDate: ($("#txtPayableEntryDate").val().trim() == "" ? "01/01/1900" : ConvertDateFormat($("#txtPayableEntryDate").val().trim()))
                , pBillID: ($('#slPayableBill option:selected').val() == "" ? 0 : $('#slPayableBill option:selected').val())

                , pIssueDate: ($("#txtPayableIssueDate").val().trim() == "" ? "01/01/1900" : ConvertDateFormat($("#txtPayableIssueDate").val().trim()))

                , pExchangeRate: ($("#txtPayableExchangeRate").val().trim() == "" ? 0 : $("#txtPayableExchangeRate").val().trim())
                , pCurrencyID: ($('#slPayableCurrency option:selected').val() == "" ? 0 : $('#slPayableCurrency option:selected').val())
                , pNotes: $("#txtPayableNotes").val().toUpperCase().trim()
                //the next 2 parameters are to check uniqueness of supplier invoice No. in the controller
                , pPartnerTypeID: $('#slPayableSupplier option:selected').attr("PartnerTypeID") == "" ? 0 : $('#slPayableSupplier option:selected').attr("PartnerTypeID")
                , pPartnerID: $('#slPayableSupplier option:selected').attr("PartnerID") == "" ? 0 : $('#slPayableSupplier option:selected').attr("PartnerID")
                , pPayableBillTo: ($('#slPayableBillTo option:selected').val() == "" ? 0 : $('#slPayableBillTo option:selected').val())
                , pSupplierSiteID: ($('#slSites option:selected').val() == "" ? 0 : $('#slSites option:selected').val())
                , pTruckingOrderID: ($("#slPayableTruckingOrder").val() == "" ? 0 : $("#slPayableTruckingOrder").val())
            }, pSaveandAddNew, "EditPayableModal"
            , function (data) {
                Payables_LoadWithPagingWithWhereClause($("#hOperationID" + AWBSuffix).val());
                OperationPartners_LoadWithPagingWithWhereClause($("#hOperationID" + AWBSuffix).val());
                if (data[1] != "") //supplier invoice number uniqueness violated
                    swal("", data[1]);
                else if (msgChargeTypeCostWarning != "")
                    swal("Attention", msgChargeTypeCostWarning);
            });
        }
    }
}
function Payables_UpdateList(pSaveandAddNew) {
    debugger;
    var AWBSuffix = "";
    if ($("#cbIsAWB").prop("checked")) {
        AWBSuffix = "AWB";
    }
    var pSelectedPayablesIDsToUpdate = GetAllSelectedIDsAsStringWithNameAttr("cbSelectPayables");//returns string array of IDs
    var pPOrCList = "";
    var pSupplierList = "";
    var pUOMList = "";
    var pQuantityList = "";
    var pCostPriceList = "";

    var pAmountWithoutVATList = "";
    var pTaxTypeIDList = "";
    var pTaxPercentageList = "";
    var pTaxAmountList = "";
    var pDiscountTypeIDList = "";
    var pDiscountPercentageList = "";
    var pDiscountAmountList = "";

    var pCostAmountList = "";
    var pInitialSalePriceList = "";
    var pSupplierInvoiceNumberList = "";
    var pSupplierReceiptNumberList = "";
    var pIssueDateList = "";
    var pEntryDateList = "";
    var pExchangeRateList = "";
    var pCurrencyList = "";
    var pPartnerTypeIDList = "";//pPartnerTypeIDList,pPartnerIDList are to check uniqueness of supplier invoice No. in the controller
    var pPartnerIDList = "";
    var pSupplierSiteIDList = "";
    var pNotesList = "";
    var pBillIDList = "";
    var _IsZeroExchangeRate = false;
    var _NullSupplierSite = false;
    var _NullSupplierInvoiceNo = false;
    var _CostExceededChargeTypeCost = false;
    let msgChargeTypeCostWarning = "";

    if (pSelectedPayablesIDsToUpdate != "") {
        FadePageCover(true);
        var NumberOfSelectRows = pSelectedPayablesIDsToUpdate.split(',').length;
        for (i = 0; i < NumberOfSelectRows; i++) {
            var currentRowID = pSelectedPayablesIDsToUpdate.split(",")[i];
            if (pDefaults.UnEditableCompanyName == "COS"
                && parseFloat($("#txtTblModalPayableCostPrice" + currentRowID).val()) > parseFloat($("#txtTblModalPayableChargeTypeCost" + currentRowID).val())
                && parseFloat($("#txtTblModalPayableChargeTypeCost" + currentRowID).val()) != 0)
                msgChargeTypeCostWarning = "One or more rows have exceeded cost amount that what defined in master data.";
            //if (pDefaults.UnEditableCompanyName == "GBL" && ($("#slPayableSupplierSiteID" + currentRowID).val() == "" || $("#slPayableSupplierSiteID" + currentRowID).val() == "0"))
            //    _NullSupplierSite = true;
            if (IsNull($("#txtTblModalPayableSupplierInvoiceNo" + currentRowID).val(), "0") == "0"
                && (pDefaults.UnEditableCompanyName == "MIL" || pDefaults.UnEditableCompanyName == "COS"))
                _NullSupplierInvoiceNo = true;
            if ($("#txtTblModalPayableExchangeRate" + currentRowID).val() == undefined || $("#txtTblModalPayableExchangeRate" + currentRowID).val() == "" || parseFloat($("#txtTblModalPayableExchangeRate" + currentRowID).val()) == 0
                || (parseFloat($("#txtTblModalPayableExchangeRate" + currentRowID).val()) == 1 && pDefaults.CurrencyID != ($("#slPayableCurrency" + currentRowID).val())))
                _IsZeroExchangeRate = true;
            pPOrCList += ((pPOrCList == "") ? "" : ",") + ($("#slPayablePOrC" + currentRowID).val() == undefined || $("#slPayablePOrC" + currentRowID).val() == "" ? 0 : $("#slPayablePOrC" + currentRowID).val());
            pSupplierList += ((pSupplierList == "") ? "" : ",") + ($("#slPayableSupplier" + currentRowID).val() == undefined || $("#slPayableSupplier" + currentRowID).val() == "" ? 0 : $("#slPayableSupplier" + currentRowID).val());
            pUOMList += ((pUOMList == "") ? "" : ",") + ($("#slPayableUOM" + currentRowID).val() == undefined || $("#slPayableUOM" + currentRowID).val() == "" ? 0 : $("#slPayableUOM" + currentRowID).val());
            pQuantityList += ((pQuantityList == "") ? "" : ",") + ($("#txtTblModalPayableQuantity" + currentRowID).val() == undefined || $("#txtTblModalPayableQuantity" + currentRowID).val() == "" ? 0 : $("#txtTblModalPayableQuantity" + currentRowID).val());
            pCostPriceList += ((pCostPriceList == "") ? "" : ",") + ($("#txtTblModalPayableCostPrice" + currentRowID).val() == undefined || $("#txtTblModalPayableCostPrice" + currentRowID).val() == "" ? 0 : $("#txtTblModalPayableCostPrice" + currentRowID).val());

            pAmountWithoutVATList += ((pAmountWithoutVATList == "") ? "" : ",") + ($("#txtTblModalPayableCostAmountWithoutVAT" + currentRowID).val() == undefined || $("#txtTblModalPayableCostAmountWithoutVAT" + currentRowID).val() == "" ? 0 : $("#txtTblModalPayableCostAmountWithoutVAT" + currentRowID).val());
            pTaxTypeIDList += ((pTaxTypeIDList == "") ? "" : ",") + ($("#slPayableTax" + currentRowID).val() == undefined || $("#slPayableTax" + currentRowID).val() == "" ? 0 : $("#slPayableTax" + currentRowID).val());
            pTaxPercentageList += ((pTaxPercentageList == "") ? "" : ",") + ($("#txtTblModalPayableTaxPercentage" + currentRowID).val() == undefined || $("#txtTblModalPayableTaxPercentage" + currentRowID).val() == "" ? 0 : $("#txtTblModalPayableTaxPercentage" + currentRowID).val());
            pTaxAmountList += ((pTaxAmountList == "") ? "" : ",") + ($("#txtTblModalPayableTaxAmount" + currentRowID).val() == undefined || $("#txtTblModalPayableTaxAmount" + currentRowID).val() == "" ? 0 : $("#txtTblModalPayableTaxAmount" + currentRowID).val());
            pDiscountTypeIDList += ((pDiscountTypeIDList == "") ? "" : ",") + ($("#slPayableDiscount" + currentRowID).val() == undefined || $("#slPayableDiscount" + currentRowID).val() == "" ? 0 : $("#slPayableDiscount" + currentRowID).val());
            pDiscountPercentageList += ((pDiscountPercentageList == "") ? "" : ",") + ($("#txtTblModalPayableDiscountPercentage" + currentRowID).val() == undefined || $("#txtTblModalPayableDiscountPercentage" + currentRowID).val() == "" ? 0 : $("#txtTblModalPayableDiscountPercentage" + currentRowID).val());
            pDiscountAmountList += ((pDiscountAmountList == "") ? "" : ",") + ($("#txtTblModalPayableDiscountAmount" + currentRowID).val() == undefined || $("#txtTblModalPayableDiscountAmount" + currentRowID).val() == "" ? 0 : $("#txtTblModalPayableDiscountAmount" + currentRowID).val());

            pCostAmountList += ((pCostAmountList == "") ? "" : ",") + ($("#txtTblModalPayableCostAmount" + currentRowID).val() == undefined || $("#txtTblModalPayableCostAmount" + currentRowID).val() == "" ? 0 : $("#txtTblModalPayableCostAmount" + currentRowID).val());
            pInitialSalePriceList += ((pInitialSalePriceList == "") ? "" : ",") + ($("#txtTblModalPayableInitialSalePrice" + currentRowID).val() == undefined || $("#txtTblModalPayableInitialSalePrice" + currentRowID).val() == "" ? 0 : $("#txtTblModalPayableInitialSalePrice" + currentRowID).val());
            pSupplierInvoiceNumberList += ((pSupplierInvoiceNumberList == "") ? "" : ",") + ($("#txtTblModalPayableSupplierInvoiceNo" + currentRowID).val() == undefined || $("#txtTblModalPayableSupplierInvoiceNo" + currentRowID).val().trim() == "" ? (pDefaults.UnEditableCompanyName == "GBL" ? "N/A" : 0) : $("#txtTblModalPayableSupplierInvoiceNo" + currentRowID).val().trim().toUpperCase());
            pSupplierReceiptNumberList += ((pSupplierReceiptNumberList == "") ? "" : ",") + ($("#txtTblModalPayableSupplierReceiptNo" + currentRowID).val() == undefined || $("#txtTblModalPayableSupplierReceiptNo" + currentRowID).val().trim() == "" ? 0 : $("#txtTblModalPayableSupplierReceiptNo" + currentRowID).val().trim().toUpperCase());
            pIssueDateList += ((pIssueDateList == "") ? "" : ",") + ($("#txtTblModalPayableIssueDate" + currentRowID).val() == undefined || $("#txtTblModalPayableIssueDate" + currentRowID).val().trim() == "" ? "" : $("#txtTblModalPayableIssueDate" + currentRowID).val().trim());
            pEntryDateList += ((pEntryDateList == "") ? "" : ",") + ($("#txtTblModalPayableEntryDate" + currentRowID).val() == undefined || $("#txtTblModalPayableEntryDate" + currentRowID).val().trim() == "" ? "" : $("#txtTblModalPayableEntryDate" + currentRowID).val().trim());
            //pEntryDateList += ((pEntryDateList == "") ? "" : ",") + ($("#txtTblModalPayableEntryDate" + currentRowID).val() == undefined || $("#txtTblModalPayableEntryDate" + currentRowID).val().trim() == "" ? "" : ConvertDateFormat($("#txtTblModalPayableEntryDate" + currentRowID).val().trim()));
            pExchangeRateList += ((pExchangeRateList == "") ? "" : ",") + ($("#txtTblModalPayableExchangeRate" + currentRowID).val() == undefined || $("#txtTblModalPayableExchangeRate" + currentRowID).val() == "" ? 0 : $("#txtTblModalPayableExchangeRate" + currentRowID).val());
            pCurrencyList += ((pCurrencyList == "") ? "" : ",") + ($("#slPayableCurrency" + currentRowID).val() == undefined || $("#slPayableCurrency" + currentRowID).val() == "" ? 0 : $("#slPayableCurrency" + currentRowID).val());
            pPartnerTypeIDList += ((pPartnerTypeIDList == "") ? "" : ",") + ($("#slPayableSupplier" + currentRowID).val() == undefined || $("#slPayableSupplier" + currentRowID).val() == "" ? 0 : $("#slPayableSupplier" + currentRowID + " option:selected").attr("PartnerTypeID"));
            pPartnerIDList += ((pPartnerIDList == "") ? "" : ",") + ($("#slPayableSupplier" + currentRowID).val() == undefined || $("#slPayableSupplier" + currentRowID).val() == "" ? 0 : $("#slPayableSupplier" + currentRowID + " option:selected").attr("PartnerID"));
            pSupplierSiteIDList += ((pSupplierSiteIDList == "") ? "" : ",") + ($("#slPayableSupplierSiteID" + currentRowID).val() == undefined || $("#slPayableSupplierSiteID" + currentRowID).val() == "" ? 0 : $("#slPayableSupplierSiteID" + currentRowID).val());
            pNotesList += ((pNotesList == "") ? "" : ",") + ($("#txtTblModalPayableNotes" + currentRowID).val() == undefined || $("#txtTblModalPayableNotes" + currentRowID).val().trim() == "" ? 0 : $("#txtTblModalPayableNotes" + currentRowID).val().trim().toUpperCase());
            pBillIDList += ((pBillIDList == "") ? "" : ",") + ($("#slPayableBill" + currentRowID).val() == undefined || $("#slPayableBill" + currentRowID).val() == "" ? 0 : $("#slPayableBill" + currentRowID).val());
        }
    }
    if (_NullSupplierSite)
        {
        swal("Sorry", "Please, select site.");
        FadePageCover(false);
       }
    else if (_IsZeroExchangeRate) {
        swal("Sorry", "Please, check exchange rate.");
        FadePageCover(false);
    }
    else if (_NullSupplierInvoiceNo) {
        swal("Sorry", "Please, Insert Supplier Invoice No.");
        FadePageCover(false);
    }
    else {
        if (pSelectedPayablesIDsToUpdate != "")
            CallPOSTFunctionWithParameters("/api/Payables/UpdateList"
                , {
                    "pIsCalledFromOperations": true
                    , "pSelectedPayablesIDsToUpdate": pSelectedPayablesIDsToUpdate
                    , "pPOrCList": pPOrCList
                    , "pSupplierList": pSupplierList
                    , "pUOMList": pUOMList
                    , "pQuantityList": pQuantityList
                    , "pCostPriceList": pCostPriceList

                    , "pAmountWithoutVATList": pAmountWithoutVATList
                    , "pTaxTypeIDList": pTaxTypeIDList
                    , "pTaxPercentageList": pTaxPercentageList
                    , "pTaxAmountList": pTaxAmountList
                    , "pDiscountTypeIDList": pDiscountTypeIDList
                    , "pDiscountPercentageList": pDiscountPercentageList
                    , "pDiscountAmountList": pDiscountAmountList

                    , "pCostAmountList": pCostAmountList
                    , "pInitialSalePriceList": pInitialSalePriceList
                    , "pSupplierInvoiceNumberList": pSupplierInvoiceNumberList
                    , "pSupplierReceiptNumberList": pSupplierReceiptNumberList
                    , "pIssueDateList": pIssueDateList
                    , "pEntryDateList": pEntryDateList
                    , "pExchangeRateList": pExchangeRateList
                    , "pCurrencyList": pCurrencyList
                    , "pPartnerTypeIDList": pPartnerTypeIDList
                    , "pPartnerIDList": pPartnerIDList
                    , "pSupplierSiteIDList": pSupplierSiteIDList
                    , "pNotesList": pNotesList
                    , "pBillIDList": pBillIDList
                }
                , function (data) {
                    debugger;
                    Payables_LoadWithPagingWithWhereClause($("#hOperationID" + AWBSuffix).val());
                    OperationPartners_LoadWithPagingWithWhereClause($("#hOperationID" + AWBSuffix).val());
                    if (data[1] != "")
                        swal(strSorry, data[1]);
                    else if (msgChargeTypeCostWarning != "") {
                        swal("Attention", msgChargeTypeCostWarning);
                        jQuery("#SelectChargesModal").modal("hide");
                    }
                    else {
                        swal("Success", "Saved successfully.");
                        jQuery("#SelectChargesModal").modal("hide");
                    }
                }, null);
    }
}
function Payables_OpenCopyChargeModal(pPayableIDToCopy, pChargeTypeName) {
    debugger;
    $("#txtNumberOfCopies").val("");
    $("#lblCopyChargeShown").html(": " + pChargeTypeName);
    $("#btnCopyCharge").attr("onclick", "Payables_Copy(" + pPayableIDToCopy + ")");
}
function Payables_Copy(pPayableIDToCopy) {
    debugger;
    if ($("#txtNumberOfCopies").val() == "" || $("#txtNumberOfCopies").val() > 20)
        swal("Sorry", "Please, enter number of copies and it must be less less than 20.");
    else {
        FadePageCover(true);
        CallGETFunctionWithParameters("/api/Payables/CopyPayable"
            , { pPayableIDToCopy: pPayableIDToCopy, pNumberOfDuplicates: $("#txtNumberOfCopies").val() }
            , function (pData) {
                var pPayables = JSON.parse(pData[0]);
                Payables_BindTableRows(pPayables);
                swal("Success", "Saved successfully.");
                jQuery("#CopyChargeModal").modal("hide");
                FadePageCover(false);
            }
            , null);
    }
}
function Payables_GetAvailableUsers(pOption) {
    debugger;
    Receptionists_GetAvailableUsers();
    $("#btnCheckboxesListApply").attr("onclick", "Payables_SendAlarm('" + pOption + "');");
    $("#btnCheckboxesListApply").html("<span class='glyphicon glyphicon-send'></span> Send");
    $("#btn-SearchItems").attr("onclick", "Receptionists_GetAvailableUsers();");
}
function Payables_SendAlarm(pOption) {
    debugger;
    var pModalName = "CheckboxesListModal";
    var pCheckboxNameAttr = "cbAddedItemID";
    var pAlarmReceiversIDs = GetAllSelectedIDsAsStringWithNameAttr(pCheckboxNameAttr);
    if (pAlarmReceiversIDs == "")
        swal("Sorry", "You did not select any receivers.");
    else {
        FadePageCover(true);
        var pSubject = pOption + " : Oper. " + $("#hOperationCode").val();
        var pBody = "Oper/Job No : " + $("#hOperationCode").val() + "\n";
        pBody += "Client " + $("#lblClient").text() + "\n";
        pBody += "B/L " + $("#lblMaster").text() + "\n";
        pBody += "Service " + $("#lblServiceScope").text() + "\n";
        var pParametersWithValues = {
            pAlarmReceiversIDs: pAlarmReceiversIDs
            , pSubject: pSubject
            , pBody: pBody
        };
        CallGETFunctionWithParameters("/api/LocalEmails/SendAlarmWithMinimalData", pParametersWithValues
            , function (pData) {
                var _MessageReturned = pData[0];
                if (pData[0] == "") {
                    swal("Success", "Sent successfully.");
                    jQuery("#CheckboxesListModal").modal("hide");
                }
                else
                    swal("Sorry", _MessageReturned);
                FadePageCover(false);
            }
            , null)
    }
}
/////////////////////////EOF Saving fns//////////////////////////////////
function Payables_SetDefaultNote() {
    debugger;
    $("#txtPayableNotes").val(
        "Operation: " + $("#hOperationCode").val()
        + ($("#slPayableSupplier").val() == "" ? "" : (" / Partner: " + $("#slPayableSupplier option:selected").text().split(":")[1].trim()))
        + ($("#txtPayableSupplierInvoiceNo").val().trim() == "" ? "" : (" / Inv No: " + $("#txtPayableSupplierInvoiceNo").val().trim().toUpperCase()))
        );
}
function Payables_FillControls(pID) {
    debugger;
    ClearAll("#EditPayableModal");
    $('#btnSavePayable').prop('disabled', false);
    $('#btnSavePayable').removeClass('hide');
    if (pDefaults.UnEditableCompanyName == "GBL") {
        $(".classShowForGBL").removeClass("hide");
        $("#slPayableTruckingOrder").parent().removeClass("hide");
        $("#txtPayableSupplierInvoiceNo").attr("data-required", "true");
    }
    
    var AWBSuffix = "";
    if ($("#cbIsAWB").prop("checked")) {
        AWBSuffix = "AWB";
    }
    $("#hPayableID").val(pID);
    FadePageCover(true);
    var tr = $("#tblPayables" + AWBSuffix + " tr[ID='" + pID + "']");
    var pPOrCID = $(tr).find("td.PayablePOrC").attr('val');
    var pPartnerSupplierID = $(tr).find("td.PayableSupplier").attr('val');
    var pPayableBillTo = $(tr).find("td.PayableBillTo").attr('val');
    var pUOMID = $(tr).find("td.PayableUOM").attr('val');
    var pCurrencyID = $(tr).find("td.PayableCurrency").attr('val');
    var pOperationID = $(tr).find("td.PayableOperation").attr('val');
    var pBillID = $(tr).find("td.PayableBillID").text();
    var pSupplierSiteID = $(tr).find("td.SupplierSiteID").text();

    var pTaxTypeID = $(tr).find("td.PayableTaxTypeID").attr('val');
    var pDiscountTypeID = $(tr).find("td.PayableDiscountTypeID").attr('val');

    var pTruckingOrderID = $(tr).find("td.TruckingOrderID").text();
    var pOperationContainersAndPackagesID = $(tr).find("td.PayableOperationContainersAndPackagesID").text();

    $("#slPayableTank").html($("#slSearchTankInPayables").html()); $("#slPayableTank").val(pOperationContainersAndPackagesID == 0 ? "" : pOperationContainersAndPackagesID);
    if ($("#cbIsTank").prop("checked") || pDefaults.UnEditableCompanyName == "CAP" || pDefaults.UnEditableCompanyName == "IST")
        $("#slPayableTank").parent().removeClass("hide");

    if ($("#hDefaultCurrencyID").val() == pCurrencyID)
        $("#txtPayableExchangeRate").attr("disabled", "disabled");
    else
        $("#txtPayableExchangeRate").removeAttr("disabled");

    debugger;

    //if (pPartnerSupplierID == 0) {
    //    $("#txtPayableSupplierInvoiceNo").attr("disabled", "disabled");
    //    //$("#txtPayableSupplierReceiptNo").attr("disabled", "disabled");
    //}
    //else {
    //    $("#txtPayableSupplierInvoiceNo").removeAttr("disabled");
    //    $("#txtPayableSupplierReceiptNo").removeAttr("disabled");
    //}

    //if ($(tr).find("td.PayableSupplierInvoiceNo").text() == "" && $(tr).find("td.PayableSupplierReceiptNo").text() == "")
    if ($(tr).find("td.PayableSupplierInvoiceNo").text() == "")
        $("#slPayableSupplier").removeAttr("disabled");
    else
        $("#slPayableSupplier").attr("disabled", "disabled");



    //PayablePOrC_GetList(pPOrCID, "slPayablePOrC");
    $("#slPayablePOrC").html($("#slOperationPOrC").html()); $("#slPayablePOrC").val(pPOrCID == 0 ? "" : pPOrCID);
    //$("#txtPayableType").val($(tr).find("td.Payable").text());
    //$("#txtPayableType").attr("ChargeTypeID", $(tr).find("td.Payable").attr("val"));
    PayableSuppliers_GetList(pPartnerSupplierID, "slPayableSupplier", function () {
        $("#slPayableBillTo").html($("#slPayableSupplier").html()); $("#slPayableBillTo").val(pPayableBillTo == 0 ? "" : pPayableBillTo);
        if (pDefaults.UnEditableCompanyName == "GBL")
            FillSupplierSites(pSupplierSiteID, "slPayableSupplier", "slPayableSupplier", "slSites");
    });

    PayableCurrency_GetList(pCurrencyID, "slPayableCurrency");
    PayableUOM_GetList(pUOMID, "slPayableUOM");
    GetListTaxTypeWithNameAndPercAttr(pTaxTypeID, "/api/TaxeTypes/LoadAllWithWhereClause"
                , TranslateString("SelectFromMenu"), "slPayableTax", "WHERE IsInactive=0 ORDER BY Name"
                , function () {
                    $("#slPayableDiscount").html($("#slPayableTax").html());
                    $("#slPayableDiscount").val(pDiscountTypeID == 0 ? "" : pDiscountTypeID);
                    $("#slPayableTax option[IsDiscount='true']").addClass('hide');
                    $("#slPayableDiscount option[IsDiscount='false']").addClass('hide');
                });

    $("#lblPayableShown").html(": " + $(tr).find("td.Payable").text());
    $("#lblPayableCreatedBy").html(" : " + $(tr).find("td.PayableCreatorName").text())
    $("#lblPayableCreationDate").html(" : " + $(tr).find("td.PayableCreationDate").text())
    $("#lblPayableUpdatedBy").html(": " + $(tr).find("td.PayableModificatorName").text())
    $("#lblPayableModificationDate").html(" : " + $(tr).find("td.PayableModificationDate").text())
    CallGETFunctionWithParameters("/api/ChargeTypes/LoadAllWithMinimalColumns"
        , { pWhereClauseWithMinimalColumns: "WHERE 1=1" }
        , function (pData) {
            FillListFromObject($(tr).find("td.Payable").attr("val"), (pDefaults.IsRepeatChargeTypeName ? 4 : 2), TranslateString("SelectFromMenu"), "slPayableChargeType", pData[0], null);
        }
        , null);
    //GetListWithOpCodeAndHouseNoAndClientEmailAttr(pBillID, "/api/Operations/LoadWithParameters", "<--Select-->", "slPayableBill"
    //    , { pPageNumber: 1, pPageSize: 99999, pWhereClause: " WHERE MasterOperationID = " + pOperationID, pOrderBy: "HouseNumber" }
    //    , function () { FadePageCover(false); });
    CallGETFunctionWithParameters("/api/Operations/LoadAll"
        , { pWhereClause: " WHERE MasterOperationID=" + pOperationID }
        , function (pData) {
            var pOperationList = pData[0];
            Fill_SelectInputAfterLoadData_WithMultiAttrAndMultiFields(pOperationList, "ID", "HouseNumber,CertificateNumber", ' / ', TranslateString("SelectFromMenu"), "#slPayableBill", pBillID, "", function () { ApplySelectListSearch(); FadePageCover(false); });
    });
    CallGETFunctionWithParameters("/api/Routings/LoadAll"
        , {
            pWhereClause: " WHERE OperationID=" + pOperationID + " AND IsOwnedByCompany=0 AND TruckingOrderCode IS NOT NULL"
            , pOrderBy: "ID"
        }
    , function (pData) {
        var pRoutingList = pData[0];
        Fill_SelectInputAfterLoadData_WithMultiAttrAndMultiFields(pRoutingList, "ID", "TruckingOrderCode", '', TranslateString("SelectFromMenu"), "#slPayableTruckingOrder", pTruckingOrderID, "", function () { ApplySelectListSearch(); FadePageCover(false); });
    });

    //CallGETFunctionWithParameters("/api/OperationContainersAndPackages/LoadAllTanks"
    //        , {
    //            pPageNumber: 1
    //            , pPageSize: 999999
    //            //, pWhereClauseForTank: "WHERE OperationID=" + $("#hOperationID").val() + " AND TankOrFlexiNumber IS NOT NULL AND ID NOT IN (SELECT OperationContainersAndPackagesID FROM Payables WHERE OperationID=" + $("#hOperationID").val() + ") "
    //            , pWhereClauseForTank: "WHERE OperationID=" + $("#hOperationID").val() + " AND TankOrFlexiNumber IS NOT NULL "
    //            , pOrderBy: "ID"
    //        }
    //        , function (pData) {
    //            var pTank = pData[0];
    //            FillListFromObject(pOperationContainersAndPackagesID, 1, "<--Select-->", "slPayableTank", pTank, null);
    //        }
    //        , null);
    $("#txtPayableQuantity").val($(tr).find("td.PayableQuantity").text());
    $("#txtPayableUnitPrice").val(parseInt($(tr).find("td.PayableCostPrice").text()) == 0 ? "" : $(tr).find("td.PayableCostPrice").text());
    $("#txtPayableAmountWithoutVAT").val(parseInt($(tr).find("td.PayableAmountWithoutVAT").text()) == 0 ? "" : $(tr).find("td.PayableAmountWithoutVAT").text());

    $("#txtPayableTaxPercentage").val($(tr).find("td.PayableTaxPercentage").text());
    $("#txtPayableTaxAmount").val($(tr).find("td.PayableTaxAmount").text());
    $("#txtPayableDiscountPercentage").val($(tr).find("td.PayableDiscountPercentage").text());
    $("#txtPayableDiscountAmount").val($(tr).find("td.PayableDiscountAmount").text());

    $("#txtPayableAmount").val(parseInt($(tr).find("td.PayableCostAmount").text()) == 0 ? "" : $(tr).find("td.PayableCostAmount").text());
    $("#txtPayableInitialSalePrice").val($(tr).find("td.PayableInitialSalePrice").text());
    $("#txtPayableExchangeRate").val($(tr).find("td.PayableExchangeRate").text());
    $("#txtPayableSupplierInvoiceNo").val($(tr).find("td.PayableSupplierInvoiceNo").text());
    $("#txtPayableSupplierReceiptNo").val($(tr).find("td.PayableSupplierReceiptNo").text());
    $("#txtPayableEntryDate").val($(tr).find("td.PayableEntryDate").text());
    $("#slPayableBill").val(pBillID == 0 ? "" : pBillID);
    $("#txtPayableIssueDate").val($(tr).find("td.PayableIssueDate").text());
    $("#txtPayableNotes").val($(tr).find("td.PayableNotes").text());

    $("#slPayableUOM").attr("onchange", "Payables_UOMChanged();");
    $("#btnSavePayable").attr("onclick", "Payables_Update(false);");


    if (pLoggedUser.Username == "BISHOY DEIF" || 
            (OEPay && $("#hIsOperationDisabled").val() == false && IsNull($(tr).find("td.IsApproved").text(), "false") == "false" && IsNull($(tr).find("td.AccNoteID").text(), "0") == "0")
        ) {
        $('#btnSavePayable').prop('disabled', false);
        $('#btnSavePayable').removeClass('hide');
    }
    else
    {
        $('#btnSavePayable').prop('disabled', true);
        $('#btnSavePayable').addClass('hide');
    }

  //   !item.IsApproved && item.AccNoteID == 0 
  //  $("#slPayableSupplier").val(pSupplierSiteID == 0 ? "" : pSupplierSiteID);
    
}
function Payables_MultiRowEdit() {
    debugger;
    FadePageCover(true);
    var AWBSuffix = "";
    if ($("#cbIsAWB").prop("checked")) {
        AWBSuffix = "AWB";
    }
    var pSelectedIDs = GetAllSelectedIDsAsString('tblPayables' + AWBSuffix);
    $("#divSelectCharges").html("");
    var pStrFnName = "/api/Payables/LoadAll";
    var pDivName = "divSelectCharges";//div name to be filled
    var ptblModalName = "tblModalPayables";
    var pCheckboxNameAttr = "cbSelectPayables";
    var pWhereClause = "";
    pWhereClause += "                WHERE OperationID = " + $("#hOperationID" + AWBSuffix).val() + " AND IsDeleted=0 AND AccNoteID IS NULL AND InterServiceProviderDepartmentID IS NULL " + " \n";
    if (pSelectedIDs != "")
        pWhereClause += "AND ID IN (" + pSelectedIDs +")" + " \n";
    if ($("#txtSearchCharges").val().trim() == "0")
        pWhereClause += "AND (CostAmount IS NULL OR CostAmount=0)" + " \n";
    else pWhereClause += " AND ( ChargeTypeCode LIKE N'%" + $("#txtSearchCharges").val().trim().toUpperCase() + "%' OR ChargeTypeName LIKE N'%" + $("#txtSearchCharges").val().trim().toUpperCase() + "%' OR HouseNumber LIKE N'%" + $("#txtSearchCharges").val().trim().toUpperCase() + "%' OR CertificateNumber LIKE N'%" + $("#txtSearchCharges").val().trim().toUpperCase() + "%') ";
    //pWhereClause += "                ORDER BY ChargeTypeName ";

    FillPayablesModalTableControls(pStrFnName, pWhereClause, pDivName, ptblModalName, pCheckboxNameAttr, false //pIsInsert
        , function () { FadePageCover(false); HighlightText("#divSelectCharges", $("#txtSearchCharges").val().trim().toUpperCase()); });

    $("#btn-SearchCharges").attr("onclick", "Payables_MultiRowEdit();");
    $("#btnSelectChargesApply").attr("onclick", "Payables_UpdateList(false);");
}
function Payables_ClearAllControls(callback) {
    $('#btnSavePayable').prop('disabled', false);
    $('#btnSavePayable').removeClass('hide');
    ClearAll("#EditPayableModal");
    if (callback != null && callback != undefined)
        callback();
}
function Payables_ResetQuantities() {
    debugger;
    swal({
        title: "Are you sure?",
        text: "Payables quantities will be reset according to calculation type.",
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
                pOperationIDToSetPayablesQuantity_NotStatic: $("#hOperationID").val()
            };
            CallGETFunctionWithParameters("/api/ChargeTypes/ChargeTypes_SetDefaultPayablesQuantity_NotStatic", pParametersWithValues
                , function (pData) {
                    var _ReturnedMessage = pData[0];
                    if (_ReturnedMessage == "") {
                        Payables_LoadWithPagingWithWhereClause(0);
                        swal("Success", "Saved Successfully.");
                    }
                    else
                        swal("Sorry", _ReturnedMessage);
                    FadePageCover(false);
                }
                , null
                , false/*async*/);
        }); //ConfirmationMessage
}
function Payables_CurrencyChanged() {
  //  $("#txtPayableExchangeRate").val($("#slPayableCurrency option:selected").attr("MasterDataExchangeRate"));
    RecalculatePayableExchangeRate();
    if ($("#hDefaultCurrencyID").val() == $("#slPayableCurrency").val())
        $("#txtPayableExchangeRate").attr("disabled", "disabled");
    else
        $("#txtPayableExchangeRate").removeAttr("disabled");
}
//to handle change of currency in the multi row edit modal
function Payables_txtTblModalCurrency_Changed(pRowID, pIsInsert) {
    debugger;
    //if (pIsInsert) { //if not insert then all IDs will be updated
    $("#txtTblModalPayableExchangeRate" + pRowID).val($("#slPayableCurrency" + pRowID + " option:selected").attr("MasterDataExchangeRate"));
    if ($("#hDefaultCurrencyID").val() == $("#slPayableCurrency" + pRowID).val())
        $("#txtTblModalPayableExchangeRate" + pRowID).attr("disabled", "disabled");
    else
        $("#txtTblModalPayableExchangeRate" + pRowID).removeAttr("disabled");
    //}
}
function Payables_txtTblModalCostAmount_Changed(pRowID, pIsInsert) {
    if (pIsInsert) { //if not insert then all IDs will be updated
        var varPayableCostAmount = $("#tblModalPayables tr[ID='" + pRowID + "']").find('input[name=txtTblModalPayableCostAmount]').val();
        if (varPayableCostAmount != 0 && varPayableCostAmount != "")
            $("#tblModalPayables tr[ID='" + pRowID + "']").find('input[name=cbSelectPayables]').prop("checked", true);
        else
            $("#tblModalPayables tr[ID='" + pRowID + "']").find('input[name=cbSelectPayables]').prop("checked", false);
    }
}
function Payables_UOMChanged() {
    //if ($("#slPayableUOM option:selected").val() != "") {
    //    //$("#txtPayableQuantity").val($("#slPayableUOM option:selected").attr("Quantity"));
    //    //todo: get the number of packages or containers from the packages tab
    //    if ($("#hBLType").val() == constFCLShipmentType || $("#hShipmentType").val() == constFTLShipmentType || $("#hShipmentType").val() == constConsolidationShipmentType)
    //        $("#txtPayableQuantity").val($('#tblOperationContainersAndPackages tr td.Container[val=' + $("#slPayableUOM option:selected").val() + ']').length);
    //    CalculatePayablesAmount();
    //}
}
function Payables_PayableSupplierInvoiceOrReceiptNoChanged(pSupplierControl, pSupplierInvoiceControlID, pSupplierReceiptControlID) { //pSupplierControl is a control not ID so dont use #
    debugger;
    //if ($(pSupplierInvoiceControlID).val() == "" && $(pSupplierReceiptControlID).val() == "") {
    if ($(pSupplierInvoiceControlID).val() == "") {
        $(pSupplierControl).removeAttr("disabled");
    }
    else {
        $(pSupplierControl).attr("disabled", "disabled");
    }
}
function CalculatePayablesAmount() {
    var decAmountWithoutVAT = 0;
    var decTaxAmount = 0; var decTaxPercentage = 0.0;
    var decDiscountAmount = 0; var decDiscountPercentage = 0.0;

    decAmountWithoutVAT = $("#txtPayableQuantity").val() * $("#txtPayableUnitPrice").val();
    $("#txtPayableAmountWithoutVAT").val(decAmountWithoutVAT);
    decTaxPercentage = $("#slPayableTax option:selected").attr("CurrentPercentage");
    decTaxAmount = decAmountWithoutVAT * decTaxPercentage / 100;
    decTaxAmount = Math.round((decTaxAmount + Number.EPSILON) * 100) / 100; //Approximate 2 decimal places
    decDiscountPercentage = $("#slPayableDiscount option:selected").attr("CurrentPercentage");
    decDiscountAmount = decAmountWithoutVAT * decDiscountPercentage / 100;
    decDiscountAmount = Math.round((decDiscountAmount + Number.EPSILON) * 100) / 100; //Approximate 2 decimal places
    $("#txtPayableTaxPercentage").val(decTaxPercentage);
    $("#txtPayableTaxAmount").val(decTaxAmount.toFixed(2));
    $("#txtPayableDiscountPercentage").val(decDiscountPercentage);
    $("#txtPayableDiscountAmount").val(decDiscountAmount.toFixed(2));
    $("#txtPayableAmount").val((decAmountWithoutVAT + decTaxAmount - decDiscountAmount).toFixed(2));
}
//if not insert (i.e. update then all will rows will be selected)
function Payables_Row_CalculatePayablesAmount(pRowID, pIsInsert) {
    var rowQuantity = $("#txtTblModalPayableQuantity" + pRowID).val();
    var rowCostPrice = $("#txtTblModalPayableCostPrice" + pRowID).val();

    var decAmountWithoutVAT = 0;
    var decTaxAmount = 0; var decTaxPercentage = 0.0;
    var decDiscountAmount = 0; var decDiscountPercentage = 0.0;

    decAmountWithoutVAT = rowQuantity * rowCostPrice;
    $("#txtTblModalPayableCostAmountWithoutVAT" + pRowID).val(decAmountWithoutVAT);
    decTaxPercentage = $("#slPayableTax" + pRowID + " option:selected").attr("CurrentPercentage");
    decTaxAmount = decAmountWithoutVAT * decTaxPercentage / 100;
    decDiscountPercentage = $("#slPayableDiscount" + pRowID + " option:selected").attr("CurrentPercentage");
    decDiscountAmount = decAmountWithoutVAT * decDiscountPercentage / 100;
    $("#txtTblModalPayableTaxPercentage" + pRowID).val(decTaxPercentage);
    $("#txtTblModalPayableTaxAmount" + pRowID).val(decTaxAmount.toFixed(2));
    $("#txtTblModalPayableDiscountPercentage" + pRowID).val(decDiscountPercentage);
    $("#txtTblModalPayableDiscountAmount" + pRowID).val(decDiscountAmount.toFixed(2));
    $("#txtTblModalPayableCostAmount" + pRowID).val((decAmountWithoutVAT + decTaxAmount - decDiscountAmount).toFixed(2));

    if (pIsInsert) //incase of insert check and uncheck the checkbox to in GetSelectedIDs incase of insert
        Payables_txtTblModalCostAmount_Changed(pRowID, pIsInsert);
}
function PayableCurrency_GetList(pID, pSlName) {
    //parameters: ID, strFnName, First Row in select list, select list name
    GetListCurrencyWithCodeAndExchangeRateAttr(pID, "/api/Currencies/LoadAll", "Cur", pSlName, " WHERE 1=1 ORDER BY Code ");
}
//function PayablePOrC_GetList(pID) {
//    //parameters: ID, strFnName, First Row in select list, select list name
//    GetListWithCode(pID, "/api/NoAccessFreightTypes/LoadAll", "Select P/C", "slPayablePOrC");
//}
function PayablePOrC_GetList(pID, pSlName) {
    //parameters: ID, strFnName, First Row in select list, select list name
    GetListWithCodeAndWhereClause(pID, "/api/NoAccessFreightTypes/LoadAll", "P/C", pSlName, " WHERE 1=1 ");
}
function PayableSuppliers_GetList(pID, pSlName, pCallback) {
    var pWhereClause = " WHERE OperationID = " + $("#hOperationID").val();
    pWhereClause += " AND PartnerID IS NOT NULL ";
    //pWhereClause += " AND PartnerTypeID != " + constCustomerPartnerTypeID;
    pWhereClause += " ORDER BY PartnerTypeName ";
    //parameters: ID, strFnName, First Row in select list, select list name, whereClause, callback
    //GetListWithCodeAndNameAndWhereClause(pID, "/api/OperationPartners/LoadAll", "Select Supplier", pSlName, pWhereClause);
    GetListOperationPartnersWithPartnerTypeIDAndPartnerIDAttrAndWhereClause(pID, "/api/OperationPartners/LoadAll", TranslateString("SelectFromMenu"), pSlName, pWhereClause, pCallback);
}
function PayableUOM_GetList(pID, pSlName) {
    var pWhereClause = "";
    //pWhereClause += " Where OperationID = " + $("#hOperationID").val();
    if ($("#hShipmentType").val() == constFCLShipmentType)
        pWhereClause += " WHERE IsUsedInFCl = 1 ";
    else
        if ($("#hShipmentType").val() == constLCLShipmentType)
            pWhereClause += " WHERE IsUsedInLCL = 1 ";
        else
            if ($("#hShipmentType").val() == constFTLShipmentType)
                pWhereClause += " WHERE IsUsedInFTL = 1 ";
            else
                if ($("#hShipmentType").val() == constLTLShipmentType)
                    pWhereClause += " WHERE IsUsedInLTL = 1 ";
                else
                    if ($("#hShipmentType").val() == constConsolidationShipmentType)
                        pWhereClause += " WHERE IsUsedInConsolidation = 1 ";
                    else
                        if ($("#hShipmentType").val() == "0")
                            pWhereClause += " WHERE IsUsedInAir = 1 ";
    pWhereClause += " OR ID = " + pID; //for the case that settings changed for a UOM at the time it is taken 
    pWhereClause += " ORDER BY Code ";
    //GetListWithCodeAndNameAndWhereClause(pID, "/api/NoAccessMeasurements/LoadAll", "Select UOM", pSlName, pWhereClause);
    GetListWithNameAndWhereClause(pID, "/api/NoAccessMeasurements/LoadAll", "Select UOM", pSlName, pWhereClause);
}
//to get total of each currecy  //the parameter is the pSelectedPayableIDs (0 unless called from Docs when need to print only selected payables)
function Payables_CalculateSubtotals(pSelectedPayableIDs) {
    debugger;
    var pParametersWithValues = {
        "pOperationID": $("#hOperationID").val()
        , "pSelectedPayableIDs": "0" //to print all
    };
    var htmlLblPayablesSubTotals = ""
    CallGETFunctionWithParameters("/api/Payables/GetPayablesSubTotals"
            , pParametersWithValues
            , function (data) {
                var pProfitList = JSON.parse(data[2]);
                var pHTMLProfitSubtotals = "";
                if (data != "") {
                    debugger;
                    //Estimated Cost
                    for (var i = 0 ; i < data[1]; i++) {
                        htmlLblPayablesSubTotals += (i == 0 ? "" : " , ") + JSON.parse(data[0])[i].QuotationCostSubTotal.toFixed(2) + " " + JSON.parse(data[0])[i].CurrencyCode;
                    }
                    if ($("#hf_ChangeLanguage").val() == "ar")
                        $("#lblPayablesSubTotals").html("<u>إجمالي العملات المقدرة: </u><br/>" + htmlLblPayablesSubTotals);
                    else {
                        $("#lblPayablesSubTotals").html("<u>Currencies Estimated Totals:</u><br/>" + htmlLblPayablesSubTotals);
                    }
                    //Actual Cost Without VAT
                    htmlLblPayablesSubTotals = "";
                    for (var i = 0 ; i < data[1]; i++) {
                        htmlLblPayablesSubTotals += (i == 0 ? "" : " , ") + JSON.parse(data[0])[i].AmountWithoutVATSubTotal.toFixed(2) + " " + JSON.parse(data[0])[i].CurrencyCode;
                    }
                    if ($("#hf_ChangeLanguage").val() == "ar")
                        $("#lblPayablesSubTotals").append("<br><br><u>إجمالي العملات الفعلية بدون ضريبة: </u><br/>" + htmlLblPayablesSubTotals);
                    else
                        $("#lblPayablesSubTotals").append("<br><br><u>Currencies Actual Totals W/O VAT:</u><br/>" + htmlLblPayablesSubTotals);

                    //Actual Cost With VAT
                    htmlLblPayablesSubTotals = "";
                    for (var i = 0 ; i < data[1]; i++) {
                        htmlLblPayablesSubTotals += (i == 0 ? "" : " , ") + JSON.parse(data[0])[i].CostAmountSubTotal.toFixed(2) + " " + JSON.parse(data[0])[i].CurrencyCode;
                    }
                    if ($("#hf_ChangeLanguage").val() == "ar")
                        $("#lblPayablesSubTotals").append("<br><br><u>إجمالي العملات الفعلية بالضريبة: </u><br/>" + htmlLblPayablesSubTotals);
                    else
                        $("#lblPayablesSubTotals").append("<br><br><u>Currencies Actual Totals With VAT:</u><br/>" + htmlLblPayablesSubTotals);
                    for (var i = 0 ; i < pProfitList.length; i++) {
                        pHTMLProfitSubtotals += (i == 0 ? "" : " , ") + pProfitList[i].AmountWithVAT.toFixed(2) + " " + pProfitList[i].CurrencyCode;
                    }
                    if (OEPay && OERec && ODDoc) {
                        $("#lblProfitSubTotalsInPayables").html("<br><u>"+ TranslateString("Profit")+":</u><br/>" + pHTMLProfitSubtotals);
                        $("#lblProfitSubTotalsInReceivables").html("<br><u>" + TranslateString("Profit") + ":</u><br/>" + pHTMLProfitSubtotals);
                    }
                }
            });
    return $("#lblPayablesSubTotals").html();
}
//used by both Payables and Receivables
function PayablesAndReceivables_CalculateSummary() {
    var decTotalQuotationCost = 0;
    var decTotalCostWithoutVAT = 0;
    var decTotalCostWithVAT = 0;
    var decTotalSaleWithoutVAT = 0;
    var decTotalSaleWithVAT = 0;
    var decProfit = 0;
    $(".PayableAmountWithoutVAT").each(function () { //calculated from w/o VAT coz receivables still not taken its VAT (done in invoices)
        var value = $(this).text();
        var valExchangeRate = $(this.parentElement.getElementsByClassName('PayableExchangeRate')).text();
        // add only if the value is number
        if (!isNaN(value) && value.length != 0) {
            decTotalCostWithoutVAT += parseFloat(value) * parseFloat(valExchangeRate);
        }
    });
    $(".PayableCostAmount").each(function () { //calculated from w/o VAT coz receivables still not taken its VAT (done in invoices)
        var value = $(this).text();
        var valExchangeRate = $(this.parentElement.getElementsByClassName('PayableExchangeRate')).text();
        // add only if the value is number
        if (!isNaN(value) && value.length != 0) {
            decTotalCostWithVAT += parseFloat(value) * parseFloat(valExchangeRate);
        }
    });
    $(".PayableQuotationCost").each(function () { //calculated from w/o VAT coz receivables still not taken its VAT (done in invoices)
        var value = $(this).text();
        var valExchangeRate = $(this.parentElement.getElementsByClassName('PayableExchangeRate')).text();
        // add only if the value is number
        if (!isNaN(value) && value.length != 0) {
            decTotalQuotationCost += parseFloat(value) * parseFloat(valExchangeRate);
        }
    });
    debugger;
    $(".ReceivableAmountWithoutVAT").each(function () {
        var value = $(this).text();
        var valExchangeRate = $(this.parentElement.getElementsByClassName('ReceivableExchangeRate')).text();
        // add only if the value is number
        if (!isNaN(value) && value.length != 0) {
            decTotalSaleWithoutVAT += parseFloat(value) * parseFloat(valExchangeRate);
        }
    });
    $(".ReceivableSaleAmount").each(function () {
        var value = $(this).text();
        var valExchangeRate = $(this.parentElement.getElementsByClassName('ReceivableExchangeRate')).text();
        // add only if the value is number
        if (!isNaN(value) && value.length != 0) {
            decTotalSaleWithVAT += parseFloat(value) * parseFloat(valExchangeRate);
        }
    });

    $("#lblEstimatedPayablesInPayables").html(": " + decTotalQuotationCost.toFixed(2).toString() + " " + $("#hDefaultCurrencyCode").val());
    $("#lblEstimatedPayablesInReceivables").html(": " + decTotalQuotationCost.toFixed(2).toString() + " " + $("#hDefaultCurrencyCode").val());

    $("#lblPayablesWithoutVATInPayables").html(": " + decTotalCostWithoutVAT.toFixed(2).toString() + " " + $("#hDefaultCurrencyCode").val());
    $("#lblPayablesWithoutVATInReceivables").html(": " + decTotalCostWithoutVAT.toFixed(2).toString() + " " + $("#hDefaultCurrencyCode").val());

    $("#lblPayablesWithVATInPayables").html(": " + decTotalCostWithVAT.toFixed(2).toString() + " " + $("#hDefaultCurrencyCode").val());
    $("#lblPayablesWithVATInReceivables").html(": " + decTotalCostWithVAT.toFixed(2).toString() + " " + $("#hDefaultCurrencyCode").val());

    $("#lblReceivablesWithoutVATInPayables").html(": " + decTotalSaleWithoutVAT.toFixed(2).toString() + " " + $("#hDefaultCurrencyCode").val());
    $("#lblReceivablesWithoutVATInReceivables").html(": " + decTotalSaleWithoutVAT.toFixed(2).toString() + " " + $("#hDefaultCurrencyCode").val());

    //$("#lblReceivablesWithVATInPayables").html(": " + decTotalSaleWithVAT.toFixed(2).toString() + " " + $("#hDefaultCurrencyCode").val());
    //$("#lblReceivablesWithVATInReceivables").html(": " + decTotalSaleWithVAT.toFixed(2).toString() + " " + $("#hDefaultCurrencyCode").val());
    if (OEPay && OERec && ODDoc) {
        $("#lblProfitInPayables").html(": " + (decTotalSaleWithoutVAT - decTotalCostWithoutVAT).toFixed(2).toString() + " " + $("#hDefaultCurrencyCode").val());
        $("#lblProfitInReceivables").html(": " + (decTotalSaleWithoutVAT - decTotalCostWithoutVAT).toFixed(2).toString() + " " + $("#hDefaultCurrencyCode").val());
    }
    if (decTotalSaleWithoutVAT < decTotalCostWithoutVAT) {
        $("#lblProfitInPayables").removeClass("static-text-primary");
        $("#lblProfitInPayables").addClass("static-text-danger");
        $("#lblProfitInReceivables").removeClass("static-text-primary");
        $("#lblProfitInReceivables").addClass("static-text-danger");
    }
    else {
        $("#lblProfitInPayables").addClass("static-text-primary");
        $("#lblProfitInPayables").removeClass("static-text-danger");
        $("#lblProfitInReceivables").addClass("static-text-primary");
        $("#lblProfitInReceivables").removeClass("static-text-danger");
    }
}
/***************************Flexi Payables*****************************/
function Payables_FillFlexiPayableModal() {
    debugger;
    FadePageCover(true);
    all_has_store = false;
    RowsCounter = 0;

    $('#tblItems > tbody').html('');
    $('#txtDate').val(getTodaysDateInddMMyyyyFormat());
    //$("#hID").val("");
    //$.ajax({
    //    type: "GET",
    //    url: strServerURL + "api/SC_Transactions/IntializeData",
    //    data: { pTransactionTypeID: "20", pID: null },
    //    contentType: "application/json; charset=utf-8",
    //    dataType: "json",
    //    success: function (d) {

    //        Fill_SelectInputAfterLoadData_WithAttr(d[0], 'ID', 'Name', '<-- select Items -->', '#hidden_slItems', '', 'ItemUnits');
    //        //Fill_SelectInputAfterLoadData(d[0], 'ID', 'Name', '<-- select Items -->', '#hidden_slItems', '');
    //        Fill_SelectInputAfterLoadData(d[3], 'ID', 'StoreName', '<-- select Store -->', '#slStores', '');
    //        Fill_SelectInputAfterLoadData(d[5], 'ID', 'Name', '<-- SELECT UNIT -->', '#hidden_slUnits', '');

    //        FadePageCover(false);
    //    },
    //    error: function (jqXHR, exception) {
    //        debugger;
    //        swal("Oops!", "Please, contact your technical support!  this is print region !", "error");
    //        FadePageCover(false);
    //    }
    //});
    FadePageCover(true);
    var pParametersWithValues = {
        pOperationID: $("#hOperationID").val()
    };
    CallGETFunctionWithParameters("/api/Payables/Payables_FillFlexiPayableModal", pParametersWithValues
        , function (pData) {
            //Fill Controls here from the returned data

            Fill_SelectInputAfterLoadData_WithAttr(pData[0], 'ID', 'Name', '<-- select Items -->', '#hidden_slItems', '', 'ItemUnits');
            //Fill_SelectInputAfterLoadData(d[0], 'ID', 'Name', '<-- select Items -->', '#hidden_slItems', '');
            Fill_SelectInputAfterLoadData(pData[1], 'ID', 'StoreName', '<-- select Store -->', '#slStores', '');
            Fill_SelectInputAfterLoadData(pData[2], 'ID', 'Name', '<-- SELECT UNIT -->', '#hidden_slUnits', '');
            FadePageCover(false);
        });
}
function Payables_SaveFlexiPayable() {
    debugger;
    IsInsert = true;
    $($('#tblItems > tbody > tr')).each(function (i, tr) {
        debugger;
        var storeid = $(tr).find('td.StoreID ').find('.selectstore').val();
        var itemid = $(tr).find('td.ItemID ').find('.selectitem').val();
        var quantityid = $(tr).find('td.Qty ').find('.inputquantity').val();
        if (storeid.trim() == "0" || itemid.trim() == "0" || quantityid.trim() == "") {

            all_has_store = false;
            return false;
        }
        else {

            all_has_store = true;
        }
    });
    setTimeout(function () {

        // $('.selectstore').html($('#slStores').html());

        if ($('#tblItems > tbody > tr').length == 0) {
            swal('Excuse me', 'Fill Items', 'warning');
        }
        else if (!all_has_store) {

            swal('Excuse me', 'Fill All Items , Quantity , Stores', 'warning');

        }
        else if (($('#txtDate').val().trim() != "" && isValidDate($('#txtDate').val(), 1))) {
            debugger;
            InsertUpdateFunctionAndReturnID("form", "/api/SC_Transactions/Insert", {
                pCode: "0",
                pCodeManual: "0",
                pTransactionDate: ConvertDateFormat($('#txtDate').val()),
                pPurchaseInvoiceID: "0",
                pPurchaseOrderID: "0",
                pExaminationID: "0",
                pIsApproved: false,
                pNotes: ($("#txtNotes").val() == "" || $("#txtNotes").val() == null ? "0" : $("#txtNotes").val()),
                pSLInvoiceID: "0",
                pDepartmentID: "0",
                pClientID: "0",
                pCostCenterID: "0",
                pIsSpareParts: "false",
                pFiscalYearID: "0",
                pSupplierID: "0",
                pParentID: "0",
                pTransactionTypeID: "20",
                pJV_ID: "0", pIsOutOfStore: "false",
                pMaterialIssueRequesitionsID: "0",
                pToStoreID: 0,
                pP_ProductionRequestID: 0,
                pP_UnitID: 0,
                pP_ItemID: 0,
                pP_LineID: 0,
                pP_Qty: 0,
                pP_FinishedDate: "01/01/1800",
                pP_StartDate: "01/01/1800",
                pEntitlementDays: 0,
                pIsClosed: false,
                pFromStore: 0,
                pJV_ID2: 0,
                pTransferParentID: 0,
                pForwardingPSInvoiceID: 0
            }, false, null, '#hID', function () {

                InsertUpdateFunction2("form", "/api/SC_Transactions/InsertPayablesItems",
                    JSON.stringify(SetArrayOfItems())
                    , false, "FlexiPayablesModal", function (Code) {
                        //$('#txtCode').val(Code[1]);
                        //PrintTransaction();
                        setTimeout(function () {
                            Payables_LoadWithPagingWithWhereClause($("#hOperationID").val()); // SC_Transactions_LoadingWithPaging();
                            IntializeData();
                            ClearAllTableRows('tblItems');
                            all_has_store = false;

                        }, 300);

                    });
            });
        }
        else {
            swal(strSorry, "Please make sure that date format is dd/MM/yyyy.");
        }
    }, 30);


}
/****************************************PricingCharges Fns***************************************/
function PricingCharges_BindTableRows(pPricing, pPricingCharge, pPricingTypeName) {
    debugger;
    //var copyControlsText = " class='btn btn-xs btn-rounded btn-info float-right' > <i class='fa fa-copy' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + TranslateString("Copy") + "</span>";
    //var NotificationControlsText = " class='btn btn-xs btn-rounded btn-lightblue float-right' > <i class='fa fa-bell' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + "Notif." + "</span>";
    //$("#hl-menu-PricingModule").parent().siblings().removeClass("active");
    //$("#hl-menu-PricingModule").parent().addClass("active");
    ClearAllTableRows("tblPricing" + pPricingTypeName);
    $("#tblPricing" + pPricingTypeName + " thead tr").html("");
    $(".classShowFor" + pPricingTypeName).removeClass("hide");
    //$("#tblPricing" + pPricingTypeName + " thead tr").append('<th id="HeaderDeletePricingID"><input id="cbPricingDeleteHeader"' + pPricingTypeName + ' type="checkbox" /></th>');
    $("#tblPricing" + pPricingTypeName + " thead tr").append('<th id="HeaderDeletePricingID"' + pPricingTypeName + '></th>');
    $("#tblPricing" + pPricingTypeName + " thead tr").append('<th class="hide">Trans.</th>');
    $("#tblPricing" + pPricingTypeName + " thead tr").append('<th>Supplier</th>');
    $("#tblPricing" + pPricingTypeName + " thead tr").append('<th>POLCountry</th>');
    $("#tblPricing" + pPricingTypeName + " thead tr").append('<th>POL</th>');
    $("#tblPricing" + pPricingTypeName + " thead tr").append('<th>PODCountry</th>');
    $("#tblPricing" + pPricingTypeName + " thead tr").append('<th>POD</th>');
    $("#tblPricing" + pPricingTypeName + " thead tr").append('<th>Equip</th>');
    $("#tblPricing" + pPricingTypeName + " thead tr").append('<th>Commodity</th>');
    $("#tblPricing" + pPricingTypeName + " thead tr").append('<th>TT</th>');
    $("#tblPricing" + pPricingTypeName + " thead tr").append('<th>Freq</th>');
    $("#tblPricing" + pPricingTypeName + " thead tr").append('<th class="">ValidFrom</th>');
    $("#tblPricing" + pPricingTypeName + " thead tr").append('<th>ValidTo</th>');
    $("#tblPricing" + pPricingTypeName + " thead tr").append('<th>Cur</th>');
    for (var i = 1 ; i <= $("#slPricingSettings" + pPricingTypeName + " option").length; i++) {
        $("#tblPricing" + pPricingTypeName + " thead tr").append('<th>' + $("#slPricingSettings" + pPricingTypeName + " :nth-child(" + i + ")").text() + '</th>');
    }
    $("#tblPricing" + pPricingTypeName + " thead tr").append('<th class="hide">TotalRowCost</th>');
    $("#tblPricing" + pPricingTypeName + " thead tr").append('<th class="hide">Save</th>');
    $("#tblPricing" + pPricingTypeName + " thead tr").append('<th class="rounded-right hide"></th>');
    //maxPricingIDInTable = 0;
    var _LeastRowCostPrice = 99999999999; //set to a very big number
    var _BestRecommendationRowID = 0;
    $.each(pPricing, function (i, item) {
        //maxPricingIDInTable = (item.ID > maxPricingIDInTable ? item.ID : maxPricingIDInTable);
        var tr = "";
        //tr += "<tr ID='" + item.ID + "' ondblclick='Pricing_EditByDblClick(" + item.ID + ");' >";
        tr += "<tr ID='" + item.ID + "' style='font-size:110%;' " + (item.IsPricingRequest ? "class='text-primary'" : "") + ">";
        tr += "     <td class='IsInsert hide'> <input type='checkbox' id='IsInsert" + item.ID + "' /></td>";
        tr += "     <td class='PricingID'> <input name='namePricingSelect' type='checkbox' value='" + item.ID + "' /></td>";
        //tr += '     <td class="PricingTypeID " val=' + item.PricingTypeID + '><select id="slPricingType' + item.ID + '" class="controlStyle" onfocus="DisableBackspaceKey(id);" onkeydown="DisableBackspaceKey(id);" onkeypress="DisableBackspaceKey(id);" onchange="Pricing_SetIsRowChanged(id); Pricing_PricingTypeChanged(' + "'" + "slPricingType" + item.ID + "','slSupplier" + item.ID + "'," + 0 + ');" data-required="false"></select></td>';
        tr += '     <td class="PricingTypeID hide" val=' + item.PricingTypeID + '><p id="cellPricingType' + item.ID + '" ondblclick="Pricing_EnterEditModeForSL(' + "'" + 'PricingType' + "'," + item.ID + ',' + item.IsPricingRequest + ');">' + (item.PricingTypeID == 0 ? "N/A" : item.PricingTypeCode) + '</p><select id="slPricingType' + item.ID + '" style="width:150px;" class="controlStyle hide" onfocus="DisableBackspaceKey(id);" onkeydown="DisableBackspaceKey(id);" onkeypress="DisableBackspaceKey(id);" onchange="Pricing_SetIsRowChanged(id); Pricing_PricingTypeChanged(' + "'" + "slPricingType" + item.ID + "','slSupplier" + item.ID + "'," + 0 + ');" data-required="false">' + "<option value=" + (item.PricingTypeID == 0 ? "" : item.PricingTypeID) + "></option>" + '</select></td>';
        tr += "     <td class='SupplierID' val='" + item.SupplierID + "'><p id='cellSupplier" + item.ID + "' ondblclick='Pricing_EnterEditModeForSL(" + '"' + "Supplier" + '",' + item.ID + "," + item.IsPricingRequest + ");'>" + (item.SupplierID == 0 ? "N/A" : item.SupplierName) + "</p><select hide id='slSupplier" + item.ID + "' style='width:150px;' class='controlStyle hide' onfocus='DisableBackspaceKey(id);' onkeydown='DisableBackspaceKey(id);' onkeypress='DisableBackspaceKey(id);' onchange='Pricing_SetIsRowChanged(id);' data-required='false'>" + "<option value=" + (item.SupplierID == 0 ? "" : item.SupplierID) + "></option>" + "</select></td>";
        tr += '     <td class="POLCountryID" val=' + item.POLCountryID + '><p id="cellPOLCountry' + item.ID + '" ondblclick="Pricing_EnterEditModeForSL(' + "'" + 'POLCountry' + "'," + item.ID + ',' + item.IsPricingRequest + ');">' + (item.POLCountryID == 0 ? "N/A" : item.POLCountryName) + '</p><select id="slPOLCountry' + item.ID + '" style="width:150px;" class="controlStyle hide" onfocus="DisableBackspaceKey(id);" onkeydown="DisableBackspaceKey(id);" onkeypress="DisableBackspaceKey(id);" onchange="Pricing_SetIsRowChanged(id); Pricing_FillPorts(' + "'" + "slPOLCountry" + item.ID + "','slPOL" + item.ID + "'," + 0 + ');" data-required="false">' + "<option value=" + (item.POLCountryID == 0 ? "" : item.POLCountryID) + "></option>" + '</select></td>';
        tr += '     <td class="POLID" val=' + item.POLID + '><p id="cellPOL' + item.ID + '" ondblclick="Pricing_EnterEditModeForSL(' + "'" + 'POL' + "'," + item.ID + ',' + item.IsPricingRequest + ');">' + (item.POLID == 0 ? "N/A" : item.POLName) + '</p><select id="slPOL' + item.ID + '" style="width:150px;" class="controlStyle hide" onfocus="DisableBackspaceKey(id);" onkeydown="DisableBackspaceKey(id);" onkeypress="DisableBackspaceKey(id);" onchange="Pricing_SetIsRowChanged(id);" data-required="false">' + "<option value=" + (item.POLID == 0 ? "" : item.POLID) + "></option>" + '</select></td>';
        tr += '     <td class="PODCountryID" val=' + item.PODCountryID + '><p id="cellPODCountry' + item.ID + '" ondblclick="Pricing_EnterEditModeForSL(' + "'" + 'PODCountry' + "'," + item.ID + ',' + item.IsPricingRequest + ');">' + (item.PODCountryID == 0 ? "N/A" : item.PODCountryName) + '</p><select id="slPODCountry' + item.ID + '" style="width:150px;" class="controlStyle hide" onfocus="DisableBackspaceKey(id);" onkeydown="DisableBackspaceKey(id);" onkeypress="DisableBackspaceKey(id);" onchange="Pricing_SetIsRowChanged(id); Pricing_FillPorts(' + "'" + "slPODCountry" + item.ID + "','slPOD" + item.ID + "'," + 0 + ');" data-required="false">' + "<option value=" + (item.PODCountryID == 0 ? "" : item.PODCountryID) + "></option>" + '</select></td>';
        tr += '     <td class="PODID" val=' + item.PODID + '><p id="cellPOD' + item.ID + '" ondblclick="Pricing_EnterEditModeForSL(' + "'" + 'POD' + "'," + item.ID + ',' + item.IsPricingRequest + ');">' + (item.PODID == 0 ? "N/A" : item.PODName) + '</p><select id="slPOD' + item.ID + '" style="width:150px;" class="controlStyle hide" onfocus="DisableBackspaceKey(id);" onkeydown="DisableBackspaceKey(id);" onkeypress="DisableBackspaceKey(id);" onchange="Pricing_SetIsRowChanged(id);" data-required="false">' + "<option value=" + (item.PODID == 0 ? "" : item.PODID) + "></option>" + '</select></td>';
        tr += "     <td class='EquipmentID' val='" + item.EquipmentID + "'><p id='cellEquipment" + item.ID + "' ondblclick='Pricing_EnterEditModeForSL(" + '"' + "Equipment" + '",' + item.ID + "," + item.IsPricingRequest + ");'>" + (item.EquipmentID == 0 ? "N/A" : item.ContainerTypeCode) + "</p><select hide id='slEquipment" + item.ID + "' style='width:auto;' class='controlStyle hide' onfocus='DisableBackspaceKey(id);' onkeydown='DisableBackspaceKey(id);' onkeypress='DisableBackspaceKey(id);' onchange='Pricing_SetIsRowChanged(id);' data-required='false'>" + "<option value=" + (item.EquipmentID == 0 ? "" : item.EquipmentID) + "></option>" + "</select></td>";
        tr += "     <td class='CommodityID' val='" + item.CommodityID + "'><p id='cellCommodity" + item.ID + "' ondblclick='Pricing_EnterEditModeForSL(" + '"' + "Commodity" + '",' + item.ID + "," + item.IsPricingRequest + ");'>" + (item.CommodityID == 0 ? "N/A" : item.CommodityName) + "</p><select hide id='slCommodity" + item.ID + "' style='width:auto;' class='controlStyle hide' onfocus='DisableBackspaceKey(id);' onkeydown='DisableBackspaceKey(id);' onkeypress='DisableBackspaceKey(id);' onchange='Pricing_SetIsRowChanged(id);' data-required='false'>" + "<option value=" + (item.CommodityID == 0 ? "" : item.CommodityID) + "></option>" + "</select></td>";
        tr += "     <td class='TransitTime'><p id='cellTransitTime" + item.ID + "' ondblclick='Pricing_EnterEditModeForTxt(" + '"' + "TransitTime" + '",' + item.ID + "," + item.IsPricingRequest + ");'>" + (item.TransitTime) + "</p><input type='text' style='width:30px;' id='txtTransitTime" + item.ID + "' class='form-control controlStyle hide' data-type='number'  onkeypress='CheckValueIsInteger(id);' onfocus='CheckValueIsInteger(id);' onchange='Pricing_SetIsRowChanged(id);' data-required='false' value='" + item.TransitTime + "' /> </td>";
        tr += "     <td class='Frequency'><p id='cellFrequency" + item.ID + "' ondblclick='Pricing_EnterEditModeForTxt(" + '"' + "Frequency" + '",' + item.ID + "," + item.IsPricingRequest + ");'>" + (item.Frequency) + "</p><input type='text' style='width:30px;' id='txtFrequency" + item.ID + "' class='form-control controlStyle hide' data-type='number'  onkeypress='CheckValueIsInteger(id);' onfocus='CheckValueIsInteger(id);' onchange='Pricing_SetIsRowChanged(id);' data-required='false' value='" + item.Frequency + "' /> </td>";
        tr += "     <td class='FrequencyNotes hide'><p id='cellFrequencyNotes" + item.ID + "' ondblclick='Pricing_EnterEditModeForTxt(" + '"' + "FrequencyNotes" + '",' + item.ID + "," + item.IsPricingRequest + ");'>" + (item.FrequencyNotes == 0 ? " " : item.FrequencyNotes) + "</p><input type='text' style='width:30px;' id='txtFrequencyNotes" + item.ID + "' class='form-control controlStyle hide' data-type='number'  onkeypress='CheckValueIsInteger(id);' onfocus='CheckValueIsInteger(id);' onchange='Pricing_SetIsRowChanged(id);' data-required='false' value='" + item.FrequencyNotes + "' /> </td>";
        tr += '     <td class="ValidFrom"><p id="cellValidFrom' + item.ID + '" ondblclick="Pricing_EnterEditModeForTxt(' + "'" + 'ValidFrom' + "'," + item.ID + ',' + item.IsPricingRequest + ');">' + (ConvertDateFormat(GetDateWithFormatMDY(item.ValidFrom))) + '</p><input id="txtValidFrom' + item.ID + '" style="width:85px;height:100%;" readonly="readonly" type="text" onfocus="DisableEnterKey(id);" onkeypress="DisableEnterKey(id);" onchange="Pricing_SetIsRowChanged(id);" style="background-color:white; cursor:text;" class="input-sm datepicker-input form-control hide" data-date-format="dd/mm/yyyy" placeholder="Select Date" value="' + ConvertDateFormat(GetDateWithFormatMDY(item.ValidFrom)) + '" /></td>';
        tr += '     <td class="ValidTo"><p id="cellValidTo' + item.ID + '" ondblclick="Pricing_EnterEditModeForTxt(' + "'" + 'ValidTo' + "'," + item.ID + ',' + item.IsPricingRequest + ');">' + (ConvertDateFormat(GetDateWithFormatMDY(item.ValidTo))) + '</p><input id="txtValidTo' + item.ID + '" style="width:85px;height:100%;" readonly="readonly" type="text" onfocus="DisableEnterKey(id);" onkeypress="DisableEnterKey(id);" onchange="Pricing_SetIsRowChanged(id);" style="background-color:white; cursor:text;" class="input-sm datepicker-input form-control hide" data-date-format="dd/mm/yyyy" placeholder="Select Date" value="' + ConvertDateFormat(GetDateWithFormatMDY(item.ValidTo)) + '" /></td>';
        tr += "     <td class='CurrencyID' val='" + item.CurrencyID + "'><p id='cellCurrency" + item.ID + "' ondblclick='Pricing_EnterEditModeForSL(" + '"' + "Currency" + '",' + item.ID + "," + item.IsPricingRequest + ");'>" + (item.CurrencyID == 0 ? "N/A" : item.CurrencyCode) + "</p><select hide id='slCurrency" + item.ID + "' style='width:auto;' class='controlStyle hide' onfocus='DisableBackspaceKey(id);' onkeydown='DisableBackspaceKey(id);' onkeypress='DisableBackspaceKey(id);' onchange='Pricing_SetIsRowChanged(id);' data-required='false'>" + "<option value=" + (item.CurrencyID == 0 ? "" : item.CurrencyID) + "></option>" + "</select></td>";
        var _CurrentRowCost = 0.0;
        for (var i = 1 ; i <= $("#slPricingSettings" + pPricingTypeName + " option").length; i++) {
            var pChargeTypeName = $("#slPricingSettings" + pPricingTypeName + " :nth-child(" + i + ")").text();
            var pChargeTypeID = $("#slPricingSettings" + pPricingTypeName + " :nth-child(" + i + ")").val();
            var pPricingChargeID = pPricingCharge.find(x => x.PricingID == item.ID && x.ChargeTypeID == pChargeTypeID) == undefined ? 0 : pPricingCharge.find(x => x.PricingID === item.ID && x.ChargeTypeID == pChargeTypeID).ID;
            var pCostPrice = pPricingCharge.find(x => x.PricingID == item.ID && x.ChargeTypeID == pChargeTypeID) == undefined ? 0 : pPricingCharge.find(x => x.PricingID === item.ID && x.ChargeTypeID == pChargeTypeID).CostPrice;
            //var pChargeTypeNameWithOnlyCharsAndNos = pChargeTypeName.replace(/\s/g, "").replace(/\//g, '').replace(/\\/g, '').replace(/&/g, '').replace(/%/g, '').replace(/\$/g, '').replace(/\(/g, '').replace(/\)/g, ''); //remove spaces,slashes,backslashes
            var pChargeTypeNameWithOnlyCharsAndNos = pChargeTypeName.replace(/[^\w]/g, '');
            _CurrentRowCost += pCostPrice;
            tr += "     <td class='" + pChargeTypeNameWithOnlyCharsAndNos + "'><p id='cell" + pChargeTypeNameWithOnlyCharsAndNos + item.ID + "' ondblclick='Pricing_EnterEditModeForTxt(" + '"' + pChargeTypeNameWithOnlyCharsAndNos + '",' + item.ID + ");'>" + pCostPrice + "</p><input type='text' style='width:60px;' id='txt" + pChargeTypeNameWithOnlyCharsAndNos + item.ID + "' PricingChargeID=" + pPricingChargeID + " class='form-control controlStyle hide' data-type='number' onkeypress='CheckValueIsDecimal(id);' onfocus='CheckValueIsDecimal(id);' onblur='CheckDecimalFormat(id);' onchange='Pricing_SetIsRowChanged(id);' data-required='false' value='" + pCostPrice.toFixed(4) + "' /> </td>";
        }
        tr += "     <td class='Notes hide'><p id='cellNotes" + item.ID + "' ondblclick='Pricing_EnterEditModeForTxt(" + '"' + "Notes" + '",' + item.ID + "," + item.IsPricingRequest + ");'>" + (item.Notes == 0 ? " " : item.Notes) + "</p><input type='text' style='width:30px;' id='txtNotes" + item.ID + "' class='form-control controlStyle hide' data-type='number'  onkeypress='CheckValueIsInteger(id);' onfocus='CheckValueIsInteger(id);' onchange='Pricing_SetIsRowChanged(id);' data-required='false' value='" + item.Notes + "' /> </td>";
        tr += "     <td class='TotalRowCost hide'><p id='cellTotalRowCost" + item.ID + "' ondblclick='Pricing_EnterEditModeForTxt(" + '"' + "TotalRowCost" + '",' + item.ID + "," + item.IsPricingRequest + ");'>" + (_CurrentRowCost) + "</p><input type='text' style='width:30px;' id='txtTotalRowCost" + item.ID + "' class='form-control controlStyle hide' data-type='number'  onkeypress='CheckValueIsInteger(id);' onfocus='CheckValueIsInteger(id);' onchange='Pricing_SetIsRowChanged(id);' data-required='false' value='" + _CurrentRowCost + "' /> </td>";

        tr += "     <td class='SelectedIDsToUpdate hide'> <input name='SelectedIDsToUpdate' id='SelectedIDsToUpdate" + item.ID + "' type='checkbox' value='" + item.ID + "' /></td>";
        tr += "     <td class='hide'>"
                        //+ (glbCallingControl == "PricingForm"
                        //    ? ("<a href='#' onclick='Pricing_CopyRow(" + item.ID + ");' " + copyControlsText + "</a>")
                        //    : ""
                        //  )
                        //+ ("<a href='#' onclick='Pricing_Notify(" + item.ID + ");' " + NotificationControlsText + "</a>")

                        ////+ "<a href='#'" + " onclick='Routings_UpdateOperationFromQuotation(" + item.ID + ");' " + createOperationControlsText + "</a>"
                  + "</td>";
        tr += "</tr>";
        AppendRowtoTable("tblPricing" + pPricingTypeName, tr);
        if ($("#hReadySlCurrencies :contains('" + item.CurrencyCode + "')").attr("MasterDataExchangeRate") > 0) {
            _CurrentRowCost = (_CurrentRowCost * parseFloat($("#hReadySlCurrencies :contains('" + item.CurrencyCode + "')").attr("MasterDataExchangeRate"))).toFixed(2);
            if (_CurrentRowCost < _LeastRowCostPrice) {
                _LeastRowCostPrice = _CurrentRowCost;
                _BestRecommendationRowID = item.ID;
            }
        }
    });

    $("#tblPricing" + pPricingTypeName + " tbody tr[ID=" + _BestRecommendationRowID + "]").addClass("text-primary");
    $.each(pPricing, function (i, item) {
        debugger;
        $("#slPricingType" + item.ID).html($("#slPricingType").html());
        $("#slPricingType" + item.ID).val(item.PricingTypeID == 0 ? "" : item.PricingTypeID);

        //$("#slSupplier" + item.ID).html($("#slSupplier").html());
        //$("#slSupplier" + item.ID).val(item.SupplierID == 0 ? "" : item.SupplierID);
        //$("#slSupplier" + item.ID).html("<option value=" + item.SupplierID + "></option>");

        //$("#slPOLCountry" + item.ID).html($("#slPOLCountry").html());
        //$("#slPOLCountry" + item.ID).val(item.POLCountryID == 0 ? "" : item.POLCountryID);
        //$("#slPODCountry" + item.ID).html($("#slPODCountry").html());
        //$("#slPODCountry" + item.ID).val(item.PODCountryID == 0 ? "" : item.PODCountryID);
        //$("#slEquipment" + item.ID).html($("#slEquipment").html());
        //$("#slEquipment" + item.ID).val(item.EquipmentID == 0 ? "" : item.EquipmentID);
        //$("#slCommodity" + item.ID).html($("#slCommodity").html());
        //$("#slCommodity" + item.ID).val(item.CommodityID == 0 ? "" : item.CommodityID);
        $("#slCurrency" + item.ID).html($("#hReadySlCurrencies").html()); //to get the exchangerate
        $("#slCurrency" + item.ID).val(item.CurrencyID == 0 ? "" : item.CurrencyID);
        //Pricing_PricingTypeChanged("slPricingType" + item.ID, "slSupplier" + item.ID, item.SupplierID);
        //Pricing_FillPorts("slPOLCountry" + item.ID, "slPOL" + item.ID, item.POLID);
        //Pricing_FillPorts("slPODCountry" + item.ID, "slPOD" + item.ID, item.PODID);

    });
    SetDatepickerFormat();
    //ApplyPermissions();
    //BindAllCheckboxonTable("tblPricing", "PricingID", "cbPricingDeleteHeader");//Parameters(pTableName, pCheckBoxColumnClassName, pCheckBoxHeaderID)
    //CheckAllCheckbox("HeaderDeletePricingID");
    //HighlightText("#tblPricing>tbody>tr", $("#txt-Search").val().trim());
    if (showDeleteFailMessage) { //sherif: if deletion failed due to foreign key, then this message is shown
        swal(strSorry, strDeleteFailMessage, "warning");
        showDeleteFailMessage = false;
    }
    $("#hl-homepage").on("click", function () { LoadViews("hl-homepage"); });
}
function PricingCharges_FillPricingChargesModal() {
    debugger;
    if ($("#cbIsOcean").prop("checked"))
        $("#slPricingType").val(constPricingOcean);
    else if ($("#cbIsAir").prop("checked"))
        $("#slPricingType").val(constPricingAir);
    else if ($("#cbIsInland").prop("checked"))
        $("#slPricingType").val(constPricingInland);
    $("#tblPricingOCEAN thead tr").html("");
    $("#tblPricingOCEAN tbody tr").html("");
    $("#tblPricingAIR thead tr").html("");
    $("#tblPricingAIR tbody tr").html("");
    $("#tblPricingINLAND thead tr").html("");
    $("#tblPricingINLAND tbody tr").html("");
    $("#tblPricingCUSTOMSCLEARANCE thead tr").html("");
    $("#tblPricingCUSTOMSCLEARANCE tbody tr").html("");

    $(".classShowForOCEAN").addClass("hide");
    $(".classShowForAir").addClass("hide");
    $(".classShowForInland").addClass("hide");
    $(".classShowForCustomsClearance").addClass("hide");

    if ($("#hQuotationRouteID").val() == "")
        swal("Sorry", "Please, save the route first.");
    else {
        ClearAllTableRows("tblPricing");
        $("#txtProfitAmount").val("");
        FadePageCover(true);
        CallGETFunctionWithParameters("/api/QuotationCharges/FillPricingChargesModal"
            , { pPricingTypeID: $("#slPricingType").val(), pMoveTypeID: $("#slMoveTypes").val() == "" ? 0 : $("#slMoveTypes").val() }
            , function (pData) {
                var pContainerTypes = pData[0];
                var pTruckers = pData[1];
                var pPricingSettings = pData[2];
                var pMoveTypeID = pData[3] == null ? null : JSON.parse(pData[3]);

                var _PricingSettings = JSON.parse(pPricingSettings);
                var pPricingSettingsOcean = jQuery.grep(_PricingSettings, function (_PricingSettings) {
                    return _PricingSettings.PricingTypeID == constPricingOcean;
                });
                var pPricingSettingsAir = jQuery.grep(_PricingSettings, function (_PricingSettings) {
                    return _PricingSettings.PricingTypeID == constPricingAir;
                });
                var pPricingSettingsInland = jQuery.grep(_PricingSettings, function (_PricingSettings) {
                    return _PricingSettings.PricingTypeID == constPricingInland;
                });
                var pPricingSettingsCustomsClearance = jQuery.grep(_PricingSettings, function (_PricingSettings) {
                    return _PricingSettings.PricingTypeID == constPricingCustomsClearance;
                });

                if (pMoveTypeID != null) {
                    $("#cbIsServiceOcean").prop("checked", pMoveTypeID.IsOcean);
                    $("#cbIsServiceAir").prop("checked", pMoveTypeID.IsAir);
                    $("#cbIsServiceInland").prop("checked", pMoveTypeID.IsInland);
                    $("#cbIsServiceCustomsClearance").prop("checked", pMoveTypeID.IsCustomsClearance);
                }
                FillListFromObject(null, 1, TranslateString("SelectFromMenu")/*"Select Container Type"*/, "slPricingEquipment", pContainerTypes, null);
                FillListFromObject(null, 2, TranslateString("SelectFromMenu")/*"Select Container Type"*/, "slPricingSupplier", pTruckers, null);
                FillListFromObject(null, 10, null, "slPricingSettingsOCEAN", JSON.stringify(pPricingSettingsOcean), null);
                FillListFromObject(null, 10, null, "slPricingSettingsAIR", JSON.stringify(pPricingSettingsAir), null);
                FillListFromObject(null, 10, null, "slPricingSettingsINLAND", JSON.stringify(pPricingSettingsInland), null);
                FillListFromObject(null, 10, null, "slPricingSettingsCUSTOMSCLEARANCE", JSON.stringify(pPricingSettingsCustomsClearance), null);

                $("#slPricingPOLCountry").html($("#slRoutingsPOLCountries").html());
                $("#slPricingPOLCountry").val($("#slRoutingsPOLCountries").val());
                $("#slPricingPOL").html($("#slRoutingsPOL").html());

                $("#slPricingPODCountry").html($("#slRoutingsPODCountries").html());
                $("#slPricingPODCountry").val($("#slRoutingsPODCountries").val()); // to fix value
                $("#slPricingPOD").html($("#slRoutingsPOD").html());

                $("#slPricingCommodity").html($("#slCommodities").html());
                $("#slPricingCommodity").val($("#slCommodities").val()); // to fix value

                jQuery("#SelectPricingChargesModal").modal("show");
                $("#btnSelectPricingChargesApply").attr("onclick", "PricingCharges_AddSelectedCharges_ToPayables();");

                PricingCharges_LoadingWithPaging();
                //FadePageCover(false);
            }
            , null);

    }
}
function PricingCharges_LoadingWithPaging() {
    debugger;
    strBindTableRowsFunctionName = "PricingCharges_BindTableRows";
    var pWhereClause = PricingCharges_GetWhereClause();
    var pOrderBy = "SupplierName, ID DESC"; //"PricingTypeID, SupplierName, POLName, PODName";
    var pPageNumber = 1;
    var pPageSize = $('#select-page-size').val();
    var pControllerParameters = { pIsReturnObjectArray: false, pPricingTypeID: $("#slPricingType").val(), pPageNumber: pPageNumber, pPageSize: pPageSize, pWhereClause: pWhereClause, pOrderBy: pOrderBy }
    LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned("div-Pager", "select-page-size", "spn-first-page-row", "spn-last-page-row", "spn-total-count", "div-Text-Total", "/api/QuotationCharges/Pricing_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned", pWhereClause, pOrderBy, pPageNumber, pPageSize, pControllerParameters
        , function (pData) {
            var pPricing = pData[0];
            var pPricingCharge = pData[1];

            var _Pricing = JSON.parse(pPricing);
            var pPricingOcean = jQuery.grep(_Pricing, function (_Pricing) {
                return _Pricing.PricingTypeID == constPricingOcean;
            });
            var pPricingAir = jQuery.grep(_Pricing, function (_Pricing) {
                return _Pricing.PricingTypeID == constPricingAir;
            });
            var pPricingInland = jQuery.grep(_Pricing, function (_Pricing) {
                return _Pricing.PricingTypeID == constPricingInland;
            });
            var pPricingCustomsClearance = jQuery.grep(_Pricing, function (_Pricing) {
                return _Pricing.PricingTypeID == constPricingCustomsClearance;
            });

            ClearAllTableRows("tblPricing" + "OCEAN");
            ClearAllTableRows("tblPricing" + "AIR");
            ClearAllTableRows("tblPricing" + "INLAND");
            ClearAllTableRows("tblPricing" + "CUSTOMSCLEARANCE");
            if (pPricingOcean.length > 0)
                PricingCharges_BindTableRows(pPricingOcean, JSON.parse(pPricingCharge), "OCEAN"); //called in LoadDataWithPaging() if paging
            if (pPricingAir.length > 0)
                PricingCharges_BindTableRows(pPricingAir, JSON.parse(pPricingCharge), "AIR"); //called in LoadDataWithPaging() if paging
            if (pPricingInland.length > 0)
                PricingCharges_BindTableRows(pPricingInland, JSON.parse(pPricingCharge), "INLAND"); //called in LoadDataWithPaging() if paging
            if (pPricingCustomsClearance.length > 0)
                PricingCharges_BindTableRows(pPricingCustomsClearance, JSON.parse(pPricingCharge), "CUSTOMSCLEARANCE"); //called in LoadDataWithPaging() if paging
        });
    //HighlightText("#tblPricing>tbody>tr", $("#txt-PricingSearch").val().trim());
}
function PricingCharges_GetWhereClause() {
    debugger;
    var pWhereClause = " WHERE 1=1 \n";
    pWhereClause += " AND ValidFrom<=GETDATE() AND ValidTo>=GETDATE() \n";
    if ($("#txt-PricingSearch").val().trim() != "") {
        pWhereClause += " AND (";
        pWhereClause += " SupplierName like N'%" + $("#txt-PricingSearch").val().trim() + "%' ";
        pWhereClause += " OR POLName like N'%" + $("#txt-PricingSearch").val().trim() + "%' ";
        pWhereClause += " OR PODName like N'%" + $("#txt-PricingSearch").val().trim() + "%' ";
        pWhereClause += ")" + " \n";
    }
    if ($("#cbIsImport").prop("checked") && $("#hConsigneeID").val() != "")
        pWhereClause += " AND (CustomerID IS NULL OR CustomerID=" + $("#hConsigneeID").val() + ")" + "\n";
    if (($("#cbIsExport").prop("checked") || $("#cbIsDomestic").prop("checked")) && $("#hShipperID").val() != "")
        pWhereClause += " AND (CustomerID IS NULL OR CustomerID=" + $("#hShipperID").val() + ")" + "\n";

    if (pDefaults.IsDepartmentOption && pDefaults.UnEditableCompanyName != "GBL") {
        pWhereClause += " AND PricingTypeID IN (0";
        if ($("#cbIsServiceOcean").prop("checked"))
            pWhereClause += "," + constPricingOcean;
        if ($("#cbIsServiceAir").prop("checked"))
            pWhereClause += "," + constPricingAir;
        if ($("#cbIsServiceInland").prop("checked"))
            pWhereClause += "," + constPricingInland;
        if ($("#cbIsServiceCustomsClearance").prop("checked"))
            pWhereClause += "," + constPricingCustomsClearance;
        pWhereClause += ")" + "\n";
    }
    else if ($("#slPricingType").val() != "" && $("#slPricingType").val() != 0) {
        pWhereClause += " AND PricingTypeID = N'" + $("#slPricingType").val() + "' " + " \n";
    }

    if ($("#slPricingSupplier").val() != "") {
        pWhereClause += $("#slPricingType").val() == constPricingOcean ? (" AND ShippingLineID = N'" + $("#slPricingSupplier").val() + "' ") : "";
        pWhereClause += $("#slPricingType").val() == constPricingAir ? (" AND AirlineID = N'" + $("#slPricingSupplier").val() + "' ") : "";
        pWhereClause += $("#slPricingType").val() == constPricingInland ? (" AND TruckerID = N'" + $("#slPricingSupplier").val() + "' ") : "";
        pWhereClause += $("#slPricingType").val() == constPricingCustomsClearance ? (" AND CCAID = N'" + $("#slPricingSupplier").val() + "' ") : "";
    }
    if ($("#hPOLCountryID").val() != "") {
        pWhereClause += " AND POLCountryID = N'" + $("#hPOLCountryID").val() + "' " + " \n";
    }
    if ($("#hPODCountryID").val() != "") {
        pWhereClause += " AND PODCountryID = N'" + $("#hPODCountryID").val() + "' " + " \n";
    }
    if ($("#hPOL").val() != "") {
        pWhereClause += " AND POLID = N'" + $("#hPOL").val() + "' " + " \n";
    }
    if ($("#hPOD").val() != "") {
        pWhereClause += " AND PODID = N'" + $("#hPOD").val() + "' " + " \n";
    }
    if ($("#slPricingEquipment").val() != "") {
        pWhereClause += " AND EquipmentID = N'" + $("#slPricingEquipment").val() + "' " + " \n";
    }
    if ($("#slPricingCommodity").val() != "") {
        pWhereClause += " AND CommodityID = N'" + $("#slPricingCommodity").val() + "' " + " \n";
    }
    return pWhereClause;
}
function Pricing_FillPorts(pCountryControlID, pPortControlID, pPortID) {
    debugger;
    if ($("#" + pCountryControlID).val() != "") {
        FadePageCover(true);
        GetListWithCodeAndNameAndWhereClause(pPortID, "/api/Ports/LoadAll", TranslateString("SelectFromMenu"), pPortControlID, " WHERE CountryID=" + $("#" + pCountryControlID).val(), function () { FadePageCover(false); });
    }
    else
        $("#" + pPortControlID).html("<option value=''><--Select--></option>");
}
function Pricing_PricingTypeChanged(pPricingTypeControlID, pSupplierControlID, pSupplierID) {
    debugger;
    var pFunctionName = "";
    var pWhereClause = "WHERE 1=1 ORDER BY Name";
    var pFirstRow = TranslateString("SelectFromMenu");
    $("#tblPricingOCEAN thead tr").html("");
    $("#tblPricingOCEAN tbody tr").html("");
    $("#tblPricingAIR thead tr").html("");
    $("#tblPricingAIR tbody tr").html("");
    $("#tblPricingINLAND thead tr").html("");
    $("#tblPricingINLAND tbody tr").html("");
    $("#tblPricingCUSTOMSCLEARANCE thead tr").html("");
    $("#tblPricingCUSTOMSCLEARANCE tbody tr").html("");

    $(".classShowForOCEAN").addClass("hide");
    $(".classShowForAIR").addClass("hide");
    $(".classShowForINLAND").addClass("hide");
    $(".classShowForCUSTOMSCLEARANCE").addClass("hide");

    if ($("#" + pPricingTypeControlID).val() == "" || $("#" + pPricingTypeControlID).val() == 0)
        $("#" + pSupplierControlID).html("<option value=''>" + pFirstRow + "</option>");
    else {
        if ($("#" + pPricingTypeControlID).val() == constPricingOcean) {
            pFunctionName = "/api/ShippingLines/LoadAll";
        }
        else if ($("#" + pPricingTypeControlID).val() == constPricingAir) {
            pFunctionName = "/api/Airlines/LoadAll";
        }
        else if ($("#" + pPricingTypeControlID).val() == constPricingInland) {
            pFunctionName = "/api/Truckers/LoadAll";
        }
        else if ($("#" + pPricingTypeControlID).val() == constPricingCustomsClearance) {
            pFunctionName = "/api/CustomsClearanceAgents/LoadAll";
        }
        FadePageCover(true);
        //CallGETFunctionWithParameters("/api/Pricing/PricingSettings_LoadAll"
        //    , { pWhereClause: "WHERE PricingTypeID=" + $("#" + pPricingTypeControlID).val() }
        //    , function (pData) {
        //        FillListFromObject(null, 10, null, "slPricingSettings", pData[0], null);
        //    }
        //    , null)
        GetListWithNameAndWhereClause(pSupplierID, pFunctionName, pFirstRow, pSupplierControlID, pWhereClause, function () { FadePageCover(false); });
    }
}
function PricingCharges_AddSelectedCharges_ToPayables() {
    debugger;
    var pSelectedPricingIDs = GetAllSelectedIDsAsStringWithNameAttr("namePricingSelect"); //GetAllSelectedIDsAsString('tblPricing');
    if (pSelectedPricingIDs != "") {
        FadePageCover(true);
        var pParametersWithValues = {
            pOperationID: $("#hOperationID").val() == "" ? 0 : $("#hOperationID").val()
            , pSelectedPricingIDs: pSelectedPricingIDs
            , pProfitType: $("input[name=cbProfitType]:checked").val()
            , pProfitAmount: $("#txtProfitAmount").val().trim() == "" ? 0 : $("#txtProfitAmount").val().trim()
        }
        CallGETFunctionWithParameters("/api/Payables/AddSelectedPricingCharges"
            , pParametersWithValues
            , function (pData) {
                debugger;
                Payables_LoadWithPagingWithWhereClause($("#hOperationID").val());
                jQuery("#SelectPricingChargesModal").modal("hide");
                swal("Success", "Saved successfully.");
                //FadePageCover(false);
            }
            , null);
    }
}
function Pricing_EnterEditModeForSL(pControlID, pRowID, pIsPricingRequest) {
    debugger;
}
function Pricing_EnterEditModeForTxt(pControlID, pRowID, pIsPricingRequest) {
    debugger;
}
/****************************************EOF PricingCharges Fns***************************************/

function IntializeData() {

}
function AddNewRow() {
    debugger;
    // $("#hl-menu-SC").parent().addClass("active");
    // ClearAllTableRows("tblItems");
    // $.each(JSON.parse(pSC_TransactionsDetails), function (i, item) {
    debugger;
    //editControlsText = " class='btn btn-xs btn-rounded btn-info float-right' > <i class='fa fa-pencil' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + TranslateString("Edit") + "</span>";
    AppendRowtoTable("tblItems",
        ("<tr ID='" + 0 + "' isdeleted='0'  counter='" + (RowsCounter + 1) + "' value='" + 0 + "'>"
            + "<td class='ID hide'> <input name='Delete' type='checkbox' value='" + "0" + "' /></td>"
            + "<td counter='" + (RowsCounter + 1) + "'> <button id='btn-DeleteDetails' type='button' tag='0' onclick='DeleteItems(this);' class='btn btn-sm btn-danger'><i class='fa fa-trash-o'></i></button><button id='btn-UndoDeleteDetails' type='button' onclick='UndoDeleteItems(" + (RowsCounter + 1) + ");' class='btn btn-sm btn-warning hide'><i class='fa fa-undo'></i></button></td>"
            + "<td class='ItemID ' val='" + "0" + "'>" + "<select id='Item-" + "0" + "' onchange='SetItemUnit(this)' tag='" + "0" + "' class='input-sm  col-sm selectitem'>" + $('#hidden_slItems').html() + "</select>" + "</td>"
            + "<td class='UnitID ' val='" + "0" + "'>" + "<select disabled='disabled' id='UnitID-" + "0" + "' tag='" + "0" + "' class='input-sm  col-sm selectunit'>" + $('#hidden_slUnits').html() + "</select>" + "</td>"
            + "<td class='Qty' val='" + "0" + "'>" + "<input type='number' class='inputquantity input-sm  col-sm'>" + "</td>"
            + "<td class='StoreID' val='" + "0" + "'>" + "<select id='store-" + "0" + "' tag='" + "0" + "' class='input-sm  col-sm selectstore'>" + $('#slStores').html() + "</select>" + "</td>"
            + "<td class='QuantityInStore' val='" + "0" + "'>" + "<button id='btn-Quantity' type='button' onclick='GetItemQuantityInStore(this);' class='btn btn-sm btn-lightblue'><span class='fa fa-calculator'>&nbsp; Calculate</span></button><span class='span_quantity'></span>" + "</td>"
            + "<td class='ReturnedQty hide' val='" + "0" + "'>" + "0" + "</td>"
            + "<td class='CurrencyID hide' val='" + "0" + "'>" + "0" + "</td>"
            + "<td class='ExchangeRate hide' val='" + "0" + "'>" + "0" + "</td>"
            + "<td class='Notes' val='" + "0" + "'>" + "<input type='text' class='inputnotes input-sm  col-sm'>" + "</td>"
            + "<td class='PurchaseInvoiceDetailsID hide' val='" + "0" + "'>" + "0" + "</td>"
            + "<td class='SLInvoiceDetailsID hide' val='" + "0" + "'>" + "0" + "</td>"
            + "<td class='SubAccountID hide' val='" + "0" + "'>" + "0" + "</td>"
            + "<td class='OriginalQty hide' val='" + "0" + "'>" + "0" + "</td>"
            + "<td class='ParentID hide' val='" + "0" + "'>" + "0" + "</td>"
            + "<td class='AveragePrice hide' val='" + "0" + "'>" + "0" + "</td>"
            + "<td class='TransactionDate hide' val='" + "0" + "'>" + "0" + "</td>"
            + "<td class='QtyFactor hide' val='" + "-1" + "'>" + "-1" + "</td>"
            + "<td class='ActualQty hide' val='" + "0" + "'>" + "0" + "</td>"
            + "<td class='TransactionTypeID hide' val='" + "20" + "'>" + "0" + "</td>"
            + "</tr>"));

    RowsCounter++;
}

function SetItemUnit(ItemSelect) {
    debugger;
    var tr = $(ItemSelect).closest("tr");
    var SelectUnit = $(tr).find("select.selectunit");
    var UnitID = $(tr).find("td.UnitID").attr("val");


    var Units = $(ItemSelect).find("option:selected").attr("itemunits").split(',');

    //if (UnitID == 0 || UnitID == "0")
    //  {
    var a = Units.indexOf("-1");
    $(SelectUnit).val(Units[a - 1]);

    //    }

}
function FillStores() {

    $($('#tblItems > tbody > tr')).each(function (i, tr) {
        // element == this
        $(tr).find('td.StoreID ').find('.selectstore').val($(tr).find('td.StoreID ').find('.selectstore').attr('tag'));
        $(tr).find('td.ItemID ').find('.selectitem').val($(tr).find('td.ItemID ').find('.selectitem').attr('tag'));
        $(tr).find('td.Qty ').find('.inputquantity').val($(tr).find('td.Qty  ').find('.inputquantity').attr('tag'));
        $(tr).find('td.Notes ').find('.inputnotes').val($(tr).find('td.Notes ').find('.inputnotes').attr('tag'));
        $(tr).find('td.UnitID ').find('.selectunit').val($(tr).find('td.UnitID ').find('.selectunit').attr('tag'));
    });

}
function DeleteItems(This) {
    if ($(This).attr('tag') == "0" || $(This).attr('tag') == "" || $(This).attr('tag') == null) {
        $(This).closest('tr').remove();
    }
    else {
        swal({
            title: "Are you sure?",
            text: "The selected  will be deleted permanently!",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, delete!",
            closeOnConfirm: true
        },
            //callback function in case of success
            function () {
                $(This).closest('tr').remove();
            });
    }
}
function GetItemQuantityInStore(Calculate_btn) {
    FadePageCover(true);
    var tr = $(Calculate_btn).closest('tr');
    //  $(Calculate_btn).siblings('.span_quantity').attr('counter')
    var storeid = $(tr).find('td.StoreID ').find('.selectstore').val();
    var itemid = $(tr).find('td.ItemID ').find('.selectitem').val();

    if (storeid.trim() == "0" || itemid.trim() == "0") {

        swal('Excuse me', 'select Item and Store', 'warning');
        FadePageCover(false);
    }
    else {
        //GetItemQuantityInStore(string pItemID , string pStoreID , DateTime pDate)
        $.ajax({
            type: "Get",
            url: "/api/SC_Transactions/CalculateItemQuantityInStore",
            data: { pItemID: itemid, pStoreID: storeid, pDate: ConvertDateFormat($('#txtDate').val()), pTransactionID: ($('#hID').val().trim() == "" ? "0" : $('#hID').val().trim()) },
            dataType: "json",
            success: function (r) {
                // $(tr).find('.QuantityInStore').html();
                $(tr).find('.QuantityInStore').find('.span_quantity').html("&nbsp;&nbsp;&nbsp;<b>" + r[0] + "</b>");
                FadePageCover(false);
                //span_quantity
            }
        });
    }

}
function SetArrayOfItems() {
    // var cobjItem = null;
    debugger;
    var arrayOfItems = new Array();
    $("#tblItems>tbody>tr").each(function (i, tr) {
        debugger;
        var objItem = new Object();
        objItem.ID = "0";
        objItem.TransactionID = $('#hID').val();
        objItem.ItemID = $(tr).find('td.ItemID ').find('.selectitem').val(); //$(tr).find('td.ItemID').attr('val');
        objItem.StoreID = $(tr).find('td.StoreID ').find('.selectstore').val();
        objItem.ReturnedQty = "0";
        objItem.CurrencyID = $(tr).find('td.CurrencyID').attr('val');
        objItem.ExchangeRate = $(tr).find('td.ExchangeRate').attr('val');
        if (i == 0) {
            objItem.Notes = ($('#txtNotes').val() == "" || $('#txtNotes').val() == null ? "0" : $('#txtNotes').val());

        }
        else {
            objItem.Notes = ($(tr).find('td.Notes').text() == "" ? "-" : $(tr).find('td.Notes').text().trim());
        }
        objItem.PurchaseInvoiceDetailsID = "0";
        objItem.SLInvoiceDetailsID = $("#hOperationID").val(); // OperationID in SLInvoiceDetails 
        objItem.SubAccountID = "0";
        objItem.OriginalQty = "0";
        objItem.ParentID = $("#hLoggedUserID").val();  // UserID in ParentID "0";
        objItem.AveragePrice = ($(tr).find('td.AveragePrice').text().trim() == "" ? "0" : $(tr).find('td.AveragePrice').text().trim()); //AveragePrice
        objItem.TransactionDate = ConvertDateFormat($('#txtDate').val());
        objItem.QtyFactor = "-1";
        objItem.ActualQty = "0";
        objItem.TransactionTypeID = "20";



        objItem.TaxAmount = "0";
        objItem.DiscountAmount = "0";
        objItem.InvoicePrice = "0";
        //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        objItem.Qty = $(tr).find('td.Qty ').find('.inputquantity').val();  // quantity after convert
        objItem.UnitID = $(tr).find('td.UnitID ').find('.selectunit').val(); // selected unit
        objItem.ItemQty = $(tr).find('td.Qty ').find('.inputquantity').val(); // inserted quantity
        objItem.UnitFactor = 1;
        objItem.TaxAmount = "0";
        objItem.DiscountAmount = "0";
        objItem.InvoicePrice = ($(tr).find('td.AveragePrice').text().trim() == "" ? "0" : $(tr).find('td.AveragePrice').text().trim());
        //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx 2
        objItem.AvaliableQty = 0;
        objItem.P_Percentage = 0;
        objItem.P_Density = 0;
        objItem.ToStoreID = 0;
        objItem.P_LiterCost = 0;
        objItem.P_ExpectedQty = 0;
        //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

        arrayOfItems.push(objItem);
    });
    return arrayOfItems;
}
function InsertUpdateFunction2(pValidateFormID, pFunctionName, pParametersWithValues, pSaveandAddNew, pModalID, callback) {
    debugger;
    if (ValidateForm(pValidateFormID, pModalID)) {
        FadePageCover(true);
        $.ajax({
            type: "POST",
            url: strServerURL + pFunctionName,
            data: { "": pParametersWithValues },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",/*"application/json; charset=utf-8"*/
            beforeSend: function () { },
            success: function (data) {
                debugger;
                if (data != undefined && data.length > 1) {
                    if (data[0] == true) {
                        if (callback != null && callback != undefined) {
                            if (data[1] != null && data[1] != undefined) //data[1] is the ID: for opening quotation or operation after saving a new one / or strMessageReturned
                                callback(data);
                        }

                        if (!pSaveandAddNew && pModalID != null) {
                            jQuery.noConflict();
                            (function ($) {
                                $('#' + pModalID).modal('hide');
                            })(jQuery);
                        }
                    }
                    else {

                        swal(strSorry, data[1]);
                        CallbackHeaderData();
                        //try {
                        //    if (insert == true) {
                        //        $('#hID').val(data[2]);
                        //        $("#btnSave").attr("onclick", "SC_Transactions_Update(false);");
                        //        $("#btnSaveandNew").attr("onclick", "SC_Transactions_Update(true);");

                        //    }
                        //}
                        //catch
                        //{

                        //}

                    }

                }
                else {
                    if (data == true) {
                        if (callback != null && callback != undefined) {
                            callback();
                        }
                        if (!pSaveandAddNew && pModalID != null) {
                            jQuery.noConflict();
                            (function ($) {
                                $('#' + pModalID).modal('hide');
                            }
                            )(jQuery);
                        }
                    }
                    else //unique key violated
                        swal(strSorry, strUniqueFailInsertUpdateMessage);
                }
                FadePageCover(false);
            },
            error: function (jqXHR, exception) {
                FadePageCover(false);
                alert('Error when trying to call function [' + pFunctionName + ']. InsertUpdateFunction fn in mainapp.master');
            }
        });
    }
    else
        FadePageCover(false);
}
/******************************************Get Suppliers Sites******************************************/
//pPartnerTypeControlID, pPartnerControlID, pSiteControlID)

//function FillSupplierSites(pID,slPayableSupplier,slPayableSupplier,slSites) {
//    debugger;
//    if (pDefaults.UnEditableCompanyName == "GBL") {
//        var pWhereClause = "WHERE SupplierID= " + $('#slPayableSupplier option:selected').attr("partnerid");
//        if ($('#slPayableSupplier option:selected').attr("partnertypeid") != '8')
//            pWhereClause = "Where 1=0";

//        CallGETFunctionWithParameters("/api/Suppliers/LoadSupplierSites"
//        , { pWhereClause: pWhereClause }
//        , function (pData) {
//            FillListFromObject(pID, 2, TranslateString("SelectFromMenu"), "slSites", pData[0], null);
//        }
//        , null);
//    }
//}
function FillSupplierSitesBySupplier(pID,pItemID) {
    debugger;
    var ControlID = slPayableSupplier + pItemID ;
    var pWhereClause = "WHERE SupplierID= " + $("#slPayableSupplier" + pItemID + " option:selected").attr("partnerid");
    if ($("#slPayableSupplier" + pItemID + " option:selected").attr("partnertypeid") != '8')
        pWhereClause = "Where 1=0";

    CallGETFunctionWithParameters("/api/Suppliers/LoadSupplierSites"
    , { pWhereClause: pWhereClause }
    , function (pData) {
        FillListFromObject(pID, 2, TranslateString("SelectFromMenu"), "slPayableSupplierSiteID" + pItemID, pData[0], null);
    }
    , null);
}

function CalculateSelectedPayablesItems()
{
    debugger;
    var TotalPaybles = 0;
    //$('#' + pTableName + ' td').find('input[type="checkbox"]:checked').each(function () {
    $('#tblPayables  td').find('input[name="Delete"]:checked').each(function () {
        TotalPaybles += ((($(this).closest('tr').find('td.PayableCostAmount').text()) == "") ? 0 : (parseFloat($(this).closest('tr').find('td.PayableCostAmount').text()) * parseFloat($(this).closest('tr').find('td.PayableExchangeRate').text())) );
    });
    $('#lblCalculateSelectedPayablesItems').html( ": " + TotalPaybles + " EGP");
}
     
function GetPayablesBySupplier()
{
    debugger;
    var pOperationID = 0;
    if (pOperationID == 0)
        pOperationID = $("#hOperationID").val();
    var pWhereClause = "";
    if ($("#cbIsAWB").prop("checked"))
        pWhereClause = " WHERE (OperationID = " + pOperationID + ") ";// + " AND IsDeleted = 0 ";
    else
        pWhereClause = " WHERE (OperationID = " + pOperationID + " OR MasterOperationID = " + pOperationID + ") ";// + " AND IsDeleted = 0 ";
    if ($("#slSearchTankInPayables").val() != "")
        pWhereClause += " AND OperationContainersAndPackagesID=" + $("#slSearchTankInPayables").val();
    if ($("#slSupplierFilterPayables").val() != "")
        pWhereClause += " AND PartnerSupplierID = " + $("#slSupplierFilterPayables").val();

    LoadWithPagingWithWhereClause("div-Pager", "select-page-size", "spn-first-page-row", "spn-last-page-row", "spn-total-count", "div-Text-Total"
        , "/api/Payables/LoadWithWhereClause", pWhereClause, 0, 1000, function (pTabelRows) { Payables_BindTableRows(pTabelRows); });
}
