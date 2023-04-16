//this file is for both Packages and containers depending wether FCL or LCL, .......

//to bind enter key when focus on $('#txtSearch') to $("#btnsearch").click();
$('#txtOperationContainerAndPackagesSearch').on('keypress', function (args) {
    if (args.keyCode == 13) {
        $("#btn-OperationsContainerAndPackagesSearch").click();
        return false;
    }
});
function OperationContainersAndPackages_SubmenuTabClicked() {
    debugger;
    if ($("#tblOperationContainersAndPackages tbody tr").length == 0) {
        FadePageCover(true);
        CallGETFunctionWithParameters("/api/OperationContainersAndPackages/LoadWithWhereClause",
            {
                pPageNumber: 1
                , pPageSize: 999999
                , pWhereClause: "WHERE OperationID=" + ($("#hOperationID").val() == "" ? 0 : $("#hOperationID").val())
                , pOrderBy: "ID"
            }
            , function (pData) { OperationContainersAndPackages_BindTableRows(JSON.parse(pData[0])); FadePageCover(false); }
            , null);
        //GetListWithOpCodeAndHouseNoAndClientEmailAttr(null, "/api/Operations/LoadWithParameters", null, "slTransferContainerToOperation"
        //    , { pPageNumber: 1, pPageSize: 99999, pWhereClause: "WHERE (Code IS NOT NULL OR MasterOperationID IS NOT NULL) AND ShipmentType IN (1,3,5) AND OperationStageID NOT IN (" + ClosedQuoteAndOperStageID + "," + CancelledQuoteAndOperStageID + ") AND CreationDate > DATEADD(mm,DATEDIFF(mm,0,GETDATE())-12,0)", pOrderBy: "ID DESC" }
        //    , null);
        CallGETFunctionWithParameters("/api/Operations/LoadOperationsToRestoreInvoices"
            , { pPageSize: 99999, pWhereClauseToGetOperationsToRestoreInvoices: "WHERE EffectiveOperationCode IS NOT NULL AND ShipmentType IN (1,3,5) AND BLType <> 2 AND OperationStageID NOT IN (" + ClosedQuoteAndOperStageID + "," + CancelledQuoteAndOperStageID + ") AND CreationDate > DATEADD(mm,DATEDIFF(mm,0,GETDATE())-12,0)", pOrderBy: "ID DESC" }
            , function (pData) {
                FillListFromObject(null, 13, "<--Select-->", "slTransferContainerToOperation", pData[0], null);
            }
            , null);
    }
    //if ($("#cbIsFCL").prop('checked'))
    //    $('#btn-SendAlarm').removeClass('hide');
    //else
    //    $('#btn-SendAlarm').addClass('hide');

}
function OperationContainersAndPackages_DownloadLog() {
    debugger;

}
function OperationContainersAndPackages_BindTableRows(pOperationContainersAndPackages) {
    debugger;
    $("#txtPackagesContainerOrRoadNumber").val("");
    ClearAllTableRows("tblOperationContainersAndPackages");
    var downloadControlsText = " class='btn btn-xs btn-rounded btn-primary float-right' > <i class='fa fa-cloud-download' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + "Download Log" + "</span>";

    ShowHidettblOperationContainersAndPackagesHeaders();

    var transferControlsText = " class='btn btn-xs btn-rounded btn-info float-right' > <i class='fa fa-pencil' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + TranslateString("Transfer") + "</span>";
    $.each(pOperationContainersAndPackages, function (i, item) {
        AppendRowtoTable("tblOperationContainersAndPackages",
            ("<tr ID='" + item.ID + "' " + (OEPac && $("#hIsOperationDisabled").val() == false ? ("ondblclick='OperationContainersAndPackages_EditByDblClick(" + item.ID + ");'") : "") + ">"
                + "<td class='ID'> <input name='Delete' type='checkbox' value='" + item.ID + "' /></td>"
                + ($("#hBLType").val() == constMasterBLType && $("#hShipmentType").val() != constConsolidationShipmentType ? "<td class='OperationConatainersAndPackagesHouseOperationCode'>" + (item.HouseOperationCode == 0 ? "" : item.HouseOperationCode) + "</td>" : "")
                + ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked') ? ("<td class='Container' val='" + item.ContainerTypeID + "'>" + item.ContainerTypeCode + "</td>") : "")
                //+ ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked') ? ("<td class='ContainerNumber'>" + (item.ContainerNumber == 0 ? "" : item.ContainerNumber) + "</td>") : "")
                + "<td class='ContainerNumber " + ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked') ? "" : "hide ") + "'>" + (item.ContainerNumber == 0 ? "" : item.ContainerNumber) + "</td>"
                + "<td class='TankOrFlexiNumber " + (/*$("#cbIsFlexi").prop('checked') ||*/ $("#cbIsTank").prop('checked') ? "" : "hide ") + "'>" + (item.TankOrFlexiNumber == 0 ? "" : item.TankOrFlexiNumber) + "</td>"
                + ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked') ? ("<td class='CarrierSeal'>" + (item.CarrierSeal == 0 ? "" : item.CarrierSeal) + "</td>") : "")
                + ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked') ? ("<td class='ShipperSeal hide'>" + (item.ShipperSeal == 0 ? "" : item.ShipperSeal) + "</td>") : "")
                + ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked') ? ("<td class='TareWeight'>" + (item.TareWeight == 0 ? "" : item.TareWeight) + "</td>") : "")
                + ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked') ? ("<td class='ContainerVolume'>" + (item.Volume == 0 ? "" : item.Volume) + "</td>") : "")
                + ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked') ? ("<td class='ContainerNetWeight'>" + (item.NetWeight == 0 ? "" : item.NetWeight) + "</td>") : "")
                + ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked') ? ("<td class='ContainerNetWeightTON'>" + (item.NetWeightTON == 0 ? "" : item.NetWeightTON) + "</td>") : "")
                + ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked') ? ("<td class='ContainerGrossWeight'>" + (item.GrossWeight == 0 ? "" : item.GrossWeight) + "</td>") : "")
                + ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked') ? ("<td class='ContainerGrossWeightTON'>" + (item.GrossWeightTON == 0 ? "" : item.GrossWeightTON) + "</td>") : "")
                + ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked') ? ("<td class='VGM'>" + (item.VGM == 0 ? "" : item.VGM) + "</td>") : "")
                + ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked') ? ("<td class='IsReefer hide'> <input type='checkbox' disabled='disabled' val='" + (item.IsReefer == true ? "true' checked='checked'" : "'") + " /></td>") : "")
                + ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked') ? ("<td class='IsNOR hide'> <input type='checkbox' disabled='disabled' val='" + (item.IsNOR == true ? "true' checked='checked'" : "'") + " /></td>") : "")
                + ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked') ? ("<td class='IsSentToWarehouse hide'> <input type='checkbox' disabled='disabled' val='" + (item.IsSentToWarehouse == true ? "true' checked='checked'" : "'") + " /></td>") : "")
                + ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked') ? ("<td class='IsLoaded hide'> <input type='checkbox' disabled='disabled' val='" + (item.IsLoaded == true ? "true' checked='checked'" : "'") + " /></td>") : "")
                + ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked') ? ("<td class='MinTemp hide'>" + item.MinTemp + "</td>") : "")
                + ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked') ? ("<td class='Humidity hide'>" + item.Humidity + "</td>") : "")
                + ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked') ? ("<td class='Ventilation hide'>" + item.Ventilation + "</td>") : "")

                + ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked') ? ("<td class='PackageTypeOnContainer hide' val='" + item.PackageTypeIDOnContainer + "'>" + (item.PackageTypeNameOnContainer == 0 ? "" : item.PackageTypeNameOnContainer) + "</td>") : "")
                + ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked') ? ("<td class='NumberOfPackagesOnContainer hide'>" + item.NumberOfPackagesOnContainer + "</td>") : "")
                + ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked') ? ("<td class='LotNumber hide'>" + (item.LotNumber == 0 ? "" : item.LotNumber) + "</td>") : "")

                + ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked') ? ("<td class='MaxTemp hide'>" + item.MaxTemp + "</td>") : "")
                + ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked') ? ("<td class='IsIMO hide'> <input type='checkbox' disabled='disabled' val='" + (item.IsIMO == true ? "true' checked='checked'" : "'") + " /></td>") : "")
                + ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked') ? ("<td class='IMOClass hide'>" + item.IMOClass + "</td>") : "")
                + ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked') ? ("<td class='UNNumber hide'>" + item.UNNumber + "</td>") : "")
                + ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked') ? ("<td class='FlashPoint hide'>" + item.FlashPoint + "</td>") : "")
                //+ ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked') ? ("<td class='DescriptionOfGoods hide'>" + (item.DescriptionOfGoods == 0 ? "" : item.DescriptionOfGoods) + "</td>") : "")
                + "<td class='DescriptionOfGoods hide'>" + (item.DescriptionOfGoods == 0 ? "" : item.DescriptionOfGoods) + "</td>"
                + "<td class='OperatorID hide'>" + item.OperatorID + "</td>"

                + ($("#cbIsLCL").prop('checked') || $("#cbIsBulk").prop('checked') || $("#cbIsLTL").prop('checked') || $("#cbIsAir").prop('checked') ? ("<td class='Package' val='" + item.PackageTypeID + "'>" + (item.PackageTypeName == 0 ? "" : item.PackageTypeName) + "</td>") : "")
                + ($("#cbIsLCL").prop('checked') || $("#cbIsBulk").prop('checked') || $("#cbIsLTL").prop('checked') || $("#cbIsAir").prop('checked') ? ("<td class='Quantity'>" + item.Quantity + "</td>") : "")
                + ($("#cbIsLCL").prop('checked') || $("#cbIsBulk").prop('checked') || $("#cbIsLTL").prop('checked') || $("#cbIsAir").prop('checked') ? ("<td class='Length'>" + item.Length + "</td>") : "")
                + ($("#cbIsLCL").prop('checked') || $("#cbIsBulk").prop('checked') || $("#cbIsLTL").prop('checked') || $("#cbIsAir").prop('checked') ? ("<td class='Width'>" + item.Width + "</td>") : "")
                + ($("#cbIsLCL").prop('checked') || $("#cbIsBulk").prop('checked') || $("#cbIsLTL").prop('checked') || $("#cbIsAir").prop('checked') ? ("<td class='Height'>" + item.Height + "</td>") : "")
                + ($("#cbIsLCL").prop('checked') || $("#cbIsBulk").prop('checked') || $("#cbIsLTL").prop('checked') || $("#cbIsAir").prop('checked') ? ("<td class='Volume'>" + item.Volume + "</td>") : "")
                + ($("#cbIsLCL").prop('checked') || $("#cbIsBulk").prop('checked') || $("#cbIsLTL").prop('checked') || $("#cbIsAir").prop('checked') ? ("<td class='VolumetricWeight'>" + item.VolumetricWeight + "</td>") : "")
                + ($("#cbIsLCL").prop('checked') || $("#cbIsBulk").prop('checked') || $("#cbIsLTL").prop('checked') || $("#cbIsAir").prop('checked') ? ("<td class='NetWeight'>" + item.NetWeight + "</td>") : "")
                + ($("#cbIsLCL").prop('checked') || $("#cbIsBulk").prop('checked') || $("#cbIsLTL").prop('checked') || $("#cbIsAir").prop('checked') ? ("<td class='NetWeightTON'>" + item.NetWeightTON + "</td>") : "")
                + ($("#cbIsLCL").prop('checked') || $("#cbIsBulk").prop('checked') || $("#cbIsLTL").prop('checked') || $("#cbIsAir").prop('checked') ? ("<td class='GrossWeight'>" + item.GrossWeight + "</td>") : "")
                + ($("#cbIsLCL").prop('checked') || $("#cbIsBulk").prop('checked') || $("#cbIsLTL").prop('checked') || $("#cbIsAir").prop('checked') ? ("<td class='GrossWeightTON'>" + item.GrossWeightTON + "</td>") : "")
                + ($("#cbIsLCL").prop('checked') || $("#cbIsBulk").prop('checked') || $("#cbIsLTL").prop('checked') || $("#cbIsAir").prop('checked') ? ("<td class='ChargeableWeight'>" + item.ChargeableWeight + "</td>") : "")
                + `<td class='hide'><a href='/api/OperationContainersAndPackages/GetLog?pID=${item.ID}&pTableName=vwOperationContainersAndPackages' target='_blank' ${downloadControlsText} </a></td>`
                + ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked')
                    //? ("<td class='hide'><a href='#EditContainerModal' data-toggle='modal' onclick='OperationContainersAndPackages_FillControls(" + item.ID + ");' " + editControlsText + "</a></td></tr>")
                    ? ("<td class='" + (OAPac && ODPac && $("#hIsOperationDisabled").val() == false ? "" : "hide") + "'><a href='#' data-toggle='modal' onclick='OperationContainersAndPackages_TransferContainerModal(" + item.ID + "," + item.OperationID + ");' " + transferControlsText + "</a></td></tr>")
                    : ("<td class='hide'><a href='#EditPackageModal' data-toggle='modal' onclick='OperationContainersAndPackages_FillControls(" + item.ID + ");' " + editControlsText + "</a></td></tr>")
                )));
        //fill the Container Number in case of LCL or LTL and RoadNumber in cas of Air
        if (item.ContainerNumber != 0)
            $("#txtPackagesContainerOrRoadNumber").val(item.ContainerNumber);
        ShowHidettblOperationContainersAndPackagesHeaders();

    });
    //ApplyPermissions();
    if (OAPac && $("#hIsOperationDisabled").val() == false) { $("#btn-SelectContainersAndPackages").removeClass("hide"); $(".classSetCargoProperties").removeClass("hide"); } else { $("#btn-SelectContainersAndPackages").addClass("hide"); $(".classSetCargoProperties").addClass("hide"); }
    if (ODPac && $("#hIsOperationDisabled").val() == false) $("#btn-DeleteContainerOrPackage").removeClass("hide"); else $("#btn-DeleteContainerOrPackage").addClass("hide");
    if (ODPac && $("#hIsOperationDisabled").val() == false) $("#btn-DownloadLogContainerOrPackage").removeClass("hide"); else $("#btn-DownloadLogContainerOrPackage").addClass("hide");
    if (OAPac && $("#hIsOperationDisabled").val() == false && ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked'))) {
        $("#btnAddFromExcel-OperationContainersAndPackages").removeClass("hide");
    }
    else {
        $("#btnAddFromExcel").addClass("hide");
    }
    BindAllCheckboxonTable("tblOperationContainersAndPackages", "ID", "cb-CheckAll-OperationContainersAndPackages");
    CheckAllCheckbox("HeaderDeleteOperationContainersAndPackagesID");
    //show/hide $("#btn-RemovePackages")
    var NumberOfContainersOrPackagesTableRows = $('#tblOperationContainersAndPackages tr').length - 1;//document.getElementById("tblOperationContainersAndPackages").getElementsByTagName("tr").length - 1;
    //if (//(NumberOfContainersOrPackagesTableRows == 1 && $("#hShipmentType").val() == constConsolidationShipmentType) //i am sure its master coz consol.
    //    //||
    //    ($("#hBLType").val() == constMasterBLType && $("#hShipmentType").val() != constConsolidationShipmentType && NumberOfContainersOrPackagesTableRows > 0))
    //    $("#btn-RemovePackages").removeClass("hide");
    //else
    //    $("#btn-RemovePackages").addClass("hide");

    if ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked')) {
        $("#btn-SelectContainersAndPackages").attr("data-target", "#EditContainerModal");
        $("#btn-SelectContainersAndPackages").attr("onclick", "OperationContainersAndPackages_ClearAllControls();");
    }
    else { //either Air or LCL or LTL
        $("#btn-SelectContainersAndPackages").attr("data-target", "#SelectContainersAndPackagesModal");
        $("#btn-SelectContainersAndPackages").attr("onclick", "OperationContainersAndPackages_GetAvailableContainersAndPackages();");
        $("#txtPackagesContainerOrRoadNumber").removeClass("hide");
        if (OEPac && $("#hIsOperationDisabled").val() == false)
            $("#btn-SaveContainerOrRoadNumber").removeClass("hide");
        ////fill the Container Number in case of LCL or LTL and RoadNumber in cas of Air
        //if (NumberOfContainersOrPackagesTableRows > 0)
        //    $("#txtPackagesContainerOrRoadNumber").val($("#tblOperationContainersAndPackages tr:nth(1) td.ContainerNumber").text());
        if ($("#cbIsAir").prop("checked")) {
            $("#btn-SaveContainerOrRoadNumber").text("Set Road No.");
            $("#txtPackagesContainerOrRoadNumber").attr("placeholder", "Road No.");
        }
        else { //this means LCL or LTL
            $("#btn-SaveContainerOrRoadNumber").text("Set Cont. No.");
            $("#txtPackagesContainerOrRoadNumber").attr("placeholder", "Cont. No.");
        }
    }
    //if (OEPac && $("#hIsOperationDisabled").val() == false && $("#hBLType").val() == constMasterBLType && !$("#cbIsConsolidation").prop('checked'))
    //    $("#btn-RebuildPackages").removeClass("hide");
    //else
    //    $("#btn-RebuildPackages").addClass("hide");

    HighlightText("#tblOperationContainersAndPackages>tbody>tr", $("#txtOperationContainerAndPackagesSearch").val().trim());
    if (showDeleteFailMessage) {//sherif: if deletion failed due to foreign key, then this message is shown
        swal(strSorry, strDeleteFailMessage);
        showDeleteFailMessage = false;
    }
    OperationContainersAndPackages_CalculateSummary();
    if (pDefaults.UnEditableCompanyName == "GBL" && $("#cbIsInland").prop("checked") && OVRec) {
        $("#tblReceivables tbody").html(""); $("#tblPayables tbody").html(""); //to reload changed quantities in Receivables
    }
}
function ShowHidettblOperationContainersAndPackagesHeaders() {
    if ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked')) { //Container headers
        $("#stepsContainers").removeClass("hide"); // in the wizard header
        $("#ContainerTypeHeader").removeClass("hide");
        $("#ContainerNumberHeader").removeClass("hide");
        $("#CarrierSealHeader").removeClass("hide");
        //$("#ShipperSealHeader").removeClass("hide");
        $("#TareWeightHeader").removeClass("hide");
        $("#VGMHeader").removeClass("hide");
        //$("#NetWeightHeader").removeClass("hide");
        $(".classTransferToOperation").removeClass("hide");

        $("#PackageTypeHeader").addClass("hide");
        $("#LengthHeader").addClass("hide");
        $("#WidthHeader").addClass("hide");
        $("#HeightHeader").addClass("hide");
        //$("#VolumeHeader").addClass("hide");
        //$("#GrossWeightHeader").addClass("hide");
        $("#VolumetricWeightHeader").addClass("hide");
        $("#ChargeableWeightHeader").addClass("hide");
        $("#QuantityHeader").addClass("hide");
    }
    if ($("#cbIsLCL").prop('checked') || $("#cbIsBulk").prop('checked') || $("#cbIsLTL").prop('checked') || $("#cbIsAir").prop('checked')) { //Package headers
        $("#stepsPackages").removeClass("hide"); // in the wizard header
        $("#ContainerTypeHeader").addClass("hide");
        $("#ContainerNumberHeader").addClass("hide");
        $("#CarrierSealHeader").addClass("hide");
        //$("#ShipperSealHeader").addClass("hide");
        $("#TareWeightHeader").addClass("hide");
        //$("#NetWeightHeader").addClass("hide");
        $("#VGMHeader").addClass("hide");

        $("#PackageTypeHeader").removeClass("hide");
        $("#LengthHeader").removeClass("hide");
        $("#WidthHeader").removeClass("hide");
        $("#HeightHeader").removeClass("hide");
        $("#VolumeHeader").removeClass("hide");
        $("#GrossWeightHeader").removeClass("hide");
        $("#VolumetricWeightHeader").removeClass("hide");
        $("#ChargeableWeightHeader").removeClass("hide");
        $("#QuantityHeader").removeClass("hide");
    }
    if ($("#cbIsTank").prop('checked')) {
        $("#TankOrFlexiNumberHeader").removeClass("hide");
    }
    if ($("#hBLType").val() == constMasterBLType && $("#hShipmentType").val() != constConsolidationShipmentType) //if master then show house header for packages
        $("#OperationContainersAndPackagesHouseHeader").removeClass("hide");
    else
        $("#OperationContainersAndPackagesHouseHeader").addClass("hide");
}
function OperationContainersAndPackages_EditByDblClick(pID) {
    debugger;
    if ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked'))
        jQuery("#EditContainerModal").modal("show");
    else
        jQuery("#EditPackageModal").modal("show");
    OperationContainersAndPackages_FillControls(pID);
}
//function OperationContainersAndPackages_LoadWithPagingWithWhereClause() {
//    LoadWithPagingWithWhereClauseAndOrderBy("div-Pager", "select-page-size", "spn-first-page-row", "spn-last-page-row", "spn-total-count", "div-Text-Total", "/api/OperationContainersAndPackages/LoadWithWhereClause", " where OperationID = " + $("#hOperationID").val(), "ContainerTypeCode, ContainerNumber, PackageTypeName", 1, 1000, function (pTabelRows) { OperationContainersAndPackages_BindTableRows(pTabelRows); });
//}
function OperationContainersAndPackages_DeleteList(callback) {
    //update first to save the ContainerNumber or RoadNumber incase the row containing it is deleted
    if ($("#cbIsAir").prop("checked") || $("#cbIsLCL").prop("checked") || $("#cbIsBulk").prop("checked") || $("#cbIsLTL").prop("checked"))
        CallGETFunctionWithParameters("/api/OperationContainersAndPackages/SaveContainerOrRoadNumber", {
            pOperationID: $("#hOperationID").val()
            , pContainerNumber: $("#txtPackagesContainerOrRoadNumber").val() == "" ? 0 : $("#txtPackagesContainerOrRoadNumber").val().trim().toUpperCase()
        }, null);
    //Confirmation message to delete
    if (GetAllSelectedIDsAsString('tblOperationContainersAndPackages') != "")
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
                DeleteListFunction("/api/OperationContainersAndPackages/Delete"
                    , { "pOperationContainersAndPackagesIDs": GetAllSelectedIDsAsString('tblOperationContainersAndPackages') }
                    , function () {
                        //OperationContainersAndPackages_LoadWithPagingWithWhereClause();
                        OperationContainersAndPackages_Search();
                    });
            });
    //DeleteListFunction("/api/OperationContainersAndPackages/Delete", { "pOperationContainersAndPackagesIDs": GetAllSelectedIDsAsString('tblOperationContainersAndPackages') }, function () { LoadViews("OperationsEdit", null, $("#hOperationID").val()); });
}
function OperationContainersAndPackages_GetAvailableContainersAndPackages(callback) {
    debugger;
    var pStrFnName = (($("#cbIsLCL").prop('checked') || $("#cbIsBulk").prop('checked') || $("#cbIsLTL").prop('checked') || $("#cbIsAir").prop('checked'))
        ? "/api/PackageTypes/LoadAll"
        : "/api/ContainerTypes/LoadAll");
    var pDivName = "divSelectContainersAndPackages";
    var pCheckboxNameAttr = "cbSelectContainersAndPackages";
    var pWhereClause = "";
    pWhereClause += " WHERE IsInactive = 0 ";
    //the next 3 lines are used just incase of LCL, LTL or Air with packages
    pWhereClause += ($("#cbIsLCL").prop('checked') ? " AND IsOcean = 1 " : "")
    pWhereClause += ($("#cbIsLTL").prop('checked') ? " AND IsInland = 1 " : "")
    pWhereClause += ($("#cbIsAir").prop('checked') ? " AND IsAir = 1 " : "")

    pWhereClause += " AND ( Code LIKE '%" + $("#txtSearchContainersAndPackages").val().trim().toUpperCase() + "%' OR Name LIKE '%" + $("#txtSearchContainersAndPackages").val().trim().toUpperCase() + "%') ";

    ////the next lines are to exclude the already selected types
    //pWhereClause += (($("#cbIsLCL").prop('checked') || $("#cbIsBulk").prop('checked') || $("#cbIsLTL").prop('checked') || $("#cbIsAir").prop('checked'))
    //    ? " AND ID NOT IN (SELECT PackageTypeID from OperationContainersAndPackages "
    //    : " AND ID NOT IN (SELECT ContainerTypeID from OperationContainersAndPackages ");
    //pWhereClause += "  WHERE OperationID = " + $("#hOperationID").val() + ") ";

    pWhereClause += " ORDER BY Code ";
    GetListAsCheckboxes(pStrFnName, pWhereClause, pDivName, pCheckboxNameAttr
        , function () { HighlightText("#divSelectContainersAndPackages", $("#txtSearchContainersAndPackages").val().trim().toUpperCase()); });
    $("#btn-SearchContainersAndPackages").attr("onclick", "OperationContainersAndPackages_GetAvailableContainersAndPackages();");
    $("#btnSelectContainersAndPackagesApply").attr("onclick", "OperationContainersAndPackages_InsertFromCheckList(true);");
}
//called when pressing Apply in SelectContainersAndPackages Modal
function OperationContainersAndPackages_InsertFromCheckList(pSaveandAddNew) {
    var pSelectedIDs = GetAllSelectedIDsAsStringWithNameAttr("cbSelectContainersAndPackages");//returns string array of IDs
    debugger;
    if (pSelectedIDs != "")
        InsertSelectedCheckboxItems("/api/OperationContainersAndPackages/InsertList"
            , { "pOperationID": $("#hOperationID").val(), "pSelectedIDs": pSelectedIDs, "pShipmentType": ($('input[name=cbShipmentType]:checked').val() == undefined ? 0 : $('input[name=cbShipmentType]:checked').val()), "pTransportType": $('input[name=cbTransportType]:checked').val() } //ShipmentType : 1-FCL 2-LCL 3-FTL 4-LTL
            , pSaveandAddNew
            , "SelectContainersAndPackagesModal" //pModalID
            , function () { OperationContainersAndPackages_GetAvailableContainersAndPackages(); }
            , function () {
                OperationContainersAndPackages_Search();
            });
}
//insert Container
function OperationContainersAndPackages_Insert(pSaveandAddNew) {
    debugger;
    if ($("#cbIsTank").prop("checked") && $("#txtTankOrFlexiNumber").val().trim() != ""
        && !$("#txtTankOrFlexiNumber").val().includes("_") && !$("#txtTankOrFlexiNumber").val().includes("/"))
        swal("Sorry", "Tank number must contain either (_) or (/)");
    else if (OperationContainersAndPackages_ValidateProperties())
        InsertUpdateFunction("form", "/api/OperationContainersAndPackages/Insert", {
            pOperationID: $("#hOperationID").val()
            , pContainerTypeID: ($("#slContainerTypes option:selected").val() == undefined ? 0 : $("#slContainerTypes option:selected").val())
            , pNumberOfContainers: $("#txtNumberOfContainers").val().trim() //pNumberOfContainers(txtNumberOfContainers) is not saved in DB, its just to decide how many containers to be inserted; between 1 and 50
            , pPackageTypeID: 0 //Not used when inserting packages //($("#txtPackageType").attr("PackageTypeID") == undefined ? 0 : $("#txtPackageType").attr("PackageTypeID"))
            , pLength: ($("#txtLength").val() == "" ? 0 : $("#txtLength").val())
            , pWidth: ($("#txtWidth").val() == "" ? 0 : $("#txtWidth").val())
            , pHeight: ($("#txtHeight").val() == "" ? 0 : $("#txtHeight").val())
            , pVolume: ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked')
                ? ($("#txtContainerVolume").val() == "" ? 0 : $("#txtContainerVolume").val())
                : ($("#txtVolume").val() == "" ? 0 : $("#txtVolume").val()))
            , pVolumetricWeight: ($("#txtVolumetricWeight").val() == "" ? 0 : $("#txtVolumetricWeight").val())
            , pNetWeight: ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked')
                ? ($("#txtContainerNetWeight").val() == "" ? 0 : $("#txtContainerNetWeight").val())
                : ($("#txtNetWeight").val() == "" ? 0 : $("#txtNetWeight").val()))
            , pNetWeightTON: ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked')
                ? ($("#txtContainerNetWeightTON").val() == "" ? 0 : $("#txtContainerNetWeightTON").val())
                : ($("#txtNetWeightTON").val() == "" ? 0 : $("#txtNetWeightTON").val()))
            , pGrossWeight: ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked')
                ? ($("#txtContainerGrossWeight").val() == "" ? 0 : $("#txtContainerGrossWeight").val())
                : ($("#txtGrossWeight").val() == "" ? 0 : $("#txtGrossWeight").val()))
            , pGrossWeightTON: ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked')
                ? ($("#txtContainerGrossWeightTON").val() == "" ? 0 : $("#txtContainerGrossWeightTON").val())
                : ($("#txtGrossWeightTON").val() == "" ? 0 : $("#txtGrossWeightTON").val()))
            , pChargeableWeight: ($("#txtChargeableWeight").val() == "" ? 0 : $("#txtChargeableWeight").val())
            //, pQuantity: ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') ? ($("#txtContainersQuantity").val() == "" ? 0 : $("#txtContainersQuantity").val()) : ($("#txtPackagesQuantity").val() == "" ? 0 : $("#txtPackagesQuantity").val()))
            , pQuantity: ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked') ? 0 : ($("#txtPackagesQuantity").val() == "" ? 0 : $("#txtPackagesQuantity").val()))

            , pContainerNumber: ($("#txtContainerNumber").val() == "" ? 0 : $("#txtContainerNumber").val().trim().toUpperCase())
            , pCarrierSeal: ($("#txtCarrierSeal").val() == "" ? 0 : $("#txtCarrierSeal").val().trim().toUpperCase())
            , pShipperSeal: ($("#txtShipperSeal").val() == "" ? 0 : $("#txtShipperSeal").val().trim().toUpperCase())
            , pTareWeight: ($("#txtTareWeight").val() == "" ? 0 : $("#txtTareWeight").val())
            , pVGM: ($("#txtVGM").val() == "" ? 0 : $("#txtVGM").val())

            , pIsReefer: ($("#cbIsReefer").prop("checked") ? true : false)
            , pIsNOR: ($("#cbIsNOR").prop("checked") ? true : false)
            , pMinTemp: ($("#txtMinTemp").val() == "" ? 0 : $("#txtMinTemp").val())
            , pMaxTemp: ($("#txtMaxTemp").val() == "" ? 0 : $("#txtMaxTemp").val())
            , pVentilation: ($("#txtVentilation").val() == "" ? 0 : $("#txtVentilation").val())
            , pHumidity: ($("#txtHumidity").val() == "" ? 0 : $("#txtHumidity").val())
            , pLotNumber: ($("#txtLotNumber").val().trim() == "" ? 0 : $("#txtLotNumber").val().trim().toUpperCase())

            , pPackageTypeIDOnContainer: ($("#slPackageTypesOnContainer").val() == "" ? 0 : $("#slPackageTypesOnContainer").val())
            , pNumberOfPackagesOnContainer: ($("#txtNumberOfPackagesOnContainer").val() == "" ? 0 : $("#txtNumberOfPackagesOnContainer").val())

            , pIsIMO: ($("#cbIsIMO").prop("checked") ? true : false)
            , pIMOClass: ($("#txtIMOClass").val() == "" ? 0 : $("#txtIMOClass").val())
            , pUNNumber: ($("#txtUNNumber").val() == "" ? 0 : $("#txtUNNumber").val())
            , pFlashPoint: ($("#txtFlashPoint").val() == "" ? 0 : $("#txtFlashPoint").val())
            , pDescriptionOfGoods: ($("#divContainerGoodsDescription").val().trim() == "" ? 0 : $("#divContainerGoodsDescription").val().trim().toUpperCase())
            , pMaxTemp: ($("#txtMaxTemp").val() == "" ? 0 : $("#txtMaxTemp").val())

            , pPlacedOnOCPID: 0
            , pIsSentToWarehouse: true
            , pIsLoaded: false

        }, pSaveandAddNew
            , ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked') ? "EditContainerModal" : "EditPackageModal") //Modal name
            , function (pData) {
                swal("Success", "Saved successfully.");
                OperationContainersAndPackages_BindTableRows(JSON.parse(pData[2]));
                OperationContainersAndPackages_FillLabels(JSON.parse(pData[3]));
                if (pSaveandAddNew) {
                    OperationContainersAndPackages_ClearAllControls();
                }
                CallGETFunctionWithParameters("/api/OperationContainersAndPackages/LoadAllTanks"
                    , {
                        pPageNumber: 1
                        , pPageSize: 999999
                        , pWhereClauseForTank: "WHERE OperationID=" + $("#hOperationID").val() + ($("#cbIsTank").prop("checked") ? " AND TankOrFlexiNumber IS NOT NULL AND TankOrFlexiNumber<>'' " : " AND ContainerNumber IS NOT NULL AND ContainerNumber<>'' ")
                        , pOrderBy: "TankOrFlexiNumber"
                    }
                    , function (pData_Tank) {
                        var pTank = pData_Tank[0];
                        if ($("#cbIsTank").prop("checked"))
                            FillListFromObject(null, 1, "<--Select Tank-->", "slSearchTankInPayables", pTank
                                , function () { $("#slSearchTankInReceivables").html($("#slSearchTankInPayables").html()); });
                        else FillListFromObject(null, 11, "<--Select Container-->", "slSearchTankInPayables", pTank
                            , function () { $("#slSearchTankInReceivables").html($("#slSearchTankInPayables").html()); });
                    }
                    , null);
            });
}
function OperationContainersAndPackages_Update(pSaveandAddNew) {
    debugger;
    if ($("#cbIsTank").prop("checked") && $("#txtTankOrFlexiNumber").val().trim() != ""
        && !$("#txtTankOrFlexiNumber").val().includes("_") && !$("#txtTankOrFlexiNumber").val().includes("/"))
        swal("Sorry", "Tank number must contain either (_) or (/)");
    else if (OperationContainersAndPackages_ValidateProperties())
        InsertUpdateFunction("form", "/api/OperationContainersAndPackages/Update", {
            pID: ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked') ? $("#hContainerID").val() : $("#hPackageID").val())
            , pOperationID: $("#hOperationID").val()
            , pContainerTypeID: ($("#slContainerTypes option:selected").val() == undefined ? 0 : $("#slContainerTypes option:selected").val())
            , pPackageTypeID: ($("#slPackageTypes").val() == "" ? 0 : $("#slPackageTypes").val()) //($("#txtPackageType").attr("PackageTypeID") == undefined ? 0 : $("#txtPackageType").attr("PackageTypeID"))
            , pLength: ($("#txtLength").val() == "" ? 0 : $("#txtLength").val())
            , pWidth: ($("#txtWidth").val() == "" ? 0 : $("#txtWidth").val())
            , pHeight: ($("#txtHeight").val() == "" ? 0 : $("#txtHeight").val())
            , pVolume: ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked')
                ? ($("#txtContainerVolume").val() == "" ? 0 : $("#txtContainerVolume").val())
                : ($("#txtVolume").val() == "" ? 0 : $("#txtVolume").val()))
            , pVolumetricWeight: ($("#txtVolumetricWeight").val() == "" ? 0 : $("#txtVolumetricWeight").val())
            , pNetWeight: ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked')
                ? ($("#txtContainerNetWeight").val() == "" ? 0 : $("#txtContainerNetWeight").val())
                : ($("#txtNetWeight").val() == "" ? 0 : $("#txtNetWeight").val()))
            , pNetWeightTON: ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked')
                ? ($("#txtContainerNetWeightTON").val() == "" ? 0 : $("#txtContainerNetWeightTON").val())
                : ($("#txtNetWeightTON").val() == "" ? 0 : $("#txtNetWeightTON").val()))
            , pGrossWeight: ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked')
                ? ($("#txtContainerGrossWeight").val() == "" ? 0 : $("#txtContainerGrossWeight").val())
                : ($("#txtGrossWeight").val() == "" ? 0 : $("#txtGrossWeight").val()))
            , pGrossWeightTON: ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked')
                ? ($("#txtContainerGrossWeightTON").val() == "" ? 0 : $("#txtContainerGrossWeightTON").val())
                : ($("#txtGrossWeightTON").val() == "" ? 0 : $("#txtGrossWeightTON").val()))
            , pChargeableWeight: ($("#txtChargeableWeight").val() == "" ? 0 : $("#txtChargeableWeight").val())
            //, pQuantity: ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') ? ($("#txtContainersQuantity").val() == "" ? 0 : $("#txtContainersQuantity").val()) : ($("#txtPackagesQuantity").val() == "" ? 0 : $("#txtPackagesQuantity").val()))
            , pQuantity: ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked') ? 0 : ($("#txtPackagesQuantity").val() == "" ? 0 : $("#txtPackagesQuantity").val()))

            , pContainerNumber: ($("#txtContainerNumber").val() == "" ? 0 : $("#txtContainerNumber").val().trim().toUpperCase())
            , pCarrierSeal: ($("#txtCarrierSeal").val() == "" ? 0 : $("#txtCarrierSeal").val().trim().toUpperCase())
            , pShipperSeal: ($("#txtShipperSeal").val() == "" ? 0 : $("#txtShipperSeal").val().trim().toUpperCase())
            , pTareWeight: ($("#txtTareWeight").val() == "" ? 0 : $("#txtTareWeight").val())
            , pVGM: ($("#txtVGM").val() == "" ? 0 : $("#txtVGM").val())

            , pIsReefer: ($("#cbIsReefer").prop("checked") ? true : false)
            , pIsNOR: ($("#cbIsNOR").prop("checked") ? true : false)
            , pMinTemp: ($("#txtMinTemp").val() == "" ? 0 : $("#txtMinTemp").val())
            , pMaxTemp: ($("#txtMaxTemp").val() == "" ? 0 : $("#txtMaxTemp").val())
            , pVentilation: ($("#txtVentilation").val() == "" ? 0 : $("#txtVentilation").val())
            , pHumidity: ($("#txtHumidity").val() == "" ? 0 : $("#txtHumidity").val())
            , pLotNumber: ($("#txtLotNumber").val().trim() == "" ? 0 : $("#txtLotNumber").val().trim().toUpperCase())

            , pPackageTypeIDOnContainer: ($("#slPackageTypesOnContainer").val() == "" || $("#slPackageTypesOnContainer").val() == null ? 0 : $("#slPackageTypesOnContainer").val())
            , pNumberOfPackagesOnContainer: ($("#txtNumberOfPackagesOnContainer").val() == "" ? 0 : $("#txtNumberOfPackagesOnContainer").val())

            , pIsIMO: ($("#cbIsIMO").prop("checked") ? true : false)
            , pIMOClass: ($("#txtIMOClass").val() == "" ? 0 : $("#txtIMOClass").val())
            , pUNNumber: ($("#txtUNNumber").val() == "" ? 0 : $("#txtUNNumber").val())
            , pFlashPoint: ($("#txtFlashPoint").val() == "" ? 0 : $("#txtFlashPoint").val())

            //, pDescriptionOfGoods: ($("#divContainerGoodsDescription").val().trim() == "" ? 0 : $("#divContainerGoodsDescription").val().trim())
            , pDescriptionOfGoods: ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked')
                ? ($("#divContainerGoodsDescription").val().trim() == "" ? 0 : $("#divContainerGoodsDescription").val().trim().toUpperCase())
                : ($("#txtPackageDescriptionOfGoods").val().trim() == "" ? 0 : $("#txtPackageDescriptionOfGoods").val().trim().toUpperCase()))
            , pOperatorID: $("#slOperator").val() == "" ? 0 : $("#slOperator").val()
            , pTankOrFlexiNumber: $("#txtTankOrFlexiNumber").val().trim() == "" ? 0 : $("#txtTankOrFlexiNumber").val().trim().toUpperCase()
            , pIsSentToWarehouse: $("#cbIsSentToWarehouse").prop("checked")
            , pIsLoaded: $("#cbIsTankLoaded").prop("checked")
        }, pSaveandAddNew
            , ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked') ? "EditContainerModal" : "EditPackageModal") //Modal name
            , function (pData) {
                var pMessageReturned = pData[4];
                var pPayables = JSON.parse(pData[5]);
                var pReceivables = JSON.parse(pData[6]);
                var pOperationPartners = JSON.parse(pData[7]);
                if (pMessageReturned == "")
                    swal("Success", "Saved successfully.");
                else
                    swal("Sorry", pMessageReturned);
                OperationContainersAndPackages_BindTableRows(JSON.parse(pData[2]));
                OperationContainersAndPackages_FillLabels(JSON.parse(pData[3]));
                if (pDefaults.UnEditableCompanyName == "GBL" && $("#cbIsInland").prop("checked") && OVRec) {
                    $("#tblReceivables tbody").html(""); $("#tblPayables tbody").html(""); //to reload changed quantities in Receivables
                }
                else {
                    Receivables_BindTableRows(pReceivables);
                    Payables_BindTableRows(pPayables);
                }
                OperationPartners_BindTableRows(pOperationPartners);
                if (pSaveandAddNew) {
                    if ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked'))
                        OperationContainersAndPackages_ClearAllControls();
                    else { //LCL
                        OperationContainersAndPackages_GetAvailableContainersAndPackages();
                        jQuery("#EditPackageModal").modal("hide");
                        jQuery("#SelectContainersAndPackagesModal").modal("show");
                    }
                }


                CallGETFunctionWithParameters("/api/OperationContainersAndPackages/LoadAllTanks"
                    , {
                        pPageNumber: 1
                        , pPageSize: 999999
                        , pWhereClauseForTank: "WHERE OperationID=" + $("#hOperationID").val() + ($("#cbIsTank").prop("checked") ? " AND TankOrFlexiNumber IS NOT NULL AND TankOrFlexiNumber<>'' " : " AND ContainerNumber IS NOT NULL AND ContainerNumber<>'' ")
                        , pOrderBy: "TankOrFlexiNumber"
                    }
                    , function (pData_Tank) {
                        var pTank = pData_Tank[0];
                        if ($("#cbIsTank").prop("checked"))
                            FillListFromObject(null, 1, "<--Select Tank-->", "slSearchTankInPayables", pTank
                                , function () { $("#slSearchTankInReceivables").html($("#slSearchTankInPayables").html()); });
                        else FillListFromObject(null, 11, "<--Select Container-->", "slSearchTankInPayables", pTank
                            , function () { $("#slSearchTankInReceivables").html($("#slSearchTankInPayables").html()); });
                    }
                    , null);

                FadePageCover(false);
            });
}
function OperationContainersAndPackages_Search() {
    debugger;
    var pSearchKey = $("#txtOperationContainerAndPackagesSearch").val().trim().toUpperCase();
    var pWhereClause = " WHERE OperationID = " + $("#hOperationID").val()
        + " AND  (ContainerTypeCode LIKE '%" + pSearchKey + "%' "
        + " OR ContainerNumber LIKE '%" + pSearchKey + "%' "
        + " OR PackageTypeName LIKE '%" + pSearchKey + "%' "
        + " OR HouseOperationCode LIKE '%" + pSearchKey + "%') "
    //var pParametersWithValues = { pPageNumber: 1, pPageSize: 99999, pWhereClause: pWhereClause, pOrderBy: "ContainerTypeCode, ContainerNumber, PackageTypeName" };
    var pParametersWithValues = { pPageNumber: 1, pPageSize: 99999, pWhereClause: pWhereClause, pOrderBy: "ID", pOperationID: $("#hOperationID").val() };
    //LoadWithPagingWithWhereClauseAndOrderBy("div-Pager", "select-page-size", "spn-first-page-row", "spn-last-page-row", "spn-total-count", "div-Text-Total", "/api/OperationContainersAndPackages/LoadWithWhereClause", pWhereClause, "ContainerTypeCode, ContainerNumber, PackageTypeName", 1, 1000, function (pTabelRows) { OperationContainersAndPackages_BindTableRows(pTabelRows); });
    FadePageCover(true);
    CallGETFunctionWithParameters("/api/OperationContainersAndPackages/Search"
        , pParametersWithValues
        , function (pData) {
            OperationContainersAndPackages_BindTableRows(JSON.parse(pData[0]));
            pOperationMasterOrDirect = JSON.parse(pData[2]);
            OperationContainersAndPackages_FillLabels(pOperationMasterOrDirect);
            FadePageCover(false);
        }, null);
}
function OperationContainersAndPackages_FillControls(pID) {
    debugger;
    $("#btn-ApplyReeferPropertiesToAll").removeClass("hide");
    $("#divShipmentContainers").addClass("hide");
    var tr = $("#tblOperationContainersAndPackages tr[ID='" + pID + "']");
    if ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked')) {

        ClearAll("#EditContainerModal");
        $("#lblContainerShown").html(": " + $(tr).find("td.ContainerNumber").text());
        //$("#divContainerPackages").removeClass("hide");
        $("#btn-NewAddContainerPackage").removeAttr("disabled");

        var pContainerTypeID = $(tr).find("td.Container").attr('val');
        ContainerTypes_GetList(pContainerTypeID, null);
        var pPackageTypeIDOnContainer = $(tr).find("td.PackageTypeOnContainer").attr('val');
        PackageTypesOnContainer_GetList(pPackageTypeIDOnContainer, "slPackageTypesOnContainer", function () { $("#slPackageTypes").html($("#slPackageTypesOnContainer").html()); $("#slPackageTypes").val(""); });

        var pOperatorID = $(tr).find("td.OperatorID").text() == 0 ? "" : $(tr).find("td.OperatorID").text();
        if ($("#cbIsTank").prop("checked")) {
            //$("#slOperator").html($("#hReadySlCustomers").html());
            //$("#slOperator").val(pOperatorID);
            $(".classShowForTank").removeClass("hide");
            $("#slOperator").parent().removeClass("hide");
            CallGETFunctionWithParameters("/api/Agents/LoadAllForCombo"
                , { pWhereClauseForCombo: "ORDER BY Name" }
                , function (pData) {
                    FillListFromObject(pOperatorID, 2, TranslateString("SelectFromMenu"), "slOperator", pData[0], null);
                }
                , null);
        }
        $("#txtTankOrFlexiNumber").val($(tr).find("td.TankOrFlexiNumber").text());

        $("#hContainerID").val(pID);
        $("#txtContainerNumber").val($(tr).find("td.ContainerNumber").text());
        $("#txtNumberOfContainers").attr("disabled", "disabled");
        $("#txtNumberOfContainers").val("N/A");
        $("#txtCarrierSeal").val($(tr).find("td.CarrierSeal").text());
        $("#txtShipperSeal").val($(tr).find("td.ShipperSeal").text());
        $("#txtTareWeight").val($(tr).find("td.TareWeight").text());
        $("#txtVGM").val($(tr).find("td.VGM").text());
        $("#txtContainerNetWeight").val($(tr).find("td.ContainerNetWeight").text());
        $("#txtContainerNetWeightTON").val($(tr).find("td.ContainerNetWeightTON").text());
        $("#txtContainerVolume").val($(tr).find("td.ContainerVolume").text());
        $("#txtContainerGrossWeight").val($(tr).find("td.ContainerGrossWeight").text());
        $("#txtContainerGrossWeightTON").val($(tr).find("td.ContainerGrossWeightTON").text());
        $("#cbIsReefer").prop('checked', $(tr).find('td.IsReefer').find('input').attr('val'));
        $("#cbIsNOR").prop('checked', $(tr).find('td.IsNOR').find('input').attr('val'));
        $("#cbIsSentToWarehouse").prop('checked', $(tr).find('td.IsSentToWarehouse').find('input').attr('val'));
        $("#cbIsTankLoaded").prop('checked', $(tr).find('td.IsLoaded').find('input').attr('val'));

        $("#txtMinTemp").val($(tr).find("td.MinTemp").text());
        $("#txtMaxTemp").val($(tr).find("td.MaxTemp").text());
        $("#txtHumidity").val($(tr).find("td.Humidity").text());
        $("#txtVentilation").val($(tr).find("td.Ventilation").text());

        $("#txtLotNumber").val($(tr).find("td.LotNumber").text());
        $("#txtNumberOfPackagesOnContainer").val($(tr).find("td.NumberOfPackagesOnContainer").text());

        $("#cbIsIMO").prop('checked', $(tr).find('td.IsIMO').find('input').attr('val'));
        $("#txtIMOClass").val($(tr).find("td.IMOClass").text());
        $("#txtUNNumber").val($(tr).find("td.UNNumber").text());
        $("#txtFlashPoint").val($(tr).find("td.FlashPoint").text());
        $("#divContainerGoodsDescription").val($(tr).find("td.DescriptionOfGoods").text());
        EnableDisableReeferProprties();
        EnableDisableIMOProprties();
        //fill the ContainerPackages Table
        LoadWithPagingWithWhereClauseAndOrderBy("div-Pager", "select-page-size", "spn-first-page-row", "spn-last-page-row", "spn-total-count", "div-Text-Total", "/api/ContainerPackages/LoadWithWhereClause"
            , " WHERE OperationContainersAndPackagesID = " + pID.toString(), " HouseOperationCode, PackageTypeName "
            , 1, 1000
            , function (data) { ContainerPackages_BindTableRows(JSON.parse(data[0])/*pTabelRows*/); });
        $("#btn-NewAddContainerPackage").attr("onclick", "ContainerPackages_ClearAllControls(null," + $("#hOperationID").val() + ");");

        $("#btnSaveContainer").attr("onclick", "OperationContainersAndPackages_Update(false);");
        $("#btnSaveAndNewContainer").attr("onclick", "OperationContainersAndPackages_Update(true);");
    }
    else {

        ClearAll("#EditPackageModal");
        $("#lblPackageShown").html(": " + $(tr).find("td.Package").text());

        $("#txtNumberOfContainers").val("N/A");
        $("#hPackageID").val(pID);
        GetListWithNameAndWhereClause($(tr).find("td.Package").attr("val"), "/api/PackageTypes/LoadAll", TranslateString("SelectFromMenu"), "slPackageTypes", "WHERE 1=1", null);
        $("#txtLength").val($(tr).find("td.Length").text());
        $("#txtWidth").val($(tr).find("td.Width").text());
        $("#txtHeight").val($(tr).find("td.Height").text());
        $("#txtVolume").val($(tr).find("td.Volume").text());
        $("#txtNetWeight").val($(tr).find("td.NetWeight").text());
        $("#txtNetWeightTON").val($(tr).find("td.NetWeightTON").text());
        $("#txtGrossWeight").val($(tr).find("td.GrossWeight").text());
        $("#txtGrossWeightTON").val($(tr).find("td.GrossWeightTON").text());
        $("#txtChargeableWeight").val($(tr).find("td.ChargeableWeight").text());
        $("#txtVolumetricWeight").val($(tr).find("td.VolumetricWeight").text());
        $("#txtPackagesQuantity").val($(tr).find("td.Quantity").text());
        $("#txtPackageDescriptionOfGoods").val($(tr).find("td.DescriptionOfGoods").text());

        $("#btnSavePackage").attr("onclick", "OperationContainersAndPackages_Update(false);");
        $("#btnSaveAndNewPackage").attr("onclick", "OperationContainersAndPackages_Update(true);");
    }
}
function OperationContainersAndPackages_ClearAllControls(callback) {
    debugger;
    $("#btn-ApplyReeferPropertiesToAll").removeClass("hide");
    $("#divShipmentContainers").addClass("hide");

    if ($("#cbIsTank").prop("checked")) {
        //$(".classShowForTank").removeClass("hide");
        $("#slOperator").parent().addClass("hide"); //because not in Controller
        $(".classHideForFlexi").addClass("hide");
        $(".classDisableForFlexi").attr("disabled", "disabled");
        $("#spanFlexiLabel").text("Flexi");
        //CallGETFunctionWithParameters("/api/Agents/LoadAllForCombo"
        //    , { pWhereClauseForCombo: "ORDER BY Name" }
        //    , function (pData) {
        //        FillListFromObject(null, 2, TranslateString("SelectFromMenu"), "slOperator", pData[0], null);
        //    }
        //    , null);
    }
    else if ($("#cbIsFlexi").prop("checked")) {
        $(".classShowForTank").removeClass("hide");
        $("#spanFlexiLabel").text("Purchase.Inv");
    }

    if ($("#cbIsVehicle").prop('checked') || $("#cbIsFCL").prop('checked') || $("#cbIsFTL").prop('checked') || $("#cbIsConsolidation").prop('checked') || $("#cbIsFlexi").prop('checked') || $("#cbIsTank").prop('checked')) {
        ClearAll("#EditContainerModal");
        $("#txtNumberOfContainers").removeAttr("disabled");
        $("#txtNumberOfContainers").val("1");
        $("#tblContainerPackages > tbody").html(""); //Clear the ContainerPackages rows
        //$("#divContainerPackages").addClass("hide");
        $("#btn-NewAddContainerPackage").attr("disabled", "disabled");
        ContainerTypes_GetList(null, null);
        PackageTypesOnContainer_GetList(null, "slPackageTypesOnContainer", function () { $("#slPackageTypes").html($("#slPackageTypesOnContainer").html()); $("#slPackageTypes").val(""); });
        //$("#btnSaveContainer").attr("onclick", "OperationContainersAndPackages_Insert(true);");
        $("#btnSaveContainer").attr("onclick", "OperationContainersAndPackages_Insert(false);");
        $("#btnSaveAndNewContainer").attr("onclick", "OperationContainersAndPackages_Insert(true);");
    }
    else {
        ClearAll("#EditPackageModal");
        GetListWithNameAndWhereClause(null, "/api/PackageTypes/LoadAll", TranslateString("SelectFromMenu"), "slPackageTypes", "WHERE 1=1", null);
    }
    if (callback != null && callback != undefined)
        callback();
}
function OperationContainersAndPackages_SaveContainerOrRoadNumber() {
    if ($("#tblOperationContainersAndPackages tr").length > 1) { //i am sure its LCL or LTL
        FadePageCover(true);
        CallGETFunctionWithParameters("/api/OperationContainersAndPackages/SaveContainerOrRoadNumber", {
            pOperationID: $("#hOperationID").val()
            , pContainerNumber: $("#txtPackagesContainerOrRoadNumber").val() == "" ? 0 : $("#txtPackagesContainerOrRoadNumber").val().trim().toUpperCase()
        }
            , function () { FadePageCover(false); });
    }
    else
        swal(strSorry, "Add at least one package type first.");
}
function EnableDisableReeferProprties() {
    if (!$("#cbIsReefer").prop('checked')) { //if it's not reefer then  NOR(Non Operating Reefer) is disabled
        $("#cbIsNOR").attr("disabled", "disabled");
        $("#cbIsNOR").prop("checked", false);
    }
    else //reefer
        $("#cbIsNOR").removeAttr("disabled");

    if ($("#cbIsReefer").prop("checked") && !$("#cbIsNOR").prop('checked')) {
        $("#txtMinTemp").removeAttr("disabled");
        $("#txtMaxTemp").removeAttr("disabled");
        $("#txtHumidity").removeAttr("disabled");
        $("#txtVentilation").removeAttr("disabled");
    }
    else {
        $("#txtMinTemp").attr("disabled", "disabled");
        $("#txtMinTemp").val(0);
        $("#txtMaxTemp").attr("disabled", "disabled");
        $("#txtMaxTemp").val(0);
        $("#txtHumidity").attr("disabled", "disabled");
        $("#txtHumidity").val(0);
        $("#txtVentilation").attr("disabled", "disabled");
        $("#txtVentilation").val(0);
    }
}
function OperationContainersAndPackages_ApplyReeferPropertiesToAll() {
    debugger;
    swal({
        title: "Are you sure?",
        text: "The reefer properties will be applied to all existing containers on that operation.",
        //type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes, Re-Build",
        closeOnConfirm: true
    },
        //callback function in case of Confirm
        function () {
            FadePageCover(true);
            var pParametersWithValues = {
                pOperationIDToApplyReeferProperties: $("#hOperationID").val()
                , pIsReefer: ($("#cbIsReefer").prop("checked") ? true : false)
                , pIsNOR: ($("#cbIsNOR").prop("checked") ? true : false)
                , pMinTemp: ($("#txtMinTemp").val() == "" ? 0 : $("#txtMinTemp").val())
                , pMaxTemp: ($("#txtMaxTemp").val() == "" ? 0 : $("#txtMaxTemp").val())
                , pVentilation: ($("#txtVentilation").val() == "" ? 0 : $("#txtVentilation").val())
                , pHumidity: ($("#txtHumidity").val() == "" ? 0 : $("#txtHumidity").val())
            };
            CallGETFunctionWithParameters("/api/OperationContainersAndPackages/ApplyReeferPropertiesToAll"
                , pParametersWithValues
                , function (pData) {
                    OperationContainersAndPackages_BindTableRows(JSON.parse(pData[0]));
                    FadePageCover(false);
                });
        });
}
function EnableDisableIMOProprties() {
    if ($("#cbIsIMO").prop("checked")) {
        $("#txtIMOClass").removeAttr("disabled");
        $("#txtUNNumber").removeAttr("disabled");
        $("#txtFlashPoint").removeAttr("disabled");
    }
    else {
        $("#txtIMOClass").attr("disabled", "disabled");
        $("#txtIMOClass").val(0);
        $("#txtUNNumber").attr("disabled", "disabled");
        $("#txtUNNumber").val(0);
        $("#txtFlashPoint").attr("disabled", "disabled");
        $("#txtFlashPoint").val(0);
    }
}
function CalculateVolume() {
    debugger;
    var _Volume = $("#txtLength").val() * $("#txtWidth").val() * $("#txtHeight").val() * $("#txtPackagesQuantity").val() / 1000000;
    if ($("#txtLength").val() != "" && $("#txtWidth").val() != "" && $("#txtHeight").val() != "" && $("#txtPackagesQuantity").val() != "") {
        $("#txtVolume").val(Math.round(100 * _Volume) / 100);
        //i am sure in the next 2 conditions that they are LCL or LTL  //if Ocean then no volumetric weight
        if ($("#cbIsAir").prop("checked"))
            $("#txtVolumetricWeight").val((_Volume * 1000000 / intChgWtDividorAirConstant).toFixed(2));
        if ($("#cbIsInland").prop("checked"))
            $("#txtVolumetricWeight").val((_Volume * 1000000 / intChgWtDividorInlandConstant).toFixed(2));
    }
    else {
        $("#txtVolume").val(0);
        $("#txtVolumetricWeight").val(0);
        if ($("#txtVolumetricWeight").val() == "" || $("#txtVolumetricWeight").val() == 0)
            $("#txtChargeableWeight").val($("#txtVolumetricWeight").val());
        else if ($("#txtGrossWeight").val() == "" || $("#txtGrossWeight").val() == 0)
            $("#txtChargeableWeight").val($("#txtVolumetricWeight").val());
        else
            $("#txtChargeableWeight").val(parseFloat($("#txtVolumetricWeight").val()) > parseFloat($("#txtGrossWeight").val())
                ? $("#txtVolumetricWeight").val()
                : $("#txtGrossWeight").val());
    }
}
function OperationContainersAndPackages_CalculateSummary() {
    debugger;
    var decTotalGrossWeight = 0;
    var decTotalVolume = 0;
    var decTotalVolumetricWeight = 0;
    var decTotalQuantity = 0; //used to fill both lblNumberOfPackages and lblQuantity

    if ($("#cbIsLCL").prop('checked') || $("#cbIsBulk").prop('checked') || $("#cbIsLTL").prop('checked') || $("#cbIsAir").prop('checked')) {
        $("#divPackagesSummary").removeClass("hide");
        //$(".GrossWeight").each(function () {
        //    var value = $(this).text();
        //    // add only if the value is number
        //    if (!isNaN(value) && value.length != 0) {
        //        decTotalGrossWeight += parseFloat(value);
        //    }
        //});
        //$(".Volume").each(function () {
        //    debugger;
        //    var value = $(this).text();
        //    // add only if the value is number
        //    if (!isNaN(value) && value.length != 0) {
        //        decTotalVolume += parseFloat(value);
        //    }
        //});
        //$(".VolumetricWeight").each(function () {
        //    var value = $(this).text();
        //    // add only if the value is number
        //    if (!isNaN(value) && value.length != 0) {
        //        decTotalVolumetricWeight += parseFloat(value);
        //    }
        //});
        //$(".Quantity").each(function () {
        //    var value = $(this).text();
        //    // add only if the value is number
        //    if (!isNaN(value) && value.length != 0) {
        //        decTotalQuantity += parseFloat(value);
        //    }
        //});
        //$("#lblTotalGrossWeight").html(": " + decTotalGrossWeight.toFixed(2).toString());
        //$("#lblTotalVolume").html(": " + decTotalVolume.toFixed(2).toString());
        //$("#lblTotalVolumetricWeight").html(": " + decTotalVolumetricWeight.toFixed(2).toString());
        //$("#lblChargeableWeight").html(": "
        //                                + (parseFloat($("#txtCargoChargeableWeight").val()) > 0
        //                                        ? parseFloat($("#txtCargoChargeableWeight").val()).toFixed(2)
        //                                        : (decTotalVolumetricWeight > decTotalGrossWeight ? decTotalVolumetricWeight.toFixed(2).toString() : decTotalGrossWeight.toFixed(2).toString())
        //                                   )
        //                               );
        //$("#lblTotalNumberOfPackages").html(": " + decTotalQuantity.toString());
    }
    else { //FCL or FTL
        $("#divContainersSummary").removeClass("hide");
        $("tblOperationContainersAndPackages tbody tr td.Quantity").each(function () {
            var value = $(this).text();
            // add only if the value is number
            if (!isNaN(value) && value.length != 0) {
                decTotalQuantity += parseFloat(value);
            }
        });
        //$("#lblTotalNumberOfContainers").html(": " + decTotalQuantity.toString());
        //var NumberOfContainers = $('#tblOperationContainersAndPackages tr').length - 1;//document.getElementById("tblOperationContainersAndPackages").getElementsByTagName("tr").length - 1;
        //$("#lblTotalNumberOfContainers").html(": " + NumberOfContainers.toString());
    }
}
function OperationContainersAndPackages_FillLabels(pOperationMasterOrDirect) {
    debugger;
    $("#lblTotalGrossWeight").html(": " + pOperationMasterOrDirect.GrossWeightSum);
    $("#lblTotalVolume").html(": " + pOperationMasterOrDirect.VolumeSum);
    $("#lblChargeableWeight").html(": " + pOperationMasterOrDirect.ChargeableWeightSum);
    $("#lblTotalNumberOfPackages").html(": " + pOperationMasterOrDirect.NumberOfPackages);

    $("#lblTotalNumberOfContainers").html(": " + pOperationMasterOrDirect.ContainerTypes);
}
function OperationContainersAndPackages_ValidateProperties() {
    var isValid = true;
    if (!CheckDecimalPlacesAndNegativeSigns('txtLength') || !CheckDecimalPlacesAndNegativeSigns('txtWidth')
        || !CheckDecimalPlacesAndNegativeSigns('txtHeight') || !CheckDecimalPlacesAndNegativeSigns('txtGrossWeight')
        || !CheckDecimalPlacesAndNegativeSigns('txtNetWeight') || !CheckDecimalPlacesAndNegativeSigns('txtVolumetricWeight')) {
        isValid = false;
        swal(strSorry, "Please, Check the numbers.");
    }
    //if ($("#txtContainerNumber").val().trim() != "" && $("#txtContainerNumber").val().toUpperCase().match("^[A-Z]{4}\[0-9]{7}$") == null) {
    //    isValid = false;
    //    swal(strSorry, "Container Number Format must be like 'ABCD1234567'");
    //}
    if (!CheckDecimalPlacesAndNegativeSigns('txtTareWeight')) {
        isValid = false;
        swal(strSorry, "Please, Revise the tare weight.");
    }
    if (!CheckDecimalPlacesAndNegativeSigns('txtVGM')) {
        isValid = false;
        swal(strSorry, "Please, Revise the VGM.");
    }
    if (!CheckDecimalPlacesAndNegativeSigns('txtMinTemp')) {
        isValid = false;
        swal(strSorry, "Please, Revise the Min. Temperature.");
    }
    if (!CheckDecimalPlacesAndNegativeSigns('txtMaxTemp')) {
        isValid = false;
        swal(strSorry, "Please, Revise the Max. Temperature.");
    }
    if (parseFloat($("#txtMinTemp").val()) > parseFloat($("#txtMaxTemp").val())) {
        isValid = false;
        swal(strSorry, "Please, Max. Temperature can not be less than Min Temperature.");
    }
    if ((!CheckDecimalPlacesAndNegativeSigns('txtIMOClass')) || $("#txtIMOClass").val().trim() != "" && ($("#txtIMOClass").val() < 0 || $("#txtIMOClass").val() > 9.9)) {
        isValid = false;
        swal(strSorry, "IMO Class must be between 0 and 9.9");
    }
    if (!CheckDecimalPlacesAndNegativeSigns('txtHumidity')) {
        isValid = false;
        swal(strSorry, "Please, Revise the humidity.");
    }
    if (!CheckDecimalPlacesAndNegativeSigns('txtVentilation')) {
        isValid = false;
        swal(strSorry, "Please, Revise the Ventilation.");
    }
    if (!CheckDecimalPlacesAndNegativeSigns('txtFlashPoint')) {
        isValid = false;
        swal(strSorry, "Please, Revise the flash point.");
    }
    if ($('#txtNumberOfContainers').val().trim() == "" || $('#txtNumberOfContainers').val().trim() < 1 || $('#txtNumberOfContainers').val().trim() > 50) {
        isValid = false;
        swal(strSorry, "Number of containers must be between 1 and 50.");
    }
    //check for packages quantity
    if (($("#cbIsLCL").prop('checked') || $("#cbIsBulk").prop('checked') || $("#cbIsLTL").prop('checked') || $("#cbIsAir").prop('checked'))
        && ($("#txtPackagesQuantity").val().trim() == "" || $("#txtPackagesQuantity").val().trim() < 1)) {
        isValid = false;
        swal(strSorry, "The Quantity must be greater than 0 !");
    }
    return isValid;
}
function ContainerTypes_GetList(pID, callback) {//the first parameter is used in case of editing to set the code or name to its original value
    //parameters: ID, strFnName, First Row in select list, select list name
    GetListWithCodeAndWhereClause(pID, "/api/ContainerTypes/LoadAll", TranslateString("Type"), "slContainerTypes", " Where 1 = 1 ORDER BY CODE ");
}
function PackageTypesOnContainer_GetList(pID, pSlName, callback) {//the first parameter is used in case of editing to set the code or name to its original value
    var pWhereClause = " Where 1=1 ";
    //the next 2 lines are to prevent selection of already selected packageTypes
    //pWhereClause += " AND ID NOT IN (SELECT PackageTypeID FROM ContainerPackages WHERE OperationContainersAndPackagesID = " + $("#hContainerID").val() + ")";
    //pWhereClause += (pID == null || pID == undefined) ? "" : " OR ID = " + pID.toString(); //incase of editing(i.e. pID has value): to add the edited PackageType to the select list
    GetListWithNameAndWhereClause(pID, "/api/PackageTypes/LoadAll", TranslateString("SelectFromMenu"), pSlName, pWhereClause, callback);
}
function OperationContainersAndPackages_RebuildPackages() {
    debugger;
    var NumberOfTableRows = $('#tblOperationContainersAndPackages tr').length - 1;//document.getElementById("tblOperationContainersAndPackages").getElementsByTagName("tr").length - 1;
    if (parseInt($('#hNumberOfHousesConnected').val()) < 1)
        swal(strSorry, "No Houses is connected to this operation.");
    else
        if ($("#cbIsConsolidation").prop("checked") && NumberOfTableRows < 1)
            swal(strSorry, "Please, Add at least 1 container.");
        else {
            swal({
                title: "Are you sure?",
                text: "The Packages for Operation '" + $("#txtOperationCode").val() + "' will be Re-Built.",
                //type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Re-Build",
                closeOnConfirm: true
            },
                //callback function in case of Confirm
                function () {
                    if (!$("#cbIsConsolidation").prop("checked")) { //this works with Master(FCL,FTL,LCL,LTL,Air) BUT NOT Consolidation Masters
                        var pSelectedIDs = GetAllSelectedIDsAsStringWithNameAttr("ConnectOrDisconnect");
                        CallGETFunctionWithParameters("/api/OperationContainersAndPackages/RebuildContainersAndPackages", { pIsRemoveHousePackages: false, pMasterOperationID: $("#hOperationID").val(), pHouseOperationsIDs: pSelectedIDs }
                            , function () {
                                OperationContainersAndPackages_Search();
                                Shipments_LoadAvailableShipments(); //to enable disable the Connect/Disconnect checkbox
                            });
                    }
                    else { //Consolidation Masters
                        //i am sure i have at least 1 container
                        if (NumberOfTableRows == 1) {//if just 1 container then add Automatically Connected House Packages to it
                            var pMasterOperationContainersAndPackagesID = $("#tblOperationContainersAndPackages tr:eq(1)").attr("id"); //i am sure i have 1 container so tr[1] is what i want (note: tr[0] is the header)
                            var pSelectedIDs = GetAllSelectedIDsAsStringWithNameAttr("ConnectOrDisconnect");
                            CallGETFunctionWithParameters("/api/OperationContainersAndPackages/RebuildContainersAndPackages_Consolidation", { pMasterConsolidationOperationID: $("#hOperationID").val(), pMasterOperationContainersAndPackagesID: pMasterOperationContainersAndPackagesID, pHouseOperationsIDs: pSelectedIDs, pWhoIsCalling: 0 }  //pWhoIsCalling: refer to it in the controller before the api fn name
                                , function () {
                                    OperationContainersAndPackages_Search();
                                    Shipments_LoadAvailableShipments(); //to enable disable the Connect/Disconnect checkbox
                                });
                        }
                        else { //i have more than 1 container
                            debugger;
                            jQuery("#RebuildConsolidationModal").modal("show");
                            RebuildConsolidation_ClearAllControls();
                            RebuildConsolidation_LoadWithPaging();
                        }
                    }
                });
        }
}
function OperationContainersAndPackages_RemovePackages() {
    if (parseInt($('#hNumberOfHousesConnected').val()) < 1)
        swal(strSorry, "No Houses is connected to this operation.");
    else {
        swal({
            title: "Are you sure?",
            text: "The Packages of Connected Houses to Operation '" + $("#txtOperationCode").val() + "' will be Re-Removed.",
            //type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, Remove",
            closeOnConfirm: true
        },
            //callback function in case of Confirm
            function () {
                if (!$("#cbIsConsolidation").prop("checked")) { //this works with Master(FCL,FTL,LCL,LTL,Air) BUT NOT Consolidation Masters
                    var pSelectedIDs = GetAllSelectedIDsAsStringWithNameAttr("ConnectOrDisconnect");
                    CallGETFunctionWithParameters("/api/OperationContainersAndPackages/RebuildContainersAndPackages", { pIsRemoveHousePackages: true, pMasterOperationID: $("#hOperationID").val(), pHouseOperationsIDs: pSelectedIDs }
                        , function () {
                            OperationContainersAndPackages_Search();
                            Shipments_LoadAvailableShipments(); //to enable disable the Connect/Disconnect checkbox
                        });
                }
                else { //Consolidation Masters
                    //this case wont be reached unless Consol. & 1 container
                    var pMasterOperationContainersAndPackagesID = 0;//coz i am removing all the connected house packages (1 Container)
                    var pSelectedIDs = GetAllSelectedIDsAsStringWithNameAttr("ConnectOrDisconnect"); //all the houses to remove their packages
                    CallGETFunctionWithParameters("/api/OperationContainersAndPackages/RebuildContainersAndPackages_Consolidation", { pMasterConsolidationOperationID: $("#hOperationID").val(), pMasterOperationContainersAndPackagesID: pMasterOperationContainersAndPackagesID, pHouseOperationsIDs: pSelectedIDs, pWhoIsCalling: 0 }  //pWhoIsCalling: refer to it in the controller before the api fn name
                        , function () {
                            OperationContainersAndPackages_Search();
                        });
                }
            });
    }
}
function OperationContainersAndPackages_TransferContainerModal(pTransferredContainerID, pOriginalOperationID) {
    debugger;
    if ($("#slTransferContainerToOperation option").length < 2) {
        FadePageCover(true);
        CallGETFunctionWithParameters("/api/Operations/LoadOperationsToRestoreInvoices"
            , { pPageSize: 99999, pWhereClauseToGetOperationsToRestoreInvoices: "WHERE EffectiveOperationCode IS NOT NULL AND ShipmentType IN (1,3,5) AND OperationStageID NOT IN (" + ClosedQuoteAndOperStageID + "," + CancelledQuoteAndOperStageID + ") AND CreationDate > DATEADD(mm,DATEDIFF(mm,0,GETDATE())-12,0)", pOrderBy: "ID DESC" }
            , function (pData) {
                FillListFromObject(null, 13, "<--Select-->", "slTransferContainerToOperation", pData[0], function () { FadePageCover(false); });
            }
            , null);
    }
    $("#btnTransferContainerToOperation").attr("onclick", "OperationContainersAndPackages_TransferToOperation(" + pTransferredContainerID + "," + pOriginalOperationID + ");");
    jQuery("#TransferContainerModal").modal("show");
}
function OperationContainersAndPackages_TransferToOperation(pTransferredContainerID, pOriginalOperationID) {
    debugger;
    if ($("#slTransferContainerToOperation").val() == "")
        swal("", "Please, select an operation to transfer the container to it.");
    else {
        swal({
            title: "Are you sure?",
            text: "The container will be transferred.",
            type: "",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, transfer!",
            closeOnConfirm: true
        },
            function () {
                FadePageCover(true);
                var pParametersWithValues = {
                    pTransferredContainerID: pTransferredContainerID
                    , pOriginalOperationID: pOriginalOperationID
                    , pTransferToOperationID: $("#slTransferContainerToOperation").val()
                };
                CallGETFunctionWithParameters("/api/OperationContainersAndPackages/TransferContainer", pParametersWithValues
                    , function (pData) {
                        var _MessageReturned = pData[0];
                        if (_MessageReturned == "") {
                            OperationContainersAndPackages_Search();
                            swal("Success", "Transferred successfully.");
                            jQuery("#TransferContainerModal").modal("hide");
                        }
                        else {
                            swal("Sorry", _MessageReturned);
                            FadePageCover(false);
                        }
                    }
                    , null);
            });
    }
}
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////CargoProperties Fns////////////////////////////////////////////////////////////
//pOption 10:Master, 22:House
function CargoProperties_FillControls(pOption) {
    debugger;
    var pOperationCargoID = 0;
    if (pOption == 10) {
        $(".classHideForMaster").addClass("hide");
        pOperationCargoID = $("#hOperationID").val();
    }
    else if (pOption == 20) {
        $(".classHideForMaster").removeClass("hide");
        pOperationCargoID = $("#hShipmentID").val();
    }
    if (pOperationCargoID == "")
        swal("Sorry", "Please, save house first.");
    else {
        ClearAll("#CargoPropertiesModal");
        FadePageCover(true);
        jQuery("#CargoPropertiesModal").modal("show");
        var pParametersWithValues = {
            pOperationIDForCargo: pOperationCargoID
            , pMasterOperationID: $("#hOperationID").val() //to always containrs on master
            , pOrderBy: "ID"
        };
        CallGETFunctionWithParameters("/api/Operations/LoadCargoProperties"
            , pParametersWithValues
            , function (pData) {
                $("#btn-SaveCargoProperties").attr("onclick", "CargoProperties_Save(" + pOperationCargoID + ");")
                var pOperationHeader = JSON.parse(pData[0]);
                var pOperationContainersAndPackages = pData[1];
                var pPackageType = pData[2];
                FillListFromObject(pOperationHeader.PackageTypeID, 2, "<--Select-->", "slCargoPackageTypes", pPackageType, null);
                FillListFromObject(pOperationHeader.PlacedOnOperationContainersAndPackagesID, 11, "<--Select-->", "slCargoPlacedOnContainer", pOperationContainersAndPackages, null);
                $("#txtCargoTareWeight").val(pOperationHeader.TareWeight);
                $("#txtCargoVolume").val(pOperationHeader.Volume);
                $("#txtCargoNetWeight").val(pOperationHeader.NetWeight);
                $("#txtCargoNetWeightTON").val(pOperationHeader.NetWeightTON);
                $("#txtCargoGrossWeight").val(pOperationHeader.GrossWeight);
                $("#txtCargoGrossWeightTON").val(pOperationHeader.GrossWeightTON);
                $("#txtCargoVGM").val(pOperationHeader.VGM);
                $("#txtCargoChargeableWeight").val(pOperationHeader.ChargeableWeight);
                $("#txtCargoVolumetricWeight").val(pOperationHeader.VolumetricWeight);
                $("#divCargoMarksAndNumbers").val(pOperationHeader.MarksAndNumbers == 0 ? "" : pOperationHeader.MarksAndNumbers);
                $("#divCargoGoodsDescription").val(pOperationHeader.Description == 0 ? "" : pOperationHeader.Description);
                $("#txtCargoNumberOfPackages").val(pOperationHeader.NumberOfPackages);
                FadePageCover(false);
            }
            , null);
    }
}
function CargoProperties_Save(pOperationCargoID) {
    debugger;
    FadePageCover(true);
    var pParametersWithValues = {
        pOperationID: pOperationCargoID
        , pTareWeight: $("#txtCargoTareWeight").val() == "" ? 0 : $("#txtCargoTareWeight").val()
        , pGrossWeight: $("#txtCargoGrossWeight").val() == "" ? 0 : $("#txtCargoGrossWeight").val()
        , pGrossWeightTON: $("#txtCargoGrossWeightTON").val() == "" ? 0 : $("#txtCargoGrossWeightTON").val()
        , pVGM: $("#txtCargoVGM").val() == "" ? 0 : $("#txtCargoVGM").val()
        , pNetWeight: $("#txtCargoNetWeight").val() == "" ? 0 : $("#txtCargoNetWeight").val()
        , pNetWeightTON: $("#txtCargoNetWeightTON").val() == "" ? 0 : $("#txtCargoNetWeightTON").val()
        , pVolume: $("#txtCargoVolume").val() == "" ? 0 : $("#txtCargoVolume").val()
        , pChargeableWeight: $("#txtCargoChargeableWeight").val() == "" ? 0 : $("#txtCargoChargeableWeight").val()
        , pVolumetricWeight: $("#txtCargoVolumetricWeight").val() == "" ? 0 : $("#txtCargoVolumetricWeight").val()
        , pNumberOfPackages: $("#txtCargoNumberOfPackages").val() == "" ? 0 : $("#txtCargoNumberOfPackages").val()
        , pPackageTypeID: $("#slCargoPackageTypes").val() == "" ? 0 : $("#slCargoPackageTypes").val()
        , pPlacedOnOperationContainersAndPackagesID: $("#slCargoPlacedOnContainer").val() == "" ? 0 : $("#slCargoPlacedOnContainer").val()
        , pMarksAndNumbers: $("#divCargoMarksAndNumbers").val().trim() == "" ? 0 : $("#divCargoMarksAndNumbers").val().trim().toUpperCase()
        , pDescription: $("#divCargoGoodsDescription").val().trim() == "" ? 0 : $("#divCargoGoodsDescription").val().trim().toUpperCase()
    };
    CallGETFunctionWithParameters("/api/OperationContainersAndPackages/CargoProperties_Save"
        , pParametersWithValues
        , function (pData) {
            var _MessageReturned = pData[0];
            if (_MessageReturned == "") {
                swal("Success", "Saved successfully");
                var pOperationMasterOrDirect = JSON.parse(pData[2]);
                jQuery("#CargoPropertiesModal").modal("hide");
                //if (parseFloat($("#txtCargoChargeableWeight").val()) != 0 && $("#txtCargoChargeableWeight").val() != "")
                //    $("#lblChargeableWeight").html(": " + parseFloat($("#txtCargoChargeableWeight").val()).toFixed(2));
                //else
                //    OperationContainersAndPackages_CalculateSummary();
                OperationContainersAndPackages_FillLabels(pOperationMasterOrDirect);
            }
            else
                swal("Sorry", _MessageReturned);
            FadePageCover(false);
        }
        , null);
}
function CargoProperties_CalculateVGM() {
    debugger;
    var pTareWeight = isNaN(parseFloat($("#txtCargoTareWeight").val())) ? 0 : parseFloat($("#txtCargoTareWeight").val());
    var pGrossWeight = isNaN(parseFloat($("#txtCargoGrossWeight").val())) ? 0 : parseFloat($("#txtCargoGrossWeight").val());
    $("#txtCargoVGM").val(pTareWeight + pGrossWeight);
}
function OperationContainersAndPackages_CalculateVGM(pTareWeightControl, pGrossWeightControl, pVGMControl) {
    debugger;
    var pTareWeight = isNaN(parseFloat($("#" + pTareWeightControl).val())) ? 0 : parseFloat($("#" + pTareWeightControl).val());
    var pGrossWeight = isNaN(parseFloat($("#" + pGrossWeightControl).val())) ? 0 : parseFloat($("#" + pGrossWeightControl).val());
    $("#" + pVGMControl).val(pTareWeight + pGrossWeight);
}
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////ContainerPackages Fns////////////////////////////////////////////////////////////
function ContainerPackages_BindTableRows(pContainerPackages) {
    ClearAllTableRows("tblContainerPackages");
    //editControlsText = " class='btn btn-xs btn-rounded btn-info float-right' > <i class='fa fa-pencil' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + TranslateString("Edit") + "</span>";
    $.each(pContainerPackages, function (i, item) {
        AppendRowtoTable("tblContainerPackages",
            ("<tr ID='" + item.ID + "' ondblclick='ContainerPackages_EditByDblClick(" + item.ID + "," + item.OperationID + ");'>"
                + "<td class='ID'> <input name='Delete' type='checkbox' value='" + item.ID + "' /></td>"

                + ($("#hBLType").val() == constMasterBLType ? "<td class='HouseOperationCode'>" + (item.HouseOperationCode == 0 ? "" : item.HouseOperationCode) + "</td>" : "")
                + "<td class='ContainerPackage' val='" + item.PackageTypeID + "'>" + item.PackageTypeName + "</td>"
                + "<td class='ContainerPackageQuantity'>" + item.Quantity + "</td>"
                + "<td class='ContainerPackageLength hide'>" + (item.Length == 0 ? "" : item.Length) + "</td>"
                + "<td class='ContainerPackageWidth hide'>" + (item.Width == 0 ? "" : item.Width) + "</td>"
                + "<td class='ContainerPackageHeight hide'>" + (item.Height == 0 ? "" : item.Height) + "</td>"
                + "<td class='ContainerPackageVolume'>" + (item.Volume == 0 ? "" : item.Volume) + "</td>"
                + "<td class='ContainerPackageVolumetricWeight hide'>" + (item.VolumetricWeight == 0 ? "" : item.VolumetricWeight) + "</td>"
                + "<td class='ContainerPackageNetWeight hide'>" + (item.NetWeight == 0 ? "" : item.NetWeight) + "</td>"
                + "<td class='ContainerPackageGrossWeight'>" + (item.GrossWeight == 0 ? "" : item.GrossWeight) + "</td>"
                + "<td class='ContainerPackageChargeableWeight hide'>" + (item.ChargeableWeight == 0 ? "" : item.ChargeableWeight) + "</td>"
                + "<td class='ContainerPackageDescriptionOfGoods'>" + (item.DescriptionOfGoods == 0 ? "" : item.DescriptionOfGoods) + "</td>"
                + "<td class='ContainerPackageMarksAndNumbers hide'>" + (item.MarksAndNumbers == 0 ? "" : item.MarksAndNumbers) + "</td>"

                + "<td class='hide'><a href='#EditContainerPackageModal' data-toggle='modal' onclick='ContainerPackages_FillControls(" + item.ID + "," + item.OperationID + ");' " + editControlsText + "</a></td></tr>"));
    });
    ShowHidettblContainerPackagesHeaders();
    //ApplyPermissions();
    BindAllCheckboxonTable("tblContainerPackages", "ID", "cb-CheckAll-ContainerPackages");
    CheckAllCheckbox("HeaderDeleteContainerPackagesID");
    HighlightText("#tblContainerPackages>tbody>tr", $("#txtContainerPackagesSearch").val().trim());
    if (showDeleteFailMessage) {//sherif: if deletion failed due to foreign key, then this message is shown
        swal(strSorry, strDeleteFailMessage, "warning");
        showDeleteFailMessage = false;
    }
    ContainerPackages_CalculateSummary();
}
function ShowHidettblContainerPackagesHeaders() {
    if ($("#hBLType").val() == constMasterBLType) //if master then show house header for packages
        $("#ContainerPackagesHouseCodeHeader").removeClass("hide");
    else
        $("#ContainerPackagesHouseCodeHeader").addClass("hide");
}
function ContainerPackages_EditByDblClick(pID, pOperationID) {
    jQuery("#EditContainerPackageModal").modal("show");
    ContainerPackages_FillControls(pID, pOperationID);
}
function ContainerPackages_LoadWithPagingWithWhereClauseAndOrderBy() {
    LoadWithPagingWithWhereClauseAndOrderBy("div-Pager", "select-page-size", "spn-first-page-row", "spn-last-page-row", "spn-total-count", "div-Text-Total", "/api/ContainerPackages/LoadWithWhereClause", " WHERE OperationContainersAndPackagesID = " + $("#hContainerID").val(), " HouseOperationCode, PackageTypeName ", 1, 1000, function (data) { ContainerPackages_BindTableRows(JSON.parse(data[0])/*pTabelRows*/); });
}
function ContainerPackages_ClearAllControls(callback, pOperationID) {
    debugger;
    if ($("#hContainerID").val() == "")
        swal("Sorry", "Please, save container first.");
    else {
        ClearAll("#EditContainerPackageModal");
        ContainerPackageTypes_GetList(null, "slContainerPackageTypes", null);
        $("#txtContainerPackageQuantity").val(1);

        $("#btnSaveContainerPackage").attr("onclick", "ContainerPackages_Insert(false," + pOperationID + ");");
        $("#btnSaveAndNewContainerPackage").attr("onclick", "ContainerPackages_Insert(true," + pOperationID + ");");

        if (callback != null && callback != undefined)
            callback();
    }
}
function ContainerPackages_FillControls(pID, pOperationID) {

    ClearAll("#EditContainerPackageModal");
    debugger;
    var tr = $("#tblContainerPackages tr[ID='" + pID + "']");

    $("#lblContainerPackageShown").html(": " + $(tr).find("td.ContainerPackage").text());

    var pContainerPackageTypeID = $(tr).find("td.ContainerPackage").attr('val');
    ContainerPackageTypes_GetList(pContainerPackageTypeID, "slContainerPackageTypes", null);

    $("#hContainerPackageID").val(pID);

    $("#txtContainerPackageQuantity").val($(tr).find("td.ContainerPackageQuantity").text());
    $("#txtContainerPackageLength").val($(tr).find("td.ContainerPackageLength").text());
    $("#txtContainerPackageWidth").val($(tr).find("td.ContainerPackageWidth").text());
    $("#txtContainerPackageHeight").val($(tr).find("td.ContainerPackageHeight").text());
    $("#txtContainerPackageVolume").val($(tr).find("td.ContainerPackageVolume").text());
    $("#txtContainerPackageNetWeight").val($(tr).find("td.ContainerPackageNetWeight").text());
    $("#txtContainerPackageGrossWeight").val($(tr).find("td.ContainerPackageGrossWeight").text());
    $("#txtContainerPackageVolumetricWeight").val($(tr).find("td.ContainerPackageVolumetricWeight").text());
    $("#txtContainerPackageMarksAndNumbers").val($(tr).find("td.ContainerPackageMarksAndNumbers").text());
    $("#txtContainerPackageDescriptionOfGoods").val($(tr).find("td.ContainerPackageDescriptionOfGoods").text());

    $("#btnSaveContainerPackage").attr("onclick", "ContainerPackages_Update(false," + pOperationID + ");");
    $("#btnSaveAndNewContainerPackage").attr("onclick", "ContainerPackages_Update(true," + pOperationID + ");");
}
//insert ContainerPackage
function ContainerPackages_Insert(pSaveandAddNew, pOperationID) {
    debugger;
    if (ContainerPackages_ValidateProperties())
        InsertUpdateFunction("form", "/api/ContainerPackages/Insert", {
            pOperationID: pOperationID //$("#hOperationID").val()
            , pOperationContainersAndPackagesID: $("#hContainerID").val()
            , pPackageTypeID: ($("#slContainerPackageTypes option:selected").val() == "" ? 0 : $("#slContainerPackageTypes option:selected").val())
            , pQuantity: ($("#txtContainerPackageQuantity").val() == "" ? 0 : $("#txtContainerPackageQuantity").val())
            , pLength: ($("#txtContainerPackageLength").val() == "" ? 0 : $("#txtContainerPackageLength").val())
            , pWidth: ($("#txtContainerPackageWidth").val() == "" ? 0 : $("#txtContainerPackageWidth").val())
            , pHeight: ($("#txtContainerPackageHeight").val() == "" ? 0 : $("#txtContainerPackageHeight").val())
            , pVolume: ($("#txtContainerPackageVolume").val() == "" ? 0 : $("#txtContainerPackageVolume").val())
            , pNetWeight: ($("#txtContainerPackageNetWeight").val() == "" ? 0 : $("#txtContainerPackageNetWeight").val())
            , pGrossWeight: ($("#txtContainerPackageGrossWeight").val() == "" ? 0 : $("#txtContainerPackageGrossWeight").val())
            , pVolumetricWeight: ($("#txtContainerPackageVolumetricWeight").val() == "" ? 0 : $("#txtContainerPackageVolumetricWeight").val())
            //, pChargeableWeight: ($("#txtContainerPackageChargeableWeight").val() == "" ? 0 : $("#txtContainerPackageChargeableWeight").val())
            , pMarksAndNumbers: ($("#txtContainerPackageMarksAndNumbers").val().trim() == "" ? 0 : $("#txtContainerPackageMarksAndNumbers").val().trim().toUpperCase())
            , pDescriptionOfGoods: ($("#txtContainerPackageDescriptionOfGoods").val().trim() == "" ? 0 : $("#txtContainerPackageDescriptionOfGoods").val().trim().toUpperCase())
        }, pSaveandAddNew
            , "EditContainerPackageModal" //Modal name
            , function () {
                ContainerPackages_LoadWithPagingWithWhereClauseAndOrderBy();
                if (pSaveandAddNew)
                    ContainerPackages_ClearAllControls(null, pOperationID);
            });
}
function ContainerPackages_Update(pSaveandAddNew, pOperationID) {
    debugger;
    if (ContainerPackages_ValidateProperties())
        InsertUpdateFunction("form", "/api/ContainerPackages/Update", {
            pID: $("#hContainerPackageID").val()
            , pOperationID: pOperationID //$("#hOperationID").val()
            , pOperationContainersAndPackagesID: $("#hContainerID").val()
            , pPackageTypeID: ($("#slContainerPackageTypes option:selected").val() == "" ? 0 : $("#slContainerPackageTypes option:selected").val())
            , pQuantity: ($("#txtContainerPackageQuantity").val() == "" ? 0 : $("#txtContainerPackageQuantity").val())
            , pLength: ($("#txtContainerPackageLength").val() == "" ? 0 : $("#txtContainerPackageLength").val())
            , pWidth: ($("#txtContainerPackageWidth").val() == "" ? 0 : $("#txtContainerPackageWidth").val())
            , pHeight: ($("#txtContainerPackageHeight").val() == "" ? 0 : $("#txtContainerPackageHeight").val())
            , pVolume: ($("#txtContainerPackageVolume").val() == "" ? 0 : $("#txtContainerPackageVolume").val())
            , pNetWeight: ($("#txtContainerPackageNetWeight").val() == "" ? 0 : $("#txtContainerPackageNetWeight").val())
            , pGrossWeight: ($("#txtContainerPackageGrossWeight").val() == "" ? 0 : $("#txtContainerPackageGrossWeight").val())
            , pVolumetricWeight: ($("#txtContainerPackageVolumetricWeight").val() == "" ? 0 : $("#txtContainerPackageVolumetricWeight").val())
            //, pChargeableWeight: ($("#txtContainerPackageChargeableWeight").val() == "" ? 0 : $("#txtContainerPackageChargeableWeight").val())
            , pMarksAndNumbers: ($("#txtContainerPackageMarksAndNumbers").val().trim() == "" ? 0 : $("#txtContainerPackageMarksAndNumbers").val().trim().toUpperCase())
            , pDescriptionOfGoods: ($("#txtContainerPackageDescriptionOfGoods").val().trim() == "" ? 0 : $("#txtContainerPackageDescriptionOfGoods").val().trim().toUpperCase())
        }, pSaveandAddNew
            , "EditContainerPackageModal" //Modal name
            , function () {
                ContainerPackages_LoadWithPagingWithWhereClauseAndOrderBy();
                if (pSaveandAddNew)
                    ContainerPackages_ClearAllControls(null, pOperationID);
            });
}
function ContainerPackages_DeleteList(callback) {
    //Confirmation message to delete
    if (GetAllSelectedIDsAsString('tblContainerPackages') != "")
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
                DeleteListFunction("/api/ContainerPackages/Delete"
                    , { "pContainerPackagesIDs": GetAllSelectedIDsAsString('tblContainerPackages') }
                    , function () {
                        ContainerPackages_LoadWithPagingWithWhereClauseAndOrderBy();
                        if ($("#cbIsConsolidation").prop('checked'))
                            Shipments_LoadAvailableShipments();
                    });
            });
}
function ContainerPackages_Search() {
    debugger;
    var pSearchKey = $("#txtContainerPackagesSearch").val().trim().toUpperCase();
    var pWhereClause = " WHERE OperationContainersAndPackagesID = " + $("#hContainerID").val()
        + " AND  (PackageTypeName LIKE '%" + pSearchKey + "%' "
        //+ " OR MarksAndNumbers LIKE '%" + pSearchKey + "%' "
        + " OR DescriptionOfGoods LIKE '%" + pSearchKey + "%') "
    var pOrderBy = " HouseOperationCode, PackageTypeName ";
    LoadWithPagingWithWhereClauseAndOrderBy("div-Pager", "select-page-size", "spn-first-page-row", "spn-last-page-row", "spn-total-count", "div-Text-Total", "/api/ContainerPackages/LoadWithWhereClause", pWhereClause, pOrderBy, 1, 1000, function (data) { ContainerPackages_BindTableRows(JSON.parse(data[0])/*pTabelRows*/); });
}
function ContainerPackageTypes_GetList(pID, pSlName, callback) {//the first parameter is used in case of editing to set the code or name to its original value
    var pWhereClause = " Where 1=1 ";
    //the next 2 lines are to prevent selection of already selected packageTypes
    //pWhereClause += " AND ID NOT IN (SELECT PackageTypeID FROM ContainerPackages WHERE OperationContainersAndPackagesID = " + $("#hContainerID").val() + ")";
    //pWhereClause += (pID == null || pID == undefined) ? "" : " OR ID = " + pID.toString(); //incase of editing(i.e. pID has value): to add the edited PackageType to the select list
    GetListWithNameAndWhereClause(pID, "/api/PackageTypes/LoadAll", TranslateString("SelectFromMenu"), pSlName, pWhereClause);
}
function ContainerPackages_CalculateSummary() {
    debugger;
    var decTotalContainerPackageSQuantity = 0;
    $(".ContainerPackageQuantity").each(function () {
        var value = $(this).text();
        // add only if the value is number
        if (!isNaN(value) && value.length != 0) {
            decTotalContainerPackageSQuantity += parseFloat(value);
        }
    });
    $("#lblTotalNumberOfContainerPackages").html(": " + decTotalContainerPackageSQuantity.toString());
}
function ContainerPackages_ValidateProperties() {
    var isValid = true;
    if (!CheckDecimalPlacesAndNegativeSigns('txtContainerPackageGrossWeight')
        || !CheckDecimalPlacesAndNegativeSigns('txtContainerPackageVolume')
        || !CheckDecimalPlacesAndNegativeSigns('txtContainerPackageLength')
        || !CheckDecimalPlacesAndNegativeSigns('txtContainerPackageWidth')
        || !CheckDecimalPlacesAndNegativeSigns('txtContainerPackageHeight')
        || !CheckDecimalPlacesAndNegativeSigns('txtContainerPackageNetWeight')
        || !CheckDecimalPlacesAndNegativeSigns('txtContainerPackageVolumetricWeight')
        //|| !CheckDecimalPlacesAndNegativeSigns('txtContainerPackageChargeableWeight')
    ) {
        isValid = false;
        swal(strSorry, "Please, Check the numbers.");
    }
    //check for packages quantity (NOT CONTAINER PACKAGE QUANTITY)
    if ($("#txtContainerPackageQuantity").val().trim() == "" || $("#txtContainerPackageQuantity").val().trim() < 1) {
        isValid = false;
        swal(strSorry, "The Quantity must be greater than 0 !");
    }
    return isValid;
}
function ContainerPackages_CalculateVolume() {
    debugger;
    if ($("#txtContainerPackageLength").val() != "" && $("#txtContainerPackageWidth").val() != "" && $("#txtContainerPackageHeight").val() != "" && $("#txtContainerPackageQuantity").val() != "") {
        $("#txtContainerPackageVolume").val((($("#txtContainerPackageLength").val() * $("#txtContainerPackageWidth").val() * $("#txtContainerPackageHeight").val() * $("#txtContainerPackageQuantity").val()) / 1000000).toFixed(2));
        ////if Ocean then no volumetric weight
        //if ($("#cbIsAir").prop("checked"))
        //    $("#txtVolumetricWeight").val(($("#txtVolume").val() * 1000000 / intChgWtDividorAirConstant).toFixed(2));
        //if ($("#cbIsInland").prop("checked"))
        //    $("#txtVolumetricWeight").val(($("#txtVolume").val() * 1000000 / intChgWtDividorInlandConstant).toFixed(2));
        $("#txtContainerPackageVolumetricWeight").val(0); //This is Ocean so no volumetric
    }
    else {
        $("#txtContainerPackageVolume").val(0);
        $("#txtContainerPackageVolumetricWeight").val(0);
    }
    if ($("#txtVolumetricWeight").val() == "" || $("#txtVolumetricWeight").val() == 0)
        $("#txtChargeableWeight").val($("#txtVolumetricWeight").val());
    else if ($("#txtGrossWeight").val() == "" || $("#txtGrossWeight").val() == 0)
        $("#txtChargeableWeight").val($("#txtVolumetricWeight").val());
    else
        $("#txtChargeableWeight").val(parseFloat($("#txtVolumetricWeight").val()) > parseFloat($("#txtGrossWeight").val())
            ? $("#txtVolumetricWeight").val()
            : $("#txtGrossWeight").val());
}

