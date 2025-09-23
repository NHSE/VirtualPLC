using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using Wpf.Ui.Controls;

namespace VirtualPLC.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _applicationTitle = "VirtualPLC";

        [ObservableProperty]
        private ObservableCollection<object> _menuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "Clean",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Cube20 },
                TargetPageType = typeof(Views.Pages.CleanChamberPLC)
            },
            new NavigationViewItem()
            {
                Content = "Dry",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Cube20 },
                TargetPageType = typeof(Views.Pages.DryChamberPLC)
            }
        };

        [ObservableProperty]
        private ObservableCollection<object> _footerMenuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "Setting",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                TargetPageType = typeof(Views.Pages.SettingsPage)
            }
        };

        [ObservableProperty]
        private ObservableCollection<MenuItem> _trayMenuItems = new()
        {
            new MenuItem { Header = "Home", Tag = "tray_home" }
        };
    }
}
