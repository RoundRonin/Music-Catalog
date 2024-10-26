﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using MusicCatalog.ViewModels;

namespace MusicCatalog.Views;

/// <summary>
/// Interaction logic for SearchPage.xaml
/// </summary>
public partial class SearchPage : Window
{
    public SearchPage()
    {
        InitializeComponent();
        DataContext = new SearchViewModel();
    }
}