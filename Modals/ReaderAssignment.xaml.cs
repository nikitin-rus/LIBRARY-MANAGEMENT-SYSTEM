﻿using LIBRARY_MANAGEMENT_SYSTEM.DataStorage;
using LIBRARY_MANAGEMENT_SYSTEM.Entities;
using LIBRARY_MANAGEMENT_SYSTEM.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace LIBRARY_MANAGEMENT_SYSTEM.Modals
{
    public partial class ReaderAssignment : Window
    {
        private Book Book { get; init; }
        private List<Reader> Readers { get; set; }
        private ObservableCollection<Reader> DisplayedReaders { get; } = [];

        public ReaderAssignment(Book book)
        {
            InitializeComponent();

            Book = book;

            using ApplicationContext db = new();

            Readers = [.. db.Readers.Include(r => r.Books)];

            // Изначальное заполение списка читателей всеми значениями
            ObservableCollectionHelper.AddRange(DisplayedReaders, Readers.ToArray());

            FoundReadersListBox.ItemsSource = DisplayedReaders;
        }

        private void FoundReadersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FoundReadersListBox.SelectedItem is Reader reader)
            {
                using ApplicationContext db = new();

                if (ReturnDatePicker.SelectedDate is null)
                {
                    MessageBox.Show("Не указана дата возврата!");
                    FoundReadersListBox.SelectedIndex = -1;
                    return;
                }

                if (ReturnDatePicker.SelectedDate is DateTime dateTime)
                {
                    reader.BorrowBook(Book, DateOnly.FromDateTime(dateTime));

                    db.UpdateRange(reader, Book);
                    db.SaveChanges();
                    Close();
                }
            }
        }

        private void ReaderNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Reader> readers = CollectionHelper.GetFilteredByPropertySubstr(
                Readers, nameof(Reader.Name), ReaderNameTextBox.Text);

            ObservableCollectionHelper.Update(DisplayedReaders, readers.ToArray());
        }
    }
}
