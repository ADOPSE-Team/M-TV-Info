using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TheMovieDbTrendingController : ControllerBase
    {

        // Trending ALL
        [HttpGet("trending")]
        public string GetTrending(){

            HttpClient http = new HttpClient();

            // http.DefaultRequestHeaders.Add(schemename, header);
            var data = http.GetAsync("https://api.themoviedb.org/3/trending/all/day?api_key=38e4159a51884d450eefb61ff274ce8c")
                        .Result.Content.ReadAsStringAsync().Result;

            return data;
        }

       // Trending Via Type and Date
       [HttpGet("{type}/{days}")]
       // days --> {day, week}
       // type --> {all, movie, tv, person}
       public string GetTrendingType(string type, string days)
       {

           HttpClient http = new HttpClient();

           var data = http.GetAsync("https://api.themoviedb.org/3/trending/" + type 
                                        + "/" + days + "?api_key=38e4159a51884d450eefb61ff274ce8c")
                        .Result.Content.ReadAsStringAsync().Result;

           return data;
       }


    }
}