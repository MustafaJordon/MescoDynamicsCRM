
function DisableEnterKeyWithSearch(pInputID) {
    debugger;
    if (pInputID == "txtSearchCustomerName2")
    {
        $("#" + pInputID).keydown(function (e) {
            //  (backspace=8) , (delete=46) , (tab=9) , (escape=), (enter=13) , (.=190)
            if (e.keyCode == 13) {
                e.preventDefault();
                CustomerEnterEvent(5);
            }
            else
                return;
        });
    }
    else
    {
        $("#" + pInputID).keydown(function (e) {
            //  (backspace=8) , (delete=46) , (tab=9) , (escape=), (enter=13) , (.=190)
            if (e.keyCode == 13) {
                e.preventDefault();
                CustomerEnterEvent(0);
            }
            else
                return;
        });
    }

}
function CustomerEnterEvent(pID, callback) {
    debugger;
    var SearchText = $('#txtSearchCustomerName').val();
    if (pID == 5)
    {
        SearchText = $('#txtSearchCustomerName2').val();
    }
    
    var SearchCondition = "Where Name like N'%" + SearchText + "%' ";

    if ($.isNumeric(SearchText))
        SearchCondition += " or Code='" + SearchText + "'";

    if (pID != 0 && pID != 5)
        SearchCondition = " Where ID=" + pID

   ////FadePageCover(true);
    CallGETFunctionWithParameters("/api/Customers/LoadAll"
        , {
            //pDate: ConvertDateFormat(pFormattedTodaysDate)
            pWhereClause: SearchCondition
        }
        , function (pData) {

            //if (JSON.parse(pData[0]).length == 1) {
            //    var pFirstID = JSON.parse(pData[0])[0].ID;
            //    if (pID == 0 || pID == "undefined" || pID == undefined)
            //        pID = pFirstID;
            //}

            if (pID == 5)
            {
                FillListFromObject(pID, 2, "<--Select-->", "slCustomer2", pData[0], function () {

                    $("#slCustomer2").css({ "width": "100%" }).select2();
                    $("#slCustomer2").trigger("change");
                    $("div[tabindex='-1']").removeAttr('tabindex');

                    if (callback != null && callback != undefined)
                        callback();
                });
            }
            else
            {
                FillListFromObject(pID, 2, "<--Select-->", "slCustomer", pData[0], function () {

                    $("#slCustomer").css({ "width": "100%" }).select2();
                    $("#slCustomer").trigger("change");
                    $("div[tabindex='-1']").removeAttr('tabindex');

                    if (callback != null && callback != undefined)
                        callback();
                });
            }
          
            FadePageCover(false);
        }
        , null);



    debugger;
    console.log('You pressed a "enter" key in textbox');
}
function GetCustomerAddress(pID, pCustodyID) {
    debugger;
    if ($("#slCustomer").val() != '') {

       // //FadePageCover(true);
        CallGETFunctionWithParameters("/api/Addresses/LoadAll"
            , {
                pWhereClause: " Where PartnerTypeID=1 And PartnerID = " + $("#slCustomer").val()
            }
            , function (pData) {

                if (JSON.parse(pData[0]).length > 0) {
                    var pFirstID = JSON.parse(pData[0])[0].ID;
                    if (pID == 0)
                        pID = pFirstID;
                }

                FillListFromObject(pID, 21, "<--Select-->", "slAddress", pData[0], null);
                if (pID != 0)
                    SetAddressInfo(true, pData, pCustodyID);

                FadePageCover(false);
            }
            , null);
    }
    else {
        $('#slAddress').html('');
    }

}
function LoadCustomerInfo(CustomerType) {
    debugger;
    if (CustomerType == 'Consignee')
    {
        if ($("#slCustomerConsigneeID").val() != '') {
            CallGETFunctionWithParameters("/api/Customers/LoadAll"
                , {
                    pWhereClause: " Where ID= " + $("#slCustomerConsigneeID").val()
                }
                , function (pData) {
                    if (JSON.parse(pData[0]).length > 0) {
                        $('#txtCustomerCode').val(JSON.parse(pData[0])[0].Code);
                        $('#txtCustomerName').val(JSON.parse(pData[0])[0].Name);
                        $('#txtPhone1Consignee').val(JSON.parse(pData[0])[0].PhonesAndFaxes);
                        $('#txtEmail').val(JSON.parse(pData[0])[0].Email);

                        Customer_Disabled(true);

                    }


                    FadePageCover(false);
                }
                , null);
        }
        else {
            Customer_Disabled(false);
            Address_Disabled(false);
            ClearAllCustomerData();
        }
    }
    else if (CustomerType == 'Shipper')
    {
        if ($("#slCustomerShipperID").val() != '') {
            //  //FadePageCover(true);
            CallGETFunctionWithParameters("/api/Customers/LoadAll"
                , {
                    pWhereClause: " Where ID= " + $("#slCustomerShipperID").val()
                }
                , function (pData) {
                    if (JSON.parse(pData[0]).length > 0) {
                        $('#txtCustomerCode').val(JSON.parse(pData[0])[0].Code);
                        $('#txtCustomerName').val(JSON.parse(pData[0])[0].Name);
                        $('#txtPhone1Shipper').val(JSON.parse(pData[0])[0].PhonesAndFaxes);
                        $('#txtEmail').val(JSON.parse(pData[0])[0].Email);

                        Customer_Disabled(true);

                    }


                    FadePageCover(false);
                }
                , null);
        }
        else {
            Customer_Disabled(false);
            Address_Disabled(false);
            ClearAllCustomerData();
        }
    }
    
}
function LoadShipperData()
{
    debugger;
    CallGETFunctionWithParameters("/api/Domestic_AWB/LoadShipperData", { pCustomerID: $('#slCustomerShipperID').val() }
          , function (pData) {
              $('#slRegionFromShipper').html($('#slRegionBase').html());
              FillShipperData(JSON.parse(pData[0]));
              FadePageCover(false);
          }
          , null);
}

