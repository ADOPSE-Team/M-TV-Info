
using System.Net.Http;
using System.Threading.Tasks;
using API.Helpers;
using M_TV_Info.Models;
using M_TV_Info.Models.TMDbModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace M_TV_Info.Controllers
{
    [Route("/trendings")]
    public class TrendingsController : Controller
    {
        
        // Return View
        // [HttpGet]
        // public async Task<IActionResult> Index()
        // {
            
        //     var model = await GetTrendings("movie", "day");

        //     return View(model);
        // }

        // GET /trendings
        [HttpGet("{type=movie}/{timeWindow=day}")]
        public async Task<IActionResult> Index(string type, string timeWindow)
        {
            HttpClient http = new HttpClient();

            // {type} -> all || movie || tv || person -> {def: all}
            // {timeWindow} -> day || week -> {def: week}

            // Check type & timeWindow
            if(type is null) type = "all";
            if(timeWindow is null) timeWindow = "day";
            
            
            var data = await http.GetStringAsync("https://api.themoviedb.org/3/trending/" + type + "/" 
                                        + timeWindow + "?api_key=" + Constants.ApiKey);
            
            if(!(data is null))
            {
                // var content = await data.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<TrendingsModel>(data);
                return View(model);
            }
            
            return null;
        }
    }
}