using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Windows;
using MusicCatalog.Views;
using Microsoft.Extensions.DependencyInjection;

namespace MusicCatalog;

public partial class MainWindow : Window
{
    private readonly IServiceProvider _serviceProvider;

    public MainWindow(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
    }

    private void OpenSearchPage(object sender, RoutedEventArgs e)
    {
        MainFrame.Navigate(_serviceProvider.GetRequiredService<SearchPage>());
    }

    private void OpenAddDataPage(object sender, RoutedEventArgs e)
    {
        MainFrame.Navigate(_serviceProvider.GetRequiredService<AddDataPage>());
    }
}
