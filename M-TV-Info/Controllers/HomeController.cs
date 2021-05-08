using API.Helpers;
using M_TV_Info.Models;
using M_TV_Info.Models.TMDbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace M_TV_Info.Controllers
{
    public class HomeController : Controller
    {
        HttpClient client = new HttpClient();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var TrendingMoviesData = await client.GetStringAsync("https://api.themoviedb.org/3/trending/movie/day?api_key=" + Constants.ApiKey);
            var TrendingTVData = await client.GetStringAsync("https://api.themoviedb.org/3/trending/tv/day?api_key=" + Constants.ApiKey);
            var UpcomingMoviesData = await client.GetStringAsync("https://api.themoviedb.org/3/movie/upcoming?api_key=" + Constants.ApiKey);

            var TrendingMoviesContent = JsonConvert.DeserializeObject<TrendingsModel>(TrendingMoviesData);
            var TrendingTVContent = JsonConvert.DeserializeObject<TrendingsModel>(TrendingTVData);
            var UpcomingMoviesContent = JsonConvert.DeserializeObject<UpcomingModel>(UpcomingMoviesData);

            HomeModel home = new HomeModel();

            home.TrendingMovies = TrendingMoviesContent.results;
            home.TrendingTvShows = TrendingTVContent.results;
            home.UpcomingMovies = UpcomingMoviesContent.results;

            return View(home);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Watchlist() 
        {
            return View();
        }
        public IActionResult Ratings()
        {
            return View();
        }
        public IActionResult Favorites()
        {
            return View();
        }
        public async Task<IActionResult> MovieView(int id)
        {
            var data = await client.GetStringAsync("https://api.themoviedb.org/3/movie/" + id + "?api_key=" + Constants.ApiKey);
            var content = JsonConvert.DeserializeObject<MovieModel>(data);

            return View(content);
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
