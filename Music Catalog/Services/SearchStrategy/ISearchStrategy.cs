using MusicCatalog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCatalog.Services.SearchStrategy;


public interface ISearchStrategy<T>
{
    IEnumerable<T> Search(SearchStrategy.SearchQuery query, Data.MusicCatalogContext context);
}


