using System;
using System.ComponentModel.DataAnnotations;

namespace M_TV_Info.Models
{
    public class WatchlistModel
    {
        [Key]
        public int id { get; set; }
        public string user_id { get; set; }
        public int media_id { get; set; }
        public string poster_path { get; set; }
        public string movie_title { get; set; }
        public DateTime w_date { get; set; }
    }

    public class WatchListModelPost
    {
        public int media_id { get; set; }
        public string poster_path { get; set; }
        public string movie_title { get; set; }
    }
}
