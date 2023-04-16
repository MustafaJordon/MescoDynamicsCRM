function BalanceSheet_Print(pOutputTo) {
    debugger;
    FadePageCover(true);
    var pParametersWithValues = {
        pFromDate : $("#txtFromDate").val()
       , pToDate: $("#txtToDate").val()
    };
    CallGETFunctionWithParameters("/api/CashFlowReport/GetPrintedData", pParametersWithValues
        , function (pData) {
            CashFlow_Draw(pData, pOutputTo);
        }
        , null);
}
function CashFlow_Draw(pData, pOutputTo) {
    debugger;
    var pDefaultsHeader = JSON.parse(pData[0]);
    var pCashFlow = JSON.parse(pData[1]);
    var pCashFlowGroups = JSON.parse(pData[2]);
    var pFormattedTodaysDate = getTodaysDateInddMMyyyyFormat();
    var pFormattedPrintTime = getTime();
    //pDescriptionOfGoods.replace(/\n/g, "<br />")
    //ReportHTML += '<div style="width:500px;height:100px;border:1px solid #000;">This is a rectangle!</div>';
    //ReportHTML += '<hr style="width:100%;height:0px;border:.5px solid #000;">';
    var pTableHTML = "";
    if ($("[id$='hf_ChangeLanguage']").val() == "en") {
    pTableHTML += '             <table id="tblCashFlow" class="table table-striped text-sm  table-bordered" style="">';  //style="border:solid #000 !Important;" 
    pTableHTML += '                 <thead class="" style="">'
    pTableHTML += '                     <tr class="" style="">';
    pTableHTML += '                         <th class="">' + 'Account' + '</th>';
    pTableHTML += '                         <th class="">' + 'Balance' + '</thb>';
    pTableHTML += '                     </tr>'     
    pTableHTML += '                 </thead>';
    pTableHTML += '                 <tbody>';
    if (pCashFlow != null)
    {
        var tabs = "&emsp;&emsp;&emsp;&emsp;&emsp;";
        var Total = 0.00;

        $.each(pCashFlowGroups, function (i, item) {
            pTableHTML += '                 <tr class="" style="font-size:95%;">';
            pTableHTML += '                     <td class="" style="text-align:left;">' + (pOutputTo != "Excel" ? "" : "") + (pOutputTo != "Excel" ? ('<b>' + item.Name + '</b>') : item.Name) + '</td>';
            pTableHTML += '                     <td class="Balance" style="text-align:left;">' + "" + '</td>';
            pTableHTML += '                 </tr>';
            var CurrenctCashFlowGroups = pCashFlow.filter(x=> x.ParentGroupID == item.ID);
            Total = 0;
            $.each(CurrenctCashFlowGroups, function (k, account) {
                Total += account.Balance;
                pTableHTML += '                 <tr class="" style="font-size:95%;">';
                pTableHTML += '                     <td class="" style="text-align:left;">' + (pOutputTo != "Excel" ? "" : "") + ( account.GroupName) + '</td>';
                pTableHTML += '                     <td class="Balance" style="text-align:left;">' + account.Balance + '</td>';
                pTableHTML += '                 </tr>';
            });
            pTableHTML += '                 <tr class="" style="font-size:95%;">';
            pTableHTML += '                     <td class="" style="text-align:left;">' + (pOutputTo != "Excel" ? "" : "") + (pOutputTo != "Excel" ? ('<b>' + item.Name.replace('التدفقات','صافى') + '</b>') : item.Name) + '</td>';
            pTableHTML += '                     <td class="GroupBalance" style="text-align:left;">' + (pOutputTo != "Excel" ? tabs : "") + Total + '</td>';
            pTableHTML += '                 </tr>';

        });



    }
    //$.each(pCashFlow, function (i, item) {
    //    if (!($("#cbSuppressForZeroes").prop("checked") && item.Balance == 0)) {
    //        var tabs = "";
    //        for (var y = 1; y < item.AccLevel ; y++)
    //            tabs += "&emsp;&emsp;";
    //        pTableHTML += '                 <tr class="" style="font-size:95%;">';
    //        pTableHTML += '                     <td class="" style="text-align:left;">' + (pOutputTo != "Excel" ? tabs : "") + (item.IsMain && pOutputTo != "Excel" ? ('<b>' + item.Account_Name + '</b>') : item.Account_Name) + '</td>';
    //        pTableHTML += '                     <td class="FinalBalance" style="text-align:left;">' + (pOutputTo != "Excel" ? tabs : "") + (item.FinalBalance).toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ",") + '</td>';
    //        //the columns after here are not shown in Excel coz the header is only 2 columns
    //        //pTableHTML += '                     <td class="ProfitAndLoss hide" style="text-align:center;">' + (item.Account_ID == 1 || item.Account_ID == 2 ? item.FinalBalance : 0).toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ",") + '</td>';
    //        pTableHTML += '                     <td class="ProfitAndLoss hide" style="text-align:center;">' + (item.Account_ID == 1 ? Math.abs(item.FinalBalance) : (item.Account_ID == 2 ? (-1 * Math.abs(item.FinalBalance)) : 0)).toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ",") + '</td>';
    //        pTableHTML += '                 </tr>';
    //    }
    //});
    pTableHTML += '                 </tbody>';
    pTableHTML += '             </table>';

    $("#hExportedTable").html(pTableHTML);
    debugger;
    //Totals row
    var pRowTotalsHTML = "";
    //var ProfitAndLossTotal = GetColumnSum("tblCashFlow", "ProfitAndLoss");
    //pRowTotalsHTML += '                         <tr class="" style="font-size:95%;">';
    //pRowTotalsHTML += '                             <td class="" style="text-align:center;"><b><u>' + 'Profit & Loss :</u> ' + ProfitAndLossTotal.toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ",") + '</b></td>';
    //pRowTotalsHTML += '                             <td class="" style="text-align:left;"><b><u>' + '' + '</td>';
    //pRowTotalsHTML += '                         </tr>';

    var tabs = "&emsp;&emsp;&emsp;&emsp;&emsp;";
    var TotalCash = GetColumnSum("tblCashFlow", "GroupBalance");
    // var OpeningCashFlowGroup = pCashFlow.filter(x=> x.ParentGroupID == 4);
    var OpeningCashFlowGroup = pCashFlow.find(x=> x.ParentGroupID == 4).Balance;
    pRowTotalsHTML += '                 <tr class="" style="font-size:95%;">';
    pRowTotalsHTML += '                     <td class="" style="text-align:left;">'  + ("صافى النقدية وما فى حكمها") + '</td>';
    pRowTotalsHTML += '                     <td class="Balance" style="text-align:left;">' + (pOutputTo != "Excel" ? tabs : "") + TotalCash.toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ",") + '</td>';
    pRowTotalsHTML += '                 </tr>';

    pRowTotalsHTML += '                 <tr class="" style="font-size:95%;">';
    pRowTotalsHTML += '                     <td class="" style="text-align:left;">'  + ("النقدية وما فى حكمها فى بداية الفترة") + '</td>';
    pRowTotalsHTML += '                     <td class="Balance" style="text-align:left;">' + (pOutputTo != "Excel" ? tabs : "") + OpeningCashFlowGroup + '</td>';
    pRowTotalsHTML += '                 </tr>';

    pRowTotalsHTML += '                 <tr class="" style="font-size:95%;">';
    pRowTotalsHTML += '                     <td class="" style="text-align:left;">'  + ("النقدية وما فى حكمها فى نهاية الفترة") + '</td>';
    pRowTotalsHTML += '                     <td class="Balance" style="text-align:left;">' + (pOutputTo != "Excel" ? tabs : "") + (OpeningCashFlowGroup + TotalCash) + '</td>';
    pRowTotalsHTML += '                 </tr>';

    $("#tblCashFlow tbody").append(pRowTotalsHTML);
}
else {
        pTableHTML += '             <table id="tblCashFlow" class="table table-striped text-sm  table-bordered" style="">';  //style="border:solid #000 !Important;" 
pTableHTML += '                 <thead class="" style="">'
pTableHTML += '                     <tr class="" style="">';
pTableHTML += '                         <th class="">' + 'الحساب' + '</th>';
pTableHTML += '                         <th class="">' + 'الرصيد' + '</thb>';
pTableHTML += '                     </tr>'     
pTableHTML += '                 </thead>';
pTableHTML += '                 <tbody>';
if (pCashFlow != null)
{
    var tabs = "&emsp;&emsp;&emsp;&emsp;&emsp;";
    var Total = 0.00;

    $.each(pCashFlowGroups, function (i, item) {
        pTableHTML += '                 <tr class="" style="font-size:95%;">';
        pTableHTML += '                     <td class="" style="text-align:right;">' + (pOutputTo != "Excel" ? "" : "") + (pOutputTo != "Excel" ? ('<b>' + item.Name + '</b>') : item.Name) + '</td>';
        pTableHTML += '                     <td class="Balance" style="text-align:right;">' + "" + '</td>';
        pTableHTML += '                 </tr>';
        var CurrenctCashFlowGroups = pCashFlow.filter(x=> x.ParentGroupID == item.ID);
        Total = 0;
        $.each(CurrenctCashFlowGroups, function (k, account) {
            Total += account.Balance;
            pTableHTML += '                 <tr class="" style="font-size:95%;">';
            pTableHTML += '                     <td class="" style="text-align:right;">' + (pOutputTo != "Excel" ? "" : "") + (account.GroupName) + '</td>';
            pTableHTML += '                     <td class="Balance" style="text-align:right;">' + account.Balance + '</td>';
            pTableHTML += '                 </tr>';
        });
        pTableHTML += '                 <tr class="" style="font-size:95%;">';
        pTableHTML += '                     <td class="" style="text-align:right;">' + (pOutputTo != "Excel" ? "" : "") + (pOutputTo != "Excel" ? ('<b>' + item.Name.replace('التدفقات', 'صافى') + '</b>') : item.Name) + '</td>';
        pTableHTML += '                     <td class="GroupBalance" style="text-align:right;">' + (pOutputTo != "Excel" ? tabs : "") + Total + '</td>';
        pTableHTML += '                 </tr>';

    });



}
//$.each(pCashFlow, function (i, item) {
//    if (!($("#cbSuppressForZeroes").prop("checked") && item.Balance == 0)) {
//        var tabs = "";
//        for (var y = 1; y < item.AccLevel ; y++)
//            tabs += "&emsp;&emsp;";
//        pTableHTML += '                 <tr class="" style="font-size:95%;">';
//        pTableHTML += '                     <td class="" style="text-align:left;">' + (pOutputTo != "Excel" ? tabs : "") + (item.IsMain && pOutputTo != "Excel" ? ('<b>' + item.Account_Name + '</b>') : item.Account_Name) + '</td>';
//        pTableHTML += '                     <td class="FinalBalance" style="text-align:left;">' + (pOutputTo != "Excel" ? tabs : "") + (item.FinalBalance).toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ",") + '</td>';
//        //the columns after here are not shown in Excel coz the header is only 2 columns
//        //pTableHTML += '                     <td class="ProfitAndLoss hide" style="text-align:center;">' + (item.Account_ID == 1 || item.Account_ID == 2 ? item.FinalBalance : 0).toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ",") + '</td>';
//        pTableHTML += '                     <td class="ProfitAndLoss hide" style="text-align:center;">' + (item.Account_ID == 1 ? Math.abs(item.FinalBalance) : (item.Account_ID == 2 ? (-1 * Math.abs(item.FinalBalance)) : 0)).toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ",") + '</td>';
//        pTableHTML += '                 </tr>';
//    }
//});
pTableHTML += '                 </tbody>';
pTableHTML += '             </table>';

$("#hExportedTable").html(pTableHTML);
debugger;
//Totals row
var pRowTotalsHTML = "";
//var ProfitAndLossTotal = GetColumnSum("tblCashFlow", "ProfitAndLoss");
//pRowTotalsHTML += '                         <tr class="" style="font-size:95%;">';
//pRowTotalsHTML += '                             <td class="" style="text-align:center;"><b><u>' + 'Profit & Loss :</u> ' + ProfitAndLossTotal.toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ",") + '</b></td>';
//pRowTotalsHTML += '                             <td class="" style="text-align:left;"><b><u>' + '' + '</td>';
//pRowTotalsHTML += '                         </tr>';

var tabs = "&emsp;&emsp;&emsp;&emsp;&emsp;";
var TotalCash = GetColumnSum("tblCashFlow", "GroupBalance");
// var OpeningCashFlowGroup = pCashFlow.filter(x=> x.ParentGroupID == 4);
var OpeningCashFlowGroup = pCashFlow.find(x=> x.ParentGroupID == 4).Balance;
pRowTotalsHTML += '                 <tr class="" style="font-size:95%;">';
pRowTotalsHTML += '                     <td class="" style="text-align:right;">' + ("صافى النقدية وما فى حكمها") + '</td>';
pRowTotalsHTML += '                     <td class="Balance" style="text-align:right;">' + (pOutputTo != "Excel" ? tabs : "") + TotalCash.toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ",") + '</td>';
pRowTotalsHTML += '                 </tr>';

