using SOHU.Data.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOHU.Data.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<TreeMenuDataTransfer<T>> GenerateTree<T, K>(this IEnumerable<T> collection, Func<T, K> id, Func<T, K> parent, K root = default(K))
        {
            foreach (var item in collection.Where(c => parent(c).Equals(root)))
            {
                yield return new TreeMenuDataTransfer<T>
                {
                    Item = item,
                    Childrens = collection.GenerateTree(id, parent, id(item))
                };
            }
        }


        /// <summary>
        /// To set value to IEnumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerable<T> Do<T>(this IEnumerable<T> self, Action<T> action)
        {
            foreach (var item in self)
            {
                action(item);
                yield return item;
            }
        }
    }
}
