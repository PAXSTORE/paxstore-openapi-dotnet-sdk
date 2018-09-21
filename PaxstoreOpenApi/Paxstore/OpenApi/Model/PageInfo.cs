using System.Collections.Generic;

namespace Paxstore.OpenApi.Model{
    public class PageInfo<T> {
        public int PageNo{set; get;}
        public int Limit{set; get;}
        public long TotalCount{set; get;}
        public bool HasNext{set; get;}
        public IList<T> DataSet{set; get;}
        public override string ToString(){
            return string.Format("PageInfo [PageNo={0}, Limit={1}, TotalCount={2}, HasNext={3}, DataSet={4}]",
                PageNo, Limit, TotalCount, HasNext, DataSet);
        }
    }
}