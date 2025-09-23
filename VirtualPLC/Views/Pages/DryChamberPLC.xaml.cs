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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VirtualPLC.ViewModels.Pages;

namespace VirtualPLC.Views.Pages
{
    /// <summary>
    /// DryChamberPLC.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DryChamberPLC : Page
    {
        public DryChamberPLCViewModel ViewModel { get; }
        public DryChamberPLC(DryChamberPLCViewModel viewModel)
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
