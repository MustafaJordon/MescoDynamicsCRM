
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
function LoadCustomerInfo() {
    debugger;
    if ($("#slCustomer").val() != '') {
      //  //FadePageCover(true);
        CallGETFunctionWithParameters("/api/Customers/LoadAll"
            , {
                pWhereClause: " Where ID= " + $("#slCustomer").val()
            }
            , function (pData) {
                if (JSON.parse(pData[0]).length > 0) {
                    $('#txtCustomerCode').val(JSON.parse(pData[0])[0].Code);
                    $('#txtCustomerName').val(JSON.parse(pData[0])[0].Name);
                    $('#txtPhone').val(JSON.parse(pData[0])[0].PhonesAndFaxes);
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
function Orders_Initialize() {
    $('#OrdersModal input[type="text"]').on('keypress', function (e) {
        if (e.keyCode === 13) {
            e.preventDefault();
        }
    });

    strBindTableRowsFunctionName = "Orders_BindTableRows";
    strLoadWithPagingFunctionName = "/api/Orders/LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned";

    var pWhereClause = " WHERE 1=1 ";
    var pOrderBy = "ID DESC";
    var pPageNumber = 1;
    var pPageSize = 10;
    var pControllerParameters = { pIsLoadArrayOfObjects: true, pLanguage: $("[id$='hf_ChangeLanguage']").val(), pPageNumber: pPageNumber, pPageSize: pPageSize, pWhereClause: pWhereClause, pOrderBy: pOrderBy }
    LoadView("/CourierAndLastMile/Orders/Orders", "div-content", function () {

        LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned("div-Pager", "select-page-size", "spn-first-page-row", "spn-last-page-row", "spn-total-count", "div-Text-Total", strLoadWithPagingFunctionName, pWhereClause, pOrderBy, pPageNumber, pPageSize, pControllerParameters
            , function (pData) {
                var pPackageTypes = pData[2];
                var pCities = pData[3];
                var pStores = pData[4];
                var pWH_MainWarehouses = pData[5];

                FillListFromObject(null, 2, "<--Select-->", "slPackageType", pPackageTypes, null);
                FillListFromObject(null, 2, "<--Select-->", "slCity", pCities, null);
                FillListFromObject(null, 2, "<--Select-->", "slCity2", pCities, null);
                FillListFromObject(null, 2, "<--Select-->", "slStore", pStores, null);
                FillListFromObject(null, 2, "<--Select-->", "slMainWarehouse", pWH_MainWarehouses, null);

                Orders_BindTableRows(JSON.parse(pData[0]));
                var pFormattedTodaysDate = getTodaysDateInddMMyyyyFormat();

                $("#txtSearchFrom").val("01/01/2000");
                $("#txtSearchTo").val(pFormattedTodaysDate);
            });
        if ($("[id$='hf_ChangeLanguage']").val() == "ar") $(".swapChildrenClass:not(.reversed)").reverseChildren();
    },
        function () { Orders_ClearAllControls(); },
        function () { Orders_DeleteList('F'); }); //the parameter 'F':NotPermanentDelete, 'D':PermanentDelete
}

function LoadWarehouseByMainWarehouses(pID, callback, callback2) {
    debugger;
    //FadePageCover(true);
    CallGETFunctionWithParameters("/api/Orders/LoadWarehouse_Area_Row"
        , {
            pWhereClause: " WHERE MainWarehouseID = " + ($('#slMainWarehouse').val() != '' ? $('#slMainWarehouse').val() : 0),
            pTableName: "WH_Warehouse"
        }
        , function (pData) {
            debugger;
            if (pID != 0 && pID != "0" && pID != undefined && pID != null)
                FillListFromObject(pID, 2, "<--Select-->", "slWarehouse", pData[0], function () {
                    if (callback != null && callback != undefined)
                        callback();
                    //if (callback2 != null && callback2 != undefined)
                    //    callback2();
                });
            else
                FillListFromObject(null, 2, "<--Select-->", "slWarehouse", pData[0], null);

            FadePageCover(false);
        }
        , null);
}
function LoadAreaByWarehouse(pID, callback) {
    debugger; 
    //FadePageCover(true);
    CallGETFunctionWithParameters("/api/Orders/LoadWarehouse_Area_Row"
        , {
            pWhereClause: " WHERE WarehouseID = " + ($('#slWarehouse').val() != '' ? $('#slWarehouse').val() : 0),
            pTableName: "WH_Area"
        }
        , function (pData) {
            debugger;
            if (pID != 0 && pID != "0" && pID != undefined && pID != null)
                FillListFromObject(pID, 2, "<--Select-->", "slArea", pData[0], function () {
                    if (callback != null && callback != undefined)
                        callback();
                    //if (callback2 != null && callback2 != undefined)
                    //    callback2();
                });
            else
                FillListFromObject(null, 2, "<--Select-->", "slArea", pData[0], null);

            FadePageCover(false);
        }
        , null);
}
function LoadRowByArea(pID, callback) {
    debugger;
    //FadePageCover(true);
    CallGETFunctionWithParameters("/api/Orders/LoadWarehouse_Area_Row"
        , {
            pWhereClause: " WHERE AreaID = " + ($('#slArea').val() != '' ? $('#slArea').val() : 0),
            pTableName: "WH_Row"
        }
        , function (pData) {
            debugger;
            if (pID != 0 && pID != "0" && pID != undefined && pID != null)
                FillListFromObject(pID, 2, "<--Select-->", "slRow", pData[0], function () {
                    if (callback != null && callback != undefined)
                        callback();
                });
            else
                FillListFromObject(null, 2, "<--Select-->", "slRow", pData[0], null);
            FadePageCover(false);
        }
        , null);
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
function Orders_BindTableRows(pOrders) {
    debugger;
    var copyControlsText = " class='btn btn-xs btn-rounded btn-info float-right' > <i class='fa fa-copy' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>Deliverd To Warehouse</span>";

    ClearAllTableRows("tblOrders");
    $.each(pOrders, function (i, item) {
        AppendRowtoTable("tblOrders",
            ("<tr ID='" + item.ID + "' ondblclick='Orders_FillControls(" + item.ID + ");'>"
                    + "<td class='ID'> <input " + " name='Delete'" + " type='checkbox' value='" + item.ID + "' /></td>"
                    + "<td class='OrderNo'>" + (item.CodeSerial == 0 ? "" : item.CodeSerial) + "</td>"
                    + "<td class='OrderDate'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.OpenDate)) < 1 ? "" : ConvertDateFormat(GetDateWithFormatMDY(item.OpenDate))) + "</td>"
                    + "<td class='OrderType '>" + (item.IsCourrier == 0 ? "LastMile" : "Courrier") + "</td>"
                    + "<td class='CustomerName'>" + (item.CustomerName == 0 ? "" : item.CustomerName) + "</td>"
                    + "<td class='CityName'>" + (item.CityName == 0 ? "" : item.CityName) + "</td>"
                    + "<td class='RegionName'>" + (item.RegionName == 0 ? "" : item.RegionName) + "</td>"
                    + "<td class='TrackingStageName'>" + (item.TrackingStageName == 0 ? "" : item.TrackingStageName) + "</td>"

                    + "<td class='IsCourrier hide'>" + (item.IsCourrier == 0 ? "" : item.IsCourrier) + "</td>"
                    + "<td class='IsLastMile hide'>" + (item.IsLastMile == 0 ? "" : item.IsLastMile) + "</td>"
                    + "<td class='IsTax hide'>" + (item.IsTax == 0 ? "" : item.IsTax) + "</td>"

                     
                    + "<td class='CashDelivery hide'>" + (item.CashDelivery == 0 ? "" : item.CashDelivery) + "</td>"
                    + "<td class='ItemPrice hide'>" + (item.ItemPrice == 0 ? "" : item.ItemPrice) + "</td>"
                    + "<td class='PortID hide'>" + (item.PortID == 0 ? "" : item.PortID) + "</td>"
                    + "<td class='CityID hide'>" + (item.CityID == 0 ? "" : item.CityID) + "</td>"
                    + "<td class='ConsigneeID hide'>" + (item.ConsigneeID == 0 ? "" : item.ConsigneeID) + "</td>"
                    + "<td class='ConsigneeAddressID hide'>" + (item.ConsigneeAddressID == 0 ? "" : item.ConsigneeAddressID) + "</td>"
                    + "<td class='ShipperID hide'>" + (item.ShipperID == 0 ? "" : item.ShipperID) + "</td>"
                    + "<td class='ShipperAddressID hide'>" + (item.ShipperAddressID == 0 ? "" : item.ShipperAddressID) + "</td>"

                    + "<td class='PaymentTypeID hide'>" + (item.PaymentTypeID == 0 ? "" : item.PaymentTypeID) + "</td>"
                    + "<td class='PackageTypeID hide'>" + (item.PackageTypeID == 0 ? "" : item.PackageTypeID) + "</td>"
                    + "<td class='NumberOfPackages hide'>" + (item.NumberOfPackages == 0 ? "" : item.NumberOfPackages) + "</td>"
                    + "<td class='PackageDescription hide'>" + (item.PackageDescription == 0 ? "" : item.PackageDescription) + "</td>"
                    + "<td class='StoreID hide'>" + (item.StoreID == 0 ? "" : item.StoreID) + "</td>"
                    + "<td class='AgentID hide'>" + (item.AgentID == 0 ? "" : item.AgentID) + "</td>"
                    + "<td class='PickupAddress hide'>" + (item.PickupAddress == 0 ? "" : item.PickupAddress) + "</td>"
                    + "<td class='OrderReference hide'>" + (item.OrderReference == 0 ? "" : item.OrderReference) + "</td>"
                    + "<td class='CustodyID hide'>" + (item.CustodyID == 0 ? "" : item.CustodyID) + "</td>"

                    + "<td class='EstimatedArrivalDateToStore hide'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.EstimatedArrivalDateToStore)) < 1 ? "" : ConvertDateFormat(GetDateWithFormatMDY(item.EstimatedArrivalDateToStore))) + "</td>"
                    + "<td class='ActualArrivalDateToStore hide'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.ActualArrivalDateToStore)) < 1 ? "" : ConvertDateFormat(GetDateWithFormatMDY(item.ActualArrivalDateToStore))) + "</td>"
                    + "<td class='ActualReceivedDate_Custody hide'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.ActualReceivedDate_Custody)) < 1 ? "" : ConvertDateFormat(GetDateWithFormatMDY(item.ActualReceivedDate_Custody))) + "</td>"
                    + "<td class='EstimatedDeliveryDateFrom hide'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.EstimatedDeliveryDateFrom)) < 1 ? "" : ConvertDateFormat(GetDateWithFormatMDY(item.EstimatedDeliveryDateFrom))) + "</td>"
                    + "<td class='EstimatedDeliveryDateTo hide'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.EstimatedDeliveryDateTo)) < 1 ? "" : ConvertDateFormat(GetDateWithFormatMDY(item.EstimatedDeliveryDateTo))) + "</td>"
                    + "<td class='ActualDeliveryDate hide'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.ActualDeliveryDate)) < 1 ? "" : ConvertDateFormat(GetDateWithFormatMDY(item.ActualDeliveryDate))) + "</td>"

                    + "<td class='CustomerName2 hide'>" + (item.CustomerName2 == 0 ? "" : item.CustomerName2) + "</td>"
                    + "<td class='PhonesAndFaxes2 hide'>" + (item.PhonesAndFaxes2 == 0 ? "" : item.PhonesAndFaxes2) + "</td>"
                    + "<td class='Email2 hide'>" + (item.Email2 == 0 ? "" : item.Email2) + "</td>"
                    + "<td class='CustodyEmail hide'>" + (item.CustodyEmail == 0 ? "" : item.CustodyEmail) + "</td>"
                    + "<td class='MainWarehouseID hide'>" + (item.MainWarehouseID == 0 ? "" : item.MainWarehouseID) + "</td>"
                    + "<td class='WarehouseID hide'>" + (item.WarehouseID ) + "</td>"
                    + "<td class='AreaID hide'>" + (item.AreaID ) + "</td>"
                    + "<td class='RowID hide'>" + (item.RowID ) + "</td>"
                    + "<td class='Width_CM hide'>" + (item.Width_CM == 0 ? "" : item.Width_CM) + "</td>"
                    + "<td class='Depth_CM hide'>" + (item.Depth_CM == 0 ? "" : item.Depth_CM) + "</td>"
                    + "<td class='Height_CM hide'>" + (item.Height_CM == 0 ? "" : item.Height_CM) + "</td>"
                    + "<td class='Vol_CM hide'>" + (item.Vol_CM == 0 ? "" : item.Vol_CM) + "</td>"
                    + "<td class='GrossWeight hide'>" + (item.GrossWeight == 0 ? "" : item.GrossWeight) + "</td>"
                    + "<td class='NetWeight hide'>" + (item.NetWeight == 0 ? "" : item.NetWeight) + "</td>"
                    
                    //+ "<td class='Username hide'>" + (item.Username == 0 ? "" : item.Username) + "</td>"
                    + "<td class='DeliverdToStore " + (item.IsCourrier ? 'hide':'' )+ "" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.ActualArrivalDateToStore)) < 1 ? "" : " hide ") + "'><a data-toggle='modal' onclick='Order_DeliverdToStore(" + item.ID + ");' " + copyControlsText + "</a></td>"
                    + "<td class='DeliverdToStore2 " + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.ActualArrivalDateToStore)) < 1 ? "hide" : "  ") + "'></td>"
                    + "<td class='hide'><a href='#OrdersModal' data-toggle='modal' onclick='Orders_FillControls(" + item.ID + ");' " + editControlsText + "</a></td></tr>"));
    });
    ApplyPermissions();
    //if ($("#hf_CanAdd").val() == 1) $(".JournalVoucherCopy").removeClass("hide"); else $(".JournalVoucherCopy").addClass("hide");
    BindAllCheckboxonTable("tblOrders", "ID", "cb-CheckAll");//Parameters(pTableName, pCheckBoxColumnClassName, pCheckBoxHeaderID)
    CheckAllCheckbox("ID");
    //HighlightText("#tblOrders>tbody>tr", $("#txt-Search").val().trim());
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
    CallGETFunctionWithParameters("/api/Orders/Order_DeliverdToStore"
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
                Orders_LoadingWithPaging();
                swal("Success", "Deliverd To Warehouse Successfully.");
            }

            FadePageCover(false);
        }
        , null);
}
function Orders_LoadingWithPaging() {
    debugger;
    var pWhereClause = Orders_GetWhereClause();
    var pPageSize = $('#select-page-size').val();
    var pPageNumber = ($("#div-Pager li.active a").text() == "" ? 1 : $("#div-Pager li.active a").text());
    var pOrderBy = "ID DESC";
    var pControllerParameters = { pIsLoadArrayOfObjects: false, pLanguage: $("[id$='hf_ChangeLanguage']").val(), pPageNumber: pPageNumber, pPageSize: pPageSize, pWhereClause: pWhereClause, pOrderBy: pOrderBy }
    LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned("div-Pager", "select-page-size", "spn-first-page-row", "spn-last-page-row", "spn-total-count", "div-Text-Total", strLoadWithPagingFunctionName, pWhereClause, pOrderBy, pPageNumber, pPageSize, pControllerParameters, function (pData) { Orders_BindTableRows(JSON.parse(pData[0])); });
    //HighlightText("#tblOrders>tbody>tr", $("#txt-Search").val().trim());
}
function Orders_GetWhereClause() {
    var pWhereClause = "WHERE 1=1 " + "\n"
    if ($("#txtFromDate").val() != '' && $("#txtToDate").val() != '')
        pWhereClause += " AND OpenDate >= '" + GetDateWithFormatyyyyMMdd($("#txtFromDate").val()) + "' AND OpenDate<='" + GetDateWithFormatyyyyMMdd($("#txtToDate").val()) + " 23:59:59'";
    if ($("#txtSearchOrderNo").val().trim() != "")
        pWhereClause += " AND CodeSerial = '" + $("#txtSearchOrderNo").val().trim() + "'" + "\n";
    if ($("#txtSearchCustomer").val().trim() != "")
        pWhereClause += " AND CustomerName like '%" + $("#txtSearchCustomer").val().trim() + "%'" + "\n";
    if ($("#txtSearchCity").val().trim() != "")
        pWhereClause += " AND CityName like '%" + $("#txtSearchCity").val().trim() + "%'" + "\n";
    if ($("#txtSearchRegion").val().trim() != "")
        pWhereClause += " AND RegionName like '%" + $("#txtSearchRegion").val().trim() + "%'" + "\n";
    if ($("#txtSearchCompanyName").val().trim() != "")
        pWhereClause += " AND CompanyName like '%" + $("#txtSearchCompanyName").val().trim() + "%'" + "\n";
    if ($("#txtSearchWarehouseName").val().trim() != "")
        pWhereClause += " AND StoreName like '%" + $("#txtSearchWarehouseName").val().trim() + "%'" + "\n";

    
    if ($("#slOrderType").val() == 1 || $("#slOrderType").val() == "1")
        pWhereClause += " AND IsLastMile = 1";
    else if ($("#slOrderType").val() == 2 || $("#slOrderType").val() == "2")
        pWhereClause += " AND IsCourrier = 1";
    //    pWhereClause += " AND TotalCredit = " + $("#txtSearchValue").val().trim() + "\n";
    //if ($("#txtSearchRemarksHeader").val().trim() != "")
    //    pWhereClause += " AND RemarksHeader LIKE N'%" + $("#txtSearchRemarksHeader").val().trim() + "%'" + "\n";
    //if ($("#slSearchStatus").val() == 10)
    //    pWhereClause += " AND Posted = 1" + "\n";
    //if ($("#slSearchStatus").val() == 20)
    //    pWhereClause += " AND Posted = 0" + "\n";
    return pWhereClause;
}
function Orders_ClearAllControls(callback) {
    var pFormattedTodaysDate = getTodaysDateInddMMyyyyFormat();
    Orders_EnableDisableEditing(1); //Enable
    ClearAll("#OrdersModal");
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

    $("#btnSave").attr("onclick", "Orders_Save();");
    //$("#btnSave").attr("onclick", "Orders_Save(false);");
    //$("#btnSaveandNew").attr("onclick", "Orders_Save(true);");
    $("#cb-CheckAll").prop('checked', false);
    jQuery("#OrdersModal").modal("show");

    if (callback != null && callback != undefined)
        callback();

}
function Orders_FillControls(pID) {

    ClearAll("#OrdersModal");

    Orders_EnableDisableEditing(1); //Enable
    

    $("#btnSave").attr("onclick", "Orders_Update();");
    $("#txtSearchCustomerName").attr('disabled', 'disabled');
    $("#slCustomer").attr('disabled', 'disabled');
    $("#slAddress").attr('disabled', 'disabled');
    $("#txtCashDelivery").attr('disabled', 'disabled');
    $("#txtItemPrice").attr('disabled', 'disabled');
    $("#txtSearchCompany").attr('disabled', 'disabled');
    $("#slCompany").attr('disabled', 'disabled');
    //$("#tblDetails tbody").html("");
    ////FadePageCover(true);
    var tr = $("#tblOrders tr[ID='" + pID + "']");
    $("#hID").val(pID);

    $("#lblShown").html("<span> : </span><span> " + $(tr).find("td.OrderNo").text() + "</span>");

    if ($(tr).find("td.IsLastMile").text() == "true")
        $("#cbLastMile").prop("checked", true);

    if ($(tr).find("td.IsCourrier").text() == "true")
        $("#cbCourier").prop("checked", true);

    debugger;
    if ($(tr).find("td.IsLastMile").text() == "true")
    {
        ActivateLastMile();
        CustomerEnterEvent($(tr).find("td.ConsigneeID").text(), function () {
            LoadCustomerInfo();
            GetCustomerAddress($(tr).find("td.ConsigneeAddressID").text(), $(tr).find("td.CustodyID").text());
            });
    }
    else if ($(tr).find("td.IsCourrier").text() == "true")
    {
        ActivateCourier();
        CustomerEnterEvent($(tr).find("td.ShipperID").text(), function () {
            LoadCustomerInfo();
            GetCustomerAddress($(tr).find("td.ShipperAddressID").text(), $(tr).find("td.CustodyID").text());
        });

        CustomerEnterEvent2($(tr).find("td.ConsigneeID").text(), function () {
            LoadCustomerInfo2();
            GetCustomerAddress2($(tr).find("td.ConsigneeAddressID").text(), $(tr).find("td.CustodyID").text());
        });
    }
 

    $("#slCity").val($(tr).find("td.CityID").text());
    $("#slRegion").val($(tr).find("td.PortID").text());



    $("#cbTax").prop("checked", $(tr).find("td.IsTax").text());
    $("#slPaymentType").val($(tr).find("td.PaymentTypeID").text());
    $("#txtCashDelivery").val($(tr).find("td.CashDelivery").text());
    $("#txtItemPrice").val($(tr).find("td.ItemPrice").text());


    $("#slStore").val($(tr).find("td.StoreID").text());
    CompanyEnterEvent($(tr).find("td.AgentID").text());
    $("#txtPickupAddress").val($(tr).find("td.PickupAddress").text());
    $("#txtOrderReference").val($(tr).find("td.OrderReference").text());
   
    $("#slPackageType").val($(tr).find("td.PackageTypeID").text());
    $("#txtNumberOfItems").val($(tr).find("td.NumberOfPackages").text());
    $("#txtPackageDescription").val($(tr).find("td.PackageDescription").text());

    $("#txtEstimatedArrivalDateToStore").val($(tr).find("td.EstimatedArrivalDateToStore").text());
    $("#txtEstimatedDeliveryDateFrom").val($(tr).find("td.EstimatedDeliveryDateFrom").text());
    $("#txtEstimatedDeliveryDateTo").val($(tr).find("td.EstimatedDeliveryDateTo").text());

    $("#slMainWarehouse").val($(tr).find("td.MainWarehouseID").text());
    //$("#slWarehouse").val($(tr).find("td.WarehouseID").text());
    //$("#slArea").val($(tr).find("td.AreaID").text());
    //$("#slRow").val($(tr).find("td.RowID").text());
    debugger;
    LoadWarehouseByMainWarehouses($(tr).find("td.WarehouseID").text(), function () {
        LoadAreaByWarehouse($(tr).find("td.AreaID").text(), function () {
            LoadRowByArea($(tr).find("td.RowID").text());
        });
      
    });
    //LoadAreaByWarehouse($(tr).find("td.WarehouseID").text());
    //LoadRowByArea($(tr).find("td.AreaID").text());

    $("#txtGrossWeight").val($(tr).find("td.GrossWeight").text());
    $("#txtNetWeight").val($(tr).find("td.NetWeight").text());
    $("#txtWidth").val($(tr).find("td.Width_CM").text());
    $("#txtDepth").val($(tr).find("td.Depth_CM").text());
    $("#txtHeight").val($(tr).find("td.Height_CM").text());
    $("#txtVolume").val($(tr).find("td.Vol_CM").text());

    jQuery("#OrdersModal").modal("show");

    PaymentChange();

   // FadePageCover(false);

}

