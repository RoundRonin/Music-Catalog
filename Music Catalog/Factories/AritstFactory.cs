using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Catalog.Factories;
public class ArtistFactory : IArtistFactory
{
    public Music_Catalog.Models.Artist CreateArtist(string name)
    {
        return new Music_Catalog.Models.Artist
        {
            Name = name,
            Albums = []
        };
    }
}
