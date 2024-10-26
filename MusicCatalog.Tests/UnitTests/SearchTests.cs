using MusicCatalog.Data;
using MusicCatalog.Models;
using MusicCatalog.Models.Factories;
using System.Linq;
using MusicCatalog.Services.SearchStrategy;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Collections.Generic;

namespace MusicCatalog.Tests;

public class DatabaseFixture
{
    public MusicCatalogContext Context { get; private set; }

    public DatabaseFixture()
    {
        var options = new DbContextOptionsBuilder<MusicCatalogContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        Context = new MusicCatalogContext(options);
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        // Clear existing data
        Context.Artists.RemoveRange(Context.Artists);
        Context.Albums.RemoveRange(Context.Albums);
        Context.Songs.RemoveRange(Context.Songs);
        Context.Playlists.RemoveRange(Context.Playlists);
        Context.SaveChanges();

        // Factories
        var artistFactory = new ArtistFactory();
        var albumFactory = new AlbumFactory();
        var songFactory = new SongFactory();
        var playlistFactory = new PlaylistFactory();

        // Add test data using factories
        var artist1 = artistFactory.CreateArtist("Test Artist 1");
        var artist2 = artistFactory.CreateArtist("Test Artist 2");

        var album1 = albumFactory.CreateAlbum("Test Album 1", artist1);
        var album2 = albumFactory.CreateAlbum("Test Album 2", artist2);

        var song1 = songFactory.CreateSong("Test Song 1", "Rock", 4.5, 2021, album1);
        var song2 = songFactory.CreateSong("Test Song 2", "Pop", 3.0, 2020, album2);

        var playlist1 = playlistFactory.CreatePlaylist("Test Playlist 1", new List<Song> { song1, song2 });

        Context.Artists.AddRange(artist1, artist2);
        Context.Albums.AddRange(album1, album2);
        Context.Songs.AddRange(song1, song2);
        Context.Playlists.Add(playlist1);
        Context.SaveChanges();
    }
}
[CollectionDefinition("Database collection")]
public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
{
}

[Collection("Database collection")]
public class ArtistSearchStrategyTests
{
    private readonly DatabaseFixture _fixture;

    public ArtistSearchStrategyTests(DatabaseFixture fixture)
    {
        _fixture = fixture;
    }
    [Fact]
    public void Search_FindsArtistsByName()
    {
        var strategy = new ArtistSearchStrategy();
        var searchQuery = new SearchQuery()
        {
            ArtistName = "Test Artist 1"
        };
        var results = strategy.Search(searchQuery, _fixture.Context);

        Assert.Single(results);
        Assert.Equal("Test Artist 1", ((Artist)results.First()).Name);
    }
}

[Collection("Database collection")]
public class AlbumSearchStrategyTests
{
    private readonly DatabaseFixture _fixture;

    public AlbumSearchStrategyTests(DatabaseFixture fixture)
    {
        _fixture = fixture;
    }
    [Fact]
    public void Search_FindsAlbumsByName()
    {
        var strategy = new AlbumSearchStrategy();
        var searchQuery = new SearchQuery()
        {
            AlbumName = "Test Album 1"
        };
        var results = strategy.Search(searchQuery, _fixture.Context);

        Assert.Single(results);
        Assert.Equal("Test Album 1", ((Album)results.First()).Name);
    }
}

[Collection("Database collection")]
public class PlaylistSearchStrategyTests
{
    private readonly DatabaseFixture _fixture;

    public PlaylistSearchStrategyTests(DatabaseFixture fixture)
    {
        _fixture = fixture;
    }
    [Fact]
    public void Search_FindsPlaylistsByName()
    {
        var strategy = new PlaylistSearchStrategy();
        var searchQuery = new SearchQuery()
        {
            PlaylistName = "Test Playlist 1"
        };
        var results = strategy.Search(searchQuery, _fixture.Context);

        Assert.Single(results);
        Assert.Equal("Test Playlist 1", ((Playlist)results.First()).Name);
    }
}

[Collection("Database collection")]
public class SongSearchStrategyTests
{
    private readonly DatabaseFixture _fixture;

    public SongSearchStrategyTests(DatabaseFixture fixture)
    {
        _fixture = fixture;
    }
    [Fact]
    public void Search_FindsSongsByCriteria()
    {
        var strategy = new SongSearchStrategy();
        var searchQuery = new SearchQuery
        {
            SongName = "Test Song 1",
            Genre = "Rock",
            Year = 2021,
            Rating = 4.5
        };
        var results = strategy.Search(searchQuery, _fixture.Context);

        Assert.Single(results);
        Assert.Equal("Test Song 1", ((Song)results.First()).Name);
        Assert.Equal("Rock", ((Song)results.First()).Genre);
        Assert.Equal(2021, ((Song)results.First()).ReleaseYear);
        Assert.Equal(4.5, ((Song)results.First()).Rating);
    }

    [Fact]
    public void Search_DoesntFindSongsByCriteria()
    {
        var strategy = new SongSearchStrategy();
        var searchQuery = new SearchQuery
        {
            SongName = "Test Song 1",
            Genre = "Pop",
            Year = 2021,
            Rating = 4.5
        };
        var results = strategy.Search(searchQuery, _fixture.Context);

        Assert.Empty(results);
    }

}
