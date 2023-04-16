// City Country ---------------------------------------------------------------
// Bind Rates Table Rows
function Rates_BindTableRows(pRates) {
    debugger;
    $("#hl-menu-MasterData").parent().addClass("active");
    ClearAllTableRows("tblRates");
    $.each(pRates, function (i, item) {
        debugger;
        //editControlsText = " class='btn btn-xs btn-rounded btn-info float-right' > <i class='fa fa-pencil' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + TranslateString("Edit") + "</span>";
        AppendRowtoTable("tblRates",
        ("<tr ID='" + item.ID + "' ondblclick='Rates_EditByDblClick(" + item.ID + ");'>"
                    + "<td class='ID'> <input name='Delete' type='checkbox' value='" + item.ID + "' /></td>"
                    //+ "<td class='CountryID' val='" + item.CountryID + "'>" + item.CountryID + "</td>"
                    + "<td class='Code' val='" + item.Code + "'>" + item.Code + "</td>"
                    + "<td class='Name' val='" + item.Name + "'>" + item.Name + "</td>"
                    + "<td class='RateFromDate' val='" + (ConvertDateFormat(GetDateWithFormatMDY(item.RateFromDate))) + "'>" + (ConvertDateFormat(GetDateWithFormatMDY(item.RateFromDate))) + "</td>"//(ConvertDateFormat(GetDateWithFormatMDY(pInvoiceHeader.DueDate)))
                    + "<td class='RateToDate' val='" + (ConvertDateFormat(GetDateWithFormatMDY(item.RateToDate))) + "'>" + (ConvertDateFormat(GetDateWithFormatMDY(item.RateToDate))) + "</td>"
                    + "<td class='Remarks' val='" + item.Remarks + "'>" + item.Remarks + "</td>"
                    //+ "<td class='hRateIDe'><a href='#RateRegions' data-toggle='modal' onclick='Rates_FillControls(" + item.ID + ");' " + editControlsText + "</a></td></tr>"));
                    + "</tr>"));
    });
    ApplyPermissions();
    BindAllCheckboxonTable("tblRates", "ID");
    CheckAllCheckbox("ID");
    HighlightText("#tblRates>tbody>tr", $("#txt-Search").val().trim());
    if (showDeleteFailMessage) {//sherif: if deletion failed due to foreign key, then this message is shown
        swal(strSorry, strDeleteFailMessage, "warning");
        showDeleteFailMessage = false;
    }
    $("#hl-homepage").on("click", function () { LoadViews("hl-homepage"); });
}

