using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MvcMusicStore.Models  
{
    public class MusicStoreEntities : IdentityDbContext {

        // Specify the name of the connections string which will be added in web.config
        public MusicStoreEntities() : base()
        {
        }
        public MusicStoreEntities(DbContextOptions<MusicStoreEntities> options) : base(options) { }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder) {
            //   dbContextOptionsBuilder.UseSqlServer(_connectionString);
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            var connectionString = configuration.GetConnectionString("MusicStoreEntitiesDataBase");
            dbContextOptionsBuilder.UseSqlServer(connectionString);
        }
    }
}