// Add & Remove From Favourites
$(document).ready( function() { 
    $('#addFav').click( function() {
        // Object
        var media_id = $('#movie_id');
        var movie_title = $('#movie_title');
        var movie_poster = $('#poster_path');
        var _item = new Object();

        // Set Object
        _item.media_id = media_id.val();
        _item.movie_title = movie_title.val();
        _item.movie_poster = movie_poster.val();

        // AJAX CALL
        $.ajax({
            type: "POST", //HTTP POST Method  
            url: "/api/AjaxAPI/AddToFavourites", // Controller/View
            dataType: "json",
            data: _item,
            //contentType: "application/json; charset=utf-8",
            success: function () {
                location.reload();
            }
        });
    });
});