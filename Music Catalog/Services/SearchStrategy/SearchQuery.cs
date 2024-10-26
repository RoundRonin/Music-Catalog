using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCatalog.Services.SearchStrategy;
public class SearchQuery
{
    public string? ArtistName { get; set; }
    public string? AlbumName { get; set; }
    public string? PlaylistName { get; set; }
    public string? SongName { get; set; }
    public string? Genre { get; set; }
    public int? Year { get; set; }
    public double? Rating { get; set; }

    public override string ToString()
    {
        return $"{ArtistName};{AlbumName};{PlaylistName};{SongName};{Genre};{Year};{Rating}";
    }
}
