using System.Windows;
using WPF.CS.ViewModels;

namespace WPF.CS
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
