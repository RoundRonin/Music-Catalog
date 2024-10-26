using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCatalog.Models.Factories;

public interface IArtistFactory
{
    Artist CreateArtist(string name);
}

