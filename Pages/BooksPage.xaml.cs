using LIBRARY_MANAGEMENT_SYSTEM.DataStorage;
using LIBRARY_MANAGEMENT_SYSTEM.Entities;
using LIBRARY_MANAGEMENT_SYSTEM.Helpers;
using LIBRARY_MANAGEMENT_SYSTEM.Modals;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace LIBRARY_MANAGEMENT_SYSTEM.Pages
{
    public partial class BooksPage : Page
    {
        private List<Book> Books { get; set; } = [];
        private ObservableCollection<Book> DisplayedBooks { get; } = [];

        public BooksPage()
        {
            InitializeComponent();

            using ApplicationContext db = new();

            Books = [.. db.Books.Include(b => b.Reader)];

            ObservableCollectionHelper.AddRange(DisplayedBooks, Books.ToArray());

            BooksDataGrid.ItemsSource = DisplayedBooks;
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            AddBookModal w = new();

            if (w.ShowDialog() == true)
            {
                using ApplicationContext db = new();

                Book book = w.Result!;

                // Обновление БД
                db.Books.Add(book);
                db.SaveChanges();

                // Обновление полученного списка книг
                Books = [.. db.Books.Include(b => b.Reader)];

                // Обновление отображаемых книг
                UpdateDisplayedBooks();
            }
        }

        private void DelBtn_Click(object sender, EventArgs e)
        {
            if (BooksDataGrid.SelectedItem is Book book)
            {
                using ApplicationContext db = new();

                Reader? reader = book.Reader;

                if (reader != null)
                {
                    reader.ReturnBook(book);
                    db.Readers.Update(reader);
                }

                // Обновление БД
                db.Books.Remove(book);
                db.SaveChanges();

                // Обновление полученного списка книг
                Books = [.. db.Books.Include(b => b.Reader)];

                // Обновление отображаемых книг
                UpdateDisplayedBooks();
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
                if (book.Reader is Reader reader)
                {
                    using ApplicationContext db = new();

                    reader.ReturnBook(book);

                    db.UpdateRange(reader, book);
                    db.SaveChanges();
                }
            }
        }

        private void BookTitleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateDisplayedBooks();
        }

        private void UpdateDisplayedBooks()
        {
            List<Book> books = CollectionHelper.GetFilteredByPropertySubstr(
                Books, nameof(Book.Title), BookTitleTextBox.Text);

            ObservableCollectionHelper.Update(DisplayedBooks, books.ToArray());
        }
    }
}
