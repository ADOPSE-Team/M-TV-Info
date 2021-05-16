// Add & Remove From WatchList
$(document).ready( function() { 
    $('#addWatch').click( function() {
        // Icons
        var _watch_icon = "/assets/watchlistButton2.png";
        var _watc = "/assets/addToWatchlistButton.png";
        var exists = $('#watch-exists').val();

        // Object
        var media_id = $('#movie_id');
        var movie_title = $('#movie_title');
        var movie_poster = $('#poster_path');

        // Set Object
        var _item = new Object();
        _item.media_id = media_id.val();
        _item.movie_title = movie_title.val();
        _item.poster_path = movie_poster.val();

        // AJAX CALL
        $.ajax({
            type: "POST", //HTTP POST Method  
            url: "/api/AjaxAPI/AddToWatchList", // Controller/View
            dataType: "json",
            data: _item,
            //contentType: "application/json; charset=utf-8",
            success: function () {
                if( exists == "1" ){
                    $("#addWatch").attr("src", _watch);
                    exists.val("0");
                }
                else{
                    $("#addWatch").attr("src", _watch_icon);
                    exists.val("1");
                }
                
            }
        });
    });
});