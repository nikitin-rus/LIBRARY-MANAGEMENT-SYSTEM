using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIBRARY_MANAGEMENT_SYSTEM.Entities
{
    public class User
    {
        public int Id { get; init; }
        public required string FirstName { get; init; }
        public required string SecondName { get; init; }
        public required long Phone { get; init; }
        public required string Login { get; init; }
        public required string Password { get; init; }

        public bool Validate(string login, string password)
        {
            return Login == login && Password == password;
        }
    }
}
