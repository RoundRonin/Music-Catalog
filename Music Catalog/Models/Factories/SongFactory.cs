using MusicCatalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCatalog.Models.Factories;

public class SongFactory : ISongFactory
{
    public Song CreateSong(string name, string genre, double rating, int releaseYear, Album album, List<Playlist>? playlists = null)
    {
        try
        {
            var song = new Song(name, genre, rating, releaseYear)
            {
                Album = album,
                Playlists = playlists ?? []
            };
            return song;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Validation Error: {ex.Message}");
            return null;
        }
    }
}
