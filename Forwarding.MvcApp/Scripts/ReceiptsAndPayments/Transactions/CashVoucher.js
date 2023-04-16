﻿var maxDetailsIDInTable = 0; //used to for when adding new row then make td control names unique
var CostCenterType = $('#hReadySlOptions option[value="12"]').attr("OptionValue");
//if ($("#hDefaultUnEditableCompanyName").val() == "ERP") {
    //Start Auto Filter
    //$(document).ready(function () {
    //    $("#slAccount").css({ "width": "100%" }).select2();
    //    $("#slSubAccount").css({ "width": "100%" }).select2();
    //    $("div[tabindex='-1']").removeAttr('tabindex');
    //});
    //End Auto Filter 
//}
$(document).keyup(function (e) {
    if (e.key === "Escape") { // escape key maps to keycode `27`
        ClearA_PaymentRequestID_ForVoucherID(); // <DO YOUR WORK HERE>
    }
});

var check = 0;
function Voucher_BindTableRows(pVoucher) { 
    debugger;
    check = 1;
    count = 1;
    $("#hl-menu-ReceiptsAndPaymentsGroup").parent().addClass("active");
    ClearAllTableRows("tblVoucher");
    //editControlsText = " class='btn btn-xs btn-rounded btn-info float-right' > <i class='fa fa-pencil' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + TranslateString("Edit") + "</span>";
    var printControlsText = " class='btn btn-xs btn-rounded btn-lightblue float-right' > <i class='fa fa-print' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + TranslateString("Print") + "</span>";
    var UploadText = " class='btn btn-xs btn-rounded btn-lightblue float-right' > <i class='fa fa-print' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + TranslateString("Upload") + "</span>";

    $.each(pVoucher, function (i, item) {
        AppendRowtoTable("tblVoucher",
            ("<tr ID='" + item.ID + "' ondblclick='Voucher_FillControls(" + item.ID + ");'>"
            //("<tr ID='" + item.ID + "'>"
                    + "<td class='ID'> <input " + (item.Approved ? " disabled='disabled'" : "name='Delete'") + " type='checkbox' value='" + item.ID + "' /></td>"
                    + "<td class='Code'>" + (item.Code == 0 ? "" : item.Code) + "</td>"
                    + "<td class='SafeID hide'>" + item.SafeID + "</td>"
                    + "<td class='SafeName'>" + (item.SafeID == 0 ? "" : item.SafeName) + "</td>"
                    + "<td class='VoucherDate'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.VoucherDate)) < 1 ? "" : ConvertDateFormat(GetDateWithFormatMDY(item.VoucherDate))) + "</td>"
                    + "<td class='ChargedPerson'>" + (item.ChargedPerson == 0 ? "" : item.ChargedPerson) + "</td>"
                    + "<td class='Notes' style='width:10% !important;'>" + (item.Notes == 0 ? "" : item.Notes) + "</td>"
                    + "<td class='Total'>" + item.Total + "</td>"
                    + "<td class='TotalAfterTax hide'>" + item.TotalAfterTax + "</td>"
                    + "<td class='CurrencyID hide'>" + item.CurrencyID + "</td>"
                    + "<td class='CurrencyCode'>" + (item.CurrencyID == 0 ? "" : item.CurrencyCode) + "</td>"
                    + "<td class='ExchangeRate hide'>" + item.ExchangeRate + "</td>"

                    + "<td class='TaxID hide'>" + item.TaxID + "</td>"
                    + "<td class='TaxID2 hide'>" + item.TaxID2 + "</td>"
                    + "<td class='TaxValue hide'>" + item.TaxValue + "</td>"
                    + "<td class='TaxValue2 hide'>" + item.TaxValue2 + "</td>"
                    + "<td class='JVID1 hide'>" + item.JVID1 + "</td>"

                    + "<td class='InvoiceID hide'>" + item.SafeID + "</td>"
                    + "<td class='DisbursementJob_ID hide'>" + item.DisbursementJob_ID + "</td>"
                    + "<td class='SL_SalesManID hide'>" + item.SL_SalesManID + "</td>"


                    + "<td class='InvoiceNo hide'>" + (item.InvoiceID == 0 ? "" : item.InvoiceNo) + "</td>"
                    + "<td class='IsCash " + (glbFormCalled == constVoucherCashOut ? "" : " hide ") + "'> <input id=cbIsCash" + item.ID + " type='checkbox' disabled='disabled' " + (item.IsCash ? " checked='checked' " : "") + " /></td>"
                    + "<td class='Approved hide'> <input id=cbApproved" + item.ID + " type='checkbox' disabled='disabled' " + (item.Approved ? " checked='checked' " : "") + " /></td>"
                    + "<td class='Posted'> <input id=cbPosted" + item.ID + " type='checkbox' disabled='disabled' " + (item.Posted ? " checked='checked' " : "") + " /></td>"
                    + "<td class='Print'><a href='' data-toggle='modal' onclick='Voucher_Print(" + item.ID + ");' " + printControlsText + "</a></td>"
                    + "<td class='Print'><a href='#tabUploadFiles' data-toggle='modal' onclick='UploadFiles(" + item.ID + ");' " + UploadText + "</a></td>"
                    + "<td class='hide'><a href='#VoucherModal' data-toggle='modal' onclick='Voucher_FillControls(" + item.ID + ");' " + editControlsText + "</a></td>"
            + "</tr>"));
    });
    ApplyPermissions();
    BindAllCheckboxonTable("tblVoucher", "ID");
    CheckAllCheckbox("ID");
    //HighlightText("#tblVoucher>tbody>tr", $("#txt-Search").val().trim());
    if (showDeleteFailMessage) { //sherif: if deletion failed due to foreign key, then this message is shown
        swal(strSorry, strDeleteFailMessage);
        showDeleteFailMessage = false;
        strDeleteFailMessage = "This command is not completed because of dependencies existance."
    }
    $("#hl-homepage").on("click", function () { LoadViews("hl-homepage"); });
}
function Voucher_LoadingWithPaging() {
    debugger;
    var pWhereClause = Voucher_GetWhereClause();
    var pPageSize = $('#select-page-size').val();
    var pPageNumber = ($("#div-Pager li.active a").text() == "" ? 1 : $("#div-Pager li.active a").text());
    var pOrderBy = "ID DESC";
    var pControllerParameters = { pIsLoadArrayOfObjects: false, pLanguage: $("[id$='hf_ChangeLanguage']").val(), pPageNumber: pPageNumber, pPageSize: pPageSize, pWhereClause: pWhereClause, pOrderBy: pOrderBy }
    LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned("div-Pager", "select-page-size", "spn-first-page-row", "spn-last-page-row", "spn-total-count", "div-Text-Total", strLoadWithPagingFunctionName, pWhereClause, pOrderBy, pPageNumber, pPageSize, pControllerParameters, function (pData) { Voucher_BindTableRows(JSON.parse(pData[0])); });
    //HighlightText("#tblVoucher>tbody>tr", $("#txt-Search").val().trim());
}
function Voucher_GetWhereClause() {
    var pWhereClause = "WHERE VoucherType=" + glbFormCalled + "\n";
    pWhereClause += " AND VoucherDate >= '" + GetDateWithFormatyyyyMMdd($("#txtSearchFrom").val()) + "' AND VoucherDate<='" + GetDateWithFormatyyyyMMdd($("#txtSearchTo").val()) + " 23:59:59'";
    if ($("#txtSearchCode").val().trim() != "")
        pWhereClause += " AND Code LIKE N'%" + $("#txtSearchCode").val().trim() + "%'" + "\n";
    if ($("#slSearchSafe").val() != 0)
        pWhereClause += " AND SafeID = " + $("#slSearchSafe").val() + "\n";
    if ($("#txtSearchTotal").val().trim() != "")
        pWhereClause += " AND TotalAfterTax = " + $("#txtSearchTotal").val().trim() + "\n";
    if ($("#slSearchCurrency").val() != 0)
        pWhereClause += " AND CurrencyID = " + $("#slSearchCurrency").val() + "\n";
    if ($("#txtSearchChargedPerson").val().trim() != "")
        pWhereClause += " AND ChargedPerson LIKE N'%" + $("#txtSearchChargedPerson").val().trim() + "%'" + "\n";
    if ($("#txtSearchNotes").val().trim() != "")
        pWhereClause += " AND Notes LIKE N'%" + $("#txtSearchNotes").val().trim() + "%'" + "\n";
    if ($("#slSearchCashOrCharge").val() == 10)
        pWhereClause += " AND IsCash = 1" + "\n";
    if ($("#slSearchCashOrCharge").val() == 20)
        pWhereClause += " AND IsCash = 0" + "\n";
    if ($("#slSearchStatus").val() == 10)
        pWhereClause += " AND Approved = 1" + "\n";
    if ($("#slSearchStatus").val() == 20)
        pWhereClause += " AND Approved = 0" + "\n";

    //var LinkUserAndSafes = $('#hReadySlOptions option[value="55"]').attr("OptionValue");//LinkUserAndSafes
    //if (LinkUserAndSafes == "true") {
    //    pWhereClause = " INNER JOIN VW_Sec_UserSafes US ON SafeID = US._SafeID " + "\n" + pWhereClause + "\n" + " AND US._UserID";
    //}


    return pWhereClause;
}
function Voucher_ClearAllControls(callback) {
    var pFormattedTodaysDate = getTodaysDateInddMMyyyyFormat();
    Voucher_EnableDisableEditing(1); //Enable
    ClearAll("#VoucherModal");
    $("#cbIsCash").prop("checked", false);
    $("#tblDetails tbody").html("");
    $("#slCurrency").val($("#hDefaultCurrencyID").val());
    $("#txtExchangeRate").attr("disabled", "disabled");
    $("#txtExchangeRate").val(1);
    $("#txtVoucherDate").val(pFormattedTodaysDate);
    $("#lblTotal").html("<span> : </span><span>" + 0 + "</span>");
    $("#lblTotalAfterTax").html("<span> : </span><span>" + 0 + "</span>");
    ////$("#btnSave").attr("onclick", "Voucher_Save(false);");
    ////$("#btnSaveandNew").attr("onclick", "Voucher_Save(true);");
    $("#cb-CheckAll").prop('checked', false);
    jQuery("#VoucherModal").modal("show");

    if (callback != null && callback != undefined)
        callback();
    if ($("[id$='hf_ChangeLanguage']").val() == "ar") {
        $("#lblTotal").reverseChildren();
        $("#lblTotalAfterTax").reverseChildren();
        //$("#lblDebitCreditDifference").reverseChildren();
        $(".swapChildrenClass:not(.reversed)").reverseChildren();
    }
}
function UploadFiles(pID) {
    debugger;
    $("#hID").val(pID);
    ClearAll("#UploadFilesModal");
    jQuery("#UploadFilesModal").modal("show");
    Vouchers_GeneralUpload_Initialise();

}
//*********************************Uploaded Files***************************************//
function Vouchers_GeneralUpload_Initialise() {
    debugger;
    glbGeneralUploadModalName = ""; //if the upload is in a modal then is not opened
    glbGeneralUploadTableName = "tblUploadedFiles_Vouchers";
    //glbGeneralUploadFolderName = $("#hID").val() == "" ? "" : $("#txtName").val().trim();
    glbGeneralUploadFolderName = $("#hID").val() == "" ? "" : $("#hID").val();

    glbGeneralUploadPath = "/DocsInFiles//Vouchers//";
    glbGeneralUploadRelativePath = "~/DocsInFiles/Vouchers/";
    glbGeneralUploadBtnUploadName = "inputFileUpload_Vouchers";
    glbTblTHSelectAllTagName = "HeaderDeleteUploadedFiles_Vouchers";
    glbTblInputSelectAllInputName = "cb-CheckAll-UploadedFiles_Vouchers";

    if (glbGeneralUploadFolderName != "")
        GeneralUpload_FillControls();
}
function Voucher_FillControls(pID) {
    //----------- It For Invoice ------

    $.ajax({
        type: "GET",
        url: strServerURL + "/api/Voucher/CheckIsForInvoice",
        data: { pVoucherID: pID },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $("#hf_CanEdit").val(JSON.parse(data[0]));
     
            if ( pDefaults.UnEditableCompanyName != 'OAO')
            {
                $("#slSafe").prop('disabled', true);
                $("#txtCode").prop('disabled', true);

            }
      
           

            debugger;
            ClearAll("#VoucherModal");
            if (!$("#cbApproved" + pID).prop("checked") && $("#hf_CanEdit").val() == "1")
                Voucher_EnableDisableEditing(1); //Enable
            else
                Voucher_EnableDisableEditing(2); //Disable

            $("#tblDetails tbody").html("");
            FadePageCover(true);
            var tr = $("#tblVoucher tr[ID='" + pID + "']");
            $("#hID").val(pID);

            $("#lblShown").html("<span> : </span><span> " + $(tr).find("td.Code").text() + "</span>");
            $("#txtCode").val($(tr).find("td.Code").text());
            $("#txtVoucherDate").val($(tr).find("td.VoucherDate").text());
            $("#slSafe").val($(tr).find("td.SafeID").text());

            $("#slTax").val($(tr).find("td.TaxID").text());
            $("#slTax2").val($(tr).find("td.TaxID2").text());
            $("#txtTaxValue").val($(tr).find("td.TaxValue").text());
            $("#txtTaxValue2").val($(tr).find("td.TaxValue2").text());
            $("#btn-OpenInvModal").attr('disabled', 'disabled');
            $("#btn-OpenOperationModal").attr('disabled', 'disabled');

            $("#btn-OfficialAllocationModalModal").attr('disabled', 'disabled');

            $("#slInvoice").val($(tr).find("td.InvoiceID").text());
            $("#slCurrency").val($(tr).find("td.CurrencyID").text());
            $("#txtExchangeRate").val($(tr).find("td.ExchangeRate").text());
            if ($(tr).find("td.CurrencyID").text() == $("#hDefaultCurrencyID").val())
                $("#txtExchangeRate").attr("disabled", "disabled");
            else
                $("#txtExchangeRate").removeAttr("disabled");
            $("#txtChargedPerson").val($(tr).find("td.ChargedPerson").text());

            $("#txtNotes").val($(tr).find("td.Notes").text());
            if ($("#cbIsCash" + pID).prop("checked"))
                $("#cbIsCash").prop("checked", true);
            else
                $("#cbIsCharge").prop("checked", true);
            $("#lblTotal").html("<span> : </span><span>" + $(tr).find("td.Total").text() + "</span>");
            $("#lblTotalAfterTax").html("<span> : </span><span>" + $(tr).find("td.TotalAfterTax").text() + "</span>");



            if (!$("#cbApproved" + pID).prop("checked")) {
                $("#btn-PrintJV").attr("disabled", "disabled");
                $("#btn-PrintJV").attr("JVID", "0");


            }

            else {
                $("#btn-PrintJV").removeAttr("disabled");
                $("#btn-PrintJV").attr("JVID", $(tr).find("td.JVID1").text());
            }



            
            $("#txtChargedPerson").removeAttr("disabled");
        
            $("#slDisbursementJobs").val($(tr).find("td.DisbursementJob_ID").text());
            $("#slSalesMan").val($(tr).find("td.SL_SalesManID").text());


            //$("#cbIsInactive").prop('checked', $(tr).find('td.IsInactive').find('input').attr('val'));

            //$("#btnSave").attr("onclick", "Currencies_Update(false);");
            //$("#btnSaveandNew").attr("onclick", "Currencies_Update(true);");

            jQuery("#VoucherModal").modal("show");
            CallGETFunctionWithParameters("/api/Voucher/GetVoucherDetails"
                , {
                    pPageNumber: 1
                    , pPageSize: 9999
                    , pWhereClauseVoucherDetails: "WHERE VoucherID=" + pID
                    , pOrderBy: "Account_Name"
                }
                , function (pData) {
                    Details_BindTableRows(JSON.parse(pData[0]));
                    FadePageCover(false);
                }
                , null);

            if ($("[id$='hf_ChangeLanguage']").val() == "ar") {
                $("#lblShown").reverseChildren();
                $("#lblTotal").reverseChildren();
                $("#lblTotalAfterTax").reverseChildren();
                //$("#lblDebitCreditDifference").reverseChildren();
                $(".swapChildrenClass:not(.reversed)").reverseChildren();
            }
        }
    });

    //----------- ---------------------




}
function Voucher_Save() {
    debugger;
    if ($("#txtExchangeRate").val() == 0)
        swal("Sorry", "Exchange rate can not be 0.");
    else if ($("#tblDetails tbody tr").length == 0)
        swal("Sorry", "You must enter details.");
    else if (ValidateForm("form", "VoucherModal")) {
        FadePageCover(true);
        Voucher_CalculateTotal();
        var pParametersWithValues = {
            pID: $("#hID").val() == "" ? 0 : $("#hID").val()
            , pCode: $("#txtCode").val().trim().toUpperCase()
            , pVoucherDate: ConvertDateFormat($("#txtVoucherDate").val())
            , pSafeID: $("#slSafe").val()
            , pCurrencyID: $("#slCurrency").val()
            , pExchangeRate: $("#txtExchangeRate").val()
            , pChargedPerson: $("#txtChargedPerson").val().trim() == "" ? "0" : $("#txtChargedPerson").val().trim().toUpperCase()
            , pNotes: $("#txtNotes").val().trim() == "" ? "0" : $("#txtNotes").val().trim().toUpperCase()
            , pTaxID: ($("#slTax").val() != 0 && $("#txtTaxValue").val() != 0 ? $("#slTax").val() : 0)
            , pTaxValue: ($("#slTax").val() != 0 && $("#txtTaxValue").val() != 0 ? $("#txtTaxValue").val() : 0)
            , pTaxSign: (
                            ($("#slTax option:selected").attr("IsDebitAccount") == 1
                                && (glbFormCalled == constVoucherCashIn || glbFormCalled == constVoucherChequeIn)
                            )
                            || ($("#slTax option:selected").attr("IsDebitAccount") == 0
                                && (glbFormCalled == constVoucherCashOut || glbFormCalled == constVoucherChequeOut)
                            )
                         )
                         ? -1 : 1
            , pTaxID2: ($("#slTax2").val() != 0 && $("#txtTaxValue2").val() != 0 ? $("#slTax2").val() : 0)
            , pTaxValue2: ($("#slTax2").val() != 0 && $("#txtTaxValue2").val() != 0 ? $("#txtTaxValue2").val() : 0)
            , pTaxSign2: (
                            ($("#slTax2 option:selected").attr("IsDebitAccount") == 1
                                && (glbFormCalled == constVoucherCashIn || glbFormCalled == constVoucherChequeIn)
                            )
                            || ($("#slTax2 option:selected").attr("IsDebitAccount") == 0
                                && (glbFormCalled == constVoucherCashOut || glbFormCalled == constVoucherChequeOut)
                            )
                         )
                         ? -1 : 1
            , pTotal: parseFloat($("#lblTotal").text().replace(":", ""))
            , pTotalAfterTax: parseFloat($("#lblTotalAfterTax").text().replace(":", ""))
            , pIsAGInvoice: false
            , pAGInvType_ID: 000
            , pInv_No: 000
            , pInvoiceID: 000
            , pJVID1: 000
            , pJVID2: 000
            , pJVID3: 000
            , pJVID4: 000
            , pSalesManID: 000
            , pforwOperationID: 000
            , pIsCustomClearance: false
            , pTransType_ID: 000
            , pVoucherType: glbFormCalled
            , pIsCash: ($("#cbIsCash").prop("checked") && glbFormCalled == constVoucherCashOut) ? true : false
            , pIsCheque: false
            , pPrintDate: "01/01/1900"
            , pChequeNo: 000
            , pChequeDate: "01/01/1900"
            , pBankID: 000
            , pOtherSideBankName: 0
            , pCollectionDate: "01/01/1900"
            , pCollectionExpense: 000
            , pDisbursementJob_ID: $("#slDisbursementJobs").val() == "" ? 0 : $("#slDisbursementJobs").val()
            , pSL_SalesManID: $("#slSalesMan").val() == "" ? 0 : $("#slSalesMan").val()


        };
        CallPOSTFunctionWithParameters("/api/Voucher/VoucherHeader_Save", pParametersWithValues
            , function (pData) {
                var pMessageReturned = pData[0];
                if (pMessageReturned == "") {
                    if ($("[id$='hf_ChangeLanguage']").val() == "en") swal("Success", "Saved successfully.");
                    else if ($("[id$='hf_ChangeLanguage']").val() == "ar") swal("نجاح", "تم الحفظ بنجاح.");
                    jQuery("#VoucherModal").modal("hide");
                    Voucher_LoadingWithPaging();
                    //FadePageCover(false); //called in LoadWithPaging
                }
                else {
                    swal("Sorry", pMessageReturned);
                    FadePageCover(false);
                }
            }
            , null);
    }
}
function Voucher_EnableDisableEditing(pOption) { //pOption 1:Enable 2:Disable
    if (pOption == 1) {
        $("#btnSave").removeAttr("disabled");
        $("#btnSaveandNew").removeAttr("disabled");
        $("#btn-AddDetails").removeAttr("disabled");
        $("#btn-DeleteDetails").removeAttr("disabled");
        $("#btnSaveDetails").removeAttr("disabled");
        $("#btnSaveandNewDetails").removeAttr("disabled");
        if ($("#hID").val().trim() == "" || $("#hID").val().trim() == "0") {
            $("#btn-OpenInvModal").removeAttr("disabled");
            $("#btn-OpenOperationModal").removeAttr("disabled");
            $("#btn-OfficialAllocationModalModal").removeAttr("disabled");
        }
        else {
            $("#btn-OpenInvModal").attr("disabled", "disabled");
            $("#btn-OpenOperationModal").attr("disabled", "disabled");
            $("#btn-OfficialAllocationModalModal").attr("disabled", "disabled");
        }

    }
    else {
        $("#btnSave").attr("disabled", "disabled");
        $("#btnSaveandNew").attr("disabled", "disabled");
        $("#btn-AddDetails").attr("disabled", "disabled");
        $("#btn-DeleteDetails").attr("disabled", "disabled");
        $("#btnSaveDetails").attr("disabled", "disabled");
        $("#btnSaveandNewDetails").attr("disabled", "disabled");
        $("#btn-OpenInvModal").attr("disabled", "disabled");
        $("#btn-OpenOperationModal").attr("disabled", "disabled");
        $("#btn-OfficialAllocationModalModal").attr("disabled", "disabled");
    }
}

function Voucher_GetCodeAndSetCurrency() {
    debugger;
    if ($("#hID").val() == 0 || $("#hID").val() == "") { //i.e. insert
        if ($("#slSafe").val() == 0)
            $("#txtCode").val("");
        else {
            var pFormattedTodaysDate = getTodaysDateInddMMyyyyFormat();
            FadePageCover(true);
            CallGETFunctionWithParameters("/api/Voucher/GetCode"
                , {
                    pSafeID: $("#slSafe").val()
                    , pBankID: 0
                    , pDate: ConvertDateFormat(pFormattedTodaysDate)
                    , pVoucherType: glbFormCalled
                }
                , function (pData) {
                    $("#txtCode").val(pData[0]);
                    $("#slCurrency").val($("#slSafe option:selected").attr("CurrencyID"));
                    Voucher_CurrencyChanged("slCurrency", "txtExchangeRate", "");

                    //FadePageCover(false); //called in Voucher_CurrencyChanged()
                }
                , null);
        }
    }
    else { //Update so just adjust currency
        $("#slCurrency").val($("#slSafe option:selected").attr("CurrencyID"));
        Voucher_CurrencyChanged("slCurrency", "txtExchangeRate", "");
    }
}
function Voucher_CalculateTotal() {
    debugger;
    //Total = Sum of all rows + Sum of positive Taxes
    //TotalAfterTax = For In-Vouchers  then (Total + Sum or Diff. of all Taxes) 
    var pTaxValue = ($("#txtTaxValue").val() == "" || $("#slTax").val() == 0 ? 0 : $("#txtTaxValue").val());
    var pTaxValue2 = ($("#txtTaxValue2").val() == "" || $("#slTax2").val() == 0 ? 0 : $("#txtTaxValue2").val());
    if (
            ($("#slTax option:selected").attr("IsDebitAccount") == 1
                && (glbFormCalled == constVoucherCashIn || glbFormCalled == constVoucherChequeIn)
            )
            || ($("#slTax option:selected").attr("IsDebitAccount") == 0
                && (glbFormCalled == constVoucherCashOut || glbFormCalled == constVoucherChequeOut)
            )
        )
        pTaxValue = pTaxValue * -1;
    if (
            ($("#slTax2 option:selected").attr("IsDebitAccount") == 1
                && (glbFormCalled == constVoucherCashIn || glbFormCalled == constVoucherChequeIn)
            )
            || ($("#slTax2 option:selected").attr("IsDebitAccount") == 0
                && (glbFormCalled == constVoucherCashOut || glbFormCalled == constVoucherChequeOut)
            )
        )
        pTaxValue2 = pTaxValue2 * -1;
    var pTotal = 0;
    var pTotalAfterTax = 0;
    //  pTotal = GetColumnSum("tblDetails", "Value");
    pTotal = Details_CalculateTotals();
    pTotalAfterTax = pTotal;
    pTotal += (parseFloat(parseFloat(pTaxValue).toFixed(4) > 0 ? parseFloat(pTaxValue).toFixed(4) : 0))
        + (parseFloat(parseFloat(pTaxValue2).toFixed(4) > 0 ? parseFloat(pTaxValue2).toFixed(4) : 0));
    pTotalAfterTax += parseFloat(pTaxValue) + parseFloat(pTaxValue2);

    $("#lblTotal").html("<span> : </span><span>" + parseFloat(pTotal).toFixed(4) + "</span>");
    $("#lblTotalAfterTax").html("<span> : </span><span>" + parseFloat(pTotalAfterTax).toFixed(4) + "</span>");
    //$("#lblDebitCreditDifference").html("<span> : </span><span>" + (parseFloat(pTotalAfterTax) - parseFloat(pTotal)).toFixed(4) + "</span>");
    if ($("[id$='hf_ChangeLanguage']").val() == "ar") {
        $("#lblTotal").reverseChildren();
        $("#lblTotalAfterTax").reverseChildren();
    }
}
function Voucher_CurrencyChanged(pCurrencyControlID, pExchangeRateControlID, pRowID) {
    debugger;
    FadePageCover(true);
    GetListCurrencyWithCodeAndExchangeRateAttr_ERP(null, "/api/Currencies/LoadCurrencyDetails"
        , null/*1st Row*/, "slCurrencyDetails"
        , ("WHERE '" + GetDateWithFormatyyyyMMdd($("#txtVoucherDate").val()) + "' >= DateAdd(hh, 0, DateDiff(dd, 0, FromDate)) "
            + " AND '" + GetDateWithFormatyyyyMMdd($("#txtVoucherDate").val()) + "' <= DateAdd(minute,59,DateAdd(hh, 23, DateDiff(dd, 0, ToDate))) "
            + " ORDER BY CODE"
          )
        , function () {
            if ($("#" + pCurrencyControlID + pRowID).val() == $("#hDefaultCurrencyID").val()) {
                $("#" + pExchangeRateControlID + pRowID).attr("disabled", "disabled");
                $("#" + pExchangeRateControlID + pRowID).val(1);

            }
            else {
                $("#" + pExchangeRateControlID + pRowID).removeAttr("disabled");
                $("#slCurrencyDetails").val($("#" + pCurrencyControlID + pRowID).val());
                if ($("#slCurrencyDetails option:selected").attr("ExchangeRate") == undefined)
                    $("#" + pExchangeRateControlID + pRowID).val("");
                else
                    $("#" + pExchangeRateControlID + pRowID).val($("#slCurrencyDetails option:selected").attr("ExchangeRate"));
            }

            Voucher_GetSafeBalance();
            FadePageCover(false);
        });

}
function Voucher_GetSafeBalance() {

    $("#txtCurrBalance").val("");
    debugger;
    if (
        $("#slSafe").val() != 0 && $("#slSafe").val() != "" &&
        $("#slCurrency").val() != 0 && $("#slCurrency").val() != ""
        ) {
        var pFormattedTodaysDate = getTodaysDateInddMMyyyyFormat();
        FadePageCover(true);
        CallGETFunctionWithParameters("/api/Voucher/GetSafeBalance"
            , {
                pSafeID: $("#slSafe").val()
                , pToDate: ConvertDateFormat(pFormattedTodaysDate)
                , pCurrID: $("#slCurrency").val()
            }
            , function (pData) {

                var pVH = JSON.parse(pData[0]);
                $("#txtCurrBalance").val(pVH.Amount);


            }
            , null);
    }

}
function Voucher_DeleteList() {
    debugger;
    var pSelectedIDs = GetAllSelectedIDsAsString('tblVoucher', 'Delete');
    if (pSelectedIDs != "")
        swal({
            title: "Are you sure?",
            text: "The selected records will be deleted permanently!",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, delete!",
            closeOnConfirm: true
        },
        function () {
            FadePageCover(true);
            if (glbFormCalled == 20)
            {
                CallGETFunctionWithParameters("/api/Voucher/DeleteCashVoucher"
               , { pDeletedIDs: pSelectedIDs, pCheckFiscalClosed: true, FormName:"CashVoucher" }
               , function (pData) {
                   if (!pData[0]) {
                       showDeleteFailMessage = true;
                       strDeleteFailMessage = "One or more vouchers are not deleted because fiscal year is closed or date is frozen.";
                   }
                   Voucher_LoadingWithPaging();
               }
               , null);
            }
            else  {
                CallGETFunctionWithParameters("/api/Voucher/Delete"
               , { pDeletedIDs: pSelectedIDs, pCheckFiscalClosed: true }
               , function (pData) {
                   if (!pData[0]) {
                       showDeleteFailMessage = true;
                       strDeleteFailMessage = "One or more vouchers are not deleted because fiscal year is closed or date is frozen.";
                   }
                   Voucher_LoadingWithPaging();
               }
               , null);
            }
        });
}
function Voucher_Print(pID) {
    debugger;
    if (pID == 0) //pID=0 this means print is pressed from modal
        pID = $("#hID").val();
    if (pID == "")
        swal("Sorry", "Please, save before printing.");
    else {
        FadePageCover(true);
        CallGETFunctionWithParameters("/api/Voucher/GetPrintedData"
            , { pVoucherIDToPrint: pID }
            , function (pData) {
                if ($("#hDefaultUnEditableCompanyName").val() == "FAI") {
                    var TodaysDateddMMyyyy = getTodaysDateInddMMyyyyFormat() + ' ' + getTime();
                    var pVoucherHeader = JSON.parse(pData[0]);
                    var pVoucherDetails = JSON.parse(pData[1]);
                    var pVoucherOperation = JSON.parse(pData[2]);
                    var pVoucherInvItem = JSON.parse(pData[3]);
                    var InvoiceNumber = "undefined";
                    $.each(pVoucherInvItem, function (j, item) {
                        InvoiceNumber = item.InvoiceNumber;

                    });
                    debugger;
                    //
                    if (InvoiceNumber = "" || InvoiceNumber == 0 || InvoiceNumber == null || InvoiceNumber == "undefined") {
                        if (pVoucherHeader.VoucherType == 10)
                            PrintFairtransNoInv(pData);
                        else
                        {
                            if ($("#hDefaultUnEditableCompanyName").val() == "CAP")
                                PrintCapital(pData);
                          else
                            PrintDefault(pData);
                        }

                           
                    }
                    else {
                        PrintFairtrans(pData);
                    }

                }
                else {
                    if ($("#hDefaultUnEditableCompanyName").val() == "CAP")
                        PrintCapital(pData);
                    else
                        PrintDefault(pData);
                }
                FadePageCover(false);
            }
            ,function()
            {
                if ($("#hDefaultUnEditableCompanyName").val() == "SAF")
                    Voucher_LoadingWithPaging();
                else
                    null
            }
            );
    }
}
function Vouchers_Print() {
    debugger;
    if ($("#txtSearchFrom").val() == '' || $("#txtSearchTo").val() == '') //pID=0 this means print is pressed from modal
        swal("Sorry", "Please, choose from and to date.");
    else if ($("#txtSearchFrom").val() != $("#txtSearchTo").val())
        swal("Sorry", "Please, choose from and to date ( one date ).");
    else {
        FadePageCover(true);
        CallGETFunctionWithParameters("/api/Voucher/GetPrintedDataByDate"
            , {
                pFromDate: ConvertDateFormat($("#txtSearchFrom").val()) ,
                pToDate:ConvertDateFormat($("#txtSearchTo").val() ),
                pVoucherTypeID :glbFormCalled
            }
            , function (pData) {
                if ($("#hDefaultUnEditableCompanyName").val() == "FAI") {
                    var TodaysDateddMMyyyy = getTodaysDateInddMMyyyyFormat() + ' ' + getTime();
                    var pVoucherHeader = JSON.parse(pData[0]);
                    var pVoucherDetails = JSON.parse(pData[1]);
                    var pVoucherOperation = JSON.parse(pData[2]);
                    var pVoucherInvItem = JSON.parse(pData[3]);
                    var InvoiceNumber = "undefined";
                    $.each(pVoucherInvItem, function (j, item) {
                        InvoiceNumber = item.InvoiceNumber;

                    });
                    debugger;
                    //
                    if (InvoiceNumber = "" || InvoiceNumber == 0 || InvoiceNumber == null || InvoiceNumber == "undefined") {
                        if (pVoucherHeader.VoucherType == 10)
                            PrintFairtransNoInv(pData);
                        else {
                            if ($("#hDefaultUnEditableCompanyName").val() == "CAP")
                                PrintCapital(pData);
                            else
                                PrintDefault(pData);
                        }


                    }
                    else {
                        PrintFairtrans(pData);
                    }

                }
                else {
                    if ($("#hDefaultUnEditableCompanyName").val() == "CAP")
                        PrintCapital(pData);
                    else
                        PrintDefaults(pData);
                }
                FadePageCover(false);
            }
            , function () {
                if ($("#hDefaultUnEditableCompanyName").val() == "SAF")
                    Voucher_LoadingWithPaging();
                else
                    null
            }
            );
    }
}
function PrintDefaults(pData) {
    debugger;
    var TodaysDateddMMyyyy = getTodaysDateInddMMyyyyFormat() + ' ' + getTime();
    var pVouchersHeader = JSON.parse(pData[0]);
    var pVouchersDetails = JSON.parse(pData[1]);
    var pVoucherOperation = JSON.parse(pData[2]);
    var mywindow = window.open('', '_blank');


    var ReportHTML = '';

    for (cnt = 0; cnt < pVouchersHeader.length; cnt++) {
        var pVoucherHeader = pVouchersHeader[cnt]; // its 1 row
        var pVoucherDetails = $(pVouchersDetails).filter(function (i, n) {
            return n.VoucherID == pVouchersHeader[cnt].ID;
        });
        //<div style="width:500px;height:100px;border:1px solid #000;">This is a rectangle!</div>

            if ($("[id$='hf_ChangeLanguage']").val() == "en") {
                ReportHTML += '<html>';
            }
            else {
                ReportHTML += '<html dir="rtl">';
            }



        ReportHTML += '     <head><title>' + TranslateString("Payment") + '</title><link rel="stylesheet" href="/Content/CSS/app.v2.css" type="text/css" /></head>';
        ReportHTML += '         <body style="background-color:white;font-size:115%;">';

        if (!(pDefaults.UnEditableCompanyName == "PHO" && pVoucherHeader.VoucherType == constVoucherCashIn))
            ReportHTML += '             <div class="col-xs-12 text-center"><img src="/Content/Images/CompanyHeader.jpg" alt="logo"/></div>';
        //ReportHTML += '             <div class="col-xs-12 text-center m-t-n"><h3>Invoice No. ' + pInvoiceDate.substr(3, 2) + '/' + pInvoiceDate.substr(8, 2) + '/' + $("#tblInvoices #" + pID + " td.InvoiceNumber").text() + '</h3></div>';

        ReportHTML += '             <div class="col-xs-12 text-center m-t-n"><h3>' + TranslateString(pVoucherHeader.VoucherTypeName) + TranslateString("No") + pVoucherHeader.Code + '</h3></div>';

        if (pVoucherHeader.VoucherType == 10) {
            ReportHTML += '             <div class="col-xs-12 text-center m-t-n"><h3>' + 'إيصال استلام نقدى' + '</h3></div>';
        }
        else {
            ReportHTML += '             <div class="col-xs-12 text-center m-t-n"><h3>' + 'إذن صرف نقدى' + '</h3></div>';
        }
        //ReportHTML += '             <div class="col-xs-12 text-ul"><h4>Payment</h4></div>';

        ReportHTML += '             <div style="clear:both;"></div>';

        ReportHTML += '             <div class="col-xs-12">'
        ReportHTML += '                 <table id="tblPaymentHeaderData" class="table table-striped b-t b-light text-sm table-bordered" style="border:solid #000;font-size:115%;">'; // table-hover
        ReportHTML += '                     <thead>';
        ReportHTML += '                         <td>';
        //ReportHTML += '                             <div class="col-xs-6 text-left"><b>Printed On :</b> ' + TodaysDateddMMyyyy + '</div>';
        ReportHTML += '                             <div class="col-xs-6 m-l-n">' + '<b><span class="float-left">' + '<b>' + TranslateString("ReceiptDate") + ':' + '</span></b>' + ConvertDateFormat(GetDateWithFormatMDY(pVoucherHeader.VoucherDate)) + '</div>';
        ReportHTML += '                             <div class="col-xs-6 m-l-n">' + '<b><span class="float-left">' + '<b>' + TranslateString("Name") + ':' + '</span></b>' + pVoucherHeader.ChargedPerson + '</div>';
        ReportHTML += '                             <div class="col-xs-6 m-l-n">' + '<b><span class="float-left">' + '<b>' + TranslateString("Safe") + ':' + '</span></b>' + pVoucherHeader.SafeName + '</div>';
        ReportHTML += '                             <div class="col-xs-6 m-l-n">' + '<b><span class="float-left">' + '<b>' + TranslateString("Total") + ':' + '</span></b>' + pVoucherHeader.Total.toFixed(2) + ' ' + pVoucherHeader.CurrencyCode + '</div>';
        ReportHTML += '                             <div class="col-xs-6 m-l-n">' + '<b><span class="float-left">' + '<b>' + TranslateString("TotalAfterTax") + ':' + '</span></b>' + pVoucherHeader.TotalAfterTax.toFixed(2) + ' ' + pVoucherHeader.CurrencyCode + '</div>';
        ReportHTML += '                             <div class="col-xs-12 m-l-n">' + '<b><span class="float-left">' + '<b>' + TranslateString("Only") + ':' + '</span></b>' + toWords_WithFractionNumbers(pVoucherHeader.TotalAfterTax.toFixed(2)) + ' ' + pVoucherHeader.CurrencyCode + '</div>';
        if (pVoucherHeader.Notes != 0)
            ReportHTML += '                             <div class="col-xs-12 "><b>' + TranslateString("Notes") + '</b>' + pVoucherHeader.Notes + '</div>';
        ReportHTML += '                         </td>';
        ReportHTML += '                     </thead>';
        ReportHTML += '                 </table>';
        ReportHTML += '             </div>';

        if (pVoucherOperation.length > 0) {
            ReportHTML += '             <div class="col-xs-12 m-t-n-sm text-ul"><h5><b>' + TranslateString("Operations") + ' </b></h5>' + '</div>';
            ReportHTML += '             <div class="row col-xs-12">';
            ReportHTML += '             <div class="col-xs-12">';
            ReportHTML += '                 <table id="tblOperationDetails" class="table table-striped b-t b-light text-sm table-bordered" style="border:solid #000;font-size:115%;">'; // table-hover
            ReportHTML += '                     <thead>';
            ReportHTML += '                         <tr>';
            ReportHTML += '                             <th>' + TranslateString("OperationCode") + '</th>';
            ReportHTML += '                         </tr>';
            ReportHTML += '                     </thead>';
            ReportHTML += '                     <tbody>';
            debugger;

            ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
            ReportHTML += ' <td>';
            $.each(pVoucherOperation, function (i, item) {

                ReportHTML += item.Code + "    &nbsp;&nbsp;&nbsp     ";


            });
            ReportHTML += ' </td>';
            ReportHTML += '                     </tr>';
            ReportHTML += '                         </tr>';
            ReportHTML += '                     </tbody>';
            ReportHTML += '                 </table>';
            ReportHTML += '             </div>';
            ReportHTML += '             </div>';
        }

        ReportHTML += '             <div class="col-xs-12 m-t-n-sm text-ul"><h5><b>' + TranslateString("PaymentDetails") + '</b></h5>' + '</div>';
        ReportHTML += '             <div class="col-xs-12">';
        ReportHTML += '                 <table id="tblPaymentDetails" class="table table-striped b-t b-light text-sm table-bordered" style="border:solid #000;font-size:115%;">'; // table-hover
        ReportHTML += '                     <thead>';
        ReportHTML += '                         <tr>';
        ReportHTML += '                             <th>' + TranslateString("Description") + '</th>';
        ReportHTML += '                             <th>' + TranslateString("Amount") + '</th>';
        ReportHTML += '                             <th>' + TranslateString("Account") + '</th>';
        ReportHTML += '                             <th>' + TranslateString("SubAccount") + '</th>';
        ReportHTML += '                             <th class="">' + TranslateString("CostCenter") + '</th>';
        ReportHTML += '                         </tr>';
        ReportHTML += '                     </thead>';
        ReportHTML += '                     <tbody>';
        debugger;
        $.each(pVoucherDetails, function (i, item) {
            ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
            ReportHTML += '                         <td>' + (item.Description == 0 ? "" : item.Description) + '</td>';
            ReportHTML += '                         <td>' + item.Value + '</td>';
            ReportHTML += '                         <td>' + item.Account_Name + '</td>';
            ReportHTML += '                         <td>' + (item.SubAccount_Name == 0 ? "" : item.SubAccount_Name) + '</td>';
            ReportHTML += '                         <td class="">' + (item.CostCenterName == 0 ? "" : item.CostCenterName) + '</td>';
            //ReportHTML += '                         <td class="">' + toWords_WithFractionNumbers(item.Value.toFixed(4)) + '</td>';
            ReportHTML += '                     </tr>';
        });
        if (pVoucherHeader.TaxValue != 0) {
            ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
            ReportHTML += '                         <td>' + (pVoucherHeader.TaxName == 0 ? "" : pVoucherHeader.TaxName) + '</td>';
            ReportHTML += '                         <td>' + pVoucherHeader.TaxValue + '</td>';
            ReportHTML += '                         <td></td>';
            ReportHTML += '                         <td></td>';
            ReportHTML += '                         <td class=""></td>';
            //ReportHTML += '                         <td class="">' + toWords_WithFractionNumbers(pVoucherHeader.TaxValue.toFixed(4)) + '</td>';
            ReportHTML += '                     </tr>';
        }
        if (pVoucherHeader.TaxValue2 != 0) {
            ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
            ReportHTML += '                         <td>' + (pVoucherHeader.TaxName2 == 0 ? "" : pVoucherHeader.TaxName2) + '</td>';
            ReportHTML += '                         <td>' + pVoucherHeader.TaxValue2 + '</td>';
            ReportHTML += '                         <td></td>';
            ReportHTML += '                         <td></td>';
            ReportHTML += '                         <td class=""></td>';
            //ReportHTML += '                         <td class="">' + toWords_WithFractionNumbers(pVoucherHeader.TaxValue2.toFixed(4)) + '</td>';
            ReportHTML += '                     </tr>';
        }
        ReportHTML += '                         <tr>';
        //ReportHTML += '                             <td colspan=5>' + '<b>TOTAL AMOUNT ONLY : ' + toWords_WithFractionNumbers(pVoucherHeader.TotalAfterTax.toFixed(4)) + ' ' + $("#hDefaultCurrencyCode").val() + '</b></td>';
        ReportHTML += '                             <td colspan=5>' + '<b>' + TranslateString("TOTALAMOUNTONLY") + toWords_WithFractionNumbers(pVoucherHeader.TotalAfterTax.toFixed(2)) + ' ' + pVoucherHeader.CurrencyCode + '</b></td>';
        ReportHTML += '                         </tr>';
        ReportHTML += '                     </tbody>';
        ReportHTML += '                 </table>';
        ReportHTML += '             </div>';
        //ReportHTML += '                 <div class="col-xs-12"><b>Approved available balance till printing time :</b> ' + pVoucherHeader.AvailableBalance + '</div>';


        ReportHTML += '             </div>';

        ReportHTML += '         </body>';
        ReportHTML += '     <footer class="footer col-xs-12" style="width:100%; position:relative; bottom:0;">';
        ReportHTML += '                 <div class="col-xs-3  float-right" ><b><div class="col-xs-11" style="height:100px;border:1px solid #000;">' + TranslateString("PreparedBy") + ' </br> </br>' + (pVoucherHeader.UserName != 0 ? pVoucherHeader.UserName : '') + '</div></b></div>';
        ReportHTML += '                 <div class="col-xs-3  float-right" ><b><div class="col-xs-11" style="height:100px;border:1px solid #000;">' + TranslateString("ReviewedBy") + '</div></div></div>';
        ReportHTML += '                 <div class="col-xs-3  float-right" ><b><div class="col-xs-11" style="height:100px;border:1px solid #000;">' + TranslateString("ApprovedBy") + '</div></b></div>';
        ReportHTML += '                 <div class="col-xs-3  float-right" ><b><div class="col-xs-11" style="height:100px;border:1px solid #000;">' + TranslateString("Receiver") + (pDefaults.UnEditableCompanyName == "ARK" ? " &emsp;&emsp; المستلم  &emsp;&emsp; " : "") + '<br><br>' + TranslateString("Name") + '<br>' + TranslateString("No") + '<br>' + TranslateString("Signature") + ' </div></b></div>';
        ReportHTML += '     <div class="col-xs-12 "><b>' + TranslateString("PrintedOn") + ' :</b> ' + TodaysDateddMMyyyy + '</div>';
        if (pVoucherHeader.VoucherType == 20 && pDefaults.UnEditableCompanyName == "ARK"
            ) {
            ReportHTML += '<br/>';
            ReportHTML += '         <div class="row text-right m-r">'
                + ' <p style="font-size:17px;"><span dir="rtl">  اقر انا الموقع اعلاه في خانة المستلم الموظف في شركة ارك للتجارة واللوجستكس ش.م.م اني استملت المبلغ الموضح اعلاه وذلك كعهدة لاستخدامها في أعمال الشركة كما موضح اعلاه، و أتعهد بالمحافظة عليها و اعادة تسليمها للشركة لدى انتهاء الغرض من استخدامها. و في حالة عدم تسليم العهدة المذكورة أعلاه ، كلها أو بعضها فأنني أفوض الشركة في اتخاذ اللازم، دون أى اعتراض مني   </span>'
                 + '</p></div>';

        }
        //ReportHTML += '         <div class="row text-right m-r">' + '  لاتعتبر الفاتورة مسددة إلا بايصال رسمى مختوم من الشركة  ' + '</div>';
        //ReportHTML += '         <div class="row text-right m-r">' + '  الاعتراض على اى بند من الفاتورة خلال 15 يوم من تاريخ استلام الفاتورة  ' + '</div>';
        //if ($("#cbIsImport").prop("checked") && $("#cbIsOcean").prop("checked"))
        //    ReportHTML += '                 <div class="row m-l">F/FFI-IS-11-03</div>';
        //ReportHTML += '         <div class="row text-center"><img src="/Content/Images/CompanyFooter.jpg" alt="footer"/></div>';
        ReportHTML += '     </footer>';
        ReportHTML += '        <div style="page-break-after: always;"></div>';
        ReportHTML += '</html>';
        //if (cnt != (pVouchersHeader.length - 1))
        //    ReportHTML += '         <div class="break"></div>';
    }

    mywindow.document.write(ReportHTML);
    mywindow.document.close();

}
function PrintDefault(pData) {
    debugger;
    var TodaysDateddMMyyyy = getTodaysDateInddMMyyyyFormat() + ' ' + getTime();
    var pVoucherHeader = JSON.parse(pData[0]);
    var pVoucherDetails = JSON.parse(pData[1]);
    var pVoucherOperation = JSON.parse(pData[2]);
    var mywindow = window.open('', '_blank');

    var ReportHTML = '';
    //<div style="width:500px;height:100px;border:1px solid #000;">This is a rectangle!</div>
    if ($("[id$='hf_ChangeLanguage']").val() == "en") {
        ReportHTML += '<html>';
    }
    else {
        ReportHTML += '<html dir="rtl">';
    }
    ReportHTML += '     <head><title>' + TranslateString("Payment")+'</title><link rel="stylesheet" href="/Content/CSS/app.v2.css" type="text/css" /></head>';
    ReportHTML += '         <body style="background-color:white;font-size:115%;">';

    if (!((pDefaults.UnEditableCompanyName == "PHO" || pDefaults.UnEditableCompanyName == "WFE") && pVoucherHeader.VoucherType == constVoucherCashIn))
        ReportHTML += '             <div class="col-xs-12 text-center"><img src="/Content/Images/CompanyHeader.jpg" alt="logo"/></div>';
    //ReportHTML += '             <div class="col-xs-12 text-center m-t-n"><h3>Invoice No. ' + pInvoiceDate.substr(3, 2) + '/' + pInvoiceDate.substr(8, 2) + '/' + $("#tblInvoices #" + pID + " td.InvoiceNumber").text() + '</h3></div>';

    ReportHTML += '             <div class="col-xs-12 text-center m-t-n"><h3>' + TranslateString(pVoucherHeader.VoucherTypeName) + TranslateString("No") + pVoucherHeader.Code + '</h3></div>';

    if (pVoucherHeader.VoucherType == 10) {
        ReportHTML += '             <div class="col-xs-12 text-center m-t-n"><h3>' + 'إيصال استلام نقدى' + '</h3></div>';
    }
    else {
        ReportHTML += '             <div class="col-xs-12 text-center m-t-n"><h3>' + 'إذن صرف نقدى' + '</h3></div>';
    }
    //ReportHTML += '             <div class="col-xs-12 text-ul"><h4>Payment</h4></div>';

    ReportHTML += '             <div style="clear:both;"></div>';

    ReportHTML += '             <div class="col-xs-12">'
    ReportHTML += '                 <table id="tblPaymentHeaderData" class="table table-striped b-t b-light text-sm table-bordered" style="border:solid #000;font-size:115%;">'; // table-hover
    ReportHTML += '                     <thead>';
    ReportHTML += '                         <td>';
    //ReportHTML += '                             <div class="col-xs-6 text-left"><b>Printed On :</b> ' + TodaysDateddMMyyyy + '</div>';
    ReportHTML += '                             <div class="col-xs-6 m-l-n">' + '<b><span class="float-left">' + '<b>' + TranslateString("ReceiptDate") + ':' + '</span></b>' + ConvertDateFormat(GetDateWithFormatMDY(pVoucherHeader.VoucherDate)) + '</div>';
    ReportHTML += '                             <div class="col-xs-6 m-l-n">' + '<b><span class="float-left">' +'<b>' + TranslateString("Name") + ':' + '</span></b>' + pVoucherHeader.ChargedPerson + '</div>';
    ReportHTML += '                             <div class="col-xs-6 m-l-n">' + '<b><span class="float-left">' +'<b>' + TranslateString("Safe") + ':' + '</span></b>' + pVoucherHeader.SafeName + '</div>';
    ReportHTML += '                             <div class="col-xs-6 m-l-n">' + '<b><span class="float-left">' + '<b>' + TranslateString("Total") + ':' + '</span></b>' + pVoucherHeader.Total.toFixed(2) + ' ' + pVoucherHeader.CurrencyCode + '</div>';
    ReportHTML += '                             <div class="col-xs-6 m-l-n">' + '<b><span class="float-left">' + '<b>' + TranslateString("TotalAfterTax") + ':' + '</span></b>' + pVoucherHeader.TotalAfterTax.toFixed(2) + ' ' + pVoucherHeader.CurrencyCode + '</div>';
    ReportHTML += '                             <div class="col-xs-12 m-l-n">' + '<b><span class="float-left">' + '<b>' + TranslateString("Only") + ':' + '</span></b>' + toWords_WithFractionNumbers(pVoucherHeader.TotalAfterTax.toFixed(2)) + ' ' + pVoucherHeader.CurrencyCode + '</div>';
    if (pVoucherHeader.Notes != 0)
        ReportHTML += '                             <div class="col-xs-12 "><b>' + TranslateString("Notes") + '</b>' + pVoucherHeader.Notes + '</div>';
    ReportHTML += '                         </td>';
    ReportHTML += '                     </thead>';
    ReportHTML += '                 </table>';
    ReportHTML += '             </div>';

    if (pVoucherOperation.length > 0) {
        ReportHTML += '             <div class="col-xs-12 m-t-n-sm text-ul"><h5><b>' + TranslateString("Operations")+' </b></h5>' + '</div>';
        ReportHTML += '             <div class="row col-xs-12">';
        ReportHTML += '             <div class="col-xs-12">';
        ReportHTML += '                 <table id="tblOperationDetails" class="table table-striped b-t b-light text-sm table-bordered" style="border:solid #000;font-size:115%;">'; // table-hover
        ReportHTML += '                     <thead>';
        ReportHTML += '                         <tr>';
        ReportHTML += '                             <th>' + TranslateString("OperationCode")+'</th>';
        ReportHTML += '                         </tr>';
        ReportHTML += '                     </thead>';
        ReportHTML += '                     <tbody>';
        debugger;

        ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
        ReportHTML += ' <td>';
        $.each(pVoucherOperation, function (i, item) {

            ReportHTML += item.Code + "    &nbsp;&nbsp;&nbsp     ";


        });
        ReportHTML += ' </td>';
        ReportHTML += '                     </tr>';
        ReportHTML += '                         </tr>';
        ReportHTML += '                     </tbody>';
        ReportHTML += '                 </table>';
        ReportHTML += '             </div>';
        ReportHTML += '             </div>';
    }

    ReportHTML += '             <div class="col-xs-12 m-t-n-sm text-ul"><h5><b>' + TranslateString("PaymentDetails")+'</b></h5>' + '</div>';
    ReportHTML += '             <div class="col-xs-12">';
    ReportHTML += '                 <table id="tblPaymentDetails" class="table table-striped b-t b-light text-sm table-bordered" style="border:solid #000;font-size:115%;">'; // table-hover
    ReportHTML += '                     <thead>';
    ReportHTML += '                         <tr>';
    ReportHTML += '                             <th>' + TranslateString("Description")+'</th>';
    ReportHTML += '                             <th>' + TranslateString("Amount")+'</th>';
    ReportHTML += '                             <th>' + TranslateString("Account")+'</th>';
    ReportHTML += '                             <th>' + TranslateString("SubAccount") + '</th>';
    ReportHTML += '                             <th class="">' + TranslateString("CostCenter") + '</th>';
    ReportHTML += '                         </tr>';
    ReportHTML += '                     </thead>';
    ReportHTML += '                     <tbody>';
    debugger;
    $.each(pVoucherDetails, function (i, item) {
        ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
        ReportHTML += '                         <td>' + (item.Description == 0 ? "" : item.Description) + '</td>';
        ReportHTML += '                         <td>' + item.Value + '</td>';
        ReportHTML += '                         <td>' + item.Account_Name + '</td>';
        ReportHTML += '                         <td>' + (item.SubAccount_Name == 0 ? "" : item.SubAccount_Name) + '</td>';
        ReportHTML += '                         <td class="">' + (item.CostCenterName == 0 ? "" : item.CostCenterName) + '</td>';
        //ReportHTML += '                         <td class="">' + toWords_WithFractionNumbers(item.Value.toFixed(4)) + '</td>';
        ReportHTML += '                     </tr>';
    });
    if (pVoucherHeader.TaxValue != 0) {
        ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
        ReportHTML += '                         <td>' + (pVoucherHeader.TaxName == 0 ? "" : pVoucherHeader.TaxName) + '</td>';
        ReportHTML += '                         <td>' + pVoucherHeader.TaxValue + '</td>';
        ReportHTML += '                         <td></td>';
        ReportHTML += '                         <td></td>';
        ReportHTML += '                         <td class=""></td>';
        //ReportHTML += '                         <td class="">' + toWords_WithFractionNumbers(pVoucherHeader.TaxValue.toFixed(4)) + '</td>';
        ReportHTML += '                     </tr>';
    }
    if (pVoucherHeader.TaxValue2 != 0) {
        ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
        ReportHTML += '                         <td>' + (pVoucherHeader.TaxName2 == 0 ? "" : pVoucherHeader.TaxName2) + '</td>';
        ReportHTML += '                         <td>' + pVoucherHeader.TaxValue2 + '</td>';
        ReportHTML += '                         <td></td>';
        ReportHTML += '                         <td></td>';
        ReportHTML += '                         <td class=""></td>';
        //ReportHTML += '                         <td class="">' + toWords_WithFractionNumbers(pVoucherHeader.TaxValue2.toFixed(4)) + '</td>';
        ReportHTML += '                     </tr>';
    }
    ReportHTML += '                         <tr>';
    //ReportHTML += '                             <td colspan=5>' + '<b>TOTAL AMOUNT ONLY : ' + toWords_WithFractionNumbers(pVoucherHeader.TotalAfterTax.toFixed(4)) + ' ' + $("#hDefaultCurrencyCode").val() + '</b></td>';
    ReportHTML += '                             <td colspan=5>' + '<b>' + TranslateString("TOTALAMOUNTONLY") + toWords_WithFractionNumbers(pVoucherHeader.TotalAfterTax.toFixed(2)) + ' ' + pVoucherHeader.CurrencyCode + '</b></td>';
    ReportHTML += '                         </tr>';
    ReportHTML += '                     </tbody>';
    ReportHTML += '                 </table>';
    ReportHTML += '             </div>';
    //ReportHTML += '                 <div class="col-xs-12"><b>Approved available balance till printing time :</b> ' + pVoucherHeader.AvailableBalance + '</div>';


    ReportHTML += '             </div>';

    ReportHTML += '         </body>';
    ReportHTML += '     <footer class="footer col-xs-12" style="width:100%; position:relative; bottom:0;">';
    ReportHTML += '                 <div class="col-xs-3  float-right" ><b><div class="col-xs-11" style="height:100px;border:1px solid #000;">' + TranslateString("PreparedBy")+' </br> </br>' + (pVoucherHeader.UserName != 0 ? pVoucherHeader.UserName : '') + '</div></b></div>';
    ReportHTML += '                 <div class="col-xs-3  float-right" ><b><div class="col-xs-11" style="height:100px;border:1px solid #000;">' + TranslateString("ReviewedBy")+'</div></div></div>';
    ReportHTML += '                 <div class="col-xs-3  float-right" ><b><div class="col-xs-11" style="height:100px;border:1px solid #000;">' + TranslateString("ApprovedBy")+'</div></b></div>';
    ReportHTML += '                 <div class="col-xs-3  float-right" ><b><div class="col-xs-11" style="height:100px;border:1px solid #000;">' + TranslateString("Receiver") + (pDefaults.UnEditableCompanyName == "ARK" ? " &emsp;&emsp; المستلم  &emsp;&emsp; " : "") + '<br><br>' + TranslateString("Name") + '<br>' + TranslateString("No") + '<br>' + TranslateString("Signature") + ' </div></b></div>';
    ReportHTML += '     <div class="col-xs-12 "><b>' + TranslateString("PrintedOn") + ' :</b> ' + TodaysDateddMMyyyy + '</div>';
    if (pVoucherHeader.VoucherType == 20   && pDefaults.UnEditableCompanyName == "ARK"
        ) {
        ReportHTML += '<br/>';
        ReportHTML += '         <div class="row text-right m-r">'
            + ' <p style="font-size:17px;"><span dir="rtl">  اقر انا الموقع اعلاه في خانة المستلم الموظف في شركة ارك للتجارة واللوجستكس ش.م.م اني استملت المبلغ الموضح اعلاه وذلك كعهدة لاستخدامها في أعمال الشركة كما موضح اعلاه، و أتعهد بالمحافظة عليها و اعادة تسليمها للشركة لدى انتهاء الغرض من استخدامها. و في حالة عدم تسليم العهدة المذكورة أعلاه ، كلها أو بعضها فأنني أفوض الشركة في اتخاذ اللازم، دون أى اعتراض مني   </span>'
             + '</p></div>';

    }
    //ReportHTML += '         <div class="row text-right m-r">' + '  لاتعتبر الفاتورة مسددة إلا بايصال رسمى مختوم من الشركة  ' + '</div>';
    //ReportHTML += '         <div class="row text-right m-r">' + '  الاعتراض على اى بند من الفاتورة خلال 15 يوم من تاريخ استلام الفاتورة  ' + '</div>';
    //if ($("#cbIsImport").prop("checked") && $("#cbIsOcean").prop("checked"))
    //    ReportHTML += '                 <div class="row m-l">F/FFI-IS-11-03</div>';
    //ReportHTML += '         <div class="row text-center"><img src="/Content/Images/CompanyFooter.jpg" alt="footer"/></div>';
    ReportHTML += '     </footer>';
    ReportHTML += '</html>';
    mywindow.document.write(ReportHTML);
    mywindow.document.close();

}
function PrintCapital(pData) {
    var TodaysDateddMMyyyy = getTodaysDateInddMMyyyyFormat() + ' ' + getTime();
    var pVoucherHeader = JSON.parse(pData[0]);
    var pVoucherDetails = JSON.parse(pData[1]);
    var pVoucherOperation = JSON.parse(pData[2]);
    var mywindow = window.open('', '_blank');

    var ReportHTML = '';
    //<div style="width:500px;height:100px;border:1px solid #000;">This is a rectangle!</div>
    if ($("[id$='hf_ChangeLanguage']").val() == "en") {
        ReportHTML += '<html>';
    }
    else {
        ReportHTML += '<html dir="rtl">';
    }
    ReportHTML += '     <head><title>' + TranslateString("Payment") + '</title><link rel="stylesheet" href="/Content/CSS/app.v2.css" type="text/css" /></head>';
    ReportHTML += '         <body style="background-color:white;font-size:115%;">';
    ReportHTML += '             <div class="col-xs-12 text-center"><img src="/Content/Images/CompanyHeader.jpg" alt="logo"/></div>';
    //ReportHTML += '             <div class="col-xs-12 text-center m-t-n"><h3>Invoice No. ' + pInvoiceDate.substr(3, 2) + '/' + pInvoiceDate.substr(8, 2) + '/' + $("#tblInvoices #" + pID + " td.InvoiceNumber").text() + '</h3></div>';

    ReportHTML += '             <div class="col-xs-12 text-center m-t-n"><h3>' + TranslateString(pVoucherHeader.VoucherTypeName) + TranslateString("No") + pVoucherHeader.Code + '</h3></div>';

    if (pVoucherHeader.VoucherType == 10) {
        ReportHTML += '             <div class="col-xs-12 text-center m-t-n"><h3>' + 'إيصال استلام نقدى' + '</h3></div>';
    }
    else {
        ReportHTML += '             <div class="col-xs-12 text-center m-t-n"><h3>' + 'إذن صرف نقدى' + '</h3></div>';
    }
    //ReportHTML += '             <div class="col-xs-12 text-ul"><h4>Payment</h4></div>';

    ReportHTML += '             <div style="clear:both;"></div>';

    ReportHTML += '             <div class="col-xs-12">'
    ReportHTML += '                 <table id="tblPaymentHeaderData" class="table table-striped b-t b-light text-sm table-bordered" style="border:solid #000;font-size:115%;">'; // table-hover
    ReportHTML += '                     <thead>';
    ReportHTML += '                         <td>';
    //ReportHTML += '                             <div class="col-xs-6 text-left"><b>Printed On :</b> ' + TodaysDateddMMyyyy + '</div>';
    ReportHTML += '                             <div class="col-xs-6 m-l-n">' + '<b><span class="float-left">' + '<b>' + TranslateString("ReceiptDate") + ':' + '</span></b>' + ConvertDateFormat(GetDateWithFormatMDY(pVoucherHeader.VoucherDate)) + '</div>';
    ReportHTML += '                             <div class="col-xs-6 m-l-n">' + '<b><span class="float-left">' + '<b>' + TranslateString("Name") + ':' + '</span></b>' + pVoucherHeader.ChargedPerson + '</div>';
    ReportHTML += '                             <div class="col-xs-6 m-l-n">' + '<b><span class="float-left">' + '<b>' + TranslateString("Safe") + ':' + '</span></b>' + pVoucherHeader.SafeName + '</div>';
    ReportHTML += '                             <div class="col-xs-6 m-l-n">' + '<b><span class="float-left">' + '<b>' + TranslateString("Total") + ':' + '</span></b>' + pVoucherHeader.Total.toFixed(2) + ' ' + pVoucherHeader.CurrencyCode + '</div>';
    ReportHTML += '                             <div class="col-xs-6 m-l-n">' + '<b><span class="float-left">' + '<b>' + TranslateString("TotalAfterTax") + ':' + '</span></b>' + pVoucherHeader.TotalAfterTax.toFixed(2) + ' ' + pVoucherHeader.CurrencyCode + '</div>';
    ReportHTML += '                             <div class="col-xs-12 m-l-n">' + '<b><span class="float-left">' + '<b>' + TranslateString("Only") + ':' + '</span></b>' + toWords_WithFractionNumbers(pVoucherHeader.TotalAfterTax.toFixed(2)) + ' ' + pVoucherHeader.CurrencyCode + '</div>';
    if (pVoucherHeader.Notes != 0)
        ReportHTML += '                             <div class="col-xs-12 "><b>' + TranslateString("Notes") + '</b>' + pVoucherHeader.Notes + '</div>';
    ReportHTML += '                         </td>';
    ReportHTML += '                     </thead>';
    ReportHTML += '                 </table>';
    ReportHTML += '             </div>';

    if (pVoucherOperation.length > 0) {
        ReportHTML += '             <div class="col-xs-12 m-t-n-sm text-ul"><h5><b>' + TranslateString("Operations") + ' </b></h5>' + '</div>';
        ReportHTML += '             <div class="row col-xs-12">';
        ReportHTML += '             <div class="col-xs-12">';
        ReportHTML += '                 <table id="tblOperationDetails" class="table table-striped b-t b-light text-sm table-bordered" style="border:solid #000;font-size:115%;">'; // table-hover
        ReportHTML += '                     <thead>';
        ReportHTML += '                         <tr>';
        ReportHTML += '                             <th>' + TranslateString("OperationCode") + '</th>';
        ReportHTML += '                         </tr>';
        ReportHTML += '                     </thead>';
        ReportHTML += '                     <tbody>';
        debugger;

        ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
        ReportHTML += ' <td>';
        $.each(pVoucherOperation, function (i, item) {

            ReportHTML += item.Code + "    &nbsp;&nbsp;&nbsp     ";


        });
        ReportHTML += ' </td>';
        ReportHTML += '                     </tr>';
        ReportHTML += '                         </tr>';
        ReportHTML += '                     </tbody>';
        ReportHTML += '                 </table>';
        ReportHTML += '             </div>';
        ReportHTML += '             </div>';
    }

    ReportHTML += '             <div class="col-xs-12 m-t-n-sm text-ul"><h5><b>' + TranslateString("PaymentDetails") + '</b></h5>' + '</div>';
    ReportHTML += '             <div class="col-xs-12">';
    ReportHTML += '                 <table id="tblPaymentDetails" class="table table-striped b-t b-light text-sm table-bordered" style="border:solid #000;font-size:115%;">'; // table-hover
    ReportHTML += '                     <thead>';
    ReportHTML += '                         <tr>';
    ReportHTML += '                             <th>' + TranslateString("Description") + '</th>';
    ReportHTML += '                             <th>' + TranslateString("Amount") + '</th>';
    ReportHTML += '                             <th>' + TranslateString("Account") + '</th>';
    ReportHTML += '                             <th>' + TranslateString("SubAccount") + '</th>';
    ReportHTML += '                             <th class="">' + TranslateString("CostCenter") + '</th>';
    ReportHTML += '                         </tr>';
    ReportHTML += '                     </thead>';
    ReportHTML += '                     <tbody>';
    debugger;
    $.each(pVoucherDetails, function (i, item) {
        ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
        ReportHTML += '                         <td>' + (item.Description == 0 ? "" : item.Description) + '</td>';
        ReportHTML += '                         <td>' + item.Value + '</td>';
        ReportHTML += '                         <td>' + item.Account_Name + '</td>';
        ReportHTML += '                         <td>' + (item.SubAccount_Name == 0 ? "" : item.SubAccount_Name) + '</td>';
        ReportHTML += '                         <td class="">' + (item.CostCenterName == 0 ? "" : item.CostCenterName) + '</td>';
        //ReportHTML += '                         <td class="">' + toWords_WithFractionNumbers(item.Value.toFixed(4)) + '</td>';
        ReportHTML += '                     </tr>';
    });
    if (pVoucherHeader.TaxValue != 0) {
        ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
        ReportHTML += '                         <td>' + (pVoucherHeader.TaxName == 0 ? "" : pVoucherHeader.TaxName) + '</td>';
        ReportHTML += '                         <td>' + pVoucherHeader.TaxValue + '</td>';
        ReportHTML += '                         <td></td>';
        ReportHTML += '                         <td></td>';
        ReportHTML += '                         <td class=""></td>';
        //ReportHTML += '                         <td class="">' + toWords_WithFractionNumbers(pVoucherHeader.TaxValue.toFixed(4)) + '</td>';
        ReportHTML += '                     </tr>';
    }
    if (pVoucherHeader.TaxValue2 != 0) {
        ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
        ReportHTML += '                         <td>' + (pVoucherHeader.TaxName2 == 0 ? "" : pVoucherHeader.TaxName2) + '</td>';
        ReportHTML += '                         <td>' + pVoucherHeader.TaxValue2 + '</td>';
        ReportHTML += '                         <td></td>';
        ReportHTML += '                         <td></td>';
        ReportHTML += '                         <td class=""></td>';
        //ReportHTML += '                         <td class="">' + toWords_WithFractionNumbers(pVoucherHeader.TaxValue2.toFixed(4)) + '</td>';
        ReportHTML += '                     </tr>';
    }
    ReportHTML += '                         <tr>';
    //ReportHTML += '                             <td colspan=5>' + '<b>TOTAL AMOUNT ONLY : ' + toWords_WithFractionNumbers(pVoucherHeader.TotalAfterTax.toFixed(4)) + ' ' + $("#hDefaultCurrencyCode").val() + '</b></td>';
    ReportHTML += '                             <td colspan=5>' + '<b>' + TranslateString("TOTALAMOUNTONLY") + toWords_WithFractionNumbers(pVoucherHeader.TotalAfterTax.toFixed(2)) + ' ' + pVoucherHeader.CurrencyCode + '</b></td>';
    ReportHTML += '                         </tr>';
    ReportHTML += '                     </tbody>';
    ReportHTML += '                 </table>';
    ReportHTML += '             </div>';
    //ReportHTML += '                 <div class="col-xs-12"><b>Approved available balance till printing time :</b> ' + pVoucherHeader.AvailableBalance + '</div>';


    ReportHTML += '             </div>';

    ReportHTML += '         </body>';
    ReportHTML += '     <footer class="footer col-xs-12" style="width:100%; position:relative; bottom:0;">';
    ReportHTML += '                 <div class="col-xs-3  float-right" ><b><div class="col-xs-11" style="height:100px;border:1px solid #000;">' + TranslateString("PreparedBy") + ' </br> </br>' + (pVoucherHeader.UserName != 0 ? pVoucherHeader.UserName : '') + '</div></b></div>';
    ReportHTML += '                 <div class="col-xs-3  float-right" ><b><div class="col-xs-11" style="height:100px;border:1px solid #000;">' + TranslateString("ReviewedBy") + '</div></div></div>';
    ReportHTML += '                 <div class="col-xs-3  float-right" ><b><div class="col-xs-11" style="height:100px;border:1px solid #000;">' + TranslateString("ApprovedBy") + '</div></b></div>';
    ReportHTML += '                 <div class="col-xs-3  float-right" ><b><div class="col-xs-11" style="height:100px;border:1px solid #000;">' + TranslateString("Receiver") + (pDefaults.UnEditableCompanyName == "ARK" ? " &emsp;&emsp; المستلم  &emsp;&emsp; " : "") + '<br><br>' + TranslateString("Name") + '<br>' + TranslateString("No") + '<br>' + TranslateString("Signature") + ' </div></b></div>';
    ReportHTML += '     <div class="col-xs-12 "><b>' + TranslateString("PrintedOn") + ' :</b> ' + TodaysDateddMMyyyy + '</div>';

//------------------------------------------------------------------------Copy 2---------------------------------------------------------------------------------------
    ReportHTML += ' <hr width="400" style="border: 2px dashed #C0C0C0"  > ';



    ReportHTML += '             <div class="col-xs-12 text-center m-t-n"><h3>' + TranslateString(pVoucherHeader.VoucherTypeName) + TranslateString("No") + pVoucherHeader.Code + '</h3></div>';

    if (pVoucherHeader.VoucherType == 10) {
        ReportHTML += '             <div class="col-xs-12 text-center m-t-n"><h3>' + 'إيصال استلام نقدى' + '</h3></div>';
    }
    else {
        ReportHTML += '             <div class="col-xs-12 text-center m-t-n"><h3>' + 'إذن صرف نقدى' + '</h3></div>';
    }
    //ReportHTML += '             <div class="col-xs-12 text-ul"><h4>Payment</h4></div>';

    ReportHTML += '             <div style="clear:both;"></div>';

    ReportHTML += '             <div class="col-xs-12">'
    ReportHTML += '                 <table id="tblPaymentHeaderData" class="table table-striped b-t b-light text-sm table-bordered" style="border:solid #000;font-size:115%;">'; // table-hover
    ReportHTML += '                     <thead>';
    ReportHTML += '                         <td>';
    //ReportHTML += '                             <div class="col-xs-6 text-left"><b>Printed On :</b> ' + TodaysDateddMMyyyy + '</div>';
    ReportHTML += '                             <div class="col-xs-6 m-l-n">' + '<b><span class="float-left">' + '<b>' + TranslateString("ReceiptDate") + ':' + '</span></b>' + ConvertDateFormat(GetDateWithFormatMDY(pVoucherHeader.VoucherDate)) + '</div>';
    ReportHTML += '                             <div class="col-xs-6 m-l-n">' + '<b><span class="float-left">' + '<b>' + TranslateString("Name") + ':' + '</span></b>' + pVoucherHeader.ChargedPerson + '</div>';
    ReportHTML += '                             <div class="col-xs-6 m-l-n">' + '<b><span class="float-left">' + '<b>' + TranslateString("Safe") + ':' + '</span></b>' + pVoucherHeader.SafeName + '</div>';
    ReportHTML += '                             <div class="col-xs-6 m-l-n">' + '<b><span class="float-left">' + '<b>' + TranslateString("Total") + ':' + '</span></b>' + pVoucherHeader.Total.toFixed(2) + ' ' + pVoucherHeader.CurrencyCode + '</div>';
    ReportHTML += '                             <div class="col-xs-6 m-l-n">' + '<b><span class="float-left">' + '<b>' + TranslateString("TotalAfterTax") + ':' + '</span></b>' + pVoucherHeader.TotalAfterTax.toFixed(2) + ' ' + pVoucherHeader.CurrencyCode + '</div>';
    ReportHTML += '                             <div class="col-xs-12 m-l-n">' + '<b><span class="float-left">' + '<b>' + TranslateString("Only") + ':' + '</span></b>' + toWords_WithFractionNumbers(pVoucherHeader.TotalAfterTax.toFixed(2)) + ' ' + pVoucherHeader.CurrencyCode + '</div>';
    if (pVoucherHeader.Notes != 0)
        ReportHTML += '                             <div class="col-xs-12 "><b>' + TranslateString("Notes") + '</b>' + pVoucherHeader.Notes + '</div>';
    ReportHTML += '                         </td>';
    ReportHTML += '                     </thead>';
    ReportHTML += '                 </table>';
    ReportHTML += '             </div>';

    if (pVoucherOperation.length > 0) {
        ReportHTML += '             <div class="col-xs-12 m-t-n-sm text-ul"><h5><b>' + TranslateString("Operations") + ' </b></h5>' + '</div>';
        ReportHTML += '             <div class="row col-xs-12">';
        ReportHTML += '             <div class="col-xs-12">';
        ReportHTML += '                 <table id="tblOperationDetails" class="table table-striped b-t b-light text-sm table-bordered" style="border:solid #000;font-size:115%;">'; // table-hover
        ReportHTML += '                     <thead>';
        ReportHTML += '                         <tr>';
        ReportHTML += '                             <th>' + TranslateString("OperationCode") + '</th>';
        ReportHTML += '                         </tr>';
        ReportHTML += '                     </thead>';
        ReportHTML += '                     <tbody>';
        debugger;

        ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
        ReportHTML += ' <td>';
        $.each(pVoucherOperation, function (i, item) {

            ReportHTML += item.Code + "    &nbsp;&nbsp;&nbsp     ";


        });
        ReportHTML += ' </td>';
        ReportHTML += '                     </tr>';
        ReportHTML += '                         </tr>';
        ReportHTML += '                     </tbody>';
        ReportHTML += '                 </table>';
        ReportHTML += '             </div>';
        ReportHTML += '             </div>';
    }

    ReportHTML += '             <div class="col-xs-12 m-t-n-sm text-ul"><h5><b>' + TranslateString("PaymentDetails") + '</b></h5>' + '</div>';
    ReportHTML += '             <div class="col-xs-12">';
    ReportHTML += '                 <table id="tblPaymentDetails" class="table table-striped b-t b-light text-sm table-bordered" style="border:solid #000;font-size:115%;">'; // table-hover
    ReportHTML += '                     <thead>';
    ReportHTML += '                         <tr>';
    ReportHTML += '                             <th>' + TranslateString("Description") + '</th>';
    ReportHTML += '                             <th>' + TranslateString("Amount") + '</th>';
    ReportHTML += '                             <th>' + TranslateString("Account") + '</th>';
    ReportHTML += '                             <th>' + TranslateString("SubAccount") + '</th>';
    ReportHTML += '                             <th class="">' + TranslateString("CostCenter") + '</th>';
    ReportHTML += '                         </tr>';
    ReportHTML += '                     </thead>';
    ReportHTML += '                     <tbody>';
    debugger;
    $.each(pVoucherDetails, function (i, item) {
        ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
        ReportHTML += '                         <td>' + (item.Description == 0 ? "" : item.Description) + '</td>';
        ReportHTML += '                         <td>' + item.Value + '</td>';
        ReportHTML += '                         <td>' + item.Account_Name + '</td>';
        ReportHTML += '                         <td>' + (item.SubAccount_Name == 0 ? "" : item.SubAccount_Name) + '</td>';
        ReportHTML += '                         <td class="">' + (item.CostCenterName == 0 ? "" : item.CostCenterName) + '</td>';
        //ReportHTML += '                         <td class="">' + toWords_WithFractionNumbers(item.Value.toFixed(4)) + '</td>';
        ReportHTML += '                     </tr>';
    });
    if (pVoucherHeader.TaxValue != 0) {
        ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
        ReportHTML += '                         <td>' + (pVoucherHeader.TaxName == 0 ? "" : pVoucherHeader.TaxName) + '</td>';
        ReportHTML += '                         <td>' + pVoucherHeader.TaxValue + '</td>';
        ReportHTML += '                         <td></td>';
        ReportHTML += '                         <td></td>';
        ReportHTML += '                         <td class=""></td>';
        //ReportHTML += '                         <td class="">' + toWords_WithFractionNumbers(pVoucherHeader.TaxValue.toFixed(4)) + '</td>';
        ReportHTML += '                     </tr>';
    }
    if (pVoucherHeader.TaxValue2 != 0) {
        ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
        ReportHTML += '                         <td>' + (pVoucherHeader.TaxName2 == 0 ? "" : pVoucherHeader.TaxName2) + '</td>';
        ReportHTML += '                         <td>' + pVoucherHeader.TaxValue2 + '</td>';
        ReportHTML += '                         <td></td>';
        ReportHTML += '                         <td></td>';
        ReportHTML += '                         <td class=""></td>';
        //ReportHTML += '                         <td class="">' + toWords_WithFractionNumbers(pVoucherHeader.TaxValue2.toFixed(4)) + '</td>';
        ReportHTML += '                     </tr>';
    }
    ReportHTML += '                         <tr>';
    //ReportHTML += '                             <td colspan=5>' + '<b>TOTAL AMOUNT ONLY : ' + toWords_WithFractionNumbers(pVoucherHeader.TotalAfterTax.toFixed(4)) + ' ' + $("#hDefaultCurrencyCode").val() + '</b></td>';
    ReportHTML += '                             <td colspan=5>' + '<b>' + TranslateString("TOTALAMOUNTONLY") + toWords_WithFractionNumbers(pVoucherHeader.TotalAfterTax.toFixed(2)) + ' ' + pVoucherHeader.CurrencyCode + '</b></td>';
    ReportHTML += '                         </tr>';
    ReportHTML += '                     </tbody>';
    ReportHTML += '                 </table>';
    ReportHTML += '             </div>';
    //ReportHTML += '                 <div class="col-xs-12"><b>Approved available balance till printing time :</b> ' + pVoucherHeader.AvailableBalance + '</div>';


    ReportHTML += '             </div>';

    ReportHTML += '         </body>';
    ReportHTML += '     <footer class="footer col-xs-12" style="width:100%; position:relative; bottom:0;">';
    ReportHTML += '                 <div class="col-xs-3  float-right" ><b><div class="col-xs-11" style="height:100px;border:1px solid #000;">' + TranslateString("PreparedBy") + ' </br> </br>' + (pVoucherHeader.UserName != 0 ? pVoucherHeader.UserName : '') + '</div></b></div>';
    ReportHTML += '                 <div class="col-xs-3  float-right" ><b><div class="col-xs-11" style="height:100px;border:1px solid #000;">' + TranslateString("ReviewedBy") + '</div></div></div>';
    ReportHTML += '                 <div class="col-xs-3  float-right" ><b><div class="col-xs-11" style="height:100px;border:1px solid #000;">' + TranslateString("ApprovedBy") + '</div></b></div>';
    ReportHTML += '                 <div class="col-xs-3  float-right" ><b><div class="col-xs-11" style="height:100px;border:1px solid #000;">' + TranslateString("Receiver") + (pDefaults.UnEditableCompanyName == "ARK" ? " &emsp;&emsp; المستلم  &emsp;&emsp; " : "") + '<br><br>' + TranslateString("Name") + '<br>' + TranslateString("No") + '<br>' + TranslateString("Signature") + ' </div></b></div>';
    ReportHTML += '     <div class="col-xs-12 "><b>' + TranslateString("PrintedOn") + ' :</b> ' + TodaysDateddMMyyyy + '</div>';

    ReportHTML += '     </footer>';
    ReportHTML += '</html>';
    mywindow.document.write(ReportHTML);
    mywindow.document.close();

}
function PrintFairtrans(pData) {
    debugger;
    var TodaysDateddMMyyyy = getTodaysDateInddMMyyyyFormat() + ' ' + getTime();
    var pVoucherHeader = JSON.parse(pData[0]);
    var pVoucherDetails = JSON.parse(pData[1]);
    var pVoucherOperation = JSON.parse(pData[2]);
    var pVoucherInvItem = JSON.parse(pData[3]);

    var mywindow = window.open('', '_blank');

    var ReportHTML = '';
    var ItemTotal = 0;
    var InvoiceNumber = '';
    var OperationNo = '';
    var BLNumber = '';
    //<div style="width:500px;height:100px;border:1px solid #000;">This is a rectangle!</div>
    ReportHTML += '<html>';
    ReportHTML += '     <head><title>Payment</title><link rel="stylesheet" href="/Content/CSS/app.v2.css" type="text/css" /></head>';
    ReportHTML += '         <body style="background-color:white;font-size:115%;">';
    ReportHTML += '             <div class="col-xs-12 text-center"><img src="/Content/Images/CompanyHeader.jpg" alt="logo"/></div>';
    //ReportHTML += '             <div class="col-xs-12 text-center m-t-n"><h3>Invoice No. ' + pInvoiceDate.substr(3, 2) + '/' + pInvoiceDate.substr(8, 2) + '/' + $("#tblInvoices #" + pID + " td.InvoiceNumber").text() + '</h3></div>';

    ReportHTML += '             <div class="col-xs-12 text-center m-t-n"><h3>' + TranslateString(pVoucherHeader.VoucherTypeName) + ' No. ' + pVoucherHeader.Code + '</h3></div>';

    if (pVoucherHeader.VoucherType == 10) {
        ReportHTML += '             <div class="col-xs-12 text-center m-t-n"><h3>' + 'إيصال استلام نقدى' + '</h3></div>';
    }
    else {
        ReportHTML += '             <div class="col-xs-12 text-center m-t-n"><h3>' + 'إذن صرف نقدى' + '</h3></div>';
    }
    //ReportHTML += '             <div class="col-xs-12 text-ul"><h4>Payment</h4></div>';

    ReportHTML += '             <div style="clear:both;"></div>';

    ReportHTML += '             <div class="col-xs-12">'
    ReportHTML += '                 <table id="tblPaymentHeaderData" class="table table-striped b-t b-light text-sm table-bordered" style="border:solid #000;font-size:115%;">'; // table-hover
    ReportHTML += '                     <thead>';
    ReportHTML += '                         <td>';
    //ReportHTML += '                             <div class="col-xs-6 text-left"><b>Printed On :</b> ' + TodaysDateddMMyyyy + '</div>';
    ReportHTML += '                             <div class="col-xs-6 text-left"><b>Voucher Date: ' + '</b>' + ConvertDateFormat(GetDateWithFormatMDY(pVoucherHeader.VoucherDate)) + '</div>';
    ReportHTML += '                             <div class="col-xs-6 text-left"><b>Name: ' + '</b>' + pVoucherHeader.ChargedPerson + '</div>';
    ReportHTML += '                             <div class="col-xs-6 text-left"><b>Safe: ' + '</b>' + pVoucherHeader.SafeName + '</div>';
    ReportHTML += '                             <div class="col-xs-6 text-left"><b>Total: ' + '</b>' + pVoucherHeader.Total.toFixed(2) + ' ' + pVoucherHeader.CurrencyCode + '</div>';
    //ReportHTML += '                             <div class="col-xs-6 text-left"><b>Total After Tax: ' + '</b>' + pVoucherHeader.TotalAfterTax.toFixed(2) + ' ' + pVoucherHeader.CurrencyCode + '</div>';
    //ReportHTML += '                             <div class="col-xs-12 text-left"><b>Only: ' + '</b>' + toWords_WithFractionNumbers(pVoucherHeader.TotalAfterTax.toFixed(2)) + ' ' + pVoucherHeader.CurrencyCode + '</div>';
    if (pVoucherHeader.Notes != 0)
        ReportHTML += '                             <div class="col-xs-12 text-left"><b>Notes: ' + '</b>' + pVoucherHeader.Notes + '</div>';
    ReportHTML += '                         </td>';
    ReportHTML += '                     </thead>';
    ReportHTML += '                 </table>';
    ReportHTML += '             </div>';

    if (pVoucherOperation.length > 0) {
        ReportHTML += '             <div class="col-xs-12 m-t-n-sm text-ul"><h5><b>Operations  : </b></h5>' + '</div>';
        ReportHTML += '             <div class="row col-xs-12">';
        ReportHTML += '             <div class="col-xs-12">';
        ReportHTML += '                 <table id="tblOperationDetails" class="table table-striped b-t b-light text-sm table-bordered" style="border:solid #000;font-size:115%;">'; // table-hover
        ReportHTML += '                     <thead>';
        ReportHTML += '                         <tr>';
        ReportHTML += '                             <th>Operation Code</th>';
        ReportHTML += '                         </tr>';
        ReportHTML += '                     </thead>';
        ReportHTML += '                     <tbody>';
        debugger;

        ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
        ReportHTML += ' <td>';
        $.each(pVoucherOperation, function (i, item) {

            ReportHTML += item.Code + "    &nbsp;&nbsp;&nbsp     ";


        });
        ReportHTML += ' </td>';
        ReportHTML += '                     </tr>';
        ReportHTML += '                         </tr>';
        ReportHTML += '                     </tbody>';
        ReportHTML += '                 </table>';
        ReportHTML += '             </div>';
        ReportHTML += '             </div>';
    }

    ReportHTML += '             <div class="col-xs-12 m-t-n-sm text-ul"><h5><b>Payment Details : </b></h5>' + '</div>';
    ReportHTML += '             <div class="col-xs-12">';
    ReportHTML += '                 <table id="tblPaymentDetails" class="table table-striped b-t b-light text-sm table-bordered" style="border:solid #000;font-size:115%;">'; // table-hover
    ReportHTML += '                     <thead>';
    ReportHTML += '                         <tr>';
    ReportHTML += '                             <th>Description</th>';
    //ReportHTML += '                             <th>Amount</th>';
    //ReportHTML += '                             <th>Account</th>';
    //ReportHTML += '                             <th>Sub Account</th>';
    ReportHTML += '                             <th class="">Amount</th>';
    ReportHTML += '                         </tr>';
    ReportHTML += '                     </thead>';
    ReportHTML += '                     <tbody>';
    debugger;
    //if ($.each(pVoucherInvItem.length - 1 == i)) {
    //    ItemTotal = item.SaleAmount
    //}
    ItemTotal = 0;
    $.each(pVoucherDetails, function (j, item2) {
        //if ($(pVoucherDetails).length -1 == j) {
        ItemTotal += item2.Value;
        //}
    });

    $.each(pVoucherInvItem, function (i, item) {

        if (i == 0) {
            ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
            //<b>Name: ' + '</b>' + pVoucherHeader.ChargedPerson + '
            ReportHTML += '                         <td> <b> Invoice NO: ' + '</b>' + item.InvoiceNumber + '<b>' + ' Operation No:' + '</b>' + item.OperationNo + '<b>' + ' B\L NO:' + '</b>' + item.BLNumber + '</td>';
            //ReportHTML += '                         <td>' + ItemTotal + '</td>';
            //ReportHTML += '                         <td>' +""+ '</td>';
            //ReportHTML += '                         <td>' + "" + '</td>';
            ReportHTML += '                         <td class="">' + ItemTotal + '</td>';
            ReportHTML += '                     </tr>';
        }
    });
    if (pVoucherHeader.TaxValue != 0) {
        ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
        ReportHTML += '                         <td>' + (pVoucherHeader.TaxName == 0 ? "" : pVoucherHeader.TaxName) + '</td>';
        //ReportHTML += '                         <td > ' + pVoucherHeader.TaxValue + '</td>';
        //ReportHTML += '                         <td>' + "" + '</td>';
        //ReportHTML += '                         <td>' + "" + '</td>';
        ReportHTML += '                         <td class="">' + pVoucherHeader.TaxValue + '</td>';
        ReportHTML += '                     </tr>';
    }
    if (pVoucherHeader.TaxValue2 != 0) {
        ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
        ReportHTML += '                         <td>' + (pVoucherHeader.TaxName2 == 0 ? "" : pVoucherHeader.TaxName2) + '</td>';
        //ReportHTML += '                         <td >' + pVoucherHeader.TaxValue2 + '</td>';
        //ReportHTML += '                         <td>' + "" + '</td>';
        //ReportHTML += '                         <td>' + "" + '</td>';
        ReportHTML += '                         <td class="">' + pVoucherHeader.TaxValue2 + '</td>';
        //ReportHTML += '                         <td></td>';
        //ReportHTML += '                         <td></td>';
        //ReportHTML += '                         <td class=""></td>';
        //ReportHTML += '                         <td class="">' + toWords_WithFractionNumbers(pVoucherHeader.TaxValue2.toFixed(4)) + '</td>';
        ReportHTML += '                     </tr>';
    }
    ReportHTML += '                         <tr>';
    //ReportHTML += '                             <td colspan=5>' + '<b>TOTAL AMOUNT ONLY : ' + toWords_WithFractionNumbers(pVoucherHeader.TotalAfterTax.toFixed(4)) + ' ' + $("#hDefaultCurrencyCode").val() + '</b></td>';
    ReportHTML += '                             <td colspan=2>' + '<b>TOTAL AMOUNT ONLY : ' + toWords_WithFractionNumbers(pVoucherHeader.TotalAfterTax.toFixed(2)) + ' ' + pVoucherHeader.CurrencyCode + '</b></td>';
    ReportHTML += '                         </tr>';
    ReportHTML += '                     </tbody>';
    ReportHTML += '                 </table>';
    ReportHTML += '             </div>';
    //ReportHTML += '                 <div class="col-xs-12"><b>Approved available balance till printing time :</b> ' + pVoucherHeader.AvailableBalance + '</div>';


    ReportHTML += '             </div>';

    ReportHTML += '         </body>';
    ReportHTML += '     <footer class="footer col-xs-12" style="width:100%; position:relative; bottom:0;">';
    //ReportHTML += '                 <div class="col-xs-3  float-right" ><b><div class="col-xs-11" style="height:100px;border:1px solid #000;">Prepared By </br> </br>' + (pVoucherHeader.UserName != 0 ? pVoucherHeader.UserName : '') + '</div></b></div>';
    //ReportHTML += '                 <div class="col-xs-3  float-right" ><b><div class="col-xs-11" style="height:100px;border:1px solid #000;">Reviewed By</div></div></div>';
    //ReportHTML += '                 <div class="col-xs-3  float-right" ><b><div class="col-xs-11" style="height:100px;border:1px solid #000;">Approved By</div></b></div>';
    ReportHTML += '                 <div class="col-xs-12  float-Left" ><b><div class="col-xs-2" style="height:80px;border:1px solid #000;">Stamp<br> </div></b></div>';
    ReportHTML += '                 <div class="col-xs-6  float-right" style="font-size:150%;">' + '  لا يعتد بهذا الإيصال إلا إذا كان مختوما بختم الشركة  ' + '</div>';
    ReportHTML += '    <br>'
    //ReportHTML += '     <div class="col-xs-12 text-left"><b>Printed On :</b> ' + TodaysDateddMMyyyy + '</div>';
    //ReportHTML += '         <div class="row text-right m-r">' + '  لا يعتد بهذا الإيصال إلا إذا كان مختوما بختم الشركة  ' + '</div>';
    //ReportHTML += '         <div class="row text-right m-r">' + '  الاعتراض على اى بند من الفاتورة خلال 15 يوم من تاريخ استلام الفاتورة  ' + '</div>';
    //if ($("#cbIsImport").prop("checked") && $("#cbIsOcean").prop("checked"))
    //    ReportHTML += '                 <div class="row m-l">F/FFI-IS-11-03</div>';
    //ReportHTML += '         <div class="row text-center"><img src="/Content/Images/CompanyFooter.jpg" alt="footer"/></div>';
    ReportHTML += '     </footer>';
    ReportHTML += '    <br>'
    ReportHTML += '             <div class="col-xs-12 text-center"> <br> </div>';
    ReportHTML += '             <hr class="col-xs-12 text-center" style="border:none; border-top:1px dashed #000; ">';
    //////////////////////////////////////////////////////////////////////////////////

    ReportHTML += '         <body style="background-color:white;font-size:115%;">';
    //ReportHTML += '             <div class="col-xs-12 text-center m-t-n"><h3>Invoice No. ' + pInvoiceDate.substr(3, 2) + '/' + pInvoiceDate.substr(8, 2) + '/' + $("#tblInvoices #" + pID + " td.InvoiceNumber").text() + '</h3></div>';

    ReportHTML += '             <div class="col-xs-12 text-center m-t-n"><h3>' + TranslateString(pVoucherHeader.VoucherTypeName) + ' No. ' + pVoucherHeader.Code + '</h3></div>';

    if (pVoucherHeader.VoucherType == 10) {
        ReportHTML += '             <div class="col-xs-12 text-center m-t-n"><h3>' + 'إيصال استلام نقدى' + '</h3></div>';
    }
    else {
        ReportHTML += '             <div class="col-xs-12 text-center m-t-n"><h3>' + 'إذن صرف نقدى' + '</h3></div>';
    }
    //ReportHTML += '             <div class="col-xs-12 text-ul"><h4>Payment</h4></div>';

    ReportHTML += '             <div style="clear:both;"></div>';

    ReportHTML += '             <div class="col-xs-12">'
    ReportHTML += '                 <table id="tblPaymentHeaderData" class="table table-striped b-t b-light text-sm table-bordered" style="border:solid #000;font-size:115%;">'; // table-hover
    ReportHTML += '                     <thead>';
    ReportHTML += '                         <td>';
    //ReportHTML += '                             <div class="col-xs-6 text-left"><b>Printed On :</b> ' + TodaysDateddMMyyyy + '</div>';
    ReportHTML += '                             <div class="col-xs-6 text-left"><b>Voucher Date: ' + '</b>' + ConvertDateFormat(GetDateWithFormatMDY(pVoucherHeader.VoucherDate)) + '</div>';
    ReportHTML += '                             <div class="col-xs-6 text-left"><b>Name: ' + '</b>' + pVoucherHeader.ChargedPerson + '</div>';
    ReportHTML += '                             <div class="col-xs-6 text-left"><b>Safe: ' + '</b>' + pVoucherHeader.SafeName + '</div>';
    ReportHTML += '                             <div class="col-xs-6 text-left"><b>Total: ' + '</b>' + pVoucherHeader.Total.toFixed(2) + ' ' + pVoucherHeader.CurrencyCode + '</div>';
    //ReportHTML += '                             <div class="col-xs-6 text-left"><b>Total After Tax: ' + '</b>' + pVoucherHeader.TotalAfterTax.toFixed(2) + ' ' + pVoucherHeader.CurrencyCode + '</div>';
    //ReportHTML += '                             <div class="col-xs-12 text-left"><b>Only: ' + '</b>' + toWords_WithFractionNumbers(pVoucherHeader.TotalAfterTax.toFixed(2)) + ' ' + pVoucherHeader.CurrencyCode + '</div>';
    if (pVoucherHeader.Notes != 0)
        ReportHTML += '                             <div class="col-xs-12 text-left"><b>Notes: ' + '</b>' + pVoucherHeader.Notes + '</div>';
    ReportHTML += '                         </td>';
    ReportHTML += '                     </thead>';
    ReportHTML += '                 </table>';
    ReportHTML += '             </div>';

    if (pVoucherOperation.length > 0) {
        ReportHTML += '             <div class="col-xs-12 m-t-n-sm text-ul"><h5><b>Operations  : </b></h5>' + '</div>';
        ReportHTML += '             <div class="row col-xs-12">';
        ReportHTML += '             <div class="col-xs-12">';
        ReportHTML += '                 <table id="tblOperationDetails" class="table table-striped b-t b-light text-sm table-bordered" style="border:solid #000;font-size:115%;">'; // table-hover
        ReportHTML += '                     <thead>';
        ReportHTML += '                         <tr>';
        ReportHTML += '                             <th>Operation Code</th>';
        ReportHTML += '                         </tr>';
        ReportHTML += '                     </thead>';
        ReportHTML += '                     <tbody>';
        debugger;

        ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
        ReportHTML += ' <td>';
        $.each(pVoucherOperation, function (i, item) {

            ReportHTML += item.Code + "    &nbsp;&nbsp;&nbsp     ";


        });
        ReportHTML += ' </td>';
        ReportHTML += '                     </tr>';
        ReportHTML += '                         </tr>';
        ReportHTML += '                     </tbody>';
        ReportHTML += '                 </table>';
        ReportHTML += '             </div>';
        ReportHTML += '             </div>';
    }

    ReportHTML += '             <div class="col-xs-12 m-t-n-sm text-ul"><h5><b>Payment Details : </b></h5>' + '</div>';
    ReportHTML += '             <div class="col-xs-12">';
    ReportHTML += '                 <table id="tblPaymentDetails" class="table table-striped b-t b-light text-sm table-bordered" style="border:solid #000;font-size:115%;">'; // table-hover
    ReportHTML += '                     <thead>';
    ReportHTML += '                         <tr>';
    ReportHTML += '                             <th>Description</th>';
    //ReportHTML += '                             <th>Amount</th>';
    //ReportHTML += '                             <th>Account</th>';
    //ReportHTML += '                             <th>Sub Account</th>';
    ReportHTML += '                             <th class="">Amount</th>';
    ReportHTML += '                         </tr>';
    ReportHTML += '                     </thead>';
    ReportHTML += '                     <tbody>';
    debugger;
    //if ($.each(pVoucherInvItem.length - 1 == i)) {
    //    ItemTotal = item.SaleAmount
    //}
    ItemTotal = 0;
    $.each(pVoucherDetails, function (j, item2) {
        //if ($(pVoucherDetails).length -1 == j) {
        ItemTotal += item2.Value;
        //}
    });
    $.each(pVoucherInvItem, function (i, item) {

        if (i == 0) {
            ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
            //<b>Name: ' + '</b>' + pVoucherHeader.ChargedPerson + '
            ReportHTML += '                         <td> <b> Invoice NO: ' + '</b>' + item.InvoiceNumber + '<b>' + ' Operation No:' + '</b>' + item.OperationNo + '<b>' + ' B\L NO:' + '</b>' + item.BLNumber + '</td>';
            //ReportHTML += '                         <td>' + ItemTotal + '</td>';
            //ReportHTML += '                         <td>' +""+ '</td>';
            //ReportHTML += '                         <td>' + "" + '</td>';
            ReportHTML += '                         <td class="">' + ItemTotal + '</td>';
            ReportHTML += '                     </tr>';
        }
    });
    if (pVoucherHeader.TaxValue != 0) {
        ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
        ReportHTML += '                         <td>' + (pVoucherHeader.TaxName == 0 ? "" : pVoucherHeader.TaxName) + '</td>';
        //ReportHTML += '                         <td > ' + pVoucherHeader.TaxValue + '</td>';
        //ReportHTML += '                         <td>' + "" + '</td>';
        //ReportHTML += '                         <td>' + "" + '</td>';
        ReportHTML += '                         <td class="">' + pVoucherHeader.TaxValue + '</td>';
        ReportHTML += '                     </tr>';
    }
    if (pVoucherHeader.TaxValue2 != 0) {
        ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
        ReportHTML += '                         <td>' + (pVoucherHeader.TaxName2 == 0 ? "" : pVoucherHeader.TaxName2) + '</td>';
        //ReportHTML += '                         <td >' + pVoucherHeader.TaxValue2 + '</td>';
        //ReportHTML += '                         <td>' + "" + '</td>';
        //ReportHTML += '                         <td>' + "" + '</td>';
        ReportHTML += '                         <td class="">' + pVoucherHeader.TaxValue2 + '</td>';
        //ReportHTML += '                         <td></td>';
        //ReportHTML += '                         <td></td>';
        //ReportHTML += '                         <td class=""></td>';
        //ReportHTML += '                         <td class="">' + toWords_WithFractionNumbers(pVoucherHeader.TaxValue2.toFixed(4)) + '</td>';
        ReportHTML += '                     </tr>';
    }
    ReportHTML += '                         <tr>';
    //ReportHTML += '                             <td colspan=5>' + '<b>TOTAL AMOUNT ONLY : ' + toWords_WithFractionNumbers(pVoucherHeader.TotalAfterTax.toFixed(4)) + ' ' + $("#hDefaultCurrencyCode").val() + '</b></td>';
    ReportHTML += '                             <td colspan=2>' + '<b>TOTAL AMOUNT ONLY : ' + toWords_WithFractionNumbers(pVoucherHeader.TotalAfterTax.toFixed(2)) + ' ' + pVoucherHeader.CurrencyCode + '</b></td>';
    ReportHTML += '                         </tr>';
    ReportHTML += '                     </tbody>';
    ReportHTML += '                 </table>';
    ReportHTML += '             </div>';
    //ReportHTML += '                 <div class="col-xs-12"><b>Approved available balance till printing time :</b> ' + pVoucherHeader.AvailableBalance + '</div>';


    ReportHTML += '             </div>';

    ReportHTML += '         </body>';
    ReportHTML += '     <footer class="footer col-xs-12" style="width:100%; position:relative; bottom:0;">';
    //ReportHTML += '                 <div class="col-xs-3  float-right" ><b><div class="col-xs-11" style="height:100px;border:1px solid #000;">Prepared By </br> </br>' + (pVoucherHeader.UserName != 0 ? pVoucherHeader.UserName : '') + '</div></b></div>';
    //ReportHTML += '                 <div class="col-xs-3  float-right" ><b><div class="col-xs-11" style="height:100px;border:1px solid #000;">Reviewed By</div></div></div>';
    //ReportHTML += '                 <div class="col-xs-3  float-right" ><b><div class="col-xs-11" style="height:100px;border:1px solid #000;">Approved By</div></b></div>';
    ReportHTML += '                 <div class="col-xs-12  float-Left" ><b><div class="col-xs-2" style="height:80px;border:1px solid #000;">Stamp<br> </div></b></div>';
    ReportHTML += '                 <div class="col-xs-6  float-right" style="font-size:150%;">' + '  لا يعتد بهذا الإيصال إلا إذا كان مختوما بختم الشركة  ' + '</div>';
    ReportHTML += '    <br>'
    //ReportHTML += '     <div class="col-xs-12 text-left"><b>Printed On :</b> ' + TodaysDateddMMyyyy + '</div>';
    //ReportHTML += '         <div class="row text-right m-r">' + '  لاتعتبر الفاتورة مسددة إلا بايصال رسمى مختوم من الشركة  ' + '</div>';
    //ReportHTML += '         <div class="row text-right m-r">' + '  الاعتراض على اى بند من الفاتورة خلال 15 يوم من تاريخ استلام الفاتورة  ' + '</div>';
    //if ($("#cbIsImport").prop("checked") && $("#cbIsOcean").prop("checked"))
    //    ReportHTML += '                 <div class="row m-l">F/FFI-IS-11-03</div>';
    //ReportHTML += '         <div class="row text-center"><img src="/Content/Images/CompanyFooter.jpg" alt="footer"/></div>';
    ReportHTML += '     </footer>';
    ///////////////////////////////////////////////////////////////////////////////////////////////
    ReportHTML += '             <div class="col-xs-12 text-center break" > <br> </div>';
    //ReportHTML += '     <head><title>Payment</title><link rel="stylesheet" href="/Content/CSS/app.v2.css" type="text/css" /></head>';
    ReportHTML += '         <body style="background-color:white;font-size:115%; " >';
    //ReportHTML += '             <div class="col-xs-12 text-center"><img src="/Content/Images/CompanyHeader.jpg" alt="logo"/></div>';
    //ReportHTML += '             <div class="col-xs-12 text-center m-t-n"><h3>Invoice No. ' + pInvoiceDate.substr(3, 2) + '/' + pInvoiceDate.substr(8, 2) + '/' + $("#tblInvoices #" + pID + " td.InvoiceNumber").text() + '</h3></div>';

    ReportHTML += '             <div class="col-xs-12 text-center m-t-n "><h3>' + TranslateString(pVoucherHeader.VoucherTypeName) + ' No. ' + pVoucherHeader.Code + '</h3></div>';

    if (pVoucherHeader.VoucherType == 10) {
        ReportHTML += '             <div class="col-xs-12 text-center m-t-n"><h3>' + 'إيصال استلام نقدى' + '</h3></div>';
    }
    else {
        ReportHTML += '             <div class="col-xs-12 text-center m-t-n"><h3>' + 'إذن صرف نقدى' + '</h3></div>';
    }
    //ReportHTML += '             <div class="col-xs-12 text-ul"><h4>Payment</h4></div>';

    ReportHTML += '             <div style="clear:both;"></div>';

    ReportHTML += '             <div class="col-xs-12">'
    ReportHTML += '                 <table id="tblPaymentHeaderData" class="table table-striped b-t b-light text-sm table-bordered" style="border:solid #000;font-size:115%;">'; // table-hover
    ReportHTML += '                     <thead>';
    ReportHTML += '                         <td>';
    //ReportHTML += '                             <div class="col-xs-6 text-left"><b>Printed On :</b> ' + TodaysDateddMMyyyy + '</div>';
    ReportHTML += '                             <div class="col-xs-6 text-left"><b>Voucher Date: ' + '</b>' + ConvertDateFormat(GetDateWithFormatMDY(pVoucherHeader.VoucherDate)) + '</div>';
    ReportHTML += '                             <div class="col-xs-6 text-left"><b>Name: ' + '</b>' + pVoucherHeader.ChargedPerson + '</div>';
    ReportHTML += '                             <div class="col-xs-6 text-left"><b>Safe: ' + '</b>' + pVoucherHeader.SafeName + '</div>';
    ReportHTML += '                             <div class="col-xs-6 text-left"><b>Total: ' + '</b>' + pVoucherHeader.Total.toFixed(2) + ' ' + pVoucherHeader.CurrencyCode + '</div>';
    //ReportHTML += '                             <div class="col-xs-6 text-left"><b>Total After Tax: ' + '</b>' + pVoucherHeader.TotalAfterTax.toFixed(2) + ' ' + pVoucherHeader.CurrencyCode + '</div>';
    //ReportHTML += '                             <div class="col-xs-12 text-left"><b>Only: ' + '</b>' + toWords_WithFractionNumbers(pVoucherHeader.TotalAfterTax.toFixed(2)) + ' ' + pVoucherHeader.CurrencyCode + '</div>';
    if (pVoucherHeader.Notes != 0)
        ReportHTML += '                             <div class="col-xs-12 text-left"><b>Notes: ' + '</b>' + pVoucherHeader.Notes + '</div>';
    ReportHTML += '                         </td>';
    ReportHTML += '                     </thead>';
    ReportHTML += '                 </table>';
    ReportHTML += '             </div>';

    if (pVoucherOperation.length > 0) {
        ReportHTML += '             <div class="col-xs-12 m-t-n-sm text-ul"><h5><b>Operations  : </b></h5>' + '</div>';
        ReportHTML += '             <div class="row col-xs-12">';
        ReportHTML += '             <div class="col-xs-12">';
        ReportHTML += '                 <table id="tblOperationDetails" class="table table-striped b-t b-light text-sm table-bordered" style="border:solid #000;font-size:115%;">'; // table-hover
        ReportHTML += '                     <thead>';
        ReportHTML += '                         <tr>';
        ReportHTML += '                             <th>Operation Code</th>';
        ReportHTML += '                         </tr>';
        ReportHTML += '                     </thead>';
        ReportHTML += '                     <tbody>';
        debugger;

        ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
        ReportHTML += ' <td>';
        $.each(pVoucherOperation, function (i, item) {

            ReportHTML += item.Code + "    &nbsp;&nbsp;&nbsp     ";


        });
        ReportHTML += ' </td>';
        ReportHTML += '                     </tr>';
        ReportHTML += '                         </tr>';
        ReportHTML += '                     </tbody>';
        ReportHTML += '                 </table>';
        ReportHTML += '             </div>';
        ReportHTML += '             </div>';
    }

    ReportHTML += '             <div class="col-xs-12 m-t-n-sm text-ul"><h5><b>Payment Details : </b></h5>' + '</div>';
    ReportHTML += '             <div class="col-xs-12">';
    ReportHTML += '                 <table id="tblPaymentDetails" class="table table-striped b-t b-light text-sm table-bordered" style="border:solid #000;font-size:115%;">'; // table-hover
    ReportHTML += '                     <thead>';
    ReportHTML += '                         <tr>';
    ReportHTML += '                             <th>Description</th>';
    ReportHTML += '                             <th>Amount</th>';
    ReportHTML += '                             <th>Account</th>';
    ReportHTML += '                             <th>Sub Account</th>';
    ReportHTML += '                             <th class="">Cost Center</th>';
    ReportHTML += '                         </tr>';
    ReportHTML += '                     </thead>';
    ReportHTML += '                     <tbody>';
    debugger;

    //$.each(pVoucherDetails, function (j, item2) {
    //    //if ($(pVoucherDetails).length -1 == j) {
    //    ItemTotal += item2.Value;
    //    //}
    //});
    ItemTotal = 0;
    $.each(pVoucherInvItem, function (j, item) {

        //if (i == 0) {
        InvoiceNumber = item.InvoiceNumber;
        OperationNo = item.OperationNo;
        BLNumber = item.BLNumber;
        //ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
        ////<b>Name: ' + '</b>' + pVoucherHeader.ChargedPerson + '
        //ReportHTML += '                         <td> <b> Invoice NO: ' + '</b>' + item.InvoiceNumber + '<b>' + ' Operation No:' + '</b>' + item.OperationNo + '<b>' + ' B\L NO:' + '</b>' + item.BLNumber + '</td>';
        //ReportHTML += '                         <td>' + ItemTotal + '</td>';
        //ReportHTML += '                         <td>' + item.Account_Name + '</td>';
        //ReportHTML += '                         <td>' + (item.SubAccount_Name == 0 ? "" : item.SubAccount_Name) + '</td>';
        //ReportHTML += '                         <td class="">' + (item.CostCenterName == 0 ? "" : item.CostCenterName) + '</td>';
        ////ReportHTML += '                         <td class="">' + toWords_WithFractionNumbers(item.Value.toFixed(4)) + '</td>';
        //ReportHTML += '                     </tr>';
        //}
    });
    $.each(pVoucherDetails, function (i, item) {
        if (i == 0) {
            ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
            //<b>Name: ' + '</b>' + pVoucherHeader.ChargedPerson + '
            ReportHTML += '                         <td> <b> Invoice NO: ' + '</b>' + InvoiceNumber + '<b>' + ' Operation No:' + '</b>' + OperationNo + '<b>' + ' B\L NO:' + '</b>' + BLNumber + '</td>';
            ReportHTML += '                         <td>' + pVoucherHeader.TotalAfterTax + '</td>';
            ReportHTML += '                         <td>' + item.Account_Name + '</td>';
            ReportHTML += '                         <td>' + (item.SubAccount_Name == 0 ? "" : item.SubAccount_Name) + '</td>';
            ReportHTML += '                         <td class="">' + (item.CostCenterName == 0 ? "" : item.CostCenterName) + '</td>';
            //ReportHTML += '                         <td class="">' + toWords_WithFractionNumbers(item.Value.toFixed(4)) + '</td>';
            ReportHTML += '                     </tr>';
        }
    });
    ReportHTML += '                         <tr>';
    //ReportHTML += '                             <td colspan=5>' + '<b>TOTAL AMOUNT ONLY : ' + toWords_WithFractionNumbers(pVoucherHeader.TotalAfterTax.toFixed(4)) + ' ' + $("#hDefaultCurrencyCode").val() + '</b></td>';
    ReportHTML += '                             <td colspan=5>' + '<b>TOTAL AMOUNT ONLY : ' + toWords_WithFractionNumbers(pVoucherHeader.TotalAfterTax.toFixed(2)) + ' ' + pVoucherHeader.CurrencyCode + '</b></td>';
    ReportHTML += '                         </tr>';
    ReportHTML += '                     </tbody>';
    ReportHTML += '                 </table>';

    //--------------------------------------------------------------------------------------------------------
    ReportHTML += '                 <table id="tblPaymentDetails" class="table table-striped b-t b-light text-sm table-bordered" style="border:solid #000;font-size:115%;">'; // table-hover
    ReportHTML += '                     <thead>';
    ReportHTML += '                         <tr>';
    ReportHTML += '                             <th>Items Invoice</th>';
    ReportHTML += '                             <th>Amount</th>';
    //ReportHTML += '                             <th>Account</th>';
    //ReportHTML += '                             <th>Sub Account</th>';
    //ReportHTML += '                             <th class="">Cost Center</th>';
    ReportHTML += '                         </tr>';
    ReportHTML += '                     </thead>';
    ReportHTML += '                     <tbody>';
    $.each(pVoucherInvItem, function (i, item) {

        ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
        ReportHTML += '                         <td>' + item.ItemName + '</td>';
        ReportHTML += '                         <td>' + item.SaleAmount + '</td>';
        //ReportHTML += '                         <td>' + item.Account_Name + '</td>';
        //ReportHTML += '                         <td>' + (item.SubAccount_Name == 0 ? "" : item.SubAccount_Name) + '</td>';
        //ReportHTML += '                         <td class="">' + (item.CostCenterName == 0 ? "" : item.CostCenterName) + '</td>';
        //ReportHTML += '                         <td class="">' + toWords_WithFractionNumbers(item.Value.toFixed(4)) + '</td>';
        ReportHTML += '                     </tr>';
    });

    //if (pVoucherInvItem.TaxAmount != 0) {
    //    ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
    //    ReportHTML += '                         <td>' + (pVoucherInvItem.TaxPercentage == 0 ? "" : pVoucherInvItem.TaxPercentage) + '</td>';
    //    ReportHTML += '                         <td>' + pVoucherInvItem.TaxAmount + '</td>';
    //    //ReportHTML += '                         <td></td>';
    //    //ReportHTML += '                         <td></td>';
    //    //ReportHTML += '                         <td class=""></td>';
    //    //ReportHTML += '                         <td class="">' + toWords_WithFractionNumbers(pVoucherHeader.TaxValue.toFixed(4)) + '</td>';
    //    ReportHTML += '                     </tr>';
    //}
    if (pVoucherHeader.TaxValue2 != 0) {
        ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
        ReportHTML += '                         <td>' + (pVoucherHeader.TaxName2 == 0 ? "" : pVoucherHeader.TaxName2) + '</td>';
        ReportHTML += '                         <td>' + pVoucherHeader.TaxValue2 + '</td>';
        //ReportHTML += '                         <td></td>';
        //ReportHTML += '                         <td></td>';
        //ReportHTML += '                         <td class=""></td>';
        //ReportHTML += '                         <td class="">' + toWords_WithFractionNumbers(pVoucherHeader.TaxValue2.toFixed(4)) + '</td>';
        ReportHTML += '                     </tr>';
    }
    ReportHTML += '                     </tbody>';
    ReportHTML += '                 </table>';
    //--------------------------------------------------------------------------------------------------------

    ReportHTML += '             </div>';
    //ReportHTML += '                 <div class="col-xs-12"><b>Approved available balance till printing time :</b> ' + pVoucherHeader.AvailableBalance + '</div>';


    ReportHTML += '             </div>';

    ReportHTML += '         </body>';
    ReportHTML += '     <footer class="footer col-xs-12" style="width:100%; position:relative; bottom:0;">';
    //ReportHTML += '                 <div class="col-xs-3  float-right" ><b><div class="col-xs-11" style="height:100px;border:1px solid #000;">Prepared By </br> </br>' + (pVoucherHeader.UserName != 0 ? pVoucherHeader.UserName : '') + '</div></b></div>';
    //ReportHTML += '                 <div class="col-xs-3  float-right" ><b><div class="col-xs-11" style="height:100px;border:1px solid #000;">Reviewed By</div></div></div>';
    //ReportHTML += '                 <div class="col-xs-3  float-right" ><b><div class="col-xs-11" style="height:100px;border:1px solid #000;">Approved By</div></b></div>';
    //ReportHTML += '                 <div class="col-xs-3  float-right" ><b><div class="col-xs-11" style="height:100px;border:1px solid #000;">Receiver + (pDefaults.UnEditableCompanyName == "ARK" ? " &emsp;&emsp; المستلم  &emsp;&emsp; " : "")<br><br>Name:<br>ID No:<br>Signature:  </div></b></div>';
    //ReportHTML += '     <div class="col-xs-6 text-left"><b>Printed On :</b> ' + TodaysDateddMMyyyy + '</div>';
    ////ReportHTML += '         <div class="row text-right m-r">' + '  لاتعتبر الفاتورة مسددة إلا بايصال رسمى مختوم من الشركة  ' + '</div>';
    ////ReportHTML += '         <div class="row text-right m-r">' + '  الاعتراض على اى بند من الفاتورة خلال 15 يوم من تاريخ استلام الفاتورة  ' + '</div>';
    ////if ($("#cbIsImport").prop("checked") && $("#cbIsOcean").prop("checked"))
    ////    ReportHTML += '                 <div class="row m-l">F/FFI-IS-11-03</div>';
    ////ReportHTML += '         <div class="row text-center"><img src="/Content/Images/CompanyFooter.jpg" alt="footer"/></div>';
    ReportHTML += '     </footer>';
    ///////////////////////////////////////////////////////////////////////////////////////////////
    ReportHTML += '</html>';
    mywindow.document.write(ReportHTML);
    mywindow.document.close();

}

function PrintFairtransNoInv(pData) {
    var TodaysDateddMMyyyy = getTodaysDateInddMMyyyyFormat() + ' ' + getTime();
    var pVoucherHeader = JSON.parse(pData[0]);
    var pVoucherDetails = JSON.parse(pData[1]);
    var pVoucherOperation = JSON.parse(pData[2]);
    var mywindow = window.open('', '_blank');

    var ReportHTML = '';
    //<div style="width:500px;height:100px;border:1px solid #000;">This is a rectangle!</div>
    ReportHTML += '<html>';
    ReportHTML += '     <head><title>Payment</title><link rel="stylesheet" href="/Content/CSS/app.v2.css" type="text/css" /></head>';
    ReportHTML += '         <body style="background-color:white;font-size:115%;">';
    ReportHTML += '             <div class="col-xs-12 text-center"><img src="/Content/Images/CompanyHeader.jpg" alt="logo"/></div>';
    //ReportHTML += '             <div class="col-xs-12 text-center m-t-n"><h3>Invoice No. ' + pInvoiceDate.substr(3, 2) + '/' + pInvoiceDate.substr(8, 2) + '/' + $("#tblInvoices #" + pID + " td.InvoiceNumber").text() + '</h3></div>';

    ReportHTML += '             <div class="col-xs-12 text-center m-t-n"><h3>' + TranslateString(pVoucherHeader.VoucherTypeName) + ' No. ' + pVoucherHeader.Code + '</h3></div>';

    if (pVoucherHeader.VoucherType == 10) {
        ReportHTML += '             <div class="col-xs-12 text-center m-t-n"><h3>' + 'إيصال استلام نقدى' + '</h3></div>';
    }
    else {
        ReportHTML += '             <div class="col-xs-12 text-center m-t-n"><h3>' + 'إذن صرف نقدى' + '</h3></div>';
    }
    //ReportHTML += '             <div class="col-xs-12 text-ul"><h4>Payment</h4></div>';

    ReportHTML += '             <div style="clear:both;"></div>';

    ReportHTML += '             <div class="col-xs-12">'
    ReportHTML += '                 <table id="tblPaymentHeaderData" class="table table-striped b-t b-light text-sm table-bordered" style="border:solid #000;font-size:115%;">'; // table-hover
    ReportHTML += '                     <thead>';
    ReportHTML += '                         <td>';
    //ReportHTML += '                             <div class="col-xs-6 text-left"><b>Printed On :</b> ' + TodaysDateddMMyyyy + '</div>';
    ReportHTML += '                             <div class="col-xs-6 text-left"><b>Voucher Date: ' + '</b>' + ConvertDateFormat(GetDateWithFormatMDY(pVoucherHeader.VoucherDate)) + '</div>';
    ReportHTML += '                             <div class="col-xs-6 text-left"><b>Name: ' + '</b>' + pVoucherHeader.ChargedPerson + '</div>';
    ReportHTML += '                             <div class="col-xs-6 text-left"><b>Safe: ' + '</b>' + pVoucherHeader.SafeName + '</div>';
    ReportHTML += '                             <div class="col-xs-6 text-left"><b>Total: ' + '</b>' + pVoucherHeader.Total.toFixed(2) + ' ' + pVoucherHeader.CurrencyCode + '</div>';
    ReportHTML += '                             <div class="col-xs-6 text-left"><b>Total After Tax: ' + '</b>' + pVoucherHeader.TotalAfterTax.toFixed(2) + ' ' + pVoucherHeader.CurrencyCode + '</div>';
    ReportHTML += '                             <div class="col-xs-12 text-left"><b>Only: ' + '</b>' + toWords_WithFractionNumbers(pVoucherHeader.TotalAfterTax.toFixed(2)) + ' ' + pVoucherHeader.CurrencyCode + '</div>';
    if (pVoucherHeader.Notes != 0)
        ReportHTML += '                             <div class="col-xs-12 text-left"><b>Notes: ' + '</b>' + pVoucherHeader.Notes + '</div>';
    ReportHTML += '                         </td>';
    ReportHTML += '                     </thead>';
    ReportHTML += '                 </table>';
    ReportHTML += '             </div>';

    if (pVoucherOperation.length > 0) {
        ReportHTML += '             <div class="col-xs-12 m-t-n-sm text-ul"><h5><b>Operations  : </b></h5>' + '</div>';
        ReportHTML += '             <div class="row col-xs-12">';
        ReportHTML += '             <div class="col-xs-12">';
        ReportHTML += '                 <table id="tblOperationDetails" class="table table-striped b-t b-light text-sm table-bordered" style="border:solid #000;font-size:115%;">'; // table-hover
        ReportHTML += '                     <thead>';
        ReportHTML += '                         <tr>';
        ReportHTML += '                             <th>Operation Code</th>';
        ReportHTML += '                         </tr>';
        ReportHTML += '                     </thead>';
        ReportHTML += '                     <tbody>';
        debugger;

        ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
        ReportHTML += ' <td>';
        $.each(pVoucherOperation, function (i, item) {

            ReportHTML += item.Code + "    &nbsp;&nbsp;&nbsp     ";


        });
        ReportHTML += ' </td>';
        ReportHTML += '                     </tr>';
        ReportHTML += '                         </tr>';
        ReportHTML += '                     </tbody>';
        ReportHTML += '                 </table>';
        ReportHTML += '             </div>';
        ReportHTML += '             </div>';
    }

    ReportHTML += '             <div class="col-xs-12 m-t-n-sm text-ul"><h5><b>Payment Details : </b></h5>' + '</div>';
    ReportHTML += '             <div class="col-xs-12">';
    ReportHTML += '                 <table id="tblPaymentDetails" class="table table-striped b-t b-light text-sm table-bordered" style="border:solid #000;font-size:115%;">'; // table-hover
    ReportHTML += '                     <thead>';
    ReportHTML += '                         <tr>';
    ReportHTML += '                             <th>Description</th>';
    ReportHTML += '                             <th>Amount</th>';
    ReportHTML += '                             <th>Account</th>';
    ReportHTML += '                             <th>Sub Account</th>';
    ReportHTML += '                             <th class="">Cost Center</th>';
    ReportHTML += '                         </tr>';
    ReportHTML += '                     </thead>';
    ReportHTML += '                     <tbody>';
    debugger;
    $.each(pVoucherDetails, function (i, item) {
        ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
        ReportHTML += '                         <td>' + (item.Description == 0 ? "" : item.Description) + '</td>';
        ReportHTML += '                         <td>' + item.Value + '</td>';
        ReportHTML += '                         <td>' + item.Account_Name + '</td>';
        ReportHTML += '                         <td>' + (item.SubAccount_Name == 0 ? "" : item.SubAccount_Name) + '</td>';
        ReportHTML += '                         <td class="">' + (item.CostCenterName == 0 ? "" : item.CostCenterName) + '</td>';
        //ReportHTML += '                         <td class="">' + toWords_WithFractionNumbers(item.Value.toFixed(4)) + '</td>';
        ReportHTML += '                     </tr>';
    });
    if (pVoucherHeader.TaxValue != 0) {
        ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
        ReportHTML += '                         <td>' + (pVoucherHeader.TaxName == 0 ? "" : pVoucherHeader.TaxName) + '</td>';
        ReportHTML += '                         <td>' + pVoucherHeader.TaxValue + '</td>';
        ReportHTML += '                         <td></td>';
        ReportHTML += '                         <td></td>';
        ReportHTML += '                         <td class=""></td>';
        //ReportHTML += '                         <td class="">' + toWords_WithFractionNumbers(pVoucherHeader.TaxValue.toFixed(4)) + '</td>';
        ReportHTML += '                     </tr>';
    }
    if (pVoucherHeader.TaxValue2 != 0) {
        ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
        ReportHTML += '                         <td>' + (pVoucherHeader.TaxName2 == 0 ? "" : pVoucherHeader.TaxName2) + '</td>';
        ReportHTML += '                         <td>' + pVoucherHeader.TaxValue2 + '</td>';
        ReportHTML += '                         <td></td>';
        ReportHTML += '                         <td></td>';
        ReportHTML += '                         <td class=""></td>';
        //ReportHTML += '                         <td class="">' + toWords_WithFractionNumbers(pVoucherHeader.TaxValue2.toFixed(4)) + '</td>';
        ReportHTML += '                     </tr>';
    }
    ReportHTML += '                         <tr>';
    //ReportHTML += '                             <td colspan=5>' + '<b>TOTAL AMOUNT ONLY : ' + toWords_WithFractionNumbers(pVoucherHeader.TotalAfterTax.toFixed(4)) + ' ' + $("#hDefaultCurrencyCode").val() + '</b></td>';
    ReportHTML += '                             <td colspan=5>' + '<b>TOTAL AMOUNT ONLY : ' + toWords_WithFractionNumbers(pVoucherHeader.TotalAfterTax.toFixed(2)) + ' ' + pVoucherHeader.CurrencyCode + '</b></td>';
    ReportHTML += '                         </tr>';
    ReportHTML += '                     </tbody>';
    ReportHTML += '                 </table>';
    ReportHTML += '             </div>';
    //ReportHTML += '                 <div class="col-xs-12"><b>Approved available balance till printing time :</b> ' + pVoucherHeader.AvailableBalance + '</div>';


    ReportHTML += '             </div>';

    ReportHTML += '         </body>';
    ReportHTML += '     <footer class="footer col-xs-12" style="width:100%; position:relative; bottom:0;">';
    //ReportHTML += '                 <div class="col-xs-3  float-right" ><b><div class="col-xs-11" style="height:100px;border:1px solid #000;">Prepared By </br> </br>' + (pVoucherHeader.UserName != 0 ? pVoucherHeader.UserName : '') + '</div></b></div>';
    //ReportHTML += '                 <div class="col-xs-3  float-right" ><b><div class="col-xs-11" style="height:100px;border:1px solid #000;">Reviewed By</div></div></div>';
    //ReportHTML += '                 <div class="col-xs-3  float-right" ><b><div class="col-xs-11" style="height:100px;border:1px solid #000;">Approved By</div></b></div>';
    ReportHTML += '                 <div class="col-xs-6  float-Left" ><b><div class="col-xs-2" style="height:80px;border:1px solid #000;">Stamp<br> </div></b></div>';
    ReportHTML += '                 <div class="col-xs-6  float-right" style="font-size:150%;">' + '  لا يعتد بهذا الإيصال إلا إذا كان مختوما بختم الشركه  ' + '</div>';
    ReportHTML += '    <br>'
    //ReportHTML += '     <div class="col-xs-12 text-left"><b>Printed On :</b> ' + TodaysDateddMMyyyy + '</div>';
    //ReportHTML += '         <div class="row text-right m-r">' + '  لاتعتبر الفاتورة مسددة إلا بايصال رسمى مختوم من الشركة  ' + '</div>';
    //ReportHTML += '         <div class="row text-right m-r">' + '  الاعتراض على اى بند من الفاتورة خلال 15 يوم من تاريخ استلام الفاتورة  ' + '</div>';
    //if ($("#cbIsImport").prop("checked") && $("#cbIsOcean").prop("checked"))
    //    ReportHTML += '                 <div class="row m-l">F/FFI-IS-11-03</div>';
    //ReportHTML += '         <div class="row text-center"><img src="/Content/Images/CompanyFooter.jpg" alt="footer"/></div>';
    ReportHTML += '     </footer>';
    ReportHTML += '    <br>'
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    ReportHTML += '             <div class="col-xs-12 text-center"> <br> </div>';
    ReportHTML += '         <body style="background-color:white;font-size:115%;">';
    //ReportHTML += '             <div class="col-xs-12 text-center"><img src="/Content/Images/CompanyHeader.jpg" alt="logo"/></div>';
    //ReportHTML += '             <div class="col-xs-12 text-center m-t-n"><h3>Invoice No. ' + pInvoiceDate.substr(3, 2) + '/' + pInvoiceDate.substr(8, 2) + '/' + $("#tblInvoices #" + pID + " td.InvoiceNumber").text() + '</h3></div>';

    ReportHTML += '             <div class="col-xs-12 text-center m-t-n"><h3>' + TranslateString(pVoucherHeader.VoucherTypeName) + ' No. ' + pVoucherHeader.Code + '</h3></div>';

    if (pVoucherHeader.VoucherType == 10) {
        ReportHTML += '             <div class="col-xs-12 text-center m-t-n"><h3>' + 'إيصال استلام نقدى' + '</h3></div>';
    }
    else {
        ReportHTML += '             <div class="col-xs-12 text-center m-t-n"><h3>' + 'إذن صرف نقدى' + '</h3></div>';
    }
    //ReportHTML += '             <div class="col-xs-12 text-ul"><h4>Payment</h4></div>';

    ReportHTML += '             <div style="clear:both;"></div>';

    ReportHTML += '             <div class="col-xs-12">'
    ReportHTML += '                 <table id="tblPaymentHeaderData" class="table table-striped b-t b-light text-sm table-bordered" style="border:solid #000;font-size:115%;">'; // table-hover
    ReportHTML += '                     <thead>';
    ReportHTML += '                         <td>';
    //ReportHTML += '                             <div class="col-xs-6 text-left"><b>Printed On :</b> ' + TodaysDateddMMyyyy + '</div>';
    ReportHTML += '                             <div class="col-xs-6 text-left"><b>Voucher Date: ' + '</b>' + ConvertDateFormat(GetDateWithFormatMDY(pVoucherHeader.VoucherDate)) + '</div>';
    ReportHTML += '                             <div class="col-xs-6 text-left"><b>Name: ' + '</b>' + pVoucherHeader.ChargedPerson + '</div>';
    ReportHTML += '                             <div class="col-xs-6 text-left"><b>Safe: ' + '</b>' + pVoucherHeader.SafeName + '</div>';
    ReportHTML += '                             <div class="col-xs-6 text-left"><b>Total: ' + '</b>' + pVoucherHeader.Total.toFixed(2) + ' ' + pVoucherHeader.CurrencyCode + '</div>';
    ReportHTML += '                             <div class="col-xs-6 text-left"><b>Total After Tax: ' + '</b>' + pVoucherHeader.TotalAfterTax.toFixed(2) + ' ' + pVoucherHeader.CurrencyCode + '</div>';
    ReportHTML += '                             <div class="col-xs-12 text-left"><b>Only: ' + '</b>' + toWords_WithFractionNumbers(pVoucherHeader.TotalAfterTax.toFixed(2)) + ' ' + pVoucherHeader.CurrencyCode + '</div>';
    if (pVoucherHeader.Notes != 0)
        ReportHTML += '                             <div class="col-xs-12 text-left"><b>Notes: ' + '</b>' + pVoucherHeader.Notes + '</div>';
    ReportHTML += '                         </td>';
    ReportHTML += '                     </thead>';
    ReportHTML += '                 </table>';
    ReportHTML += '             </div>';

    if (pVoucherOperation.length > 0) {
        ReportHTML += '             <div class="col-xs-12 m-t-n-sm text-ul"><h5><b>Operations  : </b></h5>' + '</div>';
        ReportHTML += '             <div class="row col-xs-12">';
        ReportHTML += '             <div class="col-xs-12">';
        ReportHTML += '                 <table id="tblOperationDetails" class="table table-striped b-t b-light text-sm table-bordered" style="border:solid #000;font-size:115%;">'; // table-hover
        ReportHTML += '                     <thead>';
        ReportHTML += '                         <tr>';
        ReportHTML += '                             <th>Operation Code</th>';
        ReportHTML += '                         </tr>';
        ReportHTML += '                     </thead>';
        ReportHTML += '                     <tbody>';
        debugger;

        ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
        ReportHTML += ' <td>';
        $.each(pVoucherOperation, function (i, item) {

            ReportHTML += item.Code + "    &nbsp;&nbsp;&nbsp     ";


        });
        ReportHTML += ' </td>';
        ReportHTML += '                     </tr>';
        ReportHTML += '                         </tr>';
        ReportHTML += '                     </tbody>';
        ReportHTML += '                 </table>';
        ReportHTML += '             </div>';
        ReportHTML += '             </div>';
    }

    ReportHTML += '             <div class="col-xs-12 m-t-n-sm text-ul"><h5><b>Payment Details : </b></h5>' + '</div>';
    ReportHTML += '             <div class="col-xs-12">';
    ReportHTML += '                 <table id="tblPaymentDetails" class="table table-striped b-t b-light text-sm table-bordered" style="border:solid #000;font-size:115%;">'; // table-hover
    ReportHTML += '                     <thead>';
    ReportHTML += '                         <tr>';
    ReportHTML += '                             <th>Description</th>';
    ReportHTML += '                             <th>Amount</th>';
    ReportHTML += '                             <th>Account</th>';
    ReportHTML += '                             <th>Sub Account</th>';
    ReportHTML += '                             <th class="">Cost Center</th>';
    ReportHTML += '                         </tr>';
    ReportHTML += '                     </thead>';
    ReportHTML += '                     <tbody>';
    debugger;
    $.each(pVoucherDetails, function (i, item) {
        ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
        ReportHTML += '                         <td>' + (item.Description == 0 ? "" : item.Description) + '</td>';
        ReportHTML += '                         <td>' + item.Value + '</td>';
        ReportHTML += '                         <td>' + item.Account_Name + '</td>';
        ReportHTML += '                         <td>' + (item.SubAccount_Name == 0 ? "" : item.SubAccount_Name) + '</td>';
        ReportHTML += '                         <td class="">' + (item.CostCenterName == 0 ? "" : item.CostCenterName) + '</td>';
        //ReportHTML += '                         <td class="">' + toWords_WithFractionNumbers(item.Value.toFixed(4)) + '</td>';
        ReportHTML += '                     </tr>';
    });
    if (pVoucherHeader.TaxValue != 0) {
        ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
        ReportHTML += '                         <td>' + (pVoucherHeader.TaxName == 0 ? "" : pVoucherHeader.TaxName) + '</td>';
        ReportHTML += '                         <td>' + pVoucherHeader.TaxValue + '</td>';
        ReportHTML += '                         <td></td>';
        ReportHTML += '                         <td></td>';
        ReportHTML += '                         <td class=""></td>';
        //ReportHTML += '                         <td class="">' + toWords_WithFractionNumbers(pVoucherHeader.TaxValue.toFixed(4)) + '</td>';
        ReportHTML += '                     </tr>';
    }
    if (pVoucherHeader.TaxValue2 != 0) {
        ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
        ReportHTML += '                         <td>' + (pVoucherHeader.TaxName2 == 0 ? "" : pVoucherHeader.TaxName2) + '</td>';
        ReportHTML += '                         <td>' + pVoucherHeader.TaxValue2 + '</td>';
        ReportHTML += '                         <td></td>';
        ReportHTML += '                         <td></td>';
        ReportHTML += '                         <td class=""></td>';
        //ReportHTML += '                         <td class="">' + toWords_WithFractionNumbers(pVoucherHeader.TaxValue2.toFixed(4)) + '</td>';
        ReportHTML += '                     </tr>';
    }
    ReportHTML += '                         <tr>';
    //ReportHTML += '                             <td colspan=5>' + '<b>TOTAL AMOUNT ONLY : ' + toWords_WithFractionNumbers(pVoucherHeader.TotalAfterTax.toFixed(4)) + ' ' + $("#hDefaultCurrencyCode").val() + '</b></td>';
    ReportHTML += '                             <td colspan=5>' + '<b>TOTAL AMOUNT ONLY : ' + toWords_WithFractionNumbers(pVoucherHeader.TotalAfterTax.toFixed(2)) + ' ' + pVoucherHeader.CurrencyCode + '</b></td>';
    ReportHTML += '                         </tr>';
    ReportHTML += '                     </tbody>';
    ReportHTML += '                 </table>';
    ReportHTML += '             </div>';
    //ReportHTML += '                 <div class="col-xs-12"><b>Approved available balance till printing time :</b> ' + pVoucherHeader.AvailableBalance + '</div>';


    ReportHTML += '             </div>';

    ReportHTML += '         </body>';
    ReportHTML += '     <footer class="footer col-xs-12" style="width:100%; position:relative; bottom:0;">';
    //ReportHTML += '                 <div class="col-xs-3  float-right" ><b><div class="col-xs-11" style="height:100px;border:1px solid #000;">Prepared By </br> </br>' + (pVoucherHeader.UserName != 0 ? pVoucherHeader.UserName : '') + '</div></b></div>';
    //ReportHTML += '                 <div class="col-xs-3  float-right" ><b><div class="col-xs-11" style="height:100px;border:1px solid #000;">Reviewed By</div></div></div>';
    //ReportHTML += '                 <div class="col-xs-3  float-right" ><b><div class="col-xs-11" style="height:100px;border:1px solid #000;">Approved By</div></b></div>';
    //ReportHTML += '                 <div class="col-xs-12  float-Left" ><b><div class="col-xs-2" style="height:80px;border:1px solid #000;">Stamp<br> </div></b></div>';
    //ReportHTML += '    <br>'
    //ReportHTML += '     <div class="col-xs-12 text-left"><b>Printed On :</b> ' + TodaysDateddMMyyyy + '</div>';
    //ReportHTML += '         <div class="row text-right m-r">' + '  لاتعتبر الفاتورة مسددة إلا بايصال رسمى مختوم من الشركة  ' + '</div>';
    //ReportHTML += '         <div class="row text-right m-r">' + '  الاعتراض على اى بند من الفاتورة خلال 15 يوم من تاريخ استلام الفاتورة  ' + '</div>';
    //if ($("#cbIsImport").prop("checked") && $("#cbIsOcean").prop("checked"))
    //    ReportHTML += '                 <div class="row m-l">F/FFI-IS-11-03</div>';
    //ReportHTML += '         <div class="row text-center"><img src="/Content/Images/CompanyFooter.jpg" alt="footer"/></div>';
    ReportHTML += '     </footer>';
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    ReportHTML += '             <div class="col-xs-12 text-center break" > <br> </div>';
    ReportHTML += '         <body style="background-color:white;font-size:115%;">';
    //ReportHTML += '             <div class="col-xs-12 text-center"><img src="/Content/Images/CompanyHeader.jpg" alt="logo"/></div>';
    //ReportHTML += '             <div class="col-xs-12 text-center m-t-n"><h3>Invoice No. ' + pInvoiceDate.substr(3, 2) + '/' + pInvoiceDate.substr(8, 2) + '/' + $("#tblInvoices #" + pID + " td.InvoiceNumber").text() + '</h3></div>';

    ReportHTML += '             <div class="col-xs-12 text-center m-t-n"><h3>' + TranslateString(pVoucherHeader.VoucherTypeName) + ' No. ' + pVoucherHeader.Code + '</h3></div>';

    if (pVoucherHeader.VoucherType == 10) {
        ReportHTML += '             <div class="col-xs-12 text-center m-t-n"><h3>' + 'إيصال استلام نقدى' + '</h3></div>';
    }
    else {
        ReportHTML += '             <div class="col-xs-12 text-center m-t-n"><h3>' + 'إذن صرف نقدى' + '</h3></div>';
    }
    //ReportHTML += '             <div class="col-xs-12 text-ul"><h4>Payment</h4></div>';

    ReportHTML += '             <div style="clear:both;"></div>';

    ReportHTML += '             <div class="col-xs-12">'
    ReportHTML += '                 <table id="tblPaymentHeaderData" class="table table-striped b-t b-light text-sm table-bordered" style="border:solid #000;font-size:115%;">'; // table-hover
    ReportHTML += '                     <thead>';
    ReportHTML += '                         <td>';
    //ReportHTML += '                             <div class="col-xs-6 text-left"><b>Printed On :</b> ' + TodaysDateddMMyyyy + '</div>';
    ReportHTML += '                             <div class="col-xs-6 text-left"><b>Voucher Date: ' + '</b>' + ConvertDateFormat(GetDateWithFormatMDY(pVoucherHeader.VoucherDate)) + '</div>';
    ReportHTML += '                             <div class="col-xs-6 text-left"><b>Name: ' + '</b>' + pVoucherHeader.ChargedPerson + '</div>';
    ReportHTML += '                             <div class="col-xs-6 text-left"><b>Safe: ' + '</b>' + pVoucherHeader.SafeName + '</div>';
    ReportHTML += '                             <div class="col-xs-6 text-left"><b>Total: ' + '</b>' + pVoucherHeader.Total.toFixed(2) + ' ' + pVoucherHeader.CurrencyCode + '</div>';
    ReportHTML += '                             <div class="col-xs-6 text-left"><b>Total After Tax: ' + '</b>' + pVoucherHeader.TotalAfterTax.toFixed(2) + ' ' + pVoucherHeader.CurrencyCode + '</div>';
    ReportHTML += '                             <div class="col-xs-12 text-left"><b>Only: ' + '</b>' + toWords_WithFractionNumbers(pVoucherHeader.TotalAfterTax.toFixed(2)) + ' ' + pVoucherHeader.CurrencyCode + '</div>';
    if (pVoucherHeader.Notes != 0)
        ReportHTML += '                             <div class="col-xs-12 text-left"><b>Notes: ' + '</b>' + pVoucherHeader.Notes + '</div>';
    ReportHTML += '                         </td>';
    ReportHTML += '                     </thead>';
    ReportHTML += '                 </table>';
    ReportHTML += '             </div>';

    if (pVoucherOperation.length > 0) {
        ReportHTML += '             <div class="col-xs-12 m-t-n-sm text-ul"><h5><b>Operations  : </b></h5>' + '</div>';
        ReportHTML += '             <div class="row col-xs-12">';
        ReportHTML += '             <div class="col-xs-12">';
        ReportHTML += '                 <table id="tblOperationDetails" class="table table-striped b-t b-light text-sm table-bordered" style="border:solid #000;font-size:115%;">'; // table-hover
        ReportHTML += '                     <thead>';
        ReportHTML += '                         <tr>';
        ReportHTML += '                             <th>Operation Code</th>';
        ReportHTML += '                         </tr>';
        ReportHTML += '                     </thead>';
        ReportHTML += '                     <tbody>';
        debugger;

        ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
        ReportHTML += ' <td>';
        $.each(pVoucherOperation, function (i, item) {

            ReportHTML += item.Code + "    &nbsp;&nbsp;&nbsp     ";


        });
        ReportHTML += ' </td>';
        ReportHTML += '                     </tr>';
        ReportHTML += '                         </tr>';
        ReportHTML += '                     </tbody>';
        ReportHTML += '                 </table>';
        ReportHTML += '             </div>';
        ReportHTML += '             </div>';
    }

    ReportHTML += '             <div class="col-xs-12 m-t-n-sm text-ul"><h5><b>Payment Details : </b></h5>' + '</div>';
    ReportHTML += '             <div class="col-xs-12">';
    ReportHTML += '                 <table id="tblPaymentDetails" class="table table-striped b-t b-light text-sm table-bordered" style="border:solid #000;font-size:115%;">'; // table-hover
    ReportHTML += '                     <thead>';
    ReportHTML += '                         <tr>';
    ReportHTML += '                             <th>Description</th>';
    ReportHTML += '                             <th>Amount</th>';
    ReportHTML += '                             <th>Account</th>';
    ReportHTML += '                             <th>Sub Account</th>';
    ReportHTML += '                             <th class="">Cost Center</th>';
    ReportHTML += '                         </tr>';
    ReportHTML += '                     </thead>';
    ReportHTML += '                     <tbody>';
    debugger;
    $.each(pVoucherDetails, function (i, item) {
        ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
        ReportHTML += '                         <td>' + (item.Description == 0 ? "" : item.Description) + '</td>';
        ReportHTML += '                         <td>' + item.Value + '</td>';
        ReportHTML += '                         <td>' + item.Account_Name + '</td>';
        ReportHTML += '                         <td>' + (item.SubAccount_Name == 0 ? "" : item.SubAccount_Name) + '</td>';
        ReportHTML += '                         <td class="">' + (item.CostCenterName == 0 ? "" : item.CostCenterName) + '</td>';
        //ReportHTML += '                         <td class="">' + toWords_WithFractionNumbers(item.Value.toFixed(4)) + '</td>';
        ReportHTML += '                     </tr>';
    });
    if (pVoucherHeader.TaxValue != 0) {
        ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
        ReportHTML += '                         <td>' + (pVoucherHeader.TaxName == 0 ? "" : pVoucherHeader.TaxName) + '</td>';
        ReportHTML += '                         <td>' + pVoucherHeader.TaxValue + '</td>';
        ReportHTML += '                         <td></td>';
        ReportHTML += '                         <td></td>';
        ReportHTML += '                         <td class=""></td>';
        //ReportHTML += '                         <td class="">' + toWords_WithFractionNumbers(pVoucherHeader.TaxValue.toFixed(4)) + '</td>';
        ReportHTML += '                     </tr>';
    }
    if (pVoucherHeader.TaxValue2 != 0) {
        ReportHTML += '                     <tr class="input-md" style="font-size:115%;">';
        ReportHTML += '                         <td>' + (pVoucherHeader.TaxName2 == 0 ? "" : pVoucherHeader.TaxName2) + '</td>';
        ReportHTML += '                         <td>' + pVoucherHeader.TaxValue2 + '</td>';
        ReportHTML += '                         <td></td>';
        ReportHTML += '                         <td></td>';
        ReportHTML += '                         <td class=""></td>';
        //ReportHTML += '                         <td class="">' + toWords_WithFractionNumbers(pVoucherHeader.TaxValue2.toFixed(4)) + '</td>';
        ReportHTML += '                     </tr>';
    }
    ReportHTML += '                         <tr>';
    //ReportHTML += '                             <td colspan=5>' + '<b>TOTAL AMOUNT ONLY : ' + toWords_WithFractionNumbers(pVoucherHeader.TotalAfterTax.toFixed(4)) + ' ' + $("#hDefaultCurrencyCode").val() + '</b></td>';
    ReportHTML += '                             <td colspan=5>' + '<b>TOTAL AMOUNT ONLY : ' + toWords_WithFractionNumbers(pVoucherHeader.TotalAfterTax.toFixed(2)) + ' ' + pVoucherHeader.CurrencyCode + '</b></td>';
    ReportHTML += '                         </tr>';
    ReportHTML += '                     </tbody>';
    ReportHTML += '                 </table>';
    ReportHTML += '             </div>';
    //ReportHTML += '                 <div class="col-xs-12"><b>Approved available balance till printing time :</b> ' + pVoucherHeader.AvailableBalance + '</div>';


    ReportHTML += '             </div>';

    ReportHTML += '         </body>';
    ReportHTML += '     <footer class="footer col-xs-12" style="width:100%; position:relative; bottom:0;">';
    //ReportHTML += '                 <div class="col-xs-3  float-right" ><b><div class="col-xs-11" style="height:100px;border:1px solid #000;">Prepared By </br> </br>' + (pVoucherHeader.UserName != 0 ? pVoucherHeader.UserName : '') + '</div></b></div>';
    //ReportHTML += '                 <div class="col-xs-3  float-right" ><b><div class="col-xs-11" style="height:100px;border:1px solid #000;">Reviewed By</div></div></div>';
    //ReportHTML += '                 <div class="col-xs-3  float-right" ><b><div class="col-xs-11" style="height:100px;border:1px solid #000;">Approved By</div></b></div>';
    //ReportHTML += '                 <div class="col-xs-12  float-Left" ><b><div class="col-xs-2" style="height:80px;border:1px solid #000;">Stamp<br> </div></b></div>';
    //ReportHTML += '    <br>'
    //ReportHTML += '     <div class="col-xs-12 text-left"><b>Printed On :</b> ' + TodaysDateddMMyyyy + '</div>';
    //ReportHTML += '         <div class="row text-right m-r">' + '  لاتعتبر الفاتورة مسددة إلا بايصال رسمى مختوم من الشركة  ' + '</div>';
    //ReportHTML += '         <div class="row text-right m-r">' + '  الاعتراض على اى بند من الفاتورة خلال 15 يوم من تاريخ استلام الفاتورة  ' + '</div>';
    //if ($("#cbIsImport").prop("checked") && $("#cbIsOcean").prop("checked"))
    //    ReportHTML += '                 <div class="row m-l">F/FFI-IS-11-03</div>';
    //ReportHTML += '         <div class="row text-center"><img src="/Content/Images/CompanyFooter.jpg" alt="footer"/></div>';
    ReportHTML += '     </footer>';
    ReportHTML += '</html>';
    mywindow.document.write(ReportHTML);
    mywindow.document.close();

}

function JournalVoucher_Print() {
    debugger;
    var pID = $("#btn-PrintJV").attr("JVID");
    if (pID == "" || pID == "0")
        swal("Sorry", "Please, post before printing.");
    else {
        var FormattedTodaysDate = getTodaysDateInddMMyyyyFormat() + ' ' + getTime();


        var pParametersWithValues = {
            pJournalVoucherIDForPrinting: pID
        };
        FadePageCover(true);
        //CallGETFunctionWithParameters(pFunctionName, pParametersWithValues, callback, callback1)
        CallGETFunctionWithParameters("/api/JournalVouchers/GetJournalVoucherDataForPrinting", pParametersWithValues
            , function (pData) {
                if (pData[0]) {


                    var pJVHeader = JSON.parse(pData[1]); // its 1 row
                    var pJVItems = JSON.parse(pData[2]);



                    var ReportHTML = '';

                    if ($("[id$='hf_ChangeLanguage']").val() == "en") {
                        ReportHTML += '<html>';
                    }
                    else {
                        ReportHTML += '<html dir="rtl">';
                    }
                    ReportHTML += '     <head><title>' +  TranslateString("PrintJV") + '</title><link rel="stylesheet" href="/Content/CSS/app.v2.css" type="text/css" /><link rel="stylesheet" href="/Content/CSS/app.v2.css" type="text/css" /></head>';
                    //if ($("#cbPrintLogo").prop("checked"))
                    //    ReportHTML += '     <div class="col-xs-12 text-center"><img src="/Content/Images/CompanyHeader.jpg" alt="header"/></div>';
                    //else
                    ReportHTML += '     <div class="row text-center"><br><br><br><br><br><br><br></div>';

                    ReportHTML += '     <div class="col-xs-12 text-center text-ul m-t-n"><h3> ' +  TranslateString("JournalVouchers")+'</h3></div> '; //addClass "text-ul" to underline
                    ReportHTML += '     <div class="col-xs-12 text-center text-ul m-t-n"><h3>' + pJVHeader.JVNo + '</h3></div> '; //addClass "text-ul" to underline
                    ReportHTML += '     </ br>';

                    ReportHTML += '     <div class="col-xs-6 m-l-n ">' + '<b><span class="float-left">' + TranslateString("JvNo") + '       : ' + pJVHeader.JVNo + '</span></b></div>';
                    ReportHTML += '     <div class="col-xs-6 m-l-n ">' + '<b><span class="float-left">' + TranslateString("UserName") +  '  : ' + pJVHeader.UserName + '</span></b></div>';
                    ReportHTML += '     <div class="col-xs-6 m-l-n ">' + '<b><span class="float-left">' + TranslateString("JVDate") +'      : ' + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(pJVHeader.JVDate)) < 1 ? "" : ConvertDateFormat(GetDateWithFormatMDY(pJVHeader.JVDate))) + '</span></b></div>';
                    //ReportHTML += '     <div class="col-xs-6 m-l-n text-left">' + '<b>' + 'Date : ' + FormattedTodaysDate + '</b></div>'; //addClass "text-ul" to underline
                    ReportHTML += '     <div class="col-xs-6 m-l-n ">' + '<b><span class="float-left">' + TranslateString("JournalType")+' : ' + pJVHeader.Journal_Name + '</span></b></div>';
                    ReportHTML += '     <div class="col-xs-6 m-l-n ">' + '<b><span class="float-left">' + TranslateString("JVType")+'      : ' + pJVHeader.JVType_Name + '</span></b></div>';
                    ReportHTML += '     <div class="col-xs-6 m-l-n ">' + '<b><span class="float-left">' + TranslateString("ReceiptNo")+'   : ' + pJVHeader.ReceiptNo + '</span></b></div>';
                    ReportHTML += '     <div class="col-xs-12 m-l-n ">' + '<b><span class="float-left">'+ TranslateString("Notes")+'       : ' + (pJVHeader.RemarksHeader == 0 ? "" : pJVHeader.RemarksHeader) + '</span></b></div>';

                    //ReportHTML += '     <body style="background-color:white;">';
                    ReportHTML += '     <br>';
                    ReportHTML += '     <body>';

                    ReportHTML += '         <table id="tblPrintJVItems" class="table table-striped b-t b-light text-sm table-bordered m-t-lg">';
                    ReportHTML += '             <thead>';
                    ReportHTML += '                 <tr>';
                    ReportHTML += '                     <th class="text-center">'+TranslateString("Account")+'</th>';
                    ReportHTML += '                     <th class="text-center">'+TranslateString("SubAccount")+'</th>';
                    ReportHTML += '                     <th class="text-center">'+TranslateString("CostCenter")+'</th>';
                    ReportHTML += '                     <th class="text-center">'+TranslateString("Debit")+'</th>';
                    ReportHTML += '                     <th class="text-center">'+TranslateString("Credit")+'</th>';
                    ReportHTML += '                     <th class="text-center">'+TranslateString("Cur")+'</th>';
                    ReportHTML += '                     <th class="text-center">'+TranslateString("Ex.Rate")+'</th>';
                    ReportHTML += '                     <th class="text-center">'+TranslateString("LocalDebit")+'</th>';
                    ReportHTML += '                     <th class="text-center">'+TranslateString("LocalCredit")+'</th>';
                    ReportHTML += '                     <th class="text-center">' + TranslateString("Description") + '</th>';
                    ReportHTML += '                 </tr>';
                    ReportHTML += '             </thead>';
                    ReportHTML += '             <tbody>';
                    var Counter = 0;
                    $.each(pJVItems, function (i, item) {
                        debugger;
                        ReportHTML += '                 <tr style="font-size:95%;">';
                        ReportHTML += '                     <td class="text-center">' + (item.AccountName == 0 ? '' : item.AccountName) + '</td>';
                        ReportHTML += '                     <td class="text-center">' + (item.subAccountName == 0 ? '' : item.subAccountName) + '</td>';
                        ReportHTML += '                     <td class="text-center">' + (item.CostCenter == 0 ? "" : item.CostCenter) + '</td>';
                        ReportHTML += '                     <td class="text-center">' + (item.Debit == 0 ? "" : item.Debit.toFixed(2)) + '</td>';
                        ReportHTML += '                     <td class="text-center">' + (item.Credit == 0 ? "" : item.Credit.toFixed(2)) + '</td>';
                        ReportHTML += '                     <td class="text-center">' + item.Code + '</td>';
                        ReportHTML += '                     <td class="text-center">' + item.ExchangeRate + '</td>';
                        ReportHTML += '                     <td class="text-center">' + (item.LocalDebit == 0 ? "" : item.LocalDebit.toFixed(2)) + '</td>';
                        ReportHTML += '                     <td class="text-center">' + (item.LocalCredit == 0 ? "" : item.LocalCredit.toFixed(2)) + '</td>';
                        ReportHTML += '                     <td class="text-center">' + (item.Description == 0 ? '' : item.Description.replace(/\n/g, "<br />")) + '</td>';

                        ReportHTML += '                 </tr>';
                    });
                    ReportHTML += '             </tbody>';
                    ReportHTML += '         </table>';
                    ReportHTML += '     </body>';


                    ReportHTML += '<div class="col-xs-6  text-right">' + '<b> ' + TranslateString("Total")+' : </b>' + pJVHeader.TotalDebit + ' ' + $("#hDefaultCurrencyCode").val() + '</div>';
                    ReportHTML += '<div class="col-xs-6 text-left" style=" padding-bottom: 10px;">' + toWords(pJVHeader.TotalDebit) + '</div>';
                    //if ($("#cbPrintLogo").prop("checked")) {
                    //    ReportHTML += '     <footer class="footer col-xs-12" style="width:100%; position:absolute; bottom:0;">';
                    //    ReportHTML += '         <div class="row text-center"><img src="/Content/Images/CompanyFooter.jpg" alt="footer"/></div>';
                    //    ReportHTML += '     </footer>';
                    //}
                    ReportHTML += '     <footer class="footer col-xs-12" style="width:100%; position:relative; bottom:0;  padding-top: 10px;">';
                    ReportHTML += '                 <div class="col-xs-4  float-right" ><b><div class="col-xs-11" style="height:100px;border:1px solid #000;">' + TranslateString("PreparedBy") + '</div></b></div>';
                    ReportHTML += '                 <div class="col-xs-4  float-right" ><b><div class="col-xs-11" style="height:100px;border:1px solid #000;">' + TranslateString("ReviewedBy") + '</div></div></div>';
                    ReportHTML += '                 <div class="col-xs-4  float-right" ><b><div class="col-xs-11" style="height:100px;border:1px solid #000;">' + TranslateString("ApprovedBy") + '</div></b></div>';
                    ReportHTML += '     <div class="col-xs-12 "><b>' + TranslateString("PrintedOn")+' :</b> ' + FormattedTodaysDate + '</div>';
                    ReportHTML += '     </footer>';
                    ReportHTML += '</html>';

                    //if ($("#cbPrintLogo").prop("checked")) {
                    //    ReportHTML += '     <footer class="footer col-xs-12" style="width:100%; position:absolute; bottom:0;">';
                    //    ReportHTML += '         <div class="row text-center"><img src="/Content/Images/CompanyFooter.jpg" alt="footer"/></div>';
                    //    ReportHTML += '     </footer>';
                    //}
                    ReportHTML += '</html>';



                    var mywindow = window.open('', '_blank');
                    mywindow.document.write(ReportHTML);
                    mywindow.document.close();
                }
                else {
                    if ($("[id$='hf_ChangeLanguage']").val() == "en") {
                        swal("Sorry", "Connection failed. Please try again.");
                    }
                    else {
                        swal("معذرة", "فشل الإتصال، حاول مرة أخري.");
                    }
                }
                FadePageCover(false);
            }
            , null);
    }
}

function Voucher_IsValid(pContainerName) {
    var submit = true;
    var LinkUserAndSafes = $('#hReadySlOptions option[value="55"]').attr("OptionValue");//LinkUserAndSafes
    if (LinkUserAndSafes == "true") {
        $(".classSafe").attr("data-required", "true");
    }
    else {
        $(".classSafe").attr("data-required", "false");
    }




    $.each($('#' + pContainerName + " select[data-required=true]"), function (i, item) {
        $(item).removeClass('validation-error');
        if ($(item).val() == '' || $(item).val() == '0' || $(item).val() == null || $(item).val() == undefined) {
            $(item).addClass('validation-error'); submit = false;
        }
    });
    if (submit) {
        $('input[type="text"].validation-error').removeClass("validation-error");
        $('select.validation-error').removeClass("validation-error");
        $('.div-error').slideUp();
    }
    return submit;
}
var count = 0;
/****************************************Details*********************************************/
function Details_BindTableRows(pTableRows) {
    count = 1;
    debugger;

    //tr += "     <td class='IsInsert hide'> <input type='checkbox' id='IsInsert" + maxDetailsIDInTable + "' checked='checked' /></td>";
    //tr += "     <td class='DetailsID' style='width:2%;'><input " + (1 == 2 ? " disabled='disabled' " : " name='Delete' ") + " type='checkbox' value='" + maxDetailsIDInTable + "' /></td>";
    //tr += "     <td class='Value' style='width:9%;'><input tag='" + (item.Value) + "'   type='text' style='width:100%;font-size:90%;'  id='txtValue" + maxDetailsIDInTable + "' class='form-control inputValue' data-type='number'  onkeypress='CheckValueIsDecimal(id);' onfocus='CheckValueIsDecimal(id);' onblur='CheckDecimalFormat(id);Voucher_CalculateTotal();' onchange='Details_SetIsRowChanged(id);' data-required='false' value='' /> </td>";
    //tr += "     <td class='Description' style='width:20%;'><input type='text' style='width:100%;font-size:90%;text-transform:uppercase;' id='txtDescription" + maxDetailsIDInTable + "' class='form-control inputDescription'  onkeypress='DisableEnterKey(id);' onfocus='DisableEnterKey(id);' onchange='Details_SetIsRowChanged(id);' data-required='false' value='" + ($("#txtDescription" + (maxDetailsIDInTable - 1)).val() == undefined ? "" : $("#txtDescription" + (maxDetailsIDInTable - 1)).val()) + "' /> </td>";
    //tr += "     <td class='AccountID' style='width:20%;' val=''><select id='slAccount" + maxDetailsIDInTable + "' style='width:100%;' class='form-control selectAccountID' onfocus='DisableBackspaceKey(id);' onkeydown='DisableBackspaceKey(id);' onkeypress='DisableBackspaceKey(id);' onchange='Details_SetIsRowChanged(id); Details_AccountChanged(" + maxDetailsIDInTable + ");' data-required='true'>" + "<option value=0></option>" + "</select></td>";
    //tr += "     <td class='SubAccountID' style='width:20%;' val=''><select id='slSubAccount" + maxDetailsIDInTable + "' style='width:100%;' class='form-control selectSubAccountID' onfocus='DisableBackspaceKey(id);' onkeydown='DisableBackspaceKey(id);' onkeypress='DisableBackspaceKey(id);' onchange='Details_SetIsRowChanged(id);' data-required='false'>" + "<option value=0></option>" + "</select></td>";
    //tr += "     <td class='CostCenterID' style='width:15%;' val=''><select id='slCostCenter" + maxDetailsIDInTable + "' style='width:100%;' class='form-control selectcostcenterID' onfocus='DisableBackspaceKey(id);' onkeydown='DisableBackspaceKey(id);' onkeypress='DisableBackspaceKey(id);' onchange='Details_SetIsRowChanged(id);' data-required='false'>" + "<option value=0></option>" + "</select></td>";
    //tr += "      <td class='Documented' style='width:1%;'><input id='cbDocumented" + maxDetailsIDInTable + "' name='Documented'  class='cbIsDocumented' type='checkbox' value='' ></td>";
    //tr += "     <td class='SelectedIDsToUpdate hide'> <input name='SelectedIDsToUpdate'  id='SelectedIDsToUpdate" + maxDetailsIDInTable + "' type='checkbox' value='" + maxDetailsIDInTable + "' /></td>";
    // + "<td class='AccountID ' val='" + (item.AccountID) + "'>" + "<select style='max-width:200px;' onchange='SetItemUnit(this)'  id='Item-" + (ItemsRowsCounter + 1) + "' tag='" + (typeof item.ItemID === 'undefined' ? item.D_ItemID : item.ItemID) + "' class='input-sm  col-sm selectitem'>" + $('#hidden_slItems').html() + "</select>" + "</td>"

    ClearAllTableRows("tblDetails");
    maxDetailsIDInTable = 0;
    $.each(pTableRows, function (i, item) {
     //   maxDetailsIDInTable = maxDetailsIDInTable + 1;
       maxDetailsIDInTable = (item.ID > maxDetailsIDInTable ? item.ID : maxDetailsIDInTable);

        AppendRowtoTable("tblDetails",
            ("<tr ID='" + item.ID + "' "+ ">"
                    + "<td class='DetailsID'> <input " + (1 == 2 ? " disabled='disabled' " : " name='Delete' ") + " type='checkbox' value='" + item.ID + "' /></td>" 
                    + "<td class='Value' style='width:9%;'><input  tag='" + (item.Value) + "' type='text' style='width:80px;font-size:90%;'  id='txtValue" + item.ID + "' class='controlStyle inputValue' data-type='number'  onkeypress='CheckValueIsDecimal(id);' onfocus='CheckValueIsDecimal(id);' onblur='CheckDecimalFormat(id);Voucher_CalculateTotal();' onchange='Details_SetIsRowChanged(id);Voucher_CalculateTotal();' data-required='false' value='" + (item.Value) + "' /> </td>"
                     + "<td class='Description' style='width:40%;'><input  tag='" + (item.Description) + "' type='text' style='width:200px;font-size:90%;text-transform:uppercase;' id='txtDescription" + item.ID + "' class='controlStyle inputDescription'  onkeypress='DisableEnterKey(id);' onfocus='DisableEnterKey(id);' onchange='Details_SetIsRowChanged(id);' data-required='true' value='" + item.Description + "' /> </td>"
                     + "<td class='AccountID' style='width:20%;' val='" + (item.AccountID) + "'>" + "<select  tag='" + (item.AccountID) + "' id='slAccount" + item.ID + "' style='width:100%;' class='controlStyle selectAccountID' onfocus='DisableBackspaceKey(id);' onkeydown='DisableBackspaceKey(id);' onkeypress='DisableBackspaceKey(id);' onchange='Details_SetIsRowChanged(id);Details_FillSlSubAccountInTable(this , \".SubAccountID\" ," + item.SubAccountID + ");' data-required='true'>" + $('#slAccount').html() + "</select></td>"
                    + "<td class='SubAccountID' style='width:20%;' val='" + (item.SubAccountID) + "'>" + "<select  tag='" + (item.SubAccountID) + "'id='slSubAccount" + item.ID + "' style='width:100%;' class='controlStyle selectSubAccountID' onfocus='DisableBackspaceKey(id);' onkeydown='DisableBackspaceKey(id);' onkeypress='DisableBackspaceKey(id);' onchange='Details_SetIsRowChanged(id);SubAccount_ChangedSalesMan(" + item.ID + ");' data-required='false'>" + "<option value=0></option>" + "</select></td>"
                    + "<td class='CostCenterID' style='width:15%;' val='" + (item.CostCenterID) + "'>" + "<select  tag='" + (item.CostCenterID) + "' id='slCostCenter" + item.ID + "' style='width:150px;' class='controlStyle selectcostcenterID' onfocus='DisableBackspaceKey(id);' onkeydown='DisableBackspaceKey(id);' onkeypress='DisableBackspaceKey(id);' onchange='Details_SetIsRowChanged(id);' data-required='false'>" + $('#slCostCenter').html() + "</select></td>"

                    + "<td class='OperationID' style='width:20%;' val=''><select  tag='" + (item.OperationID == 0 ? "" : item.OperationID) + "' id='slOperation" + item.ID + "' style='width:100%;' class='controlStyle selectOperation' onfocus='DisableBackspaceKey(id);' onkeydown='DisableBackspaceKey(id);' onchange='Details_HouseAndCertificateChangedByOperation(" + item.ID + ");' onkeypress='DisableBackspaceKey(id);' onchange='; ' data-required='false'></select> </td>"
                    + "<td class='OperationSearch' style='width:3%; padding:6px 0px 6px 0px;' val=''><button id='btnOperationSearch' class='btn btn-sm btn-lightblue m-t-xmd' type='button' onclick='ChangeOperation(" + item.ID + ");'><i class='fa fa-search'></i></button> </td>"
                  //   + "<td class='Operation' style='width:20%;' val=''><select  tag='" + (item.OperationID == 0 ? "" : item.OperationID) + "' id='slOperation" + i + "' style='width:100%;' class='controlStyle slOperation' onfocus='DisableBackspaceKey(id);' onkeydown='DisableBackspaceKey(id);' onchange='Details_HouseAndCertificateChangedByOperation(" + i + ");' onkeypress='DisableBackspaceKey(id);' onchange='; ' data-required='true'>" + $('#slOperation').html() + "</select></td>"
                       + "<td class='HouseID' style='width:10%;' val=''><select  tag='" + (item.HouseID == 0 ? "" : item.HouseID) + "' id='slHouse" + item.ID + "' style='width:100%;' class='controlStyle selectHouseNumber' onfocus='DisableBackspaceKey(id);' onkeydown='DisableBackspaceKey(id);' onkeypress='DisableBackspaceKey(id);' onchange='Details_ChangedByHouseAndCertificateAndTrucingOrder(" + maxDetailsIDInTable + ',' + 1 + "); ' data-required='false'>" + $('#slHouse').html() + "</select></td>"
                       + "<td class='TruckingOrderID' style='width:10%;' val=''><select  tag='" + (item.TruckingOrderID == 0 ? "" : item.TruckingOrderID) + "' id='slTruckingOrder" + item.ID + "' style='width:100%;' class='controlStyle selectTruckingOrder' onfocus='DisableBackspaceKey(id);' onkeydown='DisableBackspaceKey(id);' onkeypress='DisableBackspaceKey(id);' onchange='Details_ChangedByHouseAndCertificateAndTrucingOrder(" + maxDetailsIDInTable + ',' + 2 + "); ' data-required='false'>" + $('#slTruckingOrder').html() + "</select></td>"

                     + "<td class='BranchID' style='width:10%;' val=''><select  tag='" + (item.BranchID == 0 ? "" : item.BranchID) + "' id='slBranch" + item.ID + "' style='width:100%;' class='controlStyle selectBranch' onfocus='DisableBackspaceKey(id);' onkeydown='DisableBackspaceKey(id);' onkeypress='DisableBackspaceKey(id);' onchange='Details_ChangedByHouseAndCertificateAndTrucingOrder(" + maxDetailsIDInTable + ',' + 3 + "); ' data-required='false'>" + $('#slBranch').html() + "</select></td>"

                   + "<td class='Documented'> <input name='Documented' id=cbDocumented" + item.ID + " type='checkbox'  " + (item.IsDocumented ? " checked='checked' " : "") + " /></td>"
                    + "<td class='SelectedIDsToUpdate hide'> <input name='SelectedIDsToUpdate' id='SelectedIDsToUpdate" + item.ID + "' type='checkbox' value='" + item.ID + "' /></td></tr>"
                   ));

        $("#slOperation" + item.ID).css({ "width": "100%" }).select2();

        if ($("#slOperation" + item.ID + " option[value='" + item.OperationID, +"']").length == 0)
            $("#slOperation" + item.ID).append($('<option>', {
                value: item.OperationID,
                //BranchID: item.OperationBranchID,
               // ReferenceNumber: item.OperationReferenceNumber,
                CodeSerial: item.CodeSerial,
                text: item.OperationCode

            }));
        $("#slOperation" + item.ID).trigger("change");
        //  if ($("#hDefaultUnEditableCompanyName").val() == "ERP") {
        //Start Auto Filter
        $("#slAccount" + item.ID).css({ "width": "280px" }).select2();
        $("#slHouse" + item.ID).css({ "width": "100%" }).select2();
        $("#slTruckingOrder" + item.ID).css({ "width": "100%" }).select2();

        $("#slCostCenter" + item.ID).css({ "width": "100%" }).select2();
        $("#slCostCenter" + item.ID).trigger("change");


        $("#slBranch" + item.ID).css({ "width": "100%" }).select2();

        $("#slSubAccount" + item.ID).css({ "width": "280px" }).select2();
        $("div[tabindex='-1']").removeAttr('tabindex');
        //$("#slAccount" + item.ID).trigger("change");
        //$("#slSubAccount" + item.ID).trigger("change");
  
        //End Auto Filter
        //  }
        if ($("#hDefaultUnEditableCompanyName").val() == "SAF") {
            $(".isDepartement").removeClass("hide");
            $("#isDepartement").removeClass('hide');

            $(".isBranch").addClass("hide");
            $("#isBranch").addClass('hide');
        }
        else {
            $(".isBranch").removeClass("hide");
            $("#isBranch").removeClass('hide');

            $(".isDepartement").addClass("hide");
            $("#isDepartement").addClass('hide');
        }

        if (i == pTableRows.length - 1)
        {
            debugger;
            FillHTMLtblInputs("#tblDetails > tbody");
        }
          


    });



    //ApplyPermissions();
    BindAllCheckboxonTable("tblDetails", "DetailsID", "cbDetailsDeleteHeader");//Parameters(pTableName, pCheckBoxColumnClassName, pCheckBoxHeaderID)
    CheckAllCheckbox("HeaderDeleteDetailsID");
    //HighlightText("#tblDetails>tbody>tr", $("#txt-Search").val().trim());
    if (showDeleteFailMessage) { //sherif: if deletion failed due to foreign key, then this message is shown
        swal(strSorry, strDeleteFailMessage);
        showDeleteFailMessage = false;
    }
    count = 0;
}
function Details_ClearAllControls() {
    debugger;
    if ($("#txtExchangeRate").val() == 0)
        swal("Sorry", "Exchange rate can not be 0.");
    else if (ValidateForm("form", "VoucherModal")) {
        ClearAll("#DetailsModal");
     //   if ($("#hDefaultUnEditableCompanyName").val() == "ERP") {
            //Start Auto Filter
            $("#slAccount").trigger("change");
            $("#slSubAccount").trigger("change");
            //End Auto Filter
     //   }
        jQuery("#DetailsModal").modal("show");
    }
}
function Details_FillControls(pDetailsID) {
    debugger;
    if ($("#txtExchangeRate").val() == 0)
        swal("Sorry", "Exchange rate can not be 0");
    else if (ValidateForm("form", "VoucherModal")) {
        ClearAll("#DetailsModal");
        $("#lblDetails").html($("#lblShown").html());

        var tr = $("#tblDetails tr[ID='" + pDetailsID + "']");

        $("#lblDetails").html($("#lblShown").html());
        $("#hDetailsID").val(pDetailsID);
        $("#txtValue").val($(tr).find("td.Value").text());
        $("#txtDescription").val($(tr).find("td.Description").text());
        $("#slAccount").trigger("change");
        $("#slAccount").val($(tr).find("td.AccountID").text());
    //    if ($("#hDefaultUnEditableCompanyName").val() == "ERP") {
            //Start Auto Filter
       
            $("#slSubAccount").trigger("change");
            //End Auto Filter
     //   }
        Details_FillSlSubAccount("slSubAccount", $(tr).find("td.SubAccountID").text());
        $("#slCostCenter").val($(tr).find("td.CostCenterID").text());
        $("#cbIsDocumented").prop("checked", $("#cbIsDocumented" + pDetailsID).prop("checked"));
        jQuery("#DetailsModal").modal("show");
    }
}
function Details_Save(pSaveAndNew, callback) {
    debugger;
    if ($("#txtValue").val() == 0)
        swal("Sorry", "Value can not be 0.");
    else if (ValidateForm("form", "DetailsModal")) {
        FadePageCover(true);
        var pParametersWithValues = {
            pID: $("#hID").val() == "" ? 0 : $("#hID").val()
            , pCode: $("#txtCode").val().trim().toUpperCase()
            , pVoucherDate: ConvertDateFormat($("#txtVoucherDate").val())
            , pSafeID: $("#slSafe").val()
            , pCurrencyID: $("#slCurrency").val()
            , pExchangeRate: $("#txtExchangeRate").val()
            , pChargedPerson: $("#txtChargedPerson").val().trim() == "" ? "0" : $("#txtChargedPerson").val().trim().toUpperCase()
            , pNotes: $("#txtNotes").val().trim() == "" ? "0" : $("#txtNotes").val().trim().toUpperCase()
            , pTaxID: ($("#slTax").val() != 0 && $("#txtTaxValue").val() != 0 ? $("#slTax").val() : 0)
            , pTaxValue: ($("#slTax").val() != 0 && $("#txtTaxValue").val() != 0 ? $("#txtTaxValue").val() : 0)
            , pTaxSign: ($("#slTax option:selected").attr("IsDebitAccount") == 1
                            && (glbFormCalled == constVoucherCashIn || glbFormCalled == constVoucherChequeIn)
                         )
                         ||
                         ($("#slTax option:selected").attr("IsDebitAccount") == 0
                             && (glbFormCalled == constVoucherCashOut || glbFormCalled == constVoucherChequeOut)
                         )
                         ? -1 : 1
            , pTaxID2: ($("#slTax2").val() != 0 && $("#txtTaxValue2").val() != 0 ? $("#slTax2").val() : 0)
            , pTaxValue2: ($("#slTax2").val() != 0 && $("#txtTaxValue2").val() != 0 ? $("#txtTaxValue2").val() : 0)
            , pTaxSign2: ($("#slTax2 option:selected").attr("IsDebitAccount") == 1
                            && (glbFormCalled == constVoucherCashIn || glbFormCalled == constVoucherChequeIn)
                         )
                        ||
                         ($("#slTax2 option:selected").attr("IsDebitAccount") == 0
                             && (glbFormCalled == constVoucherCashOut || glbFormCalled == constVoucherChequeOut)
                         )
                         ? -1 : 1
            , pTotal: parseFloat($("#lblTotal").text().replace(":", "")) //will be update in controller
            , pTotalAfterTax: parseFloat($("#lblTotalAfterTax").text().replace(":", "")) //will be update in controller
            , pIsAGInvoice: false
            , pAGInvType_ID: 000
            , pInv_No: 000
            , pInvoiceID: 000
            , pJVID1: 000
            , pJVID2: 000
            , pJVID3: 000
            , pJVID4: 000
            , pSalesManID: 000
            , pforwOperationID: 000
            , pIsCustomClearance: false
            , pTransType_ID: 000
            , pVoucherType: glbFormCalled
            , pIsCash: ($("#cbIsCash").prop("checked") && glbFormCalled == constVoucherCashOut) ? true : false
            , pIsCheque: false
            , pPrintDate: "01/01/1900"
            , pChequeNo: 000
            , pChequeDate: "01/01/1900"
            , pBankID: 000
            , pOtherSideBankName: 0
            , pCollectionDate: "01/01/1900"
            , pCollectionExpense: 000
            , pDisbursementJob_ID: $("#slDisbursementJobs").val() == "" ? 0 : $("#slDisbursementJobs").val()
            , pSL_SalesManID: $("#slSalesMan").val() == "" ? 0 : $("#slSalesMan").val()

            //Details Data
            , pDetailsID: $("#hDetailsID").val() == "" ? 0 : $("#hDetailsID").val()
            , pValue: $("#txtValue").val()
            , pDescription: $("#txtDescription").val().trim() == "" ? 0 : $("#txtDescription").val().trim().toUpperCase()
            , pAccountID: $("#slAccount").val()
            , pSubAccountID: $("#slSubAccount").val()
            , pCostCenterID: $("#slCostCenter").val()
            , pIsDocumented: $("#cbIsDocumented").prop("checked")
            , pDetailsInvoiceID: 000
            , pOperationID: $("#slOperation").val()
            , pHouseID: $("#slHouse").val()
            , pBranchID: $("#slBranch").val()
            , pTruckingOrderID: $("#slTruckingOrder").val()



        };
        CallPOSTFunctionWithParameters("/api/Voucher/VoucherDetails_Save", pParametersWithValues
            , function (pData) {
                var pMessageReturned = pData[0];
                if (pMessageReturned == "") {
                    var pVoucherID = pData[1];
                    var pDetails = JSON.parse(pData[2]);
                    $("#hID").val(pVoucherID);
                    Details_BindTableRows(pDetails);
                    Voucher_CalculateTotal();
                    $("#txtCode").val(pData[3]);
                    $("#lblShown").html("<span> : </span><span> " + $("#txtCode").val().trim() + "</span>");
                    if ($("[id$='hf_ChangeLanguage']").val() == "ar")
                        $("#lblShown").reverseChildren();
                    if ($("[id$='hf_ChangeLanguage']").val() == "en") swal("Success", "Saved successfully.");
                    else if ($("[id$='hf_ChangeLanguage']").val() == "ar") swal("نجاح", "تم الحفظ بنجاح.");
                    Voucher_LoadingWithPaging();
                    if (pSaveAndNew)
                        Details_ClearAllControls();
                    else
                        jQuery("#DetailsModal").modal("hide");
                    FadePageCover(false); //called in LoadWithPaging
                    // return "1"
                    if (callback != null && callback != undefined)
                        callback();
                }
                else {
                    swal("Sorry", pMessageReturned);
                    FadePageCover(false);
                    //  return "0"
                }
            }
            , null);
    }
}
function Details_FillSlSubAccount(pSlName, pSubAccountID) {
    debugger;
    if ($("#slAccount").val() == 0) //No Account is selected so just empty subaccounts
        $("#slSubAccount").html("<option value=0>" + TranslateString("SelectFromMenu") + "</option>");
    else {
        FadePageCover(true);
        CallGETFunctionWithParameters("/api/ChartOfLinkingAccounts/LoadSubAccountDetails"
            , {
                pLanguage: $("[id$='hf_ChangeLanguage']").val()
                , pWhereClauseSubAccountDetails: "WHERE IsMain=0 AND Account_ID=" + $("#slAccount").val()
                , pOrderBy: "Name"
            }
            , function (pData) {
                FillListFromObject_ERP(pSubAccountID, 4/*pCodeOrName*/, TranslateString("SelectFromMenu"), pSlName, pData[0], null);
          //      if ($("#hDefaultUnEditableCompanyName").val() == "ERP") {
                    //Start Auto Filter
                    //$("#" + pSlName).trigger("change");
                    //End Auto Filter
         //       }
                FadePageCover(false);
            }
            , null);
    }
}
function Details_DeleteList() {
    debugger;
    var pDeletedDetailsIDs = GetAllSelectedIDsAsString('tblDetails', 'Delete');
    //if (pDeletedDetailsIDs.split(',').length == $("#tblDetails tbody tr").length)
    //    swal("Sorry", "A voucher can not remain without details.");
    //else
        if (pDeletedDetailsIDs != "")
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

            for (var i = 0; i < pDeletedDetailsIDs.split(",").length; i++)
            {
                $("#tblDetails tbody tr[Counter=" + pDeletedDetailsIDs.split(",")[i] + "]").remove();
                $("#tblDetails tbody tr[ID=" + pDeletedDetailsIDs.split(",")[i] + "]").remove();
            }

                 Voucher_CalculateTotal();
                    //CallGETFunctionWithParameters("/api/Voucher/Details_Delete"
                    //    , { pDeletedDetailsIDs: pDeletedDetailsIDs, pVoucherID: $("#hID").val() }
                    //    , function (pData) {
                    //        var pMessageReturned = pData[2];
                    //        if (pData[2] == "") {
                    //            Details_BindTableRows(JSON.parse(pData[1]));
                    //            if (pData[0])
                    //                swal("Success", "Deleted successfully.");
                    //            else
                    //                swal("Sorry", strDeleteFailMessage);
                    //            Voucher_CalculateTotal();
                    //            Voucher_LoadingWithPaging();
                    //            //FadePageCover(false); //called in LoadWithPaging
                    //        }
                    //        else {
                    //            swal("Sorry", pMessageReturned);
                    //            FadePageCover(false);
                    //        }

                    //    }
                    //    , null);
            
                   FadePageCover(false);

        });
}



//xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx  xxxxxxxxxxxxxxxxxxxx xxxxx
//xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx  xxxxxxxxxxxxxxxxxxxx xxxxx
//xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx  xxxxxxxxxxxxxxxxxxxx xxxxx


/*********************************************Main Screen Fns (showing partners)************************************************/
function OpenInvModal() {
    if ($("#txtExchangeRate").val() == 0)
        swal("Sorry", "Exchange rate can not be 0.");
    else if (ValidateForm("form", "VoucherModal")) {
        if ($('#slCurrency').val() == "0") {
            swal("Excuse me !", "Select Currency", "warning")

        }
        else {
            $("#cbIsCashInvoice").prop("checked", false);
            $("#cbIsAllocation").prop("checked", true);
            Voucher_cbIsCash();

           // if (glbFormCalled == constVoucherCashIn)
                glbTransactionType = constTransactionReceivableAllocation;
            //else
            //    glbTransactionType = constTransactionPayableAllocation;

            jQuery('#InvModal').modal('show');

            $.ajax({
                type: "GET",
                url: strServerURL + "api/A_ARAllocation/UnapprovingAllocations_IntializeData",
                data: { 'PartenertTypeID': "-1" },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (d) {
                    // Fill_SelectInputAfterLoadData(d[0], 'ID', 'Name', '<--  Partener Type -->', '#slPartnerType', '');
                    FillListFromObject(null, 1/*pCodeOrName*/, TranslateString("SelectFromMenu")/*"Select Pay. Type"*/, "slPartnerType", d[0], null);
                },
                error: function (jqXHR, exception) {
                    debugger;
                    swal("Oops!", "Please, contact your technical support!  this is print region !", "error");
                    FadePageCover(false);
                }
            });




        }
    }




}
function A_ARAllocation_Partners_BindTableRows(pTableRows) {
    debugger;
    $("#hl-menu-Accounting").parent().addClass("active");
    ClearAllTableRows("tblPartner");
    ////var editControlsText = " class='btn btn-xs btn-rounded btn-info float-right' > <i class='fa fa-pencil' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + TranslateString("Edit") + "</span>";
    //var printControlsText = " class='btn btn-xs btn-rounded btn-info float-right' > <i class='fa fa-print' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + TranslateString("Cashier") + "</span>";
    //var notificationsControlsText = " class='btn btn-xs btn-rounded btn-lightblue float-right' > <i class='fa fa-quote-left' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + TranslateString("Notifications") + "</span>";
    //var cancelControlsText = " class='btn btn-xs btn-rounded btn-danger float-right' > <i class='fa ' style='padding-left: 5px;'>X &nbsp;&nbsp;&nbsp;</i> <span style='padding-right: 5px;'>" + "إلغاء" + "</span>";
    //var cancelledControlsText = " class='btn btn-xs btn-rounded btn-danger float-right' > <i class='fa ' style='padding-left: 5px;'>X </i> <span style='padding-right: 5px;'>" + "تم إلغائها" + "</span>";
    //var restoreControlsText = " class='btn btn-xs btn-rounded btn-warning float-right' > <i class='fa fa-undo' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + "استعادة" + "</span>";
    $.each(pTableRows, function (i, item) {
        AppendRowtoTable("tblPartner",
            ("<tr ID='" + item.ID + "' "
                //+ (" ondblclick='ARAllocation_EditByDblClick(" + item.ID + "," + item.PartnerTypeID + "," + '"' + item.Name + '","' + (item.AvailableBalance.toFixed(4) + ' ' + $("#hDefaultCurrencyCode").val()) + '"' + ");' ")
                + (" ondblclick='ARAllocation_EditByDblClick(" + item.ID + "," + item.PartnerTypeID + "," + '"' + item.Name + '","' + (glbTransactionType == constTransactionReceivableAllocation ? item.UnAllocatedReceivables : item.UnAllocatedPayables) + '"' + ");' ")
                //+ (
                //    (item.IsApproved)
                //    ? (" class='text-primary' " + " ondblclick='ARAllocation_EditByDblClick(" + item.ID + ");' ")
                //    : (" ondblclick='ARAllocation_EditByDblClick(" + item.ID + ");' ")
                //    )
                + ">"
                //+ "<td class='ARFID'> <input " + (item.TotalActuallyReceived > 0 || item.UnPaidCashiersCount > 0 ? " disabled='disabled' " : " name='Delete' ") + " type='checkbox' value='" + item.ID + "' /></td>"
                //+ "<td class='PartnerID'> <input " + (item.IsApproved ? " disabled='disabled' " : " name='Delete' ") + " type='checkbox' value='" + item.ID + "' /></td>"
                + "<td class='PartnerID'> <input " + " name='Delete' " + " type='checkbox' value='" + item.ID + "' /></td>"
                + "<td class='PartnerTypeID' val=" + item.PartnerTypeID + " >" + item.PartnerTypeName + "</td>"
                + "<td class='PartnerCode hide'>" + (item.Code == 0 ? "" : item.Code) + "</td>"
                + "<td class='PartnerID' val=" + item.PartnerID + " >" + item.Name.split('(')[0] + "</td>"
                //+ "<td class='AvailableBalance'>" + item.AvailableBalance.toFixed(4) + ' ' + $("#hDefaultCurrencyCode").val() + "</td>"
                + "<td class='AvailableBalance'>" + (glbTransactionType == constTransactionReceivableAllocation ? item.UnAllocatedReceivables : item.UnAllocatedPayables) + "</td>"
                ////Note:Restore may coz ticket no. duplication so i opened notifications and prevented Restore
                //+ "<td class=''>"
                //    + (item.IsCancelled
                //        ? ("<a href='#' " + (!(IsDeptManagerRoleID || IsFinancialManagerID || IsGeneralManagerRoleID) ? " disabled='disabled' " : " ") + " onclick='ARF_CancelOrRestore(" + item.ID + ',"' + item.CustomerName + '",0' + ");' " + restoreControlsText + "</a>")
                //        //? ("<a href='#' " + ($("#hf_CanDelete").val() == 0 ? " disabled='disabled' " : " ") + (item.IsCancelled ? " disabled='disabled' " : " ") + cancelledControlsText + "</a>")
                //        : ("<a href='#' " + (!(IsDeptManagerRoleID || IsFinancialManagerID || IsGeneralManagerRoleID) ? " disabled='disabled' " : " ") + " onclick='ARF_CancelOrRestore(" + item.ID + ',"' + item.CustomerName + '",1' + ");' " + cancelControlsText + "</a>")
                //      )
                //    + "<a href='#'" + (item.IsCancelled ? "disabled='disabled' " : " ") + " onclick='Notification_ARF_FillControls(" + item.ID + ',' + item.FormNo + "," + "10" + ");' " + notificationsControlsText + "</a>" + "<a href='#'" + (item.IsCancelled ? " disabled='disabled' " : " ") + " onclick='Cashier_ARF_FillControls(" + item.ID + ',' + item.FormNo + "," + "10" + ");' " + printControlsText + "</a>"
                //    ////i disable notifications if its cancelled and not manager
                //    //+ "<a href='#'" + (item.IsCancelled && !(IsDeptManagerRoleID || IsFinancialManagerID || IsGeneralManagerRoleID) ? "disabled='disabled' " : " ") + " onclick='Notification_ARF_FillControls(" + item.ID + ',' + item.FormNo + "," + "10" + ");' " + notificationsControlsText + "</a>" + "<a href='#'" + (item.IsCancelled ? " disabled='disabled' " : " ") + " onclick='Cashier_ARF_FillControls(" + item.ID + ',' + item.FormNo + "," + "10" + ");' " + printControlsText + "</a>"
                //+ "</td>"

                + "<td class='hide'><a href='#AllocationModal' data-toggle='modal' onclick='ARAllocation_FillControls(" + item.ID + ");' " + editControlsText + "</a></td></tr>"));
    });
    ApplyPermissions();
    BindAllCheckboxonTable("tblPartner", "PartnerID", "cbPartnerDeleteHeader");//Parameters(pTableName, pCheckBoxColumnClassName, pCheckBoxHeaderID)
    CheckAllCheckbox("HeaderDeletePartnerID");
    HighlightText("#tblPartner>tbody>tr", $("#txt-Search").val().trim());
    if (showDeleteFailMessage) {//sherif: if deletion failed due to foreign key, then this message is shown
        swal(strSorry, strDeleteFailMessage, "warning");
        showDeleteFailMessage = false;
    }
    $("#hl-homepage").on("click", function () { LoadViews("hl-homepage"); });

    console.log(parent.strBindTableRowsFunctionName);
}
function ARAllocation_Partners_LoadingWithPaging() {
    debugger;
    var pWhereClause = ARAllocation_Partners_GetWhereClause();
    var pOrderBy = "PartnerTypeID, Name";
    var pPageNumber = 1;
    var pPageSize = $('#select-page-size').val();
    var pControllerParameters = { pIsLoadArrayOfObjects: false, pPageNumber: pPageNumber, pPageSize: pPageSize, pWhereClauseAllocation_Partners: pWhereClause, pOrderBy: pOrderBy }
    LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned("div-Pager", "select-page-size", "spn-first-page-row", "spn-last-page-row", "spn-total-count", "div-Text-Total", strLoadWithPagingFunctionName, pWhereClause, pOrderBy, pPageNumber, pPageSize, pControllerParameters, function (pData) { A_ARAllocation_Partners_BindTableRows(JSON.parse(pData[0])); });
    HighlightText("#tblPartner>tbody>tr", $("#txt-Search").val().trim());
}
function ARAllocation_Partners_GetWhereClause() {
    debugger;
    var pWhereClause = "";
    if (glbTransactionType == constTransactionReceivableAllocation) {
        pWhereClause = " WHERE UnAllocatedReceivables IS NOT NULL";
    }
    else if (glbTransactionType == constTransactionPayableAllocation) {
        // var pWhereClause = " WHERE (UnAllocatedPayables IS NOT NULL OR PartnerTypeID=" + constCustodyPartnerTypeID + ")";
          pWhereClause = " WHERE UnAllocatedPayables IS NOT NULL";
    }
    if ($("#txt-Search").val().trim() != "") {
        pWhereClause += " AND (";
        pWhereClause += " PartnerTypeName like N'%" + $("#txt-Search").val().trim() + "%' ";
        pWhereClause += " OR Name like N'%" + $("#txt-Search").val().trim() + "%' ";
        //pWhereClause += " OR Code like N'%" + $("#txt-Search").val().trim() + "%' ";
        pWhereClause += ")";
    }
    if ($("#slPartner").val() != "") {
        pWhereClause += " AND (";
        pWhereClause += " ID = N'" + $("#slPartner").val() + "' ";
        pWhereClause += " AND PartnerTypeID = N'" + $("#slPartner option:selected").attr("PartnerTypeID") + "' ";
        pWhereClause += ")";
    }
    if ($("#slPartnerType").val() != "") {
        pWhereClause += " AND (";
        pWhereClause += " PartnerTypeID = N'" + $("#slPartnerType").val() + "' ";
        pWhereClause += ")";
    }


    return pWhereClause;
}
function ARAllocation_PartnerTypeChanged() {
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
//function ARAllocation_PartnerTypeChanged() {
//    //debugger;
//    //$("#slPartner").val("");
//    //$("#slPartner option").removeClass("hide");
//    //if ($("#slPartnerType").val() != "") //handle show all partners
//    //    $("#slPartner option[PartnerTypeID!=" + $("#slPartnerType").val() + "][value!=''" + "]").addClass("hide");
//    debugger;
//    $("#slPartner").html("<option value=''>All Partners</option>");//to quickly empty
//    var pPartnerTypeID = $("#slPartnerType").val() == "" ? 0 : $("#slPartnerType").val();
//    var pWhereClause = ARAllocation_Partners_GetWhereClause();
//    var pOrderBy = " ID DESC "
//    var pPageNumber = 1; //($("#div-Pager li.active a").text() == "" ? 1 : $("#div-Pager li.active a").text())
//    var pPageSize = $('#select-page-size').val();
//    var pControllerParameters = { pPartnerTypeID: pPartnerTypeID, pWhereClauseAllocation_Partners: pWhereClause, pOrderBy: pOrderBy, pPageNumber: pPageNumber, pPageSize: pPageSize }
//    LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned("div-Pager", "select-page-size", "spn-first-page-row", "spn-last-page-row", "spn-total-count", "div-Text-Total", "/api/A_ARAllocation/ARAllocation_Partners_PartnerTypeChanged", pWhereClause, pOrderBy, pPageNumber, pPageSize, pControllerParameters
//        , function (pData) {
//            A_ARAllocation_Partners_BindTableRows(JSON.parse(pData[0]));
//            FillListFromObject(null, 5/*pCodeOrName*/, "All Partners", "slPartner", pData[2], null);
//            FadePageCover(false);
//        }
//        , null);
//}

function FillDivWithCheckboxes_DynamicField(pDivName, pData, pCheckboxNameAttr, callback) {
    //Clear the div
    $("#" + pDivName).html("");
    var option = "";
    $.each(JSON.parse(pData), function (i, item) {
        option += '<div class="swapCheckBoxesClass"> ';
        option += ' <input type="checkbox" Amount="' + item.Amount + '"   PaidAmount="' + item.PaidAmount + '"   RemainingAmount ="' + item.RemainingAmount + '" name="' + pCheckboxNameAttr + '" onfocus="DisableEnterKey(id);" onkeypress="DisableEnterKey(id);"  value="' + item.ID + '" /> ';
        option += ' <label>&nbsp; ' + item.Code + ' - ' + item.OperationCode + ' - ' + item.PartnerName;
        option += ' &nbsp;</label> </div>';
    });
    $("#" + pDivName).append(option);
    if ($("[id$='hf_ChangeLanguage']").val() == "ar") $(".swapCheckBoxesClass:not(.reversed)").reverseChildren();
}


/*-------------------------------Operation----------------------------------------*/
function OpenOperationModal() {

    if (ValidateForm("form", "VoucherModal")) {
        if ($('#slCurrency').val() == "0") {
            swal("Excuse me !", "Select Currency", "warning")

        }
        else if ((($('tr', $("#tblDetails").find('tbody')).length) == 0)) {
            swal("Excuse me !", "Fill Details", "warning")
        }
        else {

            jQuery('#OperationModal').modal('show');

            //$.ajax({
            //    type: "GET",
            //    url: strServerURL + "api/A_ARAllocation/UnapprovingAllocations_IntializeData",
            //    data: { 'PartenertTypeID': "-1" },
            //    contentType: "application/json; charset=utf-8",
            //    dataType: "json",
            //    success: function (d) {
            //        Fill_SelectInputAfterLoadData(d[0], 'ID', 'Name', '<--  Partener Type -->', '#slPartnerType', '');
            //    },
            //    error: function (jqXHR, exception) {
            //        debugger;
            //        swal("Oops!", "Please, contact your technical support!  this is print region !", "error");
            //        FadePageCover(false);
            //    }
            //});


        }
    }




}
function A_OperationChanged() {
    debugger;
    //$("#slPartner").html("<option value=''>All Partners</option>");//to quickly empty
    var pWhereClause = A_Operation_GetWhereClause();
    var pOrderBy = " ID DESC "
    var pPageNumber = 1; //($("#div-Pager li.active a").text() == "" ? 1 : $("#div-Pager li.active a").text())
    var pPageSize = $('#select-page-size').val();
    var pControllerParameters = {
        pIsLoadArrayOfObjects: false
        , pPageNumber: 1
        , pPageSize: $("#select-page-size option:selected").text()
        , pWhereClause: pWhereClause
        , pIsBindTableRows: false
        , pWhereClause_Routings: "0"
        , pOrderBy: ""
    }
    LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned("div-Pager", "select-page-size", "spn-first-page-row", "spn-last-page-row", "spn-total-count", "div-Text-Total", "api/Operations/LoadWithWhereClause", pWhereClause, "ID DESC", 1, 10, pControllerParameters
                        , function (pData) { Operation_BindTableRows(JSON.parse(pData[0])); });

}
function A_Operation_GetWhereClause() {
    debugger;
    var pWhereClause = "";
    if ($("#txtOperationName").val().trim() != "") {
        pWhereClause += "  ";
        pWhereClause += " where Code  like N'%" + $("#txtOperationName").val().trim() + "%' ";
        //pWhereClause += " OR Code like N'%" + $("#txt-Search").val().trim() + "%' ";
        pWhereClause += "";
    }

    return pWhereClause;
}
function Operations_LoadingWithPaging() {
    debugger;
    var pWhereClause = A_Operation_GetWhereClause();
    var pOrderBy = " ID DESC ";
    var pControllerParameters = {
        pIsLoadArrayOfObjects: false
        , pPageNumber: 1
        , pPageSize: $("#select-page-size option:selected").text()
        , pWhereClause: pWhereClause
        , pIsBindTableRows: false
        , pWhereClause_Routings: "0"
        , pOrderBy: ""
    }
    LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned("div-Pager", "select-page-size", "spn-first-page-row", "spn-last-page-row", "spn-total-count", "div-Text-Total", "api/Operations/LoadWithWhereClause", pWhereClause, "ID DESC", 1, 10, pControllerParameters
                        , function (pData) { Operation_BindTableRows(JSON.parse(pData[0])); });
    //HighlightText("#tblOperations>tbody>tr", $("#txt-Search").val().trim());
}

function OperationsManagement_BindTableRows(pOperations) {
    ClearAllTableRows("tblOperations");
    debugger;
    var FormattedTodaysDate = TodaysDate.toLocaleDateString();
    //editControlsText = " class='btn btn-xs btn-rounded btn-info float-right' > <i class='fa fa-pencil' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + TranslateString("Edit") + "</span>";
    var copyControlsText = " class='btn btn-xs btn-rounded btn-info float-right' > <i class='fa fa-copy' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + "Copy" + "</span>";
    $.each(pOperations, function (i, item) {
        AppendRowtoTable("tblOperations",
        ("<tr ID='" + item.ID + "' ondblclick='SwitchToOperationsEditView(" + item.ID + ");' class='"
            + (item.OperationStageID == CancelledQuoteAndOperStageID
                ? "static-text-danger" //cancelled operation
                : (item.OperationStageName == "CLOSED" //(Date.prototype.compareDates(FormattedTodaysDate, GetDateWithFormatMDY(item.CloseDate)) <= 0
                    ? "static-text-primary" //closed operation
                    : "")
              )
            + "'>"//of tr
                    + "<td class='shownDirectionIconName'><i class= 'fa " + item.DirectionIconName + " " + item.DirectionIconStyle + " fa-2x'/><i class= 'fa " + item.TransportIconName + " " + item.TransportIconStyle + " fa-2x'/></td>"
                    + "<td class='Code'>" + item.Code + "</td>"
                    //the next line differs from the preceeding one that date could be shown as today, tomorrow, yesterday
                    + "<td class='shownOpenDate'>" //+ "<span class='pull-left thumb-sm  calendar-icon-style'>"
                                              //+ " <i class='fa fa-calendar'></i>"
                                              //+ " <small class='text-muted'>" + ConvertDateFormat(GetDateWithFormatMDY(item.CreationDate)) + "</small>"
                                              //+ " <small>" + GetShownDate(FormattedTodaysDate, GetDateWithFormatMDY(item.OpenDate)) + "</small>"
                                              + GetShownDate(FormattedTodaysDate, GetDateWithFormatMDY(item.OpenDate))
                                              //+ "</span>"
                                              + "</td>"
                    + "<td class='Client'>" + (item.BookingPartyName != 0 && item.BLType == constMasterBLType ? item.BookingPartyName : (item.ClientName == 0 ? "" : item.ClientName)) + "</td>"
                    + "<td class='Routing'>" + ($("#hDefaultUnEditableCompanyName").val() == "VEN"
                                                    ? (item.POLCode + " > " + item.PODCode + (item.AirlineCode == 0 ? "" : " - " + item.AirlineCode))
                                                    : (item.POLName + " > " + item.PODName)
                                               ) + "</td>"
                    + "<td class='ShipmentType'>" + GetShipmentType(item.ShipmentType) + " " + item.RepBLTypeShown + "</td>"
                    + "<td class='BookingNumbers " + ($("#hDefaultUnEditableCompanyName").val() == "VEN" ? "hide" : "") + "'>" + item.BookingNumbers + "</td>"
                    + "<td class='MasterBL " + ($("#hDefaultUnEditableCompanyName").val() == "VEN" ? "hide" : "") + "'>" + (item.MasterBL == 0 ? "" : item.MasterBL) + "</td>"
                    + "<td class='MoveType " + ($("#hDefaultUnEditableCompanyName").val() == "VEN" ? "hide" : "") + "'>" + (item.MoveTypeCode == 0 ? "" : item.MoveTypeCode) + "</td>"
                    + "<td class='OperationStage' val='" + item.OperationStageID + "'>"
                            + (item.OperationStageID == CancelledQuoteAndOperStageID
                                ? item.OperationStageName //cancelled operation
                                : (Date.prototype.compareDates(FormattedTodaysDate, GetDateWithFormatMDY(item.CloseDate)) <= 0
                                    ? "CLOSED" //closed operation
                                    : item.OperationStageName)
                                ) + "</td>"
                    + "</tr>"));
    });
    //ApplyPermissions();
    //if ($("#hDefaultUnEditableCompanyName").val() == "VEN") {
    //    $("#btn-NewAdd").addClass("hide");
    //    $("#btn-NewAddFromQuotation").addClass("hide");
    //    $("#btn-NewAddAWB").removeClass("hide");
    //    $(".OperationCopyAWB").removeClass("hide");
    //}
    //else if (OA) { $(".OperationCopy").removeClass("hide"); }
    //else { $(".OperationCopy").addClass("hide"); };
    //if (OD) $("#btn-Delete").removeClass("hide"); else $("#btn-Delete").addClass("hide");

    //BindAllCheckboxonTable("tblOperations", "ID");//it attaches function to the checkboxes so when all are checked then check cb-checkall too
    //CheckAllCheckbox("ID");
    //HighlightText("#tblOperations>tbody>tr", $("#txt-Search").val().trim());
    //if (showDeleteFailMessage) { //sherif: if deletion failed due to foreign key, then this message is shown
    //    swal(strSorry, strDeleteFailMessage, "warning");
    //    showDeleteFailMessage = false;
    //    strDeleteFailMessage = "This command is not completed because of dependencies existance."
}
//$("#hl-homepage").on("click", function () { LoadViews("hl-homepage"); });

function Operation_BindTableRows(pTable) {
    var TodaysDate = new Date();
    var FormattedTodaysDate = TodaysDate.toLocaleDateString();
    //$("#tblOperation").html("");
    debugger;
    var pCheckboxNameAttr = "Delete";
    var pTableHTML = "";

    if (($('tr', $("#tblOperation").find('thead')).length) == 0) {
        pTableHTML += "<thead>";
        pTableHTML += "     <tr>";
        pTableHTML += '         <th id="HeaderDeleteOperationID" ><input id="cbAOperationItemDeleteHeader" type="checkbox" /></th>';
        pTableHTML += '         <th>Direction</th>';
        pTableHTML += '         <th>Code</th>';
        pTableHTML += '         <th>Open Date</th>';
        pTableHTML += '         <th>Client</th>';
        pTableHTML += '         <th>Routing</th>';
        pTableHTML += '         <th>ShipmentType</th>';
        pTableHTML += '         <th>BookingNumbers</th>';
        pTableHTML += '         <th>MasterBL</th>';
        pTableHTML += '         <th>MoveType</th>';
        pTableHTML += '         <th>OperationStage</th>';
        pTableHTML += '         <th class="rounded-right hide"></th>';
        pTableHTML += "     </tr>";
        pTableHTML += "</thead>";
        pTableHTML += "<tbody>";

    }


    $.each(pTable, function (i, item) {
        debugger;
        var NewID = item.ID;

        var table = $("#tblOperation");
        var exist = false;
        $('#' + "tblOperation" + ' td').find('input[name="' + "Delete" + '"]').each(function () {
            var OldID = ($(this).attr('value'));
            if (OldID == NewID) {
                exist = true;
                // break the loop once found
                return false;
            }
        });

        if (exist == false) {
            pTableHTML += "<tr ID='" + item.ID + "class='"
    + (item.OperationStageID == CancelledQuoteAndOperStageID
        ? "static-text-danger" //cancelled operation
        : (item.OperationStageName == "CLOSED" //(Date.prototype.compareDates(FormattedTodaysDate, GetDateWithFormatMDY(item.CloseDate)) <= 0
            ? "static-text-primary" //closed operation
            : "")) + "'>";
            pTableHTML += "<td class='ID ' > <input name='" + pCheckboxNameAttr + "' type='checkbox' value='" + item.ID + "' " + (1 == 1 ? "" : "checked='checked'") + "></td> ";
            pTableHTML += "<td class='shownDirectionIconName'><i class= 'fa " + item.DirectionIconName + " " + item.DirectionIconStyle + " fa-2x'/><i class= 'fa " + item.TransportIconName + " " + item.TransportIconStyle + " fa-2x'/></td>";
            pTableHTML += "<td class='Code'>" + item.Code + "</td>";
            pTableHTML += "<td class='shownOpenDate'>" //+ "<span class='pull-left thumb-sm  calendar-icon-style'>"
                //+ " <i class='fa fa-calendar'></i>"
                //+ " <small class='text-muted'>" + ConvertDateFormat(GetDateWithFormatMDY(item.CreationDate)) + "</small>"
                //+ " <small>" + GetShownDate(FormattedTodaysDate, GetDateWithFormatMDY(item.OpenDate)) + "</small>"
                + GetShownDate(FormattedTodaysDate, GetDateWithFormatMDY(item.OpenDate))
            //+ "</span>" + "</td>";
            pTableHTML += "     <td class='Client'>" + (item.BookingPartyName != 0 && item.BLType == constMasterBLType ? item.BookingPartyName : (item.ClientName == 0 ? "" : item.ClientName)) + "</td>";
            pTableHTML += "       <td class='Routing'>" + ($("#hDefaultUnEditableCompanyName").val() == "VEN"
                                                        ? (item.POLCode + " > " + item.PODCode + (item.AirlineCode == 0 ? "" : " - " + item.AirlineCode))
                                                        : (item.POLName + " > " + item.PODName)
                                                   ) + "</td>";
            pTableHTML += "       <td class='ShipmentType'>" + GetShipmentType(item.ShipmentType) + " " + item.RepBLTypeShown + "</td>";
            pTableHTML += "       <td class='BookingNumbers " + ($("#hDefaultUnEditableCompanyName").val() == "VEN" ? "hide" : "") + "'>" + item.BookingNumbers + "</td>";
            pTableHTML += "       <td class='MasterBL " + ($("#hDefaultUnEditableCompanyName").val() == "VEN" ? "hide" : "") + "'>" + (item.MasterBL == 0 ? "" : item.MasterBL) + "</td>";
            pTableHTML += "       <td class='MoveType " + ($("#hDefaultUnEditableCompanyName").val() == "VEN" ? "hide" : "") + "'>" + (item.MoveTypeCode == 0 ? "" : item.MoveTypeCode) + "</td>";
            pTableHTML += "       <td class='OperationStage' val='" + item.OperationStageID + "'>"
                //+ (item.OperationStageID == CancelledQuoteAndOperStageID
                //                    ? item.OperationStageName //cancelled operation
                //                    : (Date.prototype.compareDates(FormattedTodaysDate, GetDateWithFormatMDY(item.CloseDate)) <= 0
                //                        ? "CLOSED" //closed operation
                //                        : item.OperationStageName)
                //                    )
                                    + "</td>";

            pTableHTML += " </tr> ";
        }


    });


    // pTableHTML += "</tbody>";
    $("#tblOperation").append(pTableHTML);


    BindAllCheckboxonTable("tblOperation", "ID");
    CheckAllCheckbox("HeaderDeleteOperationID");
    ////to fill the controls after creating them in the previous loop
    //$.each(pTable, function (i, item) {
    //    $("#txtItemExchangeRate" + item.ID).val("1");

    //debugger;
    //FillListFromObject(null, 6/*pCodeOrName*/, null, "slBalanceCurrency" + item.ID, $('#slCurrency').val()
    //    , function () {
    //        ARAllocation_Row_SetExchangeRate(item.ID);
    //    });
    //});
    //HighlightText("#tblAllocationItem>tbody>tr", $("#txtSearchAllocationItems").val().trim());
}
function SetArrayOfOperationItems() {
    debugger;
    // var cobjItem = null;
    var arrayOfItems = new Array();

    $('#' + "tblOperation" + ' td').find('input[name="' + "Delete" + '"]:checked').each(function () {
        var objItem = new Object();
        objItem.VoucherID = $('#hID').val();
        objItem.OperationID = ($(this).attr('value'));
        arrayOfItems.push(objItem);

    });

    return arrayOfItems;
}
function Operation_Validate() {
    debugger;

    var strReturnedMessage = "";
    //check empty or Zero exchange rate AND AmountDue greater than Remaining Amount
    var listOfIDs = GetAllSelectedIDsAsStringWithTableNameAndNameAttr("tblOperation", "Delete")
    if (listOfIDs == "") {
        strReturnedMessage = "Please, check one operation at least . ";
    }
    return strReturnedMessage;
}
function Operation_Save(pPrint) {

    //*************
    swal({
        title: "Are you sure  ?",
        text: "You will Pay Cash for selected Operations ",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "OK  !",
        cancelButtonText: "NO",
        closeOnConfirm: true
    },
        function (isConfirm) {
            //swal("Poof! Your imaginary file has been deleted!", {
            //    icon: "success",
            //});

            if (isConfirm) {

                
                var strReturnedMessage = Operation_Validate();
                if (strReturnedMessage != "") {//there is a validation error
                    swal("Sorry", strReturnedMessage);
                    return false;
                }
                else { //Gather data to send to controller to save

                    //***************** Details **************
                    FadePageCover(true);
                    VoucherHeaderAndDetails_Save(false, function () {
                        debugger;
                        jQuery("#OperationModal").modal("hide");
                        jQuery("#VoucherModal").modal("hide");
                        console.log($("#hID").val());
                        //Voucher_Save();
                        console.log(SetArrayOfItems());

                        InsertUpdateFunction3("/api/Voucher/InsertOperationsPayment",
                                       SetArrayOfOperationItems()  //JSON.stringify(SetArrayOfItems())
                                       , false, null, function () {
                                           ClearAllModals();
                                           console.log("success InsertVoucherOperationPayment")
                                       });
                    });

                    FadePageCover(false);

                    //****************************************

                } // EOF else of strReturnedMessage == ""
            }
            else {
                console.log('refuse ');
            }
        }
    );
    //*************


}

/*------------------------------------------------------------------------------------*/



var _InvoiceTotalAmount = "";
var _SubAccountID = 0;
var _AccountID = 0;
var _Details = "";
var _DetailsOperations = "";

function FillAllocationData() {
    //var selectedInvoices = GetAllSelectedIDsAsStringWithNameAttr("nameCbInvoices");
    debugger;

    //if (selectedInvoices) {
    //    swal("Sorry", "Please, select at least one Store and one Item.");
    //}
    //else {

    var pSearchText = "";
    if (glbFormCalled == 20)
    {
        if ($("#txt-SearchOperation").val() != "") {
            pSearchText += " AND (";
            pSearchText += "  OperationCode LIKE N'%" + $("#txt-SearchOperation").val().trim() + "%'" + "\n";
            pSearchText += ")";
        }
        if ($("#txt-SearchInvoiceNo").val() != "") {
            pSearchText += " AND (";
            pSearchText += " SupplierInvoiceNo = N'" + $("#txt-SearchInvoiceNo").val().trim() + "' ";
            pSearchText += ")";
        }
    }
    else
        pSearchText= $("#txtSearchAllocationItems").val().trim() == "" ? "" : $("#txtSearchAllocationItems").val().trim().toUpperCase()

    ClearAllTableRows("tblAllocationItem"); //to quickly clear before calling controller
    ClearAllTableRows("tblPartnerBalance"); //to quickly clear before calling controller
    $('#lblTotalAllocation').html(' ');
    FadePageCover(true);
    var strFunctionName = "/api/Voucher/ARAllocation_FillAllocationData";
    var pParametersWithValues = {
        pPartnerID: $('#slPartner').val()
        , pPartnerTypeID: $('#slPartnerType').val()
        , pAllocationType: glbTransactionType
        , pSearchText: pSearchText
        , pCurrencyID: $('#slCurrency').val()
        , pIsCash: $("#cbIsCashInvoice").prop("checked")
    };
    //  $("#btn-searchAllocationItems").attr("onclick", 'ARAllocation_EditByDblClick(' + pPartnerID + ',' + pPartnerTypeID + ',"' + pPartnerName + '","' + pUnAllocatedAmount + '");');
    CallGETFunctionWithParameters(strFunctionName, pParametersWithValues
        , function (pData) {
            if (pData[0]) {
                if (pData[5] != null && pData[6] != null && pData[5] != 0 && pData[6] != 0) {
                    var pFormattedTodaysDate = getTodaysDateInddMMyyyyFormat();
                    var pInvoices = JSON.parse(pData[1]);
                    var pPartnerBalance = pData[2];
                    var pPayables = JSON.parse(pData[3]);
                    var ptxtAvailableBalance = pData[4];
                    var pvwPayablesAllocationsItems = JSON.parse(pData[7]);
                    //---
                    _AccountID = JSON.parse(pData[5]);
                    _SubAccountID = JSON.parse(pData[6]);
                    console.log(_AccountID + "" + _SubAccountID);
                    //---

                    //$("#lblAllocationShown").html(": " + pPartnerName);
                    $("#hAllocationPartnerID").val($('#slPartner').val());
                    $("#hAllocationPartnerTypeID").val($('#slPartnerType').val());
                    // $("#txtAllocationPartnerName").val(pPartnerName.split('(')[0]);
                    $("#txtAllocationDate").val(getTodaysDateInddMMyyyyFormat);
                    $("#txtAllocationAvailableBalance").val(ptxtAvailableBalance);
                    $("#txtAllocationRemainingBalance").val(pPartnerBalance);
                    ////ARAllocation_BindPartnerBalance(JSON.parse(pPartnerBalance));
                    if (glbFormCalled == 20)//Cash Issue Receipt
                    {
                        //if (glbTransactionType == constTransactionReceivableAllocation)
                        //    ARAllocation_BindAllocationItemsTableRows(pInvoices, pPartnerBalance);
                        //else if (glbTransactionType == constTransactionPayableAllocation)
                        //    ARAllocation_BindAllocationItemsTableRows(pPayables, pPartnerBalance);

                            ARAllocation_BindAllocationItemsTableRows(pvwPayablesAllocationsItems, pPartnerBalance);
                    }
                    else
                    {
                        if (glbTransactionType == constTransactionReceivableAllocation)
                            ARAllocation_BindAllocationItemsTableRows(pInvoices, pPartnerBalance);
                        else if (glbTransactionType == constTransactionPayableAllocation)
                            ARAllocation_BindAllocationItemsTableRows(pPayables, pPartnerBalance);
                    }
                    
                }
                else {

                    swal("Sorry", "The Partener Must Has Account && SubAccount", "warning")
                }
            }
            else
                swal("Sorry", "Connection failed, please try again.");
            FadePageCover(false);
        }
        , null);


    //  }

}

function ARAllocation_BindAllocationItemsTableRows(pTable, pPartnerBalance) {
    $("#tblAllocationItem").html("");
    debugger;
    var pCheckboxNameAttr = "AllocationItemID";
    var pTableHTML = "";
    pTableHTML += "<thead>";
    // pTableHTML += "     <tr>";
    pTableHTML += "<tr>";

    var IsCashInvoice = $("#cbIsCashInvoice").prop("checked")


    pTableHTML += '         <th id="AllocationItemHeaderID"' + (IsCashInvoice ? "" : "class='hide'") + '><input name="Select" id="CbAllocationItemID" type="checkbox" /></th>';
    pTableHTML += '         <th></th>';
    pTableHTML += '         <th>Partner</th>';
    if (glbFormCalled == 20)//Cash Issue Receipt
    {
         pTableHTML += '         <th>Charge</th>';
    }
    //else
       // pTableHTML += '         <th>Inv.No</th>';
    //  if (glbTransactionType == constTransactionPayableAllocation)                   
        // pTableHTML += '         <th>Charge</th>';

    pTableHTML += '         <th>Operation</th>';
    pTableHTML += '         <th>Invoice No</th>';   
    pTableHTML += '         <th>Status</th>';
    pTableHTML += '         <th>Total</th>';
    pTableHTML += '         <th>Cur</th>';
    pTableHTML += '         <th>Amount Paid</th>';
    pTableHTML += '         <th>PaidAmt</th>';
    pTableHTML += '         <th>Remaining</th>';
    // pTableHTML += '         <th>PayFrom</th>';
    // pTableHTML += '         <th>Ex.Rate</th>';
    pTableHTML += '         <th class="rounded-right hide"></th>';
    pTableHTML += "     </tr>";
    pTableHTML += "</thead>";
    pTableHTML += "<tbody>";
    console.log(pTable)
    debugger;
    if (glbFormCalled == 20)//Cash Issue Receipt
    {
        $.each(pTable, function (i, item) {
            if ((item.CostAmount.toFixed(4) - item.PaidAmount.toFixed(4)) > 0)
            {
                var pRemain = (item.CostAmount.toFixed(4) - item.PaidAmount.toFixed(4));

                pTableHTML += " <tr ID='" + item.ID + "'> ";
                // pTableHTML += " <tr ID='" + item.OperationID + "'> ";
                pTableHTML += "       <td class='ID ' > <input " + "  onclick='ARAllocation_PaidAll(" + item.ID + ',' + pRemain + ");' " + " name='" + pCheckboxNameAttr + "' type='checkbox' value='" + item.ID + "' " + (1 == 1 ? "" : "checked='checked'") + "></td> ";
               // pTableHTML += "       <td class='ID hide' > <input name='" + pCheckboxNameAttr + "' type='checkbox' value='" + item.ID + "' " + (1 == 1 ? "" : "checked='checked'") + "></td> ";
                //pTableHTML += "     <td class='Invoice' val='" + item.ChargeTypeID + "' style='width:300px;'>" + item.ChargeTypeName + "</td> ";
                pTableHTML += "       <td class='PartnerID' val='" + item.PartnerSupplierID + "'>" + item.PartnerSupplierName + "</td>"
                pTableHTML += "       <td class='PartnerTypeID hide' val='" + item.SupplierPartnerTypeID + "'>" + item.PartnerTypeCode + "</td>"
                //pTableHTML += "       <td class='InvoiceNumber'>" + (item.SupplierInvoiceNo == 0 ? "" : item.SupplierInvoiceNo) + "</td> ";
                pTableHTML += "       <td class='Charge'>" + item.ChargeTypeName + "</td> ";
                pTableHTML += "       <td class='Operation' val=" + item.OperationID + ">" + (item.OperationCode == 0 ? "" : item.OperationCode) + "</td> ";
                pTableHTML += "       <td class='InvoiceNumber'>" + (item.SupplierInvoiceNo == 0 ? "" : item.SupplierInvoiceNo) + "</td> ";
                pTableHTML += "       <td class='Status text-danger'>" + item.PayableStatus + "</td> ";
                pTableHTML += "       <td class='Amount'>" + item.CostAmount.toFixed(3) + "</td> ";
                pTableHTML += "       <td class='CurrencyID' val='" + item.CurrencyID + "'>" + item.CurrencyCode + "</td>"
                //pTableHTML += "       <td class='PayableAmountDue'> <input type='text' id='txtItemAmountDue" + item.ID + "' class='form-control controlStyle' onchange='ARAllocation_Row_CheckAmountDue(" + item.ID + ");' onkeypress='CheckValueIsDecimal(id);' onfocus='CheckValueIsDecimal(id);' onblur='CheckDecimalFormat(id);'  data-required='false' maxlength='10' placeholder='0.00' /> </td> ";
                pTableHTML += "       <td class='AmountDue'> <input style='width:60%;' type='text' id='txtItemAmountDue" + item.ID + "' class=' controlStyle' onchange='ARAllocation_ReCalculate();CalcTotal();' onkeypress='CheckValueIsDecimal(id);' onfocus='CheckValueIsDecimal(id);' onblur='CheckDecimalFormat(id);CalcTotal();'  data-required='false' maxlength='10' placeholder='0.00' /> </td> ";
                pTableHTML += "       <td class='PaidAmount'>" + item.PaidAmount.toFixed(4) + "</td> ";
                pTableHTML += "       <td class='RemainingAmount'>" + (item.CostAmount.toFixed(4) - item.PaidAmount.toFixed(4)) + "</td> ";
                //pTableHTML += "       <td class='BalanceCurrency '> <select disabled='disabled' value=" + $('#slCurrency').val() + " id='slBalanceCurrency" + item.ID + "' class='controlStyle' onfocus='DisableBackspaceKey(id);' onkeydown='DisableBackspaceKey(id);' onkeypress='DisableBackspaceKey(id);' onchange='ARAllocation_Row_SetExchangeRate(" + item.ID + ");' data-required='false'><option selected>" + $('#slCurrency option:selected').text() + "</option></select> </td> ";
                //pTableHTML += "       <td class='ExchangeRate'><input disabled='disabled' style='width:60%;' type='text' id='txtItemExchangeRate" + item.ID + "' class='InvoiceExchangeRate controlStyle' data-type='float' onkeypress='CheckValueIsDecimal(id);' onfocus='CheckValueIsDecimal(id);' onchange='ARAllocation_ReCalculate();' onblur='CheckDecimalFormat(id);'  data-required='true' maxlength='10' placeholder='0.00' " + ($("#hDefaultCurrencyID").val() == item.CurrencyID ? "disabled" : "") + " /> </td> ";

                pTableHTML += " </tr> ";
            }
          
        });
    }
    else
    {
        if (glbTransactionType == constTransactionReceivableAllocation) //Receivables
            $.each(pTable, function (i, item) {
                console.log(item);
                var pRemain = (item.Amount.toFixed(4) - item.PaidAmount.toFixed(4));

                pTableHTML += " <tr ID='" + item.ID + "'> ";
                pTableHTML += "       <td class='SelectInvoice " + (IsCashInvoice ? '' : ' hide') + "' > <input  name='Delete' id='" + pCheckboxNameAttr + "' type='checkbox' onchange='InvoiceCashChecked(" + item.ID + ");' value='" + item.ID + "' " + (1 == 1 ? "" : "checked='checked'") + "></td> ";
                pTableHTML += "       <td class='ID  " + (IsCashInvoice ? ' hide' : '') + "' > <input " + "  onclick='ARAllocation_PaidAll(" + item.ID + ',' + pRemain + ");' " + " name='" + pCheckboxNameAttr + "' type='checkbox' value='" + item.ID + "' " + (1 == 1 ? "" : "checked='checked'") + "></td> ";
                //pTableHTML += "     <td class='Invoice' val='" + item.ChargeTypeID + "' style='width:300px;'>" + item.ChargeTypeName + "</td> ";
                pTableHTML += "       <td class='PartnerID' val='" + item.PartnerID + "'>" + item.PartnerName + "</td>"
                pTableHTML += "       <td class='PartnerTypeID hide' val='" + item.PartnerTypeID + "'>" + item.PartnerTypeCode + "</td>"             
                pTableHTML += "       <td class='Charge hide'>" + "0" + "</td> ";
                pTableHTML += "       <td class='Operation' val='" + item.OperationID + "'>" + item.OperationCode + "</td> ";
                pTableHTML += "       <td class='InvoiceNumber'>" + item.ConcatenatedInvoiceNumber + "</td> ";
                pTableHTML += "       <td class='Status text-danger'>" + item.InvoiceStatus + "</td> ";
                pTableHTML += "       <td class='Amount'  id='Amount" + item.ID + "'>" + item.Amount.toFixed(4) + "</td> ";
                pTableHTML += "       <td class='CurrencyID' val='" + item.CurrencyID + "'>" + item.CurrencyCode + "</td>"
                //pTableHTML += "       <td class='InvoiceAmountDue'> <input type='text' id='txtItemAmountDue" + item.ID + "' class='form-control controlStyle' onchange='ARAllocation_Row_CheckAmountDue(" + item.ID + ");' onkeypress='CheckValueIsDecimal(id);' onfocus='CheckValueIsDecimal(id);' onblur='CheckDecimalFormat(id);'  data-required='false' maxlength='10' placeholder='0.00' /> </td> ";
                pTableHTML += "       <td class='AmountDue'> <input style='width:60%;' type='text' id='txtItemAmountDue" + item.ID + "' class=' controlStyle' onchange='ARAllocation_ReCalculate();CalcTotal();' onkeypress='CheckValueIsDecimal(id);' onfocus='CheckValueIsDecimal(id);' onblur='CheckDecimalFormat(id);CalcTotal();'  data-required='false' maxlength='10' placeholder='0.00' " + (IsCashInvoice ? "disabled='disabled'" : "") + "/> </td> ";
                pTableHTML += "       <td class='PaidAmount'>" + item.PaidAmount.toFixed(4) + "</td> ";
                pTableHTML += "       <td class='RemainingAmount'>" + (item.Amount.toFixed(4) - item.PaidAmount.toFixed(4)) + "</td> ";
                pTableHTML += "       <td class='BalanceCurrency hide'> <select disabled='disabled' value=" + $('#slCurrency').val() + " id='slBalanceCurrency" + item.ID + "' class='controlStyle' onfocus='DisableBackspaceKey(id);' onkeydown='DisableBackspaceKey(id);' onkeypress='DisableBackspaceKey(id);' onchange='ARAllocation_Row_SetExchangeRate(" + item.ID + ");' data-required='false'><option selected>" + $('#slCurrency option:selected').text() + "</option></select> </td> ";
                pTableHTML += "       <td class='ExchangeRate hide'><input disabled='disabled' style='width:60%;' type='text' id='txtItemExchangeRate" + item.ID + "' class='InvoiceExchangeRate controlStyle' data-type='float' onkeypress='CheckValueIsDecimal(id);' onfocus='CheckValueIsDecimal(id);' onchange='ARAllocation_ReCalculate();' onblur='CheckDecimalFormat(id);'  data-required='true' maxlength='10' placeholder='0.00' " + "" /*($("#hDefaultCurrencyID").val() == item.CurrencyID ? "disabled" : "")*/ + " /> </td> ";

                pTableHTML += " </tr> ";
            });
        else if (glbTransactionType == constTransactionPayableAllocation) //Payables
            $.each(pTable, function (i, item) {
                var pRemain = (item.CostAmount.toFixed(4) - item.PaidAmount.toFixed(4));

                pTableHTML += " <tr ID='" + item.ID + "'> ";
               // pTableHTML += "     <td class='ID hide' > <input name='" + pCheckboxNameAttr + "' type='checkbox' value='" + item.ID + "' " + (1 == 1 ? "" : "checked='checked'") + "></td> ";
                pTableHTML += "       <td class='ID ' > <input " + "  onclick='ARAllocation_PaidAll(" + item.ID + ',' + pRemain + ");' " + " name='" + pCheckboxNameAttr + "' type='checkbox' value='" + item.ID + "' " + (1 == 1 ? "" : "checked='checked'") + "></td> ";
                //pTableHTML += "     <td class='Invoice' val='" + item.ChargeTypeID + "' style='width:300px;'>" + item.ChargeTypeName + "</td> ";
                pTableHTML += "       <td class='PartnerID' val='" + item.PartnerSupplierID + "'>" + item.PartnerSupplierName + "</td>"
                pTableHTML += "       <td class='PartnerTypeID hide' val='" + item.SupplierPartnerTypeID + "'>" + item.PartnerTypeCode + "</td>"
                pTableHTML += "       <td class='InvoiceNumber'>" + (item.SupplierInvoiceNo == 0 ? "" : item.SupplierInvoiceNo) + "</td> ";
                pTableHTML += "       <td class='Charge'>" + item.ChargeTypeName + "</td> ";
                pTableHTML += "       <td class='Operation'>" + (item.OperationCode == 0 ? "" : item.OperationCode) + "</td> ";
                pTableHTML += "       <td class='Status text-danger'>" + item.PayableStatus + "</td> ";
                pTableHTML += "       <td class='Amount'>" + item.CostAmount.toFixed(3) + "</td> ";
                pTableHTML += "       <td class='CurrencyID' val='" + item.CurrencyID + "'>" + item.CurrencyCode + "</td>"
                //pTableHTML += "       <td class='PayableAmountDue'> <input type='text' id='txtItemAmountDue" + item.ID + "' class='form-control controlStyle' onchange='ARAllocation_Row_CheckAmountDue(" + item.ID + ");' onkeypress='CheckValueIsDecimal(id);' onfocus='CheckValueIsDecimal(id);' onblur='CheckDecimalFormat(id);'  data-required='false' maxlength='10' placeholder='0.00' /> </td> ";
                pTableHTML += "       <td class='AmountDue'> <input style='width:60%;' type='text' id='txtItemAmountDue" + item.ID + "' class=' controlStyle' onchange='ARAllocation_ReCalculate();CalcTotal();' onkeypress='CheckValueIsDecimal(id);' onfocus='CheckValueIsDecimal(id);' onblur='CheckDecimalFormat(id);CalcTotal();'  data-required='false' maxlength='10' placeholder='0.00' /> </td> ";
                pTableHTML += "       <td class='PaidAmount'>" + item.PaidAmount.toFixed(4) + "</td> ";
                pTableHTML += "       <td class='RemainingAmount'>" + (item.CostAmount.toFixed(4) - item.PaidAmount.toFixed(4)) + "</td> ";
                pTableHTML += "       <td class='BalanceCurrency '> <select disabled='disabled' value=" + $('#slCurrency').val() + " id='slBalanceCurrency" + item.ID + "' class='controlStyle' onfocus='DisableBackspaceKey(id);' onkeydown='DisableBackspaceKey(id);' onkeypress='DisableBackspaceKey(id);' onchange='ARAllocation_Row_SetExchangeRate(" + item.ID + ");' data-required='false'><option selected>" + $('#slCurrency option:selected').text() + "</option></select> </td> ";
                pTableHTML += "       <td class='ExchangeRate'><input disabled='disabled' style='width:60%;' type='text' id='txtItemExchangeRate" + item.ID + "' class='InvoiceExchangeRate controlStyle' data-type='float' onkeypress='CheckValueIsDecimal(id);' onfocus='CheckValueIsDecimal(id);' onchange='ARAllocation_ReCalculate();' onblur='CheckDecimalFormat(id);'  data-required='true' maxlength='10' placeholder='0.00' " + ($("#hDefaultCurrencyID").val() == item.CurrencyID ? "disabled" : "") + " /> </td> ";

                pTableHTML += " </tr> ";
            });
    }
    pTableHTML += "</tbody>";
    $("#tblAllocationItem").html(pTableHTML);
    //to fill the controls after creating them in the previous loop
    $.each(pTable, function (i, item) {
        $("#txtItemExchangeRate" + item.ID).val("1");

        //debugger;
        //FillListFromObject(null, 6/*pCodeOrName*/, null, "slBalanceCurrency" + item.ID, $('#slCurrency').val()
        //    , function () {
        //        ARAllocation_Row_SetExchangeRate(item.ID);
        //    });
    });

    debugger;
    BindAllCheckboxonTable("tblAllocationItem", "SelectInvoice", "CbAllocationItemID");
    CheckAllCheckboxInvoice("AllocationItemHeaderID");
    $('#lblTotalAllocation').html(' ');
    HighlightText("#tblAllocationItem>tbody>tr", $("#txtSearchAllocationItems").val().trim());
}

function ARAllocation_Row_SetExchangeRate(pRowID) {
    //debugger;
    //var pItemCurrencyID = $("#tblAllocationItem tr[ID=" + pRowID + "]").find("td.CurrencyID").attr("val");
    //if (pItemCurrencyID == $("#slBalanceCurrency" + pRowID).val()
    //    || (pItemCurrencyID != $("#hDefaultCurrencyID").val() && $("#slBalanceCurrency" + pRowID).val() != $("#hDefaultCurrencyID").val()) //this row condition is for paying from 2 diff. currencies which are not Default
    //)
    //    $("#txtItemExchangeRate" + pRowID).attr("disabled", "disabled");
    //else
    //    $("#txtItemExchangeRate" + pRowID).removeAttr("disabled");
    //var pExchangeRate = $("#hReadySlCurrencies option[value=" + $("#slBalanceCurrency" + pRowID).val() + "]").attr("MasterDataExchangeRate")
    //    / $("#hReadySlCurrencies option[value=" + pItemCurrencyID + "]").attr("MasterDataExchangeRate");
    //$("#txtItemExchangeRate" + pRowID).val(pExchangeRate.toFixed(5));
    //ARAllocation_ReCalculate();
}
function InvoiceCashChecked(pRowID) {
    debugger;
    // if ($("#cbIsCashInvoice").prop("checked"))
    if ($("#tblAllocationItem tbody tr[ID=" + pRowID + "]").find('input[name="Delete"]').is(':checked')) {
        var Amount = $("#tblAllocationItem tbody tr[ID=" + pRowID + "]").find("td.Amount").text();
        // $("#tblAllocationItem tbody tr[ID=" + pRowID + "]").find("td.AmountDue").text(Amount);
        $("#txtItemAmountDue" + pRowID).val(Amount);
    }
    else {
        $("#txtItemAmountDue" + pRowID).val("0.00");
    }

}
function ARAllocation_ReCalculate() {
    //debugger;
    //var pPartnerBalanceRows = $("#tblPartnerBalance tbody tr");
    //var pAllocationItem = $("#tblAllocationItem tbody tr");
    //for (var i = 0; i < pPartnerBalanceRows.length; i++) {
    //    var pBalanceCurrencyID = pPartnerBalanceRows[i].id;
    //    var pAvailableBalance = $("#tblPartnerBalance tbody tr[ID=" + pBalanceCurrencyID + "]").find("td.AvailableBalance").text();
    //    var decAmountDueSumOfBalance = 0.0;
    //    for (var j = 0; j < pAllocationItem.length; j++) {
    //        var pRowID = pAllocationItem[j].id;
    //        if ($("#txtItemAmountDue" + pRowID).val() != "" && $("#slBalanceCurrency" + pRowID).val() == pBalanceCurrencyID) {
    //            decAmountDueSumOfBalance += (parseFloat($("#txtItemAmountDue" + pRowID).val()) / parseFloat($("#txtItemExchangeRate" + pRowID).val()));
    //        }
    //    }
    //    $("#tblPartnerBalance tbody tr[ID=" + pBalanceCurrencyID + "]").find("td.AmountDue").text(decAmountDueSumOfBalance.toFixed(4));
    //    var decRemaining = 0;
    //    //if (glbTransactionType == constTransactionReceivableAllocation)
    //    decRemaining = parseFloat(pAvailableBalance) - parseFloat(decAmountDueSumOfBalance);
    //    //else if (glbTransactionType == constTransactionPayableAllocation)
    //    //    decRemaining = parseFloat(pAvailableBalance) + parseFloat(decAmountDueSumOfBalance);
    //    $("#tblPartnerBalance tbody tr[ID=" + pBalanceCurrencyID + "]").find("td.Remaining").text(decRemaining.toFixed(4));
    //    if (parseFloat(pAvailableBalance) < decAmountDueSumOfBalance) {
    //        $("#tblPartnerBalance tbody tr[ID=" + pBalanceCurrencyID + "]").find("td.AmountDue").removeClass("text-primary");
    //        $("#tblPartnerBalance tbody tr[ID=" + pBalanceCurrencyID + "]").find("td.AmountDue").addClass("text-danger");
    //        $("#tblPartnerBalance tbody tr[ID=" + pBalanceCurrencyID + "]").find("td.Remaining").removeClass("text-primary");
    //        $("#tblPartnerBalance tbody tr[ID=" + pBalanceCurrencyID + "]").find("td.Remaining").addClass("text-danger");
    //    }
    //    else {
    //        $("#tblPartnerBalance tbody tr[ID=" + pBalanceCurrencyID + "]").find("td.AmountDue").removeClass("text-danger");
    //        $("#tblPartnerBalance tbody tr[ID=" + pBalanceCurrencyID + "]").find("td.AmountDue").addClass("text-primary");
    //        $("#tblPartnerBalance tbody tr[ID=" + pBalanceCurrencyID + "]").find("td.Remaining").removeClass("text-danger");
    //        $("#tblPartnerBalance tbody tr[ID=" + pBalanceCurrencyID + "]").find("td.Remaining").addClass("text-primary");
    //    }
    //}
}
function ARAllocation_Save(pPrint) {
    debugger;
    //*************
    swal({
        title: "Are you sure  ?",
        text: "You will Pay Cash and Allocate selected Invoices ",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "OK , Cash and Allocate Invoices !",
        cancelButtonText: "NO",
        closeOnConfirm: true
    },
        function (isConfirm) {
            //swal("Poof! Your imaginary file has been deleted!", {
            //    icon: "success",
            //});

            if (isConfirm) {


                var strReturnedMessage = ARAllocation_Validate();
                if (strReturnedMessage != "") //there is a validation error
                {
                    swal("Sorry", strReturnedMessage);
                    if (($("#slAllocationCostCenter").val() == "0" || $("#slAllocationCostCenter").val() == "") && CostCenterType == "true")
                         $('#slAllocationCostCenter').addClass('validation-error');
                }
                   
                else { //Gather data to send to controller to save

                    //***************** Details **************
                    $("#slAccount").val(_AccountID);
                    $("#slSubAccount").html("<option value='" + _SubAccountID + "'>Partener SubAccount</option>")

                    var CostCenterID = $("#slAllocationCostCenter").val();

                    if(CostCenterID != 0 )
                       $("#slCostCenter").val(CostCenterID);

                    $("#txtValue").val(_InvoiceTotalAmount);
                    $("#slSubAccount").val(_SubAccountID);
                    if (glbFormCalled == 20)//Cash Issue Receipt
                        $("#txtDescription").val( (pDefaults.UnEditableCompanyName == "MAR" ? "" : "Cash for Allocate : " ) + _DetailsOperations);
                    else
                        $("#txtDescription").val(( pDefaults.UnEditableCompanyName == "MAR" ? " Invoices: " : "Cash for Allocate Invoices : ") + _Details);
                    $("#lblTotal").html("<span> : </span><span>" + parseFloat(_InvoiceTotalAmount).toFixed(4) + "</span>");
                    $("#lblTotalAfterTax").html("<span> : </span><span>" + parseFloat(_InvoiceTotalAmount).toFixed(4) + "</span>");


                    
                    if ($("#cbIsCashInvoice").prop("checked")) {

                        var InvoiceIDs = "";
                        $("#tblAllocationItem tbody tr").each(function (i, tr) {
                            var pRowID = $(tr).attr("id");
      
                            if ($("#txtItemAmountDue" + pRowID).val().trim() != "") {
                                InvoiceIDs = pRowID + ',';
                            }
                        });

                        

                        var arrayOfItems = new Array();
                        debugger;
                        CallGETFunctionWithParameters("/api/Voucher/GetInvoiceAccounts",
                            {
                                pInvoiceIDs: InvoiceIDs
                            }

                            , function (pData) {
                                if (pData[0]) {
                                    var InvoiceAccounts = JSON.parse(pData[0]);
                                   
                                    $.each(InvoiceAccounts, function (i, item) {
                                        debugger;

                                        maxDetailsIDInTable = (0 > maxDetailsIDInTable ? 0 : maxDetailsIDInTable);
                                            AppendRowtoTable("tblDetails",
                                                ("<tr ID='" + 0 + "' " + ">"
                                                        + "<td class='DetailsID'> <input " + (1 == 2 ? " disabled='disabled' " : " name='Delete' ") + " type='checkbox' value='" + 0 + "' /></td>"
                                                        + "<td class='Value' style='width:9%;'><input  tag='" + (item.SaleAmount) + "' type='text' style='width:100%;font-size:90%;'  id='txtValue" + maxDetailsIDInTable + "' class='form-control inputValue' data-type='number'  onkeypress='CheckValueIsDecimal(id);' onfocus='CheckValueIsDecimal(id);' onblur='CheckDecimalFormat(id);Voucher_CalculateTotal();' onchange='Details_SetIsRowChanged(id);Voucher_CalculateTotal();' data-required='false' value='" + (item.SaleAmount) + "' /> </td>"
                                                         + "<td class='Description' style='width:20%;'><input  tag='" + (item.Description) + "' type='text' style='width:100%;font-size:90%;text-transform:uppercase;' id='txtDescription" + maxDetailsIDInTable + "' class='form-control inputDescription'  onkeypress='DisableEnterKey(id);' onfocus='DisableEnterKey(id);' onchange='Details_SetIsRowChanged(id);' data-required='true' value='" + item.Description + "' /> </td>"
                                                         + "<td class='AccountID' style='width:20%;' val='" + (item.AccountID_Revenue) + "'>" + "<select  tag='" + (item.AccountID_Revenue) + "' id='slAccount" + maxDetailsIDInTable + "' style='width:100%;' class='form-control selectAccountID' onfocus='DisableBackspaceKey(id);' onkeydown='DisableBackspaceKey(id);' onkeypress='DisableBackspaceKey(id);' onchange='Details_SetIsRowChanged(id);Details_FillSlSubAccountInTable(this , \".SubAccountID\" ," + item.SubAccountID_Revenue + ");' data-required='true'>" + $('#slAccount').html() + "</select></td>"
                                                        + "<td class='SubAccountID' style='width:20%;' val='" + (item.SubAccountID_Revenue) + "'>" + "<select  tag='" + (item.SubAccountID_Revenue) + "'id='slSubAccount" + maxDetailsIDInTable + "' style='width:100%;' class='form-control selectSubAccountID' onfocus='DisableBackspaceKey(id);' onkeydown='DisableBackspaceKey(id);' onkeypress='DisableBackspaceKey(id);' onchange='Details_SetIsRowChanged(id);' data-required='false'>" + "<option value=0></option>" + "</select></td>"
                                                        + "<td class='CostCenterID' style='width:15%;' val='" + (item.CostCenterID_Revenue) + "'>" + "<select  tag='" + (item.CostCenterID_Revenue) + "' id='slCostCenter" + maxDetailsIDInTable + "' style='width:100%;' class='form-control selectcostcenterID' onfocus='DisableBackspaceKey(id);' onkeydown='DisableBackspaceKey(id);' onkeypress='DisableBackspaceKey(id);' onchange='Details_SetIsRowChanged(id);' data-required='false'>" + $('#slCostCenter').html() + "</select></td>"
                                                        + "<td class='Documented'> <input name='Documented' id=cbDocumented" + maxDetailsIDInTable + " type='checkbox'  " + (false ? " checked='checked' " : "") + " /></td>"
                                                        + "<td class='SelectedIDsToUpdate hide'> <input name='SelectedIDsToUpdate' id='SelectedIDsToUpdate" + maxDetailsIDInTable + "' type='checkbox' value='" + maxDetailsIDInTable + "' /></td></tr>"
                                                       ));
                                            if (i == InvoiceAccounts.length - 1)
                                                FillHTMLtblInputs("#tblDetails > tbody");

                                     


                                        //var objItem = new Object();
                                        //objItem.ID = 0;
                                        //objItem.VoucherID = 0;
                                        //objItem.Value = item.SaleAmount;
                                        //objItem.Description = item.Description;
                                        //objItem.AccountID = item.AccountID_Revenue;
                                        //objItem.SubAccountID = item.SubAccountID_Revenue;
                                        //objItem.CostCenterID = item.CostCenterID_Revenue;
                                        //objItem.IsDocumented = false;
                                        //objItem.InvoiceID = 0;

                                        //if (glbFormCalled == constVoucherCashIn) {
                                        //    objItem.VoucherType = 10;
                                        //}
                                        //else {
                                        //    objItem.VoucherType = 20;
                                        //}

                                        //arrayOfItems.push(objItem);

                                       
                                        if (item.AccountID_Revenue == 0) {
                                            var Mes = 'Fill  Account For Item ' + item.ItemName;
                                                swal('Excuse me',Mes, 'warning');
                                                return false;
                                            }
                                    
                                        if (i == InvoiceAccounts.length - 1)
                                             VoucherHeaderAndDetails_Save_Cash(false, arrayOfItems, function () {
                                    });
                                    });
                                }
                            }, null);

                       

                    }
                    else
                        {
                        debugger;
                        if (glbFormCalled == 10)//Cash Receiving Receipt
                    Details_Save(false, function () {

                        debugger;
                        var pAllocationItemsIDs = "";
                        var pAmounts = "";
                        var pTblAllocationItem = $("#tblAllocationItem tbody tr");
                        for (var i = 0; i < pTblAllocationItem.length; i++) {
                            //Fill Parameters from tbl controls here
                            var pRowID = pTblAllocationItem[i].id;
                            var tr = $("#tblAllocationItem tbody tr[ID=" + pRowID + "]");
                            if (parseFloat($("#txtItemAmountDue" + pRowID).val()) > 0 && $("#txtItemAmountDue" + pRowID).val() != "") {
                                pAllocationItemsIDs += (pAllocationItemsIDs == "" ? (pRowID) : ("," + pRowID));
                                pAmounts += (pAmounts == "" ? (parseFloat($("#txtItemAmountDue" + pRowID).val())) : ("," + parseFloat($("#txtItemAmountDue" + pRowID).val())));
                            }
                        }
                        if ($("#cbIsCashInvoice").prop("checked")) {

                            var pParametersWithValues = {
                                pAllocationItemsIDs: pAllocationItemsIDs
                            , pAmounts: pAmounts
                            };
                            debugger;
                            CallGETFunctionWithParameters("/api/A_ARAllocation/ARUpdateCashInvoicePaid", pParametersWithValues
    , function (pData) {
        if (pData[0]) {
            swal("Success", "Allocation done successfully.");

            // ARAllocation_Partners_LoadingWithPaging();
            jQuery("#InvModal").modal("hide");
            jQuery("#VoucherModal").modal("hide");
            console.log($("#hID").val());
            //Voucher_Save();
            console.log(SetArrayOfItems());
            debugger;
            InsertUpdateFunction3("/api/Voucher/InsertA_VoucherInvoicesPayment",
                SetArrayOfItems()  //JSON.stringify(SetArrayOfItems())
                , false, null, function () {
                    ClearAllModals();
                    console.log("success InsertVoucherInvoicesPayment")
                });

        }
        else {
            swal("Sorry", "Connection failed, please refresh and then try again.");
        }
        FadePageCover(false);
    }
    , null);

                        }
                        else {

                            $("#txtAllocationPartnerName").val($('#slPartner option:selected').text().split('(')[0]);
                            debugger;
                            var pAllocationItemsIDs = "";
                            var pInvoiceNumbers = "";
                            var pPartnerID = "";
                            var pPartnerTypeID = "";
                            var pCharge = ""
                            var pOperationCode = ""
                            var pAmounts = "";
                            var pItemCurrencyIDs = "";
                            var pBalanceCurrencyIDs = "";
                            var pItemCurrencyCodes = "";
                            var pBalanceCurrencyCodes = "";
                            var pExchangeRates = "";
                            var pBalCurLocalExRates = "";
                            var pInvCurLocalExRates = "";
                            var pTblAllocationItem = $("#tblAllocationItem tbody tr");
                            var pProfitAmounts = "";
                            var pLossAmounts = "";
                            for (var i = 0; i < pTblAllocationItem.length; i++) {
                                //Fill Parameters from tbl controls here
                                var pRowID = pTblAllocationItem[i].id;
                                var tr = $("#tblAllocationItem tbody tr[ID=" + pRowID + "]");
                                if (parseFloat($("#txtItemAmountDue" + pRowID).val()) > 0 && $("#txtItemAmountDue" + pRowID).val() != "") {
                                    pAllocationItemsIDs += (pAllocationItemsIDs == "" ? (pRowID) : ("," + pRowID));
                                    pInvoiceNumbers += (pInvoiceNumbers == "" ? (tr.find("td.InvoiceNumber").text() == "" ? "0" : tr.find("td.InvoiceNumber").text()) : ("," + (tr.find("td.InvoiceNumber").text() == "" ? "0" : tr.find("td.InvoiceNumber").text())));
                                    pPartnerID += (pPartnerID == "" ? (tr.find("td.PartnerID").attr("val") == "" ? "0" : tr.find("td.PartnerID").attr("val")) : ("," + (tr.find("td.PartnerID").attr("val") == "" ? "0" : tr.find("td.PartnerID").attr("val"))));
                                    pPartnerTypeID += (pPartnerTypeID == "" ? (tr.find("td.PartnerTypeID").attr("val") == "" ? "0" : tr.find("td.PartnerTypeID").attr("val")) : ("," + (tr.find("td.PartnerTypeID").attr("val") == "" ? "0" : tr.find("td.PartnerTypeID").attr("val"))));
                                    pCharge += (pCharge == "" ? (tr.find("td.Charge").text() == "" ? "0" : tr.find("td.Charge").text()) : ("," + (tr.find("td.Charge").text() == "" ? "0" : tr.find("td.Charge").text())));
                                    pOperationCode += (pOperationCode == "" ? (tr.find("td.Operation").text() == "" ? "0" : tr.find("td.Operation").text()) : ("," + (tr.find("td.Operation").text() == "" ? "0" : tr.find("td.Operation").text())));
                                    pAmounts += (pAmounts == "" ? (parseFloat($("#txtItemAmountDue" + pRowID).val())) : ("," + parseFloat($("#txtItemAmountDue" + pRowID).val())));
                                    pItemCurrencyIDs += (pItemCurrencyIDs == "" ? (tr.find("td.CurrencyID").attr("val")) : ("," + tr.find("td.CurrencyID").attr("val")));
                                    pBalanceCurrencyIDs += (pBalanceCurrencyIDs == "" ? ($("#slBalanceCurrency" + pRowID).val()) : ("," + $("#slBalanceCurrency" + pRowID).val()));
                                    pItemCurrencyCodes += (pItemCurrencyCodes == "" ? (tr.find("td.CurrencyID").text()) : ("," + tr.find("td.CurrencyID").text()));
                                    pBalanceCurrencyCodes += (pBalanceCurrencyCodes == "" ? ($("#slBalanceCurrency" + pRowID + " option:selected").text()) : ("," + $("#slBalanceCurrency" + pRowID + " option:selected").text()));
                                    pExchangeRates += (pExchangeRates == "" ? (parseFloat($("#txtItemExchangeRate" + pRowID).val())) : ("," + parseFloat($("#txtItemExchangeRate" + pRowID).val())));
                                    //if (tr.find("td.CurrencyID").attr("val") == $("#hDefaultCurrencyID").val())
                                    pBalCurLocalExRates += (pBalCurLocalExRates == "" ? ($("#hReadySlCurrencies option[value=" + $("#slBalanceCurrency" + pRowID).val() + "]").attr("MasterDataExchangeRate")) : ("," + $("#hReadySlCurrencies option[value=" + $("#slBalanceCurrency" + pRowID).val() + "]").attr("MasterDataExchangeRate")));
                                    //else
                                    //pBalCurLocalExRates += (pBalCurLocalExRates == "" ? (parseFloat($("#txtItemExchangeRate" + pRowID).val())) : ("," + parseFloat($("#txtItemExchangeRate" + pRowID).val())));
                                    pInvCurLocalExRates += (pInvCurLocalExRates == "" ? ($("#hReadySlCurrencies option[value=" + tr.find("td.CurrencyID").attr("val") + "]").attr("MasterDataExchangeRate")) : ("," + $("#hReadySlCurrencies option[value=" + tr.find("td.CurrencyID").attr("val") + "]").attr("MasterDataExchangeRate")));
                                    pProfitAmounts += (pProfitAmounts == "" ? (0) : ("," + 0 ));
                                    pLossAmounts += (pLossAmounts == "" ? (0) : ("," + 0));

                                }
                            }
                            var pParametersWithValues = {
                                pPartnerID: $("#hAllocationPartnerID").val()
                                , pPartnerTypeID: $("#hAllocationPartnerTypeID").val()
                                , pPartnerName: $("#txtAllocationPartnerName").val()
                                , pBranchID: $("#hUserBranchID").val()
                                , pAllocationItemsIDs: pAllocationItemsIDs
                                , pInvoiceNumbers: pInvoiceNumbers
                                , pPartnerIDList: pPartnerID
                                , pPartnerTypeIDList: pPartnerTypeID
                                , pCharge: pCharge
                                , pOperationCode: pOperationCode
                                , pAmounts: pAmounts
                                , pItemCurrencyIDs: pItemCurrencyIDs
                                , pBalanceCurrencyIDs: pItemCurrencyIDs
                                , pItemCurrencyCodes: pItemCurrencyCodes
                                , pBalanceCurrencyCodes: pBalanceCurrencyCodes
                                , pExchangeRates: pExchangeRates
                                , pBalCurLocalExRates: 1
                                , pInvCurLocalExRates: pInvCurLocalExRates
                                , pTransactionType: glbTransactionType
                                , pProfitAmounts: pProfitAmounts
                                , pLossAmounts: pLossAmounts
                                , pAccNoteID : 0
                            };
                            debugger;
                            if (pAllocationItemsIDs == "")
                                swal("Sorry", "No, allocations is assigned.");
                            else {
                                FadePageCover(true);
                                debugger;
                              //  CallGETFunctionWithParameters("/api/A_ARAllocation/ARAllocation_Save", pParametersWithValues
                                  CallPOSTFunctionWithParameters("/api/A_ARAllocation/ARAllocation_Save", pParametersWithValues
                                    , function (pData) {
                                        if (pData[0]) {
                                            debugger;
                                            swal("Success", "Allocation done successfully.");
                                            var pAccPartnerBalanceID = pData[1];
                                            // ARAllocation_Partners_LoadingWithPaging();
                                            jQuery("#InvModal").modal("hide");
                                            jQuery("#VoucherModal").modal("hide");
                                            console.log($("#hID").val());
                                            //Voucher_Save();
                                            //console.log(SetArrayOfItems());
                                            debugger
                                            InsertUpdateFunction3("/api/Voucher/InsertA_VoucherInvoicesPayment",
                                                SetArrayOfItems(pAccPartnerBalanceID, true)  //JSON.stringify(SetArrayOfItems())
                                                , false, null, function () {
                                                    ClearAllModals();
                                                    console.log("success InsertVoucherInvoicesPayment")
                                                });


                                        }
                                        else {
                                            swal("Sorry", "Connection failed, please refresh and then try again.");
                                        }
                                        FadePageCover(false);
                                    }
                                    , null);
                            } // EOF else pAllocationItemsIDs != ""



                        }

                    });
                        else if (glbFormCalled == 20)//Cash Issue Receipt
                            Details_Save(false, function () {

                                debugger;
                                var pAllocationItemsIDs = "";
                                var pAmounts = "";
                                var pTblAllocationItem = $("#tblAllocationItem tbody tr");
                                pAllocationItemsIDs = $("#tblAllocationItem tbody tr");
                                for (var i = 0; i < pTblAllocationItem.length; i++) {
                                    //Fill Parameters from tbl controls here
                                    var pRowID = pTblAllocationItem[i].id;
                                    var tr = $("#tblAllocationItem tbody tr[ID=" + pRowID + "]");
                                    if (parseFloat($("#txtItemAmountDue" + pRowID).val()) > 0 && $("#txtItemAmountDue" + pRowID).val() != "") {
                                        pAllocationItemsIDs += (pAllocationItemsIDs == "" ? (pRowID) : ("," + pRowID));
                                        pAmounts += (pAmounts == "" ? (parseFloat($("#txtItemAmountDue" + pRowID).val())) : ("," + parseFloat($("#txtItemAmountDue" + pRowID).val())));
                                    }
                                }
                                if ($("#cbIsCashInvoice").prop("checked")) {

                                    var pParametersWithValues = {
                                        pAllocationItemsIDs: pAllocationItemsIDs
                                    , pAmounts: pAmounts
                                    };
                                    debugger;
                                    CallGETFunctionWithParameters("/api/A_ARAllocation/ARUpdateCashInvoicePaid", pParametersWithValues
            , function (pData) {
                if (pData[0]) {
                    swal("Success", "Allocation done successfully.");

                    // ARAllocation_Partners_LoadingWithPaging();
                    jQuery("#InvModal").modal("hide");
                    jQuery("#VoucherModal").modal("hide");
                    console.log($("#hID").val());
                    //Voucher_Save();
                    console.log(SetArrayOfItems());
                    debugger;
                    InsertUpdateFunction3("/api/Voucher/InsertA_VoucherPayableAllocationPayment",
                        SetArrayOfItems()  //JSON.stringify(SetArrayOfItems())
                        , false, null, function () {
                            ClearAllModals();
                            console.log("success InsertVoucherInvoicesPayment")
                        });

                }
                else {
                    swal("Sorry", "Connection failed, please refresh and then try again.");
                }
                FadePageCover(false);
            }
            , null);

                                }
                                else {
                                    debugger;
                                    $("#txtAllocationPartnerName").val($('#slPartner option:selected').text().split('(')[0]);
                                    var pAllocationItemsIDs = "";
                                    var pInvoiceNumbers = "";
                                    var pPartnerID = "";
                                    var pPartnerTypeID = "";
                                    var pCharge = ""
                                    var pOperationCode = ""
                                    var pAmounts = "";
                                    var pItemCurrencyIDs = "";
                                    var pBalanceCurrencyIDs = "";
                                    var pItemCurrencyCodes = "";
                                    var pBalanceCurrencyCodes = "";
                                    var pExchangeRates = "";
                                    var pBalCurLocalExRates = "";
                                    var pInvCurLocalExRates = "";
                                    var pTblAllocationItem = $("#tblAllocationItem tbody tr");
                                    var pProfitAmounts = "";
                                    var pLossAmounts = "";
                                    for (var i = 0; i < pTblAllocationItem.length; i++) {
                                        //Fill Parameters from tbl controls here
                                        var pRowID = pTblAllocationItem[i].id;
                                        var tr = $("#tblAllocationItem tbody tr[ID=" + pRowID + "]");
                                        if (parseFloat($("#txtItemAmountDue" + pRowID).val()) > 0 && $("#txtItemAmountDue" + pRowID).val() != "") {
                                            pAllocationItemsIDs += (pAllocationItemsIDs == "" ? (pRowID) : ("," + pRowID));
                                            pInvoiceNumbers += (pInvoiceNumbers == "" ? (tr.find("td.InvoiceNumber").text() == "" ? "0" : tr.find("td.InvoiceNumber").text()) : ("," + (tr.find("td.InvoiceNumber").text() == "" ? "0" : tr.find("td.InvoiceNumber").text())));
                                            pPartnerID += (pPartnerID == "" ? (tr.find("td.PartnerID").attr("val") == "" ? "0" : tr.find("td.PartnerID").attr("val")) : ("," + (tr.find("td.PartnerID").attr("val") == "" ? "0" : tr.find("td.PartnerID").attr("val"))));
                                            pPartnerTypeID += (pPartnerTypeID == "" ? (tr.find("td.PartnerTypeID").attr("val") == "" ? "0" : tr.find("td.PartnerTypeID").attr("val")) : ("," + (tr.find("td.PartnerTypeID").attr("val") == "" ? "0" : tr.find("td.PartnerTypeID").attr("val"))));
                                            pCharge += (pCharge == "" ? (tr.find("td.Charge").text() == "" ? "0" : tr.find("td.Charge").text()) : ("," + (tr.find("td.Charge").text() == "" ? "0" : tr.find("td.Charge").text())));
                                            pOperationCode += (pOperationCode == "" ? (tr.find("td.Operation").text() == "" ? "0" : tr.find("td.Operation").text()) : ("," + (tr.find("td.Operation").text() == "" ? "0" : tr.find("td.Operation").text())));
                                            pAmounts += (pAmounts == "" ? (parseFloat($("#txtItemAmountDue" + pRowID).val())) : ("," + parseFloat($("#txtItemAmountDue" + pRowID).val())));
                                            pItemCurrencyIDs += (pItemCurrencyIDs == "" ? (tr.find("td.CurrencyID").attr("val")) : ("," + tr.find("td.CurrencyID").attr("val")));
                                            pBalanceCurrencyIDs += (pBalanceCurrencyIDs == "" ? ($("#slBalanceCurrency" + pRowID).val()) : ("," + $("#slBalanceCurrency" + pRowID).val()));
                                            pItemCurrencyCodes += (pItemCurrencyCodes == "" ? (tr.find("td.CurrencyID").text()) : ("," + tr.find("td.CurrencyID").text()));
                                            pBalanceCurrencyCodes += (pBalanceCurrencyCodes == "" ? ($("#slBalanceCurrency" + pRowID + " option:selected").text()) : ("," + $("#slBalanceCurrency" + pRowID + " option:selected").text()));
                                            pExchangeRates += (pExchangeRates == "" ? (parseFloat($("#txtItemExchangeRate" + pRowID).val())) : ("," + parseFloat($("#txtItemExchangeRate" + pRowID).val())));
                                            //if (tr.find("td.CurrencyID").attr("val") == $("#hDefaultCurrencyID").val())
                                            pBalCurLocalExRates += (pBalCurLocalExRates == "" ? ($("#hReadySlCurrencies option[value=" + $("#slBalanceCurrency" + pRowID).val() + "]").attr("MasterDataExchangeRate")) : ("," + $("#hReadySlCurrencies option[value=" + $("#slBalanceCurrency" + pRowID).val() + "]").attr("MasterDataExchangeRate")));
                                            //else
                                            //pBalCurLocalExRates += (pBalCurLocalExRates == "" ? (parseFloat($("#txtItemExchangeRate" + pRowID).val())) : ("," + parseFloat($("#txtItemExchangeRate" + pRowID).val())));
                                            pInvCurLocalExRates += (pInvCurLocalExRates == "" ? ($("#hReadySlCurrencies option[value=" + tr.find("td.CurrencyID").attr("val") + "]").attr("MasterDataExchangeRate")) : ("," + $("#hReadySlCurrencies option[value=" + tr.find("td.CurrencyID").attr("val") + "]").attr("MasterDataExchangeRate")));
                                            pProfitAmounts += (pProfitAmounts == "" ? (0) : ("," + 0));
                                            pLossAmounts += (pLossAmounts == "" ? (0) : ("," + 0));
                                        }
                                    }
                                    var pParametersWithValues = {
                                        pPartnerID: $("#hAllocationPartnerID").val()
                                        , pPartnerTypeID: $("#hAllocationPartnerTypeID").val()
                                        , pPartnerName: $("#txtAllocationPartnerName").val()
                                        , pBranchID: $("#hUserBranchID").val()
                                        , pAllocationItemsIDs: pAllocationItemsIDs
                                        , pInvoiceNumbers: pInvoiceNumbers
                                        , pPartnerIDList: pPartnerID
                                        , pPartnerTypeIDList: pPartnerTypeID
                                        , pCharge: pCharge
                                        , pOperationCode: pOperationCode
                                        , pAmounts: pAmounts
                                        , pItemCurrencyIDs: pItemCurrencyIDs
                                        , pBalanceCurrencyIDs: pItemCurrencyIDs
                                        , pItemCurrencyCodes: pItemCurrencyCodes
                                        , pBalanceCurrencyCodes: pBalanceCurrencyCodes
                                        , pExchangeRates: pExchangeRates
                                        , pBalCurLocalExRates: 1
                                        , pInvCurLocalExRates: pInvCurLocalExRates
                                        , pTransactionType: 80//glbTransactionType
                                        , pProfitAmounts: pProfitAmounts
                                        , pLossAmounts: pLossAmounts
                                        , pAccNoteID: 0
                                    };
                                    debugger;
                                    if (pAllocationItemsIDs == "")
                                        swal("Sorry", "No, allocations is assigned.");
                                    else {
                                        FadePageCover(true);
                                        debugger;
                                        // CallGETFunctionWithParameters("/api/A_ARAllocation/ARAllocation_Save", pParametersWithValues
                                        CallPOSTFunctionWithParameters("/api/A_ARAllocation/ARAllocation_Save", pParametersWithValues
                                            , function (pData) {
                                                if (pData[0]) {
                                                    debugger;
                                                    swal("Success", "Allocation done successfully.");
                                                    var pAccPartnerBalanceID = pData[1];
                                                    // ARAllocation_Partners_LoadingWithPaging();
                                                    jQuery("#InvModal").modal("hide");
                                                    jQuery("#VoucherModal").modal("hide");
                                                    console.log($("#hID").val());
                                                    //Voucher_Save();
                                                    //console.log(SetArrayOfItems());
                                                    debugger
                                                    InsertUpdateFunction3("/api/Voucher/InsertA_VoucherPayableAllocationPayment",
                                                        SetArrayOfItems(pAccPartnerBalanceID, true)  //JSON.stringify(SetArrayOfItems())
                                                        , false, null, function () {
                                                            ClearAllModals();
                                                            console.log("success InsertVoucherInvoicesPayment")
                                                        });


                                                }
                                                else {
                                                    swal("Sorry", "Connection failed, please refresh and then try again.");
                                                }
                                                FadePageCover(false);
                                            }
                                            , null);
                                    } // EOF else pAllocationItemsIDs != ""



                                }

                            });
                    }

                    //****************************************





                } // EOF else of strReturnedMessage == ""
            }
            else {
              
            }
        }
    );
    //*************


}




function ARAllocation_Validate() {
    debugger;
    _Details = "";
    _DetailsOperations = "";
    _InvoiceTotalAmount = 0;
    var strReturnedMessage = "";
    //check empty or Zero exchange rate AND AmountDue greater than Remaining Amount
    var pInvoice = $("#tblAllocationItem tbody tr");
    for (var i = 0; i < pInvoice.length; i++) {
        var pRowID = pInvoice[i].id;
        if (($("#txtItemAmountDue" + pRowID).val() != "" && $("#txtItemExchangeRate" + pRowID).val() == "")
            || ($("#txtItemAmountDue" + pRowID).val() != "" && parseFloat($("#txtItemExchangeRate" + pRowID).val()) == 0)
            || ($("#txtItemAmountDue" + pRowID).val() != "" && parseFloat($("#txtItemAmountDue" + pRowID).val()) > parseFloat($("#tblAllocationItem tbody tr[ID=" + pRowID + "]").find('td.RemainingAmount').text())) //check not to allocate more than remaining amount
        ) {
            strReturnedMessage = "Please, check amount and exchange rate for invoice " + $("#tblAllocationItem tbody tr[ID=" + pRowID + "]").find('td.InvoiceNumber').text() + ".";
        }
        //----------------------------------------------------------------------
        if ($("#txtItemAmountDue" + pRowID).val() != "") {
            _InvoiceTotalAmount += $("#txtItemAmountDue" + pRowID).val() * 1

        }
        //-----------------------------------------------------------------------


    } // of For Loop
    //check each balance amount is not exceeded in allocation
    if (strReturnedMessage == "") {
        var pPartnerBalance = $("#tblPartnerBalance tbody tr");
        for (var i = 0; i < pPartnerBalance.length; i++) {
            var pBalanceCurrencyID = pPartnerBalance[i].id;
            var pAvailableCurrencyBalance = $("#tblPartnerBalance tbody tr[ID=" + pBalanceCurrencyID + "]").find("td.AvailableBalance").text();
            var pAmountDueCurrencyBalance = $("#tblPartnerBalance tbody tr[ID=" + pBalanceCurrencyID + "]").find("td.AmountDue").text();
            if (parseFloat(pAvailableCurrencyBalance) < parseFloat(pAmountDueCurrencyBalance))
                strReturnedMessage = "Amount to be paid from the " + $("#tblPartnerBalance tbody tr[ID=" + pBalanceCurrencyID + "]").find("td.CurrencyID").text() + " balance exceeds the available limit.";
        }
    }
    debugger;
        if ($("#txt-SearchInvoiceNo").val() != "") {
        var TotalInvoice = 0.0;
        var TotalPaid = 0.0;
        var TotalRemaining = 0.0;

        $("#tblAllocationItem tbody tr").each(function (i, tr) {
            var pRowID = $(tr).attr("id");
            if($("#txt-SearchInvoiceNo").val() ==  $(tr).find("td.InvoiceNumber").html())
            {
                TotalInvoice += parseFloat($(tr).find("td.Amount").html());
                TotalPaid += parseFloat($("#txtItemAmountDue" + pRowID).val());
                TotalRemaining += parseFloat($(tr).find("td.RemainingAmount").html()) -parseFloat($("#txtItemAmountDue" +pRowID).val());
            }


            });
        _Details += ($("#txt-SearchInvoiceNo").val()
                            + " Total : " +TotalInvoice
                            + " [ " + " Paid " + TotalPaid
                            + " Remaining Amount: " + (TotalRemaining) * 1 + " " + " ] " + ",")

        _DetailsOperations += ($("#txt-SearchInvoiceNo").val()
                            + " Total : " + TotalInvoice
                            + " [ " + " Paid " + TotalPaid
                            + " Remaining Amount: " + (TotalRemaining) * 1 + " " + " ] " + ",")

        }
    else
        {
            $("#tblAllocationItem tbody tr").each(function (i, tr) {
                var pRowID = $(tr).attr("id");
                if ($("#txtItemAmountDue" + pRowID).val() != "") {
                    // _DetailsOperations += ($(tr).find("td.Operation").html() + " [ " + $("#txtItemAmountDue" + pRowID).val() * 1 + " " + $(tr).find("td.CurrencyID").html() + " ] ,")
                    _DetailsOperations += " Invoice No " + ($(tr).find("td.InvoiceNumber").html()
                        + " Total : " + $(tr).find("td.Amount").html()
                        + " [ " + " Paid "
                        + $("#txtItemAmountDue" + pRowID).val() * 1 + " " + $(tr).find("td.CurrencyID").html()
                        + " Remaining Amount: "
                        + (parseFloat($(tr).find("td.RemainingAmount").html()) - parseFloat($("#txtItemAmountDue" + pRowID).val())) * 1
                        + " " + " ] " + ",")
                    if ($("#hDefaultUnEditableCompanyName").val() == "SAF") {
                        _Details += ($(tr).find("td.InvoiceNumber").html()
                          + " Total : " + $(tr).find("td.Amount").html()
                          + " [ " + " Paid " + $("#txtItemAmountDue" + pRowID).val() * 1 + " " + $(tr).find("td.CurrencyID").html()
                          + " Remaining Amount: " + (parseFloat($(tr).find("td.RemainingAmount").html()) - parseFloat($("#txtItemAmountDue" + pRowID).val())) * 1
                          + " " + " ] " + ",")
                    }
                    else
                        _Details += ($(tr).find("td.InvoiceNumber").html() + " [ " + $("#txtItemAmountDue" + pRowID).val() * 1 + " " + $(tr).find("td.CurrencyID").html() + " ] ,")
                }

            });
        }

        if (($("#slAllocationCostCenter").val() == "0" || $("#slAllocationCostCenter").val() == "") && CostCenterType == "true") {
            strReturnedMessage = 'Choose Cost Center';
        } 
  

    return strReturnedMessage;
}

function SetArrayOfItems(pAccPartnerBalanceID, check) {
    // var cobjItem = null;
    var arrayOfItems = new Array();
    var ind = -1;
    $("#tblAllocationItem tbody tr").each(function (i, tr) {
        var pRowID = $(tr).attr("id");
        debugger;
        if ($("#txtItemAmountDue" + pRowID).val().trim() != "") {
            var objItem = new Object();
            objItem.VoucherID = $('#hID').val();
            objItem.InvoiceID = pRowID;
            objItem.DueAmount = $("#txtItemAmountDue" + pRowID).val();
            objItem.CurrencyID = $(tr).find("td.CurrencyID").attr("val");
            objItem.VoucherTypeID = glbFormCalled;
            if (glbFormCalled == 20) objItem.OperationID = $(tr).find("td.Operation").attr("val");
            ind++;
            if (check == true) {
                if (glbFormCalled == 20)
                    objItem.A_PayablesAllocationID = pAccPartnerBalanceID[ind];
                else
                    objItem.AccPartnerBalanceID = pAccPartnerBalanceID[ind];
            }
            if (objItem.DueAmount > 0)
                arrayOfItems.push(objItem);
        }
    });
    return arrayOfItems;
}

function SetArrayOfItemsPayableAllocation(pAccPartnerBalanceID, check) {
    // var cobjItem = null;
    var arrayOfItems = new Array();
    var ind = -1;
    $("#tblAllocationItem tbody tr").each(function (i, tr) {
        var pRowID = $(tr).attr("id");
        debugger;
        if ($("#txtItemAmountDue" + pRowID).val().trim() != "") {
            var objItem = new Object();
            objItem.VoucherID = $('#hID').val();
            objItem.OperationID = pRowID;
            objItem.DueAmount = $("#txtItemAmountDue" + pRowID).val();
            objItem.CurrencyID = $(tr).find("td.CurrencyID").attr("val");
            objItem.VoucherTypeID = glbFormCalled;
            ind++;
            if (check == true) {
                objItem.A_PayablesAllocationID = pAccPartnerBalanceID[ind];
            }
            if (objItem.DueAmount > 0)
                arrayOfItems.push(objItem);
        }
    });
    return arrayOfItems;
}
function InsertUpdateFunction3(pFunctionName, pParametersWithValues, pSaveandAddNew, pModalID, callback) {
    debugger;
    console.log(pParametersWithValues);
    if (1 == 1) {
        FadePageCover(true);
        $.ajax({
            type: "POST",
            url: strServerURL + pFunctionName,
            data: { "": JSON.stringify(pParametersWithValues) },
            //contentType:"application/json; charset=utf-8",
            beforeSend: function () { },
            success: function (data) {
                debugger;
                console.log("000" + data[0]);
                console.log("111" + data[1]);
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
                    else //data[0] = false
                        //swal(strSorry, strUniqueFailInsertUpdateMessage, "warning");
                        swal(strSorry, data[1]);
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

function Voucher_cbIsCash() {
    debugger;
    if ($("#cbIsCashInvoice").prop("checked")) {
        $("#slPartner").attr('disabled', 'disabled');
        $("#slPartnerType").attr('disabled', 'disabled');
        $("#btn-filterPartnerType").attr('disabled', 'disabled');
        $("#btn-filterPartner").attr('disabled', 'disabled');

        ClearAllTableRows("tblAllocationItem"); //to quickly clear before calling controller
        ClearAllTableRows("tblPartnerBalance"); //to quickly clear before calling controller
        $('#lblTotalAllocation').html(' ');

        _AccountID = $("#hCashAccountID").val();
        _SubAccountID = $("#hCashSubAccountID").val();

        if (_AccountID != 0 && _SubAccountID != 0 && _AccountID != null && _SubAccountID != null) {
            FadePageCover(true);
            var strFunctionName = "/api/Voucher/ARAllocation_FillAllocationData";
            var pParametersWithValues = {
                pPartnerID: (($('#slPartner').val() == null || $('#slPartner').val() == '') ? 0 : $('#slPartner').val())
                , pPartnerTypeID: (($('#slPartnerType').val() == null || $('#slPartnerType').val() == '') ? 0 : $('#slPartnerType').val())
                , pAllocationType: glbTransactionType
                , pSearchText: $("#txtSearchAllocationItems").val().trim() == "" ? "" : $("#txtSearchAllocationItems").val().trim().toUpperCase()
                , pCurrencyID: $('#slCurrency').val()
                , pIsCash: $("#cbIsCashInvoice").prop("checked")
            };
            //  $("#btn-searchAllocationItems").attr("onclick", 'ARAllocation_EditByDblClick(' + pPartnerID + ',' + pPartnerTypeID + ',"' + pPartnerName + '","' + pUnAllocatedAmount + '");');
            CallGETFunctionWithParameters(strFunctionName, pParametersWithValues
                , function (pData) {
                    if (pData[0]) {
                        if (pData[5] != null && pData[6] != null && pData[5] != 0 && pData[6] != 0) {
                            var pFormattedTodaysDate = getTodaysDateInddMMyyyyFormat();
                            var pInvoices = JSON.parse(pData[1]);
                            var pPartnerBalance = pData[2];
                            var pPayables = JSON.parse(pData[3]);
                            var ptxtAvailableBalance = pData[4];
                            //---

                            _AccountID = $("#hCashAccountID").val();
                            _SubAccountID = $("#hCashSubAccountID").val();

                            //_AccountID = JSON.parse(pData[5]);
                            //_SubAccountID = JSON.parse(pData[6]);

                            console.log(_AccountID + "" + _SubAccountID);
                            //---

                            //$("#lblAllocationShown").html(": " + pPartnerName);
                            $("#hAllocationPartnerID").val($('#slPartner').val());
                            $("#hAllocationPartnerTypeID").val($('#slPartnerType').val());
                            // $("#txtAllocationPartnerName").val(pPartnerName.split('(')[0]);
                            $("#txtAllocationDate").val(getTodaysDateInddMMyyyyFormat);
                            $("#txtAllocationAvailableBalance").val(ptxtAvailableBalance);
                            $("#txtAllocationRemainingBalance").val(pPartnerBalance);
                            ////ARAllocation_BindPartnerBalance(JSON.parse(pPartnerBalance));
                            if (glbTransactionType == constTransactionReceivableAllocation)
                                ARAllocation_BindAllocationItemsTableRows(pInvoices, pPartnerBalance);
                            else if (glbTransactionType == constTransactionPayableAllocation)
                                ARAllocation_BindAllocationItemsTableRows(pPayables, pPartnerBalance);



                        }
                        else {

                            swal("Sorry", "The Partener Must Has Account && SubAccount", "warning")
                        }
                    }
                    else
                        swal("Sorry", "Connection failed, please try again.");
                    FadePageCover(false);
                }
                , null);
        }
        else {

            swal("Sorry", "Cash Account Must Be Linked First", "warning")
        }

    }
    else {

        $("#slPartner").removeAttr("disabled");
        $("#slPartnerType").removeAttr("disabled");
        $("#btn-filterPartnerType").removeAttr("disabled");
        $("#btn-filterPartner").removeAttr("disabled");

        ClearAllTableRows("tblAllocationItem"); //to quickly clear before calling controller
        ClearAllTableRows("tblPartnerBalance"); //to quickly clear before calling controller
        $('#lblTotalAllocation').html(' ');
    }
}
function CheckAllCheckboxInvoice(pCheckBoxID) {
    debugger;
    $("#" + pCheckBoxID).click(function (e) {

        var table = $(e.target).closest('table');

        $('td input[name="Delete"]:checkbox', table).prop('checked',
        $(this).find('input:checkbox').is(':checked'));

        var IsChecked = $(this).find('input:checkbox').is(':checked');
        $(table).find('tbody > tr').each(function () {

            $("#txtItemAmountDue" + $(this).attr('id')).val(IsChecked ? $(this).find('td.Amount').text() : "0.00");


        });

    });
}
function ClearAllModals() {
    debugger;
    ClearAll("#VoucherModal");

    ////*****
    //ClearAllTableRows("tblDetails");
    //Details_ClearAllControls()
    ////*****
    $('#slPartner').html('');
    $('#txtOperationName').val('');
    $('#slOfficialAllocationPartner').html('');

    $('#lblTotalOfficialAllocation').html('');
    

    ClearAllTableRows("tblPartner");

    ClearAllTableRows("tblOperation");
    ClearAllTableRows("tblAllocationItem"); //to quickly clear before calling controller
    ClearAllTableRows("tblPartnerBalance");
    ClearAllTableRows("tblOfficialAllocationItem");

    $('#lblTotalAllocation').html(' ');
}
function SubAccount_Changed(pSubAccount) {
    debugger;
    if ( //glbFormCalled == constVoucherCashOut && 
    $("#hDefaultUnEditableCompanyName").val() == "SAF") {
      
        var Selected = $(pSubAccount).children("option:selected").text().split(':')[0].trim();
        //if (Selected == '<--Select-->')
        //    Selected = '';
        if (Selected != '<--Select-->' && Selected != '' )
        {
            $("#txtChargedPerson").prop("disabled", "disabled");

            $('#txtChargedPerson').val(
               // $('#slSubAccount option:selected').text().split(':')[0].trim()    
                   Selected
                );
        }
       
    }
    
}
function SubAccount_ChangedSalesMan(pRowID) {
    if (count == 0) {
        var Selected = $("#slSubAccount" + pRowID).val();
        $.ajax({
            type: "GET",
            url: strServerURL + "/api/Voucher/GetSalesIDBySupAccountID",
            data: { pSupAccountID: Selected },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (d) {

                $('#slSalesMan').val(d[0] == "" ? "0" : d[0]);




            },
            error: function (jqXHR, exception) {
                debugger;
                swal("Sorry", "Please, press Ctrl+Shft+R, and if the problem persists contact your technical support! This is print region !", "");
                FadePageCover(false);


            }
        });
    }
   
}
function Details_NewRow() {
    check = 0;
    count = 0;
    debugger;
    ++maxDetailsIDInTable;
    // var copyControlsText = " class='btn btn-xs btn-rounded btn-info float-right' > <i class='fa fa-copy' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'></span>";
    var FormattedTodaysDate = getTodaysDateInddMMyyyyFormat();
    var dateValidTo = Date.prototype.addDays(ConvertDateFormat(FormattedTodaysDate), 30);

    //"<tr ID='" + item.ID + "' "
    //            + (" ondblclick='Details_FillControls(" + item.ID + ");' ")
    //            + ">"
    //                + "<td class='DetailsID'> <input " + (1 == 2 ? " disabled='disabled' " : " name='Delete' ") + " type='checkbox' value='" + item.ID + "' /></td>"
    //                + "<td class='Value'>" + item.Value + "</td>"
    //                + "<td class='Description'>" + (item.Description == 0 ? "" : item.Description) + "</td>"
    //                + "<td class='AccountID hide'>" + item.AccountID + "</td>"
    //                + "<td class='AccountName'>" + (item.Account_Name + " - " + item.Account_Number) + "</td>"
    //                + "<td class='SubAccountID hide'>" + item.SubAccountID + "</td>"
    //                + "<td class='SubAccountName'>" + (item.SubAccountID == 0 ? "" : (item.SubAccount_Name + " - " + item.SubAccount_Number)) + "</td>"
    //                + "<td class='CostCenterID hide'>" + item.CostCenterID + "</td>"
    //                + "<td class='CostCenterName'>" + (item.CostCenterID == 0 ? "" : (item.CostCenterName + " - " + item.CostCenterNumber)) + "</td>"
    //                + "<td class='Documented'> <input id=cbDocumented" + item.ID + " type='checkbox' disabled='disabled' " + (item.IsDocumented ? " checked='checked' " : "") + " /></td>"
    ////+ "<td class='InvoiceNo " + (glbFormCalled == constVoucherCashIn ? "" : "hide") + "'>" + (item.InvoiceID == 0 ? "" : item.InvoiceNo) + "</td>"
    //                + "<td class='hide'><a href='#DetailsModal' data-toggle='modal' onclick='Details_FillControls(" + item.ID + ");' " + editControlsText + "</a></td></tr>"

    var tr = "";
    if ($("[id$='hf_ChangeLanguage']").val() == "ar") {
        tr += "<tr ID='" + 0 + "' isdeleted='0' tag='item'   counter='" + (maxDetailsIDInTable) + "' value='" + 0 + "'>"
        tr += "     <td class='IsInsert hide'> <input type='checkbox' id='IsInsert" + maxDetailsIDInTable + "' checked='checked' /></td>";
        tr += "     <td class='SelectedIDsToUpdate hide'> <input name='SelectedIDsToUpdate'  id='SelectedIDsToUpdate" + maxDetailsIDInTable + "' type='checkbox' value='" + maxDetailsIDInTable + "' /></td>";

        tr += "      <td class='Documented' style='width:1%;'><input id='cbDocumented" + maxDetailsIDInTable + "' name='Documented'  class='cbIsDocumented' type='checkbox' value='' ></td>";

        tr += "     <td class='OperationID' style='width:15%;' val=''><select id='slOperation" + maxDetailsIDInTable + "' style='width:100%;' class='controlStyle selectOperation' onchange='Details_HouseAndCertificateChangedByOperation(" + maxDetailsIDInTable + ");'  onfocus='DisableBackspaceKey(id);' onkeydown='DisableBackspaceKey(id);' onkeypress='DisableBackspaceKey(id);'   data-required='true'>" + "<option value=0></option>" + "</select></td>";
        tr += "     <td class='OperationSearch' style='width:3%; padding:6px 0px 6px 0px;' val=''><button id='btnOperationSearch' class='btn btn-sm btn-lightblue m-t-xmd' type='button' onclick='ChangeOperation(" + maxDetailsIDInTable + ");'><i class='fa fa-search'></i></button> </td>"
        tr += "     <td class='HouseID' disabled style='width:7%;' val=''><select id='slHouse" + maxDetailsIDInTable + "' style='width:100%;' class='controlStyle selectHouseNumber' onfocus='DisableBackspaceKey(id);' onkeydown='DisableBackspaceKey(id);' onkeypress='DisableBackspaceKey(id);'  onchange='Details_ChangedByHouseAndCertificateAndTrucingOrder(" + maxDetailsIDInTable + ',' + 1 + ");'  data-required='false'>" + "<option value=0></option>" + "</select></td>";
        tr += "     <td class='TruckingOrderID' disabled style='width:7%;' val=''><select id='slTruckingOrder" + maxDetailsIDInTable + "' style='width:100%;' class='controlStyle selectTruckingOrder' onfocus='DisableBackspaceKey(id);' onkeydown='DisableBackspaceKey(id);' onkeypress='DisableBackspaceKey(id);'  onchange='Details_ChangedByHouseAndCertificateAndTrucingOrder(" + maxDetailsIDInTable + ',' + 2 + ");'  data-required='false'>" + "<option value=0></option>" + "</select></td>";

        tr += "     <td class='BranchID' disabled style='width:7%;' val=''><select id='slBranch" + maxDetailsIDInTable + "' style='width:100%;' class='controlStyle selectBranch' onfocus='DisableBackspaceKey(id);' onkeydown='DisableBackspaceKey(id);' onkeypress='DisableBackspaceKey(id);'  onchange='GetSuppliers_Charges(" + maxDetailsIDInTable + ");Details_ChangedByHouseAndCertificateAndTrucingOrder(" + maxDetailsIDInTable + ',' + 3 + ");'  data-required='false'>" + "<option value=0></option>" + "</select></td>";


        tr += "     <td class='CostCenterID' style='width:15%;' val=''><select id='slCostCenter" + maxDetailsIDInTable + "' style='width:150px;' class='controlStyle selectcostcenterID' onfocus='DisableBackspaceKey(id);' onkeydown='DisableBackspaceKey(id);' onkeypress='DisableBackspaceKey(id);' onchange='Details_SetIsRowChanged(id);' data-required='false'>" + "<option value=0></option>" + "</select></td>";
        tr += "     <td class='SubAccountID' style='width:20%;' val=''><select id='slSubAccount" + maxDetailsIDInTable + "' style='width:300px;' class='controlStyle selectSubAccountID' onfocus='DisableBackspaceKey(id);' onkeydown='DisableBackspaceKey(id);' onkeypress='DisableBackspaceKey(id);' onchange='Details_SetIsRowChanged(id); SubAccount_Changed(this);SubAccount_ChangedSalesMan(" + maxDetailsIDInTable + ");' data-required='false'>" + "<option value=0></option>" + "</select></td>";
        tr += "     <td class='AccountID' style='width:20%;' val=''><select id='slAccount" + maxDetailsIDInTable + "' style='width:300px;' class='controlStyle selectAccountID' onfocus='DisableBackspaceKey(id);' onkeydown='DisableBackspaceKey(id);' onkeypress='DisableBackspaceKey(id);' onchange='Details_SetIsRowChanged(id); Details_AccountChanged(" + maxDetailsIDInTable + ");  ' data-required='true'>" + "<option value=0></option>" + "</select></td>";
        tr += "     <td class='Description' style='width:20%;'><input type='text' style='width:200px;font-size:90%;text-transform:uppercase;' id='txtDescription" + maxDetailsIDInTable + "' class='controlStyle inputDescription'  onkeypress='DisableEnterKey(id);' onfocus='DisableEnterKey(id);' onchange='Details_SetIsRowChanged(id);' data-required='true' value='" + ($("#txtDescription" + (maxDetailsIDInTable - 1)).val() == undefined ? "" : $("#txtDescription" + (maxDetailsIDInTable - 1)).val()) + "' /> </td>";

        tr += "     <td class='Value' style='width:9%;'><input type='text' style='width:80px;font-size:90%;'  id='txtValue" + maxDetailsIDInTable + "' class='controlStyle inputValue' data-type='number'  onkeypress='CheckValueIsDecimal(id);' onfocus='CheckValueIsDecimal(id);' onblur='CheckDecimalFormat(id);Voucher_CalculateTotal();' onchange='Details_SetIsRowChanged(id);Voucher_CalculateTotal();' data-required='true' value='' /> </td>";
        tr += "     <td class='DetailsID' style='width:2%;'><input " + (1 == 2 ? " disabled='disabled' " : " name='Delete' ") + " type='checkbox' value='" + maxDetailsIDInTable + "' /></td>";

        tr += "     <td class='hide'>"
                            //+ "<a href='#'  onclick='Pricing_CopyRow(" + maxDetailsIDInTable + ");' " + copyControlsText + "</a>"
                      + "</td>";
        tr += "</tr>";
    }
    else {
        tr += "<tr ID='" + 0 + "' isdeleted='0' tag='item'   counter='" + (maxDetailsIDInTable) + "' value='" + 0 + "'>"
        tr += "     <td class='IsInsert hide'> <input type='checkbox' id='IsInsert" + maxDetailsIDInTable + "' checked='checked' /></td>";
        tr += "     <td class='DetailsID' style='width:2%;'><input " + (1 == 2 ? " disabled='disabled' " : " name='Delete' ") + " type='checkbox' value='" + maxDetailsIDInTable + "' /></td>";
        tr += "     <td class='Value' style='width:9%;'><input type='text' style='width:80px;font-size:90%;'  id='txtValue" + maxDetailsIDInTable + "' class='controlStyle inputValue' data-type='number'  onkeypress='CheckValueIsDecimal(id);' onfocus='CheckValueIsDecimal(id);' onblur='CheckDecimalFormat(id);Voucher_CalculateTotal();' onchange='Details_SetIsRowChanged(id);Voucher_CalculateTotal();' data-required='true' value='' /> </td>";
        tr += "     <td class='Description' style='width:20%;'><input type='text' style='width:200px;font-size:90%;text-transform:uppercase;' id='txtDescription" + maxDetailsIDInTable + "' class='controlStyle inputDescription'  onkeypress='DisableEnterKey(id);' onfocus='DisableEnterKey(id);' onchange='Details_SetIsRowChanged(id);' data-required='true' value='" + ($("#txtDescription" + (maxDetailsIDInTable - 1)).val() == undefined ? "" : $("#txtDescription" + (maxDetailsIDInTable - 1)).val()) + "' /> </td>";
        tr += "     <td class='AccountID' style='width:20%;' val=''><select id='slAccount" + maxDetailsIDInTable + "' style='width:100%;' class='controlStyle selectAccountID' onfocus='DisableBackspaceKey(id);' onkeydown='DisableBackspaceKey(id);' onkeypress='DisableBackspaceKey(id);' onchange='Details_SetIsRowChanged(id); Details_AccountChanged(" + maxDetailsIDInTable + ");  ' data-required='true'>" + "<option value=0></option>" + "</select></td>";
        tr += "     <td class='SubAccountID' style='width:20%;' val=''><select id='slSubAccount" + maxDetailsIDInTable + "' style='width:100%;' class='controlStyle selectSubAccountID' onfocus='DisableBackspaceKey(id);' onkeydown='DisableBackspaceKey(id);' onkeypress='DisableBackspaceKey(id);' onchange='Details_SetIsRowChanged(id); SubAccount_Changed(this);SubAccount_ChangedSalesMan(" + maxDetailsIDInTable + ");' data-required='false'>" + "<option value=0></option>" + "</select></td>";
        tr += "     <td class='CostCenterID' style='width:15%;' val=''><select id='slCostCenter" + maxDetailsIDInTable + "' style='width:150px;' class='controlStyle selectcostcenterID' onfocus='DisableBackspaceKey(id);' onkeydown='DisableBackspaceKey(id);' onkeypress='DisableBackspaceKey(id);' onchange='Details_SetIsRowChanged(id);' data-required='false'>" + "<option value=0></option>" + "</select></td>";

        tr += "     <td class='OperationID' style='width:15%;' val=''><select id='slOperation" + maxDetailsIDInTable + "' style='width:100%;' class='controlStyle selectOperation' onfocus='DisableBackspaceKey(id);' onkeydown='DisableBackspaceKey(id);' onkeypress='DisableBackspaceKey(id);'  onchange='Details_HouseAndCertificateChangedByOperation(" + maxDetailsIDInTable + ");'   data-required='true'>" + "<option value=0></option>" + "</select></td>";
        tr += "     <td class='OperationSearch' style='width:3%; padding:6px 0px 6px 0px;' val=''><button id='btnOperationSearch' class='btn btn-sm btn-lightblue m-t-xmd' type='button' onclick='ChangeOperation(" + maxDetailsIDInTable + ");'><i class='fa fa-search'></i></button> </td>"
        tr += "     <td class='HouseID' disabled style='width:7%;' val=''><select id='slHouse" + maxDetailsIDInTable + "' style='width:100%;' class='controlStyle selectHouseNumber' onfocus='DisableBackspaceKey(id);' onkeydown='DisableBackspaceKey(id);' onkeypress='DisableBackspaceKey(id);'  onchange='Details_ChangedByHouseAndCertificateAndTrucingOrder(" + maxDetailsIDInTable + ',' + 1 + ");'  data-required='false'>" + "<option value=0></option>" + "</select></td>";
        tr += "     <td class='TruckingOrderID' disabled style='width:7%;' val=''><select id='slTruckingOrder" + maxDetailsIDInTable + "' style='width:100%;' class='controlStyle selectTruckingOrder' onfocus='DisableBackspaceKey(id);' onkeydown='DisableBackspaceKey(id);' onkeypress='DisableBackspaceKey(id);'  onchange='Details_ChangedByHouseAndCertificateAndTrucingOrder(" + maxDetailsIDInTable + ',' + 2 + ");'  data-required='false'>" + "<option value=0></option>" + "</select></td>";

        tr += "     <td class='BranchID' disabled style='width:7%;' val=''><select id='slBranch" + maxDetailsIDInTable + "' style='width:100%;' class='controlStyle selectBranch' onfocus='DisableBackspaceKey(id);' onkeydown='DisableBackspaceKey(id);' onkeypress='DisableBackspaceKey(id);'  onchange='Details_ChangedByHouseAndCertificateAndTrucingOrder(" + maxDetailsIDInTable + ',' + 3 + ");'  data-required='false'>" + "<option value=0></option>" + "</select></td>";



        tr += "      <td class='Documented' style='width:1%;'><input id='cbDocumented" + maxDetailsIDInTable + "' name='Documented'  class='cbIsDocumented' type='checkbox' value='' ></td>";
        tr += "     <td class='SelectedIDsToUpdate hide'> <input name='SelectedIDsToUpdate'  id='SelectedIDsToUpdate" + maxDetailsIDInTable + "' type='checkbox' value='" + maxDetailsIDInTable + "' /></td>";
        tr += "     <td class='hide'>"
                            //+ "<a href='#'  onclick='Pricing_CopyRow(" + maxDetailsIDInTable + ");' " + copyControlsText + "</a>"
                      + "</td>";
        tr += "</tr>";
    }
    //if ($("#tblDetails tbody tr").length > 0)
    //    $(tr).insertBefore('#tblDetails > tbody > tr:first');
    //else
    $("#tblDetails tbody").prepend(tr);
    if ($("[id$='hf_ChangeLanguage']").val() == "ar")
        $("#tblDetails tbody tr[ID=" + maxDetailsIDInTable + "]").reverseChildren();
    /***************************Filling row controls******************************/

    $("#slAccount" + maxDetailsIDInTable).html($("#slAccount").html());
    $("#slAccount" + maxDetailsIDInTable).val($("#slAccount" + (maxDetailsIDInTable - 1)).val() == undefined ? 0 : $("#slAccount" + (maxDetailsIDInTable - 1)).val());

    $("#slOperation" + maxDetailsIDInTable).html($("#slOperation").html());
    $("#slOperation" + maxDetailsIDInTable).val($("#slOperation" + (maxDetailsIDInTable - 1)).val() == undefined ? 0 : $("#slOperation" + (maxDetailsIDInTable - 1)).val());

    $("#slHouse" + maxDetailsIDInTable).html($("#slHouse").html());
    $("#slTruckingOrder" + maxDetailsIDInTable).html($("#slTruckingOrder").html());

    $("#slBranch" + maxDetailsIDInTable).html($("#slBranch").html());


    //  if ($("#hDefaultUnEditableCompanyName").val() == "ERP") {
    //Start Auto Filter
    $("#slAccount" + maxDetailsIDInTable).css({ "width": "280px" }).select2();
    $("#slOperation" + maxDetailsIDInTable).css({ "width": "100%" }).select2();
    $("#slHouse" + maxDetailsIDInTable).css({ "width": "100%" }).select2();
    $("#slTruckingOrder" + maxDetailsIDInTable).css({ "width": "100%" }).select2();

    $("#slBranch" + maxDetailsIDInTable).css({ "width": "100%" }).select2();

    $("#slOperation" + maxDetailsIDInTable).val("0");
    $("#slSubAccount" + maxDetailsIDInTable).css({ "width": "280px" }).select2();
    $("div[tabindex='-1']").removeAttr('tabindex');
    $("#slAccount" + maxDetailsIDInTable).trigger("change");
    $("#slOperation" + maxDetailsIDInTable).trigger("change");

    $("#slSubAccount" + maxDetailsIDInTable).trigger("change");

    $("#slBranch" + maxDetailsIDInTable).val(pLoggedUser.BranchID);
    $("#slBranch" + maxDetailsIDInTable).trigger("change");
    //End Auto Filter
    //  }

    $("#slSubAccount" + maxDetailsIDInTable).html("<option value=0>" + TranslateString("SelectFromMenu") + "</option>");
    $("#slSubAccount" + maxDetailsIDInTable).html($("#slSubAccount" + (maxDetailsIDInTable - 1)).html());
    $("#slSubAccount" + maxDetailsIDInTable).val($("#slSubAccount" + (maxDetailsIDInTable - 1)).val() == undefined ? 0 : $("#slSubAccount" + (maxDetailsIDInTable - 1)).val());

    $("#slCostCenter" + maxDetailsIDInTable).html($("#slCostCenter").html());
    $("#slCostCenter" + maxDetailsIDInTable).val($("#slCostCenter" + (maxDetailsIDInTable - 1)).val() == undefined ? 0 : $("#slCostCenter" + (maxDetailsIDInTable - 1)).val());
    $("#slCostCenter" + maxDetailsIDInTable).css({ "width": "100%" }).select2();
    $("#slCostCenter" + maxDetailsIDInTable).trigger("change");


    Details_FillSlSubAccountNew("slSubAccount" + maxDetailsIDInTable, $("#slSubAccount" + maxDetailsIDInTable).val(), $("#slAccount" + maxDetailsIDInTable).val());

    if ($("#hDefaultUnEditableCompanyName").val() == "SAF") {
        $(".isDepartement").removeClass("hide");
        $("#isDepartement").removeClass('hide');

        $(".isBranch").addClass("hide");
        $("#isBranch").addClass('hide');
    }
    else {
        $(".isBranch").removeClass("hide");
        $("#isBranch").removeClass('hide');

        $(".isDepartement").addClass("hide");
        $("#isDepartement").addClass('hide');
    }

    //SetDatepickerFormat();
    BindAllCheckboxonTable("tblDetails", "DetailsID", "cbDetailsDeleteHeader");//Parameters(pTableName, pCheckBoxColumnClassName, pCheckBoxHeaderID)
    /***********************EOF Filling row controls******************************/
}
function Details_SetIsRowChanged(pControlID) {
    debugger;
    var ChangedRowID = $("#" + pControlID).parent().parent().attr("ID");
    $("#SelectedIDsToUpdate" + ChangedRowID).prop("checked", true);
}
function Details_FillSlSubAccountNew(pSlName, pSubAccountID, pAccountID) {
    debugger;
    FadePageCover(true);

    $("div[tabindex='-1']").removeAttr('tabindex');
    $("#" + pSlName).trigger("change");

    CallGETFunctionWithParameters("/api/ChartOfLinkingAccounts/LoadSubAccountDetails"
        , {
            pLanguage: $("[id$='hf_ChangeLanguage']").val()
            , pWhereClauseSubAccountDetails: "WHERE IsMain=0 AND Account_ID=" + pAccountID
            , pOrderBy: "Name"
        }
        , function (pData) {



            FillListFromObject_ERP(pSubAccountID, 4/*pCodeOrName*/, TranslateString("SelectFromMenu"), pSlName, pData[0], null);
    //        if ($("#hDefaultUnEditableCompanyName").val() == "ERP") {
                //Start Auto Filter
                //$("#" + pSlName).css({ "width": "100%" }).select2();
                //$("div[tabindex='-1']").removeAttr('tabindex');
                //$("#" + pSlName).trigger("change");
                //End Auto Filter
     //       }
            FadePageCover(false);
        }
        , null);
}
function Details_AccountChanged(pRowID) {
    debugger;

    $("#slSubAccount" + pRowID).val(0);
    Details_FillSlSubAccountNew("slSubAccount" + pRowID, 0, $("#slAccount" + pRowID).val());
}
function Details_CalculateTotals() {
    debugger;
    var pSum = 0;

    $("#tblDetails>tbody>tr").each(function (i, tr) {
        if ($(tr).find('td.Value input[type="text"]').val() != 'undefined'
            && $(tr).find('td.Value input[type="text"]').val() != undefined
            && $(tr).find('td.Value input[type="text"]').val() != '') {
            //pSum += parseFloat($(tr).find('td.Value').find('.inputValue').val());
            var Value = $(tr).find('td.Value input[type="text"]').val();
            pSum += parseFloat(Value);

        }

    });

    return pSum;
}
function VoucherHeaderAndDetails_Save(pSaveAndNew, callback) {
    debugger;

    var TotalValue = 0;
    var _Suceess = true;

    $($('#tblDetails > tbody > tr')).each(function (i, tr) {
        debugger;

        var AccountID = $(tr).find('td.AccountID').find('.selectAccountID').val();
        var SubAccountID = $(tr).find('td.SubAccountID').find('.selectSubAccountID').val();
        var CostCenterID = $(tr).find('td.CostCenterID ').find('.selectcostcenterID').val();
        var Value = $(tr).find('td.Value').find('.inputValue').val();
        var SubAccountCount = $(tr).find('td.SubAccountID').find('.selectSubAccountID').find('option').length;

        var BranchID = $(tr).find('td.BranchID').find('.selectBranch').val();
        
        if (pDefaults.UnEditableCompanyName == 'ILSEG' || pDefaults.UnEditableCompanyName == 'ILS')
        {
            if ((BranchID.trim() == "" || BranchID.trim() == "0" || BranchID == null)) {
                swal('Excuse me', 'Select Branch', 'warning');
                _Suceess = false;
                return false;
            }

        }
       


        if ((AccountID.trim() == "" || AccountID.trim() == "0" || AccountID == null)) {
            swal('Excuse me', 'Fill All Account', 'warning');
            _Suceess = false;
            return false;
        }

       // var CostCenterType = $('#hReadySlOptions option[value="12"]').attr("OptionValue");

        if (CostCenterID.trim() == "0" && CostCenterType == "true") {
            swal('Excuse me', 'Fill  Items Cost Center', 'warning');
            _Suceess = false;
            $('td.CostCenterID ').addClass('validation-error');
            return false;
        } else {
            $('td.CostCenterID ').removeClass('validation-error');
        }
        if ((SubAccountID.trim() == "" || SubAccountID.trim() == "0" || SubAccountID == null) && SubAccountCount > 1) {
            swal('Excuse me', 'Fill  SubAccount', 'warning');
            _Suceess = false;
            return false;
        }
        //if (( CostCenterID.trim() == "")) {
        //    swal('Excuse me', 'Fill  Items Cost Center', 'warning');
        //    _Suceess = false;
        //    return false;
        //}
        if (Value.trim() == "" || Value.trim() == "0" || Value == null) {
            swal('Excuse me', 'Insert Value ', 'warning');
            _Suceess = false;
            return false;
        }

        TotalValue +=parseFloat( Value);

    });

    if (glbFormCalled == 20) {
        TotalValue = parseFloat( $("#txtCurrBalance").val()) -TotalValue ;
        if (TotalValue < 0)
        {
            _Suceess = false;
            swal({
                title: "Are you sure?",
                text: "Safe balance is not enough!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes!",
                closeOnConfirm: true
            },
                function () {
                    _Suceess = true;
                    VoucherHeaderAndDetails_SaveAction(_Suceess, callback);
                });
        }
        else
            VoucherHeaderAndDetails_SaveAction(_Suceess, callback);

    }
    else
    {
        VoucherHeaderAndDetails_SaveAction(_Suceess, callback);
    }



}
function VoucherHeaderAndDetails_SaveAction(_Suceess, callback)
{
    if ($("#txtExchangeRate").val() == 0)
        swal("Sorry", "Exchange rate can not be 0.");
    else if ($("#tblDetails tbody tr").length == 0)
        swal("Sorry", "You must enter details.");
    else if (ValidateForm("form", "VoucherModal") && _Suceess) {
        FadePageCover(true);
        Voucher_CalculateTotal();


        var pParametersWithValues = {
            pID: $("#hID").val() == "" ? 0 : $("#hID").val()
            , pCode: $("#txtCode").val().trim().toUpperCase()
            , pVoucherDate: ConvertDateFormat($("#txtVoucherDate").val())
            , pSafeID: $("#slSafe").val()
            , pCurrencyID: $("#slCurrency").val()
            , pExchangeRate: $("#txtExchangeRate").val()
            , pChargedPerson: $("#txtChargedPerson").val().trim() == "" ? "0" : $("#txtChargedPerson").val().trim().toUpperCase()
            , pNotes: $("#txtNotes").val().trim() == "" ? "0" : $("#txtNotes").val().trim().toUpperCase()
            , pTaxID: $("#slTax").val()//($("#slTax").val() != 0 && $("#txtTaxValue").val() != 0 ? $("#slTax").val() : 0)
            , pTaxValue: ($("#slTax").val() != 0 && $("#txtTaxValue").val() != 0 ? $("#txtTaxValue").val() : 0)
            , pTaxSign: (
                            ($("#slTax option:selected").attr("IsDebitAccount") == 1
                                && (glbFormCalled == constVoucherCashIn || glbFormCalled == constVoucherChequeIn)
                            )
                            || ($("#slTax option:selected").attr("IsDebitAccount") == 0
                                && (glbFormCalled == constVoucherCashOut || glbFormCalled == constVoucherChequeOut)
                            )
                         )
                         ? -1 : 1
            , pTaxID2: $("#slTax2").val()//($("#slTax2").val() != 0 && $("#txtTaxValue2").val() != 0 ? $("#slTax2").val() : 0)
            , pTaxValue2: ($("#slTax2").val() != 0 && $("#txtTaxValue2").val() != 0 ? $("#txtTaxValue2").val() : 0)
            , pTaxSign2: (
                            ($("#slTax2 option:selected").attr("IsDebitAccount") == 1
                                && (glbFormCalled == constVoucherCashIn || glbFormCalled == constVoucherChequeIn)
                            )
                            || ($("#slTax2 option:selected").attr("IsDebitAccount") == 0
                                && (glbFormCalled == constVoucherCashOut || glbFormCalled == constVoucherChequeOut)
                            )
                         )
                         ? -1 : 1
            , pTotal: parseFloat($("#lblTotal").text().replace(":", ""))
            , pTotalAfterTax: parseFloat($("#lblTotalAfterTax").text().replace(":", ""))
            , pIsAGInvoice: false
            , pAGInvType_ID: 000
            , pInv_No: 000
            , pInvoiceID: 000
            , pJVID1: 000
            , pJVID2: 000
            , pJVID3: 000
            , pJVID4: 000
            , pSalesManID: 000
            , pforwOperationID: 000
            , pIsCustomClearance: false
            , pTransType_ID: 000
            , pVoucherType: glbFormCalled
            , pIsCash: ($("#cbIsCash").prop("checked") && glbFormCalled == constVoucherCashOut) ? true : false
            , pIsCheque: false
            , pPrintDate: "01/01/1900"
            , pChequeNo: 000
            , pChequeDate: "01/01/1900"
            , pBankID: 000
            , pOtherSideBankName: 0
            , pCollectionDate: "01/01/1900"
            , pCollectionExpense: 000
            , pDisbursementJob_ID: $("#slDisbursementJobs").val() == "" ? 0 : $("#slDisbursementJobs").val()
            , pSL_SalesManID: $("#slSalesMan").val() == "" ? 0 : $("#slSalesMan").val()


        };
        debugger;
        CallPOSTFunctionWithParameters("/api/Voucher/VoucherHeader_Save", pParametersWithValues
            , function (pData) {
                debugger;
                var pMessageReturned = pData[0];
                var pID = pData[1];
                $('#hID').val(pID);
                if (pMessageReturned == "") {
                    //swal("Success", "Saved successfully.");
                    //jQuery("#VoucherModal").modal("hide");
                    //Voucher_LoadingWithPaging();
                    ListOfListOfObject = [];
                    ListOfListOfObject.push(SetArrayOfItemsDetails());
                    debugger;
                    InsertUpdateListOfObject("/api/Voucher/InsertItems", ListOfListOfObject
                            , false, "VoucherModal", function (pData) {
                                debugger;
                                var pMessageReturned = pData[1];
                                if (pMessageReturned == "" || pMessageReturned == "Done") {
                                    if (callback != null && callback != undefined)
                                        callback();
                                    if ($("[id$='hf_ChangeLanguage']").val() == "en") swal("Success", "Saved successfully.");
                                    else if ($("[id$='hf_ChangeLanguage']").val() == "ar") swal("نجاح", "تم الحفظ بنجاح.");
                                    jQuery("#VoucherModal").modal("hide");
                                    Voucher_LoadingWithPaging();
                                }
                                else {
                                    swal("Sorry", pMessageReturned);
                                    FadePageCover(false);
                                }
                            }
                            );

                    if (IsCashIssueReceiptFromSettlement == 1 && A_PaymentRequestID_ForVoucherID != 0) {
                        CallGETFunctionWithParameters("/api/PaymentRequest/UpdateVoucherIDInPaymentRequest", { pVoucherID: pID, pPaymentRequestID: A_PaymentRequestID_ForVoucherID }
                      , function (pData) {
                          debugger;
                          LoadDefaults("/api/Defaults/LoadAll", " WHERE 1=1 ");
                      }, null);
                    }
                }
                else {
                    swal("Sorry", pMessageReturned);
                    FadePageCover(false);
                }
            }
            ,
            null
            );

    }
}
function VoucherHeaderAndDetails_Save_Cash(pSaveAndNew, pInvoicesItems, callback) {
    debugger;

    var _Suceess = true;

    if ($("#txtExchangeRate").val() == 0)
        swal("Sorry", "Exchange rate can not be 0.");

    else if (ValidateForm("form", "VoucherModal") && _Suceess) {
        FadePageCover(true);
        Voucher_CalculateTotal();


        var pParametersWithValues = {
            pID: $("#hID").val() == "" ? 0 : $("#hID").val()
            , pCode: $("#txtCode").val().trim().toUpperCase()
            , pVoucherDate: ConvertDateFormat($("#txtVoucherDate").val())
            , pSafeID: $("#slSafe").val()
            , pCurrencyID: $("#slCurrency").val()
            , pExchangeRate: $("#txtExchangeRate").val()
            , pChargedPerson: $("#txtChargedPerson").val().trim() == "" ? "0" : $("#txtChargedPerson").val().trim().toUpperCase()
            , pNotes: $("#txtNotes").val().trim() == "" ? "0" : $("#txtNotes").val().trim().toUpperCase()
            , pTaxID: $("#slTax").val()   //($("#slTax").val() != 0 && $("#txtTaxValue").val() != 0 ? $("#slTax").val() : 0)
            , pTaxValue: ($("#slTax").val() != 0 && $("#txtTaxValue").val() != 0 ? $("#txtTaxValue").val() : 0)
            , pTaxSign: (
                            ($("#slTax option:selected").attr("IsDebitAccount") == 1
                                && (glbFormCalled == constVoucherCashIn || glbFormCalled == constVoucherChequeIn)
                            )
                            || ($("#slTax option:selected").attr("IsDebitAccount") == 0
                                && (glbFormCalled == constVoucherCashOut || glbFormCalled == constVoucherChequeOut)
                            )
                         )
                         ? -1 : 1
            , pTaxID2: $("#slTax2").val()//($("#slTax2").val() != 0 && $("#txtTaxValue2").val() != 0 ? $("#slTax2").val() : 0)
            , pTaxValue2: ($("#slTax2").val() != 0 && $("#txtTaxValue2").val() != 0 ? $("#txtTaxValue2").val() : 0)
            , pTaxSign2: (
                            ($("#slTax2 option:selected").attr("IsDebitAccount") == 1
                                && (glbFormCalled == constVoucherCashIn || glbFormCalled == constVoucherChequeIn)
                            )
                            || ($("#slTax2 option:selected").attr("IsDebitAccount") == 0
                                && (glbFormCalled == constVoucherCashOut || glbFormCalled == constVoucherChequeOut)
                            )
                         )
                         ? -1 : 1
            , pTotal: parseFloat($("#lblTotal").text().replace(":", ""))
            , pTotalAfterTax: parseFloat($("#lblTotalAfterTax").text().replace(":", ""))
            , pIsAGInvoice: false
            , pAGInvType_ID: 000
            , pInv_No: 000
            , pInvoiceID: 000
            , pJVID1: 000
            , pJVID2: 000
            , pJVID3: 000
            , pJVID4: 000
            , pSalesManID: 000
            , pforwOperationID: 000
            , pIsCustomClearance: false
            , pTransType_ID: 000
            , pVoucherType: glbFormCalled
            , pIsCash: ($("#cbIsCash").prop("checked") && glbFormCalled == constVoucherCashOut) ? true : false
            , pIsCheque: false
            , pPrintDate: "01/01/1900"
            , pChequeNo: 000
            , pChequeDate: "01/01/1900"
            , pBankID: 000
            , pOtherSideBankName: 0
            , pCollectionDate: "01/01/1900"
            , pCollectionExpense: 000
        };
        CallPOSTFunctionWithParameters("/api/Voucher/VoucherHeader_Save", pParametersWithValues
            , function (pData) {
                debugger;
                var pMessageReturned = pData[0];
                var pID = pData[1];
                $('#hID').val(pID);
                if (pMessageReturned == "") {
                    //swal("Success", "Saved successfully.");
                    //jQuery("#VoucherModal").modal("hide");
                    //Voucher_LoadingWithPaging();
                    debugger;
                    var pAllocationItemsIDs = "";
                    var pAmounts = "";
                    var pTblAllocationItem = $("#tblAllocationItem tbody tr");
                    for (var i = 0; i < pTblAllocationItem.length; i++) {
                        //Fill Parameters from tbl controls here
                        var pRowID = pTblAllocationItem[i].id;
                        var tr = $("#tblAllocationItem tbody tr[ID=" + pRowID + "]");
                        if (parseFloat($("#txtItemAmountDue" + pRowID).val()) > 0 && $("#txtItemAmountDue" + pRowID).val() != "") {
                            pAllocationItemsIDs += (pAllocationItemsIDs == "" ? (pRowID) : ("," + pRowID));
                            pAmounts += (pAmounts == "" ? (parseFloat($("#txtItemAmountDue" + pRowID).val())) : ("," + parseFloat($("#txtItemAmountDue" + pRowID).val())));
                        }
                    }

                    var pParametersWithValues = {
                        pAllocationItemsIDs: pAllocationItemsIDs
                    , pAmounts: pAmounts
                    };

                    CallGETFunctionWithParameters("/api/A_ARAllocation/ARUpdateCashInvoicePaid", pParametersWithValues
                        , function (pData) {
                            if (pData[0]) {
                                swal("Success", "Allocation done successfully.");

                                // ARAllocation_Partners_LoadingWithPaging();
                                jQuery("#InvModal").modal("hide");
                                jQuery("#VoucherModal").modal("hide");
                                console.log($("#hID").val());
                                //Voucher_Save();
                                console.log(SetArrayOfItems());
                                debugger;
                                InsertUpdateFunction3("/api/Voucher/InsertA_VoucherInvoicesPayment",
                                    SetArrayOfItems()  //JSON.stringify(SetArrayOfItems())
                                    , false, null, function () {
                                        ClearAllModals();
                                        console.log("success InsertVoucherInvoicesPayment")
                                    });

                            }
                            else {
                                swal("Sorry", "Connection failed, please refresh and then try again.");
                            }
                            FadePageCover(false);
                        }
                        , null);
                }
                else {
                    swal("Sorry", pMessageReturned);
                    FadePageCover(false);
                }
            }
            ,
            //null
            function () {
                $.each(pInvoicesItems, function () {
                        this.VoucherID = $('#hID').val();
                });
                ListOfListOfObject = [];
                ListOfListOfObject.push(SetArrayOfItemsDetails());
                InsertUpdateListOfObject("/api/Voucher/InsertItems", ListOfListOfObject
                        , false, "VoucherModal", function (pData) {
                            debugger;
                            var pMessageReturned = pData[1];
                            if (pMessageReturned == "" || pMessageReturned == "Done") {
                                if ($("[id$='hf_ChangeLanguage']").val() == "en") swal("Success", "Saved successfully.");
                                else if ($("[id$='hf_ChangeLanguage']").val() == "ar") swal("نجاح", "تم الحفظ بنجاح.");
                                jQuery("#VoucherModal").modal("hide");
                                Voucher_LoadingWithPaging();
                            }
                            else {
                                swal("Sorry", pMessageReturned);
                                FadePageCover(false);
                            }
                        }
                        );
            }
            );
    }
}
function SetArrayOfItemsDetails() {
    var arrayOfItems = new Array();

    $("#tblDetails>tbody>tr").each(function (i, tr) {
        debugger;

        var IsDoc = false;

        if ($(tr).attr('id') != 'undefined' && $(tr).attr('id') != undefined) {
            var objItem = new Object();
            objItem.ID = $(tr).attr('id');  //$(tr).attr('value');
            objItem.VoucherID = $('#hID').val();
            objItem.Value = $(tr).find('td.Value').find('.inputValue').val();
            objItem.Description = $(tr).find('td.Description').find('.inputDescription').val();
            objItem.AccountID = $(tr).find('td.AccountID').find('.selectAccountID').val();
            objItem.SubAccountID = $(tr).find('td.SubAccountID').find('.selectSubAccountID').val();
            objItem.CostCenterID = $(tr).find('td.CostCenterID').find('.selectcostcenterID').val();
            objItem.OperationID = $(tr).find('td.OperationID').find('.selectOperation').val() == null ? "0" : $(tr).find('td.OperationID').find('.selectOperation').val() == "" ? "0" : $(tr).find('td.OperationID').find('.selectOperation').val() == "undefined" ? "0" : $(tr).find('td.OperationID').find('.selectOperation').val();
            objItem.HouseID = $(tr).find('td.HouseID').find('.selectHouseNumber').val() == null ? "0" : $(tr).find('td.HouseID').find('.selectHouseNumber').val() == "" ? "0" : $(tr).find('td.HouseID').find('.selectHouseNumber').val() == "undefined" ? "0" : $(tr).find('td.HouseID').find('.selectHouseNumber').val();
            objItem.BranchID = $(tr).find('td.BranchID').find('.selectBranch').val() == null ? "0" : $(tr).find('td.BranchID').find('.selectBranch').val() == "" ? "0" : $(tr).find('td.BranchID').find('.selectBranch').val() == "undefined" ? "0" : $(tr).find('td.BranchID').find('.selectBranch').val();
            objItem.TruckingOrderID = $(tr).find('td.TruckingOrderID').find('.selectTruckingOrder').val() == null ? "0" : $(tr).find('td.TruckingOrderID').find('.selectTruckingOrder').val() == "" ? "0" : $(tr).find('td.TruckingOrderID').find('.selectTruckingOrder').val() == "undefined" ? "0" : $(tr).find('td.TruckingOrderID').find('.selectTruckingOrder').val();


            //$(tr).find('td.IsDocumented').find('.cbIsDocumented').prop("checked");
            if ($(tr).find('input[name="Documented"]').is(':checked'))
                IsDoc = true;

            objItem.IsDocumented = IsDoc;
            objItem.InvoiceID = 0;

            if (glbFormCalled == constVoucherCashIn) {
                objItem.VoucherType = 10;
            }
            else {
                objItem.VoucherType = 20;
            }

            arrayOfItems.push(objItem);

        }

    });
    return arrayOfItems;
}
function Details_FillSlSubAccountInTable(THIS, ChildClass, pSubAccountID) {

    debugger;

    //$(THIS).css({ "width": "100%" }).select2();
    //$("div[tabindex='-1']").removeAttr('tabindex');
    //$(THIS).trigger("change");

    var ChildComboBox = $(THIS).closest('tr').find(ChildClass).find("select");
    Fill_SelectInput_WithDependedID("/api/ChartOfLinkingAccounts/FillUpdateSubAccount", "ID", "Name", "<--Select-->", ChildComboBox, pSubAccountID, $(THIS).val())
    //        if ($("#hDefaultUnEditableCompanyName").val() == "ERP") {
    //Start Auto Filter
    //$("#" + pSlName).css({ "width": "100%" }).select2();
    //$("div[tabindex='-1']").removeAttr('tabindex');
   // $("#" + pSlName).trigger("change");
    //End Auto Filter
    //       }
    if (pSubAccountID != 0 && pDefaults.UnEditableCompanyName != "PHO" && pDefaults.UnEditableCompanyName != "EGL"
        && pDefaults.UnEditableCompanyName != "KDS" && pDefaults.UnEditableCompanyName != "ARK")
        $("#txtChargedPerson").prop("disabled", "disabled");
    //if ($(THIS).val() == 0) //No Account is selected so just empty subaccounts
    //    $(ChildComboBox).html("<option value=0>" + TranslateString("SelectFromMenu") + "</option>");
    //else {
    //    FadePageCover(true);
    //    CallGETFunctionWithParameters("/api/ChartOfLinkingAccounts/LoadSubAccountDetails"
    //        , {
    //            pLanguage: $("[id$='hf_ChangeLanguage']").val()
    //            , pWhereClauseSubAccountDetails: "WHERE IsMain=0 AND Account_ID=" + $(THIS).val()
    //            , pOrderBy: "Name"
    //        }
    //        , function (pData) {
    //           // FillListFromObject_ERP(pSubAccountID, 4/*pCodeOrName*/, TranslateString("SelectFromMenu"), pSlName, pData[0], null);

    //            Fill_SelectInputAfterLoadData(pData, "ID", "Name", null, ChildComboBox, pSubAccountID)
    //            //if ($("#hDefaultUnEditableCompanyName").val() == "ERP") {
    //            //    //Start Auto Filter
    //            //    $("#" + pSlName).trigger("change");
    //            //    //End Auto Filter
    //            //}
    //            FadePageCover(false);
    //        }
    //        , null);
    //}
}
//function PostVoucher(pValue) { //pValue 1:Post, 2:Unpost
//    debugger;
//    var pSelectedIDs = GetAllSelectedIDsAsString('tblVoucher', 'Delete');
//    if (pSelectedIDs != "") {
//        FadePageCover(true);
//        CallGETFunctionWithParameters("/api/Voucher/SetPostField"
//            , {
//                pSelectedIDs: pSelectedIDs
//                , pGivenDate: ConvertDateFormat($("#txtJVDate").val().trim())
//                , pValue: pValue
//                , pUseGivenDate: (pValue == 1 ? $("#cbIsJVDate").prop("checked") : false)
//            }
//            , function (pData) {
//                if (!pData[0]) {
//                    showDeleteFailMessage = true;
//                    strDeleteFailMessage = "One or more Vouchers can not be posted/unposted because fiscal year is closed or date is frozen.";
//                }
//                else
//                    swal("Success", "Saved successfully");
//                Post_Unpost_Voucher_LoadingWithPaging(pData[0]);
//            }
//            , null);
//    }
//}

//function Details_BindTableRows(pTableRows) {
//    debugger;
//    ClearAllTableRows("tblDetails");
//    $.each(pTableRows, function (i, item) {
//        AppendRowtoTable("tblDetails",
//            ("<tr ID='" + item.ID + "' "
//                + (" ondblclick='Details_FillControls(" + item.ID + ");' ")
//                + ">"
//                    + "<td class='DetailsID'> <input " + (1 == 2 ? " disabled='disabled' " : " name='Delete' ") + " type='checkbox' value='" + item.ID + "' /></td>"
//                    + "<td class='Value'>" + item.Value + "</td>"
//                    + "<td class='Description'>" + (item.Description == 0 ? "" : item.Description) + "</td>"
//                    + "<td class='AccountID hide'>" + item.AccountID + "</td>"
//                    + "<td class='AccountName'>" + (item.Account_Name + " - " + item.Account_Number) + "</td>"
//                    + "<td class='SubAccountID hide'>" + item.SubAccountID + "</td>"
//                    + "<td class='SubAccountName'>" + (item.SubAccountID == 0 ? "" : (item.SubAccount_Name + " - " + item.SubAccount_Number)) + "</td>"
//                    + "<td class='CostCenterID hide'>" + item.CostCenterID + "</td>"
//                    + "<td class='CostCenterName'>" + (item.CostCenterID == 0 ? "" : (item.CostCenterName + " - " + item.CostCenterNumber)) + "</td>"
//                    + "<td class='Documented'> <input id=cbDocumented" + item.ID + " type='checkbox' disabled='disabled' " + (item.IsDocumented ? " checked='checked' " : "") + " /></td>"
//                    //+ "<td class='InvoiceNo " + (glbFormCalled == constVoucherCashIn ? "" : "hide") + "'>" + (item.InvoiceID == 0 ? "" : item.InvoiceNo) + "</td>"
//                    + "<td class='hide'><a href='#DetailsModal' data-toggle='modal' onclick='Details_FillControls(" + item.ID + ");' " + editControlsText + "</a></td></tr>"));
//    });
//    //ApplyPermissions();
//    BindAllCheckboxonTable("tblDetails", "DetailsID", "cbDetailsDeleteHeader");//Parameters(pTableName, pCheckBoxColumnClassName, pCheckBoxHeaderID)
//    CheckAllCheckbox("HeaderDeleteDetailsID");
//    //HighlightText("#tblDetails>tbody>tr", $("#txt-Search").val().trim());
//    if (showDeleteFailMessage) { //sherif: if deletion failed due to foreign key, then this message is shown
//        swal(strSorry, strDeleteFailMessage);
//        showDeleteFailMessage = false;
//    }
//}

function CalcTotal() {


        $('#lblTotalAllocation').html(' ');

        var Total = 0;
        var Cost  =0;
        $('#tblAllocationItem  > tbody > tr').each(function () {
            if ($(this).find('td.AmountDue').find('input:text').val() != '')
            {
                Cost = $(this).find('td.AmountDue').find('input:text').val();
                Total += parseFloat(Cost);
            }



        });

        $('#lblTotalAllocation').append(Total.toFixed(2));
        $('#lblTotalAllocation').append(' ');


       
}


function ARAllocation_PaidAll(pID, pAmount) {
    debugger;

    if ($("#tblAllocationItem tbody tr[ID=" + pID + "]").find("td.ID").find('input:checkbox').prop('checked'))
        $('#txtItemAmountDue' + pID).val(pAmount);
    else
        $('#txtItemAmountDue' + pID).val(0);

    ARAllocation_ReCalculate();
    CalcTotal();

}


/*********************************************OfficialAllocation(showing partners)************************************************/
var _OfficialAllocationTotalAmount = "";

function OpenOfficialAllocationModal() {
    if ($("#txtExchangeRate").val() == 0)
        swal("Sorry", "Exchange rate can not be 0.");
    else if (ValidateForm("form", "VoucherModal")) {
        if ($('#slCurrency').val() == "0") {
            swal("Excuse me !", "Select Currency", "warning")

        }
        else {
            //$("#cbIsCashInvoice").prop("checked", false);
            //$("#cbIsAllocation").prop("checked", true);
            //Voucher_cbIsCash();

            // if (glbFormCalled == constVoucherCashIn)
            glbTransactionType = constTransactionReceivableAllocation;
            //else
            //    glbTransactionType = constTransactionPayableAllocation;

            jQuery('#OfficialAllocationModal').modal('show');

            $.ajax({
                type: "GET",
                url: strServerURL + "api/A_ARAllocation/UnapprovingAllocations_IntializeData",
                data: { 'PartenertTypeID': "-1" },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (d) {
                    // Fill_SelectInputAfterLoadData(d[0], 'ID', 'Name', '<--  Partener Type -->', '#slPartnerType', '');
                    FillListFromObject(1, 1/*pCodeOrName*/, TranslateString("SelectFromMenu")/*"Select Pay. Type"*/, "slOfficialAllocationPartnerType", d[0], null);
                },
                error: function (jqXHR, exception) {
                    debugger;
                    swal("Oops!", "Please, contact your technical support!  this is print region !", "error");
                    FadePageCover(false);
                }
            });




        }
    }




}
function ARAllocation_OfficialAllocationPartnerTypeChanged() {
    $("#slOfficialAllocationPartner").html("<option value=''>" + TranslateString("SelectFromMenu") + "</option>");//to quickly empty
    if ($("#slOfficialAllocationPartnerType").val() != "") {
        if ($("#slOfficialAllocationPartnerType").val() == constCustomerPartnerTypeID) {
            $("#slOfficialAllocationPartner").html($("#hReadySlCustomers").html());
        }
        else {
            FadePageCover(true);
            CallGETFunctionWithParameters("/api/InvoicesReports/FillPartners", { pPartnerTypeID: $("#slOfficialAllocationPartnerType").val() }
                , function (pData) {
                    FillListFromObject(null, 2/*pCodeOrName*/, "<--All-->", "slOfficialAllocationPartner", pData[0], null);
                    FadePageCover(false);
                }
                , null);
        }
    }
}
function FillOfficialAllocationData()
{
    debugger;

    if (  $('#slOfficialAllocationPartner').val() == null || $('#slOfficialAllocationPartner').val() == '' ||
        $('#slOfficialAllocationPartnerType').val() == null || $('#slOfficialAllocationPartnerType').val() == '')
    {
        swal("Sorry", " Choose Partner Type And Partner ", "warning")
    }
    else
    {

        var pSearchText = "";

        //if (glbFormCalled == 20) {
        //    if ($("#txt-SearchOperation").val() != "") {
        //        pSearchText += " AND (";
        //        pSearchText += "  OperationCode LIKE N'%" + $("#txt-SearchOperation").val().trim() + "%'" + "\n";
        //        pSearchText += ")";
        //    }
        //    if ($("#txt-SearchInvoiceNo").val() != "") {
        //        pSearchText += " AND (";
        //        pSearchText += " SupplierInvoiceNo = N'" + $("#txt-SearchInvoiceNo").val().trim() + "' ";
        //        pSearchText += ")";
        //    }
        //}
        //else
        pSearchText = $("#txtSearchOfficialAllocationItems").val().trim() == "" ? "" : $("#txtSearchOfficialAllocationItems").val().trim().toUpperCase()


        ClearAllTableRows("tblOfficialAllocationItem"); //to quickly clear before calling controller

        $('#lblTotalOfficialAllocation').html(' ');
        FadePageCover(true);
        var strFunctionName = "/api/Voucher/OfficialAllocation_FillAllocationData";
        var pParametersWithValues = {
            pPartnerID: $('#slOfficialAllocationPartner').val()
            , pPartnerTypeID: $('#slOfficialAllocationPartnerType').val()
            , pAllocationType: 80
            , pSearchText: pSearchText
            , pCurrencyID: $('#slCurrency').val()
        };
        //  $("#btn-searchAllocationItems").attr("onclick", 'ARAllocation_EditByDblClick(' + pPartnerID + ',' + pPartnerTypeID + ',"' + pPartnerName + '","' + pUnAllocatedAmount + '");');
        CallGETFunctionWithParameters(strFunctionName, pParametersWithValues
            , function (pData) {
                if (pData[0]) {
                    if (pData[5] != null && pData[6] != null && pData[5] != 0 && pData[6] != 0) {
                        var pFormattedTodaysDate = getTodaysDateInddMMyyyyFormat();
                        var pInvoices = JSON.parse(pData[1]);
                        var pPartnerBalance = pData[2];
                        var pPayables = JSON.parse(pData[3]);
                        var ptxtAvailableBalance = pData[4];
                        var pvwPayablesAllocationsItems = JSON.parse(pData[7]);
                        //---
                        _AccountID = JSON.parse(pData[5]);
                        _SubAccountID = JSON.parse(pData[6]);
                        console.log(_AccountID + "" + _SubAccountID);
                        //---

                        //$("#lblAllocationShown").html(": " + pPartnerName);
                        $("#hOfficialAllocationPartnerID").val($('#slOfficialAllocationPartner').val());
                        $("#hOfficialAllocationPartnerTypeID").val($('#slOfficialAllocationPartnerType').val());
                        $("#txtAllocationDate").val(getTodaysDateInddMMyyyyFormat);
                        $("#txtAllocationAvailableBalance").val(ptxtAvailableBalance);
                        $("#txtAllocationRemainingBalance").val(pPartnerBalance);

                        ARAllocation_BindOfficialAllocationItemsTableRows(pPayables, pPartnerBalance);


                    }
                    else {

                        swal("Sorry", "The Partener Must Has Account && SubAccount", "warning")
                    }
                }
                else
                    swal("Sorry", "Connection failed, please try again.");
                FadePageCover(false);
            }
            , null);
    }


}

function ARAllocation_BindOfficialAllocationItemsTableRows(pTable, pPartnerBalance) {
    $("#tblOfficialAllocationItem").html("");
    debugger;
    var pCheckboxNameAttr = "OfficialAllocationItemID";
    var pTableHTML = "";
    pTableHTML += "<thead>";
    // pTableHTML += "     <tr>";
    pTableHTML += "<tr>";

    var IsCashInvoice =false


    pTableHTML += '         <th id="OfficialAllocationItemHeaderID"' + (IsCashInvoice ? "" : "class='hide'") + '><input name="Select" id="CbOfficialAllocationItemID" type="checkbox" /></th>';
    pTableHTML += '         <th></th>';
    pTableHTML += '         <th>Partner</th>';
    pTableHTML += '         <th>Charge</th>';
    pTableHTML += '         <th>Operation</th>';
    pTableHTML += '         <th>Invoice No</th>';
    //pTableHTML += '         <th>Status</th>';
    pTableHTML += '         <th>Total</th>';
    pTableHTML += '         <th>Cur</th>';
    pTableHTML += '         <th>Amount Paid</th>';
    pTableHTML += '         <th>PaidAmt</th>';
    pTableHTML += '         <th>Remaining</th>';
    // pTableHTML += '         <th>PayFrom</th>';
    // pTableHTML += '         <th>Ex.Rate</th>';
    pTableHTML += '         <th class="rounded-right hide"></th>';
    pTableHTML += "     </tr>";
    pTableHTML += "</thead>";
    pTableHTML += "<tbody>";
    console.log(pTable)
    debugger;

    $.each(pTable, function (i, item) {
        if ((item.CostAmount.toFixed(4) - item.OfficialAmountPaid.toFixed(4)) > 0) {
            var pRemain = (item.CostAmount.toFixed(4) - item.OfficialAmountPaid.toFixed(4));

                pTableHTML += " <tr ID='" + item.ID + "'> ";
                // pTableHTML += " <tr ID='" + item.OperationID + "'> ";
                pTableHTML += "       <td class='ID ' > <input " + "  onclick='OfficialAllocation_PaidAll(" + item.ID + ',' + pRemain + ");' " + " name='" + pCheckboxNameAttr + "' type='checkbox' value='" + item.ID + "' " + (1 == 1 ? "" : "checked='checked'") + "></td> ";
                // pTableHTML += "       <td class='ID hide' > <input name='" + pCheckboxNameAttr + "' type='checkbox' value='" + item.ID + "' " + (1 == 1 ? "" : "checked='checked'") + "></td> ";
                //pTableHTML += "     <td class='Invoice' val='" + item.ChargeTypeID + "' style='width:300px;'>" + item.ChargeTypeName + "</td> ";
                pTableHTML += "       <td class='PartnerID' val='" + item.PartnerSupplierID + "'>" + item.PartnerSupplierName + "</td>"
                pTableHTML += "       <td class='PartnerTypeID hide' val='" + item.SupplierPartnerTypeID + "'>" + item.PartnerTypeCode + "</td>"
                //pTableHTML += "       <td class='InvoiceNumber'>" + (item.SupplierInvoiceNo == 0 ? "" : item.SupplierInvoiceNo) + "</td> ";
                pTableHTML += "       <td class='Charge'>" + item.ChargeTypeName + "</td> ";
                pTableHTML += "       <td class='Operation' val=" + item.OperationID + ">" + (item.OperationCode == 0 ? "" : item.OperationCode) + "</td> ";
                pTableHTML += "       <td class='InvoiceNumber'>" + (item.SupplierInvoiceNo == 0 ? "" : item.SupplierInvoiceNo) + "</td> ";
                //pTableHTML += "       <td class='Status text-danger'>" + item.PayableStatus + "</td> ";
                pTableHTML += "       <td class='Amount'>" + item.CostAmount.toFixed(3) + "</td> ";
                pTableHTML += "       <td class='CurrencyID' val='" + item.CurrencyID + "'>" + item.CurrencyCode + "</td>"
                //pTableHTML += "       <td class='PayableAmountDue'> <input type='text' id='txtItemAmountDue" + item.ID + "' class='form-control controlStyle' onchange='ARAllocation_Row_CheckAmountDue(" + item.ID + ");' onkeypress='CheckValueIsDecimal(id);' onfocus='CheckValueIsDecimal(id);' onblur='CheckDecimalFormat(id);'  data-required='false' maxlength='10' placeholder='0.00' /> </td> ";
                pTableHTML += "       <td class='AmountDue'> <input style='width:60%;' type='text' id='txtOfficialAllocationItemAmountDue" + item.ID + "' class=' controlStyle' onchange='OfficialAllocationCalcTotal();' onkeypress='CheckValueIsDecimal(id);' onfocus='CheckValueIsDecimal(id);' onblur='CheckDecimalFormat(id);CalcTotal();'  data-required='false' maxlength='10' placeholder='0.00' /> </td> ";
                pTableHTML += "       <td class='PaidAmount'>" + item.OfficialAmountPaid.toFixed(4) + "</td> ";
                pTableHTML += "       <td class='RemainingAmount'>" + (item.CostAmount.toFixed(4) - item.OfficialAmountPaid.toFixed(4)) + "</td> ";
                //pTableHTML += "       <td class='BalanceCurrency '> <select disabled='disabled' value=" + $('#slCurrency').val() + " id='slBalanceCurrency" + item.ID + "' class='controlStyle' onfocus='DisableBackspaceKey(id);' onkeydown='DisableBackspaceKey(id);' onkeypress='DisableBackspaceKey(id);' onchange='ARAllocation_Row_SetExchangeRate(" + item.ID + ");' data-required='false'><option selected>" + $('#slCurrency option:selected').text() + "</option></select> </td> ";
                //pTableHTML += "       <td class='ExchangeRate'><input disabled='disabled' style='width:60%;' type='text' id='txtItemExchangeRate" + item.ID + "' class='InvoiceExchangeRate controlStyle' data-type='float' onkeypress='CheckValueIsDecimal(id);' onfocus='CheckValueIsDecimal(id);' onchange='ARAllocation_ReCalculate();' onblur='CheckDecimalFormat(id);'  data-required='true' maxlength='10' placeholder='0.00' " + ($("#hDefaultCurrencyID").val() == item.CurrencyID ? "disabled" : "") + " /> </td> ";

                pTableHTML += " </tr> ";
            }

        });

    pTableHTML += "</tbody>";
    $("#tblOfficialAllocationItem").html(pTableHTML);
    //to fill the controls after creating them in the previous loop
    $.each(pTable, function (i, item) {
        $("#txtItemExchangeRate" + item.ID).val("1");

        //debugger;
        //FillListFromObject(null, 6/*pCodeOrName*/, null, "slBalanceCurrency" + item.ID, $('#slCurrency').val()
        //    , function () {
        //        ARAllocation_Row_SetExchangeRate(item.ID);
        //    });
    });

    debugger;
    BindAllCheckboxonTable("tblOfficialAllocationItem", "SelectInvoice", "CbOfficialAllocationItemID");
    CheckAllCheckboxInvoice("OfficialAllocationItemHeaderID");
    $('#lblOfficialTotalAllocation').html(' ');
    HighlightText("#tblOfficialAllocationItem>tbody>tr", $("#txtSearchOfficialAllocationItems").val().trim());
}

function OfficialAllocation_PaidAll(pID, pAmount) {
    debugger;

    if ($("#tblOfficialAllocationItem tbody tr[ID=" + pID + "]").find("td.ID").find('input:checkbox').prop('checked'))
        $('#txtOfficialAllocationItemAmountDue' + pID).val(pAmount);
    else
        $('#txtOfficialAllocationItemAmountDue' + pID).val(0);


    OfficialAllocationCalcTotal();

}
function OfficialAllocationCalcTotal() {


    $('#lblTotalOfficialAllocation').html(' ');

    var Total = 0;
    var Cost = 0;
    $('#tblOfficialAllocationItem  > tbody > tr').each(function () {
        if ($(this).find('td.AmountDue').find('input:text').val() != '') {
            Cost = $(this).find('td.AmountDue').find('input:text').val();
            Total += parseFloat(Cost);
        }



    });

    $('#lblTotalOfficialAllocation').append(Total.toFixed(2));
    $('#lblTotalOfficialAllocation').append(' ');



}
         
function OfficialAllocation_Save(pPrint) {
    debugger;
    //*************
    swal({
        title: "Are you sure  ?",
        text: "You will Pay Cash and Allocate selected Payables ",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "OK , Cash and Allocate Payables !",
        cancelButtonText: "NO",
        closeOnConfirm: true
    },
        function (isConfirm) {

            if (isConfirm) {

                var strReturnedMessage = OfficialAllocation_Validate();
                if (strReturnedMessage != "") //there is a validation error
                {
                    swal("Sorry", strReturnedMessage);
                    if (($("#slOfficialAllocationCostCenter").val() == "0" || $("#slOfficialAllocationCostCenter").val() == "") && CostCenterType == "true")
                        $('#slOfficialAllocationCostCenter').addClass('validation-error');
                }
                    
                else { //Gather data to send to controller to save

                    //***************** Details **************
                    $("#slAccount").val(_AccountID);
                    $("#slSubAccount").html("<option value='" + _SubAccountID + "'>Partener SubAccount</option>")

                    var CostCenterID = $("#slOfficialAllocationCostCenter").val();

                    if (CostCenterID != 0)
                        $("#slCostCenter").val(CostCenterID);

                    $("#txtValue").val(_OfficialAllocationTotalAmount);
                    $("#slSubAccount").val(_SubAccountID);
                    //if (glbFormCalled == 20)//Cash Issue Receipt
                     $("#txtDescription").val( "Cash Allocation : " + _DetailsOperations);
        
                    $("#lblTotal").html("<span> : </span><span>" + parseFloat(_OfficialAllocationTotalAmount).toFixed(4) + "</span>");
                    $("#lblTotalAfterTax").html("<span> : </span><span>" + parseFloat(_OfficialAllocationTotalAmount).toFixed(4) + "</span>");

                    debugger;
                    if (glbFormCalled == 10)//Cash Receiving Receipt
                        Details_Save(false, function () {

                            debugger;
                            var pAllocationItemsIDs = "";
                            var pAmounts = "";
                            var ptblOfficialAllocationItem = $("#tblOfficialAllocationItem tbody tr");
                            pAllocationItemsIDs = $("#tblOfficialAllocationItem tbody tr");
                            for (var i = 0; i < ptblOfficialAllocationItem.length; i++) {
                                //Fill Parameters from tbl controls here
                                var pRowID = ptblOfficialAllocationItem[i].id;
                                var tr = $("#tblOfficialAllocationItem tbody tr[ID=" + pRowID + "]");
                                if (parseFloat($("#txtOfficialAllocationItemAmountDue" + pRowID).val()) > 0 && $("#txtOfficialAllocationItemAmountDue" + pRowID).val() != "") {
                                    pAllocationItemsIDs += (pAllocationItemsIDs == "" ? (pRowID) : ("," + pRowID));
                                    pAmounts += (pAmounts == "" ? (parseFloat($("#txtOfficialAllocationItemAmountDue" + pRowID).val())) : ("," + parseFloat($("#txtOfficialAllocationItemAmountDue" + pRowID).val())));
                                }
                            }

   
                              jQuery("#OfficialAllocationModal").modal("hide");
                              jQuery("#VoucherModal").modal("hide");
                              console.log($("#hID").val());
                              debugger
                              InsertUpdateFunction3("/api/Voucher/InsertA_VoucherPayableClientPayment",
                                  SetArrayOfPayableItems()  //JSON.stringify(SetArrayOfItems())
                                  , false, null, function () {
                                      ClearAllModals();
                                      console.log("success InsertVoucherInvoicesPayment")
                                  });

                        }        );


                } // EOF else of strReturnedMessage == ""
            }
            else {

            }
        }
    );
    //*************
}
function OfficialAllocation_Validate() {
        debugger;
        //_Details = "";
        //_DetailsOperations = "";
        _OfficialAllocationTotalAmount = 0;
        var strReturnedMessage = "";
        //check empty or Zero exchange rate AND AmountDue greater than Remaining Amount
        var pInvoice = $("#tblOfficialAllocationItem tbody tr");
        for (var i = 0; i < pInvoice.length; i++) {
            var pRowID = pInvoice[i].id;
            if ( ($("#txtOfficialAllocationItemAmountDue" + pRowID).val() != "" && parseFloat($("#txtOfficialAllocationItemAmountDue" + pRowID).val()) > parseFloat($("#tblOfficialAllocationItem tbody tr[ID=" + pRowID + "]").find('td.RemainingAmount').text())) //check not to allocate more than remaining amount
            ) {
                strReturnedMessage = "Please, check not to allocate more than remaining amount " + $("#tblOfficialAllocationItem tbody tr[ID=" + pRowID + "]").find('td.InvoiceNumber').text() + ".";
            }
            //----------------------------------------------------------------------
            if ($("#txtOfficialAllocationItemAmountDue" + pRowID).val() != "") {
                _OfficialAllocationTotalAmount += $("#txtOfficialAllocationItemAmountDue" + pRowID).val() * 1

            }
            //-----------------------------------------------------------------------


        } // of For Loop
        //check each balance amount is not exceeded in allocation
        //if (strReturnedMessage == "") {
        //    var pPartnerBalance = $("#tblPartnerBalance tbody tr");
        //    for (var i = 0; i < pPartnerBalance.length; i++) {
        //        var pBalanceCurrencyID = pPartnerBalance[i].id;
        //        var pAvailableCurrencyBalance = $("#tblPartnerBalance tbody tr[ID=" + pBalanceCurrencyID + "]").find("td.AvailableBalance").text();
        //        var pAmountDueCurrencyBalance = $("#tblPartnerBalance tbody tr[ID=" + pBalanceCurrencyID + "]").find("td.AmountDue").text();
        //        if (parseFloat(pAvailableCurrencyBalance) < parseFloat(pAmountDueCurrencyBalance))
        //            strReturnedMessage = "Amount to be paid from the " + $("#tblPartnerBalance tbody tr[ID=" + pBalanceCurrencyID + "]").find("td.CurrencyID").text() + " balance exceeds the available limit.";
        //    }
        //}
        debugger;
        //if ($("#txt-SearchInvoiceNo").val() != "") {
        //    var TotalInvoice = 0.0;
        //    var TotalPaid = 0.0;
        //    var TotalRemaining = 0.0;

        //    $("#tblAllocationItem tbody tr").each(function (i, tr) {
        //        var pRowID = $(tr).attr("id");
        //        if ($("#txt-SearchInvoiceNo").val() == $(tr).find("td.InvoiceNumber").html()) {
        //            TotalInvoice += parseFloat($(tr).find("td.Amount").html());
        //            TotalPaid += parseFloat($("#txtItemAmountDue" + pRowID).val());
        //            TotalRemaining += parseFloat($(tr).find("td.RemainingAmount").html()) - parseFloat($("#txtItemAmountDue" + pRowID).val());
        //        }


        //    });
        //    _Details += ($("#txt-SearchInvoiceNo").val()
        //                        + " Total : " + TotalInvoice
        //                        + " [ " + " Paid " + TotalPaid
        //                        + " Remaining Amount: " + (TotalRemaining) * 1 + " " + " ] " + ",")

        //    _DetailsOperations += ($("#txt-SearchInvoiceNo").val()
        //                        + " Total : " + TotalInvoice
        //                        + " [ " + " Paid " + TotalPaid
        //                        + " Remaining Amount: " + (TotalRemaining) * 1 + " " + " ] " + ",")

        //}
        //else
        //{
        //    $("#tblAllocationItem tbody tr").each(function (i, tr) {
        //        var pRowID = $(tr).attr("id");
        //        if ($("#txtItemAmountDue" + pRowID).val() != "") {
        //            // _DetailsOperations += ($(tr).find("td.Operation").html() + " [ " + $("#txtItemAmountDue" + pRowID).val() * 1 + " " + $(tr).find("td.CurrencyID").html() + " ] ,")
        //            _DetailsOperations += " Invoice No " + ($(tr).find("td.InvoiceNumber").html()
        //                + " Total : " + $(tr).find("td.Amount").html()
        //                + " [ " + " Paid "
        //                + $("#txtItemAmountDue" + pRowID).val() * 1 + " " + $(tr).find("td.CurrencyID").html()
        //                + " Remaining Amount: "
        //                + (parseFloat($(tr).find("td.RemainingAmount").html()) - parseFloat($("#txtItemAmountDue" + pRowID).val())) * 1
        //                + " " + " ] " + ",")
        //            if ($("#hDefaultUnEditableCompanyName").val() == "SAF") {
        //                _Details += ($(tr).find("td.InvoiceNumber").html()
        //                  + " Total : " + $(tr).find("td.Amount").html()
        //                  + " [ " + " Paid " + $("#txtItemAmountDue" + pRowID).val() * 1 + " " + $(tr).find("td.CurrencyID").html()
        //                  + " Remaining Amount: " + (parseFloat($(tr).find("td.RemainingAmount").html()) - parseFloat($("#txtItemAmountDue" + pRowID).val())) * 1
        //                  + " " + " ] " + ",")
        //            }
        //            else
        //                _Details += ($(tr).find("td.InvoiceNumber").html() + " [ " + $("#txtItemAmountDue" + pRowID).val() * 1 + " " + $(tr).find("td.CurrencyID").html() + " ] ,")
        //        }

        //    });
        //}

        if (($("#slOfficialAllocationCostCenter").val() == "0" || $("#slOfficialAllocationCostCenter").val() == "") && CostCenterType == "true") {
            strReturnedMessage = 'Choose Cost Center';
        }


        return strReturnedMessage;
    }
function SetArrayOfPayableItems() {
    // var cobjItem = null;
    var arrayOfItems = new Array();
    var ind = -1;
    $("#tblOfficialAllocationItem tbody tr").each(function (i, tr) {
        var pRowID = $(tr).attr("id");
        debugger;
        if ($("#txtOfficialAllocationItemAmountDue" + pRowID).val().trim() != "") {
            var objItem = new Object();
            objItem.VoucherID = $('#hID').val();
            objItem.PayableID = pRowID;
            objItem.DueAmount = $("#txtOfficialAllocationItemAmountDue" + pRowID).val();
            objItem.CurrencyID = $(tr).find("td.CurrencyID").attr("val");
            objItem.VoucherTypeID = glbFormCalled;

            ind++;

            if (objItem.DueAmount > 0)
                arrayOfItems.push(objItem);
        }
    });
    return arrayOfItems;
}
function ClearA_PaymentRequestID_ForVoucherID()
{
    A_PaymentRequestID_ForVoucherID = 0;
}


// JOB SEARCH BY AHMED MAHER
function JobSearch() {
    debugger;
    if ($("#txtExchangeRate").val() == 0)
        swal("Sorry", "Exchange rate can not be 0.");
    else if (ValidateForm("form", "VoucherModal")) {
        ClearAll("#div-JobModel");
        jQuery("#div-JobModel").modal("show");
        PortJob_Inti();
    }
}
function PortJob_Inti() {
    strLoadWithPagingFunctionName = "/api/DAS_DisbursementJobs/Jobs_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned";

    var pWhereClause = " WHERE 1=1";
    var pOrderBy = "JobNumber";
    var pPageNumber = 1;
    var pPageSize = 10;
    var pControllerParameters = { pIsLoadArrayOfObjects: true, pLanguage: $("[id$='hf_ChangeLanguage']").val(), pPageNumber: pPageNumber, pPageSize: pPageSize, pWhereClause: pWhereClause, pOrderBy: pOrderBy }

    LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned("div-Pager1", "select-page-size1", "spn-first-page-row1", "spn-last-page-row1", "spn-total-count1", "div-Text-Total1", strLoadWithPagingFunctionName, pWhereClause, pOrderBy, pPageNumber, pPageSize, pControllerParameters
                 , function (pData) {
                     PortJob_BindTableRows(JSON.parse(pData[0]));
                     FillListFromObject(null, 16/*pName*/, "Select Vessel", "SlVessel_IDS", pData[3], null);
                     FillListFromObject(null, 2/*pName*/, "Select Client", "SlClient", pData[2], null);
                     FillListFromObject(null, 18/*pName*/, "Select Port Name", "SlPortName", pData[4], null);
                 });
}

function PortJob_BindTableRows(pTableRows) {


    debugger;
    $("#hl-menu-DASJobs").parent().addClass("active");
    ClearAllTableRows("tblJobs");
    var printControlsText = " class='btn btn-xs btn-rounded btn-info float-right' > <i class='fa fa-print' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + TranslateString("Print") + "</span>";
    $.each(pTableRows, function (i, item) {
        AppendRowtoTable("tblJobs",
                    ("<tr ID='" + item.DisbursementJob_ID + "' ondblclick='PortJob_EditByDblClick(" + item.DisbursementJob_ID + ");'>"

                    + "<td class='DisbursementJob_ID hide'> <input name='Delete' id='" + item.DisbursementJob_ID + "' type='checkbox' value='" + item.DisbursementJob_ID + "' onfocus='DisableEnterKey(id);' onkeypress='DisableEnterKey(id);'/></td>"
                    + "<td class='InquiryNumber hide' style=text-transform:uppercase' >" + item.InquiryNumber + "</td>"
                     + "<td class='JobNumber' style=text-transform:uppercase' >" + item.JobNumber + "</td>"
                    + "<td class='IssueDate' style=text-transform:uppercase' >" + ConvertDateFormat(GetDateWithFormatMDY(item.IssueDate)) + "</td>"
                    + "<td class='OwnerName' style=text-transform:uppercase' >" + item.OwnerName + "</td>"
                    + "<td class='Vessel_Name text-overflow-150' title='" + item.Vessel_Name + "'>" + item.Vessel_Name + "</td>"
                    + "<td class='ETA_PortName' style=text-transform:uppercase' >" + item.ETA_PortName + "</td>"
                  // + "<td class='Vessel_Name' style=text-transform:uppercase' >" + item.Vessel_Name + "</td>"
                    + "<td class='JobType' style=text-transform:uppercase' >" + item.JobType + "</td>"
                    + "<td class='Voyage hide' style=text-transform:uppercase' >" + item.VoyageNumber + "</td>"
                    + "<td class='CharterName hide' style=text-transform:uppercase' >" + item.CharterName + "</td>"
                    + "<td class='NominatoreName hide' style=text-transform:uppercase' >" + item.NominatoreName + "</td>"
                    + "<td class='CargoOperatorName hide' style=text-transform:uppercase' >" + item.CargoOperatorName + "</td>"
                    //+ "<td class=''><a href='#InvoiceTemplateModalCopy' data-toggle='modal' onclick='CopyTemplate(" + item.ID + "," + item.IsDefault + "," + item.PortID + "," + item.IsClosed + ", " + item.InvoiceTypeID + ");' " + " class='btn btn-xs btn-rounded btn-lightblue' title='Edit'> <i class='fa fa-pencil' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + TranslateString("Copy") + "</span>" + "</a></td>"

                    ///////////////sherif
                    //+ "<td class=''><a href='#' data-toggle='modal' onclick='InvoiceTemplate_Print(" + item.ID + ");' " + printControlsText + "</a></td>"
        + "</tr>"));
    });
    ApplyPermissions();
    BindAllCheckboxonTable("tblJobs", "ID", "cb-CheckAll");
    CheckAllCheckbox("HeaderDeleteJobID");
    //HighlightText("#tblJobs>tbody>tr", $("#txt-Search").val().trim());
    if (showDeleteFailMessage) {//sherif: if deletion failed due to foreign key, then this message is shown
        swal(strSorry, strDeleteFailMessage, "warning");
        showDeleteFailMessage = false;
    }
    $("#hl-homepage").on("click", function () { LoadViews("hl-homepage"); });
}
function PortJob_EditByDblClick(jobID) {
    debugger;
    $("#slDisbursementJobs").val(jobID);
    //$.getJSON("/api/Voucher/GetListJobNotes", { pWhereClause: jobID }, function (Result) {
    //    if (Result) {
    //        debugger;
    //        $("#txtNotes").val(Result[0]);

    //    }

    //});
    jQuery("#div-JobModel").modal("hide");
}
function portJobs_LoadingWithPaging() {
    debugger;
    strLoadWithPagingFunctionName = "/api/DAS_DisbursementJobs/Jobs_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned";

    var pWhereClause = PortJob_GetWhereClause();
    var pOrderBy = "Estimation_ID";
    var pPageNumber = 1;
    var pPageSize = 10;
    var pControllerParameters = { pIsLoadArrayOfObjects: true, pLanguage: $("[id$='hf_ChangeLanguage']").val(), pPageNumber: pPageNumber, pPageSize: pPageSize, pWhereClause: pWhereClause, pOrderBy: pOrderBy }

    LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned("div-Pager1", "select-page-size1", "spn-first-page-row1", "spn-last-page-row1", "spn-total-count1", "div-Text-Total1", strLoadWithPagingFunctionName, pWhereClause, pOrderBy, pPageNumber, pPageSize, pControllerParameters
                 , function (pData) {
                     PortJob_BindTableRows(JSON.parse(pData[0]));
                 });
}

function PortJob_GetWhereClause() {
    debugger;
    var pWhereClause = " WHERE 1=1 and IsClosed=0  ";
    // var pWhereClause = " WHERE 1=1 and JobType='P' and IsClosed=0  ";
    if ($("#txJobNumberSrch").val().trim() != "") {
        pWhereClause += " and DAS_vwDisbursementJobs.JobNumber like N'%" + $("#txJobNumberSrch").val().trim() + "%' ";
    }
    if ($("#txtJobTypeSrch").val().trim() != "") {
        pWhereClause += " and DAS_vwDisbursementJobs.JobType like N'%" + $("#txtJobTypeSrch").val().trim() + "%' ";
    }
    if ($("#SlVessel_IDS").val() != 0) {
        pWhereClause += " and DAS_vwDisbursementJobs.Vessel_ID=" + $("#SlVessel_IDS").val();
    }
    if ($("#SlClient").val() != 0) {
        pWhereClause += " and DAS_vwDisbursementJobs.OwnerID=" + $("#SlClient").val();
    }
    if ($("#SlPortName").val() != 0) {
        pWhereClause += " and DAS_vwDisbursementJobs.ETA_Port_ID=" + $("#SlPortName").val();
    }
    if ($("#cbIsDateFilter").prop('checked') == true) {
        if ($("#txtFromDate").val().trim() != "" && $("#txtToDate").val().trim() != "") {

            pWhereClause += " and cast( DAS_vwDisbursementJobs.IssueDate as date) between '" + ConvertDateFormat($("#txtFromDate").val()) + "' and ";
            pWhereClause += "  '" + ConvertDateFormat($("#txtToDate").val()) + "' ";
        }
    }

    return pWhereClause;
}

function PortBtnSearchJob_GetWhereClause() {
    debugger;
    var pWhereClause = " WHERE 1=1 and IsClosed=0  ";
    // var pWhereClause = " WHERE 1=1 and JobType='P' and IsClosed=0  ";
    if ($("#txJobNumberSrch").val().trim() != "") {
        pWhereClause += " and DAS_vwDisbursementJobs.txJobNumberSrch like N'%" + $("#txJobNumberSrch").val().trim() + "%' ";
    }
    if ($("#txtJobTypeSrch").val().trim() != "") {
        pWhereClause += " and DAS_vwDisbursementJobs.JobType like N'%" + $("#txtJobTypeSrch").val().trim() + "%' ";
    }
    if ($("#SlVessel_IDS").val() != 0) {
        pWhereClause += " and DAS_vwDisbursementJobs.Vessel_ID=" + $("#SlVessel_IDS").val();
    }
    if ($("#SlClient").val() != 0) {
        pWhereClause += " and DAS_vwDisbursementJobs.OwnerID=" + $("#SlClient").val();
    }
    if ($("#SlPortName").val() != 0) {
        pWhereClause += " and DAS_vwDisbursementJobs.ETA_Port_ID=" + $("#SlPortName").val();
    }
    if ($("#cbIsDateFilter").prop('checked') == true) {
        if ($("#txtFromDate").val().trim() != "" && $("#txtToDate").val().trim() != "") {

            pWhereClause += " and cast( DAS_vwDisbursementJobs.IssueDate as date) between '" + ConvertDateFormat($("#txtFromDate").val()) + "' and ";
            pWhereClause += "  '" + ConvertDateFormat($("#txtToDate").val()) + "' ";
        }
    }

    return pWhereClause;
}

function PortJob_ClearAllControls() {
    debugger;
    ClearAll("#JobModal");
    $("#liJob").siblings().removeClass("active");
    $("#liJob").addClass("active");
    $("#divJob").siblings().removeClass("active");
    $("#divJob").addClass("active");

    $("#btnSave").attr("onclick", "PortJob_Insert();");
    //$("#btnSaveandNew").attr("onclick", "ShippingOrder_Insert(true);");
}

function PortJobSearch_LoadingWithPaging(pPageNo, pPageSize) {
    debugger;


    var pWhereClause = PortBtnSearchJob_GetWhereClause();
    var pPageSize = $('#select-page-size1').val();
    var pPageNumber = pPageNo;
    var pOrderBy;
    pOrderBy = "JobNumber";

    var pControllerParameters = { pIsLoadArrayOfObjects: true, pLanguage: $("[id$='hf_ChangeLanguage']").val(), pPageNumber: pPageNo, pPageSize: pPageSize, pWhereClause: pWhereClause, pOrderBy: pOrderBy }
    LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned("div-Pager1", "select-page-size1", "spn-first-page-row1", "spn-last-page-row1", "spn-total-count1", "div-Text-Total1", strLoadWithPagingFunctionName, pWhereClause, pOrderBy, pPageNumber, pPageSize, pControllerParameters, function (pData) { PortJob_BindTableRows(JSON.parse(pData[0])); });

}
$('input[type=checkbox][name=cbIsDateFilter]').change(function () {
    debugger;
    if ($(this).prop('checked') == true) {
        $("#txtFromDate").removeAttr("disabled");
        $("#txtToDate").removeAttr("disabled");
    }
    else {
        $("#txtFromDate").attr("disabled", "disabled");
        $("#txtToDate").attr("disabled", "disabled");
    }
});

function PortJob_BtnSearchInti() {
    strLoadWithPagingFunctionName = "/api/DAS_DisbursementJobs/Jobs_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned";

    var pWhereClause = " WHERE 1=1";
    var pOrderBy = "JobNumber";
    var pPageNumber = 1;
    var pPageSize = 10;
    var pControllerParameters = { pIsLoadArrayOfObjects: true, pLanguage: $("[id$='hf_ChangeLanguage']").val(), pPageNumber: pPageNumber, pPageSize: pPageSize, pWhereClause: pWhereClause, pOrderBy: pOrderBy }

    LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned("div-Pager1", "select-page-size1", "spn-first-page-row1", "spn-last-page-row1", "spn-total-count1", "div-Text-Total1", strLoadWithPagingFunctionName, pWhereClause, pOrderBy, pPageNumber, pPageSize, pControllerParameters
                 , function (pData) {
                     PortJob_BindTableRows(JSON.parse(pData[0]));
                     FillListFromObject(null, 16/*pName*/, "Select Vessel", "SlVessel_IDS", pData[3], null);
                     FillListFromObject(null, 2/*pName*/, "Select Client", "SlClient", pData[2], null);
                     FillListFromObject(null, 18/*pName*/, "Select Port Name", "SlPortName", pData[4], null);
                 });
}
var SearchText = "";
var currentID = "";
var lastInputTimeStamp;
$(document).ready(function () {
    //Webcam.set({
    //    width: 320,
    //    height: 240,
    //    image_format: 'jpeg',
    //    jpeg_quality: 90
    //});
    //Webcam.attach('#webcam');
    //$("#btnCapture").click(function () {
    //    Webcam.snap(function (data_uri) {
    //        $("#imgCapture")[0].src = data_uri;
    //        $("#btnUpload").removeAttr("disabled");
    //    });
    //});
    //$("#btnUpload").click(function () {
    //    $.ajax({
    //        type: "POST",
    //        url: "/Home/SaveCapture",
    //        data: "{data: '" + $("#imgCapture")[0].src + "'}",
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "json",
    //        success: function (r) { }
    //    });
    //});

    $(document).on('keyup', 'input.select2-search__field', function (e) {
        debugger;

        if (currentID != this.parentElement.nextElementSibling.children[0].id) {
            currentID = this.parentElement.nextElementSibling.children[0].id;
            SearchText = "";
        }



        if (lastInputTimeStamp != e.originalEvent.timeStamp) {
            if (e.keyCode == 8 || e.charCode == 8)  // backspace pressed
            {
                SearchText = SearchText.slice(0, -1)
            }
            else if (e.key.length == 1) {
                SearchText += e.key;
                //setTimeout(function () {
                //    // to prevent repeated inputs
                //}, 50);
            }
        }
        lastInputTimeStamp = e.originalEvent.timeStamp



    });
});

function ChangeOperation(pID) {
    debugger;
    if (SearchText !="") {
        //filterByText('slOperation', 'slOperation' + pID, SearchText);
        OperationID = $("#slOperation option[CodeSerial='" + SearchText + "']").val();
        $("#slOperation" + pID).html("");
        if (OperationID != undefined) {
            let option = $("#slOperation option[CodeSerial='" + SearchText + "']");
            $("#slOperation" + pID).append(option.clone());
            //$("#slOperation" + pID).append(option);
            $("#slOperation" + pID).val(OperationID);
            $("#slOperation" + pID).trigger("change");
        }

        SearchText = "";
    }
  

}
var isOperaion = 0;
function Details_HouseAndCertificateChangedByOperation(pRowID) {
    debugger;
    if (0 == 0) {
        //$("#slHouse" + pRowID).val(0);
        //$("#slCertificateNumber" + pRowID).val(0);
        //$("#slTruckingOrder" + pRowID).val(0);

        isOperaion = 1;

        Details_FillSlHouseAndCertificate("slHouse" + pRowID, pRowID, 1);
        Details_FillSlHouseAndCertificate("slTruckingOrder" + pRowID, pRowID, 2);
        Details_FillSlHouseAndCertificate("slBranch" + pRowID, pRowID, 3);


    }


}

function Details_FillSlHouseAndCertificate(pSlName, operationID, pType) {
    debugger;
    FadePageCover(true);

    CallGETFunctionWithParameters("/api/Voucher/FillCombo"
        , {
            PoperationID: $('#slOperation' + operationID + '').val() == "0" ? 0 : $('#slOperation' + operationID + '').val() == null ? 0 : $('#slOperation' + operationID + '').val()
            , ptype: pType
            , pOrderBy: "ID"
        }
        , function (pData) {
            debugger;
            //FillListFromObject(null, 2, "<--Select-->", pSlName, pData[0], null);
            if ($('#slOperation' + operationID + '').val() != "0")
            FillListFromObject_ERP($('#slOperation' + operationID + '').val() == "0" ? null : $("#" + pSlName).val(), 2/*pCodeOrName*/, TranslateString("SelectFromMenu"), pSlName, pData[0], null);
            //   if (pDefaults.UnEditableCompanyName != "FAI") {
            //Start Auto Filter
            $("#" + pSlName).css({ "width": "100%" }).select2();
            $("div[tabindex='-1']").removeAttr('tabindex');
            if (pData[0].replace("[]", "0") != "0") {
                //$('#slBranch' + operationID + '').val(JSON.parse(pData[0])[0].ID);
                //$('#slHouse' + operationID + '').val(SelectedID);

                //if (($("#" + pSlName).attr('tag') == undefined || $("#" + pSlName).attr('tag') == "") && SelectedID !=0) {
                //    $("#" + pSlName).val(SelectedID);
                //    SelectedID = 0;
                //}
                //else if (($("#" + pSlName).attr('tag') == undefined || $("#" + pSlName).attr('tag') == "") && SelectedID == 0) {
                //    $("#" + pSlName).val("0");
                //    if (pType == 3 && $('#slOperation' + operationID + '').val() != "0" && (JSON.parse(pData[0])[0].ID != "" || JSON.parse(pData[0])[0].ID != undefined || JSON.parse(pData[0])[0].ID!="0")) {
                //        $("#" + pSlName).val(JSON.parse(pData[0])[0].ID);
                //    }

                //}
                //else {
                //    $("#" + pSlName).val($("#" + pSlName).attr('tag') == undefined ? "0" : $("#" + pSlName).attr('tag') == "" ? "0" : $("#" + pSlName).attr('tag'));
                //}
                if (pType == 3 && $('#slOperation' + operationID + '').val() != "0" && (JSON.parse(pData[0])[0].ID != "" || JSON.parse(pData[0])[0].ID != undefined || JSON.parse(pData[0])[0].ID != "0")) {
                    $("#" + pSlName).val(JSON.parse(pData[0])[0].ID);
                }
                if ($("#" + pSlName).attr('tag') != undefined && $("#" + pSlName).attr('tag') != "") {
                    $("#" + pSlName).val($("#" + pSlName).attr('tag') == undefined ? "0" : $("#" + pSlName).attr('tag') == "" ? "0" : $("#" + pSlName).attr('tag'));
                }
               

                
                $("#" + pSlName).trigger("change");

            }
            else {
                $("#" + pSlName).val("0");
                $("#" + pSlName).trigger("change");

            }

            //  Esnd Auto Filter
            //    }
            FadePageCover(false);
        }
        , null);
}


var SelectedID = 0;
function Details_ChangedByHouseAndCertificateAndTrucingOrder(pRowID, pType) {
    debugger;
    if (check == 0 && $('#slOperation' + pRowID + '').val() == "0" && ($('#slHouse' + pRowID + '').val() != "0" || $('#slBranch' + pRowID + '').val() != "0" || $('#slTruckingOrder' + pRowID + '').val() != "0")) {
        FadePageCover(true);
        SelectedID = 0;
        if (pType == 1) {
            CallGETFunctionWithParameters("/api/Voucher/GetOperationID"
                , {
                    TransID: $('#slHouse' + pRowID + '').val()
                    , ptype: pType
                }
                , function (pData) {
                    if (pData[0] > 0) {
                        debugger;
                        $('#slOperation' + pRowID + '').val(JSON.parse(pData[0]));
                        $("#slOperation" + pRowID).trigger("change");
                        SelectedID = $('#slHouse' + pRowID + '').val();
                        Details_HouseAndCertificateChangedByOperation(pRowID);


                        //  $("#slHouse" + pRowID).val(0);
                        // $("#slTruckingOrder" + pRowID).val(0);
                    }

                    FadePageCover(false);
                }
                , null);


        }
        else if (pType == 2) {
            CallGETFunctionWithParameters("/api/Voucher/GetOperationID"
                , {
                    TransID: $('#slTruckingOrder' + pRowID + '').val()
                    , ptype: pType
                }
                , function (pData) {
                    if (pData[0] > 0) {
                        debugger;
                        $('#slOperation' + pRowID + '').val(JSON.parse(pData[0]));
                        $("#slOperation" + pRowID).trigger("change");
                        SelectedID = $('#slTruckingOrder' + pRowID + '').val();
                        Details_HouseAndCertificateChangedByOperation(pRowID);


                        //  $("#slHouse" + pRowID).val(0);
                        // $("#slTruckingOrder" + pRowID).val(0);
                    }

                    FadePageCover(false);
                }
                , null);


        }
            
        else if (pType == 3) {
            CallGETFunctionWithParameters("/api/Voucher/GetOperationID"
                , {
                    TransID: $('#slBranch' + pRowID + '').val()
                    , ptype: pType
                }
                , function (pData) {
                    if (pData[0] > 0) {
                        debugger;
                    //    $('#slOperation' + pRowID + '').val(JSON.parse(pData[0]));
                     //   $("#slOperation" + pRowID).trigger("change");
                        SelectedID = $('#slBranch' + pRowID + '').val();

                      //  Details_HouseAndCertificateChangedByOperation(pRowID);


                    }

                    FadePageCover(false);
                }
                , null);
        }
        else {
            FadePageCover(false);
        }


    }
}