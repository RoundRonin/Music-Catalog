using System.Configuration;
using System.Data;
using System.Windows;

using DotNetEnv;
using Microsoft.EntityFrameworkCore;

using MusicCatalog.Models;
using MusicCatalog.Models.Factories;
using MusicCatalog.Data;
using MusicCatalog.ViewModels;
using System.Diagnostics;

namespace MusicCatalog;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        Console.WriteLine("OnStartup is executing...");

        Env.Load();

        // !! Doesn't work for some reason
        var connectionString = $"Host={Environment.GetEnvironmentVariable("HOST")};Port={Environment.GetEnvironmentVariable("PORT")};Username={Environment.GetEnvironmentVariable("POSTGRES_USER")};Password={Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")};Database={Environment.GetEnvironmentVariable("POSTGRES_DB")}";

        using (var context = new MusicCatalogContext())
        {
            //RemoveData(context);
            GenerateSampleData(context);
        }

        //AddNewArtist("GenericTestovich");
    }

    public void RemoveData(MusicCatalogContext context)
    {
        // Clear existing data
        context.Artists.RemoveRange(context.Artists);
        context.Albums.RemoveRange(context.Albums);
        context.Songs.RemoveRange(context.Songs);
        context.Playlists.RemoveRange(context.Playlists);
        context.SaveChanges();
    }

    public void AddNewArtist(string artistName)
    {
        var artistFactory = new ArtistFactory();
        var albumFactory = new AlbumFactory();
        var songFactory = new SongFactory();
        var playlistFactory = new PlaylistFactory();

        var artist = artistFactory.CreateArtist(artistName);
        var album = albumFactory.CreateAlbum("New Album", artist);
        var song = songFactory.CreateSong("New Song", "Rock", 4.8, 1997, album);
        var playlist = playlistFactory.CreatePlaylist("New Playlist", [song]);

        artist.Albums.Add(album);
        album.Songs.Add(song);
        song.Playlists.Add(playlist);

        using (var context = new MusicCatalogContext())
        {
            context.Artists.Add(artist);
            context.SaveChanges();
        }
    }

    private void GenerateSampleData(MusicCatalogContext context)
    {
        if (!context.Artists.Any())
        {
            var artistFactory = new ArtistFactory();
            var albumFactory = new AlbumFactory();
            var songFactory = new SongFactory();
            var playlistFactory = new PlaylistFactory();

            var artist1 = artistFactory.CreateArtist("Artist 1");
            var artist2 = artistFactory.CreateArtist("Artist 2");
            var artist3 = artistFactory.CreateArtist("Artist 3");

            var album1 = albumFactory.CreateAlbum("Album 1", artist1);
            var album2 = albumFactory.CreateAlbum("Album 2", artist1);
            var album3 = albumFactory.CreateAlbum("Album 3", artist2);
            var album4 = albumFactory.CreateAlbum("Album 4", artist3);

            var song1 = songFactory.CreateSong("Song 1", "Pop", 4.5, 1995, album1);
            var song2 = songFactory.CreateSong("Song 2", "Rock", 4.7, 1996, album1);
            var song3 = songFactory.CreateSong("Song 3", "Jazz", 4.9, 2003, album2);
            var song4 = songFactory.CreateSong("Song 4", "Pop",  5.0, 2004, album3);
            var song5 = songFactory.CreateSong("Song 5", "Rock", 3.7, 2001, album4);
            var song6 = songFactory.CreateSong("Song 6", "Jazz", 4.9, 2019, album4);

            var playlist1 = playlistFactory.CreatePlaylist("Playlist 1", new List<Song> { song1, song2, song3 });
            var playlist2 = playlistFactory.CreatePlaylist("Playlist 2", new List<Song> { song4, song5, song6 });

            artist1.Albums.Add(album1);
            artist1.Albums.Add(album2);
            artist2.Albums.Add(album3);
            artist3.Albums.Add(album4);

            album1.Songs.Add(song1);
            album1.Songs.Add(song2);
            album2.Songs.Add(song3);
            album3.Songs.Add(song4);
            album4.Songs.Add(song5);
            album4.Songs.Add(song6);

            song1.Playlists.Add(playlist1);
            song2.Playlists.Add(playlist1);
            song3.Playlists.Add(playlist1);
            song4.Playlists.Add(playlist2);
            song5.Playlists.Add(playlist2);
            song6.Playlists.Add(playlist2);

            context.Artists.Add(artist1);
            context.Artists.Add(artist2);
            context.Artists.Add(artist3);
            context.Albums.AddRange(new List<Album> { album1, album2, album3, album4 });
            context.Songs.AddRange(new List<Song> { song1, song2, song3, song4, song5, song6 });
            context.Playlists.AddRange(new List<Playlist> { playlist1, playlist2 });

            context.SaveChanges();
        }
    }
}
