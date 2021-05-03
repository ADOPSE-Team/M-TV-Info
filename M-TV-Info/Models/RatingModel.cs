using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace M_TV_Info.Models
{
    public class RatingModel
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public int media_id { get; set; }
        public int rate { get; set; }
        public DateTime w_date { get; set; }
    }
}
