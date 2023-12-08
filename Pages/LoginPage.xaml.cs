using LIBRARY_MANAGEMENT_SYSTEM.DataStorage;
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
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginUserBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (User user in Repository.Users)
            {
                if (user.Validate(LoginTextBox.Text, MyPasswordBox.Password))
                {
                    App.User = user;
                    MainPage p = new(App.User);

                    NavigationService.Navigate(p);

                    return;
                }
            }

            MessageBox.Show("Логин и/или пароль указаны неверно!");
        }
    }
}
