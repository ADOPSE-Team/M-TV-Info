using System.Collections.Generic;
using M_TV_Info.Models;
using M_TV_Info.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using Microsoft.Extensions.Logging;

namespace M_TV_Info.Controllers
{
    public class WatchListController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<WatchListController> _logger;

        // Def Constructor
        public WatchListController(ILogger<WatchListController> logger,
            ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;

        }

        // Add To Favourites
        [Route("api/AjaxAPI/AddToWatchList")]
        [HttpPost]
        public ActionResult AddToWatchList(WatchListModelPost item)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            WatchlistModel model = new WatchlistModel();
            DateTime date = DateTime.Now;

            var callMedia = _context.Watchlist.Where(i => i.media_id == item.media_id && i.user_id == userId).FirstOrDefault();

            if (!(callMedia is null))
            {
                _context.Watchlist.Remove(callMedia);
                _context.SaveChanges();

                return Ok();
            }
            else
            {
                model.media_id = item.media_id;
                model.movie_title = item.movie_title;
                model.poster_path = item.poster_path;
                model.user_id = userId;
                model.w_date = date;

                _context.Watchlist.Add(model);
                _context.SaveChanges();

                return Ok();
            }
        }

        // Remove From WatchList
        [Route("WatchList")]
        [HttpPost]
        public ActionResult RemoveFromWatchList(int id)
        {
            var getWatch = _context.Watchlist.Where(w => w.id == id).FirstOrDefault();

            _context.Watchlist.Remove(getWatch);

            _context.SaveChanges();

            return Redirect("Home/Watchlist");
        }

        // Check if Exists
        [Route("/api/AjaxAPI/CheckWatchList")]
        [HttpPost]
        public ActionResult CheckWatchList(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var getWatc = _context.Watchlist.Where(i => i.media_id == id && i.user_id == userId).ToList();

            if( getWatc.Any() )
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
