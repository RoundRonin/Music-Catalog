using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using Music_Catalog.Data;
using Music_Catalog.Entities;
using Microsoft.EntityFrameworkCore;

namespace MusicCatalog.Tests.IntegrationTests;

public class DatabaseTests
{
    private MusicCatalogContext GetInMemoryDbContext()
    {
        return new MusicCatalogContext();
    }

    [Fact]
    public void Test_AddAndRetrieveData()
    {
        using (var context = GetInMemoryDbContext())
        {
            var artist = new Artist
            {
                Name = "Test Artist",
                Albums = new List<Album>()
            };

            var album = new Album
            {
                Name = "Test Album",
                Artist = artist,
                Songs = new List<Song>()
            };
            artist.Albums.Add(album);

            var song = new Song
            {
                Name = "Test Song",
                Genre = "Rock",
                Album = album,
                Playlists = new List<Playlist>()
            };
            album.Songs.Add(song);

            var playlist = new Playlist
            {
                Name = "Test Playlist",
                Songs = new List<Song> { song }
            };
            song.Playlists.Add(playlist);

            context.Artists.Add(artist);
            context.SaveChanges();

            var retrievedArtist = context.Artists
                .Include(a => a.Albums)
                .ThenInclude(al => al.Songs)
                .FirstOrDefault(a => a.Name == "Test Artist");

            Assert.NotNull(retrievedArtist);
            Assert.Equal("Test Artist", retrievedArtist.Name);
            Assert.Single(retrievedArtist.Albums);
            Assert.Equal("Test Album", retrievedArtist.Albums.First().Name);
            Assert.Single(retrievedArtist.Albums.First().Songs);
            Assert.Equal("Test Song", retrievedArtist.Albums.First().Songs.First().Name);
        }
    }
}

