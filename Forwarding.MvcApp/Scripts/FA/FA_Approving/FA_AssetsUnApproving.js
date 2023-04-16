﻿// City Country ---------------------------------------------------------------
// Bind FA_AssetsUnApproving Table Rows



function FA_AssetsUnApproving_BindTableRows(pFA_AssetsUnApproving) {
    debugger;
    $("#hl-menu-FA").parent().addClass("active");
    ClearAllTableRows("tblFA_AssetsUnApproving");
    $.each(pFA_AssetsUnApproving, function (i, item) {
        debugger;
        //editControlsText = " class='btn btn-xs btn-rounded btn-info float-right' > <i class='fa fa-pencil' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + TranslateString("Edit") + "</span>";
        AppendRowtoTable("tblFA_AssetsUnApproving",
            ("<tr ID='" + item.ID + "' ondblclick='FA_AssetsUnApproving_EditByDblClick(" + item.ID + ");'>"
                + "<td class='ID'> <input name='Delete' type='checkbox' value='" + item.ID + "' /></td>"
                + "<td class='Name' val='" + item.Name + "'>" + item.Name + "</td>"
                + "<td class='Code' val='" + item.Code + "'>" + item.Code + "</td>"
                + "<td class='BarCode' val='" + item.BarCode + "'>" + item.BarCode + "</td>"
                + "<td class='BranchID' val='" + item.BranchID + "'>" + item.BranchName + "</td>"
                + "<td class='DevisonID' val='" + item.DevisonID + "'>" + item.DevisonName + "</td>"
                + "<td class='DepartmentID' val='" + item.DepartmentID + "'>" + item.DepartmentName + "</td>"
                + "<td class='GroupID' val='" + item.GroupID + "'>" + item.GroupName + "</td>"
                + "<td class='Qty hide' val='" + item.Qty + "'>" + item.Qty + "</td>"
                + "<td class='Approved' val='" + item.Approved + "'>" + (item.Approved == true ? "Approved" : "") + "</td>"
                + "<td class='SubAccountID hide' val='" + item.SubAccountID + "'>" + item.SubAccountID + "</td>"
                + "<td class='CreationDate hide' val='" + GetDateFromServer(item.CreationDate) + "'>" + GetDateFromServer(item.CreationDate) + "</td>"
                + "<td class='CurrencyID hide' val='" + item.CurrencyID + "'>" + item.CurrencyID + "</td>"
                + "<td class='DepreciableAmount hide' val='" + item.DepreciableAmount + "'>" + item.DepreciableAmount + "</td>"
                + "<td class='IntialAmount hide' val='" + item.IntialAmount + "'>" + item.IntialAmount + "</td>"
                + "<td class='OpeningDepreciationAmount hide' val='" + item.OpeningDepreciationAmount + "'>" + item.OpeningDepreciationAmount + "</td>"
                + "<td class='PurchasingAmount hide' val='" + item.PurchasingAmount + "'>" + item.PurchasingAmount + "</td>"
                + "<td class='PurchasingDate hide' val='" + GetDateFromServer(item.PurchasingDate) + "'>" + GetDateFromServer(item.PurchasingDate) + "</td>"
                + "<td class='StartDepreciationDate hide' val='" + GetDateFromServer(item.StartDepreciationDate) + "'>" + GetDateFromServer(item.StartDepreciationDate) + "</td>"
                + "<td class='PurchasingAmountLocal hide' val='" + item.PurchasingAmountLocal + "'>" + item.PurchasingAmountLocal + "</td>"
                + "<td class='ExchangeRate hide' val='" + item.ExchangeRate + "'>" + item.ExchangeRate + "</td>"
                + "<td class='BarCodeType hide' val='" + item.BarCodeType + "'>" + item.BarCodeType + "</td>"
                + "<td class='ScrappingAmount hide' val='" + item.ScrappingAmount + "'>" + item.ScrappingAmount + "</td>"
                + "<td class='IsNotDepreciable hide' val='" + item.IsNotDepreciable + "'>" + item.IsNotDepreciable + "</td>"
                + "<td class='DepreciationTypeID hide' val='" + item.DepreciationTypeID + "'>" + item.DepreciationTypeID + "</td>"
                + "<td class='HasTransaction hide' val='" + item.HasTransaction + "'>" + item.HasTransaction + "</td>"
                + "<td class='hFA_AssetsUnApproving'><a href='#FA_AssetsUnApprovingModal' data-toggle='modal' onclick='FA_AssetsUnApproving_FillControls(" + item.ID + ");' " + editControlsText + "</a></td></tr>"));
    });
    ApplyPermissions();
    BindAllCheckboxonTable("tblFA_AssetsUnApproving", "ID");
    CheckAllCheckbox("ID");
    HighlightText("#tblFA_AssetsUnApproving>tbody>tr", $("#txt-Search").val());
    if (showDeleteFailMessage) {//sherif: if deletion failed due to foreign key, then this message is shown
        swal(strSorry, strDeleteFailMessage, "warning");
        showDeleteFailMessage = false;
    }
    $("#hl-homepage").on("click", function () { LoadViews("hl-homepage"); });
}

function FA_AssetsUnApproving_LoadingWithPaging() {
    debugger;
    //  LoadWithPaging("div-Pager", "select-page-size", "spn-first-page-row", "spn-last-page-row", "spn-total-count", "div-Text-Total", "/api/FA_AssetsUnApproving/LoadWithPaging", $("#div-Pager li.active a").text(), $('#select-page-size').val().trim(), function (pTabelRows) { FA_AssetsUnApproving_BindTableRows(pTabelRows); FA_AssetsUnApproving_ClearAllControls(); });
    LoadWithPaging("div-Pager", "select-page-size", "spn-first-page-row", "spn-last-page-row", "spn-total-count", "div-Text-Total", "/api/FA_AssetsUnApproving/LoadWithPaging", $("#div-Pager li.active a").text(), $('#select-page-size').val().trim(), function (pTabelRows) { FA_AssetsUnApproving_BindTableRows(pTabelRows); });
    HighlightText("#tblFA_AssetsUnApproving>tbody>tr", $("#txt-Search").val().trim());
}



function FA_AssetsUnApproving_Approve() {

    console.log(GetAllSelectedIDsAsString("tblFA_AssetsUnApproving"));
    var pSelectedIDs = GetAllSelectedIDsAsString("tblFA_AssetsUnApproving");
    if (pSelectedIDs != "") {


        //*************
        swal({
            title: "Are you sure  ?",
            text: "You will Approve Selected Transactions ",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "OK , Approve !",
            cancelButtonText: "NO",
            closeOnConfirm: true
        },
            function (isConfirm) {
                //swal("Poof! Your imaginary file has been deleted!", {
                //    icon: "success",
                //});

                if (isConfirm) {
                    //-----------------------
                    FadePageCover(true);
                    CallGETFunctionWithParameters("/api/FA_AssetsUnApproving/Approve"
                        , { pSelectedIDs: pSelectedIDs, pDate: ConvertDateFormat(getTodaysDateInddMMyyyyFormat()), pApproved: false }
                        , function (pData) {
                            FadePageCover(false);
                            if (pData[0]) {
                                swal("Success", "Saved successfully", "success");
                                FA_AssetsUnApproving_LoadingWithPaging();

                            }
                            else {
                                swal("Sorry", pData[1], "warning");

                            }
                        }
                        , null);

                    //----------
                }
                else {
                    console.log('refuse merge');
                }
            }
        );
        //*************





    }
}





