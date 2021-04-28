using System.Net.Http;
using System.Threading.Tasks;
using API.Helpers;
using M_TV_Info.Models.TMDbModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace M_TV_Info.Controllers
{
    public class MovieController : Controller
    {
        // Return View
        public async Task<IActionResult> Movie(int id){
            
            var model = await GetMovie(id);

            return View(model);
        }
        
        private async Task<MovieModel> GetMovie(int id)
        {
            HttpClient http = new HttpClient();

            var data = http.GetAsync("https://api.themoviedb.org/3/movie/" + id + "?api_key=" + Constants.ApiKey);

            if (!(data is null))
            {
                var content = await data.Result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<MovieModel>(content);
            }
            else
            {
                return null;
            }
        }
    }
}