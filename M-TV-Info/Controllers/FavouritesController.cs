using System.Collections.Generic;
using M_TV_Info.Models;
using M_TV_Info.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace M_TV_Info.Controllers
{
    public class FavouritesController : Controller
    {
        public List<FavouriteModel> get()
        {
            var list = new List<FavouriteModel>();
            using (var context = new ApplicationDbContext())
            {
                list = context.Favourite.ToList();
            }

            return list;
        }

        public void insert(FavouriteModel item)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Favourite.Add(item);
                context.SaveChanges();
            }
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