function Orders_EnableDisableEditing(pOption) { //pOption 1:Enable 2:Disable
    if (pOption == 1) {
        $("#btnSave").removeAttr("disabled");
        $("#btnSaveandNew").removeAttr("disabled");
        $("#btn-AddDetails").removeAttr("disabled");
        $("#btn-DeleteDetails").removeAttr("disabled");
    }
    else {
        $("#btnSave").attr("disabled", "disabled");
        $("#btnSaveandNew").attr("disabled", "disabled");
        $("#btn-AddDetails").attr("disabled", "disabled");
        $("#btn-DeleteDetails").attr("disabled", "disabled");
    }
}

function Orders_Save() {
    debugger;
    //FadePageCover(true);
    var _Suceess = true;
    if (CheckDates() == false)
    {
        swal("Sorry", "Please Check Your Dates in Package Details");
    }
    else
    if (ValidateForm("form", "OrdersModal") && _Suceess) {


        var pParametersWithValues = {
            //HeaderData
            pID: ($("#hID").val() == "" ? 0 : $("#hID").val())
            , pBranchID: pDefaults.BranchID
            , pCompanyAddressID: 0
            , pPickupCityID: 0
            , pPickupAddressID: 0
            , pNotes: ''
           , pIsLastMile: $('#cbLastMile').prop('checked') ? true : false

           , pIsCourrier: $('#cbCourier').prop('checked') ? true : false

            , pCustomerID: ($("#slCustomer").val() == null || $("#slCustomer").val() == '') ? 0 : $("#slCustomer").val()
            , pCustomerAddressID: ($("#slAddress").val() == null || $("#slAddress").val() == '') ? 0 : $("#slAddress").val()
            , pCustomerName: $("#txtCustomerName").val()
            , pPhone: $("#txtPhone").val()
            , pEmail: $("#txtEmail").val()
            , pDeliveryCityID: $("#slCity").val()
            , pRegionID: $("#slRegion").val()
            , pDeliveryStreetName: $("#txtStreetName").val()
            , pBuildingNo: $("#txtBuildingNo").val() == null ? '' : $("#txtBuildingNo").val()
            , pFloorNo: $("#txtFloorNo").val() == null ? '' : $("#txtFloorNo").val()
            , pApartmentNo: $("#txtApartmentNo").val() == null ? '' : $("#txtApartmentNo").val()

            , pCustomerID_2: ($("#slCustomer2").val() == null || $("#slCustomer2").val() == '') ? 0 : $("#slCustomer2").val()
            , pCustomerAddressID_2: ($("#slAddress2").val() == null || $("#slAddress2").val() == '') ? 0 : $("#slAddress2").val()
            , pCustomerName_2: $("#txtCustomerName2").val()
            , pPhone_2: $("#txtPhone2").val()
            , pEmail_2: $("#txtEmail2").val()
            , pDeliveryCityID_2: $("#slCity2").val()
            , pRegionID_2: $("#slRegion2").val()
            , pDeliveryStreetName_2: $("#txtStreetName2").val()
            , pBuildingNo_2: $("#txtBuildingNo2").val() == null ? '' : $("#txtBuildingNo2").val()
            , pFloorNo_2: $("#txtFloorNo2").val() == null ? '' : $("#txtFloorNo2").val()
            , pApartmentNo_2: $("#txtApartmentNo2").val() == null ? '' : $("#txtApartmentNo2").val()


             , pPaymentTypeID: $("#slMainWarehouse").val()
             , pPaymentTypeID: $("#slWarehouse").val()
             , pPaymentTypeID: $("#slArea").val()
             , pPaymentTypeID: $("#slRow").val()
             , pPaymentTypeID: $("#txtApartmentNo2").val() == null ? '' : $("#txtApartmentNo2").val()
             , pPaymentTypeID: $("#txtApartmentNo2").val() == null ? '' : $("#txtApartmentNo2").val()
             , pPaymentTypeID: $("#txtApartmentNo2").val() == null ? '' : $("#txtApartmentNo2").val()
             , pPaymentTypeID: $("#txtApartmentNo2").val() == null ? '' : $("#txtApartmentNo2").val()

             , pPaymentTypeID: $("#slPaymentType").val()
             , pCashDelivery: ($("#txtCashDelivery").val() == null || $("#txtCashDelivery").val() == '') ? 0 : $("#txtCashDelivery").val()
             , pItemPrice: ($("#txtItemPrice").val() == null || $("#txtItemPrice").val() == '') ? 0 : $("#txtItemPrice").val()
             , pIsTax: $('#cbTax').prop('checked') ? true : false

             , pPackageTypeID: $("#slPackageType").val()
             , pNumberOfPackages: $("#txtNumberOfItems").val()
             , pPackageDescription: $("#txtPackageDescription").val()
            
             , pStoreID: ($("#slStore").val() == null || $("#slStore").val() == '') ? 0 : $("#slStore").val()
             , pCompanyID: ($("#slCompany").val() == null || $("#slCompany").val() == '') ? 0 : $("#slCompany").val()
             , pPickupAddress: $("#txtPickupAddress").val()
             , pOrderReference: ( $("#txtOrderReference").val() == null  ? '' : $("#txtOrderReference").val() ) 
             , pCashDelivery: ($("#txtCashDelivery").val() == null || $("#txtCashDelivery").val() == '') ? 0 : $("#txtCashDelivery").val()

             , pCustodyID: $("#slDelivery").val()

             , pEstimatedArrivalDateToStore: ConvertDateFormat($("#txtEstimatedArrivalDateToStore").val())
             , pEstimatedDeliveryDateFrom: ConvertDateFormat($("#txtEstimatedDeliveryDateFrom").val())
             , pEstimatedDeliveryDateTo: ConvertDateFormat($("#txtEstimatedDeliveryDateTo").val())


             , pMainWarehouseID: ($("#slMainWarehouse").val() == null || $("#slMainWarehouse").val() == '') ? 0 : $("#slMainWarehouse").val()
             , pWarehouseID: ($("#slWarehouse").val() == null || $("#slWarehouse").val() == '') ? 0 : $("#slWarehouse").val()
             , pAreaID: ($("#slArea").val() == null || $("#slArea").val() == '') ? 0 : $("#slArea").val()
             , pRowID: ($("#slRow").val() == null || $("#slRow").val() == '') ? 0 : $("#slRow").val()
                
             , pGrossWeight: ($("#txtGrossWeight").val() == null || $("#txtGrossWeight").val() == '') ? 0 : $("#txtGrossWeight").val()
             , pNetWeight: ($("#txtNetWeight").val() == null || $("#txtNetWeight").val() == '') ? 0 : $("#txtNetWeight").val()
             , pWidth_CM: ($("#txtWidth").val() == null || $("#txtWidth").val() == '') ? 0 : $("#txtWidth").val()
             , pDepth_CM: ($("#txtDepth").val() == null || $("#txtDepth").val() == '') ? 0 : $("#txtDepth").val()
             , pHeight_CM: ($("#txtHeight").val() == null || $("#txtHeight").val() == '') ? 0 : $("#txtHeight").val()
             , pVol_CM: ($("#txtVolume").val() == null || $("#txtVolume").val() == '') ? 0 : $("#txtVolume").val()
          
        };
        
        CallPOSTFunctionWithParameters("/api/Orders/Save"
            , pParametersWithValues
            , function (pData) {
                var pMessageReturned = pData[0];
                if (pMessageReturned != "") {
                    swal("Sorry", pMessageReturned);
                    FadePageCover(false);
                }
                else {
                    jQuery("#OrdersModal").modal("hide");
                    Orders_LoadingWithPaging();
                    swal("Success", "Saved successfully.");
                }
            }
            , null);

    }  
    else
        FadePageCover(false);
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

function Orders_Update() {
    debugger;
    //FadePageCover(true);


    var _Suceess = true;

    if (ValidateForm("form", "OrdersModal") && _Suceess) {


        var pParametersWithValues = {
            //HeaderData
            pID: ($("#hID").val() == "" ? 0 : $("#hID").val())
             , pIsTax: $('#cbTax').prop('checked') ? true : false
             , pWarehouseID: ($("#slWarehouse").val() == null || $("#slWarehouse").val() == '') ? 0 : $("#slWarehouse").val()
             , pCustodyID:  ($("#slDelivery").val() == null || $("#slDelivery").val() == '') ? 0 : $("#slDelivery").val() 

        };
        CallGETFunctionWithParameters("/api/Orders/Update"
            , pParametersWithValues
            , function (pData) {
                var pMessageReturned = pData[0];
                if (pMessageReturned != "") {
                    swal("Sorry", pMessageReturned);
                    FadePageCover(false);
                }
                else {
                    jQuery("#OrdersModal").modal("hide");
                    Orders_LoadingWithPaging();
                    swal("Success", "Saved successfully.");
                }
            }
            , null);

    }
    else
        FadePageCover(false);
}

function ActivateLastMile()
{
    debugger;
    $('.LastMile').removeClass('hide');
    $('.Courier').addClass('hide');
    $('#txtItemPrice').attr('data-required', true);
    $('#slCompany').attr('data-required', true);
    $('#txtPickupAddress').attr('data-required', true);


    $('#txtCustomerName2').attr('data-required', false);
    $('#txtPhone2').attr('data-required', false);
    $('#slCity2').attr('data-required', false);
    $('#slRegion2').attr('data-required', false);
    $('#txtStreetName2').attr('data-required', false);
    
}

function ActivateCourier()
{
    debugger;
    $('.LastMile').addClass('hide');
    $('.Courier').removeClass('hide');
    $('#txtItemPrice').attr('data-required', false);
    $('#slCompany').attr('data-required', false);
    $('#txtPickupAddress').attr('data-required', false);

    $('#txtCustomerName2').attr('data-required', true);
    $('#txtPhone2').attr('data-required', true);
    $('#slCity2').attr('data-required', true);
    $('#slRegion2').attr('data-required', true);
    $('#txtStreetName2').attr('data-required', true);
}











