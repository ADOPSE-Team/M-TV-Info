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

namespace M_TV_Info.Controllers
{
    public class FavouritesController : Controller
    {
        private readonly ApplicationDbContext _context;
        // private readonly UserManager<User> _userManager;

        public FavouritesController(ApplicationDbContext context)
        {
            // _userManager = userManager;
            _context = context;

        }

        // Add To Favourites
        [Route("api/AjaxAPI/AddToFavourites")]
        [HttpPost]
        public void AddToFavourites(FavouriteModelPost item)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.FindFirstValue(ClaimTypes.Name);

            // var data = JsonConvert.DeserializeObject<FavouriteModelPost>(item.ToString());

            // User applicationUser = await _userManager.GetUserAsync(User);
            // string userEmail = applicationUser?.Email; // will give the user's Email

            FavouriteModel model = new FavouriteModel();
            DateTime date = DateTime.Now;

            model.media_id = item.media_id;
            model.movie_title = item.movie_title;
            model.movie_poster = item.movie_poster;
            model.user_id = userId;
            model.w_date = date;

            _context.Favourite.Add(model);
            _context.SaveChanges();
        }

        public void delete(FavouriteModel item)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Favourite.Remove(item);
                context.SaveChanges();
            }
        }
        
        // private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
