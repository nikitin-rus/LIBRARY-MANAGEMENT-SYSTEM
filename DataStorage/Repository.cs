using LIBRARY_MANAGEMENT_SYSTEM.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIBRARY_MANAGEMENT_SYSTEM.DataStorage
{
    public class Repository
    {
        public static ObservableCollection<Book> Books { get; } = [];
        public static ObservableCollection<Reader> Readers { get; } = [];
    }
}
