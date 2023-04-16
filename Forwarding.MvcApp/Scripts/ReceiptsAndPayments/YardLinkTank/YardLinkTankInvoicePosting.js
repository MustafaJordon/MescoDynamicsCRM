﻿var maxIDInTable = 0; //used to for when adding new row then make td control names unique
function YardLinkTankInvoicePosting_BindTableRows(pTableRows) {
    debugger;
    $("#hl-menu-ShipLink").parent().addClass("active");
    ClearAllTableRows("tblYardLinkTankInvoicePosting");
    //editControlsText = " class='btn btn-xs btn-rounded btn-info float-right' > <i class='fa fa-pencil' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + TranslateString("Edit") + "</span>";
    $.each(pTableRows, function (i, item) {
        AppendRowtoTable("tblYardLinkTankInvoicePosting",
            //("<tr ID='" + item.ID + "' ondblclick='Voucher_FillControls(" + item.ID + ");'>"
            ("<tr ID='" + item.ID + "' class='" + (item.JVID1 == 0 ? "" : "text-primary") + "'" + ">"
                + "<td class='ID'> <input name='Delete' type='checkbox' value='" + item.ID + "' /></td>"
                + "<td class='InvoiceSerial'>" + (item.InvoiceSerial == 0 ? "" : item.InvoiceSerial) + "</td>"

                + "<td class='IssueDate'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.IssueDate)) < 1 ? "" : ConvertDateFormat(GetDateWithFormatMDY(item.IssueDate))) + "</td>"
                    //+ "<td class='PaymentDate'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.PaymentDate)) < 1 ? "" : ConvertDateFormat(GetDateWithFormatMDY(item.PaymentDate))) + "</td>"
                + "<td class='InvoiceTotal'>" + item.InvoiceTotal + "</td>"
                + "<td class='CurrencyCode'>" + (item.CurrencyCode == 0 ? "" : item.CurrencyCode) + "</td>"
                + "<td class='InvoiceTypeID'>" + item.InvoiceType + "</td>"
                + "<td class='CustomerName'>" + (item.CustomerName == 0 ? "" : item.CustomerName) + "</td>"
 
                    //+ "<td class='JobNo'>" + (item.JobNo == 0 ? "" : item.JobNo) + "</td>"

             //+ "<td class='hide'><a href='#VoucherModal' data-toggle='modal' onclick='Voucher_FillControls(" + item.ID + ");' " + editControlsText + "</a></td>"
            + "</tr>"));
    });
    //ApplyPermissions();
    BindAllCheckboxonTable("tblYardLinkTankInvoicePosting", "ID");
    CheckAllCheckbox("ID");
    //HighlightText("#tblJournalTypes>tbody>tr", $("#txt-Search").val().trim());
    if (showDeleteFailMessage) {//sherif: if deletion failed due to foreign key, then this message is shown
        swal(strSorry, strDeleteFailMessage);
        showDeleteFailMessage = false;
    }
    $("#hl-homepage").on("click", function () { LoadViews("hl-homepage"); });
}
function YardLinkTankInvoicePosting_LoadingWithPaging() {
    debugger;
    var pWhereClause = YardLinkTankInvoicePosting_GetWhereClause();
    var pPageSize = $('#select-page-size').val();
    var pPageNumber = 1;//($("#div-Pager li.active a").text() == "" ? 1 : $("#div-Pager li.active a").text());
    var pOrderBy = $("#cbIsJV1").prop("checked") ? "IssueDate" : "IssueDate";
    var pControllerParameters = { pIsLoadArrayOfObjects: false, pLanguage: $("[id$='hf_ChangeLanguage']").val(), pPageNumber: pPageNumber, pPageSize: pPageSize, pWhereClause: pWhereClause, pOrderBy: pOrderBy }
    LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned("div-Pager", "select-page-size", "spn-first-page-row", "spn-last-page-row", "spn-total-count", "div-Text-Total", strLoadWithPagingFunctionName, pWhereClause, pOrderBy, pPageNumber, pPageSize, pControllerParameters, function (pData) { YardLinkTankInvoicePosting_BindTableRows(JSON.parse(pData[0])); });
    //HighlightText("#tblYardLinkTankInvoicePosting>tbody>tr", $("#txt-Search").val().trim());
}
function YardLinkTankInvoicePosting_GetWhereClause() {
    debugger;


    var pWhereClause = "";
    //pWhereClause += 
    //     ("WHERE IssueDate >= '" + GetDateWithFormatyyyyMMdd($("#txtSearchFrom").val()) + "' AND IssueDate<='" + GetDateWithFormatyyyyMMdd($("#txtSearchTo").val()) + " 23:59:59'" + "\n")
    if ($("#cbIsApproved").prop("checked")) {
        pWhereClause += " where JVID IS NULL" + "\n";

    }
    else if ($("#cbIsUnApproved").prop("checked")) {
        pWhereClause += " where JVID IS not NULL" + "\n";

    }
    //  pWhereClause += " where JVID IS NULL" + "\n";

    //pWhereClause += ($("#hDefaultUnEditableCompanyName").val() != "ONE" ? " AND IsAudited=1 " : "");
    //if ($("#slInvoiceType").val() != 0)
    //    pWhereClause += " AND InvoiceTypeID=" + $("#slInvoiceType").val();

    if (isValidDate($("#txtSearchFrom").val().trim(), 1) && $("#txtSearchFrom").val().trim() != "")
        pWhereClause += " AND CONVERT(date,IssueDate,103) >= '" + GetDateWithFormatyyyyMMdd($("#txtSearchFrom").val().trim()) + "'" + " \n";
    if (isValidDate($("#txtSearchTo").val().trim(), 1) && $("#txtSearchTo").val().trim() != "")
        pWhereClause += " AND CONVERT(date,IssueDate,103) <= '" + GetDateWithFormatyyyyMMdd($("#txtSearchTo").val().trim()) + "'" + " \n";


    if ($("#txtSearchInvoiceNo").val().trim() != "")
        pWhereClause += " AND InvoiceSerial LIKE N'" + $("#txtSearchInvoiceNo").val().trim() + "%'" + "\n";
    //if ($("#txtSearchBillNo").val().trim() != "")
    //    pWhereClause += " AND BillNumber LIKE N'" + $("#txtSearchBillNo").val().trim() + "%'" + "\n";

    return pWhereClause;
}
function YardLinkTankInvoicePosting_LinkRevenueItems() {
    debugger;
    var pSelectedIDs = GetAllSelectedIDsAsString('tblYardLinkTankInvoicePosting', 'Delete');
    if (pSelectedIDs != "") {
        FadePageCover(true);
        CallGETFunctionWithParameters("/api/YardLinkTankInvoicePosting/GetRevenueItemsModalData"
            , {
                pSelectedIDsToGetRevenueItems: pSelectedIDs
            }
            , function (pData) {
                var pVoyageAccountID = pData[1];
                var pSubAccounts = pData[2];
                FillListFromObject_ERP(null, 4/*pCodeOrName*/, TranslateString("<--Select-->"), "slVoyageSubAccount", pSubAccounts, null);
                $("#slVoyageAccount").val(pVoyageAccountID);
                jQuery("#ShipLinkRevenueItemsModal").modal("show");
                ShipLinkRevenueItems_BindTableRows(JSON.parse(pData[0]));
                FadePageCover(false);
            }
            , null);
    }
}
function YardLinkTankInvoicePosting_Post(pIsSaveRevenueItems) {
    debugger;
    var isValid = true;
    var pPostedSelectedIDs = GetAllSelectedIDsAsString('tblYardLinkTankInvoicePosting', 'Delete');
    if (pPostedSelectedIDs != "") {
        FadePageCover(true);
        //if the Company is not ONEEgypt then pIsSaveRevenueItems will be false (set in mainapp.js)
        if (pIsSaveRevenueItems) { //then validate Revenue items before saving
            if (!ShipLinkRevenueItems_IsValid("ShipLinkRevenueItemsModal")) {
                isValid = false; FadePageCover(false);
            }
            else if (!ShipLinkRevenueItems_IsUniqueItems()) {
                isValid = false; FadePageCover(false);
            }
        }
        if (isValid) {
      
           
            CallGETFunctionWithParameters("/api/YardLinkTankInvoicePosting/Post"
                , {
                    pPostedSelectedIDs: pPostedSelectedIDs
                    , pPost: $("#cbIsApproved").prop("checked") ? true : false
                   
                }
                , function (pData) {
                    if (pData[0] == "") {
                        swal("Success", "Saved successfully");
                    }
                    else {
                        swal("Sorry", pData[0]);
                    }
                    jQuery("#ShipLinkRevenueItemsModal").modal("hide");
                    YardLinkTankInvoicePosting_LoadingWithPaging();
                    //FadePageCover(false); //done in loadwithpaging
                }
                , null);
        }
    }
}


function HideShowButtons() {
    if ($("#cbIsApproved").prop("checked") == true) {
        $("#btn-Post").removeClass("hide");
        $("#btn-UNPost").addClass("hide");


    }
    else {
        $("#btn-UNPost").removeClass("hide");
        $("#btn-Post").addClass("hide");

    }
}
