﻿var maxReceiveDetailsSerialIDInTable = 0; //used for when adding new row then make td control names unique
function ApplySelectListSearch() {
    debugger;
    $("#slFilterWarehouse").css({ "width": "100%" }).select2();
    $("#slFilterWarehouse").trigger("change");
    $("#slFilterPurchaseItem").css({ "width": "100%" }).select2();
    $("#slFilterPurchaseItem").trigger("change");
    $("#slFilterCustomer").css({ "width": "100%" }).select2();
    $("#slFilterCustomer").trigger("change");
    
    $("#slWarehouse").css({ "width": "100%" }).select2();
    $("#slWarehouse").trigger("change");
    $("#slCustomer").css({ "width": "100%" }).select2();
    $("#slCustomer").trigger("change");
    $("#slReceiveDetailsPurchaseItem").css({ "width": "100%" }).select2();
    $("#slReceiveDetailsPurchaseItem").trigger("change");
    $("#slReceiveDetailsArea").css({ "width": "100%" }).select2();
    $("#slReceiveDetailsArea").trigger("change");
    $("#slReceiveDetailsLocation").css({ "width": "100%" }).select2();
    $("#slReceiveDetailsLocation").trigger("change");
    
    $("div[tabindex='-1']").removeAttr('tabindex');
}
function ApplySelectListSearch_OnlyChange() {
    $("#slCustomer").css({ "width": "100%" }).select2();
    $("#slReceiveDetailsPurchaseItem").css({ "width": "100%" }).select2();
    $("#slWarehouse").css({ "width": "100%" }).select2();
    $("#slReceiveDetailsArea").css({ "width": "100%" }).select2();
    $("#slReceiveDetailsLocation").css({ "width": "100%" }).select2();
}
function Receive_Initialize() {
    debugger;
    $("#hl-menu-Warehousing").parent().siblings().removeClass("active");
    strBindTableRowsFunctionName = "Receive_BindTableRows";
    strLoadWithPagingFunctionName = "/api/Receive/LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned";

    var pWhereClause = " WHERE 1=1";
    var pOrderBy = "ID DESC";
    var pPageNumber = 1;
    var pPageSize = 10;
    var pControllerParameters = { pIsLoadArrayOfObjects: true, pLanguage: $("[id$='hf_ChangeLanguage']").val(), pPageNumber: pPageNumber, pPageSize: pPageSize, pWhereClause: pWhereClause, pOrderBy: pOrderBy }
    LoadView("/Warehousing/Transactions/Receive", "div-content"
        , function () {
            if (pDefaults.UnEditableCompanyName == "GBL")
                $(".classShowForGBL").removeClass("hide");
            if (pDefaults.UnEditableCompanyName == "NIL")
                $(".classAddSerialToPalletLbl").text("PalletID/Serial");
            
            if ($("[id$='hf_ChangeLanguage']").val() == "ar") $(".swapChildrenClass:not(.reversed)").reverseChildren();
            LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned("div-Pager", "select-page-size", "spn-first-page-row", "spn-last-page-row", "spn-total-count", "div-Text-Total", strLoadWithPagingFunctionName, pWhereClause, pOrderBy, pPageNumber, pPageSize, pControllerParameters
                    , function (pData) {
                        var pWarehouse = pData[2];
                        var pPurchaseItem = pData[3];
                        var pReceiveDetailsStatus = pData[4];
                        FillListFromObject(null, 2, null/*pStrFirstRow*/, "slWarehouse", pWarehouse, null);
                        FillListFromObject(null, 2, "<--Select-->", "slFilterWarehouse", pWarehouse, null);
                        //FillListFromObject(null, 9, "<--Select-->", "slReceiveDetailsPurchaseItem", pPurchaseItem, function () { $("#slFilterPurchaseItem").html($("#slReceiveDetailsPurchaseItem").html()); });
                        FillListFromObject(null, 2, null/*pStrFirstRow*/, "slReceiveDetailsStatus", pReceiveDetailsStatus, null);
                        $("#slCustomer").html($("#hReadySlCustomers").html());
                        $("#slFilterCustomer").html($("#hReadySlCustomers").html());
                        ApplySelectListSearch();
                        Receive_BindTableRows(JSON.parse(pData[0]));
                    });
            CallGETFunctionWithParameters("/api/PurchaseItem/LoadAll"
                , { pWhereClause: "WHERE 1=1" }
                , function (pData) {
                    var pPurchaseItem = pData[0];
                    //FillListFromObject(null, 9, "<--Select-->", "slReceiveDetailsPurchaseItem", pPurchaseItem, function () { $("#slFilterPurchaseItem").html($("#slReceiveDetailsPurchaseItem").html()); ApplySelectListSearch(); });
                    Fill_SelectInputAfterLoadData_WithMultiAttrAndMultiFields(pPurchaseItem, "ID", "Code,Name,PartNumber", " : ", "<--Select-->", "#slReceiveDetailsPurchaseItem", null, "", function () { $("#slFilterPurchaseItem").html($("#slReceiveDetailsPurchaseItem").html()); ApplySelectListSearch(); })
                }
                , null, true);
        }
        , function () { Receive_ClearAllControls(); }
        , function () { Receive_DeleteList(); }
    );
}
function Receive_BindTableRows(pReceive) {
    debugger;
    $("#hl-menu-Warehousing").parent().addClass("active");
    ClearAllTableRows("tblReceive");
    //editControlsText = " class='btn btn-xs btn-rounded btn-info float-right' > <i class='fa fa-pencil' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + TranslateString("Edit") + "</span>";
    $.each(pReceive, function (i, item) {
        AppendRowtoTable("tblReceive",
        ("<tr ID='" + item.ID + "' ondblclick='Receive_FillAllControls(" + item.ID + ");' class='" + (item.IsFinalized ? "text-primary" : "") + "'>"
            + "<td class='ID'> <input name='Delete' type='checkbox' value='" + item.ID + "' /></td>"
            + "<td class='Code'>" + item.Code + "</td>"
            + "<td class='WarehouseID hide'>" + item.WarehouseID + "</td>"
            + "<td class='CustomerID hide'>" + item.CustomerID + "</td>"
            + "<td class='CustomerName'>" + item.CustomerName + "</td>"
            //+ "<td class='FromDate'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.FromDate)) < 1 ? "" : ConvertDateFormat(GetDateWithFormatMDY(item.FromDate))) + "</td>"
            //+ "<td class='ToDate'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.ToDate)) < 1 ? "" : ConvertDateFormat(GetDateWithFormatMDY(item.ToDate))) + "</td>"
            //+ "<td class='StorageLimit hide'>" + item.StorageLimit + "</td>"
            //+ "<td class='StorageUnitID hide'>" + item.StorageUnitID + "</td>"
            //+ "<td class='StorageUnitCode hide'>" + (item.StorageUnitCode == 0 ? "" : item.StorageUnitCode) + "</td>"
            //+ "<td class='IsByPallet hide'> <input type='checkbox' id='cbIsByPallet" + item.ID + "' disabled='disabled' " + (item.IsByPallet ? " checked='checked' " : "") + " /></td>"
            //+ "<td class='NumberOfPallets hide'>" + item.NumberOfPallets + "</td>"
            //+ "<td class='CurrencyID hide'>" + item.CurrencyID + "</td>"
            //+ "<td class='CurrencyCode'>" + item.CurrencyCode + "</td>"
            + "<td class='ReceiveDate'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.ReceiveDate)) < 1 ? "" : ConvertDateFormat(GetDateWithFormatMDY(item.ReceiveDate))) + "</td>"
            + "<td class='Status'>" + item.StatusName + "</td>"
            + "<td class='OperationID hide'>" + item.OperationID + "</td>"
            + "<td class='OperationCode hide'>" + item.OperationCode + "</td>"
            + "<td class='IsFinalized hide'> <input type='checkbox' id='cbIsFinalized" + item.ID + "' disabled='disabled' " + (item.IsFinalized ? " checked='checked' " : "") + " /></td>"
            + "<td class='CreatorName'>" + item.CreatorName + "</td>"
            + "<td class='ModificatorName'>" + item.ModificatorName + "</td>"
            + "<td class='hide'><a href='#ReceiveModal' data-toggle='modal' onclick='Receive_FillControls(" + item.ID + ");' " + editControlsText + "</a></td></tr>"));
    });
    ApplyPermissions();
    BindAllCheckboxonTable("tblReceive", "ID");
    CheckAllCheckbox("ID");
    HighlightText("#tblReceive>tbody>tr", $("#txt-Search").val().trim());
    if (showDeleteFailMessage) {//sherif: if deletion failed due to foreign key, then this message is shown
        swal(strSorry, strDeleteFailMessage);
        showDeleteFailMessage = false;
    }
    $("#hl-homepage").on("click", function () { LoadViews("hl-homepage"); });
}
function Receive_LoadingWithPaging() {
    debugger;
    var pWhereClause = Receive_GetWhereClause();
    var pPageSize = $('#select-page-size').val();
    var pPageNumber = ($("#div-Pager li.active a").text() == "" ? 1 : $("#div-Pager li.active a").text());
    var pOrderBy = "ID DESC";
    var pControllerParameters = { pIsLoadArrayOfObjects: false, pLanguage: $("[id$='hf_ChangeLanguage']").val(), pPageNumber: pPageNumber, pPageSize: pPageSize, pWhereClause: pWhereClause, pOrderBy: pOrderBy }
    LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned("div-Pager", "select-page-size", "spn-first-page-row", "spn-last-page-row", "spn-total-count", "div-Text-Total", strLoadWithPagingFunctionName, pWhereClause, pOrderBy, pPageNumber, pPageSize, pControllerParameters, function (pData) { Receive_BindTableRows(JSON.parse(pData[0])); });
    HighlightText("#tblReceive>tbody>tr", $("#txt-Search").val().trim());
}
function Receive_GetWhereClause() {
    debugger;
    var pWhereClause = "WHERE 1=1" + "\n";
    if ($("#slFilterWarehouse").val() != "" && $("#slFilterWarehouse").val() != 0)
        pWhereClause += "AND WarehouseID =" + $("#slFilterWarehouse").val() + "\n";
    if ($("#slFilterCustomer").val() != "" && $("#slFilterCustomer").val() != 0)
        pWhereClause += "AND CustomerID =" + $("#slFilterCustomer").val() + "\n";
    if ($("#txtFilterCode").val().trim() != "")
        pWhereClause += "AND CodeSerial=" + $("#txtFilterCode").val().trim() + "\n";
    if ($("#txt-Search").val().trim() != "")
        pWhereClause += "AND Code LIKE N'%" + $("#txt-Search").val().trim() + "%'" + "\n";
    if ($("#slFilterPurchaseItem").val() != "" && $("#slFilterPurchaseItem").val() != 0 && $("#slFilterPurchaseItem").val() != null)
        pWhereClause += "AND (SELECT COUNT(1) FROM WH_ReceiveDetails RD_Temp WHERE vwWH_Receive.ID=RD_Temp.ReceiveID AND RD_Temp.PurchaseItemID=" + $("#slFilterPurchaseItem").val() + ") > 0 " + " \n";
    return pWhereClause;
}
function Receive_ClearAllControls() {
    debugger;
    $("#tblReceiveDetails tbody").html("");

    //$("#lblReceiveMaxWeight").html("<span> : </span><span>" + 0 + "</span>");
    //$("#lblReceiveMaxVolume").html("<span> : </span><span>" + 0 + "</span>");
    ClearAll("#ReceiveModal");

    $("#txtReceiveDate").val(getTodaysDateInddMMyyyyFormat());
    $(".classDisableForFinalized").removeAttr("disabled");
    ApplySelectListSearch_OnlyChange();

    $("#btnSave").attr("onclick", "Receive_Save(false);");
    $("#btnSaveAndAddNew").attr("onclick", "Receive_Save(true);");
    $("#cb-CheckAll").prop('checked', false);
}
function Receive_FillAllControls(pID) {
    debugger;
    FadePageCover(true);
    //$("#txtNumberOfLevelsPerReceive").attr("disabled", "disabled");
    //$("#txtNumberOfTraysPerLevel").attr("disabled", "disabled");
    ClearAll("#ReceiveModal");
    $("#tblReceiveDetails tbody").html("");
    jQuery("#ReceiveModal").modal("show");
    var pParametersWithValues = {
        pHeaderID: pID
    };
    CallGETFunctionWithParameters("/api/Receive/LoadHeaderWithDetails", pParametersWithValues
        , function (pData) {
            var pReceiveHeader = JSON.parse(pData[0]);
            var pReceiveDetails = JSON.parse(pData[1]);
            if (pReceiveHeader.StatusName.toUpperCase() == "FINALIZED")
                $(".classDisableForFinalized").attr("disabled", "disabled");
            else
                $(".classDisableForFinalized").removeAttr("disabled");
            $("#hID").val(pID);
            $("#lblShown").html(": " + pReceiveHeader.Code);
            $("#txtCode").val(pReceiveHeader.Code);
            $("#slWarehouse").val(pReceiveHeader.WarehouseID == 0 ? "" : pReceiveHeader.WarehouseID);
            $("#slCustomer").val(pReceiveHeader.CustomerID == 0 ? "" : pReceiveHeader.CustomerID);
            $("#txtReceiveDate").val(Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(pReceiveHeader.ReceiveDate)) < 1 ? "" : ConvertDateFormat(GetDateWithFormatMDY(pReceiveHeader.ReceiveDate)));
            $("#txtStatus").val(pReceiveHeader.StatusName);
            if (pReceiveHeader.OperationCode == 0)
                $("#slOperation").html("<option value=''></option>");
            else
                $("#slOperation").html("<option value=" + pReceiveHeader.OperationID + ">" + pReceiveHeader.OperationCode + "</option>");
            $("#cbIsFinalized").prop("checked", pReceiveHeader.IsFinalized);
            $("#txtNotes").val(pReceiveHeader.Notes == 0 ? "" : pReceiveHeader.Notes);

            ReceiveDetails_BindTableRows(pReceiveDetails);
            $("#btnSave").attr("onclick", "Receive_Save(false);");
            $("#btnSaveAndAddNew").attr("onclick", "Receive_Save(true);");
            ApplySelectListSearch_OnlyChange();
            FadePageCover(false);
        }
        , null);
}
//pReleaseDate: ($("#txtOperationReleaseDate").val().trim() == "" ? "0" : PadDateWithZeroes($("#txtOperationReleaseDate").val().trim())),
function Receive_Save(pSaveAndNew) {
    debugger;
    FadePageCover(true);
    //if (!isValidDate($("#txtFromDate").val().trim(), 1) || !isValidDate($("#txtToDate").val().trim(), 1)) {
    //    swal(strSorry, "Please, Enter Start-End dates.");
    //    FadePageCover(false);
    //}
    //if ($("#txtFromDate").val().trim() != "" && $("#txtToDate").val().trim() != ""
    //    && Date.prototype.compareDates(ConvertDateFormat($("#txtFromDate").val().trim()), ConvertDateFormat($("#txtToDate").val().trim())) < 0) {
    //    FadePageCover(false);
    //    swal("Sorry", "Please, check dates.");
    //}
    //else
    if (ValidateForm("form", "ReceiveModal")) {
        pParametersWithValues = {
            pID: $("#hID").val() == "" ? 0 : $("#hID").val()
            , pWarehouseID: $("#slWarehouse").val() == "" ? "0" : $("#slWarehouse").val()
            , pCustomerID: $("#slCustomer").val() == "" ? "0" : $("#slCustomer").val()
            , pReceiveDate: ($("#txtReceiveDate").val().trim() == "" ? "01/01/1900" : PadDateWithZeroes($("#txtReceiveDate").val().trim()))
            , pETD: "01/01/1900" //($("#txtETD").val().trim() == "" ? "0" : PadDateWithZeroes($("#txtETD").val().trim()))
            , pETA: "01/01/1900" //($("#txtETA").val().trim() == "" ? "0" : PadDateWithZeroes($("#txtETA").val().trim()))
            , pArrivalDate: "01/01/1900" //($("#txtArrivalDate").val().trim() == "" ? "0" : PadDateWithZeroes($("#txtArrivalDate").val().trim()))
            , pStatusID: $("#hID").val() == "" ? constReceiveStatusInProgress : constReceiveStatusInProgress //case of check for Location in details
            , pIsFinalized: $("#cbIsFinalized").prop("checked")
            , pFinalizeDate: "01/01/1900"
            , pNotes: $("#txtNotes").val().trim() == "" ? "0" : $("#txtNotes").val().trim().toUpperCase()

            //, pExpireDate: ($("#txtExpireDate").val().trim() == "" ? "01/01/1900" : PadDateWithZeroes($("#txtExpireDate").val().trim()))
            //, pLotNo: $("#txtLotNo").val()//case of check for Location in details
        };
        CallGETFunctionWithParameters("/api/Receive/Save", pParametersWithValues
            , function (pData) {
                if (pData[0] == "") {
                    var pReceiveHeader = JSON.parse(pData[2]);
                    //var pReceiveDetails = JSON.parse(pData[3]);
                    Receive_LoadingWithPaging();
                    if (pSaveAndNew) {
                        Receive_ClearAllControls();
                    }
                    else {
                        $("#hID").val(pData[1]);
                        $("#txtCode").val(pReceiveHeader.Code);
                        $("#txtStatus").val(pReceiveHeader.StatusName);
                        //ReceiveDetails_BindTableRows(pReceiveDetails);
                        $("#btnSave").attr("onclick", "Receive_Save(false);");
                        $("#btnSaveAndAddNew").attr("onclick", "Receive_Save(true);");
                    }
                    swal("Success", "Saved successfully.");
                }
                else {
                    swal("Sorry", pData[0]);
                    FadePageCover(false);
                }
            }
            , null);
    }
    else //if (ValidateForm("form", "ReceiveModal"))
        FadePageCover(false);
}
function Receive_DeleteList() {
    debugger;
    //Confirmation message to delete
    if (GetAllSelectedIDsAsString('tblReceive') != "")
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
            DeleteListFunction("/api/Receive/Delete", { "pReceiveIDs": GetAllSelectedIDsAsString('tblReceive') }, function () { Receive_LoadingWithPaging(); });
        });
    //DeleteListFunction("/api/Receive/Delete", { "pReceiveIDs": GetAllSelectedIDsAsString('tblReceive') }, function () { Receive_LoadingWithPaging(); });
}
function Receive_Finalize() {
    debugger;
    if ($("#txtStatus").val().toUpperCase() != "PUTAWAY")
        swal("Sorry", "The status must be PUTAWAY to finalize.");
    else {
        swal({
            title: "Are you sure?",
            text: "This record will be finalized.",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, finalize",
            closeOnConfirm: true
        },
        //callback function in case of confirm
        function () {
            CallGETFunctionWithParameters("/api/Receive/Finalize"
            , { pFinalizedReceiveID: $("#hID").val() }
            , function (pData) {
                if (pData[0] == "") {
                    swal("Success", "Finalized successfully.");
                    $(".classDisableForFinalized").attr("disabled", "disabled");
                    $("#txtStatus").val("FINALIZED");
                    $("#cbIsFinalized").prop("checked", true);
                    $("#tblReceive tr[ID=" + $("#hID").val() + "] td.Status").text("FINALIZED");
                }
                else
                    swal("Sorry", pData[0]);
            }
            , null);
        });
    }
}
function Receive_Print() {
    debugger;
    if ($("#hID").val() == "")
        swal("Sorry", "Please, save receive first.");
    else {
        FadePageCover(true);
        CallGETFunctionWithParameters("/api/Receive/LoadHeaderWithDetails", { pHeaderID: $("#hID").val() }
            , function (pData) {
                var _ReturnedMessage = pData[0];
                //if (pData[0] != "")
                //    swal("Sorry", _ReturnedMessage);
                //else { //Draw
                    var pHeader = JSON.parse(pData[0]);
                    var pDetails = JSON.parse(pData[1]);
                    var mywindow = window.open('', '_blank');
                    var ReportHTML = '';
                    //<div style="width:500px;height:100px;border:1px solid #000;">This is a rectangle!</div>
                    ReportHTML += '<html>';
                    ReportHTML += '     <head><title>' + 'Proof of Delivery' + '</title><link rel="stylesheet" href="/Content/CSS/app.v2.css" type="text/css" /></head>';
                    ReportHTML += '         <body style="background-color:white;">';
                    ReportHTML += '         <div class="col-xs-12 text-center"><img src="/Content/Images/CompanyHeader.jpg" alt="logo"/></div> </br>';
                    ReportHTML += '             <div class="col-xs-12 text-center text-ul m-t-n-lg"><h3>' + 'Receive ' + pHeader.Code + '</h3></div> </br>';
                    ReportHTML += '             <div class="col-xs-4"><b>Receive Date: ' + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(pHeader.ReceiveDate)) > 0 ? ConvertDateFormat(GetDateWithFormatMDY(pHeader.ReceiveDate)) : "") + '</b></div>';
                    ReportHTML += '             <div class="col-xs-4"><b>Receive Code : </b>' + pHeader.Code + '</div>';
                    ReportHTML += '             <div class="col-xs-4"><b>Status : </b>' + pHeader.StatusName + '</div>';
                    if (pDefaults.UnEditableCompanyName == "EXP") {
                        ReportHTML += '             <div class="col-xs-4"><b>Batch : </b>' + (pHeader.BatchNumber == 0 ? "" : pHeader.BatchNumber) + '</div>';
                        ReportHTML += '             <div class="col-xs-4"><b>Expiration : </b>' + pHeader.StatusName + '</div>';
                        ReportHTML += '             <div class="col-xs-4"><b>ImportedBy : </b>' + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(pHeader.ExpirationDate)) < 1 ? "" : ConvertDateFormat(GetDateWithFormatMDY(pHeader.ExpirationDate))) + '</div>';
                        ReportHTML += '             <div class="col-xs-4"><b>Wgt : </b>' + (pHeader.WeightInTons == 0 ? "" : pHeader.WeightInTons) + '</div>';
                    }
                    ReportHTML += '             <div class="col-xs-12 clear"><b>Customer : </b>' + pHeader.CustomerName + '</div>';
                    //ReportHTML += '             <div class="col-xs-12"><b>Bill To : </b>' + pHeader.BillToName + '</div>'; //billto
                    //ReportHTML += '             <div class="col-xs-12"><b>Address : </b>' + (pHeader.BillToAddress == 0 ? "" : pHeader.BillToAddress) + '</div>';
                    //ReportHTML += '             <div class="col-xs-12"><b>Contact Name : </b>' + (pHeader.BillToContactName == 0 ? "" : pHeader.BillToContactName) + '</div>';
                    //ReportHTML += '             <div class="col-xs-12"><b>Contact No. : </b>' + (pHeader.BillToContactPhones == 0 ? "" : pHeader.BillToContactPhones) + '</div>';
                    //ReportHTML += '             <div class="col-xs-12"><b>Delivered to : </b>' + (pHeader.EndUserName == 0 ? "" : pHeader.EndUserName) + '</div>';
                    //ReportHTML += '             <div class="col-xs-12"><b>Address : </b>' + (pHeader.EndUserAddress == 0 ? "" : pHeader.EndUserAddress) + '</div>';
                    //ReportHTML += '             <div class="col-xs-12"><b>Contact Name : </b>' + (pHeader.EndUserContactName == 0 ? "" : pHeader.EndUserContactName) + '</div>';
                    //ReportHTML += '             <div class="col-xs-12"><b>Contact No. : </b>' + (pHeader.EndUserContactPhones == 0 ? "" : pHeader.EndUserContactPhones) + '</div>';
                    //ReportHTML += '             <div class="col-xs-4"><b>Operation : </b>' + (pHeader.OperationCode == 0 ? "" : pHeader.OperationCode) + '</div>';
                    //ReportHTML += '             <div class="col-xs-4"><b>PO Number : </b>' + (pHeader.PONumber == 0 ? "" : pHeader.PONumber) + '</div>';
                    //ReportHTML += '             <div class="col-xs-4"><b>RMA No. : </b>' + (pHeader.RMANumber == 0 ? "" : pHeader.RMANumber) + '</div>';
                    ReportHTML += '                         <table id="tblPrintedReceiveDetails" class="table table-striped b-t b-light text-sm table-bordered">'; //table-hover
                    ReportHTML += '                             <thead>';
                    ReportHTML += '                                 <tr>';
                    ReportHTML += '                                     <th>Prod.Code</th>';
                    ReportHTML += '                                     <th>Prod.Name</th>';
                    ReportHTML += '                                     <th>PartNumber</th>';
                    if (pDefaults.UnEditableCompanyName == "NIL")
                        ReportHTML += '                                     <th>Ser</th>';
                    ReportHTML += '                                     <th>Quantity</th>';
                    ReportHTML += '                                     <th>Location</th>';
                    //ReportHTML += '                                     <th>Date</th>';
                    ReportHTML += '                                     <th>Status</th>';
                    ReportHTML += '                                 </tr>';
                    ReportHTML += '                             </thead>';
                    ReportHTML += '                             <tbody>';
                    debugger;
                    var _TotalQuantity = 0.0;
                    //var _TotalGrossWeight = 0.0;
                    //var _TotalVolume = 0.0;
                    //var _WeightUnitCode = 'KGM';
                    //var _VolumeUnitCode = 'CBM';
                    $.each(pDetails, function (i, item) {
                        _TotalQuantity += item.Quantity;
                        //var _RowQuantity = (item.Serial == 0 ? item.PickedQuantity : 1);
                        //var _RowGrossWeight = _RowQuantity * item.GrossWeight;
                        //var _RowVolume = _RowQuantity * item.Volume;
                        //_WeightUnitCode = item.WeightUnitCode;
                        //_VolumeUnitCode = item.VolumeUnitCode;
                        //_TotalQuantity += _RowQuantity;
                        //_TotalGrossWeight += _RowGrossWeight;
                        //_TotalVolume += _RowVolume;
                        ReportHTML += '                                     <tr class="" style="font-size:95%;">';
                        ReportHTML += '                                         <td>' + (item.PurchaseItemCode == 0 ? '' : item.PurchaseItemCode) + '</td>';
                        ReportHTML += '                                         <td>' + (item.PurchaseItemName == 0 ? '' : item.PurchaseItemName) + '</td>';
                        ReportHTML += '                                         <td>' + (item.PartNumber == 0 ? '' : item.PartNumber) + '</td>';
                        if (pDefaults.UnEditableCompanyName == "NIL")
                            ReportHTML += '                                         <td>' + (item.LotNo == 0 ? '' : item.LotNo) + '</td>';
                        ReportHTML += '                                         <td class="Quantity">' + item.Quantity + '</td>';
                        //ReportHTML += '                                         <td class="Quantity">' + (item.Serial == 0 ? item.PickedQuantity : '1') + '</td>';
                        ReportHTML += '                                         <td>' + (item.LocationCode == 0 ? '' : item.LocationCode) + '</td>';
                        //ReportHTML += '                                         <td>' + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.ReceiveDate)) < 1 ? "" : ConvertDateFormat(GetDateWithFormatMDY(item.ReceiveDate))) + '</td>';
                        ReportHTML += '                                         <td>' + (item.StatusName == 0 ? '' : item.StatusName) + '</td>';
                        //ReportHTML += '                                         <td>' + (item.Serial == 0 ? '' : item.Serial) + '</td>';
                        //ReportHTML += '                                         <td class="hide GrossWeight">' + _RowGrossWeight + '</td>';
                        //ReportHTML += '                                         <td class="hide Volume">' + _RowVolume + '</td>';
                        ReportHTML += '                                     </tr>';
                    });
                    /*********************************Summary*****************************************/
                    ReportHTML += '                                     <tr class="" style="font-size:95%;">';
                    ReportHTML += '                                         <td colspan=6>' + 'Total No of Items is ' + _TotalQuantity.toFixed(2) + '</td>';
                    ReportHTML += '                                     </tr>';
                    /*********************************EOF Summary*****************************************/
                    ReportHTML += '                             </tbody>';
                    ReportHTML += '                         </table>';

                    ReportHTML += '                         <div class="col-xs-7"><b>Received By : &nbsp;&nbsp;</b>' + '  _______________________________________	 ' + '</div>';
                    ReportHTML += '                         <div class="col-xs-5"><b>Signature : </b>' + '  _______________________	 ' + '</div>';
                    //ReportHTML += '                         <div class="col-xs-7"><b>ID No. : &emsp;&emsp;&emsp;&nbsp;&nbsp;</b>' + '  _______________________________________	 ' + '</div>';
                    ReportHTML += '                         <div class="col-xs-7"><b>Mobile : &emsp;&emsp;&emsp;&nbsp;</b>' + '  _______________________________________	 ' + '</div>';
                    ReportHTML += '                         <div class="col-xs-5"><b>Date : &emsp;&emsp;&emsp;&emsp;</b>' + getTodaysDateInddMMyyyyFormat() + '</div>';

                    //ReportHTML += '                         <div class="col-xs-12 text-center m-t"><b>' + '  The equipment was received in good condition and there were no scratches.   ' + '</b></div>';
                    //ReportHTML += '                         <div class="col-xs-12 text-center"><b>' + '  تم إستلام الأجهزة فى حالة سليمة ولا يوجد أى خدوش   ' + '</b></div>';
                    //ReportHTML += '                         <div class="col-xs-12 text-center"><b>' + '  رجاء الختم بخاتم الشركه بما يفيد الاستلام  ' + '</b></div>';
                    //ReportHTML += '                         <div class="col-xs-12 text-center"><b>' + '  Please seal the company with the receipt.	 ' + '</b></div>';
                    ReportHTML += '         </body>';
                    ReportHTML += '     <footer class="footer col-xs-12" style="width:100%; position:absolute; bottom:0;">';
                    //ReportHTML += '         <div class="row text-center"><img src="/Content/Images/CompanyFooter.jpg" alt="footer"/></div>';
                    ReportHTML += '     </footer>';
                    ReportHTML += '</html>';
                    mywindow.document.write(ReportHTML);
                    mywindow.document.close();
                //} //else { //Draw
                FadePageCover(false);
            } //function(pData)
            , null);
    } //EOF else
}
function Receive_PrintSummary() {
    debugger;
    if ($("#hID").val() == "")
        swal("Sorry", "Please, save receive first.");
    else {
        FadePageCover(true);
        CallGETFunctionWithParameters("/api/Receive/LoadHeaderWithDetails", { pHeaderID: $("#hID").val() }
            , function (pData) {
                var _ReturnedMessage = pData[0];
                //if (pData[0] != "")
                //    swal("Sorry", _ReturnedMessage);
                //else { //Draw
                var pHeader = JSON.parse(pData[0]);
                var pDetails = JSON.parse(pData[1]);
                var mywindow = window.open('', '_blank');
                var ReportHTML = '';
                //<div style="width:500px;height:100px;border:1px solid #000;">This is a rectangle!</div>
                ReportHTML += '<html>';
                ReportHTML += '     <head><title>' + 'Summary' + '</title><link rel="stylesheet" href="/Content/CSS/app.v2.css" type="text/css" /></head>';
                ReportHTML += '         <body style="background-color:white;">';
                ReportHTML += '         <div class="col-xs-12 text-center"><img src="/Content/Images/CompanyHeader.jpg" alt="logo"/></div> </br>';
                ReportHTML += '             <div class="col-xs-12 text-center text-ul m-t-n-lg"><h3>' + 'Summary ' + pHeader.Code + '</h3></div> </br>';
                ReportHTML += '             <div class="col-xs-4"><b>Receive Date: ' + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(pHeader.ReceiveDate)) > 0 ? ConvertDateFormat(GetDateWithFormatMDY(pHeader.ReceiveDate)) : "") + '</b></div>';
                ReportHTML += '             <div class="col-xs-4"><b>Receive Code : </b>' + pHeader.Code + '</div>';
                //ReportHTML += '             <div class="col-xs-4"><b>Status : </b>' + pHeader.StatusName + '</div>';
                ReportHTML += '             <div class="col-xs-12"><b>Customer : </b>' + pHeader.CustomerName + '</div>';
                //ReportHTML += '             <div class="col-xs-12"><b>Bill To : </b>' + pHeader.BillToName + '</div>'; //billto
                //ReportHTML += '             <div class="col-xs-12"><b>Address : </b>' + (pHeader.BillToAddress == 0 ? "" : pHeader.BillToAddress) + '</div>';
                //ReportHTML += '             <div class="col-xs-12"><b>Contact Name : </b>' + (pHeader.BillToContactName == 0 ? "" : pHeader.BillToContactName) + '</div>';
                //ReportHTML += '             <div class="col-xs-12"><b>Contact No. : </b>' + (pHeader.BillToContactPhones == 0 ? "" : pHeader.BillToContactPhones) + '</div>';
                //ReportHTML += '             <div class="col-xs-12"><b>Delivered to : </b>' + (pHeader.EndUserName == 0 ? "" : pHeader.EndUserName) + '</div>';
                //ReportHTML += '             <div class="col-xs-12"><b>Address : </b>' + (pHeader.EndUserAddress == 0 ? "" : pHeader.EndUserAddress) + '</div>';
                //ReportHTML += '             <div class="col-xs-12"><b>Contact Name : </b>' + (pHeader.EndUserContactName == 0 ? "" : pHeader.EndUserContactName) + '</div>';
                //ReportHTML += '             <div class="col-xs-12"><b>Contact No. : </b>' + (pHeader.EndUserContactPhones == 0 ? "" : pHeader.EndUserContactPhones) + '</div>';
                //ReportHTML += '             <div class="col-xs-4"><b>Operation : </b>' + (pHeader.OperationCode == 0 ? "" : pHeader.OperationCode) + '</div>';
                //ReportHTML += '             <div class="col-xs-4"><b>PO Number : </b>' + (pHeader.PONumber == 0 ? "" : pHeader.PONumber) + '</div>';
                //ReportHTML += '             <div class="col-xs-4"><b>RMA No. : </b>' + (pHeader.RMANumber == 0 ? "" : pHeader.RMANumber) + '</div>';
                ReportHTML += '                         <table id="tblPrintedReceiveDetails" class="table table-striped b-t b-light text-sm table-bordered">'; //table-hover
                ReportHTML += '                             <thead>';
                ReportHTML += '                                 <tr>';
                ReportHTML += '                                     <th>Item</th>';
                ReportHTML += '                                     <th>Description</th>';
                ReportHTML += '                                     <th>Quantity</th>';
                ReportHTML += '                                     <th>Location</th>';
                ReportHTML += '                                     <th>PalletID</th>';
                if (pDefaults.UnEditableCompanyName == "NIL")
                    ReportHTML += '                                     <th>Serial</th>';
                ReportHTML += '                                 </tr>';
                ReportHTML += '                             </thead>';
                ReportHTML += '                             <tbody>';
                debugger;
                var _TotalQuantity = 0.0;
                //var _TotalGrossWeight = 0.0;
                //var _TotalVolume = 0.0;
                //var _WeightUnitCode = 'KGM';
                //var _VolumeUnitCode = 'CBM';
                $.each(pDetails, function (i, item) {
                    _TotalQuantity += item.Quantity;
                    //var _RowQuantity = (item.Serial == 0 ? item.PickedQuantity : 1);
                    //var _RowGrossWeight = _RowQuantity * item.GrossWeight;
                    //var _RowVolume = _RowQuantity * item.Volume;
                    //_WeightUnitCode = item.WeightUnitCode;
                    //_VolumeUnitCode = item.VolumeUnitCode;
                    //_TotalQuantity += _RowQuantity;
                    //_TotalGrossWeight += _RowGrossWeight;
                    //_TotalVolume += _RowVolume;
                    ReportHTML += '                                     <tr class="" style="font-size:95%;">';
                    ReportHTML += '                                         <td>' + (item.PurchaseItemCode == 0 ? '' : item.PurchaseItemCode) + '</td>';
                    ReportHTML += '                                         <td>' + (item.PurchaseItemName == 0 ? '' : item.PurchaseItemName) + '</td>';
                    ReportHTML += '                                         <td class="Quantity">' + item.Quantity + '</td>';
                    ReportHTML += '                                         <td>' + (item.LocationCode == 0 ? '' : item.LocationCode) + '</td>';
                    ReportHTML += '                                         <td>' + (item.PalletID == 0 ? '' : item.PalletID) + '</td>';
                    if (pDefaults.UnEditableCompanyName == "NIL")
                        ReportHTML += '                                         <td>' + (item.LotNo == 0 ? '' : item.LotNo) + '</td>'; //Serial
                    ReportHTML += '                                     </tr>';
                });
                /*********************************Summary*****************************************/
                ReportHTML += '                                     <tr class="" style="font-size:95%;">';
                ReportHTML += '                                         <td colspan=6>' + 'Total No of Items is ' + _TotalQuantity.toFixed(2) + '</td>';
                ReportHTML += '                                     </tr>';
                /*********************************EOF Summary*****************************************/
                ReportHTML += '                             </tbody>';
                ReportHTML += '                         </table>';

                //ReportHTML += '                         <div class="col-xs-7"><b>Received By : &nbsp;&nbsp;</b>' + '  _______________________________________	 ' + '</div>';
                //ReportHTML += '                         <div class="col-xs-5"><b>Signature : </b>' + '  _______________________	 ' + '</div>';
                //ReportHTML += '                         <div class="col-xs-7"><b>Mobile : &emsp;&emsp;&emsp;&nbsp;</b>' + '  _______________________________________	 ' + '</div>';
                //ReportHTML += '                         <div class="col-xs-5"><b>Date : &emsp;&emsp;&emsp;&emsp;</b>' + getTodaysDateInddMMyyyyFormat() + '</div>';

                //ReportHTML += '                         <div class="col-xs-12 text-center m-t"><b>' + '  The equipment was received in good condition and there were no scratches.   ' + '</b></div>';
                //ReportHTML += '                         <div class="col-xs-12 text-center"><b>' + '  تم إستلام الأجهزة فى حالة سليمة ولا يوجد أى خدوش   ' + '</b></div>';
                //ReportHTML += '                         <div class="col-xs-12 text-center"><b>' + '  رجاء الختم بخاتم الشركه بما يفيد الاستلام  ' + '</b></div>';
                //ReportHTML += '                         <div class="col-xs-12 text-center"><b>' + '  Please seal the company with the receipt.	 ' + '</b></div>';
                ReportHTML += '         </body>';
                ReportHTML += '     <footer class="footer col-xs-12" style="width:100%; position:absolute; bottom:0;">';
                //ReportHTML += '         <div class="row text-center"><img src="/Content/Images/CompanyFooter.jpg" alt="footer"/></div>';
                ReportHTML += '     </footer>';
                ReportHTML += '</html>';
                mywindow.document.write(ReportHTML);
                mywindow.document.close();
                //} //else { //Draw
                FadePageCover(false);
            } //function(pData)
            , null);
    } //EOF else
}
/***************************************ReceiveDetails***************************************/
function ReceiveDetails_BindTableRows(pReceiveDetails) {
    debugger;
    if (pReceiveDetails.length == 0)
        $("#slWarehouse").removeAttr("disabled");
    else
        $("#slWarehouse").attr("disabled", "disabled");
    ClearAllTableRows("tblReceiveDetails");
    var serialControlsText = " class='btn btn-xs btn-rounded btn-info float-right' > <i class='fa fa-list-ol' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + TranslateString("Serial") + "</span>";
    var inspectionControlsText = " class='btn btn-xs btn-rounded btn-lightblue float-right hide' > <i class='fa fa-list' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + " Actions" + "</span>";
    $.each(pReceiveDetails, function (i, item) {
        AppendRowtoTable("tblReceiveDetails",
        ("<tr ID='" + item.ID + "' " + (1 == 1 ? ("ondblclick='ReceiveDetails_FillControls(" + item.ID + ");'") : "") + ">"
            + "<td class='ID'> <input " + (1 == 1 ? "name='Delete'" : "disabled='disabled'") + " type='checkbox' value='" + item.ID + "'></td>"
            + "<td class='ReceiveID hide'>" + item.ReceiveID + "</td>"
            + "<td class='BarCode'>" + (item.BarCode == 0 ? "" : item.BarCode) + "</td>"
            + "<td class='PurchaseItemID hide'>" + (item.PurchaseItemID == 0 ? "" : item.PurchaseItemID) + "</td>"
            + "<td class='PurchaseItemCode'>" + (item.PurchaseItemCode == 0 ? "" : item.PurchaseItemCode) + "</td>"
            + "<td class='PurchaseItemName'>" + (item.PurchaseItemName == 0 ? "" : item.PurchaseItemName) + "</td>"
            + "<td class='PurchaseItemPartNumber'>" + (item.PartNumber == 0 ? "" : item.PartNumber) + "</td>"
            + (pDefaults.UnEditableCompanyName == "GBL"
                ? (
                    "<td class='EngineNumber hide'>" + (item.EngineNumber == 0 ? "" : item.EngineNumber) + "</td>"
                    + "<td class='ChassisNumber hide'>" + (item.ChassisNumber == 0 ? "" : item.ChassisNumber) + "</td>"
                    + "<td class='OCNCode hide'>" + (item.OCNCode == 0 ? "" : item.OCNCode) + "</td>"
                    + "<td class='Model hide'>" + (item.Model == 0 ? "" : item.Model) + "</td>"
                    )
                : ""
               )
            + "<td class='Quantity'>" + item.Quantity + "</td>"
            + "<td class='PackageTypeID hide'>" + (item.PackageTypeID == 0 ? "" : item.PackageTypeID) + "</td>"
            + "<td class='PackageTypeName'>" + (item.PackageTypeName == 0 ? "" : item.PackageTypeName) + "</td>"
            + "<td class='AreaID hide'>" + (item.AreaID == 0 ? "" : item.AreaID) + "</td>"
            + "<td class='LocationID hide'>" + (item.LocationID == 0 ? "" : item.LocationID) + "</td>"
            + "<td class='LocationCode'>" + (item.LocationCode == 0 ? "" : item.LocationCode) + "</td>"
            + "<td class='PalletID'>" + (item.PalletID == 0 ? "" : item.PalletID) + "</td>"
            + "<td class='LotNo'>" + item.LotNo + "</td>"

            + "<td class='ReceiveDate'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.ReceiveDate)) < 1 ? "" : ConvertDateFormat(GetDateWithFormatMDY(item.ReceiveDate))) + "</td>"
            + "<td class='StatusID hide'>" + item.StatusID + "</td>"
            + "<td class='Status'>" + item.StatusName + "</td>"
            + "<td class='Notes'>" + (item.Notes == 0 ? "" : item.Notes) + "</td>"

            + "<td class='ExpireDate hide'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.ExpireDate)) < 1 ? "" : ConvertDateFormat(GetDateWithFormatMDY(item.ExpireDate))) + "</td>"
            + "<td class='ByExpireDate hide'>" + item.ByExpireDate + "</td>"
            + "<td class='BySerialNo hide'>" + item.BySerialNo + "</td>"
            + "<td class='ByLotNo hide'>" + item.ByLotNo + "</td>"
            + "<td class='ByVehicle hide'>" + item.ByVehicle + "</td>"
            + "<td class='OperationVehicleID hide'>" + item.OperationVehicleID + "</td>"

            + "<td class='BatchNumber hide'>" + (item.BatchNumber == 0 ? "" : item.BatchNumber) + "</td>"
            + "<td class='ExpirationDate hide'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.ExpirationDate)) < 1 ? "" : ConvertDateFormat(GetDateWithFormatMDY(item.ExpirationDate))) + "</td>"
            + "<td class='ImportedBy hide'>" + (item.ImportedBy == 0 ? "" : item.ImportedBy) + "</td>"
            + "<td class='WeightInTons hide'>" + (item.WeightInTons == 0 ? "" : item.WeightInTons) + "</td>"

            + "<td class=''>"
                + "<a href='#' data-toggle='modal' onclick='VehicleAction_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned(" + item.OperationVehicleID + ");' " + inspectionControlsText + "</a>"
                + "<a href='#ReceiveDetailsSerialModal' data-toggle='modal' onclick='ReceiveDetailsSerial_FillModal(" + item.ID + ");' " + serialControlsText + "</a>"
            + "</td>"
        + "</tr>"));
    });
    ////ApplyPermissions();
    //if (ODPay && $("#hIsOperationDisabled").val() == false) $("#btn-DeletePayable").removeClass("hide"); else $("#btn-DeletePayable").addClass("hide");
    BindAllCheckboxonTable("tblReceiveDetails", "ID", "cb-CheckAll-ReceiveDetails");
    CheckAllCheckbox("HeaderDeleteReceiveDetailsID");
    if (showDeleteFailMessage) { //sherif: if deletion failed due to foreign key, then this message is shown
        swal(strSorry, strDeleteFailMessage, "warning");
        showDeleteFailMessage = false;
    }
}
function ReceiveDetails_ClearAllControls() {
    debugger;
    if ($("#hID").val() == "")
        swal("Sorry", "Please, save header first.");
    else {
        FadePageCover(true);
        ClearAll("#ReceiveDetailsModal");
        $("#lblReceiveDetailsShown").html($("#lblShown").html());
        var pParametersWithValues = {
            pRowLocationWhereClauseWithMinimalColumns: "WHERE 1=2"//"WHERE WarehouseID=" + $("#slWarehouse").val() + " AND IsUsed=0"
            , pOrderBy: "Code"
            , pWarehouseID: $("#slWarehouse").val() == "" ? 0 : $("#slWarehouse").val()
        };
        CallGETFunctionWithParameters("/api/Row/RowLocation_LoadAllWithMinimalColumns", pParametersWithValues
            , function (pData) {
                var pLocation = pData[0];
                var pArea = pData[1];
                FillListFromObject(null, 2, "<--Select-->", "slReceiveDetailsArea", pArea, null);
                //FillListFromObject(null, 1, "<--Select-->", "slReceiveDetailsLocation", pLocation, null); //$("#slReceiveDetailsLocation").html("<option value=''><--Select--></option>");
                $("#slReceiveDetailsLocation").html("<option value=''><--Select--></option>");
                $("#txtReceiveDetailsReceiveDate").val($("#txtReceiveDate").val());
                $("#btnSaveReceiveDetails").attr("onclick", "ReceiveDetails_Save(false);");
                $("#btnSaveAndAddNewReceiveDetails").attr("onclick", "ReceiveDetails_Save(true);");
                ApplySelectListSearch_OnlyChange();
                jQuery("#ReceiveDetailsModal").modal("show");

                FadePageCover(false);
            }
            , null);
    }
}
function ReceiveDetails_FillControls(pID) {
    debugger;
    if ($("#slReceiveDetailsPurchaseItem").val() != null) { //to ensure it is filled because its loaded separately for performance
        FadePageCover(true);
        ClearAll("#ReceiveDetailsModal");
        $("#lblReceiveDetailsShown").html($("#lblShown").html());

        $("#hReceiveDetailsID").val(pID);
        var tr = $("#tblReceiveDetails tr[ID='" + pID + "']");
        $("#txtReceiveDetailsBarCode").val($(tr).find("td.BarCode").text());
        $("#slReceiveDetailsPurchaseItem").val($(tr).find("td.PurchaseItemID").text());
        $("#txtReceiveDetailsQuantity").val($(tr).find("td.Quantity").text());
        $("#txtReceiveDetailsPalletID").val($(tr).find("td.PalletID").text());
        $("#txtReceiveDetailsNotes").val($(tr).find("td.Notes").text());
        $("#txtReceiveDetailsReceiveDate").val($(tr).find("td.ReceiveDate").text());
        $("#slReceiveDetailsStatus").val($(tr).find("td.StatusID").text());

        $("#txtBatchNumber").val($(tr).find("td.BatchNumber").text() == 0 ? "" : $(tr).find("td.BatchNumber").text());
        $("#txtExpirationDate").val($(tr).find("td.ExpirationDate").text() == 0 ? "" : $(tr).find("td.ExpirationDate").text());
        $("#txtImportedBy").val($(tr).find("td.ImportedBy").text() == 0 ? "" : $(tr).find("td.ImportedBy").text());
        $("#txtWeightInTons").val($(tr).find("td.WeightInTons").text() == 0 ? "" : $(tr).find("td.WeightInTons").text());

        $("#txtLotNo").val($(tr).find("td.LotNo").text() == 0 ? "" : $(tr).find("td.LotNo").text());
        $("#txtExpireDate").val($(tr).find("td.ExpireDate").text());
        //$(tr).find("td.ByLotNo").text() == true.toString() ? $("#txtLotNo").removeAttr("disabled") : $("#txtLotNo").attr("disabled", "disabled");
        $(tr).find("td.ByExpireDate").text() == true.toString() ? $("#txtExpireDate").removeAttr("disabled") : $("#txtExpireDate").attr("disabled", "disabled");

        var _LocationID = ($(tr).find("td.LocationID").text());
        var _AreaID = $(tr).find("td.AreaID").text()
        $("#hOldLocationID").val(_LocationID);

        var pParametersWithValues = {
            pRowLocationWhereClauseWithMinimalColumns: "WHERE AreaID=" + (_AreaID == "" ? 0 : _AreaID) + " AND WarehouseID=" + $("#slWarehouse").val() + (pDefaults.UnEditableCompanyName == "NIL" || pDefaults.UnEditableCompanyName == "EXP" || pDefaults.UnEditableCompanyName == "KADI" || pDefaults.UnEditableCompanyName == "GBL" || pDefaults.UnEditableCompanyName == "IST" ? "" : (" AND (IsUsed=0 OR ID=" + (_LocationID == "" ? 0 : _LocationID) + ")"))
            , pOrderBy: "Code"
            , pWarehouseID: $("#slWarehouse").val() == "" ? 0 : $("#slWarehouse").val()
        };
        CallGETFunctionWithParameters("/api/Row/RowLocation_LoadAllWithMinimalColumns", pParametersWithValues
            , function (pData) {
                var pLocation = pData[0];
                var pArea = pData[1];
                FillListFromObject(_LocationID, 1, "<--Select-->", "slReceiveDetailsLocation", pLocation, function () {
                    ApplySelectListSearch_OnlyChange();
                });
                FillListFromObject(_AreaID, 2, "<--Select-->", "slReceiveDetailsArea", pArea, function () {
                    ApplySelectListSearch_OnlyChange();
                });
                FadePageCover(false);
            }
            , null);
        ApplySelectListSearch_OnlyChange();
        jQuery("#ReceiveDetailsModal").modal("show");
    }
}
function ReceiveDetails_Save(pSaveAndNew) {
    debugger;
    FadePageCover(true);
    if (parseFloat($("#txtReceiveDetailsQuantity").val()) == 0) {
        swal("Sorry", "Quantity can not be 0.");
        FadePageCover(false);
    }
    else if (ValidateForm("form", "ReceiveDetailsModal")) {
        var pParametersWithValues = {
            pReceiveDetailsID: $("#hReceiveDetailsID").val() == "" ? 0 : $("#hReceiveDetailsID").val()
            , pReceiveID: $("#hID").val()
            , pBarCode: $("#txtReceiveDetailsBarCode").val().trim() == "" ? 0 : $("#txtReceiveDetailsBarCode").val().trim().toUpperCase()
            , pPurchaseItemID: $("#slReceiveDetailsPurchaseItem").val() == "" ? 0 : $("#slReceiveDetailsPurchaseItem").val()
            , pQuantity: $("#txtReceiveDetailsQuantity").val() == "" ? 0 : $("#txtReceiveDetailsQuantity").val()
            , pExpectedQuantity: $("#txtReceiveDetailsQuantity").val() == "" ? 0 : $("#txtReceiveDetailsQuantity").val()
            , pSplitQuantity: "0"
            , pLocationID: $("#slReceiveDetailsLocation").val() == "" ? 0 : $("#slReceiveDetailsLocation").val()
            , pPalletID: $("#txtReceiveDetailsPalletID").val().trim() == "" ? 0 : $("#txtReceiveDetailsPalletID").val().trim().toUpperCase()
            , pNotes: $("#txtReceiveDetailsNotes").val().trim() == "" ? 0 : $("#txtReceiveDetailsNotes").val().trim().toUpperCase()

            , pBatchNumber: $("#txtBatchNumber").val().trim() == "" ? "0" : $("#txtBatchNumber").val().trim().toUpperCase()
            , pExpirationDate: ($("#txtExpirationDate").val().trim() == "" ? "01/01/1900" : PadDateWithZeroes($("#txtExpirationDate").val().trim()))
            , pImportedBy: $("#txtImportedBy").val().trim() == "" ? "0" : $("#txtImportedBy").val().trim().toUpperCase()
            , pWeightInTons: $("#txtWeightInTons").val().trim() == "" ? "0" : $("#txtWeightInTons").val().trim().toUpperCase()

            , pReceiveDate: ($("#txtReceiveDetailsReceiveDate").val().trim() == "" ? "01/01/1900" : PadDateWithZeroes($("#txtReceiveDetailsReceiveDate").val().trim()))
            , pStatusID: $("#slReceiveDetailsLocation").val() == "" ? constReceiveDetailsStatusPending : constReceiveDetailsStatusPutaway //$("#slReceiveDetailsStatus").val() == "" ? 0 : $("#slReceiveDetailsStatus").val()
            , pLotNo: $("#txtLotNo").val().trim() == "" ? 0 : $("#txtLotNo").val().trim().toUpperCase()
            , pExpireDate: ($("#txtExpireDate").val().trim() == "" ? ConvertDateFormat("01/01/1900") : ConvertDateFormat($("#txtExpireDate").val().trim()))
            , pOldLocationID: $("#hOldLocationID").val() == "" ? 0 : $("#hOldLocationID").val()
        };
        CallGETFunctionWithParameters("/api/Receive/ReceiveDetails_Save", pParametersWithValues
            , function (pData) {
                if (pData[0]) {
                    var pReceiveHeader = JSON.parse(pData[2]);
                    $("#txtStatus").val(pReceiveHeader.StatusName);
                    $("#tblReceive tr[ID=" + pReceiveHeader.ID + "] td.Status").text(pReceiveHeader.StatusName);
                    ReceiveDetails_BindTableRows(JSON.parse(pData[1]));
                    $("#slWarehouse").val(pReceiveHeader.WarehouseID);
                    if (pSaveAndNew) {
                        //$("#slReceiveDetailsLocation").html("<option value=''><--Select--></option>");
                        if ($("#slReceiveDetailsLocation").val() != "")
                            $("#slReceiveDetailsLocation option:selected").remove();
                        ClearAll("#ReceiveDetailsModal");
                        $("#txtReceiveDetailsReceiveDate").val($("#txtReceiveDate").val());
                    }
                    else
                        jQuery("#ReceiveDetailsModal").modal("hide");
                    swal("Success", "Saved successfully.");
                }
                else
                    swal("Sorry", "Saving failed, please try again.");
                FadePageCover(false);
            }
            , null);
    }
    else
        FadePageCover(false);
}
function ReceiveDetails_AddVehicle() {
    debugger;
    if ($("#hID").val() == "")
        swal("Sorry", "Please, save header first.");
    else {
        var pSearchKey = $("#txtVehicleTrackingDetailsChassisNumber_Search").val().trim().toUpperCase();
        if (pSearchKey != "") {
            FadePageCover(true);
            //_ReturnedMessage = CheckExistenceInTableTd("tblVehicleTrackingDetails", "ChassisNumber", pSearchKey);
            var pParametersWithValues = {
                pReceiveID: $("#hID").val() == 0 ? "" : $("#hID").val()
                , pWarehouseID: $("#slWarehouse").val() == 0 ? "" : $("#slWarehouse").val()
                , pChassisNumber: pSearchKey
            };
            CallGETFunctionWithParameters("/api/Receive/ReceiveDetails_AddVehicle"
                , pParametersWithValues
                , function (pData) {
                    var _ReturnedMessage = pData[0];
                    var _ReceiveDetails = JSON.parse(pData[1]); //get the first row
                    if (_ReturnedMessage == "") {
                        ReceiveDetails_BindTableRows(_ReceiveDetails);
                        $("#txtVehicleTrackingDetailsChassisNumber_Search").val("");
                        $("#txtVehicleTrackingDetailsChassisNumber_Search").focus();
                    }
                    else
                        swal("Sorry", _ReturnedMessage);
                    FadePageCover(false);
                }
                , null);
        }
    }
}
function ReceiveDetails_DeleteList() {
    debugger;
    //Confirmation message to delete
    var pReceiveDetailsIDsToDelete = GetAllSelectedIDsAsString('tblReceiveDetails');
    if (pReceiveDetailsIDsToDelete != "")
        swal({
            title: "Are you sure?",
            text: "The selected records will be deleted permanently!",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, delete!",
            closeOnConfirm: true
        },
        //callback function in case of confirm
        function () {
            FadePageCover(true);
            CallPOSTFunctionWithParameters("/api/Receive/ReceiveDetails_Delete"
                , { pReceiveDetailsIDsToDelete: pReceiveDetailsIDsToDelete, pReceiveID: $("#hID").val() }
                , function (pData) {
                    if (pData[0]) {
                        var pReceiveHeader = JSON.parse(pData[2]);
                        $("#txtStatus").val(pReceiveHeader.StatusName);
                        $("#tblReceive tr[ID=" + pReceiveHeader.ID + "] td.Status").text(pReceiveHeader.StatusName);
                        ReceiveDetails_BindTableRows(JSON.parse(pData[1]));
                    }
                    else
                        swal("Sorry", strDeleteFailMessage);
                    FadePageCover(false);
                });
        });
}
function ReceiveDetails_AreaChanged() {
    debugger;
    $("#slReceiveDetailsLocation").html("<option value=''><--Select--></option>");
    if ($("#slReceiveDetailsArea").val() != "") {
        FadePageCover(true);
        var pParametersWithValues = {
            pRowLocationWhereClauseWithMinimalColumns: "WHERE AreaID=" + $("#slReceiveDetailsArea").val() + " AND WarehouseID=" + $("#slWarehouse").val() + (pDefaults.UnEditableCompanyName == "NIL" || pDefaults.UnEditableCompanyName == "EXP" || pDefaults.UnEditableCompanyName == "KADI" || pDefaults.UnEditableCompanyName == "GBL" || pDefaults.UnEditableCompanyName == "IST" ? "" : (" AND (IsUsed=0 OR ID=" + ($("#hOldLocationID").val() == "" ? 0 : $("#hOldLocationID").val()) + ")"))
            , pOrderBy: "Code"
            , pWarehouseID: $("#slWarehouse").val() == "" ? 0 : $("#slWarehouse").val()
        };
        CallGETFunctionWithParameters("/api/Row/RowLocation_LoadAllWithMinimalColumns", pParametersWithValues
            , function (pData) {
                var pLocation = pData[0];
                FillListFromObject(null, 1, "<--Select-->", "slReceiveDetailsLocation", pLocation, null);
                FadePageCover(false);
            }
            , null);
    }
}
function ReceiveDetails_Export(pCalledFrom) {
    debugger;
    if (pCalledFrom == "FromFilter" && ($("#slFilterCustomer").val() == "" || $("#slFilterCustomer").val() == 0))
        swal("Sorry", "Please, select customer.");
    else if (pCalledFrom == "FromEditModal" && $("#hID").val() == "")
        swal("Sorry", "No records to export.");
    else {
        FadePageCover(true);
        var pWhereClauseExportReceiveDetails = "";
        if (pCalledFrom == "FromFilter")
            pWhereClauseExportReceiveDetails = ReceiveDetails_Export_GetWhereClause();
        else
            pWhereClauseExportReceiveDetails = "WHERE ReceiveID=" + $("#hID").val();
        var pParametersWithValues = {
            pPageNumber: 1
            , pPageSize: 999999
            , pWhereClauseExportReceiveDetails: pWhereClauseExportReceiveDetails
            , pOrderBy: "ID"
        };
        CallGETFunctionWithParameters("/api/Receive/ReceiveDetails_Export"
            , pParametersWithValues
            , function (pData) {
                var _ExportedRows = JSON.parse(pData[0]);
                //ExportToExcel(pArray, pHeader, pFileName, pExcludedColumns);
                ExportToExcel(_ExportedRows, "ReceiveCode,ProductCode,ProductName,PartNumber,Quantity,AreaName,LocationCode,PalletID,Status,ReceiveDate,Notes", "New File", null);
                FadePageCover(false);
            }
            , null);
    }
}
function ReceiveDetails_Export_GetWhereClause() {
    debugger;
    var pWhereClause = "WHERE 1=1" + "\n";
    if ($("#slFilterWarehouse").val() != "" && $("#slFilterWarehouse").val() != 0)
        pWhereClause += "AND WarehouseID =" + $("#slFilterWarehouse").val() + "\n";
    if ($("#slFilterCustomer").val() != "" && $("#slFilterCustomer").val() != 0)
        pWhereClause += "AND CustomerID =" + $("#slFilterCustomer").val() + "\n";
    if ($("#txtFilterCode").val().trim() != "")
        pWhereClause += "AND ReceiveCodeSerial=N'" + $("#txtFilterCode").val().trim() + "'" + "\n";
    return pWhereClause;
}
//*********************************Reading Excel Files***************************************//
function ReceiveDetails_Import() {
    debugger;
    if ($("#hID").val() == "")
        swal("Sorry", "Please, save basic data first.");
    else
        $("#btnAddFromExcel").click();
}
function onFileSelected(event) { //Must be saved as Excel 97-2003
    debugger;
    var selectedFile = event.target.files[0];
    if (selectedFile != undefined) { //to handle the case of not choosing a file
        var sFilename = selectedFile.name;
        // Create A File Reader HTML5
        var reader = new FileReader();
        var result = document.getElementById("hExtractedData");
        reader.onload = function (e) {
            var data = e.target.result;
            var cfb = XLS.CFB.read(data, { type: 'binary' });
            var wb = XLS.parse_xlscfb(cfb);
            // Loop Over Each Sheet
            wb.SheetNames.forEach(function (sheetName) {
                //// Obtain The Current Row As CSV
                //var sCSV = XLS.utils.make_csv(wb.Sheets[sheetName]);
                var oJS = XLS.utils.sheet_to_row_object_array(wb.Sheets[sheetName]);
                //$("#hExtractedData").val(sCSV);
                if (oJS.length > 0 && oJS[0].ProductCode != undefined && oJS[0].ReceiveDate != undefined) //if (sCSV != "")
                    ImportFromExcelFile(oJS);
                else
                    swal("Sorry", "Please, revise data and version of the file.");
            });
        };
        // Tell JS To Start Reading The File.. You could delay this if desired
        reader.readAsBinaryString(selectedFile);
    }
}
function ImportFromExcelFile(pDataRows) {
    debugger;
    FadePageCover(true);
    let pBarCodeList = "";
    let pPurchaseItemCodeList = "";
    let pQuantityList = "";
    let pPalletIDList = "";
    let pReceiveDateList = "";
    let pLocationCodeList = "";
    let pNotesList = "";
    let pSerialList = "";

    let pBatchNumberList = "";
    let pExpirationDateList = "";
    let pImportedByList = "";
    let pWeightInTonsList = "";
    
    for (var i = 0; i < pDataRows.length; i++) { //replace '\', ' ', ',' with space
        pBarCodeList += (pBarCodeList == "" ? (pDataRows[i].BarCode == undefined || pDataRows[i].BarCode.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].BarCode.replace(/[\, ]/g, ' ').toUpperCase().trim()) : ("," + (pDataRows[i].BarCode == undefined || pDataRows[i].BarCode.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].BarCode.replace(/[\, ]/g, ' ').toUpperCase().trim())));
        pPurchaseItemCodeList += (pPurchaseItemCodeList == "" ? (pDataRows[i].ProductCode == undefined || pDataRows[i].ProductCode.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].ProductCode.replace(/[\, ]/g, ' ').toUpperCase().trim()) : ("," + (pDataRows[i].ProductCode == undefined || pDataRows[i].ProductCode.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].ProductCode.replace(/[\, ]/g, ' ').toUpperCase().trim())));
        pQuantityList += (pQuantityList == "" ? (pDataRows[i].Quantity == undefined || pDataRows[i].Quantity.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].Quantity.replace(/[\, ]/g, ' ').toUpperCase().trim()) : ("," + (pDataRows[i].Quantity == undefined || pDataRows[i].Quantity.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].Quantity.replace(/[\, ]/g, ' ').toUpperCase().trim())));
        pPalletIDList += (pPalletIDList == "" ? (pDataRows[i].PalletID == undefined || pDataRows[i].PalletID.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].PalletID.replace(/[\, ]/g, ' ').toUpperCase().trim()) : ("," + (pDataRows[i].PalletID == undefined || pDataRows[i].PalletID.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].PalletID.replace(/[\, ]/g, ' ').toUpperCase().trim())));
        pReceiveDateList += (pReceiveDateList == "" ? (pDataRows[i].ReceiveDate == undefined || pDataRows[i].ReceiveDate.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].ReceiveDate.replace(/[\, ]/g, ' ').toUpperCase().trim()) : ("," + (pDataRows[i].ReceiveDate == undefined || pDataRows[i].ReceiveDate.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].ReceiveDate.replace(/[\, ]/g, ' ').toUpperCase().trim())));
        pLocationCodeList += (pLocationCodeList == "" ? (pDataRows[i].LocationCode == undefined || pDataRows[i].LocationCode.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].LocationCode.replace(/[\, ]/g, ' ').toUpperCase().trim()) : ("," + (pDataRows[i].LocationCode == undefined || pDataRows[i].LocationCode.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].LocationCode.replace(/[\, ]/g, ' ').toUpperCase().trim())));
        pNotesList += (pNotesList == "" ? (pDataRows[i].Notes == undefined || pDataRows[i].Notes.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].Notes.replace(/[\, ]/g, ' ').toUpperCase().trim()) : ("," + (pDataRows[i].Notes == undefined || pDataRows[i].Notes.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].Notes.replace(/[\, ]/g, ' ').toUpperCase().trim())));
        pSerialList += (pSerialList == "" ? (pDataRows[i].Serial == undefined || pDataRows[i].Serial.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].Serial.replace(/[\, ]/g, ' ').toUpperCase().trim()) : ("," + (pDataRows[i].Serial == undefined || pDataRows[i].Serial.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].Serial.replace(/[\, ]/g, ' ').toUpperCase().trim())));

        pBatchNumberList += (pBatchNumberList == "" ? (pDataRows[i].BatchNumber == undefined || pDataRows[i].BatchNumber.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].BatchNumber.replace(/[\, ]/g, ' ').toUpperCase().trim()) : ("," + (pDataRows[i].BatchNumber == undefined || pDataRows[i].BatchNumber.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].BatchNumber.replace(/[\, ]/g, ' ').toUpperCase().trim())));
        pExpirationDateList += (pExpirationDateList == "" ? (pDataRows[i].ExpirationDate == undefined || pDataRows[i].ExpirationDate.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].ExpirationDate.replace(/[\, ]/g, ' ').toUpperCase().trim()) : ("," + (pDataRows[i].ExpirationDate == undefined || pDataRows[i].ExpirationDate.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].ExpirationDate.replace(/[\, ]/g, ' ').toUpperCase().trim())));
        pImportedByList += (pImportedByList == "" ? (pDataRows[i].ImportedBy == undefined || pDataRows[i].ImportedBy.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].ImportedBy.replace(/[\, ]/g, ' ').toUpperCase().trim()) : ("," + (pDataRows[i].ImportedBy == undefined || pDataRows[i].ImportedBy.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].ImportedBy.replace(/[\, ]/g, ' ').toUpperCase().trim())));
        pWeightInTonsList += (pWeightInTonsList == "" ? (pDataRows[i].WeightInTons == undefined || pDataRows[i].WeightInTons.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].WeightInTons.replace(/[\, ]/g, ' ').toUpperCase().trim()) : ("," + (pDataRows[i].WeightInTons == undefined || pDataRows[i].WeightInTons.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].WeightInTons.replace(/[\, ]/g, ' ').toUpperCase().trim())));
    }
    var pParametersWithValues = {
        pReceiveID: $("#hID").val()
        , pBarCodeList: pBarCodeList
        , pPurchaseItemCodeList: pPurchaseItemCodeList
        , pQuantityList: pQuantityList
        , pPalletIDList: pPalletIDList
        , pReceiveDateList: pReceiveDateList
        , pLocationCodeList: pLocationCodeList
        , pNotesList: pNotesList
        , pSerialList: pSerialList

        , pBatchNumberList: pBatchNumberList
        , pExpirationDateList: pExpirationDateList
        , pImportedByList: pImportedByList
        , pWeightInTonsList: pWeightInTonsList
    };
    CallPOSTFunctionWithParameters("/api/Receive/ReceiveDetails_ImportFromExcel", pParametersWithValues
        , function (pData) {
            var _ReturnedMessage = pData[0];
            if (_ReturnedMessage == "") {
                swal("Success", "Saved Successfully.");
                var pReceiveHeader = JSON.parse(pData[2]);
                $("#txtStatus").val(pReceiveHeader.StatusName);
                $("#tblReceive tr[ID=" + pReceiveHeader.ID + "] td.Status").text(pReceiveHeader.StatusName);
                ReceiveDetails_BindTableRows(JSON.parse(pData[1]));
                $("#slWarehouse").val(pReceiveHeader.WarehouseID);
            }
            else {
                swal("Sorry", _ReturnedMessage);
            }
            FadePageCover(false);
        }
        , null);
    $("#btnAddFromExcel").val(""); //if removed the last selected file remains till unselected
}
/****************************ReceiveDetailsSerial*************************************/
function ReceiveDetailsSerial_BindTableRows(pReceiveDetailsSerial) {
    debugger;
    ClearAllTableRows("tblReceiveDetailsSerial");
    //editControlsText = " class='btn btn-xs btn-rounded btn-info float-right' > <i class='fa fa-pencil' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + TranslateString("Edit") + "</span>";
    $.each(pReceiveDetailsSerial, function (i, item) {
            AppendRowtoTable("tblReceiveDetailsSerial",
            ("<tr ID='" + item.ID + "' " + (1 == 2 ? ("ondblclick='ReceiveDetailsSerial_FillControls(" + item.ID + ");'") : "") + ">"
                + "<td class='IsInsert hide'> <input type='checkbox' id='IsInsert" + item.ID + "' /></td>"
                + "<td class='ID'> <input " + (1 == 1 ? "name='Delete'" : "disabled='disabled'") + " type='checkbox' value='" + item.ID + "'></td>"
                + "<td class='Serial'> <input type='text' style='width:100%; text-transform:uppercase;' id='txtReceiveDetailsSerial" + item.ID + "' autocomplete='off' class='form-control controlStyle text-center' onkeypress='DisableEnterKey(id);' onfocus='DisableEnterKey(id);' onchange='ReceiveDetailsSerial_SetIsRowChanged(id);' data-required='false' value='" + item.Serial + "' /> </td>"

                + "<td class='Vehicle'> <input type='text' style='width:100%; text-transform:uppercase;' id='txtReceiveDetailsVehicle" + item.ID + "' autocomplete='off' class='form-control controlStyle text-center' onkeypress='DisableEnterKey(id);' onfocus='DisableEnterKey(id);' onchange='ReceiveDetailsSerial_SetIsRowChanged(id);' data-required='false' value='" + item.Vehicle + "' /> </td>"
                + "<td class='MotorNo'> <input type='text' style='width:100%; text-transform:uppercase;' id='txtReceiveDetailsMotorNo" + item.ID + "' autocomplete='off' class='form-control controlStyle text-center' onkeypress='DisableEnterKey(id);' onfocus='DisableEnterKey(id);' onchange='ReceiveDetailsSerial_SetIsRowChanged(id);' data-required='false' value='" + item.MotorNo + "' /> </td>"

                + "<td class='PickupDetailsLocationID hide'> <input type='text' style='width:100%; text-transform:uppercase;' id='txtReceiveDetailsSerialPickupDetailsLocationID" + item.ID + "' autocomplete='off' class='form-control controlStyle text-center' onkeypress='DisableEnterKey(id);' onfocus='DisableEnterKey(id);' onchange='ReceiveDetailsSerial_SetIsRowChanged(id);' data-required='false' value='" + item.PickupDetailsLocationID + "' /> </td>"
                //+ "<td class='ReceiveDate'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.ReceiveDate)) < 1 ? "" : ConvertDateFormat(GetDateWithFormatMDY(item.ReceiveDate))) + "</td>"
                + "<td class='SelectedIDsToUpdate hide'> <input name='SelectedIDsToUpdate' id='SelectedIDsToUpdate" + item.ID + "' type='checkbox' value='" + item.ID + "' /></td>"
                + "<td class='hide'><a href='#ReceiveDetailsSerialModal' data-toggle='modal' onclick='ReceiveDetailsSerial_FillControls(" + item.ID + ");' " + editControlsText + "</a></td>"
            + "</tr>"));
    });
    ////ApplyPermissions();
    //if (ODPay && $("#hIsOperationDisabled").val() == false) $("#btn-DeletePayable").removeClass("hide"); else $("#btn-DeletePayable").addClass("hide");
    BindAllCheckboxonTable("tblReceiveDetailsSerial", "ID", "cb-CheckAll-ReceiveDetailsSerial");
    CheckAllCheckbox("HeaderDeleteReceiveDetailsSerialID");
    if (showDeleteFailMessage) { //sherif: if deletion failed due to foreign key, then this message is shown
        swal(strSorry, strDeleteFailMessage, "warning");
        showDeleteFailMessage = false;
    }
}
function ReceiveDetailsSerial_NewRow() {
    debugger;
    if ($("#txtReceiveDetailsSerialQuantity").val() <= $("#tblReceiveDetailsSerial tbody tr").length)
        swal("Sorry", "Number of serials can not excceed quantity");
    else {
        ++maxReceiveDetailsSerialIDInTable;
        //var copyControlsText = " class='btn btn-xs btn-rounded btn-info float-right' > <i class='fa fa-copy' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + TranslateString("Copy") + "</span>";
        //var FormattedTodaysDate = getTodaysDateInddMMyyyyFormat();
        //var dateValidTo = Date.prototype.addDays(ConvertDateFormat(FormattedTodaysDate), 30);
        var tr = "";
        tr += "<tr ID='" + maxReceiveDetailsSerialIDInTable + "'>";
        tr += "     <td class='IsInsert hide'> <input type='checkbox' id='IsInsert" + maxReceiveDetailsSerialIDInTable + "' checked='checked' /></td>";
        tr += "     <td class='ID'> <input type='checkbox' value='" + maxReceiveDetailsSerialIDInTable + "' /></td>";
        tr += "     <td class='Serial'> <input type='text' style='width:100%; text-transform:uppercase;' id='txtReceiveDetailsSerial" + maxReceiveDetailsSerialIDInTable + "' autocomplete='off' class='form-control controlStyle text-center' onkeypress='DisableEnterKey(id);' onfocus='DisableEnterKey(id);' onchange='ReceiveDetailsSerial_SetIsRowChanged(id);' data-required='false' value='' /> </td>";

        tr += "     <td class='Vehicle'> <input type='text' style='width:100%; text-transform:uppercase;' id='txtReceiveDetailsVehicle" + maxReceiveDetailsSerialIDInTable + "' autocomplete='off' class='form-control controlStyle text-center' onkeypress='DisableEnterKey(id);' onfocus='DisableEnterKey(id);' onchange='ReceiveDetailsSerial_SetIsRowChanged(id);' data-required='false' value='' /> </td>";
        tr += "     <td class='MotorNo'> <input type='text' style='width:100%; text-transform:uppercase;' id='txtReceiveDetailsMotorNo" + maxReceiveDetailsSerialIDInTable + "' autocomplete='off' class='form-control controlStyle text-center' onkeypress='DisableEnterKey(id);' onfocus='DisableEnterKey(id);' onchange='ReceiveDetailsSerial_SetIsRowChanged(id);' data-required='false' value='' /> </td>";

        tr += "     <td class='PickupDetailsLocationID hide'> <input type='text' style='width:100%; text-transform:uppercase;' id='txtReceiveDetailsSerialPickupDetailsLocationID" + maxReceiveDetailsSerialIDInTable + "' autocomplete='off' class='form-control controlStyle text-center' onkeypress='DisableEnterKey(id);' onfocus='DisableEnterKey(id);' onchange='ReceiveDetailsSerial_SetIsRowChanged(id);' data-required='false' value='0' /> </td>";
        //tr += '     <td class="ValidFrom"><input id="txtValidFrom' + maxReceiveDetailsSerialIDInTable + '" style="width:85px;height:100%;" readonly="readonly" type="text" onfocus="DisableEnterKey(id);" onkeypress="DisableEnterKey(id);" onchange="ReceiveDetailsSerial_SetIsRowChanged(id);" style="background-color:white; cursor:text;" class="input-sm datepicker-input form-control" data-date-format="dd/mm/yyyy" placeholder="Select Date" value="' + FormattedTodaysDate + '" /></td>';
        tr += "     <td class='SelectedIDsToUpdate hide'> <input name='SelectedIDsToUpdate' id='SelectedIDsToUpdate" + maxReceiveDetailsSerialIDInTable + "' type='checkbox' value='" + maxReceiveDetailsSerialIDInTable + "' /></td>";
        //tr += "     <td class=''>"
        //                    + "<a href='#'  onclick='ReceiveDetailsSerial_CopyRow(" + maxReceiveDetailsSerialIDInTable + ");' " + copyControlsText + "</a>"
        //              + "</td>";
        tr += "</tr>";
        //if ($("#tblReceiveDetailsSerial tbody tr").length > 0)
        //    $(tr).insertBefore('#tblReceiveDetailsSerial > tbody > tr:first');
        //else
        $("#tblReceiveDetailsSerial tbody").prepend(tr);
        ReceiveDetailsSerial_hideShow();

        /***************************Filling row controls******************************/
        //SetDatepickerFormat();
        /***********************EOF Filling row controls******************************/
    }
}
function ReceiveDetailsSerial_FillModal(pReceiveDetailsID) {
    debugger;
    $("#divSerialsBtns").removeClass("hide");
    $("#hReceiveDetailsID").val(pReceiveDetailsID);
    $("#divReceiveDetailsSerial").html("");

    var tr = $("#tblReceiveDetails tr[ID='" + pReceiveDetailsID + "']");
    $("#lblReceiveDetailsSerialShown").html(": " + $(tr).find("td.PurchaseItemCode").text());
    $("#txtReceiveDetailsSerialQuantity").val($(tr).find("td.Quantity").text());
    FadePageCover(true);
    CallGETFunctionWithParameters("/api/Receive/ReceiveDetailsSerial_LoadAll"
        //, { pWhereClauseReceiveDetailsSerial: "WHERE ReceiveDetailsID=" + pReceiveDetailsID + " ORDER BY Serial"}
        , { pWhereClauseReceiveDetailsSerial: "WHERE ReceiveDetailsID=" + pReceiveDetailsID + " and Serial is not null ORDER BY Serial" }
        , function (pData) {
            var pReceiveDetailsSerial = JSON.parse(pData[0]);
            ReceiveDetailsSerial_BindTableRows(pReceiveDetailsSerial);
            FadePageCover(false);
        }
        , function () {
            $(".ExpireDate").addClass("hide");
            if (pDefaults.UnEditableCompanyName != "NIL")
                $(".LotNo").addClass("hide");
            $(".Vehicle").addClass("hide");
            $(".MotorNo").addClass("hide");
            $(".Serial").removeClass("hide");
            $("#txtflag").val("ReceiveDetailsSerial_FillModal");
        });
}
function ReceiveDetailsVehicle_FillModal(pReceiveDetailsID) {
    debugger;
    $("#divSerialsBtns").removeClass("hide");

    $("#hReceiveDetailsID").val(pReceiveDetailsID);
    $("#divReceiveDetailsSerial").html("");

    var tr = $("#tblReceiveDetails tr[ID='" + pReceiveDetailsID + "']");
    $("#lblReceiveDetailsSerialShown").html(": " + $(tr).find("td.PurchaseItemCode").text());
    $("#txtReceiveDetailsSerialQuantity").val($(tr).find("td.Quantity").text());
    FadePageCover(true);
    CallGETFunctionWithParameters("/api/Receive/ReceiveDetailsSerial_LoadAll"
        , { pWhereClauseReceiveDetailsSerial: "WHERE ReceiveDetailsID=" + pReceiveDetailsID + " and (Vehicle is not null and MotorNo is not null) ORDER BY Vehicle" }
        , function (pData) {
            var pReceiveDetailsSerial = JSON.parse(pData[0]);
            ReceiveDetailsSerial_BindTableRows(pReceiveDetailsSerial);
            FadePageCover(false);
        }
        , function () {
            $(".ExpireDate").addClass("hide");
            if (pDefaults.UnEditableCompanyName != "NIL")
                $(".LotNo").addClass("hide");
            $(".Serial").addClass("hide");
            $(".Vehicle").removeClass("hide");
            $(".MotorNo").removeClass("hide");
            $("#txtflag").val("ReceiveDetailsVehicle_FillModal");
        });
}
function ReceiveDetailsSerial_GenerateDefaultSerial() {
    debugger;
    //if (GetAllSelectedIDsAsString('tblReceive') != "")
        swal({
            title: "Are you sure?",
            text: "Default serials will be created!",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, Generate!",
            closeOnConfirm: true
        },
        //callback function in case of confirm delete
        function () {
            FadePageCover(true);
            $("#divReceiveDetailsSerial").html("");
            CallGETFunctionWithParameters("/api/Receive/ReceiveDetailsSerial_GenerateSerial"
            , { pReceiveDetailsIDForSerialGenerate: $("#hReceiveDetailsID").val(), pQuantity: $("#txtReceiveDetailsSerialQuantity").val() }
            , function (pData) {
                var pReceiveDetailsSerial = JSON.parse(pData[0]);
                ReceiveDetailsSerial_BindTableRows(pReceiveDetailsSerial);
                FadePageCover(false);
            }
            , null);
        });
}
function ReceiveDetailsSerial_Save() {
    debugger;
    var pSelectedIDsToSave = GetAllSelectedIDsAsStringWithNameAttr("SelectedIDsToUpdate");
    var pIsInsertList = "";
    var pSerialList = "";
    var pPickupDetailsLocationIDList = "";

    var pVehicleList = "";
    var pMotorNoList = "";
    var pFlag = $("#txtflag").val();

    if (pSelectedIDsToSave == "")
        swal("Sorry", "No records changed.");
    else {
        FadePageCover(true);
        var NumberOfSelectRows = pSelectedIDsToSave.split(',').length;
        for (var i = 0; i < NumberOfSelectRows; i++) {
            var currentRowID = pSelectedIDsToSave.split(",")[i];
            pIsInsertList += ((pIsInsertList == "") ? "" : ",") + ($("#IsInsert" + currentRowID).prop("checked") ? "1" : "0");
            pSerialList += ((pSerialList == "") ? "" : ",") + ($("#txtReceiveDetailsSerial" + currentRowID).val().trim() == "" ? "0" : $("#txtReceiveDetailsSerial" + currentRowID).val().trim().toUpperCase());
            pPickupDetailsLocationIDList += ((pPickupDetailsLocationIDList == "") ? "" : ",") + ($("#txtReceiveDetailsSerialPickupDetailsLocationID" + currentRowID).val().trim() == "" ? "0" : $("#txtReceiveDetailsSerialPickupDetailsLocationID" + currentRowID).val().trim().toUpperCase());

            pVehicleList += ((pVehicleList == "") ? "" : ",") + ($("#txtReceiveDetailsVehicle" + currentRowID).val().trim() == "" ? "0" : $("#txtReceiveDetailsVehicle" + currentRowID).val().trim().toUpperCase());
            pMotorNoList += ((pMotorNoList == "") ? "" : ",") + ($("#txtReceiveDetailsMotorNo" + currentRowID).val().trim() == "" ? "0" : $("#txtReceiveDetailsMotorNo" + currentRowID).val().trim().toUpperCase());
        }
        var pParametersWithValues = {
            pReceiveDetailsID: $("#hReceiveDetailsID").val()
            , pSelectedIDsToSave: pSelectedIDsToSave
            , pIsInsertList: pIsInsertList
            , pSerialList: pSerialList
            , pPickupDetailsLocationIDList: pPickupDetailsLocationIDList

            , pVehicleList: pVehicleList
            , pMotorNoList: pMotorNoList
            , pFlag: pFlag
        };
        CallPOSTFunctionWithParameters("/api/Receive/ReceiveDetailsSerial_Save", pParametersWithValues
            , function (pData) {
                var _MessageReturned = pData[0];
                if (_MessageReturned == "") {
                    jQuery("#ReceiveDetailsSerialModal").modal("hide");
                    swal("Success", "Saved successfully.");
                }
                else
                    swal("Sorry", _MessageReturned);
                FadePageCover(false);
            }
            , null);
    }
}
function ReceiveDetailsSerial_DeleteList() {
    debugger;
    var pReceiveDetailsSerialIDsDeleted = GetAllSelectedIDsAsStringWithTableNameAndNameAttr("tblReceiveDetailsSerial", "Delete");
    if (pReceiveDetailsSerialIDsDeleted != "")
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
            CallPOSTFunctionWithParameters("/api/Receive/ReceiveDetailsSerial_DeleteList"
                , {
                    pReceiveDetailsSerialIDsDeleted: pReceiveDetailsSerialIDsDeleted
                    , pReceiveDetailsID: $("#hReceiveDetailsID").val()
                    , pFlag: $("#txtflag").val()
                }
                , function (pData) {
                    var _MessageReturned = pData[0];
                    if (_MessageReturned == "")
                        swal("Success", "Deleted successfully.");
                    else {
                        swal("Sorry", strDeleteFailMessage);
                    }
                    ReceiveDetailsSerial_BindTableRows(JSON.parse(pData[1]));
                    //setTimeout(function () {
                    //    ReceiveDetailsSerial_hideShow();
                    //}, 1000);
                    ReceiveDetailsSerial_hideShow();
                    FadePageCover(false);
                });
        });
}
function ReceiveDetailsSerial_hideShow() {
    if ($("#txtflag").val() == "ReceiveDetailsSerial_FillModal") {
        $(".ExpireDate").addClass("hide");
        if (pDefaults.UnEditableCompanyName != "NIL")
            $(".LotNo").addClass("hide");
        $(".Vehicle").addClass("hide");
        $(".MotorNo").addClass("hide");
    }
    else if ($("#txtflag").val() == "ReceiveDetailsVehicle_FillModal") {
        $(".ExpireDate").addClass("hide");
        if (pDefaults.UnEditableCompanyName != "NIL")
            $(".LotNo").addClass("hide");
        $(".Serial").addClass("hide");
    }
}
function ReceiveDetailsSerial_SetIsRowChanged(pControlID) {
    debugger;
    var ChangedRowID = $("#" + pControlID).parent().parent().attr("ID");
    $("#SelectedIDsToUpdate" + ChangedRowID).prop("checked", true);
}
/********************************VehicleAction*****************************************/
function VehicleAction_BindTableRows(pVehicleAction) {
    debugger;
    ClearAllTableRows("tblVehicleAction");
    //var printControlsText = " class='btn btn-xs btn-rounded btn-lightblue float-right' title='Print'> <i class='fa fa-print' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + TranslateString("Print") + "</span>";
    //var alarmControlsText = " class='btn btn-xs btn-rounded btn-warning float-right' title='Print'> <i class='fa fa-bell' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + "Alarm" + "</span>";
    //var emailControlsText = " class='btn btn-xs btn-rounded btn-lightblue float-right' title='Print'> <i class='fa fa-envelope' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + "Email" + "</span>";
    $.each(pVehicleAction, function (i, item) {
        AppendRowtoTable("tblVehicleAction",
        ("<tr ID='" + item.ID + "' " + ($("#hf_CanEdit").val() == 1 ? "ondblclick='VehicleAction_FillControls(" + item.ID + ',"tblVehicleAction"' + ");'>" : ">")
            + "<td class='VehicleActionID'> <input " + (1 == 1 ? "name='Delete'" : "disabled='disabled'") + " type='checkbox' value='" + item.ID + "' /></td>"
            + "<td class='NoAccessVehicleActionID hide'>" + item.VehicleActionID + "</td>"
            + "<td class='VehicleActionMotorNumber'>" + (item.MotorNumber == 0 ? "" : item.MotorNumber) + "</td>"
            + "<td class='VehicleActionName hide'>" + item.VehicleActionName + "</td>"
            + "<td class='VehicleActionDate'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.ActionDate)) > 0 ? ConvertDateFormat(GetDateWithFormatMDY(item.ActionDate)) : "") + "</td>"
            + "<td class='VehicleActionNotes'>" + (item.InspectionNotes == 0 ? "" : item.InspectionNotes) + "</td>"
            //+ "<td class='TrackingDone'> <input id='cbIsDone" + item.ID + "' type='checkbox' disabled='disabled' val='" + (item.Done == true ? "true' checked='checked'" : "'") + " /></td>"
            //+ "<td class='hide'><a href='#VehicleActionModal' data-toggle='modal' onclick='VehicleAction_FillControls(" + item.ID + ");' " + editControlsText + "</a></td>"
            + "<td class='hide'>"
                //+ "<a href='#' data-toggle='modal' onclick='VehicleAction_GetAvailableUsers(" + item.ID + ");' " + alarmControlsText + "</a>"
                //+ "<a href='#' data-toggle='modal' onclick='VehicleAction_SendEmail(" + item.ID + ");' " + emailControlsText + "</a>"
            + "</td>"
        + "</tr>"));
    });
    //ApplyPermissions();
    //if (1 == 1) { $("#btn-AddVehicleAction").removeClass("hide"); } else { $("#btn-AddVehicleAction").addClass("hide"); }
    //if (1 == 1) { $("#btn-DeleteVehicleAction").removeClass("hide"); } else { $("#btn-DeleteVehicleAction").removeClass("hide"); }
    BindAllCheckboxonTable("tblVehicleAction", "VehicleActionID", "cb-CheckAll-VehicleAction");
    CheckAllCheckbox("HeaderDeleteVehicleActionID");

    if (showDeleteFailMessage) { //sherif: if deletion failed due to foreign key, then this message is shown
        swal(strSorry, strDeleteFailMessage, "warning");
        showDeleteFailMessage = false;
    }
}
function VehicleAction_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned(pOperationVehicleID) {
    debugger;
    jQuery("#VehicleActionTableModal").modal("show");
    FadePageCover(true);
    var pWhereClauseForVehicleAction = "WHERE OperationVehicleID=" + pOperationVehicleID;
    CallGETFunctionWithParameters("/api/OperationVehicle/VehicleAction_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned",
        {
            pIsLoadArrayOfObjects: false
            , pLanguage: $("[id$='hf_ChangeLanguage']").val()
            , pPageNumber: 1
            , pPageSize: 999999
            , pWhereClauseForVehicleAction: pWhereClauseForVehicleAction
            , pOrderBy: "ActionDate DESC, ID DESC"
        }
        , function (pData) { VehicleAction_BindTableRows(JSON.parse(pData[0])); FadePageCover(false); }
        , null);
}
function VehicleAction_FillInspectionModal(pTableName) {
    debugger;
    var _SelectedRows = GetAllSelectedIDsAsString(pTableName);
    if (_SelectedRows == "")
        swal("Sorry", "Please, select rows.");
    else {
        //if (_SelectedRows.split(",").length > 1)
        //    $("#slVehicleAction").attr("disabled", "disabled");
        //else
        //    $("#slVehicleAction").removeAttr("disabled");
        ClearAll("#VehicleActionModal");
        $(".classHideForInspection").addClass("hide");
        $("#txtVehicleActionDate").val(getTodaysDateInddMMyyyyFormat());
        $("#slVehicleActionWarehouse").html("<option value=''><--Select--></option>");
        $("#slVehicleActionArea").html("<option value=''><--Select--></option>");
        $("#slVehicleActionRow").html("<option value=''><--Select--></option>");
        $("#slVehicleActionLocation").html("<option value=''><--Select--></option>");
        $("#btnSaveVehicleAction").attr("onclick", "VehicleAction_Save('tblReceiveDetails');");

        //$("#slVehicleActionWarehouse").attr("disabled", "disabled");
        FadePageCover(true);
        var pParametersWithValues = {
            pWhereClauseNoAccessVehicleAction: "WHERE IsWarehouseAction=1"
        };
        CallGETFunctionWithParameters("/api/OperationVehicle/FillInspectionModal", pParametersWithValues
            , function (pData) {
                jQuery("#VehicleActionModal").modal("show");
                var pNoAccessVehicleAction = pData[1];
                var pWarehouse = pData[2];
                FillListFromObject(null, 2, null, "slVehicleAction", pNoAccessVehicleAction, null);
                FillListFromObject(null, 9, "<--Select-->", "slVehicleActionWarehouse", pWarehouse, null);
                FadePageCover(false);
            }
            , null);
    }
}
function VehicleAction_Save(pTableName) {
    debugger;
    var _SelectedVehicles = GetAllSelectedIDsFromTd(pTableName, "Delete", "OperationVehicleID"); //GetAllSelectedIDsAsString(pTableName);
    if ($("#slVehicleAction").val() == constVehicleActionChangeWarehouse && $("#slVehicleActionWarehouse").val() == "")
        swal("Sorry", "Please, Select warehouse.");
    else if ($("#slVehicleAction").val() == constVehicleActionChangeWarehouse && $("#slVehicleActionWarehouse").val() == $("#slWarehouse").val())
        swal("Sorry", "This is the same warehouse.");
    else if ($("#slVehicleAction").val() == constVehicleActionPickup && !$("#cbIsFinalized").prop("checked"))
        swal("Sorry", "The receive must be finalized.");
    else {
        FadePageCover(true);
        var pParametersWithValues = {
            pOperationVehicleIDsList: _SelectedVehicles
            , pOperationID: $("#slOperation").val() == "" ? 0 : $("#slOperation").val()
            , pVehicleActionID: $("#slVehicleAction").val()
            , pActionDate: $("#txtVehicleActionDate").val()
            , pInspectionNotes: $("#txtVehicleActionNotes").val().trim() == "" ? 0 : $("#txtVehicleActionNotes").val().trim().toUpperCase()
            , pWarehouseID: $("#slVehicleActionWarehouse").val() == "" ? 0 : $("#slVehicleActionWarehouse").val()
            , pRowLocationID: $("#slVehicleActionLocation").val() == "" ? 0 : $("#slVehicleActionLocation").val()
        };
        CallPOSTFunctionWithParameters("/api/OperationVehicle/VehicleAction_Save", pParametersWithValues
        , function (pData) {
            var _MessageReturned = pData[0];
            var _ReceiveHeader = JSON.parse(pData[1]);
            var _PickupHeader = JSON.parse(pData[2]);
            var _Code = _ReceiveHeader != null ? _ReceiveHeader.Code : (_PickupHeader != null ? _PickupHeader.Code : null);

            if (_MessageReturned == "") {
                jQuery("#VehicleActionModal").modal("hide");
                Receive_FillAllControls($("#hID").val());
                swal("Success", _Code == null ? "Saved successfully." : ("Saved to " + _Code + "."));
            }
            else {
                swal("Sorry", _MessageReturned);
            }
            FadePageCover(false);
        }
        , null);
    }
}
function VehicleAction_ActionChanged() {
    debugger;
    if ($("#slVehicleAction").val() == constVehicleActionInspection || $("#slVehicleAction").val() == constVehicleActionPickup) {
        $(".classHideForInspection").addClass("hide");
        $("#slVehicleActionWarehouse").val($("#slWarehouse").val());
    }
    else {
        $(".classHideForInspection").removeClass("hide");
    }
}
function VehicleAction_WarehouseChanged() {
    debugger;
    $("#slVehicleActionArea").html("<option value=''><--Select--></option>");
    $("#slVehicleActionRow").html("<option value=''><--Select--></option>");
    $("#slVehicleActionLocation").html("<option value=''><--Select--></option>");
    if ($("#slVehicleActionWarehouse").val() != "") {
        FadePageCover(true);
        var pParametersWithValues = {
            pIsLoadArrayOfObjects: false
            , pLanguage: $("[id$='hf_ChangeLanguage']").val()
            , pPageNumber: 1
            , pPageSize: 999999
            , pWhereClause: "WHERE WarehouseID=" + $("#slVehicleActionWarehouse").val()
            , pOrderBy: "Name"
        };
        CallGETFunctionWithParameters("/api/Area/LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned"
            , pParametersWithValues
            , function (pData) {
                var _Area = pData[0];
                FillListFromObject(null, 2, "<--Select-->", "slVehicleActionArea", _Area, null);
                FadePageCover(false);
            }
            , null);
    }
}
function VehicleAction_AreaChanged() {
    debugger;
    $("#slVehicleActionRow").html("<option value=''><--Select--></option>");
    $("#slVehicleActionLocation").html("<option value=''><--Select--></option>");
    if ($("#slVehicleActionArea").val() != "") {
        FadePageCover(true);
        var pParametersWithValues = {
            pIsLoadArrayOfObjects: false
            , pLanguage: $("[id$='hf_ChangeLanguage']").val()
            , pPageNumber: 1
            , pPageSize: 999999
            , pWhereClause: "WHERE AreaID=" + $("#slVehicleActionArea").val()
            , pOrderBy: "Name"
        };
        CallGETFunctionWithParameters("/api/Row/LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned"
            , pParametersWithValues
            , function (pData) {
                var _Row = pData[0];
                FillListFromObject(null, 2, "<--Select-->", "slVehicleActionRow", _Row, null);
                FadePageCover(false);
            }
            , null);
    }
}
function VehicleAction_RowChanged() {
    debugger;
    $("#slVehicleActionLocation").html("<option value=''><--Select--></option>");
    if ($("#slVehicleActionRow").val() != "") {
        FadePageCover(true);
        var pParametersWithValues = {
            pRowLocationWhereClauseWithMinimalColumns: "WHERE IsUsed=0 AND RowID=" + $("#slVehicleActionRow").val()
            , pOrderBy: "Code"
            , pWarehouseID: $("#slVehicleActionWarehouse").val() == "" ? 0 : $("#slVehicleActionWarehouse").val()
        };
        CallGETFunctionWithParameters("/api/Row/RowLocation_LoadAllWithMinimalColumns"
            , pParametersWithValues
            , function (pData) {
                var _Row = pData[0];
                FillListFromObject(null, 1, "<--Select-->", "slVehicleActionLocation", _Row, null);
                FadePageCover(false);
            }
            , null);
    }
}
