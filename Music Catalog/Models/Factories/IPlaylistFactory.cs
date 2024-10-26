using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCatalog.Models.Factories;

public interface IPlaylistFactory
{
    Playlist CreatePlaylist(string name, List<Song> songs);
}

