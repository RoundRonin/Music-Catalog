using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Catalog.Factories;

public interface IAlbumFactory
{
    Music_Catalog.Models.Album CreateAlbum(string name, Music_Catalog.Models.Artist artist);
}

