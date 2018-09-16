namespace Com.Pax.OpenApi.Sdk.Base.Dto
{
    public class PageMetadata
    {
        public PageMetadata(){

        }

        public PageMetadata(int pageSize, int pageNo){
            PageSize = pageSize;
            PageNo = pageNo;
        }
        public int PageSize{get; set;}

        public int PageNo{get; set;}
    }
}