using LIBRARY_MANAGEMENT_SYSTEM.DataStorage;
using LIBRARY_MANAGEMENT_SYSTEM.Entities;
using LIBRARY_MANAGEMENT_SYSTEM.Helpers;
using System.Collections.ObjectModel;
using System;
using System.Windows.Controls;
using LIBRARY_MANAGEMENT_SYSTEM.Enums;
using System.Windows;
using LIBRARY_MANAGEMENT_SYSTEM.Modals;
using System.Linq;

namespace LIBRARY_MANAGEMENT_SYSTEM.Pages
{
    public partial class BooksPage : Page
    {
        private ObservableCollection<Book> Books { get; } = [];

        public BooksPage()
        {
            InitializeComponent();

            ObservableCollectionHelper.AddRange(Books, Repository.Books.ToArray());

            BooksDataGrid.ItemsSource = Books;
        }

        private void NavBtn_Click(object sender, EventArgs e)
        {
            App.DataGridsFrame?.Navigate(App.ReadersPage);
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            AddBookModal w = new();

            if (w.ShowDialog() == true)
            {
                Repository.Books.Add(w.Result!);

                ObservableCollectionHelper.Update(Books, Repository.Books.ToArray());
            }
        }

        private void DelBtn_Click(object sender, EventArgs e)
        {
            if (BooksDataGrid.SelectedItem is Book book)
            {
                if (book.Reader != null)
                {
                    book.Reader.ReturnBook(book);
                }

                Repository.Books.Remove(book);

                ObservableCollectionHelper.Update(Books, Repository.Books.ToArray());
            }
        }

        private void BorrowBookBtn_Click(object sender, RoutedEventArgs e)
        {
            if (BooksDataGrid.SelectedItem is Book book) 
            {
                ReaderAssignment w = new(book);

                w.Show();
            }
        }
        
        private void ReturnBookBtn_Click(object sender, RoutedEventArgs e)
        {
            if (BooksDataGrid.SelectedItem is Book book)
            {
                book.Reader?.ReturnBook(book);
            }
        }
        
        private void BookTitleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Books.Clear();

            Book[] books = Repository.Books.Where(book =>
                book.Title.Contains(BookTitleTextBox.Text,
                    StringComparison.CurrentCultureIgnoreCase)).ToArray();

            ObservableCollectionHelper.AddRange(Books, books);
        }
    }
}
