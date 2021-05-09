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

            model.media_id = item.media_id;
            model.movie_title = item.movie_title;
            model.poster_path = item.poster_path;
            model.user_id = userId;
            model.w_date = date;

            _context.Watchlist.Add(model);
            _context.SaveChanges();

            return Ok(model);
        }

        public void update(WatchlistModel item)
        {
            using (var context = new ApplicationDbContext())
            {
                var existing = context.Watchlist.Where(x => x.id == item.id).First();
                existing = item;
                context.SaveChanges();
            }
        }

        public void delete(WatchlistModel item)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Watchlist.Remove(item);
                context.SaveChanges();
            }
        }
    }
}
