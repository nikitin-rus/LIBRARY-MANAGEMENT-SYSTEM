using LIBRARY_MANAGEMENT_SYSTEM.DataStorage;
using LIBRARY_MANAGEMENT_SYSTEM.Entities;
using System.Windows;

namespace LIBRARY_MANAGEMENT_SYSTEM
{
    public partial class AddReaderModal : Window
    {
        public Reader? Result { get; set; }

        public AddReaderModal()
        {
            InitializeComponent();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            using ApplicationContext db = new();

            if (!long.TryParse(phoneInput.Text, out long phone))
            {
                MessageBox.Show("Введенное значение телефона должено содержать только цифры.");
                return;
            }

            foreach (Reader reader in db.Readers) 
            {
                if (reader.Phone == phone)
                {
                    MessageBox.Show("Читатель с таким телефоном уже есть в базе данных!");
                    return;
                }
            }

            Result = new() { Name = nameInput.Text, Phone = phone };

            DialogResult = true;
        }
    }
}
