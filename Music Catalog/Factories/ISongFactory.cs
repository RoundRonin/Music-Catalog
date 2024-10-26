using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Catalog.Factories;

public interface ISongFactory 
{
    Music_Catalog.Models.Song CreateSong(string name, string genre, Music_Catalog.Models.Album album);
}

