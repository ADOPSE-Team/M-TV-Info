using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using API.Helpers;
using M_TV_Info.Models;
using M_TV_Info.Data;
using M_TV_Info.Migrations;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

namespace M_TV_Info.Controllers
{
    public class MediaController
    {
        public List<MediaModel> get()
        {
            var list = new List<MediaModel>();
            using (var context = new ApplicationDbContext(null))
            { 
                list = context.Media.ToList();
            }

            return list;
        }

        public void insert(MediaModel item) 
        {
            using (var context = new ApplicationDbContext(null))
            {
                context.Media.Add(item);
                context.SaveChanges();
            }
        }

        public void update(MediaModel item)
        {
            using (var context = new ApplicationDbContext(null))
            {
                var existing = context.Media.Where(x => x.id == item.id).First();
                existing = item;
                context.SaveChanges();
            }
        }

        public void delete(MediaModel item)
        {
            using (var context = new ApplicationDbContext(null))
            {
                context.Media.Remove(item);
                context.SaveChanges();
            }
        }
    }
}
