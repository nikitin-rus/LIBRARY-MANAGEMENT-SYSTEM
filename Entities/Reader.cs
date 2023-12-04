using LIBRARY_MANAGEMENT_SYSTEM.Enums;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace LIBRARY_MANAGEMENT_SYSTEM.Entities
{
    public class Reader : Entity
    {
        static int count = 0;

        private ReaderStatus _status = ReaderStatus.WithoutBook;

        public int Id { get; } = count++;
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

        public Dictionary<Book, DateOnly> BookToReturnDate { get; } = [];

        public void BorrowBook(Book book, DateOnly returnDate)
        {
            book.Reader = this;
            book.ReturnDate = returnDate;
            book.Status = BookStatus.Borrowed;
            Status = ReaderStatus.HasBook;

            BookToReturnDate.Add(book, returnDate);
        }
        
        public void ReturnBook(Book book)
        {
            if (BookToReturnDate.Remove(book))
            {
                book.Reader = null;
                book.ReturnDate = null;
                book.Status = BookStatus.Stored;

                if (BookToReturnDate.Count < 1)
                {
                    Status = ReaderStatus.WithoutBook;
                }
            }
        }
    }
}