function RateRegions_BindTableRows(pRateRegions) {
    debugger;
    $("#hl-menu-MasterData").parent().addClass("active");
    ClearAllTableRows("tblRateRegions");
    $.each(pRateRegions, function (i, item) {
        debugger;
        //editControlsText = " class='btn btn-xs btn-rounded btn-info float-right' > <i class='fa fa-pencil' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + TranslateString("Edit") + "</span>";
        AppendRowtoTable("tblRateRegions",
        ("<tr ID='" + item.ID + "' ondblclick='RateRegions_EditByDblClick(" + item.ID + ");'>"
                    + "<td class='ID'> <input name='Delete' type='checkbox' value='" + item.ID + "' /></td>"
                    //+ "<td class='CountryID' val='" + item.CountryID + "'>" + item.CountryID + "</td>"
                    + "<td class='CityIDFrom hide' val='" + (item.CityIDFrom == 0 ? "" : item.CityIDFrom) + "'>" + (item.CityNameFrom == 0 ? "" : item.CityNameFrom) + "</td>"
                    + "<td class='RegionIDFrom' val='" + (item.RegionIDFrom == 0 ? "" : item.RegionIDFrom) + "'>" + (item.RegionNameFrom == 0 ? "" : item.RegionNameFrom) + "</td>"
                    + "<td class='CityIDTo hide' val='" + (item.CityIDTo == 0 ? "" : item.CityIDTo) + "'>" + (item.CityNameTo == 0 ? "" : item.CityNameTo) + "</td>"
                    + "<td class='RegionIDTo' val='" + (item.RegionIDTo == 0 ? "" : item.RegionIDTo) + "'>" + (item.RegionNameTo == 0 ? "" : item.RegionNameTo) + "</td>"
                    + "<td class='PackageTypeName ' val='" + (item.PackageTypeName == 0 ? "" : item.PackageTypeName) + "'>" + (item.PackageTypeName == 0 ? "" : item.PackageTypeName) + "</td>"
                    + "<td class='Quantity ' val='" + (item.Quantity == 0 ? "" : item.Quantity) + "'>" + (item.Quantity == 0 ? "" : item.Quantity) + "</td>"
                    + "<td class='Cost' val='" + (item.Cost == 0 ? "" : item.Cost) + "'>" + (item.Cost == 0 ? "" : item.Cost) + "</td>"
                    + "<td class='Selling' val='" + (item.Selling == 0 ? "" : item.Selling) + "'>" + (item.Selling == 0 ? "" : item.Selling) + "</td>"
                    + "<td class='Remarks hide' val='" + (item.Remarks == 0 ? "" : item.Remarks) + "'>" + (item.Remarks == 0 ? "" : item.Remarks) + "</td>"
                    + "<td class='PackageTypeID hide' val='" + (item.PackageTypeID == 0 ? "" : item.PackageTypeID) + "'>" + (item.PackageTypeID == 0 ? "" : item.PackageTypeID) + "</td>"
                    //+ "<td class='hRateIDe'><a href='#RateRegions' data-toggle='modal' onclick='RateRegions_FillControls(" + item.ID + ");' " + editControlsText + "</a></td></tr>"));
                    + "</tr>"));
    });
    ApplyPermissions();
    BindAllCheckboxonTable("tblRateRegions", "ID");
    CheckAllCheckbox("ID");
    HighlightText("#tblRateRegions>tbody>tr", $("#txt-SearchRateRegions").val().trim());
    if (showDeleteFailMessage) {//sherif: if deletion failed due to foreign key, then this message is shown
        swal(strSorry, strDeleteFailMessage, "warning");
        showDeleteFailMessage = false;
    }
    $("#hl-homepage").on("click", function () { LoadViews("hl-homepage"); });
}
function Rates_EditByDblClick(pID) {
    //jQuery("#RateRegionsModal").modal("show");
    jQuery("#RateModal").modal("show");
    Rates_FillControls(pID);
}

function RateRegions_EditByDblClick(pID) {
    //jQuery("#RateRegionsModal").modal("show");
    jQuery("#RateRegionsModal").modal("show");
    RateRegions_FillControls(pID);
}
// Loading with data
function Rates_LoadingWithPaging() {
    debugger;
    LoadWithPaging("div-Pager", "select-page-size", "spn-first-page-row", "spn-last-page-row", "spn-total-count", "div-Text-Total", "/api/Rates/LoadWithPaging", $("#div-Pager li.active a").text(), $('#select-page-size').val().trim(),
        function (pTabelRows) {
            Rates_BindTableRows(pTabelRows);
            //Rates_ClearAllControls();
        });
    HighlightText("#tblRates>tbody>tr", $("#txt-Search").val().trim());
}

