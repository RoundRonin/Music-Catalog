using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace Music_Catalog.Entities;

public class Song
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int AlbumId { get; set; }
    public string? Genre { get; set; }
    public required Album Album { get; set; }
    public required List<Playlist> Playlists { get; set; }
}