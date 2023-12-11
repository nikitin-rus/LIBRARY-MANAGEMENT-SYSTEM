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
            using ApplicationContext db = new();

            foreach (User user in db.Users.ToList()) 
            {
                if (user.Validate(LoginTextBox.Text, MyPasswordBox.Password) == true)
                {
                    NavigationService.Navigate(new MainPage(user));

                    return;
                }
            }

            MessageBox.Show("Логин и/или пароль указаны неверно!");
        }
    }
}
