using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIBRARY_MANAGEMENT_SYSTEM.Entities
{
    public class User
    {
        static int _count = 0;

        public int Id { get; } = _count++;
        public required string FirstName { get; init; }
        public required string SecondName { get; init; }
        public required long Phone { get; init; }
        private string Login { get; init; }
        private string Password { get; init; }

        public User(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public bool Validate(string login, string password)
        {
            return Login == login && Password == password;
        }
    }
}
