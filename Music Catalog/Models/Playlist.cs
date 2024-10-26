using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace MusicCatalog.Models;

public class Playlist
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public required List<Song> Songs { get; set; }


}