function ClearAllCustomerData()
{
        $("#txtSearchCustomerName").val('');
        $("#slAddress").val(0);
        $("#txtCustomerCode").val('');
        $("#txtCustomerName").val('');
        $("#txtPhone").val('');
        $("#txtEmail").val('');
        $("#slCity").val(0);
        $("#slRegion").val(0);
        $("#slDelivery").val(0);
        $("#txtStreetName").val('');
        $("#txtBuildingNo").val('');
        $("#txtFloorNo").val('');
        $("#txtApartmentNo").val('');
}
function LoadAddressInfo() {
    if ($("#slAddress").val() != '') {
        //FadePageCover(true);
        CallGETFunctionWithParameters("/api/Addresses/LoadAll"
            , {
                pWhereClause: " Where ID = " + $("#slAddress").val()
            }
            , function (pData) {
                if (JSON.parse(pData[0]).length > 0) {
                    SetAddressInfo(true, pData,0);
                }

                FadePageCover(false);
            }
            , null);
    }
    else {
        SetAddressInfo(false, null,0);
    }
}
function SetAddressInfo(pDisabled, pData, pCustodyID) {
    debugger;
    if (pData != null) {
        $('#slCity').val(JSON.parse(pData[0])[0].CityID);
        LoadRegionByCity(JSON.parse(pData[0])[0].PortID
            , function () {
                LoadDeliveryRepresentativeByRegionID(pCustodyID)
            });

        $('#txtStreetName').val(JSON.parse(pData[0])[0].StreetLine1);
        $('#txtBuildingNo').val(JSON.parse(pData[0])[0].BuildingNo);
        $('#txtFloorNo').val(JSON.parse(pData[0])[0].FloorNo);
        $('#txtApartmentNo').val(JSON.parse(pData[0])[0].ApartmentNo);
    }

    Address_Disabled(pDisabled);

}
function Address_Disabled(pDisabled)
{
    if (pDisabled) {
        $('#slCity').attr('disabled', 'disabled');
        $('#slRegion').attr('disabled', 'disabled');
        $('#txtStreetName').attr('disabled', 'disabled');
        $('#txtBuildingNo').attr('disabled', 'disabled');
        $('#txtFloorNo').attr('disabled', 'disabled');
        $('#txtApartmentNo').attr('disabled', 'disabled');
    }
    else {
        $('#slCity').removeAttr('disabled');
        $('#slRegion').removeAttr('disabled');
        $('#txtStreetName').removeAttr('disabled');
        $('#txtBuildingNo').removeAttr('disabled');
        $('#txtFloorNo').removeAttr('disabled');
        $('#txtApartmentNo').removeAttr('disabled');
    }
}
function Customer_Disabled(pDisabled) {
    if (pDisabled) {
        $('#txtCustomerName').attr('disabled', 'disabled');
        $('#txtPhone').attr('disabled', 'disabled');
        $('#txtEmail').attr('disabled', 'disabled');
    }
    else {
        $('#txtCustomerName').removeAttr('disabled');
        $('#txtPhone').removeAttr('disabled');
        $('#txtEmail').removeAttr('disabled');
    }
}
function Customer_New()
{
    $("#slCustomer").val(0);
    $("#slCustomer").trigger("change");
    Customer_Disabled(false);

    $('#txtCustomerCode').val('');
    $('#txtCustomerName').val('');
    $('#txtPhone').val('');
    $('#txtEmail').val('');
    
}
function Address_New() {
    $("#slAddress").val(0);
    $("#slAddress").trigger("change");
    Address_Disabled(false);

    $('#slCity').val('');
    $('#slRegion').val('');
    $('#txtStreetName').val('');
    $('#txtBuildingNo').val('');
    $('#txtFloorNo').val('');
    $('#txtFloorNo').val('');
    $('#txtApartmentNo').val('');
    
}

function Order_CalculateVolume() {
    debugger;
    if ($("#txtWidth").val() != "" && $("#txtDepth").val() != "" && $("#txtHeight").val() != "") {
        $("#txtVolume").val((($("#txtWidth").val() * $("#txtDepth").val() * $("#txtHeight").val()) / 1000000).toFixed(3));
    }
    else {
        $("#txtVolume").val(0);
    }
}

function CustomerEnterEvent2(pID, callback) {
    debugger;
    var SearchText = $('#txtSearchCustomerName2').val();
 
    var SearchCondition = "Where Name like N'%" + SearchText + "%' ";

    if ($.isNumeric(SearchText))
        SearchCondition += " or Code='" + SearchText + "'";

    if (pID != 0)
        SearchCondition = " Where ID=" + pID

    //FadePageCover(true);
    CallGETFunctionWithParameters("/api/Customers/LoadAll"
        , {
            pWhereClause: SearchCondition
        }
        , function (pData) {

                FillListFromObject(pID, 2, "<--Select-->", "slCustomer2", pData[0], function () {

                    $("#slCustomer2").css({ "width": "100%" }).select2();
                    $("#slCustomer2").trigger("change");
                    $("div[tabindex='-1']").removeAttr('tabindex');

                    if (callback != null && callback != undefined)
                        callback();
                });
           
            FadePageCover(false);
        }
        , null);
    debugger;
    console.log('You pressed a "enter" key in textbox');
}
function Customer_New2() {
    $("#slCustomer2").val(0);
    $("#slCustomer2").trigger("change");
    Customer_Disabled2(false);

    $('#txtCustomerCode2').val('');
    $('#txtCustomerName2').val('');
    $('#txtPhone2').val('');
    $('#txtEmail2').val('');
}
function Customer_Disabled2(pDisabled) {
    if (pDisabled) {
        $('#txtCustomerName2').attr('disabled', 'disabled');
        $('#txtPhone2').attr('disabled', 'disabled');
        $('#txtEmail2').attr('disabled', 'disabled');
    }
    else {
        $('#txtCustomerName2').removeAttr('disabled');
        $('#txtPhone2').removeAttr('disabled');
        $('#txtEmail2').removeAttr('disabled');
    }
}
function Address_New2() {
    $("#slAddress2").val(0);
    $("#slAddress2").trigger("change");
    Address_Disabled2(false);

    $('#slCity2').val('');
    $('#slRegion2').val('');
    $('#txtStreetName2').val('');
    $('#txtBuildingNo2').val('');
    $('#txtFloorNo2').val('');
    $('#txtFloorNo2').val('');
    $('#txtApartmentNo2').val('');

}
function Address_Disabled2(pDisabled) {
    if (pDisabled) {
        $('#slCity2').attr('disabled', 'disabled');
        $('#slRegion2').attr('disabled', 'disabled');
        $('#txtStreetName2').attr('disabled', 'disabled');
        $('#txtBuildingNo2').attr('disabled', 'disabled');
        $('#txtFloorNo2').attr('disabled', 'disabled');
        $('#txtApartmentNo2').attr('disabled', 'disabled');
    }
    else {
        $('#slCity2').removeAttr('disabled');
        $('#slRegion2').removeAttr('disabled');
        $('#txtStreetName2').removeAttr('disabled');
        $('#txtBuildingNo2').removeAttr('disabled');
        $('#txtFloorNo2').removeAttr('disabled');
        $('#txtApartmentNo2').removeAttr('disabled');
    }
}
function LoadCustomerInfo2() {
    debugger;
    if ($("#slCustomer2").val() != '') {
        //FadePageCover(true);
        CallGETFunctionWithParameters("/api/Customers/LoadAll"
            , {
                pWhereClause: " Where ID= " + $("#slCustomer2").val()
            }
            , function (pData) {
                if (JSON.parse(pData[0]).length > 0) {
                    $('#txtCustomerCode2').val(JSON.parse(pData[0])[0].Code);
                    $('#txtCustomerName2').val(JSON.parse(pData[0])[0].Name);
                    $('#txtPhone2').val(JSON.parse(pData[0])[0].PhonesAndFaxes);
                    $('#txtEmail2').val(JSON.parse(pData[0])[0].Email);

                    Customer_Disabled2(true);
                }
                FadePageCover(false);
            }
            , null);
    }
    else {
        Customer_Disabled2(false);
        Address_Disabled2(false);
        ClearAllCustomerData2();
    }
}
function ClearAllCustomerData2() {
    $("#txtSearchCustomerName2").val('');
    $("#slAddress2").val(0);
    $("#txtCustomerCode2").val('');
    $("#txtCustomerName2").val('');
    $("#txtPhone2").val('');
    $("#txtEmail2").val('');
    $("#slCity2").val(0);
    $("#slRegion2").val(0);
    $("#slDelivery2").val(0);
    $("#txtStreetName2").val('');
    $("#txtBuildingNo2").val('');
    $("#txtFloorNo2").val('');
    $("#txtApartmentNo2").val('');
}
function GetCustomerAddress2(pID, pCustodyID) {
    debugger;
    if ($("#slCustomer2").val() != '') {

        //FadePageCover(true);
        CallGETFunctionWithParameters("/api/Addresses/LoadAll"
            , {
                pWhereClause: " Where PartnerTypeID=1 And PartnerID = " + $("#slCustomer2").val()
            }
            , function (pData) {

                if (JSON.parse(pData[0]).length > 0) {
                    var pFirstID = JSON.parse(pData[0])[0].ID;
                    if (pID == 0)
                        pID = pFirstID;
                }

                FillListFromObject(pID, 21, "<--Select-->", "slAddress2", pData[0], null);
                if (pID != 0)
                    SetAddressInfo2(true, pData, pCustodyID);

                FadePageCover(false);
            }
            , null);
    }
    else {
        $('#slAddress').html('');
    }

}
function SetAddressInfo2(pDisabled, pData, pCustodyID) {
    debugger;
    if (pData != null) {
        $('#slCity2').val(JSON.parse(pData[0])[0].CityID);
        LoadRegionByCity2(JSON.parse(pData[0])[0].PortID
            , function () {
                LoadDeliveryRepresentativeByRegionID2(pCustodyID)
            });

        $('#txtStreetName2').val(JSON.parse(pData[0])[0].StreetLine1);
        $('#txtBuildingNo2').val(JSON.parse(pData[0])[0].BuildingNo);
        $('#txtFloorNo2').val(JSON.parse(pData[0])[0].FloorNo);
        $('#txtApartmentNo2').val(JSON.parse(pData[0])[0].ApartmentNo);
    }

    Address_Disabled2(pDisabled);

}
function LoadRegionByCity2(pRegionID, callback) {
    debugger;
    //FadePageCover(true);
    CallGETFunctionWithParameters("/api/Ports/LoadAll"
        , {
            pWhereClause: " Where FactoryCityID= " + ($('#slCity2').val() != '' ? $('#slCity2').val() : 0)
        }
        , function (pData) {
            FillListFromObject(pRegionID, 2, "<--Select-->", "slRegion2", pData[0],
              function () {
                  if (callback != null && callback != undefined)
                      callback();
              }
                );


            FadePageCover(false);
        }
        , null);
}
function LoadDeliveryRepresentativeByRegionID2(pCustodyID) {
    debugger;
    //FadePageCover(true);
    CallGETFunctionWithParameters("/api/Custody/LoadCustodyByRegionID"
        , {
            pRegionID: ($('#slRegion2').val() != '' ? $('#slRegion2').val() : 0)
        }
        , function (pData) {

            if (JSON.parse(pData[0]).length == 1) {
                var pFirstID = JSON.parse(pData[0])[0].CustodyID;
                if (pCustodyID == 0 || pCustodyID == "undefined" || pCustodyID == undefined)
                    pCustodyID = pFirstID;
            }

            FillListFromObject(pCustodyID, 22, "<--Select-->", "slDelivery2", pData[0], null);

            FadePageCover(false);
        }
        , null);
}


