$(document).ready(function () {
    $('#addFav').click(function () {
        $.ajax({
            type: "POST", //HTTP POST Method
            url: "Home/AddToFavourites", // Controller/View
            data: { //Passing data
                media_id: $('#movie_id').val(),
                movie_title: $('#movie_title').val(),
                movie_poster: $('#poster_path').val()
            },
            success: function (data) {
                var Movie = $('#movie_title').val();
                alert("Movie" + Movie + "has been added to favourites list");
            }
        });
    });
});