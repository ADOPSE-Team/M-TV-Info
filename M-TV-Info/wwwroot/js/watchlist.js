$(document).ready( function() { 
    $('#addWatch').click( function() {
        var media_id = $('#movie_id');
        var movie_title = $('#movie_title');
        var movie_poster = $('#poster_path');
        var _item = new Object();
        _item.media_id = media_id.val();
        _item.movie_title = movie_title.val();
        _item.movie_poster = movie_poster.val();
        $.ajax({
            type: "POST", //HTTP POST Method  
            url: "/api/AjaxAPI/AddToWatchList", // Controller/View
            dataType: "json",
            data: _item,
            //contentType: "application/json; charset=utf-8",
            success: function () {
                alert("Succesfully Added");
            }
        });
    });
});