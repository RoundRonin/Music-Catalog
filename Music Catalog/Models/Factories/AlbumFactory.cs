using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCatalog.Models.Factories;

public class AlbumFactory : IAlbumFactory
{
    public Album CreateAlbum(string name, Artist artist)
    {
        return new Album
        {
            Name = name,
            Artist = artist,
            Songs = []
        };
    }
}
