using System.Collections.Generic;

namespace M_TV_Info.Models
{
    // TrendingsModel jsonDeserialize = JsonConvert.DeserializeObject<TrendingsModel>(json); 
    public class TrendingsResult
    {
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public List<int> genre_ids { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public int vote_count { get; set; }
        public double vote_average { get; set; }
        public bool video { get; set; }
        public string release_date { get; set; }
        public string overview { get; set; }
        public string original_title { get; set; }
        public string poster_path { get; set; }
        public string original_language { get; set; }
        public double popularity { get; set; }
        public string media_type { get; set; }
        public string original_name { get; set; }
        public string name { get; set; }
        public List<string> origin_country { get; set; }
        public string first_air_date { get; set; }
    }

    public class TrendingsModel
    {
        public int page { get; set; }
        public List<TrendingsResult> results { get; set; }
    }


}