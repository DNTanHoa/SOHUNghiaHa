using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Data.DataTransferObject
{
    public class TreeMenuDataTransfer<T>
    {
        public T Item { get; set; }

        public IEnumerable<TreeMenuDataTransfer<T>> Childrens { get; set; }
    }
}