function RateRegions_LoadingWithPaging() {
    debugger;
    var pWhereClause = " WHERE RateID = " + $('#hRateID').val() + " AND (CityNameFrom like '%" + $('#txt-SearchRateRegions').val() + "%' OR CityNameTo like '%" + $('#txt-SearchRateRegions').val() + "%' OR RegionNameFrom like '%" + $('#txt-SearchRateRegions').val() + "%' OR RegionNameTo like '%" + $('#txt-SearchRateRegions').val() + "%' )";
    var pOrderBy = "ID DESC";
    var pPageNumber = 1;
    var pPageSize = 100;
    var pControllerParameters = { pPageNumber: pPageNumber, pPageSize: pPageSize, pWhereClause: pWhereClause, pOrderBy: pOrderBy }

    LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned("div-Pager-Regions", "select-page-size-Regions", "spn-first-page-row-Regions", "spn-last-page-row-Regions", "spn-total-count-Regions", "div-Text-Total-Regions", "/api/Rates/RateRegions_LoadingWithPagingByRateID", pWhereClause, pOrderBy, pPageNumber, pPageSize, pControllerParameters
       , function (pData) {
           RateRegions_BindTableRows(JSON.parse(pData[0]));
           RateRegions_ClearAllControls();
       });
}

function Rates_Insert_Update(pSaveandAddNew) {
    debugger;
    if (ValidateForm("form", "RateModal")) {
        CallPOSTFunctionWithParameters("/api/Rates/Insert_Update", {
            ID: $('#hRateID').val(),
            Code: $("#txtCode").val().trim(),
            Name: $("#txtName").val().trim(),
            RateFromDate: $("#txtRateFromDate").val() == "" ? "01/01/1900" : ConvertDateFormat($("#txtRateFromDate").val().trim()),
            RateToDate: $("#txtRateToDate").val() == "" ? "01/01/1900" : ConvertDateFormat($("#txtRateToDate").val().trim()),
            Remarks: $("#txtRemarks").val().trim()
            //PackageTypeID: $("#slPackageTypeID").val(), 
            //Quantity: $("#txtQuantity").val()
        }
                    , function (pData) {
                        debugger;
                        Rates_LoadingWithPaging();
                        if ($('#hRateID').val() == 0 || $('#hRateID').val() == '') {
                            jQuery('#RateModal').modal('hide');
                        }
                    }, null);
    }
    //InsertUpdateFunction("form", "/api/Rates/Insert_Update", {
    //    ID: $('#hRateID').val(), Code: $("#txtCode").val().trim(), Name: $("#txtName").val().trim(),
    //    RateFromDate: $("#txtRateFromDate").val().trim(),
    //    RateToDate: $("#txtRateToDate").val().trim(),
    //    Remarks: $("#txtRemarks").val().trim(),
    //}, pSaveandAddNew, "RateRegions", function () { Rates_LoadingWithPaging(); });
}
// calling this function for update
//function Rates_Insert_Update(pSaveandAddNew) {
//    debugger;
//    InsertUpdateFunction("form", "/api/Rates/Update", { pID: $("#hRateRegionsID").val(), pCountryID: $('#slCountry option:selected').val(), pCode: $("#txtCode").val().trim(), pName: $("#txtName").val().trim(), pLocalName: $("#txtLocalName").val().trim() }, pSaveandAddNew, "RateRegions", function () { Rates_LoadingWithPaging(); });
//}

function Rates_Delete(pID) {
    DeleteListFunction("/api/Rates/DeleteByID", { "pID": pID }, function () { Rates_LoadingWithPaging(); });
}
function ClearRateModal()
{
    debugger;
    ClearAll("#RateModal", null);
    $('#tblRateRegions tbody ').html('');
    $('#hRateID').val(0);
    $("#btnSave").attr("onclick", "Rates_Insert_Update(false);");
}
function Rates_DeleteList() {
    debugger;
    //Confirmation message to delete
    if (GetAllSelectedIDsAsString('tblRates') != "")
        swal({
            title: "Are you sure?",
            text: "The selected records will be deleted permanently!",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, delete!",
            closeOnConfirm: true
        },
        //callback function in case of success
        function () {
            DeleteListFunction("/api/Rates/Delete", { "pRatesIDs": GetAllSelectedIDsAsString('tblRates') }, function () { Rates_LoadingWithPaging(); });
        });
        //DeleteListFunction("/api/Rates/Delete", { "pRatesIDs": GetAllSelectedIDsAsString('tblRates') }, function () { Rates_LoadingWithPaging(); });
}

