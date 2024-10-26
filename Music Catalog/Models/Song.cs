using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace MusicCatalog.Models;

public class Song
{
    public int Id { get; set; }
    public int AlbumId { get; set; }
    public Album? Album { get; set; }
    public List<Playlist>? Playlists { get; set; }

    private string? _name;
    private string? _genre;
    private double? _rating;
    private int? _releaseYear;

    public string? Name 
    { 
        get => _name; 
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Name cannot be null or empty.");
            _name = value;
        }
    }

    public string? Genre 
    { 
        get => _genre; 
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Genre cannot be null or empty.");
            _genre = value;
        }
    }

    public double? Rating 
    { 
        get => _rating; 
        set
        {
            if (value < 0 || value > 5)
                throw new ArgumentOutOfRangeException("Rating must be between 0 and 5.");
            _rating = value;
        }
    }

    public int? ReleaseYear 
    { 
        get => _releaseYear; 
        set
        {
            if (value < 1850 || value > DateTime.Now.Year)
                throw new ArgumentOutOfRangeException($"Release year must be between 1850 and {DateTime.Now.Year}.");
            _releaseYear = value;
        }
    }

    public Song(string name, string genre, double? rating, int? releaseYear)
    {
        Name = name;
        Genre = genre;
        Rating = rating;
        ReleaseYear = releaseYear;
    }
}
