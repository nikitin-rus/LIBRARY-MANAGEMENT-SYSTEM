using LIBRARY_MANAGEMENT_SYSTEM.Enums;
using LIBRARY_MANAGEMENT_SYSTEM.Modals;
using System;

namespace LIBRARY_MANAGEMENT_SYSTEM.Entities
{
    public class Book : Entity
    {
        static int count = 0;

        Reader? _reader = null;
        DateOnly? _returnDate = null;
        BookStatus _status = BookStatus.Stored;

        public int Id { get; } = count++;
        public required string Title { get; init; }
        public required string Author { get; init; }
        public required long Isbn { get; init; }
        public Reader? Reader
        {
            get => _reader;
            set
            {
                _reader = value;
                OnPropertyChanged(nameof(Reader));
            }
        }
        public DateOnly? ReturnDate
        {
            get => _returnDate;
            set
            {
                _returnDate = value;
                OnPropertyChanged(nameof(ReturnDate));
            }
        }
        public BookStatus Status 
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        } 
    }
}
