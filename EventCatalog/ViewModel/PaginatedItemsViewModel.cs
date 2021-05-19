using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalog.ViewModel
{
    public class PaginatedItemsViewModel<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public long Count { get; set; }

        public IEnumerable<T> Data { get; set; }
    }
}
