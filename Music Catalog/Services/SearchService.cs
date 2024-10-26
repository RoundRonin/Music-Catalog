using MusicCatalog.Services.SearchStrategy;
using MusicCatalog.Models;
using MusicCatalog.Data;

namespace MusicCatalog.Services;
public class SearchService
{
    private readonly ISearchStrategy<Artist> _artistSearchStrategy;
    private readonly ISearchStrategy<Album> _albumSearchStrategy;
    private readonly ISearchStrategy<Playlist> _playlistSearchStrategy;
    private readonly ISearchStrategy<Song> _songSearchStrategy;
    private object _currentStrategy;

    public SearchService(
        ISearchStrategy<Artist> artistSearchStrategy,
        ISearchStrategy<Album> albumSearchStrategy,
        ISearchStrategy<Playlist> playlistSearchStrategy,
        ISearchStrategy<Song> songSearchStrategy)
    {
        _artistSearchStrategy = artistSearchStrategy;
        _albumSearchStrategy = albumSearchStrategy;
        _playlistSearchStrategy = playlistSearchStrategy;
        _songSearchStrategy = songSearchStrategy;
    }

    public void SetStrategy(string tabHeader)
    {
        switch (tabHeader)
        {
            case "Artists":
                _currentStrategy = _artistSearchStrategy;
                break;
            case "Albums":
                _currentStrategy = _albumSearchStrategy;
                break;
            case "Playlists":
                _currentStrategy = _playlistSearchStrategy;
                break;
            case "Songs":
                _currentStrategy = _songSearchStrategy;
                break;
        }
    }

    public IEnumerable<object> ExecuteSearch(SearchQuery query, MusicCatalogContext context)
    {
        return _currentStrategy switch
        {
            ISearchStrategy<Artist> artistStrategy => artistStrategy.Search(query, context).Cast<object>(),
            ISearchStrategy<Album> albumStrategy => albumStrategy.Search(query, context).Cast<object>(),
            ISearchStrategy<Playlist> playlistStrategy => playlistStrategy.Search(query, context).Cast<object>(),
            ISearchStrategy<Song> songStrategy => songStrategy.Search(query, context).Cast<object>(),
            _ => Enumerable.Empty<object>()
        };
    }
}