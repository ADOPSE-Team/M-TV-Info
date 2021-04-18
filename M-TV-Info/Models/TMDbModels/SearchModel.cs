using System.Collections.Generic;

namespace M_TV_Info.Models.TMDbModels
{
    // SearchModel jsonDeserialize = JsonConvert.DeserializeObject<SearchModel>(json); 
    public class SearchResult
    {
        public string name { get; set; }
        public int id { get; set; }
    }

    public class SearchModel
    {
        public int page { get; set; }
        public List<SearchResult> results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }

}