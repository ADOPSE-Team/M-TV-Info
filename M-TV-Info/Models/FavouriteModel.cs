using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace M_TV_Info.Models
{
    public class FavouriteModel
    {
        [Key]
        public int id { get; set; }
        public string user_id { get; set; }
        public int media_id { get; set; }
        public string movie_title{ get; set; }
        public string movie_poster {get; set; }
        public DateTime w_date { get; set; }
    }

    public class FavouriteModelPost
    {
        public int media_id { get; set; }
        public string movie_title { get; set; }
        public string movie_poster { get; set; }
    }

    public class FavouritesModelView
    {
        public List<FavouriteModel> FavouriteModel { get; set; }
    }
}