function DeleteRateRegions() {
    debugger;
    //Confirmation message to delete
    if (GetAllSelectedIDsAsString('tblRateRegions') != "")
        swal({
            title: "Are you sure?",
            text: "The selected records will be deleted permanently!",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, delete!",
            closeOnConfirm: true
        },
        //callback function in case of success
        function () {
            DeleteListFunction("/api/Rates/DeleteRateRegions", { "pRateRegionIDs": GetAllSelectedIDsAsString('tblRateRegions') },
                function () {
                    RateRegions_LoadingWithPaging();
                });
        });
    //DeleteListFunction("/api/Rates/Delete", { "pRatesIDs": GetAllSelectedIDsAsString('tblRates') }, function () { Rates_LoadingWithPaging(); });
}

//after pressing edit, this function fills the data
function Rates_FillControls(pID) {
    debugger;
    ClearAll("#RateModal", null);
    $('#hRateID').val(pID);
    var tr = $("tr[ID='" + pID + "']");
  
    //$('#hRateID').val($(tr).find("td.ID").text());
    $("#txtCode").val($(tr).find("td.Code").text());
    $("#txtName").val($(tr).find("td.Name").text());
    $("#txtRateFromDate").val($(tr).find("td.RateFromDate").text());
    $("#txtRateToDate").val($(tr).find("td.RateToDate").text());
    $("#txtRemarks").val($(tr).find("td.Remarks").text());
    //$("#slPackageTypeID").val($(tr).find("td.PackageTypeID").text());
    //$("#txtQuantity").val($(tr).find("td.Quantity").text());

    RateRegions_LoadingWithPaging();
    $("#btnSave").attr("onclick", "Rates_Insert_Update(false);");
    $("#btnSaveandNew").attr("onclick", "Rates_Insert_Update(true);");
}

function RateRegions_FillControls(pID) {
    debugger;
    ClearAll("#RateRegionsModal", null);
    $('#hRateRegionsID').val(pID);
    var tr = $("tr[ID='" + pID + "']");

    $('#slRegionFrom').html($('#slRegionBase').html());
    $('#slRegionTo').html($('#slRegionBase').html());
    $("#slCityFrom").val($(tr).find("td.CityIDFrom").attr('val'));
    $("#slRegionFrom").val($(tr).find("td.RegionIDFrom").attr('val'));
    $("#slCityTo").val($(tr).find("td.CityIDTo").attr('val'));
    $("#slRegionTo").val($(tr).find("td.RegionIDTo").attr('val'));
    $("#txtCost").val($(tr).find("td.Cost").attr('val'));
    $("#txtSelling").val($(tr).find("td.Selling").attr('val'));
    $("#txtRemarksRateRegions").val($(tr).find("td.Remarks").attr('val'));
    $("#slPackageTypeID").val(($(tr).find("td.PackageTypeID").attr('val') == "0" ? "":($(tr).find("td.PackageTypeID").attr('val')) ));
    $("#txtQuantity").val(($(tr).find("td.Quantity").attr('val') == "0" ?  "":($(tr).find("td.Quantity").attr('val'))));

    $("#btnSaveRateRegions").attr("onclick", "RateRegions_Insert_Update(false);");
    $("#btnSaveandNewRateRegions").attr("onclick", "RateRegions_Insert_Update(true);");
}
function Rates_ClearAllControls() {
    debugger;
    //ClearAllControls(new Array("hRateRegionsID", "txtCode", "txtName", "txtLocalName"), new Array("slCountry"), null);
    ClearAll("#RateRegions", null);
    Countries_GetList( pDefaults.DefaultCountryID);
    $('#hRateID').val(0);
    $("#btnSave").attr("onclick", "Rates_Insert_Update(false);");
    $("#btnSaveandNew").attr("onclick", "Rates_Insert_Update(true);");
    $("#cb-CheckAll").prop('checked', false);

}

