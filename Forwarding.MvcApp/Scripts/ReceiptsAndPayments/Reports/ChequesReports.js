﻿function ChequesReports_Print(pOutputTo) {
    debugger;
    if (
        ($("#txtFromDate").val().trim() == "" || isValidDate($("#txtFromDate").val(), 1))
        && ($("#txtToDate").val().trim() == "" || isValidDate($("#txtToDate").val(), 1))
        ) {
        FadePageCover(true);
        var pWhereClause = ChequesReports_GetFilterWhereClause();
        //if ($('#ulReportTypes .active').val() == 0)
        //    swal(strSorry, "Please, Select a report type.");
        //else {
        var pParametersWithValues = {
            pWhereClause: pWhereClause
            //, pDirectionType: ($("#lbl-filter-import").hasClass('active') ? "Import"
            //    : $("#lbl-filter-export").hasClass('active') ? "Export"
            //    : $("#lbl-filter-domestic").hasClass('active') ? "Domestic"
            //    : "ALL")
            //, pTransPortType: ($("#lbl-filter-ocean").hasClass('active') ? "Ocean"
            //    : $("#lbl-filter-air").hasClass('active') ? "Air"
            //    : $("#lbl-filter-inland").hasClass('active') ? "Inland"
            //    : "ALL")
        };
        CallGETFunctionWithParameters("/api/ChequesStatueReports/LoadData"
            , pParametersWithValues
            , function (data) {
                debugger;
                if (data[0]) //pRecordsExist
                    ChequesReports_DrawReport(data, pOutputTo);
                else
                    swal(strSorry, "No records are found for that search criteria.");
                FadePageCover(false);
            });
        //}
    }
    else
        swal(strSorry, "Please make sure that date format is dd/MM/yyyy.");
}

