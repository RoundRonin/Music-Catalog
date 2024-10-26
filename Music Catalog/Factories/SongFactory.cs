using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Catalog.Factories;

public class SongFactory : ISongFactory
{
    public Music_Catalog.Models.Song CreateSong(string name, string genre, Music_Catalog.Models.Album album)
    {
        return new Music_Catalog.Models.Song
        {
            Name = name,
            Genre = genre,
            Album = album,
            Playlists = []
        };
    }
}
