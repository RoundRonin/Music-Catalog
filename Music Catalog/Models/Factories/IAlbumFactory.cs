using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCatalog.Models.Factories;

public interface IAlbumFactory
{
    Album CreateAlbum(string name, Artist artist);
}

