using System.Net.Http;
using System.Threading.Tasks;
using API.Helpers;
using M_TV_Info.Models.TMDbModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace M_TV_Info.Controllers
{
    public class UpcomingController : Controller
    {
        public IActionResult Upcoming(){
            
            var model = GetUpcoming();

            return View(model);
        }

        // [HttpGet("upcoming")]
        private async Task<UpcomingModel> GetUpcoming()
        {
            HttpClient http = new HttpClient();

            var data = http.GetAsync("https://api.themoviedb.org/3/movie/upcoming?api_key=" + Constants.ApiKey);

            if(!(data is null))
            {
                var content = await data.Result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UpcomingModel>(content);
            }

            return null;
        }
    }
}