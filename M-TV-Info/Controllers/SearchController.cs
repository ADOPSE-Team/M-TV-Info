using System.Net.Http;
using System.Threading.Tasks;
using API.Helpers;
using M_TV_Info.Models.TMDbModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace M_TV_Info.Controllers
{
    public class SearchController : Controller
    {

        // Return View
        public IActionResult Search(string query)
        {
            var model = GetSearchResult(query);

            return View(model);
        } 

        // GET /search/keyword
        [HttpGet("search/{query}")]
        public async Task<SearchModel> GetSearchResult(string query)
        {
            HttpClient http = new HttpClient();

            if(!(query is null))
            {
                var data = http.GetAsync("https://api.themoviedb.org/3/search/multi?api_key=" + Constants.ApiKey + 
                                            "&query" + query);
                
                if(!(data is null))
                {
                    var content = await data.Result.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<SearchModel>(content);
                }
            }

            return null;
        }
    }
}