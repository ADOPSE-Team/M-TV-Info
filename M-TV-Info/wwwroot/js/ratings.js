// Movie Review
$(document).ready( function(){

    // 5 Star
    $("#star5").click( function(){
        var media_id = $('#movie_id');
        var movie_title = $('#movie_title');
        var movie_poster = $('#poster_path');
        var star = $('#star5');
        var _review = new Object();
        _review.media_id = media_id.val();
        _review.movie_title = movie_title.val();
        _review.movie_poster = movie_poster.val();
        _review.star = star.val();
        $.ajax({
            type: "POST", //HTTP POST Method  
            url: "/api/AjaxAPI/AddRating", // Controller/View
            dataType: "json",
            data: _review,
            //contentType: "application/json; charset=utf-8",
            success: function () {
                location.reload();
            }
        });
    });

    // 4 Star
    $("#star4").click( function(){
        var media_id = $('#movie_id');
        var movie_title = $('#movie_title');
        var movie_poster = $('#poster_path');
        var star = $('#star4');
        var _review = new Object();
        _review.media_id = media_id.val();
        _review.movie_title = movie_title.val();
        _review.movie_poster = movie_poster.val();
        _review.star = star.val();
        $.ajax({
            type: "POST", //HTTP POST Method  
            url: "/api/AjaxAPI/AddRating", // Controller/View
            dataType: "json",
            data: _review,
            //contentType: "application/json; charset=utf-8",
            success: function () {
                location.reload();
            }
        });
    });

    // 3 Star
    $("#star3").click( function(){
        var media_id = $('#movie_id');
        var movie_title = $('#movie_title');
        var movie_poster = $('#poster_path');
        var star = $('#star3');
        var _review = new Object();
        _review.media_id = media_id.val();
        _review.movie_title = movie_title.val();
        _review.movie_poster = movie_poster.val();
        _review.star = star.val();
        $.ajax({
            type: "POST", //HTTP POST Method  
            url: "/api/AjaxAPI/AddRating", // Controller/View
            dataType: "json",
            data: _review,
            //contentType: "application/json; charset=utf-8",
            success: function () {
                location.reload();
            }
        });
    });

    // 2 Star
    $("#star2").click( function(){
        var media_id = $('#movie_id');
        var movie_title = $('#movie_title');
        var movie_poster = $('#poster_path');
        var star = $('#star2');
        var _review = new Object();
        _review.media_id = media_id.val();
        _review.movie_title = movie_title.val();
        _review.movie_poster = movie_poster.val();
        _review.star = star.val();
        $.ajax({
            type: "POST", //HTTP POST Method  
            url: "/api/AjaxAPI/AddRating", // Controller/View
            dataType: "json",
            data: _review,
            //contentType: "application/json; charset=utf-8",
            success: function () {
                location.reload();
            }
        });
    });

    // 1 Star
    $("#star1").click( function(){
        var media_id = $('#movie_id');
        var movie_title = $('#movie_title');
        var movie_poster = $('#poster_path');
        var star = $('#star1');
        var _review = new Object();
        _review.media_id = media_id.val();
        _review.movie_title = movie_title.val();
        _review.movie_poster = movie_poster.val();
        _review.star = star.val();
        $.ajax({
            type: "POST", //HTTP POST Method  
            url: "/api/AjaxAPI/AddRating", // Controller/View
            dataType: "json",
            data: _review,
            //contentType: "application/json; charset=utf-8",
            success: function () {
                location.reload();
            }
        });
    });
});