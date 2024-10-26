using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MusicCatalog.Data;

namespace MusicCatalog.ViewModels.SearchStrategy;


public class SearchContext
{
    private ISearchStrategy? _strategy;

    public void SetStrategy(ISearchStrategy strategy)
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