function ChequesReports_GetFilterWhereClause() {
    var pWhereClause = "";
    var pSalesmanFilter = "";
    var pBranchFilter = "";
    var pBankAccountFilter = "";
    var pPRTypeFilter = "";
    var pPartnerTypeFilter = "";
    var pPartnerFilter = "";
    var pCurrencyFilter = "";
    var pFromDateFilter = "";
    var pToDateFilter = "";

    //if ($("#slChequeStatus").val() == 10) //Approved
    //    pWhereClause += "WHERE IsApproved=1 ";
    //else if ($("#slChequeStatus").val() == 20) //UnderCollect
    //    pWhereClause += " WHERE IsApproved=0 ";
     if ($("#slChequeStatus").val() == 30) //Refused
         pWhereClause += " WHERE UnderCollection=1 and  Collected=0 and Returned=0";
    else if ($("#slChequeStatus").val() == 40) //Refused
        pWhereClause += " WHERE Collected=1 ";
    else if ($("#slChequeStatus").val() == 50) //Refused
        pWhereClause += " WHERE Returned=1 ";

    //pBranchFilter = ($("#slBranch").val() == "" ? "" : " BranchID = " + $("#slBranch").val());
    //if (pBranchFilter != "" && pWhereClause != "")
    //    pWhereClause += " AND " + pBranchFilter;
    //else
    //    if (pBranchFilter != "" && pWhereClause == "")
    //        pWhereClause += " WHERE " + pBranchFilter;
    debugger;
    pBankAccountFilter = ($("#slBankAccount").val() == "" ? "" : $("#slBankAccount").val() == 0 ? "" : " BankID = " + $("#slBankAccount").val());
    if (pBankAccountFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pBankAccountFilter;
    else
        if (pBankAccountFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pBankAccountFilter;
    debugger;
    pPRTypeFilter = ($("#slPRType").val() == "" ? "" : " VoucherType = " + $("#slPRType").val());
    if (pPRTypeFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pPRTypeFilter;
    else
        if (pPRTypeFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pPRTypeFilter;

    //pPartnerFilter = ($("#slPartner").val() == "" ? "" : (" PartnerID = " + $("#slPartner").val()));
    //if (pPartnerFilter != "" && pWhereClause != "")
    //    pWhereClause += " AND " + pPartnerFilter;
    //else
    //    if (pPartnerFilter != "" && pWhereClause == "")
    //        pWhereClause += " WHERE " + pPartnerFilter;

    //pPartnerTypeFilter = ($("#slPartnerType").val() == "" ? "" : (" PartnerTypeID = " + $("#slPartnerType").val()));
    //if (pPartnerTypeFilter != "" && pWhereClause != "")
    //    pWhereClause += " AND " + pPartnerTypeFilter;
    //else
    //    if (pPartnerTypeFilter != "" && pWhereClause == "")
    //        pWhereClause += " WHERE " + pPartnerTypeFilter;

    pCurrencyFilter = ($("#slCurrency").val() == "" ? "" : " CurrencyID = " + $("#slCurrency").val());
    if (pCurrencyFilter != "" && pWhereClause != "")
        pWhereClause += " AND " + pCurrencyFilter;
    else
        if (pCurrencyFilter != "" && pWhereClause == "")
            pWhereClause += " WHERE " + pCurrencyFilter;

    //2nd parameter in isValidDate: 1-dd/mm/yyyy 2-mm/dd/yyyy
    if (isValidDate($("#txtFromDate").val().trim(), 1) && $("#txtFromDate").val().trim() != "") {
        pFromDateFilter = " CAST(ChequeDate AS date) >= '" + GetDateWithFormatyyyyMMdd($("#txtFromDate").val().trim()) + "'";
        if (pFromDateFilter != "" && pWhereClause != "")
            pWhereClause += " AND " + pFromDateFilter;
        else
            if (pFromDateFilter != "" && pWhereClause == "")
                pWhereClause += " WHERE " + pFromDateFilter;
    }

    //2nd parameter in isValidDate: 1-dd/mm/yyyy 2-mm/dd/yyyy
    if (isValidDate($("#txtToDate").val().trim(), 1) && $("#txtToDate").val().trim() != "") {
        pToDateFilter = " CAST(ChequeDate AS date) <= '" + GetDateWithFormatyyyyMMdd($("#txtToDate").val().trim()) + "'";
        if (pToDateFilter != "" && pWhereClause != "")
            pWhereClause += " AND " + pToDateFilter;
        else
            if (pToDateFilter != "" && pWhereClause == "")
                pWhereClause += " WHERE " + pToDateFilter;
    }

    pWhereClause = (pWhereClause == "" ? " WHERE (1=1) " : pWhereClause) + " ORDER BY ID ";
    //pWhereClause = (pWhereClause == "" ? " WHERE (1=1) " : pWhereClause) + " AND IsConversionRow=0 ORDER BY ID ";

    return pWhereClause;
}

function ChequesReports_PartnerTypeChanged() {
    //debugger;
    //$("#slPartner").val("");
    //$("#slPartner option").removeClass("hide");
    //if ($("#slPartnerType").val() != "") //handle show all partners
    //    $("#slPartner option[PartnerTypeID!=" + $("#slPartnerType").val() + "][value!=''" + "]").addClass("hide");
    debugger;
    $("#slPartner").html("<option value=''><--All--></option>");//to quickly empty
    if ($("#slPartnerType").val() != "") {
        FadePageCover(true);
        CallGETFunctionWithParameters("/api/ChequesStatueReports/FillPartners", { pPartnerTypeID: $("#slPartnerType").val() }
            , function (pData) {
                FillListFromObject(null, 2/*pCodeOrName*/, "<--All-->", "slPartner", pData[0], null);
                FadePageCover(false);
            }
            , null);
    }
}

function ChequesReports_DrawReport(data, pOutputTo) {
    debugger;
    var pReportRows = JSON.parse(data[1]);
    var pTotalSummary = data[2];
    if ($("[id$='hf_ChangeLanguage']").val() == "en") {
        var pReportTitle = "Cheques Report";
    }
    else {
        var pReportTitle = "تقرير الشيكات";
    }
    var TodaysDateddMMyyyy = getTodaysDateInddMMyyyyFormat();
    var pTablesHTML = "";
    //pTablesHTML += '                         <table id="tblChequesReports" class="table table-striped text-sm table-hover b-t b-r b-b b-l ">';//remove t1 class to remove row numbers
    pTablesHTML += '                         <table id="tblChequesReports" class="table table-striped text-sm table-bordered print " style="border:solid #999 !important;">';//remove t1 class to remove row numbers
    pTablesHTML += '                             <thead>';
    ReportHTML += '                                 <tr class="" style="background-color:whitesmoke;font-size:95%;">';
    //pTablesHTML += '                                     <th class="hide">Ser.</th>';
    pTablesHTML += '                                     <th>' + TranslateString("Code") + '</th>';
    pTablesHTML += '                                     <th>' + TranslateString("VoucherType") + '</th>';
    pTablesHTML += '                                     <th>' + TranslateString("AccountName") + '</th>';
    pTablesHTML += '                                     <th>' + TranslateString("BankName") + '</th>';
    //pTablesHTML += '                                     <th class="hide">Partner Type</th>';
    pTablesHTML += '                                     <th>' + TranslateString("Status") + '</th>';
    pTablesHTML += '                                     <th>' + TranslateString("Total") + '</th>';
    pTablesHTML += '                                     <th>' + TranslateString("Cur") + '</th>';
    pTablesHTML += '                                     <th>' + TranslateString("DueDate") + '</th>';
    pTablesHTML += '                                     <th>' + TranslateString("ChequeNo") + '</th>';
    pTablesHTML += '                                     <th>' + TranslateString("Notes") + '</th>';
    pTablesHTML += '                                 </tr>';
    pTablesHTML += '                             </thead>';
    pTablesHTML += '                             <tbody>';
    debugger;
    var serial = 0;
    $.each((pReportRows), function (i, item) {
        //pTablesHTML += '                                     <tr class="' + (item.Cargo == "Totals In Default Currency:" ? "font-bold text-ul" : "") + '" style="font-size:95%; ' + (item.Cargo == "Total:" ? "background-color: #f9f9f9;!Important" : "") + '">' + (item.Code == "Total" ? "<b>" : "");
        pTablesHTML += '                                     <tr style="font-size:95%;">';
        //pTablesHTML += '                                         <td class="hide">' + (++serial) + '</td>';
        //pTablesHTML += '                                         <td' + (item.PRType == constPRTypeReceivable ? ' class="text-primary" ' : ' class="text-danger" ') + '>' + item.PaymentCode + '</td>';
        pTablesHTML += '                                         <td>' + item.Code + '</td>';
        pTablesHTML += '                                         <td>' + (item.VoucherTypeName == 0 ? "" : item.VoucherTypeName) + '</td>';
        pTablesHTML += '                                         <td>' + (item.AccountName == 0 ? "" : item.AccountName) + '</td>';
        pTablesHTML += '                                         <td>' + (item.Name == 0 ? item.Name : item.Name) + '</td>';
        //pTablesHTML += '                                         <td class="hide">' + item.PartnerTypeCode + '</td>';
        pTablesHTML += '                                         <td>' + item.ChequeStatus + '</td>';
        //pTablesHTML += '                                         <td>' + (item.PRType == constPRTypeReceivable ? item.Total.toFixed(2) : (-1 * item.Total).toFixed(2)) + '</td>';
        pTablesHTML += '                                         <td>' + ( item.Total.toFixed(2)) + '</td>';

        pTablesHTML += '                                         <td>' + item.CurrencyCode + '</td>';
        pTablesHTML += '                                         <td>' + ConvertDateFormat(GetDateWithFormatMDY(item.ChequeDate)) + '</td>';
        pTablesHTML += '                                         <td>' + (item.ChequeNo == 0 ? "" : item.ChequeNo) + '</td>';
        pTablesHTML += '                                         <td>' + (item.Notes == 0 ? "" : item.Notes) + '</td>';
        //pTablesHTML += '                                         <td' + (item.InvoiceStatus == "Paid" ? ' class="text-primary" ' : ' class="text-danger" ') + '>' + item.RemainingAmount.toFixed(2) + '</td>';
        pTablesHTML += '                                     </tr>';
    });
    pTablesHTML += '                             </tbody>';
    pTablesHTML += '                         </table>';
    $("#hExportedTable").html(pTablesHTML); //in all cases i put the tables in hidden div so i select each table separately
    if (pOutputTo == "Excel") {
        /*********************Append table summaries*************************/
        var pTableSummary = "";
        pTableSummary += '                                     <tr style="font-size:95%;">';
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
        pTableSummary += '                                     </tr>';
        pTableSummary += '                                     <tr style="font-size:95%;">';
        pTableSummary += '                                         <td>' + TranslateString("Total") + ' :</td>';
        pTableSummary += '                                         <td>' + pTotalSummary + '</td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                         <td></td>';
        pTableSummary += '                                     </tr>';
        $("#tblChequesReports" + " tbody").append(pTableSummary);
        ExportHtmlTableToCsv_RemovingCommasForNumbers("tblChequesReports", "Cheques Report");
    }
    else {
        var mywindow = window.open('', '_blank');
        var ReportHTML = '';
        //<div style="width:500px;height:100px;border:1px solid #000;">This is a rectangle!</div>
        if ($("[id$='hf_ChangeLanguage']").val() == "en") {
            ReportHTML += '<html>';
        }
        else {
            ReportHTML += '<html dir="rtl">';
        }
        ReportHTML += '     <head><title>' + pReportTitle + '</title><link rel="stylesheet" href="/Content/CSS/app.v2.css" type="text/css" /></head>';
        ReportHTML += '         <body style="background-color:white;">';
        ReportHTML += '             <div class="col-xs-12 text-center"><img src="/Content/Images/CompanyHeader.jpg" alt="logo"/></div> </br>';
        ReportHTML += '             <div class="col-xs-12 text-center text-ul m-t-n"><h3>' + pReportTitle + '</h3></div> </br>';

        ReportHTML += '             <div class="col-xs-4"><b>' + TranslateString("PrintedOn") + ' :</b> ' + TodaysDateddMMyyyy + '</div>';
        //ReportHTML += '             <div class="col-xs-4"><b>Transport Type :</b> ' + $("#divTransportFilter label.active").attr("title") + '</div>';
        //ReportHTML += '             <div class="col-xs-4"><b>Direction Type :</b> ' + $("#divDirectionFilter label.active").attr("title") + '</div>';
        //ReportHTML += '             <div class="col-xs-4"><b>B/L Type :</b> ' + $("#divBLTypeFilter label.active").attr("title") + '</div>';
        ReportHTML += '             <div class="col-xs-4"><b>' + TranslateString("ChequesStatus") + ' :</b> ' + $("#slChequeStatus option:selected").text() + '</div>';
      //  ReportHTML += '             <div class="col-xs-4"><b>Partner Type:</b> ' + $("#slPartnerType option:selected").text() + '</div>';
       // ReportHTML += '             <div class="col-xs-4"><b>Partner :</b> ' + $("#slPartner option:selected").text() + '</div>';
        //ReportHTML += '             <div class="col-xs-4"><b>Branch :</b> ' + $("#slBranch option:selected").text() + '</div>';
        ReportHTML += '             <div class="col-xs-4"><b>' + TranslateString("BankAcc") + ' :</b> ' + $("#slBankAccount option:selected").text() + '</div>';
    //    ReportHTML += '             <div class="col-xs-4"><b>Pay/Rec :</b> ' + $("#slPRType option:selected").text() + '</div>';
        ReportHTML += '             <div class="col-xs-4"><b>' + TranslateString("Currency") + ' :</b> ' + $("#slCurrency option:selected").text() + '</div>';
        ReportHTML += '             <div class="col-xs-8"><b>' + TranslateString("DueDate") + ' :</b> ' + ($("#txtFromDate").val() == "" && $("#txtToDate").val() == ""
                                                                                    ? "All Dates"
                                                                                    : ($("#txtFromDate").val() == "" ? "" : "From " + $("#txtFromDate").val() + " ") + ($("#txtToDate").val() == "" ? "" : "To " + $("#txtToDate").val())) + '</div>';
        ////ReportHTML += '                 <section class="panel panel-default">';
        //ReportHTML += '                     <div class="table-responsive">';
        ReportHTML += '                         <div> &nbsp; </div>'

        ReportHTML += pTablesHTML;

        //ReportHTML += '                     </div>';//of table-responsive
        ReportHTML += '             <div class="col-xs-12"><b>' + TranslateString("Total") + ':</b> ' + pTotalSummary + '</div>';
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
        mywindow.document.write(ReportHTML);
        mywindow.document.close();
    }
}