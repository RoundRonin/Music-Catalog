using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;

using MusicCatalog.Models;
using MusicCatalog.Models.Factories;
using MusicCatalog.Data;
using MusicCatalog.ViewModels.Utility;

namespace MusicCatalog.ViewModels
{
    public class AddDataViewModel : INotifyPropertyChanged
    {
        private readonly IArtistFactory _artistFactory;
        private readonly IAlbumFactory _albumFactory;
        private readonly ISongFactory _songFactory;
        private readonly MusicCatalogContext _context;

        public ObservableCollection<Artist> Artists { get; set; }
        public ObservableCollection<Album> Albums { get; set; }

        public string ArtistName { get; set; }
        public string AlbumName { get; set; }
        public string SongName { get; set; }
        public string Genre { get; set; }
        public string Year { get; set; }
        public string Rating { get; set; }

        private Artist _selectedArtist;
        public Artist SelectedArtist
        {
            get => _selectedArtist;
            set
            {
                _selectedArtist = value;
                OnPropertyChanged(nameof(SelectedArtist));
            }
        }

        private Album _selectedAlbum;
        public Album SelectedAlbum
        {
            get => _selectedAlbum;
            set
            {
                _selectedAlbum = value;
                OnPropertyChanged(nameof(SelectedAlbum));
            }
        }

        public ICommand AddArtistCommand { get; }
        public ICommand AddAlbumCommand { get; }
        public ICommand AddSongCommand { get; }

        public AddDataViewModel(IArtistFactory artistFactory, IAlbumFactory albumFactory, ISongFactory songFactory, MusicCatalogContext context)
        {
            _artistFactory = artistFactory;
            _albumFactory = albumFactory;
            _songFactory = songFactory;
            _context = context;

            Artists = new ObservableCollection<Artist>(_context.Artists.ToList());
            Albums = new ObservableCollection<Album>(_context.Albums.ToList());

            AddArtistCommand = new RelayCommand(AddArtist);
            AddAlbumCommand = new RelayCommand(AddAlbum);
            AddSongCommand = new RelayCommand(AddSong);
        }

          private void AddArtist(object parameter)
        {
            var artist = _artistFactory.CreateArtist(ArtistName);
            _context.Artists.Add(artist);
            _context.SaveChanges();
            ResetArtistFields();
            LoadArtists();
        }

        private void AddAlbum(object parameter)
        {
            if (SelectedArtist != null)
            {
                var album = _albumFactory.CreateAlbum(AlbumName, SelectedArtist);
                _context.Albums.Add(album);
                _context.SaveChanges();
                ResetAlbumFields();
                LoadArtists();
            }
        }

        private void AddSong(object parameter)
        {
            if (SelectedAlbum != null)
            {
                var song = _songFactory.CreateSong(
                    SongName,
                    Genre,
                    double.TryParse(Rating, out var rating) ? rating : 0,
                    int.TryParse(Year, out var year) ? year : 0,
                    SelectedAlbum
                );
                _context.Songs.Add(song);
                _context.SaveChanges();
                ResetSongFields();
                LoadArtists();
            }
        }

        private void ResetArtistFields()
        {
            ArtistName = string.Empty;
            OnPropertyChanged(nameof(ArtistName));
        }

        private void ResetAlbumFields()
        {
            AlbumName = string.Empty;
            SelectedArtist = null;
            OnPropertyChanged(nameof(AlbumName));
            OnPropertyChanged(nameof(SelectedArtist));
        }

        private void ResetSongFields()
        {
            SongName = string.Empty;
            Genre = string.Empty;
            Year = string.Empty;
            Rating = string.Empty;
            SelectedAlbum = null;
            OnPropertyChanged(nameof(SongName));
            OnPropertyChanged(nameof(Genre));
            OnPropertyChanged(nameof(Year));
            OnPropertyChanged(nameof(Rating));
            OnPropertyChanged(nameof(SelectedAlbum));
        } 
        private void LoadArtists()
        {
            Artists.Clear();
            foreach (var artist in _context.Artists.ToList())
            {
                Artists.Add(artist);
            }

            Albums.Clear();
            foreach (var album in _context.Albums.ToList())
            {
                Albums.Add(album);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
    
