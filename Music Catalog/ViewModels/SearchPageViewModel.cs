using System.Collections.ObjectModel;
using System.Windows.Input;
using MusicCatalog.Models;
using System.Collections.Generic;
using System.Linq;
using MusicCatalog.Data;
using MusicCatalog.ViewModels.SearchStrategy;
using System.ComponentModel;
using System.Diagnostics;

namespace MusicCatalog.ViewModels
{
    public class SearchViewModel : INotifyPropertyChanged
    {
        private readonly MusicCatalogContext _context;
        private readonly SearchContext _searchContext;


        private SearchQuery _searchQuery; 

        public ObservableCollection<object> Items { get; set; }

        public string ArtistNameQuery
        {
            get => _searchQuery.ArtistName ?? "";
            set
            {
                _searchQuery.ArtistName = value;
                //OnPropertyChanged(nameof(ArtistNameQuery));
                DebouncedSearch();
            }
        }

        public string AlbumNameQuery
        {
            set
            {
                _searchQuery.AlbumName = value;
                //OnPropertyChanged(nameof(AlbumNameQuery));
                DebouncedSearch();
            }
        }

        public string PlaylistNameQuery
        {
            set
            {
                _searchQuery.PlaylistName = value;
                //OnPropertyChanged(nameof(PlaylistNameQuery));
                DebouncedSearch();
            }
        }

        public string SongNameQuery
        {
            set
            {
                _searchQuery.SongName = value;
                //OnPropertyChanged(nameof(SongNameQuery));
                DebouncedSearch();
            }
        }

        public string GenreQuery
        {
            set
            {
                _searchQuery.Genre = value;
                //OnPropertyChanged(nameof(GenreQuery));
                DebouncedSearch();
            }
        }

        public string YearQuery
        {
            set
            {
                _searchQuery.Year = int.TryParse(value, out int year) ? (int?)year : null;
                //OnPropertyChanged(nameof(YearQuery));
                DebouncedSearch();
            }
        }

        public double RatingQuery
        {
            set
            {
                _searchQuery.Rating = value;
                //OnPropertyChanged(nameof(RatingQuery));
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

            Items = [];

            SearchCommand = new RelayCommand(Search);
            ItemSelectedCommand = new RelayCommand(ItemSelected);

            _searchQuery = new SearchQuery();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Search(object parameter)
        {
            Debug.WriteLine("YES SEARCHING");
            Debug.WriteLine(_searchQuery.ToString());

            App.Current.Dispatcher.Invoke(() =>
            { Items.Clear(); });


            if (!string.IsNullOrWhiteSpace(_searchQuery.ArtistName))
            {
                _searchContext.SetStrategy(new ArtistSearchStrategy());
            }
            else if (!string.IsNullOrWhiteSpace(_searchQuery.AlbumName))
            {
                _searchContext.SetStrategy(new AlbumSearchStrategy());
            }
            else if (!string.IsNullOrWhiteSpace(_searchQuery.PlaylistName))
            {
                _searchContext.SetStrategy(new PlaylistSearchStrategy());
            }
            else if (!string.IsNullOrWhiteSpace(_searchQuery.SongName) || !string.IsNullOrWhiteSpace(_searchQuery.Genre) || _searchQuery.Year != null || _searchQuery.Rating >= 0)
            {
                _searchContext.SetStrategy(new SongSearchStrategy());
            }

            var results = _searchContext.ExecuteSearch(_searchQuery, _context);

            App.Current.Dispatcher.Invoke(() =>
            {
                foreach (var item in results) Items.Add(item);

                // Clear all queries after search
                //ArtistNameQuery = AlbumNameQuery = PlaylistNameQuery = SongNameQuery = GenreQuery = YearQuery = string.Empty;
                //RatingQuery = 0;
                //OnPropertyChanged(nameof(ArtistNameQuery));
                //OnPropertyChanged(nameof(AlbumNameQuery));
                //OnPropertyChanged(nameof(PlaylistNameQuery));
                //OnPropertyChanged(nameof(SongNameQuery));
                //OnPropertyChanged(nameof(GenreQuery));
                //OnPropertyChanged(nameof(YearQuery));
                //OnPropertyChanged(nameof(RatingQuery));
            });
    
            // Clear all queries after search
            //ArtistNameQuery = AlbumNameQuery = PlaylistNameQuery = SongNameQuery = GenreQuery = YearQuery = string.Empty;
            //RatingQuery = 0;
            //OnPropertyChanged(nameof(ArtistNameQuery));
            //OnPropertyChanged(nameof(AlbumNameQuery));
            //OnPropertyChanged(nameof(PlaylistNameQuery));
            //OnPropertyChanged(nameof(SongNameQuery));
            //OnPropertyChanged(nameof(GenreQuery));
            //OnPropertyChanged(nameof(YearQuery));
            //OnPropertyChanged(nameof(RatingQuery));
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