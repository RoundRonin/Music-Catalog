using System.Windows.Controls;
using MusicCatalog.ViewModels;

namespace MusicCatalog.Views
{
    public partial class AddDataPage : Page
    {
        public AddDataPage(AddDataViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