pRowTotalsHTML += '                 <tr class="" style="font-size:95%;">';
pRowTotalsHTML += '                     <td class="" style="text-align:right;">' + ("النقدية وما فى حكمها فى بداية الفترة") + '</td>';
pRowTotalsHTML += '                     <td class="Balance" style="text-align:right;">' + (pOutputTo != "Excel" ? tabs : "") + OpeningCashFlowGroup + '</td>';
pRowTotalsHTML += '                 </tr>';

pRowTotalsHTML += '                 <tr class="" style="font-size:95%;">';
pRowTotalsHTML += '                     <td class="" style="text-align:right;">' + ("النقدية وما فى حكمها فى نهاية الفترة") + '</td>';
pRowTotalsHTML += '                     <td class="Balance" style="text-align:right;">' + (pOutputTo != "Excel" ? tabs : "") + (OpeningCashFlowGroup + TotalCash) + '</td>';
pRowTotalsHTML += '                 </tr>';

$("#tblCashFlow tbody").append(pRowTotalsHTML);
}

if (pOutputTo == "Excel") {
    ExportHtmlTableToCsv_RemovingCommasForNumbers("tblCashFlow");
}
else {
    var mywindow = window.open('', '_blank');
    var ReportHTML = '';
    if ($("[id$='hf_ChangeLanguage']").val() == "en") {
        ReportHTML += '<html>';
        ReportHTML += '     <head><title>' + 'Cash Flow' + '</title><link rel="stylesheet" href="/Content/CSS/app.v2.css" type="text/css" /></head>';
        ReportHTML += '         <body style="background-color:white;">';
        ReportHTML += '             <div class="col-xs-12 text-center"><img src="/Content/Images/CompanyHeader.jpg" alt="logo"/></div>';
        ReportHTML += '             <div class="col-xs-12 text-center" style="font-size: 20px;"><b><u>Cash Flow</u></b>' + '</div>';
        ReportHTML += '             <div class="col-xs-3"><b>' + 'From : ' + '</b>' + $("#txtFromDate").val() + '</div>'
        ReportHTML += '             <div class="col-xs-3"><b>' + 'To : ' + '</b>' + $("#txtToDate").val() + '</div>'
        ReportHTML += '             <div class="col-xs-9 text-right"><b>' + 'Printed at :  ' + '</b>' + pFormattedTodaysDate + ' ' + pFormattedPrintTime + '</div>';
        //ReportHTML += '             <div class="col-xs-3"><b>' + 'Printed By :  ' + '</b>' + $("#hLoggedUserNameNotLogin").val() + '</div>';

        //ReportHTML += pTablesHTML; //Add table html in the next lines
        ReportHTML += '             <table id="tblCashFlow' + '" class="table table-striped text-sm  table-bordered" style="">';  //style="border:solid #000 !Important;" 
        ReportHTML += '             ' + $("#tblCashFlow").html();
        ReportHTML += '             </table>';

        //ReportHTML += '             <div class="col-xs-12 m-t-n text-right"><b>' + 'Total records :  ' + '</b>' + pBalanceSheet.length + '</div>';

        ReportHTML += '         <body>';
        //ReportHTML += '     <footer class="footer col-xs-12" style="width:100%; position:absolute; bottom:0;">';
        //ReportHTML += '         <div class="row text-center"><img src="/Content/Images/CompanyFooter.jpg" alt="footer"/></div>';
        //ReportHTML += '     </footer>';
        ReportHTML += '</html>';
    }
    else {
        ReportHTML += '<html dir="rtl">';
        ReportHTML += '     <head><title>' + 'التدفق النقدي' + '</title><link rel="stylesheet" href="/Content/CSS/app.v2.css" type="text/css" /></head>';
        ReportHTML += '         <body style="background-color:white;">';
        ReportHTML += '             <div class="col-xs-12 text-center"><img src="/Content/Images/CompanyHeader.jpg" alt="logo"/></div>';
        ReportHTML += '             <div class="col-xs-12 text-center" style="font-size: 20px;"><b><u>التدفق النقدي</u></b>' + '</div>';
        ReportHTML += '             <div dir="rtl" class="col-xs-12 " >';
        ReportHTML += '             <div class="col-xs-6 text-left"><b>' + 'تمت الطباعة:   ' + '</b>' + pFormattedTodaysDate + ' ' + pFormattedPrintTime + '</div>';
        ReportHTML += '             <div class="col-xs-3 "><b>' + 'إلي:  ' + '</b>' + $("#txtToDate").val() + '</div>';
        ReportHTML += '             <div class="col-xs-3 "><b>' + 'من:  ' + '</b>' + $("#txtFromDate").val() + '</div>';
        ReportHTML += '             </div>';
        //ReportHTML += '             <div class="col-xs-3"><b>' + 'Printed By :  ' + '</b>' + $("#hLoggedUserNameNotLogin").val() + '</div>';

        //ReportHTML += pTablesHTML; //Add table html in the next lines
        ReportHTML += '             <table id="tblCashFlow' + '" class="table table-striped text-sm  table-bordered" style="">';  //style="border:solid #000 !Important;" 
        ReportHTML += '             ' + $("#tblCashFlow").html();
        ReportHTML += '             </table>';

        //ReportHTML += '             <div class="col-xs-12 m-t-n text-right"><b>' + 'Total records :  ' + '</b>' + pBalanceSheet.length + '</div>';

        ReportHTML += '         <body>';
        //ReportHTML += '     <footer class="footer col-xs-12" style="width:100%; position:absolute; bottom:0;">';
        //ReportHTML += '         <div class="row text-center"><img src="/Content/Images/CompanyFooter.jpg" alt="footer"/></div>';
        //ReportHTML += '     </footer>';
        ReportHTML += '</html>';
    }

        mywindow.document.write(ReportHTML);
        mywindow.document.close();
    }

    FadePageCover(false);
}
