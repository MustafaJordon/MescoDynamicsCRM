
function SetubSalesLead_Calculate()
{
    debugger;
    pParametersWithValues = {
        pNoDays: $("#txtNoMonths").val() == "" ? 0 : ($("#txtNoMonths").val() * 30)
    };
    CallGETFunctionWithParameters("/api/CRM_Clients/SetubSalesLead_Calculate", pParametersWithValues
           , function (pData) {
               debugger;
               SetubSalesLead_BindTableRows(JSON.parse(pData[0]));
           }, null);
}

function SetubSalesLead_BindTableRows(pSetubSalesLead) {
    debugger;
    ClearAllTableRows("tblSetubSalesLead");
    //("<tr ID='" + item.ClientID +"' "+  (item.Valid == 0 ? 'style="background:#dfa8a8;"' : '')  +" ondblclick='SetubSalesLead_FillAllControls(" + item.ClientID + ");' class='" + (1 == 2 ? "text-primary" : "") + "'>"
    $.each(pSetubSalesLead, function (i, item) {
        AppendRowtoTable("tblSetubSalesLead",
        ("<tr ID='" + item.ClientID +"' ondblclick='SetubSalesLead_FillAllControls(" + item.ClientID + ");' class='" + (1 == 2 ? "text-primary" : "") + "'>"
            + "<td class='ID'> <input name='Delete' type='checkbox' value='" + item.ClientID + "' /></td>"
            + "<td class='ClientID' value='" + item.ClientID + "'>" + (item.ClientName == 0 ? "" : item.ClientName) + "</td>"
            + "<td class='ACTION' value='" + item.ACTION + "'>" + (item.ACTION == 0 ? "" : item.ACTION) + "</td>"
            + "<td class='LastActionDate'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.LastActionDate)) < 1 ? "" : ConvertDateFormat(GetDateWithFormatMDY(item.LastActionDate))) + "</td>"
            + "<td class='DaysToLastAction' value='" + item.DaysToLastAction + "'>" + (item.DaysToLastAction == 0 ? "" : item.DaysToLastAction) + "</td>"
            + "<td class='DaysDiff' value='" + item.DaysDiff + "'>" + (item.DaysDiff == 0 ? "" : item.DaysDiff) + "</td>"
            + "<td class='Valid' value='" + item.Valid + "'>" + (item.Valid == 0 ? "False" : "True") + "</td>"
            + "<td class='Code hide' value='" + item.Code + "'>" + (item.Code == 0 ? "" : item.Code) + "</td></tr>"));

    });

}

function SetubSalesLead_Save() {
    debugger;
    //var tr = $("tr[ID='" + pID + "']");
    var ClientID = "";
    var ClientName  = "";
    var Code  = "";
    var LastActionDate  = "";
    var ACTION  = "";
    var DaysToLastAction = "";//عدد الايام من اخر متابعه حتي تاريخ اليوم
    var DaysDiff = "";// Not valid عدد الايام من بعد ما أصبح 
    var Valid  = "";
    var NoDays = "";//عدد الأيام اللي انا حاسب على اساسها اللى هى ببقي حاطتها بالشهور
    var NumberOfInsertedRows = 0;
    $('#tblSetubSalesLead > tbody  > tr').each(function (index, tr) {
        debugger;
        if (($(tr).find("td.Valid").attr('value')) == 0)
        {
            ClientID += (($(tr).find("td.ClientID").attr('value')) == "" ? 0 : ($(tr).find("td.ClientID").attr('value'))).toString() + ","
            ClientName += (($(tr).find("td.ClientID").text()) == "" ? 0 : ($(tr).find("td.ClientID").text())).toString() + ","
            Code += (($(tr).find("td.Code").attr('value')) == "" ? 0 : (($(tr).find("td.Code").attr('value')))).toString() + ","
            LastActionDate += (($(tr).find("td.LastActionDate").text()) == "" ? "01/01/1900" : ConvertDateFormat($(tr).find("td.LastActionDate").text())).toString() + ","
            ACTION += (($(tr).find("td.ACTION").attr('value')) == "" ? 0 : ($(tr).find("td.ACTION").attr('value'))).toString() + ","
            DaysToLastAction += (($(tr).find("td.DaysToLastAction").attr('value')) == "" ? 0 : (($(tr).find("td.DaysToLastAction").attr('value')))).toString() + ","
            DaysDiff += (($(tr).find("td.DaysDiff").text()) == "" ? 0 : (($(tr).find("td.DaysDiff").text()))).toString() + ","
            Valid += (($(tr).find("td.Valid").attr('value')) == "" ? 0 : (($(tr).find("td.Valid").attr('value')))).toString() + ","
            NumberOfInsertedRows += 1;

        }
       //NoDays += parseInt($(tr).find("td.NoDays").attr('value')) == "" ? 0 : (parseInt($(tr).find("td.NoDays").attr('value'))) + ","
    });
    if (ClientID.length == "")
    {
        swal("Sorry", "There is no Sales Leads to save.")
    }
    else
    {
        var pParametersWithValues = {
            pID: 0,
            pClientID: ClientID.slice(0, -1),
            pClientName: ClientName.slice(0, -1),
            pCode: Code.slice(0, -1),
            pLastActionDate: LastActionDate.slice(0, -1),
            pACTION: ACTION.slice(0, -1),
            pDaysToLastAction: DaysToLastAction.slice(0, -1),
            pDaysDiff: DaysDiff.slice(0, -1),
            pValid: Valid.slice(0, -1),
            pNoDays: ($('#txtNoMonths').val() * 30),
            pNumberOfInsertedRows: NumberOfInsertedRows
        };
        CallGETFunctionWithParameters("/api/CRM_Clients/SetubSalesLead_Save", pParametersWithValues
               , function (pData) {
                   debugger;
                   if (JSON.parse(pData[0]) == "") {
                       swal("Success", "Data Saved Successfully")
                   }
                   //SetubSalesLead_BindTableRows(JSON.parse(pData[0]));
               }, null);
    }
   
}