function DisableEnterKeyWithSearchCompany(pInputID) {
    $("#" + pInputID).keydown(function (e) {
        //  (backspace=8) , (delete=46) , (tab=9) , (escape=), (enter=13) , (.=190)
        if (e.keyCode == 13) {
            debugger;
            e.preventDefault();
            CompanyEnterEvent(0);
        }
        else
            return;
    });
}
function CompanyEnterEvent(pID) {

    var SearchText = $('#txtSearchCompany').val();
    var SearchCondition = "Where Name like N'%" + SearchText + "%' ";

    if ($.isNumeric(SearchText))
        SearchCondition += " or Code='" + SearchText + "'";

    if (pID != 0)
        SearchCondition = " Where ID=" + pID

    //FadePageCover(true);
    CallGETFunctionWithParameters("/api/Agents/LoadAll"
        , {
            //pDate: ConvertDateFormat(pFormattedTodaysDate)
            pWhereClause: SearchCondition
        }
        , function (pData) {
            FillListFromObject(pID, 2, "<--Select-->", "slCompany", pData[0], null);
            FadePageCover(false);
        }
        , null);

    $("#slCompany").css({ "width": "100%" }).select2();
    $("#slCompany").trigger("change");
    $("div[tabindex='-1']").removeAttr('tabindex');

    debugger;
    console.log('You pressed a "enter" key in textbox');
}
function PaymentChange() {

    if ($('#slPaymentType').val() == '1') //With Cash Collection (All)
    {
        $('#txtCashDelivery').attr('data-required', true);
        $('#txtItemPrice').attr('data-required', true);

    }
    else if ($('#slPaymentType').val() == '2') //With Cash Collection (Delivery only)
    {
        $('#txtCashDelivery').attr('data-required', true);
        $('#txtItemPrice').attr('data-required', false);
    }
    else if ($('#slPaymentType').val() == '3') //Without Cash Collection 
    {
        $('#txtCashDelivery').attr('data-required', false);
        $('#txtItemPrice').attr('data-required', false);
    }
}
function DomesticAWB_Initialize() {
    $('#DomesticAWBModal input[type="text"]').on('keypress', function (e) {
        if (e.keyCode === 13) {
            e.preventDefault();
        }
    });

    strBindTableRowsFunctionName = "DomesticAWB_BindTableRows";
    strLoadWithPagingFunctionName = "/api/Domestic_AWB/LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned";

    var pWhereClause = " WHERE 1=1 ";
    var pOrderBy = "ID DESC";
    var pPageNumber = 1;
    var pPageSize = 10;
    var pControllerParameters = { pIsLoadArrayOfObjects: true, pLanguage: $("[id$='hf_ChangeLanguage']").val(), pPageNumber: pPageNumber, pPageSize: pPageSize, pWhereClause: pWhereClause, pOrderBy: pOrderBy }
    LoadView("/CourierAndLastMile/Orders/Domestic_AWB", "div-content", function () {

        LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned("div-Pager", "select-page-size", "spn-first-page-row", "spn-last-page-row", "spn-total-count", "div-Text-Total", strLoadWithPagingFunctionName, pWhereClause, pOrderBy, pPageNumber, pPageSize, pControllerParameters
            , function (pData) {
                var pCities = pData[2];
                var pRegions = pData[3];
                var PackageTypes = pData[6];
                var pRates = pData[7];
                
                FillListFromObject(null, 2, "<--Select-->", "slCityFromShipper", pCities, null);
                FillListFromObject(null, 2, "<--Select-->", "slCityToConsignee", pCities, null);
                FillListFromObject(null, 2, "<--Select-->", "slRegionFromShipper", pRegions, null);
                FillListFromObject(null, 2, "<--Select-->", "slRegionToConsignee", pRegions, null);
                
                FillListFromObject(null, 2, "<--Select-->", "slRegionBase", pRegions, null);
                FillListFromObject(null, 2, "<--Select-->", "slCityBase", pCities, null);
                FillListFromObject(null, 2, "<--Select Rate-->", "slRateBase", pRates, null);

                FillListFromObject(null, 2, "<--Select Package Type-->", "slPackageTypeID", PackageTypes, null);
                FillListFromObject(null, 2, "<--Select Rate-->", "slRatesToCustomer", pRates, null);

                $('#slCityFromShipper_New').html($('#slCityBase').html());
                $('#slRegionFromShipper_New').html($('#slRegionBase').html());

                DomesticAWB_BindTableRows(JSON.parse(pData[0]));
                var pFormattedTodaysDate = getTodaysDateInddMMyyyyFormat();

                $("#txtSearchFrom").val("01/01/2000");
                $("#txtSearchTo").val(pFormattedTodaysDate);
            });
        if ($("[id$='hf_ChangeLanguage']").val() == "ar") $(".swapChildrenClass:not(.reversed)").reverseChildren();
    },
        function () { DomesticAWB_ClearAllControls(); },
        function () { DomesticAWB_DeleteList('F'); }); //the parameter 'F':NotPermanentDelete, 'D':PermanentDelete
}

