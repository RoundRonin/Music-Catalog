using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MusicCatalog.Data;
using MusicCatalog.Models;

namespace MusicCatalog.ViewModels.SearchStrategy;

public class PlaylistSearchStrategy : ISearchStrategy
{
    public IEnumerable<object> Search(SearchQuery searchQuery, MusicCatalogContext context)
    {
        if (searchQuery.PlaylistName == null) return context.Playlists.ToList();
        return context.Playlists.Where(p => p.Name.Contains(searchQuery.PlaylistName)).ToList();
    }
}
