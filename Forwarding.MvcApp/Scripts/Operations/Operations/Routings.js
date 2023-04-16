var RoutingSuffix = "";
function Routings_SubmenuTabClicked() {
    debugger;
    if ($("#tblRoutings tbody tr").length == 0) {
        FadePageCover(true);
        CallGETFunctionWithParameters("/api/Routings/LoadWithWhereClause",
            {
                pPageNumber: 1
                , pPageSize: 999999
                , pWhereClause: "WHERE OperationID=" + ($("#hOperationID").val() == "" ? 0 : $("#hOperationID").val())
            }
            , function (pData) {
                Routings_BindTableRows(JSON.parse(pData[0]));
                FadePageCover(false);
                FillListFromObject(null, 2, TranslateString("SelectFromMenu"), "slTrailerTruckingOrder", pData[1], null);
                FillListFromObject(null, 2, TranslateString("SelectFromMenu"), "slDriverTruckingOrder", pData[2], null);
                FillListFromObject(null, 2, TranslateString("SelectFromMenu"), "slDriverAssistantTruckingOrder", pData[3], null);
                FillListFromObject(null, 2, TranslateString("SelectFromMenu"), "slRoutingsLoadingZoneTruckingOrder", pData[4], null);
                FillListFromObject(null, 2, TranslateString("SelectFromMenu"), "slRoutingsFirstCuringAreaTruckingOrder", pData[4], null);
                FillListFromObject(null, 2, TranslateString("SelectFromMenu"), "slRoutingsSecondCuringAreaTruckingOrder", pData[4], null);
                FillListFromObject(null, 2, TranslateString("SelectFromMenu"), "slRoutingsThirdCuringAreaTruckingOrder", pData[4], null);
                FillListFromObject(null, 2, TranslateString("SelectFromMenu"), "slEquipmentTruckingOrder", pData[5], null);
                FillListFromObject(null, 2, TranslateString("SelectFromMenu"), "slCC_ClearanceTypeID", pData[6], null);
                FillListFromObject(null, 24, TranslateString("SelectFromMenu"), "slCC_CustomItemsID", pData[7], null);
                DocsOut_SubmenuTabClicked(false);
                ApplySelectListSearch();
                //FadePageCover(false); //done in DocsOut_SubmenuTabClicked() 
            }
            , null);
    }

    if (pDefaults.UnEditableCompanyName == "ILS" || pDefaults.UnEditableCompanyName == "ILSEG") {
        $('#lblGateInDate').html('FreeTime / Cutoff تاريخ');
        $('#lblGateOutDate').html('تاريخ التواجد');
        $('#lblStuffingDate').html('تاريخ السحب');
        $('#lblTruckingOrderLoadingTimeTruckingOrder').html('ميعاد التواجد');
    }
    if ($("#cbIsFCL").prop("checked") || $("#cbIsFTL").prop("checked") || $("#cbIsTank").prop("checked") || $("#cbIsFlexi").prop("checked")) {
        $('.classHideForContainers').addClass('hide');
        $('.classShowForContainers').removeClass('hide');
        if (pDefaults.UnEditableCompanyName != "ILS" && pDefaults.UnEditableCompanyName != "ILSEG") {
            $('#lblGateInDate').html('Gate In Date - تاريخ دخول الميناء');
            $('#lblGateOutDate').html('Gate Out Date - تاريخ الخروج');
        }
    }
    else {
        $('.classHideForContainers').removeClass('hide');
        $('.classShowForContainers').addClass('hide');
        if (pDefaults.UnEditableCompanyName != "ILS" && pDefaults.UnEditableCompanyName != "ILSEG") {
            $('#lblGateInDate').html('تاريخ الدخول');
            $('#lblGateOutDate').html('تاريخ الخروج ');
        }
    }

    if (pDefaults.UnEditableCompanyName == "SKY") {
        $(".classShowForSKY").removeClass("hide");
        $(".classHideForSKY").addClass("hide");

        $("#lblCCACertificateValue").text("  قيمه فاتوره الشحنه   ");
        $("#lblCCASpendDate").text("  تاريخ سحب اذن الاستلام   ");
        $("#lblCCAReceiveDate").text("  تاريخ تقديم المستندات لنافذه   ");
        $("#lblCCAOthers").text("  عرض  ");
    }
}

