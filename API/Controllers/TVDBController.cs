using System;
using System.Collections.Generic;
using API.Models;
using TMDbLib.Client;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.Trending;
using TMDbLib.Objects.General;
using System.Linq;

namespace API.Controllers
{  
    public static class TVcontroller
    {
        private static TMDbClient client = new TMDbClient("38e4159a51884d450eefb61ff274ce8c");
        private static MongoCRUD db = new MongoCRUD();

        public static List<MEDIA_LOOKUP_model> getTrendingMovies(TimeWindow timeWindow)
        {
            List<MEDIA_LOOKUP_model> list = new List<MEDIA_LOOKUP_model>();
            List<SearchMovie> trending = client.GetTrendingMoviesAsync(timeWindow).Result.Results;

            trending.ForEach(x => {
                var item = new Models.MEDIA_LOOKUP_model { MEDIA_ID = x.Id, NAME = x.Title, POSTER_URL = x.PosterPath, ID = new System.Guid() };
                list.Add(item);
                db.UpsertMedia(item);
            });
            return list;
        }

        public static List<MEDIA_LOOKUP_model> getTrendingTV(TimeWindow timeWindow)
        {
            List<MEDIA_LOOKUP_model> list = new List<MEDIA_LOOKUP_model>();
            List<SearchTv> trending = client.GetTrendingTvAsync(timeWindow).Result.Results;

            trending.ForEach(x => {
                var item = new Models.MEDIA_LOOKUP_model { MEDIA_ID = x.Id, NAME = x.Name, POSTER_URL = x.PosterPath, ID = new System.Guid() };
                list.Add(item);
                db.UpsertMedia(item);           
            });
            return list;
        }

        public class SearchMediaOptions
        {
            public MediaType? type { get; set; }

        }
        public static List<MEDIA_LOOKUP_model> searchMedia(string query, SearchMediaOptions options = null)
        {
            List<SearchBase> result = client.SearchMultiAsync(query).Result.Results;
            List<MEDIA_LOOKUP_model> list = new List<MEDIA_LOOKUP_model>();

            result.ForEach(x => {
                switch(x.MediaType) 
                {
                    case MediaType.Movie: {
                        var searchMovie = (x as SearchMovie);
                        var media = new Models.MEDIA_LOOKUP_model { MEDIA_ID = searchMovie.Id, NAME = searchMovie.Title, POSTER_URL = searchMovie.PosterPath, ID = new System.Guid() };
                        db.UpsertMedia(media);
                        
                        if (options == null || (options != null && options.type == MediaType.Movie)) list.Add(media);
                        break;
                    }
                    case MediaType.Tv: {
                        var searchTv = (x as SearchTv);
                        var media = new Models.MEDIA_LOOKUP_model { MEDIA_ID = searchTv.Id, NAME = searchTv.Name, POSTER_URL = searchTv.PosterPath, ID = new System.Guid() };
                        db.UpsertMedia(media);

                        if (options == null || (options != null && options.type == MediaType.Tv)) list.Add(media);
                        break;
                    }
                }
                //db.UpsertMedia(new Models.MEDIA_LOOKUP_model { MEDIA_ID = x.Id, NAME = x.Name, POSTER_URL = x.PosterPath, ID = new System.Guid() });
            });

            //if (options != null) result.Where(x => x.MediaType == options.type).ToList();

            return list;
        }

    }
}