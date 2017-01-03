using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlowerShop.Core
{
    public enum SortDirection
    {
        Ascending,
        Descending
    }
    public class TableParam
    {
        public IEnumerable<Field> Fields { get; set; }
    }

    public class Field
    {
        public string Name { get; set; }

        public bool Sort { get; set; }

        public SortDirection SortDirection { get; set; }
    }
}