function Routings_BindTableRows(pRoutings) {
    debugger;
    ClearAllTableRows("tblRoutings");
    var printControlsText = " class='btn btn-xs btn-rounded btn-lightblue float-right'> <i class='fa fa-print' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + TranslateString("Print") + "</span>";
    var printCCStagesControlsText = " class='btn btn-xs btn-rounded btn-info float-right'> <i class='fa fa-print' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + "Stages" + "</span>";
    var AssignedETAPOLDate = new Date(); var AssignedATAPOLDate = new Date();
    var AssignedExpectedDeparture = new Date(); var AssignedActualDeparture = new Date();
    var AssignedExpectedArrival = new Date(); var AssignedActualArrival = new Date();
    var AssignedShippingLineName = ""; var AssignedAirlineName = ""; var AssignedTruckerName = "";
    var downloadControlsText = " class='btn btn-xs btn-rounded btn-primary float-right' > <i class='fa fa-cloud-download' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + "Download Log" + "</span>";

    //editControlsText = " class='btn btn-xs btn-rounded btn-info float-right' > <i class='fa fa-pencil' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + TranslateString("Edit") + "</span>";
    var emailControlsText = " class='btn btn-xs btn-rounded btn-primary float-right' > <i class='fa fa-envelope-o' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + "Mail" + "</span>";
    $.each(pRoutings, function (i, item) {
        if ($("#hBLType").val() == constHouseBLType && item.RoutingTypeID == MainCarraigeRoutingTypeID) { //House and MainRoute so get from masterOp (from vwOperations in OperationsEdit) 
            AssignedETAPOLDate = $("#hETAPOLDate").val();
            AssignedATAPOLDate = $("#hATAPOLDate").val();
            AssignedExpectedDeparture = $("#hExpectedDeparture").val();
            AssignedActualDeparture = $("#hActualDeparture").val();
            AssignedExpectedArrival = $("#hExpectedArrival").val();
            AssignedActualArrival = $("#hActualArrival").val();
            AssignedShippingLineName = $("#hShippingLineName").val();
            AssignedAirlineName = $("#hAirlineName").val();
            AssignedTruckerName = $("#hTruckerName").val();
        }
        else { //not House and MainRoute
            AssignedETAPOLDate = item.ETAPOLDate;
            AssignedATAPOLDate = item.ATAPOLDate;
            AssignedExpectedDeparture = item.ExpectedDeparture;
            AssignedActualDeparture = item.ActualDeparture;
            AssignedExpectedArrival = item.ExpectedArrival;
            AssignedActualArrival = item.ActualArrival;
            AssignedShippingLineName = item.ShippingLineName;
            AssignedAirlineName = item.AirlineName;
            AssignedTruckerName = item.TruckerName;
        }
        AppendRowtoTable("tblRoutings",
        ("<tr ID='" + item.ID + "' val='" + item.RoutingTypeID + "' " + (OERou && $("#hIsOperationDisabled").val() == false ? ("ondblclick='Routings_EditByDblClick(" + item.ID + ");'") : "") + ">"
            //+ "<td class='RoutingID'> <input " + (item.RoutingTypeID != MainCarraigeRoutingTypeID || (item.RoutingTypeID == MainCarraigeRoutingTypeID && $("#tblRoutings tbody tr td.RoutingTypeID[val=" + MainCarraigeRoutingTypeID + "]").length > 0) ? "name='Delete'" : "disabled='disabled'") + " type='checkbox' value='" + item.ID + "' /></td>"
            + "<td class='RoutingID'> <input " + (item.RoutingTypeID != MainCarraigeRoutingTypeID ? "name='Delete'" : "disabled='disabled'") + " type='checkbox' value='" + item.ID + "' /></td>"
            + "<td class='RoutingType' val='" + item.RoutingTypeID + "'>" + item.RoutingName + "</td>"
            //TransportType : 1-Ocean 2-Air 3-Inland
            + "<td class='TransportType hide' val='" + item.TransportType + "'>" + GetTransportType(item.TransportType) + "</td>"
            + "<td class='shownTransportIconName' ><i class= 'fa " + item.TransportIconName + " " + item.TransportIconStyle + " fa-2x'/></td>"
            + "<td class='TransportIconName hide'>" + item.TransportIconName + "</td>"//this line is to get the value of the IconName to fill it in the modal 
            + "<td class='TransportIconStyle hide'>" + item.TransportIconStyle + "</td>"//this line is to get the value of the IconStyle to fill it in the modal 

            + "<td class='POLCountry hide' val='" + item.POLCountryID + "'>" + item.POLCountryID + "</td>"
            + "<td class='PODCountry hide' val='" + item.PODCountryID + "'>" + item.PODCountryID + "</td>"
            + "<td class='POL' val='" + item.POL + "'><small>" + item.POLCountryCode + " (" + item.POLCode + " " + item.POLName + ")" +
                    "<br/>Exp. Departure : <span  class='static-text-blue'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(AssignedExpectedDeparture)) < 1 ? "Unspecified" : GetShownDate(FormattedTodaysDate, GetDateWithFormatMDY(AssignedExpectedDeparture))) + " </span>" +
                    "<br/>Act. Departure : <span  class='static-text-blue'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(AssignedActualDeparture)) < 1 ? "Unspecified" : GetShownDate(FormattedTodaysDate, GetDateWithFormatMDY(AssignedActualDeparture))) + " </span>" +
                    "</small></td>"


            + "<td class='POD' val='" + item.POD + "'><small>" + item.PODCountryCode + " (" + item.PODCode + " " + item.PODName + ")" +
                    "<br/>Exp. Arrival   : <span  class='static-text-blue'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(AssignedExpectedArrival)) < 1 ? "Unspecified" : GetShownDate(FormattedTodaysDate, GetDateWithFormatMDY(AssignedExpectedArrival))) + " </span>" +
                    "<br/>Act. Arrival   : <span  class='static-text-blue'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(AssignedActualArrival)) < 1 ? "Unspecified" : GetShownDate(FormattedTodaysDate, GetDateWithFormatMDY(AssignedActualArrival))) + " </span>" +
                    "</small></td>"

            + "<td class='ETAPOLDate hide' val='" + GetDateWithFormatMDY(AssignedETAPOLDate) + "'>" + GetShownDate(FormattedTodaysDate, GetDateWithFormatMDY(AssignedETAPOLDate)) + "</td>"
            + "<td class='ATAPOLDate hide' val='" + GetDateWithFormatMDY(AssignedATAPOLDate) + "'>" + GetShownDate(FormattedTodaysDate, GetDateWithFormatMDY(AssignedATAPOLDate)) + "</td>"

            + "<td class='ExpectedArrival hide' val='" + GetDateWithFormatMDY(AssignedExpectedArrival) + "'>" + GetShownDate(FormattedTodaysDate, GetDateWithFormatMDY(AssignedExpectedArrival)) + "</td>"
            + "<td class='ExpectedDeparture hide' val='" + GetDateWithFormatMDY(AssignedExpectedDeparture) + "'>" + GetShownDate(FormattedTodaysDate, GetDateWithFormatMDY(AssignedExpectedDeparture)) + "</td>"
            + "<td class='ActualArrival hide' val='" + GetDateWithFormatMDY(AssignedActualArrival) + "'>" + GetShownDate(FormattedTodaysDate, GetDateWithFormatMDY(AssignedActualArrival)) + "</td>"
            + "<td class='ActualDeparture hide' val='" + GetDateWithFormatMDY(AssignedActualDeparture) + "'>" + GetShownDate(FormattedTodaysDate, GetDateWithFormatMDY(AssignedActualDeparture)) + "</td>"

            + "<td class='Line' val='" + (item.TransportType == OceanTransportType ? ($("#hBLType").val() == constHouseBLType && item.RoutingTypeID == MainCarraigeRoutingTypeID ? $("#hShippingLineID").val() : item.ShippingLineID)//In case of House and main route get from vwOperations(The Line from MasterOp)
                                        : (item.TransportType == AirTransportType ? ($("#hBLType").val() == constHouseBLType && item.RoutingTypeID == MainCarraigeRoutingTypeID ? $("#hAirlineID").val() : item.AirlineID)//In case of House and main route get from vwOperations(The Line from MasterOp)
                                        : ($("#hBLType").val() == constHouseBLType && item.RoutingTypeID == MainCarraigeRoutingTypeID ? $("#hTruckerID").val() : item.TruckerID)) //In case of House and main route get from vwOperations(The Line from MasterOp)
                                        ) //EOF getting the carrier ID val
                                                + "'>" + (item.TransportType == OceanTransportType ? (AssignedShippingLineName == 0 ? "" : AssignedShippingLineName) //Ocean
                                                : (item.TransportType == AirTransportType ? (AssignedAirlineName == 0 ? "" : AssignedAirlineName) //Air
                                                : (AssignedTruckerName == 0 ? "" : AssignedTruckerName)) //Inland
                                                )
                                        + (item.CCAID == 0 ? "" : (" / " + item.CCAName))
            + "</td>"
            + "<td class='Vessel hide' val='" + item.VesselID + "'>" + (item.VesselID == 0 ? "" : item.VesselName) + "</td>"
            + "<td class='VoyageOrTruckNumber hide'>" + (item.VoyageOrTruckNumber == 0 ? "" : item.VoyageOrTruckNumber) + "</td>"
            + "<td class='TransientTime hide'>" + (item.TransientTime == 0 ? "" : item.TransientTime) + "</td>"
            + "<td class='Validity hide'>" + (item.Validity == 0 ? "" : item.Validity) + "</td>"
            + "<td class='FreeTime hide'>" + (item.FreeTime == 0 ? "" : item.FreeTime) + "</td>"
            + "<td class='Notes hide'>" + (item.Notes == 0 ? "" : item.Notes) + "</td>"
            + "<td class='RoadNumber hide'>" + (item.RoadNumber == 0 ? "" : item.RoadNumber) + "</td>"
            + "<td class='DeliveryOrderNumber hide'>" + (item.DeliveryOrderNumber == 0 ? "" : item.DeliveryOrderNumber) + "</td>"
            + "<td class='WareHouse hide'>" + (item.WareHouse == 0 ? "" : item.WareHouse) + "</td>"
            + "<td class='WareHouseLocation hide'>" + (item.WareHouseLocation == 0 ? "" : item.WareHouseLocation) + "</td>"

            + "<td class='GensetSupplierID hide'>" + item.GensetSupplierID + "</td>"
            + "<td class='CCAID hide'>" + item.CCAID + "</td>"
            + "<td class='Quantity hide'>" + (item.Quantity == 0 ? "" : item.Quantity) + "</td>"
            + "<td class='ContactPerson hide'>" + (item.ContactPerson == 0 ? "" : item.ContactPerson) + "</td>"
            + "<td class='PickupAddress hide'>" + (item.PickupAddress == 0 ? "" : item.PickupAddress) + "</td>"
            + "<td class='DeliveryAddress hide'>" + (item.DeliveryAddress == 0 ? "" : item.DeliveryAddress) + "</td>"
            + "<td class='GateInPortID hide'>" + item.GateInPortID + "</td>"
            + "<td class='GateOutPortID hide'>" + item.GateOutPortID + "</td>"
            + "<td class='GateInDate hide'>" + item.GateInDate + "</td>"

            /*****************TransportOrder*******************/
            + "<td class='CustomerID hide'>" + item.CustomerID + "</td>"
            + "<td class='SubContractedCustomerID hide'>" + item.SubContractedCustomerID + "</td>"
            + "<td class='Cost hide'>" + item.Cost + "</td>"
            + "<td class='Sale hide'>" + item.Sale + "</td>"
            + "<td class='IsIsFleet hide'><input type='checkbox' id='cbIsIsFleet" + item.ID + "' disabled='disabled' " + (item.IsIsFleet ? " checked='checked' " : "") + " /></td>"
            + "<td class='CommodityID hide'>" + item.CommodityID + "</td>"
            + "<td class='LoadingDate hide'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.LoadingDate)) > 0 ? ConvertDateFormat(GetDateWithFormatMDY(item.LoadingDate)) : "") + "</td>"
            + "<td class='LoadingReference hide'>" + (item.LoadingReference == 0 ? "" : item.LoadingReference) + "</td>"
            + "<td class='UnloadingDate hide'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.UnloadingDate)) > 0 ? ConvertDateFormat(GetDateWithFormatMDY(item.UnloadingDate)) : "") + "</td>"
            + "<td class='UnloadingReference hide'>" + (item.UnloadingReference == 0 ? "" : item.UnloadingReference) + "</td>"
            + "<td class='UnloadingTime hide'>" + (item.UnloadingTime == 0 ? "" : item.UnloadingTime) + "</td>"
            /*****************TransportOrder*******************/

            + "<td class='GateOutDate hide'>" + item.GateOutDate + "</td>"
            + "<td class='StuffingDate hide'>" + item.StuffingDate + "</td>"
            + "<td class='DeliveryDate hide'>" + item.DeliveryDate + "</td>"
            + "<td class='BookingNumber hide'>" + (item.BookingNumber == 0 ? "" : item.BookingNumber) + "</td>"
            + "<td class='Delays hide'>" + (item.Delays == 0 ? "" : item.Delays) + "</td>"
            + "<td class='DriverName hide'>" + (item.DriverName == 0 ? "" : item.DriverName) + "</td>"
            + "<td class='DriverPhones hide'>" + (item.DriverPhones == 0 ? "" : item.DriverPhones) + "</td>"
            + "<td class='PowerFromGateInTillActualSailing hide'>" + (item.PowerFromGateInTillActualSailing == 0 ? "" : item.PowerFromGateInTillActualSailing) + "</td>"
            + "<td class='ContactPersonPhones hide'>" + (item.ContactPersonPhones == 0 ? "" : item.ContactPersonPhones) + "</td>"
            + "<td class='LoadingTime hide'>" + (item.LoadingTime == 0 ? "" : item.LoadingTime) + "</td>"

            + "<td class='CCAFreight hide'>" + (item.CCAFreight == 0 ? "" : item.CCAFreight) + "</td>"
            + "<td class='CCAFOB hide'>" + (item.CCAFOB == 0 ? "" : item.CCAFOB) + "</td>"
            + "<td class='CCACFValue hide'>" + (item.CCACFValue == 0 ? "" : item.CCACFValue) + "</td>"
            + "<td class='CCAInvoiceNumber hide'>" + (item.CCAInvoiceNumber == 0 ? "" : item.CCAInvoiceNumber) + "</td>"

            + "<td class='CCAInsurance hide'>" + (item.CCAInsurance == 0 ? "" : item.CCAInsurance) + "</td>"
            + "<td class='CCADischargeValue hide'>" + (item.CCADischargeValue == 0 ? "" : item.CCADischargeValue) + "</td>"
            + "<td class='CCAAcceptedValue hide'>" + (item.CCAAcceptedValue == 0 ? "" : item.CCAAcceptedValue) + "</td>"
            + "<td class='CCAImportValue hide'>" + (item.CCAImportValue == 0 ? "" : item.CCAImportValue) + "</td>"
            + "<td class='CCADocumentReceiveDate hide'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.CCADocumentReceiveDate)) < 1 ? "" : ConvertDateFormat(GetDateWithFormatMDY(item.CCADocumentReceiveDate))) + "</td>"
            + "<td class='CCAExchangeRate hide'>" + (item.CCAExchangeRate == 0 ? "" : item.CCAExchangeRate) + "</td>"
            + "<td class='CCAVATCertificateNumber hide'>" + (item.CCAVATCertificateNumber == 0 ? "" : item.CCAVATCertificateNumber) + "</td>"
            + "<td class='CCAVATCertificateValue hide'>" + (item.CCAVATCertificateValue == 0 ? "" : item.CCAVATCertificateValue) + "</td>"
            + "<td class='CCACommercialProfitCertificateNumber hide'>" + (item.CCACommercialProfitCertificateNumber == 0 ? "" : item.CCACommercialProfitCertificateNumber) + "</td>"
            + "<td class='CCAOthers hide'>" + (item.CCAOthers == 0 ? "" : item.CCAOthers) + "</td>"
            + "<td class='CCASpendDate hide'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.CCASpendDate)) < 1 ? "" : ConvertDateFormat(GetDateWithFormatMDY(item.CCASpendDate))) + "</td>"

            + "<td class='CertificateNumber hide'>" + (item.CertificateNumber == 0 ? "" : item.CertificateNumber) + "</td>"
            + "<td class='CertificateValue hide'>" + (item.CertificateValue == 0 ? "" : item.CertificateValue) + "</td>"
            + "<td class='CertificateDate hide'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.CertificateDate)) < 1 ? "" : ConvertDateFormat(GetDateWithFormatMDY(item.CertificateDate))) + "</td>"
            + "<td class='QasimaNumber hide'>" + (item.QasimaNumber == 0 ? "" : item.QasimaNumber) + "</td>"
            + "<td class='Match hide'>" + (item.Match == 0 ? "" : item.Match) + "</td>"
            + "<td class='QasimaDate hide'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.QasimaDate)) < 1 ? "" : ConvertDateFormat(GetDateWithFormatMDY(item.QasimaDate))) + "</td>"
            + "<td class='SalesDateReceived hide'>" + item.SalesDateReceived + "</td>"
            + "<td class='CommerceDateReceived hide'>" + item.CommerceDateReceived + "</td>"
            + "<td class='InspectionDateReceived hide'>" + item.InspectionDateReceived + "</td>"
            + "<td class='FinishDateReceived hide'>" + item.FinishDateReceived + "</td>"
            + "<td class='SalesDateDelivered hide'>" + item.SalesDateDelivered + "</td>"
            + "<td class='CommerceDateDelivered hide'>" + item.CommerceDateDelivered + "</td>"
            + "<td class='InspectionDateDelivered hide'>" + item.InspectionDateDelivered + "</td>"
            + "<td class='FinishDateDelivered hide'>" + item.FinishDateDelivered + "</td>"

            + "<td class='CCDropBackReceived hide' val='" + GetDateFromServer(IsNull(item.CCDropBackReceived, "01/01/1900")) + "' >" + GetDateFromServer(IsNull(item.CCDropBackReceived, "01/01/1900"))+ "</td>"
            + "<td class='CCDropBackDelivered hide'  val='" + GetDateFromServer(IsNull(item.CCDropBackDelivered, "01/01/1900")) + "'>" + GetDateFromServer(IsNull(item.CCDropBackDelivered, "01/01/1900")) + "</td>"
            + "<td class='CCAllowTemporaryReceived hide' val='" + GetDateFromServer(IsNull(item.CCAllowTemporaryReceived, "01/01/1900")) + "'>" + GetDateFromServer(IsNull(item.CCAllowTemporaryReceived, "01/01/1900"))+ "</td>"
            + "<td class='CCAllowTemporaryDelivered hide' val='" + GetDateFromServer(IsNull(item.CCAllowTemporaryDelivered, "01/01/1900")) + "'>" + GetDateFromServer(IsNull(item.CCAllowTemporaryDelivered, "01/01/1900"))  + "</td>"
            + "<td class='CC_ClearanceTypeID hide' val='" + (item.CC_ClearanceTypeID == 0 ? "" : item.CC_ClearanceTypeID) + "' >" + item.CC_ClearanceTypeID + "</td>"
            + "<td class='CC_CustomItemsID hide' val='" + (item.CC_CustomItemsID == 0 ? "" : item.CC_CustomItemsID) + "' >" + item.CC_CustomItemsID + "</td>"
            + "<td class='CCReleaseNo hide' val='" +  item.CCReleaseNo + "'>" + item.CCReleaseNo  + "</td>"




            + "<td class='IsOwnedByCompany hide'><input type='checkbox' id='cbIsOwnedByCompany" + item.ID + "' disabled='disabled' " + (item.IsOwnedByCompany ? " checked='checked' " : "") + " /></td>"
            + "<td class='TrailerID hide'>" + (item.TrailerID == 0 ? "" : item.TrailerID) + "</td>"
            + "<td class='DriverID hide'>" + (item.DriverID == 0 ? "" : item.DriverID) + "</td>"
            + "<td class='DriverAssistantID hide'>" + (item.DriverAssistantID == 0 ? "" : item.DriverAssistantID) + "</td>"
            + "<td class='EquipmentID hide'>" + (item.EquipmentID == 0 ? "" : item.EquipmentID) + "</td>"
            + "<td class='LoadingZoneID hide'>" + (item.LoadingZoneID == 0 ? "" : item.LoadingZoneID) + "</td>"
            + "<td class='FirstCuringAreaID hide'>" + (item.FirstCuringAreaID == 0 ? "" : item.FirstCuringAreaID) + "</td>"
            + "<td class='SecondCuringAreaID hide'>" + (item.SecondCuringAreaID == 0 ? "" : item.SecondCuringAreaID) + "</td>"
            + "<td class='ThirdCuringAreaID hide'>" + (item.ThirdCuringAreaID == 0 ? "" : item.ThirdCuringAreaID) + "</td>"
            + "<td class='BillNumber hide'>" + (item.BillNumber == 0 ? "" : item.BillNumber) + "</td>"
            + "<td class='TruckingOrderCode hide'>" + (item.TruckingOrderCode == 0 ? "" : item.TruckingOrderCode) + "</td>"

            + (item.RoutingTypeID == TruckingOrderRoutingTypeID
                ? ("<td class=''>"
                        + "<a data-toggle='modal' onclick='DocsOut_Print(" + TruckingOrderDocumentTypeID + "," + item.ID + ");' " + printControlsText + "</a>"
                        + "<a onclick='DocsOut_OpenPartnersModal(" + TruckingOrderDocumentTypeID + "," + item.ID + ",null,null," + '"Email"' + ");' " + emailControlsText + "</a>"
                    + "</td>")
                : (item.RoutingTypeID == CustomsClearanceRoutingTypeID
                    ? ("<td class=''>"
                            + "<a data-toggle='modal'  " + (pDefaults.UnEditableCompanyName == "ELI" ? "" : " class='hide' ") + " onclick=" + '"' + "RoutingsCC_PrintRequest(" + item.ID + "," + "'Print'" + ");" + '"' + " title='طلب تخليص' " + printControlsText + "</a>"
                            + "<a data-toggle='modal' onclick=" + '"' + "RoutingsCC_PrintStages(" + item.ID + "," + "'Print'" + ");" + '"' + " title='Print Clearance Stages' " + printCCStagesControlsText + "</a>"
                            //+ "<a onclick='DocsOut_OpenPartnersModal(" + ClearanceRequestDocumentTypeID + "," + item.ID + ",null,null," + '"Email"' + ");' " + emailControlsText + "</a>"
                        + "</td>")
                    : "<td></td>"
                    )
              )
            //+ "<td class='hide'><a href='#RoutingModal' data-toggle='modal' onclick='Routings_FillControls(" + item.ID + ");' " + editControlsText + "</a></td>"
            + `<td class='${ODRou ? `` : `hide`}'><a href='/api/Routings/GetLog?pID=${item.ID}&pTableName=vwRoutings' target='_blank' ${downloadControlsText} </a></td>`
            + `<td class='${ODRou ? `hide` : ``}'></td>`
        + "</tr>"));
    });
    //ApplyPermissions();
    //if (OARou && $("#hIsOperationDisabled").val() == false) { $("#btn-AddRoute").removeClass("hide"); } else { $("#btn-AddRoute").addClass("hide"); }
    if (OARou && $("#hIsOperationDisabled").val() == false) { $("#btn-AddRouteTruckingOrder").removeClass("hide"); $("#btn-AddRouteCustomsClearance").removeClass("hide"); } else { $("#btn-AddRouteTruckingOrder").addClass("hide"); $("#btn-AddRouteCustomsClearance").addClass("hide"); }
    if (ODRou && $("#hIsOperationDisabled").val() == false) $("#btn-DeleteRoute").removeClass("hide"); else $("#btn-DeleteRoute").addClass("hide");
    BindAllCheckboxonTable("tblRoutings", "RoutingID", "cb-CheckAll-Routings");
    CheckAllCheckbox("HeaderDeleteRoutingID");
    //HighlightText("#tblRoutings>tbody>tr", $("#txt-Search").val().trim());
    if (showDeleteFailMessage) { //sherif: if deletion failed due to foreign key, then this message is shown
        swal(strSorry, strDeleteFailMessage, "warning");
        showDeleteFailMessage = false;
    }
    Routings_SetTableProperties();//Set Routing Table Properties according to BLType and if Connected or Not
}
function Routings_Insert(pSaveandAddNew) {
    if (RoutingSuffix != "CustomsClearance" && !Routings_CheckDatesLogic())
        swal(strSorry, strCheckDates);
        //uncomment if i want to remove logic date validation
        //else //check dates are not before open date
        //    if (
        //        ($("#txtExpectedDeparture").val().trim() != "" && Date.prototype.compareDates(ConvertDateFormat($("#txtOpenDate").val().trim()), ConvertDateFormat($("#txtExpectedDeparture").val().trim())) < 0)
        //        || ($("#txtActualDeparture").val().trim() != "" && Date.prototype.compareDates(ConvertDateFormat($("#txtOpenDate").val().trim()), ConvertDateFormat($("#txtActualDeparture").val().trim())) < 0)
        //        || ($("#txtExpectedArrival").val().trim() != "" && Date.prototype.compareDates(ConvertDateFormat($("#txtOpenDate").val().trim()), ConvertDateFormat($("#txtExpectedArrival").val().trim())) < 0)
        //        || ($("#txtActualArrival").val().trim() != "" && Date.prototype.compareDates(ConvertDateFormat($("#txtOpenDate").val().trim()), ConvertDateFormat($("#txtActualArrival").val().trim())) < 0)
        //        )
        //        swal(strSorry, "Dates must be after open date.");
    else if (ValidateForm("form", "RoutingModal" + RoutingSuffix)) {
        FadePageCover(true);
        var pParametersWithValues = {
            pOperationID: $("#hOperationID").val()
            , pRoutingTypeID: $('#slRoutingTypes' + RoutingSuffix + ' option:selected').val()
            , pTransportTypeID: $("#hRoutingTransportType" + RoutingSuffix).val()
            , pTransportIconName: $("#hRoutingTransportIconName" + RoutingSuffix).val()
            , pTransportIconStyle: $("#hRoutingTransportIconStyle" + RoutingSuffix).val()

            , pPOLCountryID: RoutingSuffix == "CustomsClearance" ? $("#hPOLCountryID").val() : $('#slRoutingsPOLCountries' + RoutingSuffix + ' option:selected').val()
            , pPODCountryID: RoutingSuffix == "CustomsClearance" ? $("#hPODCountryID").val() : $('#slRoutingsPODCountries' + RoutingSuffix + ' option:selected').val()
            , pPOLID: RoutingSuffix == "CustomsClearance" ? $("#hPOL").val() : $('#slRoutingsPOL' + RoutingSuffix + ' option:selected').val()
            , pPODID: RoutingSuffix == "CustomsClearance" ? $("#hPOL").val() : $('#slRoutingsPOD' + RoutingSuffix + ' option:selected').val()

            , pETAPOLDate: (RoutingSuffix == "CustomsClearance" || $("#txtETAPOLDate" + RoutingSuffix).val() == "" ? "01/01/1900" : ConvertDateFormat($("#txtETAPOLDate" + RoutingSuffix).val()))
            , pATAPOLDate: (RoutingSuffix == "CustomsClearance" || $("#txtATAPOLDate" + RoutingSuffix).val() == "" ? "01/01/1900" : ConvertDateFormat($("#txtATAPOLDate" + RoutingSuffix).val()))
            , pExpectedArrival: (RoutingSuffix == "CustomsClearance" || $("#txtExpectedArrival" + RoutingSuffix).val() == "" ? "01/01/1900" : ConvertDateFormat($("#txtExpectedArrival" + RoutingSuffix).val()))
            , pExpectedDeparture: (RoutingSuffix == "CustomsClearance" || $("#txtExpectedDeparture" + RoutingSuffix).val() == "" ? "01/01/1900" : ConvertDateFormat($("#txtExpectedDeparture" + RoutingSuffix).val()))
            , pActualArrival: (RoutingSuffix == "CustomsClearance" || $("#txtActualArrival" + RoutingSuffix).val() == "" ? "01/01/1900" : ConvertDateFormat($("#txtActualArrival" + RoutingSuffix).val()))
            , pActualDeparture: (RoutingSuffix == "CustomsClearance" || $("#txtActualDeparture" + RoutingSuffix).val() == "" ? "01/01/1900" : ConvertDateFormat($("#txtActualDeparture" + RoutingSuffix).val()))

            , pShippingLineID: (RoutingSuffix == "CustomsClearance")
                ? 0
                : ($("#hRoutingTransportType" + RoutingSuffix).val() == OceanTransportType
                    ? ($('#slRoutingsLines' + RoutingSuffix + ' option:selected').val() == "" ? 0 : $('#slRoutingsLines' + RoutingSuffix + ' option:selected').val())
                    : 0)
            , pAirlineID: (RoutingSuffix == "CustomsClearance")
                ? 0
                : ($("#hRoutingTransportType" + RoutingSuffix).val() == AirTransportType
                    ? ($('#slRoutingsLines' + RoutingSuffix + ' option:selected').val() == "" ? 0 : $('#slRoutingsLines' + RoutingSuffix + ' option:selected').val())
                    : 0)
            , pTruckerID: (RoutingSuffix == "CustomsClearance")
                ? ($('#slRoutingsLines' + RoutingSuffix + ' option:selected').val() == "" ? 0 : $('#slRoutingsLines' + RoutingSuffix + ' option:selected').val())
                : ($("#hRoutingTransportType" + RoutingSuffix).val() == InlandTransportType
                  ? (($('#slRoutingsLines' + RoutingSuffix + ' option:selected').val() == "" || ($("#hBLType").val() == constHouseBLType && $("#slRoutingTypes" + RoutingSuffix).val() == MainCarraigeRoutingTypeID)) ? 0 : $('#slRoutingsLines' + RoutingSuffix + ' option:selected').val())
                  : 0)
            , pVesselID: (RoutingSuffix == "CustomsClearance")
                ? 0
                : ($("#hRoutingTransportType" + RoutingSuffix).val() == OceanTransportType
                    ? ($('#slRoutingVessels' + RoutingSuffix + ' option:selected').val() == "" ? 0 : $('#slRoutingVessels' + RoutingSuffix + ' option:selected').val())
                    : 0)
            , pVoyageOrTruckNumber: ($("#cbIsOwnedByCompany").prop("checked") || RoutingSuffix == "CustomsClearance") ? 0 : $("#txtRoutingVoyageOrTruckNumber" + RoutingSuffix).val().trim().toUpperCase()
            , pTransientTime: RoutingSuffix == "CustomsClearance" || $("#txtRoutingTransientTime" + RoutingSuffix).val().trim() == "" ? 0 : $("#txtRoutingTransientTime" + RoutingSuffix).val()
            , pValidity: RoutingSuffix == "CustomsClearance" || $("#txtRoutingValidity" + RoutingSuffix).val().trim() == "" ? 0 : $("#txtRoutingValidity" + RoutingSuffix).val()
            , pFreeTime: RoutingSuffix == "CustomsClearance" || $("#txtRoutingFreeTime" + RoutingSuffix).val().trim() == "" ? 0 : $("#txtRoutingFreeTime" + RoutingSuffix).val()
            , pNotes: $("#txtRoutingNotes" + RoutingSuffix).val().trim().toUpperCase()

            , pGensetSupplierID: RoutingSuffix == "CustomsClearance" || $("#slTruckingOrderGensetSupplier" + RoutingSuffix).val() == "" ? 0 : $("#slTruckingOrderGensetSupplier" + RoutingSuffix).val()
            , pCCAID: $("#slRoutingCCA" + RoutingSuffix).val() == "" ? 0 : $("#slRoutingCCA" + RoutingSuffix).val()
            , pQuantity: RoutingSuffix == "CustomsClearance" || $("#txtTruckingOrderQuantity" + RoutingSuffix).val().trim() == "" ? 0 : $("#txtTruckingOrderQuantity" + RoutingSuffix).val().trim().toUpperCase()
            , pContactPerson: RoutingSuffix == "CustomsClearance" ? 0 : $("#txtTruckingOrderContactPerson" + RoutingSuffix).val().trim().toUpperCase()
            , pTruckingOrderPickupAddress: RoutingSuffix == "CustomsClearance" || $("#txtTruckingOrderPickupAddress" + RoutingSuffix).val().trim().toUpperCase()
            , pTruckingOrderDeliveryAddress: RoutingSuffix == "CustomsClearance" ? 0 : $("#txtTruckingOrderDeliveryAddress" + RoutingSuffix).val().trim().toUpperCase()
            , pGateInPortID: RoutingSuffix == "CustomsClearance" || $("#slTruckingOrderGateInPort" + RoutingSuffix).val() == "" || $("#slTruckingOrderGateInPort" + RoutingSuffix).val() == null ? 0 : $("#slTruckingOrderGateInPort" + RoutingSuffix).val()
            , pGateOutPortID: RoutingSuffix == "CustomsClearance" || $("#slTruckingOrderGateOutPort" + RoutingSuffix).val() == "" || $("#slTruckingOrderGateOutPort" + RoutingSuffix).val() == null ? 0 : $("#slTruckingOrderGateOutPort" + RoutingSuffix).val()
            , pGateInDate: (RoutingSuffix == "CustomsClearance" || $("#txtTruckingOrderGateInDate" + RoutingSuffix).val() == "" ? "01/01/1900" : ConvertDateFormat($("#txtTruckingOrderGateInDate" + RoutingSuffix).val()))
            /****************************TransportOrder**************************/
            , pCustomerID: 0
            , pSubContractedCustomerID: 0
            , pCost: 0
            , pSale: 0
            , pIsFleet: false
            , pCommodityID: 0
            , pLoadingDate: "01/01/1900"
            , pLoadingReference: 0
            , pUnloadingDate: "01/01/1900"
            , pUnloadingReference: 0
            , pUnloadingTime: 0
            , pQuotationRouteID: 0
            /****************************TransportOrder**************************/
            , pGateOutDate: (RoutingSuffix == "CustomsClearance" || $("#txtTruckingOrderGateOutDate" + RoutingSuffix).val() == "" ? "01/01/1900" : ConvertDateFormat($("#txtTruckingOrderGateOutDate" + RoutingSuffix).val()))
            , pStuffingDate: (RoutingSuffix == "CustomsClearance" || $("#txtTruckingOrderStuffingDate" + RoutingSuffix).val() == "" ? "01/01/1900" : ConvertDateFormat($("#txtTruckingOrderStuffingDate" + RoutingSuffix).val()))
            , pDeliveryDate: (RoutingSuffix == "CustomsClearance" || $("#txtTruckingOrderDeliveryDate" + RoutingSuffix).val() == "" ? "01/01/1900" : ConvertDateFormat($("#txtTruckingOrderDeliveryDate" + RoutingSuffix).val()))
            , pBookingNumber: RoutingSuffix == "CustomsClearance" ? ($("#txtCCBookingNumber").val().trim() == "" ? 0 : $("#txtCCBookingNumber").val().trim().toUpperCase()) : $("#txtTruckingOrderBookingNumber" + RoutingSuffix).val().trim().toUpperCase()
            , pDelays: RoutingSuffix == "CustomsClearance" ? 0 : $("#txtTruckingOrderDelays" + RoutingSuffix).val().trim().toUpperCase()
            , pDriverName:  ( $("#cbIsOwnedByCompany").prop("checked") || RoutingSuffix == "CustomsClearance") ? 0 : $("#txtTruckingOrderDriverName" + RoutingSuffix).val().trim().toUpperCase()
            , pDriverPhones: ($("#cbIsOwnedByCompany").prop("checked") || RoutingSuffix == "CustomsClearance") ? 0 : $("#txtTruckingOrderDriverPhones" + RoutingSuffix).val().trim().toUpperCase()
            , pPowerFromGateInTillActualSailing: RoutingSuffix == "CustomsClearance" ? 0 : $("#txtTruckingOrderPowerFromGateInTillActualSailing" + RoutingSuffix).val().trim().toUpperCase()

            , pContactPersonPhones: RoutingSuffix == "CustomsClearance" ? 0 : $("#txtTruckingOrderContactPersonPhones" + RoutingSuffix).val().trim().toUpperCase()
            , pLoadingTime: RoutingSuffix == "CustomsClearance" ? 0 : $("#txtTruckingOrderLoadingTime" + RoutingSuffix).val().trim().toUpperCase()

            , pCCAFreight: RoutingSuffix != "CustomsClearance" || $("#txtCCAFreight").val().trim() == "" ? 0 : $("#txtCCAFreight").val().trim().toUpperCase()
            , pCCAFOB: RoutingSuffix != "CustomsClearance" || $("#txtCCAFOB").val().trim() == "" ? 0 : $("#txtCCAFOB").val().trim().toUpperCase()
            , pCCACFValue: RoutingSuffix != "CustomsClearance" || $("#txtCCACFValue").val().trim() == "" ? 0 : $("#txtCCACFValue").val().trim().toUpperCase()
            , pCCAInvoiceNumber: RoutingSuffix != "CustomsClearance" || $("#txtCCAInvoiceNumber").val().trim() == "" ? 0 : $("#txtCCAInvoiceNumber").val().trim().toUpperCase()

            , pCCAInsurance: RoutingSuffix != "CustomsClearance" || $("#txtCCAInsurance").val().trim() == "" ? 0 : $("#txtCCAInsurance").val().trim().toUpperCase()
            , pCCADischargeValue: RoutingSuffix != "CustomsClearance" || $("#txtCCADischargeValue").val().trim() == "" ? 0 : $("#txtCCADischargeValue").val().trim().toUpperCase()
            , pCCAAcceptedValue: RoutingSuffix != "CustomsClearance" || $("#txtCCAAcceptedValue").val().trim() == "" ? 0 : $("#txtCCAAcceptedValue").val().trim().toUpperCase()
            , pCCAImportValue: RoutingSuffix != "CustomsClearance" || $("#txtCCAImportValue").val().trim() == "" ? 0 : $("#txtCCAImportValue").val().trim().toUpperCase()
            , pCCADocumentReceiveDate: (RoutingSuffix != "CustomsClearance" || $("#txtCCADocumentReceiveDate").val().trim() == "" ? "01/01/1900" : PadDateWithZeroes($("#txtCCADocumentReceiveDate").val().trim()))
            , pCCAExchangeRate: RoutingSuffix != "CustomsClearance" || $("#txtCCAExchangeRate").val().trim() == "" ? 0 : $("#txtCCAExchangeRate").val().trim().toUpperCase()
            , pCCAVATCertificateNumber: RoutingSuffix != "CustomsClearance" || $("#txtCCAVATCertificateNumber").val().trim() == "" ? 0 : $("#txtCCAVATCertificateNumber").val().trim().toUpperCase()
            , pCCAVATCertificateValue: RoutingSuffix != "CustomsClearance" || $("#txtCCAVATCertificateValue").val().trim() == "" ? 0 : $("#txtCCAVATCertificateValue").val().trim().toUpperCase()
            , pCCACommercialProfitCertificateNumber: RoutingSuffix != "CustomsClearance" || $("#txtCCACommercialProfitCertificateNumber").val().trim() == "" ? 0 : $("#txtCCACommercialProfitCertificateNumber").val().trim().toUpperCase()
            , pCCAOthers: RoutingSuffix != "CustomsClearance" || $("#txtCCAOthers").val().trim() == "" ? 0 : $("#txtCCAOthers").val().trim().toUpperCase()
            , pCCASpendDate: (RoutingSuffix != "CustomsClearance" || $("#txtCCASpendDate").val().trim() == "" ? "01/01/1900" : PadDateWithZeroes($("#txtCCASpendDate").val().trim()))

            , pCertificateNumber: RoutingSuffix != "CustomsClearance" || $("#txtCertificateNumber").val().trim() == "" ? 0 : $("#txtCertificateNumber").val().trim().toUpperCase()
            , pCertificateValue: RoutingSuffix != "CustomsClearance" || $("#txtCertificateValue").val().trim() == "" ? 0 : $("#txtCertificateValue").val().trim().toUpperCase()
            , pCertificateDate: (RoutingSuffix != "CustomsClearance" || $("#txtCertificateDate").val().trim() == "" ? "01/01/1900" : PadDateWithZeroes($("#txtCertificateDate").val().trim()))
            , pQasimaNumber: RoutingSuffix != "CustomsClearance" || $("#txtQasimaNumber").val().trim() == "" ? 0 : $("#txtQasimaNumber").val().trim().toUpperCase()
            , pQasimaDate: (RoutingSuffix != "CustomsClearance" || $("#txtQasimaDate").val().trim() == "" ? "01/01/1900" : PadDateWithZeroes($("#txtQasimaDate").val().trim()))
            , pMatch: RoutingSuffix != "CustomsClearance" || $("#match").prop("checked")
            , pSalesDateReceived: (RoutingSuffix != "CustomsClearance" || $("#txtSalesDateReceived").val().trim() == "" ? "01/01/1900" : PadDateWithZeroes($("#txtSalesDateReceived").val().trim()))
            , pCommerceDateReceived: (RoutingSuffix != "CustomsClearance" || $("#txtCommerceDateReceived").val().trim() == "" ? "01/01/1900" : PadDateWithZeroes($("#txtCommerceDateReceived").val().trim()))
            , pInspectionDateReceived: (RoutingSuffix != "CustomsClearance" || $("#txtInspectionDateReceived").val().trim() == "" ? "01/01/1900" : PadDateWithZeroes($("#txtInspectionDateReceived").val().trim()))

            , pFinishDateReceived: (RoutingSuffix != "CustomsClearance" || $("#txtFinishDateReceived").val().trim() == "" ? "01/01/1900" : PadDateWithZeroes($("#txtFinishDateReceived").val().trim()))
            , pSalesDateDelivered: (RoutingSuffix != "CustomsClearance" || $("#txtSalesDateDelivered").val().trim() == "" ? "01/01/1900" : PadDateWithZeroes($("#txtSalesDateDelivered").val().trim()))
            , pCommerceDateDelivered: (RoutingSuffix != "CustomsClearance" || $("#txtCommerceDateDelivered").val().trim() == "" ? "01/01/1900" : PadDateWithZeroes($("#txtCommerceDateDelivered").val().trim()))
            , pInspectionDateDelivered: (RoutingSuffix != "CustomsClearance" || $("#txtInspectionDateDelivered").val().trim() == "" ? "01/01/1900" : PadDateWithZeroes($("#txtInspectionDateDelivered").val().trim()))
            , pFinishDateDelivered: (RoutingSuffix != "CustomsClearance" || $("#txtFinishDateDelivered").val().trim() == "" ? "01/01/1900" : PadDateWithZeroes($("#txtFinishDateDelivered").val().trim()))

            , pCCDropBackReceived:( (RoutingSuffix != "CustomsClearance" || $("#txtCCDropBackReceived").val().trim() == "") ? "01/01/1900" : ConvertDateFormat($("#txtCCDropBackReceived").val().trim()) )
            , pCCDropBackDelivered:( (RoutingSuffix != "CustomsClearance" || $("#txtCCDropBackDelivered").val().trim() == "") ? "01/01/1900" : ConvertDateFormat($("#txtCCDropBackDelivered").val().trim()) )
            , pCCAllowTemporaryReceived:( (RoutingSuffix != "CustomsClearance" || $("#txtCCAllowTemporaryReceived").val().trim() == "") ? "01/01/1900" : ConvertDateFormat($("#txtCCAllowTemporaryReceived").val().trim()) )
            , pCCAllowTemporaryDelivered:( (RoutingSuffix != "CustomsClearance" || $("#txtCCAllowTemporaryDelivered").val().trim() == "") ? "01/01/1900" : ConvertDateFormat($("#txtCCAllowTemporaryDelivered").val().trim()) )
            , pCC_ClearanceTypeID: RoutingSuffix != "CustomsClearance" || $("#slCC_ClearanceTypeID").val().trim() == "" ? 0 : $("#slCC_ClearanceTypeID").val().trim().toUpperCase()
            , pCC_CustomItemsID: RoutingSuffix != "CustomsClearance" || $("#slCC_CustomItemsID").val().trim() == "" ? 0 : $("#slCC_CustomItemsID").val().trim().toUpperCase()
            , pCCReleaseNo: RoutingSuffix != "CustomsClearance" || IsNull($("#txtCCReleaseNo").val().trim() , "") == "" ? 0 : $("#txtCCReleaseNo").val()






            , pRoadNumber: "0" //Insert is never main route
            , pDeliveryOrderNumber: "0" //Insert is never main route
            , pWareHouse: "0" //Insert is never main route
            , pWareHouseLocation: "0" //Insert is never main route


            , pIsOwnedByCompany: $("#cbIsOwnedByCompany").prop("checked")
            , pTrailerID: (!$("#cbIsOwnedByCompany").prop("checked") || $("#slTrailer" + RoutingSuffix).val() == "" ? 0 : $("#slTrailer" + RoutingSuffix).val())
            , pDriverID: (!$("#cbIsOwnedByCompany").prop("checked") || $("#slDriver" + RoutingSuffix).val() == "" ? 0 : $("#slDriver" + RoutingSuffix).val())
            , pDriverAssistantID: (!$("#cbIsOwnedByCompany").prop("checked") || $("#slDriverAssistant" + RoutingSuffix).val() == "" ? 0 : $("#slDriverAssistant" + RoutingSuffix).val())
            , pEquipmentID: $("#slEquipment" + RoutingSuffix).val() == "" ? 0 : $("#slEquipment" + RoutingSuffix).val()
            , pLoadingZoneID: $("#slRoutingsLoadingZone" + RoutingSuffix).val() == "" ? 0 : $("#slRoutingsLoadingZone" + RoutingSuffix).val()
            , pFirstCuringAreaID: $("#slRoutingsFirstCuringArea" + RoutingSuffix).val() == "" ? 0 : $("#slRoutingsFirstCuringArea" + RoutingSuffix).val()
            , pSecondCuringAreaID: $("#slRoutingsSecondCuringArea" + RoutingSuffix).val() == "" ? 0 : $("#slRoutingsSecondCuringArea" + RoutingSuffix).val()
            , pThirdCuringAreaID: $("#slRoutingsThirdCuringArea" + RoutingSuffix).val() == "" ? 0 : $("#slRoutingsThirdCuringArea" + RoutingSuffix).val()
            , pBillNumber: ''
            , pTruckingOrderCode: ''
            , pOffloadingDate: "01/01/1900"
            , pLastTruckCounter: 0
            , pMaxSupplierContainers : 0
        };
        CallPOSTFunctionWithParameters("/api/Routings/Insert", pParametersWithValues
            , function (pData) {
                var pSavedRoute = JSON.parse(pData[0]);
                var pRoutings = JSON.parse(pData[1]);
                Routings_BindTableRows(pRoutings);
                //set lblRouting,..... incase of changing MainCarraige Type
                if ($("#slRoutingTypes" + RoutingSuffix + " option:selected").val() == MainCarraigeRoutingTypeID) {
                    $("#lblRouting" + RoutingSuffix).html(" : " + $("#slRoutingsPOL" + RoutingSuffix + " option:selected").text() + " > " + $("#slRoutingsPOD" + RoutingSuffix + " option:selected").text());
                    $("#hPOLCountryID" + RoutingSuffix).val($('#slRoutingsPOLCountries' + RoutingSuffix + ' option:selected').val());
                    $("#hPODCountryID" + RoutingSuffix).val($('#slRoutingsPODCountries' + RoutingSuffix + ' option:selected').val());
                    $("#hPOL").val($('#slRoutingsPOL' + RoutingSuffix + ' option:selected').val());
                    $("#hPOD").val($('#slRoutingsPOD' + RoutingSuffix + ' option:selected').val());
                    $("#hShippingLineID").val($("#hRoutingTransportType" + RoutingSuffix).val() == OceanTransportType ? $('#slRoutingsLines' + RoutingSuffix + ' option:selected').val() : 0);
                    $("#hAirlineID").val($("#hRoutingTransportType" + RoutingSuffix).val() == AirTransportType ? $("#slRoutingsLines" + RoutingSuffix).val() : 0);
                    $("#hTruckerID").val($("#hRoutingTransportType" + RoutingSuffix).val() == InlandTransportType ? $("#slRoutingsLines" + RoutingSuffix).val() : 0);
                }
                $("#hRoutingID" + RoutingSuffix).val(pSavedRoute.ID);
                if (RoutingSuffix != "CustomsClearance")
                    jQuery("#" + "RoutingModal" + RoutingSuffix).modal("hide");
                swal("Success", "Saved successfully,");
                FadePageCover(false);
            }
            , null);
    }
}
function Routings_Update(pSaveandAddNew) {
    debugger;
    if (RoutingSuffix != "CustomsClearance" && !Routings_CheckDatesLogic())
        swal(strSorry, strCheckDates);
        //uncomment if i want to remove logic date validation
        //else //check dates are not before open date
        //    if (!$("#cbIsImport").prop("checked") //No validation for import
        //        &&(
        //            ($("#txtExpectedDeparture").val().trim() != "" && Date.prototype.compareDates(ConvertDateFormat($("#txtOpenDate").val().trim()), ConvertDateFormat($("#txtExpectedDeparture").val().trim())) < 0)
        //            || ($("#txtActualDeparture").val().trim() != "" && Date.prototype.compareDates(ConvertDateFormat($("#txtOpenDate").val().trim()), ConvertDateFormat($("#txtActualDeparture").val().trim())) < 0)
        //            || ($("#txtExpectedArrival").val().trim() != "" && Date.prototype.compareDates(ConvertDateFormat($("#txtOpenDate").val().trim()), ConvertDateFormat($("#txtExpectedArrival").val().trim())) < 0)
        //            || ($("#txtActualArrival").val().trim() != "" && Date.prototype.compareDates(ConvertDateFormat($("#txtOpenDate").val().trim()), ConvertDateFormat($("#txtActualArrival").val().trim())) < 0)
        //            )
        //        )
        //        swal(strSorry, "Dates must be after open date.");
    else if (ValidateForm("form", "RoutingModal" + RoutingSuffix)) {
        FadePageCover(true);
        var pParametersWithValues = {
            pID: $("#hRoutingID" + RoutingSuffix).val()
            , pOperationID: $("#hOperationID").val()
            , pRoutingTypeID: $('#slRoutingTypes' + RoutingSuffix + ' option:selected').val()
            , pTransportTypeID: $("#hRoutingTransportType" + RoutingSuffix).val()
            , pTransportIconName: $("#hRoutingTransportIconName" + RoutingSuffix).val()
            , pTransportIconStyle: $("#hRoutingTransportIconStyle" + RoutingSuffix).val()

            , pPOLCountryID: RoutingSuffix == "CustomsClearance" ? $("#hPOLCountryID").val() : $('#slRoutingsPOLCountries' + RoutingSuffix + ' option:selected').val()
            , pPODCountryID: RoutingSuffix == "CustomsClearance" ? $("#hPODCountryID").val() : $('#slRoutingsPODCountries' + RoutingSuffix + ' option:selected').val()
            , pPOLID: RoutingSuffix == "CustomsClearance" ? $("#hPOL").val() : $('#slRoutingsPOL' + RoutingSuffix + ' option:selected').val()
            , pPODID: RoutingSuffix == "CustomsClearance" ? $("#hPOL").val() : $('#slRoutingsPOD' + RoutingSuffix + ' option:selected').val()

            , pPickupAddress: (RoutingSuffix == "CustomsClearance" || $("#txtPickupAddress" + RoutingSuffix).val().trim() == "" ? "0" : $("#txtPickupAddress" + RoutingSuffix).val().trim().toUpperCase())
            , pDeliveryAddress: (RoutingSuffix == "CustomsClearance" || $("#txtDeliveryAddress" + RoutingSuffix).val().trim() == "" ? "0" : $("#txtDeliveryAddress" + RoutingSuffix).val().trim().toUpperCase())

            , pETAPOLDate: (RoutingSuffix == "CustomsClearance" || $("#txtETAPOLDate" + RoutingSuffix).val() == "" || ($("#hBLType").val() == constHouseBLType && $("#slRoutingTypes" + RoutingSuffix).val() == MainCarraigeRoutingTypeID) ? "01/01/1900" : ConvertDateFormat($("#txtETAPOLDate" + RoutingSuffix).val()))
            , pATAPOLDate: (RoutingSuffix == "CustomsClearance" || $("#txtATAPOLDate" + RoutingSuffix).val() == "" || ($("#hBLType").val() == constHouseBLType && $("#slRoutingTypes" + RoutingSuffix).val() == MainCarraigeRoutingTypeID) ? "01/01/1900" : ConvertDateFormat($("#txtATAPOLDate" + RoutingSuffix).val()))
            , pExpectedArrival: (RoutingSuffix == "CustomsClearance" || $("#txtExpectedArrival" + RoutingSuffix).val() == "" || ($("#hBLType").val() == constHouseBLType && $("#slRoutingTypes" + RoutingSuffix).val() == MainCarraigeRoutingTypeID) ? "01/01/1900" : ConvertDateFormat($("#txtExpectedArrival" + RoutingSuffix).val()))
            , pExpectedDeparture: (RoutingSuffix == "CustomsClearance" || $("#txtExpectedDeparture" + RoutingSuffix).val() == "" || ($("#hBLType").val() == constHouseBLType && $("#slRoutingTypes" + RoutingSuffix).val() == MainCarraigeRoutingTypeID) ? "01/01/1900" : ConvertDateFormat($("#txtExpectedDeparture" + RoutingSuffix).val()))
            , pActualArrival: (RoutingSuffix == "CustomsClearance" || $("#txtActualArrival" + RoutingSuffix).val() == "" || ($("#hBLType").val() == constHouseBLType && $("#slRoutingTypes" + RoutingSuffix).val() == MainCarraigeRoutingTypeID) ? "01/01/1900" : ConvertDateFormat($("#txtActualArrival" + RoutingSuffix).val()))
            , pActualDeparture: (RoutingSuffix == "CustomsClearance" || $("#txtActualDeparture" + RoutingSuffix).val() == "" || ($("#hBLType").val() == constHouseBLType && $("#slRoutingTypes" + RoutingSuffix).val() == MainCarraigeRoutingTypeID) ? "01/01/1900" : ConvertDateFormat($("#txtActualDeparture" + RoutingSuffix).val()))

            , pShippingLineID: (RoutingSuffix == "CustomsClearance")
                ? 0
                : ($("#hRoutingTransportType" + RoutingSuffix).val() == OceanTransportType
                    ? ($('#slRoutingsLines' + RoutingSuffix + ' option:selected').val() == "" || ($("#hBLType").val() == constHouseBLType && $("#slRoutingTypes" + RoutingSuffix).val() == MainCarraigeRoutingTypeID) ? 0 : $('#slRoutingsLines' + RoutingSuffix + ' option:selected').val())
                    : 0)
            , pAirlineID: (RoutingSuffix == "CustomsClearance")
                ? 0
                : ($("#hRoutingTransportType" + RoutingSuffix).val() == AirTransportType
                    ? ($('#slRoutingsLines' + RoutingSuffix + ' option:selected').val() == "" || ($("#hBLType").val() == constHouseBLType && $("#slRoutingTypes" + RoutingSuffix).val() == MainCarraigeRoutingTypeID) ? 0 : $('#slRoutingsLines' + RoutingSuffix + ' option:selected').val())
                    : 0)
            , pTruckerID: (RoutingSuffix == "CustomsClearance")
                ? ($('#slRoutingsLines' + RoutingSuffix + ' option:selected').val() == "" ? 0 : $('#slRoutingsLines' + RoutingSuffix + ' option:selected').val())
                : ($("#hRoutingTransportType" + RoutingSuffix).val() == InlandTransportType
                  ? (($('#slRoutingsLines' + RoutingSuffix + ' option:selected').val() == "" || ($("#hBLType").val() == constHouseBLType && $("#slRoutingTypes" + RoutingSuffix).val() == MainCarraigeRoutingTypeID)) ? 0 : $('#slRoutingsLines' + RoutingSuffix + ' option:selected').val())
                  : 0)
            , pVesselID: (RoutingSuffix == "CustomsClearance")
                ? 0
                : ($("#hRoutingTransportType" + RoutingSuffix).val() == OceanTransportType
                    ? ($('#slRoutingVessels' + RoutingSuffix + ' option:selected').val() == "" ? 0 : $('#slRoutingVessels' + RoutingSuffix + ' option:selected').val())
                    : 0)
            , pVoyageOrTruckNumber: ($("#cbIsOwnedByCompany").prop("checked") ||  RoutingSuffix == "CustomsClearance" || $("#txtRoutingVoyageOrTruckNumber" + RoutingSuffix).val().trim() == "" ? "0" : $("#txtRoutingVoyageOrTruckNumber" + RoutingSuffix).val().trim().toUpperCase())
            , pTransientTime: RoutingSuffix == "CustomsClearance" || $("#txtRoutingTransientTime" + RoutingSuffix).val().trim() == "" ? 0 : $("#txtRoutingTransientTime" + RoutingSuffix).val()
            , pValidity: RoutingSuffix == "CustomsClearance" || $("#txtRoutingValidity" + RoutingSuffix).val().trim() == "" ? 0 : $("#txtRoutingValidity" + RoutingSuffix).val()
            , pFreeTime: RoutingSuffix == "CustomsClearance" || $("#txtRoutingFreeTime" + RoutingSuffix).val().trim() == "" ? 0 : $("#txtRoutingFreeTime" + RoutingSuffix).val()
            , pNotes: $("#txtRoutingNotes" + RoutingSuffix).val().toUpperCase()
            , pBLType: $("#hBLType").val()
            , pMasterBL: RoutingSuffix == "CustomsClearance" ? 0 : Routings_GetMasterBL()
            , pBLDate: RoutingSuffix == "CustomsClearance" ? "01/01/1900" : Routings_GetpBLDate() // in the controller it will be saved just in case its Main And Not House
            , pMAWBSuffix: (RoutingSuffix == "CustomsClearance" || $("#txtMAWBSuffix" + RoutingSuffix).val().trim() == "" ? 0 : $("#txtMAWBSuffix" + RoutingSuffix).val().trim())
            , pNumberOfHousesConnected: $("#hNumberOfHousesConnected").val()//used in the controller to be compared to NumberOfHousesConnected retrieved from DB at time of save to handle other sessions changes

            , pGensetSupplierID: RoutingSuffix == "CustomsClearance" || $("#slTruckingOrderGensetSupplier" + RoutingSuffix).val() == "" ? 0 : $("#slTruckingOrderGensetSupplier" + RoutingSuffix).val()
            , pCCAID: $("#slRoutingCCA" + RoutingSuffix).val() == "" ? 0 : $("#slRoutingCCA" + RoutingSuffix).val()
            , pQuantity: RoutingSuffix == "CustomsClearance" || $("#txtTruckingOrderQuantity" + RoutingSuffix).val().trim() == "" ? 0 : $("#txtTruckingOrderQuantity" + RoutingSuffix).val().trim().toUpperCase()
            , pContactPerson: RoutingSuffix == "CustomsClearance" ? 0 : $("#txtTruckingOrderContactPerson" + RoutingSuffix).val().trim().toUpperCase()
            , pTruckingOrderPickupAddress: RoutingSuffix == "CustomsClearance" ? 0 : $("#txtTruckingOrderPickupAddress" + RoutingSuffix).val().trim().toUpperCase()
            , pTruckingOrderDeliveryAddress: RoutingSuffix == "CustomsClearance" ? 0 : $("#txtTruckingOrderDeliveryAddress" + RoutingSuffix).val().trim().toUpperCase()
            , pGateInPortID: RoutingSuffix == "CustomsClearance" || $("#slTruckingOrderGateInPort" + RoutingSuffix).val().trim() == "" || $("#slTruckingOrderGateInPort" + RoutingSuffix).val() == null ? 0 : $("#slTruckingOrderGateInPort" + RoutingSuffix).val()
            , pGateOutPortID: RoutingSuffix == "CustomsClearance" || $("#slTruckingOrderGateOutPort" + RoutingSuffix).val().trim() == "" || $("#slTruckingOrderGateOutPort" + RoutingSuffix).val() == null ? 0 : $("#slTruckingOrderGateOutPort" + RoutingSuffix).val()
            , pGateInDate: (RoutingSuffix == "CustomsClearance" || $("#txtTruckingOrderGateInDate" + RoutingSuffix).val().trim() == "" ? "01/01/1900" : ConvertDateFormat($("#txtTruckingOrderGateInDate" + RoutingSuffix).val()))
            /****************************TransportOrder**************************/
            , pCustomerID: 0
            , pSubContractedCustomerID: 0
            , pCost: 0
            , pSale: 0
            , pIsFleet: false
            , pCommodityID: 0
            , pLoadingDate: $("#txtLoadingDate").val() == "" ? "01/01/1900" : ConvertDateFormat($("#txtLoadingDate").val())
            , pLoadingReference: 0
            , pUnloadingDate: $("#txtUnloadingDate").val() == "" ? "01/01/1900" : ConvertDateFormat($("#txtUnloadingDate").val())
            , pUnloadingReference: 0
            , pUnloadingTime: $("#txtUnloadingTime").val().trim() == "" ? 0 : $("#txtUnloadingTime").val().trim().toUpperCase()
            , pQuotationRouteID: 0
            /****************************TransportOrder**************************/
            , pGateOutDate: (RoutingSuffix == "CustomsClearance" || $("#txtTruckingOrderGateOutDate" + RoutingSuffix).val().trim() == "" ? "01/01/1900" : ConvertDateFormat($("#txtTruckingOrderGateOutDate" + RoutingSuffix).val()))
            , pStuffingDate: (RoutingSuffix == "CustomsClearance" || $("#txtTruckingOrderStuffingDate" + RoutingSuffix).val().trim() == "" ? "01/01/1900" : ConvertDateFormat($("#txtTruckingOrderStuffingDate" + RoutingSuffix).val()))
            , pDeliveryDate: (RoutingSuffix == "CustomsClearance" || $("#txtTruckingOrderDeliveryDate" + RoutingSuffix).val().trim() == "" ? "01/01/1900" : ConvertDateFormat($("#txtTruckingOrderDeliveryDate" + RoutingSuffix).val()))
            , pBookingNumber: RoutingSuffix == "CustomsClearance" ? ($("#txtCCBookingNumber").val().trim() == "" ? 0 : $("#txtCCBookingNumber").val().trim().toUpperCase()) : $("#txtTruckingOrderBookingNumber" + RoutingSuffix).val().trim().toUpperCase()
            , pDelays: RoutingSuffix == "CustomsClearance" ? 0 : $("#txtTruckingOrderDelays" + RoutingSuffix).val().trim().toUpperCase()
            , pDriverName: ($("#cbIsOwnedByCompany").prop("checked") || RoutingSuffix == "CustomsClearance") ? 0 : $("#txtTruckingOrderDriverName" + RoutingSuffix).val().trim().toUpperCase()
            , pDriverPhones: ($("#cbIsOwnedByCompany").prop("checked") || RoutingSuffix == "CustomsClearance") ? 0 : $("#txtTruckingOrderDriverPhones" + RoutingSuffix).val().trim().toUpperCase()
            , pPowerFromGateInTillActualSailing: RoutingSuffix == "CustomsClearance" ? 0 : $("#txtTruckingOrderPowerFromGateInTillActualSailing" + RoutingSuffix).val().trim().toUpperCase()

            , pContactPersonPhones: RoutingSuffix == "CustomsClearance" ? 0 : $("#txtTruckingOrderContactPersonPhones" + RoutingSuffix).val().trim().toUpperCase()
            , pLoadingTime: RoutingSuffix == "CustomsClearance" ? 0 : $("#txtTruckingOrderLoadingTime" + RoutingSuffix).val().trim().toUpperCase()

            , pCCAFreight: RoutingSuffix != "CustomsClearance" || $("#txtCCAFreight").val().trim() == "" ? 0 : $("#txtCCAFreight").val().trim().toUpperCase()
            , pCCAFOB: RoutingSuffix != "CustomsClearance" || $("#txtCCAFOB").val().trim() == "" ? 0 : $("#txtCCAFOB").val().trim().toUpperCase()
            , pCCACFValue: RoutingSuffix != "CustomsClearance" || $("#txtCCACFValue").val().trim() == "" ? 0 : $("#txtCCACFValue").val().trim().toUpperCase()
            , pCCAInvoiceNumber: RoutingSuffix != "CustomsClearance" || $("#txtCCAInvoiceNumber").val().trim() == "" ? 0 : $("#txtCCAInvoiceNumber").val().trim().toUpperCase()

            , pCCAInsurance: RoutingSuffix != "CustomsClearance" || $("#txtCCAInsurance").val().trim() == "" ? 0 : $("#txtCCAInsurance").val().trim().toUpperCase()
            , pCCADischargeValue: RoutingSuffix != "CustomsClearance" || $("#txtCCADischargeValue").val().trim() == "" ? 0 : $("#txtCCADischargeValue").val().trim().toUpperCase()
            , pCCAAcceptedValue: RoutingSuffix != "CustomsClearance" || $("#txtCCAAcceptedValue").val().trim() == "" ? 0 : $("#txtCCAAcceptedValue").val().trim().toUpperCase()
            , pCCAImportValue: RoutingSuffix != "CustomsClearance" || $("#txtCCAImportValue").val().trim() == "" ? 0 : $("#txtCCAImportValue").val().trim().toUpperCase()
            , pCCADocumentReceiveDate: (RoutingSuffix != "CustomsClearance" || $("#txtCCADocumentReceiveDate").val().trim() == "" ? "01/01/1900" : PadDateWithZeroes($("#txtCCADocumentReceiveDate").val().trim()))
            , pCCAExchangeRate: RoutingSuffix != "CustomsClearance" || $("#txtCCAExchangeRate").val().trim() == "" ? 0 : $("#txtCCAExchangeRate").val().trim().toUpperCase()
            , pCCAVATCertificateNumber: RoutingSuffix != "CustomsClearance" || $("#txtCCAVATCertificateNumber").val().trim() == "" ? 0 : $("#txtCCAVATCertificateNumber").val().trim().toUpperCase()
            , pCCAVATCertificateValue: RoutingSuffix != "CustomsClearance" || $("#txtCCAVATCertificateValue").val().trim() == "" ? 0 : $("#txtCCAVATCertificateValue").val().trim().toUpperCase()
            , pCCACommercialProfitCertificateNumber: RoutingSuffix != "CustomsClearance" || $("#txtCCACommercialProfitCertificateNumber").val().trim() == "" ? 0 : $("#txtCCACommercialProfitCertificateNumber").val().trim().toUpperCase()
            , pCCAOthers: RoutingSuffix != "CustomsClearance" || $("#txtCCAOthers").val().trim() == "" ? 0 : $("#txtCCAOthers").val().trim().toUpperCase()
            , pCCASpendDate: (RoutingSuffix != "CustomsClearance" || $("#txtCCASpendDate").val().trim() == "" ? "01/01/1900" : PadDateWithZeroes($("#txtCCASpendDate").val().trim()))

            , pCertificateNumber: RoutingSuffix != "CustomsClearance" || $("#txtCertificateNumber").val().trim() == "" ? 0 : $("#txtCertificateNumber").val().trim().toUpperCase()
            , pCertificateValue: RoutingSuffix != "CustomsClearance" || $("#txtCertificateValue").val().trim() == "" ? 0 : $("#txtCertificateValue").val().trim().toUpperCase()
            , pCertificateDate: (RoutingSuffix != "CustomsClearance" || $("#txtCertificateDate").val().trim() == "" ? "01/01/1900" : PadDateWithZeroes($("#txtCertificateDate").val().trim()))
            , pQasimaNumber: RoutingSuffix != "CustomsClearance" || $("#txtQasimaNumber").val().trim() == "" ? 0 : $("#txtQasimaNumber").val().trim().toUpperCase()
            , pQasimaDate: (RoutingSuffix != "CustomsClearance" || $("#txtQasimaDate").val().trim() == "" ? "01/01/1900" : PadDateWithZeroes($("#txtQasimaDate").val().trim()))
            , pMatch: RoutingSuffix != "CustomsClearance" || $("#match").prop("checked")
            , pSalesDateReceived: (RoutingSuffix != "CustomsClearance" || $("#txtSalesDateReceived").val().trim() == "" ? "01/01/1900" : PadDateWithZeroes($("#txtSalesDateReceived").val().trim()))
            , pCommerceDateReceived: (RoutingSuffix != "CustomsClearance" || $("#txtCommerceDateReceived").val().trim() == "" ? "01/01/1900" : PadDateWithZeroes($("#txtCommerceDateReceived").val().trim()))
            , pInspectionDateReceived: (RoutingSuffix != "CustomsClearance" || $("#txtInspectionDateReceived").val().trim() == "" ? "01/01/1900" : PadDateWithZeroes($("#txtInspectionDateReceived").val().trim()))
            , pFinishDateReceived: (RoutingSuffix != "CustomsClearance" || $("#txtFinishDateReceived").val().trim() == "" ? "01/01/1900" : PadDateWithZeroes($("#txtFinishDateReceived").val().trim()))
            , pSalesDateDelivered: (RoutingSuffix != "CustomsClearance" || $("#txtSalesDateDelivered").val().trim() == "" ? "01/01/1900" : PadDateWithZeroes($("#txtSalesDateDelivered").val().trim()))
            , pCommerceDateDelivered: (RoutingSuffix != "CustomsClearance" || $("#txtCommerceDateDelivered").val().trim() == "" ? "01/01/1900" : PadDateWithZeroes($("#txtCommerceDateDelivered").val().trim()))
            , pInspectionDateDelivered: (RoutingSuffix != "CustomsClearance" || $("#txtInspectionDateDelivered").val().trim() == "" ? "01/01/1900" : PadDateWithZeroes($("#txtInspectionDateDelivered").val().trim()))
            , pFinishDateDelivered: (RoutingSuffix != "CustomsClearance" || $("#txtFinishDateDelivered").val().trim() == "" ? "01/01/1900" : PadDateWithZeroes($("#txtFinishDateDelivered").val().trim()))


            , pCCDropBackReceived: ((RoutingSuffix != "CustomsClearance" || $("#txtCCDropBackReceived").val().trim() == "")  ? "01/01/1900" : ConvertDateFormat($("#txtCCDropBackReceived").val().trim()))
            , pCCDropBackDelivered: ((RoutingSuffix != "CustomsClearance" || $("#txtCCDropBackDelivered").val().trim() == "" ) ? "01/01/1900" : ConvertDateFormat($("#txtCCDropBackDelivered").val().trim()))
            , pCCAllowTemporaryReceived: ((RoutingSuffix != "CustomsClearance" || $("#txtCCAllowTemporaryReceived").val().trim()== "")  ? "01/01/1900" : ConvertDateFormat($("#txtCCAllowTemporaryReceived").val().trim()))
            , pCCAllowTemporaryDelivered: ((RoutingSuffix != "CustomsClearance" || $("#txtCCAllowTemporaryDelivered").val().trim()== "")  ? "01/01/1900" : ConvertDateFormat($("#txtCCAllowTemporaryDelivered").val().trim()))
            , pCC_ClearanceTypeID: RoutingSuffix != "CustomsClearance" || $("#slCC_ClearanceTypeID").val().trim() == "" ? 0 : $("#slCC_ClearanceTypeID").val().trim().toUpperCase()
            , pCC_CustomItemsID: RoutingSuffix != "CustomsClearance" || $("#slCC_CustomItemsID").val().trim() == "" ? 0 : $("#slCC_CustomItemsID").val().trim().toUpperCase()
            , pCCReleaseNo: RoutingSuffix != "CustomsClearance" || IsNull($("#txtCCReleaseNo").val().trim(), "") == "" ? 0 : $("#txtCCReleaseNo").val()



            , pRoadNumber: (RoutingSuffix != "" || $("#txtRoutingRoadNumber" + RoutingSuffix).val().trim() == "" ? "0" : $("#txtRoutingRoadNumber").val().trim().toUpperCase())
            , pDeliveryOrderNumber: (RoutingSuffix != "" || $("#txtRoutingDeliveryOrderNumber" + RoutingSuffix).val().trim() == "" ? "0" : $("#txtRoutingDeliveryOrderNumber").val().trim().toUpperCase())
            , pWareHouse: (RoutingSuffix != "" || $("#txtRoutingWareHouse" + RoutingSuffix).val().trim() == "" ? "0" : $("#txtRoutingWareHouse").val().trim().toUpperCase())
            , pWareHouseLocation: (RoutingSuffix != "" || $("#txtRoutingWareHouseLocation" + RoutingSuffix).val().trim() == "" ? "0" : $("#txtRoutingWareHouseLocation").val().trim().toUpperCase())

            , pIsOwnedByCompany: $("#cbIsOwnedByCompany").prop("checked")
            , pTrailerID: (!$("#cbIsOwnedByCompany").prop("checked") || $("#slTrailer" + RoutingSuffix).val() == "" ? 0 : $("#slTrailer" + RoutingSuffix).val())
            , pDriverID: (!$("#cbIsOwnedByCompany").prop("checked") || $("#slDriver" + RoutingSuffix).val() == "" ? 0 : $("#slDriver" + RoutingSuffix).val())
            , pDriverAssistantID: (!$("#cbIsOwnedByCompany").prop("checked") || $("#slDriverAssistant" + RoutingSuffix).val() == "" ? 0 : $("#slDriverAssistant" + RoutingSuffix).val())
            , pEquipmentID: $("#slEquipment" + RoutingSuffix).val() == "" ? 0 : $("#slEquipment" + RoutingSuffix).val()
            , pLoadingZoneID: $("#slRoutingsLoadingZone" + RoutingSuffix).val() == "" ? 0 : $("#slRoutingsLoadingZone" + RoutingSuffix).val()
            , pFirstCuringAreaID: $("#slRoutingsFirstCuringArea" + RoutingSuffix).val() == "" ? 0 : $("#slRoutingsFirstCuringArea" + RoutingSuffix).val()
            , pSecondCuringAreaID: $("#slRoutingsSecondCuringArea" + RoutingSuffix).val() == "" ? 0 : $("#slRoutingsSecondCuringArea" + RoutingSuffix).val()
            , pThirdCuringAreaID: $("#slRoutingsThirdCuringArea" + RoutingSuffix).val() == "" ? 0 : $("#slRoutingsThirdCuringArea" + RoutingSuffix).val()
            , pBillNumber: RoutingSuffix == "CustomsClearance" || $("#txtBillNumber" + RoutingSuffix).val() == undefined ? 0 : $("#txtBillNumber" + RoutingSuffix).val().trim().toUpperCase()
            ,pTruckingOrderCode : ''
            , pOffloadingDate: "01/01/1900"
            , pLastTruckCounter: 0
            , pMaxSupplierContainers: 0
        };
        CallPOSTFunctionWithParameters("/api/Routings/Update", pParametersWithValues
            , function (pData) {
                var pSavedRoute = JSON.parse(pData[2]);
                var pRoutings = JSON.parse(pData[3]);
                if (pData[1] != "") //pData[1]: is a message returned from controller in case of change in another session that prevents saving main route
                    swal(strSorry, pData[1]);
                else {
                    Routings_BindTableRows(pRoutings);
                    //set lblRouting,..... incase of changing MainCarraige Type
                    if ($("#slRoutingTypes" + RoutingSuffix + " option:selected").val() == MainCarraigeRoutingTypeID) {

                        $("#lblRouting" + RoutingSuffix).html(" : " + $("#slRoutingsPOL" + RoutingSuffix + " option:selected").text().substring(0, 5) + " > " + $("#slRoutingsPOD" + RoutingSuffix + " option:selected").text().substring(0, 5));
                        if ($("#hBLType").val() != constHouseBLType) {
                            var x = Routings_GetMasterBL();
                            $("#lblMaster").html(" :" + (x == 0 ? "N/A" : x));
                            $("#hMasterBL").val(x);
                        }
                        $("#hPOLCountryID").val($('#slRoutingsPOLCountries' + RoutingSuffix + ' option:selected').val());
                        $("#hPODCountryID").val($('#slRoutingsPODCountries' + RoutingSuffix + ' option:selected').val());
                        $("#hPOL").val($('#slRoutingsPOL' + RoutingSuffix + ' option:selected').val());
                        $("#hPOD").val($('#slRoutingsPOD' + RoutingSuffix + ' option:selected').val());

                        $("#hPickupAddress").val($('#txtPickupAddress' + RoutingSuffix).val().trim().toUpperCase());
                        $("#hDeliveryAddress").val($('#txtDeliveryAddress' + RoutingSuffix).val().trim().toUpperCase());

                        $("#hShippingLineID").val($("#hRoutingTransportType" + RoutingSuffix).val() == OceanTransportType ? $('#slRoutingsLines' + RoutingSuffix + ' option:selected').val() : 0);
                        $("#hAirlineID").val($("#hRoutingTransportType" + RoutingSuffix).val() == AirTransportType ? $("#slRoutingsLines" + RoutingSuffix).val() : 0);
                        $("#hTruckerID").val($("#hRoutingTransportType" + RoutingSuffix).val() == InlandTransportType ? $("#slRoutingsLines" + RoutingSuffix).val() : 0);

                        if ($("#hBLType").val() != constHouseBLType && $("#hTransportType").val() == AirTransportType) {//not house and air,(i am sure its main isa)
                            $("#hMAWBSuffix").val($("#txtMAWBSuffix" + RoutingSuffix).val().trim() == "" ? 0 : $("#txtMAWBSuffix" + RoutingSuffix).val().trim());
                            $("#hBLDate").val($("#txtMAWBDate" + RoutingSuffix).val().trim() == "" ? "01/01/1900" : $("#txtMAWBDate" + RoutingSuffix).val().trim());
                        }
                        if ($("#hBLType").val() != constHouseBLType && $("#hTransportType").val() != AirTransportType) {//not house and bot aie,(i am sure its main isa)
                            $("#hBLDate").val($("#txtOBLDate" + RoutingSuffix).val().trim() == "" ? "01/01/1900" : $("#txtOBLDate" + RoutingSuffix).val().trim());
                        }
                        //if ($("#hBLType").val() != constHouseBLType)
                        //    $("#hMasterBL").val(Routings_GetMasterBL());
                        //in the next condition, its already MainRoute Type
                        if ($("#hBLType").val() == constMasterBLType) //if its Master then i need to Reload Shipments for the case its a MainRoute
                            Shipments_LoadAvailableShipments();
                    }
                    $("#hRoutingID" + RoutingSuffix).val(pSavedRoute.ID);
                    if (RoutingSuffix != "CustomsClearance")
                        jQuery("#" + "RoutingModal" + RoutingSuffix).modal("hide");
                    swal("Success", "Saved successfully,");
                }
                FadePageCover(false);
                //debugger;
                //if ($("#hBLType").val() == constMasterBLType) {
                //    SwitchToOperationsEditView($("#hOperationID").val());
                //    ActivateRoutingsTab();
                //}
            }, null);
    }
}
function Routings_EditByDblClick(pID) {
    var tr = $("#tblRoutings tr[ID='" + pID + "']");
    if ($(tr).find("td.RoutingType").attr("val") == MainCarraigeRoutingTypeID)
        RoutingSuffix = "";
    else if ($(tr).find("td.RoutingType").attr("val") == TruckingOrderRoutingTypeID)
        RoutingSuffix = "TruckingOrder";
    else if ($(tr).find("td.RoutingType").attr("val") == CustomsClearanceRoutingTypeID)
        RoutingSuffix = "CustomsClearance";
    jQuery("#RoutingModal" + RoutingSuffix).modal("show");
    Routings_FillControls(pID);
}
function Routings_DeleteList(callback) {
    //Confirmation message to delete
    if (GetAllSelectedIDsAsString('tblRoutings') != "")
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
            DeleteListFunction("/api/Routings/Delete"
                , { "pRoutingsIDs": GetAllSelectedIDsAsString('tblRoutings') }
                , function () {
                    LoadWithPagingWithWhereClause("div-Pager", "select-page-size", "spn-first-page-row", "spn-last-page-row", "spn-total-count", "div-Text-Total", "/api/Routings/LoadWithWhereClause", " where OperationID = " + $("#hOperationID").val(), 0, 1000
                        , function (pData) {
                            Routings_BindTableRows(JSON.parse(pData[0]));
                        });
                });
        });
}
function Routings_ClearAllControls(pRoutingTypeName) {
    debugger;
    if (pRoutingTypeName == "CustomsClearance" && $("#tblRoutings tbody td.RoutingType[val=" + CustomsClearanceRoutingTypeID + "]").length > 0)
        swal("Sorry", "The operation can have just one customs clearance record.");
    else {
        if (pRoutingTypeName == "CustomsClearance") {
            jQuery("#RoutingModalCustomsClearance").modal("show");
            Routings_FillRoutingModal(0);
        }
        ClearAll("#RoutingModalTruckingOrder"); //i want it cleared in all cases to use trucking control names
        if (pRoutingTypeName == "" || pRoutingTypeName == undefined)
            RoutingSuffix == "";
        else
            RoutingSuffix = pRoutingTypeName;
        if (pRoutingTypeName != "TruckingOrder") //otherwise then the modal is already cleared up
            ClearAll("#RoutingModal" + RoutingSuffix);
        //set to default values
        //$("#cbIsOceanRouting"+RoutingSuffix).prop('checked', true);
        $("#cbIsInlandRouting" + RoutingSuffix).prop('checked', true);
        $("#txtPickupAddress" + RoutingSuffix).val($("#hPickupAddress").val());
        $("#txtDeliveryAddress" + RoutingSuffix).val($("#hDeliveryAddress").val());
        $("#slTruckingOrderGateInPort" + RoutingSuffix).val("");
        $("#slTruckingOrderGateOutPort" + RoutingSuffix).val("");

        if (pDefaults.UnEditableCompanyName == "ELI") {
            $("#txtCCBookingNumber").val($("#txtOperationBookingNumber").val());
            $("#txtTruckingOrderBookingNumber" + RoutingSuffix).val($("#txtOperationBookingNumber").val());
            
        }


        //GetListWithNameAndWhereClause(null, "/api/Suppliers/LoadAll", "<--Select-->", "slTruckingOrderGensetSupplier" + RoutingSuffix, "ORDER BY Name", null);
        //GetListWithNameAndWhereClause(null, "/api/CustomsClearanceAgents/LoadAll", "<--Select-->", "slRoutingCCA" + RoutingSuffix, "ORDER BY Name", null);
        Routings_ShowHideOwnedByCompanyProperties(false);
        if (RoutingSuffix == "TruckingOrder") {
            GetListWithNameAndWhereClause(null
                , "/api/Suppliers/LoadAll"
                , TranslateString("SelectFromMenu"), "slTruckingOrderGensetSupplier" + RoutingSuffix
                , "WHERE ID IN (SELECT PartnerID FROM vwOperationPartners WHERE PartnerTypeID=" + constSupplierPartnerTypeID + " AND OperationID=" + $("#hOperationID").val() + ") ORDER BY Name"
                , null);
            CallGETFunctionWithParameters("/api/Routings/FillRoutingModal"
                , { pRouteIDToFillModal: 0, pOperationID: $("#hOperationID").val() }
                , function (pData) {
                    var _Operation = JSON.parse(pData[1]);
                    var _Route = JSON.parse(pData[2]);
                    //$("#txtTruckingOrderPickupAddressTruckingOrder").val(_Operation.ClientAddress == 0 ? "" : _Operation.ClientAddress);
                    $("#txtTruckingOrderPickupAddressTruckingOrder").val($("#hClientPickupAddress").val());
            }
            , null);
        }
        else {//not TruckingOrder so i don't need to add GensetSuppliers
            $("#slTruckingOrderGensetSupplier" + RoutingSuffix).html("<option value=''>"+TranslateString("SelectFromMenu")+"</option>");
        }
        GetListWithNameAndWhereClause(null
            , "/api/CustomsClearanceAgents/LoadAll"
            , TranslateString("SelectFromMenu"), "slRoutingCCA" + RoutingSuffix
            , "WHERE ID IN (SELECT PartnerID FROM vwOperationPartners WHERE PartnerTypeID=" + constCustomsClearanceAgentPartnerTypeID + " AND OperationID=" + $("#hOperationID").val() + ") ORDER BY Name"
            , null);

        Routings_ShowHideTruckingOrderFields(true);
        Routings_TransportType_SetIconNameAndStyle();
        Routings_EnableCbTransportType();

        //fill select boxes
        //RoutingTypes_GetList(RoutingSuffix == "TruckingOrder" ? TruckingOrderRoutingTypeID : CustomsClearanceRoutingTypeID, null);
        if (RoutingSuffix == "")
            $("#slRoutingTypes" + RoutingSuffix).html('<option value="' + MainCarraigeRoutingTypeID + '">MAIN CARRAIGE</option>');
        else if (RoutingSuffix == "TruckingOrder")
            $("#slRoutingTypes" + RoutingSuffix).html('<option value="' + TruckingOrderRoutingTypeID + '">TRUCKING ORDER</option>');
        else if (RoutingSuffix == "CustomsClearance")
            $("#slRoutingTypes" + RoutingSuffix).html('<option value="' + CustomsClearanceRoutingTypeID + '">Customs Clearance</option>');

        Routings_Countries_GetList($("#hDefaultCountryID").val(), $("#hDefaultCountryID").val(), null);//Routings_Countries_GetList(null, null, null);
        Routings_Ports_GetList(null, $("#hDefaultCountryID").val(), 1, function () { $("#slRoutingsPOL" + RoutingSuffix).val($("#slRoutingsPOL" + RoutingSuffix + " option:contains(EG000)").val()); }); //Routings_Ports_GetList(null, null, 1);
        Routings_Ports_GetList(null, $("#hDefaultCountryID").val(), 2, function () { $("#slRoutingsPOD" + RoutingSuffix).val($("#slRoutingsPOD" + RoutingSuffix + " option:contains(EG000)").val()); }); //Routings_Ports_GetList(null, null, 2);
        Routings_Lines_GetList(null, function () {
            //if ($("#hRoutingTransportType" + RoutingSuffix).val() == OceanTransportType)
            //    Routings_Vessels_GetList(null, null, "slRoutingVessels" + RoutingSuffix, null);
            //else
            //    $("#slRoutingVessels" + RoutingSuffix).val("");
        });
        Routings_EnableControlsForNewAdd();
        Routings_HideMasterBL(); //I am sure its not main routing(coz main routing cant be deleted)
        Routings_ShowHideVessels();
        Routings_ShowHidePickupAndDeliveryAddress(false); //false: coz it will never be main route
        //Operation_cbPickupOrDeliveryChange();//to show hide Delivery and Pickup Cities according to checkboxes
        //PickupCity_GetList(item.PickupCityID, item.POLCountryID);
        //DeliveryCity_GetList(item.DeliveryCityID, item.PODCountryID);


        $("#btnSaveRouting" + RoutingSuffix).attr("onclick", "Routings_Insert(false);");
        $("#btnSaveandNewRouting" + RoutingSuffix).attr("onclick", "Routings_Insert(true);");

    } //if (pRoutingTypeName == "CustomsClearance" && $("#tblRoutings tbody td.RoutingType[val=" + CustomsClearanceRoutingTypeID + "]").length > 0)
}
function Routings_FillControls(pID) {
    debugger;
    ClearAll("#RoutingModalTruckingOrder"); //i want it cleared in all cases to use trucking control names
    if (RoutingSuffix != "TruckingOrder") //otherwise then the modal is already cleared up
        ClearAll("#RoutingModal" + RoutingSuffix);

    var tr = $("#tblRoutings tr[ID='" + pID + "']");

    $("#hRoutingID" + RoutingSuffix).val(pID);
    $("#hRoutingTransportType" + RoutingSuffix).val($(tr).find("td.TransportType").attr("val"));
    $("#hRoutingTransportIconName" + RoutingSuffix).val($(tr).find("td.TransportIconName").text());
    $("#hRoutingTransportIconStyle" + RoutingSuffix).val($(tr).find("td.TransportIconStyle").text());
    $("#lblRoutingShown" + RoutingSuffix).html(": " + $(tr).find("td.RoutingType").text());
    $("#txtPickupAddress" + RoutingSuffix).val($("#hPickupAddress").val());
    $("#txtDeliveryAddress" + RoutingSuffix).val($("#hDeliveryAddress").val());

    Routings_SetCbRoutingTransportType($("#hRoutingTransportType" + RoutingSuffix).val());
    Routings_ShowHideTruckingOrderFields($(tr).find("td.RoutingType").attr("val") == TruckingOrderRoutingTypeID ? true : false);

    var pPOLCountryID = $(tr).find("td.POLCountry").attr("val");
    var pPODCountryID = $(tr).find("td.PODCountry").attr("val");
    var pPOLID = $(tr).find("td.POL").attr("val");
    var pPODID = $(tr).find("td.POD").attr("val");
    var pLineID = $(tr).find("td.Line").attr("val");
    var pVesselID = $(tr).find("td.Vessel").attr("val");
    var ETAPOLDate = $(tr).find("td.ETAPOLDate").attr("val");
    var ATAPOLDate = $(tr).find("td.ATAPOLDate").attr("val");
    var ExpectedArrival = $(tr).find("td.ExpectedArrival").attr("val");
    var ExpectedDeparture = $(tr).find("td.ExpectedDeparture").attr("val");
    var ActualArrival = $(tr).find("td.ActualArrival").attr("val");
    var ActualDeparture = $(tr).find("td.ActualDeparture").attr("val");
    var pGensetSupplierID = $(tr).find("td.GensetSupplierID").text();
    var pCCAID = $(tr).find("td.CCAID").text();
    var pGateInPortID = $(tr).find("td.GateInPortID").text() == 0 ? "" : $(tr).find("td.GateInPortID").text();
    var pGateOutPortID = $(tr).find("td.GateOutPortID").text() == 0 ? "" : $(tr).find("td.GateOutPortID").text();
    var pCCAID = $(tr).find("td.CCAID").text();
    $("#slTruckingOrderGateInPort" + RoutingSuffix).val(pGateInPortID);
    $("#slTruckingOrderGateOutPort" + RoutingSuffix).val(pGateOutPortID);
    //fill select boxes
    //RoutingTypes_GetList($(tr).find("td.RoutingType").attr("val"), null);
    if (RoutingSuffix == "") {
        $("#slRoutingTypes" + RoutingSuffix).html('<option value="' + MainCarraigeRoutingTypeID + '">MAIN CARRAIGE</option>');
        $("#txtRoutingRoadNumber").val($(tr).find("td.RoadNumber").text());
        $("#txtRoutingDeliveryOrderNumber").val($(tr).find("td.DeliveryOrderNumber").text());
        $("#txtRoutingWareHouse").val($(tr).find("td.WareHouse").text());
        $("#txtRoutingWareHouseLocation").val($(tr).find("td.WareHouseLocation").text());
    }
    else if (RoutingSuffix == "TruckingOrder") {
        $("#slRoutingTypes" + RoutingSuffix).html('<option value="' + TruckingOrderRoutingTypeID + '">TRUCKING ORDER</option>');
    }
    else if (RoutingSuffix == "CustomsClearance") {
        $("#slRoutingTypes" + RoutingSuffix).html('<option value="' + CustomsClearanceRoutingTypeID + '">Customs Clearance</option>');
        Routings_FillRoutingModal(pID);

        CustomClearanceTracking_LoadData()
    }

    //GetListWithNameAndWhereClause(pGensetSupplierID, "/api/Suppliers/LoadAll", "<--Select-->", "slTruckingOrderGensetSupplier" + RoutingSuffix, "ORDER BY Name", null);
    //GetListWithNameAndWhereClause(pCCAID, "/api/CustomsClearanceAgents/LoadAll", "<--Select-->", "slRoutingCCA" + RoutingSuffix, "ORDER BY Name", null);
    if (RoutingSuffix == "TruckingOrder")
        GetListWithNameAndWhereClause(pGensetSupplierID
            , "/api/Suppliers/LoadAll"
            , TranslateString("SelectFromMenu"), "slTruckingOrderGensetSupplier" + RoutingSuffix
            , "WHERE ID IN (SELECT PartnerID FROM vwOperationPartners WHERE PartnerTypeID=" + constSupplierPartnerTypeID + " AND OperationID=" + $("#hOperationID").val() + ") ORDER BY Name"
            , null);
    else //not TruckingOrder so i don't need to add GensetSuppliers
        $("#slTruckingOrderGensetSupplier" + RoutingSuffix).html("<option value=''>" + TranslateString("SelectFromMenu") + "</option>");
    GetListWithNameAndWhereClause(pCCAID
        , "/api/CustomsClearanceAgents/LoadAll"
        , TranslateString("SelectFromMenu"), "slRoutingCCA" + RoutingSuffix
        , "WHERE ID IN (SELECT PartnerID FROM vwOperationPartners WHERE PartnerTypeID=" + constCustomsClearanceAgentPartnerTypeID + " AND OperationID=" + $("#hOperationID").val() + ") ORDER BY Name"
        , null);

    Routings_ShowHidePickupAndDeliveryAddress($(tr).find("td.RoutingType").attr("val") == MainCarraigeRoutingTypeID ? true : false);

    //Incase of (Master OR Direct) && Main Route so i have to show MasterBL
    if (($("#hBLType").val() == constMasterBLType || $("#hBLType").val() == constDirectBLType) && $(tr).find("td.RoutingType").attr("val") == MainCarraigeRoutingTypeID)
        Routings_Show_Suitable_MasterBL();
    else
        Routings_HideMasterBL();
    Routings_Set_btnMAWBStockCaption(); //to write wether (Get From Stock) or (Return To Stock)

    if ($(tr).find("td.RoutingType").attr("val") == MainCarraigeRoutingTypeID) {
        $("#slRoutingTypes" + RoutingSuffix).attr("disabled", "disabled"); //coz its main
        Routings_DisableCbTransportType(); //to enable or disable cbTransportType
        //Incase of Master && Connected
        if ($("#hBLType").val() == constMasterBLType && parseInt($("#hNumberOfHousesConnected").val()) > 0)
            Routings_Master_And_Connected_Properties();
        //Incase of Master && Not Connected
        if ($("#hBLType").val() == constMasterBLType && parseInt($("#hNumberOfHousesConnected").val()) == 0)
            Routings_Master_And_NotConnected_Properties();
        //Incase of House && Connected
        if ($("#hBLType").val() == constHouseBLType && parseInt($("#hMasterOperationID").val()) > 0)
            Routings_House_And_Connected_Properties();
        //Incase of House && Connected
        if ($("#hBLType").val() == constHouseBLType && parseInt($("#hMasterOperationID").val()) == 0)
            Routings_House_And_NotConnected_Properties();
        //Incase of Direct And MAWB taken from stock
        if ($("#hBLType").val() == constDirectBLType && $("#hMAWBStockID").val() != "0")
            Routings_DisableLines();
        //Incase of Direct And MAWB Not taken from stock
        if ($("#hBLType").val() == constDirectBLType && $("#hMAWBStockID").val() == "0")
            Routings_EnableLines();

        //to Add/Remove data-target attr that calls the modal according to (Return to Stock or Get From Stock)
        if ($("#hMAWBStockID").val() == "0")
            Routings_Add_DataTarget_Attr_To_btnMAWBStock();//so modal opens to select MAWB from
        else // do return MAWB to stock
            Routings_Remove_DataTarget_Attr_From_btnMAWBStock();
    }
    else {
        $("#slRoutingTypes" + RoutingSuffix).removeAttr("disabled");
        Routings_EnableCbTransportType();
        Routings_EnablePorts();
        Routings_EnableDates();
        Routings_EnableLines();
    }
    //Enable/Disable $("#txtMAWBStock")
    if ($("#hMAWBStockID").val() == "0")
        Routings_EnableMAWBSuffix();
    else
        Routings_DisableMAWBSuffix();

    Routings_Countries_GetList(pPOLCountryID, pPODCountryID, null);
    Routings_Ports_GetList(pPOLID, pPOLCountryID, 1);
    Routings_Ports_GetList(pPODID, pPODCountryID, 2);
    /////////////////////////////////////Todo: Fill Via Ports here///////////////////////////
    //Routings_Lines_GetList(pLineID, null);
    Routings_Lines_GetList(pLineID
        , function () {
            if ($("#hRoutingTransportType" + RoutingSuffix).val() == OceanTransportType) {
                $(".classRoutingVessels" + RoutingSuffix).removeClass("hide");
                if ($("#slRoutingVessels option").length < 2)
                    Routings_Vessels_GetList(pVesselID, pLineID, "slRoutingVessels", function () { $("#slRoutingFeeders").html($("#slRoutingVessels").html()); });
                else {
                    $("#slRoutingVessels").val(pVesselID);
                    //$("#slRoutingFeeders").val(pFeederID);
                }
            }
            else {
                $(".classRoutingVessels" + RoutingSuffix).addClass("hide");
                $("#slRoutingVessels" + RoutingSuffix).val("");
                $("#slRoutingFeeders").val("");
            }
        });

    $("#txtETAPOLDate" + RoutingSuffix).val(Date.prototype.compareDates("01/01/1900", ETAPOLDate) < 1 ? "" : ConvertDateFormat(ETAPOLDate));
    $("#txtATAPOLDate" + RoutingSuffix).val(Date.prototype.compareDates("01/01/1900", ATAPOLDate) < 1 ? "" : ConvertDateFormat(ATAPOLDate));
    $("#txtExpectedArrival" + RoutingSuffix).val(Date.prototype.compareDates("01/01/1900", ExpectedArrival) < 1 ? "" : ConvertDateFormat(ExpectedArrival));
    $("#txtExpectedDeparture" + RoutingSuffix).val(Date.prototype.compareDates("01/01/1900", ExpectedDeparture) < 1 ? "" : ConvertDateFormat(ExpectedDeparture));
    $("#txtActualArrival" + RoutingSuffix).val(Date.prototype.compareDates("01/01/1900", ActualArrival) < 1 ? "" : ConvertDateFormat(ActualArrival));
    $("#txtActualDeparture" + RoutingSuffix).val(Date.prototype.compareDates("01/01/1900", ActualDeparture) < 1 ? "" : ConvertDateFormat(ActualDeparture));
    $("#txtLoadingDate").val($(tr).find("td.LoadingDate").text()); //Starting Date For MED
    $("#txtUnloadingDate").val($(tr).find("td.UnloadingDate").text()); //Receiving Date For MED

    if ($("#hBlType").val() != constHouseBLType && $("#hTransportType").val() == AirTransportType) {
        $("#txtMAWBSuffix" + RoutingSuffix).val($("#hMAWBSuffix").val().toString() == "0" ? "" : $("#hMAWBSuffix").val());
        $("#txtMAWBDate" + RoutingSuffix).val(Date.prototype.compareDates("01/01/1900", ConvertDateFormat($("#hBLDate").val())) < 1 ? "" : $("#hBLDate").val());
    }
    if ($("#hBlType").val() != constHouseBLType && $("#hTransportType").val() != AirTransportType) {
        $("#txtOBL" + RoutingSuffix).val(($("#hMasterBL").val() == 0 ? "" : $("#hMasterBL").val()));
        $("#txtOBLDate" + RoutingSuffix).val(Date.prototype.compareDates("01/01/1900", ConvertDateFormat($("#hBLDate").val())) < 1 ? "" : $("#hBLDate").val());
    }
    $("#txtRoutingVoyageOrTruckNumber" + RoutingSuffix).val($(tr).find("td.VoyageOrTruckNumber").text());
    $("#txtRoutingTransientTime" + RoutingSuffix).val($(tr).find("td.TransientTime").text());
    $("#txtRoutingFreeTime" + RoutingSuffix).val($(tr).find("td.FreeTime").text());
    $("#txtRoutingValidity" + RoutingSuffix).val($(tr).find("td.Validity").text());
    $("#txtRoutingNotes" + RoutingSuffix).val($(tr).find("td.Notes").text());

    $("#txtTruckingOrderQuantity" + RoutingSuffix).val($(tr).find("td.Quantity").text());
    $("#txtTruckingOrderContactPerson" + RoutingSuffix).val($(tr).find("td.ContactPerson").text());
    $("#txtTruckingOrderPickupAddress" + RoutingSuffix).val($(tr).find("td.PickupAddress").text());
    $("#txtTruckingOrderDeliveryAddress" + RoutingSuffix).val($(tr).find("td.DeliveryAddress").text());
    $("#txtTruckingOrderGateInDate" + RoutingSuffix).val($(tr).find("td.GateInDate").text());
    $("#txtTruckingOrderGateOutDate" + RoutingSuffix).val($(tr).find("td.GateOutDate").text());
    $("#txtTruckingOrderStuffingDate" + RoutingSuffix).val($(tr).find("td.StuffingDate").text());
    $("#txtTruckingOrderDeliveryDate" + RoutingSuffix).val($(tr).find("td.DeliveryDate").text());
    $("#txtTruckingOrderBookingNumber" + RoutingSuffix).val($(tr).find("td.BookingNumber").text());
    if (pDefaults.UnEditableCompanyName == "ELI") {
        $("#txtCCBookingNumber").val($("#txtOperationBookingNumber").val());
        $("#txtTruckingOrderBookingNumber" + RoutingSuffix).val($("#txtOperationBookingNumber").val());
    }

    $("#txtTruckingOrderDelays" + RoutingSuffix).val($(tr).find("td.Delays").text());
    $("#txtTruckingOrderDriverName" + RoutingSuffix).val($(tr).find("td.DriverName").text());
    $("#txtTruckingOrderDriverPhones" + RoutingSuffix).val($(tr).find("td.DriverPhones").text());
    $("#txtTruckingOrderPowerFromGateInTillActualSailing" + RoutingSuffix).val($(tr).find("td.PowerFromGateInTillActualSailing").text());

    $("#txtTruckingOrderContactPersonPhones" + RoutingSuffix).val($(tr).find("td.ContactPersonPhones").text());
    $("#txtTruckingOrderLoadingTime" + RoutingSuffix).val($(tr).find("td.LoadingTime").text());
    $("#txtUnloadingTime").val($(tr).find("td.UnloadingTime").text());
    $("#txtBillNumber" + RoutingSuffix).val($(tr).find("td.BillNumber").text());

    $("#txtCCBookingNumber").val($(tr).find("td.BookingNumber").text());
    $("#txtCCAFreight").val($(tr).find("td.CCAFreight").text());
    $("#txtCCAFOB").val($(tr).find("td.CCAFOB").text());
    $("#txtCCACFValue").val($(tr).find("td.CCACFValue").text());
    $("#txtCCAInvoiceNumber").val($(tr).find("td.CCAInvoiceNumber").text());

    $("#txtCCAInsurance").val($(tr).find("td.CCAInsurance").text());
    $("#txtCCADischargeValue").val($(tr).find("td.CCADischargeValue").text());
    $("#txtCCAAcceptedValue").val($(tr).find("td.CCAAcceptedValue").text());
    $("#txtCCAImportValue").val($(tr).find("td.CCAImportValue").text());
    $("#txtCCADocumentReceiveDate").val($(tr).find("td.CCADocumentReceiveDate").text());
    $("#txtCCAExchangeRate").val($(tr).find("td.CCAExchangeRate").text());
    $("#txtCCAVATCertificateNumber").val($(tr).find("td.CCAVATCertificateNumber").text());
    $("#txtCCAVATCertificateValue").val($(tr).find("td.CCAVATCertificateValue").text());
    $("#txtCCACommercialProfitCertificateNumber").val($(tr).find("td.CCACommercialProfitCertificateNumber").text());
    $("#txtCCAOthers").val($(tr).find("td.CCAOthers").text());
    $("#txtCCASpendDate").val($(tr).find("td.CCASpendDate").text());

    $("#txtCertificateNumber").val($(tr).find("td.CertificateNumber").text());
    $("#txtCertificateValue").val($(tr).find("td.CertificateValue").text());
    $("#txtCertificateDate").val($(tr).find("td.CertificateDate").text());
    $("#txtQasimaNumber").val($(tr).find("td.QasimaNumber").text());
    $("#txtQasimaDate").val($(tr).find("td.QasimaDate").text());
    $("#match").prop("checked", $(tr).find("td.Match").text());
    $("#txtSalesDateReceived").val($(tr).find("td.SalesDateReceived").text());
    $("#txtCommerceDateReceived").val($(tr).find("td.CommerceDateReceived").text());
    $("#txtInspectionDateReceived").val($(tr).find("td.InspectionDateReceived").text());
    $("#txtFinishDateReceived").val($(tr).find("td.FinishDateReceived").text());
    $("#txtSalesDateDelivered").val($(tr).find("td.SalesDateDelivered").text());
    $("#txtCommerceDateDelivered").val($(tr).find("td.CommerceDateDelivered").text());
    $("#txtInspectionDateDelivered").val($(tr).find("td.InspectionDateDelivered").text());
    $("#txtFinishDateDelivered").val($(tr).find("td.FinishDateDelivered").text());




    //---- mostaa 7-3-2021

    $("#slCC_ClearanceTypeID").val($(tr).find("td.CC_ClearanceTypeID").attr('val'));
    $("#slCC_CustomItemsID").val($(tr).find("td.CC_CustomItemsID").attr('val'));
    $("#txtCCReleaseNo").val($(tr).find("td.CCReleaseNo").attr('val'));
    $("#txtCCDropBackReceived").val(($(tr).find("td.CCDropBackReceived").attr('val') == "01/01/1900" ? "" : $(tr).find("td.CCDropBackReceived").attr('val') ) );
    $("#txtCCDropBackDelivered").val(($(tr).find("td.CCDropBackDelivered").attr('val') == "01/01/1900" ? "" : $(tr).find("td.CCDropBackDelivered").attr('val')));
    $("#txtCCAllowTemporaryReceived").val(($(tr).find("td.CCAllowTemporaryReceived").attr('val') == "01/01/1900" ? "" : $(tr).find("td.CCAllowTemporaryReceived").attr('val')));
    $("#txtCCAllowTemporaryDelivered").val(($(tr).find("td.CCAllowTemporaryDelivered").attr('val') == "01/01/1900" ? "" : $(tr).find("td.CCAllowTemporaryDelivered").attr('val')));





    $("#cbIsOwnedByCompany").prop("checked", $("#cbIsOwnedByCompany" + pID).prop("checked"));
    Routings_ShowHideOwnedByCompanyProperties($("#cbIsOwnedByCompany" + pID).prop("checked"));
    if (!$("#cbIsOwnedByCompany").prop("checked")) {
        $("#txtSupplierDriverName" + RoutingSuffix).val($(tr).find("td.DriverName" + RoutingSuffix).text());
        $("#txtSupplierDriverAssistantName" + RoutingSuffix).val($(tr).find("td.DriverAssistantName" + RoutingSuffix).text());
        $("#txtSupplierTrailerName" + RoutingSuffix).val($(tr).find("td.TrailerName" + RoutingSuffix).text());
    }
    else {
        $("#slEquipment" + RoutingSuffix).val(tr.find("td.EquipmentID").text()); //Shown and set always because elite uses in all cases
        $("#slTrailer" + RoutingSuffix).val(tr.find("td.TrailerID").text());
        $("#slDriver" + RoutingSuffix).val(tr.find("td.DriverID").text());
        $("#slDriverAssistant" + RoutingSuffix).val(tr.find("td.DriverAssistantID").text());
    }
    if (pDefaults.UnEditableCompanyName == "ELI") //Shown and set always because elite uses in all cases
        $("#slEquipment" + RoutingSuffix).val(tr.find("td.EquipmentID").text());

    $("#slRoutingsLoadingZone" + RoutingSuffix).val(tr.find("td.LoadingZoneID").text());
    $("#slRoutingsFirstCuringArea" + RoutingSuffix).val(tr.find("td.FirstCuringAreaID").text());
    $("#slRoutingsSecondCuringArea" + RoutingSuffix).val(tr.find("td.SecondCuringAreaID").text());
    $("#slRoutingsThirdCuringArea" + RoutingSuffix).val(tr.find("td.ThirdCuringAreaID").text());


    $("#btnSaveRouting" + RoutingSuffix).attr("onclick", "Routings_Update(false);");
    $("#btnSaveandNewRouting" + RoutingSuffix).attr("onclick", "Routings_Update(true);");
}
function Routings_FillRoutingModal(pRouteIDToFillModal) {
    debugger;
    var pParametersWithValues = {
        pRouteIDToFillModal: pRouteIDToFillModal
        , pOperationID: $("#hOperationID").val()
    };
    CallGETFunctionWithParameters("/api/Routings/FillRoutingModal", pParametersWithValues
        , function (pData) {
            var _Operation = JSON.parse(pData[1]);
            var _Route = JSON.parse(pData[2]);
            $("#lblCCAConsignee").text(": " + (_Operation.ConsigneeName == 0 ? "N/A" : _Operation.ConsigneeName));
            $("#lblCCAShipper").text(": " + (_Operation.ShipperName == 0 ? "N/A" : _Operation.ShipperName));
            $("#lblCCAMBL").text(": " + (_Operation.MasterBL == 0 ? "N/A" : _Operation.MasterBL));
            $("#lblCCAHBL").text(": " + (_Operation.HouseNumber == 0 ? "N/A" : _Operation.HouseNumber));
            $("#lblCCAShipment").text(": " + (_Operation.ShipmentTypeCode == 0 ? "N/A" : _Operation.ShipmentTypeCode));
            $("#lblCCACargo").text(": "
                + (_Operation.NumberOfVehicles > 0 
                    ? (_Operation.NumberOfVehicles + " Vehicles")
                    : (_Operation.ContainerTypes != 0 ? _Operation.ContainerTypes : (_Operation.NumberOfPackages + "x" + _Operation.PackageTypeName))
                  )
                );
            $("#lblCCACommodity").text(": " + (_Operation.CommodityName == 0 ? "N/A" : _Operation.CommodityName));
            $("#lblCCAPONumber").text(": " + (_Operation.PONumber == 0 ? "N/A" : _Operation.PONumber));
            $("#lblCCAPOValue").text(": " + (_Operation.POValue == 0 ? "N/A" : _Operation.POValue));
            $("#lblCCAPODate").text(": " + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(_Operation.PODate)) > 0 ? ConvertDateFormat(GetDateWithFormatMDY(_Operation.PODate)) : "N/A"));
            $("#lblCCAIncoterm").text(": " + (_Operation.IncotermName == 0 ? "N/A" : _Operation.IncotermName));
            $("#lblCCAPOD").text(": " + (_Operation.PODName == 0 ? "N/A" : _Operation.PODName));
            $("#lblCCAATAPOD").text(": " + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(_Operation.ActualArrival)) > 0 ? ConvertDateFormat(GetDateWithFormatMDY(_Operation.ActualArrival)) : "N/A"));
        }
        ,null);
}
function Routings_ShowHideOwnedByCompanyProperties(pIsCompanyOwned) {
    debugger;
    if ($("#cbIsOwnedByCompany").prop("checked") || pIsCompanyOwned) {
        $(".classOwnedByCompany").removeClass("hide");
        $(".classOwnedBySupplier").addClass("hide");
    }
    else {
        $(".classOwnedByCompany").addClass("hide");
        $(".classOwnedBySupplier").removeClass("hide");
    }
}
function Routings_ShowHideTruckingOrderFields(pFlag) {
    debugger;
    if (pFlag) {
        $("#divPorts" + RoutingSuffix).addClass("hide");
        $("#divPortDates" + RoutingSuffix).addClass("hide");
        $("#divTruckingOrder" + RoutingSuffix).removeClass("hide");
    }
    else {
        $("#divTruckingOrder" + RoutingSuffix).addClass("hide");
        $("#divPortDates" + RoutingSuffix).removeClass("hide");
        $("#divPorts" + RoutingSuffix).removeClass("hide");
    }
}
function Routings_TransportType_SetIconNameAndStyle() {
    if (RoutingSuffix == "CustomsClearance") {
        $("#hRoutingTransportType" + RoutingSuffix).val(CustomsClearanceTransportType);
        $("#hRoutingTransportIconName" + RoutingSuffix).val(CustomsClearanceIconName);
        $("#hRoutingTransportIconStyle" + RoutingSuffix).val(strCustomsClearanceIconStyleClassName);
    }
    else {
        if ($("#cbIsOceanRouting" + RoutingSuffix).prop('checked')) {
            $("#hRoutingTransportType" + RoutingSuffix).val(OceanTransportType);
            $("#hRoutingTransportIconName" + RoutingSuffix).val(OceanIconName);
            $("#hRoutingTransportIconStyle" + RoutingSuffix).val(strOceanIconStyleClassName);
            ////show section ShipmentType (FCL,LCL)
            //$("#secShipmentType" + RoutingSuffix).removeClass("hide");
            //$("#divOceanType" + RoutingSuffix).removeClass("hide");
            //$("#divInlandType" + RoutingSuffix).addClass("hide");
            ////set FCL as default
            //$("#cbIsFCL" + RoutingSuffix).prop('checked', true);
        }
        if ($("#cbIsAirRouting" + RoutingSuffix).prop('checked')) {
            $("#hRoutingTransportType" + RoutingSuffix).val(AirTransportType);
            $("#hRoutingTransportIconName" + RoutingSuffix).val(AirIconName);
            $("#hRoutingTransportIconStyle" + RoutingSuffix).val(strAirIconStyleClassName);
            ////hide section ShipmentType (FCL,LCL,FTL,LTL)
            //$("#secShipmentType" + RoutingSuffix).addClass("hide");
            ////uncheck all ShipmentTypes
            //$('input[name=cbShipmentType' + RoutingSuffix + ']').prop('checked', false);
        }
        if ($("#cbIsInlandRouting" + RoutingSuffix).prop('checked')) {
            $("#hRoutingTransportType" + RoutingSuffix).val(InlandTransportType);
            $("#hRoutingTransportIconName" + RoutingSuffix).val(InlandIconName);
            $("#hRoutingTransportIconStyle" + RoutingSuffix).val(strInlandIconStyleClassName);
            ////show section ShipmentType (FTL,LTL)
            //$("#secShipmentType" + RoutingSuffix).removeClass("hide");
            //$("#divOceanType" + RoutingSuffix).addClass("hide");
            //$("#divInlandType" + RoutingSuffix).removeClass("hide");
            ////set FTL as default
            //$("#cbIsFTL").prop('checked', true);
        }
    }
}
function RoutingTypes_GetList(pID, callback) { //the first parameter is used in case of editing to set the code or name to its original value
    debugger;
    var pWhereClause = "";
    //pWhereClause = " WHERE ID NOT IN (SELECT R.RoutingTypeID From Routings R ";
    //pWhereClause += " 					WHERE R.OperationID = " + $('#hOperationID').val() + ")";
    //pWhereClause += (pID != null && pID != undefined ? " OR ID = " + pID : ""); //this is fill so i need to retreive the edited type too
    //pWhereClause += " ORDER BY ViewOrder ";

    pWhereClause = " WHERE ID NOT IN (" + MainCarraigeRoutingTypeID + "," + PreCarraigeRoutingTypeID + "," + DeliveryRoutingTypeID + "," + PickupRoutingTypeID + "," + OnCarraigeRoutingTypeID + ")"; ////////////i get just 1 main carraige and any no of other routing types
    pWhereClause += (pID != null && pID != undefined ? " OR ID = " + pID : ""); //this is fill so i need to retreive the edited type too
    pWhereClause += " ORDER BY ViewOrder ";

    //parameters: ID, strFnName, First Row in select list, select list name, WhereClause
    //GetListWithRoutingTypesCodeAndWhereClauseAndPartnerTypeAttr(pID, "/api/NoAccessRoutingTypes/LoadAll", "Select Partner Type", "slRoutingTypes", pWhereClause);
    GetListWithNameAndWhereClause(pID, "/api/NoAccessRoutingTypes/LoadAll", null/*"Select Type"*/, "slRoutingTypes" + RoutingSuffix, pWhereClause
        , function () { //this callback inside the callback is to fill the slPartnerContacts
            if (callback != null && callback != undefined)
                callback();
        });
}
//fill slPOLCountries and slPODCountries
function Routings_Countries_GetList(pPOLCountryID, pPODCountryID, callback) {//pID is used in case of editing to set the code or name
    //parameters: ID, strFnName, First Row in select list, select list name
    GetListWithName(pPOLCountryID, "/api/Countries/LoadAll", "Select Country", "slRoutingsPOLCountries" + RoutingSuffix);
    GetListWithName(pPODCountryID, "/api/Countries/LoadAll", "Select Country", "slRoutingsPODCountries" + RoutingSuffix);
}
//POLorPOD: 1-POL 2-POD
function Routings_Ports_GetList(pID, pCountryID, POLorPOD, callback) { //all the commented COMMAND lines are to remove the IsPort condition, if u wanna return it back again just remove the comments of commands and comment the beside command if exists
    //parameters: ID, strFnName, First Row in select list, select list name, pWhereClause
    var pWhereClause = "";
    if (pCountryID != null) //this means editing
    {
        pWhereClause = " where IsPort = 1 and IsInactive = 0 and CountryID = " + pCountryID + " OR ( ID = " + pID + ") ";
        //pWhereClause = " where IsInactive = 0 and CountryID = " + pCountryID;
    }
    else //when changing the Country or Transport Type
    {
        pWhereClause = " where IsPort = 1 and IsInactive = 0 and CountryID = ";
        //pWhereClause = " where IsInactive = 0 and CountryID = ";
        if (POLorPOD == 1) //POL
            pWhereClause += ($('#slRoutingsPOLCountries' + RoutingSuffix + ' option:selected').val() == null || $('#slRoutingsPOLCountries' + RoutingSuffix + ' option:selected').val() == ""
                ? 0 : $('#slRoutingsPOLCountries' + RoutingSuffix + ' option:selected').val());
        else //POD
            pWhereClause += ($('#slRoutingsPODCountries' + RoutingSuffix + ' option:selected').val() == null || $('#slRoutingsPODCountries' + RoutingSuffix + ' option:selected').val() == ""
            ? 0 : $('#slRoutingsPODCountries' + RoutingSuffix + ' option:selected').val());
        pWhereClause += " OR ( ID = " + pID + ") ";
    }
    //if ($('input[name=cbRoutingTransportType' + RoutingSuffix + ']:checked').val() == 1)
    //    pWhereClause += " and IsOcean = 1 ";
    //if ($('input[name=cbRoutingTransportType' + RoutingSuffix + ']:checked').val() == 2)
    //    pWhereClause += " and IsAir = 1 ";
    //if ($('input[name=cbRoutingTransportType' + RoutingSuffix + ']:checked').val() == 3)
    //    pWhereClause += " and IsInland = 1 ";

    pWhereClause += " order by Name ";
    if (POLorPOD == 1) //POL
        //GetListWithCodeAndWhereClause(pID, "/api/Ports/LoadAll", "Select POL", "slRoutingsPOL", pWhereClause);
        GetListWithCodeAndNameAndWhereClause(pID, "/api/Ports/LoadAll", "Select POL", "slRoutingsPOL" + RoutingSuffix, pWhereClause, callback);
    else
        //GetListWithCodeAndWhereClause(pID, "/api/Ports/LoadAll", "Select POD", "slRoutingsPOD", pWhereClause);
        GetListWithCodeAndNameAndWhereClause(pID, "/api/Ports/LoadAll", "Select POD", "slRoutingsPOD" + RoutingSuffix, pWhereClause, callback);
}
function Routings_Lines_GetList(pID, callback) {
    debugger;
    //parameters: ID, strFnName, First Row in select list, select list name
    var strFnName = "";
    var str1stRow = "";
    var pWhereClause = " WHERE 1=1 ORDER BY Name ";
    if ($('input[name=cbRoutingTransportType' + RoutingSuffix + ']:checked').val() == 1
        || $('input[name=cbRoutingTransportType' + RoutingSuffix + ']:checked').val() == null) { //null is for case of first load
        strFnName = "/api/ShippingLines/LoadAll";
        str1stRow = "Select Shipping Line";
    }
    if ($('input[name=cbRoutingTransportType' + RoutingSuffix + ']:checked').val() == 2) {
        strFnName = "/api/Airlines/LoadAll";
        str1stRow = "Select Airline";
    }
    if ($('input[name=cbRoutingTransportType' + RoutingSuffix + ']:checked').val() == 3) {
        strFnName = "/api/Truckers/LoadAll";
        str1stRow = TranslateString("Select Trucker");
        if (pDefaults.UnEditableCompanyName == "ELI") {
            // load into slRoutingsLinesTruckingOrder only the Truckers in OperationPartners
            pWhereClause = " WHERE ID IN (SELECT TruckerID FROM OperationPartners WHERE OperationID=" + $("#hOperationID").val() +" AND OperationPartnerTypeID=" + constTruckerOperationPartnerTypeID +") ORDER BY Name "
        }
    }
    if ($('input[name=cbRoutingTransportType' + RoutingSuffix + ']:checked').val() != 2)
        GetListWithNameAndWhereClause(pID, strFnName, str1stRow, "slRoutingsLines" + RoutingSuffix, pWhereClause, function () {
            // if when adding a Trucking Order, slRoutingsLinesTruckingOrder has only one row, select it
            if (pDefaults.UnEditableCompanyName == "ELI" && $('input[name=cbRoutingTransportType' + RoutingSuffix + ']:checked').val() == 3 && pID == null) {
                if ($("#slRoutingsLinesTruckingOrder option").length == 2) {
                    $('#slRoutingsLinesTruckingOrder option:eq(1)').attr('selected', 'selected');
                }

            }
        });
    else { //incase of Air to get the prefix for MAWB
        GetListWithNameAndPrefixAttr(pID, strFnName, str1stRow, "slRoutingsLines" + RoutingSuffix, pWhereClause, function () {
            Routings_SetAirlinePrefix();
            // if when adding a Trucking Order, slRoutingsLinesTruckingOrder has only one row, select it
            if (pDefaults.UnEditableCompanyName == "ELI" && $('input[name=cbRoutingTransportType' + RoutingSuffix + ']:checked').val() == 3 && pID == null) {
                if ($("#slRoutingsLinesTruckingOrder option").length == 2) {
                    $('#slRoutingsLinesTruckingOrder option:eq(1)').attr('selected', 'selected');
                }

            }
        });
    }
    if (callback != null && callback != undefined) {
        callback();
    }
}
function Routings_Vessels_GetList(pID, pLineID, pSlName, callback) {
    debugger;
    var strFnName = "/api/Vessels/LoadAll";
    str1stRow = TranslateString("Select Vessel");
    var pWhereClause = "";
    pWhereClause += " WHERE ID = " + pID;
    pWhereClause += " OR ShippingLineID = " + pLineID;
    pWhereClause += " OR ShippingLineID IS NULL ";
    pWhereClause += (pLineID == "" || pLineID == null ? " AND 1=2 " : ""); //to prevent getting any vessels in case of no line is choosen
    pWhereClause += " ORDER BY Name ";
    GetListWithNameAndWhereClause(pID, strFnName, str1stRow, pSlName, pWhereClause, callback);
}
function Routings_AddVessel() {
    debugger;
    if (ValidateForm("form", "RoutingVesselModal")) {
        FadePageCover(true);
        var pParametersWithValues = {
            pCode: $("#txtRoutingsVesselCode").val().trim() == "" ? 0 : $("#txtRoutingsVesselCode").val().trim().toUpperCase()
            , pName: $("#txtRoutingsVesselName").val().trim() == "" ? 0 : $("#txtRoutingsVesselName").val().trim().toUpperCase()
            , pLocalName: $("#txtRoutingsVesselLocalName").val().trim() == "" ? 0 : $("#txtRoutingsVesselLocalName").val().trim().toUpperCase()
            , pNotes: $("#txtRoutingsVesselNotes").val().trim() == "" ? 0 : $("#txtRoutingsVesselNotes").val().trim().toUpperCase()
        };
        CallGETFunctionWithParameters("/api/Routings/AddVesselFromRoutings", pParametersWithValues
            , function (pData) {
                if (pData[0] == 0)
                    swal("Sorry", "Code and Name must be unique.");
                else {
                    FillListFromObject(pData[0], 2, TranslateString("Select Vessel"), "slRoutingVessels", pData[1], null);
                    $("#slRoutingFeeders").append("<option value=" + pData[0] + ">" + $("#txtRoutingsVesselName").val().trim() + "</option>");
                    jQuery("#RoutingVesselModal").modal("hide");
                }
                FadePageCover(false);
            }
            , null);
    }
}
/*OnChangeFunctions*/
//used to make sure that if MainCarraigeRoutingType is selected then the transportType cant be changed from whats in the operations table
function Routings_RoutingTypeChanged() {
    debugger;
    Routings_EnableDisableLines();
    $("#lblRoutingShown" + RoutingSuffix).html(": " + $("#slRoutingTypes" + RoutingSuffix + " option:selected").text());
    if ($("#slRoutingTypes" + RoutingSuffix + " option:selected").val() == MainCarraigeRoutingTypeID) {
        //if in the future i allowed changing transport type then i have to update FCL, LCL, FTL, LTL too
        Routings_SetCbRoutingTransportType($("#hTransportType").val());
        $("#hRoutingTransportType" + RoutingSuffix).val($("#hTransportType").val());
        Routings_TransportTypeChanged();
        Routings_DisableCbTransportType();
    }
    else
        Routings_EnableCbTransportType();
}
function Routings_TransportTypeChanged(pDontRefillList) {
    //set Ocean as the default Choice
    $("#hRoutingTransportType" + RoutingSuffix).val($('input[name=cbRoutingTransportType' + RoutingSuffix + ']:checked').val());
    Routings_TransportType_SetIconNameAndStyle();
    Routings_ShowHideVessels();
    //Routings_Ports_GetList(null, null, 1);
    //Routings_Ports_GetList(null, null, 2);
    Routings_Lines_GetList(null, function () {
        if ($("#hRoutingTransportType" + RoutingSuffix).val() == OceanTransportType)
            Routings_Vessels_GetList(null, 0, "slRoutingVessels" + RoutingSuffix, function () { $("#slRoutingFeeders").html($("#slRoutingVessels").html()); });
        else //to make value of 
            $("#slRoutingVessels" + RoutingSuffix).val("");
    });
}
function Routings_slRoutingsLinesChanged() {
    if ($("#hRoutingTransportType" + RoutingSuffix).val() == OceanTransportType)
        Routings_Vessels_GetList(null, $("#slRoutingsLines" + RoutingSuffix).val(), "slRoutingVessels" + RoutingSuffix, null);
    else
        if ($("#hRoutingTransportType" + RoutingSuffix).val() == AirTransportType)
            Routings_SetAirlinePrefix();
}
/*EOF OnChangeFunctions*/
function Routings_DisableCbTransportType() {
    $("#secTransportType" + RoutingSuffix).addClass("hide"); //Islam asked to this section not disable
    $("#cbIsOceanRouting" + RoutingSuffix).attr('disabled', 'disabled');
    $("#cbIsAirRouting" + RoutingSuffix).attr('disabled', 'disabled');
    $("#cbIsInlandRouting" + RoutingSuffix).attr('disabled', 'disabled');
}
function Routings_EnableCbTransportType() {
    ////I disabled the transport type always to cancel this swap the commented with the uncommented
    //$("#secTransportType" + RoutingSuffix).removeClass("hide");
    //$("#cbIsOceanRouting" + RoutingSuffix).removeAttr('disabled');
    //$("#cbIsAirRouting" + RoutingSuffix).removeAttr('disabled');
    //$("#cbIsInlandRouting" + RoutingSuffix).removeAttr('disabled');
    $("#secTransportType" + RoutingSuffix).addClass("hide");
}
function Routings_EnableDisableLines() {
    if ($("#slRoutingTypes" + RoutingSuffix).val() == MainCarraigeRoutingTypeID && $("#hBLType").val() == constHouseBLType) {
        $("#slRoutingsLines" + RoutingSuffix).attr("disabled", "disabled");
        $("#slRoutingsLines" + RoutingSuffix).val("");
    }
    else if (!(pDefaults.UnEditableCompanyName == "ILS" && pDefaults.UnEditableCompanyName == "ILSEG" && RoutingSuffix == "TruckingOrder"))
        $("#slRoutingsLines" + RoutingSuffix).removeAttr("disabled");
}
function Routings_SetCbRoutingTransportType(pOptionToSelect) {
    if (pOptionToSelect == OceanTransportType) {
        $("#cbIsOceanRouting" + RoutingSuffix).prop('checked', true);
        $("#cbIsAirRouting" + RoutingSuffix).prop('checked', false);
        $("#cbIsInlandRouting" + RoutingSuffix).prop('checked', false);
    }
    if (pOptionToSelect == AirTransportType) {
        $("#cbIsOceanRouting" + RoutingSuffix).prop('checked', false);
        $("#cbIsAirRouting" + RoutingSuffix).prop('checked', true);
        $("#cbIsInlandRouting" + RoutingSuffix).prop('checked', false);
    }
    if (pOptionToSelect == InlandTransportType || pOptionToSelect == CustomsClearanceTransportType/*to load truckers in case of clearance*/) {
        $("#cbIsOceanRouting" + RoutingSuffix).prop('checked', false);
        $("#cbIsAirRouting" + RoutingSuffix).prop('checked', false);
        $("#cbIsInlandRouting" + RoutingSuffix).prop('checked', true);
    }
}
function Routings_CheckDatesLogic() {
    var isCorrectLogic = true;
    if (
           (!isValidDate($("#txtETAPOLDate" + RoutingSuffix).val().trim(), 1) && $("#txtETAPOLDate" + RoutingSuffix).val().trim() != "")
        || (!isValidDate($("#txtATAPOLDate" + RoutingSuffix).val().trim(), 1) && $("#txtATAPOLDate" + RoutingSuffix).val().trim() != "")

        || (!isValidDate($("#txtExpectedArrival" + RoutingSuffix).val().trim(), 1) && $("#txtExpectedArrival" + RoutingSuffix).val().trim() != "")
        || (!isValidDate($("#txtExpectedDeparture" + RoutingSuffix).val().trim(), 1) && $("#txtExpectedDeparture" + RoutingSuffix).val().trim() != "")
        || (!isValidDate($("#txtActualArrival" + RoutingSuffix).val().trim(), 1) && $("#txtActualArrival" + RoutingSuffix).val().trim() != "")
        || (!isValidDate($("#txtActualDeparture" + RoutingSuffix).val().trim(), 1) && $("#txtActualDeparture" + RoutingSuffix).val().trim() != "")
        || (!isValidDate($("#txtMAWBDate" + RoutingSuffix).val().trim(), 1) && $("#txtMAWBDate" + RoutingSuffix).val().trim() != "")
        || (!isValidDate($("#txtOBLDate" + RoutingSuffix).val().trim(), 1) && $("#txtOBLDate" + RoutingSuffix).val().trim() != "")

        || (!isValidDate($("#txtTruckingOrderGateInDate" + RoutingSuffix).val().trim(), 1) && $("#txtTruckingOrderGateInDate" + RoutingSuffix).val().trim() != "")
        || (!isValidDate($("#txtTruckingOrderGateOutDate" + RoutingSuffix).val().trim(), 1) && $("#txtTruckingOrderGateOutDate" + RoutingSuffix).val().trim() != "")
        || (!isValidDate($("#txtTruckingOrderStuffingDate" + RoutingSuffix).val().trim(), 1) && $("#txtTruckingOrderStuffingDate" + RoutingSuffix).val().trim() != "")
        || (!isValidDate($("#txtTruckingOrderDeliveryDate" + RoutingSuffix).val().trim(), 1) && $("#txtTruckingOrderDeliveryDate" + RoutingSuffix).val().trim() != "")
        )
        isCorrectLogic = false;
    //uncomment if i want logic validation
    //else
    //    if ( //the 1st 2 conditions in each of the next 2 lines is coz incase of being empty the return value from ConvertDateFormat() fn is 1 and i dont need the condition
    //        (ConvertDateFormat($("#txtExpectedDeparture" + RoutingSuffix).val()) != 1 && ConvertDateFormat($("#txtActualDeparture" + RoutingSuffix).val()) != 1 && Date.prototype.compareDates(ConvertDateFormat($("#txtExpectedDeparture" + RoutingSuffix).val()), ConvertDateFormat($("#txtActualDeparture" + RoutingSuffix).val())) < 0)
    //        || (ConvertDateFormat($("#txtExpectedArrival" + RoutingSuffix).val()) != 1 && ConvertDateFormat($("#txtActualArrival" + RoutingSuffix).val()) != 1 && Date.prototype.compareDates(ConvertDateFormat($("#txtExpectedArrival" + RoutingSuffix).val()), ConvertDateFormat($("#txtActualArrival" + RoutingSuffix).val())) < 0)
    //        //the rest of the conditions are to make sure that arrival is after departure
    //        || (ConvertDateFormat($("#txtATAPOLDate" + RoutingSuffix).val()) != 1 && Date.prototype.compareDates(ConvertDateFormat($("#txtETAPOLDate" + RoutingSuffix).val()), ConvertDateFormat($("#txtATAPOLDate" + RoutingSuffix).val())) < 0)
    //        || (ConvertDateFormat($("#txtExpectedDeparture" + RoutingSuffix).val()) != 1 && Date.prototype.compareDates(ConvertDateFormat($("#txtATAPOLDate" + RoutingSuffix).val()), ConvertDateFormat($("#txtExpectedDeparture" + RoutingSuffix).val())) < 0)
    //        || (ConvertDateFormat($("#txtExpectedArrival" + RoutingSuffix).val()) != 1 && Date.prototype.compareDates(ConvertDateFormat($("#txtExpectedDeparture" + RoutingSuffix).val()), ConvertDateFormat($("#txtExpectedArrival" + RoutingSuffix).val())) < 0)
    //        || (ConvertDateFormat($("#txtExpectedArrival" + RoutingSuffix).val()) != 1 && Date.prototype.compareDates(ConvertDateFormat($("#txtActualDeparture" + RoutingSuffix).val()), ConvertDateFormat($("#txtExpectedArrival" + RoutingSuffix).val())) < 0)
    //        || (ConvertDateFormat($("#txtActualArrival" + RoutingSuffix).val()) != 1 && Date.prototype.compareDates(ConvertDateFormat($("#txtExpectedDeparture" + RoutingSuffix).val()), ConvertDateFormat($("#txtActualArrival" + RoutingSuffix).val())) < 0)
    //        || (ConvertDateFormat($("#txtActualArrival" + RoutingSuffix).val()) != 1 && Date.prototype.compareDates(ConvertDateFormat($("#txtActualDeparture" + RoutingSuffix).val()), ConvertDateFormat($("#txtActualArrival" + RoutingSuffix).val())) < 0)
    //       )
    //        isCorrectLogic = false;
    return isCorrectLogic;
}
//Set MainCarraigeRoutingType Always not to be deleted
function Routings_SetTableProperties() {
    //in case of connected prevent deleting Main Route
    //if ($("#hNumberOfHousesConnected").val() > 0 || $("#hMasterOperationID").val() > 0) {
    $("#tblRoutings tr[val=" + MainCarraigeRoutingTypeID.toString() + "] input:checkbox").attr("disabled", "disabled");
    $("#tblRoutings tr[val=" + MainCarraigeRoutingTypeID.toString() + "] input:checkbox").removeAttr("checked");
    //}
    //else
    //    $("#tblRoutings tr[val=" + MainCarraigeRoutingTypeID.toString() + "] input:checkbox").removeAttr("disabled");
}
//set controls in case of Master && Connected when calling Routing Modal (note:if this fn is called then i am sure its main isa)
function Routings_Master_And_Connected_Properties() {
    //Routings_DisablePorts();
    Routings_EnableDates();
    if ($("#hMAWBStockID").val() == "0")
        Routings_EnableLines();
    else
        Routings_DisableLines();
}
//set controls in case of Master && Not Connected when calling Routing Modal (note:if this fn is called then i am sure its main isa)
function Routings_Master_And_NotConnected_Properties() {
    Routings_EnablePorts();
    Routings_EnableDates();
    if ($("#hMAWBStockID").val() == "0")
        Routings_EnableLines();
    else
        Routings_DisableLines();
}
//set controls in case of House && Connected when calling Routing Modal (note:if this fn is called then i am sure its main isa)
function Routings_House_And_Connected_Properties() {
    //Routings_DisablePorts();
    Routings_DisableDates();
    Routings_DisableLines();
}
//set controls in case of House && Not Connected when calling Routing Modal (note:if this fn is called then i am sure its main isa)
function Routings_House_And_NotConnected_Properties() {
    Routings_EnablePorts();
    Routings_DisableDates();
    Routings_DisableLines();
}
//called in case of Master & Main Carraige Route
function Routings_Show_Suitable_MasterBL() {
    if ($("#cbIsAir").prop("checked")) { //Air so show MAWB
        $("#divOBL" + RoutingSuffix).addClass("hide");
        $("#divOBLDate" + RoutingSuffix).addClass("hide");
        $("#divMAWBSuffix" + RoutingSuffix).removeClass("hide");
        $("#divMAWBPrefix" + RoutingSuffix).removeClass("hide");
        $("#divMAWBDate" + RoutingSuffix).removeClass("hide");
        $("#divBtnStock" + RoutingSuffix).removeClass("hide");
    }
    else { //Master but not air so show OBL
        $("#divOBL" + RoutingSuffix).removeClass("hide");
        $("#divOBLDate" + RoutingSuffix).removeClass("hide");
        $("#divMAWBSuffix" + RoutingSuffix).addClass("hide");
        $("#divMAWBPrefix" + RoutingSuffix).addClass("hide");
        $("#divMAWBDate" + RoutingSuffix).addClass("hide");
        $("#divBtnStock" + RoutingSuffix).addClass("hide");
    }
}
function Routings_HideMasterBL() {
    $("#divOBL" + RoutingSuffix).addClass("hide");
    $("#divOBLDate" + RoutingSuffix).addClass("hide");
    $("#divMAWBSuffix" + RoutingSuffix).addClass("hide");
    $("#divMAWBPrefix" + RoutingSuffix).addClass("hide");
    $("#divMAWBDate" + RoutingSuffix).addClass("hide");
    $("#divBtnStock" + RoutingSuffix).addClass("hide");
}
function Routings_EnablePorts() {
    $("#slRoutingsPOLCountries" + RoutingSuffix).removeAttr("disabled");
    $("#slRoutingsPODCountries" + RoutingSuffix).removeAttr("disabled");
    $("#slRoutingsPOL" + RoutingSuffix).removeAttr("disabled");
    $("#slRoutingsPOD" + RoutingSuffix).removeAttr("disabled");
}
function Routings_DisablePorts() {
    $("#slRoutingsPOLCountries" + RoutingSuffix).attr("disabled", "disabled");
    $("#slRoutingsPODCountries" + RoutingSuffix).attr("disabled", "disabled");
    $("#slRoutingsPOL" + RoutingSuffix).attr("disabled", "disabled");
    $("#slRoutingsPOD" + RoutingSuffix).attr("disabled", "disabled");
}
function Routings_EnableDates() {
    $("#txtETAPOLDate" + RoutingSuffix).removeAttr("disabled");
    $("#txtATAPOLDate" + RoutingSuffix).removeAttr("disabled");
    $("#txtExpectedDeparture" + RoutingSuffix).removeAttr("disabled");
    $("#txtActualDeparture" + RoutingSuffix).removeAttr("disabled");
    $("#txtExpectedArrival" + RoutingSuffix).removeAttr("disabled");
    $("#txtActualArrival" + RoutingSuffix).removeAttr("disabled");
    if (!(pDefaults.UnEditableCompanyName == "ILS" && pDefaults.UnEditableCompanyName == "ILSEG" && RoutingSuffix == "TruckingOrder"))
        $("#slRoutingsLines" + RoutingSuffix).removeAttr("disabled");
}
function Routings_DisableDates() {
    $("#txtETAPOLDate" + RoutingSuffix).attr("disabled", "disabled");
    $("#txtATAPOLDate" + RoutingSuffix).attr("disabled", "disabled");
    $("#txtExpectedDeparture" + RoutingSuffix).attr("disabled", "disabled");
    $("#txtActualDeparture" + RoutingSuffix).attr("disabled", "disabled");
    $("#txtExpectedArrival" + RoutingSuffix).attr("disabled", "disabled");
    $("#txtActualArrival" + RoutingSuffix).attr("disabled", "disabled");
    $("#slRoutingsLines" + RoutingSuffix).attr("disabled", "disabled");
}
function Routings_EnableLines() {
    if (!(pDefaults.UnEditableCompanyName == "ILS" && pDefaults.UnEditableCompanyName == "ILSEG" && RoutingSuffix == "TruckingOrder"))
        $("#slRoutingsLines" + RoutingSuffix).removeAttr("disabled");
    $("#slRoutingVessels" + RoutingSuffix).removeAttr("disabled");
    $("#txtRoutingVoyageOrTruckNumber" + RoutingSuffix).removeAttr("disabled");
}
function Routings_DisableLines() {
    $("#slRoutingsLines" + RoutingSuffix).attr("disabled", "disabled");
    $("#slRoutingVessels" + RoutingSuffix).attr("disabled", "disabled");
    $("#txtRoutingVoyageOrTruckNumber" + RoutingSuffix).attr("disabled", "disabled");
}
function Routings_ShowHideVessels() {
    debugger;
    if ($("#hRoutingTransportType" + RoutingSuffix).val() == OceanTransportType) {
        $(".classRoutingVessels" + RoutingSuffix).removeClass("hide");
    }
    else {
        $(".classRoutingVessels" + RoutingSuffix).addClass("hide");
        $("#slRoutingVessels" + RoutingSuffix).val("");
        if ($("#slRoutingVessels" + RoutingSuffix + " option").length < 2)
            Routings_Vessels_GetList(null, null, "slRoutingVessels" + RoutingSuffix, function () { $("#slRoutingFeeders").html($("#slRoutingVessels").html()); });
    }
}
//function Routings_ShowHideOwnedByCompanyProperties(pIsCompanyOwned) {
//    debugger;
//    //if ($("#cbTruckingOrderIsOwnedByCompany").prop("checked") || pIsCompanyOwned) {
//    //    $(".classOwnedByCompany").removeClass("hide");
//    //    $(".classOwnedBySupplier").addClass("hide");
//    //}
//    //else {
//    //    $(".classOwnedByCompany").addClass("hide");
//    //    $(".classOwnedBySupplier").removeClass("hide");
//    //}
//}
function Routings_ShowHidePickupAndDeliveryAddress(pFlag) {
    debugger;
    if (pFlag) {
        $("#divPickupAddress" + RoutingSuffix).removeClass("hide");
        $("#divDeliveryAddress" + RoutingSuffix).removeClass("hide");
    }
    else {
        $("#divPickupAddress" + RoutingSuffix).addClass("hide");
        $("#divDeliveryAddress" + RoutingSuffix).addClass("hide");
    }
    if (pDefaults.UnEditableCompanyName == "ILS" || pDefaults.UnEditableCompanyName == "ILSEG")
        $("#slRoutingsLinesTruckingOrder").attr("disabled", "disabled");
}
function Routings_EnableMAWBSuffix() {
    $("#txtMAWBSuffix" + RoutingSuffix).removeAttr("disabled");
}
function Routings_DisableMAWBSuffix() {
    $("#txtMAWBSuffix" + RoutingSuffix).attr("disabled", "disabled");
}
function Routings_Add_DataTarget_Attr_To_btnMAWBStock() {
    $("#btnMAWBStock" + RoutingSuffix).attr("data-target", "#MAWBStockSelectModal");
}
function Routings_Remove_DataTarget_Attr_From_btnMAWBStock() {
    $("#btnMAWBStock" + RoutingSuffix).removeAttr("data-target");
}
function Routings_EnableControlsForNewAdd() {
    $("#slRoutingTypes" + RoutingSuffix).removeAttr("disabled");
    Routings_EnablePorts();
    Routings_EnableDates();
    Routings_EnableLines();
}
function Routings_Set_btnMAWBStockCaption() {
    debugger;
    if ($("#hMAWBStockID").val() != "0")
        $("#btnMAWBStock" + RoutingSuffix).text("Return To Stock");
    else
        $("#btnMAWBStock" + RoutingSuffix).text("Get From Stock");
}
function Routings_SetAirlinePrefix() {
    if ($("#slRoutingsLines" + RoutingSuffix).val() == "")
        $("#btnMAWBStock" + RoutingSuffix).attr("disabled", "disabled");
    else
        $("#btnMAWBStock" + RoutingSuffix).removeAttr("disabled");
    $("#txtMAWBPrefix" + RoutingSuffix).val($("#slRoutingsLines" + RoutingSuffix + " option:selected").attr("Prefix"));
}
function Routings_GetMasterBL() {
    debugger;
    //Not House (i.e. Master || Direct) && Air && Main Carraige && NOT Empty
    if ($("#hBLType").val() != constHouseBLType
        && $("#hTransportType").val() == AirTransportType
        && $('#slRoutingTypes' + RoutingSuffix + ' option:selected').val() == MainCarraigeRoutingTypeID
        && $("#txtMAWBSuffix" + RoutingSuffix).val().trim() != "" && $("#txtMAWBSuffix" + RoutingSuffix).val().trim() != "0")
        return $("#txtMAWBPrefix" + RoutingSuffix).val().toString() + "-" + $("#txtMAWBSuffix" + RoutingSuffix).val().trim().toUpperCase().toString();
    else //Not House (i.e. Master or Direct) && NOT Air && NOT Empty
        if ($("#hBLType").val() != constHouseBLType
        && $("#hTransportType").val() != AirTransportType
        && $('#slRoutingTypes' + RoutingSuffix + ' option:selected').val() == MainCarraigeRoutingTypeID
        && $("#txtOBL" + RoutingSuffix).val().trim() != "")
            return $("#txtOBL" + RoutingSuffix).val().trim().toUpperCase().toString();
        else //House
            if ($("#hBLType").val() == constHouseBLType)
                return "0";
    return "0";
}
//used to set the pBLDate parameter in case of updating because its value may come from $("#txtOBLDate") or $("#txtMAWBDate")
function Routings_GetpBLDate() {
    if ($("#hBLType").val() != constHouseBLType && $("#slRoutingTypes" + RoutingSuffix + " option:selected").val() == MainCarraigeRoutingTypeID)
        if ($("#cbIsAir").prop("checked"))
            return ($("#txtMAWBDate" + RoutingSuffix).val() == "" ? "NULL" : ConvertDateFormat($("#txtMAWBDate" + RoutingSuffix).val()));
        else
            return ($("#txtOBLDate" + RoutingSuffix).val() == "" ? "NULL" : ConvertDateFormat($("#txtOBLDate" + RoutingSuffix).val()));
    else
        return "NULL";
}
function Routings_GenerateMissingRoutes() {
    debugger;
    FadePageCover(true);
    CallGETFunctionWithParameters("/api/Routings/GenerateMissingRoutes", { pOperationIDToRestoreRoute: $("#hOperationID").val() }
        , function (pData) {
            Routings_BindTableRows(JSON.parse(pData[0]));
            FadePageCover(false);
        }, null);
}
/*****************************Customs Clearance*************************************/
function RoutingsCC_OpenCustomsClearanceTrackingStageModal() {
    debugger;
    if ($("#hRoutingIDCustomsClearance").val() == "")
        swal("Sorry", "Please, save record first.");
    else {
        jQuery("#CustomsClearanceTrackingModal").modal("show");
        //$("#tblCustomsClearanceTracking tbody").html($("#tblTracking tbody").html()); //written in Tracking_BindTableRows
    }
}
function RoutingsCC_PrintRequest(pRoutingIDCCToPrint, pOption) {
    debugger;
    FadePageCover(true);
    var pParametersWithValues = {
        pRoutingIDCCToPrint: pRoutingIDCCToPrint
    };
    CallGETFunctionWithParameters("/api/Routings/RoutingsCC_PrintRequest", pParametersWithValues
        , function (pData) {
            var _ReturnedMessage = pData[0];
            var pRoutingCC = JSON.parse(pData[1]);
            var pOperationHeader = JSON.parse(pData[2]);
            var _ReportHTML = '';
            var _ReportTitle = "طلب تخليص";
            if (_ReturnedMessage == "") {
                //<div style="width:500px;height:100px;border:1px solid #000;">This is a rectangle!</div>
                _ReportHTML += '<html>';
                _ReportHTML += '     <head><title>' + _ReportTitle + '</title><link rel="stylesheet" href="/Content/CSS/app.v2.css" type="text/css" /></head>';
                _ReportHTML += '         <body style="background-color:white;>';
                _ReportHTML += '            <div class="col-xs-12 text-center"><img src="/Content/Images/CompanyHeader.jpg" alt="logo"/></div> </br>';
                _ReportHTML += '            <div class="col-xs-12 text-center text-ul"><h2><b>' + _ReportTitle + '</b></h2></div>';

                _ReportHTML += '             <div class="col-xs-12 m-l-n m-t-xs text-right">' + (pRoutingCC.CCAName == 0 ? "" : pRoutingCC.CCAName) + '<b>' + '  / السادة ' + '</b></div>';
                _ReportHTML += '             <div class="col-xs-12 m-l-n-lg m-t text-right"><b>' + '  ..... تحية طيبة و بعد ' + '</b></div>';
                _ReportHTML += '             <div class="col-xs-12 m-l-n m-t text-right"><b>' + '' + '  : برجاء إنهاء إجراءات التخليص الخاصة للشحنه وبيانتها كالتالي ' + '</b></div>';

                _ReportHTML += '             <div class="col-xs-12 m-l-n text-right m-t">' + pOperationHeader.ClientName + '<b class="float-right">' + '  : اسم العميل -  ' + '</b></div>';
                _ReportHTML += '             <div class="col-xs-12 m-l-n text-right m-t-sm">' + pOperationHeader.POLName + "  --> " + pOperationHeader.PODName + '<b class="float-right">' + ' :   ميناء الشحن / التفريغ  -  ' + '</b></div>';
                _ReportHTML += '             <div class="col-xs-12 m-l-n text-right m-t-sm">' + (pRoutingCC.CC_ClearanceTypeName == 0 ? "&emsp;" : pRoutingCC.CC_ClearanceTypeName) + '<b class="float-right">' + ' :     نوع التخليص  -   ' + '</b></div>';
                _ReportHTML += '             <div class="col-xs-12 m-l-n text-right m-t-sm">' + (pOperationHeader.ContainerTypes == 0 ? "&emsp;" : pOperationHeader.ContainerTypes) + '<b class="float-right">' + ' :     عدد الحاويات  -   ' + '</b></div>';
                _ReportHTML += '             <div class="col-xs-12 m-l-n text-right m-t-sm">' + (pOperationHeader.LineName == 0 ? "&emsp;" : pOperationHeader.LineName) + '<b class="float-right">' + ' :     الخط الملاحي  -   ' + '</b></div>';
                _ReportHTML += '             <div class="col-xs-12 m-l-n text-right m-t-sm">' + (pRoutingCC.CCAInvoiceNumber == 0 ? "&emsp;" : pRoutingCC.CCAInvoiceNumber) + '<b class="float-right">' + ' :     رقم الفاتوره  -   ' + '</b></div>';
                _ReportHTML += '             <div class="col-xs-12 m-l-n text-right m-t-sm">' + (pRoutingCC.BookingNumber == 0 ? "&emsp;" : pRoutingCC.BookingNumber) + '<b class="float-right">' + ' :     رقم اذن الشحن  -   ' + '</b></div>';

                _ReportHTML += '             <div class="col-xs-12 m-l-n m-t text-center"><b>' + '' + ' ..... وتفضلوا بقبول فائق الشكر و تقدير  ' + '</b></div>';
                _ReportHTML += '             <div class="col-xs-12 m-l-n m-t"><b>' + pDefaults.CompanyLocalName + '</b></div>';

                _ReportHTML += '         </body>';
                _ReportHTML += '     <footer class="footer col-xs-12" style="width:100%; position:absolute; bottom:0;">';
                _ReportHTML += '         <div class="row text-center"><img src="/Content/Images/CompanyFooter.jpg" alt="footer"/></div>';
                _ReportHTML += '     </footer>';
                _ReportHTML += '</html>';
                if (pOption == "Print" || pOption == undefined || pOption == null) {
                    var mywindow = window.open('', '_blank');
                    mywindow.document.write(_ReportHTML);
                    mywindow.document.close();
                }
                else if (pOption == "Email") {
                    ////SendPDFEmail_General(pEmail_Subject, pEmail_To, pReportHTML, pReportTitle, null);
                    //SendPDFEmail_General("Operation " + $("#hOperationCode").val(), pEmail_To, _ReportHTML, pReportTitle, null);
                }
            } //if (_ReturnedMessage == "") {
            else
                swal("Sorry", _ReturnedMessage);
            FadePageCover(false);
        }
        , null);
}
function RoutingsCC_PrintStages(pRoutingIDCCToPrint, pOption) {
    debugger;
    FadePageCover(true);
    var pParametersWithValues = {
        pRoutingIDCCToPrint: pRoutingIDCCToPrint
    };
    CallGETFunctionWithParameters("/api/Routings/RoutingsCC_PrintRequest", pParametersWithValues
        , function (pData) {
            var _ReturnedMessage = pData[0];
            var pRoutingCC = JSON.parse(pData[1]);
            var pOperationHeader = JSON.parse(pData[2]);
            var pClearanceTracking = JSON.parse(pData[3]);
            var _ReportHTML = '';
            var _ReportTitle = "Clearance Stages";
            if (_ReturnedMessage == "") {

                var ReportHTML = '';
                //$("#tblDocsOut tr[id=60] td.IsPrintISOCode input[type=checkbox]").prop('checked')
                //<div style="width:500px;height:100px;border:1px solid #000;">This is a rectangle!</div>
                ReportHTML += '<html>';
                ReportHTML += '     <head><title>' + _ReportTitle + '</title><link rel="stylesheet" href="/Content/CSS/app.v2.css" type="text/css" /></head>';
                ReportHTML += '         <body style="background-color:white;">';
                //if ($("#tblDocsOut tr[id=" + pDocumentTypeID + "] td.ShowHeader input[type=checkbox]").prop("checked"))
                    ReportHTML += '         <div class="col-xs-12 text-center"><img src="/Content/Images/CompanyHeader.jpg" alt="logo"/></div> </br>';
                    ReportHTML += '             <div class="col-xs-6"><b>Operation No. :</b> ' + pOperationHeader.Code + '</div><div class="col-xs-6 text-right"><b>Date :</b> ' + getTodaysDateInddMMyyyyFormat() + '</div>';
                ReportHTML += '             <div class="col-xs-12 text-center text-ul"><h3>' + _ReportTitle + '</h3></div> </br>';
                //ReportHTML += '             <div> &nbsp; </div>'

                ReportHTML += '                         <table id="tblClearanceStagesDocsOut" class="table table-striped b-light b-a text-sm table-bordered">'; //table-hover
                ReportHTML += '                             <thead>';
                ReportHTML += '                                 <tr>';
                ReportHTML += '                                     <th>Stage</th>';
                ReportHTML += '                                     <th>Date</th>';
                ReportHTML += '                                     <th>Notes</th>';
                //ReportHTML += '                                     <th>Done(Y/N)</th>';
                ReportHTML += '                                 </tr>';
                ReportHTML += '                             </thead>';
                ReportHTML += '                             <tbody>';
                $.each(pClearanceTracking, function (i, item) { //no final shipping declaration for air
                    ReportHTML += '                                 <tr class="" style="font-size:95%;">';
                    ReportHTML += '                                     <td>' + item.TrackingStageName + '</td>';
                    ReportHTML += '                                     <td>' + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.TrackingDate)) < 1 ? "" : ConvertDateFormat(GetDateWithFormatMDY(item.TrackingDate))) + '</td>';
                    ReportHTML += '                                     <td>' + (item.Notes == 0 ? "" : item.Notes) + '</td>';
                    //ReportHTML += '                                     <td>' + (item.Done ? "Y" : "N") + '</td>';
                    ReportHTML += '                                 </tr>';

                });

                ReportHTML += '                             </tbody>';
                ReportHTML += '                         </table>';

                ReportHTML += '         </body>';
                ReportHTML += '         <footer class="footer col-xs-12" style="width:100%; position:absolute; bottom:0;">';
                //ReportHTML += '           <div class="row m-l">Please apply to our office with the original B/L to release your Delivery Order / Letter as soon as possible.</div>';
                //ReportHTML += '           <div class="row text-right m-r">الشركه خاضعه لنظام الدفعات المقدمه تطبيقا لأحكام الماده 62 من القانون رقم 91 لسنة 2005 و لا يجوز الخصم عليها</div>';
                //if ($("#tblDocsOut tr[id=" + pDocumentTypeID + "] td.IsPrintISOCode input[type=checkbox]").prop("checked"))
                    //ReportHTML += '         <div class="row m-l">' + $("#tblDocsOut tr[id=" + pDocumentTypeID + "] td.ISOCode").text() + '</div>';
                //if ($("#tblDocsOut tr[id=" + pDocumentTypeID + "] td.ShowFooter input[type=checkbox]").prop("checked"))
                    //ReportHTML += '         <div class="row text-center"><img src="/Content/Images/CompanyFooter.jpg" alt="footer"/></div>';
                ReportHTML += '         </footer>';

                ReportHTML += '</html>';
                if (pOption == "Print" || pOption == undefined || pOption == null) {
                    var mywindow = window.open('', '_blank');
                    mywindow.document.write(ReportHTML);
                    mywindow.document.close();
                }
                else if (pOption == "Email") {
                    //DocsOut_SendEmail($("#tblDocsOut tr[id=" + pDocumentTypeID + "] td.TableOrViewName").text(), ReportHTML);
                }
            }//if (_ReturnedMessage == "") {
            else
                swal("Sorry", _ReturnedMessage);
            FadePageCover(false);
        }
        , null);
}
function RoutingsCC_OpenCustomsTranslationModal() {
    debugger;
    swal("Sorry", "Under Development.");
}
function RoutingsCC_OpenDeliveryOrderReceiptModal() {
    debugger;
    swal("Sorry", "Under Development.");
}
function RoutingsCC_CalculateCFValue() {
    debugger;
    var _CCAFreight = $("#txtCCAFreight").val().trim() == "" ? 0 : parseFloat($("#txtCCAFreight").val().trim());
    var _CCAFOB = $("#txtCCAFOB").val().trim() == "" ? 0 : parseFloat($("#txtCCAFOB").val().trim());
    var _CCACFValue = (_CCAFreight + _CCAFOB).toFixed(2);
    $("#txtCCACFValue").val(_CCACFValue);
    return _CCACFValue;
}
/*****************************************Trucking Order Cargo********************************/