function GetRegionsInCity(From_To) {
    debugger;
    var City = (From_To == "From") ? $('#slCityFromShipper').val() : $('#slCityToConsignee').val();
    var Region_From_To = (From_To == "From") ? "slRegionFromShipper" : "slRegionToConsignee";
    CallGETFunctionWithParameters("/api/Rates/LoadAll", { pWhereClause: " Where FactoryCityID=" + City, Tables: "Region" }
                   , function (pData) {
                       FillListFromObject(0, 2, TranslateString("Select Region"), Region_From_To, pData[0], null);
                   }, null);
}

function LoadRegionByCity(pRegionID, callback) {
    debugger;
    //FadePageCover(true);
    CallGETFunctionWithParameters("/api/Ports/LoadAll"
        , {
            pWhereClause: " Where FactoryCityID= " + ($('#slCity').val() != '' ? $('#slCity').val() : 0)
        }
        , function (pData) {
            FillListFromObject(pRegionID, 2, "<--Select-->", "slRegion", pData[0],
              function () {
                  if (callback != null && callback != undefined)
                      callback();
              }
                );


            FadePageCover(false);
        }
        , null);
}
function LoadDeliveryRepresentativeByRegionID(pCustodyID) {
    debugger;
    //FadePageCover(true);
    CallGETFunctionWithParameters("/api/Custody/LoadCustodyByRegionID"
        , {
            pRegionID: ($('#slRegion').val() != '' ? $('#slRegion').val() : 0)
        }
        , function (pData) {

            if (JSON.parse(pData[0]).length == 1 ) {
                var pFirstID = JSON.parse(pData[0])[0].CustodyID;
                if (pCustodyID == 0 || pCustodyID == "undefined" ||  pCustodyID == undefined)
                    pCustodyID = pFirstID;
            }

            FillListFromObject(pCustodyID, 22, "<--Select-->", "slDelivery", pData[0], null);

            FadePageCover(false);
        }
        , null);
}
var maxDetailsIDInTable = 0; //used to for when adding new row then make td control names unique
function DomesticAWB_BindTableRows(pDomesticAWB) {
    debugger;
    var copyControlsText = " class='btn btn-xs btn-rounded btn-info float-right' > <i class='fa fa-copy' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>Deliverd To Warehouse</span>";

    ClearAllTableRows("tblDomesticAWB");
    $.each(pDomesticAWB, function (i, item) {
        AppendRowtoTable("tblDomesticAWB",//ID, AWBNumber, ShipperID, ConsigneeID, ActWgt, DimWgt, ChgWgt, Pcs, CODAmount, Description, Remarks
            ("<tr ID='" + item.ID + "' ondblclick='DomesticAWB_FillControls(" + item.ID + ");'>"
                    + "<td class='ID'> <input " + " name='Delete'" + " type='checkbox' value='" + item.ID + "' /></td>"
                    + "<td class='AWBNumber'>" + (item.AWBNumber == 0 ? "" : item.AWBNumber) + "</td>"
                    + "<td class='creationDate'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.creationDate)) < 1 ? "" : ConvertDateFormat(GetDateWithFormatMDY(item.creationDate))) + "</td>"
                    + "<td class='ShipperID' val='" + item.ShipperID + "'>" + (item.ShipperName == 0 ? "" : item.ShipperName) + "</td>"
                    + "<td class='ConsigneeID' val='" + item.ConsigneeID + "'>" + (item.ConsigneeName == 0 ? "" : item.ConsigneeName) + "</td>"
                    + "<td class='ActWgt'>" + (item.ActWgt == 0 ? "" : item.ActWgt) + "</td>"
                    + "<td class='DimWgt'>" + (item.DimWgt == 0 ? "" : item.DimWgt) + "</td>"
                    + "<td class='ChgWgt'>" + (item.ChgWgt == 0 ? "" : item.ChgWgt) + "</td>"
                    + "<td class='Pcs'>" + (item.Pcs == 0 ? "" : item.Pcs) + "</td>"
                    + "<td class='CODAmount'>" + (item.CODAmount == 0 ? "" : item.CODAmount) + "</td>"
                    + "<td class='Description'>" + (item.Description == 0 ? "" : item.Description) + "</td>"
                    + "<td class='Remarks hide'>" + (item.Remarks == 0 ? "" : item.Remarks) + "</td>"
                  

                    + "<td class='ConsigneeName hide'>" + (item.ConsigneeName == 0 ? "" : item.ConsigneeName) + "</td>"
                    + "<td class='ConsigneeCityID hide'>" + (item.ConsigneeCityID == 0 ? "" : item.ConsigneeCityID) + "</td>"
                    + "<td class='ConsigneeRegionID hide'>" + (item.ConsigneeRegionID == 0 ? "" : item.ConsigneeRegionID) + "</td>"
                    + "<td class='ConsigneePhone1 hide'>" + (item.ConsigneePhone1 == 0 ? "" : item.ConsigneePhone1) + "</td>"
                    + "<td class='ConsigneePhone2 hide'>" + (item.ConsigneePhone2 == 0 ? "" : item.ConsigneePhone2) + "</td>"
                    + "<td class='ConsigneeCompanyName hide'>" + (item.ConsigneeCompanyName == 0 ? "" : item.ConsigneeCompanyName) + "</td>"
                    + "<td class='ConsigneeSenderName hide'>" + (item.ConsigneeSenderName == 0 ? "" : item.ConsigneeSenderName) + "</td>"
                    + "<td class='ConsigneeAccountNo hide'>" + (item.ConsigneeAccountNo == 0 ? "" : item.ConsigneeAccountNo) + "</td>"
                    + "<td class='ConsigneeCity hide'>" + (item.ConsigneeCity == 0 ? "" : item.ConsigneeCity) + "</td>"
                    + "<td class='ConsigneeAddress hide'>" + (item.ConsigneeAddress == 0 ? "" : item.ConsigneeAddress) + "</td>"
                    //ShipperName, ShipperCityID, ShipperRegionID, ShipperPhone1, ShipperPhone2, ShipperCompanyName, ShipperSenderName, ShipperAccountNo, ShipperCity, ShipperAddress

                    + "<td class='ShipperName hide'>" + (item.ShipperName == 0 ? "" : item.ShipperName) + "</td>"
                    + "<td class='ShipperCityID hide'>" + (item.ShipperCityID == 0 ? "" : item.ShipperCityID) + "</td>"
                    + "<td class='ShipperRegionID hide'>" + (item.ShipperRegionID == 0 ? "" : item.ShipperRegionID) + "</td>"
                    + "<td class='ShipperPhone1 hide'>" + (item.ShipperPhone1 == 0 ? "" : item.ShipperPhone1) + "</td>"
                    + "<td class='ShipperPhone2 hide'>" + (item.ShipperPhone2 == 0 ? "" : item.ShipperPhone2) + "</td>"
                    + "<td class='ShipperCompanyName hide'>" + (item.ShipperCompanyName == 0 ? "" : item.ShipperCompanyName) + "</td>"
                    + "<td class='ShipperSenderName hide'>" + (item.ShipperSenderName == 0 ? "" : item.ShipperSenderName) + "</td>"
                    + "<td class='ShipperAccountNo hide'>" + (item.ShipperAccountNo == 0 ? "" : item.ShipperAccountNo) + "</td>"
                    + "<td class='ShipperCity hide'>" + (item.ShipperCity == 0 ? "" : item.ShipperCity) + "</td>"
                    + "<td class='ShipperAddress hide'>" + (item.ShipperAddress == 0 ? "" : item.ShipperAddress) + "</td>"

                    + "<td class='RateID hide'>" + (item.RateID == 0 ? "" : item.RateID) + "</td>"
                    + "<td class='PackageTypeID hide'>" + (item.PackageTypeID == 0 ? "" : item.PackageTypeID) + "</td>"
                    + "<td class='Quantity hide'>" + (item.Quantity == 0 ? "" : item.Quantity) + "</td>"
                    + "<td class='SellingAmount hide'>" + (item.SellingAmount == 0 ? "" : item.SellingAmount) + "</td>"
                    + "<td class='CommodityFees hide'>" + (item.CommodityFees == 0 ? "" : item.CommodityFees) + "</td>"
                    + "<td class='RateDetails hide'>" + (item.RateDetails == 0 ? "" : item.RateDetails) + "</td>"

                    + "<td class='hide'><a href='#DomesticAWBModal' data-toggle='modal' onclick='DomesticAWB_FillControls(" + item.ID + ");' " + editControlsText + "</a></td></tr>"));
    });
    ApplyPermissions();
    BindAllCheckboxonTable("tblDomesticAWB", "ID", "cb-CheckAll");//Parameters(pTableName, pCheckBoxColumnClassName, pCheckBoxHeaderID)
    CheckAllCheckbox("ID");
    //HighlightText("#tblDomesticAWB>tbody>tr", $("#txt-Search").val().trim());
    if (showDeleteFailMessage) {//sherif: if deletion failed due to foreign key, then this message is shown
        swal(strSorry, strDeleteFailMessage);
        showDeleteFailMessage = false;
        strDeleteFailMessage = "This command is not completed because of dependencies existance."
    }
    $("#hl-homepage").on("click", function () { LoadViews("hl-homepage"); });
}
function Order_DeliverdToStore(pID)
{
    //FadePageCover(true);
    CallGETFunctionWithParameters("/api/Domestic_AWB/Order_DeliverdToStore"
        , {
            //pDate: ConvertDateFormat(pFormattedTodaysDate)
            pId : pID
        }
        , function (pData) {
            pMessageReturned =pData[0] ;
            if (pMessageReturned != "") {
                swal("Sorry", pMessageReturned);
                 
            }
            else {
                DomesticAWB_LoadingWithPaging();
                swal("Success", "Deliverd To Warehouse Successfully.");
            }

            FadePageCover(false);
        }
        , null);
}
function DomesticAWB_LoadingWithPaging() {
    debugger;
    var pWhereClause = DomesticAWB_GetWhereClause();
    var pPageSize = $('#select-page-size').val();
    var pPageNumber = ($("#div-Pager li.active a").text() == "" ? 1 : $("#div-Pager li.active a").text());
    var pOrderBy = "ID DESC";
    var pControllerParameters = { pIsLoadArrayOfObjects: false, pLanguage: $("[id$='hf_ChangeLanguage']").val(), pPageNumber: pPageNumber, pPageSize: pPageSize, pWhereClause: pWhereClause, pOrderBy: pOrderBy }
    LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned("div-Pager", "select-page-size", "spn-first-page-row", "spn-last-page-row", "spn-total-count", "div-Text-Total", strLoadWithPagingFunctionName, pWhereClause, pOrderBy, pPageNumber, pPageSize, pControllerParameters,
        function (pData) {
            DomesticAWB_BindTableRows(JSON.parse(pData[0]));
        });
    //HighlightText("#tblDomesticAWB>tbody>tr", $("#txt-Search").val().trim());
}
function DomesticAWB_GetWhereClause() {
    var pWhereClause = "WHERE 1=1 ";
    //if ($("#txtFromDate").val() != '' && $("#txtToDate").val() != '')
    //    pWhereClause += " AND OpenDate >= '" + GetDateWithFormatyyyyMMdd($("#txtFromDate").val()) + "' AND OpenDate<='" + GetDateWithFormatyyyyMMdd($("#txtToDate").val()) + " 23:59:59'";
    if ($("#txt-Search").val().trim() != "")
    {
        pWhereClause += " AND (";
        pWhereClause += " AWBNumber like N'%" + $("#txt-Search").val().trim() + "%' ";
        pWhereClause += " OR ShipperName like N'%" + $("#txt-Search").val().trim() + "%' ";
        pWhereClause += " OR ConsigneeName like N'%" + $("#txt-Search").val().trim() + "%' ";
        pWhereClause += " OR ShipperAccountNo like N'%" + $("#txt-Search").val().trim() + "%' ";
        pWhereClause += " OR ConsigneeAccountNo like N'%" + $("#txt-Search").val().trim() + "%' ";
        pWhereClause += ")";
    }
    return pWhereClause;
}
function DomesticAWB_ClearAllControls(callback) {
    var pFormattedTodaysDate = getTodaysDateInddMMyyyyFormat();
    DomesticAWB_EnableDisableEditing(1); //Enable
    ClearAll("#DomesticAWBModal");
    //$("#txtUsername").val($("#hLoggedUserNameNotLogin").val());
    $("#slCustomer").trigger("change");
    $("#slCompany").trigger("change");

    $("#txtSearchCustomerName").removeAttr('disabled');
    $("#slCustomer").removeAttr('disabled');
    $("#slAddress").removeAttr('disabled');
    $("#txtCashDelivery").removeAttr('disabled');
    $("#txtItemPrice").removeAttr('disabled');
    $("#txtSearchCompany").removeAttr('disabled');
    $("#slCompany").removeAttr('disabled');

    $("#btnSave").attr("onclick", "DomesticAWB_Save();");
    //$("#btnSave").attr("onclick", "DomesticAWB_Save(false);");
    //$("#btnSaveandNew").attr("onclick", "DomesticAWB_Save(true);");
    $("#cb-CheckAll").prop('checked', false);
    jQuery("#DomesticAWBModal").modal("show");

    if (callback != null && callback != undefined)
        callback();
}
var _Action = "";
function DomesticAWB_FillControls(pID) {
    debugger;
    ClearAll("#DomesticAWBModal");

    DomesticAWB_EnableDisableEditing(2); //Enable

    jQuery("#DomesticAWBModal").modal("show");
    //$("#btnSave").attr("onclick", "DomesticAWB_Update();");
    _Action = 'UPDATE';
    var tr = $("#tblDomesticAWB tr[ID='" + pID + "']");
    $("#hID").val(pID);

    //slRegionBase slCityBase
    $('#slRegionFromShipper').html($('#slRegionBase').html());
    $('#slRegionToConsignee').html($('#slRegionBase').html());
    $('#slRatesToCustomer').html($('#slRateBase').html());
    
    

    $("#slCustomerShipperID").val($(tr).find("td.ShipperID").attr('val'));
    $("#slCustomerConsigneeID").val($(tr).find("td.ConsigneeID").attr('val'));

    $("#txtAWBNumber").val($(tr).find("td.AWBNumber").text());
    $("#creationDate").val($(tr).find("td.creationDate").text());
    $("#txtDimWgt").val($(tr).find("td.DimWgt").text());
    $("#txtActWgt").val($(tr).find("td.ActWgt").text());
    $("#txtChgWgt").val($(tr).find("td.ChgWgt").text());
    $("#txtPcs").val($(tr).find("td.Pcs").text());
    
    $("#txtCustomer_Rate_CommodityFees").val($(tr).find("td.CommodityFees").text());
    $("#txtCODAmount").val($(tr).find("td.CODAmount").text());
    $("#txtDescription").val($(tr).find("td.Description").text());
    $("#txtRemarks").val($(tr).find("td.Remarks").text());


    $("#txtSearchConsigneeName").val($(tr).find("td.ConsigneeName").text());
    $("#slCityToConsignee").val($(tr).find("td.ConsigneeCityID").text());
    $("#slRegionToConsignee").val($(tr).find("td.ConsigneeRegionID").text());
    $("#txtPhone1Consignee").val($(tr).find("td.ConsigneePhone1").text());
    $("#txtPhone2Consignee").val($(tr).find("td.ConsigneePhone2").text());
    $("#txtCompanyNameConsignee").val($(tr).find("td.ConsigneeCompanyName").text());
    $("#txtSenderNameConsignee").val($(tr).find("td.ConsigneeSenderName").text());
    $("#txtAccountNoConsignee").val($(tr).find("td.ConsigneeAccountNo").text());
    $("#txtCityConsignee").val($(tr).find("td.ConsigneeCity").text());
    $("#txtAddressConsignee").val($(tr).find("td.ConsigneeAddress").text());


    //$("#ShipperName").val($(tr).find("td.ShipperName").text());
    $("#slCityFromShipper").val($(tr).find("td.ShipperCityID").text());
    $("#slRegionFromShipper").val($(tr).find("td.ShipperRegionID").text());
    $("#txtPhone1Shipper").val($(tr).find("td.ShipperPhone1").text());
    $("#txtPhone2Shipper").val($(tr).find("td.ShipperPhone2").text());
    $("#txtCompanyNameShipper").val($(tr).find("td.ShipperCompanyName").text());
    $("#txtSenderNameShipper").val($(tr).find("td.ShipperSenderName").text());
    $("#txtAccountNoShipper").val($(tr).find("td.ShipperAccountNo").text());
    $("#txtCityShipper").val($(tr).find("td.ShipperCity").text());
    $("#txtAddressShipper").val($(tr).find("td.ShipperAddress").text());

    $("#slRatesToCustomer").val($(tr).find("td.RateID").text())
    $("#slPackageTypeID").val($(tr).find("td.PackageTypeID").text())
    $("#txtQuantity").val($(tr).find("td.Quantity").text()) 
    $("#txtCustomer_Rate_SellingAmount").val($(tr).find("td.SellingAmount").text())
    $("#txtRateDetails").val($(tr).find("td.RateDetails").text())
 
    GetShippersWithFilterByID($(tr).find("td.ShipperID").attr('val'));
    //GetConsigneeWithFilterByID($(tr).find("td.ConsigneeID").attr('val'));
    GetShipperAndConsigneeWithFilterByID(($(tr).find("td.ShipperID").attr('val')), ($(tr).find("td.ConsigneeID").attr('val')));
     
}

