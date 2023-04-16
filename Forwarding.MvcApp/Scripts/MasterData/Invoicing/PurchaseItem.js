function PurchaseItem_Initialize() {
    debugger;
    strBindTableRowsFunctionName = "PurchaseItem_BindTableRows";
    strLoadWithPagingFunctionName = "/api/PurchaseItem/LoadWithPaging";
    //the first parameter in the LoadView() fn. is the route in the RouteConfig
    LoadView("/MasterData/PurchaseItem", "div-content", function () {

        if (glbCallingControl == "PurchaseItem") {
            $("#liGroupName").text("Master Data");
            $("#liGroupName").attr("onclick", "LoadViews('Invoicing')");
            $("#liTabName").text("Invoicing");
            $("#liTabName").attr("onclick", "LoadViews('Invoicing')");
        }
        else { //Warehousing
            $("#liGroupName").text("Master Data");
            $("#liGroupName").attr("onclick", "LoadViews('WarehousingMasterData')");
            $("#liTabName").text("Warehousing");
            $("#liTabName").attr("onclick", "LoadViews('WarehousingMasterData')");
        }
        if (pDefaults.UnEditableCompanyName == "NIL") {
            $(".classMandatoryForNIL").attr("data-required", "true");
            $(".classHideForNIL").addClass("hide");
        }
        $("#slCurrency").html($("#hReadySlCurrencies").html());
        LoadWithPagingWithObjectArrayReturned("div-Pager", "select-page-size", "spn-first-page-row", "spn-last-page-row", "spn-total-count", "div-Text-Total", strLoadWithPagingFunctionName, 0, 10
            , function (pData) {
                var pLengthUnit = pData[2];
                var pWeightUnit = pData[3];
                var pVolumeUnit = pData[4];
                var pCommodity = pData[5];
                var pPackageType = pData[6];
                var pIMOClass = pData[7];
                var pWH_Area = pData[8];

                // For ERP
                var ItemsType = pData[9];
                var ItemsGroup = pData[10];

                Fill_SelectInputAfterLoadData(ItemsType, "ID", "Name", "Select Item Type", "#slItemType", '');
                Fill_SelectInputAfterLoadData(ItemsGroup, "ID", "Name", "Select Item Group", "#slItemGroup", '');


                PurchaseItem_BindTableRows(JSON.parse(pData[0]));
                FillListFromObject(pDefaults.LengthUnitID, 1, null/*pStrFirstRow*/, "slLengthUnit", pLengthUnit, null);
                FillListFromObject(pDefaults.WeightUnitID, 1, null/*pStrFirstRow*/, "slWeightUnit", pWeightUnit, null);
                FillListFromObject(pDefaults.VolumeUnitID, 1, null/*pStrFirstRow*/, "slVolumeUnit", pVolumeUnit, null);
                FillListFromObject(null, 2, "<--Select-->", "slCommodity", pCommodity, null);
                FillListFromObject(null, 2, "<--Select-->", "slPackageType", pPackageType
                , function () { $("#slPackageTypeBarCode").html($("#slPackageType").html()); $("#slToPackageType").html($("#slPackageType").html()); $("#slFromPackageType").html($("#slPackageType").html()); });
                FillListFromObject(null, 9, "<--Select-->", "slIMOClass", pIMOClass, null);
                FillListFromObject(null, 2, "<--Select-->", "slPreferredArea", pWH_Area, null);

            });
    },
        function () { PurchaseItem_ClearAllControls(); },
        function () { PurchaseItem_DeleteList(); });
}
function PurchaseItem_BindTableRows(pPurchaseItem) {
    debugger;
    if (glbCallingControl == "PurchaseItem")
        $("#hl-menu-MasterData").parent().addClass("active");
    else
        $("#hl-menu-Warehousing").parent().addClass("active");
    ClearAllTableRows("tblPurchaseItem");
    //editControlsText = " class='btn btn-xs btn-rounded btn-info float-right' > <i class='fa fa-pencil' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + TranslateString("Edit") + "</span>";
    $.each(pPurchaseItem, function (i, item) {
        AppendRowtoTable("tblPurchaseItem",
        ("<tr ID='" + item.ID + "' ondblclick='PurchaseItem_EditByDblClick(" + item.ID + ");'>"
                    + "<td class='ID'> <input " + (item.Code.toUpperCase() == "FLEXI" || item.Code.toUpperCase() == "HEATER PAD" || item.Code.toUpperCase() == "IRON" ? " disabled=disabled " : " name='Delete' ") + " type='checkbox' value='" + item.ID + "' /></td>"
                    + "<td class='Code'>" + (item.Code == 0 ? "" : item.Code) + "</td>"
                    + "<td class='Name'>" + (item.Name == 0 ? "" : item.Name) + "</td>"
                    + "<td class='LocalName hide'>" + (item.LocalName == 0 ? "" : item.LocalName) + "</td>"
                    + "<td class='PartNumber'>" + (item.PartNumber == 0 ? "" : item.PartNumber) + "</td>"
                    + "<td class='HSCode'>" + (item.HSCode == 0 ? "" : item.HSCode) + "</td>"
                    + "<td class='Price'>" + item.Price + "</td>"
                    + "<td class='CurrencyID hide'>" + item.CurrencyID + "</td>"
                    + "<td class='CurrencyCode hide'>" + item.CurrencyCode + "</td>"
                    + "<td class='AccountID hide'>" + item.AccountID + "</td>"
                    + "<td class='SubAccountID hide'>" + item.SubAccountID + "</td>"
                    + "<td class='CostCenterID hide'>" + item.CostCenterID + "</td>"
                    + "<td class='ViewOrder hide'>" + item.ViewOrder + "</td>"
                    + "<td class='Notes hide'>" + (item.Notes == 0 ? "" : item.Notes) + "</td>"
                    
                    + "<td class='hide'><a href='#PurchaseItemModal' data-toggle='modal' onclick='PurchaseItem_FillControls(" + item.ID + ");' " + editControlsText + "</a></td></tr>"));
    });
    debugger;
    ApplyPermissions();
    BindAllCheckboxonTable("tblPurchaseItem", "ID");
    CheckAllCheckbox("ID");
    HighlightText("#tblPurchaseItem>tbody>tr", $("#txt-Search").val().trim());//sherif:new
    if (showDeleteFailMessage) {//sherif: if deletion failed due to foreign key, then this message is shown
        swal(strSorry, strDeleteFailMessage, "warning");
        showDeleteFailMessage = false;
    }
    $("#hl-homepage").on("click", function () { LoadViews("hl-homepage"); });
}
function PurchaseItem_EditByDblClick(pID) {
    jQuery("#PurchaseItemModal").modal("show");
    PurchaseItem_FillControls(pID);
}
// Loading with data
function PurchaseItem_LoadingWithPaging() {
    debugger;
    LoadWithPaging("div-Pager", "select-page-size", "spn-first-page-row", "spn-last-page-row", "spn-total-count", "div-Text-Total", "/api/PurchaseItem/LoadWithPaging", $("#div-Pager li.active a").text(), $('#select-page-size').val().trim(), function (pTabelRows) { PurchaseItem_BindTableRows(pTabelRows); });
    HighlightText("#tblPurchaseItem>tbody>tr", $("#txt-Search").val().trim());
}
function PurchaseItem_ClearAllControls(callback) {
    debugger;
    $("#tblPackageTypeBarCode tbody").html("");
    $("#tblPackageTypeConversion tbody").html("");
    $("#tblDocsIn tbody").html("");
    ClearAll("#PurchaseItemModal");
    PurchaseItem_EnableDisableIMOProprties(false);
    $("#slCurrency").val($("#hDefaultCurrencyID").val());
    $("#slLengthUnit").val(pDefaults.LengthUnitID);
    $("#slWeightUnit").val(pDefaults.WeightUnitID);
    $("#slVolumeUnit").val(pDefaults.VolumeUnitID);
    $("#btnSave").attr("onclick", "PurchaseItem_Insert(false);");
    $("#btnSaveAndAddNew").attr("onclick", "PurchaseItem_Insert(true);");
    $("#cb-CheckAll").prop('checked', false);
                    //***** For ERP
    $('#slItemGroup').val("0");
    $('#slItemType').val("0");
    if ($("#hDefaultUnEditableCompanyName").val() == "ERP") {
        $("#txtCode").attr("disabled", "disabled");
       // $("#txtName").attr("disabled", "disabled");
       // $("#txtLocalName").attr("disabled", "disabled");
    }
    if (callback != null && callback != undefined)
        callback();
}
function PurchaseItem_FillControls(pID) {
    debugger;
    FadePageCover(true);
    $("#tblPackageTypeBarCode tbody").html("");
    $("#tblPackageTypeConversion tbody").html("");
    $("#tblDocsIn tbody").html("");
    CallGETFunctionWithParameters("/api/PurchaseItem/LoadHeaderWithDetails"
        , { pPurchaseItemHeaderID: pID }
        , function (pData) {
            var pHeader = JSON.parse(pData[0]);
            var pPackageTypeBarCode = JSON.parse(pData[1]);
            var pPackageTypeConversion = JSON.parse(pData[2]);
            if ($("#hDefaultUnEditableCompanyName").val() == "ERP") {
                $("#txtCode").attr("disabled", "disabled");
               // $("#txtName").attr("disabled", "disabled");
               // $("#txtLocalName").attr("disabled", "disabled");
            }
            else
            {

                if (pHeader.Code == "FLEXI" || pHeader.Code == "HEATER PAD" || pHeader.Code == "IRON")
                {
                    $("#txtCode").attr("disabled", "disabled");
                    $("#txtName").attr("disabled", "disabled");
                    $("#txtLocalName").attr("disabled", "disabled");
                }
                else
                {
                    $("#txtCode").removeAttr("disabled");
                    $("#txtName").removeAttr("disabled");
                    $("#txtLocalName").removeAttr("disabled");
                }
            }
           
            $("#hID").val(pID);
            $("#txtUploadFolderName").val(pHeader.CreationYear + '_' + pID);
            PackageTypeBarCode_BindTableRows(pPackageTypeBarCode);
            PackageTypeConversion_BindTableRows(pPackageTypeConversion);
            PurchaseItem_EnableDisableIMOProprties(pHeader.IsIMO);
            $("#lblShown").html(": " + pHeader.Name == 0 ? "" : pHeader.Name);
            $("#txtCode").val(pHeader.Code == 0 ? "" : pHeader.Code);
            $("#txtName").val(pHeader.Name == 0 ? "" : pHeader.Name);
            $("#txtLocalName").val(pHeader.LocalName == 0 ? "" : pHeader.LocalName);
            $("#txtPartNumber").val(pHeader.PartNumber == 0 ? "" : pHeader.PartNumber);
            $("#txtHSCode").val(pHeader.HSCode == 0 ? "" : pHeader.HSCode);
            $("#txtPrice").val(pHeader.Price);
            $("#txtStockUnitQuantity").val(pHeader.StockUnitQuantity);
            $("#slCurrency").val(pHeader.CurrencyID);
            $("#txtViewOrder").val(pHeader.ViewOrder);
            $("#txtNotes").val(pHeader.Notes == 0 ? "" : pHeader.Notes);
            $("#txtModelNumber").val(pHeader.ModelNumber == 0 ? "" : pHeader.ModelNumber);
            $("#txtBrandName").val(pHeader.BrandName == 0 ? "" : pHeader.BrandName);
            $("#txtProductType").val(pHeader.ProductType == 0 ? "" : pHeader.ProductType);

            $("#slCommodity").val(pHeader.CommodityID == 0 ? "" : pHeader.CommodityID);
            $("#slPackageType").val(pHeader.PackageTypeID == 0 ? "" : pHeader.PackageTypeID);
            $("#txtGrossWeight").val(pHeader.GrossWeight == 0 ? "" : pHeader.GrossWeight);
            $("#txtNetWeight").val(pHeader.NetWeight == 0 ? "" : pHeader.NetWeight);
            $("#slWeightUnit").val(pHeader.WeightUnitID == 0 ? pDefaults.WeightUnitID : pHeader.WeightUnitID);
            $("#txtWidth").val(pHeader.Width == 0 ? "" : pHeader.Width);
            $("#txtDepth").val(pHeader.Depth == 0 ? "" : pHeader.Depth);
            $("#txtHeight").val(pHeader.Height == 0 ? "" : pHeader.Height);
            $("#slLengthUnit").val(pHeader.LengthUnitID == 0 ? pDefaults.LengthUnitID : pHeader.LengthUnitID);
            $("#txtVolume").val(pHeader.Volume == 0 ? "" : pHeader.Volume);
            $("#slVolumeUnit").val(pHeader.VolumeUnitID == 0 ? pDefaults.VolumeUnitID : pHeader.VolumeUnitID);
            $("#cbIsFragile").prop("checked", pHeader.IsFragile);

            $("#cbIsIMO").prop("checked", pHeader.IsIMO);
            $("#slIMOClass").val(pHeader.IMOClassID == 0 ? "" : pHeader.IMOClassID);
            $("#txtUN").val(pHeader.UN == 0 ? "" : pHeader.UN);
            $("#slPG").val(pHeader.PG == 0 ? "" : pHeader.PG);

            $("#slPreferredArea").val(pHeader.PreferredArea == 0 ? "" : pHeader.PreferredArea);
            $("#cbByExpireDate").prop("checked", pHeader.ByExpireDate);
            $("#cbBySerialNo").prop("checked", pHeader.BySerialNo);
            $("#cbByLotNo").prop("checked", pHeader.ByLotNo);
            $("#cbByVehicle").prop("checked", pHeader.ByVehicle);
            //*************** For ERP ******************************
            $('#slItemGroup').val(pHeader.ParentGroupID);
            $('#slItemType').val(pHeader.ItemTypeID);


            $("#btnSave").attr("onclick", "PurchaseItem_Update(false);");
            $("#btnSaveAndAddNew").attr("onclick", "PurchaseItem_Update(true);");
            FadePageCover(false);
        }
        , null);
}
function PurchaseItem_Insert(pSaveAndNew) {
    debugger;
    FadePageCover(true);

    if ($("#hDefaultUnEditableCompanyName").val() == "ERP")
    {
        $("#txtCode").val("AUTO")
    }

    if ($("#hDefaultUnEditableCompanyName").val() != "ERP" && ($("#txtCode").val() == "" || $("#txtCode").val() == "0")    ) {
        swal("Sorry", "You Must Insert Code", "warning");
        FadePageCover(false);
    }
    else
    {





        if (ValidateForm("form", "PurchaseItemModal")) {
            pParametersWithValues = {
                  pCode: $("#txtCode").val().trim() == "" ? "0" : $("#txtCode").val().trim().toUpperCase()
                , pName: $("#txtName").val().trim() == "" ? "0" : $("#txtName").val().trim().toUpperCase()
                , pLocalName: $("#txtLocalName").val().trim() == "" ? "0" : $("#txtLocalName").val().trim().toUpperCase()
                , pPartNumber: $("#txtPartNumber").val().trim() == "" ? "0" : $("#txtPartNumber").val().trim().toUpperCase()
                , pHSCode: $("#txtHSCode").val().trim() == "" ? "0" : $("#txtHSCode").val().trim().toUpperCase()
                , pStockUnitQuantity: $("#txtStockUnitQuantity").val().trim() == "" ? "0" : $("#txtStockUnitQuantity").val().trim().toUpperCase()
                , pPrice: $("#txtPrice").val().trim() == "" ? "0" : $("#txtPrice").val().trim().toUpperCase()
                , pCurrencyID: $("#slCurrency").val()
                , pAccountID: 0
                , pSubAccountID: 0
                , pCostCenterID: 0
                , pViewOrder: $("#txtViewOrder").val().trim() == "" ? "0" : $("#txtViewOrder").val().trim().toUpperCase()
                , pNotes: $("#txtNotes").val().trim() == "" ? "0" : $("#txtNotes").val().trim().toUpperCase()
                , pModelNumber: $("#txtModelNumber").val().trim() == "" ? "0" : $("#txtModelNumber").val().trim().toUpperCase()
                , pBrandName: $("#txtBrandName").val().trim() == "" ? "0" : $("#txtBrandName").val().trim().toUpperCase()
                , pProductType: $("#txtProductType").val().trim() == "" ? "0" : $("#txtProductType").val().trim().toUpperCase()
                //Warehouse parameters
                , pCommodityID: $("#slCommodity").val() == "" ? "0" : $("#slCommodity").val()
                , pPackageTypeID: $("#slPackageType").val() == "" ? "0" : $("#slPackageType").val()
                , pGrossWeight: $("#txtGrossWeight").val() == "" ? "0" : $("#txtGrossWeight").val()
                , pNetWeight: $("#txtNetWeight").val() == "" ? "0" : $("#txtNetWeight").val()
                , pWeightUnitID: $("#slWeightUnit").val() == "" ? "0" : $("#slWeightUnit").val()
                , pWidth: $("#txtWidth").val() == "" ? "0" : $("#txtWidth").val()
                , pDepth: $("#txtDepth").val() == "" ? "0" : $("#txtDepth").val()
                , pHeight: $("#txtHeight").val() == "" ? "0" : $("#txtHeight").val()
                , pLengthUnitID: $("#slLengthUnit").val() == "" ? "0" : $("#slLengthUnit").val()
                , pVolume: $("#txtVolume").val() == "" ? "0" : $("#txtVolume").val()
                , pVolumeUnitID: $("#slVolumeUnit").val() == "" ? "0" : $("#slVolumeUnit").val()
                , pIsFragile: $("#cbIsFragile").prop("checked")
                , pIsIMO: $("#cbIsIMO").prop("checked")
                , pIMOClassID: $("#slIMOClass").val() == "" ? "0" : $("#slIMOClass").val()
                , pUN: $("#txtUN").val() == "" ? "0" : $("#txtUN").val()
                , pPG: $("#slPG").val() == "" ? "0" : $("#slPG").val()
                , pPreferredAreaID: $("#slPreferredArea").val() == "" ? "0" : $("#slPreferredArea").val()
                , pByExpireDate: $("#cbByExpireDate").prop("checked")
                , pBySerialNo: $("#cbBySerialNo").prop("checked")
                , pByLotNo: $("#cbByLotNo").prop("checked")
                , pByVehicle: $("#cbByVehicle").prop("checked")
                //***** For ERP
                , pGroupID: $('#slItemGroup').val()
                , pTypeID: $('#slItemType').val()
            };
            CallGETFunctionWithParameters("/api/PurchaseItem/Insert", pParametersWithValues
                , function (pData) {
                    if (pData[0]) {
                        var pCreationYear = pData[2];
                        PurchaseItem_LoadingWithPaging();
                        if (pSaveAndNew) {
                            PurchaseItem_ClearAllControls();
                        }
                        else {
                            $("#hID").val(pData[1]);
                            $("#txtUploadFolderName").val(pCreationYear + '_' + pData[1]);
                            $("#btnSave").attr("onclick", "PurchaseItem_Update(false);");
                            $("#btnSaveAndAddNew").attr("onclick", "PurchaseItem_Update(true);");
                        }
                        swal("Success", "Saved successfully.");
                    }
                    else {
                        swal("Sorry", strUniqueFailInsertUpdateMessage);
                        FadePageCover(false);
                    }
                }
                , null);
        }
        else //if (ValidateForm("form", "PurchaseItemModal"))
            FadePageCover(false);
    }
}
function PurchaseItem_Update(pSaveAndNew) {
    debugger;
    FadePageCover(true);


    if ($("#hDefaultUnEditableCompanyName").val() != "ERP" && ($("#txtCode").val() == "" || $("#txtCode").val() == "0")) {
        swal("Sorry", "You Must Insert Code", "warning");
        FadePageCover(false);
    }
    else {
        if (ValidateForm("form", "PurchaseItemModal")) {
            pParametersWithValues = {
                pID: $("#hID").val()
                , pCode: $("#txtCode").val().trim() == "" ? "0" : $("#txtCode").val().trim().toUpperCase()
                , pName: $("#txtName").val().trim() == "" ? "0" : $("#txtName").val().trim().toUpperCase()
                , pLocalName: $("#txtLocalName").val().trim() == "" ? "0" : $("#txtLocalName").val().trim().toUpperCase()
                , pPartNumber: $("#txtPartNumber").val().trim() == "" ? "0" : $("#txtPartNumber").val().trim().toUpperCase()
                , pHSCode: $("#txtHSCode").val().trim() == "" ? "0" : $("#txtHSCode").val().trim().toUpperCase()
                , pStockUnitQuantity: $("#txtStockUnitQuantity").val().trim() == "" ? "0" : $("#txtStockUnitQuantity").val().trim().toUpperCase()
                , pPrice: $("#txtPrice").val().trim() == "" ? "0" : $("#txtPrice").val().trim().toUpperCase()
                , pCurrencyID: $("#slCurrency").val()
                , pAccountID: 0
                , pSubAccountID: 0
                , pCostCenterID: 0
                , pViewOrder: $("#txtViewOrder").val().trim() == "" ? "0" : $("#txtViewOrder").val().trim().toUpperCase()
                , pNotes: $("#txtNotes").val().trim() == "" ? "0" : $("#txtNotes").val().trim().toUpperCase()
                , pModelNumber: $("#txtModelNumber").val().trim() == "" ? "0" : $("#txtModelNumber").val().trim().toUpperCase()
                , pBrandName: $("#txtBrandName").val().trim() == "" ? "0" : $("#txtBrandName").val().trim().toUpperCase()
                , pProductType: $("#txtProductType").val().trim() == "" ? "0" : $("#txtProductType").val().trim().toUpperCase()
                //Warehouse parameters
                , pCommodityID: $("#slCommodity").val() == "" ? "0" : $("#slCommodity").val()
                , pPackageTypeID: $("#slPackageType").val() == "" ? "0" : $("#slPackageType").val()
                , pGrossWeight: $("#txtGrossWeight").val() == "" ? "0" : $("#txtGrossWeight").val()
                , pNetWeight: $("#txtNetWeight").val() == "" ? "0" : $("#txtNetWeight").val()
                , pWeightUnitID: $("#slWeightUnit").val() == "" ? "0" : $("#slWeightUnit").val()
                , pWidth: $("#txtWidth").val() == "" ? "0" : $("#txtWidth").val()
                , pDepth: $("#txtDepth").val() == "" ? "0" : $("#txtDepth").val()
                , pHeight: $("#txtHeight").val() == "" ? "0" : $("#txtHeight").val()
                , pLengthUnitID: $("#slLengthUnit").val() == "" ? "0" : $("#slLengthUnit").val()
                , pVolume: $("#txtVolume").val() == "" ? "0" : $("#txtVolume").val()
                , pVolumeUnitID: $("#slVolumeUnit").val() == "" ? "0" : $("#slVolumeUnit").val()
                , pIsFragile: $("#cbIsFragile").prop("checked")
                , pIsIMO: $("#cbIsIMO").prop("checked")
                , pIMOClassID: $("#slIMOClass").val() == "" ? "0" : $("#slIMOClass").val()
                , pUN: $("#txtUN").val() == "" ? "0" : $("#txtUN").val()
                , pPG: $("#slPG").val() == "" ? "0" : $("#slPG").val()
                , pPreferredAreaID: $("#slPreferredArea").val() == "" ? "0" : $("#slPreferredArea").val()
                , pByExpireDate: $("#cbByExpireDate").prop("checked")
                , pBySerialNo: $("#cbBySerialNo").prop("checked")
                , pByLotNo: $("#cbByLotNo").prop("checked")
                , pByVehicle: $("#cbByVehicle").prop("checked")
                //***** For ERP
                , pGroupID: $('#slItemGroup').val()
                , pTypeID: $('#slItemType').val()
            };
            CallGETFunctionWithParameters("/api/PurchaseItem/Update", pParametersWithValues
                , function (pData) {
                    if (pData[0]) {
                        var pCreationYear = pData[2];
                        PurchaseItem_LoadingWithPaging();
                        if (pSaveAndNew) {
                            PurchaseItem_ClearAllControls();
                        }
                        else {
                            $("#hID").val(pData[1]);
                            $("#txtUploadFolderName").val(pCreationYear + '_' + pData[1]);
                            $("#btnSave").attr("onclick", "PurchaseItem_Update(false);");
                            $("#btnSaveAndAddNew").attr("onclick", "PurchaseItem_Update(true);");
                        }
                        swal("Success", "Saved successfully.");
                    }
                    else {
                        swal("Sorry", strUniqueFailInsertUpdateMessage);
                        FadePageCover(false);
                    }
                }
                , null);
        }
        else //if (ValidateForm("form", "PurchaseItemModal"))
            FadePageCover(false);
    }
}
function PurchaseItem_DeleteList() {
    debugger;
    //Confirmation message to delete
    if (GetAllSelectedIDsAsString('tblPurchaseItem') != "")
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
            DeleteListFunction("/api/PurchaseItem/Delete", { "pPurchaseItemIDs": GetAllSelectedIDsAsString('tblPurchaseItem') }, function () { PurchaseItem_LoadingWithPaging(); });
        });
    //DeleteListFunction("/api/PurchaseItem/Delete", { "pPurchaseItemIDs": GetAllSelectedIDsAsString('tblPurchaseItem') }, function () { PurchaseItem_LoadingWithPaging(); });
}
function PurchaseItem_CalculateVolume() {
    debugger;
    if ($("#txtWidth").val() != "" && $("#txtDepth").val() != "" && $("#txtHeight").val() != "") {
        $("#txtVolume").val((($("#txtWidth").val() * $("#txtDepth").val() * $("#txtHeight").val()) / 1000000).toFixed(3));
    }
    else {
        $("#txtVolume").val(0);
    }
}
function PurchaseItem_EnableDisableIMOProprties(pIsEnable) {
    if (pIsEnable || $("#cbIsIMO").prop("checked")) {
        $("#txtUN").removeAttr("disabled");
        $("#slIMOClass").removeAttr("disabled");
        $("#slPG").removeAttr("disabled");
    }
    else {
        $("#txtUN").attr("disabled", "disabled");
        $("#txtUN").val("");
        $("#slIMOClass").attr("disabled", "disabled");
        $("#slIMOClass").val("");
        $("#slPG").attr("disabled", "disabled");
        $("#slPG").val("");
    }
}
//*********************************PackageTypeBarCode***************************************//
function PackageTypeBarCode_BindTableRows(pPackageTypeBarCode) {
    ClearAllTableRows("tblPackageTypeBarCode");
    //editControlsText = " class='btn btn-xs btn-rounded btn-info float-right' > <i class='fa fa-pencil' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + TranslateString("Edit") + "</span>";
    $.each(pPackageTypeBarCode, function (i, item) {

        AppendRowtoTable("tblPackageTypeBarCode",
        ("<tr ID='" + item.ID + "' " + (1 == 1 ? ("ondblclick='PackageTypeBarCode_FillControls(" + item.ID + ");'") : "") + ">"
                    + "<td class='ID'> <input " + (1 == 1 ? "name='Delete'" : "disabled='disabled'") + " type='checkbox' value='" + item.ID + "'></td>"
                    + "<td class='PackageTypeID hide'>" + item.PackageTypeID + "</td>"
                    + "<td class='PackageTypeName'>" + (item.PackageTypeName == 0 ? "" : item.PackageTypeName) + "</td>"
                    + "<td class='BarCode'>" + (item.BarCode == 0 ? "" : item.BarCode) + "</td>"
                    + "<td class='hide'><a href='#PackageTypeBarCodeModal' data-toggle='modal' onclick='PackageTypeBarCode_FillControls(" + item.ID + ");' " + editControlsText + "</a></td></tr>"));
    });
    ////ApplyPermissions();
    //if (ODPay && $("#hIsOperationDisabled").val() == false) $("#btn-DeletePayable").removeClass("hide"); else $("#btn-DeletePayable").addClass("hide");
    BindAllCheckboxonTable("tblPackageTypeBarCode", "ID", "cb-CheckAll-PackageTypeBarCode");
    CheckAllCheckbox("HeaderDeletePackageTypeBarCodeID");
    if (showDeleteFailMessage) {//sherif: if deletion failed due to foreign key, then this message is shown
        swal(strSorry, strDeleteFailMessage, "warning");
        showDeleteFailMessage = false;
    }
}
function PackageTypeBarCode_ClearAllControls() {
    debugger;
    if ($("#hID").val() == "")
        swal("Sorry", "Please, save header first.");
    else {
        ClearAll("#PackageTypeBarCodeModal");
        $("#btnSavePackageTypeBarCode").attr("onclick", "PackageTypeBarCode_Save(false);");
        $("#btnSaveAndAddNewPackageTypeBarCode").attr("onclick", "PackageTypeBarCode_Save(true);");
        jQuery("#PackageTypeBarCodeModal").modal("show");
    }
}
function PackageTypeBarCode_FillControls(pID) {
    debugger;
    ClearAll("#PackageTypeBarCodeModal");
    $("#hPackageTypeBarCodeID").val(pID);
    var tr = $("#tblPackageTypeBarCode tr[ID='" + pID + "']");
    $("#slPackageTypeBarCode").val($(tr).find("td.PackageTypeID").text());
    $("#txtBarCode").val($(tr).find("td.BarCode").text());
    jQuery("#PackageTypeBarCodeModal").modal("show");
}
function PackageTypeBarCode_Save(pSaveAndNew) {
    debugger;
    FadePageCover(true);
    if (ValidateForm("form", "PackageTypeBarCodeModal")) {
        var pParametersWithValues = {
            pPackageTypeBarCodeID: $("#hPackageTypeBarCodeID").val() == "" ? 0 : $("#hPackageTypeBarCodeID").val()
            , pPurchaseItemID: $("#hID").val()
            , pPackageTypeID: $("#slPackageTypeBarCode").val() == "" ? 0 : $("#slPackageTypeBarCode").val()
            , pBarCode: $("#txtBarCode").val().trim() == "" ? 0 : $("#txtBarCode").val().trim()
        };
        CallGETFunctionWithParameters("/api/PurchaseItem/PackageTypeBarCode_Save", pParametersWithValues
            , function (pData) {
                if (pData[0]) {
                    PackageTypeBarCode_BindTableRows(JSON.parse(pData[1]));
                    if (pSaveAndNew)
                        ClearAll("#PackageTypeBarCodeModal");
                    else
                        jQuery("#PackageTypeBarCodeModal").modal("hide");
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
function PackageTypeBarCode_DeleteList() {
    debugger;
    //Confirmation message to delete
    var pPackageTypeBarCodeIDsToDelete = GetAllSelectedIDsAsString('tblPackageTypeBarCode');
    if (pPackageTypeBarCodeIDsToDelete != "")
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
            CallGETFunctionWithParameters("/api/PurchaseItem/PackageTypeBarCode_Delete"
                , { pPackageTypeBarCodeIDsToDelete: pPackageTypeBarCodeIDsToDelete, pPurchaseItemID: $("#hID").val() }
                , function (pData) {
                    if (pData[0]) {
                        PackageTypeBarCode_BindTableRows(JSON.parse(pData[1]));
                    }
                    else
                        swal("Sorry", strDeleteFailMessage);
                    FadePageCover(false);
                });
        });
}
//*********************************EOF PackageTypeBarCode***************************************//

//*********************************PackageTypeConversion***************************************//
function PackageTypeConversion_BindTableRows(pPackageTypeConversion) {
    ClearAllTableRows("tblPackageTypeConversion");
    //editControlsText = " class='btn btn-xs btn-rounded btn-info float-right' > <i class='fa fa-pencil' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + TranslateString("Edit") + "</span>";
    $.each(pPackageTypeConversion, function (i, item) {

        AppendRowtoTable("tblPackageTypeConversion",
        ("<tr ID='" + item.ID + "' " + (1 == 1 ? ("ondblclick='PackageTypeConversion_FillControls(" + item.ID + ");'") : "") + ">"
                    + "<td class='ID'> <input " + (1 == 1 ? "name='Delete'" : "disabled='disabled'") + " type='checkbox' value='" + item.ID + "'></td>"
                    + "<td class='FromPackageTypeID hide'>" + item.FromPackageTypeID + "</td>"
                    + "<td class='FromPackageTypeName'>" + (item.FromPackageTypeName == 0 ? "" : item.FromPackageTypeName) + "</td>"
                    + "<td class='ToPackageTypeID hide'>" + item.ToPackageTypeID + "</td>"
                    + "<td class='ToPackageTypeName'>" + (item.ToPackageTypeName == 0 ? "" : item.ToPackageTypeName) + "</td>"
                    + "<td class='Factor'>" + (item.Factor == 0 ? "" : item.Factor) + "</td>"
                    + "<td class='hide'><a href='#PackageTypeConversionModal' data-toggle='modal' onclick='PackageTypeConversion_FillControls(" + item.ID + ");' " + editControlsText + "</a></td></tr>"));
    });
    ////ApplyPermissions();
    //if (ODPay && $("#hIsOperationDisabled").val() == false) $("#btn-DeletePayable").removeClass("hide"); else $("#btn-DeletePayable").addClass("hide");
    BindAllCheckboxonTable("tblPackageTypeConversion", "ID", "cb-CheckAll-PackageTypeConversion");
    CheckAllCheckbox("HeaderDeletePackageTypeConversionID");
    if (showDeleteFailMessage) {//sherif: if deletion failed due to foreign key, then this message is shown
        swal(strSorry, strDeleteFailMessage, "warning");
        showDeleteFailMessage = false;
    }
}
function PackageTypeConversion_ClearAllControls() {
    debugger;
    if ($("#hID").val() == "")
        swal("Sorry", "Please, save header first.");
    else {
        ClearAll("#PackageTypeConversionModal");
        $("#btnSavePackageTypeConversion").attr("onclick", "PackageTypeConversion_Save(false);");
        $("#btnSaveAndAddNewPackageTypeConversion").attr("onclick", "PackageTypeConversion_Save(true);");
        jQuery("#PackageTypeConversionModal").modal("show");
    }
}
function PackageTypeConversion_FillControls(pID) {
    debugger;
    ClearAll("#PackageTypeConversionModal");
    $("#hPackageTypeConversionID").val(pID);
    var tr = $("#tblPackageTypeConversion tr[ID='" + pID + "']");
    $("#slFromPackageType").val($(tr).find("td.FromPackageTypeID").text());
    $("#slToPackageType").val($(tr).find("td.ToPackageTypeID").text());
    $("#txtFactor").val($(tr).find("td.Factor").text());
    jQuery("#PackageTypeConversionModal").modal("show");
}
function PackageTypeConversion_Save(pSaveAndNew) {
    debugger;
    FadePageCover(true);
    if (parseFloat($("#txtFactor").val().trim()) == 0) {
        swal("Sorry", "Factor can not be 0.");
        FadePageCover(false);
    }
    else if ($("#slFromPackageType").val() == $("#slToPackageType").val()) {
        swal("Sorry", "Stock units can not be the same.");
        FadePageCover(false);
    }
    else if (ValidateForm("form", "PackageTypeConversionModal")) {
        var pParametersWithValues = {
            pPackageTypeConversionID: $("#hPackageTypeConversionID").val() == "" ? 0 : $("#hPackageTypeConversionID").val()
            , pPurchaseItemID: $("#hID").val()
            , pFromPackageTypeID: $("#slFromPackageType").val() == "" ? 0 : $("#slFromPackageType").val()
            , pToPackageTypeID: $("#slToPackageType").val() == "" ? 0 : $("#slToPackageType").val()
            , pFactor: $("#txtFactor").val().trim() == "" ? 0 : $("#txtFactor").val().trim()
        };
        CallGETFunctionWithParameters("/api/PurchaseItem/PackageTypeConversion_Save", pParametersWithValues
            , function (pData) {
                if (pData[0]) {
                    PackageTypeConversion_BindTableRows(JSON.parse(pData[1]));
                    if (pSaveAndNew)
                        ClearAll("#PackageTypeConversionModal");
                    else
                        jQuery("#PackageTypeConversionModal").modal("hide");
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
function PackageTypeConversion_DeleteList() {
    debugger;
    //Confirmation message to delete
    var pPackageTypeConversionIDsToDelete = GetAllSelectedIDsAsString('tblPackageTypeConversion');
    if (pPackageTypeConversionIDsToDelete != "")
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
            CallGETFunctionWithParameters("/api/PurchaseItem/PackageTypeConversion_Delete"
                , { pPackageTypeConversionIDsToDelete: pPackageTypeConversionIDsToDelete, pPurchaseItemID: $("#hID").val() }
                , function (pData) {
                    if (pData[0]) {
                        PackageTypeConversion_BindTableRows(JSON.parse(pData[1]));
                    }
                    else
                        swal("Sorry", strDeleteFailMessage);
                    FadePageCover(false);
                });
        });
}
//*********************************EOF PackageTypeConversion***************************************//
//*********************************Reading Excel Files***************************************//
//Must be saved as Excel 97-2003
function onFileSelected(event) {
    debugger;
    var selectedFile = event.target.files[0];
    if (selectedFile != undefined) { //to handle the case of not choosing a file
        var sFilename = selectedFile.name;
        // Create A File Reader HTML5
        var reader = new FileReader();
        var result = document.getElementById("hExtractedData");
        reader.onload = function (e) {
            var data = e.target.result;
            $("#btnAddFromExcel").val(""); ///////////////////////////////////
            var cfb = XLS.CFB.read(data, { type: 'binary' });
            var wb = XLS.parse_xlscfb(cfb);
            // Loop Over Each Sheet
            wb.SheetNames.forEach(function (sheetName) {
                //// Obtain The Current Row As CSV
                //var sCSV = XLS.utils.make_csv(wb.Sheets[sheetName]);
                var oJS = XLS.utils.sheet_to_row_object_array(wb.Sheets[sheetName]);
                //$("#hExtractedData").val(sCSV);
                if (oJS.length > 0 && oJS[0].Code != undefined && oJS[0].Name != undefined) //if (sCSV != "")
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
    var pCodeList = "";
    var pNameList = "";
    var pPartNumberList = "";
    var pHSCodeList = "";
    var pPriceList = "";
    var pGrossWeightList = "";
    var pNetWeightList = "";
    var pWidthList = "";
    var pDepthList = "";
    var pHeightList = "";
    var pVolumeList = "";
    var pModelNumberList = "";
    var pBrandNameList = "";
    var pProductTypeList = "";
    for (var i = 0; i < pDataRows.length; i++) { //replace '\', ' ', ',' with space
        pCodeList += (pCodeList == "" ? (pDataRows[i].Code == undefined || pDataRows[i].Code.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].Code.replace(/[\, ]/g, ' ').toUpperCase().trim()) : ("," + (pDataRows[i].Code == undefined || pDataRows[i].Code.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].Code.replace(/[\, ]/g, ' ').toUpperCase().trim())));
        pNameList += (pNameList == "" ? (pDataRows[i].Name == undefined || pDataRows[i].Name.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].Name.replace(/[\, ]/g, ' ').toUpperCase().trim()) : ("," + (pDataRows[i].Name == undefined || pDataRows[i].Name.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].Name.replace(/[\, ]/g, ' ').toUpperCase().trim())));
        pPartNumberList += (pPartNumberList == "" ? (pDataRows[i].PartNumber == undefined || pDataRows[i].PartNumber.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].PartNumber.replace(/[\, ]/g, ' ').toUpperCase().trim()) : ("," + (pDataRows[i].PartNumber == undefined || pDataRows[i].PartNumber.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].PartNumber.replace(/[\, ]/g, ' ').toUpperCase().trim())));
        pHSCodeList += (pHSCodeList == "" ? (pDataRows[i].HSCode == undefined || pDataRows[i].HSCode.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].HSCode.replace(/[\, ]/g, ' ').toUpperCase().trim()) : ("," + (pDataRows[i].HSCode == undefined || pDataRows[i].HSCode.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].HSCode.replace(/[\, ]/g, ' ').toUpperCase().trim())));
        pPriceList += (pPriceList == "" ? (pDataRows[i].Price == undefined || pDataRows[i].Price.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].Price.replace(/[\, ]/g, ' ').toUpperCase().trim()) : ("," + (pDataRows[i].Price == undefined || pDataRows[i].Price.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].Price.replace(/[\, ]/g, ' ').toUpperCase().trim())));
        pGrossWeightList += (pGrossWeightList == "" ? (pDataRows[i].GrossWeight == undefined || pDataRows[i].GrossWeight.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].GrossWeight.replace(/[\, ]/g, ' ').toUpperCase().trim()) : ("," + (pDataRows[i].GrossWeight == undefined || pDataRows[i].GrossWeight.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].GrossWeight.replace(/[\, ]/g, ' ').toUpperCase().trim())));
        pNetWeightList += (pNetWeightList == "" ? (pDataRows[i].NetWeight == undefined || pDataRows[i].NetWeight.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].NetWeight.replace(/[\, ]/g, ' ').toUpperCase().trim()) : ("," + (pDataRows[i].NetWeight == undefined || pDataRows[i].NetWeight.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].NetWeight.replace(/[\, ]/g, ' ').toUpperCase().trim())));
        pWidthList += (pWidthList == "" ? (pDataRows[i].Width == undefined || pDataRows[i].Width.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].Width.replace(/[\, ]/g, ' ').toUpperCase().trim()) : ("," + (pDataRows[i].Width == undefined || pDataRows[i].Width.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].Width.replace(/[\, ]/g, ' ').toUpperCase().trim())));
        pDepthList += (pDepthList == "" ? (pDataRows[i].Depth == undefined || pDataRows[i].Depth.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].Depth.replace(/[\, ]/g, ' ').toUpperCase().trim()) : ("," + (pDataRows[i].Depth == undefined || pDataRows[i].Depth.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].Depth.replace(/[\, ]/g, ' ').toUpperCase().trim())));
        pHeightList += (pHeightList == "" ? (pDataRows[i].Height == undefined || pDataRows[i].Height.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].Height.replace(/[\, ]/g, ' ').toUpperCase().trim()) : ("," + (pDataRows[i].Height == undefined || pDataRows[i].Height.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].Height.replace(/[\, ]/g, ' ').toUpperCase().trim())));
        pVolumeList += (pVolumeList == "" ? (pDataRows[i].Volume == undefined || pDataRows[i].Volume.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].Volume.replace(/[\, ]/g, ' ').toUpperCase().trim()) : ("," + (pDataRows[i].Volume == undefined || pDataRows[i].Volume.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].Volume.replace(/[\, ]/g, ' ').toUpperCase().trim())));
        pModelNumberList += (pModelNumberList == "" ? (pDataRows[i].ModelNumber == undefined || pDataRows[i].ModelNumber.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].ModelNumber.replace(/[\, ]/g, ' ').toUpperCase().trim()) : ("," + (pDataRows[i].ModelNumber == undefined || pDataRows[i].ModelNumber.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].ModelNumber.replace(/[\, ]/g, ' ').toUpperCase().trim())));
        pBrandNameList += (pBrandNameList == "" ? (pDataRows[i].BrandName == undefined || pDataRows[i].BrandName.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].BrandName.replace(/[\, ]/g, ' ').toUpperCase().trim()) : ("," + (pDataRows[i].BrandName == undefined || pDataRows[i].BrandName.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].BrandName.replace(/[\, ]/g, ' ').toUpperCase().trim())));
        pProductTypeList += (pProductTypeList == "" ? (pDataRows[i].ProductType == undefined || pDataRows[i].ProductType.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].ProductType.replace(/[\, ]/g, ' ').toUpperCase().trim()) : ("," + (pDataRows[i].ProductType == undefined || pDataRows[i].ProductType.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].ProductType.replace(/[\, ]/g, ' ').toUpperCase().trim())));
    }
    var pParametersWithValues = {
        pCodeList: pCodeList
        , pNameList: pNameList
        , pPartNumberList: pPartNumberList
        , pHSCodeList: pHSCodeList
        , pPriceList: pPriceList
        , pGrossWeightList: pGrossWeightList
        , pNetWeightList: pNetWeightList
        , pWidthList: pWidthList
        , pDepthList: pDepthList
        , pHeightList: pHeightList
        , pVolumeList: pVolumeList
        , pModelNumberList: pModelNumberList
        , pBrandNameList: pBrandNameList
        , pProductTypeList: pProductTypeList
    };
    CallPOSTFunctionWithParameters("/api/PurchaseItem/InsertList", pParametersWithValues
        , function (pData) {
            var _ReturnedMessage = pData[0];
            if (_ReturnedMessage == "") {
                swal("Success", "Saved Successfully.");
                PurchaseItem_LoadingWithPaging();
            }
            else {
                swal("Sorry", _ReturnedMessage);
                FadePageCover(false);
            }
        }
        , null);
    $("#btnAddFromExcel").val(""); //if removed the last selected file remains till unselected
}
//******************************EOF Reading Excel Files***************************************//

