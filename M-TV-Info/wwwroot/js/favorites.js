$(document).ready( function() { 
    $('#addFav').click( function() {
        var media_id = $('#movie_id');
        var movie_title = $('#movie_title');
        var movie_poster = $('#poster_path');
        var _item = new Object();
        _item.media_id = media_id.val();
        _item.movie_title = movie_title.val();
        _item.movie_poster = movie_poster.val();
        $.ajax({
            type: "POST", //HTTP POST Method  
            url: "/api/AjaxAPI/AddToFavourites", // Controller/View
            dataType: "json",
            data: _item,
            //contentType: "application/json; charset=utf-8",
            success: function () {
                alert("Succesfully Added");
            },
            error: function() {
                alert("Already Exist");
            }
        });
    });
});

// Remove From Favourites
// $(document).ready( function(){
//     $('#removeFav').click( function(){
//         var fav_id = $('#fav_id').val();
//         $.ajax({
//             type: "POST", //HTTP POST Method
//             url: "/api/AjaxAPI/RemoveFromFavourites",
//             dataType: "int",
//             data: fav_id,
//             success: function (data) {
//                 alert("Succesfully Deleted");
//             }
//         });
//         location.reload();
//     });
// });