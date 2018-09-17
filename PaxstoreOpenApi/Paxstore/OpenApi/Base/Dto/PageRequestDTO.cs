namespace Paxstore.OpenApi.Base.Dto{
    public class PageRequestDTO{

        public PageRequestDTO(){
            PageNo=1;
            PageSize=Constants.DEFAULT_PAGE_SIZE;
        } 
        public int PageNo{get; set;}

        public int PageSize{get; set;}

        public string OrderBy{get; set;}

    }
}