using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MusicCatalog.Data;
using MusicCatalog.Models;

namespace MusicCatalog.ViewModels.SearchStrategy;
using System.Diagnostics;


public class SongSearchStrategy : ISearchStrategy
{
    public IEnumerable<object> Search(SearchQuery searchQuery, MusicCatalogContext context)
    {
        Debug.WriteLine(searchQuery.ToString());
        //return context.Songs.Where(s =>  string.IsNullOrEmpty(searchQuery.SongName) || s.Name.Contains(searchQuery.SongName)
        //                                 && (s.Genre == searchQuery.Genre || string.IsNullOrEmpty(searchQuery.Genre))
        //                                 && (s.ReleaseYear > searchQuery.Year || searchQuery.Year == null)
        //                                 && (s.Rating > searchQuery.Rating || searchQuery.Rating == null)).ToList();
        return context.Songs.Where(s =>
            (string.IsNullOrEmpty(searchQuery.SongName) || s.Name.Contains(searchQuery.SongName)) &&
            (string.IsNullOrEmpty(searchQuery.Genre) || s.Genre.Contains(searchQuery.Genre)) &&
            (searchQuery.Year == null || s.ReleaseYear >= searchQuery.Year) &&
            (searchQuery.Rating == null || s.Rating >= searchQuery.Rating)
        ).ToList();
    }
}
