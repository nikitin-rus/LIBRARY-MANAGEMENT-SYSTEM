using LIBRARY_MANAGEMENT_SYSTEM.DataStorage;
using LIBRARY_MANAGEMENT_SYSTEM.Entities;
using LIBRARY_MANAGEMENT_SYSTEM.Modals;
using System.Collections.ObjectModel;
using System;
using System.Windows.Controls;
using LIBRARY_MANAGEMENT_SYSTEM.Helpers;
using System.Linq;
using static System.Reflection.Metadata.BlobBuilder;

namespace LIBRARY_MANAGEMENT_SYSTEM.Pages
{
    public partial class ReadersPage: Page
    {
        private ObservableCollection<Reader> Readers { get; } = [];

        public ReadersPage()
        {
            InitializeComponent();

            ObservableCollectionHelper.AddRange(Readers, Repository.Readers.ToArray());

            ReadersDataGrid.ItemsSource = Readers;
        }

        private void NavBtn_Click(object sender, EventArgs e)
        {
            App.DataGridsFrame?.Navigate(App.BooksPage);
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            AddReaderModal w = new();

            if (w.ShowDialog() == true)
            {
                Repository.Readers.Add(w.Result!);

                ObservableCollectionHelper.Update(Readers, Repository.Readers.ToArray());
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

                ObservableCollectionHelper.Update(Readers, Repository.Readers.ToArray());
            }
        }

        private void ReaderNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Readers.Clear();

            Reader[] readers = Repository.Readers.Where(reader =>
                reader.Name.Contains(ReaderNameTextBox.Text,
                    StringComparison.CurrentCultureIgnoreCase)).ToArray();

            ObservableCollectionHelper.AddRange(Readers, readers);
        }
    }
}
