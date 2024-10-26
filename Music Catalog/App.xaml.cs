using System.Windows;

using DotNetEnv;

using MusicCatalog.Data;

//namespace MusicCatalog;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
//public partial class App : Application
//{
//    protected override void OnStartup(StartupEventArgs e)
//    {
//        base.OnStartup(e);

//        Console.WriteLine("OnStartup is executing...");

//        Env.Load();
//        var dg = new Utility.DataGen();

//        // !! Doesn't work for some reason
//        var connectionString = $"Host={Environment.GetEnvironmentVariable("HOST")};Port={Environment.GetEnvironmentVariable("PORT")};Username={Environment.GetEnvironmentVariable("POSTGRES_USER")};Password={Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")};Database={Environment.GetEnvironmentVariable("POSTGRES_DB")}";

//        using (var context = new MusicCatalogContext())
//        {
//            //RemoveData(context);
//            dg.GenerateSampleData(context);
//        }
//    }
//}

using Microsoft.Extensions.DependencyInjection;
using System.Windows;

using MusicCatalog.Views;
using MusicCatalog.Models;
using MusicCatalog.Models.Factories;
using MusicCatalog.Services;
using MusicCatalog.Services.SearchStrategy;
using MusicCatalog.ViewModels;

namespace MusicCatalog;

public partial class App : Application
{
    public new static IServiceProvider Current { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        Env.Load();
        var dg = new Utility.DataGen();

        Current = serviceCollection.BuildServiceProvider();

        using (var context = new MusicCatalogContext())
        {
            //RemoveData(context);
            dg.GenerateSampleData(context);
        }

        var mainWindow = Current.GetRequiredService<MainWindow>();
        var searchPage = Current.GetRequiredService<SearchPage>();
        var addPage = Current.GetRequiredService<AddDataPage>();
        mainWindow.Show();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<MusicCatalogContext>();
        services.AddSingleton<ISearchStrategy<Artist>, ArtistSearchStrategy>();
        services.AddSingleton<ISearchStrategy<Album>, AlbumSearchStrategy>();
        services.AddSingleton<ISearchStrategy<Playlist>, PlaylistSearchStrategy>();
        services.AddSingleton<ISearchStrategy<Song>, SongSearchStrategy>();
        services.AddSingleton<SearchService>();
        services.AddSingleton<SearchViewModel>();
        services.AddSingleton<AddDataViewModel>();
        services.AddSingleton<MainWindow>();
        services.AddSingleton<SearchPage>();
        services.AddSingleton<AddDataPage>();
        services.AddSingleton<IArtistFactory, ArtistFactory>(); // Register ArtistFactory
        services.AddSingleton<IAlbumFactory, AlbumFactory>();   // Register AlbumFactory
        services.AddSingleton<ISongFactory, SongFactory>();
    }
}