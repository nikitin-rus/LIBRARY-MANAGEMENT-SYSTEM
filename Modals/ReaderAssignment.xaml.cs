using LIBRARY_MANAGEMENT_SYSTEM.DataStorage;
using LIBRARY_MANAGEMENT_SYSTEM.Entities;
using LIBRARY_MANAGEMENT_SYSTEM.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace LIBRARY_MANAGEMENT_SYSTEM.Modals
{
    public partial class ReaderAssignment : Window
    {
        private Book Book { get; set; }
        private ObservableCollection<Reader> FoundReaders { get; } = [];

        public ReaderAssignment(Book book)
        {
            InitializeComponent();

            Book = book;

            // Изначальное заполение списка читателей всеми значениями
            ObservableCollectionHelper.AddRange(FoundReaders, Repository.Readers.ToArray());

            FoundReadersListBox.ItemsSource = FoundReaders;
        }

        private void ReaderNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FoundReaders.Clear();

            foreach (Reader r in Repository.Readers.Where(reader =>
                reader.Name.Contains(ReaderNameTextBox.Text,
                    StringComparison.CurrentCultureIgnoreCase)))
            {
                FoundReaders.Add(r);
            }
        }

        private void FoundReadersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FoundReadersListBox.SelectedItem is Reader reader)
            {
                if (ReturnDatePicker.SelectedDate is null)
                {
                    MessageBox.Show("Не указана дата возврата!");
                    FoundReadersListBox.SelectedIndex = -1;
                    return;
                }

                if (ReturnDatePicker.SelectedDate is DateTime dateTime)
                {
                    reader.BorrowBook(Book, DateOnly.FromDateTime(dateTime));
                    Close();
                    return;
                }
            }
        }
    }
}
