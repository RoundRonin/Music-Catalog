using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using MusicCatalog.Data;
using MusicCatalog.Models;
using MusicCatalog.Models.Factories;
using Microsoft.EntityFrameworkCore;

namespace MusicCatalog.Tests.IntegrationTests;

public class DatabaseTests
{
    private MusicCatalogContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<MusicCatalogContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        return new MusicCatalogContext(options);
    }

    [Fact]
    public void Test_AddAndRetrieveData()
    {
        using (var context = GetInMemoryDbContext())
        {
            var artistFactory = new ArtistFactory();
            var albumFactory = new AlbumFactory();
            var songFactory = new SongFactory();
            var playlistFactory = new PlaylistFactory();

            var artist = artistFactory.CreateArtist("Test Artist");
            var album = albumFactory.CreateAlbum("Test Album", artist);
            artist.Albums.Add(album);

            var song = songFactory.CreateSong("Test Song", "Rock", 4.5, 2021, album);
            album.Songs.Add(song);

            var playlist = playlistFactory.CreatePlaylist("Test Playlist", new List<Song> { song });
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

