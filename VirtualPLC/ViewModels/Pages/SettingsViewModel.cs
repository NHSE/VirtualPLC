using VirtualPLC.Interfaces;
using Wpf.Ui.Abstractions.Controls;
using Wpf.Ui.Appearance;

namespace VirtualPLC.ViewModels.Pages
{
    public partial class SettingsViewModel : ObservableObject, INavigationAware
    {
        private bool _isInitialized = false;
        private readonly IConfigManager _configManager;

        [ObservableProperty]
        private string _appVersion = String.Empty;

        [ObservableProperty]
        private string _iP = String.Empty;

        [ObservableProperty]
        private int _port;

        [RelayCommand]
        private void Save()
        {
            _configManager.UpdateConfigValue("IP", this.IP);
            _configManager.UpdateConfigValue("Port", this.Port.ToString());

            _configManager.InitConfig();
        }

        public SettingsViewModel(IConfigManager configManager)
        {
            this._configManager = configManager;
            this._configManager.ConfigRead += ConfigRead;

            this._configManager.InitConfig();
        }

        private void ConfigRead()
        {
            this.IP = _configManager.IP;
            this.Port = _configManager.Port;
        }

        public Task OnNavigatedToAsync()
        {
            if (!_isInitialized)
                InitializeViewModel();

            return Task.CompletedTask;
        }

        public Task OnNavigatedFromAsync() => Task.CompletedTask;

        private void InitializeViewModel()
        {
            AppVersion = $"VirtualPLC - {GetAssemblyVersion()}";

            _isInitialized = true;
        }

        private string GetAssemblyVersion()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString()
                ?? String.Empty;
        }
    }
}
