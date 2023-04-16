function ACIDDetails_SubmenuTabClicked() {
    debugger;
    //if ($("#tblACIDDetails tbody tr").length == 0) {
        ACIDDetails_LoadAll();
    //}
}
function ACIDDetails_LoadAll() {
    debugger;
    FadePageCover(true);
    CallGETFunctionWithParameters("/api/OperationsACIDDetails/LoadAll",
        {
            pWhereClause: "WHERE OperationACIDNumber IS NOT NULL AND OperationID IN (SELECT ID FROM Operations WHERE MasterOperationID=" + $("#hOperationID").val() + ") "
        }
        , function (pData) { ACIDDetails_BindTableRows(JSON.parse(pData[0])); FadePageCover(false); }
        , null);
}
function ACIDDetails_BindTableRows(pACIDDetails) {
    debugger;
    ClearAllTableRows("tblACIDDetails");
    //var printControlsText = " class='btn btn-xs btn-rounded btn-lightblue float-right' title='Print'> <i class='fa fa-print' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + TranslateString("Print") + "</span>";
    $.each(pACIDDetails, function (i, item) {
        AppendRowtoTable("tblACIDDetails",
            ("<tr ID='" + item.ID + "' " + (OEACID && $("#hIsOperationDisabled").val() == false ? "ondblclick='ACIDDetails_FillControls(" + item.ID + ',"tblACIDDetails"' + ");'>" : ">")
                + "<td class='ACIDDetailsID'> <input " + (1 == 1 ? "name='Delete'" : "disabled='disabled'") + " type='checkbox' value='" + item.ID + "' /></td>"
                + "<td class='ACIDDetailsOperationID hide' val='" + item.OperationID + "'></td>"

                + "<td class='ACIDDetailsOperationACIDNumber'>" + (item.OperationACIDNumber == "0" ? "" : item.OperationACIDNumber) + "</td>"
                + "<td class='ACIDDetailsOperationOpenedBy'>" + (item.OperationOpenedBy == "0" ? "" : item.OperationOpenedBy) + "</td>"
                + "<td class='ACIDDetailsOperationOperationManName'>" + (item.OperationOperationManName == "0" ? "" : item.OperationOperationManName) + "</td>"
                + "<td class='ACIDDetailsExpirationDate'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.ExpirationDate)) < 1 ? "" : ConvertDateFormat(GetDateWithFormatMDY(item.ExpirationDate))) + "</td>"

                + "<td class='ACIDDetailsOperationConsigneeName'>" + (item.OperationConsigneeName == "0" ? "" : item.OperationConsigneeName) + "</td>"
                + "<td class='ACIDDetailsOperationConsignee2Name'>" + (item.OperationConsignee2Name == "0" ? "" : item.OperationConsignee2Name) + "</td>"

                + "<td class='ACIDDetailsOperationMasterBL'>" + (item.OperationMasterBL == "0" ? "" : item.OperationMasterBL) + "</td>"
                + "<td class='ACIDDetailsOperationHouseNumber'>" + (item.OperationHouseNumber == "0" ? "" : item.OperationHouseNumber) + "</td>"
                + "<td class='ACIDDetailsOperationQuantity'>" + (item.OperationQuantity == "0" ? "" : item.OperationQuantity.trim()) + "</td>"

                + "<td class='ACIDDetailsReImportApproval'>" + (item.ReImportApproval == "0" ? "" : item.ReImportApproval) + "</td>"
                + "<td class='ACIDDetailsReImportApprovalNumber'>" + (item.ReImportApprovalNumber == "0" ? "" : item.ReImportApprovalNumber) + "</td>"
                + "<td class='ACIDDetailsSurveyRequest'>" + (item.SurveyRequest == "0" ? "" : item.SurveyRequest) + "</td>"
                + "<td class='ACIDDetailsSurveyDate'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.SurveyDate)) < 1 ? "" : ConvertDateFormat(GetDateWithFormatMDY(item.SurveyDate))) + "</td>"
                + "<td class='ACIDDetailsUploadCargoDate'>" + (Date.prototype.compareDates("01/01/1900", GetDateWithFormatMDY(item.UploadCargoDate)) < 1 ? "" : ConvertDateFormat(GetDateWithFormatMDY(item.UploadCargoDate))) + "</td>"
                + "<td class='ACIDDetailsBankNomination'>" + (item.BankNomination == "0" ? "" : item.BankNomination) + "</td>"
                + "<td class='ACIDDetailsTransactionMethod'>" + (item.TransactionMethod == "0" ? "" : item.TransactionMethod) + "</td>"
                + "<td class='ACIDDetailsBankNominationOpenedBy'>" + (item.BankNominationOpenedBy == "0" ? "" : item.BankNominationOpenedBy) + "</td>"
                + "<td class='ACIDDetailsCustomsCertificateNo'>" + (item.CustomsCertificateNo == "0" ? "" : item.CustomsCertificateNo) + "</td>"

                + "<td class='ACIDDetailsOperationNotes'>" + (item.OperationNotes == "0" ? "" : item.OperationNotes) + "</td>"

                + "</tr>"));
    });
    
    //ApplyPermissions();
    BindAllCheckboxonTable("tblACIDDetails", "ACIDDetailsID", "cb-CheckAll-ACIDDetails");
    CheckAllCheckbox("HeaderDeleteACIDDetailsID");

    if (showDeleteFailMessage) { //sherif: if deletion failed due to foreign key, then this message is shown
        swal(strSorry, strDeleteFailMessage, "warning");
        showDeleteFailMessage = false;
    }

}
function ACIDDetails_ClearAllControls() {
    ClearAll("#ACIDDetailsModal");
    var TodaysDateInddMMyyyyFormat = getTodaysDateInddMMyyyyFormat();
    $("#txtACIDDetailsExpirationDate").val(TodaysDateInddMMyyyyFormat);
    $("#txtACIDDetailsSurveyDate").val(TodaysDateInddMMyyyyFormat);
    $("#txtACIDDetailsUploadCargoDate").val(TodaysDateInddMMyyyyFormat);
    $("#txtACIDDetailsReImportApproval").val("");
    $("#txtACIDDetailsReImportApprovalNumber").val("");
    $("#txtACIDDetailsSurveyRequest").val("");
    $("#txtACIDDetailsBankNomination").val("");
    $("#txtACIDDetailsTransactionMethod").val("");
    $("#txtACIDDetailsBankNominationOpenedBy").val("");
    $("#txtACIDDetailsCustomsCertificateNo").val("");
    
}
function ACIDDetails_FillControls(pID, pTableName) {
    debugger;
    jQuery("#ACIDDetailsModal").modal("show");
    ClearAll("#ACIDDetailsModal");
    $("#hACIDDetailsID").val(pID);
    var tr = $("#" + pTableName + " tr[ID='" + pID + "']");

    $("#txtACIDDetailsExpirationDate").val($(tr).find("td.ACIDDetailsExpirationDate").text());
    $("#txtACIDDetailsSurveyDate").val($(tr).find("td.ACIDDetailsSurveyDate").text());
    $("#txtACIDDetailsUploadCargoDate").val($(tr).find("td.ACIDDetailsUploadCargoDate").text());

    $("#txtACIDDetailsReImportApproval").val($(tr).find("td.ACIDDetailsReImportApproval").text());
    $("#txtACIDDetailsReImportApprovalNumber").val($(tr).find("td.ACIDDetailsReImportApprovalNumber").text());
    $("#txtACIDDetailsSurveyRequest").val($(tr).find("td.ACIDDetailsSurveyRequest").text());
    $("#txtACIDDetailsBankNomination").val($(tr).find("td.ACIDDetailsBankNomination").text());
    $("#txtACIDDetailsTransactionMethod").val($(tr).find("td.ACIDDetailsTransactionMethod").text());
    $("#txtACIDDetailsBankNominationOpenedBy").val($(tr).find("td.ACIDDetailsBankNominationOpenedBy").text());
    $("#txtACIDDetailsCustomsCertificateNo").val($(tr).find("td.ACIDDetailsCustomsCertificateNo").text());

    $("#hACIDDetailsOperationID").val($(tr).find("td.ACIDDetailsOperationID").attr("val"));
    $("#txtACIDDetailsOperationCode").val($("#hOperationCode").val());
    $("#txtACIDDetailsACIDNumber").val($(tr).find("td.ACIDDetailsOperationACIDNumber").text());
    $("#txtACIDDetailsOpenedBy").val($(tr).find("td.ACIDDetailsOperationOpenedBy").text());
    $("#txtACIDDetailsOperationMan").val($(tr).find("td.ACIDDetailsOperationOperationManName").text());

    $("#txtACIDDetailsConsigneeName").val($(tr).find("td.ACIDDetailsOperationConsigneeName").text());
    $("#txtACIDDetailsConsignee2Name").val($(tr).find("td.ACIDDetailsOperationConsignee2Name").text());
    $("#txtACIDDetailsMasterBL").val($(tr).find("td.ACIDDetailsOperationMasterBL").text());
    $("#txtACIDDetailsHBL").val($(tr).find("td.ACIDDetailsOperationHouseNumber").text());
    $("#txtACIDDetailsQuantity").val($(tr).find("td.ACIDDetailsOperationQuantity").text());
    $("#txtACIDDetailsNotes").val($(tr).find("td.ACIDDetailsOperationNotes").text());

}
function ACIDDetails_Update(pSaveandAddNew) {
    debugger;
    if (ValidateForm("form", "ACIDDetailsModal")) {
        FadePageCover(true);
        var pParametersWithValues = {
            pID: $("#hACIDDetailsID").val()
            , pOperationID: $("#hACIDDetailsOperationID").val()
            , pACIDDetailsExpirationDate: $("#txtACIDDetailsExpirationDate").val() == "" ? "01/01/1900" : ConvertDateFormat($("#txtACIDDetailsExpirationDate").val())
            , pACIDDetailsSurveyDate: $("#txtACIDDetailsSurveyDate").val() == "" ? "01/01/1900" : ConvertDateFormat($("#txtACIDDetailsSurveyDate").val())
            , pACIDDetailsUploadCargoDate: $("#txtACIDDetailsUploadCargoDate").val() == "" ? "01/01/1900" : ConvertDateFormat($("#txtACIDDetailsUploadCargoDate").val())
            , pACIDDetailsReImportApproval: $("#txtACIDDetailsReImportApproval").val().trim() == "" ? "0" : $("#txtACIDDetailsReImportApproval").val().trim().toUpperCase()
            , pACIDDetailsReImportApprovalNumber: $("#txtACIDDetailsReImportApprovalNumber").val().trim() == "" ? "0" : $("#txtACIDDetailsReImportApprovalNumber").val().trim().toUpperCase()
            , pACIDDetailsSurveyRequest: $("#txtACIDDetailsSurveyRequest").val().trim() == "" ? "0" : $("#txtACIDDetailsSurveyRequest").val().trim().toUpperCase()
            , pACIDDetailsBankNomination: $("#txtACIDDetailsBankNomination").val().trim() == "" ? "0" : $("#txtACIDDetailsBankNomination").val().trim().toUpperCase()
            , pACIDDetailsTransactionMethod: $("#txtACIDDetailsTransactionMethod").val().trim() == "" ? "0" : $("#txtACIDDetailsTransactionMethod").val().trim().toUpperCase()
            , pACIDDetailsBankNominationOpenedBy: $("#txtACIDDetailsBankNominationOpenedBy").val().trim() == "" ? "0" : $("#txtACIDDetailsBankNominationOpenedBy").val().trim().toUpperCase()
            , pACIDDetailsCustomsCertificateNo: $("#txtACIDDetailsCustomsCertificateNo").val().trim() == "" ? "0" : $("#txtACIDDetailsCustomsCertificateNo").val().trim().toUpperCase()
        };
        CallGETFunctionWithParameters("/api/OperationsACIDDetails/Update", pParametersWithValues
            , function (pData) {
                debugger;
                if (pData[0]) {
                    ACIDDetails_LoadAll();
                    jQuery("#ACIDDetailsModal").modal("hide");
                }
                else
                    swal("Sorry", "Connection failed, please try again.");
                FadePageCover(false);
            }
            , null);
    }
}
function ACIDDetails_DeleteList(pTableName) {
    debugger;
    var pDeletedACIDDetailsIDs = GetAllSelectedIDsAsString(pTableName);
    if (pDeletedACIDDetailsIDs != "")
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
                var pParametersWithValues = { pDeletedACIDDetailsIDs: pDeletedACIDDetailsIDs, pOperationID: $("#hOperationID").val() };
                CallGETFunctionWithParameters("/api/OperationsACIDDetails/Delete", pParametersWithValues
                    , function (pData) {
                        if (pData[0])
                            ACIDDetails_BindTableRows(JSON.parse(pData[1]));
                        else
                            swal("Sorry", "Connection failed, please try again.");
                        FadePageCover(false);
                    }
                    , null);
            });
}
