using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SOHU.Mobile.Helpers
{
    public static class EnumerableExtension
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
        {
            return new ObservableCollection<T>(source);
        }
    }
}
