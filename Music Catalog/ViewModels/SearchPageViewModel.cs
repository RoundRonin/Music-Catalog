using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using MusicCatalog.Data;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Controls;

using MusicCatalog.Services;
using MusicCatalog.Services.SearchStrategy;
using MusicCatalog.Utility;

namespace MusicCatalog.ViewModels;

public class SearchViewModel : INotifyPropertyChanged
{
    private readonly MusicCatalogContext _context;
    private readonly SearchService _searchService;
    private readonly DebounceHelper _debounceHelper = new DebounceHelper();
    private SearchQuery _searchQuery;

    private TabItem _selectedTabItem;
    public TabItem SelectedTabItem
    {
        get => _selectedTabItem;
        set
        {
            _selectedTabItem = value;
            _searchService.SetStrategy(value.Header as string);
            OnPropertyChanged(nameof(SelectedTabItem));
            Items.Clear();
            Search(null);
        }
    }

    public ObservableCollection<object> Items { get; set; } = new ObservableCollection<object>();
    public ObservableCollection<double> Ratings { get; set; } = new ObservableCollection<double> { 0.0, 0.5, 1.0, 1.5, 2.0, 2.5, 3.0, 3.5, 4.0, 4.5, 5.0 };

    // Property setters remain the same but use helper method...
    public string ArtistNameQuery
    {
        set => SetSearchQuery(value, newValue => _searchQuery.ArtistName = newValue);
    }

    public string AlbumNameQuery
    {
        set => SetSearchQuery(value, newValue => _searchQuery.AlbumName = newValue);
    }

    public string PlaylistNameQuery
    {
        set => SetSearchQuery(value, newValue => _searchQuery.PlaylistName = newValue);
    }

    public string SongNameQuery
    {
        set => SetSearchQuery(value, newValue => _searchQuery.SongName = newValue);
    }

    public string GenreQuery
    {
        set => SetSearchQuery(value, newValue => _searchQuery.Genre = newValue);
    }

    public string YearQuery
    {
        set => SetSearchQuery(int.TryParse(value, out int year) ? (int?)year : null, newValue => _searchQuery.Year = newValue);
    }

    public double RatingQuery
    {
        set => SetSearchQuery(double.TryParse(value.ToString(), out double rating) ? (double?)rating : null, newValue => _searchQuery.Rating = newValue);
    }


    public ICommand SearchCommand { get; }
    public ICommand ItemSelectedCommand { get; }
    public event PropertyChangedEventHandler PropertyChanged;

    public SearchViewModel(SearchService searchService, MusicCatalogContext context)
    {
        _context = context;
        _searchService = searchService;
        _searchQuery = new SearchQuery();
        SearchCommand = new RelayCommand(Search);
        ItemSelectedCommand = new RelayCommand(ItemSelected);
    }

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void Search(object parameter)
    {
        Debug.WriteLine("SEARCHING");
        Debug.WriteLine(_searchQuery.ToString());
        Application.Current.Dispatcher.Invoke(() =>
        {
            Items.Clear();
        });
        var results = _searchService.ExecuteSearch(_searchQuery, _context);
        Application.Current.Dispatcher.Invoke(() =>
        {
            foreach (var item in results)
            {
                Items.Add(item);
            }
        });
    }
    private void SetSearchQuery<T>(T value, Action<T> updateAction)
    {
        updateAction(value);
        _debounceHelper.Debounce(() => Search(null), 300);
    }

    private void ItemSelected(object parameter)
    {
        // TODO: Item handling
    }
}