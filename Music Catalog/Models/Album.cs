using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace MusicCatalog.Models;

public class Album
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int ArtistId { get; set; }
    public required Artist Artist { get; set; }
    public required List<Song> Songs { get; set; }
}