using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Catalog.Factories;

public interface IPlaylistFactory 
{
    Music_Catalog.Models.Playlist CreatePlaylist(string name, List<Music_Catalog.Models.Song> songs);
}

