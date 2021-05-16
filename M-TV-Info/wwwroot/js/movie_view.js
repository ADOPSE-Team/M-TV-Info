﻿$(document).ready(function () {
    var _fav_icon = "/assets/favoriteListButton2.png";
    var _watch_icon = "/assets/watchlistButton2.png";
    var _movie = $("#movie_id");
    // Check Fav
    $.ajax({
        type: "POST", //HTTP POST Method
        url: "/api/AjaxAPI/CheckFavourites", // Controller/View
        data: _movie,
        success: function () {
            $("#addFav").attr("src", _fav_icon);
            $('#fav-exists').val(1);
        },
        error: function () {
            $('#fav-exists').val(0);
        } 
    });
    // Check Watch
    $.ajax({
        type: "POST", //HTTP POST Method
        url: "/api/AjaxAPI/CheckWatchList", // Controller/View
        data: _movie,
        success: function () {
            $("#addWatch").attr("src", _watch_icon);
            $('#watch-exists').val(1);
        },
        error: function () {
            $('#watch-exists').val(0);
        }
    });
});