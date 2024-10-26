﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCatalog.Models.Factories;

public interface ISongFactory
{
    Song CreateSong(string name, string genre, double rating, int year, Album album, List<Playlist>? playlists = null);
}

