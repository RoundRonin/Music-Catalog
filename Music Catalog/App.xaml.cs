using System.Configuration;
using System.Data;
using System.Windows;

using DotNetEnv;

namespace MusicCatalog;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        Env.Load();

        var connectionString = $"Host={Environment.GetEnvironmentVariable("HOST")};Port={Environment.GetEnvironmentVariable("PORT")};Username={Environment.GetEnvironmentVariable("POSTGRES_USER")};Password={Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")};Database={Environment.GetEnvironmentVariable("POSTGRES_DB")}";

        using (var context = new Music_Catalog.Data.MusicCatalogContext(connectionString))
        {
            // TODO test DB
        }
    }
}
