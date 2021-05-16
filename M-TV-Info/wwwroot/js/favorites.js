// Add & Remove From Favourites
$(document).ready( function() { 
    $('#addFav').click( function() {

        // Icons
        var _fav_icon = "/assets/favoriteListButton2.png";
        var _fav = "/assets/addToFavoriteListButton.png";
        var exists = $('#fav-exists').val();

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
                if( exists == "1")
                {
                    $("#addFav").attr("src", _fav);
                    $('#watch-exists').val("0");
                }
                else
                {
                    $("#addFav").attr("src", _fav_icon);
                    $('#watch-exists').val("1");
                }
            }
        });
    });
});