//*********************************Reading Excel Files***************************************//
//Must be saved as Excel 97-2003
function OperationContainersAndPackages_onFileSelected(event, pBtnName) {
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
                if (oJS.length > 0 && oJS[0].ContainerNumber != undefined) //if (sCSV != "")
                    OperationContainersAndPackages_ImportFromExcelFile(oJS, pBtnName);
                else {
                    swal("Sorry", "Please, revise data and version of the file.");
                    $("#" + pBtnName).val("");
                }
            });
        };
        // Tell JS To Start Reading The File.. You could delay this if desired
        reader.readAsBinaryString(selectedFile);
    }
}
function OperationContainersAndPackages_ImportFromExcelFile(pDataRows, pBtnName) {
    debugger;
    FadePageCover(true);
    var pContainerNumberList = "";
    var pContainerTypeCodeList = "";
    var pCarrierSealList = "";
    var pTareWeightList = "";
    var pVolumeList = "";
    var pNetWeightList = "";
    var pGrossWeightList = "";
    var pVGMList = "";
    var pDescriptionOfGoodsList = "";
    var pNumberOfPackagesList = "";
    var pPackageTypeList = "";
    for (var i = 0; i < pDataRows.length; i++) { //replace '\', ' ', ',' with space
        pContainerNumberList += (pContainerNumberList == ""
            ? (pDataRows[i].ContainerNumber == undefined || pDataRows[i].ContainerNumber.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].ContainerNumber.replace(/[\, ]/g, ' ').toUpperCase().trim().slice(0, 11))
            : ("," + (pDataRows[i].ContainerNumber == undefined || pDataRows[i].ContainerNumber.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].ContainerNumber.replace(/[\, ]/g, ' ').toUpperCase().trim().slice(0, 11)))
        );
        pContainerTypeCodeList += (pContainerTypeCodeList == ""
            ? (pDataRows[i].ContainerTypeCode == undefined || pDataRows[i].ContainerTypeCode.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].ContainerTypeCode.replace(/[\, ]/g, ' ').toUpperCase().trim().slice(0, 11))
            : ("," + (pDataRows[i].ContainerTypeCode == undefined || pDataRows[i].ContainerTypeCode.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].ContainerTypeCode.replace(/[\, ]/g, ' ').toUpperCase().trim().slice(0, 11)))
        );
        pCarrierSealList += (pCarrierSealList == ""
            ? (pDataRows[i].CarrierSeal == undefined || pDataRows[i].CarrierSeal.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].CarrierSeal.replace(/[\, ]/g, ' ').toUpperCase().trim().slice(0, 15))
            : ("," + (pDataRows[i].CarrierSeal == undefined || pDataRows[i].CarrierSeal.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].CarrierSeal.replace(/[\, ]/g, ' ').toUpperCase().trim().slice(0, 15)))
        );
        pTareWeightList += (pTareWeightList == ""
            ? (pDataRows[i].TareWeight == undefined || pDataRows[i].TareWeight.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].TareWeight.replace(/[\, ]/g, ' ').toUpperCase().trim())
            : ("," + (pDataRows[i].TareWeight == undefined || pDataRows[i].TareWeight.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].TareWeight.replace(/[\, ]/g, ' ').toUpperCase().trim()))
        );
        pVolumeList += (pVolumeList == ""
            ? (pDataRows[i].Volume == undefined || pDataRows[i].Volume.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].Volume.replace(/[\, ]/g, ' ').toUpperCase().trim())
            : ("," + (pDataRows[i].Volume == undefined || pDataRows[i].Volume.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].Volume.replace(/[\, ]/g, ' ').toUpperCase().trim()))
        );
        pNetWeightList += (pNetWeightList == ""
            ? (pDataRows[i].NetWeight == undefined || pDataRows[i].NetWeight.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].NetWeight.replace(/[\, ]/g, ' ').toUpperCase().trim())
            : ("," + (pDataRows[i].NetWeight == undefined || pDataRows[i].NetWeight.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].NetWeight.replace(/[\, ]/g, ' ').toUpperCase().trim()))
        );
        pGrossWeightList += (pGrossWeightList == ""
            ? (pDataRows[i].GrossWeight == undefined || pDataRows[i].GrossWeight.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].GrossWeight.replace(/[\, ]/g, ' ').toUpperCase().trim())
            : ("," + (pDataRows[i].GrossWeight == undefined || pDataRows[i].GrossWeight.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].GrossWeight.replace(/[\, ]/g, ' ').toUpperCase().trim()))
        );
        pVGMList += (pVGMList == ""
            ? (pDataRows[i].VGM == undefined || pDataRows[i].VGM.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].VGM.replace(/[\, ]/g, ' ').toUpperCase().trim())
            : ("," + (pDataRows[i].VGM == undefined || pDataRows[i].VGM.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].VGM.replace(/[\, ]/g, ' ').toUpperCase().trim()))
        );
        pDescriptionOfGoodsList += (pDescriptionOfGoodsList == ""
            ? (pDataRows[i].DescriptionOfGoods == undefined || pDataRows[i].DescriptionOfGoods.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].DescriptionOfGoods.replace(/[\, ]/g, ' ').toUpperCase().trim())
            : ("," + (pDataRows[i].DescriptionOfGoods == undefined || pDataRows[i].DescriptionOfGoods.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].DescriptionOfGoods.replace(/[\, ]/g, ' ').toUpperCase().trim()))
        );
        pNumberOfPackagesList += (pNumberOfPackagesList == ""
            ? (pDataRows[i].NumberOfPackages == undefined || pDataRows[i].NumberOfPackages.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].NumberOfPackages.replace(/[\, ]/g, ' ').toUpperCase().trim())
            : ("," + (pDataRows[i].NumberOfPackages == undefined || pDataRows[i].NumberOfPackages.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].NumberOfPackages.replace(/[\, ]/g, ' ').toUpperCase().trim()))
        );
        pPackageTypeList += (pPackageTypeList == ""
            ? (pDataRows[i].PackageType == undefined || pDataRows[i].PackageType.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].PackageType.replace(/[\, ]/g, ' ').toUpperCase().trim())
            : ("," + (pDataRows[i].PackageType == undefined || pDataRows[i].PackageType.replace(/[\, ]/g, ' ').trim() == "" ? 0 : pDataRows[i].PackageType.replace(/[\, ]/g, ' ').toUpperCase().trim()))
        );
    }
    var pParametersWithValues = {
        pOperationID: $("#hOperationID").val()
        , pContainerNumberList: pContainerNumberList
        , pContainerTypeCodeList: pContainerTypeCodeList
        , pCarrierSealList: pCarrierSealList
        , pTareWeightList: pTareWeightList
        , pVolumeList: pVolumeList
        , pNetWeightList: pNetWeightList
        , pGrossWeightList: pGrossWeightList
        , pVGMList: pVGMList
        , pDescriptionOfGoodsList: pDescriptionOfGoodsList
        , pNumberOfPackagesList: pNumberOfPackagesList
        , pPackageTypeList: pPackageTypeList
    };
    CallPOSTFunctionWithParameters("/api/OperationContainersAndPackages/InsertListFromExcel", pParametersWithValues
        , function (pData) {
            if (pData[0]) {
                var pOperationContainersAndPackages = JSON.parse(pData[1]);
                swal("Success", "Saved Successfully.");
                OperationContainersAndPackages_BindTableRows(pOperationContainersAndPackages);
            }
            else {
                swal("Sorry", "Please, revise data and version of the file.");
            }
            FadePageCover(false);
        }
        , null);
    $("#" + pBtnName).val(""); //if removed the last selected file remains till unselected
}
//******************************EOF Reading Excel Files***************************************//
