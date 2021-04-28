
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
        // [HttpGet]
        public async Task<IActionResult> Index(){
            
            var model = await GetTrendings("movie", "day");

            return View(model);
        }

        // GET /trendings
        // [HttpGet("trendings/{movie}&{week}")]
        private async Task<TrendingsModel> GetTrendings(string type, string timeWindow)
        {
            HttpClient http = new HttpClient();

            // {type} -> all || movie || tv || person -> {def: all}
            // {timeWindow} -> day || week -> {def: week}

            // Check type & timeWindow
            if(type is null) type = "all";
            if(timeWindow is null) timeWindow = "day";
            
            
            var data = await http.GetAsync("https://api.themoviedb.org/3/trending/" + type + "/" 
                                        + timeWindow + "?api_key=" + Constants.ApiKey);
            
            if(data.IsSuccessStatusCode)
            {
                var content = await data.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TrendingsModel>(content);
            }
            
            return null;
        }
    }
}