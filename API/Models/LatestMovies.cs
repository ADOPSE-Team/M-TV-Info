using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class LatestMovies
    {
        [Required]
        public string Title { get; set; }
        public int Id { get; set; }
        public string  ImdbId { get; set; }
        public DateTime ReleaseDate { get; set; }

    }
}