using MusicCatalog.Data;
using MusicCatalog.Models;

namespace MusicCatalog.Services.SearchStrategy;
using System.Diagnostics;


public class SongSearchStrategy : ISearchStrategy<Song>
{
    public IEnumerable<Song> Search(SearchQuery searchQuery, MusicCatalogContext context)
    {
        Debug.WriteLine(searchQuery.ToString());
        return context.Songs.Where(s =>
            (string.IsNullOrEmpty(searchQuery.SongName) || s.Name.Contains(searchQuery.SongName)) &&
            (string.IsNullOrEmpty(searchQuery.Genre) || s.Genre.Contains(searchQuery.Genre)) &&
            (searchQuery.Year == null || s.ReleaseYear >= searchQuery.Year) &&
            (searchQuery.Rating == null || s.Rating >= searchQuery.Rating)
        ).ToList();
    }
}
