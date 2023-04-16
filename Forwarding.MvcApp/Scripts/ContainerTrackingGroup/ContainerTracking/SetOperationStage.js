function SetOperationStage_Initialize() {
    debugger;
    LoadView("/ContainerTrackingGroup/ContainerTrackingTab/SetOperationStage", "div-content", function () {
        var _TodaysDate = getTodaysDateInddMMyyyyFormat();
        //$("#liTabName").attr("onclick", "LoadViews('Warehousing')");
        
            //CallGETFunctionWithParameters("/api/OperationsStatistics/GetOperationsStatisticsFilter", null
            //, function (data) {
            //    //data[0]:Users //data[1]:Branches //data[2]:Customers //data[3]:Currencies //data[4]:Oper.States
            //    debugger;
                
            //    FillListFromObject(null, 2, TranslateString("SelectFromMenu"), "slOperationStages", data[4], null);

            //    $("#txtFromDate").val(_TodaysDate);
            //    $("#txtToDate").val(_TodaysDate);
            //    $("#txtCloseDate").val(_TodaysDate);
            //}
            //, function () { FadePageCover(false); $("#hl-menu-SetOperationStage").parent().addClass("active"); }
        //);
        $("#txtFromDateSelectOperations").val(_TodaysDate);
        $("#txtToDateSelectOperations").val(_TodaysDate);
        $("#txtCloseDate").val("01/01/2030");
        FadePageCover(false);
    });
}

function OperationsStatistics_SelectAllOperations() {
    debugger;
    $('input[name="cbAddedItemID"]').prop("checked", true);
}

function OperationsStatistics_ClearAllOperations() {
    debugger;
    $('input[name="cbAddedItemID"]').prop("checked", false);
}

function OperationsStatistics_FilterOperationsModal() {
    debugger;
    FadePageCover(true);
    var pWhereClause = "WHERE BLType<>2 \n";
    if ($("#txtSearchItems").val().trim() != "")
        pWhereClause += "AND SUBSTRING(Code,12,10)='" + $("#txtSearchItems").val().trim() + "' \n";
    //2nd parameter in isValidDate: 1-dd/mm/yyyy 2-mm/dd/yyyy
    if (isValidDate($("#txtFromDateSelectOperations").val().trim(), 1) && $("#txtFromDateSelectOperations").val().trim() != "") {
        pWhereClause += " AND OpenDate >= '" + GetDateWithFormatyyyyMMdd($("#txtFromDateSelectOperations").val().trim()) + "'";
    }
    if (isValidDate($("#txtToDateSelectOperations").val().trim(), 1) && $("#txtToDateSelectOperations").val().trim() != "") {
        pWhereClause += " AND CAST(OpenDate AS date) <= '" + GetDateWithFormatyyyyMMdd($("#txtToDateSelectOperations").val().trim()) + "'";
    }

    GetListAsCheckboxesWithVariousParameters("/api/Operations/LoadAll"
        , { pWhereClause: pWhereClause }
        , "divCheckboxesList"
        , "cbAddedItemID"
        , function () { FadePageCover(false); }
        , 2
        , "col-sm-2");
}

function SetOperationStage() {
    debugger;
    var pCheckboxNameAttr = "cbAddedItemID";
    var pSelectedItemsIDs = GetAllSelectedIDsAsStringWithNameAttr(pCheckboxNameAttr);
    if (pSelectedItemsIDs == "")
        swal("Sorry", "Please, select at least 1 operation.");
    else
        swal({
        title: "Are you sure?",
        text: "The stage will be changed to '" + $('#slOperationStages option:selected').text() + "' for the specified period.",
        //type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes, Apply!",
        closeOnConfirm: false
    },
                //callback function in case of confirm
                function () {
                    FadePageCover(true);
                    CallGETFunctionWithParameters("/api/Operations/SetOperationStage_ByDates",
                        {
                            pOperationIDs_ToSet: pSelectedItemsIDs
                            , pOperationStageID: $('#slOperationStages').val()
                            , pCloseDate: GetDateWithFormatyyyyMMdd($('#txtCloseDate').val())/*Dummy Date not used in this case*/
                        }
                        , function (pData) {
                            let pReturnedMessage = pData[0];
                            if (pData[0] != "") {
                                swal("Sorry", pReturnedMessage);
                                FadePageCover(false);
                            }
                            else {
                                swal("Success", "Done successfully.");
                                FadePageCover(false);
                            }
                        });
                });
}