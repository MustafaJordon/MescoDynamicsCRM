﻿$(document).ready(function () {
    //$(".username").focus(function () {
    //    $(".user-icon").css("left", "-48px");
    //});
    //$(".username").blur(function () {
    //    $(".user-icon").css("left", "0px");
    //});

    //$(".password").focus(function () {
    //    $(".pass-icon").css("left", "-48px");
    //});
    //$(".password").blur(function () {
    //    $(".pass-icon").css("left", "0px");
    //});

    //to bind the Enter key to button CheckIsValidUser
    $(document).keypress(function (e) {
        if (e.which == 13) {
            $("#CheckIsValidUser").click();
        }
    });

    $("#CheckIsValidUser").click(function () {
        debugger;
        //alert("khlkh");

        var data = {
            "pUsername": $("#txtUserName").val(),
            "pPassword": $("#txtPassword").val(),
            "pUrl": window.location.href + 'Home'
        };
        $.ajax({
            url: "/Login/CheckIsValidUser",
            type: "POST",
            data: JSON.stringify(data),
            dataType: "html",
            contentType: "application/json",
            success: function (data) {
                debugger;
                if (data == "") //data comes as a string coz datatype is 'html'
                    window.location.href = window.location.origin + '/Home';
                else
                    $("#lblErrorMessage").text(data);
                //$("#message").html(status.Message);
                //if (status.data) {
                //    debugger;
                //    window.location.href = status.TargetURL;
                //}
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                debugger;
                //var url = window.location.href + 'Home';
                //window.location.href = url;
                //$("#message").html("Error while authenticating user credentials!");
            }
        });
        debugger;
        //var url = window.location.href +'Home';
        //window.location.href = url;
    });

    $("#CheckIsValidUser_PageDirectly").click(function () {
        debugger;
        //alert("khlkh");

        var data = {
            "pUsername": $("#txtUserName").val(),
            "pPassword": $("#txtPassword").val(),
            "pUrl": 'PageDirectly',
        };
        $.ajax({
            url: "/Login/CheckIsValidUser",
            type: "POST",
            data: JSON.stringify(data),
            dataType: "html",
            contentType: "application/json",
            success: function (data) {
                debugger;
                if (data == "") //data comes as a string coz datatype is 'html'
                    window.location.href = window.location.origin + '/Home';
                else
                    $("#lblErrorMessage").text(data);
                LoadViews("CRM");
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                debugger;
            }
        });
        debugger;
    });

    $("#aResetPassword").click(function () {
        debugger;
        var data = {
            "pUserNameToReset": $("#txtUserName").val().trim(),
            "pUrl": window.location.href + 'Home'
        };
        $.ajax({
            url: "/Login/ResetPassword",
            type: "POST",
            data: JSON.stringify(data),
            dataType: "html",
            contentType: "application/json",
            success: function (data) {
                debugger;
                if (data == "") //data comes as a string coz datatype is 'html'
                    window.location.href = window.location.origin + '/Home';
                else
                    //$("#lblErrorMessage").text(data);
                alert(data);
                //$("#message").html(status.Message);
                //if (status.data) {
                //    debugger;
                //    window.location.href = status.TargetURL;
                //}
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                debugger;
                //var url = window.location.href + 'Home';
                //window.location.href = url;
                //$("#message").html("Error while authenticating user credentials!");
            }
        });
        debugger;
        //var url = window.location.href +'Home';
        //window.location.href = url;
    });

});
