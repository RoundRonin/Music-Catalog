using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

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
        public ObservableCollection<Playlist> Playlists { get; set; }
        public ObservableCollection<Song> ExistingSongs { get; set; }

        public Playlist SelectedPlaylist { get; set; }
        public Song SelectedSong { get; set; }
        public string NewPlaylistName { get; set; }


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

        public ICommand CreatePlaylistCommand { get; }
        public ICommand AddSongToPlaylistCommand { get; }
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
            Playlists = new ObservableCollection<Playlist>(_context.Playlists.ToList());
            ExistingSongs = new ObservableCollection<Song>(_context.Songs.ToList());

            AddArtistCommand = new RelayCommand(AddArtist);
            AddAlbumCommand = new RelayCommand(AddAlbum);
            AddSongCommand = new RelayCommand(AddSong);
            CreatePlaylistCommand = new RelayCommand(CreatePlaylist);
            AddSongToPlaylistCommand = new RelayCommand(AddSongToPlaylist); 
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
                try
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
                catch (ArgumentException ex)
                {
                    ShowAlert("Validation Error", ex.Message);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }
        private void CreatePlaylist(object parameter)
        {
            var playlist = new Playlist { Name = NewPlaylistName, Songs = new List<Song>() };
            _context.Playlists.Add(playlist);
            _context.SaveChanges();
            Playlists.Add(playlist);
        }

        private void AddSongToPlaylist(object parameter)
        {
            if (SelectedPlaylist != null && SelectedSong != null)
            {
                SelectedPlaylist.Songs.Add(SelectedSong);
                _context.SaveChanges();
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
        private void ResetPlaylistFields()
        {
            NewPlaylistName = string.Empty;
            OnPropertyChanged(nameof(NewPlaylistName));
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

        private void ShowAlert(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
    
