using LIBRARY_MANAGEMENT_SYSTEM.Entities;
using LIBRARY_MANAGEMENT_SYSTEM.Pages;
using System.Windows;
using System.Windows.Controls;

namespace LIBRARY_MANAGEMENT_SYSTEM
{
    public partial class App : Application
    {
        public static User? User { get; set; }
        public static Frame? DataGridsFrame { get; set; }
        public static BooksPage? BooksPage { get; } = new();
        public static ReadersPage? ReadersPage { get; } = new();
        public static LoginPage? LoginPage { get; } = new();
    }
}