//*********************************Uploaded Docs***************************************//
function DocsIn_FillControls() {
    debugger;
    if ($("#txtUploadFolderName").val() == "")
        swal("Sorry", "Please, save header first.");
    else {
        var pStrFolderPath = "";
        constProductDocsInFilesPath = "/DocsInFiles//PurchaseItem//";
        pStrFolderPath = "~/DocsInFiles/PurchaseItem/";
        jQuery("#DocsInModal").modal("show");
        //CallGETFunctionWithParameters(pFunctionName, pParametersWithValues, callback, callback1)
        CallGETFunctionWithParameters("/api/PurchaseItem/LoadFiles"
            , { pFolderName: $("#txtUploadFolderName").val(), pStrFolderPath: pStrFolderPath }
            , function (data) {
                DocsIn_BindTableRows(JSON.parse(data[0]));
            }
            , null);
    }
}
function DocsIn_BindTableRows(pDocsInFileNames) {
    debugger;
    ClearAllTableRows("tblDocsIn");
    if (pDocsInFileNames != null) {
        //var editControlsText = " class='btn btn-xs btn-rounded btn-info float-right' > <i class='fa fa-pencil' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + TranslateString("Edit") + "</span>";
        var downloadControlsText = " class='btn btn-xs btn-rounded btn-primary float-right' > <i class='fa fa-cloud-download' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + "Download" + "</span>";
        var openControlsText = " class='btn btn-xs btn-rounded btn-info float-right' > <i class='fa fa-folder-open' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + "Open" + "</span>";
        var emailControlsText = " class='btn btn-xs btn-rounded btn-primary float-right' > <i class='fa fa-envelope-o' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + "Mail" + "</span>";
        for (i = 0; i < pDocsInFileNames.length; i++) {
            AppendRowtoTable("tblDocsIn",
            //("<tr ID='" + item.ID + "' ondblclick='DocumentTypes_EditByDblClick(" + item.ID + ");'>"
            ("<tr ID='" + i + "'>"
                + "<td class='DocsInID'> <input " + (1 == 1 ? "name='Delete'" : "disabled='disabled'") + " type='checkbox' value='" + pDocsInFileNames[i] + "'></td>"
                + "<td class='DocsInSerial'>" + (parseInt(i) + 1) + "</td>"
                + "<td class='FileName'>" + pDocsInFileNames[i] + "</td>"
                //+ "<td class=''><a onclick='DocsIn_OpenUploadedFile(" + '"' + pDocsInFileNames[i] + '","' + $("#txtUploadFolderName").val() + '"' + ");' " + openControlsText + "</a><a onclick='SaveFile(" + '"' + pDocsInFileNames[i] + '","' + $("#txtUploadFolderName").val() + '"' + ");' " + downloadControlsText + "</a>&nbsp;&nbsp;&nbsp;</td>"
                + "<td class=''><a onclick='DocsIn_OpenUploadedFile(" + '"' + pDocsInFileNames[i] + '","' + $("#txtUploadFolderName").val() + '"' + ");' " + openControlsText + "</a>&nbsp;&nbsp;&nbsp;</td>"
                //+ ($("#hIsOperationDisabled").val() == false
                //? ("<td class=''><a href='#DocumentTypeModal' data-toggle='modal' onclick='DocsOut_Print(" + item.ID + ");' " + printControlsText + "</a><a onclick='DocsOut_SendEmail(" + item.ID + ", function(){window.onbeforeunload = confirmExit;});' " + emailControlsText + "</a>&nbsp;&nbsp;&nbsp;</td>")
                //: "<td></td>")
                + "</tr>"));
        }
    }
    //ApplyPermissions();
    //if (OADocIn && $("#hIsOperationDisabled").val() == false) { $("#inputFileUpload").removeClass("hide"); $("#divUpload").removeClass("hide"); } else { $("#inputFileUpload").addClass("hide"); $("#divUpload").addClass("hide"); }
    //if (ODDocIn && $("#hIsOperationDisabled").val() == false) { $("#btn-DeleteDocsIn").removeClass("hide"); } else { $("#btn-DeleteDocsIn").addClass("hide"); }
    BindAllCheckboxonTable("tblDocsIn", "DocsInID", "cb-CheckAll-DocsIn");
    CheckAllCheckbox("HeaderDeleteDocsInID");
}
// Asynchronous file upload process
function DocsIn_UploadFile() {
    debugger;
    constProductDocsInFilesPath = "/DocsInFiles//PurchaseItem//";
    //maxTotalSize = 10485760;//10MB total of uploaded files together
    maxTotalSize = 20971520;//20MB total of uploaded files together
    var formData = new FormData();
    var files = $("#inputFileUpload").get(0).files;
    var totalFilesSize = 0;
    if (files.length > 0) {
        //check files total size is less than 20MB
        for (i = 0; i < files.length; i++)
            totalFilesSize += files[i].size;
        if (totalFilesSize > maxTotalSize)
            swal(strSorry, "Total file(s) size can't exceed 20MBs at one upload.");
        else {
            // Add the uploaded files content to the form data collection
            if (files.length > 0) {
                FadePageCover(true);
                for (i = 0; i < files.length; i++)
                    formData.append("FileNames", files[i]);
            }
            formData.append("pFolderName", $("#txtUploadFolderName").val())
            formData.append("pStrFolderPath", "~/DocsInFiles/PurchaseItem/")
            // Make Ajax request with the contentType = false, and processDate = false
            var ajaxRequest = $.ajax({
                type: "POST",
                xhr: function () {  // Custom XMLHttpRequest
                    var myXhr = $.ajaxSettings.xhr();
                    if (myXhr.upload) { // Check if upload property exists
                        myXhr.upload.addEventListener('progress', progressHandlingFunction, false); // For handling the progress of the upload
                    }
                    return myXhr;
                },
                url: "/api/PurchaseItem/UploadFile",
                contentType: false,
                processData: false,
                data: formData,
                success: function (data) { //data[0]: The filenames returned
                    DocsIn_BindTableRows(JSON.parse(data[0]));
                    FadePageCover(false);
                    swal("Success", "File(s) uploaded successfully.");
                },
                error: function (jqXHR, exception) {
                    FadePageCover(false);
                    swal(strSorry, "An error occured, please try again.");
                }
            });
            ajaxRequest.done(function (xhr, textStatus) {
                // Do other operation
                debugger;
            });
        }//of else (correct file sizes)
    }//of if (files.length == 0)
}
function DocsIn_DeleteList() {
    debugger;
    var pStrFolderPath = "";
    constProductDocsInFilesPath = "/DocsInFiles//PurchaseItem//";
    pStrFolderPath = "~/DocsInFiles/PurchaseItem/";
    //Confirmation message to delete
    var pFileNames = GetAllSelectedIDsAsString('tblDocsIn', 'Delete');
    if (pFileNames != "")
        swal({
            title: "Are you sure?",
            text: "The selected files will be removed from server!",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, remove!",
            closeOnConfirm: true
        },
        //callback function in case of confirm delete
        function () {
            FadePageCover(true);
            //CallGETFunctionWithParameters(pFunctionName, pParametersWithValues, callback, callback1)
            CallGETFunctionWithParameters("/api/PurchaseItem/DeleteUploadedFile"
            //CallGETFunctionWithParameters("/api/DocsIn/Delete"
                , { "pFolderName": $("#txtUploadFolderName").val(), "pFileNames": pFileNames, "pStrFolderPath": pStrFolderPath }
                , function (data) { //data[0]: pDocsInFileNames
                    DocsIn_BindTableRows(JSON.parse(data[0]));
                    FadePageCover(false);
                }
                , null);
        });
}
function DocsIn_OpenUploadedFile(pFileName, pFolderName) {
    debugger;
    window.open(constProductDocsInFilesPath + pFolderName + "\\" + pFileName, '_blank');
    //var myWindow = window.open("", "_blank");
    //myWindow.document.write("<p>I replaced the current window.</p>");
}
//function progressHandlingFunction(e) {
//    if (e.lengthComputable) {
//        $('progress').attr({ value: e.loaded, max: e.total });
//    }
//}
//*********************************EOF Uploaded Docs***************************************//
$('input[type=checkbox][name=cbByExpireDate]').change(function () {
    debugger;
    if ($(this).prop('checked') == true) {
        $("#cbBySerialNo").prop("checked", false);
        $("#cbByVehicle").prop("checked", false);
    }
});
$('input[type=checkbox][name=cbByLotNo]').change(function () {
    debugger;
    if ($(this).prop('checked') == true) {
        $("#cbBySerialNo").prop("checked", false);
        $("#cbByVehicle").prop("checked", false);
    }
});
$('input[type=checkbox][name=cbBySerialNo]').change(function () {
    debugger;
    if ($(this).prop('checked') == true) {
        $("#cbByExpireDate").prop("checked", false);
        $("#cbByLotNo").prop("checked", false);
        $("#cbByVehicle").prop("checked", false);
    }
});
$('input[type=checkbox][name=cbByVehicle]').change(function () {
    debugger;
    if ($(this).prop('checked') == true) {
        $("#cbByExpireDate").prop("checked", false);
        $("#cbByLotNo").prop("checked", false);
        $("#cbBySerialNo").prop("checked", false);
    }
});