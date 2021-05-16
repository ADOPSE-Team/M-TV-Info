using System.Collections.Generic;
using M_TV_Info.Models;
using M_TV_Info.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;

namespace M_TV_Info.Controllers
{
    public class RatingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RatingController(ApplicationDbContext context)
        {
            _context = context;

        }

        // Add Rating
        [Route("api/AjaxAPI/AddRating")]
        [HttpPost]
        public ActionResult AddRating(RatingsModelPost ratings)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            RatingsModel _ratings = new RatingsModel();
            DateTime date = DateTime.Now;

            var callRating = _context.Rating.Where(i => i.media_id == ratings.media_id && i.user_id ==userId).FirstOrDefault();

            if(!(callRating is null))
            {

                callRating.rate = ratings.star;
                _context.SaveChanges();
            }
            else 
            {
                _ratings.media_id = ratings.media_id;
                _ratings.movie_poster = ratings.movie_poster;
                _ratings.movie_title = ratings.movie_title;
                _ratings.user_id = userId;
                _ratings.rate = ratings.star;
                _ratings.w_date = date;

                _context.Add(_ratings);
                _context.SaveChanges();
            }

            return Ok(_ratings);
        }

        // Remove from Ratings
        [Route("Ratings")]
        [HttpPost]
        public ActionResult RemoveRating(int id)
        {
             var getRate = _context.Rating.Where(r => r.id == id).FirstOrDefault();

            _context.Rating.Remove(getRate);

            _context.SaveChanges();

            return Redirect("Home/Ratings");
        }

        // Check if Exists
        [Route("/api/AjaxAPI/CheckRating")]
        [HttpPost]
        public int CheckRating(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var getFav = _context.Favourite.Where(i => i.media_id == id && i.user_id == userId).FirstOrDefault();

            if( !(getFav is null) )
            {
                return getFav.id;
            }
            else
            {
                return 0;
            }
        }
    }
}