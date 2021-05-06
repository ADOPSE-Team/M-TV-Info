using System.Collections.Generic;
using M_TV_Info.Models;
using M_TV_Info.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace M_TV_Info.Controllers
{
    public class WatchListController : Controller
    {
        public List<WatchlistModel> get()
        {
            var list = new List<WatchlistModel>();
            using (var context = new ApplicationDbContext())
            {
                list = context.Watchlist.ToList();
            }

            return list;
        }

        public void insert(WatchlistModel item)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Watchlist.Add(item);
                context.SaveChanges();
            }
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
