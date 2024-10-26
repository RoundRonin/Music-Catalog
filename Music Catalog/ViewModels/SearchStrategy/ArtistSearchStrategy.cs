using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MusicCatalog.Data;
using MusicCatalog.Models;

namespace MusicCatalog.ViewModels.SearchStrategy;

public class ArtistSearchStrategy : ISearchStrategy
{
    public IEnumerable<object> Search(SearchQuery searchQuery, MusicCatalogContext context)
    {
        if (searchQuery.ArtistName == null) return context.Artists.ToList();
        return context.Artists.Where(a => a.Name.Contains(searchQuery.ArtistName)).ToList();
    }
}
