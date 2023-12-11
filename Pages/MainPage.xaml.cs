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
using LIBRARY_MANAGEMENT_SYSTEM.Entities;

namespace LIBRARY_MANAGEMENT_SYSTEM.Pages
{
    public partial class MainPage : Page
    {
        public MainPage(User user)
        {
            InitializeComponent();

            HelloTextBox.Text = $"Привет, {user.FirstName}!";

            DataGridsFrame.Navigate(new BooksPage());
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }

        private void NavigateBooks_Click(object sender, RoutedEventArgs e)
        {
            DataGridsFrame.Navigate(new BooksPage());
        }

        private void NavigateReaders_Click(object sender, RoutedEventArgs e)
        {
            DataGridsFrame.Navigate(new ReadersPage());
        }
    }
}
