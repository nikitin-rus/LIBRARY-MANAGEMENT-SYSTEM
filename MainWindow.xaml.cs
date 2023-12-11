using LIBRARY_MANAGEMENT_SYSTEM.DataStorage;
using LIBRARY_MANAGEMENT_SYSTEM.Pages;
using System.Windows;
using System.Windows.Navigation;

namespace LIBRARY_MANAGEMENT_SYSTEM
{
    public partial class MainWindow : NavigationWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            Navigate(new LoginPage());
        }
    }
}
