using API.Helpers;
using M_TV_Info.Data;
using M_TV_Info.Models;
using M_TV_Info.Models.TMDbModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace M_TV_Info.Controllers
{
    public class HomeController : Controller
    {
        HttpClient client = new HttpClient();
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger,
            ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }


        // Index View
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

        // WatchList Page
        public IActionResult Watchlist()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.FindFirstValue(ClaimTypes.Name);

            var currentUserWatchList = _context.Watchlist.Where(i => i.user_id == userId).ToList();

            return View(currentUserWatchList);
        }
        public IActionResult Ratings()
        {
            return View();
        }

        // Favorites Page
        public IActionResult Favorites()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.FindFirstValue(ClaimTypes.Name);

            var currentUserFavourites = _context.Favourite.Where(i => i.user_id == userId).ToList();

            return View(currentUserFavourites);
        }

        // Movie View Page
        public async Task<IActionResult> MovieView(int id)
        {
            var data = await client.GetStringAsync("https://api.themoviedb.org/3/movie/" + id + "?api_key=" + Constants.ApiKey);
            var content = JsonConvert.DeserializeObject<MovieModel>(data);

            return View(content);
        }

        // Search 
        [HttpPost]
        public async Task<IActionResult> Search(string query)
        {
            return View();
        }

        // Auto Complete Search
        [Produces("application/json")]
        [HttpGet("search")]
        public async Task<IActionResult> AutocompleteSearch()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                if(!(term is null))
                {
                    var data = await client.GetStringAsync("https://api.themoviedb.org/3/search/multi?api_key=" + Constants.ApiKey + 
                                            "&query" + term);
                    
                    var content = JsonConvert.DeserializeObject<SearchModel>(data);

                    return Ok(content);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
