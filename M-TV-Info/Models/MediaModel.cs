using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace M_TV_Info.Models
{
    public class MediaModel
    {
        [Key]
        public int id { get; set; }
        public string title { get; set; }
        public string poser_url { get; set; }
    }
}