function GetShipperAndConsigneeWithFilterByID(ShipperID, ConsigneeID) {
    $('#txtSearchShipperName').val();
    CallGETFunctionWithParameters("/api/Domestic_AWB/LoadShipperAndConsignee"
        , { pShipperID: ShipperID, pConsigneeID: ConsigneeID }
        , function (pData) {
            FillListFromObject(CustomerID, 2, "<--Select-->", "slCustomerShipperID", pData[0], null);
            //FillListFromObject(CustomerID, 2, "<--Select-->", "slCustomerConsigneeID", pData[1], null);

        }, null);
}

function Collapse_Shipper_Consignee()
{
    $("#ShipperAndConsignee").css("overflow-y", "scroll");
}
function GetShippersWithFilterByID(CustomerID) {
    $('#txtSearchShipperName').val();
    CallGETFunctionWithParameters("/api/Customers/LoadAll"
        , { pWhereClause: " Where ID = " + CustomerID }
        , function (pData) {
            FillListFromObject(CustomerID, 2, "<--Select-->", "slCustomerShipperID", pData[0], null);

        }, null);
}

//function GetConsigneeWithFilterByID(CustomerID) {
//    $('#txtSearchConsigneeName').val();
//    CallGETFunctionWithParameters("/api/Customers/LoadAll"
//        , { pWhereClause: " Where Name like '%" + $('#txtSearchConsigneeName').val() + "%'" }
//        , function (pData) {
//            FillListFromObject(CustomerID, 2, "<--Select-->", "slCustomerConsigneeID", pData[0], null);
//        }, null);
//}

