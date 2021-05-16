$(document).ready(function () {
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
        }
    });

    // Check Watch
    $.ajax({
        type: "POST", //HTTP POST Method
        url: "/api/AjaxAPI/CheckWatchList", // Controller/View
        data: _movie,
        success: function () {
            $("#addWatch").attr("src", _watch_icon);
        }
    });

    // Check Rating
    $.ajax({
        type: "POST", // HTTP POST Method
        url: "/api/AjaxAPI/CheckRating", // Controller/View
        data: _movie,
        success: function (data) {
            switch( data ){
                case 1:
                    $("#star1").prop("checked", true);
                    break;
                case 2:
                    $("#star2").prop("checked", true);
                    break;
                case 3:
                    $("#star3").prop("checked", true);
                    break;
                case 4:
                    $("#star4").prop("checked", true);
                    break;
                case 5:
                    $("#star5").prop("checked", true);
                    break;
            }
        }
    });
});