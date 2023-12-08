using LIBRARY_MANAGEMENT_SYSTEM.DataStorage;
using LIBRARY_MANAGEMENT_SYSTEM.Entities;
using LIBRARY_MANAGEMENT_SYSTEM.Helpers;
using LIBRARY_MANAGEMENT_SYSTEM.Pages;
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

namespace LIBRARY_MANAGEMENT_SYSTEM
{
    public partial class MainWindow : NavigationWindow
    {
        public static BooksPage BooksPage { get; set; }
        public static ReadersPage ReadersPage { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            ObservableCollectionHelper.AddRange(Repository.Books, new Book[]
            {
                new() { Title = "По ком звонит колокол", Author = "Эрнест Хемингуэй", Isbn = 9785170908318 },
                new() { Title = "Владычица озера", Author = "Анджей Сапковский", Isbn = 9785170908319 },
                new() { Title = "Преступление и наказание", Author = "Федор Достоевский", Isbn = 9785170908320 },
                new() { Title = "Сто лет одиночества", Author = "Габриэль Гарсиа Маркес", Isbn = 9785170908321 },
            });

            ObservableCollectionHelper.AddRange(Repository.Readers, [new() { Name = "Иванов Иван Иванович", Phone = 77654014102 }]);

            ObservableCollectionHelper.AddRange(Repository.Users, new User[]
            {
                new("test_login1", "test_password") { FirstName = "Руслан", SecondName = "Никитин", Phone = 75675144073 },
                new("test_login2", "test_password") { FirstName = "Иван", SecondName = "Иванов", Phone = 75675144074 },
            });

            Repository.Readers[0].BorrowBook(Repository.Books[0], DateOnly.FromDateTime(DateTime.Now));

            Navigate(App.LoginPage);
        }
    }
}
