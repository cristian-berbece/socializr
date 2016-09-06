using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Socializr.Code.Pagination
{
    public class Pager<T>
    {
        public int CurrentPageNumber { get; set; }
        public IEnumerable<T> ElementList { get; set; }
        public int ResultsPerPage { get; set; }
        public int TotalResultNumber { get; set; }
        public int LastPageNumber
        {
            get
            {
                return (TotalResultNumber % ResultsPerPage == 0) ?  TotalResultNumber / ResultsPerPage  : TotalResultNumber / ResultsPerPage + 1; 
            }
        }

        public bool isEmptyPage()
        {
            return !ElementList.Any();
        }
    }
}