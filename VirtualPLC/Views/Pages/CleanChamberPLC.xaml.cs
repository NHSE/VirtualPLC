using Microsoft.Extensions.DependencyInjection;
using VirtualPLC.Services;
using VirtualPLC.ViewModels.Pages;
using Wpf.Ui.Abstractions.Controls;

namespace VirtualPLC.Views.Pages
{
    public partial class CleanChamberPLC : INavigableView<CleanChamberPLCViewModel>
    {
        public CleanChamberPLCViewModel ViewModel { get; }

        public CleanChamberPLC(CleanChamberPLCViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            InitializeComponent();

            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
        }
    }
}
