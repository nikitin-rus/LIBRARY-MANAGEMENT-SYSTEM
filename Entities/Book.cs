using LIBRARY_MANAGEMENT_SYSTEM.Enums;
using LIBRARY_MANAGEMENT_SYSTEM.Modals;
using System;

namespace LIBRARY_MANAGEMENT_SYSTEM.Entities
{
    public class Book : Entity
    {
        Reader? _reader = null;
        DateOnly? _returnDate = null;
        BookStatus _status = BookStatus.Stored;

        public int Id { get; init; }
        public required string Title { get; init; }
        public required string Author { get; init; }
        public required long Isbn { get; init; }
        public int? ReaderId { get; set; }
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
