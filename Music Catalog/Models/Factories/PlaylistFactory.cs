using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCatalog.Models.Factories;

public class PlaylistFactory : IPlaylistFactory
{
    public Playlist CreatePlaylist(string name, List<Song> songs)
    {
        var playlist = new Playlist
        {
            Name = name,
            Songs = songs
        };

        foreach (var song in songs)
        {
            song.Playlists.Add(playlist);
        }

        return playlist;
    }
}

