
using System.Net.Http;
using System.Threading.Tasks;
using API.Helpers;
using M_TV_Info.Models;
using M_TV_Info.Models.TMDbModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace M_TV_Info.Controllers
{
    public class TrendingsController : Controller
    {
        
        // Return View
        public IActionResult Trendings(string? type, string? timeWindow){
            
            var model = GetTrendings(type, timeWindow);

            return View(model);
        }

        // GET /trendings
        // [HttpGet("trendings/{movie}&{week}")]
        private async Task<TrendingsModel> GetTrendings(string? type, string? timeWindow)
        {
            HttpClient http = new HttpClient();

            // {type} -> all || movie || tv || person -> {def: all}
            // {timeWindow} -> day || week -> {def: week}

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
    }
}