using Paxstore.OpenApi.Base.Dto;
using System.Collections.Generic;


namespace Paxstore.OpenApi.Model{
    public class Result<T> {
        public int BusinessCode{get; set;}
        public string Message{get; set;}
        public IList<string> ValidationErrors{get; set;}

        public T Data{get; set;}
        public PageInfo<T> PageInfo{get; set;}

        public Result() {
		
	    }
	
        public Result(IList<string> errors) {
            BusinessCode = -1;
            ValidationErrors = errors;
        }
        
        public Result(Response<T> response) {
            BusinessCode = response.BusinessCode;
            Message = response.Message;
            Data = response.Data;
        }
        
        public Result(PageResponse<T> response) {
            BusinessCode = response.BusinessCode;
            Message = response.Message;
            PageInfo<T> pageInfo = new PageInfo<T>();
            pageInfo.DataSet = response.Dataset;
            pageInfo.HasNext = response.HasNext;
            pageInfo.Limit = response.Limit;
            pageInfo.OrderBy = response.OrderBy;
            pageInfo.PageNo = response.PageNo;
            pageInfo.TotalCount = response.TotalCount;
            PageInfo = pageInfo;
        }
        
        public Result(EmptyResponse response) {
            if(response == null) {
                BusinessCode = 0;
            }else {
                BusinessCode = response.BusinessCode;
                Message = response.Message;
            }
        }

        public override string ToString(){
            return string.Format("Result [Business code={0}, Message={1}, ValidationErrors={2}, Data={3}, PageInfo={4}]",BusinessCode, Message, 
                ValidationErrors,Data==null?"":Data.ToString(), PageInfo.ToString());
        }
    }

}