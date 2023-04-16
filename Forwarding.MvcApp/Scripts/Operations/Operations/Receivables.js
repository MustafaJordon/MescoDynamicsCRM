function Receivables_SubmenuTabClicked() {
    debugger;
    $("#cbIsReceipt").parent().parent().removeClass("hide"); //shown only in single edit from inside the operation
    if ($("#tblPayables tbody tr").length == 0 && $("#tblReceivables tbody tr").length == 0) {
        //Payables_LoadWithPagingWithWhereClause($("#hOperationID").val()
        //    , function () {
        //        LoadWithPagingWithWhereClause("div-Pager", "select-page-size", "spn-first-page-row", "spn-last-page-row", "spn-total-count", "div-Text-Total", "/api/Payables/LoadWithWhereClause", "WHERE IsDeleted=0 AND OperationID=" + $("#hOperationID").val(), 0, 1000, function (pTabelRows) { Payables_BindTableRows(pTabelRows); });
        //    });
        //Receivables_LoadWithPagingWithWhereClause($("#hOperationID").val());
        Receivables_LoadWithPagingWithWhereClause($("#hOperationID").val()
            , function () {
                LoadWithPagingWithWhereClause("div-Pager", "select-page-size", "spn-first-page-row", "spn-last-page-row", "spn-total-count", "div-Text-Total", "/api/Payables/LoadWithWhereClause", "WHERE IsDeleted=0 AND OperationID=" + $("#hOperationID").val(), 0, 1000, function (pTabelRows) { Payables_BindTableRows(pTabelRows); }, false);
            });
    }
    if (pDefaults.UnEditableCompanyName == "GBL") {
        $("#h6ChargesHeader_Quotation").html($("#lblDirection").parent().html());
        $("#h6ChargesHeader_Quotation").removeClass("hide");
    }
}
function Receivables_BindTableRows(pReceivables) {
    ClearAllTableRows("tblReceivables");
    debugger;
    //var editControlsText = " class='btn btn-xs btn-rounded btn-info float-right' > <i class='fa fa-pencil' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + TranslateString("Edit") + "</span>";
    var printControlsText = " class='btn btn-xs btn-rounded btn-lightblue float-right' > <i class='fa fa-print' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + TranslateString("Print") + "</span>";
    var copyControlsText = " class='btn btn-xs btn-rounded btn-info float-right' > <i class='fa fa-copy' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + TranslateString("Copy") + "</span>";
    $.each(pReceivables, function (i, item) {
        AppendRowtoTable("tblReceivables",
            ("<tr ID='" + item.ID + "' " + (item.InvoiceID == 0 && item.DraftInvoiceID == 0 && item.AccNoteID == 0 && OERec ? "ondblclick='Receivables_EditByDblClick(" + item.ID + ");'>" : ">")
                + "<td class='ReceivableID'> <input " + (item.InvoiceID == 0 && item.DraftInvoiceID == 0 && item.AccNoteID == 0 ? "name='Delete'" : "disabled='disabled'") + " type='checkbox' value='" + item.ID + "' /></td>"
                //+ "<td class='Receivable' val='" + item.ChargeTypeID + "'>" + item.ChargeTypeCode + " (" + item.ChargeTypeName + ")" + "</td>"
                + "<td class='Receivable' val='" + item.ChargeTypeID + "'>" + item.ChargeTypeName + ((pDefaults.IsRepeatChargeTypeName ? "(" + item.ChargeTypeCode + ")" : "")) + "</td>"
                + "<td class='ReceivablePOrC hide' val='" + item.POrC + "'>" + (item.POrC == 0 ? "" : item.POrCCode) + "</td>"
                //+ "<td class='ReceivableSupplier hide' val='" + item.SupplierID + "'>" + (item.SupplierID == 0 ? "" : item.SupplierName) + "</td>"
                //+ ($("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') ? ("<td class='ReceivableUOM' val='" + item.ContainerTypeID + "'>" + (item.ContainerTypeCode == 0 ? "" : item.ContainerTypeCode) + "</td>") : "")
                //+ ($("#cbIsLCL").prop('checked') || $("#cbIsLTL").prop('checked') || $("#cbIsAir").prop('checked') ? ("<td class='ReceivableUOM' val='" + item.PackageTypeID + "'>" + (item.PackageTypeName == 0 ? "" : item.PackageTypeName) + "</td>") : "")
                + "<td class='ReceivableUOM hide' val='" + item.MeasurementID + "'>" + (item.MeasurementID == 0 ? "" : item.MeasurementCode) + "</td>"
                + "<td class='ReceivableQuantity'>" + item.Quantity + "</td>"
                + "<td class='ReceivableCostPrice hide'>" + (item.CostPrice).toFixed(4) + "</td>"
                + "<td class='ReceivableCostAmount hide'>" + (item.CostAmount).toFixed(4) + "</td>"
                + "<td class='ReceivableSalePrice'>" + (item.SalePrice).toFixed(4) + "</td>"

                + "<td class='ReceivableAmountWithoutVAT hide'>" + item.AmountWithoutVAT.toFixed(4) + "</td>"
                + "<td class='ReceivableTaxTypeID' val=" + item.TaxTypeID + ">" + (item.TaxTypeID == 0 ? "" : item.TaxTypeName) + "</td>"
                + "<td class='ReceivableTaxPercentage hide'>" + item.TaxPercentage.toFixed(4) + "</td>"
                + "<td class='ReceivableTaxAmount hide'>" + item.TaxAmount.toFixed(4) + "</td>"
                + "<td class='ReceivableDiscountTypeID' val=" + item.DiscountTypeID + ">" + (item.DiscountTypeID == 0 ? "" : (item.DiscountPercentage + '%')) + "</td>"
                + "<td class='ReceivableDiscountPercentage hide'>" + item.DiscountPercentage.toFixed(4) + "</td>"
                + "<td class='ReceivableDiscountAmount hide'>" + item.DiscountAmount.toFixed(4) + "</td>"

                + "<td class='ReceivableSaleAmount'>" + (item.SaleAmount).toFixed(4) + "</td>"
                + "<td class='ReceivableExchangeRate'>" + item.ExchangeRate.toFixed(4) + "</td>"
                + "<td class='ReceivableCurrency' val='" + item.CurrencyID + "'>" + (item.CurrencyID == 0 ? "" : item.CurrencyCode) + "</td>"
                + "<td class='ReceivableForeignRate " + (pDefaults.UnEditableCompanyName == "FIV" ? "" : " hide ") + "'>" + (item.SalePrice + " " + item.CurrencyCode_Foreign + "/Unit") + "</td>"

                + "<td class='ReceivableTruckingOrderID hide'>" + (item.TruckingOrderID == "0" ? "" : item.TruckingOrderID) + "</td>"
                + "<td class='ReceivableOperationVehicleID hide'>" + (item.OperationVehicleID == "0" ? "" : item.OperationVehicleID) + "</td>"
                + "<td class='ReceivableChassisNumber " + (pDefaults.UnEditableCompanyName == "GBL" ? "" : " hide ") + "'>" + (item.ChassisNumber == "0" ? "" : item.ChassisNumber) + "</td>"
                + "<td class='ReceivableTruckID hide'>" + item.TruckID + "</td>"
                + "<td class='ReceivableTruckNumber " + (pDefaults.UnEditableCompanyName == "GBL" ? "" : " hide ") + "'>" + (item.TruckNumber == "0" ? "" : item.TruckNumber) + "</td>"
                + "<td class='ReceivableOperationContainersAndPackagesID hide'>" + item.OperationContainersAndPackagesID + "</td>"
                + "<td class='ReceivableTankOrFlexiNumber " + (/*$("#cbIsTank").prop('checked')*/1 == 1 ? "" : " hide ") + "'>" + (item.TankOrFlexiNumber == "0" ? (item.ContainerNumber == "0" ? "" : item.ContainerNumber) : item.TankOrFlexiNumber) + "</td>"
                + "<td class='ReceivableInvoice hide' val='" + item.InvoiceID + "'>" + (item.InvoiceID == 0 ? "" : (item.InvoiceNumber + " / " + item.InvoiceTypeName)) + "</td>"
                + "<td class='ReceivableAccNote hide' val='" + item.AccNoteID + "'>" + (item.AccNoteID == 0 ? "" : item.AccNoteCode) + "</td>"
                + "<td class='ReceivableShownInvoiceOrAccNote'>" + (item.AccNoteID == 0
                    ? (item.InvoiceID == 0 && item.DraftInvoiceID == 0 ? "" : (item.InvoiceNumber + " / " + item.InvoiceTypeName))
                    : item.AccNoteCode)
                + "</td>"
                + "<td class='ReceivableOperation hide' val='" + item.OperationID + "'>" + (item.OperationID == 0 ? "" : item.OperationCode) + "</td>"
                + "<td class='ReceivableNotes hide'>" + (item.Notes == "0" ? "" : item.Notes) + "</td>"

                + "<td class='ReceivableIssueDate hide'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.IssueDate)) > 0 ? ConvertDateFormat(GetDateWithFormatMDY(item.IssueDate)) : "") + "</td>"

                + "<td class='ReceivableCreatorName hide'>" + item.CreatorName + "</td>"
                //+ "<td class='ReceivableCreationDate hide'>" + item.CreationDate + "</td>"
                + "<td class='ReceivableCreationDate hide'>" + "<span class='pull-left thumb-sm  calendar-icon-style'>"
                + " <i class='fa fa-calendar'></i>"
                //+ " <small class='text-muted'>" + ConvertDateFormat(GetDateWithFormatMDY(item.CreationDate)) + "</small>"
                + " <small>" + GetShownDate(FormattedTodaysDate, GetDateWithFormatMDY(item.CreationDate)) + "</small>"
                + "</span>"
                + "</td>"
                + "<td class='ReceivableModificatorName hide'>" + item.ModificatorName + "</td>"
                //+ "<td class='ReceivableModificationDate hide'>" + item.ModificationDate + "</td>"
                + "<td class='ReceivableModificationDate hide'>" + "<span class='pull-left thumb-sm  calendar-icon-style'>"
                + " <i class='fa fa-calendar'></i>"
                //+ " <small class='text-muted'>" + ConvertDateFormat(GetDateWithFormatMDY(item.ModificationDate)) + "</small>"
                + " <small>" + GetShownDate(FormattedTodaysDate, GetDateWithFormatMDY(item.ModificationDate)) + "</small>"
                + "</span>"
                + "</td>"
                + "<td class='ReceivableBillTo hide' val='" + item.BillTo + "'>" + (item.BillTo == 0 ? "" : item.BillToName) + "</td>"
                + "<td class='ReceivableHouseNumber'>" + (item.PayableHouseNumber == 0 ? "" : item.PayableHouseNumber) + (item.PayableCertificateNumber == 0 ? "" : (" / " + item.PayableCertificateNumber)) + "</td>"
                + "<td class='IsReceipt'> <input id='cbIsReceipt" + item.ID + "' type='checkbox' disabled='disabled' val='" + (item.IsReceipt == true ? "true' checked='checked'" : "'") + " /></td>"

                + "<td class='ReceivableSalePrice_Foreign hide'>" + (item.SalePrice_Foreign).toFixed(4) + "</td>"
                + "<td class='ReceivableCurrency_Foreign hide' val='" + item.CurrencyID_Foreign + "'>" + (item.CurrencyID_Foreign == 0 ? "" : item.CurrencyCode_Foreign) + "</td>"
                + "<td class='ReceivableExchangeRate_Foreign hide'>" + item.ExchangeRate_Foreign.toFixed(4) + "</td>"

                + "<td class='ReceivableBillID hide'>" + item.HouseBillID + "</td>"
                + "<td class='ReceivableIDToExcludePurchase'> <input name='nameExcludePurchase'  type='checkbox' value='" + item.ID + "' /></td>"
                + "<td class='hide classKDS'><a href='#' data-toggle='modal' onclick='Receivables_Print(" + item.ID + ");' " + printControlsText + "</a></td>"
                + "<td>"
                + "<a " + (OARec && $("#hIsOperationDisabled").val() == false ? "" : " disabled='disabled' ") + " href='#CopyChargeModal' data-toggle='modal' " + 'onclick="Receivables_OpenCopyChargeModal(' + item.ID + ",'" + item.ChargeTypeName + "'" + ');" ' + copyControlsText + "</a>"
                //+ "<a href='#EditReceivableModal' data-toggle='modal' onclick='Receivables_FillControls(" + item.ID + ");' " + editControlsText + "</a>"
                + "</td>"
                + "</tr>"));
    });
    //ApplyPermissions();
    if (OARec && $("#hIsOperationDisabled").val() == false) { $("#btn-AddReceivables").removeClass("hide"); /*$("#btn-GenerateDefaultReceivables").removeClass("hide");*/ $("#btn-GenerateReceivablesFromQuotation").removeClass("hide"); $("#btn-GenerateReceivablesFromPayables").removeClass("hide"); $("#btn-ApplyInvoiceTypeDefaults").removeClass("hide"); $("#slReceivableInvoiceTypes").removeClass("hide"); }
    else { $("#btn-AddReceivables").addClass("hide"); /*$("#btn-GenerateDefaultReceivables").addClass("hide"); $("#btn-GenerateReceivablesFromQuotation").addClass("hide");*/ $("#btn-GenerateReceivablesFromPayables").addClass("hide"); $("#btn-ApplyInvoiceTypeDefaults").addClass("hide"); $("#slReceivableInvoiceTypes").addClass("hide"); }
    if (ODRec && $("#hIsOperationDisabled").val() == false) $("#btn-DeleteReceivable").removeClass("hide"); else $("#btn-DeleteReceivable").addClass("hide");
    if (OERec && $("#hIsOperationDisabled").val() == false) {
        $("#btn-MultiRowEditReceivables").removeClass("hide");
        //if (pDefaults.UnEditableCompanyName == "MAR" || pDefaults.UnEditableCompanyName == "IST")
        $("#btn-ResetQuantities").removeClass("hide");
    }
    else {
        $("#btn-MultiRowEditReceivables").addClass("hide");
        //if (pDefaults.UnEditableCompanyName == "MAR" || pDefaults.UnEditableCompanyName == "IST")
        $("#btn-ResetQuantities").addClass("hide");
    }

    if (OAInv && $("#hIsOperationDisabled").val() == false) $("#btn-NewInvoice").removeClass("hide"); else $("#btn-NewInvoice").addClass("hide");
    if (ODInv && $("#hIsOperationDisabled").val() == false) $("#btn-DeleteInvoice").removeClass("hide"); else $("#btn-DeleteInvoice").addClass("hide");

    if (OANot && $("#hIsOperationDisabled").val() == false) { $("#btn-NewDebitNote").removeClass("hide"); $("#btn-NewCreditNote").removeClass("hide"); } else { $("#btn-NewDebitNote").addClass("hide"); $("#btn-NewCreditNote").addClass("hide"); }
    if (ODNot && $("#hIsOperationDisabled").val() == false) $("#btn-DeleteAccNote").removeClass("hide"); else $("#btn-DeleteAccNote").addClass("hide");

    if (OADraftInv && $("#hIsOperationDisabled").val() == false) $("#btn-NewDraftInvoice").removeClass("hide"); else $("#btn-NewDraftInvoice").addClass("hide");
    if (ODDraftInv && $("#hIsOperationDisabled").val() == false) $("#btn-DeleteDraftInvoice").removeClass("hide"); else $("#btn-DeleteDraftInvoice").addClass("hide");

    if (pDefaults.UnEditableCompanyName == "KDS" || pDefaults.UnEditableCompanyName == "NEW") $(".classKDS").removeClass("hide"); else $(".classKDS").addClass("hide");
    BindAllCheckboxonTable("tblReceivables", "ReceivableID", "cb-CheckAll-Receivables");
    CheckAllCheckbox("HeaderDeleteReceivableID");
    //HighlightText("#tblReceivables>tbody>tr", $("#txt-Search").val().trim());
    if (showDeleteFailMessage) {//sherif: if deletion failed due to foreign key, then this message is shown
        swal(strSorry, strDeleteFailMessage, "warning");
        showDeleteFailMessage = false;
    }
    if (OERec) {
        PayablesAndReceivables_CalculateSummary();
        Receivables_CalculateSubtotals();
    }
}
function Receivables_EditByDblClick(pID) {
    jQuery("#EditReceivableModal").modal("show");
    Receivables_FillControls(pID);
}
function Receivables_LoadWithPagingWithWhereClause(pOperationID, pCallback, pFadePageCover) {
    pFadePageCover = (pFadePageCover != undefined && pFadePageCover != null ? pFadePageCover : true);
    if (pOperationID == 0)
        pOperationID = $("#hOperationID").val();
    //var pWhereClause = " WHERE (OperationID = " + pOperationID + " OR MasterOperationID = " + pOperationID + ") " + " AND IsDeleted = 0 ";
    var pWhereClause = " WHERE OperationID = " + pOperationID + " AND IsDeleted = 0 ";
    if ($("#slSearchTankInReceivables").val() != "")
        pWhereClause += " AND OperationContainersAndPackagesID='" + $("#slSearchTankInReceivables").val() + "'";
    if ($("#txtSearchReceivables").val().trim() == "0")
        pWhereClause += "AND (SaleAmount IS NULL OR SaleAmount=0)" + " \n";
    else if ($("#txtSearchReceivables").val().trim() != "") {
        pWhereClause += "AND (" + " \n";
        pWhereClause += "   PayableHouseNumber=N'" + $("#txtSearchReceivables").val().trim() + "'" + " \n";
        pWhereClause += "   OR PayableCertificateNumber=N'" + $("#txtSearchReceivables").val().trim() + "'" + " \n";
        pWhereClause += ")" + "\n";
    }
    LoadWithPagingWithWhereClause("div-Pager", "select-page-size", "spn-first-page-row", "spn-last-page-row", "spn-total-count", "div-Text-Total", "/api/Receivables/LoadWithWhereClause", pWhereClause, 0, 1000
        , function (pTabelRows) {
            Receivables_BindTableRows(pTabelRows);
            if (pCallback != null && pCallback != undefined)
                pCallback();
        }
        , pFadePageCover);
}
function Receivables_DeleteList(callback) {
    //Confirmation message to delete
    if (GetAllSelectedIDsAsString('tblReceivables') != "")
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
                DeleteListFunction("/api/Receivables/Delete"
                    , { pReceivablesIDs: GetAllSelectedIDsAsString('tblReceivables'), pOperationID: $("#hOperationID").val() }
                    , function () {
                        Receivables_LoadWithPagingWithWhereClause($("#hOperationID").val());
                    });
            });
    //DeleteListFunction("/api/Receivables/Delete", { "pReceivablesIDs": GetAllSelectedIDsAsString('tblReceivables') }, function () { LoadViews("OperationsEdit", null, $("#hOperationID").val()); });
}
function Receivables_Print(pReceivableIDToPrint) {
    debugger;
    CallGETFunctionWithParameters("/api/Receivables/PrintReceivable"
        , { pReceivableIDToPrint: pReceivableIDToPrint, pOperationID: $("#tblReceivables tbody tr[ID=" + pReceivableIDToPrint + "] td.ReceivableOperation").attr("val") }
        , function (pData) {
            var pReceivable = JSON.parse(pData[0]);
            var pOperation = JSON.parse(pData[1]);
            var mywindow = window.open('', '_blank');

            var ReportHTML = '';
            //<div style="width:500px;height:100px;border:1px solid #000;">This is a rectangle!</div>
            ReportHTML += '<html>';
            ReportHTML += '     <head><title>' + 'Debit Note' + '</title><link rel="stylesheet" href="/Content/CSS/app.v2.css" type="text/css" /></head>';
            //ReportHTML += '         <body style="background-color:white;">';
            ReportHTML += '         <body style="background-color:white; font-size=160%;">';
            ReportHTML += '             <div class="col-xs-12 text-center"><img src="/Content/Images/CompanyHeader.jpg" alt="logo"/></div> </br>';
            ReportHTML += '             <div class="col-xs-12 text-center text-ul"><h3>' + 'Debit Note ' + (pReceivable.ViewOrder == 0 ? "" : pReceivable.ViewOrder) + '</h3></div> </br>';
            ReportHTML += '             <div class="col-xs-12 m-l-n"><b>Printed on: </b>' + ConvertDateFormat(FormattedTodaysDate) + '</div>';
            ReportHTML += '             <div class="col-xs-12 m-l-n"><b>Client : </b>' + pOperation.ClientName + '</div>';
            ReportHTML += '             <div class="col-xs-12 m-l-n"><b>' + pOperation.VesselName + ' - Voy. </b>' + pOperation.VoyageOrTruckNumber + ' <b> AT </b> ' + (pOperation.DirectionType == constImportDirectionType ? pOperation.POLName : pOperation.PODName) + ' ' + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(pOperation.ActualArrival)) > 0 ? ('(' + ConvertDateFormat(GetDateWithFormatMDY(pOperation.ActualArrival)) + ') ') : "") + ' ' + $("#lblServiceScope").text() + '</div>';
            ReportHTML += '             <div> &nbsp; </div>'
            ReportHTML += '                         <table id="tblPrintReceivable" class="table table-striped b-t b-light text-sm table-bordered">'; //table-hover
            ReportHTML += '                             <thead>';
            ReportHTML += '                                 <tr style="font-size:110%;">';
            ReportHTML += '                                     <th>Description</th>';
            ReportHTML += '                                     <th>Amount (' + pReceivable.CurrencyCode + ')' + '</th>';
            ReportHTML += '                                 </tr>';
            ReportHTML += '                             </thead>';
            ReportHTML += '                             <tbody>';
            debugger;
            //$.each($("#tblPayables tr"), function (i, item) { debugger; });
            //$.each(JSON.parse(data[19]), function (i, item) {
            ReportHTML += '                                     <tr class="" style="font-size:110%;">';
            ReportHTML += '                                         <td style="text-align:left; ">' + pReceivable.ChargeTypeName + '</td>';
            ReportHTML += '                                         <td>' + pReceivable.SaleAmount.toFixed(2) + '</td>';
            ReportHTML += '                                     </tr>';
            //ReportHTML += '                                     <tr class="" style="font-size:110%;">';
            //ReportHTML += '                                         <td colspan=2 style="text-align:left; ">' + (pReceivable.Notes == 0 ? "" : pReceivable.Notes.replace(/ /g, "&nbsp;").replace(/\n/g, "<br/>")) + '<br><br><b>ONLY: ' + toWords_WithFractionNumbers(pReceivable.SaleAmount) + '</b></td>';
            //ReportHTML += '                                         <td>' + '</td>';
            //ReportHTML += '                                     </tr>';
            ReportHTML += '                                     <tr class="" style="font-size:110%;">';
            ReportHTML += '                                         <td colspan=2 style="text-align:left; ">' + (pReceivable.Notes == 0 ? "" : (pReceivable.Notes.replace(/ /g, "&nbsp;").replace(/\n/g, "<br/>") + '<br><br>')) + '<b>ONLY: ' + toWords_WithFractionNumbers(pReceivable.SaleAmount) + ' ' + pReceivable.CurrencyCode + '</b></td>';
            ReportHTML += '                                     </tr>';
            //});
            ReportHTML += '                             </tbody>';
            ReportHTML += '                         </table>';
            //if (pProfitCurrenciesSubtotal != "") {
            //    ReportHTML += '                         <div class="col-xs-12 m-t-n text-right"><b> Total Payables: ' + pPayablesCurrenciesSubtotal + " =(" + pPayablesInDefaultCurrency + " " + $("#hDefaultCurrencyCode").val() + ")" + '</b></div>';
            //    ReportHTML += '                         <div class="col-xs-12 m-t-n text-right"></br><b> Total Inv./Rec.: ' + pReceivablesOrInvoicesCurrenciesSubtotal + " =(" + pReceivablesInDefaultCurrency + " " + $("#hDefaultCurrencyCode").val() + ")" + '</b></div>';
            //    ReportHTML += '                         <div class="col-xs-12 m-t-n text-right"></br><b> Total Profit: ' + pProfitCurrenciesSubtotal + " =(" + pProfitInDefaultCurrency + " " + $("#hDefaultCurrencyCode").val() + ")" + '</b></div>';
            //}
            ReportHTML += '         </body>';
            ReportHTML += '     <footer class="footer col-xs-12" style="width:100%; position:absolute; bottom:0;">';
            //if ($("#tblDocsOut tr[id=" + pDocumentTypeID + "] td.IsPrintISOCode input[type=checkbox]").prop("checked"))
            //    ReportHTML += '     <div class="row m-l">' + $("#tblDocsOut tr[id=" + pDocumentTypeID + "] td.ISOCode").text() + '</div>';
            //if ($("#tblDocsOut tr[id=" + pDocumentTypeID + "] td.ShowFooter input[type=checkbox]").prop("checked"))
            //ReportHTML += '         <div class="row text-right m-r">' + '  الشركة تخضع لنظام الدفعات المقدمة  ' + '</div>';
            //ReportHTML += '         <div class="row text-right m-r">' + '  لاتعتبر الفاتورة مسددة إلا بموجب إيصال سداد معتمد من الشركة بتمام قيمة الفاتورة  ' + '</div>';
            ReportHTML += '         <div class="row text-center"><img src="/Content/Images/CompanyFooter' /*+ (pDefaults.UnEditableCompanyName == "KDS" ? '-KDS-InvoiceTaxNumbers' : "")*/ + '.jpg"' + ' alt="logo"/></div>';
            ReportHTML += '     </footer>';
            ReportHTML += '</html>';
            mywindow.document.write(ReportHTML);
            mywindow.document.close();
        }
        , null);
}
function Receivables_PrintAll() {
    debugger;
    var arr_Keys = new Array();
    var arr_Values = new Array();

    arr_Keys.push("ReceivablesWithoutVAT");
    arr_Values.push($("#lblReceivablesWithoutVATInReceivables").text().replaceAll(',', ''));

    arr_Keys.push("CurrenciesTotals");
    arr_Values.push($("#lblReceivablesSubTotals").text().replaceAll(',', ''));

    arr_Keys.push("ClientName");
    arr_Values.push($("#lblClient").text().replaceAll(',', '').replaceAll(':', ''));

    var ReportName = "AllReceivablesReport"
    var query = `SELECT ChargeTypeName,Quantity,SalePrice,SaleAmount,ExchangeRate,CurrencyCode,OperationCode FROM vwReceivables WHERE OperationID=${$("#hOperationID").val()}`
    var pParametersWithValues =
    {
        query: query
        , arr_Keys: arr_Keys
        , arr_Values: arr_Values
        , pTitle: ReportName
        , pReportName: ReportName
        , pReportType: "Print"
    };

    var win = window.open("", "_blank");

    url = '/ReportMainClass/PrintReportQueryAndParams?pTitle="' + pParametersWithValues.pTitle + '"'
        + '&query=' + pParametersWithValues.query + ''
        + '&arr_Keys=' + pParametersWithValues.arr_Keys + ''
        + '&arr_Values=' + pParametersWithValues.arr_Values + ''
        + '&pReportName=' + pParametersWithValues.pReportName + ''
        + '&pReportType=' + pParametersWithValues.pReportType + '';

    win.location = url;


}
///////////////////////The next 4 fns are for the first 4 buttons in the receivable screen/////////////////
function Receivables_GetAvailableCharges() {
    debugger;
    FadePageCover(true);
    $("#divSelectCharges").html("");
    $("#lblShownOperationCode").html($("#hOperationCode").val());
    var pStrFnName = "/api/ChargeTypes/LoadAll";
    var pDivName = "divSelectCharges";
    var ptblModalName = "tblModalReceivables";
    var pCheckboxNameAttr = "cbSelectCharges";
    var pWhereClause = "";
    pWhereClause += " WHERE IsUsedInReceivable = 1 AND IsInactive = 0 AND IsOperationChargeType=1 ";
    pWhereClause += ($("#cbIsOcean").prop('checked') == true ? " AND IsOcean = 1 " : "");
    pWhereClause += ($("#cbIsAir").prop('checked') == true ? " AND IsAir = 1 " : "");
    pWhereClause += ($("#cbIsInland").prop('checked') == true ? " AND IsInland = 1 " : "");
    pWhereClause += " AND ( Code LIKE N'%" + $("#txtSearchCharges").val().trim().toUpperCase() + "%' OR Name LIKE N'%" + $("#txtSearchCharges").val().trim().toUpperCase() + "%') ";
    //pWhereClause += " AND ID NOT IN (SELECT ChargeTypeID from Receivables ";
    //pWhereClause += "                WHERE OperationID = " + $("#hOperationID").val() + ") ";
    GetListAsCheckboxesWithVariousParameters(pStrFnName, { pWhereClause: pWhereClause }, pDivName, pCheckboxNameAttr
        , function () {
            HighlightText("#divSelectCharges", $("#txtSearchCharges").val().trim().toUpperCase());
            FadePageCover(false);
        }
        , pDefaults.UnEditableCompanyName == "MAR"
            ? 2
            : (pDefaults.IsRepeatChargeTypeName ? 3 : 1)
        , "col-sm-3"/*pColumnSize*/);
    $("#btn-SearchCharges").attr("onclick", "Receivables_GetAvailableCharges();");
    $("#btnSelectChargesApply").attr("onclick", "Receivables_InsertListWithoutValues(false);");

    //FillReceivablesModalTableControls(pStrFnName, pWhereClause, pDivName, ptblModalName, pCheckboxNameAttr, true //pIsInsert
    //    , false /*pIsEditInvoice*/, function () {
    //        HighlightText("#divSelectCharges", $("#txtSearchCharges").val().trim().toUpperCase());
    //        $("#btnSelectChargesApply").attr("onclick", "Receivables_InsertListWithValues(false);");
    //    });
    //$("#btn-SearchCharges").attr("onclick", "Receivables_GetAvailableCharges();");
}
//pWhoIsCalling : 1-GenerateDefaults pressed(defaults checked in chargetypes are applied) 2-InvoiceType Defaults is applied 
function Receivables_GenerateDefaults(pWhoIsCalling) {
    if (pWhoIsCalling == 2 && $("#slReceivableInvoiceTypes").val() == "")
        swal(strSorry, "Please select an invoice type to apply its defaults.");
    else
        swal({
            title: strAreYouSure,
            text: "The Default Receivables For Operation '" + $("#hOperationCode").val() + "' Will Be Applied.",
            //type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, Apply!",
            closeOnConfirm: true
        },
            //callback function in case of success
            function () {
                var pWhereClause = " WHERE ";
                if (pWhoIsCalling == 1) { //Generate Defaults button is pressed
                    pWhereClause += " IsUsedInReceivable = 1 AND IsDefaultInOperations = 1 AND IsInactive = 0 ";
                    pWhereClause += ($("#cbIsOcean").prop('checked') == true ? " AND IsOcean = 1 " : "");
                    pWhereClause += ($("#cbIsAir").prop('checked') == true ? " AND IsAir = 1 " : "");
                    pWhereClause += ($("#cbIsInland").prop('checked') == true ? " AND IsInland = 1 " : "");
                }
                else { //pWhoIsCalling = 2 (Generate InvoiceType Defaults) 
                    pWhereClause += " InvoiceTypeID = " + $("#slReceivableInvoiceTypes").val();
                }
                debugger;
                FadePageCover(true);
                CallGETFunctionWithParameters("/api/Receivables/ApplyDefaultReceivables"
                    , { pOperationID: $("#hOperationID").val(), pWhereClause: pWhereClause }
                    , function () {
                        Receivables_LoadWithPagingWithWhereClause($("#hOperationID").val());
                    });
            });
}
function Receivables_FillQuotationRouteModal() {
    debugger;
    FadePageCover(true);
    $("#slQuotationRoutes").html("<option value=''>" + TranslateString("SelectFromMenu") + "</option>");
    //Whereclause: accepted and not expired
    var pWhereClauseQRWithMinimalColumns = "WHERE GETDATE()<=DateAdd(minute,59,DateAdd(hh, 23, DateDiff(dd, 0, ExpirationDate))) AND QuotationStageID=" + AcceptedQuoteAndOperStageID + "\n"
        + "AND POL=" + $("#hPOL").val() + " AND POD=" + $("#hPOD").val() + "\n"
        + "AND (ClientID IN (" + $("#hConsigneeID").val() + "," + $("#hShipperID").val() + ") OR AgentID=" + $("#hAgentID").val() + ") \n"
        + "AND (ShippingLineID=" + $("#hShippingLineID").val() + " OR AirlineID=" + $("#hAirlineID").val() + " OR TruckerID=" + $("#hTruckerID").val() + ") \n";
    CallGETFunctionWithParameters("/api/Quotations/QR_LoadAllWithMinimalColumns",
        { pWhereClauseQRWithMinimalColumns: pWhereClauseQRWithMinimalColumns, pOrderBy: "ID DESC" }
        , function (pData) {
            if (pData[0]) {
                FillListFromObject(null, 1, TranslateString("SelectFromMenu"), "slQuotationRoutes", pData[1], null);
                jQuery("#SelectQuotationRouteModal").modal("show");
                FadePageCover(false);
            }
        }, null);
}
function Receivables_GenerateFromQuotation() {
    //if ($("#hQuotationRouteID").val() == 0)
    //    swal(strSorry, "This Operation is not generated from a quotation.", "warning");
    if ($("#slQuotationRoutes").val() == "")
        swal(strSorry, "Please, select a quotation.");
    else
        swal({
            title: strAreYouSure,
            text: "The Receivables For Operation '" + $("#hOperationCode").val() + "' Will Be Generated From Quotation.",
            //type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, Apply!",
            closeOnConfirm: true
        },
            //callback function in case of success
            function () {
                FadePageCover(true);
                var pWhereClause = " WHERE QuotationRouteID = " + $("#slQuotationRoutes").val(); //$("#hQuotationRouteID").val();
                debugger;
                CallGETFunctionWithParameters("/api/Receivables/GenerateReceivablesFromQuotation"
                    , { pOperationID: $("#hOperationID").val(), pWhereClause1: pWhereClause, pQuotationRouteID: $("#slQuotationRoutes").val() } // i called the pWhereClause1 instead of pWhereClause to avoid the error of routing when 2 actions have the same signature
                    , function () {
                        $("#hQuotationRouteID").val($("#slQuotationRoutes").val());
                        $("#lblQuotation").text(": " + $("#slQuotationRoutes option:selected").text());
                        Receivables_LoadWithPagingWithWhereClause($("#hOperationID").val());
                        //Receivables_MultiRowEdit();//enable incase of transfering button to the multirowEdit
                    });
            });
}
function Receivables_GenerateFromPayables() {
    debugger;
    FadePageCover(true);
    $("#tblModalReceivables tbody").html("");
    var pStrFnName = "/api/Payables/LoadAll";
    var pDivName = "divSelectCharges";//div name to be filled
    var ptblModalName = "tblModalReceivables";
    var pCheckboxNameAttr = "cbSelectReceivables";
    var pWhereClause = "";
    pWhereClause += " WHERE IsDeleted=0 AND (ReceivableID IS NULL OR receivableid=0) AND (OperationID = " + $("#hOperationID").val() + " OR MasterOperationID = " + $("#hOperationID").val() + ") ";
    pWhereClause += " AND ( ChargeTypeCode LIKE N'%" + $("#txtSearchCharges").val().trim().toUpperCase() + "%' OR ChargeTypeName LIKE N'%" + $("#txtSearchCharges").val().trim().toUpperCase() + "%' OR HouseNumber LIKE '%" + $("#txtSearchCharges").val().trim().toUpperCase() + "%' OR CertificateNumber LIKE '%" + $("#txtSearchCharges").val().trim().toUpperCase() + "%') \n";
    //pWhereClause += "                ORDER BY ChargeTypeCode ";

    FillReceivablesModalTableFromPayables(pStrFnName, pWhereClause, pDivName, ptblModalName, pCheckboxNameAttr, true //pIsInsertChoice
        , function () { FadePageCover(false); HighlightText("#" + pDivName, $("#txtSearchCharges").val().trim().toUpperCase()); });

    $("#btn-SearchCharges").attr("onclick", "Receivables_GenerateFromPayables();");
    $("#btnSelectChargesApply").attr("onclick", "Receivables_InsertListWithValuesFromPayables(false);");
}
//called when pressing Apply in SelectCharges Modal
function Receivables_InsertListWithoutValues(pSaveandAddNew) {
    var pSelectedIDs = GetAllSelectedIDsAsStringWithNameAttr("cbSelectCharges");//returns string array of IDs
    debugger;
    if (pSelectedIDs != "") {
        FadePageCover(true);
        InsertSelectedCheckboxItems("/api/Receivables/InsertListWithoutValues"
            , {
                pOperationID: $("#hOperationID").val(), pSelectedIDs: pSelectedIDs, pQuotationRouteID: 0
                , pOperationContainersAndPackagesID: 0, pOperationVehicleID: 0, pTruckingOrderID: 0
            }
            , pSaveandAddNew
            , "SelectChargesModal" //pModalID
            , null //function () { Receivables_GetAvailableCharges(); }
            , function () {
                Receivables_LoadWithPagingWithWhereClause($("#hOperationID").val());
            });
    }
}
function Receivables_InsertListWithValuesFromPayables(pSaveandAddNew) {
    if (GetAllSelectedIDsAsStringWithNameAttr("cbSelectReceivables") != "")
        swal({
            title: strAreYouSure,
            text: "The Receivables For Operation '" + $("#hOperationCode").val() + "' Will Be Generated From Payables.",
            //type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, Apply!",
            closeOnConfirm: true
        },
            //callback function in case of success
            function () {
                var pSelectedPayableIDs = GetAllSelectedIDsAsStringWithNameAttr("cbSelectReceivables");//here i changed it in mainapp to be ChargeTypeID
                debugger;
                var ArrayOfIDs = pSelectedPayableIDs.split(',');
                var pSelectedChargeTypeIDs = "";
                var pPOrCList = "";
                var pUOMList = "";
                var pQuantityList = "";
                var pSalePriceList = "";
                var pSaleAmountList = "";
                var pExchangeRateList = "";
                var pCurrencyList = "";
                if (pSelectedPayableIDs != "") {
                    FadePageCover(true);
                    var NumberOfSelectRows = ArrayOfIDs.length;
                    for (i = 0; i < NumberOfSelectRows; i++) {
                        var currentRowID = ArrayOfIDs[i];
                        pSelectedChargeTypeIDs += ((pSelectedChargeTypeIDs == "") ? "" : ",") + ($("#ReceivableChargeType" + currentRowID).attr("val") == undefined || $("#ReceivableChargeType" + currentRowID).attr("val") == "" ? 0 : $("#ReceivableChargeType" + currentRowID).attr("val"));
                        pPOrCList += ((pPOrCList == "") ? "" : ",") + ($("#slReceivablePOrC" + currentRowID).val() == undefined || $("#slReceivablePOrC" + currentRowID).val() == "" ? 0 : $("#slReceivablePOrC" + currentRowID).val());
                        pUOMList += ((pUOMList == "") ? "" : ",") + ($("#slReceivableUOM" + currentRowID).val() == undefined || $("#slReceivableUOM" + currentRowID).val() == "" ? 0 : $("#slReceivableUOM" + currentRowID).val());
                        pQuantityList += ((pQuantityList == "") ? "" : ",") + ($("#txtTblModalReceivableQuantity" + currentRowID).val() == undefined || $("#txtTblModalReceivableQuantity" + currentRowID).val() == "" ? 1 : $("#txtTblModalReceivableQuantity" + currentRowID).val());
                        pSalePriceList += ((pSalePriceList == "") ? "" : ",") + ($("#txtTblModalReceivableSalePrice" + currentRowID).val() == undefined || $("#txtTblModalReceivableSalePrice" + currentRowID).val() == "" ? 0 : $("#txtTblModalReceivableSalePrice" + currentRowID).val());
                        pSaleAmountList += ((pSaleAmountList == "") ? "" : ",") + ($("#txtTblModalReceivableSaleAmount" + currentRowID).val() == undefined || $("#txtTblModalReceivableSaleAmount" + currentRowID).val() == "" ? 0 : $("#txtTblModalReceivableSaleAmount" + currentRowID).val());
                        //pSaleAmountList += ((pSaleAmountList == "") ? "" : ",") + ($("#txtTblModalReceivableAmountWithoutVAT" + currentRowID).val() == undefined || $("#txtTblModalReceivableAmountWithoutVAT" + currentRowID).val() == "" ? 0 : $("#txtTblModalReceivableAmountWithoutVAT" + currentRowID).val());
                        pExchangeRateList += ((pExchangeRateList == "") ? "" : ",") + ($("#txtTblModalReceivableExchangeRate" + currentRowID).val() == undefined || $("#txtTblModalReceivableExchangeRate" + currentRowID).val() == "" ? 0 : $("#txtTblModalReceivableExchangeRate" + currentRowID).val());
                        pCurrencyList += ((pCurrencyList == "") ? "" : ",") + ($("#slReceivableCurrency" + currentRowID).val() == undefined || $("#slReceivableCurrency" + currentRowID).val() == "" ? 0 : $("#slReceivableCurrency" + currentRowID).val());
                    }
                }
                if (pSelectedPayableIDs != "")
                    InsertSelectedCheckboxItems("/api/Receivables/InsertListWithValuesFromPayables"
                        , {
                            "pOperationID": $("#hOperationID").val()
                            , "pSelectedPayableIDs": pSelectedPayableIDs
                            , "pSelectedChargeTypeIDs": pSelectedChargeTypeIDs
                            , "pPOrCList": pPOrCList
                            , "pUOMList": pUOMList
                            , "pQuantityList": pQuantityList
                            , "pSalePriceList": pSalePriceList
                            , "pSaleAmountList": pSaleAmountList
                            , "pExchangeRateList": pExchangeRateList
                            , "pCurrencyList": pCurrencyList
                        }
                        , pSaveandAddNew
                        , "SelectChargesModal" //pModalID
                        , null//function () { Receivables_GetAvailableCharges(); }
                        , function () {
                            Receivables_LoadWithPagingWithWhereClause($("#hOperationID").val());
                        });
            });
    else
        swal(strSorry, "No Charges is selected.");
}
function Receivables_Update(pSaveandAddNew) {
    debugger;
    if (pDefaults.UnEditableCompanyName == "FIV" && $("#txtReceivableExchangeRate_Foreign").val() == 0)
        swal("Sorry", "Please, add exchange rate to currencies in the master data.");
    else if (!isValidDate($("#txtReceivableIssueDate").val().trim(), 1) && $("#txtReceivableIssueDate").val().trim() != "")
        swal(strSorry, strCheckDates);
    else if ($("#txtReceivableExchangeRate").val() == "" || parseFloat($("#txtReceivableExchangeRate").val()) == 0
        || (parseFloat($("#txtReceivableExchangeRate").val()) == 1 && pDefaults.CurrencyID != ($("#slReceivableCurrency").val())))
        swal("Sorry", "Please, check exchange rate.");
    else {
        InsertUpdateFunction("form", "/api/Receivables/Update", {
            pSavedFrom: 10 //pSavedFrom=10 : saved from Operations
            , pOperationContainersAndPackagesID: $("#slReceivableTank").val() == "" ? 0 : $("#slReceivableTank").val()
            , pIsReceipt: $("#cbIsReceipt").prop("checked")
            , pHouseBillID: ($('#slReceivableBill option:selected').val() == "" ? 0 : $('#slReceivableBill option:selected').val())

            , pID: $("#hReceivableID").val()
            //, pMeasurementID: $("#txtCalculationType").attr("MeasurementID")
            , pOperationID: $("#hOperationID").val()
            , pChargeTypeID: $("#slReceivableChargeType").val() == "" ? 0 : $("#slReceivableChargeType").val() //$("#txtReceivableType").attr("ChargeTypeID")
            , pMeasurementID: $('#slReceivableUOM option:selected').val() != ""
                ? $('#slReceivableUOM option:selected').val()
                : 0
            //, pContainerTypeID: ((($("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked')) && $('#slReceivableUOM option:selected').val() != "")
            //    ? $('#slReceivableUOM option:selected').val()
            //    : 0)
            , pContainerTypeID: 0
            //, pPackageTypeID: ((($("#cbIsLCL").prop('checked') || $("#cbIsLTL").prop('checked') || $("#cbIsAir").prop('checked')) && $('#slReceivableUOM option:selected').val() != "")
            //    ? $('#slReceivableUOM option:selected').val()
            //    : 0)
            , pPOrC: ($('#slReceivablePOrC option:selected').val() == "" ? 0 : $('#slReceivablePOrC option:selected').val())
            , pSupplierID: 0//($('#slReceivableSupplier option:selected').val() == "" ? 0 : $('#slReceivableSupplier option:selected').val())
            , pQuantity: ($("#txtReceivableQuantity").val().trim() == "" ? 0 : $("#txtReceivableQuantity").val().trim())
            , pCostPrice: ($("#txtReceivableUnitPrice").val().trim() == "" ? 0 : $("#txtReceivableUnitPrice").val().trim())
            , pCostAmount: ($("#txtReceivableAmount").val().trim() == "" ? 0 : $("#txtReceivableAmount").val().trim())
            , pSalePrice: ($("#txtReceivableUnitPrice").val().trim() == "" ? 0 : $("#txtReceivableUnitPrice").val().trim())

            , pAmountWithoutVAT: $("#txtReceivableAmountWithoutVAT").val() == "" ? 0 : $("#txtReceivableAmountWithoutVAT").val()
            , pTaxTypeID: $("#slReceivableTax").val() == "" ? 0 : $("#slReceivableTax").val()
            , pTaxPercentage: $("#txtReceivableTaxPercentage").val() == "" ? 0 : $("#txtReceivableTaxPercentage").val()
            , pTaxAmount: $("#txtReceivableTaxAmount").val() == "" ? 0 : $("#txtReceivableTaxAmount").val()
            , pDiscountTypeID: $("#slReceivableDiscount").val() == "" ? 0 : $("#slReceivableDiscount").val()
            , pDiscountPercentage: $("#txtReceivableDiscountPercentage").val() == "" ? 0 : $("#txtReceivableDiscountPercentage").val()
            , pDiscountAmount: $("#txtReceivableDiscountAmount").val() == "" ? 0 : $("#txtReceivableDiscountAmount").val()

            , pSaleAmount: ($("#txtReceivableAmount").val().trim() == "" ? 0 : $("#txtReceivableAmount").val().trim())
            , pExchangeRate: ($("#txtReceivableExchangeRate").val().trim() == "" ? 0 : $("#txtReceivableExchangeRate").val().trim())
            , pCurrencyID: ($('#slReceivableCurrency option:selected').val() == "" ? 0 : $('#slReceivableCurrency option:selected').val())
            , pNotes: $("#txtReceivableNotes").val().toUpperCase().trim()

            , pIssueDate: ($("#txtReceivableIssueDate").val().trim() == "" ? "01/01/1900" : ConvertDateFormat($("#txtReceivableIssueDate").val().trim()))

            , pSalePrice_Foreign: ($("#txtReceivableUnitPrice_Foreign").val().trim() == "" ? 0 : $("#txtReceivableUnitPrice_Foreign").val().trim())
            , pExchangeRate_Foreign: ($("#txtReceivableExchangeRate_Foreign").val().trim() == "" ? 0 : $("#txtReceivableExchangeRate_Foreign").val().trim())
            , pCurrencyID_Foreign: ($("#slReceivableCurrency_Foreign").val().trim() == "" ? 0 : $("#slReceivableCurrency_Foreign").val().trim())

        }, pSaveandAddNew, "EditReceivableModal", function () { Receivables_LoadWithPagingWithWhereClause($("#hOperationID").val()); });
    }
    //}
    //else
    //    swal(strSorry, strCheckEntries, "warning");
}
function Receivables_UpdateList(pSaveandAddNew, pInvoiceID, pIsRemoveItems) { // if (pInvoiceID > 0) then this is  updating Invoice Items(called from invoices_update)
    debugger;
    var pSelectedReceivablesIDsToUpdate = "";
    if (pInvoiceID == 0) //this is called normally from the receivables edit modal
        pSelectedReceivablesIDsToUpdate = GetAllSelectedIDsAsStringWithNameAttr("cbSelectReceivables"); //i get from selected 
    else { //this is called from invoice update
        if (pIsRemoveItems) //here i get only the unchecked items coz the others will be deleted in the controllers
            pSelectedReceivablesIDsToUpdate = GetAllNOTSelectedIDsAsStringWithNameAttr("cbSelectInvoiceItems");
        else // here i get all IDs to handle the case of checking items then pressing save and not remove items
            pSelectedReceivablesIDsToUpdate = GetAllIDsAsStringWithNameAttr("tblModalInvoiceItems", "cbSelectInvoiceItems");
    }
    var ArrayOfIDs = pSelectedReceivablesIDsToUpdate.split(',');
    var pPOrCList = "";
    var pUOMList = "";
    var pQuantityList = "";
    var pSalePriceList = "";

    var pAmountWithoutVATList = "";
    var pTaxTypeIDList = "";
    var pTaxPercentageList = "";
    var pTaxAmountList = "";
    var pDiscountTypeIDList = "";
    var pDiscountPercentageList = "";
    var pDiscountAmountList = "";

    var pSaleAmountList = "";
    var pExchangeRateList = "";
    var pCurrencyList = "";
    var pReceiptNoList = "";
    var pReceiptDateList = "";
    var pNotesList = "";
    var pViewOrderList = "";
    var _IsZeroExchangeRate = false;
    if (pSelectedReceivablesIDsToUpdate != "") {
        var NumberOfSelectRows = ArrayOfIDs.length;
        for (i = 0; i < NumberOfSelectRows; i++) {
            var currentRowID = ArrayOfIDs[i];
            if ($("#txtTblModalReceivableExchangeRate" + currentRowID).val() == undefined || $("#txtTblModalReceivableExchangeRate" + currentRowID).val() == "" || parseFloat($("#txtTblModalReceivableExchangeRate" + currentRowID).val()) == 0
                || (parseFloat($("#txtTblModalReceivableExchangeRate" + currentRowID).val()) == 1 && pDefaults.CurrencyID != ($("#slReceivableCurrency" + currentRowID).val())))
                _IsZeroExchangeRate = true;

            pPOrCList += ((pPOrCList == "") ? "" : ",") + ($("#slReceivablePOrC" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val() == undefined || $("#slReceivablePOrC" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val() == "" ? 0 : $("#slReceivablePOrC" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val());
            pUOMList += ((pUOMList == "") ? "" : ",") + ($("#slReceivableUOM" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val() == undefined || $("#slReceivableUOM" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val() == "" ? 0 : $("#slReceivableUOM" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val());
            pQuantityList += ((pQuantityList == "") ? "" : ",") + ($("#txtTblModalReceivableQuantity" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val() == undefined || $("#txtTblModalReceivableQuantity" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val() == "" ? 0 : $("#txtTblModalReceivableQuantity" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val());
            pSalePriceList += ((pSalePriceList == "") ? "" : ",") + ($("#txtTblModalReceivableSalePrice" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val() == undefined || $("#txtTblModalReceivableSalePrice" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val() == "" ? 0 : $("#txtTblModalReceivableSalePrice" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val());

            pAmountWithoutVATList += ((pAmountWithoutVATList == "") ? "" : ",") + ($("#txtTblModalReceivableAmountWithoutVAT" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val() == undefined || $("#txtTblModalReceivableAmountWithoutVAT" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val() == "" ? 0 : $("#txtTblModalReceivableAmountWithoutVAT" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val());
            pTaxTypeIDList += ((pTaxTypeIDList == "") ? "" : ",") + ($("#slReceivableTax" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val() == undefined || $("#slReceivableTax" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val() == "" ? 0 : $("#slReceivableTax" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val());
            pTaxPercentageList += ((pTaxPercentageList == "") ? "" : ",") + ($("#txtTblModalReceivableTaxPercentage" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val() == undefined || $("#txtTblModalReceivableTaxPercentage" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val() == "" ? 0 : $("#txtTblModalReceivableTaxPercentage" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val());
            pTaxAmountList += ((pTaxAmountList == "") ? "" : ",") + ($("#txtTblModalReceivableTaxAmount" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val() == undefined || $("#txtTblModalReceivableTaxAmount" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val() == "" ? 0 : $("#txtTblModalReceivableTaxAmount" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val());
            pDiscountTypeIDList += ((pDiscountTypeIDList == "") ? "" : ",") + ($("#slReceivableDiscount" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val() == undefined || $("#slReceivableDiscount" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val() == "" ? 0 : $("#slReceivableDiscount" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val());
            pDiscountPercentageList += ((pDiscountPercentageList == "") ? "" : ",") + ($("#txtTblModalReceivableDiscountPercentage" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val() == undefined || $("#txtTblModalReceivableDiscountPercentage" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val() == "" ? 0 : $("#txtTblModalReceivableDiscountPercentage" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val());
            pDiscountAmountList += ((pDiscountAmountList == "") ? "" : ",") + ($("#txtTblModalReceivableDiscountAmount" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val() == undefined || $("#txtTblModalReceivableDiscountAmount" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val() == "" ? 0 : $("#txtTblModalReceivableDiscountAmount" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val());

            pSaleAmountList += ((pSaleAmountList == "") ? "" : ",") + ($("#txtTblModalReceivableSaleAmount" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val() == undefined || $("#txtTblModalReceivableSaleAmount" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val() == "" ? 0 : $("#txtTblModalReceivableSaleAmount" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val());
            pExchangeRateList += ((pExchangeRateList == "") ? "" : ",") + ($("#txtTblModalReceivableExchangeRate" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val() == undefined || $("#txtTblModalReceivableExchangeRate" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val() == "" ? 0 : $("#txtTblModalReceivableExchangeRate" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val());
            pCurrencyList += ((pCurrencyList == "") ? "" : ",") + ($("#slReceivableCurrency" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val() == undefined || $("#slReceivableCurrency" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val() == "" ? 0 : $("#slReceivableCurrency" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val());
            pReceiptNoList += ((pReceiptNoList == "") ? "" : ",") + ($("#txtTblModalReceivableReceiptNo" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val() == undefined || $("#txtTblModalReceivableReceiptNo" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val() == "" ? 0 : $("#txtTblModalReceivableReceiptNo" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val());
            pReceiptDateList += ((pReceiptDateList == "") ? "" : ",") + ($("#txtTblModalReceivableReceiptDate" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val() == undefined || $("#txtTblModalReceivableReceiptDate" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val() == "" ? "01/01/1900" : ConvertDateFormat($("#txtTblModalReceivableReceiptDate" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val().trim()) );
            pNotesList += ((pNotesList == "") ? "" : ",") + ($("#txtTblModalReceivableNotes" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val() == undefined || $("#txtTblModalReceivableNotes" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val() == "" ? 0 : $("#txtTblModalReceivableNotes" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val());
            pViewOrderList += ((pViewOrderList == "") ? "" : ",") + ($("#txtTblModalReceivableViewOrder" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val() == undefined || $("#txtTblModalReceivableViewOrder" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val() == "" ? 0 : $("#txtTblModalReceivableViewOrder" + (pInvoiceID == 0 ? "" : "InvoiceEdit") + currentRowID).val());
        }
    }
    if (_IsZeroExchangeRate) {
        swal("Sorry", "Please, check exchange rate.");
        FadePageCover(false);
    }
    else {
        if (pSelectedReceivablesIDsToUpdate != "")
            InsertSelectedCheckboxItems_Post("/api/Receivables/UpdateList"
                , {
                    "pSelectedReceivablesIDsToUpdate": pSelectedReceivablesIDsToUpdate
                    , "pPOrCList": pPOrCList
                    , "pUOMList": pUOMList
                    , "pQuantityList": pQuantityList
                    , "pSalePriceList": pSalePriceList

                    , "pAmountWithoutVATList": pAmountWithoutVATList
                    , "pTaxTypeIDList": pTaxTypeIDList
                    , "pTaxPercentageList": pTaxPercentageList
                    , "pTaxAmountList": pTaxAmountList
                    , "pDiscountTypeIDList": pDiscountTypeIDList
                    , "pDiscountPercentageList": pDiscountPercentageList
                    , "pDiscountAmountList": pDiscountAmountList

                    , "pSaleAmountList": pSaleAmountList
                    , "pExchangeRateList": pExchangeRateList
                    , "pCurrencyList": pCurrencyList
                    , "pReceiptNoList": pReceiptNoList
                    , "pReceiptDateList": pReceiptDateList
                    , "pNotesList": pNotesList
                    , "pViewOrderList": pViewOrderList
                    , "pInvoiceID": pInvoiceID //if pInvoiceID==0 then its not used else this is invoice items update
                }
                , pSaveandAddNew
                , "SelectChargesModal" //pModalID
                , function () { /*Receivables_GetAvailableCharges();*/ }
                , function () {
                    Receivables_LoadWithPagingWithWhereClause($("#hOperationID").val());
                });
        else
            swal(strSorry, "No available items to be updated.");
    }
}
function Receivables_OpenCopyChargeModal(pReceivableIDToCopy, pChargeTypeName) {
    debugger;
    $("#txtNumberOfCopies").val("");
    $("#lblCopyChargeShown").html(": " + pChargeTypeName);
    $("#btnCopyCharge").attr("onclick", "Receivables_Copy(" + pReceivableIDToCopy + ")");
}
function Receivables_Copy(pReceivableIDToCopy) {
    if ($("#txtNumberOfCopies").val() == "" || $("#txtNumberOfCopies").val() > 20)
        swal("Sorry", "Please, enter number of copies and it must be less less than 20.");
    else {
        FadePageCover(true);
        CallGETFunctionWithParameters("/api/Receivables/CopyReceivable"
            , { pReceivableIDToCopy: pReceivableIDToCopy, pNumberOfDuplicates: $("#txtNumberOfCopies").val() }
            , function (pData) {
                var pReceivables = JSON.parse(pData[0]);
                Receivables_BindTableRows(pReceivables);
                swal("Success", "Saved successfully.");
                jQuery("#CopyChargeModal").modal("hide");
                FadePageCover(false);
            }
            , null);
    }
}
/////////////////////////EOF Saving fns//////////////////////////////////
function Receivables_FillControls(pID) {
    debugger;
    ClearAll("#EditReceivableModal");

    $("#hReceivableID").val(pID);
    FadePageCover(true);
    var tr = $("#tblReceivables tr[ID='" + pID + "']");
    var pPOrCID = $(tr).find("td.ReceivablePOrC").attr('val');
    var pSupplierID = $(tr).find("td.ReceivableSupplier").attr('val');
    var pUOMID = $(tr).find("td.ReceivableUOM").attr('val');
    var pCurrencyID = $(tr).find("td.ReceivableCurrency").attr('val');
    var pCurrencyID_Foreign = $(tr).find("td.ReceivableCurrency_Foreign").attr('val');
    var pTaxTypeID = $(tr).find("td.ReceivableTaxTypeID").attr('val');
    var pDiscountTypeID = $(tr).find("td.ReceivableDiscountTypeID").attr('val');
    var pDiscountTypeID = $(tr).find("td.ReceivableDiscountTypeID").attr('val');
    var pBillID = $(tr).find("td.ReceivableBillID").text();
    var pOperationID = $(tr).find("td.ReceivableOperation").attr('val');

    var pOperationContainersAndPackagesID = $(tr).find("td.ReceivableOperationContainersAndPackagesID").text();

    $("#slReceivableTank").html($("#slSearchTankInReceivables").html()); $("#slReceivableTank").val(pOperationContainersAndPackagesID == 0 ? "" : pOperationContainersAndPackagesID);
    if ($("#cbIsTank").prop("checked") || pDefaults.UnEditableCompanyName == "CAP")
        $("#slReceivableTank").parent().removeClass("hide");

    if ($("#hDefaultCurrencyID").val() == pCurrencyID)
        $("#txtReceivableExchangeRate").attr("disabled", "disabled");
    else
        $("#txtReceivableExchangeRate").removeAttr("disabled");

    $("#cbIsReceipt").prop("checked", $("#cbIsReceipt" + pID).prop("checked"));
    //ReceivablePOrC_GetList(pPOrCID, "slReceivablePOrC");
    $("#slReceivablePOrC").html($("#slOperationPOrC").html()); $("#slReceivablePOrC").val(pPOrCID == 0 ? "" : pPOrCID);
    //ReceivableSuppliers_GetList(pSupplierID, "slReceivableSupplier");
    ReceivableCurrency_GetList(pCurrencyID, "slReceivableCurrency", null);
    $("#slReceivableCurrency_Foreign").val(pCurrencyID_Foreign == 0 ? pDefaults.CurrencyID : pCurrencyID_Foreign);
    ReceivableUOM_GetList(pUOMID, "slReceivableUOM");

    $("#lblReceivableShown").html(": " + $(tr).find("td.Receivable").text());
    $("#lblReceivableCreatedBy").html(" : " + $(tr).find("td.ReceivableCreatorName").text())
    $("#lblReceivableCreationDate").html(" : " + $(tr).find("td.ReceivableCreationDate").text())
    $("#lblReceivableUpdatedBy").html(": " + $(tr).find("td.ReceivableModificatorName").text())
    $("#lblReceivableModificationDate").html(" : " + $(tr).find("td.ReceivableModificationDate").text())

    //$("#txtReceivableType").val($(tr).find("td.Receivable").text());
    //$("#txtReceivableType").attr("ChargeTypeID", $(tr).find("td.Receivable").attr("val"));
    CallGETFunctionWithParameters("/api/ChargeTypes/LoadAllWithMinimalColumns"
        , { pWhereClauseWithMinimalColumns: "WHERE 1=1" }
        , function (pData) {
            FillListFromObject($(tr).find("td.Receivable").attr("val"), (pDefaults.IsRepeatChargeTypeName ? 4 : 2), "<--Select-->", "slReceivableChargeType", pData[0], null);
            FadePageCover(false);
        }
        , null);
    if ($("#slReceivableTax option").length < 2 || $("#slReceivableDiscount option").length < 2)
        GetListTaxTypeWithNameAndPercAttr(pTaxTypeID, "/api/TaxeTypes/LoadAllWithWhereClause"
            , "<--Select-->", "slReceivableTax", "WHERE IsInactive=0 ORDER BY Name"
            , function () {
                $("#slReceivableDiscount").html($("#slReceivableTax").html());
                $("#slReceivableDiscount").val(pDiscountTypeID == 0 ? "" : pDiscountTypeID);
                $("#slReceivableTax option[IsDiscount='true']").addClass('hide');
                $("#slReceivableDiscount option[IsDiscount='false']").addClass('hide');
            });
    else {
        $("#slReceivableTax").val(pTaxTypeID == 0 ? "" : pTaxTypeID);
        $("#slReceivableDiscount").val(pDiscountTypeID == 0 ? "" : pDiscountTypeID);
    }
    $("#txtReceivableQuantity").val($(tr).find("td.ReceivableQuantity").text());
    //$("#txtReceivableUnitPrice").val($(tr).find("td.ReceivableCostPrice").text());
    //$("#txtReceivableAmount").val($(tr).find("td.ReceivableCostAmount").text());
    $("#txtReceivableUnitPrice").val(parseInt($(tr).find("td.ReceivableSalePrice").text()) == 0 ? "" : $(tr).find("td.ReceivableSalePrice").text());
    $("#txtReceivableUnitPrice_Foreign").val(parseInt($(tr).find("td.ReceivableSalePrice_Foreign").text()) == 0 ? "" : $(tr).find("td.ReceivableSalePrice_Foreign").text());

    $("#txtReceivableAmountWithoutVAT").val(parseInt($(tr).find("td.ReceivableAmountWithoutVAT").text()) == 0 ? "" : $(tr).find("td.ReceivableAmountWithoutVAT").text());
    $("#txtReceivableTaxPercentage").val($(tr).find("td.ReceivableTaxPercentage").text());
    $("#txtReceivableTaxAmount").val($(tr).find("td.ReceivableTaxAmount").text());
    $("#txtReceivableDiscountPercentage").val($(tr).find("td.ReceivableDiscountPercentage").text());
    $("#txtReceivableDiscountAmount").val($(tr).find("td.ReceivableDiscountAmount").text());

    $("#txtReceivableAmount").val(parseInt($(tr).find("td.ReceivableSaleAmount").text()) == 0 ? "" : $(tr).find("td.ReceivableSaleAmount").text());
    $("#txtReceivableExchangeRate").val($(tr).find("td.ReceivableExchangeRate").text());
    $("#txtReceivableExchangeRate_Foreign").val($(tr).find("td.ReceivableExchangeRate_Foreign").text() == 0 ? 1 : $(tr).find("td.ReceivableExchangeRate_Foreign").text());

    $("#txtReceivableNotes").val($(tr).find("td.ReceivableNotes").text());
    $("#txtReceivableIssueDate").val($(tr).find("td.ReceivableIssueDate").text());
    $("#txtReceivableBillTo").val($(tr).find("td.ReceivableBillTo").text());

    if (pDefaults.UnEditableCompanyName == "DGL")
        CallGETFunctionWithParameters("/api/Operations/LoadAll"
            , { pWhereClause: " WHERE MasterOperationID=" + pOperationID }
            , function (pData) {
                var pOperationList = pData[0];
                Fill_SelectInputAfterLoadData_WithMultiAttrAndMultiFields(pOperationList, "ID", "HouseNumber,CertificateNumber", ' / ', TranslateString("SelectFromMenu"), "#slReceivableBill", pBillID, "", function () { /*ApplySelectListSearch();*/ FadePageCover(false); });
            });

    $("#slReceivableUOM").attr("onchange", "Receivables_UOMChanged();");
    $("#btnSaveReceivable").attr("onclick", "Receivables_Update(false);");
}
function Receivables_MultiRowEdit() {
    debugger;
    FadePageCover(true);
    $("#divSelectCharges").html("");
    var pStrFnName = "/api/Receivables/LoadAll";
    var pDivName = "divSelectCharges";//div name to be filled
    var ptblModalName = "tblModalReceivables";
    var pCheckboxNameAttr = "cbSelectReceivables";
    var pWhereClause = "";
    pWhereClause += " WHERE OperationID = " + $("#hOperationID").val() + " AND IsDeleted = 0 AND AccNoteID IS NULL ";
    if (pDefaults.UnEditableCompanyName == "FIV")
        pWhereClause += "";
    if ($("#txtSearchCharges").val().trim() == "0")
        pWhereClause += "AND (SaleAmount IS NULL OR SaleAmount=0)" + " \n";
    else
        pWhereClause += " AND (ChargeTypeCode LIKE N'%" + $("#txtSearchCharges").val().trim().toUpperCase() + "%' OR ChargeTypeName LIKE N'%" + $("#txtSearchCharges").val().trim().toUpperCase() + "%' OR PayableHouseNumber LIKE N'%" + $("#txtSearchCharges").val().trim().toUpperCase() + "%' OR PayableCertificateNumber LIKE N'%" + $("#txtSearchCharges").val().trim().toUpperCase() + "%') ";
    //pWhereClause += " ORDER BY ChargeTypeName ";

    FillReceivablesModalTableControls(pStrFnName, pWhereClause, pDivName, ptblModalName, pCheckboxNameAttr, false /*pIsInsert*/, false /*pIsInvoiceEdit*/
        , function () {
            FadePageCover(false);
            HighlightText("#" + divSelectCharges, $("#txtSearchCharges").val().trim().toUpperCase());
        });

    $("#btn-SearchCharges").attr("onclick", "Receivables_MultiRowEdit();");
    $("#btnSelectChargesApply").attr("onclick", "Receivables_UpdateList(false, 0, false);");//parameters are(pSaveAndNew, pInvoiceID, pIsRemoveItems)
}
function Receivables_ResetQuantities() {
    debugger;
    swal({
        title: "Are you sure?",
        text: "Receivables quantities will be reset according to calculation type.",
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
                pOperationIDToSetReceivablesQuantity_NotStatic: $("#hOperationID").val()
            };
            CallGETFunctionWithParameters("/api/ChargeTypes/ChargeTypes_SetDefaultReceivablesQuantity_NotStatic", pParametersWithValues
                , function (pData) {
                    var _ReturnedMessage = pData[0];
                    if (_ReturnedMessage == "") {
                        Receivables_LoadWithPagingWithWhereClause(0);
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
function Receivables_ClearAllControls(callback) {
    ClearAll("#EditReceivableModal");
    if (callback != null && callback != undefined)
        callback();
}
function Receivables_CurrencyChanged() {
    $("#txtReceivableExchangeRate").val($("#slReceivableCurrency option:selected").attr("MasterDataExchangeRate"));
    if ($("#hDefaultCurrencyID").val() == $("#slReceivableCurrency").val()) {
        $("#txtReceivableExchangeRate").attr("disabled", "disabled");
    }
    else {
        $("#txtReceivableExchangeRate").attr("disabled", "disabled");
    }
}
//to handle change of currency in the multi row edit modal
function Receivables_txtTblModalCurrency_Changed(pRowID, pIsInvoiceEdit) {
    debugger;
    $("#txtTblModalReceivableExchangeRate" + (pIsInvoiceEdit ? "InvoiceEdit" : "") + pRowID).val($("#slReceivableCurrency" + (pIsInvoiceEdit ? "InvoiceEdit" : "") + pRowID + " option:selected").attr("MasterDataExchangeRate"));
    if ($("#hDefaultCurrencyID").val() == $("#slReceivableCurrency" + (pIsInvoiceEdit ? "InvoiceEdit" : "") + pRowID).val())
        $("#txtTblModalReceivableExchangeRate" + (pIsInvoiceEdit ? "InvoiceEdit" : "") + pRowID).attr("disabled", "disabled");
    else
        $("#txtTblModalReceivableExchangeRate" + (pIsInvoiceEdit ? "InvoiceEdit" : "") + pRowID).removeAttr("disabled");
}
function Receivables_txtTblModalSaleAmount_Changed(pRowID, pIsInsertChoice) {
    if (pIsInsertChoice) { //if not insert then all IDs will be updated
        var varReceivableSaleAmount = $("#tblModalReceivables tr[ID='" + pRowID + "']").find('input[name=txtTblModalReceivableSaleAmount]').val();
        if (varReceivableSaleAmount != 0 && varReceivableSaleAmount != "")
            $("#tblModalReceivables tr[ID='" + pRowID + "']").find('input[name=cbSelectReceivables]').prop("checked", true);
        else
            $("#tblModalReceivables tr[ID='" + pRowID + "']").find('input[name=cbSelectReceivables]').prop("checked", false);
    }
}
function Receivables_UOMChanged() {
    //if ($("#slReceivableUOM option:selected").val() != "") {
    //    //$("#txtReceivableQuantity").val($("#slReceivableUOM option:selected").attr("Quantity"));
    //    //get the number of packages or containers from the packages tab
    //    if ($("#hBLType").val() == constFCLShipmentType || $("#hShipmentType").val() == constFTLShipmentType || $("#hShipmentType").val() == constConsolidationShipmentType)
    //        $("#txtReceivableQuantity").val($('#tblOperationContainersAndPackages tr td.Container[val=' + $("#slReceivableUOM option:selected").val() + ']').length);
    //    CalculateReceivablesAmount();
    //}
}
function CalculateReceivablesAmount() {
    debugger;
    var decAmountWithoutVAT = 0;
    var decTaxAmount = 0; var decTaxPercentage = 0.0;
    var decDiscountAmount = 0; var decDiscountPercentage = 0.0;

    decAmountWithoutVAT = $("#txtReceivableQuantity").val() * $("#txtReceivableUnitPrice").val();
    $("#txtReceivableAmountWithoutVAT").val(decAmountWithoutVAT);
    decTaxPercentage = $("#slReceivableTax option:selected").attr("CurrentPercentage");
    decTaxAmount = (Math.round((decAmountWithoutVAT * decTaxPercentage / 100) * 100) / 100).toFixed(2);
    decDiscountPercentage = $("#slReceivableDiscount option:selected").attr("CurrentPercentage");
    decDiscountAmount = (Math.round((decAmountWithoutVAT * decDiscountPercentage / 100) * 100) / 100).toFixed(2);

    $("#txtReceivableTaxPercentage").val(decTaxPercentage);
    $("#txtReceivableTaxAmount").val(decTaxAmount);
    $("#txtReceivableDiscountPercentage").val(decDiscountPercentage);
    $("#txtReceivableDiscountAmount").val(decDiscountAmount);
    $("#txtReceivableAmount").val((Math.round(((parseFloat(decAmountWithoutVAT) + parseFloat(decTaxAmount) - parseFloat(decDiscountAmount))) * 100) / 100).toFixed(2));
}
function CalculateReceivablesAmount_Foreign() {
    debugger;
    if (pDefaults.UnEditableCompanyName == "FIV") {
        $("#txtReceivableExchangeRate_Foreign").val($("#slReceivableCurrency_Foreign option:selected").attr("MasterDataExchangeRate"));
        let decExchangeRate_Foreign = $("#slReceivableCurrency_Foreign option:selected").attr("MasterDataExchangeRate");
        let decUnitPrice_Foreign = $("#txtReceivableUnitPrice_Foreign").val();
        $("#txtReceivableUnitPrice").val((decUnitPrice_Foreign * decExchangeRate_Foreign).toFixed(2));
    }

    let decAmountWithoutVAT = 0;
    let decTaxAmount = 0; let decTaxPercentage = 0.0;
    let decDiscountAmount = 0; let decDiscountPercentage = 0.0;

    decAmountWithoutVAT = $("#txtReceivableQuantity").val() * $("#txtReceivableUnitPrice").val();
    $("#txtReceivableAmountWithoutVAT").val(decAmountWithoutVAT);
    decTaxPercentage = $("#slReceivableTax option:selected").attr("CurrentPercentage");
    decTaxAmount = (Math.round((decAmountWithoutVAT * decTaxPercentage / 100) * 100) / 100).toFixed(2);
    decDiscountPercentage = $("#slReceivableDiscount option:selected").attr("CurrentPercentage");
    decDiscountAmount = (Math.round((decAmountWithoutVAT * decDiscountPercentage / 100) * 100) / 100).toFixed(2);

    $("#txtReceivableTaxPercentage").val(decTaxPercentage);
    $("#txtReceivableTaxAmount").val(decTaxAmount);
    $("#txtReceivableDiscountPercentage").val(decDiscountPercentage);
    $("#txtReceivableDiscountAmount").val(decDiscountAmount);
    $("#txtReceivableAmount").val((Math.round(((parseFloat(decAmountWithoutVAT) + parseFloat(decTaxAmount) - parseFloat(decDiscountAmount))) * 100) / 100).toFixed(2));
}
////if not insert (i.e. update then all will rows will be selected)
//function Receivables_Row_CalculateReceivablesAmount(pRowID, pIsInsertChoice) {
//    var rowQuantity = $("#txtTblModalReceivableQuantity" + pRowID).val();
//    var rowSalePrice = $("#txtTblModalReceivableSalePrice" + pRowID).val();
//    $("#txtTblModalReceivableSaleAmount" + pRowID).val(rowQuantity * rowSalePrice);
//    if (pIsInsertChoice) //incase of insert check and uncheck the checkbox to in GetSelectedIDs incase of insert
//        Receivables_txtTblModalSaleAmount_Changed(pRowID, pIsInsertChoice);
//}
//if not insert (i.e. update then all will rows will be selected)
function Receivables_Row_CalculateReceivablesAmount(pRowID, pIsInsertChoice) {
    debugger;
    var rowQuantity = $("#txtTblModalReceivableQuantity" + pRowID).val();
    var rowSalePrice = $("#txtTblModalReceivableSalePrice" + pRowID).val();

    var decAmountWithoutVAT = 0;
    var decTaxAmount = 0; var decTaxPercentage = 0.0;
    var decDiscountAmount = 0; var decDiscountPercentage = 0.0;

    decAmountWithoutVAT = (Math.round((rowQuantity * rowSalePrice) * 100) / 100).toFixed(2);
    $("#txtTblModalReceivableAmountWithoutVAT" + pRowID).val(decAmountWithoutVAT);
    if (pDefaults.IsTaxOnItems && !pIsInsertChoice/*to not calc. tax in case of Gen from Payables*/) {
        decTaxPercentage = $("#slReceivableTax" + pRowID + " option:selected").attr("CurrentPercentage");
        decTaxAmount = (Math.round((decAmountWithoutVAT * decTaxPercentage / 100) * 100) / 100).toFixed(2);
        decDiscountPercentage = $("#slReceivableDiscount" + pRowID + " option:selected").attr("CurrentPercentage");
        decDiscountAmount = (Math.round((decAmountWithoutVAT * decDiscountPercentage / 100) * 100) / 100).toFixed(2);
    }
    $("#txtTblModalReceivableTaxPercentage" + pRowID).val(decTaxPercentage);
    $("#txtTblModalReceivableTaxAmount" + pRowID).val(decTaxAmount);
    $("#txtTblModalReceivableDiscountPercentage" + pRowID).val(decDiscountPercentage);
    $("#txtTblModalReceivableDiscountAmount" + pRowID).val(decDiscountAmount);
    $("#txtTblModalReceivableSaleAmount" + pRowID).val((Math.round(((parseFloat(decAmountWithoutVAT) + parseFloat(decTaxAmount) - parseFloat(decDiscountAmount))) * 100) / 100).toFixed(2));

    if (pIsInsertChoice) //incase of insert check and uncheck the checkbox to in GetSelectedIDs incase of insert
        Receivables_txtTblModalSaleAmount_Changed(pRowID, pIsInsertChoice);
}
function ReceivableCurrency_GetList(pID, pSlName, callback) {
    //parameters: ID, strFnName, First Row in select list, select list name
    GetListCurrencyWithCodeAndExchangeRateAttr(pID, "/api/Currencies/LoadAll", "Select Currency", pSlName, " WHERE 1=1 ORDER BY Code "
        , function () { //this callback is used to set the InitialSalePrice in case of generating from payables
            if (callback != null && callback != undefined)
                callback();
        });
}
function ReceivablePOrC_GetList(pID, pSlName) {
    //parameters: ID, strFnName, First Row in select list, select list name
    GetListWithCodeAndWhereClause(pID, "/api/NoAccessFreightTypes/LoadAll", "Select P/C", pSlName, " WHERE 1=1 ");
}
function ReceivableSuppliers_GetList(pID, pSlName) {
    //parameters: ID, strFnName, First Row in select list, select list name, whereClause, callback
    GetListWithNameAndWhereClause(pID, "/api/Suppliers/LoadAll", "Select Supplier", pSlName, " ORDER BY Name ");
}
function ReceivableInvoiceTypes_GetList(pID, pSlName) {
    //slReceivableInvoiceTypes
    //parameters: ID, strFnName, First Row in select list, select list name, whereClause, callback
    GetListWithNameAndWhereClause(pID, "/api/InvoiceTypes/LoadAll", "Inv.Type Defaults", pSlName, " ORDER BY Name ");
}
//function ReceivableUOM_GetList(pID, pSlName) {
//    debugger;
//    if ($("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked')) { //ContainerType
//        var pWhereClause = "";
//        pWhereClause += " Where OperationID = " + $("#hOperationID").val();
//        pWhereClause += " ORDER BY ContainerTypeCode ";

//        GetListWithContainerTypeCodeAndQuantityAttr(pID, "/api/OperationContainersAndPackages/LoadAll", "Select ContainerType", pSlName, pWhereClause);
//    }
//    else //PackageType
//        if ($("#cbIsLCL").prop('checked') || $("#cbIsLTL").prop('checked') || $("#cbIsAir").prop('checked')) {
//            var pWhereClause = "";
//            pWhereClause += " Where OperationID = " + $("#hOperationID").val();
//            pWhereClause += " ORDER BY PackageTypeName ";

//            GetListWithPackageTypeNameAndQuantityAttr(pID, "/api/OperationContainersAndPackages/LoadAll", "Select PackageType", pSlName, pWhereClause);
//        }
//}
function ReceivableUOM_GetList(pID, pSlName) {
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
//to get total of each currecy
function Receivables_CalculateSubtotals() {
    debugger;
    var pParametersWithValues = { "pOperationID": $("#hOperationID").val() }; // here the condition " AND IsDeleted = 0 " is added in the view itself
    var htmlLblReceivablesSubTotals = ""
    CallGETFunctionWithParameters("/api/Receivables/GetReceivablesSubTotals"
        , pParametersWithValues
        , function (data) {
            if (data != "") {
                debugger;
                for (var i = 0; i < data[1]; i++) {
                    htmlLblReceivablesSubTotals += (i == 0 ? "" : " , ") + JSON.parse(data[0])[i].SaleAmountSubTotal.toFixed(4) + " " + JSON.parse(data[0])[i].CurrencyCode;
                }
                if ($("#hf_ChangeLanguage").val() == "ar") {
                    $("#lblReceivablesSubTotals").html("<u>إجمالي العملات: </u><br/>" + htmlLblReceivablesSubTotals);
                } else {
                    $("#lblReceivablesSubTotals").html("<u>Currencies Totals:</u><br/>" + htmlLblReceivablesSubTotals);
                }
            }
        });
}