function Routings_GenerateCargo() {
    debugger;

    var pTruckerID = $('#slRoutingsLinesTruckingOrder').val();

if(pTruckerID == "" || pTruckerID == null)
    swal(strSorry, "Choose Trucker First");
else
{
    FadePageCover(true);
    if ($("#cbIsVehicle").prop("checked") == true) {
        var pWhereClause = "WHERE OperationID=" + ($("#hOperationID").val() == "" ? 0 : $("#hOperationID").val()) +
          " AND IsReceived=1  AND ID NOT IN (SELECT OperationVehicleID FROM TruckingOrderCargo where OperationVehicleID is not null)"
        + " And LastTruckerID=" + pTruckerID;
        CallGETFunctionWithParameters("/api/OperationVehicle/LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned",
            {
                pIsLoadArrayOfObjects: false
                , pLanguage: $("[id$='hf_ChangeLanguage']").val()
                , pPageNumber: 1
                , pPageSize: 999999
                , pWhereClause: pWhereClause
                , pOrderBy: "Code"
            }
            , function (pData) { RountingsVehicle_BindTableRows(JSON.parse(pData[0])); FadePageCover(false); }
            , null);

    }
    else {

        CallGETFunctionWithParameters("/api/OperationContainersAndPackages/LoadWithWhereClause",
                {
                    pPageNumber: 1
                    , pPageSize: 999999
                    , pWhereClause: "WHERE OperationID=" + ($("#hOperationID").val() == "" ? 0 : $("#hOperationID").val()) + " AND ID NOT IN (SELECT OperationsContainersAndPackagesID FROM TruckingOrderCargo where OperationsContainersAndPackagesID is not null)"
                    , pOrderBy: "ID"
                }
                   , function (pData) { RountingsVehicle_BindTableRows(JSON.parse(pData[0])); FadePageCover(false); }
                , null);

    }
}





    //$("#btn-SearchCharges").attr("onclick", "Receivables_GenerateFromPayables();");
    //$("#btnSelectChargesApply").attr("onclick", "Receivables_InsertListWithValuesFromPayables(false);");
}
function RountingsVehicle_BindTableRows(pVehicle) {
    debugger;
   // ClearAllTableRows("tblVehicle");
    var inspectionControlsText = " class='btn btn-xs btn-rounded btn-lightblue float-right' > <i class='fa fa-list' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + " Actions" + "</span>";

    var pDivName = "divSelectCargo";
    var ptblModalName = "tblVehicle";
    var pCheckboxNameAttr = "Delete";

    $("#btnCargoApply").attr("onclick", "Vehicle_Save('tblVehicle');");

    $("#" + pDivName).html("");
   
    //var divData = ' <section class="panel panel-default"  > ';
    //divData += '<section class="vbox" >';
    //divData += ' <section id="sec-content" class="scrollable padder" ">';
   
    var divData = '';

    if ($("#cbIsVehicle").prop("checked")==true )
    {
        divData += ' <section class="panel panel-default"  > ';
        divData += '     <div class="table-responsive" > ';
        divData += '        <table id="' + ptblModalName + '" class="table table-striped b-t b-light text-sm  table-hover" style="font-size:100%;"> ';
        divData += '            <thead> ';
        divData += '            <tr> ';
        divData += '                <th id="HeaderSelectCargoIDs"> ';
        divData += '                <input id="cb-CheckAll-RoutingsCargo" type="checkbox"> ';
        divData += '                </th> ';
        divData += '                <th>Code</th> ';
        divData += '                <th>ChassisNumber</th> ';
        divData += '                <th>EngineNumber</th> ';
        divData += '                <th>OCNCode</th> ';
        divData += '                <th>Model</th> ';
        divData += '                <th>KeyNumber</th> ';
        divData += '                <th>EC</th> ';
        divData += '                <th>PaintType</th> ';
        divData += '                <th>IC</th> ';
        divData += '                <th>CommercialInvoiceNumber</th> ';
        divData += '                <th>BillNumber</th> ';
        divData += '                <th>InsurancePolicyNumber</th> ';
        divData += '                <th>ProductionOrder</th> ';
        divData += '                <th>PINumber</th> ';
        divData += '                <th class="rounded-right hide"></th> ';
        divData += '            </tr> ';
        divData += '            </thead> ';
        divData += '            <tbody> ';

        $.each(pVehicle, function (i, item) {
            divData += "<tr ID='" + item.ID + "' " + ">";
            divData += "<td class='ID'> <input name='Delete' " + " type='checkbox' value='" + item.ID + "' /></td>";
            divData += "<td class='ChassisNumber'>" + (item.PurchaseItemCode == 0 ? "" : item.PurchaseItemCode) + "</td>";
            divData += "<td class='ChassisNumber'>" + (item.ChassisNumber == 0 ? "" : item.ChassisNumber) + "</td>";
            divData += "<td class='EngineNumber'>" + (item.EngineNumber == 0 ? "" : item.EngineNumber) + "</td>";
            divData += "<td class='OCNCode'>" + (item.OCNCode == 0 ? "" : item.OCNCode) + "</td>";
            divData += "<td class='Model'>" + (item.Model == 0 ? "" : item.Model) + "</td>";
            divData += "<td class='KeyNumber'>" + (item.KeyNumber == 0 ? "" : item.KeyNumber) + "</td>";
            divData += "<td class='EC'>" + (item.EC == 0 ? "" : item.EC) + "</td>";
            divData += "<td class='PaintType'>" + (item.PaintType == 0 ? "" : item.PaintType) + "</td>";
            divData += "<td class='IC'>" + (item.IC == 0 ? "" : item.IC) + "</td>";
            divData += "<td class='CommercialInvoiceNumber'>" + (item.CommercialInvoiceNumber == 0 ? "" : item.CommercialInvoiceNumber) + "</td>";
            divData += "<td class='BillNumber'>" + (item.BillNumber == 0 ? "" : item.BillNumber) + "</td>";
            divData += "<td class='InsurancePolicyNumber'>" + (item.InsurancePolicyNumber == 0 ? "" : item.InsurancePolicyNumber) + "</td>";
            divData += "<td class='ProductionOrder'>" + (item.ProductionOrder == 0 ? "" : item.ProductionOrder) + "</td>";
            divData += "<td class='PINumber'>" + (item.PINumber == 0 ? "" : item.PINumber) + "</td>";
        }
        );
        divData += '            </tbody> ';
        divData += '        </table> ';
        divData += '    </div> ';
        divData += ' </section> ';
        divData += ' </section> ';
        divData += ' </section> ';
        $("#" + pDivName).append(divData);

        BindAllCheckboxonTable("tblVehicle", "ID", "cb-CheckAll-RoutingsCargo");
        CheckAllCheckbox("HeaderSelectCargoIDs");
    }
   else if ( $("#cbIsFCL").prop("checked") || $("#cbIsFTL").prop("checked") || $("#cbIsTank").prop("checked") || $("#cbIsFlexi").prop("checked")  )
   {
       divData += ' <section class="panel panel-default"  > ';
       divData += '     <div class="table-responsive" > ';
       divData += '        <table id="' + ptblModalName + '" class="table table-striped b-t b-light text-sm  table-hover" style="font-size:100%;"> ';
       divData += '            <thead> ';
       divData += '            <tr> ';
       divData += '                <th id="HeaderSelectCargoIDs"> ';
       divData += '                <input id="cb-CheckAll-RoutingsCargo" type="checkbox"> ';
       divData += '                </th> ';
       divData += '                <th>Container Type</th> ';
       divData += '                <th>Container Number</th> ';
       divData += '                <th>Carrier Seal</th> ';  
       divData += '                <th>TareWt(KG)</th> ';
       divData += '                <th>Vol.(CBM)</th> ';
       divData += '                <th>Net Wt(KG)</th> ';
       divData += '                <th>GrossWt(KG)</th> ';
       divData += '                <th>VGM(KG)</th> ';
       divData += '                <th class="rounded-right hide"></th> ';
       divData += '            </tr> ';
       divData += '            </thead> ';
       divData += '            <tbody> ';
       // Bind Rows
       $.each(pVehicle, function (i, item) {
           divData += "        <tr ID='" + item.ID + "'> ";
           divData += "            <td class='tblModalTransferCargoID'" + " > <input name='" + pCheckboxNameAttr + "' type='checkbox' value='" + item.ID + "' ></td> ";

           divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.ContainerTypeName + "</td> ";
           divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.ContainerNumber + "</td> ";
           divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.CarrierSeal + "</td> ";
           divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.TareWeight + "</td> ";
           divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.Volume + "</td> ";
           divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.NetWeight + "</td> ";
           divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.GrossWeight + "</td> ";
           divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.VGM + "</td> ";
           divData += "        </tr> ";
       });
       divData += '            </tbody> ';
       divData += '        </table> ';
       divData += '    </div> ';
       divData += ' </section> ';
       divData += ' </section> ';
       divData += ' </section> ';
       $("#" + pDivName).append(divData);

       BindAllCheckboxonTable("tblVehicle", "ID", "cb-CheckAll-RoutingsCargo");
       CheckAllCheckbox("HeaderSelectCargoIDs");
   }
   else
   {
       //divData += '                <th id="HeaderSelectCargoIDs"> ';
       //divData += '                <input id="cb-CheckAll-RoutingsCargo" type="checkbox"> ';
       //divData += '                </th> ';
       //divData += '                <th>Package Type</th> ';
       //divData += '                <th>Quantity</th> ';
       //divData += '                <th>Length(CM)</th> ';
       //divData += '                <th>Width(CM)</th> ';
       //divData += '                <th>Height(CM)</th> ';
       //divData += '                <th>Vol.(CBM)</th> ';
       //divData += '                <th>Volumetric.Wt</th> ';
       //divData += '                <th>Net Wt(KG)</th> ';
       //divData += '                <th>GrossWt(KG)</th> ';
       //divData += '                <th>Chg.Wt</th> ';
       //divData += '                <th class="rounded-right hide"></th> ';
       //divData += '            </tr> ';
       //divData += '            </thead> ';
       //divData += '            <tbody> ';
       //// Bind Rows
       //$.each(pVehicle, function (i, item) {
       //    divData += "        <tr ID='" + item.ID + "'> ";
       //    divData += "            <td class='tblModalTransferCargoID'" + " > <input name='" + pCheckboxNameAttr + "' type='checkbox' value='" + item.ID + "' ></td> ";
       //    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.PackageTypeName + "</td> ";
       //    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.Quantity + "</td> ";
       //    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.Length + "</td> ";
       //    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.Width + "</td> ";
       //    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.Height + "</td> ";
       //    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.Volume + "</td> ";
       //    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.VolumetricWeight + "</td> ";
       //    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.NetWeight + "</td> ";
       //    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.GrossWeight + "</td> ";
       //    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.ChargeableWeight + "</td> ";
       //    divData += "        </tr> ";
       //});
       divData += ' <div class="form-group col-sm-2">';
       divData += ' <label>Total Gross Weight</label>';
       divData += '<input type="text" id="txtGrossWeightTruckingOrderAdd" onfocus="DisableEnterKey(id);" onkeypress="DisableEnterKey(id);" class="form-control parsley-validated input-sm" data-required="false" maxlength="15" placeholder="" style="text-transform:uppercase" disabled="disabled" />';
       divData += '</div>';


       divData += ' <div class="form-group col-sm-2">';
       divData += ' <label>Total Trucking Order Gross Weight</label>';
       divData += '<input type="text" id="txtTotalAddedGrossWeightTruckingOrder" onfocus="DisableEnterKey(id);" onkeypress="DisableEnterKey(id);" class="form-control parsley-validated input-sm" data-required="false" maxlength="15" placeholder="" style="text-transform:uppercase" disabled="disabled" />';
       divData += '</div>';

       divData += ' <div class="form-group col-sm-2">';
       divData += ' <label>Added Gross Weight</label>';
       divData += '<input type="text" id="txtAddedGrossWeightTruckingOrderAdd" onkeypress="CheckValueIsDecimal(id);" onfocus="CheckValueIsDecimal(id);"  placeholder="0" class="form-control parsley-validated input-sm" data-required="false" maxlength="15"  style="text-transform:uppercase" onblur="CheckDecimalFormat(id);CalculateRemainingGrossWeight();" />';
       divData += '</div>';


       divData += ' <div class="form-group col-sm-2">';
       divData += ' <label>Remaining Gross Weight</label>';
       divData += '<input type="text" id="txtRemainingGrossWeightTruckingOrderAdd" onfocus="DisableEnterKey(id);" onkeypress="DisableEnterKey(id);" class="form-control parsley-validated input-sm" data-required="false" maxlength="15" placeholder="" style="text-transform:uppercase" disabled="disabled" />';
       divData += '</div>';

       $("#" + pDivName).append(divData);

       var CargoGrossWeight = $('#txtAddedGrossWeightTruckingOrder').val();
       var TotalGrossWeight = $('#lblTotalGrossWeight').text().replace(':', '').replace(' ', '');

       $("#txtGrossWeightTruckingOrderAdd").val($('#lblTotalGrossWeight').text().replace(':', '').replace(' ', ''));
       $("#txtTotalAddedGrossWeightTruckingOrder").val(CargoGrossWeight);
       $("#txtAddedGrossWeightTruckingOrderAdd").val(0);
       $("#txtRemainingGrossWeightTruckingOrderAdd").val((TotalGrossWeight - CargoGrossWeight));

   }




   // HighlightText("#tblVehicle>tbody>tr", $("#txtOperationContainerAndPackagesSearch").val().trim());
    if (showDeleteFailMessage) {//sherif: if deletion failed due to foreign key, then this message is shown
        swal(strSorry, strDeleteFailMessage);
        showDeleteFailMessage = false;
    }
}
function CalculateRemainingGrossWeight()
{
    debugger;

    var CargoGrossWeight = $('#txtAddedGrossWeightTruckingOrderAdd').val();
    var TotalGrossWeight = $('#lblTotalGrossWeight').text().replace(':', '').replace(' ', '');
    var TotalTruckingOrderGross = $("#txtTotalAddedGrossWeightTruckingOrder").val();

    var RemainingGrossWeight = parseFloat(TotalGrossWeight) - parseFloat(CargoGrossWeight) - parseFloat(TotalTruckingOrderGross);
  
    $("#txtRemainingGrossWeightTruckingOrderAdd").val(RemainingGrossWeight);
}
function Vehicle_Save(pTableName) {
    debugger;


    //if ($("#hRoutingID" + RoutingSuffix).val() == "")
    //    Routings_Insert(false);
    var pCargoGrossWeight = $('#txtAddedGrossWeightTruckingOrderAdd').val();
    var _SelectedVehicles = GetAllSelectedIDsAsString(pTableName);
    var ErrorMsg='';

    if(!$("#cbIsVehicle").prop("checked") && !$("#cbIsFCL").prop("checked") && !$("#cbIsFTL").prop("checked") && !$("#cbIsTank").prop("checked")  && !$("#cbIsFlexi").prop("checked"))
    {
        var TotalGrossWeight = $('#lblTotalGrossWeight').text().replace(':', '').replace(' ', '');

        if (pCargoGrossWeight == '' || parseFloat(pCargoGrossWeight) == 0)
            ErrorMsg = "Please, Insert Gross Weight.";
        else if (parseFloat($("#txtRemainingGrossWeightTruckingOrderAdd").val()) < 0)
            ErrorMsg = "Please, Insert Gross Weight Less Or Equal To Total Gross Weight.";
     
    }

    if (_SelectedVehicles == "" && ($("#cbIsVehicle").prop("checked") || $("#cbIsFCL").prop("checked") || $("#cbIsFTL").prop("checked") || $("#cbIsTank").prop("checked") || $("#cbIsFlexi").prop("checked")))
    {
        if ($("#cbIsVehicle").prop("checked") == true)
            swal("Sorry", "Please, select vehicles.");
        else if ($("#cbIsFCL").prop("checked") || $("#cbIsFTL").prop("checked") || $("#cbIsTank").prop("checked") || $("#cbIsFlexi").prop("checked"))
            swal("Sorry", "Please, select Containers.");
    }
    else if ( ErrorMsg !='' )
    {
        swal("Sorry", ErrorMsg);
    }
    else {
        FadePageCover(true);
        var pParametersWithValues = {
            pOperationVehicleIDsList: $("#cbIsVehicle").prop("checked") ? _SelectedVehicles : ""
            , pOperationsContainersAndPackagesIDsList: $("#cbIsVehicle").prop("checked") ? "" : _SelectedVehicles
            , pOperationID: $("#hOperationID").val()
            , pRoutingID: $("#hRoutingID" + RoutingSuffix).val()
            , pCargoGrossWeight: pCargoGrossWeight
        };
        CallPOSTFunctionWithParameters("/api/Routings/Vehicle_Save", pParametersWithValues
        , function (pData) {
            var _MessageReturned = pData[0];
          
            if (_MessageReturned == "") {
                jQuery("#SelectCargoModal").modal("hide");
                swal("Success",  "Saved successfully." );
                Routing_TransferCargo();
            }
            else {
                swal("Sorry", _MessageReturned);
                FadePageCover(false);
            }
        }
        , null);
    }
}
function Routings_CargoDeleteList(callback) {
    debugger;
    //Confirmation message to delete
                        
    var ptblModalName = "SelectTransferCargoModal";
    if (GetAllSelectedIDsAsString(ptblModalName) != "")
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
            DeleteListFunction("/api/Routings/DeleteCargo"
                , { "pTruckingOrderCargoIDs": GetAllSelectedIDsAsString(ptblModalName) }
                , function () {
                    Routing_TransferCargo()
                });
        });
}

