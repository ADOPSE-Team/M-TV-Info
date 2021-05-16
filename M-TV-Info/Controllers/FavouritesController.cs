using System.Collections.Generic;
using M_TV_Info.Models;
using M_TV_Info.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace M_TV_Info.Controllers
{
    public class FavouritesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FavouritesController> _logger;
        public FavouritesController(ILogger<FavouritesController> logger,
            ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Add To Favourites
        [Route("api/AjaxAPI/AddToFavourites")]
        [HttpPost]
        public ActionResult AddToFavourites(FavouriteModelPost item)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            FavouriteModel model = new FavouriteModel();
            DateTime date = DateTime.Now;

            var callMedia = _context.Favourite.Where(i => i.media_id == item.media_id && i.user_id == userId).FirstOrDefault();

            if (!(callMedia is null))
            {
                _context.Favourite.Remove(callMedia);
                _context.SaveChanges();

                return Ok();
            }
            else
            {
                model.media_id = item.media_id;
                model.movie_title = item.movie_title;
                model.movie_poster = item.movie_poster;
                model.user_id = userId;
                model.w_date = date;

                _context.Favourite.Add(model);
                _context.SaveChanges();

                return Ok();
            }
        }
        
        [Route("Favourites")]
        // Remove From Favourites
        [HttpPost]
        public ActionResult RemoveFromFavourites(int id)
        {
            var getFav = _context.Favourite.Where(f => f.id == id).FirstOrDefault();

            _context.Favourite.Remove(getFav);

            _context.SaveChanges();

            return Redirect("Home/Favorites");
        }

        // Check if Exists
        [Route("/api/AjaxAPI/CheckFavourites")]
        [HttpPost]
        public ActionResult CheckFavourites(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var getFav = _context.Favourite.Where(i => i.media_id == id && i.user_id == userId).ToList();

            if( getFav.Any() )
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