function RateRegions_ClearAllControls() {
    debugger;
    //ClearAll("#RateRegions", null);
   
    //$('#hRateID').val(0);
    //$("#btnSave").attr("onclick", "Rates_Insert(false);");
    //$("#btnSaveandNew").attr("onclick", "Rates_Insert(true);");
    //$("#cb-CheckAll").prop('checked', false);

}
// City Country ---------------------------------------------------------------

//to fill the select box
function Countries_GetList(pID) {//pID is used in case of editing to set the Country code or name
    //parameters: ID, strFnName, First Row in select list, select list name
    GetListWithName(pID, "/api/Countries/LoadAll", "Select Country", "slCountry");
}

function LoadAllData()
{
    debugger;
    CallGETFunctionWithParameters("/api/Rates/LoadAll", { pWhereClause: " Where 1=1", Tables: "City-Region-PackageTypes" }
                   , function (pData) {
                       FillListFromObject(0, 2, TranslateString("Select City"), "slCityFrom", pData[0], null);
                       FillListFromObject(0, 2, TranslateString("Select Region"), "slRegionFrom", pData[1], null);
                       FillListFromObject(0, 2, TranslateString("Select City"), "slCityTo", pData[0], null);
                       FillListFromObject(0, 2, TranslateString("Select Region"), "slRegionTo", pData[1], null);
                       FillListFromObject(0, 2, TranslateString("Select Region"), "slRegionBase", pData[1], null);
                       FillListFromObject(0, 2, TranslateString("Select Package Type"), "slPackageTypeID", pData[2], null);
                       
                   }, null);
}

function GetRegionsInCity(From_To)
{
    debugger;
    var City = (From_To == "From") ? $('#slCityFrom').val() : $('#slCityTo').val();
    var Region_From_To = (From_To == "From") ? "slRegionFrom" : "slRegionTo";
    CallGETFunctionWithParameters("/api/Rates/LoadAll", { pWhereClause: " Where FactoryCityID=" + City, Tables: "Region" }
                   , function (pData) {
                       FillListFromObject(0, 2, TranslateString("Select Region"), Region_From_To, pData[0], null);
                   }, null);
}

function ClearRateRegions()
{
    debugger;
    ClearAll("#RateRegionsModal");
    if(parseInt($('#hRateID').val()) >= 1)
    {
        jQuery("#RateRegionsModal").modal("show");
    }
    else
    {
        jQuery("#RateRegionsModal").modal("hide");
        swal("Sorry", "You must insert rate first", "warning");
     
    }
    $('#hRateRegionsID').val(0);
    $("#btnSaveRateRegions").attr("onclick", "RateRegions_Insert_Update(false);");
    $("#btnSaveandNewRateRegions").attr("onclick", "RateRegions_Insert_Update(true);");
    //$("#cb-CheckAll").prop('checked', false);
}

function RateRegions_Insert_Update(pSaveandAddNew)
{
    debugger;
    if (ValidateForm("form", "RateRegionsModal")) {
        CallPOSTFunctionWithParameters("/api/Rates/RateRegionsInsert_Update",
            {
                ID: $('#hRateRegionsID').val(),
                RateID: $('#hRateID').val(),
                CityIDFrom: $("#slCityFrom").val(),
                RegionIDFrom: $("#slRegionFrom").val(),
                CityIDTo: $("#slCityTo").val(),
                RegionIDTo: $("#slRegionTo").val(),
                Cost: $("#txtCost").val(),
                Selling: $("#txtSelling").val(),
                Remarks: $("#txtRemarksRateRegions").val(),
                PackageTypeID: $("#slPackageTypeID").val(),
                Quantity: $("#txtQuantity").val(),

            }, function (pData) {
                debugger;
                RateRegions_LoadingWithPaging();
                if (pSaveandAddNew == false)
                    jQuery('#RateRegionsModal').modal('hide');
            }, null);
    }
}
function RateRegions_Update()
{

}