function DomesticAWB_EnableDisableEditing(pOption) { //pOption 1:Enable 2:Disable
    if (pOption == 1) {
        //$("#btnSave").removeAttr("disabled");
        //$("#btnSaveandNew").removeAttr("disabled");
    }
    else {
        //$("#btnSave").attr("disabled", "disabled");
        //$("#btnSaveandNew").attr("disabled", "disabled");
        $("#txtAWBNumber").attr("disabled", true);
    }
}
function ClearModalAWB()
{
    _Action = 'INSERT';
    $("#txtAWBNumber").attr("disabled", false);
}
function DomesticAWB_Save() {
    debugger;
    //FadePageCover(true);
    var _Suceess = true;
    //if (CheckDates() == false)
    //{
    //    swal("Sorry", "Please Check Your Dates in Package Details");
    //}
    //else
    if (ValidateForm("form", "DomesticAWBModal") && _Suceess) {
        //ConsigneeName, ConsigneeCityID, ConsigneeRegionID, ConsigneePhone1,ConsigneePhone2, ConsigneeCompanyName, ConsigneeSenderName, ConsigneeAccountNo, ConsigneeCity, ConsigneeAddress
        var pParametersWithValues = {
               pID: ($("#hID").val() == "" ? 0 : $("#hID").val())
             //, pEstimatedArrivalDateToStore: ConvertDateFormat($("#txtEstimatedArrivalDateToStore").val())
             , pAWBNumber: ($("#txtAWBNumber").val() == null || $("#txtAWBNumber").val() == '') ? 0 : $("#txtAWBNumber").val()
             , pCustomerShipperID: ($("#slCustomerShipperID").val() == null || $("#slCustomerShipperID").val() == '') ? 0 : $("#slCustomerShipperID").val()
             //, pCustomerConsigneeID: ($("#slCustomerConsigneeID").val() == null || $("#slCustomerConsigneeID").val() == '') ? 0 : $("#slCustomerConsigneeID").val()
             , pActWgt: ($("#txtActWgt").val() == null || $("#txtActWgt").val() == '') ? 0 : $("#txtActWgt").val()
             , pDimWgt: ($("#txtDimWgt").val() == null || $("#txtDimWgt").val() == '') ? 0 : $("#txtDimWgt").val()
             , pChgWgt: ($("#txtChgWgt").val() == null || $("#txtChgWgt").val() == '') ? 0 : $("#txtChgWgt").val()
             , pPcs: ($("#txtPcs").val() == null || $("#txtPcs").val() == '') ? 0 : $("#txtPcs").val()
             , pCODAmount: ($("#txtCODAmount").val() == null || $("#txtCODAmount").val() == '') ? 0 : $("#txtCODAmount").val()
             , pDescription: ($("#txtDescription").val() == null || $("#txtDescription").val() == '') ? 0 : $("#txtDescription").val()
             , pRemarks: ($("#txtRemarks").val() == null || $("#txtRemarks").val() == '') ? 0 : $("#txtRemarks").val()
            
             , pConsigneeName: ($("#txtSearchConsigneeName").val() == null || $("#txtSearchConsigneeName").val() == '') ? 0 : $("#txtSearchConsigneeName").val()
             , pConsigneeCityID: ($("#slCityToConsignee").val() == null || $("#slCityToConsignee").val() == '') ? 0 : $("#slCityToConsignee").val()
             , pConsigneeRegionID: ($("#slRegionToConsignee").val() == null || $("#slRegionToConsignee").val() == '') ? 0 : $("#slRegionToConsignee").val()
             , pConsigneePhone1: ($("#txtPhone1Consignee").val() == null || $("#txtPhone1Consignee").val() == '') ? 0 : $("#txtPhone1Consignee").val()
             , pConsigneePhone2: ($("#txtPhone2Consignee").val() == null || $("#txtPhone2Consignee").val() == '') ? 0 : $("#txtPhone2Consignee").val()
             , pConsigneeCompanyName: ($("#txtCompanyNameConsignee").val() == null || $("#txtCompanyNameConsignee").val() == '') ? 0 : $("#txtCompanyNameConsignee").val()
             , pConsigneeSenderName: ($("#txtSenderNameConsignee").val() == null || $("#txtSenderNameConsignee").val() == '') ? 0 : $("#txtSenderNameConsignee").val()
             , pConsigneeAccountNo: ($("#txtAccountNoConsignee").val() == null || $("#txtAccountNoConsignee").val() == '') ? 0 : $("#txtAccountNoConsignee").val()
             , pConsigneeCity: ($("#txtCityConsignee").val() == null || $("#txtCityConsignee").val() == '') ? 0 : $("#txtCityConsignee").val()
             , pConsigneeAddress: ($("#txtAddressConsignee").val() == null || $("#txtAddressConsignee").val() == '') ? 0 : $("#txtAddressConsignee").val()

          
             , pShipperName: ($('#slCustomerShipperID option:selected').text() == null || $('#slCustomerShipperID option:selected').text() == '') ? 0 : $('#slCustomerShipperID option:selected').text()
             , pShipperCityID: ($("#slCityFromShipper").val() == null || $("#slCityFromShipper").val() == '') ? 0 : $("#slCityFromShipper").val()
             , pShipperRegionID:  ($("#slRegionFromShipper").val() == null || $("#slRegionFromShipper").val() == '') ? 0 : $("#slRegionFromShipper").val()
             , pShipperPhone1: ($("#txtPhone1Shipper").val() == null || $("#txtPhone1Shipper").val() == '') ? 0 : $("#txtPhone1Shipper").val()
             , pShipperPhone2:  ($("#txtPhone2Shipper").val() == null || $("#txtPhone2Shipper").val() == '') ? 0 : $("#txtPhone2Shipper").val()
             , pShipperCompanyName:  ($("#txtCompanyNameShipper").val() == null || $("#txtCompanyNameShipper").val() == '') ? 0 : $("#txtCompanyNameShipper").val()
             , pShipperSenderName:  ($("#txtSenderNameShipper").val() == null || $("#txtSenderNameShipper").val() == '') ? 0 : $("#txtSenderNameShipper").val()
             , pShipperAccountNo:  ($("#txtAccountNoShipper").val() == null || $("#txtAccountNoShipper").val() == '') ? 0 : $("#txtAccountNoShipper").val()
             , pShipperCity:  ($("#txtCityShipper").val() == null || $("#txtCityShipper").val() == '') ? 0 : $("#txtCityShipper").val()
             , pShipperAddress: ($("#txtAddressShipper").val() == null || $("#txtAddressShipper").val() == '') ? 0 : $("#txtAddressShipper").val()

             , pPaymentTypeID:0
             , pStoreID:0
             , pPickupAddress:"0"
             , pEstimatedReceivedDate_Custody:("01/01/1900")
             , pActualReceivedDate_Custody:("01/01/1900")
             , pEstimatedArrivalDateToStore:("01/01/1900")
             , pActualArrivalDateToStore:("01/01/1900")
             , pEstimatedDeliveryDateFrom:("01/01/1900")
             , pEstimatedDeliveryDateTo:("01/01/1900")
             , pActualDeliveryDate:("01/01/1900")
             , pTrackingStageID: 0
           
            , pRateID: ($("#slRatesToCustomer").val() == null || $("#slRatesToCustomer").val() == '') ? 0 : $("#slRatesToCustomer").val()
            , pPackageTypeID: ($("#slPackageTypeID").val() == null || $("#slPackageTypeID").val() == '') ? 0 : $("#slPackageTypeID").val()
            , pQuantity: ($("#txtQuantity").val() == '') ? 0 : $("#txtQuantity").val()
            , pSellingAmount: ($("#txtCustomer_Rate_SellingAmount").val() == '') ? 0 : $("#txtCustomer_Rate_SellingAmount").val()
            , pRateDetails: ($("#txtRateDetails").val() == '') ? 0 : $("#txtRateDetails").val()
            , pCommodityFees: ($("#txtCustomer_Rate_CommodityFees").val() == '') ? 0 : $("#txtCustomer_Rate_CommodityFees").val()
            
        };
        
        CallPOSTFunctionWithParameters("/api/Domestic_AWB/Save"
            , pParametersWithValues
            , function (pData) {
                var pMessageReturned = pData;
                if (pData != true) {
                    swal("Sorry", pMessageReturned);
                    FadePageCover(false);
                }
                else {
                    if(_Action == 'INSERT')
                        jQuery("#DomesticAWBModal").modal("hide");
                    DomesticAWB_LoadingWithPaging();
                    swal("Success", "Saved successfully.");
                }
            }
            , null);

    }  
    else
        FadePageCover(false);
}

