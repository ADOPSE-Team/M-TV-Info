using System.Collections.Generic;
using M_TV_Info.Models.TMDbModels;

namespace M_TV_Info.Models
{
    public class HomeModel
    {
        public List<TrendingsResult> TrendingMovies { get; set; }
        public List<TrendingsResult> TrendingTvShows { get; set; }
        public List<UpcomingResult> UpcomingMovies { get; set; }
    }

}