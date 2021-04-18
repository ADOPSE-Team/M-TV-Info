
using System.Net.Http;
using System.Threading.Tasks;
using API.Helpers;
using M_TV_Info.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace M_TV_Info.Controllers
{
    public class TheMovieDbController : Controller
    {
        // {type} -> all || movie || tv || person -> {def: all}
        [HttpGet("trendings/")]
        public async Task<TrendingsModel> GetTrendings(string? type, string? timeWindow)
        {
            HttpClient http = new HttpClient();

            // Check type & timeWindow
            if(type is null) type = "all";
            if(timeWindow is null) timeWindow = "day";
            
            
            var data = http.GetAsync("https://api.themoviedb.org/3/trending/" + type + "/" 
                                        + timeWindow + "?api_key=" + Constants.ApiKey);
            
            if(!(data is null))
            {  
                var content = await data.Result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TrendingsModel>(content);
            }

            return null;
        }

        public async Task<IActionResult> Index()
        {
            var model = await GetTrendings("", "");

            return View(model);
        }
    }
}