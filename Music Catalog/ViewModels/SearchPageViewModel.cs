using System.Collections.ObjectModel;
using System.Windows.Input;
using MusicCatalog.Models;
using System.Collections.Generic;
using System.Linq;
using MusicCatalog.Data;
using MusicCatalog.ViewModels.SearchStrategy;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Controls;

namespace MusicCatalog.ViewModels
{
    public class SearchViewModel : INotifyPropertyChanged
    {
        private readonly MusicCatalogContext _context;
        private readonly SearchContext _searchContext;


        private SearchQuery _searchQuery; 
        private ArtistSearchStrategy _artistSearchStrategy;
        private AlbumSearchStrategy _albumSearchStrategy;
        private PlaylistSearchStrategy _playlistSearchStrategy;
        private SongSearchStrategy _songSearchStrategy;

        private TabItem _selectedTabItem;
        public TabItem SelectedTabItem
        {
            get => _selectedTabItem;
            set
            {
                _selectedTabItem = value;
                Debug.WriteLine(value);
                OnPropertyChanged(nameof(SelectedTabItem));
                Items.Clear();
                AssignStrategy();
                Search(null);
            }
        }

        public ObservableCollection<object> Items { get; set; }
        public ObservableCollection<double> Ratings { get; set; }

        public string ArtistNameQuery
        {
            set
            {
                _searchQuery.ArtistName = value;
                DebouncedSearch();
            }
        }

        public string AlbumNameQuery
        {
            set
            {
                _searchQuery.AlbumName = value;
                DebouncedSearch();
            }
        }

        public string PlaylistNameQuery
        {
            set
            {
                _searchQuery.PlaylistName = value;
                DebouncedSearch();
            }
        }

        public string SongNameQuery
        {
            set
            {
                _searchQuery.SongName = value;
                DebouncedSearch();
            }
        }

        public string GenreQuery
        {
            set
            {
                _searchQuery.Genre = value;
                DebouncedSearch();
            }
        }

        public string YearQuery
        {
            set
            {
                _searchQuery.Year = int.TryParse(value, out int year) ? (int?)year : null;
                DebouncedSearch();
            }
        }

        public double RatingQuery
        {
            set
            {
                _searchQuery.Rating = double.TryParse(value.ToString(), out double rating) ? (double?)rating : null;
                DebouncedSearch();
            }
        }

        public ICommand SearchCommand { get; }
        public ICommand ItemSelectedCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public SearchViewModel()
        {
            _context = new MusicCatalogContext();
            _searchContext = new SearchContext();


            _artistSearchStrategy = new ArtistSearchStrategy();
            _albumSearchStrategy = new AlbumSearchStrategy();
            _playlistSearchStrategy = new PlaylistSearchStrategy();
            _songSearchStrategy = new SongSearchStrategy();


            Items = [];
            Ratings = new ObservableCollection<double> { 0.0, 0.5, 1.0, 1.5, 2.0, 2.5, 3.0, 3.5, 4.0, 4.5, 5.0 };

            SearchCommand = new RelayCommand(Search);
            ItemSelectedCommand = new RelayCommand(ItemSelected);

            _searchQuery = new SearchQuery();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AssignStrategy()
        {
            if (_selectedTabItem != null)
            {
                var header = _selectedTabItem.Header as string;
                switch (header)
                {
                    case "Artists":
                        _searchContext.SetStrategy(_artistSearchStrategy);
                        break;
                    case "Albums":
                        _searchContext.SetStrategy(_albumSearchStrategy);
                        break;
                    case "Playlists":
                        _searchContext.SetStrategy(_playlistSearchStrategy);
                        break;
                    case "Songs":
                        _searchContext.SetStrategy(_songSearchStrategy);
                        break;
                }
            }
        }

        private void Search(object parameter)
        {
            Debug.WriteLine("EARCHING");
            Debug.WriteLine(_searchQuery.ToString());

            App.Current.Dispatcher.Invoke(() =>
            { Items.Clear(); });

            var results = _searchContext.ExecuteSearch(_searchQuery, _context);

            App.Current.Dispatcher.Invoke(() =>
            {
                foreach (var item in results) Items.Add(item);
            });
        }


        private void ItemSelected(object parameter)
        {
            // TODO Item handling
        }

        private CancellationTokenSource _debounceToken;

        private void DebouncedSearch()
        {
            _debounceToken?.Cancel();
            _debounceToken = new CancellationTokenSource();
            var token = _debounceToken.Token;
            Task.Delay(300, token).ContinueWith(t =>
            {
                if (!t.IsCanceled)
                {
                    Search(null);
                }
            }, token);
        }
    }

}