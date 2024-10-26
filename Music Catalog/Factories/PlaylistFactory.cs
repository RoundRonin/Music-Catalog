using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Catalog.Factories;

public class PlaylistFactory : IPlaylistFactory
{
    public Music_Catalog.Models.Playlist CreatePlaylist(string name, List<Music_Catalog.Models.Song> songs)
    {
        return new Music_Catalog.Models.Playlist
        {
            Name = name,
            Songs = songs
        };
    }
}

