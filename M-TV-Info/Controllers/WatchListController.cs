using System.Collections.Generic;
using M_TV_Info.Models;
using M_TV_Info.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;

namespace M_TV_Info.Controllers
{
    public class WatchListController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Def Constructor
        public WatchListController(ApplicationDbContext context)
        {
            _context = context;

        }

        // Add To Favourites
        [Route("api/AjaxAPI/AddToWatchList")]
        [HttpPost]
        public ActionResult AddToWatchList(WatchListModelPost item)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.FindFirstValue(ClaimTypes.Name);

            WatchlistModel model = new WatchlistModel();
            DateTime date = DateTime.Now;

            var callMedia = _context.Watchlist.Where( i => i.media_id == item.media_id && i.user_id == userId).ToList();

            if( callMedia.Any())
            {
                return BadRequest();
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

                return Ok(model);
            }
        }

        // Remove From WatchList
        [Route("api/AjaxAPI/RemoveFromWatchList")]
        [HttpPost]
        public ActionResult RemoveFromWatchList(int id)
        {
            var getWatch = _context.Watchlist.Where(w => w.id == id).First();

            _context.Watchlist.Remove(getWatch);

            _context.SaveChanges();
            
            return Ok("Deleted");
        }
    }
}