function DomesticAWB_Delete()
{
    //DeleteListFunction("/api/Cities/DeleteByID", { "pID": pID }, function () { Cities_LoadingWithPaging(); });
    if (GetAllSelectedIDsAsString('tblDomesticAWB') != "")
        swal({
            title: "Are you sure?",
            text: "The selected records will be deleted permanently!",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, delete!",
            closeOnConfirm: true
        },
        function () {
            DeleteListFunction("/api/Domestic_AWB/Delete", { "pDomestic_AWBIDs": GetAllSelectedIDsAsString('tblDomesticAWB') }, function () {
                DomesticAWB_LoadingWithPaging();
            });
        });
}

function CheckDates()
{
    debugger;
    if ($('#txtEstimatedArrivalDateToStore').val() != "" && $('#txtEstimatedDeliveryDateFrom').val() != "")
    {
        if ((Date.prototype.compareDates(ConvertDateFormat($("#txtEstimatedArrivalDateToStore").val().trim()), ConvertDateFormat($("#txtEstimatedDeliveryDateFrom").val().trim()))) < 0)
        {
            return false;
        }
    }
    if ($('#txtEstimatedDeliveryDateFrom').val() != "" && $('#txtEstimatedDeliveryDateTo').val() != "")
    {
        if ((Date.prototype.compareDates(ConvertDateFormat($("#txtEstimatedDeliveryDateFrom").val().trim()), ConvertDateFormat($("#txtEstimatedDeliveryDateTo").val().trim()))) < 0) {
            return false;
        }
    }
    return true;
}

function DomesticAWB_Update() {
    debugger;
    //FadePageCover(true);
    var _Suceess = true;

    if (ValidateForm("form", "DomesticAWBModal") && _Suceess) {


        var pParametersWithValues = {
            //HeaderData
            pID: ($("#hID").val() == "" ? 0 : $("#hID").val())
             , pIsTax: $('#cbTax').prop('checked') ? true : false
             , pWarehouseID: ($("#slWarehouse").val() == null || $("#slWarehouse").val() == '') ? 0 : $("#slWarehouse").val()
             , pCustodyID:  ($("#slDelivery").val() == null || $("#slDelivery").val() == '') ? 0 : $("#slDelivery").val() 

        };
        CallGETFunctionWithParameters("/api/Domestic_AWB/Update"
            , pParametersWithValues
            , function (pData) {
                var pMessageReturned = pData[0];
                if (pMessageReturned != "") {
                    swal("Sorry", pMessageReturned);
                    FadePageCover(false);
                }
                else {
                    jQuery("#DomesticAWBModal").modal("hide");
                    DomesticAWB_LoadingWithPaging();
                    swal("Success", "Saved successfully.");
                }
            }
            , null);

    }
    else
        FadePageCover(false);
}


