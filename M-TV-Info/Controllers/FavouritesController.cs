using System.Collections.Generic;
using M_TV_Info.Models;
using M_TV_Info.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System;

namespace M_TV_Info.Controllers
{
    public class FavouritesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FavouritesController(ApplicationDbContext context)
        {
            _context = context;

        }

        public List<FavouriteModel> get()
        {
            var list = new List<FavouriteModel>();
            using (var context = new ApplicationDbContext())
            {
                list = context.Favourite.ToList();
            }

            return list;
        }

        // Add To Favourites
        [Route("api/AjaxAPI/AddToFavourites")]
        [HttpPost]
        public void AddToFavourites(FavouriteModelPost item)
        {
            FavouriteModel model = new FavouriteModel();
            DateTime date = DateTime.Now;
            
            model.media_id = item.media_id;
            model.user_id = "1";
            model.w_date = date;

            _context.Favourite.Add(model);
            _context.SaveChanges();
        }

        public void update(FavouriteModel item)
        {
            using (var context = new ApplicationDbContext())
            {
                var existing = context.Favourite.Where(x => x.id == item.id).First();
                existing = item;
                context.SaveChanges();
            }
        }

        public void delete(FavouriteModel item)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Favourite.Remove(item);
                context.SaveChanges();
            }
        }
    }
}
