using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Catalog.Factories;

public class AlbumFactory : IAlbumFactory
{
    public Music_Catalog.Models.Album CreateAlbum(string name, Music_Catalog.Models.Artist artist)
    {
        return new Music_Catalog.Models.Album
        {
            Name = name,
            Artist = artist,
            Songs = []
        };
    }
}
