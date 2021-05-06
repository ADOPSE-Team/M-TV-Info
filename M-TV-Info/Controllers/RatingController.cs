using System.Collections.Generic;
using M_TV_Info.Models;
using M_TV_Info.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace M_TV_Info.Controllers
{
    public class RatingController : Controller
    {
        public List<RatingModel> get()
        {
            var list = new List<RatingModel>();
            using (var context = new ApplicationDbContext())
            {
                list = context.Rating.ToList();
            }

            return list;
        }

        public void insert(RatingModel item)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Rating.Add(item);
                context.SaveChanges();
            }
        }

        public void update(RatingModel item)
        {
            using (var context = new ApplicationDbContext())
            {
                var existing = context.Rating.Where(x => x.id == item.id).First();
                existing = item;
                context.SaveChanges();
            }
        }

        public void delete(RatingModel item)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Rating.Remove(item);
                context.SaveChanges();
            }
        }
    }
}