function Routing_TransferCargo() {
    debugger; 
    var pStrFnName = "/api/Routings/LoadCargoWithWhereClause";
                    
    var pDivName = "divSelectTransferCargo";//div name to be filled
    var ptblModalName = "SelectTransferCargoModal";
    var pCheckboxNameAttr = "Delete";
    var pWhereClause = "";
    pWhereClause += "Where RoutingID= " + $("#hRoutingID" + RoutingSuffix).val();

    FillTransferCargoModalTable(pStrFnName, pWhereClause, pDivName, ptblModalName, pCheckboxNameAttr, true //pIsInsertChoice
        ,null
        //function () {
        //    HighlightText("#" + pDivName, $("#txtSearchCharges").val().trim().toUpperCase());
        //}
        );

    //$("#btn-SearchCharges").attr("onclick", "Receivables_GenerateFromPayables();");
    //$("#btnSelectChargesApply").attr("onclick", "Receivables_InsertListWithValuesFromPayables(false);");
}
function FillTransferCargoModalTable(pStrFnName, pWhereClause, pDivName, ptblModalName, pCheckboxNameAttr, pIsInsertChoice, callback) {
    FadePageCover(true);
    $.ajax({
        type: "GET",
        url: pStrFnName,
        data: { pWhereClause: pWhereClause, pOperationID: $("#hOperationID").val() == "" ? 0 : $("#hOperationID").val() },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            //Clear the div
            $("#" + pDivName).html("");
            var divData = '';
            if ($("#cbIsVehicle").prop("checked")== true )
            {
                divData += ' <section class="panel panel-default" > ';
                divData += '    <div class="table-responsive"> ';
                divData += '        <table id="' + ptblModalName + '" class="table table-striped b-t b-light text-sm  table-hover"> ';
                divData += '            <thead> ';
                divData += '            <tr> ';
                divData += '                <th id="HeaderSelectTransferCargoIDs"> ';
                divData += '                <input id="cb-CheckAll-RoutingsTransferCargo" type="checkbox"> ';
                divData += '                </th> ';
                divData += '                <th>Code</th> ';
                divData += '                <th>ChassisNumber</th> ';
                divData += '                <th>EngineNumber</th> ';
                divData += '                <th>OCNCode</th> ';
                divData += '                <th>Model</th> ';
                divData += '                <th>KeyNumber</th> ';
                divData += '                <th>EC</th> ';
                divData += '                <th>PaintType</th> ';
                divData += '                <th>IC</th> ';
                divData += '                <th>CommercialInvoiceNumber</th> ';
                divData += '                <th>BillNumber</th> ';
                divData += '                <th>InsurancePolicyNumber</th> ';
                divData += '                <th>ProductionOrder</th> ';
                divData += '                <th>PINumber</th> ';
                divData += '                <th class="rounded-right hide"></th> ';
                divData += '            </tr> ';
                divData += '            </thead> ';
                divData += '            <tbody> ';
                // Bind Rows
                $.each(JSON.parse(data[0]), function (i, item) {
                    divData += "        <tr ID='" + item.ID + "'> ";
                    divData += "            <td class='tblModalTransferCargoID'" + " > <input name='" + pCheckboxNameAttr + "' type='checkbox' value='" + item.ID + "' ></td> ";
                    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.PurchaseItemCode + "</td> ";
                    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.ChassisNumber + "</td> ";
                    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.EngineNumber + "</td> ";
                    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.OCNCode + "</td> ";
                    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.Model + "</td> ";
                    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.KeyNumber + "</td> ";
                    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.EC + "</td> ";
                    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.PaintType + "</td> ";
                    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.IC + "</td> ";
                    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.CommercialInvoiceNumber + "</td> ";
                    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.BillNumber + "</td> ";
                    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.InsurancePolicyNumber + "</td> ";
                    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.ProductionOrder + "</td> ";
                    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.PINumber + "</td> ";
                    divData += "        </tr> ";
                });
                divData += '            </tbody> ';
                divData += '        </table> ';
                divData += '    </div> ';
                divData += ' </section> ';
                $("#" + pDivName).append(divData);
    
                BindAllCheckboxonTable(ptblModalName, "ID", "cb-CheckAll-RoutingsTransferCargo");
                CheckAllCheckbox("HeaderSelectTransferCargoIDs");
            }
            else if ($("#cbIsFCL").prop("checked") || $("#cbIsFTL").prop("checked") || $("#cbIsTank").prop("checked") || $("#cbIsFlexi").prop("checked")) {
                divData += ' <section class="panel panel-default" > ';
                divData += '    <div class="table-responsive"> ';
                divData += '        <table id="' + ptblModalName + '" class="table table-striped b-t b-light text-sm  table-hover"> ';
                divData += '            <thead> ';
                divData += '            <tr> ';
                divData += '                <th id="HeaderSelectTransferCargoIDs"> ';
                divData += '                <input id="cb-CheckAll-RoutingsTransferCargo" type="checkbox"> ';
                divData += '                </th> ';
                divData += '                <th>'+ TranslateString("Container Type")+'</th> ';
                divData += '                <th>'+ TranslateString("Container Number")+'</th> ';
                divData += '                <th>'+ TranslateString("Carrier Seal")+'</th> ';
                divData += '                <th>'+ TranslateString("TareWt(KG)")+'</th> ';
                divData += '                <th>'+ TranslateString("Vol.(CBM)")+'</th> ';
                divData += '                <th>'+ TranslateString("Net Wt(KG)")+'</th> ';
                divData += '                <th>'+ TranslateString("GrossWt(KG)")+'</th> ';
                divData += '                <th>' + TranslateString("VGM(KG)") + '</th> ';
                divData += '                <th class="rounded-right hide"></th> ';
                divData += '            </tr> ';
                divData += '            </thead> ';
                divData += '            <tbody> ';
                // Bind Rows
                $.each(JSON.parse(data[0]), function (i, item) {
                    divData += "        <tr ID='" + item.ID + "'> ";
                    divData += "            <td class='tblModalTransferCargoID'" + " > <input name='" + pCheckboxNameAttr + "' type='checkbox' value='" + item.ID + "' ></td> ";

                    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.ContainerTypeName + "</td> ";
                    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.ContainerNumber + "</td> ";
                    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.CarrierSeal + "</td> ";
                    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.TareWeight + "</td> ";
                    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.Volume + "</td> ";
                    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.NetWeight + "</td> ";
                    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.GrossWeight + "</td> ";
                    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.VGM + "</td> ";
                    divData += "        </tr> ";
                });
                divData += '            </tbody> ';
                divData += '        </table> ';
                divData += '    </div> ';
                divData += ' </section> ';
                $("#" + pDivName).append(divData);

    
                BindAllCheckboxonTable(ptblModalName, "ID", "cb-CheckAll-RoutingsTransferCargo");
                CheckAllCheckbox("HeaderSelectTransferCargoIDs");
            }
            else
            {
                divData += ' <section class="panel panel-default hide" > ';
                divData += '    <div class="table-responsive "> ';
                divData += '        <table id="' + ptblModalName + '" class="table table-striped b-t b-light text-sm  table-hover"> ';
                divData += '            <thead> ';
                divData += '            <tr> ';
                divData += '                <th id="HeaderSelectTransferCargoIDs"> ';
                divData += '                <input id="cb-CheckAll-RoutingsTransferCargo" type="checkbox"> ';
                divData += '                </th> ';
                divData += '                <th>Package Type</th> ';
                divData += '                <th>Quantity</th> ';
                divData += '                <th>Length(CM)</th> ';
                divData += '                <th>Width(CM)</th> ';
                divData += '                <th>Height(CM)</th> ';
                divData += '                <th>Vol.(CBM)</th> ';
                divData += '                <th>Volumetric.Wt</th> ';
                divData += '                <th>Net Wt(KG)</th> ';
                divData += '                <th>GrossWt(KG)</th> ';
                divData += '                <th>Chg.Wt</th> ';
                divData += '                <th class="rounded-right hide"></th> ';
                divData += '            </tr> ';
                divData += '            </thead> ';
                divData += '            <tbody> ';
                // Bind Rows
                $.each(JSON.parse(data[0]), function (i, item) {
                    divData += "        <tr ID='" + item.ID + "'> ";
                    divData += "            <td class='tblModalTransferCargoID'" + " > <input name='" + pCheckboxNameAttr + "' type='checkbox' checked value='" + item.ID + "' ></td> ";

                    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.PackageTypeName + "</td> ";
                    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.Quantity + "</td> ";
                    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.Length + "</td> ";
                    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.Width + "</td> ";
                    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.Height + "</td> ";
                    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.Volume + "</td> ";
                    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.VolumetricWeight + "</td> ";
                    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.NetWeight + "</td> ";
                    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.GrossWeight + "</td> ";
                    divData += "            <td class='tblModalReceivable' id=PackageType" + item.ID + "  style='width:300px;'>" + item.ChargeableWeight + "</td> ";


                //    divData += "        </tr> ";
                });
                divData += '            </tbody> ';
                divData += '        </table> ';
                divData += '    </div> ';
                divData += ' </section> ';

                divData += ' <div class="form-group col-sm-2">';
                divData += ' <label>Total Gross Weight</label>';
                divData += '<input type="text" id="txtGrossWeightTruckingOrder" onfocus="DisableEnterKey(id);" onkeypress="DisableEnterKey(id);" class="form-control parsley-validated input-sm" data-required="false" maxlength="15" placeholder="" style="text-transform:uppercase" disabled="disabled" />';
                divData += '</div>';

                divData += ' <div class="form-group col-sm-2">';
                divData += ' <label>Added Gross Weight</label>';
                divData += '<input type="text" id="txtAddedGrossWeightTruckingOrder" onfocus="DisableEnterKey(id);" onkeypress="DisableEnterKey(id);" class="form-control parsley-validated input-sm" data-required="false" maxlength="15" placeholder="" style="text-transform:uppercase" disabled="disabled" />';
                divData += '</div>';

                divData += ' <div class="form-group col-sm-2">';
                divData += ' <label>Remaining Gross Weight</label>';
                divData += '<input type="text" id="txtRemainingGrossWeightTruckingOrder" onfocus="DisableEnterKey(id);" onkeypress="DisableEnterKey(id);" class="form-control parsley-validated input-sm" data-required="false" maxlength="15" placeholder="" style="text-transform:uppercase" disabled="disabled" />';
                divData += '</div>';
                
                $("#" + pDivName).append(divData);

                var CargoGrossWeight = JSON.parse(data[0]).length > 0 ? JSON.parse(data[0])[0].CargoGrossWeight : 0;
                var TotalGrossWeight = $('#lblTotalGrossWeight').text().replace(':', '').replace(' ', '');
                var RemainingGrossWeight = parseFloat(TotalGrossWeight) - parseFloat(CargoGrossWeight);

                $("#txtGrossWeightTruckingOrder").val($('#lblTotalGrossWeight').text().replace(':', '').replace(' ', ''));
                $("#txtAddedGrossWeightTruckingOrder").val(CargoGrossWeight);
                $("#txtRemainingGrossWeightTruckingOrder").val(RemainingGrossWeight);
            }
            FadePageCover(false);

            if (callback != null && callback != undefined)
                callback();
        },
        error: function (jqXHR, exception) {
                FadePageCover(false);
                swal("Sorry", "Please, press Ctrl+Shft+R, and if the problem persists contact your technical support! FillTransferCargoModalTable in mainapp.master.js", "");
        }
    });
}


