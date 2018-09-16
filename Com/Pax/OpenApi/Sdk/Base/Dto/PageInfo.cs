using System.Collections.Generic;

namespace Com.Pax.OpenApi.Sdk.Base{
    public class PageInfo<T> {
        public int PageNo{set; get;}
        public int Limit{set; get;}
        public string OrderBy{set; get;}
        public long TotalCount{set; get;}
        public bool HasNext{set; get;}
        public IList<T> DataSet{set; get;}
        public override string ToString(){
            return string.Format("PageInfo [PageNo={0}, Limit={1}, OrderBy={2}, TotalCount={3}, HasNext={4}, DataSet={5}]",
                PageNo, Limit, OrderBy, TotalCount, HasNext, DataSet);
        }
    }
}