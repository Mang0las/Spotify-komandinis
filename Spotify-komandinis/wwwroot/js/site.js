// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//var express = require('express'); // Express web server framework
//var app = express();
//app.use(express.static(__dirname + '/public'));
//console.log('Listening on 8888');
//app.listen(8888);

$(document).ready(function () {

    //var url = window.location.href;
    //console.log(window.location.search);
    //console.log(url);
    //const urlParams = new URLSearchParams(url);
    //var token = urlParams.get('#access_token');
    //console.log(token);

    var hash = window.location.hash.substring(1);
    var accessString = hash.indexOf("&");

/* 13 because that bypasses 'access_token' string */
    //@Html.Hidden("token", 0);
    //var access_token = hash.substring(13, accessString);
    //$('token').val(access_token);
    //console.log("Access Token: " + access_token);
    //window.location.href = "https://localhost:44382/Overview";


});