/*****************************Tracking Fns*************************************/
function CustomClearanceTracking_LoadData() {
    debugger;
    if ($("#tblCustomClearanceTracking tbody tr").length == 0) {
        FadePageCover(true);
        CallGETFunctionWithParameters("/api/OperationCustomClearanceTracking/LoadAll",
            {
                pWhereClause: "WHERE CustomClearanceRoutingID =" + ($("#hRoutingIDCustomsClearance").val() == "" ? 0 : $("#hRoutingIDCustomsClearance").val()) + " ORDER BY ViewOrder, TrackingDate"
            }
            , function (pData) { CustomClearanceTracking_BindTableRows(JSON.parse(pData[0])); FadePageCover(false); }
            , null);
    }
}
function CustomClearanceTracking_BindTableRows(pCustomClearanceTracking) {
    debugger;
    ClearAllTableRows("tblCustomClearanceTracking");
    //var printControlsText = " class='btn btn-xs btn-rounded btn-lightblue float-right' title='Print'> <i class='fa fa-print' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + TranslateString("Print") + "</span>";
    var alarmControlsText = " class='btn btn-xs btn-rounded btn-warning float-right' title='Print'> <i class='fa fa-bell' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + "Alarm" + "</span>";
    var emailControlsText = " class='btn btn-xs btn-rounded btn-lightblue float-right' title='Print'> <i class='fa fa-envelope' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + "Email" + "</span>";
    $.each(pCustomClearanceTracking, function (i, item) {
        AppendRowtoTable("tblCustomClearanceTracking",
            ("<tr ID='" + item.ID + "' " + (1==1 ? "ondblclick='CustomClearanceTracking_FillControls(" + item.ID + ',"tblCustomClearanceTracking"' + ");'>" : ">")
                + "<td class='TrackingID'> <input " + (1 == 1 ? "name='Delete'" : "disabled='disabled'") + " type='checkbox' value='" + item.ID + "' /></td>"
                + "<td class='TrackingStageID hide'>" + item.TrackingStageID + "</td>"
                + "<td class='TrackingStageName'>" + (item.TrackingStageID == 0 ? "" : item.TrackingStageName) + "</td>"
                + "<td class='TrackingCustodyID hide'>" + item.CustodyID + "</td>"
                + "<td class='TrackingCustodyName'>" + (item.CustodyID == 0 ? "" : item.CustodyName) + "</td>"
                //+ "<td class='TrackingStageNotes hide'>" + item.TrackingStageNotes + "</td>"
                + "<td class='TrackingStringTrackingDate'>" + item.StringTrackingDate + "</td>"
                + "<td class='TrackingNotes'>" + item.Notes + "</td>"
                + "<td class='TrackingDone'> <input id='cbIsDone" + item.ID + "' type='checkbox' disabled='disabled' val='" + (item.Done == true ? "true' checked='checked'" : "'") + " /></td>"
                //+ "<td class='TrackingCreatorName hide'>" + item.CreatorName + "</td>"
                //+ "<td class='ModificatorName hide'>" + item.ModificatorName + "</td>"
                //+ "<td class='hide'><a href='#TrackingModal' data-toggle='modal' onclick='Tracking_FillControls(" + item.ID + ");' " + editControlsText + "</a></td>"
                + "<td class=''>"
                + "<a href='#' class='hide' data-toggle='modal' onclick='CustomClearanceTracking_GetAvailableUsers(" + item.ID + ");' " + alarmControlsText + "</a>"
                + "<a href='#' data-toggle='modal' onclick='CustomClearanceTracking_OpenSendEmailModal(" + item.ID + " , \"tblCustomClearanceTracking\");' " + emailControlsText + "</a>"
                + "</td>"
                + "</tr>"));
    });
    //$.each(pTracking, function (i, item) {
    //    AppendRowtoTable("tblCustomClearanceTracking",
    //        ("<tr ID='" + item.ID + "' " + (OETra && $("#hIsOperationDisabled").val() == false ? "ondblclick='Tracking_FillControls(" + item.ID + ',"tblCustomClearanceTracking"' + ");'>" : ">")
    //            + "<td class='CustomClearanceTrackingID'> <input " + (1 == 1 ? "name='Delete'" : "disabled='disabled'") + " type='checkbox' value='" + item.ID + "' /></td>"
    //            + "<td class='TrackingStageID hide'>" + item.TrackingStageID + "</td>"
    //            + "<td class='TrackingStageName'>" + (item.TrackingStageID == 0 ? "" : item.TrackingStageName) + "</td>"
    //            + "<td class='TrackingCustodyID hide'>" + item.CustodyID + "</td>"
    //            + "<td class='TrackingCustodyName'>" + (item.CustodyID == 0 ? "" : item.CustodyName) + "</td>"
    //            //+ "<td class='TrackingStageNotes hide'>" + item.TrackingStageNotes + "</td>"
    //            + "<td class='TrackingStringTrackingDate'>" + item.StringTrackingDate + "</td>"
    //            + "<td class='TrackingNotes'>" + item.Notes + "</td>"
    //            + "<td class='TrackingDone'> <input id='cbIsDone_CustomClearanceTracking" + item.ID + "' type='checkbox' disabled='disabled' val='" + (item.Done == true ? "true' checked='checked'" : "'") + " /></td>"
    //            //+ "<td class='TrackingCreatorName hide'>" + item.CreatorName + "</td>"
    //            //+ "<td class='ModificatorName hide'>" + item.ModificatorName + "</td>"
    //            //+ "<td class='hide'><a href='#TrackingModal' data-toggle='modal' onclick='Tracking_FillControls(" + item.ID + ");' " + editControlsText + "</a></td>"
    //            + "<td class=''>"
    //            + "<a href='#' data-toggle='modal' onclick='Tracking_GetAvailableUsers(" + item.ID + ");' " + alarmControlsText + "</a>"
    //            + "<a href='#' data-toggle='modal' onclick='Tracking_OpenSendEmailModal(" + item.ID + ", \"tblCustomClearanceTracking\");' " + emailControlsText + "</a>"
    //            + "</td>"
    //            + "</tr>"));
    //});

    BindAllCheckboxonTable("tblCustomClearanceTracking", "CustomClearanceTrackingID", "cb-CheckAll-CustomClearanceTracking");
    CheckAllCheckbox("HeaderDeleteCustomClearanceTrackingID");

    if (showDeleteFailMessage) { //sherif: if deletion failed due to foreign key, then this message is shown
        swal(strSorry, strDeleteFailMessage, "warning");
        showDeleteFailMessage = false;
    }
}


