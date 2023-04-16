﻿function YardLinkTankClients_BindTableRows(pTableRows) {
    debugger;
    $("#hl-menu-ShipLink").parent().addClass("active");
    ClearAllTableRows("tblYardLinkTankClients");
    //editControlsText = " class='btn btn-xs btn-rounded btn-info float-right' > <i class='fa fa-pencil' style='padding-left: 5px;'></i> <span style='padding-right: 5px;'>" + TranslateString("Edit") + "</span>";
    $.each(pTableRows, function (i, item) {
        AppendRowtoTable("tblYardLinkTankClients",
        //("<tr ID='" + item.ID + "' ondblclick='YardLinkTankClients_FillControls(" + item.ID + ");'>"
        ("<tr ID=" + item.ID + ">"
                    + "<td class='ID'> <input name='Delete' type='checkbox' value='" + item.ID + "' /></td>"
                    + "<td class='Name'>" + (item.Name == 0 ? "" : item.Name) + "</td>"
                    + "<td class='ArName'>" + (item.ArName == 0 ? "" : item.ArName) + "</td>"
                    //+ "<td class='hide'><a href='#YardLinkTankClientsModal' data-toggle='modal' onclick='YardLinkTankClients_FillControls(" + item.ID + ");' " + editControlsText + "</a></td>"
        + "</tr>"));
    });
    ApplyPermissions();
    BindAllCheckboxonTable("tblYardLinkTankClients", "ID");
    CheckAllCheckbox("ID");
    HighlightText("#tblYardLinkTankClients>tbody>tr", $("#txt-Search").val().trim());
    if (showDeleteFailMessage) {//sherif: if deletion failed due to foreign key, then this message is shown
        swal(strSorry, strDeleteFailMessage);
        showDeleteFailMessage = false;
    }
    $("#hl-homepage").on("click", function () { LoadViews("hl-homepage"); });
}


function YardLinkTankClients_LoadingWithPaging() {
    debugger;
    var pWhereClause = YardLinkTankClients_GetWhereClause();
    var pPageSize = $('#select-page-size').val();
    var pPageNumber = 1;//($("#div-Pager li.active a").text() == "" || $("#txt-Search").val().trim() != "" ? 1 : $("#div-Pager li.active a").text());
    var pOrderBy = "Name, ArName";
    var pControllerParameters = { pIsLoadArrayOfObjects: false, pLanguage: $("[id$='hf_ChangeLanguage']").val(), pPageNumber: pPageNumber, pPageSize: pPageSize, pWhereClause: pWhereClause, pOrderBy: pOrderBy }
    LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned("div-Pager", "select-page-size", "spn-first-page-row", "spn-last-page-row", "spn-total-count", "div-Text-Total", strLoadWithPagingFunctionName, pWhereClause, pOrderBy, pPageNumber, pPageSize, pControllerParameters, function (pData) { YardLinkTankClients_BindTableRows(JSON.parse(pData[0])); });
    HighlightText("#tblYardLinkTankClients>tbody>tr", $("#txt-Search").val().trim());
}
function YardLinkTankClients_GetWhereClause() {
    debugger;
    var pWhereClause = "WHERE 1=1";
    if ($("#txt-Search").val().trim() != "") {
        pWhereClause += " AND (" + "\n";
        pWhereClause += "Name LIKE N'%" + $("#txt-Search").val().trim() + "%'";
        pWhereClause += "OR ArName LIKE N'%" + $("#txt-Search").val().trim() + "%'";
        pWhereClause += ")" + "\n";
    }
    return pWhereClause;
}
function YardLinkTankClients_Save() {
    debugger;
    var pSelectedIDs = GetAllSelectedIDsAsString('tblYardLinkTankClients', 'Delete');
    if (pSelectedIDs == "")
        swal("Sorry", "Please, select at least one client.");
    else if ($("#slClientGroup").val() == 0)
        swal("Sorry", "Please, Select client group.");
    else {
        FadePageCover(true);
        if (pSelectedIDs != "") {
            CallGETFunctionWithParameters("/api/YardLinkTankClients/Save"
                , {
                    pSelectedIDs: pSelectedIDs
                    , pClientGroupID: $("#slClientGroup").val()
                }
                , function (pData) {
                    if (pData[0]) {
                        swal("Success", "Saved successfully");
                        YardLinkTankClients_LoadingWithPaging();
                    }
                    else
                        swal("Sorry", "Connection failed, please try again.");
                    FadePageCover(false);
                }
                , null);
        }
    }
}
