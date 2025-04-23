using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetB2BTransfer.Core.Utils
{
    public static class PaginationBuilder
    {
        public static IEnumerable<T> GetPage<T>(IEnumerable<T> collection, int pageNumber, int resultsPerPage)
        {
            int startIndex = (pageNumber - 1) * resultsPerPage;
            return collection.Skip(startIndex).Take(resultsPerPage);
        }

        public static int GetPageCount<T>(IEnumerable<T> collection, int resultsPerPage)
        {
            return (int)Math.Ceiling(collection.Count() / (double)resultsPerPage);
        }
    }
}
