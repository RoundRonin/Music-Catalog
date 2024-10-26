using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Music_Catalog.Entities;

namespace Music_Catalog.Data;

public class MusicCatalogContext: DbContext
{
    private readonly string? _connectionString;

    public DbSet<Artist> Artists { get; set; }
    public DbSet<Album> Albums { get; set; }
    public DbSet<Song> Songs { get; set; }
    public DbSet<Playlist> Playlists { get; set; }

    public MusicCatalogContext() 
    {
    }


    public MusicCatalogContext(DbContextOptions<MusicCatalogContext> options) 
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=yourpassword;Database=music_catalog_db");
        }
    }
    
    //public MusicCatalogContext(string connectionString) 
    //    : base() 
    //{
    //    _connectionString = connectionString;
    //}
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    if (!string.IsNullOrEmpty(_connectionString))
    //    {
    //        optionsBuilder.UseNpgsql(_connectionString);
    //    }
    //    else
    //    {
    //        // TODO kinda weird...
    //        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=yourpassword;Database=music_catalog_db");
    //    }
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Album>()
            .HasMany(a => a.Songs)
            .WithOne(s => s.Album)
            .HasForeignKey(s => s.AlbumId);

        modelBuilder.Entity<Artist>()
            .HasMany(a => a.Albums)
            .WithOne(al => al.Artist)
            .HasForeignKey(al => al.ArtistId);

        modelBuilder.Entity<Playlist>()
            .HasMany(p => p.Songs)
            .WithMany(s => s.Playlists);
    }
}
