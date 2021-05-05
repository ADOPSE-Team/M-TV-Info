using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using API.Helpers;
using M_TV_Info.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using M_TV_Info.Data;

namespace M_TV_Info.Controllers
{
    public class MediaController
    {
        public List<MediaModel> get()
        {
            var list = new List<MediaModel>();
            
            using (var entities = new ApplicationDbContext())
            {
                
            }

            return list;
        }
    }
}
