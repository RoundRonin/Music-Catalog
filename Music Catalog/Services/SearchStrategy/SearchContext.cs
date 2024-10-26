using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicCatalog.Data;

namespace MusicCatalog.Services.SearchStrategy;


public class SearchContext
{
    private ISearchStrategy<object>? _strategy;

    public void SetStrategy(ISearchStrategy<object> strategy)
    {
        _strategy = strategy;
    }

    public IEnumerable<object> ExecuteSearch(SearchQuery searchQuery, MusicCatalogContext context)
    {
        if (_strategy == null) throw new ArgumentNullException(paramName: nameof(_strategy));

        try
        {
            return _strategy.Search(searchQuery, context);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return [];
        }

    }
}