function CustomClearanceTracking_OpenModal()
{
    
    if ( IsNull($("#hRoutingIDCustomsClearance").val() , "") == "")
        swal("Sorry", "Please, save record first.");
    else
    {
        CustomClearanceTracking_ClearAllControls();
        jQuery("#CustomClearanceTrackingModal").modal("show");

    }
}


function CustomClearanceTracking_ClearAllControls() {
    ClearAll("#CustomClearanceTrackingModal");
    var TodaysDateInddMMyyyyFormat = getTodaysDateInddMMyyyyFormat();
    $("#txtCustomClearanceTrackingDate").val(TodaysDateInddMMyyyyFormat);
    var pWhereClause = "WHERE 1=1 AND IsClearance=1";
    if ($("#slCustomClearanceTrackingCustody option").length < 2) {
        FadePageCover(true);
        GetListWithNameAndWhereClause(null, "/api/Custody/LoadAll", TranslateString("SelectFromMenu"), "slCustomClearanceTrackingCustody"
            , "ORDER BY Name"
            , null //function () { FadePageCover(false); }
        );
        //GetListWithNameAndWhereClause(null, "/api/CustomClearanceTrackingStage/LoadAll", TranslateString("SelectFromMenu"), "slCustomClearanceTrackingStage"
        //, pWhereClause
        //    , function () { FadePageCover(false); }
        //);
        GetListWithNameAndWhereClauseWithMultiAttrs(null, "Notes", "/api/TrackingStage/LoadAll", TranslateString("SelectFromMenu")
            , "slCustomClearanceTrackingStage"
            , pWhereClause
            , function () { FadePageCover(false); });
    }
    else {
        $("#slCustomClearanceTrackingCustody").val("");
        $("#slCustomClearanceTrackingStage").val("");
    }
    $("#btnSaveCustomClearanceTracking").attr("onclick", "CustomClearanceTracking_Insert(false);");
    $("#btnSaveAndNewCustomClearanceTracking").attr("onclick", "CustomClearanceTracking_Insert(false);");
}
function CustomClearanceTracking_FillControls(pID, pTableName) {
    debugger;
    jQuery("#CustomClearanceTrackingModal").modal("show");
    ClearAll("#CustomClearanceTrackingModal");
    $("#hCustomClearanceTrackingID").val(pID);
    var tr = $("#" + pTableName + " tr[ID='" + pID + "']");
    $("#lblCustomClearanceTrackingShown").html(": " + $(tr).find("td.TrackingStageName").text());
    var pCustomClearanceTrackingStageID = $(tr).find("td.TrackingStageID").text();
    var pCustomClearanceTrackingCustodyID = ($(tr).find("td.TrackingCustodyID").text() == 0 ? "" : $(tr).find("td.TrackingCustodyID").text());
    $("#txtCustomClearanceTrackingDate").val($(tr).find("td.TrackingStringTrackingDate").text());
    $("#txtCustomClearanceTrackingNotes").val($(tr).find("td.TrackingNotes").text());
    if (pTableName == "tblCustomClearanceTracking")
        $("#cbIsDoneCustomClearanceTracking").prop("checked", $("#cbIsDone" + pID).prop("checked"));
    else
        $("#cbIsDoneCustomClearanceTracking").prop("checked", $("#cbIsDone" + pID).prop("checked"));
    var pWhereClause = "WHERE 1=1 AND IsClearance = 1";

    pWhereClause += " ORDER BY ViewOrder";
    if ($("#slCustomClearanceTrackingCustody option").length < 2) {
        FadePageCover(true);
        GetListWithNameAndWhereClause(pCustomClearanceTrackingCustodyID, "/api/Custody/LoadAll", TranslateString("SelectFromMenu")
            , "slCustomClearanceTrackingCustody"
            , "ORDER BY Name"
            , null //function () { FadePageCover(false); }
        );


        GetListWithNameAndWhereClauseWithMultiAttrs(pCustomClearanceTrackingStageID, "Notes", "/api/TrackingStage/LoadAll", TranslateString("SelectFromMenu")
            , "slCustomClearanceTrackingStage"
            , pWhereClause
            , function () { FadePageCover(false); });
    }
    else {
        $("#slCustomClearanceTrackingCustody").val(pCustomClearanceTrackingCustodyID);
        $("#slCustomClearanceTrackingStage").val(pCustomClearanceTrackingStageID);
    }

    $("#hPreCustomClearanceTrackingStageNotes").val($(tr).find("td.TrackingNotes").text());
    $("#hPreCustomClearanceTrackingStageID").val(pCustomClearanceTrackingStageID);
    $("#btnSaveCustomClearanceTracking").attr("onclick", "CustomClearanceTracking_Update(false);");
    $("#btnSaveAndNewCustomClearanceTracking").attr("onclick", "CustomClearanceTracking_Update(false);");

}
function GetCustomClearanceTrackingStageNotes() {
    if (IsNull($("#hCustomClearanceTrackingID").val(), "0") == "0" || $('#hPreCustomClearanceTrackingStageID').val() != $("#slCustomClearanceTrackingStage").val()) {
        $('#txtCustomClearanceTrackingNotes').val($("#slCustomClearanceTrackingStage option:selected").attr('Notes'));
    }
    else if ($('#hPreCustomClearanceTrackingStageID').val() == $("#slCustomClearanceTrackingStage").val()) {
        $('#txtCustomClearanceTrackingNotes').val($("#hPreCustomClearanceTrackingStageNotes").val());
    }

}
function CustomClearanceTracking_Insert(pSaveandAddNew) {
    debugger;
    if (ValidateForm("form", "CustomClearanceTrackingModal")) {
        FadePageCover(true);
        var pParametersWithValues = {
              pCustomClearanceRoutingID: $("#hRoutingIDCustomsClearance").val()
            , pTrackingStageID: $("#slCustomClearanceTrackingStage").val()
            , pCustodyID: $("#slCustomClearanceTrackingCustody").val() == "" ? 0 : $("#slCustomClearanceTrackingCustody").val()
            , pTrackingDate: ConvertDateFormat($("#txtCustomClearanceTrackingDate").val())
            , pNotes: $("#txtCustomClearanceTrackingNotes").val().trim() == "" ? "0" : $("#txtCustomClearanceTrackingNotes").val().trim().toUpperCase()
            , pDone: $("#cbIsDoneCustomClearanceTracking").prop("checked")
        };
        CallGETFunctionWithParameters("/api/OperationCustomClearanceTracking/Insert", pParametersWithValues
            , function (pData) {
                debugger;
                if (pData[0]) {
                    CustomClearanceTracking_BindTableRows(JSON.parse(pData[1]));
                    jQuery("#CustomClearanceTrackingModal").modal("hide");
                }
                else
                    swal("Sorry", "Connection failed, please try again.");
                FadePageCover(false);
            }
            , null);
    }
}
function CustomClearanceTracking_Update(pSaveandAddNew) {
    debugger;
    if (ValidateForm("form", "CustomClearanceTrackingModal")) {
        FadePageCover(true);
        var pParametersWithValues = {
            pID: $("#hCustomClearanceTrackingID").val()
            ,  pCustomClearanceRoutingID: $("#hRoutingIDCustomsClearance").val()
            , pTrackingStageID: $("#slCustomClearanceTrackingStage").val()
            , pCustodyID: $("#slCustomClearanceTrackingCustody").val() == "" ? 0 : $("#slCustomClearanceTrackingCustody").val()
            , pTrackingDate: ConvertDateFormat($("#txtCustomClearanceTrackingDate").val())
            , pNotes: $("#txtCustomClearanceTrackingNotes").val().trim() == "" ? "0" : $("#txtCustomClearanceTrackingNotes").val().trim().toUpperCase()
            , pDone: $("#cbIsDoneCustomClearanceTracking").prop("checked")
        };
        CallGETFunctionWithParameters("/api/OperationCustomClearanceTracking/Update", pParametersWithValues
            , function (pData) {
                debugger;
                if (pData[0]) {
                    CustomClearanceTracking_BindTableRows(JSON.parse(pData[1]));
                    jQuery("#CustomClearanceTrackingModal").modal("hide");
                }
                else
                    swal("Sorry", "Connection failed, please try again.");
                FadePageCover(false);
            }
            , null);
    }
}
function CustomClearanceTracking_DeleteList(pTableName) {
    debugger;
    var pDeletedCustomClearanceTrackingIDs = GetAllSelectedIDsAsString(pTableName);
    if (pDeletedCustomClearanceTrackingIDs != "")
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
                var pParametersWithValues = { pDeletedTrackingIDs: pDeletedCustomClearanceTrackingIDs, pCustomClearanceRoutingID: $("#hRoutingIDCustomsClearance").val()};
                CallGETFunctionWithParameters("/api/OperationCustomClearanceTracking/Delete", pParametersWithValues
                    , function (pData) {
                        if (pData[0])
                            CustomClearanceTracking_BindTableRows(JSON.parse(pData[1]));
                        else
                            swal("Sorry", "Connection failed, please try again.");
                        FadePageCover(false);
                    }
                    , null);
            });
}
function CustomClearanceTracking_GetAvailableUsers(pTrackingStageID) {
    debugger;
    Receptionists_GetAvailableUsers();
    $("#btnCheckboxesListApply").attr("onclick", "CustomClearanceTracking_SendAlarm(" + pCustomClearanceTrackingStageID + ");");
    $("#btnCheckboxesListApply").html("<span class='glyphicon glyphicon-send'></span> Send");
    $("#btn-SearchItems").attr("onclick", "Receptionists_GetAvailableUsers();");
}
function CustomClearanceTracking_SendAlarm(pCustomClearanceTrackingStageID) {
    debugger;
    var pModalName = "CheckboxesListModal";
    var pCheckboxNameAttr = "cbAddedItemID";
    var pAlarmReceiversIDs = GetAllSelectedIDsAsStringWithNameAttr(pCheckboxNameAttr);
    if (pAlarmReceiversIDs == "")
        swal("Sorry", "You did not select any receivers.");
    else {
        FadePageCover(true);
        var pParametersWithValues = {
            pTrackingStageID: pCustomClearanceTrackingStageID
            , pAlarmReceiversIDs: pAlarmReceiversIDs
        };
        CallGETFunctionWithParameters("/api/OperationCustomClearanceTracking/SendAlarm", pParametersWithValues
            , function (pData) {
                var _MessageReturned = pData[0];
                if (pData[0] == "")
                    swal("Success", "Task sent successfully.");
                else
                    swal("Sorry", _MessageReturned);
                FadePageCover(false);
            }
            , null);
    }
}
//MOSTAA_01/10/2020
function CustomClearanceTracking_OpenSendEmailModal(pID, tblID) {
    jQuery("#CustomClearanceTrackingEMailModal").modal("show");
    ClearAll("#CustomClearanceTrackingEMailModal");
    $("#hEmailCustomClearanceTrackingID").val(pID);
    var tr = $("#" + tblID + " tr[ID='" + pID + "']");
    $('#txtCustomClearanceTrackingEmailNotes').val($(tr).find("td.TrackingNotes").text());

    $('#lblShownCustomClearanceTrackingEmail').text($("#hClientEmail").val().toLowerCase());
    $('#txtCustomClearanceTrackingEmailTo').val($("#hClientEmail").val().toLowerCase());
}


function CustomClearanceTracking_SendEmail() {
    debugger;
    swal({
        title: "Are you sure?",
        text: "This will be sent via email.",
        type: "", //"warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes, Send.",
        closeOnConfirm: true
    },
        function () {
            FadePageCover(true);
            var pParametersWithValues = {
                pTrackingStageID_SendEmail: $("#hEmailCustomClearanceTrackingID").val()
                , pSubject: $('#txtCustomClearanceTrackingEmailSubject').val()
                , pTo: $('#txtCustomClearanceTrackingEmailTo').val()
                , pCC: $('#txtCustomClearanceTrackingEmailCC').val()
                , pBody: $('#txtCustomClearanceTrackingEmailNotes').val()
            }; 
            CallGETFunctionWithParameters("/api/OperationCustomClearanceTracking/CustomClearanceTracking_SendEmail", pParametersWithValues
                , function (pData) {
                    var _MessageReturned = pData[0];
                    if (pData[0] == "")
                        swal("Success", "Task sent successfully.");
                    else
                        swal("Sorry", _MessageReturned);
                    FadePageCover(false);
                }
                , null);
        });
}
/***********************************************************************************/