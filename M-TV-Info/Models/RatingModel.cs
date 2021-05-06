using System;
using System.ComponentModel.DataAnnotations;

namespace M_TV_Info.Models
{
    public class RatingModel
    {
        [Key]
        public int id { get; set; }
        public int user_id { get; set; }
        public int media_id { get; set; }
        public int rate { get; set; }
        public DateTime w_date { get; set; }
    }
}
