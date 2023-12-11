using LIBRARY_MANAGEMENT_SYSTEM.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.NetworkInformation;

namespace LIBRARY_MANAGEMENT_SYSTEM.Entities
{
    public class Reader : INotifyPropertyChanged
    {
        private ReaderStatus _status = ReaderStatus.WithoutBook;

        public int Id { get; init; }
        public required string Name { get; init; }
        public required long Phone { get; init; }
        public ReaderStatus Status 
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
        public List<Book> Books { get; } = [];

        public void BorrowBook(Book book, DateOnly returnDate)
        {
            book.Reader = this;
            book.ReturnDate = returnDate;
            book.Status = BookStatus.Borrowed;
            Status = ReaderStatus.HasBook;

            Books.Add(book);
        }
        
        public void ReturnBook(Book book)
        {
            if (Books.Remove(book) == true)
            {
                book.ReaderId = null;
                book.Reader = null;
                book.ReturnDate = null;
                book.Status = BookStatus.Stored;

                if (Books.Count < 1)
                {
                    Status = ReaderStatus.WithoutBook;
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
