using System;
using System.Collections.ObjectModel;

namespace LIBRARY_MANAGEMENT_SYSTEM.Helpers
{
    public class ObservableCollectionHelper
    {
        public static void AddRange<T>(ObservableCollection<T> collection, params T[] items)
        {
            ArgumentNullException.ThrowIfNull(collection, nameof(collection));

            foreach (T item in items) collection.Add(item);
        }

        public static void Update<T>(ObservableCollection<T> collection, params T[] items)
        {
            collection.Clear();

            AddRange(collection, items);
        }
    }
}