function Consignee_New()
{
    ClearAll('#ConsigneeModal')
}
function Shipper_New()
{
    ClearAll('#ShipperModal')
}

function GetShippersWithFilter()
{
    debugger;
    $('#txtSearchShipperName').val();
    CallGETFunctionWithParameters("/api/Domestic_AWB/LoadCustomerWithFilter"
        , { pWhereClause: " Where Name like '%" + $('#txtSearchShipperName').val() + "%'" }
        , function (pData) {
            FillListFromObject(0, 2, "<--Select-->", "slCustomerShipperID", pData[0], null);
        }, null);
}

//function GetConsigneeWithFilter() {
//    $('#txtSearchConsigneeName').val();
//    CallGETFunctionWithParameters("/api/Customers/LoadAll"
//        , { pWhereClause: " Where Name like '%" + $('#txtSearchConsigneeName').val() + "%'" }
//        , function (pData) {
//            FillListFromObject(0, 2, "<--Select-->", "slCustomerConsigneeID", pData[0], null);
//        }, null);
//}

function LoadRatesToCustomer()
{
    debugger;
    CallGETFunctionWithParameters("/api/Rates/LoadRatesToCustomer"
    , { pWhereClause: " WHERE ISNULL(IsInActive,0) = 0 AND CustomerID = " + $('#slCustomerShipperID').val() + " ORDER BY ID desc" }
    , function (pData) {
        FillListFromObject(0, 23, "<--Select-->", "slRatesToCustomer", pData[0], null);
    }, null);
}

function LoadSellingAmount()
{
    debugger;
    CallGETFunctionWithParameters("/api/Domestic_AWB/LoadSellingAmount"
    , {
        pCustomerShipperID: ($('#slCustomerShipperID').val() == "" ? 0 : $('#slCustomerShipperID').val()),
        pRegionIDFromShipper: ($('#slRegionFromShipper').val() == "" ? 0 : $('#slRegionFromShipper').val()),
        pRegionIDToConsignee: ($('#slRegionToConsignee').val() == "" ? 0 : $('#slRegionToConsignee').val()),
        pRateID: ($('#slRatesToCustomer').val() == "" ? 0 : $('#slRatesToCustomer').val()),
        pPackageTypeID: ($('#slPackageTypeID').val() == "" ? 0 : $('#slPackageTypeID').val()),
        pQuantity: ($('#txtQuantity').val() == "" ? 0 : $('#txtQuantity').val())//($('#txtQuantity').val() == "" ? 1 : $('#txtQuantity').val())
        }
    , function (pData) {
        debugger;
        $('#txtCustomer_Rate_SellingAmount').val('');
        $('#txtRateDetails').val('');
        //if (JSON.parse(pData[0]).length > 0)
        //    $('#txtCustomer_Rate_SellingAmount').val(JSON.parse(pData[0])[0].Selling);
        if (JSON.parse(pData[1]).length > 0)
            $('#txtCustomer_Rate_SellingAmount').val(JSON.parse(pData[1])[0].Selling);
        if (JSON.parse(pData[1]).length > 0)
        {
            var SellingAmountDetails = "Qty: " + JSON.parse(pData[1])[0].Quantity + ' - Selling: ' + JSON.parse(pData[1])[0].Selling;
            $('#txtRateDetails').val(SellingAmountDetails);
            $('#txtRateDetails_Quantity').val(JSON.parse(pData[1])[0].Quantity);
            
            RecalculateCODAmount();
            if ($('#txtCODAmount').val() == "")
                CalculateCODamount();
        }
           
    }, null);
}
function RecalculateCODAmount()
{
    if ($('#txtRateDetails_Quantity').val() == $('#txtQuantity').val())
    {
        var CODamount = $('#txtCustomer_Rate_SellingAmount').val() 
        $('#txtCODAmount').val(CODamount)
    }
    else
    {
        var ModQuantity = $('#txtQuantity').val() % $('#txtRateDetails_Quantity').val();
        var BaseQuantity = Math.floor($('#txtQuantity').val() / $('#txtRateDetails_Quantity').val());
        var CODamount = ($('#txtCustomer_Rate_SellingAmount').val() * BaseQuantity) + (ModQuantity / $('#txtRateDetails_Quantity').val() * $('#txtCustomer_Rate_SellingAmount').val());//$('#txtCustomer_Rate_SellingAmount').val()
        $('#txtCODAmount').val(CODamount)
    }
 
}
function Shippers_Insert() {
    var pParameters = {
        pName: $('#txtSearchShipperNameNew').val(),
        pCityID: $('#slCityFromShipper_New').val(),
        pRegionID: $('#slRegionFromShipper_New').val(),
        pPhone1: $('#txtPhone1Shipper_New').val(),
        pPhone2: $('#txtPhone2Shipper_New').val(),
        pCompanyName: $('#txtCompanyNameShipper_New').val(),
        pSenderName: $('#txtSenderNameShipper_New').val(),
        pAccountNo: $('#txtAccountNoShipper_New').val(),
        pCity: $('#txtCityShipper_New').val(),
        pAddress: $('#txtAddressShipper_New').val()
    };

    debugger;
    if (ValidateForm("form", "ShipperModal")) {
        CallPOSTFunctionWithParameters("/api/Domestic_AWB/InsertCustomer"
        , pParameters
        , function (pData) {
            FillListFromObject(pData[1], 2, "<--Select-->", "slCustomerShipperID", pData[0], null);
            jQuery("#ShipperModal").modal("hide");
            $('#slRegionFromShipper').html($('#slRegionBase').html());
            FillShipperData(JSON.parse(pData[0]));
        }, null);
    }
}
function FillShipperData(Data) {
    //CompanyName SenderName CityID RegionID StreetLine1 Phone1 Phone2
    ClearAll('#ShipperBody');
    var ShipperData = Data[0];
    if ($("#slCustomerShipperID option[value='" + ShipperData.ID + "']").length > 0 && ShipperData.ID != 0)
        $('#slCustomerShipperID').val(ShipperData.ID);
    if ($("#slCityFromShipper option[value='" + ShipperData.CityID + "']").length > 0 && ShipperData.CityID != 0)
        $('#slCityFromShipper').val(ShipperData.CityID);
    if ($("#slRegionFromShipper option[value='" + ShipperData.RegionID + "']").length > 0 && ShipperData.RegionID != 0)
        $('#slRegionFromShipper').val(ShipperData.RegionID);
    $('#txtPhone1Shipper').val(ShipperData.Phone1);
    $('#txtPhone2Shipper').val(ShipperData.Phone2);
    $('#txtCompanyNameShipper').val(ShipperData.CompanyName);
    $('#txtSenderNameShipper').val(ShipperData.SenderName);
    $('#txtAccountNoShipper').val(ShipperData.AccountNo);
    
    //$('#txtAccountNoShipper').val(ShipperData.);
    //$('#txtCityShipper').val(ShipperData.);
    $('#txtAddressShipper').val(ShipperData.StreetLine1);
}
function CalculateCODamount()
{
    var ShippingFees = parseFloat($('#txtCustomer_Rate_SellingAmount').val() == "" ? 0 : $('#txtCustomer_Rate_SellingAmount').val())
    var CommodityFees = parseFloat($('#txtCustomer_Rate_CommodityFees').val() == "" ? 0 : $('#txtCustomer_Rate_CommodityFees').val())
    var CODAmount = parseFloat(ShippingFees + CommodityFees);
    $('#txtCODAmount').val(CODAmount);
}