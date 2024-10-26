using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicCatalog.Data;
using MusicCatalog.Models;

namespace MusicCatalog.Services.SearchStrategy;

public class ArtistSearchStrategy : ISearchStrategy<Artist>
{
    public IEnumerable<Artist> Search(SearchQuery searchQuery, MusicCatalogContext context)
    {
        if (searchQuery.ArtistName == null) return context.Artists.ToList();
        return context.Artists.Where(a => a.Name.Contains(searchQuery.ArtistName)).ToList();
    }
}
