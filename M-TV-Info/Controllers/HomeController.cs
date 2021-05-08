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
using System.Net.Http;
using System.Threading.Tasks;

namespace M_TV_Info.Controllers
{
    public class HomeController : Controller
    {
        HttpClient client = new HttpClient();
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public HomeController(ILogger<HomeController> logger,
            ApplicationDbContext context,
            UserManager<User> userManager)
        {
            _userManager = userManager;
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

        // Search 
        [HttpPost]
        public async Task<IActionResult> Search(string query)
        {
            return View();
        }

        // Add To Favorites
        [HttpPost]
        public async Task<IActionResult> AddToFavourites(Object obj)
        {
            var media = JsonConvert.DeserializeObject<FavouriteModelPost>(obj.ToString());

            FavouriteModel fav = new FavouriteModel();
            // User usr = await GetCurrentUserAsync();
            DateTime date = DateTime.Now;

            fav.media_id = media.media_id;
            fav.user_id = "1";
            fav.w_date = date;

            if (!(fav is null))
            {
                _context.Favourite.Add(fav);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                throw new ArgumentNullException(nameof(fav));
            }
        }

        // Get Current USER
        // private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
