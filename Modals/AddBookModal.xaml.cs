using LIBRARY_MANAGEMENT_SYSTEM.DataStorage;
using LIBRARY_MANAGEMENT_SYSTEM.Entities;
using System;
using System.Windows;

namespace LIBRARY_MANAGEMENT_SYSTEM
{
    public partial class AddBookModal : Window
    {
        public Book? Result { get; set; }

        public AddBookModal()
        {
            InitializeComponent();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!long.TryParse(isbnInput.Text, out long isbn))
            {
                MessageBox.Show("Введенное значение isbn должено содержать только цифры");
                return;
            }

            foreach (Book book in Repository.Books)
            {
                if (book.Isbn == isbn)
                {
                    MessageBox.Show("Книга с таким isbn уже есть в базе данных!");
                    return;
                }
            }

            Result = new()
            {
                Title = bookTitleInput.Text,
                Author = authorInput.Text,
                Isbn = isbn,
            };
            
            DialogResult = true;
        }
    }
}
