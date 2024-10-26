using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MusicCatalog.Data;
using MusicCatalog.Models;

namespace MusicCatalog.ViewModels.SearchStrategy;

public class AlbumSearchStrategy : ISearchStrategy
{
    public IEnumerable<object> Search(SearchQuery searchQuery, MusicCatalogContext context)
    {
        if (searchQuery.AlbumName == null) return context.Albums.ToList();
        return context.Albums.Where(a => a.Name.Contains(searchQuery.AlbumName)).ToList();
    }
}
