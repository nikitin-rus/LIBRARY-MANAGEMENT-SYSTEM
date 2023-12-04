using LIBRARY_MANAGEMENT_SYSTEM.DataStorage;
using LIBRARY_MANAGEMENT_SYSTEM.Entities;
using LIBRARY_MANAGEMENT_SYSTEM.Modals;
using System.Collections.ObjectModel;
using System;
using System.Windows.Controls;
using LIBRARY_MANAGEMENT_SYSTEM.Helpers;

namespace LIBRARY_MANAGEMENT_SYSTEM.Pages
{
    public partial class ReadersPage: Page
    {
        public ReadersPage()
        {
            InitializeComponent();

            ReadersDataGrid.ItemsSource = Repository.Readers;
        }

        private void NavBtn_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(MainWindow.BooksPage);
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            AddReaderModal w = new();

            if (w.ShowDialog() == true)
            {
                Repository.Readers.Add(w.Result!);
            }
        }

        private void DelBtn_Click(object sender, EventArgs e)
        {
            if (ReadersDataGrid.SelectedItem is Reader reader)
            {
                Repository.Readers.Remove(reader);

                foreach (Book book in reader.BookToReturnDate.Keys)
                {
                    reader.ReturnBook(book);
                }
            }
        }
    }
}
