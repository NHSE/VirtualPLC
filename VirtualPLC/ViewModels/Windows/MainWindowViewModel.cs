using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using VirtualPLC.Interfaces;
using Wpf.Ui.Controls;

namespace VirtualPLC.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private readonly IPLCManager _plcManager;

        [ObservableProperty]
        private string _applicationTitle = "VirtualPLC";

        [ObservableProperty]
        private bool _connect = false;

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
        private ObservableCollection<MenuItem> _trayMenuItems = new()
        {
            new MenuItem { Header = "Home", Tag = "tray_home" }
        };

        public MainWindowViewModel(IPLCManager plcManager)
        {
            this._plcManager = plcManager;
            this._plcManager.IsConnect += OnConnect;

            Task.Run(async () => await this._plcManager.Initalize());
        }

        private void OnConnect()
        {
            this.Connect = this._plcManager.bConnect;
        }
    }
}
