using LIBRARY_MANAGEMENT_SYSTEM.DataStorage;
using LIBRARY_MANAGEMENT_SYSTEM.Entities;
using LIBRARY_MANAGEMENT_SYSTEM.Modals;
using System.Collections.ObjectModel;
using System;
using System.Windows.Controls;
using LIBRARY_MANAGEMENT_SYSTEM.Helpers;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace LIBRARY_MANAGEMENT_SYSTEM.Pages
{
    public partial class ReadersPage: Page
    {
        private Frame DataGridsFrame {  get; init; }
        private List<Reader> Readers { get; set; } = [];
        private ObservableCollection<Reader> DisplayedReaders { get; } = [];

        public ReadersPage(Frame dataGridsFrame)
        {
            InitializeComponent();

            DataGridsFrame = dataGridsFrame;

            using ApplicationContext db = new();

            Readers = [.. db.Readers.Include(r => r.Books)];
            ObservableCollectionHelper.AddRange(DisplayedReaders, Readers.ToArray());

            ReadersDataGrid.ItemsSource = DisplayedReaders;
        }

        private void NavBtn_Click(object sender, EventArgs e)
        {
            DataGridsFrame.Navigate(new BooksPage(DataGridsFrame));
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            AddReaderModal w = new();

            if (w.ShowDialog() == true)
            {
                using ApplicationContext db = new();

                Reader reader = w.Result!;

                // Обновление БД
                db.Readers.Add(reader);
                db.SaveChanges();

                // Обновление списка читателей
                Readers = [.. db.Readers.Include(r => r.Books)];

                // Обновление отображаемого списка читателей
                UpdateDisplayedReaders(ReaderNameTextBox.Text);
            }
        }

        private void DelBtn_Click(object sender, EventArgs e)
        {
            if (ReadersDataGrid.SelectedItem is Reader reader)
            {
                using ApplicationContext db = new();

                foreach (Book book in reader.Books.ToArray())
                {
                    reader.ReturnBook(book);
                    db.Books.Update(book);
                }

                db.Readers.Remove(reader);
                db.SaveChanges();

                // Обновление списка читателей
                Readers = [.. db.Readers.Include(r => r.Books)];

                // Обновление отображаемого списка читателей
                UpdateDisplayedReaders(ReaderNameTextBox.Text);
            }
        }

        private void ReaderNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateDisplayedReaders(ReaderNameTextBox.Text);
        }

        private void UpdateDisplayedReaders(string name)
        {
            List<Reader> readers = [];

            foreach (Reader r in Readers)
            {
                if (r.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase))
                {
                    readers.Add(r);
                }
            }

            ObservableCollectionHelper.Update(DisplayedReaders, readers.ToArray());
        }
    }
}
