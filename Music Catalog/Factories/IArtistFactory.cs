using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Catalog.Factories;

public interface IArtistFactory
{
    Music_Catalog.Models.Artist CreateArtist(string name);
}

