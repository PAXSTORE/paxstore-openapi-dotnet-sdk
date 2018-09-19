using System.Collections;
using System.Collections.Generic;

namespace Paxstore.OpenApi.Base.Dto{
    public class PageResponse<T> : BaseDTO{
        public int PageNo{get; set;}
        public int Limit{get; set;}
        public string OrderBy{get; set;}
        public long TotalCount{get; set;}
        public IList<T> Dataset{get; set;}
        public bool HasNext{get; set;}
    }
}