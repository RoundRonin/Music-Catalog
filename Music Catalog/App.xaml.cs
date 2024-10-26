using System.Configuration;
using System.Data;
using System.Windows;

using DotNetEnv;
using Microsoft.EntityFrameworkCore;

using Music_Catalog.Models;
using Music_Catalog.Factories;
using Music_Catalog.Data;

namespace Music_Catalog;

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

        }

        AddNewArtist("GenericTestovich");
    }

    public void AddNewArtist(string artistName)
    {
        var artistFactory = new ArtistFactory();
        var albumFactory = new AlbumFactory();
        var songFactory = new SongFactory();
        var playlistFactory = new PlaylistFactory();

        var artist = artistFactory.CreateArtist(artistName);
        var album = albumFactory.CreateAlbum("New Album", artist);
        var song = songFactory.CreateSong("New Song", "Rock", album);
        var playlist = playlistFactory.CreatePlaylist("New Playlist", new List<Song> { song });

        artist.Albums.Add(album);
        album.Songs.Add(song);
        song.Playlists.Add(playlist);

        using (var context = new MusicCatalogContext())
        {
            context.Artists.Add(artist);
            context.SaveChanges();
        }
    }

}
