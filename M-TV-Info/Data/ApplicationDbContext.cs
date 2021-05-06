using M_TV_Info.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace M_TV_Info.Data
{
    //public class ApplicationDbContext : IdentityDbContext
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<FavouriteModel> Favourite { get; set; }
        public DbSet<MediaModel> Media { get; set; }
        public DbSet<RatingModel> Rating { get; set; }
        public DbSet<WatchlistModel> Watchlist { get; set; }
    }
}
